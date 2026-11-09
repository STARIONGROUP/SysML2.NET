// ------------------------------------------------------------------------------------------------
// <copyright file="ReadBackAssertionsTestFixture.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SySML2.NET.REST.Tests.HelpersTests
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Reflection;
    using System.Threading;
    using System.Threading.Tasks;

    using Moq;

    using NUnit.Framework;
    using NUnit.Framework.Interfaces;

    using SySML2.NET.REST;
    using SySML2.NET.REST.Tests.Helpers;
    using SySML2.NET.REST.Tests.Helpers.Models;

    using SysML2.NET.Common;
    using SysML2.NET.Core.DTO.Kernel.Packages;
    using SysML2.NET.Core.DTO.Root.Dependencies;
    using SysML2.NET.Core.DTO.Root.Elements;
    using SysML2.NET.Core.DTO.Root.Namespaces;
    using SysML2.NET.PSM;
    using SysML2.NET.PSM.DTO;
    using SysML2.NET.PSM.Enumerations;
    using SysML2.NET.REST.Exceptions;

    using Direction = SysML2.NET.PSM.Enumerations.Direction;

    [TestFixture]
    public class ReadBackAssertionsTestFixture
    {
        private static readonly MethodInfo CloneMethod = typeof(object).GetMethod("MemberwiseClone", BindingFlags.Instance | BindingFlags.NonPublic);

        private readonly Guid projectId = Guid.NewGuid();

        private readonly Guid commitId = Guid.NewGuid();

        private Mock<IPsmApiClient> client;

        private ScenarioState state;

        private ReadBackAssertions assertions;

        [SetUp]
        public void SetUp()
        {
            this.client = new Mock<IPsmApiClient>();
            this.state = new ScenarioState { ProjectId = this.projectId };
            this.assertions = new ReadBackAssertions(this.client.Object, this.state);
        }

        [Test]
        public async Task VerifyReadAllPagesAsync()
        {
            var requests = new List<QueryParameters>();

            var (records, pages) = await ReadBackAssertions.ReadAllPagesAsync(parameters =>
            {
                requests.Add(parameters);
                var index = requests.Count;

                return Task.FromResult(new PagedResult<int>
                {
                    Records = [index * 10, (index * 10) + 1],
                    Next = index < 3 ? new Page($"cursor-{index}") : null
                });
            });

            var (single, singlePages) = await ReadBackAssertions.ReadAllPagesAsync(_ => Task.FromResult(new PagedResult<int> { Records = [7] }));

            using (Assert.EnterMultipleScope())
            {
                Assert.That(records, Is.EqualTo((int[])[10, 11, 20, 21, 30, 31]));
                Assert.That(pages, Is.EqualTo(3));
                Assert.That(requests[0].PageAfter, Is.Null);
                Assert.That(requests[1].PageAfter?.Token, Is.EqualTo("cursor-1"));
                Assert.That(requests[2].PageAfter?.Token, Is.EqualTo("cursor-2"));
                Assert.That(requests.TrueForAll(parameters => parameters.PageSize == 100), Is.True);
                Assert.That(single, Is.EqualTo((int[])[7]));
                Assert.That(singlePages, Is.EqualTo(1));
            }

            var counter = 0;

            await Assert.ThatAsync(() => ReadBackAssertions.ReadAllPagesAsync(_ => Task.FromResult(new PagedResult<int> { Records = [], Next = new Page("same") })), Throws.InvalidOperationException.With.Message.Contains("repeated"));
            await Assert.ThatAsync(() => ReadBackAssertions.ReadAllPagesAsync(_ => Task.FromResult(new PagedResult<int> { Records = [], Next = new Page($"cursor-{++counter}") })), Throws.InvalidOperationException.With.Message.Contains("did not end"));
        }

        [Test]
        public async Task VerifyVerifyCascadeAsync()
        {
            var kept = new Package { Id = Guid.NewGuid() };
            var inner = new Package { Id = Guid.NewGuid() };
            var note = new Package { Id = Guid.NewGuid() };
            IReadOnlyList<IElement> listed = [kept];
            var readable = new HashSet<Guid>();
            var readFailures = new Dictionary<Guid, Exception>();
            var unreadable = false;

            this.client.Setup(api => api.GetElementsByProjectCommitAsync(this.projectId, this.commitId, It.IsAny<bool?>(), It.IsAny<QueryParameters>(), It.IsAny<CancellationToken>()))
                .Returns((Guid _, Guid _, bool? _, QueryParameters parameters, CancellationToken _) => unreadable ? throw new InvalidOperationException("no list") : Paged(listed, parameters));
            this.client.Setup(api => api.GetElementByProjectCommitIdAsync(this.projectId, this.commitId, It.IsAny<Guid>(), It.IsAny<bool?>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((Guid _, Guid _, Guid elementId, bool? _, CancellationToken _) => readFailures.TryGetValue(elementId, out var readFailure) ? throw readFailure : readable.Contains(elementId) ? new Package { Id = elementId } : throw new PsmApiException(HttpStatusCode.NotFound, null));

            var cascaded = await IsolatedAssertions.RunAsync(() => this.assertions.VerifyCascadeAsync(this.commitId, [inner, note], [kept]));

            readable.Add(note.Id);

            var stillReadable = await IsolatedAssertions.RunAsync(() => this.assertions.VerifyCascadeAsync(this.commitId, [inner, note], [kept]));

            readable.Clear();
            readFailures[note.Id] = new PsmApiException(HttpStatusCode.InternalServerError, null);

            var serverError = await IsolatedAssertions.RunAsync(() => this.assertions.VerifyCascadeAsync(this.commitId, [inner, note], [kept]));

            readFailures[note.Id] = new InvalidOperationException("unreadable");

            var clientError = await IsolatedAssertions.RunAsync(() => this.assertions.VerifyCascadeAsync(this.commitId, [inner, note], [kept]));

            readFailures.Clear();
            listed = [kept, inner, note];

            var notCascaded = await IsolatedAssertions.RunAsync(() => this.assertions.VerifyCascadeAsync(this.commitId, [inner, note], [kept]));

            listed = [kept, inner];

            var partly = await IsolatedAssertions.RunAsync(() => this.assertions.VerifyCascadeAsync(this.commitId, [inner, note], [kept]));

            listed = [inner, note];

            var keptLost = await IsolatedAssertions.RunAsync(() => this.assertions.VerifyCascadeAsync(this.commitId, [inner, note], [kept]));

            unreadable = true;

            var failed = await IsolatedAssertions.RunAsync(() => this.assertions.VerifyCascadeAsync(this.commitId, [inner, note], [kept]));

            using (Assert.EnterMultipleScope())
            {
                Assert.That(cascaded, Is.Empty);
                Assert.That(stillReadable, Has.Count.EqualTo(1));
                Assert.That(stillReadable[0].Message, Does.Contain($"reading Package {note.Id}, owned by the deleted element"));
                Assert.That(serverError.Select(result => result.Status), Is.EqualTo((AssertionStatus[])[AssertionStatus.Failed]), "a 500 fails without ending the check");
                Assert.That(serverError[0].Message, Does.Contain($"reading Package {note.Id}, owned by the deleted element"));
                Assert.That(clientError.Select(result => result.Message), Is.EqualTo((string[])[$"reading Package {note.Id}, owned by the deleted element threw InvalidOperationException: unreadable"]));
                Assert.That(notCascaded.Select(result => result.Status), Is.EqualTo((AssertionStatus[])[AssertionStatus.Warning]));
                Assert.That(notCascaded[0].Message, Does.Contain(KnownServerDivergences.DeleteDoesNotCascade.Summary));
                Assert.That(partly.Select(result => result.Status), Has.Some.EqualTo(AssertionStatus.Failed), "a partial cascade is no known divergence");
                Assert.That(keptLost.Select(result => result.Status), Has.Some.EqualTo(AssertionStatus.Failed), "an element that was not deleted must stay");
                Assert.That(failed.Select(result => result.Message), Is.EqualTo((string[])["GetElementsByProjectCommitAsync after the cascading delete threw InvalidOperationException: no list"]));
            }
        }

        [Test]
        public async Task VerifyVerifyCommitAsync()
        {
            var branchId = Guid.NewGuid();
            var previousId = Guid.NewGuid();

            List<IElement> changed = [.. Enumerable.Range(0, 150).Select(index => new Package { Id = Guid.NewGuid(), DeclaredName = $"P{index}" })];
            var changes = changed.Select(element => new DataVersion { Id = Guid.NewGuid(), Identity = new DataIdentity { Id = element.Id }, Payload = Clone(element) }).ToList();
            var commit = new Commit { Id = this.commitId, OwningProject = this.projectId };
            List<Commit> commits = [commit];
            DataVersion changeById = null;
            Guid? head = this.commitId;
            var changesUnreadable = false;
            var changesOnOnePage = false;

            var commitUnreadable = false;

            var recorded = new List<EndpointCall>();
            EndpointCall commitResponse = null;

            this.client.Setup(api => api.GetCommitByProjectAndIdAsync(this.projectId, this.commitId, It.IsAny<CancellationToken>())).ReturnsAsync(() =>
            {
                if (commitResponse != null)
                {
                    recorded.Add(commitResponse);
                }

                return commitUnreadable ? throw new InvalidOperationException("previousCommit is an object") : commit;
            });
            this.client.Setup(api => api.GetCommitsByProjectAsync(this.projectId, It.IsAny<QueryParameters>(), It.IsAny<CancellationToken>())).Returns((Guid _, QueryParameters parameters, CancellationToken _) => Paged(commits, parameters));
            this.client.Setup(api => api.GetBranchesByProjectAndIdAsync(this.projectId, branchId, It.IsAny<CancellationToken>())).ReturnsAsync(() => new Branch { Id = branchId, Head = head });
            this.client.Setup(api => api.GetChangesByProjectCommitAsync(this.projectId, this.commitId, It.IsAny<QueryParameters>(), It.IsAny<CancellationToken>())).Returns((Guid _, Guid _, QueryParameters parameters, CancellationToken _) => changesUnreadable ? throw new InvalidOperationException("no changes") : Paged(changes, changesOnOnePage ? null : parameters));
            this.client.Setup(api => api.GetChangeByProjectCommitIdAsync(this.projectId, this.commitId, It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync((Guid _, Guid _, Guid changeId, CancellationToken _) => changeById ?? changes.Single(change => change.Id == changeId));

            var firstCommit = await IsolatedAssertions.RunAsync(() => this.assertions.VerifyCommitAsync(commit, branchId, null, changed));

            this.client.Verify(api => api.GetChangeByProjectCommitIdAsync(this.projectId, this.commitId, changes[0].Id, It.IsAny<CancellationToken>()), Times.Once);

            var unexpectedPrevious = await IsolatedAssertions.RunAsync(() => this.assertions.VerifyCommitAsync(commit, branchId, previousId, changed));

            commit.PreviousCommit = [previousId];

            var laterCommit = await IsolatedAssertions.RunAsync(() => this.assertions.VerifyCommitAsync(commit, branchId, previousId, changed));

            head = previousId;

            var headNotMoved = await IsolatedAssertions.RunAsync(() => this.assertions.VerifyCommitAsync(commit, branchId, previousId, changed));

            head = this.commitId;
            commits = [];

            var notListed = await IsolatedAssertions.RunAsync(() => this.assertions.VerifyCommitAsync(commit, branchId, previousId, changed));

            commits = [commit];
            commit.OwningProject = Guid.NewGuid();

            var otherProject = await IsolatedAssertions.RunAsync(() => this.assertions.VerifyCommitAsync(commit, branchId, previousId, changed));

            commit.OwningProject = this.projectId;
            var renamed = (Package)Clone(changed[3]);
            renamed.DeclaredName = "Changed";
            changes[3].Payload = renamed;
            var renamedChangeId = changes[3].Id;

            var payloadChanged = await IsolatedAssertions.RunAsync(() => this.assertions.VerifyCommitAsync(commit, branchId, previousId, changed));

            changes[3].Payload = Clone(changed[3]);
            changeById = new DataVersion { Id = changes[0].Id, Identity = new DataIdentity { Id = Guid.NewGuid() }, Payload = changes[0].Payload };

            var changeByIdOther = await IsolatedAssertions.RunAsync(() => this.assertions.VerifyCommitAsync(commit, branchId, previousId, changed));

            changeById = null;
            changesOnOnePage = true;

            var onePage = await IsolatedAssertions.RunAsync(() => this.assertions.VerifyCommitAsync(commit, branchId, previousId, changed));

            changesOnOnePage = false;
            changes = changes[..5];

            var smallCommit = await IsolatedAssertions.RunAsync(() => this.assertions.VerifyCommitAsync(commit, branchId, previousId, changed[..5]));
            var missingChanges = await IsolatedAssertions.RunAsync(() => this.assertions.VerifyCommitAsync(commit, branchId, previousId, changed));

            changes = [];
            this.client.Invocations.Clear();

            var noChanges = await IsolatedAssertions.RunAsync(() => this.assertions.VerifyCommitAsync(commit, branchId, previousId, []));

            changesUnreadable = true;

            var unreadableChanges = await IsolatedAssertions.RunAsync(() => this.assertions.VerifyCommitAsync(commit, branchId, previousId, []));

            changesUnreadable = false;

            commitUnreadable = true;

            var unreadableCommit = await IsolatedAssertions.RunAsync(() => this.assertions.VerifyCommitAsync(commit, branchId, previousId, []));

            var recording = new ReadBackAssertions(this.client.Object, this.state, () => recorded);
            var commitCall = new EndpointCall(null, "GET /projects/p/commits/c", HttpStatusCode.OK);

            commitResponse = commitCall with { ResponseViolations = [new ContractViolation(ContractViolationKind.SchemaMismatch, "/previousCommit type: Value is \"object\" but should be \"array\"")] };

            var unreadableOnBreakingResponse = await IsolatedAssertions.RunAsync(() => recording.VerifyCommitAsync(commit, branchId, previousId, []));

            commitResponse = commitCall with { ResponseViolations = [new ContractViolation(ContractViolationKind.SchemaMismatch, "/ required: Required properties [\"alias\"] are not present")] };

            var unreadableOnToleratedResponse = await IsolatedAssertions.RunAsync(() => recording.VerifyCommitAsync(commit, branchId, previousId, []));

            commitResponse = commitCall;

            var unreadableOnConformingResponse = await IsolatedAssertions.RunAsync(() => recording.VerifyCommitAsync(commit, branchId, previousId, []));

            commitResponse = null;
            recorded.Add(commitCall with { ResponseViolations = [new ContractViolation(ContractViolationKind.SchemaMismatch, "/previousCommit type: Value is \"object\" but should be \"array\"")] });

            var unreadableAfterEarlierBreakingResponse = await IsolatedAssertions.RunAsync(() => recording.VerifyCommitAsync(commit, branchId, previousId, []));

            using (Assert.EnterMultipleScope())
            {
                Assert.That(firstCommit, Is.Empty);
                Assert.That(unexpectedPrevious, Has.Count.EqualTo(1));
                Assert.That(laterCommit, Is.Empty);
                Assert.That(headNotMoved, Has.Count.EqualTo(1));
                Assert.That(notListed, Has.Count.EqualTo(1));
                Assert.That(otherProject, Has.Count.EqualTo(1));
                Assert.That(payloadChanged, Has.Count.EqualTo(1));
                Assert.That(payloadChanged[0].Message, Does.Contain($"the payload of change {renamedChangeId} differs from what was sent: Element.DeclaredName"));
                Assert.That(changeByIdOther, Has.Count.EqualTo(1));
                Assert.That(onePage, Has.Count.EqualTo(1));
                Assert.That(onePage[0].Message, Does.Contain("paging is untested"));
                Assert.That(smallCommit, Is.Empty, "a commit that fits on one page does not test paging, and need not");
                Assert.That(missingChanges, Has.Count.EqualTo(2));
                Assert.That(noChanges, Is.Empty);
                Assert.That(unreadableChanges.Select(result => result.Message), Is.EqualTo((string[])["GetChangesByProjectCommitAsync threw InvalidOperationException: no changes"]), "the commit, the list and the branch head still pass");
                Assert.That(unreadableCommit.Select(result => result.Message), Has.Some.Contains("GetCommitByProjectAndIdAsync threw InvalidOperationException: previousCommit is an object"));
                Assert.That(unreadableCommit, Has.Count.EqualTo(1), "the commit read fails, and the other commit checks still run");
                Assert.That(unreadableOnBreakingResponse.Select(result => result.Status), Is.EquivalentTo((AssertionStatus[])[AssertionStatus.Warning]));
                Assert.That(unreadableOnBreakingResponse.Single(result => result.Status == AssertionStatus.Warning).Message, Does.Contain(KnownServerDivergences.PreviousCommitAsObject.Summary).And.Contain("(GET /projects/p/commits/c)"));
                Assert.That(unreadableOnToleratedResponse.Select(result => result.Status), Is.EqualTo((AssertionStatus[])[AssertionStatus.Failed]), "a tolerated divergence that does not break deserialization excuses nothing");
                Assert.That(unreadableOnConformingResponse.Select(result => result.Status), Is.EqualTo((AssertionStatus[])[AssertionStatus.Failed]));
                Assert.That(unreadableAfterEarlierBreakingResponse.Select(result => result.Status), Is.EqualTo((AssertionStatus[])[AssertionStatus.Failed]), "a breaking response of an earlier call excuses nothing");
            }

            this.client.Verify(api => api.GetChangeByProjectCommitIdAsync(It.IsAny<Guid>(), It.IsAny<Guid>(), It.IsAny<Guid>(), It.IsAny<CancellationToken>()), Times.Never);
        }

        [Test]
        public async Task VerifyVerifyDataCommitAsync()
        {
            var branchId = Guid.NewGuid();
            var previousId = Guid.NewGuid();
            var commit = new Commit { Id = this.commitId, OwningProject = this.projectId, PreviousCommit = [previousId] };
            var data = new ExternalData { Id = Guid.NewGuid(), ResourceIdentifier = new Uri("https://example.org/r") };
            var usage = new ProjectUsage { Id = Guid.NewGuid(), UsedProject = Guid.NewGuid(), UsedCommit = Guid.NewGuid() };
            var package = new Package { Id = Guid.NewGuid(), DeclaredName = "P" };
            var deletedId = Guid.NewGuid();
            Guid? head = this.commitId;
            var changesUnreadable = false;

            List<DataVersion> Changes(IData dataPayload, IData usagePayload, IData deletedPayload) =>
            [
                new() { Id = Guid.NewGuid(), Identity = new DataIdentity { Id = data.Id }, Payload = dataPayload },
                new() { Id = Guid.NewGuid(), Identity = new DataIdentity { Id = usage.Id }, Payload = usagePayload },
                new() { Id = Guid.NewGuid(), Identity = new DataIdentity { Id = package.Id }, Payload = Clone(package) },
                new() { Id = Guid.NewGuid(), Identity = new DataIdentity { Id = deletedId }, Payload = deletedPayload }
            ];

            var changes = Changes(new ExternalData { Id = data.Id, ResourceIdentifier = new Uri("https://example.org/r") }, new ProjectUsage { Id = usage.Id, UsedProject = usage.UsedProject, UsedCommit = usage.UsedCommit }, null);

            this.client.Setup(api => api.GetCommitByProjectAndIdAsync(this.projectId, this.commitId, It.IsAny<CancellationToken>())).ReturnsAsync(() => commit);
            this.client.Setup(api => api.GetCommitsByProjectAsync(this.projectId, It.IsAny<QueryParameters>(), It.IsAny<CancellationToken>())).Returns((Guid _, QueryParameters parameters, CancellationToken _) => Paged([commit], parameters));
            this.client.Setup(api => api.GetBranchesByProjectAndIdAsync(this.projectId, branchId, It.IsAny<CancellationToken>())).ReturnsAsync(() => new Branch { Id = branchId, Head = head });
            this.client.Setup(api => api.GetChangesByProjectCommitAsync(this.projectId, this.commitId, It.IsAny<QueryParameters>(), It.IsAny<CancellationToken>())).Returns((Guid _, Guid _, QueryParameters parameters, CancellationToken _) => changesUnreadable ? throw new InvalidOperationException("no changes") : Paged(changes, parameters));

            List<(Guid Id, IData Payload)> expected = [(data.Id, data), (usage.Id, usage), (package.Id, package), (deletedId, null)];

            var matching = await IsolatedAssertions.RunAsync(() => this.assertions.VerifyDataCommitAsync(commit, branchId, previousId, expected));

            changes = Changes(new ExternalData { Id = data.Id, ResourceIdentifier = new Uri("https://example.org/other") }, usage, null);

            var dataChanged = await IsolatedAssertions.RunAsync(() => this.assertions.VerifyDataCommitAsync(commit, branchId, previousId, expected));

            changes = Changes(data, new ExternalData { Id = usage.Id }, null);

            var otherKind = await IsolatedAssertions.RunAsync(() => this.assertions.VerifyDataCommitAsync(commit, branchId, previousId, expected));

            changes = Changes(data, usage, new ExternalData { Id = deletedId });

            var notDeleted = await IsolatedAssertions.RunAsync(() => this.assertions.VerifyDataCommitAsync(commit, branchId, previousId, expected));

            changes = Changes(null, usage, null);

            var deletedInstead = await IsolatedAssertions.RunAsync(() => this.assertions.VerifyDataCommitAsync(commit, branchId, previousId, expected));

            changes = Changes(data, usage, null)[..3];

            var changeMissing = await IsolatedAssertions.RunAsync(() => this.assertions.VerifyDataCommitAsync(commit, branchId, previousId, expected));

            var cascadedId = Guid.NewGuid();
            changes = [.. Changes(data, usage, null), new DataVersion { Id = Guid.NewGuid(), Identity = new DataIdentity { Id = cascadedId } }];

            var cascadeDeleted = await IsolatedAssertions.RunAsync(() => this.assertions.VerifyDataCommitAsync(commit, branchId, previousId, expected, [cascadedId]));

            changes[^1].Payload = new Package { Id = cascadedId };

            var cascadeEdited = await IsolatedAssertions.RunAsync(() => this.assertions.VerifyDataCommitAsync(commit, branchId, previousId, expected, [cascadedId]));
            var notCascaded = await IsolatedAssertions.RunAsync(() => this.assertions.VerifyDataCommitAsync(commit, branchId, previousId, expected));

            changes = Changes(data, usage, null);
            head = previousId;

            var headNotMoved = await IsolatedAssertions.RunAsync(() => this.assertions.VerifyDataCommitAsync(commit, branchId, previousId, expected));

            head = this.commitId;
            changesUnreadable = true;

            var unreadable = await IsolatedAssertions.RunAsync(() => this.assertions.VerifyDataCommitAsync(commit, branchId, previousId, expected));

            using (Assert.EnterMultipleScope())
            {
                Assert.That(matching, Is.Empty);
                Assert.That(dataChanged.Select(result => result.Message), Has.Some.Contains($"the payload of the change of ExternalData {data.Id} differs from what was sent").And.Some.Contains("ResourceIdentifier"));
                Assert.That(otherKind.Select(result => result.Message), Has.Some.Contains($"the payload of the change of ProjectUsage {usage.Id} was read back as another kind of data"));
                Assert.That(notDeleted.Select(result => result.Message), Has.Some.Contains($"the change of {deletedId} deletes it"));
                Assert.That(deletedInstead, Has.Count.EqualTo(1), "a server that deletes what was written fails");
                Assert.That(deletedInstead[0].Message, Does.Contain($"the payload of the change of ExternalData {data.Id} was read back as another kind of data"));
                Assert.That(changeMissing.Select(result => result.Message), Has.Some.Contains("one change per data item"));
                Assert.That(changeMissing, Has.Count.EqualTo(1));
                Assert.That(cascadeDeleted, Is.Empty, "an element deleted with its owner may be listed as deleted");
                Assert.That(cascadeEdited.Select(result => result.Status), Is.EqualTo((AssertionStatus[])[AssertionStatus.Warning]));
                Assert.That(cascadeEdited[0].Message, Does.Contain(KnownServerDivergences.DeleteDoesNotCascade.Summary));
                Assert.That(notCascaded.Select(result => result.Message), Has.Some.Contains("one change per data item"), "a change of an element the commit does not delete");
                Assert.That(headNotMoved, Has.Count.EqualTo(1));
                Assert.That(unreadable.Select(result => result.Message), Is.EqualTo((string[])["GetChangesByProjectCommitAsync threw InvalidOperationException: no changes"]), "the commit record still passes");
            }
        }

        [Test]
        public async Task VerifyVerifyDiffAsync()
        {
            var baseCommitId = Guid.NewGuid();
            var compareCommitId = Guid.NewGuid();
            var updated = new Package { Id = Guid.NewGuid() };
            var created = new Package { Id = Guid.NewGuid() };
            this.state.ExpectedModels[baseCommitId] = [updated];

            static DataVersion Version(IElement element) => new() { Id = Guid.NewGuid(), Identity = new DataIdentity { Id = element.Id }, Payload = element };

            List<DataDifference> differences = [new DataDifference { BaseData = Version(updated), CompareData = Version(updated) }, new DataDifference { CompareData = Version(created) }];
            Exception failure = null;

            this.client.Setup(api => api.DiffAsync(this.projectId, baseCommitId, compareCommitId, It.IsAny<QueryParameters>(), It.IsAny<CancellationToken>()))
                .Returns((Guid _, Guid _, Guid _, QueryParameters parameters, CancellationToken _) => failure != null ? throw failure : Paged(differences, parameters));

            var expected = await IsolatedAssertions.RunAsync(() => this.assertions.VerifyDiffAsync(baseCommitId, compareCommitId, [updated, created]));

            differences = [new DataDifference { BaseData = Version(updated), CompareData = Version(updated) }];

            var missing = await IsolatedAssertions.RunAsync(() => this.assertions.VerifyDiffAsync(baseCommitId, compareCommitId, [updated, created]));

            differences = [new DataDifference { CompareData = Version(updated) }, new DataDifference { BaseData = Version(created), CompareData = Version(created) }];

            var misclassified = await IsolatedAssertions.RunAsync(() => this.assertions.VerifyDiffAsync(baseCommitId, compareCommitId, [updated, created]));

            differences = [new DataDifference { BaseData = Version(updated), CompareData = Version(updated) }, new DataDifference { BaseData = Version(created) }, new DataDifference()];

            var deletedAndUnnamed = await IsolatedAssertions.RunAsync(() => this.assertions.VerifyDiffAsync(baseCommitId, compareCommitId, [updated, created]));

            var removed = new Package { Id = Guid.NewGuid() };
            differences = [new DataDifference { BaseData = Version(updated), CompareData = Version(updated) }, new DataDifference { BaseData = Version(removed) }];

            var deletion = await IsolatedAssertions.RunAsync(() => this.assertions.VerifyDiffAsync(baseCommitId, compareCommitId, [updated], [removed.Id]));

            differences = [new DataDifference { BaseData = Version(updated), CompareData = Version(updated) }, new DataDifference { BaseData = Version(removed), CompareData = Version(removed) }];

            var deletionAsUpdate = await IsolatedAssertions.RunAsync(() => this.assertions.VerifyDiffAsync(baseCommitId, compareCommitId, [updated], [removed.Id]));

            differences = [new DataDifference { BaseData = Version(updated), CompareData = Version(updated) }];

            var deletionMissing = await IsolatedAssertions.RunAsync(() => this.assertions.VerifyDiffAsync(baseCommitId, compareCommitId, [updated], [removed.Id]));

            var owned = new Package { Id = Guid.NewGuid() };
            var ownedMembership = new Package { Id = Guid.NewGuid() };
            this.state.ExpectedModels[baseCommitId] = [updated, removed, owned, ownedMembership];
            differences = [new DataDifference { BaseData = Version(updated), CompareData = Version(updated) }, new DataDifference { BaseData = Version(removed) }, new DataDifference { BaseData = Version(owned) }, new DataDifference { BaseData = Version(ownedMembership) }];

            var cascade = await IsolatedAssertions.RunAsync(() => this.assertions.VerifyDiffAsync(baseCommitId, compareCommitId, [updated], [removed.Id], [owned.Id, ownedMembership.Id]));

            differences = [new DataDifference { BaseData = Version(updated), CompareData = Version(updated) }, new DataDifference { BaseData = Version(removed) }, new DataDifference { BaseData = Version(ownedMembership), CompareData = Version(ownedMembership) }];

            var notCascaded = await IsolatedAssertions.RunAsync(() => this.assertions.VerifyDiffAsync(baseCommitId, compareCommitId, [updated], [removed.Id], [owned.Id, ownedMembership.Id]));

            differences = [new DataDifference { BaseData = Version(updated), CompareData = Version(updated) }, new DataDifference { BaseData = Version(removed) }, new DataDifference { BaseData = Version(owned) }];

            var partlyCascaded = await IsolatedAssertions.RunAsync(() => this.assertions.VerifyDiffAsync(baseCommitId, compareCommitId, [updated], [removed.Id], [owned.Id, ownedMembership.Id]));

            failure = new PsmApiException(HttpStatusCode.NotImplemented, null);

            var notImplemented = await IsolatedAssertions.RunAsync(() => this.assertions.VerifyDiffAsync(baseCommitId, compareCommitId, [updated, created]));

            failure = new PsmApiException(HttpStatusCode.BadRequest, null);

            var rejected = await IsolatedAssertions.RunAsync(() => this.assertions.VerifyDiffAsync(baseCommitId, compareCommitId, [updated, created]));

            using (Assert.EnterMultipleScope())
            {
                Assert.That(expected, Is.Empty);
                Assert.That(missing, Has.Count.EqualTo(1));
                Assert.That(misclassified.Select(result => result.Message), Has.Some.Contains("UPDATED"));
                Assert.That(misclassified.Select(result => result.Message), Has.Some.Contains("CREATED"));
                Assert.That(deletedAndUnnamed.Select(result => result.Message), Has.Some.Contains("a difference names the data it is about"));
                Assert.That(deletion, Is.Empty);
                Assert.That(deletionAsUpdate.Select(result => result.Message), Has.Some.Contains("a deleted element is DELETED"));
                Assert.That(deletionMissing, Has.Count.EqualTo(1));
                Assert.That(cascade, Is.Empty);
                Assert.That(notCascaded.Select(result => result.Status), Is.EqualTo((AssertionStatus[])[AssertionStatus.Warning]));
                Assert.That(notCascaded[0].Message, Does.Contain(KnownServerDivergences.DeleteDoesNotCascade.Summary));
                Assert.That(partlyCascaded.Select(result => result.Message), Has.Some.Contains("an element deleted with its owner is DELETED"));
                Assert.That(notImplemented.Select(result => result.Status), Is.EqualTo((AssertionStatus[])[AssertionStatus.Warning]));
                Assert.That(notImplemented[0].Message, Does.Contain(KnownServerDivergences.DiffNotImplemented.Summary));
                Assert.That(rejected.Select(result => result.Status), Is.EqualTo((AssertionStatus[])[AssertionStatus.Failed]));
                Assert.That(rejected[0].Message, Does.Contain("DiffAsync threw PsmApiException"));
            }
        }

        [Test]
        public async Task VerifyVerifyElementIdsAsync()
        {
            IReadOnlyList<IElement> listed = [new Package { Id = Guid.NewGuid() }, new Package { Id = Guid.NewGuid() }];
            var unreadable = false;

            Task<PagedResult<IElement>> ReadPage(QueryParameters parameters) => unreadable ? throw new InvalidOperationException("no list") : Paged(listed, parameters);

            var expectedIds = listed.Select(element => element.Id).ToList();

            var same = await IsolatedAssertions.RunAsync(() => this.assertions.VerifyElementIdsAsync("the list", ReadPage, expectedIds));
            var missing = await IsolatedAssertions.RunAsync(() => this.assertions.VerifyElementIdsAsync("the list", ReadPage, [.. expectedIds, Guid.NewGuid()]));

            unreadable = true;

            var failed = await IsolatedAssertions.RunAsync(() => this.assertions.VerifyElementIdsAsync("the list", ReadPage, expectedIds));

            using (Assert.EnterMultipleScope())
            {
                Assert.That(same, Is.Empty);
                Assert.That(missing, Has.Count.EqualTo(1));
                Assert.That(missing[0].Message, Does.StartWith("  the list"));
                Assert.That(failed.Select(result => result.Message), Is.EqualTo((string[])["the list threw InvalidOperationException: no list"]));
            }
        }

        [Test]
        public async Task VerifyVerifyHistoryAsync()
        {
            var earlierCommitId = Guid.NewGuid();
            var element = new Package { Id = Guid.NewGuid(), DeclaredName = "Before" };
            IElement atEarlierCommit = Clone(element);
            var unreadable = false;

            this.client.Setup(api => api.GetElementByProjectCommitIdAsync(this.projectId, earlierCommitId, element.Id, It.IsAny<bool?>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(() => unreadable ? throw new InvalidOperationException("gone") : atEarlierCommit);

            var unchanged = await IsolatedAssertions.RunAsync(() => this.assertions.VerifyHistoryAsync(earlierCommitId, element));

            var rewritten = (Package)Clone(element);
            rewritten.DeclaredName = "After";
            atEarlierCommit = rewritten;

            var changed = await IsolatedAssertions.RunAsync(() => this.assertions.VerifyHistoryAsync(earlierCommitId, element));

            unreadable = true;

            var failed = await IsolatedAssertions.RunAsync(() => this.assertions.VerifyHistoryAsync(earlierCommitId, element));

            using (Assert.EnterMultipleScope())
            {
                Assert.That(unchanged, Is.Empty);
                Assert.That(changed.Select(result => result.Message), Is.EqualTo((string[])[$"Package {element.Id} at the earlier commit {earlierCommitId} differs from what was sent: Element.DeclaredName"]));
                Assert.That(failed, Has.Count.EqualTo(1));
                Assert.That(failed[0].Message, Does.Contain("threw InvalidOperationException: gone"));
            }
        }

        [Test]
        public async Task VerifyVerifyModelAsync()
        {
            var model = new RootModel();
            var elements = model.Builder.ToDtos();
            List<IElement> changed = [.. elements.OrderBy(element => element.Id != model.Nested.Id)];
            var space = elements.Single(element => element.GetType() == typeof(Namespace));
            var relationships = elements.OfType<IRelationship>().ToList();
            var readBack = elements.ToDictionary(element => element.Id, Clone);
            var unowned = elements.Where(element => element.OwningRelationship == null).ToList();
            IReadOnlyList<IElement> roots = unowned;
            IReadOnlyList<IElement> listed = elements;
            var onePage = false;
            var unreadable = new HashSet<Guid>();
            var breakingFor = new HashSet<Guid>();
            var breaking = new ContractViolation(ContractViolationKind.SchemaMismatch, "/previousCommit type: Value is \"object\" but should be \"array\"");
            var recordedReads = new ConcurrentQueue<EndpointCall>();
            var inFlight = 0;
            var maximumInFlight = 0;
            var dropIncoming = false;
            var listUnreadable = false;
            Direction? unreadableDirection = null;

            this.client.Setup(api => api.GetElementByProjectCommitIdAsync(this.projectId, this.commitId, It.IsAny<Guid>(), It.IsAny<bool?>(), It.IsAny<CancellationToken>()))
                .Returns(async (Guid _, Guid _, Guid elementId, bool? _, CancellationToken _) =>
                {
                    var now = Interlocked.Increment(ref inFlight);
                    Interlocked.Exchange(ref maximumInFlight, Math.Max(Interlocked.CompareExchange(ref maximumInFlight, 0, 0), now));
                    await Task.Delay(5);
                    recordedReads.Enqueue(new EndpointCall(null, $"GET /elements/{elementId}", HttpStatusCode.OK) { ResponseViolations = breakingFor.Contains(elementId) ? [breaking] : [] });
                    Interlocked.Decrement(ref inFlight);

                    return unreadable.Contains(elementId) ? throw new InvalidOperationException("not deserializable") : readBack[elementId];
                });

            this.client.Setup(api => api.GetElementsByProjectCommitAsync(this.projectId, this.commitId, It.IsAny<bool?>(), It.IsAny<QueryParameters>(), It.IsAny<CancellationToken>()))
                .Returns((Guid _, Guid _, bool? _, QueryParameters parameters, CancellationToken _) => listUnreadable ? throw new InvalidOperationException("no list") : Paged(listed, onePage ? null : parameters));

            this.client.Setup(api => api.GetRootsByProjectCommitAsync(this.projectId, this.commitId, It.IsAny<bool?>(), It.IsAny<QueryParameters>(), It.IsAny<CancellationToken>()))
                .Returns((Guid _, Guid _, bool? _, QueryParameters parameters, CancellationToken _) => Paged(roots, parameters));

            this.client.Setup(api => api.GetRelationshipsByProjectCommitRelatedElementAsync(this.projectId, this.commitId, It.IsAny<Guid>(), It.IsAny<Direction?>(), It.IsAny<bool?>(), It.IsAny<QueryParameters>(), It.IsAny<CancellationToken>()))
                .Returns((Guid _, Guid _, Guid subjectId, Direction? direction, bool? _, QueryParameters parameters, CancellationToken _) => direction == unreadableDirection ? throw new InvalidOperationException("no relationships") :
                    Paged(relationships.Where(relationship => direction switch
                    {
                        Direction.@out => relationship.Source.Contains(subjectId),
                        Direction.@in => !dropIncoming && relationship.Target.Contains(subjectId),
                        _ => relationship.Source.Contains(subjectId) || relationship.Target.Contains(subjectId)
                    }).ToList(), parameters));

            var firstRead = elements.ToList();
            var identical = await IsolatedAssertions.RunAsync(this.VerifyModel(elements, changed, model.Nested.Id));
            var recorded = this.state.ReadBack.ToList();
            var readsById = this.client.Invocations.Count(invocation => invocation.Method.Name == nameof(IPsmApiClient.GetElementByProjectCommitIdAsync));
            var listPageSizes = this.client.Invocations.Where(invocation => invocation.Method.Name == nameof(IPsmApiClient.GetElementsByProjectCommitAsync)).Select(invocation => ((QueryParameters)invocation.Arguments[3]).PageSize).ToList();

            var larger = new RootModel();
            _ = new CoreModel(larger);
            var largerElements = larger.Builder.ToDtos();

            foreach (var element in largerElements.Where(element => !readBack.ContainsKey(element.Id)))
            {
                readBack[element.Id] = Clone(element);
            }

            listed = largerElements;
            roots = [.. largerElements.Where(element => element.OwningRelationship == null)];
            this.client.Invocations.Clear();

            var largerModel = await IsolatedAssertions.RunAsync(() => this.assertions.VerifyModelAsync(this.commitId, largerElements, [], larger.Nested.Id));
            var walkPages = this.client.Invocations.Count(invocation => invocation.Method.Name == nameof(IPsmApiClient.GetElementsByProjectCommitAsync) && ((QueryParameters)invocation.Arguments[3]).PageSize == 10);

            listed = elements;

            roots = elements;

            var everyElementRoot = await IsolatedAssertions.RunAsync(this.VerifyModel(elements, changed, model.Nested.Id));

            roots = [.. elements.Where(element => element is not IRelationship { OwningRelatedElement: not null })];

            var notOwnedRelatedRoots = await IsolatedAssertions.RunAsync(this.VerifyModel(elements, changed, model.Nested.Id));

            roots = [.. elements.Where(element => element is not IRelationship)];

            var nonRelationshipRoots = await IsolatedAssertions.RunAsync(this.VerifyModel(elements, changed, model.Nested.Id));

            roots = [.. unowned.Where(element => element.Id != model.Root.Id)];

            var rootMissing = await IsolatedAssertions.RunAsync(this.VerifyModel(elements, changed, model.Nested.Id));

            roots = [.. unowned, new Package { Id = Guid.NewGuid() }];

            var foreignRoot = await IsolatedAssertions.RunAsync(this.VerifyModel(elements, changed, model.Nested.Id));

            roots = unowned;
            var renamed = (Package)Clone(readBack[model.Nested.Id]);
            renamed.DeclaredName = "Changed";
            readBack[model.Nested.Id] = renamed;

            var elementChanged = await IsolatedAssertions.RunAsync(this.VerifyModel(elements, changed, model.Nested.Id));

            readBack[model.Nested.Id] = new Namespace { Id = model.Nested.Id };

            var otherMetaclass = await IsolatedAssertions.RunAsync(this.VerifyModel(elements, changed, model.Nested.Id));

            readBack[model.Nested.Id] = null;

            var nothingRead = await IsolatedAssertions.RunAsync(this.VerifyModel(elements, changed, model.Nested.Id));

            readBack[model.Nested.Id] = Clone(elements.Single(element => element.Id == model.Nested.Id));
            listed = elements.Skip(1).ToList();

            var elementNotListed = await IsolatedAssertions.RunAsync(this.VerifyModel(elements, changed, model.Nested.Id));

            listed = [.. elements, elements[0]];

            var elementListedTwice = await IsolatedAssertions.RunAsync(this.VerifyModel(elements, changed, model.Nested.Id));

            listed = elements;
            onePage = true;

            var listOnOnePage = await IsolatedAssertions.RunAsync(this.VerifyModel(elements, changed, model.Nested.Id));

            onePage = false;
            dropIncoming = true;

            var incomingLost = await IsolatedAssertions.RunAsync(this.VerifyModel(elements, changed, model.Nested.Id));

            dropIncoming = false;

            var unrelatedSubject = await IsolatedAssertions.RunAsync(this.VerifyModel(elements, changed, Guid.NewGuid()));

            var alias = elements.Single(element => element.GetType() == typeof(Membership));
            var lostMember = (Membership)Clone(alias);
            lostMember.MemberElement = Guid.Empty;
            readBack[alias.Id] = lostMember;

            var memberElementLost = await IsolatedAssertions.RunAsync(this.VerifyModel(elements, changed, model.Nested.Id));

            var lostName = (Membership)Clone(alias);
            lostName.MemberName = null;
            readBack[alias.Id] = lostName;

            var memberNameLost = await IsolatedAssertions.RunAsync(this.VerifyModel(elements, changed, model.Nested.Id));

            readBack[alias.Id] = Clone(alias);
            var dependency = elements.OfType<Dependency>().Single();
            var emptyClient = (Dependency)Clone(dependency);
            emptyClient.Client = [Guid.Empty];
            readBack[dependency.Id] = emptyClient;

            var emptyReference = await IsolatedAssertions.RunAsync(this.VerifyModel(elements, changed, model.Nested.Id));

            readBack[dependency.Id] = Clone(dependency);
            unreadable.Add(model.Nested.Id);

            var elementUnreadable = await IsolatedAssertions.RunAsync(this.VerifyModel(elements, changed, model.Nested.Id));

            unreadable.Clear();
            listUnreadable = true;
            var readBackBefore = this.state.ReadBack.Count;

            var listUnreadableResults = await IsolatedAssertions.RunAsync(this.VerifyModel(elements, changed, model.Nested.Id));
            var recordedWithoutList = this.state.ReadBack.Skip(readBackBefore).Select(entry => entry.Data).ToList();

            listUnreadable = false;
            unreadableDirection = Direction.@in;

            var incomingUnreadable = await IsolatedAssertions.RunAsync(this.VerifyModel(elements, changed, model.Nested.Id));

            unreadableDirection = null;

            var listedRenamed = (Package)Clone(elements.Single(element => element.Id == model.Nested.Id));
            listedRenamed.DeclaredName = "Listed";
            listed = [.. elements.Select(element => element.Id == model.Nested.Id ? listedRenamed : element)];
            this.client.Invocations.Clear();

            var unchangedListedDifferently = await IsolatedAssertions.RunAsync(() => this.assertions.VerifyModelAsync(this.commitId, elements, [], model.Nested.Id));

            this.client.Verify(api => api.GetElementByProjectCommitIdAsync(It.IsAny<Guid>(), It.IsAny<Guid>(), It.IsAny<Guid>(), It.IsAny<bool?>(), It.IsAny<CancellationToken>()), Times.Never, "an element that the commit did not change is not read by id");

            listed = elements;
            unreadable.UnionWith([model.Nested.Id, space.Id]);
            breakingFor.Add(space.Id);
            var recording = new ReadBackAssertions(this.client.Object, this.state, () => [.. recordedReads], [KnownServerDivergences.PreviousCommitAsObject with { Explains = (_, violation) => violation == breaking }]);
            Interlocked.Exchange(ref maximumInFlight, 0);

            var concurrentFailures = await IsolatedAssertions.RunAsync(() => recording.VerifyModelAsync(this.commitId, elements, changed, model.Nested.Id));

            unreadable.Clear();

            using (Assert.EnterMultipleScope())
            {
                Assert.That(identical, Is.Empty);
                Assert.That(readsById, Is.EqualTo(elements.Select(element => element.GetType()).Distinct().Count()), "one changed element of each metaclass is read by id");
                Assert.That(listPageSizes, Is.EqualTo((int?[])[100, 10, 10, 10]), "the whole list in large pages, then the paging walk over the three small pages the model fills");
                Assert.That(largerElements, Has.Count.GreaterThan(30));
                Assert.That(largerModel, Is.Empty);
                Assert.That(walkPages, Is.EqualTo(3), "the paging walk stops after three small pages");
                Assert.That(recorded.TrueForAll(entry => entry.CommitId == this.commitId), Is.True);
                Assert.That(recorded.Select(entry => entry.Data), Is.EqualTo(firstRead.Cast<IData>()).Using<IData>((actual, expected) => ReferenceEquals(actual, expected)));
                Assert.That(unowned, Has.Count.GreaterThan(1), "owned relationships have no owner, so they are roots");
                Assert.That(everyElementRoot.Select(result => result.Status), Is.EqualTo((AssertionStatus[])[AssertionStatus.Failed]));
                Assert.That(notOwnedRelatedRoots.Select(result => result.Status), Is.EqualTo((AssertionStatus[])[AssertionStatus.Warning]));
                Assert.That(notOwnedRelatedRoots[0].Message, Does.Contain(KnownServerDivergences.RootsIgnoreOwningRelationship.Summary));
                Assert.That(nonRelationshipRoots.Select(result => result.Status), Is.EqualTo((AssertionStatus[])[AssertionStatus.Failed]));
                Assert.That(rootMissing.Select(result => result.Status), Is.EqualTo((AssertionStatus[])[AssertionStatus.Failed]));
                Assert.That(foreignRoot.Select(result => result.Status), Is.EqualTo((AssertionStatus[])[AssertionStatus.Failed]));
                Assert.That(elementChanged.Select(result => result.Message), Is.EqualTo((string[])[$"Package {model.Nested.Id} differs from what was sent: Element.DeclaredName"]));
                Assert.That(otherMetaclass.Select(result => result.Message), Has.Some.Contains($"Package {model.Nested.Id} was read back as another metaclass"));
                Assert.That(nothingRead.Select(result => result.Status), Is.EqualTo((AssertionStatus[])[AssertionStatus.Failed]));
                Assert.That(elementNotListed, Has.Count.EqualTo(1));
                Assert.That(elementListedTwice, Has.Count.EqualTo(2), "the list and the paging walk, which reads all of this small model, both report the duplicate");
                Assert.That(elementListedTwice.Select(result => result.Message), Has.Some.Contains("the pages hold distinct elements of the model"));
                Assert.That(listOnOnePage, Has.Count.EqualTo(1));
                Assert.That(listOnOnePage[0].Message, Does.Contain("the elements did not span several pages, so paging is untested"));
                Assert.That(incomingLost, Has.Count.EqualTo(1));
                Assert.That(unrelatedSubject.Select(result => result.Message), Has.Some.Contains("out direction is untested"));
                Assert.That(unrelatedSubject.Select(result => result.Message), Has.Some.Contains("in direction is untested"));
                Assert.That(memberElementLost.Select(result => result.Message), Has.Some.Contains("lost its memberElement (#424)"));
                Assert.That(memberElementLost.Select(result => result.Message), Has.Some.Contains("holds empty references (#424)"));
                Assert.That(memberElementLost.Select(result => result.Message), Has.None.Contains("lost its memberName"));
                Assert.That(memberNameLost.Select(result => result.Message), Has.Some.Contains("lost its memberName (#424)"));
                Assert.That(memberNameLost.Select(result => result.Message), Has.None.Contains("holds empty references"));
                Assert.That(emptyReference.Select(result => result.Message), Has.Some.Contains($"Dependency {dependency.Id} holds empty references (#424)"));
                Assert.That(concurrentFailures.Single(result => result.Message.Contains(model.Nested.Id.ToString()) && result.Message.Contains("threw")).Status, Is.EqualTo(AssertionStatus.Failed), "a breaking response of a concurrent read excuses nothing");
                Assert.That(concurrentFailures.Single(result => result.Message.Contains(space.Id.ToString()) && result.Message.Contains("threw")).Status, Is.EqualTo(AssertionStatus.Warning), "its own breaking response excuses a read");
                Assert.That(maximumInFlight, Is.InRange(2, 4), "the reads run concurrently, at most four at a time");
                Assert.That(unchangedListedDifferently.Select(result => result.Message), Is.EqualTo((string[])[$"Package {model.Nested.Id} as listed differs from what was sent: Element.DeclaredName"]));
                Assert.That(listUnreadableResults.Select(result => result.Message), Is.EqualTo((string[])["GetElementsByProjectCommitAsync threw InvalidOperationException: no list", "GetElementsByProjectCommitAsync in small pages threw InvalidOperationException: no list"]));
                Assert.That(recordedWithoutList, Is.EquivalentTo(changed.GroupBy(element => element.GetType()).Select(metaclass => (IData)readBack[metaclass.First().Id])).Using<IData>((actual, expected) => ReferenceEquals(actual, expected)), "without the list, the elements read by id are recorded");
                Assert.That(incomingUnreadable.Select(result => result.Message), Is.EqualTo((string[])["GetRelationshipsByProjectCommitRelatedElementAsync in threw InvalidOperationException: no relationships"]), "the out and both directions still pass");
                Assert.That(elementUnreadable.Select(result => result.Message), Is.EqualTo((string[])[$"GetElementByProjectCommitIdAsync of Package {model.Nested.Id} threw InvalidOperationException: not deserializable"]));
            }
        }

        [Test]
        public async Task VerifyVerifyProjectUsageAsync()
        {
            var elementId = Guid.NewGuid();
            var openApi = OpenApiDocument.Load(Path.Combine(TestContext.CurrentContext.TestDirectory, "Resources", "ptc-25-02-30.json"));
            var usageOperation = openApi.Operations.Single(operation => operation.ToString() == "GET /projects/{projectId}/commits/{commitId}/elements/{elementId}/projectUsage");
            var usage = new ProjectUsage { Id = Guid.NewGuid(), UsedProject = Guid.NewGuid(), UsedCommit = Guid.NewGuid() };
            ProjectUsage answered = new() { Id = usage.Id, UsedProject = usage.UsedProject, UsedCommit = usage.UsedCommit };
            Exception failure = null;
            var recorded = new List<EndpointCall>();

            this.client.Setup(api => api.GetProjectUsageByProjectCommitElementAsync(this.projectId, this.commitId, elementId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(() => failure != null ? throw failure : answered);

            var same = await IsolatedAssertions.RunAsync(() => this.assertions.VerifyProjectUsageAsync(this.commitId, elementId, usage));

            answered = new ProjectUsage { Id = usage.Id, UsedProject = usage.UsedProject, UsedCommit = Guid.NewGuid() };

            var otherCommit = await IsolatedAssertions.RunAsync(() => this.assertions.VerifyProjectUsageAsync(this.commitId, elementId, usage));

            failure = new InvalidOperationException("an array");

            var failed = await IsolatedAssertions.RunAsync(() => this.assertions.VerifyProjectUsageAsync(this.commitId, elementId, usage));

            var recording = new ReadBackAssertions(this.client.Object, this.state, () => recorded, [KnownServerDivergences.ProjectUsageAsArray]);
            this.client.Setup(api => api.GetProjectUsageByProjectCommitElementAsync(this.projectId, this.commitId, elementId, It.IsAny<CancellationToken>()))
                .Returns(() =>
                {
                    recorded.Add(new EndpointCall(usageOperation, "GET /projects/p/commits/c/elements/e/projectUsage", HttpStatusCode.OK) { ResponseViolations = [new ContractViolation(ContractViolationKind.SchemaMismatch, "/ type: Value is \"array\" but should be \"object\"")] });
                    throw new InvalidOperationException("an array");
                });

            var excused = await IsolatedAssertions.RunAsync(() => recording.VerifyProjectUsageAsync(this.commitId, elementId, usage));

            using (Assert.EnterMultipleScope())
            {
                Assert.That(same, Is.Empty);
                Assert.That(otherCommit.Select(result => result.Message), Has.Some.Contains($"the project usage of {elementId} differs from what was sent"));
                Assert.That(failed.Select(result => result.Status), Is.EqualTo((AssertionStatus[])[AssertionStatus.Failed]));
                Assert.That(excused.Select(result => result.Status), Is.EqualTo((AssertionStatus[])[AssertionStatus.Warning]), "its own breaking response excuses the read");
            }
        }

        [Test]
        public async Task VerifyVerifyQueryRecord()
        {
            var projectId = Guid.NewGuid();
            var sent = ScenarioQueries.Stored;
            var sentWhere = (CompositeConstraintRequest)sent.Where;

            Query Record(Action<Query> change = null)
            {
                var query = new Query { Id = Guid.NewGuid(), OwningProject = projectId, Name = sent.Name, Description = sent.Description, Alias = [.. sent.Alias], Select = [.. sent.Select], Where = new CompositeConstraint { Operator = CompositeConstraintOperator.or, Constraint = sentWhere.Constraint } };
                change?.Invoke(query);
                return query;
            }

            Task<IReadOnlyList<AssertionResult>> Verify(Query query) => IsolatedAssertions.RunAsync(() =>
            {
                ReadBackAssertions.VerifyQueryRecord(query, sent, projectId, "the query");
                return Task.CompletedTask;
            });

            var same = await Verify(Record());
            var lost = await Verify(Record(query => { query.Name = null; query.Description = null; query.Alias = []; }));
            var descriptionLost = await Verify(Record(query => query.Description = null));
            var otherProject = await Verify(Record(query => query.OwningProject = Guid.NewGuid()));
            var otherSelect = await Verify(Record(query => query.Select = ["@id"]));
            var otherWhere = await Verify(Record(query => query.Where = new CompositeConstraint { Operator = CompositeConstraintOperator.and, Constraint = sentWhere.Constraint }));

            using (Assert.EnterMultipleScope())
            {
                Assert.That(same, Is.Empty);
                Assert.That(lost.Select(result => result.Status), Is.EqualTo((AssertionStatus[])[AssertionStatus.Warning]));
                Assert.That(lost[0].Message, Does.Contain(KnownServerDivergences.QueryLosesNameAndDescription.Summary));
                Assert.That(descriptionLost.Select(result => result.Status), Is.EqualTo((AssertionStatus[])[AssertionStatus.Failed]), "losing the description alone is no known divergence");
                Assert.That(otherProject.Select(result => result.Message), Has.Some.Contains("the query belongs to the project"));
                Assert.That(otherSelect.Select(result => result.Message), Has.Some.Contains("the query selects what was sent"));
                Assert.That(otherWhere.Select(result => result.Message), Has.Some.Contains("the query has the constraint that was sent"));
            }
        }

        [Test]
        public async Task VerifyVerifyResultsAsync()
        {
            var first = new Package { Id = Guid.NewGuid(), DeclaredName = "First" };
            var second = new Package { Id = Guid.NewGuid(), DeclaredName = "Second" };
            IReadOnlyList<IData> results = [Clone(first), Clone(second)];
            var unreadable = false;

            Task<PagedResult<IData>> ReadPage(QueryParameters parameters) => unreadable ? throw new InvalidOperationException("no results") : Paged(results, parameters);

            var same = await IsolatedAssertions.RunAsync(() => this.assertions.VerifyResultsAsync("the results", ReadPage, [first, second]));
            var extra = await IsolatedAssertions.RunAsync(() => this.assertions.VerifyResultsAsync("the results", ReadPage, [first]));

            var renamed = (Package)Clone(second);
            renamed.DeclaredName = "Renamed";
            results = [Clone(first), renamed];

            var changed = await IsolatedAssertions.RunAsync(() => this.assertions.VerifyResultsAsync("the results", ReadPage, [first, second]));

            unreadable = true;

            var failed = await IsolatedAssertions.RunAsync(() => this.assertions.VerifyResultsAsync("the results", ReadPage, [first, second]));

            using (Assert.EnterMultipleScope())
            {
                Assert.That(same, Is.Empty);
                Assert.That(extra, Has.Count.EqualTo(1));
                Assert.That(changed.Select(result => result.Message), Is.EqualTo((string[])[$"Package {second.Id} in the results differs from what was sent: Element.DeclaredName"]));
                Assert.That(failed.Select(result => result.Message), Is.EqualTo((string[])["the results threw InvalidOperationException: no results"]));
            }
        }

        [Test]
        public async Task VerifyVerifyRootsAsync()
        {
            var elements = new RootModel().Builder.ToDtos();
            var unowned = elements.Where(element => element.OwningRelationship == null).ToList<IElement>();
            IReadOnlyList<IElement> roots = unowned;
            var unreadable = false;

            Task<PagedResult<IElement>> ReadPage(QueryParameters parameters) => unreadable ? throw new InvalidOperationException("no roots") : Paged(roots, parameters);

            var exact = await IsolatedAssertions.RunAsync(() => this.assertions.VerifyRootsAsync("the roots", ReadPage, elements));

            roots = [.. elements.Where(element => element is not IRelationship { OwningRelatedElement: not null })];

            var divergent = await IsolatedAssertions.RunAsync(() => this.assertions.VerifyRootsAsync("the roots", ReadPage, elements));

            roots = elements;

            var everyElement = await IsolatedAssertions.RunAsync(() => this.assertions.VerifyRootsAsync("the roots", ReadPage, elements));

            unreadable = true;

            var failed = await IsolatedAssertions.RunAsync(() => this.assertions.VerifyRootsAsync("the roots", ReadPage, elements));

            using (Assert.EnterMultipleScope())
            {
                Assert.That(exact, Is.Empty);
                Assert.That(divergent.Select(result => result.Status), Is.EqualTo((AssertionStatus[])[AssertionStatus.Warning]), "the known divergence is reported");
                Assert.That(divergent[0].Message, Does.Contain(KnownServerDivergences.RootsIgnoreOwningRelationship.Summary));
                Assert.That(everyElement.Select(result => result.Status), Is.EqualTo((AssertionStatus[])[AssertionStatus.Failed]));
                Assert.That(failed.Select(result => result.Message), Is.EqualTo((string[])["the roots threw InvalidOperationException: no roots"]));
            }
        }

        private static IElement Clone(IElement element) => (IElement)CloneMethod.Invoke(element, null);

        private static Task<PagedResult<T>> Paged<T>(IReadOnlyList<T> records, QueryParameters parameters)
        {
            var start = parameters?.PageAfter is { } after ? int.Parse(after.Token) : 0;
            var size = parameters is { PageSize: > 0 } ? parameters.PageSize : records.Count;
            var end = Math.Min(start + size, records.Count);

            return Task.FromResult(new PagedResult<T>
            {
                Records = records.Skip(start).Take(end - start).ToList(),
                Next = end < records.Count ? new Page(end.ToString()) : null
            });
        }

        private Func<Task> VerifyModel(IReadOnlyList<IElement> elements, IReadOnlyList<IElement> changed, Guid relationshipSubjectId) => () => this.assertions.VerifyModelAsync(this.commitId, elements, changed, relationshipSubjectId);
    }
}
