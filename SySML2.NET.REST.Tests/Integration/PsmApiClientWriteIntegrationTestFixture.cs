// ------------------------------------------------------------------------------------------------
// <copyright file="PsmApiClientWriteIntegrationTestFixture.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SySML2.NET.REST.Tests.Integration
{
    using System;
    using System.Linq;
    using System.Net;
    using System.Text.Json;
    using System.Threading.Tasks;

    using NUnit.Framework;

    using SysML2.NET.Core.DTO.Systems.Parts;
    using SysML2.NET.PSM.DTO;
    using SysML2.NET.PSM.Enumerations;
    using SysML2.NET.REST.Exceptions;

    [TestFixture]
    [Category("Integration")]
    public class PsmApiClientWriteIntegrationTestFixture
    {
        private const string ProjectNamePrefix = "SysML2.NET-it-";

        private static readonly Guid UnknownId = Guid.Parse("5b0c3f1e-7d2a-4e9b-8f6c-1a2b3c4d5e6f");

        private static readonly string[] SelectedProperties = ["@id", "@type"];

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

            var project = await this.client.PostProjectAsync(new ProjectRequest { Name = $"{ProjectNamePrefix}{Guid.NewGuid()}", Description = "Throw-away project of the SysML2.NET integration tests" });

            this.projectId = project.Id;
            this.defaultBranchId = project.DefaultBranch;
            this.seedCommitId = await this.CommitAsync(DefaultSeedModel.CreateCommitRequest(), this.defaultBranchId);

            await TestContext.Out.WriteLineAsync($"Writing to project {this.projectId}, seed commit {this.seedCommitId}");
        }

        [OneTimeTearDown]
        public async Task OneTimeTearDown()
        {
            try
            {
                if (this.projectId != Guid.Empty)
                {
                    await this.client.DeleteProjectByIdAsync(this.projectId);
                }
            }
            finally
            {
                this.host.Dispose();
            }
        }

        [Test]
        public async Task VerifyPutProjectByIdAsync()
        {
            var name = $"{ProjectNamePrefix}{Guid.NewGuid()}";

            var project = await this.client.PutProjectByIdAsync(this.projectId, new ProjectRequest { Name = name, DefaultBranch = this.defaultBranchId });

            using (Assert.EnterMultipleScope())
            {
                Assert.That(project.Id, Is.EqualTo(this.projectId));
                Assert.That(project.Name, Is.EqualTo(name));
                Assert.That((await this.client.GetProjectByIdAsync(this.projectId)).Name, Is.EqualTo(name));
            }

            await Assert.ThatAsync(() => this.client.PutProjectByIdAsync(UnknownId, new ProjectRequest { Name = name }),
                Throws.TypeOf<PsmApiException>().With.Property(nameof(PsmApiException.StatusCode)).EqualTo(HttpStatusCode.NotFound));
        }

        [Test]
        public async Task VerifyDeleteProjectByIdAsync()
        {
            var project = await this.client.PostProjectAsync(new ProjectRequest { Name = $"{ProjectNamePrefix}{Guid.NewGuid()}" });

            await this.client.DeleteProjectByIdAsync(project.Id);

            await Assert.ThatAsync(() => this.client.GetProjectByIdAsync(project.Id),
                Throws.TypeOf<PsmApiException>().With.Property(nameof(PsmApiException.StatusCode)).EqualTo(HttpStatusCode.NotFound));

            await Assert.ThatAsync(() => this.client.DeleteProjectByIdAsync(UnknownId), Throws.TypeOf<PsmApiException>());
        }

        [Test]
        public async Task VerifyPostCommitByProjectAsync()
        {
            var commit = await this.client.PostCommitByProjectAsync(this.projectId, DefaultSeedModel.CreateRenameRequest("Car"), this.defaultBranchId);
            var branch = await this.client.GetBranchesByProjectAndIdAsync(this.projectId, this.defaultBranchId);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(commit.OwningProject, Is.EqualTo(this.projectId));
                Assert.That(branch.Head, Is.EqualTo(commit.Id));
            }

            await Assert.ThatAsync(() => this.client.PostCommitByProjectAsync(UnknownId, DefaultSeedModel.CreateRenameRequest("Car")), Throws.TypeOf<PsmApiException>());
        }

        [Test]
        public async Task VerifyGetRootsByProjectCommitAsync()
        {
            var roots = await this.client.GetRootsByProjectCommitAsync(this.projectId, this.seedCommitId);
            var partDefinition = await this.client.GetElementByProjectCommitIdAsync(this.projectId, this.seedCommitId, DefaultSeedModel.PartDefinitionId);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(roots.Records.Select(root => root.Id), Is.EquivalentTo([DefaultSeedModel.PackageId]));
                Assert.That(partDefinition, Is.TypeOf<PartDefinition>().With.Property(nameof(PartDefinition.DeclaredName)).EqualTo(DefaultSeedModel.PartDefinitionName));
            }

            await Assert.ThatAsync(() => this.client.GetElementByProjectCommitIdAsync(this.projectId, this.seedCommitId, UnknownId),
                Throws.TypeOf<PsmApiException>().With.Property(nameof(PsmApiException.StatusCode)).EqualTo(HttpStatusCode.NotFound));
        }

        [Test]
        public async Task VerifyPostBranchByProjectAsync()
        {
            var branch = await this.client.PostBranchByProjectAsync(this.projectId, new BranchRequest { Name = "feature", Head = this.seedCommitId });

            using (Assert.EnterMultipleScope())
            {
                Assert.That(branch.OwningProject, Is.EqualTo(this.projectId));
                Assert.That(branch.Head, Is.EqualTo(this.seedCommitId));
                Assert.That((await this.client.GetBranchesByProjectAsync(this.projectId)).Records.Select(record => record.Id), Does.Contain(branch.Id));
            }

            await Assert.ThatAsync(() => this.client.PostBranchByProjectAsync(UnknownId, new BranchRequest { Name = "feature", Head = this.seedCommitId }), Throws.TypeOf<PsmApiException>());
        }

        [Test]
        public async Task VerifyDeleteBranchByProjectAndIdAsync()
        {
            var branch = await this.client.PostBranchByProjectAsync(this.projectId, new BranchRequest { Name = "to-delete", Head = this.seedCommitId });

            await this.client.DeleteBranchByProjectAndIdAsync(this.projectId, branch.Id);

            var deletedBranch = await GetOrNullWhenNotFoundAsync(() => this.client.GetBranchesByProjectAndIdAsync(this.projectId, branch.Id));

            Assert.That(deletedBranch is null || deletedBranch.Deleted is not null, Is.True);

            await Assert.ThatAsync(() => this.client.DeleteBranchByProjectAndIdAsync(this.projectId, UnknownId),
                Throws.TypeOf<PsmApiException>().With.Property(nameof(PsmApiException.StatusCode)).EqualTo(HttpStatusCode.NotFound));
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

            await Assert.ThatAsync(() => this.client.PostTagByProjectAsync(UnknownId, new TagRequest { Name = "seed", TaggedCommit = this.seedCommitId }), Throws.TypeOf<PsmApiException>());
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
            }

            await Assert.ThatAsync(() => this.client.GetTagByProjectAndIdAsync(this.projectId, UnknownId),
                Throws.TypeOf<PsmApiException>().With.Property(nameof(PsmApiException.StatusCode)).EqualTo(HttpStatusCode.NotFound));
        }

        [Test]
        public async Task VerifyDeleteTagByProjectAndIdAsync()
        {
            var tag = await this.client.PostTagByProjectAsync(this.projectId, new TagRequest { Name = "to-delete", TaggedCommit = this.seedCommitId });

            await this.client.DeleteTagByProjectAndIdAsync(this.projectId, tag.Id);

            var deletedTag = await GetOrNullWhenNotFoundAsync(() => this.client.GetTagByProjectAndIdAsync(this.projectId, tag.Id));

            Assert.That(deletedTag is null || deletedTag.Deleted is not null, Is.True);

            await Assert.ThatAsync(() => this.client.DeleteTagByProjectAndIdAsync(this.projectId, UnknownId),
                Throws.TypeOf<PsmApiException>().With.Property(nameof(PsmApiException.StatusCode)).EqualTo(HttpStatusCode.NotFound));
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

            await Assert.ThatAsync(() => this.client.PostQueryByProjectAsync(UnknownId, CreatePartDefinitionQuery()), Throws.TypeOf<PsmApiException>());
        }

        [Test]
        public async Task VerifyGetQueryByProjectAndIdAsync()
        {
            var query = await this.client.PostQueryByProjectAsync(this.projectId, CreatePartDefinitionQuery());

            Assert.That((await this.client.GetQueryByProjectAndIdAsync(this.projectId, query.Id)).Id, Is.EqualTo(query.Id));

            await Assert.ThatAsync(() => this.client.GetQueryByProjectAndIdAsync(this.projectId, UnknownId),
                Throws.TypeOf<PsmApiException>().With.Property(nameof(PsmApiException.StatusCode)).EqualTo(HttpStatusCode.NotFound));
        }

        [Test]
        public async Task VerifyPutQueryByProjectAndIdAsync()
        {
            var query = await this.client.PostQueryByProjectAsync(this.projectId, CreatePartDefinitionQuery());
            var request = CreatePartDefinitionQuery();
            request.Name = "renamed";

            Assert.That((await this.client.PutQueryByProjectAndIdAsync(this.projectId, query.Id, request)).Name, Is.EqualTo("renamed"));

            await Assert.ThatAsync(() => this.client.PutQueryByProjectAndIdAsync(this.projectId, UnknownId, request),
                Throws.TypeOf<PsmApiException>().With.Property(nameof(PsmApiException.StatusCode)).EqualTo(HttpStatusCode.NotFound));
        }

        [Test]
        public async Task VerifyDeleteQueryByProjectAndIdAsync()
        {
            var query = await this.client.PostQueryByProjectAsync(this.projectId, CreatePartDefinitionQuery());

            await this.client.DeleteQueryByProjectAndIdAsync(this.projectId, query.Id);

            await Assert.ThatAsync(() => this.client.GetQueryByProjectAndIdAsync(this.projectId, query.Id),
                Throws.TypeOf<PsmApiException>().With.Property(nameof(PsmApiException.StatusCode)).EqualTo(HttpStatusCode.NotFound));

            await Assert.ThatAsync(() => this.client.DeleteQueryByProjectAndIdAsync(this.projectId, UnknownId),
                Throws.TypeOf<PsmApiException>().With.Property(nameof(PsmApiException.StatusCode)).EqualTo(HttpStatusCode.NotFound));
        }

        [Test]
        public async Task VerifyGetQueryResultsByProjectIdQueryIdAsync()
        {
            var query = await this.client.PostQueryByProjectAsync(this.projectId, CreatePartDefinitionQuery());

            var results = await this.client.GetQueryResultsByProjectIdQueryIdAsync(this.projectId, query.Id, this.seedCommitId);

            Assert.That(results.Records.Select(result => result.Id), Is.EquivalentTo([DefaultSeedModel.PartDefinitionId]));

            await Assert.ThatAsync(() => this.client.GetQueryResultsByProjectIdQueryIdAsync(this.projectId, UnknownId),
                Throws.TypeOf<PsmApiException>().With.Property(nameof(PsmApiException.StatusCode)).EqualTo(HttpStatusCode.NotFound));
        }

        [Test]
        public async Task VerifyGetQueryResultsByProjectIdQueryAsync()
        {
            var results = await SupportedAsync(() => this.client.GetQueryResultsByProjectIdQueryAsync(this.projectId, CreatePartDefinitionQuery(), this.seedCommitId));

            Assert.That(results.Records.Select(result => result.Id), Is.EquivalentTo([DefaultSeedModel.PartDefinitionId]));

            await Assert.ThatAsync(() => this.client.GetQueryResultsByProjectIdQueryAsync(UnknownId, CreatePartDefinitionQuery()), Throws.TypeOf<PsmApiException>());
        }

        [Test]
        public async Task VerifyGetQueryResultsByProjectIdQueryPostAsync()
        {
            var results = await this.client.GetQueryResultsByProjectIdQueryPostAsync(this.projectId, CreatePartDefinitionQuery(), this.seedCommitId);

            Assert.That(results.Records.Select(result => result.Id), Is.EquivalentTo([DefaultSeedModel.PartDefinitionId]));

            await Assert.ThatAsync(() => this.client.GetQueryResultsByProjectIdQueryPostAsync(UnknownId, CreatePartDefinitionQuery()), Throws.TypeOf<PsmApiException>());
        }

        [Test]
        public async Task VerifyDiffAsync()
        {
            var renameCommitId = await this.CommitAsync(DefaultSeedModel.CreateRenameRequest("Truck"), this.defaultBranchId);

            var differences = await SupportedAsync(() => this.client.DiffAsync(this.projectId, this.seedCommitId, renameCommitId));

            Assert.That(differences.Records.Select(difference => difference.CompareData?.Identity?.Id), Is.EquivalentTo([DefaultSeedModel.PartDefinitionId]));

            await Assert.ThatAsync(() => this.client.DiffAsync(this.projectId, this.seedCommitId, UnknownId),
                Throws.TypeOf<PsmApiException>().With.Property(nameof(PsmApiException.StatusCode)).EqualTo(HttpStatusCode.NotFound));
        }

        [Test]
        public async Task VerifyMergeAsync()
        {
            var source = await this.client.PostBranchByProjectAsync(this.projectId, new BranchRequest { Name = "merge-source", Head = this.seedCommitId });
            var target = await this.client.PostBranchByProjectAsync(this.projectId, new BranchRequest { Name = "merge-target", Head = this.seedCommitId });
            var sourceCommitId = await this.CommitAsync(DefaultSeedModel.CreateRenameRequest("Bicycle"), source.Id);

            var merge = await SupportedAsync(() => this.client.MergeAsync(this.projectId, [sourceCommitId], target.Id, description: "merge"));

            Assert.That(merge.OwningProject, Is.EqualTo(this.projectId));

            await this.CommitAsync(DefaultSeedModel.CreateRenameRequest("Boat"), source.Id);
            var conflictingCommitId = await this.CommitAsync(DefaultSeedModel.CreateRenameRequest("Plane"), target.Id);

            await Assert.ThatAsync(() => this.client.MergeAsync(this.projectId, [conflictingCommitId], source.Id), Throws.TypeOf<MergeConflictException>());
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

        private async Task<Guid> CommitAsync(CommitRequest request, Guid branchId)
        {
            try
            {
                return (await this.client.PostCommitByProjectAsync(this.projectId, request, branchId)).Id;
            }
            catch (InvalidOperationException exception)
            {
                await TestContext.Out.WriteLineAsync($"The commit response could not be read, the head of the branch is used instead: {exception.Message}");

                var branch = await this.client.GetBranchesByProjectAndIdAsync(this.projectId, branchId);

                return branch.Head ?? throw new InvalidOperationException("The branch has no head after the commit", exception);
            }
        }
    }
}
