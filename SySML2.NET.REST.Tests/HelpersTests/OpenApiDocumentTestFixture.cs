// ------------------------------------------------------------------------------------------------
// <copyright file="OpenApiDocumentTestFixture.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SySML2.NET.REST.Tests.HelpersTests
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Net.Http;

    using NUnit.Framework;

    using SySML2.NET.REST.Tests.Helpers;

    [TestFixture]
    public class OpenApiDocumentTestFixture
    {
        private const string SmallDocument = """
            {
              "openapi": "3.1.0",
              "paths": {
                "/a/{x}": {
                  "get": {
                    "parameters": [{ "$ref": "#/components/parameters/x" }],
                    "responses": {
                      "200": { "content": { "application/json": { "schema": { "$ref": "#/components/schemas/Thing" } } } },
                      "404": { "$ref": "#/components/responses/404" }
                    }
                  }
                },
                "/a/b": { "get": {} }
              },
              "components": {
                "parameters": { "x": { "name": "x", "in": "path", "required": true, "schema": { "type": "string" } } },
                "responses": { "404": { "content": { "application/json": { "schema": { "$ref": "relative/Name" } } } } },
                "schemas": {
                  "Thing": {
                    "$id": "relative/Thing",
                    "type": "object",
                    "properties": { "name": { "$ref": "#/components/schemas/Name" }, "other": { "$ref": "relative/Name" } },
                    "required": ["name"],
                    "additionalProperties": false
                  },
                  "Name": { "$id": "relative/Name", "type": "string" }
                }
              }
            }
            """;

        private OpenApiDocument openApi;

        private string smallDocumentPath;

        [SetUp]
        public void SetUp()
        {
            this.openApi = OpenApiDocument.Load(Path.Combine(TestContext.CurrentContext.TestDirectory, "Resources", "ptc-25-02-30.json"));
            this.smallDocumentPath = Path.Combine(TestContext.CurrentContext.WorkDirectory, $"{Guid.NewGuid()}.json");
        }

        [TearDown]
        public void TearDown()
        {
            File.Delete(this.smallDocumentPath);
        }

        [Test]
        public void VerifyLoad()
        {
            using (Assert.EnterMultipleScope())
            {
                Assert.That(this.openApi.Operations, Has.Count.EqualTo(35));
                Assert.That(this.openApi.Operations.Select(operation => operation.ToString()), Is.Unique);
                Assert.That(this.openApi.Operations.Single(operation => operation.ToString() == "GET /projects").DeclaresClientError, Is.False);
                Assert.That(this.openApi.Operations.Single(operation => operation.ToString() == "GET /projects/{projectId}").DeclaresClientError, Is.True);
                Assert.That(this.openApi.Operations.Single(operation => operation.ToString() == "GET /projects/{projectId}").Responses["404"].SchemasByMediaType.Keys, Is.EqualTo((string[])["application/json"]));
                Assert.That(this.openApi.Operations.Single(operation => operation.ToString() == "DELETE /projects/{projectId}").Responses["204"].SchemasByMediaType.Keys, Is.EqualTo((string[])["*/*"]));
            }

            File.WriteAllText(this.smallDocumentPath, SmallDocument);

            var small = OpenApiDocument.Load(this.smallDocumentPath);
            var thing = small.Operations.Single(operation => operation.PathTemplate == "/a/{x}");
            var bare = small.Operations.Single(operation => operation.PathTemplate == "/a/b");

            using (Assert.EnterMultipleScope())
            {
                Assert.That(small.Operations, Has.Count.EqualTo(2));
                Assert.That(thing.Parameters.Select(parameter => parameter.Name), Is.EqualTo((string[])["x"]));
                Assert.That(thing.CheckResponse(HttpStatusCode.OK, "application/json", "{\"name\":\"n\",\"other\":\"o\"}"), Is.Empty);
                Assert.That(thing.CheckResponse(HttpStatusCode.OK, "application/json", "{\"name\":5}").Select(violation => violation.Kind), Is.EqualTo((ContractViolationKind[])[ContractViolationKind.SchemaMismatch]));
                Assert.That(thing.CheckResponse(HttpStatusCode.OK, "application/json", "{\"name\":\"n\",\"other\":5}").Select(violation => violation.Kind), Is.EqualTo((ContractViolationKind[])[ContractViolationKind.SchemaMismatch]));
                Assert.That(thing.CheckResponse(HttpStatusCode.NotFound, "application/json", "\"missing\""), Is.Empty);
                Assert.That(thing.DeclaresClientError, Is.True);
                Assert.That(bare.Responses, Is.Empty);
                Assert.That(bare.DeclaresClientError, Is.False);
                Assert.That(bare.CheckResponse(HttpStatusCode.OK, null, string.Empty).Select(violation => violation.Kind), Is.EqualTo((ContractViolationKind[])[ContractViolationKind.UndeclaredStatus]));
            }

            File.WriteAllText(this.smallDocumentPath, SmallDocument.Replace("#/components/responses/404", "#/components/responses/410"));

            Assert.That(() => OpenApiDocument.Load(this.smallDocumentPath), Throws.InvalidOperationException.With.Message.Contains("#/components/responses/410"));
        }

        [Test]
        public void VerifyMatch()
        {
            var commitId = "5d2c1a3e-0000-4000-8000-000000000001";

            using (Assert.EnterMultipleScope())
            {
                Assert.That(this.openApi.Match(HttpMethod.Get, "/projects")?.PathTemplate, Is.EqualTo("/projects"));
                Assert.That(this.openApi.Match(HttpMethod.Post, "/projects")?.Method, Is.EqualTo(HttpMethod.Post));
                Assert.That(this.openApi.Match(HttpMethod.Get, $"/projects/p/commits/{commitId}/elements/e")?.PathTemplate, Is.EqualTo("/projects/{projectId}/commits/{commitId}/elements/{elementId}"));
                Assert.That(this.openApi.Match(HttpMethod.Get, $"/projects/p/commits/{commitId}/elements/e/relationships")?.PathTemplate, Is.EqualTo("/projects/{projectId}/commits/{commitId}/elements/{relatedElementId}/relationships"));
                Assert.That(this.openApi.Match(HttpMethod.Get, $"/projects/p/commits/{commitId}/elements/e/projectUsage")?.PathTemplate, Is.EqualTo("/projects/{projectId}/commits/{commitId}/elements/{elementId}/projectUsage"));
                Assert.That(this.openApi.Match(HttpMethod.Get, $"/projects/p/commits/{commitId}/diff")?.PathTemplate, Is.EqualTo("/projects/{projectId}/commits/{compareCommitId}/diff"));
                Assert.That(this.openApi.Match(HttpMethod.Patch, "/projects"), Is.Null);
                Assert.That(this.openApi.Match(HttpMethod.Get, "/projects/p/unknown"), Is.Null);
                Assert.That(this.openApi.Match(HttpMethod.Get, "/"), Is.Null);
            }

            File.WriteAllText(this.smallDocumentPath, SmallDocument);

            var small = OpenApiDocument.Load(this.smallDocumentPath);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(small.Match(HttpMethod.Get, "/a/b")?.PathTemplate, Is.EqualTo("/a/b"));
                Assert.That(small.Match(HttpMethod.Get, "/a/c")?.PathTemplate, Is.EqualTo("/a/{x}"));
            }
        }
    }
}
