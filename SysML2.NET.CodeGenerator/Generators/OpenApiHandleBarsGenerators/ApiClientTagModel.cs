// ------------------------------------------------------------------------------------------------
// <copyright file="ApiClientTagModel.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.CodeGenerator.Generators.OpenApiHandleBarsGenerators
{
    using System.Collections.Generic;

    /// <summary>
    /// The template model of the operations that share one tag of the Systems Modeling API and Services
    /// </summary>
    public sealed class ApiClientTagModel
    {
        /// <summary>
        /// Gets the tag as declared in the OpenAPI document
        /// </summary>
        public required string Tag { get; init; }

        /// <summary>
        /// Gets the identifier-safe name of the tag, which names the generated file
        /// </summary>
        public required string Name { get; init; }

        /// <summary>
        /// Gets the operations of the tag, in document order
        /// </summary>
        public required IReadOnlyList<ApiClientOperationModel> Operations { get; init; }
    }
}
