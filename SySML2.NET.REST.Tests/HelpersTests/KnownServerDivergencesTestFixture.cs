// ------------------------------------------------------------------------------------------------
// <copyright file="KnownServerDivergencesTestFixture.cs" company="Starion Group S.A.">
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
    using System.Threading.Tasks;

    using NUnit.Framework;
    using NUnit.Framework.Interfaces;

    using SySML2.NET.REST.Tests.Helpers;

    using SysML2.NET.PSM.DTO;
    using SysML2.NET.REST.Exceptions;

    [TestFixture]
    public class KnownServerDivergencesTestFixture
    {
        private static readonly EndpointCall Conforming = new(null, "GET /projects/p", HttpStatusCode.NotFound);

        private static readonly EndpointCall Breaking = Conforming with { ResponseViolations = [new ContractViolation(ContractViolationKind.UnexpectedBody, "no body")] };

        [Test]
        public void VerifyAll()
        {
            using (Assert.EnterMultipleScope())
            {
                Assert.That(KnownServerDivergences.All, Is.EquivalentTo((ServerDivergence[])[KnownServerDivergences.RootsIgnoreOwningRelationship, KnownServerDivergences.DiffNotImplemented, KnownServerDivergences.MergeNotImplemented, KnownServerDivergences.MergeRequiresArrayBody, KnownServerDivergences.UnknownResourceNotFound, KnownServerDivergences.UnknownResourceAccepted, KnownServerDivergences.ErrorWithoutErrorBody, KnownServerDivergences.CreatedAnsweredOk, KnownServerDivergences.RecordsWithoutRequiredProperties, KnownServerDivergences.PreviousCommitAsObject, KnownServerDivergences.ElementsWithoutDerivedProperties, KnownServerDivergences.DatatypeIdNotUri, KnownServerDivergences.DataListedAsElements, KnownServerDivergences.ProjectUsageAsArray, KnownServerDivergences.QueryResultsProjected, KnownServerDivergences.QueryLosesNameAndDescription, KnownServerDivergences.InstanceOfRejected, KnownServerDivergences.QueryResultsGetNotImplemented, KnownServerDivergences.DeleteDoesNotCascade, KnownServerDivergences.DeleteUnknownBranchFails, KnownServerDivergences.DeletedProjectDeletedAgain]));
                Assert.That(KnownServerDivergences.All.Select(divergence => divergence.Reference), Has.All.Contains("Open-MBEE/flexo-mms-sysmlv2@836d904d"));
            }
        }

        [Test]
        public void VerifyCreatedAnsweredOk()
        {
            var openApi = OpenApiDocument.Load(Path.Combine(TestContext.CurrentContext.TestDirectory, "Resources", "ptc-25-02-30.json"));
            var postProject = openApi.Operations.Single(operation => operation.ToString() == "POST /projects");
            var postQueryResults = openApi.Operations.Single(operation => operation.ToString() == "POST /projects/{projectId}/query-results");
            var getProject = openApi.Operations.Single(operation => operation.ToString() == "GET /projects/{projectId}");
            var explains = KnownServerDivergences.CreatedAnsweredOk.Explains;
            var mismatch = new ContractViolation(ContractViolationKind.SchemaMismatch, "/@type const: Expected \"Error\"");

            using (Assert.EnterMultipleScope())
            {
                Assert.That(explains(new EndpointCall(postProject, "POST /projects", HttpStatusCode.OK), mismatch), Is.True);
                Assert.That(explains(new EndpointCall(postProject, "POST /projects", HttpStatusCode.Created), mismatch), Is.False);
                Assert.That(explains(new EndpointCall(postQueryResults, "POST /projects/p/query-results", HttpStatusCode.OK), mismatch), Is.False);
                Assert.That(explains(new EndpointCall(getProject, "GET /projects/p", HttpStatusCode.OK), mismatch), Is.False);
                Assert.That(explains(new EndpointCall(null, "POST /unknown", HttpStatusCode.OK), mismatch), Is.False);
            }
        }

        [Test]
        public void VerifyDataListedAsElements()
        {
            var openApi = OpenApiDocument.Load(Path.Combine(TestContext.CurrentContext.TestDirectory, "Resources", "ptc-25-02-30.json"));
            var getElements = openApi.Operations.Single(operation => operation.ToString() == "GET /projects/{projectId}/commits/{commitId}/elements");
            var getRoots = openApi.Operations.Single(operation => operation.ToString() == "GET /projects/{projectId}/commits/{commitId}/roots");
            var getChanges = openApi.Operations.Single(operation => operation.ToString() == "GET /projects/{projectId}/commits/{commitId}/changes");
            var explains = KnownServerDivergences.DataListedAsElements.Explains;
            static ContractViolation UnknownType(string type) => new(ContractViolationKind.SchemaMismatch, $"/2/@type const: \"{type}\" is none of the types the schema admits");

            using (Assert.EnterMultipleScope())
            {
                Assert.That(explains(new EndpointCall(getElements, "GET /projects/p/commits/c/elements", HttpStatusCode.OK), UnknownType("ExternalData")), Is.True);
                Assert.That(explains(new EndpointCall(getRoots, "GET /projects/p/commits/c/roots", HttpStatusCode.OK), UnknownType("ProjectUsage")), Is.True);
                Assert.That(explains(new EndpointCall(getElements, "GET /projects/p/commits/c/elements", HttpStatusCode.OK), UnknownType("ExternalRelationship")), Is.True);
                Assert.That(explains(new EndpointCall(getElements, "GET /projects/p/commits/c/elements", HttpStatusCode.OK), UnknownType("Pakage")), Is.False, "a misspelt metaclass is no kind of data");
                Assert.That(explains(new EndpointCall(getChanges, "GET /projects/p/commits/c/changes", HttpStatusCode.OK), UnknownType("ExternalData")), Is.False);
                Assert.That(explains(new EndpointCall(null, "GET /unknown", HttpStatusCode.OK), UnknownType("ExternalData")), Is.False);
                Assert.That(explains(new EndpointCall(getElements, "GET /projects/p/commits/c/elements", HttpStatusCode.OK), UnknownType("ExternalData") with { Kind = ContractViolationKind.InvalidJson }), Is.False, "only a schema mismatch is explained");
            }

            var dataItem = getElements.CheckResponse(HttpStatusCode.OK, "application/json", "[{\"@type\":\"Package\",\"@id\":\"3fa85f64-5717-4562-b3fc-2c963f66afa6\"},{\"@type\":\"ExternalData\",\"@id\":\"3fa85f64-5717-4562-b3fc-2c963f66afa7\",\"resourceIdentifier\":\"https://example.org/r\"}]");
            var misspelt = getElements.CheckResponse(HttpStatusCode.OK, "application/json", "[{\"@type\":\"Pakage\",\"@id\":\"3fa85f64-5717-4562-b3fc-2c963f66afa6\"}]");
            var call = new EndpointCall(getElements, "GET /projects/p/commits/c/elements", HttpStatusCode.OK);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(dataItem.Where(violation => explains(call, violation)).Select(violation => violation.Message), Has.Some.StartsWith("/1/@type"), "the violation the conformance check produces for a data item is explained");
                Assert.That(misspelt, Is.Not.Empty.And.None.Matches<ContractViolation>(violation => explains(call, violation)));
            }
        }

        [Test]
        public void VerifyDatatypeIdNotUri()
        {
            var openApi = OpenApiDocument.Load(Path.Combine(TestContext.CurrentContext.TestDirectory, "Resources", "ptc-25-02-30.json"));
            var getDatatype = openApi.Operations.Single(operation => operation.ToString() == "GET /meta/datatypes/{datatypeId}");
            var getProject = openApi.Operations.Single(operation => operation.ToString() == "GET /projects/{projectId}");
            var explains = KnownServerDivergences.DatatypeIdNotUri.Explains;
            var idNotUri = new ContractViolation(ContractViolationKind.SchemaMismatch, "/$id format: Value does not match format \"uri\"");

            using (Assert.EnterMultipleScope())
            {
                Assert.That(explains(new EndpointCall(getDatatype, "GET /meta/datatypes/Project", HttpStatusCode.OK), idNotUri), Is.True);
                Assert.That(explains(new EndpointCall(getDatatype, "GET /meta/datatypes/Project", HttpStatusCode.OK), new ContractViolation(ContractViolationKind.SchemaMismatch, "/type const: Expected \"object\"")), Is.False);
                Assert.That(explains(new EndpointCall(getProject, "GET /projects/p", HttpStatusCode.OK), idNotUri), Is.False);
            }
        }

        [Test]
        public void VerifyElementsWithoutDerivedProperties()
        {
            var explains = KnownServerDivergences.ElementsWithoutDerivedProperties.Explains;
            var call = Conforming with { StatusCode = HttpStatusCode.OK };

            using (Assert.EnterMultipleScope())
            {
                Assert.That(explains(call, Missing("/", "\"owner\",\"qualifiedName\"")), Is.True);
                Assert.That(explains(call, Missing("/3/payload", "\"documentation\"")), Is.True);
                Assert.That(explains(call, Missing("/", "\"owner\",\"declaredName\"")), Is.False);
                Assert.That(explains(call, Missing("/", "\"alias\"")), Is.False);
                Assert.That(explains(call, new ContractViolation(ContractViolationKind.SchemaMismatch, "/ additionalProperties: [\"owner\"]")), Is.False);
                Assert.That(explains(call, new ContractViolation(ContractViolationKind.InvalidParameter, "/ required: Required properties [\"owner\"] are not present")), Is.False);
            }
        }

        [Test]
        public void VerifyErrorWithoutErrorBody()
        {
            var explains = KnownServerDivergences.ErrorWithoutErrorBody.Explains;
            var forbidden = Conforming with { StatusCode = HttpStatusCode.Forbidden };
            var success = Conforming with { StatusCode = HttpStatusCode.OK };

            using (Assert.EnterMultipleScope())
            {
                Assert.That(explains(Conforming, new ContractViolation(ContractViolationKind.UnexpectedBody, "no body")), Is.True);
                Assert.That(explains(forbidden, new ContractViolation(ContractViolationKind.UndeclaredMediaType, "text/plain")), Is.True);
                Assert.That(explains(Conforming, new ContractViolation(ContractViolationKind.SchemaMismatch, "an Error without description")), Is.False);
                Assert.That(explains(success, new ContractViolation(ContractViolationKind.UnexpectedBody, "no body")), Is.False);
                Assert.That(KnownServerDivergences.UnknownResourceNotFound.Explains, Is.Null);
            }
        }

        [Test]
        public async Task VerifyExpectNotFoundAsync()
        {
            var notFound = KnownServerDivergences.UnknownResourceNotFound;
            var accepted = KnownServerDivergences.UnknownResourceAccepted;
            var branchFails = KnownServerDivergences.DeleteUnknownBranchFails;

            using (Assert.EnterMultipleScope())
            {
                Assert.That(await Outcome(Answer(HttpStatusCode.NotFound), null, Conforming), Is.Empty);
                Assert.That(await Outcome(Answer(HttpStatusCode.NotFound, withError: false), null, Conforming), Is.EqualTo((AssertionStatus[])[AssertionStatus.Failed]));
                Assert.That(await Outcome(Answer(HttpStatusCode.NotFound, withError: false), null, Breaking), Is.Empty);
                Assert.That(await Outcome(Answer(HttpStatusCode.NotFound, withError: false), null, null), Is.Empty);
                Assert.That(await Outcome(Answer(HttpStatusCode.Forbidden), null, Conforming), Is.EqualTo((AssertionStatus[])[AssertionStatus.Failed]));
                Assert.That(await Outcome(Succeed, null, Conforming), Is.EqualTo((AssertionStatus[])[AssertionStatus.Failed]));

                Assert.That(await Outcome(Answer(HttpStatusCode.Forbidden), notFound, Conforming), Is.EqualTo((AssertionStatus[])[AssertionStatus.Warning]));
                Assert.That(await Outcome(Answer(HttpStatusCode.NotFound), notFound, Conforming), Is.Empty);
                Assert.That(await Outcome(Answer(HttpStatusCode.InternalServerError), notFound, Conforming), Is.EqualTo((AssertionStatus[])[AssertionStatus.Failed]));
                Assert.That(await Outcome(Succeed, notFound, Conforming), Is.EqualTo((AssertionStatus[])[AssertionStatus.Failed]));

                Assert.That(await Outcome(Succeed, accepted, Conforming), Is.EqualTo((AssertionStatus[])[AssertionStatus.Warning]));
                Assert.That(await Outcome(Answer(HttpStatusCode.NotFound), accepted, Conforming), Is.Empty);
                Assert.That(await Outcome(Answer(HttpStatusCode.Forbidden), accepted, Conforming), Is.EqualTo((AssertionStatus[])[AssertionStatus.Failed]));

                Assert.That(await Outcome(Answer(HttpStatusCode.InternalServerError), branchFails, Conforming), Is.EqualTo((AssertionStatus[])[AssertionStatus.Warning]));
                Assert.That(await Outcome(Answer(HttpStatusCode.NotFound), branchFails, Conforming), Is.Empty);
                Assert.That(await Outcome(Answer(HttpStatusCode.Forbidden), branchFails, Conforming), Is.EqualTo((AssertionStatus[])[AssertionStatus.Failed]));
                Assert.That(await Outcome(Succeed, branchFails, Conforming), Is.EqualTo((AssertionStatus[])[AssertionStatus.Failed]));
            }

            var warning = await IsolatedAssertions.RunAsync(() => KnownServerDivergences.ExpectNotFoundAsync(Answer(HttpStatusCode.Forbidden), () => Conforming, notFound, "the call"));
            var lostError = await IsolatedAssertions.RunAsync(() => KnownServerDivergences.ExpectNotFoundAsync(Answer(HttpStatusCode.NotFound, withError: false), () => Conforming, null, "the call"));

            using (Assert.EnterMultipleScope())
            {
                Assert.That(warning[0].Message, Does.Contain("the call answered 403 Forbidden").And.Contain(notFound.Summary));
                Assert.That(lostError[0].Message, Does.Contain("the call: the 404 body conforms to the OpenAPI document, but the client read no Error from it"));
            }

            await Assert.ThatAsync(() => KnownServerDivergences.ExpectNotFoundAsync(() => throw new InvalidOperationException("a client defect"), () => Conforming, notFound), Throws.InvalidOperationException);
        }

        [Test]
        public void VerifyPreviousCommitAsObject()
        {
            var explains = KnownServerDivergences.PreviousCommitAsObject.Explains;
            var call = Conforming with { StatusCode = HttpStatusCode.OK };

            using (Assert.EnterMultipleScope())
            {
                Assert.That(explains(call, new ContractViolation(ContractViolationKind.SchemaMismatch, "/previousCommit type: Value is \"object\" but should be \"array\"")), Is.True);
                Assert.That(explains(call, new ContractViolation(ContractViolationKind.SchemaMismatch, "/0/previousCommit type: Value is \"object\" but should be \"array\"")), Is.True);
                Assert.That(explains(call, new ContractViolation(ContractViolationKind.SchemaMismatch, "/change type: Value is \"object\" but should be \"array\"")), Is.False);
                Assert.That(explains(call, new ContractViolation(ContractViolationKind.InvalidJson, "/previousCommit type: Value is \"object\" but should be \"array\"")), Is.False);
                Assert.That(KnownServerDivergences.All.Where(divergence => divergence.BreaksDeserialization), Is.EquivalentTo((ServerDivergence[])[KnownServerDivergences.PreviousCommitAsObject, KnownServerDivergences.DataListedAsElements, KnownServerDivergences.ProjectUsageAsArray, KnownServerDivergences.QueryResultsProjected]));
            }
        }

        [Test]
        public void VerifyProjectUsageAsArray()
        {
            var openApi = OpenApiDocument.Load(Path.Combine(TestContext.CurrentContext.TestDirectory, "Resources", "ptc-25-02-30.json"));
            var getUsage = openApi.Operations.Single(operation => operation.ToString() == "GET /projects/{projectId}/commits/{commitId}/elements/{elementId}/projectUsage");
            var getElement = openApi.Operations.Single(operation => operation.ToString() == "GET /projects/{projectId}/commits/{commitId}/elements/{elementId}");
            var explains = KnownServerDivergences.ProjectUsageAsArray.Explains;
            var array = new ContractViolation(ContractViolationKind.SchemaMismatch, "/ type: Value is \"array\" but should be \"object\"");

            using (Assert.EnterMultipleScope())
            {
                Assert.That(explains(new EndpointCall(getUsage, "GET /projects/p/commits/c/elements/e/projectUsage", HttpStatusCode.OK), array), Is.True);
                Assert.That(explains(new EndpointCall(getUsage, "GET /projects/p/commits/c/elements/e/projectUsage", HttpStatusCode.OK), new ContractViolation(ContractViolationKind.SchemaMismatch, "/usedProject type: Value is \"array\" but should be \"object\"")), Is.False);
                Assert.That(explains(new EndpointCall(getElement, "GET /projects/p/commits/c/elements/e", HttpStatusCode.OK), array), Is.False);
            }
        }

        [Test]
        public void VerifyQueryResultsProjected()
        {
            var openApi = OpenApiDocument.Load(Path.Combine(TestContext.CurrentContext.TestDirectory, "Resources", "ptc-25-02-30.json"));
            var queryResults = openApi.Operations.Single(operation => operation.ToString() == "GET /projects/{projectId}/queries/{queryId}/results");
            var postQueryResults = openApi.Operations.Single(operation => operation.ToString() == "POST /projects/{projectId}/query-results");
            var getElements = openApi.Operations.Single(operation => operation.ToString() == "GET /projects/{projectId}/commits/{commitId}/elements");
            var explains = KnownServerDivergences.QueryResultsProjected.Explains;
            var projected = Missing("/0", "\"@type\",\"aliasIds\",\"owner\"");

            using (Assert.EnterMultipleScope())
            {
                Assert.That(explains(new EndpointCall(queryResults, "GET /projects/p/queries/q/results", HttpStatusCode.OK), projected), Is.True);
                Assert.That(explains(new EndpointCall(postQueryResults, "POST /projects/p/query-results", HttpStatusCode.OK), projected), Is.True);
                Assert.That(explains(new EndpointCall(queryResults, "GET /projects/p/queries/q/results", HttpStatusCode.OK), Missing("/0", "\"aliasIds\",\"owner\"")), Is.False, "complete results that miss properties are another matter");
                Assert.That(explains(new EndpointCall(getElements, "GET /projects/p/commits/c/elements", HttpStatusCode.OK), projected), Is.False);
                Assert.That(explains(new EndpointCall(queryResults, "GET /projects/p/queries/q/results", HttpStatusCode.OK), projected with { Kind = ContractViolationKind.InvalidJson }), Is.False, "only a schema mismatch is explained");
            }

            var selected = postQueryResults.CheckResponse(HttpStatusCode.OK, "application/json", "[{\"@id\":\"3fa85f64-5717-4562-b3fc-2c963f66afa6\",\"declaredName\":\"x\"}]");
            var call = new EndpointCall(postQueryResults, "POST /projects/p/query-results", HttpStatusCode.OK);

            Assert.That(selected.Where(violation => explains(call, violation)), Is.Not.Empty, "the violation the conformance check produces for a projected result is explained");
        }

        [Test]
        public void VerifyRecordsWithoutRequiredProperties()
        {
            var explains = KnownServerDivergences.RecordsWithoutRequiredProperties.Explains;
            var call = Conforming with { StatusCode = HttpStatusCode.OK };

            using (Assert.EnterMultipleScope())
            {
                Assert.That(explains(call, Missing("/", "\"alias\",\"deleted\",\"description\"")), Is.True);
                Assert.That(explains(call, Missing("/0/identity", "\"alias\",\"name\"")), Is.True);
                Assert.That(explains(call, new ContractViolation(ContractViolationKind.SchemaMismatch, "/0/previousCommit type: Value is \"null\" but should be \"array\"")), Is.True);
                Assert.That(explains(call, Missing("/", "\"alias\",\"created\"")), Is.False);
                Assert.That(explains(call, new ContractViolation(ContractViolationKind.SchemaMismatch, "/previousCommit type: Value is \"string\" but should be \"array\"")), Is.False);
            }
        }

        [Test]
        public async Task VerifyReport()
        {
            var divergence = new ServerDivergence("the summary", "the reference");

            var results = await IsolatedAssertions.RunAsync(() =>
            {
                KnownServerDivergences.Report(divergence, "the detail");
                return Task.CompletedTask;
            });

            using (Assert.EnterMultipleScope())
            {
                Assert.That(results, Has.Count.EqualTo(1));
                Assert.That(results[0].Status, Is.EqualTo(AssertionStatus.Warning));
                Assert.That(results[0].Message, Does.Contain("the summary").And.Contain("the reference").And.Contain("the detail"));
            }
        }

        private static Func<Task> Answer(HttpStatusCode statusCode, bool withError = true) =>
            () => throw new PsmApiException(statusCode, withError ? new Error { Description = "unknown" } : null);

        private static async Task<IReadOnlyList<AssertionStatus>> Outcome(Func<Task> call, ServerDivergence tolerated, EndpointCall lastCall)
        {
            var results = await IsolatedAssertions.RunAsync(() => KnownServerDivergences.ExpectNotFoundAsync(call, () => lastCall, tolerated));

            return results.Select(result => result.Status).ToList();
        }

        private static ContractViolation Missing(string location, string names) => new(ContractViolationKind.SchemaMismatch, $"{location} required: Required properties [{names}] are not present");

        private static Task Succeed() => Task.CompletedTask;
    }
}
