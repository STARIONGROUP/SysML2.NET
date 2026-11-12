// ------------------------------------------------------------------------------------------------
// <copyright file="LiveServerTestHost.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SySML2.NET.REST.Tests.Integration
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;

    using Microsoft.Extensions.Logging;

    using NUnit.Framework;

    using SysML2.NET.Serializer.Json;

    /// <summary>
    /// Creates an <see cref="IPsmApiClient"/> on the Systems Modeling API server named by <see cref="BaseAddressVariable"/>,
    /// ignoring the owning fixture when no server is configured or reachable, and failing it when the server refuses the probe
    /// </summary>
    public sealed class LiveServerTestHost : IDisposable
    {
        /// <summary>
        /// The name of the environment variable that holds the absolute base address of the server
        /// </summary>
        private const string BaseAddressVariable = "SYSML2_API_BASE_ADDRESS";

        /// <summary>
        /// The <see cref="HttpClient"/> used by the <see cref="Client"/>
        /// </summary>
        private readonly HttpClient httpClient = new() { Timeout = TimeSpan.FromSeconds(180) };

        /// <summary>
        /// The <see cref="ILoggerFactory"/> that writes the warnings of the client to the console
        /// </summary>
        private readonly ILoggerFactory loggerFactory = LoggerFactory.Create(builder => builder.AddSimpleConsole());

        /// <summary>
        /// Gets the <see cref="IPsmApiClient"/> bound to the server
        /// </summary>
        public IPsmApiClient Client { get; private set; }

        /// <summary>
        /// Disposes the <see cref="HttpClient"/> and the <see cref="ILoggerFactory"/>
        /// </summary>
        public void Dispose()
        {
            this.httpClient.Dispose();
            this.loggerFactory.Dispose();
        }

        /// <summary>
        /// Creates the client, ignoring the owning fixture when the server is not configured or not reachable,
        /// and failing it when the server answers the probe with any status other than 200 OK
        /// </summary>
        /// <returns>
        /// An awaitable <see cref="Task"/>
        /// </returns>
        public async Task OpenAsync()
        {
            if (!Uri.TryCreate(Environment.GetEnvironmentVariable(BaseAddressVariable), UriKind.Absolute, out var baseAddress))
            {
                Assert.Ignore($"Set {BaseAddressVariable} to the absolute base address of a Systems Modeling API server to run the live tests");
            }

            this.httpClient.BaseAddress = baseAddress.AbsoluteUri.EndsWith('/') ? baseAddress : new Uri($"{baseAddress.AbsoluteUri}/");

            HttpResponseMessage response = null;

            try
            {
                response = await this.httpClient.GetAsync("projects?page%5Bsize%5D=1");
            }
            catch (Exception exception) when (exception is HttpRequestException or TaskCanceledException)
            {
                Assert.Ignore($"The Systems Modeling API server at {baseAddress} is not reachable: {exception.Message}");
            }

            using (response)
            {
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    var body = await response.Content.ReadAsStringAsync();
                    Assert.Fail($"The Systems Modeling API server at {baseAddress} answered GET /projects with {(int)response.StatusCode} {response.StatusCode}: {body}");
                }
            }

            this.Client = new PsmApiClient(this.httpClient, new DeSerializer(), new Serializer(), this.loggerFactory);
        }
    }
}
