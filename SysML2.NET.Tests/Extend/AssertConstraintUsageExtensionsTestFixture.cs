// ------------------------------------------------------------------------------------------------
// <copyright file="AssertConstraintUsageExtensionsTestFixture.cs" company="Starion Group S.A.">
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
    using SysML2.NET.Core.POCO.Systems.Constraints;
    using SysML2.NET.Core.POCO.Systems.Parts;
    using SysML2.NET.Extensions;

    [TestFixture]
    public class AssertConstraintUsageExtensionsTestFixture
    {
        [Test]
        public void VerifyComputeAssertedConstraint()
        {
            Assert.That(() => ((IAssertConstraintUsage)null).ComputeAssertedConstraint(), Throws.TypeOf<ArgumentNullException>());

            // No ownedReferenceSubsetting → ReferencedFeatureTarget() returns null →
            // self-fallback branch: returns the subject itself (which IS-A IConstraintUsage).
            var selfFallbackUsage = new AssertConstraintUsage();

            Assert.That(selfFallbackUsage.ComputeAssertedConstraint(), Is.SameAs(selfFallbackUsage));

            // ownedReferenceSubsetting points to a ConstraintUsage → that ConstraintUsage is returned
            // (since the referenced feature's featureTarget is itself when it has no chainingFeatures).
            var withConstraintRefUsage = new AssertConstraintUsage();
            var targetConstraint = new ConstraintUsage();
            var referenceSubsetting = new ReferenceSubsetting { ReferencedFeature = targetConstraint };
            withConstraintRefUsage.AssignOwnership(referenceSubsetting);

            Assert.That(withConstraintRefUsage.ComputeAssertedConstraint(), Is.SameAs(targetConstraint));

            // ownedReferenceSubsetting points to a non-ConstraintUsage feature → the "as IConstraintUsage"
            // cast yields null (the OCL "else null" branch).
            var withNonConstraintRefUsage = new AssertConstraintUsage();
            var nonConstraintTarget = new PartUsage();
            var nonConstraintRefSubsetting = new ReferenceSubsetting { ReferencedFeature = nonConstraintTarget };
            withNonConstraintRefUsage.AssignOwnership(nonConstraintRefSubsetting);

            Assert.That(withNonConstraintRefUsage.ComputeAssertedConstraint(), Is.Null);
        }
    }
}
