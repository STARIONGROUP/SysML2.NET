// ------------------------------------------------------------------------------------------------
// <copyright file="SchemaHelper.cs" company="Starion Group S.A.">
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
    /// A handlebars block helper for the <see cref="IOpenApiSchema"/> interface
    /// </summary>
    public static class SchemaHelper
    {
        /// <summary>
        /// Registers the <see cref="SchemaHelper"/>
        /// </summary>
        /// <param name="handlebars">
        /// The <see cref="IHandlebars"/> context with which the helper needs to be registered
        /// </param>
        /// <param name="queryImplementedInterfaces">
        /// A function that returns the interfaces implemented by the class generated for a schema name
        /// </param>
        /// <param name="queryTypeOverride">
        /// A function that returns the declared type of a property whose schema has no C# equivalent
        /// </param>
        public static void RegisterSchemaHelper(this IHandlebars handlebars, Func<string, IEnumerable<string>> queryImplementedInterfaces, Func<string, string, string> queryTypeOverride)
        {
            ArgumentNullException.ThrowIfNull(handlebars);
            ArgumentNullException.ThrowIfNull(queryImplementedInterfaces);
            ArgumentNullException.ThrowIfNull(queryTypeOverride);

            handlebars.RegisterHelper("Schema.WriteImplementedInterfaces", (writer, _, arguments) =>
            {
                if (arguments.Length != 1)
                {
                    throw new HandlebarsException("{{#Schema.WriteImplementedInterfaces}} helper must have exactly one argument");
                }

                var schemaName = arguments[0] as string ?? throw new ArgumentException("supposed to be a schema name");

                var implementedInterfaces = queryImplementedInterfaces(schemaName)
                    .OrderBy(interfaceName => interfaceName, StringComparer.Ordinal)
                    .ToList();

                if (implementedInterfaces.Count > 0)
                {
                    writer.WriteSafeString($" : {string.Join(", ", implementedInterfaces)}");
                }
            });

            handlebars.RegisterHelper("Schema.QueryHasTypeDiscriminator", (_, arguments) =>
            {
                if (arguments.Length != 1)
                {
                    throw new HandlebarsException("{{#Schema.QueryHasTypeDiscriminator}} helper must have exactly one argument");
                }

                var schema = arguments[0] as IOpenApiSchema ?? throw new ArgumentException("supposed to be IOpenApiSchema");

                return !string.IsNullOrWhiteSpace(schema.QueryTypeDiscriminator());
            });

            handlebars.RegisterHelper("Schema.WriteTypeDiscriminator", (writer, _, arguments) =>
            {
                if (arguments.Length != 1)
                {
                    throw new HandlebarsException("{{#Schema.WriteTypeDiscriminator}} helper must have exactly one argument");
                }

                var schema = arguments[0] as IOpenApiSchema ?? throw new ArgumentException("supposed to be IOpenApiSchema");

                writer.WriteSafeString(schema.QueryTypeDiscriminator());
            });

            handlebars.RegisterHelper("Schema.QueryPropertyNames", (_, arguments) =>
            {
                if (arguments.Length != 2)
                {
                    throw new HandlebarsException("{{#Schema.QueryPropertyNames}} helper must have exactly two arguments");
                }

                var schema = arguments[0] as IOpenApiSchema ?? throw new ArgumentException("supposed to be IOpenApiSchema");
                var className = arguments[1] as string ?? throw new ArgumentException("supposed to be a schema name");

                return schema.QueryMappableProperties(className, queryTypeOverride).Select(property => property.Key).ToList();
            });

            handlebars.RegisterHelper("Schema.WriteUnionAlternatives", (writer, _, arguments) =>
            {
                if (arguments.Length != 1)
                {
                    throw new HandlebarsException("{{#Schema.WriteUnionAlternatives}} helper must have exactly one argument");
                }

                var schema = arguments[0] as IOpenApiSchema ?? throw new ArgumentException("supposed to be IOpenApiSchema");

                writer.WriteSafeString(string.Join(", ", schema.QueryUnionAlternativeNames()));
            });
        }
    }
}
