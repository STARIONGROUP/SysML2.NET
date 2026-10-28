// ------------------------------------------------------------------------------------------------
// <copyright file="PropertyHelper.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.CodeGenerator.OpenApiHandleBarHelpers
{
    using System;
    using System.Text;

    using HandlebarsDotNet;

    using Microsoft.OpenApi;

    using SysML2.NET.CodeGenerator.Extensions;

    /// <summary>
    /// A handlebars block helper that writes the properties declared by an <see cref="IOpenApiSchema"/>
    /// </summary>
    public static class PropertyHelper
    {
        /// <summary>
        /// Registers the <see cref="PropertyHelper"/>
        /// </summary>
        /// <param name="handlebars">
        /// The <see cref="IHandlebars"/> context with which the helper needs to be registered
        /// </param>
        /// <param name="queryTypeOverride">
        /// A function that returns the declared type of a property whose schema has no C# equivalent
        /// </param>
        public static void RegisterPropertyHelper(this IHandlebars handlebars, Func<string, string, string> queryTypeOverride)
        {
            ArgumentNullException.ThrowIfNull(handlebars);
            ArgumentNullException.ThrowIfNull(queryTypeOverride);

            handlebars.RegisterHelper("Property.WriteForClass", (writer, _, arguments) =>
            {
                if (arguments.Length != 3)
                {
                    throw new HandlebarsException("{{#Property.WriteForClass}} helper must have exactly three arguments");
                }

                var propertyName = arguments[0] as string ?? throw new ArgumentException("supposed to be a property name");
                var className = arguments[1] as string ?? throw new ArgumentException("supposed to be a schema name");
                var classSchema = arguments[2] as IOpenApiSchema ?? throw new ArgumentException("supposed to be IOpenApiSchema");

                var propertySchema = classSchema.Properties[propertyName];
                var isRequired = classSchema.Required?.Contains(propertyName) == true;

                var isOverridden = queryTypeOverride(className, propertyName) is not null;

                var stringBuilder = new StringBuilder();
                stringBuilder.Append("public ");
                stringBuilder.Append(propertySchema.QueryCSharpTypeName(className, propertyName, isRequired, queryTypeOverride));
                stringBuilder.Append(' ');
                stringBuilder.Append(OpenApiSchemaExtensions.QueryPropertyName(propertyName));
                stringBuilder.Append(" { get; set; }");

                if (!isOverridden && propertySchema.QueryIsCollection())
                {
                    stringBuilder.Append(" = [];");
                }

                writer.WriteSafeString(stringBuilder + Environment.NewLine);
            });
        }
    }
}
