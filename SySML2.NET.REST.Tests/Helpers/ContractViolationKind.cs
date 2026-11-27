// ------------------------------------------------------------------------------------------------
// <copyright file="ContractViolationKind.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SySML2.NET.REST.Tests.Helpers
{
    /// <summary>
    /// The ways in which a request or a response can break the OpenAPI document
    /// </summary>
    public enum ContractViolationKind
    {
        /// <summary>
        /// A query parameter that the operation does not declare
        /// </summary>
        UndeclaredParameter,

        /// <summary>
        /// A required query parameter that is missing
        /// </summary>
        MissingParameter,

        /// <summary>
        /// A query parameter whose value does not fit its schema
        /// </summary>
        InvalidParameter,

        /// <summary>
        /// A status that the operation declares neither explicitly nor through <c>default</c>
        /// </summary>
        UndeclaredStatus,

        /// <summary>
        /// A body, or its absence, where the document declares the opposite
        /// </summary>
        UnexpectedBody,

        /// <summary>
        /// A body whose media type is not declared
        /// </summary>
        UndeclaredMediaType,

        /// <summary>
        /// A body with a JSON media type that is not JSON
        /// </summary>
        InvalidJson,

        /// <summary>
        /// A JSON body that does not validate against its schema
        /// </summary>
        SchemaMismatch
    }
}
