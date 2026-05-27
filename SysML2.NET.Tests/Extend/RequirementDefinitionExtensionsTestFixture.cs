// -------------------------------------------------------------------------------------------------
// <copyright file="RequirementDefinitionExtensionsTestFixture.cs" company="Starion Group S.A.">
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
    using SysML2.NET.Core.POCO.Kernel.Behaviors;
    using SysML2.NET.Core.POCO.Root.Annotations;
    using SysML2.NET.Core.POCO.Systems.Constraints;
    using SysML2.NET.Core.POCO.Systems.DefinitionAndUsage;
    using SysML2.NET.Core.POCO.Systems.Parts;
    using SysML2.NET.Core.POCO.Systems.Requirements;
    using SysML2.NET.Core.Systems.Requirements;
    using SysML2.NET.Extensions;

    [TestFixture]
    public class RequirementDefinitionExtensionsTestFixture
    {
        private static readonly string[] ExpectedSingleComputedText = ["The requirement text."];
        private static readonly string[] ExpectedMultipleComputedText = ["The requirement text.", "Additional context."];

        [Test]
        public void VerifyComputeActorParameter()
        {
            Assert.That(() => ((IRequirementDefinition)null).ComputeActorParameter(), Throws.TypeOf<ArgumentNullException>());

            var requirementDefinition = new RequirementDefinition();

            Assert.That(requirementDefinition.ComputeActorParameter(), Is.Empty);

            // Discrimination: add a ParameterMembership (not ActorMembership) — must be excluded from result.
            var parameterMembership = new ParameterMembership();
            var parameterUsage = new Usage();
            requirementDefinition.AssignOwnership(parameterMembership, parameterUsage);

            Assert.That(requirementDefinition.ComputeActorParameter(), Is.Empty);

            // Populated case: ActorMembership is present; selecting ownedActorParameter triggers an
            // upstream stub (ActorMembershipExtensions.ComputeOwnedActorParameter is not yet implemented).
            var actorMembership = new ActorMembership();
            var actorPartUsage = new PartUsage();
            requirementDefinition.AssignOwnership(actorMembership, actorPartUsage);

            Assert.That(() => requirementDefinition.ComputeActorParameter(), Throws.TypeOf<NotSupportedException>());
        }

        [Test]
        public void VerifyComputeAssumedConstraint()
        {
            Assert.That(() => ((IRequirementDefinition)null).ComputeAssumedConstraint(), Throws.TypeOf<ArgumentNullException>());

            var requirementDefinition = new RequirementDefinition();

            Assert.That(requirementDefinition.ComputeAssumedConstraint(), Is.Empty);

            // Type discrimination: add a FeatureMembership (not IRequirementConstraintMembership) — excluded.
            var featureMembership = new FeatureMembership();
            var featureUsage = new Usage();
            requirementDefinition.AssignOwnership(featureMembership, featureUsage);

            Assert.That(requirementDefinition.ComputeAssumedConstraint(), Is.Empty);

            // Kind discrimination: add a RequirementConstraintMembership with Kind = Requirement — excluded.
            var requiredMembership = new RequirementConstraintMembership { Kind = RequirementConstraintKind.Requirement };
            var requiredConstraintUsage = new ConstraintUsage();
            requirementDefinition.AssignOwnership(requiredMembership, requiredConstraintUsage);

            Assert.That(requirementDefinition.ComputeAssumedConstraint(), Is.Empty);

            // Populated case: RequirementConstraintMembership with Kind = Assumption — the Kind
            // filter selects it, and ownedConstraint surfaces the wired ConstraintUsage.
            var assumedMembership = new RequirementConstraintMembership { Kind = RequirementConstraintKind.Assumption };
            var assumedConstraintUsage = new ConstraintUsage();
            requirementDefinition.AssignOwnership(assumedMembership, assumedConstraintUsage);

            Assert.That(requirementDefinition.ComputeAssumedConstraint(), Is.EqualTo([assumedConstraintUsage]));
        }

        [Test]
        public void VerifyComputeFramedConcern()
        {
            Assert.That(() => ((IRequirementDefinition)null).ComputeFramedConcern(), Throws.TypeOf<ArgumentNullException>());

            var requirementDefinition = new RequirementDefinition();

            Assert.That(requirementDefinition.ComputeFramedConcern(), Is.Empty);

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
            requirementDefinition.AssignOwnership(framedMembership, concernUsage);

            Assert.That(() => requirementDefinition.ComputeFramedConcern(), Throws.TypeOf<NotSupportedException>());
        }

        [Test]
        public void VerifyComputeRequiredConstraint()
        {
            Assert.That(() => ((IRequirementDefinition)null).ComputeRequiredConstraint(), Throws.TypeOf<ArgumentNullException>());

            var requirementDefinition = new RequirementDefinition();

            Assert.That(requirementDefinition.ComputeRequiredConstraint(), Is.Empty);

            // Type discrimination: add a FeatureMembership (not IRequirementConstraintMembership) — excluded.
            var featureMembership = new FeatureMembership();
            var featureUsage = new Usage();
            requirementDefinition.AssignOwnership(featureMembership, featureUsage);

            Assert.That(requirementDefinition.ComputeRequiredConstraint(), Is.Empty);

            // Kind discrimination: add a RequirementConstraintMembership with Kind = Assumption — excluded.
            var assumedMembership = new RequirementConstraintMembership { Kind = RequirementConstraintKind.Assumption };
            var assumedConstraintUsage = new ConstraintUsage();
            requirementDefinition.AssignOwnership(assumedMembership, assumedConstraintUsage);

            Assert.That(requirementDefinition.ComputeRequiredConstraint(), Is.Empty);

            // Populated case: RequirementConstraintMembership with Kind = Requirement — the Kind
            // filter selects it, and ownedConstraint surfaces the wired ConstraintUsage.
            var requiredMembership = new RequirementConstraintMembership { Kind = RequirementConstraintKind.Requirement };
            var requiredConstraintUsage = new ConstraintUsage();
            requirementDefinition.AssignOwnership(requiredMembership, requiredConstraintUsage);

            Assert.That(requirementDefinition.ComputeRequiredConstraint(), Is.EqualTo([requiredConstraintUsage]));
        }

        [Test]
        public void VerifyComputeStakeholderParameter()
        {
            Assert.That(() => ((IRequirementDefinition)null).ComputeStakeholderParameter(), Throws.TypeOf<ArgumentNullException>());

            var requirementDefinition = new RequirementDefinition();

            Assert.That(requirementDefinition.ComputeStakeholderParameter(), Is.Empty);

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
            requirementDefinition.AssignOwnership(stakeholderMembership, stakeholderPartUsage);

            Assert.That(() => requirementDefinition.ComputeStakeholderParameter(), Throws.TypeOf<NotSupportedException>());
        }

        [Test]
        public void VerifyComputeSubjectParameter()
        {
            Assert.That(() => ((IRequirementDefinition)null).ComputeSubjectParameter(), Throws.TypeOf<ArgumentNullException>());

            var requirementDefinition = new RequirementDefinition();

            // Empty case: no SubjectMembership in featureMembership → null.
            Assert.That(requirementDefinition.ComputeSubjectParameter(), Is.Null);

            // Discrimination: add a ParameterMembership (not SubjectMembership) → still null.
            var parameterMembership = new ParameterMembership();
            var parameterUsage = new Usage();
            requirementDefinition.AssignOwnership(parameterMembership, parameterUsage);

            Assert.That(requirementDefinition.ComputeSubjectParameter(), Is.Null);

            // Populated case: SubjectMembership is present alongside the earlier ParameterMembership.
            // OfType<ISubjectMembership> must discriminate — only the subject's ownedSubjectParameter surfaces.
            // This also covers the mixed-state discrimination: both a ParameterMembership and a SubjectMembership
            // are wired; the result must be the subject usage, not the parameter usage.
            var subjectMembership = new SubjectMembership();
            var subjectUsage = new Usage();
            requirementDefinition.AssignOwnership(subjectMembership, subjectUsage);

            Assert.That(requirementDefinition.ComputeSubjectParameter(), Is.SameAs(subjectUsage));
        }

        [Test]
        public void VerifyComputeText()
        {
            Assert.That(() => ((IRequirementDefinition)null).ComputeText(), Throws.TypeOf<ArgumentNullException>());

            var requirementDefinition = new RequirementDefinition();

            // Empty case: no Documentation elements → empty list.
            Assert.That(requirementDefinition.ComputeText(), Is.Empty);

            // One Documentation with a body.
            var firstDocumentation = new Documentation { Body = "The requirement text." };
            var firstAnnotation = new Annotation();
            requirementDefinition.AssignOwnership(firstAnnotation, firstDocumentation);

            Assert.That(requirementDefinition.ComputeText(), Is.EqualTo(ExpectedSingleComputedText));

            // Two Documentation elements — both bodies appear in order.
            var secondDocumentation = new Documentation { Body = "Additional context." };
            var secondAnnotation = new Annotation();
            requirementDefinition.AssignOwnership(secondAnnotation, secondDocumentation);

            Assert.That(requirementDefinition.ComputeText(), Is.EqualTo(ExpectedMultipleComputedText));
        }
    }
}
