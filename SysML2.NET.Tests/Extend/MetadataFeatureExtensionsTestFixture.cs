// -------------------------------------------------------------------------------------------------
// <copyright file="MetadataFeatureExtensionsTestFixture.cs" company="Starion Group S.A.">
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
    using SysML2.NET.Core.POCO.Kernel.FeatureValues;
    using SysML2.NET.Core.POCO.Kernel.Functions;
    using SysML2.NET.Core.POCO.Kernel.Metadata;
    using SysML2.NET.Extensions;

    [TestFixture]
    public class MetadataFeatureExtensionsTestFixture
    {
        [Test]
        public void VerifyComputeMetaclass()
        {
            Assert.That(() => ((IMetadataFeature)null).ComputeMetaclass(), Throws.TypeOf<ArgumentNullException>());

            var metadataFeature = new MetadataFeature();

            // Empty: no FeatureTyping → null.
            Assert.That(metadataFeature.ComputeMetaclass(), Is.Null);

            // Negative: FeatureTyping pointing at a non-Metaclass Type → null.
            var nonMetaclassType = new FeatureTyping { Type = new Feature() };
            metadataFeature.AssignOwnership(nonMetaclassType);

            Assert.That(metadataFeature.ComputeMetaclass(), Is.Null);

            // Positive: FeatureTyping pointing at a Metaclass → returned.
            var metaclass1 = new Metaclass();
            var typingToMetaclass1 = new FeatureTyping { Type = metaclass1 };
            metadataFeature.AssignOwnership(typingToMetaclass1);

            Assert.That(metadataFeature.ComputeMetaclass(), Is.SameAs(metaclass1));

            // Multiple Metaclass typings → first returned (insertion order).
            var metaclass2 = new Metaclass();
            var typingToMetaclass2 = new FeatureTyping { Type = metaclass2 };
            metadataFeature.AssignOwnership(typingToMetaclass2);

            Assert.That(metadataFeature.ComputeMetaclass(), Is.SameAs(metaclass1));
        }

        [Test]
        public void VerifyComputeEvaluateFeatureOperation()
        {
            // Null guard on subject.
            Assert.That(() => ((IMetadataFeature)null).ComputeEvaluateFeatureOperation(new Feature()), Throws.TypeOf<ArgumentNullException>());

            var metadataFeature = new MetadataFeature();
            var baseFeature = new Feature();

            // Null baseFeature: throws ArgumentNullException (matching subject null-guard convention).
            Assert.That(() => metadataFeature.ComputeEvaluateFeatureOperation(null), Throws.TypeOf<ArgumentNullException>());

            // Empty feature list: MetadataFeature with no feature members → [].
            Assert.That(metadataFeature.ComputeEvaluateFeatureOperation(baseFeature), Is.Empty);

            // Negative discrimination: feature present whose redefinition closure does NOT include baseFeature → [].
            var unrelatedFeature = new Feature();
            var unrelatedMembership = new FeatureMembership();
            metadataFeature.AssignOwnership(unrelatedMembership, unrelatedFeature);

            Assert.That(metadataFeature.ComputeEvaluateFeatureOperation(baseFeature), Is.Empty);

            // Direct match: candidate IS baseFeature (closure includes start node).
            // No FeatureValue owned by the closure → returns [].
            var metadataFeature2 = new MetadataFeature();
            var directMembership = new FeatureMembership();
            metadataFeature2.AssignOwnership(directMembership, baseFeature);

            Assert.That(metadataFeature2.ComputeEvaluateFeatureOperation(baseFeature), Is.Empty);

            // Transitive match: candidate redefines baseFeature via a one-step chain.
            // No FeatureValue → still returns [].
            var metadataFeature3 = new MetadataFeature();
            var transitiveCandidate = new Feature();
            var transitiveRedefinition = new Redefinition { RedefinedFeature = baseFeature };
            transitiveCandidate.AssignOwnership(transitiveRedefinition);

            var transitiveMembership = new FeatureMembership();
            metadataFeature3.AssignOwnership(transitiveMembership, transitiveCandidate);

            Assert.That(metadataFeature3.ComputeEvaluateFeatureOperation(baseFeature), Is.Empty);

            // Cycle test: featureA.ownedRedefinition → featureB, featureB.ownedRedefinition → featureA.
            // Closure helper must terminate; if the cycle does not include baseFeature → [].
            var metadataFeature4 = new MetadataFeature();
            var cycleFeatureA = new Feature();
            var cycleFeatureB = new Feature();

            var redefinitionAtoB = new Redefinition { RedefinedFeature = cycleFeatureB };
            cycleFeatureA.AssignOwnership(redefinitionAtoB);

            var redefinitionBtoA = new Redefinition { RedefinedFeature = cycleFeatureA };
            cycleFeatureB.AssignOwnership(redefinitionBtoA);

            var cycleMembership = new FeatureMembership();
            metadataFeature4.AssignOwnership(cycleMembership, cycleFeatureA);

            // Must not throw or infinite-loop; cycle does not include baseFeature → terminates with [].
            Assert.That(() => metadataFeature4.ComputeEvaluateFeatureOperation(baseFeature), Throws.Nothing);
            Assert.That(metadataFeature4.ComputeEvaluateFeatureOperation(baseFeature), Is.Empty);

            // Matched candidate with a FeatureValue whose value is a non-null Expression that has
            // NO ResultExpressionMembership wired. Expression.Evaluate returns an empty list, so
            // the operation returns the same empty list. (The stub-blocker case where Evaluate
            // throws NotSupportedException via ResultExpressionMembershipExtensions.ComputeOwnedResultExpression
            // cannot be wired here without modifying the sibling stub — scope discipline.)
            var metadataFeature5 = new MetadataFeature();
            var matchingFeature = new Feature();
            var redefinitionToBase = new Redefinition { RedefinedFeature = baseFeature };
            matchingFeature.AssignOwnership(redefinitionToBase);

            var featureValue = new FeatureValue();
            var valueExpression = new Expression();
            matchingFeature.AssignOwnership(featureValue, valueExpression);

            var matchingMembership = new FeatureMembership();
            metadataFeature5.AssignOwnership(matchingMembership, matchingFeature);

            Assert.That(metadataFeature5.ComputeEvaluateFeatureOperation(baseFeature), Is.Empty);
        }

        [Test]
        public void VerifyComputeIsSemanticOperation()
        {
            // Null guard.
            Assert.That(() => ((IMetadataFeature)null).ComputeIsSemanticOperation(), Throws.TypeOf<ArgumentNullException>());

            var metadataFeature = new MetadataFeature();

            // Empty: no library specialization context → SpecializesFromLibrary("Metaobjects::SemanticMetadata")
            // resolves to null → returns false.
            Assert.That(metadataFeature.ComputeIsSemanticOperation(), Is.False);

            // Discrimination: having a supertype that is a different library element still returns false.
            // Wire a non-SemanticMetadata OwningMembership-contained type so the specialization chain is
            // present but does not match the "Metaobjects::SemanticMetadata" qualified name.
            var metadataFeature2 = new MetadataFeature();
            var unrelatedSupertype = new Feature();
            var subsetting = new Subsetting { SubsettedFeature = unrelatedSupertype };
            metadataFeature2.AssignOwnership(subsetting);

            Assert.That(metadataFeature2.ComputeIsSemanticOperation(), Is.False);

            // NOTE: Wiring a true positive (returns true) requires constructing a "Metaobjects::SemanticMetadata"
            // library namespace reachable via ResolveGlobal, which is infrastructure not available in unit tests.
            // Integration-level coverage is required for the true-positive path.
        }

        [Test]
        public void VerifyComputeIsSyntacticOperation()
        {
            // Null guard.
            Assert.That(() => ((IMetadataFeature)null).ComputeIsSyntacticOperation(), Throws.TypeOf<ArgumentNullException>());

            var metadataFeature = new MetadataFeature();

            // Empty: no library specialization context → SpecializesFromLibrary("KerML::Element")
            // resolves to null → returns false.
            Assert.That(metadataFeature.ComputeIsSyntacticOperation(), Is.False);

            // Discrimination: having a supertype that is a different library element still returns false.
            var metadataFeature2 = new MetadataFeature();
            var unrelatedSupertype = new Feature();
            var subsetting = new Subsetting { SubsettedFeature = unrelatedSupertype };
            metadataFeature2.AssignOwnership(subsetting);

            Assert.That(metadataFeature2.ComputeIsSyntacticOperation(), Is.False);

            // NOTE: Wiring a true positive (returns true) requires constructing a "KerML::Element" library
            // namespace reachable via ResolveGlobal, which is infrastructure not available in unit tests.
            // Integration-level coverage is required for the true-positive path.
        }

        [Test]
        public void VerifyComputeSyntaxElementOperation_ThrowsNotSupportedException()
        {
            // Pending MOF reflective metaclass registry. Follow-up issue required.
            // The KerML spec defines this operation with body "No OCL"; computing syntaxElement requires
            // an inverse map from a runtime MetadataFeature back to the Element it reflects — infrastructure
            // that does not yet exist in this SDK.
            Assert.That(() => new MetadataFeature().ComputeSyntaxElementOperation(), Throws.TypeOf<NotSupportedException>());
        }
    }
}
