// ------------------------------------------------------------------------------------------------
// <copyright file="EndpointRecorder.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SySML2.NET.REST.Tests.Helpers
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// A <see cref="DelegatingHandler"/> that records which OpenAPI operation every request addressed, with which
    /// status it was answered, how the request and the response break the document, and which request bodies
    /// reference the empty GUID
    /// </summary>
    public sealed class EndpointRecorder : DelegatingHandler
    {
        /// <summary>
        /// The text of the empty GUID; a payload referencing it refers to no element
        /// </summary>
        private const string EmptyGuid = "00000000-0000-0000-0000-000000000000";

        /// <summary>
        /// The operations that requests are matched against
        /// </summary>
        private readonly OpenApiDocument openApi;

        /// <summary>
        /// The recorded calls
        /// </summary>
        private readonly ConcurrentQueue<EndpointCall> calls = new();

        /// <summary>
        /// The requests whose body referenced the empty GUID
        /// </summary>
        private readonly ConcurrentQueue<string> emptyGuidRequests = new();

        /// <summary>
        /// Initializes a new instance of the <see cref="EndpointRecorder"/> class
        /// </summary>
        /// <param name="openApi">
        /// The operations that requests are matched against
        /// </param>
        /// <param name="innerHandler">
        /// The handler that sends the request on
        /// </param>
        public EndpointRecorder(OpenApiDocument openApi, HttpMessageHandler innerHandler) : base(innerHandler)
        {
            this.openApi = openApi ?? throw new ArgumentNullException(nameof(openApi));
        }

        /// <summary>
        /// Gets the recorded calls, in order
        /// </summary>
        public IReadOnlyCollection<EndpointCall> Calls => this.calls;

        /// <summary>
        /// Gets the requests, as <c>METHOD path</c>, whose body referenced the empty GUID
        /// </summary>
        public IReadOnlyCollection<string> EmptyGuidRequests => this.emptyGuidRequests;

        /// <summary>
        /// Gets the call recorded last, or <c>null</c> before the first
        /// </summary>
        public EndpointCall LastCall { get; private set; }

        /// <summary>
        /// Sends the request on, and records the operation it addressed, the status it was answered with, and how the
        /// request and the response break the OpenAPI document
        /// </summary>
        /// <param name="request">
        /// The request to send
        /// </param>
        /// <param name="cancellationToken">
        /// The <see cref="CancellationToken"/> that cancels the operation
        /// </param>
        /// <returns>
        /// The response of the server, with its body buffered so that it can be read again
        /// </returns>
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var requestLine = $"{request.Method} {request.RequestUri?.PathAndQuery}";
            var requestBody = string.Empty;

            if (request.Content != null)
            {
                await request.Content.LoadIntoBufferAsync(cancellationToken);
                requestBody = await request.Content.ReadAsStringAsync(cancellationToken);

                if (requestBody.Contains(EmptyGuid, StringComparison.OrdinalIgnoreCase))
                {
                    this.emptyGuidRequests.Enqueue(requestLine);
                }
            }

            var response = await base.SendAsync(request, cancellationToken);

            await response.Content.LoadIntoBufferAsync(cancellationToken);
            var responseBody = await response.Content.ReadAsStringAsync(cancellationToken);

            var operation = this.openApi.Match(request.Method, request.RequestUri?.AbsolutePath ?? string.Empty);

            var call = new EndpointCall(operation, requestLine, response.StatusCode)
            {
                RequestViolations = operation?.CheckRequest(request.RequestUri?.Query, request.Content?.Headers.ContentType?.MediaType, requestBody) ?? [],
                ResponseViolations = operation?.CheckResponse(response.StatusCode, response.Content.Headers.ContentType?.MediaType, responseBody) ?? []
            };

            this.calls.Enqueue(call);
            this.LastCall = call;

            return response;
        }
    }
}
