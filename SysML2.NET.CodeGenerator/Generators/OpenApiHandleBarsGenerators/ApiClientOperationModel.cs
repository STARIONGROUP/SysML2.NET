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
    using System.Collections.Generic;
    using System.Linq;

    using Microsoft.OpenApi;

    /// <summary>
    /// The template model of one operation of the Systems Modeling API and Services, bound to its path and HTTP method
    /// </summary>
    public sealed class ApiClientOperationModel
    {
        /// <summary>
        /// The names of the cursor-based pagination query parameters, which the generated client groups into one <c>QueryParameters</c> argument
        /// </summary>
        private static readonly string[] PageParameterNames = ["page[after]", "page[before]", "page[size]"];

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

        /// <summary>
        /// Gets the parameters of the operation other than the cursor-based pagination query parameters, in declaration order
        /// </summary>
        public IEnumerable<IOpenApiParameter> Parameters => this.Operation.Parameters?.Where(parameter => !PageParameterNames.Contains(parameter.Name)) ?? [];

        /// <summary>
        /// Gets a value indicating whether the operation declares one of the cursor-based pagination query parameters
        /// </summary>
        public bool IsPaged => this.Operation.Parameters?.Any(parameter => PageParameterNames.Contains(parameter.Name)) == true;

        /// <summary>
        /// Gets a value indicating whether the 409 response of the operation lists the conflicting data as <c>DataIdentity</c> items
        /// </summary>
        public bool ThrowsMergeConflict => this.Operation.Responses?.TryGetValue("409", out var conflictResponse) == true
                                           && conflictResponse.Content?.TryGetValue("application/json", out var mediaType) == true
                                           && mediaType.Schema?.Items is OpenApiSchemaReference { Reference.Id: "DataIdentity" };
    }
}
