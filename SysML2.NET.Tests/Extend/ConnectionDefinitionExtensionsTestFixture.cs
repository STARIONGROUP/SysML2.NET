// ------------------------------------------------------------------------------------------------
// <copyright file="ConnectionDefinitionExtensionsTestFixture.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.Tests.Extend
{
    using System;

    using NUnit.Framework;

    using SysML2.NET.Core.POCO.Core.Features;
    using SysML2.NET.Core.POCO.Core.Types;
    using SysML2.NET.Core.POCO.Systems.Connections;
    using SysML2.NET.Core.POCO.Systems.DefinitionAndUsage;
    using SysML2.NET.Extensions;

    [TestFixture]
    public class ConnectionDefinitionExtensionsTestFixture
    {
        [Test]
        public void VerifyComputeConnectionEnd()
        {
            Assert.That(() => ((IConnectionDefinition)null).ComputeConnectionEnd(), Throws.TypeOf<ArgumentNullException>());

            var emptyConnectionDefinition = new ConnectionDefinition();

            Assert.That(emptyConnectionDefinition.ComputeConnectionEnd(), Is.Empty);

            // A feature with IsEnd = false must not be included.
            var nonEndConnectionDefinition = new ConnectionDefinition();
            var nonEndUsage = new Usage { IsEnd = false };
            nonEndConnectionDefinition.AssignOwnership(new FeatureMembership(), nonEndUsage);

            Assert.That(nonEndConnectionDefinition.ComputeConnectionEnd(), Is.Empty);

            // A Feature (not IUsage) with IsEnd = true must be filtered out by OfType<IUsage>().
            var nonUsageConnectionDefinition = new ConnectionDefinition();
            var nonUsageFeature = new Feature { IsEnd = true };
            nonUsageConnectionDefinition.AssignOwnership(new FeatureMembership(), nonUsageFeature);

            Assert.That(nonUsageConnectionDefinition.ComputeConnectionEnd(), Is.Empty);

            // Two Usage features with IsEnd = true must both be returned.
            var subject = new ConnectionDefinition();
            var firstEnd = new Usage { IsEnd = true };
            var secondEnd = new Usage { IsEnd = true };
            var excludedNonEnd = new Usage { IsEnd = false };
            subject.AssignOwnership(new FeatureMembership(), firstEnd);
            subject.AssignOwnership(new FeatureMembership(), secondEnd);
            subject.AssignOwnership(new FeatureMembership(), excludedNonEnd);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(subject.ComputeConnectionEnd(), Has.Count.EqualTo(2));
                Assert.That(subject.ComputeConnectionEnd(), Does.Contain(firstEnd));
                Assert.That(subject.ComputeConnectionEnd(), Does.Contain(secondEnd));
                Assert.That(subject.ComputeConnectionEnd(), Does.Not.Contain(excludedNonEnd));
            }
        }
    }
}
