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
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;

    using Microsoft.Extensions.Logging;

    using SysML2.NET.PSM;
    using SysML2.NET.PSM.DTO;
    using SysML2.NET.Serializer.Json;

    /// <summary>
    /// The <see cref="Session"/> holds the connection to a server of the Systems Modeling API and Services
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
        /// <param name="loggerFactory">
        /// The <see cref="ILoggerFactory"/> used to set up logging, or <c>null</c> to disable logging
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="httpClientFactory"/>, <paramref name="deSerializer"/> or <paramref name="serializer"/> is <c>null</c>
        /// </exception>
        public Session(IHttpClientFactory httpClientFactory, IDeSerializer deSerializer, ISerializer serializer, ILoggerFactory loggerFactory = null)
        {
            this.httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
            this.deSerializer = deSerializer ?? throw new ArgumentNullException(nameof(deSerializer));
            this.serializer = serializer ?? throw new ArgumentNullException(nameof(serializer));
            this.loggerFactory = loggerFactory;
        }

        /// <summary>
        /// Gets the <see cref="IPsmApiClient"/> of the open connection, or <c>null</c> when the session is closed
        /// </summary>
        public IPsmApiClient Client { get; private set; }

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
        /// Closes the connection to the server
        /// </summary>
        public void Close()
        {
            this.Client = null;
        }
    }
}
