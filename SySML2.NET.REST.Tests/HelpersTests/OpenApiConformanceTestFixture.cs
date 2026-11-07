// ------------------------------------------------------------------------------------------------
// <copyright file="OpenApiConformanceTestFixture.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SySML2.NET.REST.Tests.HelpersTests
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;

    using NUnit.Framework;
    using NUnit.Framework.Interfaces;

    using SySML2.NET.REST.Tests.Helpers;

    [TestFixture]
    public class OpenApiConformanceTestFixture
    {
        [Test]
        public async Task VerifyVerify()
        {
            var divergence = new ServerDivergence("no Error body", "the reference", (call, violation) => call.StatusCode == HttpStatusCode.NotFound && violation.Kind == ContractViolationKind.UnexpectedBody);
            var getProjects = new OpenApiOperation(HttpMethod.Get, "/projects", [], null, false, new Dictionary<string, OpenApiContent>());
            var getProject = new OpenApiOperation(HttpMethod.Get, "/projects/{projectId}", [], null, false, new Dictionary<string, OpenApiContent>());
            var conforming = new EndpointCall(getProjects, "GET /projects", HttpStatusCode.OK);
            var badRequest = conforming with { RequestLine = "GET /projects?page%5Bsize%5D=ten", RequestViolations = [new ContractViolation(ContractViolationKind.InvalidParameter, "page[size]=ten")] };
            var explained = new EndpointCall(getProject, "GET /projects/p", HttpStatusCode.NotFound) { ResponseViolations = [new ContractViolation(ContractViolationKind.UnexpectedBody, "no body")] };
            var alsoExplained = explained with { RequestLine = "GET /projects/q" };
            var unexplained = conforming with { ResponseViolations = [new ContractViolation(ContractViolationKind.SchemaMismatch, "/0 required: alias"), new ContractViolation(ContractViolationKind.SchemaMismatch, "/1 required: alias")] };

            var none = await Run([], divergence);
            var clean = await Run([conforming], divergence);
            var clientDefect = await Run([conforming, badRequest], divergence);
            var known = await Run([explained, alsoExplained], divergence);
            var unknown = await Run([unexplained], divergence);
            var mixed = await Run([badRequest, explained, unexplained], divergence);
            var withoutDivergences = await Run([explained]);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(none, Is.Empty);
                Assert.That(clean, Is.Empty);
                Assert.That(clientDefect.Select(result => result.Status), Is.EqualTo((AssertionStatus[])[AssertionStatus.Failed]));
                Assert.That(clientDefect[0].Message, Does.Contain("requests of the client").And.Contain("GET /projects answered 200: InvalidParameter: page[size]=ten (1x, e.g. GET /projects?page%5Bsize%5D=ten)"));
                Assert.That(known.Select(result => result.Status), Is.EqualTo((AssertionStatus[])[AssertionStatus.Warning]));
                Assert.That(known[0].Message, Does.Contain("no Error body").And.Contain("GET /projects/{projectId} answered 404: UnexpectedBody: no body (2x, e.g. GET /projects/p)"));
                Assert.That(unknown.Select(result => result.Status), Is.EqualTo((AssertionStatus[])[AssertionStatus.Failed]));
                Assert.That(unknown[0].Message, Does.Contain("match no known divergence").And.Contain("GET /projects answered 200: SchemaMismatch: /* required: alias (2x, e.g. GET /projects)"));
                Assert.That(mixed.Select(result => result.Status), Is.EquivalentTo((AssertionStatus[])[AssertionStatus.Warning, AssertionStatus.Failed, AssertionStatus.Failed]));
                Assert.That(withoutDivergences.Select(result => result.Status), Is.EqualTo((AssertionStatus[])[AssertionStatus.Failed]));
            }
        }

        private static Task<IReadOnlyList<AssertionResult>> Run(EndpointCall[] calls, params ServerDivergence[] divergences)
        {
            return IsolatedAssertions.RunAsync(() =>
            {
                OpenApiConformance.Verify(calls, divergences);
                return Task.CompletedTask;
            });
        }
    }
}
