// ------------------------------------------------------------------------------------------------
// <copyright file="OpenApiApiClientGenerator.cs" company="Starion Group S.A.">
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

    using SysML2.NET.CodeGenerator.OpenApiHandleBarHelpers;

    /// <summary>
    /// OpenAPI based handlebars generator for the client of the Systems Modeling API and Services, one partial per tag
    /// </summary>
    public class OpenApiApiClientGenerator : OpenApiHandleBarsGenerator
    {
        /// <summary>
        /// The name of the template that generates the partial of the client for one tag
        /// </summary>
        private const string ClassTemplateName = "psm-apiclient-openapi-template";

        /// <summary>
        /// The name of the template that generates the interface of the client
        /// </summary>
        private const string InterfaceTemplateName = "psm-apiclient-interface-openapi-template";

        /// <summary>
        /// The names of the partial templates shared by the class and the interface templates
        /// </summary>
        private static readonly string[] PartialTemplateNames =
        [
            "psm-apiclient-typename-openapi-partial",
            "psm-apiclient-documentation-openapi-partial",
            "psm-apiclient-signature-openapi-partial"
        ];

        /// <summary>
        /// The name of the generated interface of the client
        /// </summary>
        private const string InterfaceName = "IPsmApiClient";

        /// <summary>
        /// Generates the interface of the client and one partial of the client per tag of the OpenAPI document
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
        /// Generates the partial of the client for the specified tag
        /// </summary>
        /// <param name="openApiDocument">
        /// the <see cref="OpenApiDocument"/> that contains the OpenAPI model to generate from
        /// </param>
        /// <param name="outputDirectory">
        /// The target <see cref="DirectoryInfo"/>
        /// </param>
        /// <param name="tag">
        /// The tag, as declared in the OpenAPI document, to generate the partial for
        /// </param>
        /// <returns>
        /// The generated code
        /// </returns>
        public Task<string> GenerateApiClientPartialAsync(OpenApiDocument openApiDocument, DirectoryInfo outputDirectory, string tag)
        {
            ArgumentNullException.ThrowIfNull(openApiDocument);
            ArgumentNullException.ThrowIfNull(outputDirectory);
            ArgumentException.ThrowIfNullOrWhiteSpace(tag);

            return this.WritePartialAsync(QueryTagModels(openApiDocument).Single(model => model.Tag == tag), outputDirectory);
        }

        /// <summary>
        /// Generates the interface of the client
        /// </summary>
        /// <param name="openApiDocument">
        /// the <see cref="OpenApiDocument"/> that contains the OpenAPI model to generate from
        /// </param>
        /// <param name="outputDirectory">
        /// The target <see cref="DirectoryInfo"/>
        /// </param>
        /// <returns>
        /// The generated code
        /// </returns>
        public Task<string> GenerateApiClientInterfaceAsync(OpenApiDocument openApiDocument, DirectoryInfo outputDirectory)
        {
            ArgumentNullException.ThrowIfNull(openApiDocument);
            ArgumentNullException.ThrowIfNull(outputDirectory);

            return this.WriteInterfaceAsync(QueryTagModels(openApiDocument), outputDirectory);
        }

        /// <summary>
        /// Queries the operations of the OpenAPI document grouped by their first tag, in the tag order of the document
        /// </summary>
        /// <param name="openApiDocument">
        /// the <see cref="OpenApiDocument"/> that contains the OpenAPI model to generate from
        /// </param>
        /// <returns>
        /// One <see cref="ApiClientTagModel"/> per tag that has at least one operation
        /// </returns>
        /// <exception cref="InvalidOperationException">
        /// Thrown when an operation declares a shape that the generated client cannot express
        /// </exception>
        public static IReadOnlyList<ApiClientTagModel> QueryTagModels(OpenApiDocument openApiDocument)
        {
            ArgumentNullException.ThrowIfNull(openApiDocument);

            var operationsByTag = openApiDocument.Paths
                .SelectMany(path => path.Value.Operations.Select(operation => (Tag: operation.Value.Tags.First().Name, Model: new ApiClientOperationModel
                {
                    Path = path.Key,
                    HttpMethod = operation.Key.Method,
                    Operation = ValidateOperation(operation.Value)
                })))
                .ToLookup(entry => entry.Tag, entry => entry.Model, StringComparer.Ordinal);

            return openApiDocument.Tags
                .Where(tag => operationsByTag.Contains(tag.Name))
                .Select((tag, tagIndex) => new ApiClientTagModel
                {
                    Tag = tag.Name,
                    Name = QueryTagName(tag.Name),
                    IsPrimary = tagIndex == 0,
                    Operations = operationsByTag[tag.Name].ToList()
                })
                .ToList();
        }

        /// <summary>
        /// Verifies that the generated client can express the operation
        /// </summary>
        /// <param name="operation">
        /// The <see cref="OpenApiOperation"/> to verify
        /// </param>
        /// <returns>
        /// The verified <paramref name="operation"/>
        /// </returns>
        /// <exception cref="InvalidOperationException">
        /// Thrown when the operation has more than one successful JSON response, or an array query parameter not written as a comma separated list
        /// </exception>
        private static OpenApiOperation ValidateOperation(OpenApiOperation operation)
        {
            var successfulJsonResponseCount = operation.Responses?
                .Count(response => response.Key.StartsWith('2') && response.Value.Content?.ContainsKey("application/json") == true) ?? 0;

            if (successfulJsonResponseCount > 1)
            {
                throw new InvalidOperationException($"The operation {operation.OperationId} declares {successfulJsonResponseCount} successful JSON responses; the generated client returns one");
            }

            var unsupportedArrayParameter = operation.Parameters?
                .FirstOrDefault(parameter => parameter.In == ParameterLocation.Query
                                             && parameter.Schema?.Type == JsonSchemaType.Array
                                             && (parameter.Style is not (null or ParameterStyle.Form) || parameter.Explode));

            if (unsupportedArrayParameter is not null)
            {
                throw new InvalidOperationException($"The array query parameter {unsupportedArrayParameter.Name} of the operation {operation.OperationId} is not declared with style form and explode false");
            }

            return operation;
        }

        /// <summary>
        /// Register the custom helpers
        /// </summary>
        protected override void RegisterHelpers()
        {
            this.Handlebars.RegisterTextHelper();
            this.Handlebars.RegisterHttpMethodHelper();
        }

        /// <summary>
        /// Register the code templates
        /// </summary>
        protected override void RegisterTemplates()
        {
            foreach (var partialTemplateName in PartialTemplateNames)
            {
                this.RegisterPartialTemplate(partialTemplateName);
            }

            this.RegisterTemplate(ClassTemplateName);
            this.RegisterTemplate(InterfaceTemplateName);
        }

        /// <summary>
        /// Queries the identifier-safe name of a tag by keeping only its letters and digits
        /// </summary>
        /// <param name="tag">
        /// The tag as declared in the OpenAPI document
        /// </param>
        /// <returns>
        /// The identifier-safe name
        /// </returns>
        private static string QueryTagName(string tag)
        {
            return string.Concat(tag.Where(char.IsLetterOrDigit));
        }

        /// <summary>
        /// Generates the interface of the client and one partial of the client per tag of the OpenAPI document
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
            var tagModels = QueryTagModels(openApiDocument);

            await this.WriteInterfaceAsync(tagModels, outputDirectory);

            foreach (var tagModel in tagModels)
            {
                await this.WritePartialAsync(tagModel, outputDirectory);
            }
        }

        /// <summary>
        /// Applies the class template to the tag and writes the result to disk
        /// </summary>
        /// <param name="tagModel">
        /// The <see cref="ApiClientTagModel"/> to generate the partial for
        /// </param>
        /// <param name="outputDirectory">
        /// The target <see cref="DirectoryInfo"/>
        /// </param>
        /// <returns>
        /// The generated code
        /// </returns>
        private async Task<string> WritePartialAsync(ApiClientTagModel tagModel, DirectoryInfo outputDirectory)
        {
            var generatedCode = this.CodeCleanup(this.Templates[ClassTemplateName](tagModel));

            await WriteAsync(generatedCode, outputDirectory, $"{tagModel.Name}Api.cs");

            return generatedCode;
        }

        /// <summary>
        /// Applies the interface template to all tags and writes the result to disk
        /// </summary>
        /// <param name="tagModels">
        /// The <see cref="ApiClientTagModel"/>s whose operations the interface declares
        /// </param>
        /// <param name="outputDirectory">
        /// The target <see cref="DirectoryInfo"/>
        /// </param>
        /// <returns>
        /// The generated code
        /// </returns>
        private async Task<string> WriteInterfaceAsync(IReadOnlyList<ApiClientTagModel> tagModels, DirectoryInfo outputDirectory)
        {
            var generatedCode = this.CodeCleanup(this.Templates[InterfaceTemplateName](tagModels));

            await WriteAsync(generatedCode, outputDirectory, $"{InterfaceName}.cs");

            return generatedCode;
        }
    }
}
