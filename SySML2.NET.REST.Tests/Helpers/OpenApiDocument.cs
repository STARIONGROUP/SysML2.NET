// ------------------------------------------------------------------------------------------------
// <copyright file="OpenApiDocument.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SySML2.NET.REST.Tests.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net.Http;
    using System.Text.Json;
    using System.Text.Json.Nodes;

    using Json.Schema;

    /// <summary>
    /// The operations of an OpenAPI 3.1 document, with their parameters, request bodies and responses as JSON
    /// Schema 2020-12, and the matching of request paths to operations
    /// </summary>
    /// <remarks>
    /// The component schemas carry <c>$id</c>s, some relative, and are referenced both as
    /// <c>#/components/schemas/…</c> and by those <c>$id</c>s; the <c>$id</c>s are removed and every reference made
    /// absolute before any schema is built.
    /// </remarks>
    public sealed class OpenApiDocument
    {
        /// <summary>
        /// The base URI under which the component schemas are registered
        /// </summary>
        private const string DefinitionsUri = "urn:sysml2-net:openapi";

        /// <summary>
        /// The prefix of a reference to a component schema
        /// </summary>
        private const string SchemaReferencePrefix = "#/components/schemas/";

        /// <summary>
        /// The HTTP methods that OpenAPI path items can declare as operations
        /// </summary>
        private static readonly HashSet<string> OperationMethods = ["get", "put", "post", "delete", "options", "head", "patch", "trace"];

        /// <summary>
        /// Initializes a new instance of the <see cref="OpenApiDocument"/> class
        /// </summary>
        /// <param name="operations">
        /// The operations of the document
        /// </param>
        private OpenApiDocument(IReadOnlyList<OpenApiOperation> operations)
        {
            this.Operations = operations;
        }

        /// <summary>
        /// Gets the operations of the document
        /// </summary>
        public IReadOnlyList<OpenApiOperation> Operations { get; }

        /// <summary>
        /// Reads an OpenAPI 3.1 document
        /// </summary>
        /// <param name="path">
        /// The path of the OpenAPI JSON document
        /// </param>
        /// <returns>
        /// The <see cref="OpenApiDocument"/>
        /// </returns>
        public static OpenApiDocument Load(string path)
        {
            var root = JsonNode.Parse(File.ReadAllText(path))!.AsObject();
            var components = root["components"]?.AsObject() ?? [];
            var schemas = components["schemas"]?.AsObject() ?? [];

            var keysById = schemas
                .Where(schema => schema.Value?["$id"] is JsonValue)
                .GroupBy(schema => (string)schema.Value["$id"], schema => schema.Key, StringComparer.Ordinal)
                .Where(keys => keys.Count() == 1)
                .ToDictionary(keys => keys.Key, keys => keys.Single(), StringComparer.Ordinal);

            RemoveIds(schemas);
            RewriteSchemaReferences(root, keysById);

            var reader = new SchemaReader();

            reader.Build(new JsonObject
            {
                ["$schema"] = "https://json-schema.org/draft/2020-12/schema",
                ["$id"] = DefinitionsUri,
                ["$defs"] = schemas.DeepClone()
            }, new Uri(DefinitionsUri));

            var operations = root["paths"]!.AsObject()
                .SelectMany(pathItem => pathItem.Value!.AsObject()
                    .Where(operation => OperationMethods.Contains(operation.Key))
                    .Select(operation => ReadOperation(pathItem.Key, pathItem.Value.AsObject(), operation.Key, operation.Value!.AsObject(), components, reader)))
                .ToList();

            return new OpenApiDocument(operations);
        }

        /// <summary>
        /// Finds the operation that a request addresses; where several templates match, the one with the most
        /// literal segments wins
        /// </summary>
        /// <param name="method">
        /// The method of the request
        /// </param>
        /// <param name="relativePath">
        /// The path of the request relative to the base address of the API, without query
        /// </param>
        /// <returns>
        /// The matching <see cref="OpenApiOperation"/>, or <c>null</c> when none matches
        /// </returns>
        public OpenApiOperation Match(HttpMethod method, string relativePath)
        {
            var segments = relativePath.Split('/', StringSplitOptions.RemoveEmptyEntries);

            return this.Operations
                .Where(operation => operation.Method == method && operation.Matches(segments))
                .OrderByDescending(operation => operation.LiteralSegmentCount)
                .FirstOrDefault();
        }

        /// <summary>
        /// Reads the body declared by a request body or response object: a schema per media type
        /// </summary>
        /// <param name="declaration">
        /// The request body or response object, or <c>null</c>
        /// </param>
        /// <param name="reader">
        /// The reader that builds the schemas
        /// </param>
        /// <returns>
        /// The <see cref="OpenApiContent"/>, empty when no content is declared
        /// </returns>
        private static OpenApiContent ReadContent(JsonObject declaration, SchemaReader reader)
        {
            var content = declaration?["content"]?.AsObject() ?? [];

            return new OpenApiContent(content.ToDictionary(
                mediaType => mediaType.Key,
                mediaType => reader.Build(mediaType.Value?["schema"] ?? new JsonObject()),
                StringComparer.OrdinalIgnoreCase));
        }

        /// <summary>
        /// Reads one operation
        /// </summary>
        /// <param name="pathTemplate">
        /// The path template of the operation
        /// </param>
        /// <param name="pathItem">
        /// The path item that declares the operation
        /// </param>
        /// <param name="method">
        /// The lower-case method of the operation
        /// </param>
        /// <param name="operation">
        /// The operation object
        /// </param>
        /// <param name="components">
        /// The components that <c>$ref</c>s to parameters and responses resolve against
        /// </param>
        /// <param name="reader">
        /// The reader that builds the schemas
        /// </param>
        /// <returns>
        /// The <see cref="OpenApiOperation"/>
        /// </returns>
        private static OpenApiOperation ReadOperation(string pathTemplate, JsonObject pathItem, string method, JsonObject operation, JsonObject components, SchemaReader reader)
        {
            var parameters = (pathItem["parameters"]?.AsArray() ?? []).Concat(operation["parameters"]?.AsArray() ?? [])
                .Select(parameter => Resolve(parameter!.AsObject(), components))
                .Select(parameter => new OpenApiParameter(
                    (string)parameter["name"],
                    (string)parameter["in"],
                    parameter["required"]?.GetValue<bool>() ?? false,
                    parameter["schema"]?["type"]?.GetValueKind() == JsonValueKind.String ? (string)parameter["schema"]["type"] : null,
                    reader.Build(parameter["schema"] ?? new JsonObject())))
                .ToList();

            var requestBody = operation["requestBody"] is JsonObject body ? Resolve(body, components) : null;

            var responses = (operation["responses"]?.AsObject() ?? [])
                .ToDictionary(response => response.Key, response => ReadContent(Resolve(response.Value!.AsObject(), components), reader), StringComparer.OrdinalIgnoreCase);

            return new OpenApiOperation(
                new HttpMethod(method.ToUpperInvariant()),
                pathTemplate,
                parameters,
                requestBody == null ? null : ReadContent(requestBody, reader),
                requestBody?["required"]?.GetValue<bool>() ?? false,
                responses);
        }

        /// <summary>
        /// Removes every <c>$id</c> from a tree of schemas
        /// </summary>
        /// <param name="node">
        /// The root of the tree
        /// </param>
        private static void RemoveIds(JsonNode node)
        {
            switch (node)
            {
                case JsonObject jsonObject:
                    jsonObject.Remove("$id");

                    foreach (var child in jsonObject.Select(property => property.Value).ToList())
                    {
                        RemoveIds(child);
                    }

                    break;
                case JsonArray jsonArray:
                    foreach (var child in jsonArray)
                    {
                        RemoveIds(child);
                    }

                    break;
            }
        }

        /// <summary>
        /// Resolves an object that is a <c>$ref</c> to a component, e.g. <c>#/components/responses/404</c>
        /// </summary>
        /// <param name="declaration">
        /// The object, a reference or not
        /// </param>
        /// <param name="components">
        /// The components of the document
        /// </param>
        /// <returns>
        /// The referenced component, or <paramref name="declaration"/> when it is no reference
        /// </returns>
        /// <exception cref="InvalidOperationException">
        /// Thrown when the reference does not name a component of the document
        /// </exception>
        private static JsonObject Resolve(JsonObject declaration, JsonObject components)
        {
            if (declaration["$ref"]?.GetValue<string>() is not { } reference)
            {
                return declaration;
            }

            var parts = reference.Split('/');

            return parts is ["#", "components", var kind, var name] && components[kind]?[name] is JsonObject component
                ? component
                : throw new InvalidOperationException($"The reference {reference} names no component of the document");
        }

        /// <summary>
        /// Makes every reference to a component schema, by JSON pointer or by <c>$id</c>, absolute under
        /// <see cref="DefinitionsUri"/>
        /// </summary>
        /// <param name="node">
        /// The root of the tree
        /// </param>
        /// <param name="keysById">
        /// The key of every component schema, by its <c>$id</c>
        /// </param>
        private static void RewriteSchemaReferences(JsonNode node, IReadOnlyDictionary<string, string> keysById)
        {
            switch (node)
            {
                case JsonObject jsonObject:
                    if (jsonObject["$ref"] is JsonValue reference && reference.GetValue<string>() is { } target)
                    {
                        var key = target.StartsWith(SchemaReferencePrefix, StringComparison.Ordinal) ? target[SchemaReferencePrefix.Length..] : keysById.GetValueOrDefault(target);

                        if (key != null)
                        {
                            jsonObject["$ref"] = $"{DefinitionsUri}#/$defs/{key}";
                        }
                    }

                    foreach (var child in jsonObject.Select(property => property.Value).ToList())
                    {
                        RewriteSchemaReferences(child, keysById);
                    }

                    break;
                case JsonArray jsonArray:
                    foreach (var child in jsonArray)
                    {
                        RewriteSchemaReferences(child, keysById);
                    }

                    break;
            }
        }

        /// <summary>
        /// Builds schemas against one registry that holds the component schemas, each under a base URI of its own
        /// </summary>
        private sealed class SchemaReader
        {
            /// <summary>
            /// The options that every schema of the document is built with
            /// </summary>
            private readonly BuildOptions options = new() { SchemaRegistry = new SchemaRegistry() };

            /// <summary>
            /// The number of schemas built so far, which names the base URI of the next one
            /// </summary>
            private int count;

            /// <summary>
            /// Builds a schema
            /// </summary>
            /// <param name="schema">
            /// The schema
            /// </param>
            /// <param name="baseUri">
            /// The base URI of the schema, or <c>null</c> for a new one
            /// </param>
            /// <returns>
            /// The built <see cref="JsonSchema"/>
            /// </returns>
            public JsonSchema Build(JsonNode schema, Uri baseUri = null)
            {
                return JsonSchema.Build(JsonSerializer.SerializeToElement(schema), this.options, baseUri ?? new Uri($"{DefinitionsUri}:{++this.count}"));
            }
        }
    }
}
