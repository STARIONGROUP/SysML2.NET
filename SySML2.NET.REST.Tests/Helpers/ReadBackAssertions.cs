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
    using System.Net;
    using System.Reflection;
    using System.Threading;
    using System.Threading.Tasks;

    using NUnit.Framework;

    using SySML2.NET.REST;

    using SysML2.NET.Common;
    using SysML2.NET.Core.DTO.Root.Elements;
    using SysML2.NET.Core.DTO.Root.Namespaces;
    using SysML2.NET.Extensions.Core.DTO.Comparers;
    using SysML2.NET.PSM;
    using SysML2.NET.PSM.DTO;
    using SysML2.NET.REST.Exceptions;

    using Direction = SysML2.NET.PSM.Enumerations.Direction;

    /// <summary>
    /// The assertions that every model step makes after a commit: the commit, its changes, and the model read back
    /// through every read operation, against the model that was sent
    /// </summary>
    public sealed class ReadBackAssertions
    {
        /// <summary>
        /// The page size used to read a whole list
        /// </summary>
        private const int ListPageSize = 100;

        /// <summary>
        /// The page size of the paging walk, small enough that a step's model spans several pages
        /// </summary>
        private const int PagingPageSize = 10;

        /// <summary>
        /// The number of pages the paging walk reads
        /// </summary>
        private const int PagingWalkPages = 3;

        /// <summary>
        /// The maximum number of pages read from one list, guarding against a server that never ends a list
        /// </summary>
        private const int MaximumPages = 1000;

        /// <summary>
        /// The number of independent reads sent to the server at the same time
        /// </summary>
        private const int ReadConcurrency = 4;

        /// <summary>
        /// The client under test
        /// </summary>
        private readonly IPsmApiClient client;

        /// <summary>
        /// The scenario state that read-back data is recorded in
        /// </summary>
        private readonly ScenarioState state;

        /// <summary>
        /// Gets the calls that the <see cref="EndpointRecorder"/> recorded so far, in order, or <c>null</c>
        /// </summary>
        private readonly Func<IReadOnlyCollection<EndpointCall>> recordedCalls;

        /// <summary>
        /// The known divergences, of which those that break deserialization excuse a failed read
        /// </summary>
        private readonly IReadOnlyList<ServerDivergence> divergences;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReadBackAssertions"/> class
        /// </summary>
        /// <param name="client">
        /// The client under test
        /// </param>
        /// <param name="state">
        /// The scenario state that read-back data is recorded in
        /// </param>
        /// <param name="recordedCalls">
        /// Gets the calls that the <see cref="EndpointRecorder"/> recorded so far, so that a read failing on a response
        /// the client cannot be expected to read is told apart from a client defect; <c>null</c> when nothing is recorded
        /// </param>
        /// <param name="divergences">
        /// The known divergences, <see cref="KnownServerDivergences.All"/> when <c>null</c>
        /// </param>
        public ReadBackAssertions(IPsmApiClient client, ScenarioState state, Func<IReadOnlyCollection<EndpointCall>> recordedCalls = null, IReadOnlyList<ServerDivergence> divergences = null)
        {
            this.client = client;
            this.state = state;
            this.recordedCalls = recordedCalls;
            this.divergences = divergences ?? KnownServerDivergences.All;
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
            var (records, pages, ended) = await ReadPagesAsync(readPage, ListPageSize, MaximumPages);

            return ended ? (records, pages) : throw new InvalidOperationException($"The list did not end within {MaximumPages} pages");
        }

        /// <summary>
        /// Verifies a query read back against the request it was created or updated with: its project, select and
        /// constraint, and its name, description and aliases, or the known divergence that the server lost all three
        /// </summary>
        /// <param name="query">
        /// The query read back
        /// </param>
        /// <param name="sent">
        /// The request the query was created or updated with
        /// </param>
        /// <param name="projectId">
        /// The project the query belongs to
        /// </param>
        /// <param name="description">
        /// The description of the query in assertion messages
        /// </param>
        public static void VerifyQueryRecord(Query query, QueryRequest sent, Guid projectId, string description)
        {
            Assert.That(query.OwningProject, Is.EqualTo(projectId), $"{description} belongs to the project");
            Assert.That(query.Select, Is.EqualTo(sent.Select), $"{description} selects what was sent");
            Assert.That(ScenarioQueries.ConstraintText(query.Where), Is.EqualTo(ScenarioQueries.ConstraintText(sent.Where)), $"{description} has the constraint that was sent");

            if (string.IsNullOrWhiteSpace(query.Name) && query.Description == null && query.Alias.Count == 0)
            {
                KnownServerDivergences.Report(KnownServerDivergences.QueryLosesNameAndDescription, $"{description} came back without the name \"{sent.Name}\"");
                return;
            }

            Assert.That(query.Name, Is.EqualTo(sent.Name), $"{description} has the name that was sent");
            Assert.That(query.Description, Is.EqualTo(sent.Description), $"{description} has the description that was sent");
            Assert.That(query.Alias, Is.EqualTo(sent.Alias), $"{description} has the aliases that were sent");
        }

        /// <summary>
        /// Verifies that deleting an element deleted what it owns: none of the owned elements is listed or readable at
        /// the commit, or, as the known divergence <see cref="KnownServerDivergences.DeleteDoesNotCascade"/>, all of
        /// them are still listed and nothing else differs
        /// </summary>
        /// <param name="commitId">
        /// The commit that deleted the owner
        /// </param>
        /// <param name="owned">
        /// The elements the deleted owner owned, directly or indirectly, that the commit did not list
        /// </param>
        /// <param name="expected">
        /// The model expected at <paramref name="commitId"/>, without the owned elements
        /// </param>
        /// <returns>
        /// An awaitable <see cref="Task"/>
        /// </returns>
        public async Task VerifyCascadeAsync(Guid commitId, IReadOnlyList<IElement> owned, IReadOnlyList<IElement> expected)
        {
            var projectId = this.state.ProjectId!.Value;
            var (listed, _) = await this.ReadAsync(() => ReadAllPagesAsync(parameters => this.client.GetElementsByProjectCommitAsync(projectId, commitId, queryParameters: parameters)), "GetElementsByProjectCommitAsync after the cascading delete");

            if (listed == null)
            {
                return;
            }

            var listedIds = listed.Select(element => element.Id).ToHashSet();
            var expectedIds = expected.Select(element => element.Id).ToHashSet();

            if (owned.Count > 0 && owned.All(element => listedIds.Contains(element.Id)) && listedIds.SetEquals(expectedIds.Union(owned.Select(element => element.Id))))
            {
                KnownServerDivergences.Report(KnownServerDivergences.DeleteDoesNotCascade, $"the {owned.Count} elements the deleted element owned are still listed");
                return;
            }

            using (Assert.EnterMultipleScope())
            {
                Assert.That(listedIds, Is.EquivalentTo(expectedIds), "deleting an element deletes what it owns");

                foreach (var element in owned)
                {
                    var description = $"reading {element.GetType().Name} {element.Id}, owned by the deleted element";

                    try
                    {
                        await KnownServerDivergences.ExpectNotFoundAsync(() => this.client.GetElementByProjectCommitIdAsync(projectId, commitId, element.Id), () => this.recordedCalls?.Invoke().LastOrDefault(), null, description);
                    }
                    catch (Exception exception) when (exception is not ResultStateException)
                    {
                        this.ReportFailedRead(description, exception, (this.recordedCalls?.Invoke() ?? []).TakeLast(1));
                    }
                }
            }
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

            using (Assert.EnterMultipleScope())
            {
                await this.VerifyCommitRecordAsync(commit, branchId, previousCommitId);

                var (changes, changePages) = await this.ReadAsync(() => ReadAllPagesAsync(parameters => this.client.GetChangesByProjectCommitAsync(projectId, commit.Id, parameters)), "GetChangesByProjectCommitAsync");

                if (changes == null)
                {
                    return;
                }

                Assert.That(changes.Select(change => change.Identity.Id), Is.EquivalentTo(changed.Select(element => element.Id)));
                if (changed.Count > ListPageSize)
                {
                    Assert.That(changePages, Is.GreaterThan(1), "the changes did not span several pages, so paging is untested");
                }

                var sentById = changed.ToDictionary(element => element.Id);

                foreach (var change in changes.Where(change => sentById.ContainsKey(change.Identity.Id)))
                {
                    VerifySame(sentById[change.Identity.Id], change.Payload as IElement, $"the payload of change {change.Id}");
                }

                if (changes.Count == 0)
                {
                    return;
                }

                var firstChange = changes[0];
                var changeById = await this.ReadAsync(() => this.client.GetChangeByProjectCommitIdAsync(projectId, commit.Id, firstChange.Id), "GetChangeByProjectCommitIdAsync");

                if (changeById == null)
                {
                    return;
                }

                Assert.That(changeById.Id, Is.EqualTo(firstChange.Id));
                Assert.That(changeById.Identity.Id, Is.EqualTo(firstChange.Identity.Id));

                if (firstChange.Payload is IElement firstPayload)
                {
                    VerifySame(firstPayload, changeById.Payload as IElement, $"the payload of change {changeById.Id} read by id");
                }
            }
        }

        /// <summary>
        /// Verifies a commit of data that is no element, such as project usages and external data, which a client reads
        /// back through the changes of the commit: the commit itself, its place in the history, the branch head, and one
        /// change per data item, with the payload that was sent or none for a deletion
        /// </summary>
        /// <param name="commit">
        /// The commit that <c>PostCommitByProjectAsync</c> returned
        /// </param>
        /// <param name="branchId">
        /// The branch the commit was made on
        /// </param>
        /// <param name="previousCommitId">
        /// The head of the branch before the commit
        /// </param>
        /// <param name="expected">
        /// Per data item, its id and the payload the commit sent, or <c>null</c> when the commit deletes it
        /// </param>
        /// <param name="cascaded">
        /// The ids of the elements that the commit deletes with their owner, which may also be listed as deleted; none
        /// when <c>null</c>
        /// </param>
        /// <returns>
        /// An awaitable <see cref="Task"/>
        /// </returns>
        public async Task VerifyDataCommitAsync(Commit commit, Guid branchId, Guid? previousCommitId, IReadOnlyList<(Guid Id, IData Payload)> expected, IReadOnlyCollection<Guid> cascaded = null)
        {
            var projectId = this.state.ProjectId!.Value;

            using (Assert.EnterMultipleScope())
            {
                await this.VerifyCommitRecordAsync(commit, branchId, previousCommitId);

                var (changes, _) = await this.ReadAsync(() => ReadAllPagesAsync(parameters => this.client.GetChangesByProjectCommitAsync(projectId, commit.Id, parameters)), "GetChangesByProjectCommitAsync");

                if (changes == null)
                {
                    return;
                }

                var cascadedChanges = changes.Where(change => change.Identity != null && (cascaded ?? []).Contains(change.Identity.Id) && expected.All(entry => entry.Id != change.Identity.Id)).ToList();
                var edited = cascadedChanges.Count(change => change.Payload != null);

                if (edited > 0)
                {
                    KnownServerDivergences.Report(KnownServerDivergences.DeleteDoesNotCascade, $"the commit also lists {edited} element(s) owned by the deleted element as changed");
                }

                Assert.That(changes.Select(change => change.Identity?.Id), Is.EquivalentTo(expected.Select(entry => (Guid?)entry.Id).Concat(cascadedChanges.Select(change => (Guid?)change.Identity.Id))), "one change per data item");

                var changesById = changes.Where(change => change.Identity != null).GroupBy(change => change.Identity.Id).ToDictionary(group => group.Key, group => group.First());

                foreach (var (id, payload) in expected.Where(entry => changesById.ContainsKey(entry.Id)))
                {
                    if (payload == null)
                    {
                        Assert.That(changesById[id].Payload, Is.Null, $"the change of {id} deletes it");
                    }
                    else
                    {
                        VerifySameData(payload, changesById[id].Payload, $"the payload of the change of {payload.GetType().Name} {id}");
                    }
                }
            }
        }

        /// <summary>
        /// Verifies the diff of two commits: one difference per element created, changed or deleted between them, an
        /// element of the base commit as updated, and an element added since as created
        /// </summary>
        /// <param name="baseCommitId">
        /// The base commit, whose expected model is known
        /// </param>
        /// <param name="compareCommitId">
        /// The compare commit
        /// </param>
        /// <param name="changed">
        /// The elements created or changed between the two commits
        /// </param>
        /// <param name="deleted">
        /// The ids of the elements deleted between the two commits, none when <c>null</c>
        /// </param>
        /// <param name="cascaded">
        /// The ids of the elements deleted with their deleted owner, none when <c>null</c>
        /// </param>
        /// <returns>
        /// An awaitable <see cref="Task"/>
        /// </returns>
        public async Task VerifyDiffAsync(Guid baseCommitId, Guid compareCommitId, IReadOnlyList<IElement> changed, IReadOnlyCollection<Guid> deleted = null, IReadOnlyCollection<Guid> cascaded = null)
        {
            static Guid? IdOf(DataDifference difference) => (difference.CompareData ?? difference.BaseData)?.Identity?.Id;

            static bool IsDeletion(DataDifference difference) => difference.BaseData != null && difference.CompareData == null;

            var projectId = this.state.ProjectId!.Value;
            var notImplemented = false;

            using (Assert.EnterMultipleScope())
            {
                var (differences, _) = await this.ReadAsync(async () =>
                {
                    try
                    {
                        return await ReadAllPagesAsync(parameters => this.client.DiffAsync(projectId, baseCommitId, compareCommitId, parameters));
                    }
                    catch (PsmApiException exception) when (exception.StatusCode == HttpStatusCode.NotImplemented)
                    {
                        notImplemented = true;
                        return (null, 0);
                    }
                }, "DiffAsync");

                if (notImplemented)
                {
                    KnownServerDivergences.Report(KnownServerDivergences.DiffNotImplemented, $"DiffAsync from {baseCommitId} to {compareCommitId} answered 501");
                    return;
                }

                if (differences == null)
                {
                    return;
                }

                var atBase = this.state.ExpectedModels[baseCommitId].Select(element => element.Id).ToHashSet();
                var compared = differences.Where(difference => difference.CompareData?.Identity != null).ToList();
                var cascadedIds = (cascaded ?? []).ToHashSet();
                var cascadedDifferences = differences.Where(difference => IdOf(difference) is { } id && cascadedIds.Contains(id)).ToList();

                Assert.That(differences, Has.None.Matches<DataDifference>(difference => (difference.CompareData ?? difference.BaseData)?.Identity == null), "a difference names the data it is about");
                Assert.That(differences.Select(IdOf), Is.EquivalentTo(changed.Select(element => (Guid?)element.Id).Concat((deleted ?? []).Select(id => (Guid?)id)).Concat(cascadedDifferences.Select(IdOf))));
                Assert.That(differences.Where(difference => (deleted ?? []).Contains(IdOf(difference) ?? Guid.Empty)), Has.All.Matches<DataDifference>(IsDeletion), "a deleted element is DELETED");

                if (cascadedIds.Count > 0 && !cascadedDifferences.Exists(IsDeletion))
                {
                    KnownServerDivergences.Report(KnownServerDivergences.DeleteDoesNotCascade, $"the diff shows none of the {cascadedIds.Count} elements the deleted element owned as DELETED");
                }
                else
                {
                    Assert.That(cascadedDifferences.Where(IsDeletion).Select(IdOf), Is.EquivalentTo(cascadedIds.Select(id => (Guid?)id)), "an element deleted with its owner is DELETED");
                }

                Assert.That(compared.Where(difference => atBase.Contains(difference.CompareData.Identity.Id)), Has.All.Matches<DataDifference>(difference => difference.BaseData != null), "an element of the base commit is UPDATED");
                Assert.That(compared.Where(difference => !atBase.Contains(difference.CompareData.Identity.Id)), Has.All.Matches<DataDifference>(difference => difference.BaseData == null), "an element added since is CREATED");
            }
        }

        /// <summary>
        /// Verifies that a list of elements holds exactly the expected elements
        /// </summary>
        /// <param name="description">
        /// The description of the read in assertion messages
        /// </param>
        /// <param name="readPage">
        /// Reads one page of the list for the given <see cref="QueryParameters"/>
        /// </param>
        /// <param name="expectedIds">
        /// The ids of the elements the list must hold
        /// </param>
        /// <returns>
        /// An awaitable <see cref="Task"/>
        /// </returns>
        public async Task VerifyElementIdsAsync(string description, Func<QueryParameters, Task<PagedResult<IElement>>> readPage, IReadOnlyCollection<Guid> expectedIds)
        {
            var (listed, _) = await this.ReadAsync(() => ReadAllPagesAsync(readPage), description);

            if (listed != null)
            {
                Assert.That(listed.Select(element => element.Id), Is.EquivalentTo(expectedIds), description);
            }
        }

        /// <summary>
        /// Verifies that the history is immutable: an element read at an earlier commit is still the version of that
        /// commit
        /// </summary>
        /// <param name="earlierCommitId">
        /// The earlier commit
        /// </param>
        /// <param name="expected">
        /// The element as it was at <paramref name="earlierCommitId"/>
        /// </param>
        /// <returns>
        /// An awaitable <see cref="Task"/>
        /// </returns>
        public async Task VerifyHistoryAsync(Guid earlierCommitId, IElement expected)
        {
            var description = $"{expected.GetType().Name} {expected.Id} at the earlier commit {earlierCommitId}";
            var (succeeded, actual) = await this.TryReadAsync(() => this.client.GetElementByProjectCommitIdAsync(this.state.ProjectId!.Value, earlierCommitId, expected.Id), $"GetElementByProjectCommitIdAsync of {description}");

            if (succeeded)
            {
                VerifySame(expected, actual, description);
            }
        }

        /// <summary>
        /// Verifies the model at a commit through every element read operation, recording what the server returned:
        /// one changed element of each metaclass is read by id, every element is compared as listed, and the first
        /// pages of the list are read in small pages
        /// </summary>
        /// <remarks>
        /// The list already compares every element, so one read by id per metaclass covers that operation for every
        /// metaclass the commit changed; the membership checks still cover every changed element.
        /// </remarks>
        /// <param name="commitId">
        /// The commit to read the model at
        /// </param>
        /// <param name="expected">
        /// The complete model expected at <paramref name="commitId"/>
        /// </param>
        /// <param name="changed">
        /// The elements that the commit created or changed
        /// </param>
        /// <param name="relationshipSubjectId">
        /// The element whose relationships are read in each direction
        /// </param>
        /// <returns>
        /// An awaitable <see cref="Task"/>
        /// </returns>
        public async Task VerifyModelAsync(Guid commitId, IReadOnlyList<IElement> expected, IReadOnlyList<IElement> changed, Guid relationshipSubjectId)
        {
            var projectId = this.state.ProjectId!.Value;
            var readById = new Dictionary<Guid, IElement>();
            var sample = changed.GroupBy(element => element.GetType()).Select(metaclass => metaclass.First()).ToList();

            using (Assert.EnterMultipleScope())
            {
                var byId = await this.ReadManyAsync(
                    sample,
                    element => this.client.GetElementByProjectCommitIdAsync(projectId, commitId, element.Id),
                    element => $"GetElementByProjectCommitIdAsync of {element.GetType().Name} {element.Id}",
                    (element, call) => call.RequestLine.Contains(element.Id.ToString(), StringComparison.OrdinalIgnoreCase));

                foreach (var (element, actual) in byId.Where(outcome => outcome.Succeeded).Select(outcome => (outcome.Key, outcome.Value)))
                {
                    readById[element.Id] = actual;
                }

                foreach (var element in sample.Where(element => readById.ContainsKey(element.Id)))
                {
                    VerifySame(element, readById[element.Id], $"{element.GetType().Name} {element.Id}");
                }

                var (listed, _) = await this.ReadAsync(() => ReadAllPagesAsync(parameters => this.client.GetElementsByProjectCommitAsync(projectId, commitId, queryParameters: parameters)), "GetElementsByProjectCommitAsync");

                this.state.ReadBack.AddRange((listed ?? [.. readById.Values]).Select(element => (commitId, (IData)element)));

                var listedById = (listed ?? []).GroupBy(element => element.Id).ToDictionary(group => group.Key, group => group.First());

                if (listed != null)
                {
                    Assert.That(listed.Select(element => element.Id), Is.EquivalentTo(expected.Select(element => element.Id)));

                    foreach (var element in expected.Where(element => !readById.ContainsKey(element.Id) && listedById.ContainsKey(element.Id)))
                    {
                        VerifySame(element, listedById[element.Id], $"{element.GetType().Name} {element.Id} as listed");
                    }
                }

                await this.VerifyPagingAsync(commitId, expected);

                var (roots, _) = await this.ReadAsync(() => ReadAllPagesAsync(parameters => this.client.GetRootsByProjectCommitAsync(projectId, commitId, queryParameters: parameters)), "GetRootsByProjectCommitAsync");

                if (roots != null)
                {
                    VerifyRoots(roots, expected);
                }

                var readBack = changed
                    .Select(element => readById.GetValueOrDefault(element.Id) ?? listedById.GetValueOrDefault(element.Id))
                    .Where(element => element != null)
                    .ToDictionary(element => element.Id);

                VerifyMemberships([.. changed.Where(element => readBack.ContainsKey(element.Id))], readBack);
                VerifyNoEmptyReferences(readBack.Values);

                await this.VerifyRelationshipsAsync(commitId, expected, relationshipSubjectId);
            }
        }

        /// <summary>
        /// Verifies the project usage that brings an element of a used project into a commit
        /// </summary>
        /// <param name="commitId">
        /// The commit
        /// </param>
        /// <param name="elementId">
        /// The element of the used project
        /// </param>
        /// <param name="expected">
        /// The project usage that was committed
        /// </param>
        /// <returns>
        /// An awaitable <see cref="Task"/>
        /// </returns>
        public async Task VerifyProjectUsageAsync(Guid commitId, Guid elementId, ProjectUsage expected)
        {
            var (succeeded, actual) = await this.TryReadAsync(() => this.client.GetProjectUsageByProjectCommitElementAsync(this.state.ProjectId!.Value, commitId, elementId), $"GetProjectUsageByProjectCommitElementAsync of {elementId}");

            if (succeeded)
            {
                VerifySameData(expected, actual, $"the project usage of {elementId}");
            }
        }

        /// <summary>
        /// Verifies the results of a query: exactly the expected elements, each equal to the element that was sent
        /// </summary>
        /// <param name="description">
        /// The description of the read in assertion messages
        /// </param>
        /// <param name="readPage">
        /// Reads one page of the results for the given <see cref="QueryParameters"/>
        /// </param>
        /// <param name="expected">
        /// The elements the results must hold
        /// </param>
        /// <returns>
        /// An awaitable <see cref="Task"/>
        /// </returns>
        public async Task VerifyResultsAsync(string description, Func<QueryParameters, Task<PagedResult<IData>>> readPage, IReadOnlyList<IElement> expected)
        {
            var (results, _) = await this.ReadAsync(() => ReadAllPagesAsync(readPage), description);

            if (results == null)
            {
                return;
            }

            using (Assert.EnterMultipleScope())
            {
                Assert.That(results.Select(result => result.Id), Is.EquivalentTo(expected.Select(element => element.Id)), description);

                var resultsById = results.GroupBy(result => result.Id).ToDictionary(group => group.Key, group => group.First());

                foreach (var element in expected.Where(element => resultsById.ContainsKey(element.Id)))
                {
                    VerifySame(element, resultsById[element.Id] as IElement, $"{element.GetType().Name} {element.Id} in {description}");
                }
            }
        }

        /// <summary>
        /// Verifies a list of roots against a model: the elements without an <c>owner</c>, or the known divergence
        /// <see cref="KnownServerDivergences.RootsIgnoreOwningRelationship"/>
        /// </summary>
        /// <param name="description">
        /// The description of the read in assertion messages
        /// </param>
        /// <param name="readPage">
        /// Reads one page of the roots for the given <see cref="QueryParameters"/>
        /// </param>
        /// <param name="model">
        /// The model whose roots the list must hold
        /// </param>
        /// <returns>
        /// An awaitable <see cref="Task"/>
        /// </returns>
        public async Task VerifyRootsAsync(string description, Func<QueryParameters, Task<PagedResult<IElement>>> readPage, IReadOnlyList<IElement> model)
        {
            var (roots, _) = await this.ReadAsync(() => ReadAllPagesAsync(readPage), description);

            if (roots != null)
            {
                VerifyRoots(roots, model);
            }
        }

        /// <summary>
        /// Reads pages of a list operation, following <see cref="PagedResult{T}.Next"/>, up to a number of pages
        /// </summary>
        /// <typeparam name="T">
        /// The type of the records
        /// </typeparam>
        /// <param name="readPage">
        /// Reads one page for the given <see cref="QueryParameters"/>
        /// </param>
        /// <param name="pageSize">
        /// The page size to request
        /// </param>
        /// <param name="pageLimit">
        /// The number of pages after which reading stops
        /// </param>
        /// <returns>
        /// The records of the pages read, in order, the number of pages read, and whether the list ended
        /// </returns>
        /// <exception cref="InvalidOperationException">
        /// Thrown when the server repeats a page cursor
        /// </exception>
        private static async Task<(IReadOnlyList<T> Records, int Pages, bool Ended)> ReadPagesAsync<T>(Func<QueryParameters, Task<PagedResult<T>>> readPage, int pageSize, int pageLimit)
        {
            var records = new List<T>();
            var cursors = new HashSet<string>();
            var parameters = new QueryParameters { PageSize = pageSize };

            for (var pages = 1; pages <= pageLimit; pages++)
            {
                var page = await readPage(parameters);
                records.AddRange(page.Records);

                if (page.Next == null)
                {
                    return (records, pages, true);
                }

                if (!cursors.Add(page.Next.Value.Token))
                {
                    throw new InvalidOperationException($"The server repeated the page cursor {page.Next.Value.Token} after {pages} pages");
                }

                parameters = new QueryParameters { PageSize = pageSize, PageAfter = page.Next };
            }

            return (records, pageLimit, false);
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
        private static void VerifyMemberships(IReadOnlyList<IElement> expected, Dictionary<Guid, IElement> readById)
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
        /// <remarks>
        /// KerML derives <c>owner</c> as <c>owningRelationship.owningRelatedElement</c>, so an element has no owner exactly
        /// when it has no <c>owningRelationship</c>, which is not derived and is known for every element sent.
        /// </remarks>
        /// <param name="roots">
        /// The roots read back
        /// </param>
        /// <param name="model">
        /// The model that was sent
        /// </param>
        private static void VerifyRoots(IReadOnlyList<IElement> roots, IReadOnlyList<IElement> model)
        {
            var rootIds = roots.Select(element => element.Id).ToHashSet();
            var unowned = model.Where(element => element.OwningRelationship == null).Select(element => element.Id).ToList();
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
        /// Verifies that data read back is of the kind that was sent and equal to it, naming the properties that differ;
        /// an element is compared as in <see cref="VerifySame"/>
        /// </summary>
        /// <param name="sent">
        /// The data that was sent
        /// </param>
        /// <param name="actual">
        /// The data read back, or <c>null</c>
        /// </param>
        /// <param name="description">
        /// The description of the data in assertion messages
        /// </param>
        private static void VerifySameData(IData sent, IData actual, string description)
        {
            if (sent is IElement element)
            {
                VerifySame(element, actual as IElement, description);
                return;
            }

            Assert.That(actual, Is.TypeOf(sent.GetType()), $"{description} was read back as another kind of data");

            if (actual?.GetType() != sent.GetType())
            {
                return;
            }

            var differing = sent.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .Where(property => !Equals(property.GetValue(sent), property.GetValue(actual)))
                .Select(property => property.Name)
                .ToList();

            Assert.That(differing, Is.Empty, $"{description} differs from what was sent");
        }

        /// <summary>
        /// Reads from the server, reporting a read that throws as a failure, so that the checks that do not depend on
        /// it still run
        /// </summary>
        /// <typeparam name="T">
        /// The type of what is read
        /// </typeparam>
        /// <param name="read">
        /// The read
        /// </param>
        /// <param name="description">
        /// The description of the read in the failure message
        /// </param>
        /// <returns>
        /// What was read, or the default of <typeparamref name="T"/> when the read threw
        /// </returns>
        private async Task<T> ReadAsync<T>(Func<Task<T>> read, string description)
        {
            var (_, value) = await this.TryReadAsync(read, description);

            return value;
        }

        /// <summary>
        /// Runs independent reads, <see cref="ReadConcurrency"/> at a time, and reports every read that throws once all
        /// have ended, so that every assertion runs on the context of the test
        /// </summary>
        /// <typeparam name="TKey">
        /// The type of what a read is about
        /// </typeparam>
        /// <typeparam name="T">
        /// The type of what is read
        /// </typeparam>
        /// <param name="keys">
        /// What the reads are about, one read each
        /// </param>
        /// <param name="read">
        /// The read for a key
        /// </param>
        /// <param name="describe">
        /// The description of the read for a key in the failure message
        /// </param>
        /// <param name="isCallOf">
        /// Whether a recorded call is the request of the read for a key; concurrent reads share the recorder
        /// </param>
        /// <returns>
        /// Per key, in order, whether its read succeeded and what it read
        /// </returns>
        private async Task<IReadOnlyList<(TKey Key, bool Succeeded, T Value)>> ReadManyAsync<TKey, T>(IReadOnlyList<TKey> keys, Func<TKey, Task<T>> read, Func<TKey, string> describe, Func<TKey, EndpointCall, bool> isCallOf)
        {
            var callsBefore = this.recordedCalls?.Invoke().Count ?? 0;

            using var gate = new SemaphoreSlim(ReadConcurrency);

            var outcomes = await Task.WhenAll(keys.Select(async key =>
            {
                await gate.WaitAsync();

                try
                {
                    return (Key: key, Failure: (Exception)null, Value: await read(key));
                }
                catch (Exception exception) when (exception is not ResultStateException)
                {
                    return (Key: key, Failure: exception, Value: default(T));
                }
                finally
                {
                    gate.Release();
                }
            }));

            var recorded = (this.recordedCalls?.Invoke() ?? []).Skip(callsBefore).ToList();

            foreach (var (key, failure, _) in outcomes.Where(outcome => outcome.Failure != null))
            {
                this.ReportFailedRead(describe(key), failure, recorded.Where(call => isCallOf(key, call)));
            }

            return [.. outcomes.Select(outcome => (outcome.Key, outcome.Failure == null, outcome.Value))];
        }

        /// <summary>
        /// Reports a read that threw: as a warning when one of its calls got a response showing a known divergence that
        /// breaks deserialization, which <see cref="OpenApiConformance"/> reports, and as a failure otherwise
        /// </summary>
        /// <remarks>
        /// Every Flexo response breaks the document in some tolerated way, so only a divergence that breaks
        /// deserialization excuses a failed read; a client defect must still fail.
        /// </remarks>
        /// <param name="description">
        /// The description of the read
        /// </param>
        /// <param name="exception">
        /// What the read threw
        /// </param>
        /// <param name="calls">
        /// The calls that the read made
        /// </param>
        private void ReportFailedRead(string description, Exception exception, IEnumerable<EndpointCall> calls)
        {
            var failure = $"{description} threw {exception.GetType().Name}: {exception.Message}";

            var excused = calls
                .SelectMany(call => call.ResponseViolations.Select(violation => (Call: call, Divergence: this.divergences.FirstOrDefault(divergence => divergence.BreaksDeserialization && divergence.Explains?.Invoke(call, violation) == true))))
                .FirstOrDefault(entry => entry.Divergence != null);

            if (excused.Divergence != null)
            {
                Assert.Warn($"{failure}, on a response that shows the known divergence that {excused.Divergence.Summary} ({excused.Call.RequestLine}); the OpenAPI conformance check reports the response");
            }
            else
            {
                Assert.Fail(failure);
            }
        }

        /// <summary>
        /// Reads from the server, reporting a read that throws as a failure, or as a warning when a response of that
        /// read shows a known divergence that breaks deserialization, which <see cref="OpenApiConformance"/> reports
        /// </summary>
        /// <remarks>
        /// Only the calls recorded while the read ran count, so that a response of an earlier read excuses nothing.
        /// </remarks>
        /// <typeparam name="T">
        /// The type of what is read
        /// </typeparam>
        /// <param name="read">
        /// The read
        /// </param>
        /// <param name="description">
        /// The description of the read in the failure message
        /// </param>
        /// <returns>
        /// Whether the read succeeded, and what was read
        /// </returns>
        private async Task<(bool Succeeded, T Value)> TryReadAsync<T>(Func<Task<T>> read, string description)
        {
            var callsBefore = this.recordedCalls?.Invoke().Count ?? 0;

            try
            {
                return (true, await read());
            }
            catch (Exception exception) when (exception is not ResultStateException)
            {
                this.ReportFailedRead(description, exception, (this.recordedCalls?.Invoke() ?? []).Skip(callsBefore));
                return (false, default);
            }
        }

        /// <summary>
        /// Verifies the record of a commit: read by id it is the commit, in the project, after the previous head; it is
        /// listed; and it is the head of its branch
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
        /// <returns>
        /// An awaitable <see cref="Task"/>
        /// </returns>
        private async Task VerifyCommitRecordAsync(Commit commit, Guid branchId, Guid? previousCommitId)
        {
            var projectId = this.state.ProjectId!.Value;

            Guid[] expectedPrevious = previousCommitId.HasValue ? [previousCommitId.Value] : [];

            var commitById = await this.ReadAsync(() => this.client.GetCommitByProjectAndIdAsync(projectId, commit.Id), "GetCommitByProjectAndIdAsync");

            if (commitById != null)
            {
                Assert.That(commitById.Id, Is.EqualTo(commit.Id));
                Assert.That(commitById.OwningProject, Is.EqualTo(projectId));
                Assert.That(commitById.PreviousCommit, Is.EqualTo(expectedPrevious));
            }

            var (commits, _) = await this.ReadAsync(() => ReadAllPagesAsync(parameters => this.client.GetCommitsByProjectAsync(projectId, parameters)), "GetCommitsByProjectAsync");

            if (commits != null)
            {
                Assert.That(commits.Select(record => record.Id), Does.Contain(commit.Id));
            }

            var branch = await this.ReadAsync(() => this.client.GetBranchesByProjectAndIdAsync(projectId, branchId), "GetBranchesByProjectAndIdAsync");

            if (branch != null)
            {
                Assert.That(branch.Head, Is.EqualTo(commit.Id));
            }
        }

        /// <summary>
        /// Verifies paging on the first <see cref="PagingWalkPages"/> small pages of the element list: the model spans
        /// several pages, and the pages read hold distinct elements of the model
        /// </summary>
        /// <param name="commitId">
        /// The commit to read at
        /// </param>
        /// <param name="expected">
        /// The complete model expected at <paramref name="commitId"/>
        /// </param>
        /// <returns>
        /// An awaitable <see cref="Task"/>
        /// </returns>
        private async Task VerifyPagingAsync(Guid commitId, IReadOnlyList<IElement> expected)
        {
            var projectId = this.state.ProjectId!.Value;
            var (walked, pages, _) = await this.ReadAsync(() => ReadPagesAsync(parameters => this.client.GetElementsByProjectCommitAsync(projectId, commitId, queryParameters: parameters), PagingPageSize, PagingWalkPages), "GetElementsByProjectCommitAsync in small pages");

            if (walked == null)
            {
                return;
            }

            var expectedIds = expected.Select(element => element.Id).ToHashSet();

            if (expected.Count > PagingPageSize)
            {
                Assert.That(pages, Is.GreaterThan(1), "the elements did not span several pages, so paging is untested");
            }

            Assert.That(walked.Select(element => element.Id), Is.Unique.And.All.Matches<Guid>(expectedIds.Contains), "the pages hold distinct elements of the model");
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

            (Direction Direction, List<Guid> Expected)[] directions = [(Direction.@out, outgoing), (Direction.@in, incoming), (Direction.both, [.. outgoing.Union(incoming)])];

            using (Assert.EnterMultipleScope())
            {
                Assert.That(outgoing, Is.Not.Empty, "the subject is the source of no relationship, so the out direction is untested");
                Assert.That(incoming, Is.Not.Empty, "the subject is the target of no relationship, so the in direction is untested");

                var reads = await this.ReadManyAsync(
                    directions,
                    entry => ReadAllPagesAsync(parameters => this.client.GetRelationshipsByProjectCommitRelatedElementAsync(projectId, commitId, subjectId, entry.Direction, queryParameters: parameters)),
                    entry => $"GetRelationshipsByProjectCommitRelatedElementAsync {entry.Direction}",
                    (entry, call) => call.RequestLine.Contains($"direction={entry.Direction}", StringComparison.Ordinal));

                foreach (var ((direction, expectedIds), _, (read, _)) in reads.Where(outcome => outcome.Succeeded))
                {
                    Assert.That(read.Select(relationship => relationship.Id), Is.EquivalentTo(expectedIds), $"the {direction} relationships");
                }
            }
        }
    }
}
