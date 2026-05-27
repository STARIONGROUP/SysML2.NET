// -------------------------------------------------------------------------------------------------
// <copyright file="RequirementConstraintMembershipExtensionsTestFixture.cs" company="Starion Group S.A.">
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
    using SysML2.NET.Core.POCO.Root.Elements;
    using SysML2.NET.Core.POCO.Root.Namespaces;
    using SysML2.NET.Core.POCO.Systems.Constraints;
    using SysML2.NET.Core.POCO.Systems.Requirements;
    using SysML2.NET.Exceptions;
    using SysML2.NET.Extensions;

    using Type = SysML2.NET.Core.POCO.Core.Types.Type;

    [TestFixture]
    public class RequirementConstraintMembershipExtensionsTestFixture
    {
        [Test]
        public void VerifyComputeOwnedConstraint()
        {
            Assert.That(() => ((IRequirementConstraintMembership)null).ComputeOwnedConstraint(), Throws.TypeOf<ArgumentNullException>());

            // Empty OwnedRelatedElement → [1..1] violation: throws IncompleteModelException.
            var membership = new RequirementConstraintMembership();

            Assert.That(() => membership.ComputeOwnedConstraint(), Throws.TypeOf<IncompleteModelException>());

            // Single IConstraintUsage wired via the public API → returned.
            var owningType = new Type();
            var constraintUsage = new ConstraintUsage();

            owningType.AssignOwnership(membership, constraintUsage);

            Assert.That(membership.ComputeOwnedConstraint(), Is.SameAs(constraintUsage));

            // Two IConstraintUsage elements in OwnedRelatedElement → [1..1] violation: throws IncompleteModelException.
            var twoConstraintMembership = new RequirementConstraintMembership();
            var firstConstraint = new ConstraintUsage();
            var secondConstraint = new ConstraintUsage();

            ((IContainedRelationship)twoConstraintMembership).OwnedRelatedElement.Add(firstConstraint);
            ((IContainedRelationship)twoConstraintMembership).OwnedRelatedElement.Add(secondConstraint);

            Assert.That(() => twoConstraintMembership.ComputeOwnedConstraint(), Throws.TypeOf<IncompleteModelException>());

            // Mixed-type owned related elements: exactly one IConstraintUsage alongside a non-IConstraintUsage (Namespace).
            // The OfType<IConstraintUsage>() projection MUST pick out the IConstraintUsage regardless of its position
            // (this is the core robustness guarantee — never positionally index the unfiltered collection).
            var mixedMembership = new RequirementConstraintMembership();
            var siblingNonConstraint = new Namespace();
            var mixedConstraint = new ConstraintUsage();

            ((IContainedRelationship)mixedMembership).OwnedRelatedElement.Add(siblingNonConstraint);
            ((IContainedRelationship)mixedMembership).OwnedRelatedElement.Add(mixedConstraint);

            Assert.That(mixedMembership.ComputeOwnedConstraint(), Is.SameAs(mixedConstraint));

            // OwnedRelatedElement populated with non-IConstraintUsage element(s) only → no IConstraintUsage match:
            // [1..1] violation, throws IncompleteModelException.
            var nonConstraintMembership = new RequirementConstraintMembership();
            var nonConstraintElement = new Namespace();

            ((IContainedRelationship)nonConstraintMembership).OwnedRelatedElement.Add(nonConstraintElement);

            Assert.That(() => nonConstraintMembership.ComputeOwnedConstraint(), Throws.TypeOf<IncompleteModelException>());
        }

        [Test]
        public void VerifyComputeReferencedConstraint()
        {
            Assert.That(() => ((IRequirementConstraintMembership)null).ComputeReferencedConstraint(), Throws.TypeOf<ArgumentNullException>());

            // Empty OwnedRelatedElement → IncompleteModelException bubbles from ComputeOwnedConstraint.
            var emptyMembership = new RequirementConstraintMembership();

            Assert.That(() => emptyMembership.ComputeReferencedConstraint(), Throws.TypeOf<IncompleteModelException>());

            // ownedConstraint has no ReferenceSubsetting → referencedFeatureTarget() returns null → return ownedConstraint itself.
            var owningType3 = new Type();
            var membership3 = new RequirementConstraintMembership();
            var ownedConstraint3 = new ConstraintUsage();

            owningType3.AssignOwnership(membership3, ownedConstraint3);

            Assert.That(membership3.ComputeReferencedConstraint(), Is.SameAs(ownedConstraint3));

            // ownedConstraint has a ReferenceSubsetting pointing to another ConstraintUsage → returns that ConstraintUsage.
            var owningType4 = new Type();
            var membership4 = new RequirementConstraintMembership();
            var ownedConstraint4 = new ConstraintUsage();
            var referencedConstraintUsage = new ConstraintUsage();

            owningType4.AssignOwnership(membership4, ownedConstraint4);
            ownedConstraint4.AssignOwnership(new ReferenceSubsetting { ReferencedFeature = referencedConstraintUsage });

            Assert.That(membership4.ComputeReferencedConstraint(), Is.SameAs(referencedConstraintUsage));

            // ownedConstraint has a ReferenceSubsetting pointing to a plain Feature (not IConstraintUsage) → returns null.
            var owningType5 = new Type();
            var membership5 = new RequirementConstraintMembership();
            var ownedConstraint5 = new ConstraintUsage();
            var plainFeature = new Feature();

            owningType5.AssignOwnership(membership5, ownedConstraint5);
            ownedConstraint5.AssignOwnership(new ReferenceSubsetting { ReferencedFeature = plainFeature });

            Assert.That(membership5.ComputeReferencedConstraint(), Is.Null);
        }
    }
}
