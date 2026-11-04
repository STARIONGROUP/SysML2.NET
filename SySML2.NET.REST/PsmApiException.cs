// ------------------------------------------------------------------------------------------------
// <copyright file="PsmApiException.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SySML2.NET.REST
{
    using System;
    using System.Net;

    using SysML2.NET.PSM.DTO;

    /// <summary>
    /// The exception thrown when the Systems Modeling API and Services responds with an error status code
    /// </summary>
    public class PsmApiException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PsmApiException"/> class
        /// </summary>
        /// <param name="statusCode">
        /// The <see cref="HttpStatusCode"/> of the response
        /// </param>
        /// <param name="error">
        /// The <see cref="Error"/> carried by the response, or <c>null</c> when the response carries none
        /// </param>
        public PsmApiException(HttpStatusCode statusCode, Error error)
            : base(string.IsNullOrWhiteSpace(error?.Description) ? $"The server responded with status code {(int)statusCode} ({statusCode})" : error.Description)
        {
            this.StatusCode = statusCode;
            this.Error = error;
        }

        /// <summary>
        /// Gets the <see cref="HttpStatusCode"/> of the response
        /// </summary>
        public HttpStatusCode StatusCode { get; }

        /// <summary>
        /// Gets the <see cref="Error"/> carried by the response, or <c>null</c> when the response carries none
        /// </summary>
        public Error Error { get; }
    }
}
