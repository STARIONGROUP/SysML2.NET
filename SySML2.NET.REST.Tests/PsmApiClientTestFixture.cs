// ------------------------------------------------------------------------------------------------
// <copyright file="PsmApiClientTestFixture.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SySML2.NET.REST.Tests
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net;
    using System.Net.Http;
    using System.Text.Json;
    using System.Threading.Tasks;

    using NUnit.Framework;

    using RichardSzalay.MockHttp;

    using SysML2.NET.Core.DTO.Root.Namespaces;
    using SysML2.NET.PSM;
    using SysML2.NET.PSM.DTO;
    using SysML2.NET.PSM.Enumerations;
    using SysML2.NET.REST.Exceptions;
    using SysML2.NET.Serializer.Json;

    [TestFixture]
    public class PsmApiClientTestFixture
    {
        private const string JsonMediaType = "application/json";

        private const string ErrorPayload = "{\"@type\":\"Error\",\"description\":\"the error description\"}";

        private static readonly Uri BaseAddress = new("http://localhost/api/");

        private static readonly Guid ProjectId = Guid.Parse("000e9890-6935-43e6-a5d7-5d7cac601f4c");

        private static readonly Guid BranchId = Guid.Parse("c294a463-6c9c-47a8-b592-01252c5ab2a7");

        private static readonly Guid CommitId = Guid.Parse("6d7ad9fd-6520-4ff2-885b-8c5c129e6c27");

        private static readonly Guid OtherCommitId = Guid.Parse("2f1a0e1c-6b2a-4d5e-9c3f-8a7b6d5e4c3b");

        private static readonly Guid RootNamespaceId = Guid.Parse("864d5403-c87b-4679-9d3a-b09c7fde1dd9");

        private MockHttpMessageHandler mockHttp;

        private PsmApiClient client;

        [SetUp]
        public void SetUp()
        {
            this.mockHttp = new MockHttpMessageHandler();

            var httpClient = this.mockHttp.ToHttpClient();
            httpClient.BaseAddress = BaseAddress;

            this.client = new PsmApiClient(httpClient, new DeSerializer(), new Serializer());
        }

        [TearDown]
        public void TearDown()
        {
            this.mockHttp.Dispose();
        }

        private static string ReadPayload(string fileName)
        {
            return File.ReadAllText(Path.Combine(TestContext.CurrentContext.TestDirectory, "Data", fileName));
        }

        private static KeyValuePair<string, string>[] CreateLinkHeader(string link)
        {
            return [new KeyValuePair<string, string>("Link", link)];
        }

        [Test]
        public void VerifyConstructor()
        {
            using var httpClient = new HttpClient();

            using (Assert.EnterMultipleScope())
            {
                Assert.That(() => new PsmApiClient(null, new DeSerializer(), new Serializer()), Throws.TypeOf<ArgumentNullException>());
                Assert.That(() => new PsmApiClient(httpClient, null, new Serializer()), Throws.TypeOf<ArgumentNullException>());
                Assert.That(() => new PsmApiClient(httpClient, new DeSerializer(), null), Throws.TypeOf<ArgumentNullException>());
                Assert.That(() => new PsmApiClient(httpClient, new DeSerializer(), new Serializer()), Throws.Nothing);
            }
        }

        [Test]
        public async Task VerifyGetBranchesByProjectAsync()
        {
            var branchesUri = $"{BaseAddress}projects/{ProjectId}/branches";
            var previousPage = Page.FromCursor(1618861496632, OtherCommitId);
            var nextPage = Page.FromCursor(1618861496632, Guid.Parse("20109f40-4825-416a-86f6-7508ac42a021"));

            this.mockHttp.Expect(HttpMethod.Get, branchesUri)
                .WithExactQueryString($"page[size]=2&page[after]={Uri.EscapeDataString(previousPage.ToString())}")
                .Respond(HttpStatusCode.OK,
                    CreateLinkHeader($"<{branchesUri}?page[after]=MTYxODg2MTQ5NjYzMnwyMDEwOWY0MC00ODI1LTQxNmEtODZmNi03NTA4YWM0MmEwMjE&page[size]=2>; rel=\"next\", <{branchesUri}?page[before]={Uri.EscapeDataString(previousPage.ToString())}>; rel=\"prev\""),
                    JsonMediaType, ReadPayload("branches.json"));

            var branches = await this.client.GetBranchesByProjectAsync(ProjectId, new QueryParameters { PageSize = 2, PageAfter = previousPage });

            using (Assert.EnterMultipleScope())
            {
                Assert.That(branches.Records, Has.Count.EqualTo(1));
                Assert.That(branches.Records[0].Id, Is.EqualTo(BranchId));
                Assert.That(branches.Records[0].Name, Is.EqualTo("main"));
                Assert.That(branches.Next, Is.EqualTo(nextPage));
                Assert.That(branches.Previous, Is.EqualTo(previousPage));
            }

            this.mockHttp.Expect(HttpMethod.Get, branchesUri).WithExactQueryString(string.Empty).Respond(JsonMediaType, "[]");

            branches = await this.client.GetBranchesByProjectAsync(ProjectId);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(branches.Records, Has.Count.EqualTo(0));
                Assert.That(branches.Next, Is.Null);
                Assert.That(branches.Previous, Is.Null);
            }

            this.mockHttp.Expect(HttpMethod.Get, branchesUri)
                .Respond(HttpStatusCode.OK, CreateLinkHeader($"<{branchesUri}?page[after]=not-a-page>; rel=\"next\""), JsonMediaType, "[]");

            branches = await this.client.GetBranchesByProjectAsync(ProjectId);

            Assert.That(branches.Next, Is.EqualTo(new Page("not-a-page")));

            this.mockHttp.Expect(HttpMethod.Get, branchesUri)
                .WithExactQueryString("page[after]=not-a-page")
                .Respond(HttpStatusCode.OK, CreateLinkHeader($"<{branchesUri}?page[after]=>; rel=\"next\""), JsonMediaType, "[]");

            branches = await this.client.GetBranchesByProjectAsync(ProjectId, new QueryParameters { PageAfter = branches.Next });

            Assert.That(branches.Next, Is.Null);

            this.mockHttp.Expect(HttpMethod.Get, branchesUri).Respond(HttpStatusCode.NotFound, JsonMediaType, ErrorPayload);

            await Assert.ThatAsync(() => this.client.GetBranchesByProjectAsync(ProjectId),
                Throws.TypeOf<PsmApiException>()
                    .With.Property(nameof(PsmApiException.StatusCode)).EqualTo(HttpStatusCode.NotFound)
                    .And.Property(nameof(PsmApiException.Error)).Property(nameof(Error.Description)).EqualTo("the error description"));

            this.mockHttp.Expect(HttpMethod.Get, branchesUri).Respond(HttpStatusCode.NotFound, JsonMediaType, "{}");

            await Assert.ThatAsync(() => this.client.GetBranchesByProjectAsync(ProjectId),
                Throws.TypeOf<PsmApiException>().With.Property(nameof(PsmApiException.Error)).Null);

            this.mockHttp.Expect(HttpMethod.Get, branchesUri).Respond(HttpStatusCode.NotFound, JsonMediaType, "{\"@type\":\"Unknown\"}");

            await Assert.ThatAsync(() => this.client.GetBranchesByProjectAsync(ProjectId),
                Throws.TypeOf<PsmApiException>().With.Property(nameof(PsmApiException.Error)).Null);

            this.mockHttp.Expect(HttpMethod.Get, branchesUri).Respond(HttpStatusCode.BadGateway, "text/plain", "the gateway is down");

            await Assert.ThatAsync(() => this.client.GetBranchesByProjectAsync(ProjectId),
                Throws.TypeOf<PsmApiException>()
                    .With.Property(nameof(PsmApiException.StatusCode)).EqualTo(HttpStatusCode.BadGateway)
                    .And.Property(nameof(PsmApiException.Error)).Null);

            this.mockHttp.VerifyNoOutstandingExpectation();
        }

        [Test]
        public async Task VerifyDeleteBranchByProjectAndIdAsync()
        {
            var branchUri = $"{BaseAddress}projects/{ProjectId}/branches/{BranchId}";

            this.mockHttp.Expect(HttpMethod.Delete, branchUri).Respond(JsonMediaType, ReadPayload("branch.json"));

            var branch = await this.client.DeleteBranchByProjectAndIdAsync(ProjectId, BranchId);

            Assert.That(branch.Id, Is.EqualTo(BranchId));

            this.mockHttp.Expect(HttpMethod.Delete, branchUri).Respond(HttpStatusCode.NoContent);

            Assert.That(await this.client.DeleteBranchByProjectAndIdAsync(ProjectId, BranchId), Is.Null);

            this.mockHttp.Expect(HttpMethod.Delete, branchUri).Respond(HttpStatusCode.NotFound, JsonMediaType, ErrorPayload);

            await Assert.ThatAsync(() => this.client.DeleteBranchByProjectAndIdAsync(ProjectId, BranchId),
                Throws.TypeOf<PsmApiException>().With.Property(nameof(PsmApiException.StatusCode)).EqualTo(HttpStatusCode.NotFound));

            this.mockHttp.VerifyNoOutstandingExpectation();
        }

        [Test]
        public async Task VerifyPostCommitByProjectAsync()
        {
            var commitsUri = $"{BaseAddress}projects/{ProjectId}/commits";
            var commitRequest = new CommitRequest { Description = "the commit description" };

            this.mockHttp.Expect(HttpMethod.Post, commitsUri)
                .WithExactQueryString($"branchId={BranchId}")
                .With(request => request.Content.Headers.ContentType.MediaType == JsonMediaType
                                 && request.Content.ReadAsStringAsync().GetAwaiter().GetResult().Contains("the commit description"))
                .Respond(HttpStatusCode.Created, JsonMediaType, ReadPayload("commit.json"));

            var commit = await this.client.PostCommitByProjectAsync(ProjectId, commitRequest, BranchId);

            Assert.That(commit.Id, Is.EqualTo(CommitId));

            this.mockHttp.Expect(HttpMethod.Post, commitsUri)
                .With(request => string.IsNullOrWhiteSpace(request.RequestUri.Query))
                .Respond(HttpStatusCode.BadRequest, JsonMediaType, ErrorPayload);

            await Assert.ThatAsync(() => this.client.PostCommitByProjectAsync(ProjectId, commitRequest),
                Throws.TypeOf<PsmApiException>().With.Property(nameof(PsmApiException.StatusCode)).EqualTo(HttpStatusCode.BadRequest));

            await Assert.ThatAsync(() => this.client.PostCommitByProjectAsync(ProjectId, null), Throws.ArgumentNullException);

            this.mockHttp.VerifyNoOutstandingExpectation();
        }

        [Test]
        public async Task VerifyGetRootsByProjectCommitAsync()
        {
            var rootsUri = $"{BaseAddress}projects/{ProjectId}/commits/{CommitId}/roots";

            this.mockHttp.Expect(HttpMethod.Get, rootsUri)
                .WithExactQueryString("excludeUsed=true")
                .Respond(JsonMediaType, ReadPayload("roots.json"));

            var roots = await this.client.GetRootsByProjectCommitAsync(ProjectId, CommitId, true);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(roots.Records, Has.Count.EqualTo(2));
                Assert.That(roots.Records[0].Id, Is.EqualTo(RootNamespaceId));
            }

            this.mockHttp.Expect(HttpMethod.Get, rootsUri).Respond(HttpStatusCode.NotFound, JsonMediaType, ErrorPayload);

            await Assert.ThatAsync(() => this.client.GetRootsByProjectCommitAsync(ProjectId, CommitId),
                Throws.TypeOf<PsmApiException>().With.Property(nameof(PsmApiException.StatusCode)).EqualTo(HttpStatusCode.NotFound));

            this.mockHttp.VerifyNoOutstandingExpectation();
        }

        [Test]
        public async Task VerifyGetRelationshipsByProjectCommitRelatedElementAsync()
        {
            var relationshipsUri = $"{BaseAddress}projects/{ProjectId}/commits/{CommitId}/elements/{RootNamespaceId}/relationships";

            this.mockHttp.Expect(HttpMethod.Get, relationshipsUri).WithExactQueryString("direction=in").Respond(JsonMediaType, "[]");

            var relationships = await this.client.GetRelationshipsByProjectCommitRelatedElementAsync(ProjectId, CommitId, RootNamespaceId, Direction.@in);

            Assert.That(relationships.Records, Has.Count.EqualTo(0));

            this.mockHttp.Expect(HttpMethod.Get, relationshipsUri).WithExactQueryString("direction=both&excludeUsed=true").Respond(JsonMediaType, "[]");

            relationships = await this.client.GetRelationshipsByProjectCommitRelatedElementAsync(ProjectId, CommitId, RootNamespaceId, Direction.both, true);

            Assert.That(relationships.Records, Has.Count.EqualTo(0));

            this.mockHttp.Expect(HttpMethod.Get, relationshipsUri).WithExactQueryString(string.Empty).Respond(JsonMediaType, "[]");

            relationships = await this.client.GetRelationshipsByProjectCommitRelatedElementAsync(ProjectId, CommitId, RootNamespaceId);

            Assert.That(relationships.Next, Is.Null);

            this.mockHttp.VerifyNoOutstandingExpectation();
        }

        [Test]
        public async Task VerifyGetElementByProjectCommitIdAsync()
        {
            var elementUri = $"{BaseAddress}projects/{ProjectId}/commits/{CommitId}/elements/{RootNamespaceId}";
            var roots = ReadPayload("roots.json");

            using (var rootsDocument = JsonDocument.Parse(roots))
            {
                this.mockHttp.Expect(HttpMethod.Get, elementUri)
                    .WithExactQueryString("excludeUsed=false")
                    .Respond(JsonMediaType, rootsDocument.RootElement[0].GetRawText());
            }

            var element = await this.client.GetElementByProjectCommitIdAsync(ProjectId, CommitId, RootNamespaceId, false);

            Assert.That(element, Is.TypeOf<Namespace>().With.Property(nameof(Namespace.Id)).EqualTo(RootNamespaceId));

            this.mockHttp.Expect(HttpMethod.Get, elementUri).Respond(JsonMediaType, roots);

            await Assert.ThatAsync(() => this.client.GetElementByProjectCommitIdAsync(ProjectId, CommitId, RootNamespaceId), Throws.TypeOf<InvalidOperationException>());

            this.mockHttp.Expect(HttpMethod.Get, elementUri).Respond(JsonMediaType, "[]");

            await Assert.ThatAsync(() => this.client.GetElementByProjectCommitIdAsync(ProjectId, CommitId, RootNamespaceId), Throws.TypeOf<InvalidOperationException>());

            this.mockHttp.Expect(HttpMethod.Get, elementUri).Respond(HttpStatusCode.NotFound, JsonMediaType, ErrorPayload);

            await Assert.ThatAsync(() => this.client.GetElementByProjectCommitIdAsync(ProjectId, CommitId, RootNamespaceId),
                Throws.TypeOf<PsmApiException>().With.Property(nameof(PsmApiException.StatusCode)).EqualTo(HttpStatusCode.NotFound));

            this.mockHttp.VerifyNoOutstandingExpectation();
        }

        [Test]
        public async Task VerifyGetQueryResultsByProjectIdQueryPostAsync()
        {
            var queryResultsUri = $"{BaseAddress}projects/{ProjectId}/query-results";
            var queryRequest = new QueryRequest { Name = "the query name" };

            this.mockHttp.Expect(HttpMethod.Post, queryResultsUri)
                .WithExactQueryString($"page[size]=2&commitId={CommitId}")
                .With(request => request.Content.ReadAsStringAsync().GetAwaiter().GetResult().Contains("the query name"))
                .Respond(JsonMediaType, ReadPayload("roots.json"));

            var results = await this.client.GetQueryResultsByProjectIdQueryPostAsync(ProjectId, queryRequest, CommitId, new QueryParameters { PageSize = 2 });

            using (Assert.EnterMultipleScope())
            {
                Assert.That(results.Records, Has.Count.EqualTo(2));
                Assert.That(results.Records[0], Is.TypeOf<Namespace>());
                Assert.That(results.Next, Is.Null);
            }

            this.mockHttp.Expect(HttpMethod.Post, queryResultsUri)
                .With(request => string.IsNullOrWhiteSpace(request.RequestUri.Query))
                .Respond(HttpStatusCode.BadRequest, JsonMediaType, ErrorPayload);

            await Assert.ThatAsync(() => this.client.GetQueryResultsByProjectIdQueryPostAsync(ProjectId, queryRequest),
                Throws.TypeOf<PsmApiException>().With.Property(nameof(PsmApiException.StatusCode)).EqualTo(HttpStatusCode.BadRequest));

            this.mockHttp.VerifyNoOutstandingExpectation();
        }

        [Test]
        public async Task VerifyPutProjectByIdAsync()
        {
            var projectUri = $"{BaseAddress}projects/{ProjectId}";
            var projectRequest = new ProjectRequest { Name = "the project name" };

            this.mockHttp.Expect(HttpMethod.Put, projectUri)
                .With(request => request.Content.ReadAsStringAsync().GetAwaiter().GetResult().Contains("the project name"))
                .Respond(JsonMediaType, ReadPayload("project.json"));

            var project = await this.client.PutProjectByIdAsync(ProjectId, projectRequest);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(project.Id, Is.EqualTo(ProjectId));
                Assert.That(project.DefaultBranch, Is.EqualTo(BranchId));
            }

            this.mockHttp.Expect(HttpMethod.Put, projectUri)
                .With(request => request.Content is null)
                .Respond(JsonMediaType, ReadPayload("project.json"));

            project = await this.client.PutProjectByIdAsync(ProjectId);

            Assert.That(project.Id, Is.EqualTo(ProjectId));

            this.mockHttp.Expect(HttpMethod.Put, projectUri).Respond(HttpStatusCode.BadRequest, JsonMediaType, ErrorPayload);

            await Assert.ThatAsync(() => this.client.PutProjectByIdAsync(ProjectId, projectRequest),
                Throws.TypeOf<PsmApiException>()
                    .With.Property(nameof(PsmApiException.StatusCode)).EqualTo(HttpStatusCode.BadRequest)
                    .And.Message.EqualTo("the error description"));

            this.mockHttp.VerifyNoOutstandingExpectation();
        }

        [Test]
        public async Task VerifyMergeAsync()
        {
            var mergeUri = $"{BaseAddress}projects/{ProjectId}/branches/{BranchId}/merge";

            this.mockHttp.Expect(HttpMethod.Post, mergeUri)
                .WithExactQueryString($"sourceCommitId={CommitId},{OtherCommitId}&description=the%20merge%20description")
                .With(request => request.Content.ReadAsStringAsync().GetAwaiter().GetResult().Contains(RootNamespaceId.ToString()))
                .Respond(HttpStatusCode.Created, JsonMediaType, ReadPayload("commit.json"));

            var mergeCommit = await this.client.MergeAsync(ProjectId, [CommitId, OtherCommitId], BranchId, [new Namespace { Id = RootNamespaceId }], "the merge description");

            Assert.That(mergeCommit.Id, Is.EqualTo(CommitId));

            await Assert.ThatAsync(() => this.client.MergeAsync(ProjectId, null, BranchId), Throws.TypeOf<ArgumentNullException>());

            await Assert.ThatAsync(() => this.client.MergeAsync(ProjectId, [], BranchId), Throws.TypeOf<ArgumentException>());

            this.mockHttp.Expect(HttpMethod.Post, mergeUri)
                .WithExactQueryString($"sourceCommitId={CommitId}")
                .With(request => request.Content is null)
                .Respond(HttpStatusCode.Created, JsonMediaType, ReadPayload("commit.json"));

            mergeCommit = await this.client.MergeAsync(ProjectId, [CommitId], BranchId);

            Assert.That(mergeCommit.Id, Is.EqualTo(CommitId));

            this.mockHttp.Expect(HttpMethod.Post, mergeUri).Respond(HttpStatusCode.Conflict, "text/plain", "the branch has moved");

            await Assert.ThatAsync(() => this.client.MergeAsync(ProjectId, [CommitId], BranchId, null),
                Throws.TypeOf<PsmApiException>().With.Property(nameof(PsmApiException.StatusCode)).EqualTo(HttpStatusCode.Conflict));

            this.mockHttp.Expect(HttpMethod.Post, mergeUri)
                .Respond(HttpStatusCode.Conflict, JsonMediaType, $"[{{\"@id\":\"{OtherCommitId}\",\"@type\":\"DataIdentity\"}}]");

            await Assert.ThatAsync(() => this.client.MergeAsync(ProjectId, [CommitId], BranchId, null),
                Throws.TypeOf<MergeConflictException>()
                    .With.Property(nameof(PsmApiException.StatusCode)).EqualTo(HttpStatusCode.Conflict)
                    .And.Property(nameof(MergeConflictException.Conflicts)).With.Count.EqualTo(1));

            this.mockHttp.Expect(HttpMethod.Post, mergeUri).Respond(HttpStatusCode.Conflict, JsonMediaType, ErrorPayload);

            await Assert.ThatAsync(() => this.client.MergeAsync(ProjectId, [CommitId], BranchId, null),
                Throws.TypeOf<PsmApiException>()
                    .With.Property(nameof(PsmApiException.StatusCode)).EqualTo(HttpStatusCode.Conflict)
                    .And.Property(nameof(PsmApiException.Error)).Property(nameof(Error.Description)).EqualTo("the error description"));

            this.mockHttp.Expect(HttpMethod.Post, mergeUri).Respond(HttpStatusCode.Conflict, JsonMediaType, "{ not json");

            await Assert.ThatAsync(() => this.client.MergeAsync(ProjectId, [CommitId], BranchId, null),
                Throws.TypeOf<PsmApiException>().With.Property(nameof(PsmApiException.StatusCode)).EqualTo(HttpStatusCode.Conflict));

            this.mockHttp.Expect(HttpMethod.Post, mergeUri).Respond(HttpStatusCode.NotFound, JsonMediaType, ErrorPayload);

            await Assert.ThatAsync(() => this.client.MergeAsync(ProjectId, [CommitId], BranchId, null),
                Throws.TypeOf<PsmApiException>().With.Property(nameof(PsmApiException.StatusCode)).EqualTo(HttpStatusCode.NotFound));

            this.mockHttp.VerifyNoOutstandingExpectation();
        }

        [Test]
        public async Task VerifyGetDatatypesAsync()
        {
            var datatypesUri = $"{BaseAddress}meta/datatypes";
            const string datatypesPayload = "{\"$schema\":\"https://json-schema.org/draft/2020-12/schema\",\"$defs\":{\"Boolean\":{\"type\":\"boolean\"}}}";
            var nextPage = Page.FromCursor(1618861496632, CommitId);

            this.mockHttp.Expect(HttpMethod.Get, datatypesUri)
                .WithExactQueryString("page[size]=1")
                .Respond(HttpStatusCode.OK, CreateLinkHeader($"<{datatypesUri}?page[after]={nextPage}&page[size]=1>; rel=\"next\""), JsonMediaType, datatypesPayload);

            var datatypes = await this.client.GetDatatypesAsync(new QueryParameters { PageSize = 1 });

            using (datatypes.Records[0])
            {
                using (Assert.EnterMultipleScope())
                {
                    Assert.That(datatypes.Records, Has.Count.EqualTo(1));
                    Assert.That(datatypes.Records[0].RootElement.GetProperty("$defs").GetProperty("Boolean").GetProperty("type").GetString(), Is.EqualTo("boolean"));
                    Assert.That(datatypes.Next, Is.EqualTo(nextPage));
                    Assert.That(datatypes.Previous, Is.Null);
                }
            }

            this.mockHttp.Expect(HttpMethod.Get, datatypesUri).WithExactQueryString(string.Empty).Respond(JsonMediaType, datatypesPayload);

            datatypes = await this.client.GetDatatypesAsync();

            using (datatypes.Records[0])
            {
                Assert.That(datatypes.Next, Is.Null);
            }

            this.mockHttp.Expect(HttpMethod.Get, datatypesUri).Respond(HttpStatusCode.NotFound, JsonMediaType, ErrorPayload);

            await Assert.ThatAsync(() => this.client.GetDatatypesAsync(),
                Throws.TypeOf<PsmApiException>().With.Property(nameof(PsmApiException.StatusCode)).EqualTo(HttpStatusCode.NotFound));

            this.mockHttp.VerifyNoOutstandingExpectation();
        }

        [Test]
        public async Task VerifyGetDatatypeByIdAsync()
        {
            const string datatypeId = "https://www.omg.org/spec/SysML/the/datatype";
            var datatypeUri = $"{BaseAddress}meta/datatypes/{Uri.EscapeDataString(datatypeId)}";

            this.mockHttp.Expect(HttpMethod.Get, datatypeUri)
                .Respond(JsonMediaType, "{\"$schema\":\"https://json-schema.org/draft/2020-12/schema\",\"type\":\"boolean\"}");

            using (var datatype = await this.client.GetDatatypeByIdAsync(datatypeId))
            {
                Assert.That(datatype.RootElement.GetProperty("type").GetString(), Is.EqualTo("boolean"));
            }

            this.mockHttp.Expect(HttpMethod.Get, datatypeUri).Respond(HttpStatusCode.NotFound, JsonMediaType, ErrorPayload);

            await Assert.ThatAsync(() => this.client.GetDatatypeByIdAsync(datatypeId),
                Throws.TypeOf<PsmApiException>().With.Property(nameof(PsmApiException.StatusCode)).EqualTo(HttpStatusCode.NotFound));

            this.mockHttp.Expect(HttpMethod.Get, datatypeUri).Respond(JsonMediaType, "not json");

            await Assert.ThatAsync(() => this.client.GetDatatypeByIdAsync(datatypeId), Throws.InstanceOf<JsonException>());

            using (Assert.EnterMultipleScope())
            {
                await Assert.ThatAsync(() => this.client.GetDatatypeByIdAsync(null), Throws.ArgumentNullException);
                await Assert.ThatAsync(() => this.client.GetDatatypeByIdAsync(" "), Throws.ArgumentException);
            }

            this.mockHttp.VerifyNoOutstandingExpectation();
        }
    }
}
