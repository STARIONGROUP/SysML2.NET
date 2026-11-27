// ------------------------------------------------------------------------------------------------
// <copyright file="OpenApiConformance.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SySML2.NET.REST.Tests.Helpers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;

    using NUnit.Framework;

    /// <summary>
    /// The assertions that the recorded calls conform to the OpenAPI document: a request that breaks it is a client
    /// defect and fails; a response that breaks it is reported as the known divergence that explains it, or fails
    /// </summary>
    /// <remarks>
    /// Violations are grouped by operation, status, kind and message, with array indices folded, so that one shape of
    /// violation is reported once with its count and an example request.
    /// </remarks>
    public static partial class OpenApiConformance
    {
        /// <summary>
        /// Matches an array index in an instance location, e.g. the <c>/3</c> of <c>/3/identity</c>
        /// </summary>
        [GeneratedRegex(@"/\d+(?=[/ ])")]
        private static partial Regex ArrayIndex { get; }

        /// <summary>
        /// Verifies that recorded calls conform to the OpenAPI document, and writes every violation to the test output
        /// </summary>
        /// <param name="calls">
        /// The calls to verify
        /// </param>
        /// <param name="divergences">
        /// The known divergences that may explain a response violation
        /// </param>
        public static void Verify(IReadOnlyCollection<EndpointCall> calls, IReadOnlyList<ServerDivergence> divergences)
        {
            var requestViolations = Group(calls.SelectMany(call => call.RequestViolations.Select(violation => (Call: call, Violation: violation))));

            var responseViolations = calls
                .SelectMany(call => call.ResponseViolations.Select(violation => (Call: call, Violation: violation, Divergence: divergences.FirstOrDefault(divergence => divergence.Explains?.Invoke(call, violation) == true))))
                .ToList();

            foreach (var explained in responseViolations.Where(entry => entry.Divergence != null).GroupBy(entry => entry.Divergence))
            {
                KnownServerDivergences.Report(explained.Key, string.Join("; ", Group(explained.Select(entry => (entry.Call, entry.Violation)))));
            }

            var unexplained = Group(responseViolations.Where(entry => entry.Divergence == null).Select(entry => (entry.Call, entry.Violation)));

            foreach (var line in requestViolations.Select(line => $"request: {line}").Concat(unexplained.Select(line => $"response: {line}")))
            {
                TestContext.Out.WriteLine($"OpenAPI violation, {line}");
            }

            using (Assert.EnterMultipleScope())
            {
                Assert.That(requestViolations, Is.Empty, "requests of the client that break the OpenAPI document");
                Assert.That(unexplained, Is.Empty, "responses of the server that break the OpenAPI document and match no known divergence");
            }
        }

        /// <summary>
        /// Groups violations by operation, status, kind and message with array indices folded
        /// </summary>
        /// <param name="violations">
        /// The violations and the calls they were found in
        /// </param>
        /// <returns>
        /// One line per group: the shape of the violation, its count and an example request
        /// </returns>
        private static List<string> Group(IEnumerable<(EndpointCall Call, ContractViolation Violation)> violations)
        {
            return violations
                .GroupBy(entry => $"{entry.Call.Operation?.ToString() ?? entry.Call.RequestLine} answered {(int)entry.Call.StatusCode}: {entry.Violation.Kind}: {ArrayIndex.Replace(entry.Violation.Message, "/*")}")
                .Select(group => $"{group.Key} ({group.Count()}x, e.g. {group.First().Call.RequestLine})")
                .ToList();
        }
    }
}
