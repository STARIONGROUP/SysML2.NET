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
    using SysML2.NET.Core.POCO.Systems.Constraints;
    using SysML2.NET.Core.POCO.Systems.Parts;
    using SysML2.NET.Core.POCO.Systems.Requirements;
    using SysML2.NET.Extensions;

    [TestFixture]
    public class ConstraintUsageExtensionsTestFixture
    {
        [Test]
        public void VerifyComputeConstraintDefinition()
        {
            Assert.That(() => ((IConstraintUsage)null).ComputeConstraintDefinition(), Throws.TypeOf<ArgumentNullException>());

            // Empty OwnedRelationship → no FeatureTyping → null.
            var emptyUsage = new ConstraintUsage();

            Assert.That(emptyUsage.ComputeConstraintDefinition(), Is.Null);

            // FeatureTyping whose Type is a non-Predicate → returns null (no IPredicate match).
            var nonPredicateUsage = new ConstraintUsage();
            var partDefinition = new PartDefinition();
            var nonPredicateTyping = new FeatureTyping { Type = partDefinition };
            nonPredicateUsage.AssignOwnership(nonPredicateTyping);

            Assert.That(nonPredicateUsage.ComputeConstraintDefinition(), Is.Null);

            // FeatureTyping whose Type is a Predicate → returns it.
            var predicateUsage = new ConstraintUsage();
            var predicate = new Predicate();
            var predicateTyping = new FeatureTyping { Type = predicate };
            predicateUsage.AssignOwnership(predicateTyping);

            Assert.That(predicateUsage.ComputeConstraintDefinition(), Is.SameAs(predicate));

            // FeatureTyping whose Type is a ConstraintDefinition (which IS-A Predicate) → returns it.
            var constraintDefUsage = new ConstraintUsage();
            var constraintDefinition = new ConstraintDefinition();
            var constraintDefTyping = new FeatureTyping { Type = constraintDefinition };
            constraintDefUsage.AssignOwnership(constraintDefTyping);

            Assert.That(constraintDefUsage.ComputeConstraintDefinition(), Is.SameAs(constraintDefinition));
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
