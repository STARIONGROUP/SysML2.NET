// -------------------------------------------------------------------------------------------------
// <copyright file="OpenApiMessagePackFormatterGenerator.cs" company="Starion Group S.A.">
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
    /// OpenAPI based handlebars generator for the MessagePack formatters
    /// </summary>
    public class OpenApiMessagePackFormatterGenerator : OpenApiHandleBarsGenerator
    {
        /// <summary>
        /// The name of the template that generates the formatter of a class
        /// </summary>
        private const string FormatterTemplateName = "psm-messagepack-formatter-openapi-template";

        /// <summary>
        /// The name of the template that generates a payload envelope
        /// </summary>
        private const string PayloadTemplateName = "psm-messagepack-payload-openapi-template";

        /// <summary>
        /// The name of the template that generates the factory of a payload envelope
        /// </summary>
        private const string PayloadFactoryTemplateName = "psm-messagepack-payloadfactory-openapi-template";

        /// <summary>
        /// The name of the template that generates the formatter of a payload envelope
        /// </summary>
        private const string PayloadFormatterTemplateName = "psm-messagepack-payloadformatter-openapi-template";

        /// <summary>
        /// The name of the template that generates the formatter resolution helper
        /// </summary>
        private const string FormatterHelperTemplateName = "psm-messagepack-formatterhelper-openapi-template";

        /// <summary>
        /// The suffix that marks a schema as belonging to the request family
        /// </summary>
        private const string RequestSchemaNameSuffix = "Request";

        /// <summary>
        /// The C# types of the properties whose schema has no C# equivalent
        /// </summary>
        private static readonly Dictionary<(string ClassName, string PropertyName), string> PropertyTypeOverrides =
            new()
            {
                [("PrimitiveConstraint", "value")] = "List<ConstraintValue>",
                [("PrimitiveConstraintRequest", "value")] = "List<ConstraintValue>"
            };

        /// <summary>
        /// The writers that emit the items of the properties whose schema has no C# equivalent
        /// </summary>
        private static readonly Dictionary<(string ClassName, string PropertyName), string> ElementWriterOverrides =
            new()
            {
                [("PrimitiveConstraint", "value")] = "ConstraintValueMessagePackWriter.Write",
                [("PrimitiveConstraintRequest", "value")] = "ConstraintValueMessagePackWriter.Write"
            };

        /// <summary>
        /// The readers that read the items of the properties whose schema has no C# equivalent
        /// </summary>
        private static readonly Dictionary<(string ClassName, string PropertyName), string> ElementReaderOverrides =
            new()
            {
                [("PrimitiveConstraint", "value")] = "ConstraintValueMessagePackReader.Read",
                [("PrimitiveConstraintRequest", "value")] = "ConstraintValueMessagePackReader.Read"
            };

        /// <summary>
        /// The C# types of the items of the properties whose schema has no C# equivalent
        /// </summary>
        private static readonly Dictionary<(string ClassName, string PropertyName), string> ElementTypeOverrides =
            new()
            {
                [("PrimitiveConstraint", "value")] = "ConstraintValue",
                [("PrimitiveConstraintRequest", "value")] = "ConstraintValue"
            };

        /// <summary>
        /// The C# types that the union schemas resolve to, where the union admits types declared outside the document
        /// </summary>
        private static readonly Dictionary<string, string> UnionTypeOverrides =
            new(StringComparer.Ordinal)
            {
                ["Data"] = "SysML2.NET.Common.IData",
                ["DataRequest"] = "SysML2.NET.Common.IDataRequest"
            };

        /// <summary>
        /// Generates the MessagePack formatters of the schemas of the OpenAPI document
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
        /// Generates the MessagePack formatter of the specified schema
        /// </summary>
        /// <param name="openApiDocument">
        /// the <see cref="OpenApiDocument"/> that contains the OpenAPI model to generate from
        /// </param>
        /// <param name="outputDirectory">
        /// The target <see cref="DirectoryInfo"/>
        /// </param>
        /// <param name="name">
        /// The name of the schema to generate the formatter for
        /// </param>
        /// <returns>
        /// The generated code
        /// </returns>
        public Task<string> GenerateFormatterAsync(OpenApiDocument openApiDocument, DirectoryInfo outputDirectory, string name)
        {
            ArgumentNullException.ThrowIfNull(openApiDocument);
            ArgumentNullException.ThrowIfNull(outputDirectory);
            ArgumentException.ThrowIfNullOrWhiteSpace(name);

            return this.GenerateFormatterInternalAsync(openApiDocument, outputDirectory, name);
        }

        /// <summary>
        /// Queries the schemas that a formatter is generated for
        /// </summary>
        /// <param name="openApiDocument">
        /// the <see cref="OpenApiDocument"/> that contains the OpenAPI model to generate from
        /// </param>
        /// <returns>
        /// The in-scope schemas that are not unions, ordered by name
        /// </returns>
        public static IReadOnlyList<KeyValuePair<string, IOpenApiSchema>> QueryFormattableSchemas(OpenApiDocument openApiDocument)
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
                QueryPropertyTypeOverride);
            this.Handlebars.RegisterJsonSerializerHelper(QueryElementWriterOverride);
            this.Handlebars.RegisterMessagePackFormatterHelper(QueryPropertyTypeOverride, QueryUnionType, QueryElementReaderOverride, QueryElementTypeOverride);
        }

        /// <summary>
        /// Register the code templates
        /// </summary>
        protected override void RegisterTemplates()
        {
            this.RegisterTemplate(FormatterTemplateName);
            this.RegisterTemplate(PayloadTemplateName);
            this.RegisterTemplate(PayloadFactoryTemplateName);
            this.RegisterTemplate(PayloadFormatterTemplateName);
            this.RegisterTemplate(FormatterHelperTemplateName);
        }

        /// <summary>
        /// Queries the C# type of a property whose schema has no C# equivalent
        /// </summary>
        /// <param name="className">
        /// The name of the class that declares the property
        /// </param>
        /// <param name="propertyName">
        /// The name of the property as it appears in the schema
        /// </param>
        /// <returns>
        /// The C# type, or <c>null</c> when the property is resolved from its schema
        /// </returns>
        private static string QueryPropertyTypeOverride(string className, string propertyName)
        {
            return PropertyTypeOverrides.TryGetValue((className, propertyName), out var typeName) ? typeName : null;
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
            return ElementReaderOverrides.TryGetValue((className, propertyName), out var readerName) ? readerName : null;
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
        /// The C# type, or <c>null</c> when the property is resolved from its schema
        /// </returns>
        private static string QueryElementTypeOverride(string className, string propertyName)
        {
            return ElementTypeOverrides.TryGetValue((className, propertyName), out var typeName) ? typeName : null;
        }

        /// <summary>
        /// Queries the C# type that a union schema resolves to
        /// </summary>
        /// <param name="unionSchemaName">
        /// The name of the union schema
        /// </param>
        /// <returns>
        /// The C# type
        /// </returns>
        private static string QueryUnionType(string unionSchemaName)
        {
            return UnionTypeOverrides.TryGetValue(unionSchemaName, out var typeName)
                ? typeName
                : OpenApiSchemaExtensions.QueryInterfaceName(unionSchemaName);
        }

        /// <summary>
        /// Generates the MessagePack formatters of the schemas of the OpenAPI document
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
            var schemas = QueryFormattableSchemas(openApiDocument);

            foreach (var schema in schemas)
            {
                await this.WriteFormatterAsync(schema, outputDirectory);
            }

            await this.WritePayloadAsync(schemas, outputDirectory, true);
            await this.WritePayloadAsync(schemas, outputDirectory, false);

            var generatedHelper = this.CodeCleanup(this.Templates[FormatterHelperTemplateName](schemas.Select(schema => schema.Key).ToList()));

            await WriteAsync(generatedHelper, outputDirectory, "PsmDataResolverGetFormatterHelper.cs");
        }

        /// <summary>
        /// Generates the MessagePack formatter of the specified schema
        /// </summary>
        /// <param name="openApiDocument">
        /// the <see cref="OpenApiDocument"/> that contains the OpenAPI model to generate from
        /// </param>
        /// <param name="outputDirectory">
        /// The target <see cref="DirectoryInfo"/>
        /// </param>
        /// <param name="name">
        /// The name of the schema to generate the formatter for
        /// </param>
        /// <returns>
        /// The generated code
        /// </returns>
        private async Task<string> GenerateFormatterInternalAsync(OpenApiDocument openApiDocument, DirectoryInfo outputDirectory, string name)
        {
            var schema = QueryFormattableSchemas(openApiDocument).Single(candidate => candidate.Key == name);

            return await this.WriteFormatterAsync(schema, outputDirectory);
        }

        /// <summary>
        /// Applies the formatter template to the schema and writes the result to disk
        /// </summary>
        /// <param name="schema">
        /// The schema to generate the formatter for
        /// </param>
        /// <param name="outputDirectory">
        /// The target <see cref="DirectoryInfo"/>
        /// </param>
        /// <returns>
        /// The generated code
        /// </returns>
        private async Task<string> WriteFormatterAsync(KeyValuePair<string, IOpenApiSchema> schema, DirectoryInfo outputDirectory)
        {
            var generatedCode = this.CodeCleanup(this.Templates[FormatterTemplateName](schema));

            await WriteAsync(generatedCode, outputDirectory, $"{schema.Key}MessagePackFormatter.cs");

            return generatedCode;
        }

        /// <summary>
        /// Applies the payload templates to the schemas of one family and writes the results to disk
        /// </summary>
        /// <param name="schemas">
        /// The schemas that a formatter is generated for
        /// </param>
        /// <param name="outputDirectory">
        /// The target <see cref="DirectoryInfo"/>
        /// </param>
        /// <param name="isRequestFamily">
        /// Asserts that the envelope carries the request family rather than the response family
        /// </param>
        /// <returns>
        /// an awaitable <see cref="Task"/>
        /// </returns>
        private async Task WritePayloadAsync(IReadOnlyList<KeyValuePair<string, IOpenApiSchema>> schemas, DirectoryInfo outputDirectory, bool isRequestFamily)
        {
            var payloadName = isRequestFamily ? "RequestPayload" : "ResponsePayload";

            var entries = schemas
                .Where(schema => schema.Key.EndsWith(RequestSchemaNameSuffix, StringComparison.Ordinal) == isRequestFamily)
                .Select((schema, index) => new Dictionary<string, object>
                {
                    ["SchemaName"] = schema.Key,
                    ["InstanceName"] = char.ToLowerInvariant(schema.Key[0]) + schema.Key[1..],
                    ["SlotIndex"] = index + 1
                })
                .ToList();

            var model = new Dictionary<string, object>
            {
                ["PayloadName"] = payloadName,
                ["MarkerInterface"] = isRequestFamily ? "IRequest" : "IResponse",
                ["Family"] = isRequestFamily ? "request" : "response",
                ["SlotCount"] = entries.Count + 1,
                ["Entries"] = entries
            };

            var generatedPayload = this.CodeCleanup(this.Templates[PayloadTemplateName](model));

            await WriteAsync(generatedPayload, outputDirectory, $"{payloadName}.cs");

            var generatedFactory = this.CodeCleanup(this.Templates[PayloadFactoryTemplateName](model));

            await WriteAsync(generatedFactory, outputDirectory, $"{payloadName}Factory.cs");

            var generatedFormatter = this.CodeCleanup(this.Templates[PayloadFormatterTemplateName](model));

            await WriteAsync(generatedFormatter, outputDirectory, $"{payloadName}MessagePackFormatter.cs");
        }
    }
}
