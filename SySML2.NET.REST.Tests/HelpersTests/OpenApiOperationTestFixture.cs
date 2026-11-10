// ------------------------------------------------------------------------------------------------
// <copyright file="OpenApiOperationTestFixture.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SySML2.NET.REST.Tests.HelpersTests
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Text.Json;

    using Json.Schema;

    using NUnit.Framework;

    using SySML2.NET.REST.Tests.Helpers;

    [TestFixture]
    public class OpenApiOperationTestFixture
    {
        private const string ErrorBody = "{\"@type\":\"Error\",\"description\":\"Resource not found\"}";

        private const string DeleteCommitBody = "{\"@type\":\"Commit\",\"change\":[{\"@type\":\"DataVersion\",\"identity\":{\"@id\":\"3fa85f64-5717-4562-b3fc-2c963f66afa6\"},\"payload\":null}]}";

        private string documentPath;

        private OpenApiDocument openApi;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            this.documentPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Resources", "ptc-25-02-30.json");
            this.openApi = OpenApiDocument.Load(this.documentPath);
        }

        [Test]
        public void VerifyCheckRequest()
        {
            var getElements = this.Operation("GET /projects/{projectId}/commits/{commitId}/elements");
            var getRelationships = this.Operation("GET /projects/{projectId}/commits/{commitId}/elements/{relatedElementId}/relationships");
            var postProject = this.Operation("POST /projects");
            var postCommit = this.Operation("POST /projects/{projectId}/commits");
            var merge = this.Operation("POST /projects/{projectId}/branches/{targetBranchId}/merge");

            using (Assert.EnterMultipleScope())
            {
                Assert.That(getElements.CheckRequest("?page%5Bsize%5D=10&page%5Bafter%5D=abc&excludeUsed=true", null, string.Empty), Is.Empty);
                Assert.That(getElements.CheckRequest(null, null, string.Empty), Is.Empty);
                Assert.That(Kinds(getElements.CheckRequest("page%5Bsize%5D=ten", null, string.Empty)), Is.EqualTo((ContractViolationKind[])[ContractViolationKind.InvalidParameter]));
                Assert.That(Kinds(getElements.CheckRequest("?excludeUsed=True", null, string.Empty)), Is.EqualTo((ContractViolationKind[])[ContractViolationKind.InvalidParameter]));
                Assert.That(Kinds(getElements.CheckRequest("?page%5Bnumber%5D=2", null, string.Empty)), Is.EqualTo((ContractViolationKind[])[ContractViolationKind.UndeclaredParameter]));
                Assert.That(Kinds(getElements.CheckRequest(string.Empty, "application/json", "{}")), Is.EqualTo((ContractViolationKind[])[ContractViolationKind.UnexpectedBody]));
                Assert.That(getRelationships.CheckRequest("?direction=in", null, string.Empty), Is.Empty);
                Assert.That(Kinds(getRelationships.CheckRequest("?direction=sideways", null, string.Empty)), Is.EqualTo((ContractViolationKind[])[ContractViolationKind.InvalidParameter]));
                Assert.That(postProject.CheckRequest(string.Empty, "application/json", this.Example("ProjectRequest")), Is.Empty);
                Assert.That(postProject.CheckRequest(string.Empty, null, string.Empty), Is.Empty);
                Assert.That(Kinds(postProject.CheckRequest(string.Empty, "application/json", "{\"@type\":\"Project\",\"name\":5}")), Is.EqualTo((ContractViolationKind[])[ContractViolationKind.SchemaMismatch]));
                Assert.That(Kinds(postProject.CheckRequest(string.Empty, "text/plain", "name")), Is.EqualTo((ContractViolationKind[])[ContractViolationKind.UndeclaredMediaType]));
                Assert.That(postCommit.CheckRequest("?branchId=3fa85f64-5717-4562-b3fc-2c963f66afa6", "application/json", DeleteCommitBody), Is.Empty);
                Assert.That(Kinds(postCommit.CheckRequest("?branchId=main", "application/json", DeleteCommitBody)), Is.EqualTo((ContractViolationKind[])[ContractViolationKind.InvalidParameter]));
                Assert.That(Kinds(postCommit.CheckRequest(string.Empty, null, string.Empty)), Is.EqualTo((ContractViolationKind[])[ContractViolationKind.UnexpectedBody]));
                Assert.That(Kinds(merge.CheckRequest(string.Empty, null, string.Empty)), Is.EqualTo((ContractViolationKind[])[ContractViolationKind.MissingParameter]));
                Assert.That(merge.CheckRequest("?sourceCommitId=3fa85f64-5717-4562-b3fc-2c963f66afa6,3fa85f64-5717-4562-b3fc-2c963f66afa7", null, string.Empty), Is.Empty);
            }
        }

        [Test]
        public void VerifyCheckResponse()
        {
            var getProject = this.Operation("GET /projects/{projectId}");
            var deleteProject = this.Operation("DELETE /projects/{projectId}");
            var json = new Dictionary<string, JsonSchema> { ["application/json"] = JsonSchema.FromText("{\"type\":\"object\"}") };
            var withoutDefault = new OpenApiOperation(HttpMethod.Get, "/things", [], null, false, new Dictionary<string, OpenApiContent> { ["200"] = new(json), ["4XX"] = new(json) });

            using (Assert.EnterMultipleScope())
            {
                Assert.That(getProject.CheckResponse(HttpStatusCode.OK, "application/json", this.Example("Project")), Is.Empty);
                Assert.That(Kinds(getProject.CheckResponse(HttpStatusCode.OK, "application/json", "{\"@type\":\"Project\"}")), Is.EqualTo((ContractViolationKind[])[ContractViolationKind.SchemaMismatch]));
                Assert.That(getProject.CheckResponse(HttpStatusCode.NotFound, "application/json", ErrorBody), Is.Empty);
                Assert.That(getProject.CheckResponse(HttpStatusCode.Forbidden, "application/json", ErrorBody), Is.Empty);
                Assert.That(Kinds(getProject.CheckResponse(HttpStatusCode.NotFound, null, string.Empty)), Is.EqualTo((ContractViolationKind[])[ContractViolationKind.UnexpectedBody]));
                Assert.That(Kinds(getProject.CheckResponse(HttpStatusCode.NotFound, "text/plain", "org.openmbee.flexo.mms.Http404Exception")), Is.EqualTo((ContractViolationKind[])[ContractViolationKind.UndeclaredMediaType]));
                Assert.That(Kinds(getProject.CheckResponse(HttpStatusCode.NotFound, "application/json", "not json")), Is.EqualTo((ContractViolationKind[])[ContractViolationKind.InvalidJson]));
                Assert.That(deleteProject.CheckResponse(HttpStatusCode.NoContent, null, string.Empty), Is.Empty);
                Assert.That(withoutDefault.CheckResponse(HttpStatusCode.NotFound, "application/json", "{}"), Is.Empty);
                Assert.That(Kinds(withoutDefault.CheckResponse(HttpStatusCode.InternalServerError, "application/json", "{}")), Is.EqualTo((ContractViolationKind[])[ContractViolationKind.UndeclaredStatus]));
            }
        }

        [Test]
        public void VerifyMatches()
        {
            var getElement = this.Operation("GET /projects/{projectId}/commits/{commitId}/elements/{elementId}");

            using (Assert.EnterMultipleScope())
            {
                Assert.That(getElement.Matches(["projects", "p", "commits", "c", "elements", "e"]), Is.True);
                Assert.That(getElement.Matches(["projects", "p", "commits", "c", "roots", "e"]), Is.False);
                Assert.That(getElement.Matches(["projects", "p", "commits", "c", "elements"]), Is.False);
                Assert.That(getElement.Matches(["Projects", "p", "commits", "c", "elements", "e"]), Is.False);
            }
        }

        [Test]
        public void VerifyOpenApiOperation()
        {
            Assert.That(() => new OpenApiOperation(null, "/things", [], null, false, new Dictionary<string, OpenApiContent>()), Throws.ArgumentNullException);

            var getProject = this.Operation("GET /projects/{projectId}");
            var postBranch = this.Operation("POST /projects/{projectId}/branches");

            using (Assert.EnterMultipleScope())
            {
                Assert.That(getProject.ToString(), Is.EqualTo("GET /projects/{projectId}"));
                Assert.That(getProject.LiteralSegmentCount, Is.EqualTo(1));
                Assert.That(getProject.DeclaresClientError, Is.True);
                Assert.That(getProject.RequestBody, Is.Null);
                Assert.That(getProject.Parameters.Select(parameter => (parameter.Name, parameter.Location, parameter.IsRequired)), Is.EqualTo(((string, string, bool)[])[("projectId", "path", true)]));
                Assert.That(postBranch.DeclaresClientError, Is.False);
                Assert.That(postBranch.IsRequestBodyRequired, Is.True);
                Assert.That(postBranch.RequestBody?.SchemasByMediaType.Keys, Is.EqualTo((string[])["application/json"]));
            }
        }

        private static List<ContractViolationKind> Kinds(IReadOnlyList<ContractViolation> violations) => violations.Select(violation => violation.Kind).ToList();

        private string Example(string name)
        {
            using var document = JsonDocument.Parse(File.ReadAllText(this.documentPath));

            return document.RootElement.GetProperty("components").GetProperty("examples").GetProperty(name).GetProperty("value").GetRawText();
        }

        private OpenApiOperation Operation(string text) => this.openApi.Operations.Single(operation => operation.ToString() == text);
    }
}
