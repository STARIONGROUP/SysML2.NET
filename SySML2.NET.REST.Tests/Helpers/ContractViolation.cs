// ------------------------------------------------------------------------------------------------
// <copyright file="ContractViolation.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SySML2.NET.REST.Tests.Helpers
{
    /// <summary>
    /// One way in which a request or a response breaks the OpenAPI document
    /// </summary>
    /// <param name="Kind">
    /// The kind of violation
    /// </param>
    /// <param name="Message">
    /// What was found, for assertion messages
    /// </param>
    public sealed record ContractViolation(ContractViolationKind Kind, string Message)
    {
        /// <summary>
        /// Returns the violation as <c>Kind: Message</c>
        /// </summary>
        /// <returns>
        /// The text of the violation
        /// </returns>
        public override string ToString() => $"{this.Kind}: {this.Message}";
    }
}
