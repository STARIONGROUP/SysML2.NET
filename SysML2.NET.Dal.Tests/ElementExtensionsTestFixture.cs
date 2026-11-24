// ------------------------------------------------------------------------------------------------
// <copyright file="ElementExtensionsTestFixture.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.Dal.Tests
{
    using System;

    using Microsoft.Extensions.Logging;

    using Moq;

    using NUnit.Framework;

    using SysML2.NET.Core.POCO.Kernel.Packages;
    using SysML2.NET.Dal;

    [TestFixture]
    public class ElementExtensionsTestFixture
    {
        [Test]
        public void VerifyTryMap()
        {
            var package = new Package { Id = Guid.NewGuid() };
            var logger = new Mock<ILogger>();
            logger.Setup(x => x.IsEnabled(LogLevel.Warning)).Returns(true);

            var mapped = false;
            package.TryMap("name", () => mapped = true, logger.Object);

            Assert.That(mapped, Is.True);
            logger.Verify(x => x.Log(LogLevel.Warning, It.IsAny<EventId>(), It.IsAny<It.IsAnyType>(), It.IsAny<Exception>(), It.IsAny<Func<It.IsAnyType, Exception, string>>()), Times.Never);

            package.TryMap("name", () => throw new NotSupportedException(), logger.Object);

            logger.Verify(x => x.Log(LogLevel.Warning, It.IsAny<EventId>(), It.IsAny<It.IsAnyType>(), It.IsAny<NotSupportedException>(), It.IsAny<Func<It.IsAnyType, Exception, string>>()), Times.Once);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(() => package.TryMap("name", () => throw new NotSupportedException(), null), Throws.Nothing);
                Assert.That(() => package.TryMap("name", () => throw new InvalidOperationException(), logger.Object), Throws.TypeOf<InvalidOperationException>());
            }
        }
    }
}
