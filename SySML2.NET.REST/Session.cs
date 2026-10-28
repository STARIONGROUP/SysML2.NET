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

    using SysML2.NET.PIM;
    
    /// <summary>
    /// The purpose of the <see cref="Session"/> class used to provide an abstraction over the REST API
    /// </summary>
    public class Session : ISession
    {
        /// <summary>
        /// The base uri or hostname of the model server REST API
        /// </summary>
        private string baseUri;

        /// <summary>
        /// Dependency injected <see cref="IRestClient"/> used to perform HTTP requests
        /// </summary>
        private readonly IRestClient restClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="Session"/> class
        /// </summary>
        /// <param name="restClient">
        /// Dependency injected <see cref="IRestClient"/> used to perform HTTP requests
        /// </param>
        public Session(IRestClient restClient)
        {
            this.restClient = restClient;
        }

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
        public async Task Open(string username, string password, string uri, CancellationToken cancellationToken)
        {
            try
            {
                this.baseUri = uri;

                await this.restClient.Open(username, password, this.baseUri, cancellationToken);
            }
            catch (Exception)
            {
                this.baseUri = null;
                throw;
            }
        }

        /// <summary>
        /// Closes a connection to the SysML2 model server REST API
        /// </summary>
        public void Close()
        {
            this.baseUri = null;
        }
    }
}
