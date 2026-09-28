// -------------------------------------------------------------------------------------------------
// <copyright file="OpenApiEnumProviderGenerator.cs" company="Starion Group S.A.">
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

    using SysML2.NET.CodeGenerator.OpenApiHandleBarHelpers;

    /// <summary>
    /// OpenAPI based handlebars generator for the providers that convert enumerations to and from the wire
    /// </summary>
    public class OpenApiEnumProviderGenerator : OpenApiHandleBarsGenerator
    {
        /// <summary>
        /// The name of the template that generates the provider of an enumeration
        /// </summary>
        private const string EnumerationProviderTemplateName = "psm-enumprovider-openapi-template";

        /// <summary>
        /// Generates the providers of the enumerations declared inline by the schemas of the OpenAPI document
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
        public override Task GenerateAsync(OpenApiDocument openApiDocument, DirectoryInfo outputDirectory)
        {
            ArgumentNullException.ThrowIfNull(openApiDocument);
            ArgumentNullException.ThrowIfNull(outputDirectory);

            return this.GenerateInternalAsync(openApiDocument, outputDirectory);
        }

        /// <summary>
        /// Generates the provider of the enumeration with the specified name
        /// </summary>
        /// <param name="openApiDocument">
        /// the <see cref="OpenApiDocument"/> that contains the OpenAPI model to generate from
        /// </param>
        /// <param name="outputDirectory">
        /// The target <see cref="DirectoryInfo"/>
        /// </param>
        /// <param name="name">
        /// The name of the enumeration whose provider is to be generated
        /// </param>
        /// <returns>
        /// The generated code
        /// </returns>
        public Task<string> GenerateEnumerationProviderAsync(OpenApiDocument openApiDocument, DirectoryInfo outputDirectory, string name)
        {
            ArgumentNullException.ThrowIfNull(openApiDocument);
            ArgumentNullException.ThrowIfNull(outputDirectory);
            ArgumentException.ThrowIfNullOrWhiteSpace(name);

            return this.GenerateEnumerationProviderInternalAsync(openApiDocument, outputDirectory, name);
        }

        /// <summary>
        /// Register the custom helpers
        /// </summary>
        protected override void RegisterHelpers()
        {
            this.Handlebars.RegisterEnumerationHelper();
        }

        /// <summary>
        /// Register the code templates
        /// </summary>
        protected override void RegisterTemplates()
        {
            this.RegisterTemplate(EnumerationProviderTemplateName);
        }

        /// <summary>
        /// Generates the providers of the enumerations declared inline by the schemas of the OpenAPI document
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
        private async Task GenerateInternalAsync(OpenApiDocument openApiDocument, DirectoryInfo outputDirectory)
        {
            foreach (var enumeration in OpenApiEnumGenerator.QueryEnumerations(openApiDocument))
            {
                await this.WriteEnumerationProviderAsync(enumeration, outputDirectory);
            }
        }

        /// <summary>
        /// Generates the provider of the enumeration with the specified name
        /// </summary>
        /// <param name="openApiDocument">
        /// the <see cref="OpenApiDocument"/> that contains the OpenAPI model to generate from
        /// </param>
        /// <param name="outputDirectory">
        /// The target <see cref="DirectoryInfo"/>
        /// </param>
        /// <param name="name">
        /// The name of the enumeration whose provider is to be generated
        /// </param>
        /// <returns>
        /// The generated code
        /// </returns>
        private async Task<string> GenerateEnumerationProviderInternalAsync(OpenApiDocument openApiDocument, DirectoryInfo outputDirectory, string name)
        {
            var enumeration = OpenApiEnumGenerator.QueryEnumerations(openApiDocument).Single(candidate => candidate.Key == name);

            return await this.WriteEnumerationProviderAsync(enumeration, outputDirectory);
        }

        /// <summary>
        /// Applies the provider template to the property schema and writes the result to disk
        /// </summary>
        /// <param name="enumeration">
        /// The generated enumeration name paired with the property schema that declares it
        /// </param>
        /// <param name="outputDirectory">
        /// The target <see cref="DirectoryInfo"/>
        /// </param>
        /// <returns>
        /// The generated code
        /// </returns>
        private async Task<string> WriteEnumerationProviderAsync(KeyValuePair<string, IOpenApiSchema> enumeration, DirectoryInfo outputDirectory)
        {
            var generatedCode = this.CodeCleanup(this.Templates[EnumerationProviderTemplateName](enumeration));

            await WriteAsync(generatedCode, outputDirectory, $"{enumeration.Key}Provider.cs");

            return generatedCode;
        }
    }
}
