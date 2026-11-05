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

    using SysML2.NET.PSM;
    using SysML2.NET.PSM.DTO;

    /// <summary>
    /// Definition of the <see cref="ISession"/> that holds the connection to a server of the Systems Modeling API and Services
    /// </summary>
    public interface ISession
    {
        /// <summary>
        /// Gets the <see cref="IPsmApiClient"/> of the open connection, or <c>null</c> when the session is closed
        /// </summary>
        IPsmApiClient Client { get; }

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

        /// <summary>
        /// Closes the connection to the server
        /// </summary>
        void Close();
    }
}
