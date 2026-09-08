// -------------------------------------------------------------------------------------------------
// <copyright file="DocumentationHelper.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//
//   Licensed under the Apache License, Version 2.0 (the "License");
//   you may not use this file except in compliance with the License.
//   You may obtain a copy of the License at
//
//        http://www.apache.org/licenses/LICENSE-2.0
//
//    Unless required by applicable law or agreed to in writing, software
//    distributed under the License is distributed on an "AS IS" BASIS,
//    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//    See the License for the specific language governing permissions and
//    limitations under the License.
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.CodeGenerator.OpenApiHandleBarHelpers
{
    using System;

    using HandlebarsDotNet;

    using Microsoft.OpenApi;

    using SysML2.NET.CodeGenerator.Extensions;

    /// <summary>
    /// A handlebars block helper that writes the XML documentation of generated members
    /// </summary>
    public static class DocumentationHelper
    {
        /// <summary>
        /// Registers the <see cref="DocumentationHelper"/>
        /// </summary>
        /// <param name="handlebars">
        /// The <see cref="IHandlebars"/> context with which the helper needs to be registered
        /// </param>
        public static void RegisterDocumentationHelper(this IHandlebars handlebars)
        {
            ArgumentNullException.ThrowIfNull(handlebars);

            handlebars.RegisterHelper("Documentation.WriteForProperty", (writer, _, arguments) =>
            {
                if (arguments.Length != 3)
                {
                    throw new HandlebarsException("{{#Documentation.WriteForProperty}} helper must have exactly three arguments");
                }

                var propertyName = arguments[0] as string ?? throw new ArgumentException("supposed to be a property name");
                var className = arguments[1] as string ?? throw new ArgumentException("supposed to be a schema name");
                var classSchema = arguments[2] as IOpenApiSchema ?? throw new ArgumentException("supposed to be IOpenApiSchema");

                var propertySchema = classSchema.Properties[propertyName];

                writer.WriteSafeString(WriteSummary(QueryPropertySummary(propertySchema, propertyName, className)));
            });

            handlebars.RegisterHelper("Documentation.WriteForEnumerationLiteral", (writer, _, arguments) =>
            {
                if (arguments.Length != 1)
                {
                    throw new HandlebarsException("{{#Documentation.WriteForEnumerationLiteral}} helper must have exactly one argument");
                }

                var enumerationValue = arguments[0] as string ?? throw new ArgumentException("supposed to be an enumeration value");

                writer.WriteSafeString(WriteSummary($"The \"{Escape(enumerationValue)}\" value"));
            });
        }

        /// <summary>
        /// Queries the summary of a generated property
        /// </summary>
        /// <param name="propertySchema">The <see cref="IOpenApiSchema"/> that defines the property.</param>
        /// <param name="propertyName">The name of the property as it appears in the schema.</param>
        /// <param name="className">The name of the class that declares the property.</param>
        /// <returns>The summary text.</returns>
        private static string QueryPropertySummary(IOpenApiSchema propertySchema, string propertyName, string className)
        {
            if (!string.IsNullOrWhiteSpace(propertySchema.Description))
            {
                return Escape(propertySchema.Description);
            }

            if (propertyName == OpenApiSchemaExtensions.IdentifierPropertyName)
            {
                return $"Gets or sets the unique identifier of the {className}";
            }

            var referencedSchemaName = propertySchema.QueryReferencedSchemaName();

            if (propertySchema.QueryCoreTypeName(className, propertyName) != "Guid" || string.IsNullOrWhiteSpace(referencedSchemaName))
            {
                return $"Gets or sets the {Escape(propertyName)} of the {className}";
            }

            return propertySchema.QueryIsCollection()
                ? $"Gets or sets the unique identifiers of the referenced {referencedSchemaName} instances"
                : $"Gets or sets the unique identifier of the referenced {referencedSchemaName}";
        }

        /// <summary>
        /// Writes a summary XML documentation block
        /// </summary>
        /// <param name="summary">The summary text.</param>
        /// <returns>The documentation block.</returns>
        private static string WriteSummary(string summary)
        {
            return $"/// <summary>{Environment.NewLine}/// {summary}{Environment.NewLine}/// </summary>{Environment.NewLine}";
        }

        /// <summary>
        /// Escapes the characters that may not appear unescaped in XML documentation
        /// </summary>
        /// <param name="value">The text to escape.</param>
        /// <returns>The escaped text.</returns>
        private static string Escape(string value)
        {
            return value.Replace("&", "&amp;").Replace("<", "&lt;").Replace(">", "&gt;");
        }
    }
}
