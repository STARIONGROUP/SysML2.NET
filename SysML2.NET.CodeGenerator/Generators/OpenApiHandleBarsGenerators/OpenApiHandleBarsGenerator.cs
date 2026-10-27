// ------------------------------------------------------------------------------------------------
// <copyright file="OpenApiHandleBarsGenerator.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.CodeGenerator.Generators.OpenApiHandleBarsGenerators
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.OpenApi;

    /// <summary>
    /// Abstract super class from which all openapi based <see cref="HandlebarsDotNet"/> generators
    /// need to derive
    /// </summary>
    public abstract class OpenApiHandleBarsGenerator: HandleBarsGenerator
    {
        /// <summary>
        /// The <c>$id</c> prefix that marks a schema as belonging to the Systems Modeling API and Services
        /// </summary>
        public const string SystemsModelingApiSchemaIdPrefix = "https://www.omg.org/spec/SystemsModelingAPI";

        /// <summary>
        /// Generates code specific to the concrete implementation
        /// </summary>
        /// <param name="openApiDocument">
        /// the <see cref="OpenApiDocument"/> that contains the OpenAPI model to generate from
        /// </param>
        /// <param name="outputDirectory">
        /// The target <see cref="DirectoryInfo"/>
        /// </param>
        /// <returns>
        /// an awaitable <see cref="Task"/>
        /// </returns>
        public abstract Task GenerateAsync(OpenApiDocument openApiDocument, DirectoryInfo outputDirectory);

        /// <summary>
        /// Queries the schemas of the OpenAPI document that belong to the Systems Modeling API and Services
        /// </summary>
        /// <param name="openApiDocument">
        /// the <see cref="OpenApiDocument"/> that contains the OpenAPI model to generate from
        /// </param>
        /// <returns>
        /// The in-scope schemas, ordered by name
        /// </returns>
        public static IReadOnlyList<KeyValuePair<string, IOpenApiSchema>> QuerySchemas(OpenApiDocument openApiDocument)
        {
            ArgumentNullException.ThrowIfNull(openApiDocument);

            return openApiDocument.Components?.Schemas is null
                ? []
                : openApiDocument.Components.Schemas
                    .Where(schema => schema.Value.Id?.StartsWith(SystemsModelingApiSchemaIdPrefix, StringComparison.Ordinal) == true)
                    .OrderBy(schema => schema.Key, StringComparer.Ordinal)
                    .ToList();
        }

        /// <summary>
        /// Gets an optional subfolder location path to locate templates
        /// </summary>
        /// <returns>An optional subfolder name</returns>
        protected override string GetOptionalSubfolderTemplateLocation()
        {
            return "OpenAPI";
        }
    }
}
