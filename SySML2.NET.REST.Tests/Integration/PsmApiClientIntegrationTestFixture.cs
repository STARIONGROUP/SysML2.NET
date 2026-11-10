// ------------------------------------------------------------------------------------------------
// <copyright file="PsmApiClientIntegrationTestFixture.cs" company="Starion Group S.A.">
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
    using System.Threading.Tasks;

    using NUnit.Framework;
    using NUnit.Framework.Constraints;

    using SysML2.NET.Core.DTO.Kernel.Packages;
    using SysML2.NET.Core.DTO.Root.Namespaces;
    using SysML2.NET.Core.DTO.Systems.Parts;
    using SysML2.NET.PSM;
    using SysML2.NET.PSM.DTO;
    using SysML2.NET.PSM.Enumerations;
    using SysML2.NET.REST.Exceptions;

    [TestFixture]
    [Category("Integration")]
    public class PsmApiClientIntegrationTestFixture
    {
        private const int PageSize = 2;

        private const int DatatypePageSize = 100;

        private const string ProjectNamePrefix = "SysML2.NET-it-";

        private const string ProjectDescription = "Throw-away project of the SysML2.NET integration tests";

        private const string PartUsageDatatypeId = "https://www.omg.org/spec/SysML/20250201/PartUsage";

        private const HttpStatusCode LastClientError = (HttpStatusCode)499;

        private static readonly Guid UnknownId = Guid.Parse("5b0c3f1e-7d2a-4e9b-8f6c-1a2b3c4d5e6f");

        private static readonly string[] SelectedProperties = ["@id", "@type"];

        private readonly List<Guid> createdProjectIds = [];

        private LiveServerTestHost host;

        private IPsmApiClient client;

        private Guid projectId;

        private Guid defaultBranchId;

        private Guid seedCommitId;

        [OneTimeSetUp]
        public async Task OneTimeSetUp()
        {
            this.host = new LiveServerTestHost();

            await this.host.OpenAsync();

            this.client = this.host.Client;

            var project = await this.CreateProjectAsync();

            this.projectId = project.Id;
            this.defaultBranchId = project.DefaultBranch;
            this.seedCommitId = (await this.client.PostCommitByProjectAsync(this.projectId, DefaultSeedModel.CreateCommitRequest(), this.defaultBranchId)).Id;

            await TestContext.Out.WriteLineAsync($"Testing on project {this.projectId}, seed commit {this.seedCommitId}");
        }

        [OneTimeTearDown]
        public async Task OneTimeTearDown()
        {
            try
            {
                foreach (var createdProjectId in this.createdProjectIds)
                {
                    try
                    {
                        await this.client.DeleteProjectByIdAsync(createdProjectId);
                    }
                    catch (PsmApiException exception)
                    {
                        await TestContext.Out.WriteLineAsync($"Project {createdProjectId} is left on the server, its delete answered {(int)exception.StatusCode} {exception.StatusCode}");
                    }
                }
            }
            finally
            {
                this.host.Dispose();
            }
        }

        [Test]
        public async Task VerifyDeleteBranchByProjectAndIdAsync()
        {
            var branch = await this.client.PostBranchByProjectAsync(this.projectId, new BranchRequest { Name = "to-delete", Head = this.seedCommitId });

            await this.client.DeleteBranchByProjectAndIdAsync(this.projectId, branch.Id);

            var deletedBranch = await GetOrNullWhenNotFoundAsync(() => this.client.GetBranchesByProjectAndIdAsync(this.projectId, branch.Id));

            Assert.That(deletedBranch, Is.Null.Or.Property(nameof(Branch.Deleted)).Not.Null);

            await Assert.ThatAsync(() => this.client.DeleteBranchByProjectAndIdAsync(this.projectId, UnknownId), ThrowsNotFound());
        }

        [Test]
        public async Task VerifyDeleteProjectByIdAsync()
        {
            var project = await this.CreateProjectAsync();

            await this.client.DeleteProjectByIdAsync(project.Id);
            this.createdProjectIds.Remove(project.Id);

            await Assert.ThatAsync(() => this.client.GetProjectByIdAsync(project.Id), ThrowsNotFound());

            try
            {
                await this.client.DeleteProjectByIdAsync(UnknownId);
            }
            catch (PsmApiException exception)
            {
                // DELETE /projects/{projectId} declares no 404: an idempotent success and a client error both conform.
                Assert.That(exception.StatusCode, Is.InRange(HttpStatusCode.BadRequest, LastClientError));
            }
        }

        [Test]
        public async Task VerifyDeleteQueryByProjectAndIdAsync()
        {
            var query = await this.client.PostQueryByProjectAsync(this.projectId, CreatePartDefinitionQuery());

            await this.client.DeleteQueryByProjectAndIdAsync(this.projectId, query.Id);

            await Assert.ThatAsync(() => this.client.GetQueryByProjectAndIdAsync(this.projectId, query.Id), ThrowsNotFound());

            await Assert.ThatAsync(() => this.client.DeleteQueryByProjectAndIdAsync(this.projectId, UnknownId), ThrowsNotFound());
        }

        [Test]
        public async Task VerifyDeleteTagByProjectAndIdAsync()
        {
            var tag = await this.client.PostTagByProjectAsync(this.projectId, new TagRequest { Name = "to-delete", TaggedCommit = this.seedCommitId });

            await this.client.DeleteTagByProjectAndIdAsync(this.projectId, tag.Id);

            var deletedTag = await GetOrNullWhenNotFoundAsync(() => this.client.GetTagByProjectAndIdAsync(this.projectId, tag.Id));

            Assert.That(deletedTag, Is.Null.Or.Property(nameof(Tag.Deleted)).Not.Null);

            await Assert.ThatAsync(() => this.client.DeleteTagByProjectAndIdAsync(this.projectId, UnknownId), ThrowsNotFound());
        }

        [Test]
        public async Task VerifyDiffAsync()
        {
            var branch = await this.client.PostBranchByProjectAsync(this.projectId, new BranchRequest { Name = "diff", Head = this.seedCommitId });
            var renameCommit = await this.client.PostCommitByProjectAsync(this.projectId, DefaultSeedModel.CreateRenameRequest("Truck"), branch.Id);

            var differences = await SupportedAsync(() => this.client.DiffAsync(this.projectId, this.seedCommitId, renameCommit.Id));
            var noDifferences = await this.client.DiffAsync(this.projectId, this.seedCommitId, this.seedCommitId);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(differences.Records.Select(difference => difference.CompareData?.Identity?.Id), Is.EquivalentTo([DefaultSeedModel.PartDefinitionId]));
                Assert.That(noDifferences.Records, Is.Empty);
            }

            await Assert.ThatAsync(() => this.client.DiffAsync(this.projectId, this.seedCommitId, UnknownId), ThrowsNotFound());
        }

        [Test]
        public async Task VerifyGetBranchesByProjectAndIdAsync()
        {
            var branch = await this.client.GetBranchesByProjectAndIdAsync(this.projectId, this.defaultBranchId);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(branch.Id, Is.EqualTo(this.defaultBranchId));
                Assert.That(branch.Head, Is.EqualTo(this.seedCommitId));
                Assert.That(branch.OwningProject, Is.EqualTo(this.projectId));
            }

            await Assert.ThatAsync(() => this.client.GetBranchesByProjectAndIdAsync(this.projectId, UnknownId), ThrowsNotFound());
        }

        [Test]
        public async Task VerifyGetBranchesByProjectAsync()
        {
            var branches = await this.client.GetBranchesByProjectAsync(this.projectId);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(branches.Records.Select(branch => branch.Id), Does.Contain(this.defaultBranchId));
                Assert.That(branches.Records.Select(branch => branch.OwningProject), Is.All.EqualTo(this.projectId));
            }

            await Assert.ThatAsync(() => this.client.GetBranchesByProjectAsync(UnknownId), ThrowsNotFound());
        }

        [Test]
        public async Task VerifyGetChangeByProjectCommitIdAsync()
        {
            var changes = await this.client.GetChangesByProjectCommitAsync(this.projectId, this.seedCommitId);
            var partDefinitionChange = changes.Records.Single(change => change.Identity?.Id == DefaultSeedModel.PartDefinitionId);

            var change = await this.client.GetChangeByProjectCommitIdAsync(this.projectId, this.seedCommitId, partDefinitionChange.Id);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(change.Id, Is.EqualTo(partDefinitionChange.Id));
                Assert.That(change.Identity?.Id, Is.EqualTo(DefaultSeedModel.PartDefinitionId));
                Assert.That(change.Payload, Is.TypeOf<PartDefinition>().With.Property(nameof(PartDefinition.DeclaredName)).EqualTo(DefaultSeedModel.PartDefinitionName));
            }

            await Assert.ThatAsync(() => this.client.GetChangeByProjectCommitIdAsync(this.projectId, this.seedCommitId, UnknownId), ThrowsNotFound());
        }

        [Test]
        public async Task VerifyGetChangesByProjectCommitAsync()
        {
            var changes = await this.client.GetChangesByProjectCommitAsync(this.projectId, this.seedCommitId);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(changes.Records.Select(change => change.Identity?.Id), Is.EquivalentTo([DefaultSeedModel.PackageId, DefaultSeedModel.MembershipId, DefaultSeedModel.PartDefinitionId]));
                Assert.That(changes.Records.Select(change => change.Payload?.GetType()), Is.EquivalentTo([typeof(Package), typeof(OwningMembership), typeof(PartDefinition)]));
            }

            await Assert.ThatAsync(() => this.client.GetChangesByProjectCommitAsync(this.projectId, UnknownId), ThrowsNotFound());
        }

        [Test]
        public async Task VerifyGetCommitByProjectAndIdAsync()
        {
            var commit = await this.client.GetCommitByProjectAndIdAsync(this.projectId, this.seedCommitId);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(commit.Id, Is.EqualTo(this.seedCommitId));
                Assert.That(commit.OwningProject, Is.EqualTo(this.projectId));
                Assert.That(commit.PreviousCommit, Is.Empty);
            }

            await Assert.ThatAsync(() => this.client.GetCommitByProjectAndIdAsync(this.projectId, UnknownId), ThrowsNotFound());
        }

        [Test]
        public async Task VerifyGetCommitsByProjectAsync()
        {
            var commits = await this.client.GetCommitsByProjectAsync(this.projectId);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(commits.Records.Select(commit => commit.Id), Does.Contain(this.seedCommitId));
                Assert.That(commits.Records.Select(commit => commit.OwningProject), Is.All.EqualTo(this.projectId));
            }

            await Assert.ThatAsync(() => this.client.GetCommitsByProjectAsync(UnknownId), ThrowsNotFound());
        }

        [Test]
        public async Task VerifyGetDatatypeByIdAsync()
        {
            using (var datatype = await this.client.GetDatatypeByIdAsync(PartUsageDatatypeId))
            {
                Assert.That(datatype.RootElement.GetProperty("$id").GetString(), Is.EqualTo(PartUsageDatatypeId));
            }

            await Assert.ThatAsync(() => this.client.GetDatatypeByIdAsync($"{PartUsageDatatypeId}Unknown"), ThrowsNotFound());
        }

        [Test]
        public async Task VerifyGetDatatypesAsync()
        {
            var firstPage = await this.client.GetDatatypesAsync(new QueryParameters { PageSize = PageSize });

            try
            {
                Assert.That(firstPage.Records, Has.Count.EqualTo(1));

                using (Assert.EnterMultipleScope())
                {
                    Assert.That(firstPage.Records[0].RootElement.GetProperty("$defs").EnumerateObject().Select(datatype => datatype.Name), Has.Exactly(PageSize).Items);
                    Assert.That(firstPage.Next, Is.Not.Null);
                }
            }
            finally
            {
                foreach (var datatypes in firstPage.Records)
                {
                    datatypes.Dispose();
                }
            }

            Assert.That(await this.ReadDatatypeIdsAsync(), Has.Member(PartUsageDatatypeId));
        }

        [Test]
        public async Task VerifyGetElementByProjectCommitIdAsync()
        {
            var element = await this.client.GetElementByProjectCommitIdAsync(this.projectId, this.seedCommitId, DefaultSeedModel.PartDefinitionId);

            Assert.That(element, Is.TypeOf<PartDefinition>().With.Property(nameof(PartDefinition.DeclaredName)).EqualTo(DefaultSeedModel.PartDefinitionName));

            await Assert.ThatAsync(() => this.client.GetElementByProjectCommitIdAsync(this.projectId, this.seedCommitId, UnknownId), ThrowsNotFound());
        }

        [Test]
        public async Task VerifyGetElementsByProjectCommitAsync()
        {
            var firstPage = await this.client.GetElementsByProjectCommitAsync(this.projectId, this.seedCommitId, queryParameters: new QueryParameters { PageSize = PageSize });

            using (Assert.EnterMultipleScope())
            {
                Assert.That(firstPage.Records, Has.Count.EqualTo(PageSize));
                Assert.That(firstPage.Next, Is.Not.Null);
            }

            var secondPage = await this.client.GetElementsByProjectCommitAsync(this.projectId, this.seedCommitId, queryParameters: new QueryParameters { PageSize = PageSize, PageAfter = firstPage.Next });

            Assert.That(firstPage.Records.Concat(secondPage.Records).Select(element => element.Id), Is.EquivalentTo([DefaultSeedModel.PackageId, DefaultSeedModel.MembershipId, DefaultSeedModel.PartDefinitionId]));

            await Assert.ThatAsync(() => this.client.GetElementsByProjectCommitAsync(this.projectId, UnknownId), ThrowsNotFound());
        }

        [Test]
        public async Task VerifyGetProjectByIdAsync()
        {
            var project = await this.client.GetProjectByIdAsync(this.projectId);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(project.Id, Is.EqualTo(this.projectId));
                Assert.That(project.DefaultBranch, Is.EqualTo(this.defaultBranchId));
            }

            await Assert.ThatAsync(() => this.client.GetProjectByIdAsync(UnknownId), ThrowsNotFound());
        }

        [Test]
        public async Task VerifyGetProjectsAsync()
        {
            for (var projectIndex = 0; projectIndex < PageSize; projectIndex++)
            {
                await this.CreateProjectAsync();
            }

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
        public async Task VerifyGetProjectUsageByProjectCommitElementAsync()
        {
            await Assert.ThatAsync(() => this.client.GetProjectUsageByProjectCommitElementAsync(this.projectId, this.seedCommitId, UnknownId), ThrowsNotFound());
        }

        [Test]
        public async Task VerifyGetQueriesByProjectAsync()
        {
            var query = await this.client.PostQueryByProjectAsync(this.projectId, CreatePartDefinitionQuery());

            var queries = await this.client.GetQueriesByProjectAsync(this.projectId);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(queries.Records.Select(record => record.Id), Does.Contain(query.Id));
                Assert.That(queries.Records.Select(record => record.OwningProject), Is.All.EqualTo(this.projectId));
            }

            await Assert.ThatAsync(() => this.client.GetQueriesByProjectAsync(UnknownId), ThrowsNotFound());
        }

        [Test]
        public async Task VerifyGetQueryByProjectAndIdAsync()
        {
            var query = await this.client.PostQueryByProjectAsync(this.projectId, CreatePartDefinitionQuery());

            var readQuery = await this.client.GetQueryByProjectAndIdAsync(this.projectId, query.Id);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(readQuery.Id, Is.EqualTo(query.Id));
                Assert.That(readQuery.Select, Is.EquivalentTo(SelectedProperties));
                Assert.That(readQuery.Where, Is.TypeOf<PrimitiveConstraint>());
            }

            await Assert.ThatAsync(() => this.client.GetQueryByProjectAndIdAsync(this.projectId, UnknownId), ThrowsNotFound());
        }

        [Test]
        public async Task VerifyGetQueryResultsByProjectIdQueryAsync()
        {
            var partDefinitions = await SupportedAsync(() => this.client.GetQueryResultsByProjectIdQueryAsync(this.projectId, CreatePartDefinitionQuery(), this.seedCommitId));
            var package = await this.client.GetQueryResultsByProjectIdQueryAsync(this.projectId, CreateIdentifierQuery(DefaultSeedModel.PackageId), this.seedCommitId);
            var unknownElement = await this.client.GetQueryResultsByProjectIdQueryAsync(this.projectId, CreateIdentifierQuery(UnknownId), this.seedCommitId);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(partDefinitions.Records.Select(result => result.Id), Is.EquivalentTo([DefaultSeedModel.PartDefinitionId]));
                Assert.That(package.Records.Select(result => result.Id), Is.EquivalentTo([DefaultSeedModel.PackageId]));
                Assert.That(unknownElement.Records, Is.Empty);
            }

            await Assert.ThatAsync(() => this.client.GetQueryResultsByProjectIdQueryAsync(UnknownId, CreatePartDefinitionQuery()), ThrowsNotFound());
        }

        [Test]
        public async Task VerifyGetQueryResultsByProjectIdQueryIdAsync()
        {
            var query = await this.client.PostQueryByProjectAsync(this.projectId, CreatePartDefinitionQuery());

            var results = await this.client.GetQueryResultsByProjectIdQueryIdAsync(this.projectId, query.Id, this.seedCommitId);

            Assert.That(results.Records.Select(result => result.Id), Is.EquivalentTo([DefaultSeedModel.PartDefinitionId]));

            await Assert.ThatAsync(() => this.client.GetQueryResultsByProjectIdQueryIdAsync(this.projectId, UnknownId, this.seedCommitId), ThrowsNotFound());
        }

        [Test]
        public async Task VerifyGetQueryResultsByProjectIdQueryPostAsync()
        {
            var results = await this.client.GetQueryResultsByProjectIdQueryPostAsync(this.projectId, CreatePartDefinitionQuery(), this.seedCommitId);

            Assert.That(results.Records.Select(result => result.Id), Is.EquivalentTo([DefaultSeedModel.PartDefinitionId]));

            await Assert.ThatAsync(() => this.client.GetQueryResultsByProjectIdQueryPostAsync(UnknownId, CreatePartDefinitionQuery()), ThrowsNotFound());
        }

        [Test]
        public async Task VerifyGetRelationshipsByProjectCommitRelatedElementAsync()
        {
            var packageOutgoing = await this.client.GetRelationshipsByProjectCommitRelatedElementAsync(this.projectId, this.seedCommitId, DefaultSeedModel.PackageId, Direction.@out);
            var packageIncoming = await this.client.GetRelationshipsByProjectCommitRelatedElementAsync(this.projectId, this.seedCommitId, DefaultSeedModel.PackageId, Direction.@in);
            var packageBoth = await this.client.GetRelationshipsByProjectCommitRelatedElementAsync(this.projectId, this.seedCommitId, DefaultSeedModel.PackageId, Direction.both);
            var partDefinitionOutgoing = await this.client.GetRelationshipsByProjectCommitRelatedElementAsync(this.projectId, this.seedCommitId, DefaultSeedModel.PartDefinitionId, Direction.@out);
            var partDefinitionIncoming = await this.client.GetRelationshipsByProjectCommitRelatedElementAsync(this.projectId, this.seedCommitId, DefaultSeedModel.PartDefinitionId, Direction.@in);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(packageOutgoing.Records.Select(relationship => relationship.Id), Is.EquivalentTo([DefaultSeedModel.MembershipId]));
                Assert.That(packageIncoming.Records, Is.Empty);
                Assert.That(packageBoth.Records.Select(relationship => relationship.Id), Is.EquivalentTo([DefaultSeedModel.MembershipId]));
                Assert.That(partDefinitionOutgoing.Records, Is.Empty);
                Assert.That(partDefinitionIncoming.Records.Select(relationship => relationship.Id), Is.EquivalentTo([DefaultSeedModel.MembershipId]));
            }

            await Assert.ThatAsync(() => this.client.GetRelationshipsByProjectCommitRelatedElementAsync(this.projectId, this.seedCommitId, UnknownId), ThrowsNotFound());
        }

        [Test]
        public async Task VerifyGetRootsByProjectCommitAsync()
        {
            var roots = await this.client.GetRootsByProjectCommitAsync(this.projectId, this.seedCommitId);

            Assert.That(roots.Records.Select(root => root.Id), Is.EquivalentTo([DefaultSeedModel.PackageId]));

            await Assert.ThatAsync(() => this.client.GetRootsByProjectCommitAsync(this.projectId, UnknownId), ThrowsNotFound());
        }

        [Test]
        public async Task VerifyGetTagByProjectAndIdAsync()
        {
            var tag = await this.client.PostTagByProjectAsync(this.projectId, new TagRequest { Name = "read", TaggedCommit = this.seedCommitId });

            var readTag = await this.client.GetTagByProjectAndIdAsync(this.projectId, tag.Id);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(readTag.Id, Is.EqualTo(tag.Id));
                Assert.That(readTag.Name, Is.EqualTo("read"));
                Assert.That(readTag.TaggedCommit, Is.EqualTo(this.seedCommitId));
            }

            await Assert.ThatAsync(() => this.client.GetTagByProjectAndIdAsync(this.projectId, UnknownId), ThrowsNotFound());
        }

        [Test]
        public async Task VerifyGetTagsByProjectAsync()
        {
            var tag = await this.client.PostTagByProjectAsync(this.projectId, new TagRequest { Name = "listed", TaggedCommit = this.seedCommitId });

            var tags = await this.client.GetTagsByProjectAsync(this.projectId);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(tags.Records.Select(record => record.Id), Does.Contain(tag.Id));
                Assert.That(tags.Records.Select(record => record.OwningProject), Is.All.EqualTo(this.projectId));
            }

            await Assert.ThatAsync(() => this.client.GetTagsByProjectAsync(UnknownId), ThrowsNotFound());
        }

        [Test]
        public async Task VerifyMergeAsync()
        {
            var source = await this.client.PostBranchByProjectAsync(this.projectId, new BranchRequest { Name = "merge-source", Head = this.seedCommitId });
            var target = await this.client.PostBranchByProjectAsync(this.projectId, new BranchRequest { Name = "merge-target", Head = this.seedCommitId });
            var sourceCommit = await this.client.PostCommitByProjectAsync(this.projectId, DefaultSeedModel.CreateRenameRequest("Bicycle"), source.Id);

            var merge = await SupportedAsync(() => this.client.MergeAsync(this.projectId, [sourceCommit.Id], target.Id, description: "merge"));

            var mergedTarget = await this.client.GetBranchesByProjectAndIdAsync(this.projectId, target.Id);
            var mergedPartDefinition = await this.client.GetElementByProjectCommitIdAsync(this.projectId, merge.Id, DefaultSeedModel.PartDefinitionId);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(merge.OwningProject, Is.EqualTo(this.projectId));
                Assert.That(mergedTarget.Head, Is.EqualTo(merge.Id));
                Assert.That(mergedPartDefinition, Is.TypeOf<PartDefinition>().With.Property(nameof(PartDefinition.DeclaredName)).EqualTo("Bicycle"));
            }

            await this.client.PostCommitByProjectAsync(this.projectId, DefaultSeedModel.CreateRenameRequest("Boat"), source.Id);
            var conflictingCommit = await this.client.PostCommitByProjectAsync(this.projectId, DefaultSeedModel.CreateRenameRequest("Plane"), target.Id);

            MergeConflictException conflict = null;

            try
            {
                await this.client.MergeAsync(this.projectId, [conflictingCommit.Id], source.Id);
            }
            catch (MergeConflictException exception)
            {
                conflict = exception;
            }

            Assert.That(conflict, Is.Not.Null);

            if (conflict.Conflicts.Count == 0)
            {
                Assert.Inconclusive("The server answered the merge conflict without listing the conflicting data");
            }

            Assert.That(conflict.Conflicts.Select(identity => identity.Id), Does.Contain(DefaultSeedModel.PartDefinitionId));
        }

        [Test]
        public async Task VerifyPostBranchByProjectAsync()
        {
            var branch = await this.client.PostBranchByProjectAsync(this.projectId, new BranchRequest { Name = "feature", Head = this.seedCommitId });

            var branches = await this.client.GetBranchesByProjectAsync(this.projectId);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(branch.OwningProject, Is.EqualTo(this.projectId));
                Assert.That(branch.Head, Is.EqualTo(this.seedCommitId));
                Assert.That(branches.Records.Select(record => record.Id), Does.Contain(branch.Id));
            }

            await Assert.ThatAsync(() => this.client.PostBranchByProjectAsync(UnknownId, new BranchRequest { Name = "feature", Head = this.seedCommitId }), ThrowsClientError());
        }

        [Test]
        public async Task VerifyPostCommitByProjectAsync()
        {
            var branch = await this.client.PostBranchByProjectAsync(this.projectId, new BranchRequest { Name = "rename", Head = this.seedCommitId });

            var commit = await this.client.PostCommitByProjectAsync(this.projectId, DefaultSeedModel.CreateRenameRequest("Car"), branch.Id);

            var committedBranch = await this.client.GetBranchesByProjectAndIdAsync(this.projectId, branch.Id);
            var renamed = await this.client.GetElementByProjectCommitIdAsync(this.projectId, commit.Id, DefaultSeedModel.PartDefinitionId);
            var original = await this.client.GetElementByProjectCommitIdAsync(this.projectId, this.seedCommitId, DefaultSeedModel.PartDefinitionId);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(commit.OwningProject, Is.EqualTo(this.projectId));
                Assert.That(commit.PreviousCommit, Is.EquivalentTo([this.seedCommitId]));
                Assert.That(committedBranch.Head, Is.EqualTo(commit.Id));
                Assert.That(renamed, Is.TypeOf<PartDefinition>().With.Property(nameof(PartDefinition.DeclaredName)).EqualTo("Car"));
                Assert.That(original, Is.TypeOf<PartDefinition>().With.Property(nameof(PartDefinition.DeclaredName)).EqualTo(DefaultSeedModel.PartDefinitionName));
            }

            await Assert.ThatAsync(() => this.client.PostCommitByProjectAsync(UnknownId, DefaultSeedModel.CreateRenameRequest("Car")), ThrowsClientError());
        }

        [Test]
        public async Task VerifyPostProjectAsync()
        {
            var project = await this.CreateProjectAsync();

            var readProject = await this.client.GetProjectByIdAsync(project.Id);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(project.Name, Does.StartWith(ProjectNamePrefix));
                Assert.That(project.Description, Is.EqualTo(ProjectDescription));
                Assert.That(project.DefaultBranch, Is.Not.EqualTo(Guid.Empty));
                Assert.That(readProject.Name, Is.EqualTo(project.Name));
            }
        }

        [Test]
        public async Task VerifyPostQueryByProjectAsync()
        {
            var query = await this.client.PostQueryByProjectAsync(this.projectId, CreatePartDefinitionQuery());

            using (Assert.EnterMultipleScope())
            {
                Assert.That(query.OwningProject, Is.EqualTo(this.projectId));
                Assert.That(query.Select, Is.EquivalentTo(SelectedProperties));
                Assert.That(query.Where, Is.TypeOf<PrimitiveConstraint>());
            }

            await Assert.ThatAsync(() => this.client.PostQueryByProjectAsync(UnknownId, CreatePartDefinitionQuery()), ThrowsClientError());
        }

        [Test]
        public async Task VerifyPostTagByProjectAsync()
        {
            var tag = await this.client.PostTagByProjectAsync(this.projectId, new TagRequest { Name = "seed", TaggedCommit = this.seedCommitId });

            using (Assert.EnterMultipleScope())
            {
                Assert.That(tag.OwningProject, Is.EqualTo(this.projectId));
                Assert.That(tag.TaggedCommit, Is.EqualTo(this.seedCommitId));
            }

            await Assert.ThatAsync(() => this.client.PostTagByProjectAsync(UnknownId, new TagRequest { Name = "seed", TaggedCommit = this.seedCommitId }), ThrowsClientError());
        }

        [Test]
        public async Task VerifyPutProjectByIdAsync()
        {
            var name = $"{ProjectNamePrefix}{Guid.NewGuid()}";

            var project = await this.client.PutProjectByIdAsync(this.projectId, new ProjectRequest { Name = name, DefaultBranch = this.defaultBranchId });

            var readProject = await this.client.GetProjectByIdAsync(this.projectId);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(project.Id, Is.EqualTo(this.projectId));
                Assert.That(project.Name, Is.EqualTo(name));
                Assert.That(readProject.Name, Is.EqualTo(name));
            }

            await Assert.ThatAsync(() => this.client.PutProjectByIdAsync(UnknownId, new ProjectRequest { Name = name }), ThrowsNotFound());
        }

        [Test]
        public async Task VerifyPutQueryByProjectAndIdAsync()
        {
            var query = await this.client.PostQueryByProjectAsync(this.projectId, CreatePartDefinitionQuery());
            var request = CreatePartDefinitionQuery();
            request.Name = "renamed";

            var updatedQuery = await this.client.PutQueryByProjectAndIdAsync(this.projectId, query.Id, request);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(updatedQuery.Id, Is.EqualTo(query.Id));
                Assert.That(updatedQuery.Name, Is.EqualTo("renamed"));
            }

            await Assert.ThatAsync(() => this.client.PutQueryByProjectAndIdAsync(this.projectId, UnknownId, request), ThrowsNotFound());
        }

        private static QueryRequest CreateIdentifierQuery(Guid elementId)
        {
            return new QueryRequest
            {
                Select = [.. SelectedProperties],
                Where = new PrimitiveConstraintRequest
                {
                    Property = "@id",
                    Operator = PrimitiveConstraintRequestOperator.equalto,
                    Inverse = false,
                    Value = [elementId.ToString()]
                }
            };
        }

        private static QueryRequest CreatePartDefinitionQuery()
        {
            return new QueryRequest
            {
                Name = "part definitions",
                Select = [.. SelectedProperties],
                Where = new PrimitiveConstraintRequest
                {
                    Property = "@type",
                    Operator = PrimitiveConstraintRequestOperator.equalto,
                    Inverse = false,
                    Value = ["PartDefinition"]
                }
            };
        }

        private static async Task<T> GetOrNullWhenNotFoundAsync<T>(Func<Task<T>> read) where T : class
        {
            try
            {
                return await read();
            }
            catch (PsmApiException exception) when (exception.StatusCode == HttpStatusCode.NotFound)
            {
                return null;
            }
        }

        private static async Task<T> SupportedAsync<T>(Func<Task<T>> operation)
        {
            try
            {
                return await operation();
            }
            catch (PsmApiException exception) when (exception.StatusCode is HttpStatusCode.MethodNotAllowed or HttpStatusCode.NotImplemented)
            {
                Assert.Inconclusive($"The server does not support the operation: {(int)exception.StatusCode} {exception.StatusCode}");
                throw;
            }
        }

        private static RangeConstraint ThrowsClientError()
        {
            return Throws.TypeOf<PsmApiException>().With.Property(nameof(PsmApiException.StatusCode)).InRange(HttpStatusCode.BadRequest, LastClientError);
        }

        private static EqualConstraint<HttpStatusCode> ThrowsNotFound()
        {
            return Throws.TypeOf<PsmApiException>().With.Property(nameof(PsmApiException.StatusCode)).EqualTo(HttpStatusCode.NotFound);
        }

        private async Task<Project> CreateProjectAsync()
        {
            var project = await this.client.PostProjectAsync(new ProjectRequest { Name = $"{ProjectNamePrefix}{Guid.NewGuid()}", Description = ProjectDescription });

            this.createdProjectIds.Add(project.Id);

            return project;
        }

        private async Task<List<string>> ReadDatatypeIdsAsync()
        {
            var datatypeIds = new List<string>();
            Page? pageAfter = null;

            do
            {
                var page = await this.client.GetDatatypesAsync(new QueryParameters { PageSize = DatatypePageSize, PageAfter = pageAfter });

                foreach (var datatypes in page.Records)
                {
                    using (datatypes)
                    {
                        datatypeIds.AddRange(datatypes.RootElement.GetProperty("$defs").EnumerateObject().Select(datatype => datatype.Name));
                    }
                }

                pageAfter = page.Next;
            }
            while (pageAfter.HasValue);

            return datatypeIds;
        }
    }
}
