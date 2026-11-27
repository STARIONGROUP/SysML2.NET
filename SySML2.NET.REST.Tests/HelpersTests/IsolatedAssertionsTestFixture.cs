// ------------------------------------------------------------------------------------------------
// <copyright file="IsolatedAssertionsTestFixture.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SySML2.NET.REST.Tests.HelpersTests
{
    using System.Linq;
    using System.Threading.Tasks;

    using NUnit.Framework;
    using NUnit.Framework.Interfaces;

    using SySML2.NET.REST.Tests.Helpers;

    [TestFixture]
    public class IsolatedAssertionsTestFixture
    {
        [Test]
        public async Task VerifyRunAsync()
        {
            var passed = await IsolatedAssertions.RunAsync(() =>
            {
                Assert.That(1, Is.EqualTo(1));
                return Task.CompletedTask;
            });

            var warned = await IsolatedAssertions.RunAsync(() =>
            {
                Assert.Warn("warned");
                return Task.CompletedTask;
            });

            var failed = await IsolatedAssertions.RunAsync(async () =>
            {
                await Task.Yield();
                Assert.That(1, Is.EqualTo(2), "failed");
            });

            var failedTwice = await IsolatedAssertions.RunAsync(() =>
            {
                using (Assert.EnterMultipleScope())
                {
                    Assert.That(1, Is.EqualTo(2), "first");
                    Assert.That(1, Is.EqualTo(3), "second");
                }

                return Task.CompletedTask;
            });

            using (Assert.EnterMultipleScope())
            {
                Assert.That(passed, Is.Empty);
                Assert.That(warned.Select(result => result.Status), Is.EqualTo((AssertionStatus[])[AssertionStatus.Warning]));
                Assert.That(warned[0].Message, Does.Contain("warned"));
                Assert.That(failed.Select(result => result.Status), Is.EqualTo((AssertionStatus[])[AssertionStatus.Failed]));
                Assert.That(failed[0].Message, Does.Contain("failed"));
                Assert.That(failedTwice, Has.Count.EqualTo(2));
                Assert.That(failedTwice[1].Message, Does.Contain("second"));
            }
        }
    }
}
