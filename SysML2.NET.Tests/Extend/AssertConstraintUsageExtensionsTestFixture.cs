// -------------------------------------------------------------------------------------------------
// <copyright file="AssertConstraintUsageExtensionsTestFixture.cs" company="Starion Group S.A.">
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
