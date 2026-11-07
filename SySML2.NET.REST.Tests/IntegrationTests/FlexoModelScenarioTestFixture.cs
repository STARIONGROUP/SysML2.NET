// ------------------------------------------------------------------------------------------------
// <copyright file="FlexoModelScenarioTestFixture.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SySML2.NET.REST.Tests.IntegrationTests
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Threading.Tasks;

    using NUnit.Framework;

    using SySML2.NET.REST;
    using SySML2.NET.REST.Tests.Helpers;
    using SySML2.NET.REST.Tests.Helpers.Models;

    using SysML2.NET.Core.DTO.Root.Elements;
    using SysML2.NET.PSM;
    using SysML2.NET.PSM.DTO;
    using SysML2.NET.REST.Exceptions;
    using SysML2.NET.Serializer.Json;

    /// <summary>
    /// The scenario of <c>SySML2.NET.REST.Tests/INTEGRATION_TEST_PLAN.md</c>: it builds a model step by step on a
    /// Flexo server through the REST client, verifying the client by reading back everything it wrote
    /// </summary>
    /// <remarks>
    /// One <c>[Test]</c> per scenario step, each depending on the previous one with <c>AllowFailure</c>, so the
    /// steps run in order and an assertion failure in one step does not hide the outcome of the next.
    /// </remarks>
    [TestFixture]
    [Category("Integration")]
    [NonParallelizable]
    public class FlexoModelScenarioTestFixture
    {
        private readonly ScenarioState state = new();

        private FlexoTestHost host;

        private OpenApiDocument openApi;

        private IntegrationHttpClientFactory httpClientFactory;

        private Session session;

        private IPsmApiClient client;

        private ReadBackAssertions readBack;

        private int verifiedCalls;

        private RootModel rootModel;

        private CoreModel coreModel;

        private KernelModel kernelModel;

        private KerMLUpdateModel kermlUpdateModel;

        [OneTimeSetUp]
        public async Task OneTimeSetUp()
        {
            this.host = new FlexoTestHost();

            using var startTimeout = new CancellationTokenSource(TimeSpan.FromMinutes(15));
            await this.host.StartAsync(startTimeout.Token);

            this.openApi = OpenApiDocument.Load(Path.Combine(TestContext.CurrentContext.TestDirectory, "Resources", "ptc-25-02-30.json"));
            this.httpClientFactory = new IntegrationHttpClientFactory(this.host.Token, this.openApi);
            this.session = new Session(this.httpClientFactory, new DeSerializer(), new Serializer());

            await this.session.Open(this.host.BaseAddress, CancellationToken.None);

            this.client = this.session.Client;
            this.readBack = new ReadBackAssertions(this.client, this.state, () => this.httpClientFactory.Recorder.Calls);
        }

        [OneTimeTearDown]
        public async Task OneTimeTearDown()
        {
            this.session?.Close();
            this.httpClientFactory?.Dispose();

            if (this.host != null)
            {
                await this.host.DisposeAsync();
            }
        }

        [TearDown]
        public void TearDown()
        {
            if (this.httpClientFactory == null)
            {
                return;
            }

            var calls = this.httpClientFactory.Recorder.Calls.ToList();
            var unverified = calls[this.verifiedCalls..];
            this.verifiedCalls = calls.Count;

            OpenApiConformance.Verify(unverified, KnownServerDivergences.All);
        }

        [Test]
        public async Task VerifyEmptyServer()
        {
            var datatypes = await this.client.GetDatatypesAsync();
            var projects = await this.client.GetProjectsAsync();

            Assert.That(datatypes.Records, Is.Not.Empty);

            var datatypeId = datatypes.Records[0].RootElement.GetProperty("$defs").EnumerateObject().First().Name;
            var datatype = await this.client.GetDatatypeByIdAsync(datatypeId);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(datatype.RootElement.ValueKind, Is.EqualTo(System.Text.Json.JsonValueKind.Object));
                Assert.That(projects.Records, Is.Empty);
            }

            await this.ExpectNotFoundAsync(() => this.client.GetDatatypeByIdAsync("NoSuchDatatype"));
            await this.ExpectNotFoundAsync(() => this.client.GetProjectByIdAsync(Guid.NewGuid()));
        }

        [Test]
        [DependsOnTest(nameof(VerifyEmptyServer), AllowFailure = true)]
        public async Task VerifyProjectAndRootModel()
        {
            var created = await this.client.PostProjectAsync(new ProjectRequest { Name = "CoverageModel", Description = "The model of the REST client integration scenario" });

            this.state.ProjectId = created.Id;
            this.state.DefaultBranchId = created.DefaultBranch;

            var read = await this.client.GetProjectByIdAsync(created.Id);
            var renamed = await this.client.PutProjectByIdAsync(created.Id, new ProjectRequest { Name = "Coverage model", Description = "Renamed by step 2", DefaultBranch = created.DefaultBranch });
            var (projects, _) = await ReadBackAssertions.ReadAllPagesAsync(parameters => this.client.GetProjectsAsync(parameters));

            using (Assert.EnterMultipleScope())
            {
                Assert.That(read.Name, Is.EqualTo("CoverageModel"));
                Assert.That(read.DefaultBranch, Is.EqualTo(created.DefaultBranch));
                Assert.That(renamed.Name, Is.EqualTo("Coverage model"));
                Assert.That(renamed.Description, Is.EqualTo("Renamed by step 2"));
                Assert.That(projects.Select(project => project.Id), Is.EqualTo([created.Id]));
            }

            this.rootModel = new RootModel();

            var committed = await this.CommitModelAsync(this.rootModel.Builder, "Root model");
            var commit = committed.Commit;
            var elements = committed.Model;
            var unknown = Guid.NewGuid();

            using (Assert.EnterMultipleScope())
            {
                await this.ExpectNotFoundAsync(() => this.client.PutProjectByIdAsync(unknown, new ProjectRequest { Name = "Unknown" }));
                await ExpectClientErrorAsync(() => this.client.PostCommitByProjectAsync(unknown, ModelBuilder.ToCommitRequest(elements, "Unknown project")));
                await this.ExpectNotFoundAsync(() => this.client.GetCommitsByProjectAsync(unknown), KnownServerDivergences.UnknownResourceNotFound);
                await this.ExpectNotFoundAsync(() => this.client.GetCommitByProjectAndIdAsync(created.Id, unknown));
                await this.ExpectNotFoundAsync(() => this.client.GetChangesByProjectCommitAsync(created.Id, unknown));
                await this.ExpectNotFoundAsync(() => this.client.GetChangeByProjectCommitIdAsync(created.Id, commit.Id, unknown));
                await this.ExpectNotFoundAsync(() => this.client.GetElementsByProjectCommitAsync(created.Id, unknown));
                await this.ExpectNotFoundAsync(() => this.client.GetElementByProjectCommitIdAsync(created.Id, commit.Id, unknown));
                await this.ExpectNotFoundAsync(() => this.client.GetRootsByProjectCommitAsync(created.Id, unknown));
                await this.ExpectNotFoundAsync(() => this.client.GetRelationshipsByProjectCommitRelatedElementAsync(created.Id, commit.Id, unknown), KnownServerDivergences.UnknownResourceAccepted);
                await this.ExpectNotFoundAsync(() => this.client.GetBranchesByProjectAndIdAsync(created.Id, unknown));
            }

            await this.VerifyCommittedModelAsync(committed, this.rootModel.Nested.Id);
        }

        [Test]
        [DependsOnTest(nameof(VerifyProjectAndRootModel), AllowFailure = true)]
        public async Task VerifyKerMLCore()
        {
            if (this.rootModel == null || this.state.HeadCommitId == null)
            {
                Assert.Ignore("step 2 committed no model to build on");
            }

            this.coreModel = new CoreModel(this.rootModel);

            var committed = await this.CommitModelAsync(this.coreModel.Builder, "KerML Core");

            await this.VerifyCommittedModelAsync(committed, this.coreModel.A.Id);
        }

        [Test]
        [DependsOnTest(nameof(VerifyKerMLCore), AllowFailure = true)]
        public async Task VerifyKerMLKernel()
        {
            if (this.coreModel == null)
            {
                Assert.Ignore("step 3 built no model to build on");
            }

            this.kernelModel = new KernelModel(this.rootModel, this.coreModel);

            var committed = await this.CommitModelAsync(this.kernelModel.Builder, "KerML Kernel");
            var projectId = this.state.ProjectId!.Value;

            var tag = await this.client.PostTagByProjectAsync(projectId, new TagRequest { Name = "kernel", Description = "The KerML model of step 4", TaggedCommit = committed.Commit.Id });
            this.state.TagIds["kernel"] = tag.Id;

            var tagById = await this.client.GetTagByProjectAndIdAsync(projectId, tag.Id);
            var (tags, _) = await ReadBackAssertions.ReadAllPagesAsync(parameters => this.client.GetTagsByProjectAsync(projectId, parameters));

            using (Assert.EnterMultipleScope())
            {
                Assert.That(tag.Name, Is.EqualTo("kernel"));
                Assert.That(tag.TaggedCommit, Is.EqualTo(committed.Commit.Id));
                Assert.That(tag.OwningProject, Is.EqualTo(projectId));
                Assert.That(tagById.Id, Is.EqualTo(tag.Id));
                Assert.That(tagById.TaggedCommit, Is.EqualTo(committed.Commit.Id));
                Assert.That(tags.Select(record => record.Id), Is.EqualTo((Guid[])[tag.Id]));

                await this.ExpectNotFoundAsync(() => this.client.GetTagByProjectAndIdAsync(projectId, Guid.NewGuid()));
            }

            await this.VerifyCommittedModelAsync(committed, this.kernelModel.Class.Id);
        }

        [Test]
        [DependsOnTest(nameof(VerifyKerMLKernel), AllowFailure = true)]
        public async Task VerifyKerMLUpdate()
        {
            if (this.kernelModel == null || !this.state.TagIds.ContainsKey("kernel"))
            {
                Assert.Ignore("step 4 built no tagged model to update");
            }

            var projectId = this.state.ProjectId!.Value;
            var kernelTag = await this.client.GetTagByProjectAndIdAsync(projectId, this.state.TagIds["kernel"]);

            this.kermlUpdateModel = new KerMLUpdateModel(this.rootModel, this.coreModel, this.kernelModel);

            var committed = await this.CommitModelAsync(this.kermlUpdateModel.Builder, "KerML update");

            await this.readBack.VerifyDiffAsync(kernelTag.TaggedCommit, committed.Commit.Id, committed.Changed);
            await this.VerifyCommittedModelAsync(committed, this.coreModel.A.Id);
        }

        [Test]
        [DependsOnTest(nameof(VerifyKerMLUpdate), AllowFailure = true)]
        public async Task VerifyCoverage()
        {
            var verdict = new CoverageVerdict(this.state.ReadBack, this.httpClientFactory.Recorder.Calls, this.openApi);

            await TestContext.Out.WriteLineAsync($"Missing metaclasses ({verdict.MissingMetaclasses.Count}): {string.Join(", ", verdict.MissingMetaclasses)}");
            await TestContext.Out.WriteLineAsync($"Unset properties ({verdict.UnsetProperties.Count}): {string.Join(", ", verdict.UnsetProperties)}");
            await TestContext.Out.WriteLineAsync($"Unchanged properties ({verdict.UnchangedProperties.Count}): {string.Join(", ", verdict.UnchangedProperties)}");
            await TestContext.Out.WriteLineAsync($"Missing literals ({verdict.MissingLiterals.Count}): {string.Join(", ", verdict.MissingLiterals)}");
            await TestContext.Out.WriteLineAsync($"Uncovered operations ({verdict.UncoveredOperations.Count}): {string.Join(", ", verdict.UncoveredOperations)}");
            await TestContext.Out.WriteLineAsync($"Unmatched requests ({verdict.UnmatchedRequests.Count}): {string.Join(", ", verdict.UnmatchedRequests)}");
            await TestContext.Out.WriteLineAsync($"{this.httpClientFactory.Recorder.Calls.Count} calls: {this.httpClientFactory.Recorder.ServerTime} waiting for the server, {this.httpClientFactory.Recorder.CheckTime} checking against the OpenAPI document");

            using (Assert.EnterMultipleScope())
            {
                Assert.That(verdict.MissingMetaclasses, Is.Empty, "metaclasses of which the server returned no instance");
                Assert.That(verdict.UnsetProperties, Is.Empty, "properties never returned with a non-default value");
                Assert.That(verdict.UnchangedProperties, Is.Empty, "properties never changed by an update");
                Assert.That(verdict.MissingLiterals, Is.Empty, "enumeration literals never returned");
                Assert.That(verdict.UncoveredOperations, Is.Empty, "operations not called with a success, or with a client error where the OpenAPI document declares one");
                Assert.That(verdict.UnmatchedRequests, Is.Empty, "requests that address no operation of the OpenAPI document");
                Assert.That(this.httpClientFactory.Recorder.EmptyGuidRequests, Is.Empty, "requests that referenced the empty GUID");
            }
        }

        private static async Task ExpectClientErrorAsync(Func<Task> call, [CallerArgumentExpression(nameof(call))] string description = null)
        {
            await Assert.ThatAsync(() => call(), Throws.InstanceOf<PsmApiException>().With.Property(nameof(PsmApiException.StatusCode)).Matches<HttpStatusCode>(status => (int)status is >= 400 and < 500), description);
        }

        private async Task<(Commit Commit, IReadOnlyList<IElement> Model, IReadOnlyList<IElement> Changed, Guid? PreviousHead)> CommitModelAsync(ModelBuilder builder, string description)
        {
            var previousHead = this.state.HeadCommitId;
            IReadOnlyList<IElement> previous = previousHead is { } head ? this.state.ExpectedModels[head] : [];
            var model = builder.ToDtos();
            var changed = ModelBuilder.Changed(previous, model);

            var commit = await this.client.PostCommitByProjectAsync(this.state.ProjectId!.Value, ModelBuilder.ToCommitRequest(changed, description), this.state.DefaultBranchId);

            this.state.HeadCommitId = commit.Id;
            this.state.ExpectedModels[commit.Id] = model;

            return (commit, model, changed, previousHead);
        }

        private Task ExpectNotFoundAsync(Func<Task> call, ServerDivergence tolerated = null, [CallerArgumentExpression(nameof(call))] string description = null)
        {
            return KnownServerDivergences.ExpectNotFoundAsync(call, () => this.httpClientFactory.Recorder.LastCall, tolerated, description);
        }

        private async Task VerifyCommittedModelAsync((Commit Commit, IReadOnlyList<IElement> Model, IReadOnlyList<IElement> Changed, Guid? PreviousHead) committed, Guid relationshipSubjectId)
        {
            using (Assert.EnterMultipleScope())
            {
                Assert.That(this.httpClientFactory.Recorder.EmptyGuidRequests, Is.Empty, "a request referenced the empty GUID");

                await this.readBack.VerifyCommitAsync(committed.Commit, this.state.DefaultBranchId!.Value, committed.PreviousHead, committed.Changed);
                await this.readBack.VerifyModelAsync(committed.Commit.Id, committed.Model, committed.Changed, relationshipSubjectId);

                if (committed.PreviousHead is { } previousHead)
                {
                    var previous = this.state.ExpectedModels[previousHead];
                    var changedIds = committed.Changed.Select(element => element.Id).ToHashSet();

                    await this.readBack.VerifyHistoryAsync(previousHead, previous.FirstOrDefault(element => changedIds.Contains(element.Id)) ?? previous[0]);
                }
            }
        }
    }
}
