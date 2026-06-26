// -------------------------------------------------------------------------------------------------
// <copyright file="StakeholderMembershipExtensionsTestFixture.cs" company="Starion Group S.A.">
// 
//   Copyright (C) 2022-2026 Starion Group S.A.
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

    using SysML2.NET.Core.POCO.Root.Elements;
    using SysML2.NET.Core.POCO.Root.Namespaces;
    using SysML2.NET.Core.POCO.Systems.Parts;
    using SysML2.NET.Core.POCO.Systems.Requirements;
    using SysML2.NET.Exceptions;
    using SysML2.NET.Extensions;

    using Type = SysML2.NET.Core.POCO.Core.Types.Type;

    [TestFixture]
    public class StakeholderMembershipExtensionsTestFixture
    {
        [Test]
        public void VerifyComputeOwnedStakeholderParameter()
        {
            Assert.That(() => ((IStakeholderMembership)null).ComputeOwnedStakeholderParameter(), Throws.TypeOf<ArgumentNullException>());

            // Empty OwnedRelatedElement → [1..1] violation: throws IncompleteModelException.
            var stakeholderMembership = new StakeholderMembership();

            Assert.That(() => stakeholderMembership.ComputeOwnedStakeholderParameter(), Throws.TypeOf<IncompleteModelException>());

            // Single IPartUsage wired via the public API → returned.
            var owningType = new Type();
            var stakeholderPartUsage = new PartUsage();

            owningType.AssignOwnership(stakeholderMembership, stakeholderPartUsage);

            Assert.That(stakeholderMembership.ComputeOwnedStakeholderParameter(), Is.SameAs(stakeholderPartUsage));

            // Two IPartUsages in OwnedRelatedElement → upper-bound violation: throws MultiplicityViolationException.
            var twoPartMembership = new StakeholderMembership();
            var firstPart = new PartUsage();
            var secondPart = new PartUsage();

            ((IContainedRelationship)twoPartMembership).OwnedRelatedElement.Add(firstPart);
            ((IContainedRelationship)twoPartMembership).OwnedRelatedElement.Add(secondPart);

            Assert.That(() => twoPartMembership.ComputeOwnedStakeholderParameter(), Throws.TypeOf<MultiplicityViolationException>());

            // Mixed-type owned related elements: exactly one IPartUsage alongside a non-IPartUsage (Namespace).
            // The SingleStrict<IPartUsage> projection MUST pick out the IPartUsage regardless of position
            // (this is the core robustness guarantee — never positionally index the unfiltered collection).
            var mixedMembership = new StakeholderMembership();
            var siblingNonPart = new Namespace();
            var mixedPart = new PartUsage();

            ((IContainedRelationship)mixedMembership).OwnedRelatedElement.Add(siblingNonPart);
            ((IContainedRelationship)mixedMembership).OwnedRelatedElement.Add(mixedPart);

            Assert.That(mixedMembership.ComputeOwnedStakeholderParameter(), Is.SameAs(mixedPart));

            // OwnedRelatedElement populated with non-IPartUsage element(s) only → no IPartUsage match:
            // [1..1] violation, throws IncompleteModelException.
            var nonPartMembership = new StakeholderMembership();
            var nonPartElement = new Namespace();

            ((IContainedRelationship)nonPartMembership).OwnedRelatedElement.Add(nonPartElement);

            Assert.That(() => nonPartMembership.ComputeOwnedStakeholderParameter(), Throws.TypeOf<IncompleteModelException>());
        }
    }
}
