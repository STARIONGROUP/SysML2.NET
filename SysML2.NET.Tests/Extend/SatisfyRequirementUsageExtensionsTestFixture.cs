// -------------------------------------------------------------------------------------------------
// <copyright file="SatisfyRequirementUsageExtensionsTestFixture.cs" company="Starion Group S.A.">
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
    using SysML2.NET.Core.POCO.Core.Types;
    using SysML2.NET.Core.POCO.Kernel.Connectors;
    using SysML2.NET.Core.POCO.Systems.Constraints;
    using SysML2.NET.Core.POCO.Systems.DefinitionAndUsage;
    using SysML2.NET.Core.POCO.Systems.Requirements;
    using SysML2.NET.Extensions;

    [TestFixture]
    public class SatisfyRequirementUsageExtensionsTestFixture
    {
        [Test]
        public void VerifyComputeSatisfiedRequirement()
        {
            Assert.That(() => ((ISatisfyRequirementUsage)null).ComputeSatisfiedRequirement(), Throws.TypeOf<ArgumentNullException>());

            // No ownedReferenceSubsetting → AssertConstraintUsage.assertedConstraint returns subject itself,
            // which IS-A IRequirementUsage (ISatisfyRequirementUsage : IRequirementUsage) → returns subject.
            var selfFallbackUsage = new SatisfyRequirementUsage();

            Assert.That(selfFallbackUsage.ComputeSatisfiedRequirement(), Is.SameAs(selfFallbackUsage));

            // ownedReferenceSubsetting points to a RequirementUsage → returns it.
            var withReqRefUsage = new SatisfyRequirementUsage();
            var targetRequirement = new RequirementUsage();
            var refSubsetting = new ReferenceSubsetting { ReferencedFeature = targetRequirement };
            withReqRefUsage.AssignOwnership(refSubsetting);

            Assert.That(withReqRefUsage.ComputeSatisfiedRequirement(), Is.SameAs(targetRequirement));

            // ownedReferenceSubsetting points to a ConstraintUsage that is NOT a RequirementUsage →
            // assertedConstraint returns the ConstraintUsage, but the "as IRequirementUsage" cast yields null.
            var withWrongTypeUsage = new SatisfyRequirementUsage();
            var nonRequirementTarget = new ConstraintUsage();
            var wrongRefSubsetting = new ReferenceSubsetting { ReferencedFeature = nonRequirementTarget };
            withWrongTypeUsage.AssignOwnership(wrongRefSubsetting);

            Assert.That(withWrongTypeUsage.ComputeSatisfiedRequirement(), Is.Null);
        }

        [Test]
        public void VerifyComputeSatisfyingFeature()
        {
            Assert.That(() => ((ISatisfyRequirementUsage)null).ComputeSatisfyingFeature(), Throws.TypeOf<ArgumentNullException>());

            // Empty case: no ownedMember at all → no BindingConnectors → returns null.
            var emptyUsage = new SatisfyRequirementUsage();

            Assert.That(emptyUsage.ComputeSatisfyingFeature(), Is.Null);

            // Populated case: a BindingConnector with two ends, one referencing subjectParameter and one
            // referencing a "satisfying" Feature → ComputeSatisfyingFeature returns the satisfying Feature.
            // Constructing the chain end-to-end:
            //   srfUsage.subjectParameter  ← SubjectMembership.ownedSubjectParameter
            //   bindingConnector.relatedElement
            //     ← connectorEnd.ownedReferenceSubsetting.SubsettedFeature
            //     ← featureMembership.ownedMemberFeature.Where(IsEnd).OwnedRelationship.OfType<IReferenceSubsetting>().FirstOrDefault().ReferencedFeature
            var populatedUsage = new SatisfyRequirementUsage();

            var subjectMembership = new SubjectMembership();
            var subjectParameter = new Usage();
            populatedUsage.AssignOwnership(subjectMembership, subjectParameter);

            var satisfyingFeature = new Feature();

            var endForSubject = new Feature { IsEnd = true };
            endForSubject.AssignOwnership(new ReferenceSubsetting { ReferencedFeature = subjectParameter });

            var endForSatisfying = new Feature { IsEnd = true };
            endForSatisfying.AssignOwnership(new ReferenceSubsetting { ReferencedFeature = satisfyingFeature });

            var bindingConnector = new BindingConnector();
            bindingConnector.AssignOwnership(new FeatureMembership(), endForSubject);
            bindingConnector.AssignOwnership(new FeatureMembership(), endForSatisfying);

            populatedUsage.AssignOwnership(new FeatureMembership(), bindingConnector);

            Assert.That(populatedUsage.ComputeSatisfyingFeature(), Is.SameAs(satisfyingFeature));

            // Degenerate case: a BindingConnector with only ONE end (referencing subjectParameter) →
            // bindings[0].relatedElement contains only subjectParameter → FirstOrDefault with the
            // !ReferenceEquals(r, subjectParameter) predicate finds no match → returns null.
            var degenUsage = new SatisfyRequirementUsage();

            var degenSubjectMembership = new SubjectMembership();
            var degenSubjectParameter = new Usage();
            degenUsage.AssignOwnership(degenSubjectMembership, degenSubjectParameter);

            var degenEnd = new Feature { IsEnd = true };
            degenEnd.AssignOwnership(new ReferenceSubsetting { ReferencedFeature = degenSubjectParameter });

            var degenBindingConnector = new BindingConnector();
            degenBindingConnector.AssignOwnership(new FeatureMembership(), degenEnd);

            degenUsage.AssignOwnership(new FeatureMembership(), degenBindingConnector);

            Assert.That(degenUsage.ComputeSatisfyingFeature(), Is.Null);
        }
    }
}
