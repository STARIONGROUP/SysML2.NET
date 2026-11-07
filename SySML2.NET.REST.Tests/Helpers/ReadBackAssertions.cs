// ------------------------------------------------------------------------------------------------
// <copyright file="ReadBackAssertions.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SySML2.NET.REST.Tests.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using NUnit.Framework;

    using SySML2.NET.REST;

    using SysML2.NET.Common;
    using SysML2.NET.Core.DTO.Root.Elements;
    using SysML2.NET.Core.DTO.Root.Namespaces;
    using SysML2.NET.Extensions.Core.DTO.Comparers;
    using SysML2.NET.PSM;
    using SysML2.NET.PSM.DTO;

    using Direction = SysML2.NET.PSM.Enumerations.Direction;

    /// <summary>
    /// The assertions that every model step makes after a commit: the commit, its changes, and the model read back
    /// through every read operation, against the model that was sent
    /// </summary>
    public sealed class ReadBackAssertions
    {
        /// <summary>
        /// The page size used to read lists, small enough that a step's model spans several pages
        /// </summary>
        private const int PageSize = 10;

        /// <summary>
        /// The maximum number of pages read from one list, guarding against a server that never ends a list
        /// </summary>
        private const int MaximumPages = 1000;

        /// <summary>
        /// The client under test
        /// </summary>
        private readonly IPsmApiClient client;

        /// <summary>
        /// The scenario state that read-back data is recorded in
        /// </summary>
        private readonly ScenarioState state;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReadBackAssertions"/> class
        /// </summary>
        /// <param name="client">
        /// The client under test
        /// </param>
        /// <param name="state">
        /// The scenario state that read-back data is recorded in
        /// </param>
        public ReadBackAssertions(IPsmApiClient client, ScenarioState state)
        {
            this.client = client;
            this.state = state;
        }

        /// <summary>
        /// Reads every page of a list operation, following <see cref="PagedResult{T}.Next"/>
        /// </summary>
        /// <typeparam name="T">
        /// The type of the records
        /// </typeparam>
        /// <param name="readPage">
        /// Reads one page for the given <see cref="QueryParameters"/>
        /// </param>
        /// <returns>
        /// The records of every page, in order, and the number of pages read
        /// </returns>
        /// <exception cref="InvalidOperationException">
        /// Thrown when the server repeats a page cursor or the list does not end within the page limit
        /// </exception>
        public static async Task<(IReadOnlyList<T> Records, int Pages)> ReadAllPagesAsync<T>(Func<QueryParameters, Task<PagedResult<T>>> readPage)
        {
            var records = new List<T>();
            var cursors = new HashSet<string>();
            var parameters = new QueryParameters { PageSize = PageSize };

            for (var pages = 1; pages <= MaximumPages; pages++)
            {
                var page = await readPage(parameters);
                records.AddRange(page.Records);

                if (page.Next == null)
                {
                    return (records, pages);
                }

                if (!cursors.Add(page.Next.Value.Token))
                {
                    throw new InvalidOperationException($"The server repeated the page cursor {page.Next.Value.Token} after {pages} pages");
                }

                parameters = new QueryParameters { PageSize = PageSize, PageAfter = page.Next };
            }

            throw new InvalidOperationException($"The list did not end within {MaximumPages} pages");
        }

        /// <summary>
        /// Verifies a commit on a branch: the commit itself, its place in the history, the branch head and its changes
        /// </summary>
        /// <param name="commit">
        /// The commit that <c>PostCommitByProjectAsync</c> returned
        /// </param>
        /// <param name="branchId">
        /// The branch the commit was made on
        /// </param>
        /// <param name="previousCommitId">
        /// The head of the branch before the commit, or <c>null</c> for the first commit
        /// </param>
        /// <param name="changed">
        /// The elements the commit created or replaced
        /// </param>
        /// <returns>
        /// An awaitable <see cref="Task"/>
        /// </returns>
        public async Task VerifyCommitAsync(Commit commit, Guid branchId, Guid? previousCommitId, IReadOnlyList<IElement> changed)
        {
            var projectId = this.state.ProjectId!.Value;

            Guid[] expectedPrevious = previousCommitId.HasValue ? [previousCommitId.Value] : [];

            var commitById = await this.client.GetCommitByProjectAndIdAsync(projectId, commit.Id);
            var (commits, _) = await ReadAllPagesAsync(parameters => this.client.GetCommitsByProjectAsync(projectId, parameters));
            var branch = await this.client.GetBranchesByProjectAndIdAsync(projectId, branchId);
            var (changes, changePages) = await ReadAllPagesAsync(parameters => this.client.GetChangesByProjectCommitAsync(projectId, commit.Id, parameters));

            using (Assert.EnterMultipleScope())
            {
                Assert.That(commitById.Id, Is.EqualTo(commit.Id));
                Assert.That(commitById.OwningProject, Is.EqualTo(projectId));
                Assert.That(commitById.PreviousCommit, Is.EqualTo(expectedPrevious));
                Assert.That(commits.Select(record => record.Id), Does.Contain(commit.Id));
                Assert.That(branch.Head, Is.EqualTo(commit.Id));
                Assert.That(changes.Select(change => change.Identity.Id), Is.EquivalentTo(changed.Select(element => element.Id)));
                Assert.That(changePages, Is.GreaterThan(1), "the changes did not span several pages, so paging is untested");

                var sentById = changed.ToDictionary(element => element.Id);

                foreach (var change in changes.Where(change => sentById.ContainsKey(change.Identity.Id)))
                {
                    VerifySame(sentById[change.Identity.Id], change.Payload as IElement, $"the payload of change {change.Id}");
                }
            }

            if (changes.Count == 0)
            {
                return;
            }

            var firstChange = changes[0];
            var changeById = await this.client.GetChangeByProjectCommitIdAsync(projectId, commit.Id, firstChange.Id);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(changeById.Id, Is.EqualTo(firstChange.Id));
                Assert.That(changeById.Identity.Id, Is.EqualTo(firstChange.Identity.Id));

                if (firstChange.Payload is IElement firstPayload)
                {
                    VerifySame(firstPayload, changeById.Payload as IElement, $"the payload of change {changeById.Id} read by id");
                }
            }
        }

        /// <summary>
        /// Verifies the model at a commit through every element read operation, recording what the server returned
        /// </summary>
        /// <param name="commitId">
        /// The commit to read the model at
        /// </param>
        /// <param name="expected">
        /// The complete model expected at <paramref name="commitId"/>
        /// </param>
        /// <param name="relationshipSubjectId">
        /// The element whose relationships are read in each direction
        /// </param>
        /// <returns>
        /// An awaitable <see cref="Task"/>
        /// </returns>
        public async Task VerifyModelAsync(Guid commitId, IReadOnlyList<IElement> expected, Guid relationshipSubjectId)
        {
            var projectId = this.state.ProjectId!.Value;
            var readById = new Dictionary<Guid, IElement>();

            foreach (var element in expected)
            {
                readById[element.Id] = await this.client.GetElementByProjectCommitIdAsync(projectId, commitId, element.Id);
            }

            this.state.ReadBack.AddRange(readById.Values.Select(element => (commitId, (IData)element)));

            var (listed, listPages) = await ReadAllPagesAsync(parameters => this.client.GetElementsByProjectCommitAsync(projectId, commitId, queryParameters: parameters));
            var (roots, _) = await ReadAllPagesAsync(parameters => this.client.GetRootsByProjectCommitAsync(projectId, commitId, queryParameters: parameters));

            using (Assert.EnterMultipleScope())
            {
                foreach (var element in expected)
                {
                    VerifySame(element, readById[element.Id], $"{element.GetType().Name} {element.Id}");
                }

                Assert.That(listed.Select(element => element.Id), Is.EquivalentTo(expected.Select(element => element.Id)));
                Assert.That(listPages, Is.GreaterThan(1), "the elements did not span several pages, so paging is untested");
                VerifyRoots(roots, expected);

                VerifyMemberships(expected, readById);
                VerifyNoEmptyReferences(readById.Values);
            }

            await this.VerifyRelationshipsAsync(commitId, expected, relationshipSubjectId);
        }

        /// <summary>
        /// Verifies that every membership read back names the member element and member name that were sent,
        /// which the generated comparers do not compare
        /// </summary>
        /// <param name="expected">
        /// The model that was sent
        /// </param>
        /// <param name="readById">
        /// The elements read back, by id
        /// </param>
        private static void VerifyMemberships(IReadOnlyList<IElement> expected, IReadOnlyDictionary<Guid, IElement> readById)
        {
            foreach (var membership in expected.OfType<IMembership>())
            {
                var actual = readById[membership.Id] as IMembership;

                Assert.That(actual?.MemberElement, Is.EqualTo(membership.MemberElement), $"{membership.GetType().Name} {membership.Id} lost its memberElement (#424)");
                Assert.That(actual?.MemberName, Is.EqualTo(membership.MemberName), $"{membership.GetType().Name} {membership.Id} lost its memberName (#424)");
            }
        }

        /// <summary>
        /// Verifies that no non-derived reference read back is the empty GUID, which refers to no element
        /// </summary>
        /// <param name="elements">
        /// The elements read back
        /// </param>
        private static void VerifyNoEmptyReferences(IEnumerable<IElement> elements)
        {
            foreach (var element in elements.Where(element => element != null))
            {
                var emptyReferences = DtoMetadata.PropertiesOf(element.GetType())
                    .Where(property => property.ValueOf(element) switch
                    {
                        Guid reference => reference == Guid.Empty,
                        IEnumerable<Guid> references => references.Contains(Guid.Empty),
                        _ => false
                    })
                    .Select(property => property.Name)
                    .ToList();

                Assert.That(emptyReferences, Is.Empty, $"{element.GetType().Name} {element.Id} holds empty references (#424)");
            }
        }

        /// <summary>
        /// Verifies the roots read back: exactly the elements without an <c>owner</c>, or, as the known divergence
        /// <see cref="KnownServerDivergences.RootsIgnoreOwningRelationship"/>, exactly the elements without an
        /// <c>owningRelatedElement</c>
        /// </summary>
        /// <param name="roots">
        /// The roots read back
        /// </param>
        /// <param name="model">
        /// The model that was sent
        /// </param>
        private static void VerifyRoots(IReadOnlyList<IElement> roots, IReadOnlyList<IElement> model)
        {
            var rootIds = roots.Select(element => element.Id).ToHashSet();
            var unowned = model.Where(element => element.owner == null).Select(element => element.Id).ToList();
            var notOwnedRelated = model.Where(element => element is not IRelationship { OwningRelatedElement: not null }).Select(element => element.Id).ToList();

            if (!rootIds.SetEquals(unowned) && roots.Count == notOwnedRelated.Count && rootIds.SetEquals(notOwnedRelated))
            {
                KnownServerDivergences.Report(KnownServerDivergences.RootsIgnoreOwningRelationship, $"{roots.Count} roots for {unowned.Count} elements without an owner");
                return;
            }

            Assert.That(roots.Select(element => element.Id), Is.EquivalentTo(unowned), "the roots are the elements without an owner");
        }

        /// <summary>
        /// Verifies that an element read back is of the metaclass that was sent and equal to it, naming the
        /// properties that differ
        /// </summary>
        /// <param name="sent">
        /// The element that was sent
        /// </param>
        /// <param name="actual">
        /// The element read back, or <c>null</c>
        /// </param>
        /// <param name="description">
        /// The description of the element in assertion messages
        /// </param>
        private static void VerifySame(IElement sent, IElement actual, string description)
        {
            Assert.That(actual, Is.TypeOf(sent.GetType()), $"{description} was read back as another metaclass");

            if (actual?.GetType() != sent.GetType() || ComparerProvider.Resolve(sent).Equals(sent, actual))
            {
                return;
            }

            var differing = DtoMetadata.PropertiesOf(sent.GetType())
                .Where(property => !DtoMetadata.ValueEquals(property.ValueOf(sent), property.ValueOf(actual)))
                .Select(property => property.Name)
                .ToList();

            Assert.Fail($"{description} differs from what was sent: {(differing.Count == 0 ? "a member that only its comparer compares" : string.Join(", ", differing))}");
        }

        /// <summary>
        /// Verifies the relationships of an element in each direction against the sources and targets that were sent
        /// </summary>
        /// <param name="commitId">
        /// The commit to read at
        /// </param>
        /// <param name="expected">
        /// The model that was sent
        /// </param>
        /// <param name="subjectId">
        /// The related element
        /// </param>
        /// <returns>
        /// An awaitable <see cref="Task"/>
        /// </returns>
        private async Task VerifyRelationshipsAsync(Guid commitId, IReadOnlyList<IElement> expected, Guid subjectId)
        {
            var projectId = this.state.ProjectId!.Value;
            var relationships = expected.OfType<IRelationship>().ToList();

            var outgoing = relationships.Where(relationship => relationship.Source.Contains(subjectId)).Select(relationship => relationship.Id).ToList();
            var incoming = relationships.Where(relationship => relationship.Target.Contains(subjectId)).Select(relationship => relationship.Id).ToList();

            var (readOut, _) = await ReadAllPagesAsync(parameters => this.client.GetRelationshipsByProjectCommitRelatedElementAsync(projectId, commitId, subjectId, Direction.@out, queryParameters: parameters));
            var (readIn, _) = await ReadAllPagesAsync(parameters => this.client.GetRelationshipsByProjectCommitRelatedElementAsync(projectId, commitId, subjectId, Direction.@in, queryParameters: parameters));
            var (readBoth, _) = await ReadAllPagesAsync(parameters => this.client.GetRelationshipsByProjectCommitRelatedElementAsync(projectId, commitId, subjectId, Direction.both, queryParameters: parameters));

            using (Assert.EnterMultipleScope())
            {
                Assert.That(outgoing, Is.Not.Empty, "the subject is the source of no relationship, so the out direction is untested");
                Assert.That(incoming, Is.Not.Empty, "the subject is the target of no relationship, so the in direction is untested");
                Assert.That(readOut.Select(relationship => relationship.Id), Is.EquivalentTo(outgoing));
                Assert.That(readIn.Select(relationship => relationship.Id), Is.EquivalentTo(incoming));
                Assert.That(readBoth.Select(relationship => relationship.Id), Is.EquivalentTo(outgoing.Union(incoming)));
            }
        }
    }
}
