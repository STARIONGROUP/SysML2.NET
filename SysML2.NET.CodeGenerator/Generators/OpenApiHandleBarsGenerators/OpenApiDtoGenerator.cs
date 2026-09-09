// -------------------------------------------------------------------------------------------------
// <copyright file="OpenApiDtoGenerator.cs" company="Starion Group S.A.">
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
    /// OpenAPI based handlebars generator for DTO
    /// </summary>
    public class OpenApiDtoGenerator : OpenApiHandleBarsGenerator
    {
        /// <summary>
        /// The suffix that marks a schema as one that is sent to the Systems Modeling API and Services
        /// </summary>
        private const string RequestSchemaNameSuffix = "Request";

        /// <summary>
        /// The name of the template that generates a data transfer object class
        /// </summary>
        private const string ClassTemplateName = "psm-dto-class-openapi-template";

        /// <summary>
        /// The name of the template that generates a marker interface
        /// </summary>
        private const string InterfaceTemplateName = "psm-dto-interface-openapi-template";

        /// <summary>
        /// The interfaces that the interface generated for a union schema extends
        /// </summary>
        private static readonly Dictionary<string, string> BaseInterfaceNames =
            new(StringComparer.Ordinal) { ["Data"] = "SysML2.NET.Common.IData" };

        /// <summary>
        /// The declared types of the properties whose schema has no C# equivalent, keyed by class and property name
        /// </summary>
        private static readonly Dictionary<(string ClassName, string PropertyName), string> PropertyTypeOverrides =
            new()
            {
                [("PrimitiveConstraint", "value")] = "List<ConstraintValue>",
                [("PrimitiveConstraintRequest", "value")] = "List<ConstraintValue>"
            };

        /// <summary>
        /// The interfaces implemented by the class generated for a schema name, scoped to the current generation run
        /// </summary>
        private ILookup<string, string> implementedInterfaces = Enumerable.Empty<string>().ToLookup(name => name);

        /// <summary>
        /// Generates the data transfer objects, marker interfaces and enumerations of the OpenAPI document
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
        /// Generates the data transfer object class of the specified schema
        /// </summary>
        /// <param name="openApiDocument">
        /// the <see cref="OpenApiDocument"/> that contains the OpenAPI model to generate from
        /// </param>
        /// <param name="outputDirectory">
        /// The target <see cref="DirectoryInfo"/>
        /// </param>
        /// <param name="name">
        /// The name of the schema to generate the class for
        /// </param>
        /// <returns>
        /// The generated code
        /// </returns>
        public Task<string> GenerateDataTransferObjectClassAsync(OpenApiDocument openApiDocument, DirectoryInfo outputDirectory, string name)
        {
            ArgumentNullException.ThrowIfNull(openApiDocument);
            ArgumentNullException.ThrowIfNull(outputDirectory);
            ArgumentException.ThrowIfNullOrWhiteSpace(name);

            return this.GenerateDataTransferObjectClassInternalAsync(openApiDocument, outputDirectory, name);
        }

        /// <summary>
        /// Generates the marker interface of the specified union schema
        /// </summary>
        /// <param name="openApiDocument">
        /// the <see cref="OpenApiDocument"/> that contains the OpenAPI model to generate from
        /// </param>
        /// <param name="outputDirectory">
        /// The target <see cref="DirectoryInfo"/>
        /// </param>
        /// <param name="name">
        /// The name of the union schema to generate the interface for
        /// </param>
        /// <returns>
        /// The generated code
        /// </returns>
        public Task<string> GenerateDataTransferObjectInterfaceAsync(OpenApiDocument openApiDocument, DirectoryInfo outputDirectory, string name)
        {
            ArgumentNullException.ThrowIfNull(openApiDocument);
            ArgumentNullException.ThrowIfNull(outputDirectory);
            ArgumentException.ThrowIfNullOrWhiteSpace(name);

            return this.GenerateDataTransferObjectInterfaceInternalAsync(openApiDocument, outputDirectory, name);
        }

        /// <summary>
        /// Register the custom helpers
        /// </summary>
        protected override void RegisterHelpers()
        {
            this.Handlebars.RegisterSchemaHelper(
                schemaName => this.implementedInterfaces[schemaName],
                schemaName => BaseInterfaceNames.TryGetValue(schemaName, out var baseInterfaceName) ? [baseInterfaceName] : [],
                QueryPropertyTypeOverride);
            this.Handlebars.RegisterPropertyHelper(QueryPropertyTypeOverride);
            this.Handlebars.RegisterDocumentationHelper();
            this.Handlebars.RegisterEnumerationHelper();
        }

        /// <summary>
        /// Register the code templates
        /// </summary>
        protected override void RegisterTemplates()
        {
            this.RegisterTemplate(ClassTemplateName);
            this.RegisterTemplate(InterfaceTemplateName);
        }

        /// <summary>
        /// Queries the interfaces implemented by the class generated for each in-scope schema
        /// </summary>
        /// <param name="schemas">
        /// The in-scope schemas
        /// </param>
        /// <returns>
        /// A lookup from schema name to the interfaces its generated class implements
        /// </returns>
        private static ILookup<string, string> QueryImplementedInterfaces(IReadOnlyList<KeyValuePair<string, IOpenApiSchema>> schemas)
        {
            var schemaNames = schemas.Select(schema => schema.Key).ToHashSet(StringComparer.Ordinal);

            var unionMemberships = schemas
                .Where(schema => schema.Value.QueryIsUnion())
                .SelectMany(union => union.Value.QueryUnionAlternativeNames()
                    .Where(schemaNames.Contains)
                    .Select(alternativeName => (Alternative: alternativeName, Interface: OpenApiSchemaExtensions.QueryInterfaceName(union.Key))));

            var familyMemberships = schemas
                .Where(schema => !schema.Value.QueryIsUnion())
                .SelectMany(schema => QueryFamilyInterfaces(schema.Key, schema.Value)
                    .Select(interfaceName => (Alternative: schema.Key, Interface: interfaceName)));

            return unionMemberships
                .Concat(familyMemberships)
                .ToLookup(pair => pair.Alternative, pair => pair.Interface, StringComparer.Ordinal);
        }

        /// <summary>
        /// Queries the family interfaces that the class generated for a schema implements
        /// </summary>
        /// <param name="schemaName">
        /// The name of the schema
        /// </param>
        /// <param name="schema">
        /// The <see cref="IOpenApiSchema"/> that the class is generated for
        /// </param>
        /// <returns>
        /// The family interfaces
        /// </returns>
        private static IEnumerable<string> QueryFamilyInterfaces(string schemaName, IOpenApiSchema schema)
        {
            yield return schemaName.EndsWith(RequestSchemaNameSuffix, StringComparison.Ordinal) ? "IRequest" : "IResponse";

            if (schema.QueryHasRequiredIdentifier())
            {
                yield return "SysML2.NET.Common.IIdentified";
            }
        }

        /// <summary>
        /// Queries the declared type of a property whose schema has no C# equivalent
        /// </summary>
        /// <param name="className">
        /// The name of the class that declares the property
        /// </param>
        /// <param name="propertyName">
        /// The name of the property as it appears in the schema
        /// </param>
        /// <returns>
        /// The declared type, or <c>null</c> when the property is resolved from its schema
        /// </returns>
        private static string QueryPropertyTypeOverride(string className, string propertyName)
        {
            return PropertyTypeOverrides.TryGetValue((className, propertyName), out var typeName) ? typeName : null;
        }

        /// <summary>
        /// Generates the data transfer objects, marker interfaces and enumerations of the OpenAPI document
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
            var schemas = this.AssignGenerationScope(openApiDocument);

            foreach (var schema in schemas)
            {
                if (schema.Value.QueryIsUnion())
                {
                    await this.WriteInterfaceAsync(schema, outputDirectory);

                    continue;
                }

                await this.WriteClassAsync(schema, outputDirectory);
            }
        }

        /// <summary>
        /// Generates the data transfer object class of the specified schema
        /// </summary>
        /// <param name="openApiDocument">
        /// the <see cref="OpenApiDocument"/> that contains the OpenAPI model to generate from
        /// </param>
        /// <param name="outputDirectory">
        /// The target <see cref="DirectoryInfo"/>
        /// </param>
        /// <param name="name">
        /// The name of the schema to generate the class for
        /// </param>
        /// <returns>
        /// The generated code
        /// </returns>
        private async Task<string> GenerateDataTransferObjectClassInternalAsync(OpenApiDocument openApiDocument, DirectoryInfo outputDirectory, string name)
        {
            var schemas = this.AssignGenerationScope(openApiDocument);

            return await this.WriteClassAsync(schemas.Single(schema => schema.Key == name), outputDirectory);
        }

        /// <summary>
        /// Generates the marker interface of the specified union schema
        /// </summary>
        /// <param name="openApiDocument">
        /// the <see cref="OpenApiDocument"/> that contains the OpenAPI model to generate from
        /// </param>
        /// <param name="outputDirectory">
        /// The target <see cref="DirectoryInfo"/>
        /// </param>
        /// <param name="name">
        /// The name of the union schema to generate the interface for
        /// </param>
        /// <returns>
        /// The generated code
        /// </returns>
        private async Task<string> GenerateDataTransferObjectInterfaceInternalAsync(OpenApiDocument openApiDocument, DirectoryInfo outputDirectory, string name)
        {
            var schemas = this.AssignGenerationScope(openApiDocument);

            return await this.WriteInterfaceAsync(schemas.Single(schema => schema.Key == name), outputDirectory);
        }

        /// <summary>
        /// Queries the in-scope schemas and assigns the state that the registered helpers read from
        /// </summary>
        /// <param name="openApiDocument">
        /// the <see cref="OpenApiDocument"/> that contains the OpenAPI model to generate from
        /// </param>
        /// <returns>
        /// The in-scope schemas, ordered by name
        /// </returns>
        private IReadOnlyList<KeyValuePair<string, IOpenApiSchema>> AssignGenerationScope(OpenApiDocument openApiDocument)
        {
            var schemas = QuerySchemas(openApiDocument);

            this.implementedInterfaces = QueryImplementedInterfaces(schemas);

            return schemas;
        }

        /// <summary>
        /// Applies the class template to the schema and writes the result to disk
        /// </summary>
        /// <param name="schema">
        /// The schema to generate the class for
        /// </param>
        /// <param name="outputDirectory">
        /// The target <see cref="DirectoryInfo"/>
        /// </param>
        /// <returns>
        /// The generated code
        /// </returns>
        private async Task<string> WriteClassAsync(KeyValuePair<string, IOpenApiSchema> schema, DirectoryInfo outputDirectory)
        {
            var generatedCode = this.CodeCleanup(this.Templates[ClassTemplateName](schema));

            await WriteAsync(generatedCode, outputDirectory, $"{schema.Key}.cs");

            return generatedCode;
        }

        /// <summary>
        /// Applies the interface template to the union schema and writes the result to disk
        /// </summary>
        /// <param name="schema">
        /// The union schema to generate the interface for
        /// </param>
        /// <param name="outputDirectory">
        /// The target <see cref="DirectoryInfo"/>
        /// </param>
        /// <returns>
        /// The generated code
        /// </returns>
        private async Task<string> WriteInterfaceAsync(KeyValuePair<string, IOpenApiSchema> schema, DirectoryInfo outputDirectory)
        {
            var generatedCode = this.CodeCleanup(this.Templates[InterfaceTemplateName](schema));

            await WriteAsync(generatedCode, outputDirectory, $"{OpenApiSchemaExtensions.QueryInterfaceName(schema.Key)}.cs");

            return generatedCode;
        }
    }
}
