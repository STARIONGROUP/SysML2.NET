// ------------------------------------------------------------------------------------------------
// <copyright file="VariantMembershipExtensionsTestFixture.cs" company="Starion Group S.A.">
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
    using SysML2.NET.Core.POCO.Systems.DefinitionAndUsage;
    using SysML2.NET.Exceptions;
    using SysML2.NET.Extensions;

    using Type = SysML2.NET.Core.POCO.Core.Types.Type;

    [TestFixture]
    public class VariantMembershipExtensionsTestFixture
    {
        [Test]
        public void VerifyComputeOwnedVariantUsage()
        {
            Assert.That(() => ((IVariantMembership)null).ComputeOwnedVariantUsage(), Throws.TypeOf<ArgumentNullException>());

            // Empty OwnedRelatedElement → [1..1] violation: throws IncompleteModelException.
            var variantMembership = new VariantMembership();

            Assert.That(() => variantMembership.ComputeOwnedVariantUsage(), Throws.TypeOf<IncompleteModelException>());

            // Single IUsage wired via the public API → returned.
            var owningType = new Type();
            var variantUsage = new Usage();

            owningType.AssignOwnership(variantMembership, variantUsage);

            Assert.That(variantMembership.ComputeOwnedVariantUsage(), Is.SameAs(variantUsage));

            // Two IUsages in OwnedRelatedElement → upper-bound violation: throws MultiplicityViolationException.
            var twoUsageMembership = new VariantMembership();
            var firstUsage = new Usage();
            var secondUsage = new Usage();

            ((IContainedRelationship)twoUsageMembership).OwnedRelatedElement.Add(firstUsage);
            ((IContainedRelationship)twoUsageMembership).OwnedRelatedElement.Add(secondUsage);

            Assert.That(() => twoUsageMembership.ComputeOwnedVariantUsage(), Throws.TypeOf<MultiplicityViolationException>());

            // Mixed-type owned related elements: exactly one IUsage alongside a non-IUsage (Namespace).
            // The OfType<IUsage>() projection MUST pick out the IUsage regardless of its position
            // (this is the core robustness guarantee — never positionally index the unfiltered collection).
            var mixedMembership = new VariantMembership();
            var siblingNonUsage = new Namespace();
            var mixedUsage = new Usage();

            ((IContainedRelationship)mixedMembership).OwnedRelatedElement.Add(siblingNonUsage);
            ((IContainedRelationship)mixedMembership).OwnedRelatedElement.Add(mixedUsage);

            Assert.That(mixedMembership.ComputeOwnedVariantUsage(), Is.SameAs(mixedUsage));

            // OwnedRelatedElement populated with non-IUsage element(s) only → no IUsage match:
            // [1..1] violation, throws IncompleteModelException.
            var nonUsageMembership = new VariantMembership();
            var nonUsageElement = new Namespace();

            ((IContainedRelationship)nonUsageMembership).OwnedRelatedElement.Add(nonUsageElement);

            Assert.That(() => nonUsageMembership.ComputeOwnedVariantUsage(), Throws.TypeOf<IncompleteModelException>());
        }
    }
}
