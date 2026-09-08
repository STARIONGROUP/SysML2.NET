// -------------------------------------------------------------------------------------------------
// <copyright file="OpenApiHandleBarsGenerator.cs" company="Starion Group S.A.">
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
