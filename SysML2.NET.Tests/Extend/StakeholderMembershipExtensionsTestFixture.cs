// ------------------------------------------------------------------------------------------------
// <copyright file="StakeholderMembershipExtensionsTestFixture.cs" company="Starion Group S.A.">
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

    using SysML2.NET.Core.POCO.Root.Elements;
    using SysML2.NET.Core.POCO.Root.Namespaces;
    using SysML2.NET.Core.POCO.Systems.Parts;
    using SysML2.NET.Core.POCO.Systems.Requirements;
    using SysML2.NET.Extensions;

    using Type = SysML2.NET.Core.POCO.Core.Types.Type;

    [TestFixture]
    public class StakeholderMembershipExtensionsTestFixture
    {
        [Test]
        public void VerifyComputeOwnedStakeholderParameter()
        {
            Assert.That(() => ((IStakeholderMembership)null).ComputeOwnedStakeholderParameter(), Throws.TypeOf<ArgumentNullException>());

            var stakeholderMembership = new StakeholderMembership();

            Assert.That(stakeholderMembership.ComputeOwnedStakeholderParameter(), Is.Null);

            // Single IPartUsage wired via the public API → returned.
            var owningType = new Type();
            var stakeholderPartUsage = new PartUsage();

            owningType.AssignOwnership(stakeholderMembership, stakeholderPartUsage);

            Assert.That(stakeholderMembership.ComputeOwnedStakeholderParameter(), Is.SameAs(stakeholderPartUsage));

            var twoPartMembership = new StakeholderMembership();
            var firstPart = new PartUsage();
            var secondPart = new PartUsage();

            ((IContainedRelationship)twoPartMembership).OwnedRelatedElement.Add(firstPart);
            ((IContainedRelationship)twoPartMembership).OwnedRelatedElement.Add(secondPart);

            Assert.That(twoPartMembership.ComputeOwnedStakeholderParameter(), Is.SameAs(firstPart));

            // Mixed-type owned related elements: exactly one IPartUsage alongside a non-IPartUsage (Namespace).
            // The OfType<IPartUsage>() projection MUST pick out the IPartUsage regardless of position
            // (this is the core robustness guarantee — never positionally index the unfiltered collection).
            var mixedMembership = new StakeholderMembership();
            var siblingNonPart = new Namespace();
            var mixedPart = new PartUsage();

            ((IContainedRelationship)mixedMembership).OwnedRelatedElement.Add(siblingNonPart);
            ((IContainedRelationship)mixedMembership).OwnedRelatedElement.Add(mixedPart);

            Assert.That(mixedMembership.ComputeOwnedStakeholderParameter(), Is.SameAs(mixedPart));

            var nonPartMembership = new StakeholderMembership();
            var nonPartElement = new Namespace();

            ((IContainedRelationship)nonPartMembership).OwnedRelatedElement.Add(nonPartElement);

            Assert.That(nonPartMembership.ComputeOwnedStakeholderParameter(), Is.Null);
        }
    }
}
