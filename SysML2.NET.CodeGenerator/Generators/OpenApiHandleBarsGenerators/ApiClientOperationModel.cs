// ------------------------------------------------------------------------------------------------
// <copyright file="ApiClientOperationModel.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.CodeGenerator.Generators.OpenApiHandleBarsGenerators
{
    using Microsoft.OpenApi;

    /// <summary>
    /// The template model of one operation of the Systems Modeling API and Services, bound to its path and HTTP method
    /// </summary>
    public sealed class ApiClientOperationModel
    {
        /// <summary>
        /// Gets the path template of the operation, relative to the server root
        /// </summary>
        public required string Path { get; init; }

        /// <summary>
        /// Gets the HTTP method of the operation, in upper case
        /// </summary>
        public required string HttpMethod { get; init; }

        /// <summary>
        /// Gets the <see cref="OpenApiOperation"/> as declared in the OpenAPI document
        /// </summary>
        public required OpenApiOperation Operation { get; init; }
    }
}
