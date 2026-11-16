// ------------------------------------------------------------------------------------------------
// <copyright file="DtoValidatorHelper.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.CodeGenerator.HandleBarHelpers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using HandlebarsDotNet;

    using SysML2.NET.CodeGenerator.Extensions;

    using uml4net.Classification;
    using uml4net.Extensions;
    using uml4net.SimpleClassifiers;
    using uml4net.StructuredClassifiers;

    /// <summary>
    /// A handlebars block helper that writes the FluentValidation rules derived from the non-derived properties of an <see cref="IClass"/>
    /// </summary>
    public static class DtoValidatorHelper
    {
        /// <summary>
        /// Registers the <see cref="DtoValidatorHelper"/>
        /// </summary>
        /// <param name="handlebars">
        /// The <see cref="IHandlebars"/> context with which the helper needs to be registered
        /// </param>
        public static void RegisterDtoValidatorHelper(this IHandlebars handlebars)
        {
            ArgumentNullException.ThrowIfNull(handlebars);

            handlebars.RegisterHelper("DtoValidator.WriteRules", (writer, _, arguments) =>
            {
                if (arguments.Length != 1)
                {
                    throw new HandlebarsException("{{#DtoValidator.WriteRules}} helper must have exactly one argument");
                }

                var @class = arguments[0] as IClass ?? throw new ArgumentException("supposed to be IClass");

                foreach (var rule in QueryRules(@class))
                {
                    writer.WriteSafeString(rule + Environment.NewLine);
                }
            });
        }

        /// <summary>
        /// Queries the rule statements derived from every non-derived property of the class that is not redefined in it
        /// </summary>
        /// <param name="class">The <see cref="IClass"/> that the validator validates.</param>
        /// <returns>The rule statements, ordered by property name.</returns>
        public static List<string> QueryRules(IClass @class)
        {
            ArgumentNullException.ThrowIfNull(@class);

            var properties = @class.QueryAllProperties()
                .Where(property => !property.TryQueryRedefinedByProperty(@class, out _))
                .ToList();

            var requiredDerivedProperties = properties
                .Where(property => (property.IsDerived || property.IsDerivedUnion) && property.Lower > 0)
                .Select(property => (property.Lower, SupersetIds: QuerySupersetIds(property)))
                .ToList();

            return properties
                .Where(property => !property.IsDerived && !property.IsDerivedUnion)
                .OrderBy(property => property.Name.CapitalizeFirstLetter(), StringComparer.Ordinal)
                .SelectMany(property => QueryPropertyRules(property, QueryEffectiveLower(property, requiredDerivedProperties)))
                .ToList();
        }

        /// <summary>
        /// Queries the lower bound of a non-derived property, raised by every required derived property that subsets or redefines it
        /// </summary>
        /// <param name="property">The non-derived <see cref="IProperty"/>.</param>
        /// <param name="requiredDerivedProperties">The lower bound and the superset ids of each required derived property of the class.</param>
        /// <returns>The effective lower bound.</returns>
        private static int QueryEffectiveLower(IProperty property, List<(int Lower, HashSet<string> SupersetIds)> requiredDerivedProperties)
        {
            var redefinitionChainIds = QueryRedefinitionChainIds(property);

            return requiredDerivedProperties
                .Where(derived => derived.SupersetIds.Overlaps(redefinitionChainIds))
                .Select(derived => derived.Lower)
                .Append(property.Lower)
                .Max();
        }

        /// <summary>
        /// Queries the ids of the property and of every property it transitively subsets or redefines
        /// </summary>
        /// <param name="property">The <see cref="IProperty"/> to start from.</param>
        /// <returns>The ids of the property and its supersets.</returns>
        private static HashSet<string> QuerySupersetIds(IProperty property)
        {
            var visited = new HashSet<string> { property.XmiId };
            var queue = new Queue<IProperty>([property]);

            while (queue.Count > 0)
            {
                var current = queue.Dequeue();

                foreach (var superset in current.RedefinedProperty.Concat(current.SubsettedProperty).Where(superset => visited.Add(superset.XmiId)))
                {
                    queue.Enqueue(superset);
                }
            }

            return visited;
        }

        /// <summary>
        /// Queries the ids of the property and of every property it transitively redefines
        /// </summary>
        /// <param name="property">The <see cref="IProperty"/> to start from.</param>
        /// <returns>The ids of the property and of the properties it redefines.</returns>
        private static HashSet<string> QueryRedefinitionChainIds(IProperty property)
        {
            var visited = new HashSet<string> { property.XmiId };
            var queue = new Queue<IProperty>([property]);

            while (queue.Count > 0)
            {
                var current = queue.Dequeue();

                foreach (var redefined in current.RedefinedProperty.Where(redefined => visited.Add(redefined.XmiId)))
                {
                    queue.Enqueue(redefined);
                }
            }

            return visited;
        }

        /// <summary>
        /// Queries the rule statements derived from the multiplicity and the type of one property
        /// </summary>
        /// <param name="property">The non-derived <see cref="IProperty"/>.</param>
        /// <param name="collectionLower">The effective lower bound applied when the property is a collection.</param>
        /// <returns>The rule statements.</returns>
        private static IEnumerable<string> QueryPropertyRules(IProperty property, int collectionLower)
        {
            var selector = $"dto => dto.{property.Name.CapitalizeFirstLetter()}";

            return property.QueryIsEnumerable()
                ? QueryCollectionRules(property, collectionLower, selector)
                : QueryScalarRules(property, selector);
        }

        /// <summary>
        /// Queries the rule statements of a property whose upper bound is greater than one
        /// </summary>
        /// <param name="property">The collection <see cref="IProperty"/>.</param>
        /// <param name="lower">The effective lower bound of the property.</param>
        /// <param name="selector">The lambda that selects the property on the DTO.</param>
        /// <returns>The rule statements.</returns>
        private static IEnumerable<string> QueryCollectionRules(IProperty property, int lower, string selector)
        {
            var countRule = QueryCollectionCountRule(lower, property.QueryUpperValue());

            if (countRule != null)
            {
                yield return $"this.RuleFor({selector}){countRule};";
            }
        }

        /// <summary>
        /// Queries the single rule chain that enforces the lower and upper bounds of a collection
        /// </summary>
        /// <param name="lower">The lower bound of the collection.</param>
        /// <param name="upper">The upper bound of the collection, <see cref="int.MaxValue"/> when unbounded.</param>
        /// <returns>The rule chain appended to <c>RuleFor</c>, or <c>null</c> when the bounds demand nothing.</returns>
        private static string QueryCollectionCountRule(int lower, int upper)
        {
            var isBounded = upper != int.MaxValue;

            if (lower == 0)
            {
                return isBounded ? $".Must(value => value == null || value.Count <= {upper})" : null;
            }

            var chain = ".Cascade(CascadeMode.Stop).NotEmpty()";

            if (lower > 1)
            {
                chain += $".Must(value => value.Count >= {lower})";
            }

            if (isBounded)
            {
                chain += $".Must(value => value.Count <= {upper})";
            }

            return chain;
        }

        /// <summary>
        /// Queries the rule statements of a property whose upper bound is one
        /// </summary>
        /// <param name="property">The scalar <see cref="IProperty"/>.</param>
        /// <param name="selector">The lambda that selects the property on the DTO.</param>
        /// <returns>The rule statements.</returns>
        private static IEnumerable<string> QueryScalarRules(IProperty property, string selector)
        {
            switch (property.Type)
            {
                case IEnumeration:
                    yield return $"this.RuleFor({selector}).IsInEnum();";
                    break;
                case IDataType when property.QueryIsString() && property.Lower > 0:
                    yield return $"this.RuleFor({selector}).NotNull();";
                    break;
                case IDataType:
                    break;
                default:
                    if (property.Lower > 0)
                    {
                        yield return $"this.RuleFor({selector}).NotEmpty();";
                    }

                    break;
            }
        }
    }
}
