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
    using System.Linq;

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
        /// Gets a value indicating whether the partial generated for this tag carries the class-level attributes, which a class declares once
        /// </summary>
        public required bool IsPrimary { get; init; }

        /// <summary>
        /// Gets the operations of the tag, in document order
        /// </summary>
        public required IReadOnlyList<ApiClientOperationModel> Operations { get; init; }

        /// <summary>
        /// Gets a value indicating whether an operation of the tag declares a query parameter with an inline set of allowed values
        /// </summary>
        public bool UsesEnumerations => this.Operations.Any(operation => operation.Operation.Parameters?.Any(parameter => parameter.Schema?.Enum?.Count > 0) == true);
    }
}
