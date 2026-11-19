// ------------------------------------------------------------------------------------------------
// <copyright file="ISession.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SySML2.NET.REST
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using SysML2.NET.Dal;
    using SysML2.NET.PSM;
    using SysML2.NET.PSM.DTO;

    /// <summary>
    /// Definition of the <see cref="ISession"/> that holds the connection to a server of the Systems Modeling API and Services
    /// </summary>
    public interface ISession
    {
        /// <summary>
        /// Gets the <see cref="IAssembler"/> that holds the element model of the current commit
        /// </summary>
        IAssembler Assembler { get; }

        /// <summary>
        /// Gets the <see cref="IPsmApiClient"/> of the open connection, or <c>null</c> when the session is closed
        /// </summary>
        IPsmApiClient Client { get; }

        /// <summary>
        /// Gets the project and commit whose element model the session holds, or <c>null</c> when it holds none
        /// </summary>
        (Guid ProjectId, Guid CommitId)? CurrentCommit { get; }

        /// <summary>
        /// Closes the connection to the server and drops the element model it read
        /// </summary>
        void Close();

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
        Task LoadBranchHead(Guid projectId, Guid branchId, CancellationToken cancellationToken);

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
        Task LoadCommit(Guid projectId, Guid commitId, CancellationToken cancellationToken);

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
        Task<PagedResult<Project>> Open(Uri baseAddress, CancellationToken cancellationToken);
    }
}
