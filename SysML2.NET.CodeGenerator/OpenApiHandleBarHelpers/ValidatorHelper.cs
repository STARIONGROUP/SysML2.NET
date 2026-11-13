// ------------------------------------------------------------------------------------------------
// <copyright file="ValidatorHelper.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.CodeGenerator.OpenApiHandleBarHelpers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using HandlebarsDotNet;

    using Microsoft.OpenApi;

    using SysML2.NET.CodeGenerator.Extensions;

    /// <summary>
    /// A handlebars block helper that writes the FluentValidation rules derived from a request <see cref="IOpenApiSchema"/>
    /// </summary>
    public static class ValidatorHelper
    {
        /// <summary>
        /// Registers the <see cref="ValidatorHelper"/>
        /// </summary>
        /// <param name="handlebars">
        /// The <see cref="IHandlebars"/> context with which the helper needs to be registered
        /// </param>
        /// <param name="queryHasValidator">
        /// A function that returns whether a validator is generated for a schema name
        /// </param>
        public static void RegisterValidatorHelper(this IHandlebars handlebars, Func<string, bool> queryHasValidator)
        {
            ArgumentNullException.ThrowIfNull(handlebars);
            ArgumentNullException.ThrowIfNull(queryHasValidator);

            handlebars.RegisterHelper("Validator.WriteRules", (writer, _, arguments) =>
            {
                if (arguments.Length != 2)
                {
                    throw new HandlebarsException("{{#Validator.WriteRules}} helper must have exactly two arguments");
                }

                var className = arguments[0] as string ?? throw new ArgumentException("supposed to be a schema name");
                var classSchema = arguments[1] as IOpenApiSchema ?? throw new ArgumentException("supposed to be IOpenApiSchema");

                foreach (var rule in QueryRules(className, classSchema, queryHasValidator))
                {
                    writer.WriteSafeString(rule + Environment.NewLine);
                }
            });
        }

        /// <summary>
        /// Queries the rule statements derived from every property of the class schema except its type discriminator
        /// </summary>
        /// <param name="className">The name of the class that the validator validates.</param>
        /// <param name="classSchema">The <see cref="IOpenApiSchema"/> of the class.</param>
        /// <param name="queryHasValidator">A function that returns whether a validator is generated for a schema name.</param>
        /// <returns>The rule statements, ordered by property name.</returns>
        private static List<string> QueryRules(string className, IOpenApiSchema classSchema, Func<string, bool> queryHasValidator)
        {
            return (classSchema.Properties ?? new Dictionary<string, IOpenApiSchema>())
                .Where(property => property.Key != OpenApiSchemaExtensions.TypeDiscriminatorPropertyName)
                .OrderBy(property => property.Key, StringComparer.Ordinal)
                .SelectMany(property => QueryPropertyRules(className, classSchema, property.Key, property.Value, queryHasValidator))
                .ToList();
        }

        /// <summary>
        /// Queries the rule statements derived from one property of the class schema
        /// </summary>
        /// <param name="className">The name of the class that declares the property.</param>
        /// <param name="classSchema">The <see cref="IOpenApiSchema"/> of the class.</param>
        /// <param name="propertyName">The name of the property as it appears in the schema.</param>
        /// <param name="propertySchema">The <see cref="IOpenApiSchema"/> of the property.</param>
        /// <param name="queryHasValidator">A function that returns whether a validator is generated for a schema name.</param>
        /// <returns>The rule statements.</returns>
        private static IEnumerable<string> QueryPropertyRules(string className, IOpenApiSchema classSchema, string propertyName, IOpenApiSchema propertySchema, Func<string, bool> queryHasValidator)
        {
            var selector = $"dto => dto.{OpenApiSchemaExtensions.QueryPropertyName(propertyName)}";
            var isPresenceRequired = classSchema.Required?.Contains(propertyName) == true && !propertySchema.QueryAdmitsNull();
            var isCollection = propertySchema.QueryIsCollection();
            var coreTypeName = propertySchema.QueryCoreTypeName(className, propertyName);

            if (coreTypeName == "Guid" && !isCollection)
            {
                yield return isPresenceRequired
                    ? $"this.RuleFor({selector}).NotEmpty();"
                    : $"this.RuleFor({selector}).Must(value => !value.HasValue || value.Value != Guid.Empty);";
            }
            else if (coreTypeName == "string" && !isCollection && isPresenceRequired)
            {
                yield return $"this.RuleFor({selector}).NotEmpty();";
            }
            else if (isPresenceRequired && (isCollection || !propertySchema.QueryIsValueType()))
            {
                yield return $"this.RuleFor({selector}).NotNull();";
            }

            if (coreTypeName == OpenApiSchemaExtensions.QueryEnumerationTypeName(className, propertyName))
            {
                yield return $"this.RuleFor({selector}).IsInEnum();";
            }

            if (propertySchema.QueryMinItems() is { } minItems)
            {
                yield return $"this.RuleFor({selector}).Must(value => value == null || value.Count >= {minItems});";
            }

            if (coreTypeName == "Uri")
            {
                yield return $"this.RuleFor({selector}).Must(value => value == null || value.IsAbsoluteUri);";
            }

            var ruleName = isCollection ? "RuleForEach" : "RuleFor";
            var itemGuard = isCollection ? ".NotNull()" : string.Empty;

            if (propertySchema.QueryIsCompositeReference())
            {
                var referencedName = propertySchema.QueryTerminalReferenceName();

                if (queryHasValidator(referencedName))
                {
                    yield return $"this.{ruleName}({selector}){itemGuard}.SetValidator(new {referencedName}Validator());";
                }
            }
            else if (propertySchema.QueryIsPolymorphicReference())
            {
                var alternativeNames = propertySchema.QueryTerminalReference().QueryUnionAlternativeNames()
                    .Where(queryHasValidator)
                    .ToList();

                if (alternativeNames.Count > 0)
                {
                    var registrations = string.Concat(alternativeNames.Select(alternativeName => $"validators.Add<{alternativeName}>(new {alternativeName}Validator());{Environment.NewLine}"));

                    yield return $"this.{ruleName}({selector}){itemGuard}.SetInheritanceValidator(validators =>{Environment.NewLine}{{{Environment.NewLine}{registrations}}});";
                }
                else if (isCollection)
                {
                    yield return $"this.RuleForEach({selector}).NotNull();";
                }
            }
        }
    }
}
