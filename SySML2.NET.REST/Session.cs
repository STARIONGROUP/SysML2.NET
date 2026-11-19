// ------------------------------------------------------------------------------------------------
// <copyright file="Session.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SySML2.NET.REST
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;

    using Microsoft.Extensions.Logging;

    using SysML2.NET.Core.DTO.Root.Elements;
    using SysML2.NET.Dal;
    using SysML2.NET.PSM;
    using SysML2.NET.PSM.DTO;
    using SysML2.NET.Serializer.Json;

    /// <summary>
    /// The <see cref="Session"/> holds the connection to a server of the Systems Modeling API and Services and the element model of its current commit
    /// </summary>
    public class Session : ISession
    {
        /// <summary>
        /// The <see cref="IHttpClientFactory"/> that creates a new <see cref="HttpClient"/> for each opened connection
        /// </summary>
        private readonly IHttpClientFactory httpClientFactory;

        /// <summary>
        /// The <see cref="IDeSerializer"/> handed to each <see cref="PsmApiClient"/>
        /// </summary>
        private readonly IDeSerializer deSerializer;

        /// <summary>
        /// The <see cref="ISerializer"/> handed to each <see cref="PsmApiClient"/>
        /// </summary>
        private readonly ISerializer serializer;

        /// <summary>
        /// The <see cref="ILoggerFactory"/> handed to each <see cref="PsmApiClient"/>
        /// </summary>
        private readonly ILoggerFactory loggerFactory;

        /// <summary>
        /// The lock that keeps the <see cref="Assembler"/>, <see cref="loadedCommit"/> and <see cref="generation"/> consistent between a read and <see cref="Close"/>
        /// </summary>
        private readonly object gate = new();

        /// <summary>
        /// The project and commit loaded in the <see cref="Assembler"/>, with a <c>null</c> commit for a branch without commit, or <c>null</c> when none is loaded
        /// </summary>
        private (Guid ProjectId, Guid? CommitId)? loadedCommit;

        /// <summary>
        /// The number of times the session was closed, so that a model read before a close is not loaded after it
        /// </summary>
        private int generation;

        /// <summary>
        /// Initializes a new instance of the <see cref="Session"/> class
        /// </summary>
        /// <param name="httpClientFactory">
        /// The <see cref="IHttpClientFactory"/> that creates a new <see cref="HttpClient"/> for each opened connection
        /// </param>
        /// <param name="deSerializer">
        /// The <see cref="IDeSerializer"/> that reads the response payloads
        /// </param>
        /// <param name="serializer">
        /// The <see cref="ISerializer"/> that writes the request payloads
        /// </param>
        /// <param name="assembler">
        /// The <see cref="IAssembler"/> that holds the element model of the current commit
        /// </param>
        /// <param name="loggerFactory">
        /// The <see cref="ILoggerFactory"/> used to set up logging, or <c>null</c> to disable logging
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="httpClientFactory"/>, <paramref name="deSerializer"/>, <paramref name="serializer"/> or <paramref name="assembler"/> is <c>null</c>
        /// </exception>
        public Session(IHttpClientFactory httpClientFactory, IDeSerializer deSerializer, ISerializer serializer, IAssembler assembler, ILoggerFactory loggerFactory = null)
        {
            this.httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
            this.deSerializer = deSerializer ?? throw new ArgumentNullException(nameof(deSerializer));
            this.serializer = serializer ?? throw new ArgumentNullException(nameof(serializer));
            this.Assembler = assembler ?? throw new ArgumentNullException(nameof(assembler));
            this.loggerFactory = loggerFactory;
        }

        /// <summary>
        /// Gets the <see cref="IAssembler"/> that holds the element model of the current commit
        /// </summary>
        public IAssembler Assembler { get; }

        /// <summary>
        /// Gets the <see cref="IPsmApiClient"/> of the open connection, or <c>null</c> when the session is closed
        /// </summary>
        public IPsmApiClient Client { get; private set; }

        /// <summary>
        /// Gets the project and commit whose element model the session holds, or <c>null</c> when it holds none
        /// </summary>
        public (Guid ProjectId, Guid CommitId)? CurrentCommit
        {
            get
            {
                lock (this.gate)
                {
                    return this.loadedCommit is { CommitId: { } commitId } loaded ? (loaded.ProjectId, commitId) : null;
                }
            }
        }

        /// <summary>
        /// Closes the connection to the server and drops the element model it read
        /// </summary>
        public void Close()
        {
            lock (this.gate)
            {
                this.generation++;
                this.loadedCommit = null;
                this.Assembler.Synchronize([], this.Assembler.Cache.Keys.ToList());
            }

            this.Client = null;
        }

        /// <summary>
        /// Loads the element model of the head of a branch into the <see cref="Assembler"/>, leaving it empty when the branch has no commit
        /// </summary>
        /// <param name="projectId">
        /// The identifier of the project
        /// </param>
        /// <param name="branchId">
        /// The identifier of the branch
        /// </param>
        /// <param name="cancellationToken">
        /// The <see cref="CancellationToken"/> that cancels the operation
        /// </param>
        /// <returns>
        /// An awaitable <see cref="Task"/>
        /// </returns>
        /// <exception cref="InvalidOperationException">
        /// Thrown when the session is closed, or when the server links a page of elements that it already returned
        /// </exception>
        public async Task LoadBranchHead(Guid projectId, Guid branchId, CancellationToken cancellationToken)
        {
            var client = this.QueryOpenClient();
            var readGeneration = Volatile.Read(ref this.generation);

            var branch = await client.GetBranchesByProjectAndIdAsync(projectId, branchId, cancellationToken);

            if (branch.Head is { } headId)
            {
                await this.LoadCommit(projectId, headId, cancellationToken);
                return;
            }

            this.Load((projectId, null), [], readGeneration);
        }

        /// <summary>
        /// Loads the element model of a commit into the <see cref="Assembler"/>, read once per commit since a commit is immutable (Systems Modeling API and Services §7.1.2)
        /// </summary>
        /// <param name="projectId">
        /// The identifier of the project
        /// </param>
        /// <param name="commitId">
        /// The identifier of the commit
        /// </param>
        /// <param name="cancellationToken">
        /// The <see cref="CancellationToken"/> that cancels the operation
        /// </param>
        /// <returns>
        /// An awaitable <see cref="Task"/>
        /// </returns>
        /// <exception cref="InvalidOperationException">
        /// Thrown when the session is closed, or when the server links a page of elements that it already returned
        /// </exception>
        public async Task LoadCommit(Guid projectId, Guid commitId, CancellationToken cancellationToken)
        {
            var client = this.QueryOpenClient();

            if (this.CurrentCommit == (projectId, commitId))
            {
                return;
            }

            var readGeneration = Volatile.Read(ref this.generation);
            var elements = await ReadElementsAsync(client, projectId, commitId, cancellationToken);

            this.Load((projectId, commitId), elements, readGeneration);
        }

        /// <summary>
        /// Opens a connection to the server, replacing any open connection, and reads the first page of its projects
        /// </summary>
        /// <param name="baseAddress">
        /// The absolute base address of the server
        /// </param>
        /// <param name="cancellationToken">
        /// The <see cref="CancellationToken"/> that cancels the operation
        /// </param>
        /// <returns>
        /// The first page of the projects of the server
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="baseAddress"/> is <c>null</c>
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown when <paramref name="baseAddress"/> is not absolute
        /// </exception>
        public async Task<PagedResult<Project>> Open(Uri baseAddress, CancellationToken cancellationToken)
        {
#if NET6_0_OR_GREATER
            ArgumentNullException.ThrowIfNull(baseAddress);
#else
            if (baseAddress is null)
            {
                throw new ArgumentNullException(nameof(baseAddress));
            }
#endif

            if (!baseAddress.IsAbsoluteUri)
            {
                throw new ArgumentException("The base address must be absolute", nameof(baseAddress));
            }

            this.Close();

            var httpClient = this.httpClientFactory.CreateClient(nameof(Session));

            // a relative request URI drops the last path segment of a base address that does not end with a slash
            httpClient.BaseAddress = baseAddress.AbsoluteUri.EndsWith('/') ? baseAddress : new Uri($"{baseAddress.AbsoluteUri}/");

            var client = new PsmApiClient(httpClient, this.deSerializer, this.serializer, this.loggerFactory);

            var projects = await client.GetProjectsAsync(cancellationToken: cancellationToken);

            this.Client = client;

            return projects;
        }

        /// <summary>
        /// Reads every page of the elements of a commit
        /// </summary>
        /// <param name="client">
        /// The <see cref="IPsmApiClient"/> of the open connection
        /// </param>
        /// <param name="projectId">
        /// The identifier of the project
        /// </param>
        /// <param name="commitId">
        /// The identifier of the commit
        /// </param>
        /// <param name="cancellationToken">
        /// The <see cref="CancellationToken"/> that cancels the operation
        /// </param>
        /// <returns>
        /// The elements of the commit
        /// </returns>
        /// <exception cref="InvalidOperationException">
        /// Thrown when the server links a page whose cursor it already returned
        /// </exception>
        private static async Task<List<IElement>> ReadElementsAsync(IPsmApiClient client, Guid projectId, Guid commitId, CancellationToken cancellationToken)
        {
            var elements = new List<IElement>();
            var readCursors = new HashSet<string>(StringComparer.Ordinal);
            var page = await client.GetElementsByProjectCommitAsync(projectId, commitId, cancellationToken: cancellationToken);
            elements.AddRange(page.Records);

            while (page.Next is { } next)
            {
                if (!readCursors.Add(next.Token))
                {
                    throw new InvalidOperationException("The server linked a page of elements that it already returned");
                }

                page = await client.GetElementsByProjectCommitAsync(projectId, commitId, queryParameters: new QueryParameters { PageAfter = next }, cancellationToken: cancellationToken);
                elements.AddRange(page.Records);
            }

            return elements;
        }

        /// <summary>
        /// Replaces the content of the <see cref="Assembler"/> with the elements of a commit, unless the session was closed while they were read
        /// </summary>
        /// <param name="commit">
        /// The project and commit of the <paramref name="elements"/>, with a <c>null</c> commit for a branch without commit
        /// </param>
        /// <param name="elements">
        /// The elements of the commit
        /// </param>
        /// <param name="readGeneration">
        /// The <see cref="generation"/> when the read started
        /// </param>
        private void Load((Guid ProjectId, Guid? CommitId) commit, IReadOnlyCollection<IElement> elements, int readGeneration)
        {
            lock (this.gate)
            {
                if (this.generation != readGeneration)
                {
                    return;
                }

                var keptIdentifiers = elements.Select(element => element.Id).ToHashSet();
                this.Assembler.Synchronize(elements, this.Assembler.Cache.Keys.Where(identifier => !keptIdentifiers.Contains(identifier)).ToList());
                this.loadedCommit = commit;
            }
        }

        /// <summary>
        /// Queries the <see cref="IPsmApiClient"/> of the open connection
        /// </summary>
        /// <returns>
        /// The <see cref="Client"/>
        /// </returns>
        /// <exception cref="InvalidOperationException">
        /// Thrown when the session is closed
        /// </exception>
        private IPsmApiClient QueryOpenClient() => this.Client ?? throw new InvalidOperationException("The session is closed");
    }
}
