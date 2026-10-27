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
    using System.Threading;
    using System.Threading.Tasks;
    
    /// <summary>
    /// Definition of the <see cref="ISession"/> used to provide an abstraction over the SysML2 model server REST API
    /// </summary>
    public interface ISession
    {
        /// <summary>
        /// Opens a connection to the SysML2 model server REST API
        /// </summary>
        /// <param name="username">The username used to login a SysML2 model server</param>
        /// <param name="password">The password (secret) used to login a SysML2 model server</param>
        /// <param name="uri">
        /// The uri of the model-server
        /// </param>
        /// <param name="cancellationToken">
        /// The <see cref="CancellationToken"/> used to cancel the operation
        /// </param>
        Task Open(string username, string password, string uri, CancellationToken cancellationToken);

        /// <summary>
        /// Closes a connection to the SysML2 model server REST API
        /// </summary>
        void Close();
    }
}
