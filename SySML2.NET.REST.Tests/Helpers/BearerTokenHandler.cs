// ------------------------------------------------------------------------------------------------
// <copyright file="BearerTokenHandler.cs" company="Starion Group S.A.">
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
    using System.Net.Http.Headers;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// A <see cref="DelegatingHandler"/> that adds a bearer token to every request, because the
    /// <see cref="Session"/> has no authentication option of its own
    /// </summary>
    public sealed class BearerTokenHandler : DelegatingHandler
    {
        /// <summary>
        /// The token that is added to every request
        /// </summary>
        private readonly string token;

        /// <summary>
        /// Initializes a new instance of the <see cref="BearerTokenHandler"/> class
        /// </summary>
        /// <param name="token">
        /// The token that is added to every request
        /// </param>
        /// <param name="innerHandler">
        /// The handler that sends the request on
        /// </param>
        /// <exception cref="ArgumentException">
        /// Thrown when <paramref name="token"/> is null or whitespace
        /// </exception>
        public BearerTokenHandler(string token, HttpMessageHandler innerHandler) : base(innerHandler)
        {
            if (string.IsNullOrWhiteSpace(token))
            {
                throw new ArgumentException("The token may not be null or whitespace", nameof(token));
            }

            this.token = token;
        }

        /// <summary>
        /// Adds the bearer token and sends the request on
        /// </summary>
        /// <param name="request">
        /// The request to send
        /// </param>
        /// <param name="cancellationToken">
        /// The <see cref="CancellationToken"/> that cancels the operation
        /// </param>
        /// <returns>
        /// The response of the server
        /// </returns>
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", this.token);

            return base.SendAsync(request, cancellationToken);
        }
    }
}
