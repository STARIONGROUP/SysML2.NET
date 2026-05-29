// -------------------------------------------------------------------------------------------------
// <copyright file="AllocationDefinitionExtensionsTestFixture.cs" company="Starion Group S.A.">
//
//   Copyright 2022-2026 Starion Group S.A.
//
//   Licensed under the Apache License, Version 2.0 (the "License");
//   you may not use this file except in compliance with the License.
//   You may obtain a copy of the License at
//
//        http://www.apache.org/licenses/LICENSE-2.0
//
//    Unless required by applicable law or agreed to in writing, software
//    distributed under the License is distributed on an "AS IS" BASIS,
//    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//    See the License for the specific language governing permissions and
//    limitations under the License.
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
