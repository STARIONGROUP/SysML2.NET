// ------------------------------------------------------------------------------------------------
// <copyright file="EndpointCall.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SySML2.NET.REST.Tests.Helpers
{
    using System.Collections.Generic;
    using System.Net;

    /// <summary>
    /// One request recorded by the <see cref="EndpointRecorder"/>, with how the request and its response break the
    /// OpenAPI document
    /// </summary>
    /// <param name="Operation">
    /// The OpenAPI operation the request addressed, or <c>null</c> when it addressed none
    /// </param>
    /// <param name="RequestLine">
    /// The method, path and query of the request
    /// </param>
    /// <param name="StatusCode">
    /// The status the server answered with
    /// </param>
    public sealed record EndpointCall(OpenApiOperation Operation, string RequestLine, HttpStatusCode StatusCode)
    {
        /// <summary>
        /// Gets a value indicating whether the server answered with a success status (2xx)
        /// </summary>
        public bool IsSuccess => (int)this.StatusCode is >= 200 and < 300;

        /// <summary>
        /// Gets a value indicating whether the server rejected the request as a client error (4xx)
        /// </summary>
        public bool IsClientError => (int)this.StatusCode is >= 400 and < 500;

        /// <summary>
        /// Gets the ways in which the request of the client breaks the document, which are client defects
        /// </summary>
        public IReadOnlyList<ContractViolation> RequestViolations { get; init; } = [];

        /// <summary>
        /// Gets the ways in which the response of the server breaks the document, which are server divergences
        /// </summary>
        public IReadOnlyList<ContractViolation> ResponseViolations { get; init; } = [];
    }
}
