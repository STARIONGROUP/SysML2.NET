// ------------------------------------------------------------------------------------------------
// <copyright file="PsmApiExceptionTestFixture.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SySML2.NET.REST.Tests
{
    using System.Net;

    using NUnit.Framework;

    using SysML2.NET.PSM.DTO;

    [TestFixture]
    public class PsmApiExceptionTestFixture
    {
        [Test]
        public void VerifyConstructor()
        {
            var error = new Error { Description = "the error description" };

            var exception = new PsmApiException(HttpStatusCode.NotFound, error);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(exception.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
                Assert.That(exception.Error, Is.SameAs(error));
                Assert.That(exception.Message, Is.EqualTo("the error description"));
            }

            exception = new PsmApiException(HttpStatusCode.BadRequest, new Error { Description = " " });

            Assert.That(exception.Message, Is.EqualTo("The server responded with status code 400 (BadRequest)"));

            exception = new PsmApiException(HttpStatusCode.InternalServerError, null);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(exception.Error, Is.Null);
                Assert.That(exception.Message, Is.EqualTo("The server responded with status code 500 (InternalServerError)"));
            }
        }
    }
}
