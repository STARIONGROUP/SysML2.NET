// -------------------------------------------------------------------------------------------------
// <copyright file="ConnectorExtensionsTestFixture.cs" company="Starion Group S.A.">
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
    using SysML2.NET.Core.POCO.Kernel.Associations;
    using SysML2.NET.Core.POCO.Kernel.Connectors;
    using SysML2.NET.Extensions;

    using PocoType = SysML2.NET.Core.POCO.Core.Types.Type;

    [TestFixture]
    public class ConnectorExtensionsTestFixture
    {
        [Test]
        public void VerifyComputeAssociation()
        {
            Assert.That(() => ((IConnector)null).ComputeAssociation(), Throws.TypeOf<ArgumentNullException>());

            var connector = new Connector();

            // Empty: no OwnedRelationship → returns empty list.
            Assert.That(connector.ComputeAssociation(), Is.Empty);

            // Negative: FeatureTyping pointing at a non-Association Type → excluded.
            var nonAssociationType = new PocoType();
            var typingToNonAssociation = new FeatureTyping { Type = nonAssociationType };
            connector.AssignOwnership(typingToNonAssociation);

            Assert.That(connector.ComputeAssociation(), Is.Empty);

            // Positive: FeatureTyping pointing at an Association → returned.
            var association1 = new Association();
            var typingToAssociation1 = new FeatureTyping { Type = association1 };
            connector.AssignOwnership(typingToAssociation1);

            Assert.That(connector.ComputeAssociation(), Is.EqualTo(new[] { association1 }));

            // Populated: second Association also returned, in iteration order.
            var association2 = new Association();
            var typingToAssociation2 = new FeatureTyping { Type = association2 };
            connector.AssignOwnership(typingToAssociation2);

            Assert.That(connector.ComputeAssociation(), Is.EqualTo(new[] { association1, association2 }));
        }

        [Test]
        public void VerifyComputeConnectorEnd()
        {
            Assert.That(() => ((IConnector)null).ComputeConnectorEnd(), Throws.TypeOf<ArgumentNullException>());

            var connector = new Connector();

            // Empty: no features → empty list.
            Assert.That(connector.ComputeConnectorEnd(), Is.Empty);

            // Negative: feature with IsEnd = false → excluded.
            var nonEndFeature = new Feature { IsEnd = false };
            var nonEndMembership = new FeatureMembership();
            connector.AssignOwnership(nonEndMembership, nonEndFeature);

            Assert.That(connector.ComputeConnectorEnd(), Is.Empty);

            // Positive: feature with IsEnd = true → returned.
            var endFeature1 = new Feature { IsEnd = true };
            var endMembership1 = new FeatureMembership();
            connector.AssignOwnership(endMembership1, endFeature1);

            Assert.That(connector.ComputeConnectorEnd(), Is.EqualTo(new[] { endFeature1 }));

            // Discrimination: second IsEnd=true also returned, IsEnd=false remains excluded.
            var endFeature2 = new Feature { IsEnd = true };
            var endMembership2 = new FeatureMembership();
            connector.AssignOwnership(endMembership2, endFeature2);

            Assert.That(connector.ComputeConnectorEnd(), Is.EqualTo(new[] { endFeature1, endFeature2 }));
        }

        [Test]
        public void VerifyComputeDefaultFeaturingType()
        {
            Assert.That(() => ((IConnector)null).ComputeDefaultFeaturingType(), Throws.TypeOf<ArgumentNullException>());

            var connector = new Connector();

            // Empty: no relatedFeatures (no connectorEnds) → null.
            Assert.That(connector.ComputeDefaultFeaturingType(), Is.Null);

            // Set up helper: creates a connector end Feature with a ReferenceSubsetting pointing at
            // a relatedTarget Feature, and adds the end to the connector via FeatureMembership.
            // Returns the relatedTarget Feature so callers can wire featuringType on it.
            static Feature AddConnectorEnd(Connector owningConnector, Feature relatedTarget)
            {
                var endFeature = new Feature { IsEnd = true };
                var referenceSubsetting = new ReferenceSubsetting { ReferencedFeature = relatedTarget };
                endFeature.AssignOwnership(referenceSubsetting);

                var membership = new FeatureMembership();
                owningConnector.AssignOwnership(membership, endFeature);

                return endFeature;
            }

            // Single relatedFeature with no featuringType → closure = {relatedTarget itself};
            // relatedTarget.IsFeaturedWithin(relatedTarget): featuringType is empty → All() is
            // vacuously true → relatedTarget qualifies as a common featuring type.
            // nearestCommonFeaturingTypes = {relatedTarget} (nothing dominates it) → returns relatedTarget.
            var relatedTarget1 = new Feature();
            AddConnectorEnd(connector, relatedTarget1);

            Assert.That(connector.ComputeDefaultFeaturingType(), Is.SameAs(relatedTarget1));

            // Two relatedFeatures sharing exactly one common featuringType T:
            // Build a fresh connector to avoid state from the previous case.
            var connector2 = new Connector();
            var commonType = new PocoType();

            var sharedTarget1 = new Feature();
            var typeFeaturing1 = new TypeFeaturing { FeatureOfType = sharedTarget1, FeaturingType = commonType };
            sharedTarget1.AssignOwnership(typeFeaturing1);

            var sharedTarget2 = new Feature();
            var typeFeaturing2 = new TypeFeaturing { FeatureOfType = sharedTarget2, FeaturingType = commonType };
            sharedTarget2.AssignOwnership(typeFeaturing2);

            AddConnectorEnd(connector2, sharedTarget1);
            AddConnectorEnd(connector2, sharedTarget2);

            Assert.That(connector2.ComputeDefaultFeaturingType(), Is.SameAs(commonType));

            // Disjoint featuringTypes → no common type → null.
            var connector3 = new Connector();
            var typeForFirst = new PocoType();
            var typeForSecond = new PocoType();

            var disjointTarget1 = new Feature();
            var typeFeaturingDisjoint1 = new TypeFeaturing { FeatureOfType = disjointTarget1, FeaturingType = typeForFirst };
            disjointTarget1.AssignOwnership(typeFeaturingDisjoint1);

            var disjointTarget2 = new Feature();
            var typeFeaturingDisjoint2 = new TypeFeaturing { FeatureOfType = disjointTarget2, FeaturingType = typeForSecond };
            disjointTarget2.AssignOwnership(typeFeaturingDisjoint2);

            AddConnectorEnd(connector3, disjointTarget1);
            AddConnectorEnd(connector3, disjointTarget2);

            Assert.That(connector3.ComputeDefaultFeaturingType(), Is.Null);

            // Cycle: featureA.featuringType = [featureB], featureB.featuringType = [featureA].
            // The closure must terminate (cycle-safe BFS); result is deterministic.
            var connector4 = new Connector();
            var cycleFeatureA = new Feature();
            var cycleFeatureB = new Feature();

            var typeFeaturingAtB = new TypeFeaturing { FeatureOfType = cycleFeatureA, FeaturingType = cycleFeatureB };
            cycleFeatureA.AssignOwnership(typeFeaturingAtB);

            var typeFeaturingBtoA = new TypeFeaturing { FeatureOfType = cycleFeatureB, FeaturingType = cycleFeatureA };
            cycleFeatureB.AssignOwnership(typeFeaturingBtoA);

            AddConnectorEnd(connector4, cycleFeatureA);
            AddConnectorEnd(connector4, cycleFeatureB);

            // Must not throw or infinite-loop; result may be null or one of the features depending
            // on the IsFeaturedWithin evaluation — we just verify it terminates and returns a value.
            Assert.That(() => connector4.ComputeDefaultFeaturingType(), Throws.Nothing);
        }

        [Test]
        public void VerifyComputeRelatedFeature()
        {
            Assert.That(() => ((IConnector)null).ComputeRelatedFeature(), Throws.TypeOf<ArgumentNullException>());

            var connector = new Connector();

            // Empty: no connectorEnds → empty list.
            Assert.That(connector.ComputeRelatedFeature(), Is.Empty);

            // ConnectorEnd with null ownedReferenceSubsetting → filtered out → empty.
            var endWithNoSubsetting = new Feature { IsEnd = true };
            var noSubsettingMembership = new FeatureMembership();
            connector.AssignOwnership(noSubsettingMembership, endWithNoSubsetting);

            Assert.That(connector.ComputeRelatedFeature(), Is.Empty);

            // ConnectorEnd with a ReferenceSubsetting whose SubsettedFeature is set → that feature returned.
            var targetFeature1 = new Feature();
            var referenceSubsetting1 = new ReferenceSubsetting { ReferencedFeature = targetFeature1 };
            var endWithSubsetting1 = new Feature { IsEnd = true };
            endWithSubsetting1.AssignOwnership(referenceSubsetting1);

            var subsettingMembership1 = new FeatureMembership();
            connector.AssignOwnership(subsettingMembership1, endWithSubsetting1);

            Assert.That(connector.ComputeRelatedFeature(), Is.EqualTo(new[] { targetFeature1 }));

            // Second end with a distinct SubsettedFeature → both returned in connector-end order.
            var targetFeature2 = new Feature();
            var referenceSubsetting2 = new ReferenceSubsetting { ReferencedFeature = targetFeature2 };
            var endWithSubsetting2 = new Feature { IsEnd = true };
            endWithSubsetting2.AssignOwnership(referenceSubsetting2);

            var subsettingMembership2 = new FeatureMembership();
            connector.AssignOwnership(subsettingMembership2, endWithSubsetting2);

            Assert.That(connector.ComputeRelatedFeature(), Is.EqualTo(new[] { targetFeature1, targetFeature2 }));
        }

        [Test]
        public void VerifyComputeSourceFeature()
        {
            Assert.That(() => ((IConnector)null).ComputeSourceFeature(), Throws.TypeOf<ArgumentNullException>());

            var connector = new Connector();

            // Empty: no relatedFeatures → null.
            Assert.That(connector.ComputeSourceFeature(), Is.Null);

            // Wire one connectorEnd with a ReferencedFeature so relatedFeature is populated.
            var targetFeature1 = new Feature();
            var referenceSubsetting1 = new ReferenceSubsetting { ReferencedFeature = targetFeature1 };
            var endFeature1 = new Feature { IsEnd = true };
            endFeature1.AssignOwnership(referenceSubsetting1);

            var membership1 = new FeatureMembership();
            connector.AssignOwnership(membership1, endFeature1);

            // Single relatedFeature → that feature is the source.
            Assert.That(connector.ComputeSourceFeature(), Is.SameAs(targetFeature1));

            // Two relatedFeatures → first is returned (not the second).
            var targetFeature2 = new Feature();
            var referenceSubsetting2 = new ReferenceSubsetting { ReferencedFeature = targetFeature2 };
            var endFeature2 = new Feature { IsEnd = true };
            endFeature2.AssignOwnership(referenceSubsetting2);

            var membership2 = new FeatureMembership();
            connector.AssignOwnership(membership2, endFeature2);

            Assert.That(connector.ComputeSourceFeature(), Is.SameAs(targetFeature1));
        }

        [Test]
        public void VerifyComputeTargetFeature()
        {
            Assert.That(() => ((IConnector)null).ComputeTargetFeature(), Throws.TypeOf<ArgumentNullException>());

            var connector = new Connector();

            // Zero relatedFeatures → empty list (not null).
            Assert.That(connector.ComputeTargetFeature(), Is.Empty);

            // One relatedFeature → still empty (size < 2).
            var targetFeature1 = new Feature();
            var referenceSubsetting1 = new ReferenceSubsetting { ReferencedFeature = targetFeature1 };
            var endFeature1 = new Feature { IsEnd = true };
            endFeature1.AssignOwnership(referenceSubsetting1);

            var membership1 = new FeatureMembership();
            connector.AssignOwnership(membership1, endFeature1);

            Assert.That(connector.ComputeTargetFeature(), Is.Empty);

            // Two relatedFeatures [target1, target2] → targetFeature = [target2].
            var targetFeature2 = new Feature();
            var referenceSubsetting2 = new ReferenceSubsetting { ReferencedFeature = targetFeature2 };
            var endFeature2 = new Feature { IsEnd = true };
            endFeature2.AssignOwnership(referenceSubsetting2);

            var membership2 = new FeatureMembership();
            connector.AssignOwnership(membership2, endFeature2);

            Assert.That(connector.ComputeTargetFeature(), Is.EqualTo(new[] { targetFeature2 }));

            // Three relatedFeatures [target1, target2, target3] → targetFeature = [target2, target3].
            var targetFeature3 = new Feature();
            var referenceSubsetting3 = new ReferenceSubsetting { ReferencedFeature = targetFeature3 };
            var endFeature3 = new Feature { IsEnd = true };
            endFeature3.AssignOwnership(referenceSubsetting3);

            var membership3 = new FeatureMembership();
            connector.AssignOwnership(membership3, endFeature3);

            Assert.That(connector.ComputeTargetFeature(), Is.EqualTo(new[] { targetFeature2, targetFeature3 }));
        }
    }
}
