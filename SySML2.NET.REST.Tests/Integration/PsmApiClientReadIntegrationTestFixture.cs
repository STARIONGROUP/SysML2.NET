// ------------------------------------------------------------------------------------------------
// <copyright file="PsmApiClientReadIntegrationTestFixture.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SySML2.NET.REST.Tests.Integration
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Text.Json;
    using System.Threading.Tasks;

    using NUnit.Framework;

    using SysML2.NET.Core.DTO.Root.Elements;
    using SysML2.NET.PSM;
    using SysML2.NET.PSM.DTO;
    using SysML2.NET.PSM.Enumerations;
    using SysML2.NET.REST.Exceptions;

    [TestFixture]
    [Category("Integration")]
    public class PsmApiClientReadIntegrationTestFixture
    {
        private const int PageSize = 2;

        private const int DiscoveryPageLimit = 40;

        private static readonly Guid UnknownId = Guid.Parse("5b0c3f1e-7d2a-4e9b-8f6c-1a2b3c4d5e6f");

        private LiveServerTestHost host;

        private IPsmApiClient client;

        private Guid projectId;

        private Guid branchId;

        private Guid commitId;

        private IElement root;

        private Query query;

        private Tag tag;

        [OneTimeSetUp]
        public async Task OneTimeSetUp()
        {
            this.host = new LiveServerTestHost();

            await this.host.OpenAsync();

            this.client = this.host.Client;

            foreach (var project in await this.DiscoverProjectsAsync())
            {
                if (this.root is null)
                {
                    await this.TrySelectProjectAsync(project);
                }

                if (this.query is null)
                {
                    var queries = await this.client.GetQueriesByProjectAsync(project.Id, new QueryParameters { PageSize = 1 });
                    this.query = queries.Records.Count > 0 ? queries.Records[0] : null;
                }

                if (this.tag is null)
                {
                    var tags = await this.client.GetTagsByProjectAsync(project.Id, new QueryParameters { PageSize = 1 });
                    this.tag = tags.Records.Count > 0 ? tags.Records[0] : null;
                }
            }

            if (this.root is null)
            {
                Assert.Ignore("No readable project has a default branch whose head commit has root elements");
            }

            await TestContext.Out.WriteLineAsync($"Reading project {this.projectId}, branch {this.branchId}, commit {this.commitId}, query {this.query?.Id}, tag {this.tag?.Id}");
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            this.host.Dispose();
        }

        [Test]
        public async Task VerifyGetProjectsAsync()
        {
            var firstPage = await this.client.GetProjectsAsync(new QueryParameters { PageSize = PageSize });

            using (Assert.EnterMultipleScope())
            {
                Assert.That(firstPage.Records, Has.Count.EqualTo(PageSize));
                Assert.That(firstPage.Next, Is.Not.Null);
            }

            var secondPage = await this.client.GetProjectsAsync(new QueryParameters { PageSize = PageSize, PageAfter = firstPage.Next });

            using (Assert.EnterMultipleScope())
            {
                Assert.That(secondPage.Records, Is.Not.Empty);
                Assert.That(secondPage.Records.Select(project => project.Id), Has.None.AnyOf(firstPage.Records.Select(project => project.Id).ToArray()));
                Assert.That(secondPage.Previous, Is.Not.Null);
            }

            var previousPage = await this.client.GetProjectsAsync(new QueryParameters { PageSize = PageSize, PageBefore = secondPage.Previous });

            Assert.That(previousPage.Records.Select(project => project.Id), Is.EqualTo(firstPage.Records.Select(project => project.Id)));
        }

        [Test]
        public async Task VerifyGetProjectByIdAsync()
        {
            var project = await this.client.GetProjectByIdAsync(this.projectId);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(project.Id, Is.EqualTo(this.projectId));
                Assert.That(project.DefaultBranch, Is.EqualTo(this.branchId));
            }

            await Assert.ThatAsync(() => this.client.GetProjectByIdAsync(UnknownId),
                Throws.TypeOf<PsmApiException>().With.Property(nameof(PsmApiException.StatusCode)).EqualTo(HttpStatusCode.NotFound));
        }

        [Test]
        public async Task VerifyGetBranchesByProjectAsync()
        {
            var branches = await this.client.GetBranchesByProjectAsync(this.projectId);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(branches.Records.Select(branch => branch.Id), Does.Contain(this.branchId));
                Assert.That(branches.Records.Select(branch => branch.OwningProject), Is.All.EqualTo(this.projectId));
            }

            await AssertEmptyOrNotFoundAsync(() => this.client.GetBranchesByProjectAsync(UnknownId));
        }

        [Test]
        public async Task VerifyGetBranchesByProjectAndIdAsync()
        {
            var branch = await this.client.GetBranchesByProjectAndIdAsync(this.projectId, this.branchId);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(branch.Id, Is.EqualTo(this.branchId));
                Assert.That(branch.Head, Is.EqualTo(this.commitId));
                Assert.That(branch.OwningProject, Is.EqualTo(this.projectId));
            }

            await Assert.ThatAsync(() => this.client.GetBranchesByProjectAndIdAsync(this.projectId, UnknownId),
                Throws.TypeOf<PsmApiException>().With.Property(nameof(PsmApiException.StatusCode)).EqualTo(HttpStatusCode.NotFound));
        }

        [Test]
        public async Task VerifyGetTagsByProjectAsync()
        {
            var tags = await this.client.GetTagsByProjectAsync(this.projectId);

            Assert.That(tags.Records.Select(tag => tag.OwningProject), Is.All.EqualTo(this.projectId));

            await AssertEmptyOrNotFoundAsync(() => this.client.GetTagsByProjectAsync(UnknownId));
        }

        [Test]
        public async Task VerifyGetCommitsByProjectAsync()
        {
            var commits = await this.client.GetCommitsByProjectAsync(this.projectId, new QueryParameters { PageSize = PageSize });

            using (Assert.EnterMultipleScope())
            {
                Assert.That(commits.Records, Has.Count.InRange(1, PageSize));
                Assert.That(commits.Records.Select(commit => commit.OwningProject), Is.All.EqualTo(this.projectId));
            }

            await AssertEmptyOrNotFoundAsync(() => this.client.GetCommitsByProjectAsync(UnknownId));
        }

        [Test]
        public async Task VerifyGetCommitByProjectAndIdAsync()
        {
            var commit = await this.client.GetCommitByProjectAndIdAsync(this.projectId, this.commitId);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(commit.Id, Is.EqualTo(this.commitId));
                Assert.That(commit.OwningProject, Is.EqualTo(this.projectId));
            }

            await Assert.ThatAsync(() => this.client.GetCommitByProjectAndIdAsync(this.projectId, UnknownId),
                Throws.TypeOf<PsmApiException>().With.Property(nameof(PsmApiException.StatusCode)).EqualTo(HttpStatusCode.NotFound));
        }

        [Test]
        public async Task VerifyGetChangesByProjectCommitAsync()
        {
            var changes = await this.client.GetChangesByProjectCommitAsync(this.projectId, this.commitId, new QueryParameters { PageSize = PageSize });

            using (Assert.EnterMultipleScope())
            {
                Assert.That(changes.Records, Has.Count.InRange(0, PageSize));
                Assert.That(changes.Records.Select(change => change.Identity), Has.None.Null);
            }

            await AssertEmptyOrNotFoundAsync(() => this.client.GetChangesByProjectCommitAsync(this.projectId, UnknownId));
        }

        [Test]
        public async Task VerifyGetChangeByProjectCommitIdAsync()
        {
            var changes = await this.client.GetChangesByProjectCommitAsync(this.projectId, this.commitId, new QueryParameters { PageSize = 1 });

            if (changes.Records.Count > 0)
            {
                var change = await this.client.GetChangeByProjectCommitIdAsync(this.projectId, this.commitId, changes.Records[0].Id);

                Assert.That(change.Id, Is.EqualTo(changes.Records[0].Id));
            }

            await Assert.ThatAsync(() => this.client.GetChangeByProjectCommitIdAsync(this.projectId, this.commitId, UnknownId),
                Throws.TypeOf<PsmApiException>().With.Property(nameof(PsmApiException.StatusCode)).EqualTo(HttpStatusCode.NotFound));
        }

        [Test]
        public async Task VerifyGetElementsByProjectCommitAsync()
        {
            var firstPage = await this.client.GetElementsByProjectCommitAsync(this.projectId, this.commitId, queryParameters: new QueryParameters { PageSize = PageSize });

            Assert.That(firstPage.Records, Has.Count.InRange(1, PageSize));

            if (firstPage.Next is { } next)
            {
                var secondPage = await this.client.GetElementsByProjectCommitAsync(this.projectId, this.commitId, queryParameters: new QueryParameters { PageSize = PageSize, PageAfter = next });

                Assert.That(secondPage.Records.Select(element => element.Id), Has.None.AnyOf(firstPage.Records.Select(element => element.Id).ToArray()));
            }

            await AssertEmptyOrNotFoundAsync(() => this.client.GetElementsByProjectCommitAsync(this.projectId, UnknownId));
        }

        [Test]
        public async Task VerifyGetElementByProjectCommitIdAsync()
        {
            var element = await this.client.GetElementByProjectCommitIdAsync(this.projectId, this.commitId, this.root.Id);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(element.Id, Is.EqualTo(this.root.Id));
                Assert.That(element.GetType(), Is.EqualTo(this.root.GetType()));
            }

            await Assert.ThatAsync(() => this.client.GetElementByProjectCommitIdAsync(this.projectId, this.commitId, UnknownId),
                Throws.TypeOf<PsmApiException>().With.Property(nameof(PsmApiException.StatusCode)).EqualTo(HttpStatusCode.NotFound));
        }

        [Test]
        public async Task VerifyGetRootsByProjectCommitAsync()
        {
            var roots = await this.client.GetRootsByProjectCommitAsync(this.projectId, this.commitId);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(roots.Records.Select(element => element.Id), Does.Contain(this.root.Id));
                Assert.That(roots.Records.Select(element => element.OwningRelationship), Is.All.Null);
            }

            await AssertEmptyOrNotFoundAsync(() => this.client.GetRootsByProjectCommitAsync(this.projectId, UnknownId));
        }

        [Test]
        public async Task VerifyGetRelationshipsByProjectCommitRelatedElementAsync()
        {
            var page = new QueryParameters { PageSize = PageSize };
            var outgoing = await this.client.GetRelationshipsByProjectCommitRelatedElementAsync(this.projectId, this.commitId, this.root.Id, Direction.@out, queryParameters: page);
            var incoming = await this.client.GetRelationshipsByProjectCommitRelatedElementAsync(this.projectId, this.commitId, this.root.Id, Direction.@in, queryParameters: page);
            var both = await this.client.GetRelationshipsByProjectCommitRelatedElementAsync(this.projectId, this.commitId, this.root.Id, Direction.both, queryParameters: page);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(outgoing.Records.Select(relationship => relationship.Source), Has.All.Contains(this.root.Id));
                Assert.That(incoming.Records.Select(relationship => relationship.Target), Has.All.Contains(this.root.Id));
                Assert.That(both.Records, Has.All.Matches<IRelationship>(relationship => relationship.Source.Contains(this.root.Id) || relationship.Target.Contains(this.root.Id)));
            }

            await AssertEmptyOrNotFoundAsync(() => this.client.GetRelationshipsByProjectCommitRelatedElementAsync(this.projectId, this.commitId, UnknownId));
        }

        [Test]
        public async Task VerifyGetProjectUsageByProjectCommitElementAsync()
        {
            await Assert.ThatAsync(() => this.client.GetProjectUsageByProjectCommitElementAsync(this.projectId, this.commitId, UnknownId),
                Throws.TypeOf<PsmApiException>().With.Property(nameof(PsmApiException.StatusCode)).EqualTo(HttpStatusCode.NotFound));
        }

        [Test]
        public async Task VerifyGetQueriesByProjectAsync()
        {
            var queries = await this.client.GetQueriesByProjectAsync(this.projectId);

            Assert.That(queries.Records.Select(query => query.OwningProject), Is.All.EqualTo(this.projectId));

            await AssertEmptyOrNotFoundAsync(() => this.client.GetQueriesByProjectAsync(UnknownId));
        }

        [Test]
        public async Task VerifyGetDatatypesAsync()
        {
            var datatypes = await this.client.GetDatatypesAsync(new QueryParameters { PageSize = PageSize });

            try
            {
                Assert.That(datatypes.Records, Has.Count.InRange(1, PageSize));
            }
            finally
            {
                foreach (var datatype in datatypes.Records)
                {
                    datatype.Dispose();
                }
            }
        }

        [Test]
        public async Task VerifyGetDatatypeByIdAsync()
        {
            const string datatypeId = "https://www.omg.org/spec/SysML/20250201/PartUsage";

            using (var datatype = await this.client.GetDatatypeByIdAsync(datatypeId))
            {
                Assert.That(datatype.RootElement.GetProperty("$id").GetString(), Is.EqualTo(datatypeId));
            }

            await Assert.ThatAsync(() => this.client.GetDatatypeByIdAsync($"{datatypeId}Unknown"),
                Throws.TypeOf<PsmApiException>().With.Property(nameof(PsmApiException.StatusCode)).EqualTo(HttpStatusCode.NotFound));
        }

        [Test]
        public async Task VerifyGetTagByProjectAndIdAsync()
        {
            await Assert.ThatAsync(() => this.client.GetTagByProjectAndIdAsync(this.projectId, UnknownId),
                Throws.TypeOf<PsmApiException>().With.Property(nameof(PsmApiException.StatusCode)).EqualTo(HttpStatusCode.NotFound));

            if (this.tag is null)
            {
                Assert.Inconclusive("No readable project holds a tag");
            }

            var readTag = await this.client.GetTagByProjectAndIdAsync(this.tag.OwningProject, this.tag.Id);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(readTag.Id, Is.EqualTo(this.tag.Id));
                Assert.That(readTag.TaggedCommit, Is.EqualTo(this.tag.TaggedCommit));
            }
        }

        [Test]
        public async Task VerifyGetQueryByProjectAndIdAsync()
        {
            await Assert.ThatAsync(() => this.client.GetQueryByProjectAndIdAsync(this.projectId, UnknownId),
                Throws.TypeOf<PsmApiException>().With.Property(nameof(PsmApiException.StatusCode)).EqualTo(HttpStatusCode.NotFound));

            if (this.query is null)
            {
                Assert.Inconclusive("No readable project holds a query");
            }

            var readQuery = await this.client.GetQueryByProjectAndIdAsync(this.query.OwningProject, this.query.Id);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(readQuery.Id, Is.EqualTo(this.query.Id));
                Assert.That(readQuery.Select, Is.EqualTo(this.query.Select));
                Assert.That(readQuery.Where?.GetType(), Is.EqualTo(this.query.Where?.GetType()));
            }
        }

        [Test]
        public async Task VerifyGetQueryResultsByProjectIdQueryIdAsync()
        {
            if (this.query is null)
            {
                Assert.Inconclusive("No readable project holds a query");
            }

            var queryProject = await this.client.GetProjectByIdAsync(this.query.OwningProject);
            var queryBranch = await this.client.GetBranchesByProjectAndIdAsync(queryProject.Id, queryProject.DefaultBranch);

            var results = await this.client.GetQueryResultsByProjectIdQueryIdAsync(queryProject.Id, this.query.Id, queryBranch.Head, new QueryParameters { PageSize = PageSize });

            Assert.That(results.Records, Has.Count.InRange(0, PageSize));

            await Assert.ThatAsync(() => this.client.GetQueryResultsByProjectIdQueryIdAsync(this.projectId, UnknownId, this.commitId),
                Throws.TypeOf<PsmApiException>().With.Property(nameof(PsmApiException.StatusCode)).EqualTo(HttpStatusCode.NotFound));
        }

        [Test]
        public async Task VerifyGetQueryResultsByProjectIdQueryAsync()
        {
            var request = CreateIdentifierQuery(this.root.Id);

            var results = await this.client.GetQueryResultsByProjectIdQueryAsync(this.projectId, request, this.commitId);

            Assert.That(results.Records.Select(result => result.Id), Is.EquivalentTo([this.root.Id]));

            var unknownResults = await this.client.GetQueryResultsByProjectIdQueryAsync(this.projectId, CreateIdentifierQuery(UnknownId), this.commitId);

            Assert.That(unknownResults.Records, Is.Empty);
        }

        [Test]
        public async Task VerifyDiffAsync()
        {
            var differences = await this.client.DiffAsync(this.projectId, this.commitId, this.commitId);

            Assert.That(differences.Records, Is.Empty);

            await Assert.ThatAsync(() => this.client.DiffAsync(this.projectId, this.commitId, UnknownId),
                Throws.TypeOf<PsmApiException>().With.Property(nameof(PsmApiException.StatusCode)).EqualTo(HttpStatusCode.NotFound));
        }

        private static QueryRequest CreateIdentifierQuery(Guid elementId)
        {
            return new QueryRequest
            {
                Select = ["@id", "@type"],
                Where = new PrimitiveConstraintRequest
                {
                    Property = "@id",
                    Operator = PrimitiveConstraintRequestOperator.equalto,
                    Inverse = false,
                    Value = [elementId.ToString()]
                }
            };
        }

        private static async Task AssertEmptyOrNotFoundAsync<T>(Func<Task<PagedResult<T>>> query)
        {
            try
            {
                var result = await query();

                Assert.That(result.Records, Is.Empty);
            }
            catch (PsmApiException exception)
            {
                Assert.That(exception.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
            }
        }

        private async Task<List<Project>> DiscoverProjectsAsync()
        {
            var projects = new List<Project>();
            var parameters = new QueryParameters { PageSize = 1 };

            for (var pageIndex = 0; pageIndex < DiscoveryPageLimit; pageIndex++)
            {
                PagedResult<Project> page;

                try
                {
                    page = await this.client.GetProjectsAsync(parameters);
                }
                catch (JsonException exception)
                {
                    await TestContext.Out.WriteLineAsync($"Project discovery stops at page {pageIndex + 1}, its record is not readable: {exception.Message}");
                    break;
                }

                projects.AddRange(page.Records);

                if (page.Next is not { } next)
                {
                    break;
                }

                parameters = new QueryParameters { PageSize = 1, PageAfter = next };
            }

            return projects;
        }

        private async Task TrySelectProjectAsync(Project project)
        {
            var branch = await this.client.GetBranchesByProjectAndIdAsync(project.Id, project.DefaultBranch);

            if (branch.Head is not { } head)
            {
                return;
            }

            var roots = await this.client.GetRootsByProjectCommitAsync(project.Id, head, queryParameters: new QueryParameters { PageSize = 1 });

            if (roots.Records.Count == 0)
            {
                return;
            }

            this.projectId = project.Id;
            this.branchId = branch.Id;
            this.commitId = head;
            this.root = roots.Records[0];
        }
    }
}
