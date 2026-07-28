// -------------------------------------------------------------------------------------------------
// <copyright file="ConstraintUsageExtensionsTestFixture.cs" company="Starion Group S.A.">
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
    using SysML2.NET.Core.POCO.Kernel.Functions;
    using SysML2.NET.Core.POCO.Systems.Calculations;
    using SysML2.NET.Core.POCO.Systems.Constraints;
    using SysML2.NET.Core.POCO.Systems.Parts;
    using SysML2.NET.Core.POCO.Systems.Requirements;
    using SysML2.NET.Exceptions;
    using SysML2.NET.Extensions;

    [TestFixture]
    public class ConstraintUsageExtensionsTestFixture
    {
        [Test]
        public void VerifyComputeConstraintDefinition()
        {
            Assert.That(() => ((IConstraintUsage)null).ComputeConstraintDefinition(), Throws.TypeOf<ArgumentNullException>());

            // Empty OwnedRelationship → no FeatureTyping → null.
            var constraintUsage = new ConstraintUsage();

            Assert.That(constraintUsage.ComputeConstraintDefinition(), Is.Null);

            // Discrimination: FeatureTyping targets a CalculationDefinition (a Function, NOT an
            // IPredicate) — filtered out → still null.
            var calculationDefinition = new CalculationDefinition();
            constraintUsage.AssignOwnership(new FeatureTyping { Type = calculationDefinition });
            Assert.That(constraintUsage.ComputeConstraintDefinition(), Is.Null);

            // Populated case: FeatureTyping whose Type is an IPredicate (ConstraintDefinition) → returned.
            var constraintDefinition = new ConstraintDefinition();
            constraintUsage.AssignOwnership(new FeatureTyping { Type = constraintDefinition });
            Assert.That(constraintUsage.ComputeConstraintDefinition(), Is.SameAs(constraintDefinition));

            // [0..1] upper-bound violation: two matching typings → MultiplicityViolationException.
            var secondConstraintDefinition = new ConstraintDefinition();
            constraintUsage.AssignOwnership(new FeatureTyping { Type = secondConstraintDefinition });
            Assert.That(() => constraintUsage.ComputeConstraintDefinition(), Throws.TypeOf<MultiplicityViolationException>());
        }

        [Test]
        public void VerifyComputeRedefinedNamingFeatureOperation()
        {
            Assert.That(() => ((IConstraintUsage)null).ComputeRedefinedNamingFeatureOperation(), Throws.TypeOf<ArgumentNullException>());

            // No owningFeatureMembership → else branch delegates to UsageExtensions.ComputeRedefinedNamingFeatureOperation.
            // With no OwnedRelationship (no Redefinition) and not a VariantMembership → null.
            var elseFallbackUsage = new ConstraintUsage();

            Assert.That(elseFallbackUsage.ComputeRedefinedNamingFeatureOperation(), Is.Null);

            // owningFeatureMembership IS a RequirementConstraintMembership AND ownedReferenceSubsetting present →
            // returns ownedReferenceSubsetting.ReferencedFeature.featureTarget (which is itself for a feature
            // with no chainingFeatures).
            var owningRequirementUsage = new RequirementUsage();
            var requirementConstraintMembership = new RequirementConstraintMembership();
            var usageInBranch1 = new ConstraintUsage();
            owningRequirementUsage.AssignOwnership(requirementConstraintMembership, usageInBranch1);

            var refTarget = new Feature();
            var referenceSubsetting = new ReferenceSubsetting { ReferencedFeature = refTarget };
            usageInBranch1.AssignOwnership(referenceSubsetting);

            Assert.That(usageInBranch1.ComputeRedefinedNamingFeatureOperation(), Is.SameAs(refTarget));

            // owningFeatureMembership is RequirementConstraintMembership but NO ownedReferenceSubsetting →
            // condition fails → else branch → returns null (no Redefinition).
            var owningRequirementUsage2 = new RequirementUsage();
            var rcmNoRef = new RequirementConstraintMembership();
            var usageNoRef = new ConstraintUsage();
            owningRequirementUsage2.AssignOwnership(rcmNoRef, usageNoRef);

            Assert.That(usageNoRef.ComputeRedefinedNamingFeatureOperation(), Is.Null);
        }

        [Test]
        public void VerifyComputeRedefinedModelLevelEvaluableOperation()
        {
            Assert.That(() => ((IConstraintUsage)null).ComputeRedefinedModelLevelEvaluableOperation([]), Throws.TypeOf<ArgumentNullException>());

            // OCL body is literally `false` — independent of subject state and of the visited list.
            var usage = new ConstraintUsage();

            using (Assert.EnterMultipleScope())
            {
                Assert.That(usage.ComputeRedefinedModelLevelEvaluableOperation([]), Is.False);
                Assert.That(usage.ComputeRedefinedModelLevelEvaluableOperation(null), Is.False);
                Assert.That(usage.ComputeRedefinedModelLevelEvaluableOperation([new Feature()]), Is.False);
            }
        }
    }
}
