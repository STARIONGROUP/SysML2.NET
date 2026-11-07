// ------------------------------------------------------------------------------------------------
// <copyright file="ServerDivergence.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SySML2.NET.REST.Tests.Helpers
{
    using System;

    /// <summary>
    /// A way in which the server under the scenario diverges from the specification
    /// </summary>
    /// <param name="Summary">
    /// What the server does differently
    /// </param>
    /// <param name="Reference">
    /// Where the behaviour was found, or the upstream issue that reports it
    /// </param>
    /// <param name="Explains">
    /// Whether the divergence explains a contract violation of a response, or <c>null</c> when it explains none
    /// </param>
    public sealed record ServerDivergence(string Summary, string Reference, Func<EndpointCall, ContractViolation, bool> Explains = null);
}
