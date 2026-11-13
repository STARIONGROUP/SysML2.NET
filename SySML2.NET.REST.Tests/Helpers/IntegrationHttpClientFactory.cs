// ------------------------------------------------------------------------------------------------
// <copyright file="IntegrationHttpClientFactory.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SySML2.NET.REST.Tests.Helpers
{
    using System;
    using System.Net.Http;

    /// <summary>
    /// An <see cref="IHttpClientFactory"/> whose clients share one handler chain: a <see cref="BearerTokenHandler"/>
    /// in front of an <see cref="EndpointRecorder"/>
    /// </summary>
    public sealed class IntegrationHttpClientFactory : IHttpClientFactory, IDisposable
    {
        /// <summary>
        /// The first handler of the shared chain
        /// </summary>
        private readonly HttpMessageHandler handler;

        /// <summary>
        /// Initializes a new instance of the <see cref="IntegrationHttpClientFactory"/> class
        /// </summary>
        /// <param name="token">
        /// The bearer token added to every request
        /// </param>
        /// <param name="openApi">
        /// The OpenAPI operations that requests are matched against
        /// </param>
        public IntegrationHttpClientFactory(string token, OpenApiDocument openApi)
        {
            this.Recorder = new EndpointRecorder(openApi, new HttpClientHandler());
            this.handler = new BearerTokenHandler(token, this.Recorder);
        }

        /// <summary>
        /// Gets the <see cref="EndpointRecorder"/> of the shared chain
        /// </summary>
        public EndpointRecorder Recorder { get; }

        /// <summary>
        /// Creates an <see cref="HttpClient"/> over the shared chain; disposing the client leaves the chain intact
        /// </summary>
        /// <param name="name">
        /// The logical name of the client, ignored
        /// </param>
        /// <returns>
        /// The <see cref="HttpClient"/>
        /// </returns>
        public HttpClient CreateClient(string name) => new(this.handler, false);

        /// <summary>
        /// Disposes the shared handler chain
        /// </summary>
        public void Dispose() => this.handler.Dispose();
    }
}
