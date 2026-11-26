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
        /// The suffix that lowers a rule inferred from a required derived property to a warning
        /// </summary>
        private const string WarningSeverity = ".WithSeverity(Severity.Warning)";

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
                .Select(property => (property.Lower, SupersetIds: QueryTransitiveIds(property, current => current.RedefinedProperty.Concat(current.SubsettedProperty))))
                .ToList();

            return properties
                .Where(property => !property.IsDerived && !property.IsDerivedUnion)
                .OrderBy(property => property.Name.CapitalizeFirstLetter(), StringComparer.Ordinal)
                .SelectMany(property => QueryPropertyRules(property, QueryEffectiveLower(property, requiredDerivedProperties)))
                .ToList();
        }

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
        /// Queries the single rule chain that enforces the lower and upper bounds of a collection
        /// </summary>
        /// <param name="lower">The lower bound of the collection.</param>
        /// <param name="upper">The upper bound of the collection, <see cref="int.MaxValue"/> when unbounded.</param>
        /// <param name="isLowerInferred">Whether the lower bound is raised by a required derived property, which reports its violation as a warning.</param>
        /// <returns>The rule chain appended to <c>RuleFor</c>, or <c>null</c> when the bounds demand nothing.</returns>
        internal static string QueryCollectionCountRule(int lower, int upper, bool isLowerInferred)
        {
            var isBounded = upper != int.MaxValue;

            if (lower == 0)
            {
                return isBounded ? $".Must(value => value == null || value.Count <= {upper})" : null;
            }

            var lowerSeverity = isLowerInferred ? WarningSeverity : string.Empty;

            List<string> validators = [".NotEmpty()" + lowerSeverity];

            if (lower > 1)
            {
                validators.Add($".Must(value => value.Count >= {lower})" + lowerSeverity);
            }

            if (isBounded)
            {
                validators.Add($".Must(value => value.Count <= {upper})");
            }

            var chain = string.Concat(validators);

            return validators.Count > 1 ? ".Cascade(CascadeMode.Stop)" + chain : chain;
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
            var countRule = QueryCollectionCountRule(lower, property.QueryUpperValue(), lower > property.Lower);

            if (countRule != null)
            {
                yield return $"this.RuleFor({selector}){countRule};";
            }
        }

        /// <summary>
        /// Queries the lower bound of a non-derived property, raised by every required derived property that subsets or redefines it
        /// </summary>
        /// <param name="property">The non-derived <see cref="IProperty"/>.</param>
        /// <param name="requiredDerivedProperties">The lower bound and the superset ids of each required derived property of the class.</param>
        /// <returns>The effective lower bound.</returns>
        private static int QueryEffectiveLower(IProperty property, List<(int Lower, HashSet<string> SupersetIds)> requiredDerivedProperties)
        {
            var redefinitionChainIds = QueryTransitiveIds(property, current => current.RedefinedProperty);

            return requiredDerivedProperties
                .Where(derived => derived.SupersetIds.Overlaps(redefinitionChainIds))
                .Select(derived => derived.Lower)
                .Append(property.Lower)
                .Max();
        }

        /// <summary>
        /// Queries the rule statements derived from the multiplicity and the type of one property
        /// </summary>
        /// <param name="property">The non-derived <see cref="IProperty"/>.</param>
        /// <param name="lower">The effective lower bound of the property.</param>
        /// <returns>The rule statements.</returns>
        private static IEnumerable<string> QueryPropertyRules(IProperty property, int lower)
        {
            var selector = $"dto => dto.{property.Name.CapitalizeFirstLetter()}";

            return property.QueryIsEnumerable()
                ? QueryCollectionRules(property, lower, selector)
                : QueryScalarRules(property, lower, selector);
        }

        /// <summary>
        /// Queries the rule statements of a property whose upper bound is one
        /// </summary>
        /// <param name="property">The scalar <see cref="IProperty"/>.</param>
        /// <param name="lower">The effective lower bound of the property.</param>
        /// <param name="selector">The lambda that selects the property on the DTO.</param>
        /// <returns>The rule statements.</returns>
        private static IEnumerable<string> QueryScalarRules(IProperty property, int lower, string selector)
        {
            var severity = lower > property.Lower ? WarningSeverity : string.Empty;

            switch (property.Type)
            {
                case IEnumeration:
                    yield return $"this.RuleFor({selector}).IsInEnum();";
                    break;
                case IDataType when property.QueryIsString() && lower > 0:
                    yield return $"this.RuleFor({selector}).NotNull(){severity};";
                    break;
                case IDataType:
                    break;
                default:
                    if (lower > 0)
                    {
                        yield return $"this.RuleFor({selector}).NotEmpty(){severity};";
                    }

                    break;
            }
        }

        /// <summary>
        /// Queries the ids of the property and of every property transitively reached through the given edges
        /// </summary>
        /// <param name="property">The <see cref="IProperty"/> to start from.</param>
        /// <param name="next">The edges followed from each visited property.</param>
        /// <returns>The ids of the property and of every property reached.</returns>
        private static HashSet<string> QueryTransitiveIds(IProperty property, Func<IProperty, IEnumerable<IProperty>> next)
        {
            HashSet<string> visited = [property.XmiId];
            var queue = new Queue<IProperty>([property]);

            while (queue.Count > 0)
            {
                foreach (var reached in next(queue.Dequeue()).Where(reached => visited.Add(reached.XmiId)))
                {
                    queue.Enqueue(reached);
                }
            }

            return visited;
        }
    }
}
