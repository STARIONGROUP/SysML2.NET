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
    using System.Collections.Generic;
    using System.Linq;
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
                Assert.That(requests.TrueForAll(parameters => parameters.PageSize == 10), Is.True);
                Assert.That(single, Is.EqualTo((int[])[7]));
                Assert.That(singlePages, Is.EqualTo(1));
            }

            var counter = 0;

            await Assert.ThatAsync(() => ReadBackAssertions.ReadAllPagesAsync(_ => Task.FromResult(new PagedResult<int> { Records = [], Next = new Page("same") })), Throws.InvalidOperationException.With.Message.Contains("repeated"));
            await Assert.ThatAsync(() => ReadBackAssertions.ReadAllPagesAsync(_ => Task.FromResult(new PagedResult<int> { Records = [], Next = new Page($"cursor-{++counter}") })), Throws.InvalidOperationException.With.Message.Contains("did not end"));
        }

        [Test]
        public async Task VerifyVerifyCommitAsync()
        {
            var branchId = Guid.NewGuid();
            var previousId = Guid.NewGuid();

            List<IElement> changed = [.. Enumerable.Range(0, 25).Select(index => new Package { Id = Guid.NewGuid(), DeclaredName = $"P{index}" })];
            var changes = changed.Select(element => new DataVersion { Id = Guid.NewGuid(), Identity = new DataIdentity { Id = element.Id }, Payload = Clone(element) }).ToList();
            var commit = new Commit { Id = this.commitId, OwningProject = this.projectId };
            List<Commit> commits = [commit];
            DataVersion changeById = null;
            Guid? head = this.commitId;

            this.client.Setup(api => api.GetCommitByProjectAndIdAsync(this.projectId, this.commitId, It.IsAny<CancellationToken>())).ReturnsAsync(() => commit);
            this.client.Setup(api => api.GetCommitsByProjectAsync(this.projectId, It.IsAny<QueryParameters>(), It.IsAny<CancellationToken>())).Returns((Guid _, QueryParameters parameters, CancellationToken _) => Paged(commits, parameters));
            this.client.Setup(api => api.GetBranchesByProjectAndIdAsync(this.projectId, branchId, It.IsAny<CancellationToken>())).ReturnsAsync(() => new Branch { Id = branchId, Head = head });
            this.client.Setup(api => api.GetChangesByProjectCommitAsync(this.projectId, this.commitId, It.IsAny<QueryParameters>(), It.IsAny<CancellationToken>())).Returns((Guid _, Guid _, QueryParameters parameters, CancellationToken _) => Paged(changes, parameters));
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
            changes = changes[..5];

            var onePage = await IsolatedAssertions.RunAsync(() => this.assertions.VerifyCommitAsync(commit, branchId, previousId, changed[..5]));
            var missingChanges = await IsolatedAssertions.RunAsync(() => this.assertions.VerifyCommitAsync(commit, branchId, previousId, changed));

            changes = [];
            this.client.Invocations.Clear();

            var noChanges = await IsolatedAssertions.RunAsync(() => this.assertions.VerifyCommitAsync(commit, branchId, previousId, []));

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
                Assert.That(missingChanges, Has.Count.EqualTo(2));
                Assert.That(noChanges, Has.Count.EqualTo(1));
            }

            this.client.Verify(api => api.GetChangeByProjectCommitIdAsync(It.IsAny<Guid>(), It.IsAny<Guid>(), It.IsAny<Guid>(), It.IsAny<CancellationToken>()), Times.Never);
        }

        [Test]
        public async Task VerifyVerifyModelAsync()
        {
            var model = new RootModel();
            var elements = model.Builder.ToDtos();
            var relationships = elements.OfType<IRelationship>().ToList();
            var readBack = elements.ToDictionary(element => element.Id, Clone);
            var unowned = elements.Where(element => element.owner == null).ToList();
            IReadOnlyList<IElement> roots = unowned;
            IReadOnlyList<IElement> listed = elements;
            var onePage = false;
            var dropIncoming = false;

            this.client.Setup(api => api.GetElementByProjectCommitIdAsync(this.projectId, this.commitId, It.IsAny<Guid>(), It.IsAny<bool?>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((Guid _, Guid _, Guid elementId, bool? _, CancellationToken _) => readBack[elementId]);

            this.client.Setup(api => api.GetElementsByProjectCommitAsync(this.projectId, this.commitId, It.IsAny<bool?>(), It.IsAny<QueryParameters>(), It.IsAny<CancellationToken>()))
                .Returns((Guid _, Guid _, bool? _, QueryParameters parameters, CancellationToken _) => Paged(listed, onePage ? null : parameters));

            this.client.Setup(api => api.GetRootsByProjectCommitAsync(this.projectId, this.commitId, It.IsAny<bool?>(), It.IsAny<QueryParameters>(), It.IsAny<CancellationToken>()))
                .Returns((Guid _, Guid _, bool? _, QueryParameters parameters, CancellationToken _) => Paged(roots, parameters));

            this.client.Setup(api => api.GetRelationshipsByProjectCommitRelatedElementAsync(this.projectId, this.commitId, It.IsAny<Guid>(), It.IsAny<Direction?>(), It.IsAny<bool?>(), It.IsAny<QueryParameters>(), It.IsAny<CancellationToken>()))
                .Returns((Guid _, Guid _, Guid subjectId, Direction? direction, bool? _, QueryParameters parameters, CancellationToken _) =>
                    Paged(relationships.Where(relationship => direction switch
                    {
                        Direction.@out => relationship.Source.Contains(subjectId),
                        Direction.@in => !dropIncoming && relationship.Target.Contains(subjectId),
                        _ => relationship.Source.Contains(subjectId) || relationship.Target.Contains(subjectId)
                    }).ToList(), parameters));

            var firstRead = elements.Select(element => readBack[element.Id]).ToList();
            var identical = await IsolatedAssertions.RunAsync(this.VerifyModel(elements, model.Nested.Id));
            var recorded = this.state.ReadBack.ToList();

            roots = elements;

            var everyElementRoot = await IsolatedAssertions.RunAsync(this.VerifyModel(elements, model.Nested.Id));

            roots = [.. elements.Where(element => element is not IRelationship { OwningRelatedElement: not null })];

            var notOwnedRelatedRoots = await IsolatedAssertions.RunAsync(this.VerifyModel(elements, model.Nested.Id));

            roots = [.. elements.Where(element => element is not IRelationship)];

            var nonRelationshipRoots = await IsolatedAssertions.RunAsync(this.VerifyModel(elements, model.Nested.Id));

            roots = [.. unowned.Where(element => element.Id != model.Root.Id)];

            var rootMissing = await IsolatedAssertions.RunAsync(this.VerifyModel(elements, model.Nested.Id));

            roots = [.. unowned, new Package { Id = Guid.NewGuid() }];

            var foreignRoot = await IsolatedAssertions.RunAsync(this.VerifyModel(elements, model.Nested.Id));

            roots = unowned;
            var renamed = (Package)Clone(readBack[model.Nested.Id]);
            renamed.DeclaredName = "Changed";
            readBack[model.Nested.Id] = renamed;

            var elementChanged = await IsolatedAssertions.RunAsync(this.VerifyModel(elements, model.Nested.Id));

            readBack[model.Nested.Id] = new Namespace { Id = model.Nested.Id };

            var otherMetaclass = await IsolatedAssertions.RunAsync(this.VerifyModel(elements, model.Nested.Id));

            readBack[model.Nested.Id] = null;

            var nothingRead = await IsolatedAssertions.RunAsync(this.VerifyModel(elements, model.Nested.Id));

            readBack[model.Nested.Id] = Clone(elements.Single(element => element.Id == model.Nested.Id));
            listed = elements.Skip(1).ToList();

            var elementNotListed = await IsolatedAssertions.RunAsync(this.VerifyModel(elements, model.Nested.Id));

            listed = [.. elements, elements[0]];

            var elementListedTwice = await IsolatedAssertions.RunAsync(this.VerifyModel(elements, model.Nested.Id));

            listed = elements;
            onePage = true;

            var listOnOnePage = await IsolatedAssertions.RunAsync(this.VerifyModel(elements, model.Nested.Id));

            onePage = false;
            dropIncoming = true;

            var incomingLost = await IsolatedAssertions.RunAsync(this.VerifyModel(elements, model.Nested.Id));

            dropIncoming = false;

            var unrelatedSubject = await IsolatedAssertions.RunAsync(this.VerifyModel(elements, Guid.NewGuid()));

            var alias = elements.Single(element => element.GetType() == typeof(Membership));
            var lostMember = (Membership)Clone(alias);
            lostMember.MemberElement = Guid.Empty;
            readBack[alias.Id] = lostMember;

            var memberElementLost = await IsolatedAssertions.RunAsync(this.VerifyModel(elements, model.Nested.Id));

            var lostName = (Membership)Clone(alias);
            lostName.MemberName = null;
            readBack[alias.Id] = lostName;

            var memberNameLost = await IsolatedAssertions.RunAsync(this.VerifyModel(elements, model.Nested.Id));

            readBack[alias.Id] = Clone(alias);
            var dependency = elements.OfType<Dependency>().Single();
            var emptyClient = (Dependency)Clone(dependency);
            emptyClient.Client = [Guid.Empty];
            readBack[dependency.Id] = emptyClient;

            var emptyReference = await IsolatedAssertions.RunAsync(this.VerifyModel(elements, model.Nested.Id));

            using (Assert.EnterMultipleScope())
            {
                Assert.That(identical, Is.Empty);
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
                Assert.That(elementListedTwice, Has.Count.EqualTo(1));
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

        private Func<Task> VerifyModel(IReadOnlyList<IElement> elements, Guid relationshipSubjectId) => () => this.assertions.VerifyModelAsync(this.commitId, elements, relationshipSubjectId);
    }
}
