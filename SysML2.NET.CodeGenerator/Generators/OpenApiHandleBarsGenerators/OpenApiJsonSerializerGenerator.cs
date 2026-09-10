// -------------------------------------------------------------------------------------------------
// <copyright file="OpenApiJsonSerializerGenerator.cs" company="Starion Group S.A.">
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

    using SysML2.NET.CodeGenerator.Extensions;
    using SysML2.NET.CodeGenerator.OpenApiHandleBarHelpers;

    /// <summary>
    /// OpenAPI based handlebars generator for the JSON serializers
    /// </summary>
    public class OpenApiJsonSerializerGenerator : OpenApiHandleBarsGenerator
    {
        /// <summary>
        /// The name of the template that generates the serializer of a class
        /// </summary>
        private const string SerializerTemplateName = "psm-json-serializer-openapi-template";

        /// <summary>
        /// The name of the template that generates the serialization provider
        /// </summary>
        private const string ProviderTemplateName = "psm-json-serialization-provider-openapi-template";

        /// <summary>
        /// The writers that emit the items of the properties whose schema has no C# equivalent
        /// </summary>
        private static readonly Dictionary<(string ClassName, string PropertyName), string> ElementWriterOverrides =
            new()
            {
                [("PrimitiveConstraint", "value")] = "ConstraintValueWriter.Write",
                [("PrimitiveConstraintRequest", "value")] = "ConstraintValueWriter.Write"
            };

        /// <summary>
        /// Generates the JSON serializers of the schemas of the OpenAPI document
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
        /// Generates the JSON serializer of the specified schema
        /// </summary>
        /// <param name="openApiDocument">
        /// the <see cref="OpenApiDocument"/> that contains the OpenAPI model to generate from
        /// </param>
        /// <param name="outputDirectory">
        /// The target <see cref="DirectoryInfo"/>
        /// </param>
        /// <param name="name">
        /// The name of the schema to generate the serializer for
        /// </param>
        /// <returns>
        /// The generated code
        /// </returns>
        public Task<string> GenerateSerializerAsync(OpenApiDocument openApiDocument, DirectoryInfo outputDirectory, string name)
        {
            ArgumentNullException.ThrowIfNull(openApiDocument);
            ArgumentNullException.ThrowIfNull(outputDirectory);
            ArgumentException.ThrowIfNullOrWhiteSpace(name);

            return this.GenerateSerializerInternalAsync(openApiDocument, outputDirectory, name);
        }

        /// <summary>
        /// Queries the schemas that a serializer is generated for
        /// </summary>
        /// <param name="openApiDocument">
        /// the <see cref="OpenApiDocument"/> that contains the OpenAPI model to generate from
        /// </param>
        /// <returns>
        /// The in-scope schemas that are not unions, ordered by name
        /// </returns>
        public static IReadOnlyList<KeyValuePair<string, IOpenApiSchema>> QuerySerializableSchemas(OpenApiDocument openApiDocument)
        {
            ArgumentNullException.ThrowIfNull(openApiDocument);

            return QuerySchemas(openApiDocument).Where(schema => !schema.Value.QueryIsUnion()).ToList();
        }

        /// <summary>
        /// Register the custom helpers
        /// </summary>
        protected override void RegisterHelpers()
        {
            this.Handlebars.RegisterSchemaHelper(
                _ => [],
                QueryElementWriterOverride);
            this.Handlebars.RegisterJsonSerializerHelper(QueryElementWriterOverride);
        }

        /// <summary>
        /// Register the code templates
        /// </summary>
        protected override void RegisterTemplates()
        {
            this.RegisterTemplate(SerializerTemplateName);
            this.RegisterTemplate(ProviderTemplateName);
        }

        /// <summary>
        /// Queries the writer that emits the items of a property whose schema has no C# equivalent
        /// </summary>
        /// <param name="className">
        /// The name of the class that declares the property
        /// </param>
        /// <param name="propertyName">
        /// The name of the property as it appears in the schema
        /// </param>
        /// <returns>
        /// The writer, or <c>null</c> when the property is serialized from its schema
        /// </returns>
        private static string QueryElementWriterOverride(string className, string propertyName)
        {
            return ElementWriterOverrides.TryGetValue((className, propertyName), out var writerName) ? writerName : null;
        }

        /// <summary>
        /// Generates the JSON serializers of the schemas of the OpenAPI document
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
            var schemas = QuerySerializableSchemas(openApiDocument);

            foreach (var schema in schemas)
            {
                await this.WriteSerializerAsync(schema, outputDirectory);
            }

            var generatedProvider = this.CodeCleanup(this.Templates[ProviderTemplateName](schemas.Select(schema => schema.Key).ToList()));

            await WriteAsync(generatedProvider, outputDirectory, "SerializationProvider.cs");
        }

        /// <summary>
        /// Generates the JSON serializer of the specified schema
        /// </summary>
        /// <param name="openApiDocument">
        /// the <see cref="OpenApiDocument"/> that contains the OpenAPI model to generate from
        /// </param>
        /// <param name="outputDirectory">
        /// The target <see cref="DirectoryInfo"/>
        /// </param>
        /// <param name="name">
        /// The name of the schema to generate the serializer for
        /// </param>
        /// <returns>
        /// The generated code
        /// </returns>
        private async Task<string> GenerateSerializerInternalAsync(OpenApiDocument openApiDocument, DirectoryInfo outputDirectory, string name)
        {
            var schema = QuerySerializableSchemas(openApiDocument).Single(candidate => candidate.Key == name);

            return await this.WriteSerializerAsync(schema, outputDirectory);
        }

        /// <summary>
        /// Applies the serializer template to the schema and writes the result to disk
        /// </summary>
        /// <param name="schema">
        /// The schema to generate the serializer for
        /// </param>
        /// <param name="outputDirectory">
        /// The target <see cref="DirectoryInfo"/>
        /// </param>
        /// <returns>
        /// The generated code
        /// </returns>
        private async Task<string> WriteSerializerAsync(KeyValuePair<string, IOpenApiSchema> schema, DirectoryInfo outputDirectory)
        {
            var generatedCode = this.CodeCleanup(this.Templates[SerializerTemplateName](schema));

            await WriteAsync(generatedCode, outputDirectory, $"{schema.Key}Serializer.cs");

            return generatedCode;
        }
    }
}
