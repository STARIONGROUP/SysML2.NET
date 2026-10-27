// ------------------------------------------------------------------------------------------------
// <copyright file="AllocationUsageExtensionsTestFixture.cs" company="Starion Group S.A.">
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
    using SysML2.NET.Core.POCO.Systems.Allocations;
    using SysML2.NET.Core.POCO.Systems.Connections;
    using SysML2.NET.Extensions;

    [TestFixture]
    public class AllocationUsageExtensionsTestFixture
    {
        [Test]
        public void VerifyComputeAllocationDefinition()
        {
            Assert.That(() => ((IAllocationUsage)null).ComputeAllocationDefinition(), Throws.TypeOf<ArgumentNullException>());

            var emptyAllocationUsage = new AllocationUsage();

            Assert.That(emptyAllocationUsage.ComputeAllocationDefinition(), Has.Count.EqualTo(0));

            var subject = new AllocationUsage();

            var firstDef = new AllocationDefinition();
            subject.AssignOwnership(new FeatureTyping { Type = firstDef });

            var secondDef = new AllocationDefinition();
            subject.AssignOwnership(new FeatureTyping { Type = secondDef });

            // A FeatureTyping whose Type is a ConnectionDefinition (not IAllocationDefinition) must be filtered out.
            var connectionDef = new ConnectionDefinition();
            subject.AssignOwnership(new FeatureTyping { Type = connectionDef });

            using (Assert.EnterMultipleScope())
            {
                Assert.That(subject.ComputeAllocationDefinition(), Has.Count.EqualTo(2));
                Assert.That(subject.ComputeAllocationDefinition(), Does.Contain(firstDef));
                Assert.That(subject.ComputeAllocationDefinition(), Does.Contain(secondDef));
                Assert.That(subject.ComputeAllocationDefinition(), Does.Not.Contain(connectionDef));
            }
        }
    }
}
