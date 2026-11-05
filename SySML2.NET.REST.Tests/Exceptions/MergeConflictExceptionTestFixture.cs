// ------------------------------------------------------------------------------------------------
// <copyright file="MergeConflictExceptionTestFixture.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SySML2.NET.REST.Tests.Exceptions
{
    using System;
    using System.Net;

    using NUnit.Framework;

    using SysML2.NET.PSM.DTO;
    using SysML2.NET.REST.Exceptions;

    [TestFixture]
    public class MergeConflictExceptionTestFixture
    {
        [Test]
        public void VerifyConstructor()
        {
            var exception = new MergeConflictException([]);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(exception.Conflicts, Has.Count.EqualTo(0));
                Assert.That(exception.StatusCode, Is.EqualTo(HttpStatusCode.Conflict));
                Assert.That(exception.Error, Is.Null);
            }

            var conflict = new DataIdentity { Id = Guid.Parse("2f1a0e1c-6b2a-4d5e-9c3f-8a7b6d5e4c3b") };

            exception = new MergeConflictException([conflict]);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(exception.Conflicts, Has.Count.EqualTo(1));
                Assert.That(exception.Conflicts[0], Is.SameAs(conflict));
                Assert.That(exception, Is.InstanceOf<PsmApiException>());
            }

            exception = new MergeConflictException();

            using (Assert.EnterMultipleScope())
            {
                Assert.That(exception.Conflicts, Has.Count.EqualTo(0));
                Assert.That(exception.StatusCode, Is.EqualTo(HttpStatusCode.Conflict));
            }

            exception = new MergeConflictException("the message");

            using (Assert.EnterMultipleScope())
            {
                Assert.That(exception.Message, Is.EqualTo("the message"));
                Assert.That(exception.Conflicts, Has.Count.EqualTo(0));
            }

            var innerException = new InvalidOperationException();

            exception = new MergeConflictException("the message", innerException);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(exception.InnerException, Is.SameAs(innerException));
                Assert.That(exception.StatusCode, Is.EqualTo(HttpStatusCode.Conflict));
                Assert.That(exception.Error, Is.Null);
            }
        }
    }
}
