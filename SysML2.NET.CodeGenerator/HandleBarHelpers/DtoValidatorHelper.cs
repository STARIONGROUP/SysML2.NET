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

            return @class.QueryAllProperties()
                .Where(property => !property.IsDerived && !property.IsDerivedUnion && !property.TryQueryRedefinedByProperty(@class, out _))
                .OrderBy(property => property.Name.CapitalizeFirstLetter(), StringComparer.Ordinal)
                .SelectMany(QueryPropertyRules)
                .ToList();
        }

        /// <summary>
        /// Queries the rule statements derived from the multiplicity and the type of one property
        /// </summary>
        /// <param name="property">The non-derived <see cref="IProperty"/>.</param>
        /// <returns>The rule statements.</returns>
        private static IEnumerable<string> QueryPropertyRules(IProperty property)
        {
            var selector = $"dto => dto.{property.Name.CapitalizeFirstLetter()}";

            return property.QueryIsEnumerable()
                ? QueryCollectionRules(property, selector)
                : QueryScalarRules(property, selector);
        }

        /// <summary>
        /// Queries the rule statements of a property whose upper bound is greater than one
        /// </summary>
        /// <param name="property">The collection <see cref="IProperty"/>.</param>
        /// <param name="selector">The lambda that selects the property on the DTO.</param>
        /// <returns>The rule statements.</returns>
        private static IEnumerable<string> QueryCollectionRules(IProperty property, string selector)
        {
            yield return $"this.RuleFor({selector}).NotNull();";

            if (property.Lower > 0)
            {
                yield return $"this.RuleFor({selector}).Must(value => value == null || value.Count >= {property.Lower});";
            }

            var upper = property.QueryUpperValue();

            if (upper != int.MaxValue)
            {
                yield return $"this.RuleFor({selector}).Must(value => value == null || value.Count <= {upper});";
            }

            if (property.IsUnique)
            {
                yield return $"this.RuleFor({selector}).Must(value => value == null || value.Distinct().Count() == value.Count);";
            }

            var itemRule = property.Type switch
            {
                IEnumeration => "IsInEnum()",
                IDataType => property.QueryIsString() ? "NotNull()" : null,
                _ => "NotEmpty()"
            };

            if (itemRule != null)
            {
                yield return $"this.RuleForEach({selector}).{itemRule};";
            }
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
                    yield return property.QueryIsNullableAndNotString()
                        ? $"this.RuleFor({selector}).Must(value => !value.HasValue || value.Value != System.Guid.Empty);"
                        : $"this.RuleFor({selector}).NotEmpty();";
                    break;
            }
        }
    }
}
