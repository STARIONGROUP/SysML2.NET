// -------------------------------------------------------------------------------------------------
// <copyright file="OpenApiJsonDeSerializerGenerator.cs" company="Starion Group S.A.">
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
    /// OpenAPI based handlebars generator for the JSON deserializers
    /// </summary>
    public class OpenApiJsonDeSerializerGenerator : OpenApiHandleBarsGenerator
    {
        /// <summary>
        /// The name of the template that generates the deserializer of a class
        /// </summary>
        private const string DeSerializerTemplateName = "psm-json-deserializer-openapi-template";

        /// <summary>
        /// The name of the template that generates a deserialization provider
        /// </summary>
        private const string ProviderTemplateName = "psm-json-deserialization-provider-openapi-template";

        /// <summary>
        /// The suffix that marks a schema as belonging to the request family
        /// </summary>
        private const string RequestSchemaNameSuffix = "Request";

        /// <summary>
        /// The readers that read the items of the properties whose schema has no C# equivalent, paired with the
        /// C# type of those items
        /// </summary>
        private static readonly Dictionary<(string ClassName, string PropertyName), (string Reader, string ElementType)> ElementReaderOverrides =
            new()
            {
                [("PrimitiveConstraint", "value")] = ("ConstraintValueReader.Read", "ConstraintValue"),
                [("PrimitiveConstraintRequest", "value")] = ("ConstraintValueReader.Read", "ConstraintValue")
            };

        /// <summary>
        /// The dispatcher methods that read the unions that admit types declared outside the OpenAPI document
        /// </summary>
        private static readonly Dictionary<string, string> DispatchedUnionReaders =
            new(StringComparer.Ordinal)
            {
                ["Data"] = "PsmDeSerializationDispatcher.ReadData",
                ["DataRequest"] = "PsmDeSerializationDispatcher.ReadDataRequest"
            };

        /// <summary>
        /// Generates the JSON deserializers of the schemas of the OpenAPI document
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
        /// Generates the JSON deserializer of the specified schema
        /// </summary>
        /// <param name="openApiDocument">
        /// the <see cref="OpenApiDocument"/> that contains the OpenAPI model to generate from
        /// </param>
        /// <param name="outputDirectory">
        /// The target <see cref="DirectoryInfo"/>
        /// </param>
        /// <param name="name">
        /// The name of the schema to generate the deserializer for
        /// </param>
        /// <returns>
        /// The generated code
        /// </returns>
        public Task<string> GenerateDeSerializerAsync(OpenApiDocument openApiDocument, DirectoryInfo outputDirectory, string name)
        {
            ArgumentNullException.ThrowIfNull(openApiDocument);
            ArgumentNullException.ThrowIfNull(outputDirectory);
            ArgumentException.ThrowIfNullOrWhiteSpace(name);

            return this.GenerateDeSerializerInternalAsync(openApiDocument, outputDirectory, name);
        }

        /// <summary>
        /// Queries the schemas that a deserializer is generated for
        /// </summary>
        /// <param name="openApiDocument">
        /// the <see cref="OpenApiDocument"/> that contains the OpenAPI model to generate from
        /// </param>
        /// <returns>
        /// The in-scope schemas that are not unions, ordered by name
        /// </returns>
        public static IReadOnlyList<KeyValuePair<string, IOpenApiSchema>> QueryDeSerializableSchemas(OpenApiDocument openApiDocument)
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
                QueryElementReaderOverride);
            this.Handlebars.RegisterJsonSerializerHelper(QueryElementReaderOverride);
            this.Handlebars.RegisterJsonDeSerializerHelper(QueryElementReaderOverride, QueryElementTypeOverride, QueryDispatchedUnionReader);
        }

        /// <summary>
        /// Register the code templates
        /// </summary>
        protected override void RegisterTemplates()
        {
            this.RegisterTemplate(DeSerializerTemplateName);
            this.RegisterTemplate(ProviderTemplateName);
        }

        /// <summary>
        /// Queries the reader that reads the items of a property whose schema has no C# equivalent
        /// </summary>
        /// <param name="className">
        /// The name of the class that declares the property
        /// </param>
        /// <param name="propertyName">
        /// The name of the property as it appears in the schema
        /// </param>
        /// <returns>
        /// The reader, or <c>null</c> when the property is deserialized from its schema
        /// </returns>
        private static string QueryElementReaderOverride(string className, string propertyName)
        {
            return ElementReaderOverrides.TryGetValue((className, propertyName), out var over) ? over.Reader : null;
        }

        /// <summary>
        /// Queries the C# type of the items of a property whose schema has no C# equivalent
        /// </summary>
        /// <param name="className">
        /// The name of the class that declares the property
        /// </param>
        /// <param name="propertyName">
        /// The name of the property as it appears in the schema
        /// </param>
        /// <returns>
        /// The C# type, or <c>null</c> when the property is deserialized from its schema
        /// </returns>
        private static string QueryElementTypeOverride(string className, string propertyName)
        {
            return ElementReaderOverrides.TryGetValue((className, propertyName), out var over) ? over.ElementType : null;
        }

        /// <summary>
        /// Queries the dispatcher method that reads a union that admits types declared outside the OpenAPI document
        /// </summary>
        /// <param name="unionSchemaName">
        /// The name of the union schema
        /// </param>
        /// <returns>
        /// The dispatcher method, or <c>null</c> when every alternative is declared by the document
        /// </returns>
        private static string QueryDispatchedUnionReader(string unionSchemaName)
        {
            return unionSchemaName != null && DispatchedUnionReaders.TryGetValue(unionSchemaName, out var reader) ? reader : null;
        }

        /// <summary>
        /// Generates the JSON deserializers of the schemas of the OpenAPI document
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
            var schemas = QueryDeSerializableSchemas(openApiDocument);

            foreach (var schema in schemas)
            {
                await this.WriteDeSerializerAsync(schema, outputDirectory);
            }

            await this.WriteProviderAsync(schemas, outputDirectory, true);
            await this.WriteProviderAsync(schemas, outputDirectory, false);
        }

        /// <summary>
        /// Generates the JSON deserializer of the specified schema
        /// </summary>
        /// <param name="openApiDocument">
        /// the <see cref="OpenApiDocument"/> that contains the OpenAPI model to generate from
        /// </param>
        /// <param name="outputDirectory">
        /// The target <see cref="DirectoryInfo"/>
        /// </param>
        /// <param name="name">
        /// The name of the schema to generate the deserializer for
        /// </param>
        /// <returns>
        /// The generated code
        /// </returns>
        private async Task<string> GenerateDeSerializerInternalAsync(OpenApiDocument openApiDocument, DirectoryInfo outputDirectory, string name)
        {
            var schema = QueryDeSerializableSchemas(openApiDocument).Single(candidate => candidate.Key == name);

            return await this.WriteDeSerializerAsync(schema, outputDirectory);
        }

        /// <summary>
        /// Applies the deserializer template to the schema and writes the result to disk
        /// </summary>
        /// <param name="schema">
        /// The schema to generate the deserializer for
        /// </param>
        /// <param name="outputDirectory">
        /// The target <see cref="DirectoryInfo"/>
        /// </param>
        /// <returns>
        /// The generated code
        /// </returns>
        private async Task<string> WriteDeSerializerAsync(KeyValuePair<string, IOpenApiSchema> schema, DirectoryInfo outputDirectory)
        {
            var generatedCode = this.CodeCleanup(this.Templates[DeSerializerTemplateName](schema));

            await WriteAsync(generatedCode, outputDirectory, $"{schema.Key}DeSerializer.cs");

            return generatedCode;
        }

        /// <summary>
        /// Applies the provider template to the schemas of one family and writes the result to disk
        /// </summary>
        /// <param name="schemas">
        /// The schemas that a deserializer is generated for
        /// </param>
        /// <param name="outputDirectory">
        /// The target <see cref="DirectoryInfo"/>
        /// </param>
        /// <param name="isRequestFamily">
        /// Asserts that the provider resolves the request family rather than the response family
        /// </param>
        /// <returns>
        /// an awaitable <see cref="Task"/>
        /// </returns>
        private async Task WriteProviderAsync(IReadOnlyList<KeyValuePair<string, IOpenApiSchema>> schemas, DirectoryInfo outputDirectory, bool isRequestFamily)
        {
            var providerName = isRequestFamily ? "RequestDeSerializationProvider" : "ResponseDeSerializationProvider";

            var model = new Dictionary<string, object>
            {
                ["ProviderName"] = providerName,
                ["DelegateName"] = isRequestFamily ? "DeSerializeRequestDelegate" : "DeSerializeResponseDelegate",
                ["MarkerInterface"] = isRequestFamily ? "IRequest" : "IResponse",
                ["Family"] = isRequestFamily ? "request" : "response",
                ["Entries"] = schemas
                    .Where(schema => schema.Key.EndsWith(RequestSchemaNameSuffix, StringComparison.Ordinal) == isRequestFamily)
                    .Select(schema => new Dictionary<string, object>
                    {
                        ["SchemaName"] = schema.Key,
                        ["TypeDiscriminator"] = schema.Value.QueryTypeDiscriminator() ?? schema.Key
                    })
                    .ToList()
            };

            var generatedProvider = this.CodeCleanup(this.Templates[ProviderTemplateName](model));

            await WriteAsync(generatedProvider, outputDirectory, $"{providerName}.cs");
        }
    }
}
