// ------------------------------------------------------------------------------------------------
// <copyright file="EndpointCallTestFixture.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SySML2.NET.REST.Tests.HelpersTests
{
    using System.Linq;
    using System.Net;

    using NUnit.Framework;

    using SySML2.NET.REST.Tests.Helpers;

    [TestFixture]
    public class EndpointCallTestFixture
    {
        private static readonly int[] Statuses = [199, 200, 299, 300, 304, 399, 400, 499, 500];

        [Test]
        public void VerifyEndpointCall()
        {
            var call = new EndpointCall(null, "GET /projects", HttpStatusCode.OK);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(call.RequestViolations, Is.Empty);
                Assert.That(call.ResponseViolations, Is.Empty);
            }
        }

        [Test]
        public void VerifyIsClientError()
        {
            var clientErrors = Statuses.Where(status => new EndpointCall(null, "GET /", (HttpStatusCode)status).IsClientError);

            Assert.That(clientErrors, Is.EqualTo((int[])[400, 499]));
        }

        [Test]
        public void VerifyIsSuccess()
        {
            var successes = Statuses.Where(status => new EndpointCall(null, "GET /", (HttpStatusCode)status).IsSuccess);

            Assert.That(successes, Is.EqualTo((int[])[200, 299]));
        }
    }
}
