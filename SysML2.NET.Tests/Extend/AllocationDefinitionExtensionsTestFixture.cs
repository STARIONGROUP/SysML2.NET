// ------------------------------------------------------------------------------------------------
// <copyright file="AllocationDefinitionExtensionsTestFixture.cs" company="Starion Group S.A.">
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

    using SysML2.NET.Core.POCO.Core.Types;
    using SysML2.NET.Core.POCO.Systems.Allocations;
    using SysML2.NET.Core.POCO.Systems.Connections;
    using SysML2.NET.Core.POCO.Systems.DefinitionAndUsage;
    using SysML2.NET.Extensions;

    [TestFixture]
    public class AllocationDefinitionExtensionsTestFixture
    {
        [Test]
        public void VerifyComputeAllocation()
        {
            Assert.That(() => ((IAllocationDefinition)null).ComputeAllocation(), Throws.TypeOf<ArgumentNullException>());

            var emptyAllocationDefinition = new AllocationDefinition();

            Assert.That(emptyAllocationDefinition.ComputeAllocation(), Has.Count.EqualTo(0));

            // Mixed usages: only AllocationUsage instances must be returned.
            var subject = new AllocationDefinition();
            var allocationUsage = new AllocationUsage();
            var connectionUsage = new ConnectionUsage();
            var bareUsage = new Usage();

            subject.AssignOwnership(new FeatureMembership(), allocationUsage);
            subject.AssignOwnership(new FeatureMembership(), connectionUsage);
            subject.AssignOwnership(new FeatureMembership(), bareUsage);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(subject.ComputeAllocation(), Does.Contain(allocationUsage));
                Assert.That(subject.ComputeAllocation(), Does.Not.Contain(connectionUsage));
                Assert.That(subject.ComputeAllocation(), Does.Not.Contain(bareUsage));
            }
        }
    }
}
