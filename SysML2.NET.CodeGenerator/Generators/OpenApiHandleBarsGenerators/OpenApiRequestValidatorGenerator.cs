// ------------------------------------------------------------------------------------------------
// <copyright file="OpenApiRequestValidatorGenerator.cs" company="Starion Group S.A.">
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

    using SysML2.NET.CodeGenerator.Extensions;
    using SysML2.NET.CodeGenerator.OpenApiHandleBarHelpers;

    /// <summary>
    /// OpenAPI based handlebars generator for the FluentValidation validators of the request data transfer objects
    /// </summary>
    public class OpenApiRequestValidatorGenerator : OpenApiHandleBarsGenerator
    {
        /// <summary>
        /// The name of the template that generates a request validator
        /// </summary>
        private const string ValidatorTemplateName = "psm-request-validator-openapi-template";

        /// <summary>
        /// The names of the schemas that a validator is generated for, scoped to the current generation run
        /// </summary>
        private HashSet<string> validatedSchemaNames = [];

        /// <summary>
        /// Queries the request schemas that a validator is generated for
        /// </summary>
        /// <param name="openApiDocument">
        /// the <see cref="OpenApiDocument"/> that contains the OpenAPI model to generate from
        /// </param>
        /// <returns>
        /// The request schemas that are not unions, ordered by name
        /// </returns>
        public static IReadOnlyList<KeyValuePair<string, IOpenApiSchema>> QueryValidatedSchemas(OpenApiDocument openApiDocument)
        {
            ArgumentNullException.ThrowIfNull(openApiDocument);

            return QuerySchemas(openApiDocument)
                .Where(schema => schema.Key.EndsWith(OpenApiDtoGenerator.RequestSchemaNameSuffix, StringComparison.Ordinal) && !schema.Value.QueryIsUnion())
                .ToList();
        }

        /// <summary>
        /// Generates the validators of the request schemas of the OpenAPI document
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
        /// Generates the validator of the specified request schema
        /// </summary>
        /// <param name="openApiDocument">
        /// the <see cref="OpenApiDocument"/> that contains the OpenAPI model to generate from
        /// </param>
        /// <param name="outputDirectory">
        /// The target <see cref="DirectoryInfo"/>
        /// </param>
        /// <param name="name">
        /// The name of the request schema to generate the validator for
        /// </param>
        /// <returns>
        /// The generated code
        /// </returns>
        public Task<string> GenerateRequestValidatorAsync(OpenApiDocument openApiDocument, DirectoryInfo outputDirectory, string name)
        {
            ArgumentNullException.ThrowIfNull(openApiDocument);
            ArgumentNullException.ThrowIfNull(outputDirectory);
            ArgumentException.ThrowIfNullOrWhiteSpace(name);

            return this.GenerateRequestValidatorInternalAsync(openApiDocument, outputDirectory, name);
        }

        /// <summary>
        /// Register the custom helpers
        /// </summary>
        protected override void RegisterHelpers()
        {
            this.Handlebars.RegisterValidatorHelper(schemaName => this.validatedSchemaNames.Contains(schemaName));
        }

        /// <summary>
        /// Register the code templates
        /// </summary>
        protected override void RegisterTemplates()
        {
            this.RegisterTemplate(ValidatorTemplateName);
        }

        /// <summary>
        /// Generates the validators of the request schemas of the OpenAPI document
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
            foreach (var schema in this.AssignGenerationScope(openApiDocument))
            {
                await this.WriteValidatorAsync(schema, outputDirectory);
            }
        }

        /// <summary>
        /// Generates the validator of the specified request schema
        /// </summary>
        /// <param name="openApiDocument">
        /// the <see cref="OpenApiDocument"/> that contains the OpenAPI model to generate from
        /// </param>
        /// <param name="outputDirectory">
        /// The target <see cref="DirectoryInfo"/>
        /// </param>
        /// <param name="name">
        /// The name of the request schema to generate the validator for
        /// </param>
        /// <returns>
        /// The generated code
        /// </returns>
        private async Task<string> GenerateRequestValidatorInternalAsync(OpenApiDocument openApiDocument, DirectoryInfo outputDirectory, string name)
        {
            var schemas = this.AssignGenerationScope(openApiDocument);

            return await this.WriteValidatorAsync(schemas.Single(schema => schema.Key == name), outputDirectory);
        }

        /// <summary>
        /// Queries the request schemas and assigns the state that the registered helpers read from
        /// </summary>
        /// <param name="openApiDocument">
        /// the <see cref="OpenApiDocument"/> that contains the OpenAPI model to generate from
        /// </param>
        /// <returns>
        /// The request schemas that a validator is generated for, ordered by name
        /// </returns>
        private IReadOnlyList<KeyValuePair<string, IOpenApiSchema>> AssignGenerationScope(OpenApiDocument openApiDocument)
        {
            var schemas = QueryValidatedSchemas(openApiDocument);

            this.validatedSchemaNames = schemas.Select(schema => schema.Key).ToHashSet(StringComparer.Ordinal);

            return schemas;
        }

        /// <summary>
        /// Applies the validator template to the schema and writes the result to disk
        /// </summary>
        /// <param name="schema">
        /// The request schema to generate the validator for
        /// </param>
        /// <param name="outputDirectory">
        /// The target <see cref="DirectoryInfo"/>
        /// </param>
        /// <returns>
        /// The generated code
        /// </returns>
        private async Task<string> WriteValidatorAsync(KeyValuePair<string, IOpenApiSchema> schema, DirectoryInfo outputDirectory)
        {
            var generatedCode = this.CodeCleanup(this.Templates[ValidatorTemplateName](schema));

            await WriteAsync(generatedCode, outputDirectory, $"{schema.Key}Validator.cs");

            return generatedCode;
        }
    }
}
