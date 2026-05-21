// -------------------------------------------------------------------------------------------------
// <copyright file="RequirementUsageExtensionsTestFixture.cs" company="Starion Group S.A.">
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
    using SysML2.NET.Core.POCO.Kernel.Behaviors;
    using SysML2.NET.Core.POCO.Root.Annotations;
    using SysML2.NET.Core.POCO.Systems.Constraints;
    using SysML2.NET.Core.POCO.Systems.DefinitionAndUsage;
    using SysML2.NET.Core.POCO.Systems.Parts;
    using SysML2.NET.Core.POCO.Systems.Requirements;
    using SysML2.NET.Core.Systems.Requirements;
    using SysML2.NET.Extensions;

    [TestFixture]
    public class RequirementUsageExtensionsTestFixture
    {
        [Test]
        public void VerifyComputeActorParameter()
        {
            Assert.That(() => ((IRequirementUsage)null).ComputeActorParameter(), Throws.TypeOf<ArgumentNullException>());

            var requirementUsage = new RequirementUsage();

            Assert.That(requirementUsage.ComputeActorParameter(), Is.Empty);

            // Discrimination: add a ParameterMembership (not ActorMembership) — must be excluded from result.
            var parameterMembership = new ParameterMembership();
            var parameterUsage = new Usage();
            requirementUsage.AssignOwnership(parameterMembership, parameterUsage);

            Assert.That(requirementUsage.ComputeActorParameter(), Is.Empty);

            // Populated case: ActorMembership is present; selecting ownedActorParameter triggers an
            // upstream stub (ActorMembershipExtensions.ComputeOwnedActorParameter is not yet implemented).
            var actorMembership = new ActorMembership();
            var actorPartUsage = new PartUsage();
            requirementUsage.AssignOwnership(actorMembership, actorPartUsage);

            Assert.That(() => requirementUsage.ComputeActorParameter(), Throws.TypeOf<NotSupportedException>());
        }

        [Test]
        public void VerifyComputeAssumedConstraint()
        {
            Assert.That(() => ((IRequirementUsage)null).ComputeAssumedConstraint(), Throws.TypeOf<ArgumentNullException>());

            var requirementUsage = new RequirementUsage();

            Assert.That(requirementUsage.ComputeAssumedConstraint(), Is.Empty);

            // Type discrimination: add a FeatureMembership (not IRequirementConstraintMembership) — excluded.
            var featureMembership = new FeatureMembership();
            var featureUsage = new Usage();
            requirementUsage.AssignOwnership(featureMembership, featureUsage);

            Assert.That(requirementUsage.ComputeAssumedConstraint(), Is.Empty);

            // Kind discrimination: add a RequirementConstraintMembership with Kind = Requirement — excluded.
            var requiredMembership = new RequirementConstraintMembership { Kind = RequirementConstraintKind.Requirement };
            var requiredConstraintUsage = new ConstraintUsage();
            requirementUsage.AssignOwnership(requiredMembership, requiredConstraintUsage);

            Assert.That(requirementUsage.ComputeAssumedConstraint(), Is.Empty);

            // Populated case: RequirementConstraintMembership with Kind = Assumption; selecting
            // ownedConstraint triggers an upstream stub (RequirementConstraintMembershipExtensions
            // .ComputeOwnedConstraint is not yet implemented).
            var assumedMembership = new RequirementConstraintMembership { Kind = RequirementConstraintKind.Assumption };
            var assumedConstraintUsage = new ConstraintUsage();
            requirementUsage.AssignOwnership(assumedMembership, assumedConstraintUsage);

            Assert.That(() => requirementUsage.ComputeAssumedConstraint(), Throws.TypeOf<NotSupportedException>());
        }

        [Test]
        public void VerifyComputeFramedConcern()
        {
            Assert.That(() => ((IRequirementUsage)null).ComputeFramedConcern(), Throws.TypeOf<ArgumentNullException>());

            var requirementUsage = new RequirementUsage();

            Assert.That(requirementUsage.ComputeFramedConcern(), Is.Empty);

            // Discrimination cannot be tested at this layer: any IFeatureMembership subtype (including
            // RequirementConstraintMembership) causes subject.featureMembership to traverse
            // RemoveRedefinedFeatures → IFeatureMembership.ownedMemberFeature, which dispatches to the
            // stubbed RequirementConstraintMembershipExtensions.ComputeOwnedConstraint and throws
            // NotSupportedException — identical to the populated stub-blocker case below.
            // The discrimination block is omitted until upstream stubs are implemented.

            // Populated case: FramedConcernMembership is present; selecting ownedConcern triggers an
            // upstream stub (FramedConcernMembershipExtensions.ComputeOwnedConcern is not yet implemented).
            var framedMembership = new FramedConcernMembership();
            var concernUsage = new ConcernUsage();
            requirementUsage.AssignOwnership(framedMembership, concernUsage);

            Assert.That(() => requirementUsage.ComputeFramedConcern(), Throws.TypeOf<NotSupportedException>());
        }

        [Test]
        public void VerifyComputeRequiredConstraint()
        {
            Assert.That(() => ((IRequirementUsage)null).ComputeRequiredConstraint(), Throws.TypeOf<ArgumentNullException>());

            var requirementUsage = new RequirementUsage();

            Assert.That(requirementUsage.ComputeRequiredConstraint(), Is.Empty);

            // Type discrimination: add a FeatureMembership (not IRequirementConstraintMembership) — excluded.
            var featureMembership = new FeatureMembership();
            var featureUsage = new Usage();
            requirementUsage.AssignOwnership(featureMembership, featureUsage);

            Assert.That(requirementUsage.ComputeRequiredConstraint(), Is.Empty);

            // Kind discrimination: add a RequirementConstraintMembership with Kind = Assumption — excluded.
            var assumedMembership = new RequirementConstraintMembership { Kind = RequirementConstraintKind.Assumption };
            var assumedConstraintUsage = new ConstraintUsage();
            requirementUsage.AssignOwnership(assumedMembership, assumedConstraintUsage);

            Assert.That(requirementUsage.ComputeRequiredConstraint(), Is.Empty);

            // Populated case: RequirementConstraintMembership with Kind = Requirement; selecting
            // ownedConstraint triggers an upstream stub (RequirementConstraintMembershipExtensions
            // .ComputeOwnedConstraint is not yet implemented).
            var requiredMembership = new RequirementConstraintMembership { Kind = RequirementConstraintKind.Requirement };
            var requiredConstraintUsage = new ConstraintUsage();
            requirementUsage.AssignOwnership(requiredMembership, requiredConstraintUsage);

            Assert.That(() => requirementUsage.ComputeRequiredConstraint(), Throws.TypeOf<NotSupportedException>());
        }

        [Test]
        public void VerifyComputeRequirementDefinition()
        {
            Assert.That(() => ((IRequirementUsage)null).ComputeRequirementDefinition(), Throws.TypeOf<ArgumentNullException>());

            var requirementUsage = new RequirementUsage();

            // Empty case: no OwnedRelationship → returns null.
            Assert.That(requirementUsage.ComputeRequirementDefinition(), Is.Null);

            // Negative case: FeatureTyping whose Type is a ConstraintDefinition — no IRequirementDefinition match → null.
            var constraintDefinition = new ConstraintDefinition();
            var typingToConstraint = new FeatureTyping { Type = constraintDefinition };
            requirementUsage.AssignOwnership(typingToConstraint);

            Assert.That(requirementUsage.ComputeRequirementDefinition(), Is.Null);

            // Positive case: add a FeatureTyping whose Type is a RequirementDefinition → it is returned.
            var requirementDefinition = new RequirementDefinition();
            var typingToRequirement = new FeatureTyping { Type = requirementDefinition };
            requirementUsage.AssignOwnership(typingToRequirement);

            Assert.That(requirementUsage.ComputeRequirementDefinition(), Is.EqualTo(requirementDefinition));

            // Multiple typings: add a second RequirementDefinition; FirstOrDefault returns the first match.
            var secondRequirementDefinition = new RequirementDefinition();
            var typingToSecond = new FeatureTyping { Type = secondRequirementDefinition };
            requirementUsage.AssignOwnership(typingToSecond);

            Assert.That(requirementUsage.ComputeRequirementDefinition(), Is.EqualTo(requirementDefinition));
        }

        [Test]
        public void VerifyComputeStakeholderParameter()
        {
            Assert.That(() => ((IRequirementUsage)null).ComputeStakeholderParameter(), Throws.TypeOf<ArgumentNullException>());

            var requirementUsage = new RequirementUsage();

            Assert.That(requirementUsage.ComputeStakeholderParameter(), Is.Empty);

            // Discrimination cannot be tested at this layer: any IFeatureMembership subtype (including
            // ActorMembership) causes subject.featureMembership to traverse RemoveRedefinedFeatures →
            // IFeatureMembership.ownedMemberFeature, which dispatches to the stubbed
            // ActorMembershipExtensions.ComputeOwnedActorParameter and throws NotSupportedException —
            // identical to the populated stub-blocker case below.
            // The discrimination block is omitted until upstream stubs are implemented.

            // Populated case: StakeholderMembership is present; selecting ownedStakeholderParameter
            // triggers an upstream stub (StakeholderMembershipExtensions.ComputeOwnedStakeholderParameter
            // is not yet implemented).
            var stakeholderMembership = new StakeholderMembership();
            var stakeholderPartUsage = new PartUsage();
            requirementUsage.AssignOwnership(stakeholderMembership, stakeholderPartUsage);

            Assert.That(() => requirementUsage.ComputeStakeholderParameter(), Throws.TypeOf<NotSupportedException>());
        }

        [Test]
        public void VerifyComputeSubjectParameter()
        {
            Assert.That(() => ((IRequirementUsage)null).ComputeSubjectParameter(), Throws.TypeOf<ArgumentNullException>());

            var requirementUsage = new RequirementUsage();

            // Empty case: no SubjectMembership in featureMembership → null.
            Assert.That(requirementUsage.ComputeSubjectParameter(), Is.Null);

            // Discrimination: add a ParameterMembership (not SubjectMembership) → still null.
            var parameterMembership = new ParameterMembership();
            var parameterUsage = new Usage();
            requirementUsage.AssignOwnership(parameterMembership, parameterUsage);

            Assert.That(requirementUsage.ComputeSubjectParameter(), Is.Null);

            // Populated case: SubjectMembership is present; selecting ownedSubjectParameter triggers an
            // upstream stub (SubjectMembershipExtensions.ComputeOwnedSubjectParameter is not yet implemented).
            var subjectMembership = new SubjectMembership();
            var subjectUsage = new Usage();
            requirementUsage.AssignOwnership(subjectMembership, subjectUsage);

            Assert.That(() => requirementUsage.ComputeSubjectParameter(), Throws.TypeOf<NotSupportedException>());
        }

        private static readonly string[] ExpectedSingleComputedText = new[] { "The requirement text." };
        private static readonly string[] ExpectedMultipleComputedText = new[] { "The requirement text.", "Additional context." };

        [Test]
        public void VerifyComputeText()
        {
            Assert.That(() => ((IRequirementUsage)null).ComputeText(), Throws.TypeOf<ArgumentNullException>());

            var requirementUsage = new RequirementUsage();

            // Empty case: no Documentation elements → empty list.
            Assert.That(requirementUsage.ComputeText(), Is.Empty);

            // One Documentation with a body.
            var firstDocumentation = new Documentation { Body = "The requirement text." };
            var firstAnnotation = new Annotation();
            requirementUsage.AssignOwnership(firstAnnotation, firstDocumentation);

            Assert.That(requirementUsage.ComputeText(), Is.EqualTo(ExpectedSingleComputedText));

            // Two Documentation elements — both bodies appear in order.
            var secondDocumentation = new Documentation { Body = "Additional context." };
            var secondAnnotation = new Annotation();
            requirementUsage.AssignOwnership(secondAnnotation, secondDocumentation);

            Assert.That(requirementUsage.ComputeText(), Is.EqualTo(ExpectedMultipleComputedText));
        }
    }
}
