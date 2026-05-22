// -------------------------------------------------------------------------------------------------
// <copyright file="FlowExtensionsTestFixture.cs" company="Starion Group S.A.">
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

    using SysML2.NET.Core.POCO.Core.Classifiers;
    using SysML2.NET.Core.POCO.Core.Features;
    using SysML2.NET.Core.POCO.Core.Types;
    using SysML2.NET.Core.POCO.Kernel.Interactions;
    using SysML2.NET.Extensions;

    using PocoType = SysML2.NET.Core.POCO.Core.Types.Type;

    [TestFixture]
    public class FlowExtensionsTestFixture
    {
        [Test]
        public void VerifyComputeFlowEnd()
        {
            Assert.That(() => ((IFlow)null).ComputeFlowEnd(), Throws.TypeOf<ArgumentNullException>());

            var flow = new Flow();

            // Empty: no connectorEnds → empty list.
            Assert.That(flow.ComputeFlowEnd(), Is.Empty);

            // Discrimination: a plain Feature (IsEnd=true) wired via FeatureMembership is NOT a FlowEnd → excluded.
            var plainEndFeature = new Feature { IsEnd = true };
            var plainEndMembership = new FeatureMembership();
            flow.AssignOwnership(plainEndMembership, plainEndFeature);

            Assert.That(flow.ComputeFlowEnd(), Is.Empty);

            // Positive: FlowEnd with IsEnd=true → returned.
            var flowEnd1 = new FlowEnd { IsEnd = true };
            var flowEndMembership1 = new FeatureMembership();
            flow.AssignOwnership(flowEndMembership1, flowEnd1);

            Assert.That(flow.ComputeFlowEnd(), Is.EqualTo([flowEnd1]));

            // Populated: second FlowEnd also returned, in iteration order.
            var flowEnd2 = new FlowEnd { IsEnd = true };
            var flowEndMembership2 = new FeatureMembership();
            flow.AssignOwnership(flowEndMembership2, flowEnd2);

            Assert.That(flow.ComputeFlowEnd(), Is.EqualTo([flowEnd1, flowEnd2]));
        }

        [Test]
        public void VerifyComputeInteraction()
        {
            Assert.That(() => ((IFlow)null).ComputeInteraction(), Throws.TypeOf<ArgumentNullException>());

            var flow = new Flow();

            // Empty: no OwnedRelationship → empty list.
            Assert.That(flow.ComputeInteraction(), Is.Empty);

            // Discrimination: FeatureTyping pointing at a non-Interaction Type → excluded.
            var nonInteractionType = new PocoType();
            var typingToNonInteraction = new FeatureTyping { Type = nonInteractionType };
            flow.AssignOwnership(typingToNonInteraction);

            Assert.That(flow.ComputeInteraction(), Is.Empty);

            // Positive: FeatureTyping pointing at an Interaction → returned.
            var interaction1 = new Interaction();
            var typingToInteraction1 = new FeatureTyping { Type = interaction1 };
            flow.AssignOwnership(typingToInteraction1);

            Assert.That(flow.ComputeInteraction(), Is.EqualTo([interaction1]));

            // Populated: second Interaction also returned, in iteration order.
            var interaction2 = new Interaction();
            var typingToInteraction2 = new FeatureTyping { Type = interaction2 };
            flow.AssignOwnership(typingToInteraction2);

            Assert.That(flow.ComputeInteraction(), Is.EqualTo([interaction1, interaction2]));
        }

        [Test]
        public void VerifyComputePayloadFeature()
        {
            Assert.That(() => ((IFlow)null).ComputePayloadFeature(), Throws.TypeOf<ArgumentNullException>());

            var flow = new Flow();

            // Empty: no ownedFeature → null.
            Assert.That(flow.ComputePayloadFeature(), Is.Null);

            // Discrimination: plain Feature (not PayloadFeature) wired via FeatureMembership → null.
            var plainFeature = new Feature();
            var plainFeatureMembership = new FeatureMembership();
            flow.AssignOwnership(plainFeatureMembership, plainFeature);

            Assert.That(flow.ComputePayloadFeature(), Is.Null);

            // Positive: PayloadFeature wired via FeatureMembership → returned.
            var payloadFeature1 = new PayloadFeature();
            var payloadMembership1 = new FeatureMembership();
            flow.AssignOwnership(payloadMembership1, payloadFeature1);

            Assert.That(flow.ComputePayloadFeature(), Is.SameAs(payloadFeature1));

            // Multiple PayloadFeatures → first one returned (index 0).
            var payloadFeature2 = new PayloadFeature();
            var payloadMembership2 = new FeatureMembership();
            flow.AssignOwnership(payloadMembership2, payloadFeature2);

            Assert.That(flow.ComputePayloadFeature(), Is.SameAs(payloadFeature1));
        }

        [Test]
        public void VerifyComputePayloadType()
        {
            Assert.That(() => ((IFlow)null).ComputePayloadType(), Throws.TypeOf<ArgumentNullException>());

            var flow = new Flow();

            // Empty: no PayloadFeature → empty list (short-circuits).
            Assert.That(flow.ComputePayloadType(), Is.Empty);

            // Wire a PayloadFeature with no types → empty list.
            var payloadFeatureEmpty = new PayloadFeature();
            var emptyPayloadMembership = new FeatureMembership();
            flow.AssignOwnership(emptyPayloadMembership, payloadFeatureEmpty);

            Assert.That(flow.ComputePayloadType(), Is.Empty);

            // Build a fresh flow for the discrimination and positive cases.
            var flow2 = new Flow();
            var payloadFeature = new PayloadFeature();
            var payloadMembership = new FeatureMembership();
            flow2.AssignOwnership(payloadMembership, payloadFeature);

            // Discrimination: non-Classifier IType in payload feature type → excluded.
            var nonClassifierType = new PocoType();
            var typingToNonClassifier = new FeatureTyping { Type = nonClassifierType };
            payloadFeature.AssignOwnership(typingToNonClassifier);

            Assert.That(flow2.ComputePayloadType(), Is.Empty);

            // Positive: Classifier type in payload feature → returned.
            var classifier1 = new Classifier();
            var typingToClassifier1 = new FeatureTyping { Type = classifier1 };
            payloadFeature.AssignOwnership(typingToClassifier1);

            Assert.That(flow2.ComputePayloadType(), Is.EqualTo([classifier1]));

            // Multiple Classifier types → all returned.
            var classifier2 = new Classifier();
            var typingToClassifier2 = new FeatureTyping { Type = classifier2 };
            payloadFeature.AssignOwnership(typingToClassifier2);

            Assert.That(flow2.ComputePayloadType(), Is.EqualTo([classifier1, classifier2]));
        }

        [Test]
        public void VerifyComputeSourceOutputFeature()
        {
            Assert.That(() => ((IFlow)null).ComputeSourceOutputFeature(), Throws.TypeOf<ArgumentNullException>());

            var flow = new Flow();

            // Empty connectorEnd → null.
            Assert.That(flow.ComputeSourceOutputFeature(), Is.Null);

            // One connectorEnd with no ownedFeature → flat sequence empty → null.
            var end1 = new Feature { IsEnd = true };
            var endMembership1 = new FeatureMembership();
            flow.AssignOwnership(endMembership1, end1);

            Assert.That(flow.ComputeSourceOutputFeature(), Is.Null);

            // One connectorEnd with one ownedFeature → that feature returned.
            var innerFeatureA = new Feature();
            var innerMembershipA = new FeatureMembership();
            end1.AssignOwnership(innerMembershipA, innerFeatureA);

            Assert.That(flow.ComputeSourceOutputFeature(), Is.SameAs(innerFeatureA));

            // Two connectorEnds (each with one ownedFeature) → first end's first ownedFeature returned.
            var end2 = new Feature { IsEnd = true };
            var endMembership2 = new FeatureMembership();
            flow.AssignOwnership(endMembership2, end2);

            var innerFeatureB = new Feature();
            var innerMembershipB = new FeatureMembership();
            end2.AssignOwnership(innerMembershipB, innerFeatureB);

            Assert.That(flow.ComputeSourceOutputFeature(), Is.SameAs(innerFeatureA));

            // Empty first end + non-empty second end → second end's first feature returned.
            var flow2 = new Flow();

            var emptyEnd = new Feature { IsEnd = true };
            var emptyEndMembership = new FeatureMembership();
            flow2.AssignOwnership(emptyEndMembership, emptyEnd);

            var nonEmptyEnd = new Feature { IsEnd = true };
            var nonEmptyEndMembership = new FeatureMembership();
            flow2.AssignOwnership(nonEmptyEndMembership, nonEmptyEnd);

            var secondEndFeature = new Feature();
            var secondEndInnerMembership = new FeatureMembership();
            nonEmptyEnd.AssignOwnership(secondEndInnerMembership, secondEndFeature);

            Assert.That(flow2.ComputeSourceOutputFeature(), Is.SameAs(secondEndFeature));
        }

        [Test]
        public void VerifyComputeTargetInputFeature()
        {
            Assert.That(() => ((IFlow)null).ComputeTargetInputFeature(), Throws.TypeOf<ArgumentNullException>());

            var flow = new Flow();

            // Zero connectorEnds → Count < 2 → null.
            Assert.That(flow.ComputeTargetInputFeature(), Is.Null);

            // One connectorEnd → Count == 1 < 2 → null.
            var end1 = new Feature { IsEnd = true };
            var endMembership1 = new FeatureMembership();
            flow.AssignOwnership(endMembership1, end1);

            Assert.That(flow.ComputeTargetInputFeature(), Is.Null);

            // Two connectorEnds, second has empty ownedFeature → null.
            var end2 = new Feature { IsEnd = true };
            var endMembership2 = new FeatureMembership();
            flow.AssignOwnership(endMembership2, end2);

            Assert.That(flow.ComputeTargetInputFeature(), Is.Null);

            // Two connectorEnds, second has one ownedFeature → that feature returned.
            var targetFeature = new Feature();
            var targetMembership = new FeatureMembership();
            end2.AssignOwnership(targetMembership, targetFeature);

            Assert.That(flow.ComputeTargetInputFeature(), Is.SameAs(targetFeature));

            // Three connectorEnds — still returns second end's feature (NOT third end).
            var end3 = new Feature { IsEnd = true };
            var endMembership3 = new FeatureMembership();
            flow.AssignOwnership(endMembership3, end3);

            var thirdEndFeature = new Feature();
            var thirdEndMembership = new FeatureMembership();
            end3.AssignOwnership(thirdEndMembership, thirdEndFeature);

            Assert.That(flow.ComputeTargetInputFeature(), Is.SameAs(targetFeature));
        }
    }
}
