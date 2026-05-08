// -------------------------------------------------------------------------------------------------
// <copyright file="FeatureExtensionsTestFixture.cs" company="Starion Group S.A.">
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

    using SysML2.NET.Core.Core.Types;
    using SysML2.NET.Core.POCO.Core.Features;
    using SysML2.NET.Core.POCO.Core.Types;
    using SysML2.NET.Core.POCO.Root.Namespaces;
    using SysML2.NET.Core.Root.Namespaces;
    using SysML2.NET.Extensions;

    using Type = SysML2.NET.Core.POCO.Core.Types.Type;

    [TestFixture]
    public class FeatureExtensionsTestFixture
    {
        [Test]
        public void VerifyComputeOwnedFeatureChaining()
        {
            Assert.That(() => ((IFeature)null).ComputeOwnedFeatureChaining(), Throws.TypeOf<ArgumentNullException>());

            var feature = new Feature();

            Assert.That(feature.ComputeOwnedFeatureChaining(), Has.Count.EqualTo(0));

            var subsetting = new Subsetting();
            feature.AssignOwnership(subsetting);

            Assert.That(feature.ComputeOwnedFeatureChaining(), Has.Count.EqualTo(0));

            var chainingTarget1 = new Feature();
            var chaining1 = new FeatureChaining { ChainingFeature = chainingTarget1 };
            feature.AssignOwnership(chaining1);

            var chainingTarget2 = new Feature();
            var chaining2 = new FeatureChaining { ChainingFeature = chainingTarget2 };
            feature.AssignOwnership(chaining2);

            var result = feature.ComputeOwnedFeatureChaining();

            using (Assert.EnterMultipleScope())
            {
                Assert.That(result, Has.Count.EqualTo(2));
                Assert.That(result[0], Is.SameAs(chaining1));
                Assert.That(result[1], Is.SameAs(chaining2));
            }
        }

        [Test]
        public void VerifyComputeOwnedFeatureInverting()
        {
            Assert.That(() => ((IFeature)null).ComputeOwnedFeatureInverting(), Throws.TypeOf<ArgumentNullException>());

            var feature = new Feature();

            Assert.That(feature.ComputeOwnedFeatureInverting(), Has.Count.EqualTo(0));

            var otherFeature = new Feature();
            var invertingPointingElsewhere = new FeatureInverting { FeatureInverted = otherFeature };
            feature.AssignOwnership(invertingPointingElsewhere);

            Assert.That(feature.ComputeOwnedFeatureInverting(), Has.Count.EqualTo(0));

            var invertingPointingSelf = new FeatureInverting { FeatureInverted = feature };
            feature.AssignOwnership(invertingPointingSelf);

            var result = feature.ComputeOwnedFeatureInverting();

            using (Assert.EnterMultipleScope())
            {
                Assert.That(result, Has.Count.EqualTo(1));
                Assert.That(result[0], Is.SameAs(invertingPointingSelf));
            }
        }

        [Test]
        public void VerifyComputeOwnedTypeFeaturing()
        {
            Assert.That(() => ((IFeature)null).ComputeOwnedTypeFeaturing(), Throws.TypeOf<ArgumentNullException>());

            var feature = new Feature();

            Assert.That(feature.ComputeOwnedTypeFeaturing(), Has.Count.EqualTo(0));

            var otherFeature = new Feature();
            var typeFeaturingPointingElsewhere = new TypeFeaturing { FeatureOfType = otherFeature };
            feature.AssignOwnership(typeFeaturingPointingElsewhere);

            Assert.That(feature.ComputeOwnedTypeFeaturing(), Has.Count.EqualTo(0));

            var featuringType = new Type();
            var typeFeaturingPointingSelf = new TypeFeaturing { FeatureOfType = feature, FeaturingType = featuringType };
            feature.AssignOwnership(typeFeaturingPointingSelf);

            var result = feature.ComputeOwnedTypeFeaturing();

            using (Assert.EnterMultipleScope())
            {
                Assert.That(result, Has.Count.EqualTo(1));
                Assert.That(result[0], Is.SameAs(typeFeaturingPointingSelf));
            }
        }

        [Test]
        public void VerifyComputeOwnedSubsetting()
        {
            Assert.That(() => ((IFeature)null).ComputeOwnedSubsetting(), Throws.TypeOf<ArgumentNullException>());

            var feature = new Feature();

            Assert.That(feature.ComputeOwnedSubsetting(), Has.Count.EqualTo(0));

            var specialization = new Specialization();
            feature.AssignOwnership(specialization);

            Assert.That(feature.ComputeOwnedSubsetting(), Has.Count.EqualTo(0));

            var subsetting = new Subsetting();
            feature.AssignOwnership(subsetting);

            var redefinition = new Redefinition();
            feature.AssignOwnership(redefinition);

            var referenceSubsetting = new ReferenceSubsetting();
            feature.AssignOwnership(referenceSubsetting);

            var result = feature.ComputeOwnedSubsetting();

            Assert.That(result, Has.Count.EqualTo(3));
        }

        [Test]
        public void VerifyComputeOwnedRedefinition()
        {
            Assert.That(() => ((IFeature)null).ComputeOwnedRedefinition(), Throws.TypeOf<ArgumentNullException>());

            var feature = new Feature();

            Assert.That(feature.ComputeOwnedRedefinition(), Has.Count.EqualTo(0));

            var subsetting = new Subsetting();
            feature.AssignOwnership(subsetting);

            Assert.That(feature.ComputeOwnedRedefinition(), Has.Count.EqualTo(0));

            var redefinition1 = new Redefinition();
            feature.AssignOwnership(redefinition1);

            var redefinition2 = new Redefinition();
            feature.AssignOwnership(redefinition2);

            var result = feature.ComputeOwnedRedefinition();

            using (Assert.EnterMultipleScope())
            {
                Assert.That(result, Has.Count.EqualTo(2));
                Assert.That(result, Does.Contain(redefinition1));
                Assert.That(result, Does.Contain(redefinition2));
            }
        }

        [Test]
        public void VerifyComputeOwnedReferenceSubsetting()
        {
            Assert.That(() => ((IFeature)null).ComputeOwnedReferenceSubsetting(), Throws.TypeOf<ArgumentNullException>());

            var feature = new Feature();

            Assert.That(feature.ComputeOwnedReferenceSubsetting(), Is.Null);

            var referenceSubsetting = new ReferenceSubsetting();
            feature.AssignOwnership(referenceSubsetting);

            Assert.That(feature.ComputeOwnedReferenceSubsetting(), Is.SameAs(referenceSubsetting));
        }

        [Test]
        public void VerifyComputeOwnedCrossSubsetting()
        {
            Assert.That(() => ((IFeature)null).ComputeOwnedCrossSubsetting(), Throws.TypeOf<ArgumentNullException>());

            var feature = new Feature();

            Assert.That(feature.ComputeOwnedCrossSubsetting(), Is.Null);

            var crossSubsetting = new CrossSubsetting();
            feature.AssignOwnership(crossSubsetting);

            Assert.That(feature.ComputeOwnedCrossSubsetting(), Is.SameAs(crossSubsetting));
        }

        [Test]
        public void VerifyComputeOwnedTyping()
        {
            Assert.That(() => ((IFeature)null).ComputeOwnedTyping(), Throws.TypeOf<ArgumentNullException>());

            var feature = new Feature();

            Assert.That(feature.ComputeOwnedTyping(), Has.Count.EqualTo(0));

            var specialization = new Specialization();
            feature.AssignOwnership(specialization);

            Assert.That(feature.ComputeOwnedTyping(), Has.Count.EqualTo(0));

            var type1 = new Type();
            var typing1 = new FeatureTyping { Type = type1 };
            feature.AssignOwnership(typing1);

            var type2 = new Type();
            var typing2 = new FeatureTyping { Type = type2 };
            feature.AssignOwnership(typing2);

            var result = feature.ComputeOwnedTyping();

            using (Assert.EnterMultipleScope())
            {
                Assert.That(result, Has.Count.EqualTo(2));
                Assert.That(result[0], Is.SameAs(typing1));
                Assert.That(result[1], Is.SameAs(typing2));
            }
        }

        [Test]
        public void VerifyComputeChainingFeature()
        {
            Assert.That(() => ((IFeature)null).ComputeChainingFeature(), Throws.TypeOf<ArgumentNullException>());

            var feature = new Feature();

            Assert.That(feature.ComputeChainingFeature(), Has.Count.EqualTo(0));

            var target1 = new Feature();
            var chaining1 = new FeatureChaining { ChainingFeature = target1 };
            feature.AssignOwnership(chaining1);

            var target2 = new Feature();
            var chaining2 = new FeatureChaining { ChainingFeature = target2 };
            feature.AssignOwnership(chaining2);

            var result = feature.ComputeChainingFeature();

            using (Assert.EnterMultipleScope())
            {
                Assert.That(result, Has.Count.EqualTo(2));
                Assert.That(result[0], Is.SameAs(target1));
                Assert.That(result[1], Is.SameAs(target2));
            }
        }

        [Test]
        public void VerifyComputeOwningFeatureMembership()
        {
            Assert.That(() => ((IFeature)null).ComputeOwningFeatureMembership(), Throws.TypeOf<ArgumentNullException>());

            var feature = new Feature();

            Assert.That(feature.ComputeOwningFeatureMembership(), Is.Null);

            var ownerWithMembership = new Type();
            var membership = new OwningMembership();
            ownerWithMembership.AssignOwnership(membership, feature);

            Assert.That(feature.ComputeOwningFeatureMembership(), Is.Null);

            var feature2 = new Feature();
            var ownerWithFeatureMembership = new Type();
            var featureMembership = new FeatureMembership();
            ownerWithFeatureMembership.AssignOwnership(featureMembership, feature2);

            Assert.That(feature2.ComputeOwningFeatureMembership(), Is.SameAs(featureMembership));
        }

        [Test]
        public void VerifyComputeOwningType()
        {
            Assert.That(() => ((IFeature)null).ComputeOwningType(), Throws.TypeOf<ArgumentNullException>());

            var feature = new Feature();

            Assert.That(feature.ComputeOwningType(), Is.Null);

            var ownerNamespace = new Type();
            var membership = new OwningMembership();
            ownerNamespace.AssignOwnership(membership, feature);

            Assert.That(feature.ComputeOwningType(), Is.Null);

            var feature2 = new Feature();
            var ownerType = new Type();
            var featureMembership = new FeatureMembership();
            ownerType.AssignOwnership(featureMembership, feature2);

            Assert.That(feature2.ComputeOwningType(), Is.SameAs(ownerType));
        }

        [Test]
        public void VerifyComputeFeatureTarget()
        {
            Assert.That(() => ((IFeature)null).ComputeFeatureTarget(), Throws.TypeOf<ArgumentNullException>());

            var feature = new Feature();

            Assert.That(feature.ComputeFeatureTarget(), Is.SameAs(feature));

            var target1 = new Feature();
            var chaining1 = new FeatureChaining { ChainingFeature = target1 };
            feature.AssignOwnership(chaining1);

            var lastTarget = new Feature();
            var chaining2 = new FeatureChaining { ChainingFeature = lastTarget };
            feature.AssignOwnership(chaining2);

            Assert.That(feature.ComputeFeatureTarget(), Is.SameAs(lastTarget));
        }

        [Test]
        public void VerifyComputeEndOwningType()
        {
            Assert.That(() => ((IFeature)null).ComputeEndOwningType(), Throws.TypeOf<ArgumentNullException>());

            var feature = new Feature();

            Assert.That(feature.ComputeEndOwningType(), Is.Null);

            var feature2 = new Feature();
            var ownerType = new Type();
            var regularFeatureMembership = new FeatureMembership();
            ownerType.AssignOwnership(regularFeatureMembership, feature2);

            Assert.That(feature2.ComputeEndOwningType(), Is.Null);

            var feature3 = new Feature();
            var endOwnerType = new Type();
            var endFeatureMembership = new EndFeatureMembership();
            endOwnerType.AssignOwnership(endFeatureMembership, feature3);

            Assert.That(feature3.ComputeEndOwningType(), Is.SameAs(endOwnerType));
        }

        [Test]
        public void VerifyComputeCrossFeature()
        {
            Assert.That(() => ((IFeature)null).ComputeCrossFeature(), Throws.TypeOf<ArgumentNullException>());

            var feature = new Feature();

            Assert.That(feature.ComputeCrossFeature(), Is.Null);

            var crossedFeature = new Feature();
            var crossSubsetting = new CrossSubsetting { CrossedFeature = crossedFeature };
            feature.AssignOwnership(crossSubsetting);

            Assert.That(feature.ComputeCrossFeature(), Is.Null);

            var chainingTarget1 = new Feature();
            var chaining1 = new FeatureChaining { ChainingFeature = chainingTarget1 };
            crossedFeature.AssignOwnership(chaining1);

            Assert.That(feature.ComputeCrossFeature(), Is.Null);

            var chainingTarget2 = new Feature();
            var chaining2 = new FeatureChaining { ChainingFeature = chainingTarget2 };
            crossedFeature.AssignOwnership(chaining2);

            Assert.That(feature.ComputeCrossFeature(), Is.SameAs(chainingTarget2));
        }

        [Test]
        public void VerifyComputeFeaturingType()
        {
            Assert.That(() => ((IFeature)null).ComputeFeaturingType(), Throws.TypeOf<ArgumentNullException>());

            var feature = new Feature();

            Assert.That(feature.ComputeFeaturingType(), Has.Count.EqualTo(0));

            var theType = new Type();
            var typeFeaturing = new TypeFeaturing { FeatureOfType = feature, FeaturingType = theType };
            feature.AssignOwnership(typeFeaturing);

            var result = feature.ComputeFeaturingType();

            using (Assert.EnterMultipleScope())
            {
                Assert.That(result, Has.Count.EqualTo(1));
                Assert.That(result[0], Is.SameAs(theType));
            }

            var chainingTarget = new Feature();
            var chainingTargetType = new Type();
            var chainingTypeFeaturing = new TypeFeaturing { FeatureOfType = chainingTarget, FeaturingType = chainingTargetType };
            chainingTarget.AssignOwnership(chainingTypeFeaturing);

            var chaining = new FeatureChaining { ChainingFeature = chainingTarget };
            feature.AssignOwnership(chaining);

            var resultWithChaining = feature.ComputeFeaturingType();

            Assert.That(resultWithChaining, Has.Count.EqualTo(2));
        }

        [Test]
        public void VerifyComputeType()
        {
            Assert.That(() => ((IFeature)null).ComputeType(), Throws.TypeOf<ArgumentNullException>());

            var feature = new Feature();

            Assert.That(feature.ComputeType(), Has.Count.EqualTo(0));

            var type1 = new Type();
            var typing1 = new FeatureTyping { Type = type1 };
            feature.AssignOwnership(typing1);

            var type2 = new Type();
            var typing2 = new FeatureTyping { Type = type2 };
            feature.AssignOwnership(typing2);

            var result = feature.ComputeType();

            using (Assert.EnterMultipleScope())
            {
                Assert.That(result, Has.Count.EqualTo(2));
                Assert.That(result[0], Is.SameAs(type1));
                Assert.That(result[1], Is.SameAs(type2));
            }
        }

        [Test]
        public void VerifyComputeNamingFeatureOperation()
        {
            Assert.That(() => ((IFeature)null).ComputeNamingFeatureOperation(), Throws.TypeOf<ArgumentNullException>());

            var feature = new Feature();

            Assert.That(feature.ComputeNamingFeatureOperation(), Is.Null);

            var redefinedFeature = new Feature { DeclaredName = "redefined" };
            var redefinition = new Redefinition { RedefinedFeature = redefinedFeature };
            feature.AssignOwnership(redefinition);

            Assert.That(feature.ComputeNamingFeatureOperation(), Is.SameAs(redefinedFeature));

            var redefinedFeature2 = new Feature { DeclaredName = "redefined2" };
            var redefinition2 = new Redefinition { RedefinedFeature = redefinedFeature2 };
            feature.AssignOwnership(redefinition2);

            Assert.That(feature.ComputeNamingFeatureOperation(), Is.SameAs(redefinedFeature));
        }

        [Test]
        public void VerifyComputeRedefinedEffectiveShortNameOperation()
        {
            Assert.That(() => ((IFeature)null).ComputeRedefinedEffectiveShortNameOperation(), Throws.TypeOf<ArgumentNullException>());

            var feature = new Feature();

            Assert.That(feature.ComputeRedefinedEffectiveShortNameOperation(), Is.Null);

            var feature2 = new Feature { DeclaredName = "someName" };

            Assert.That(feature2.ComputeRedefinedEffectiveShortNameOperation(), Is.Null);

            var feature3 = new Feature { DeclaredShortName = "sn" };

            Assert.That(feature3.ComputeRedefinedEffectiveShortNameOperation(), Is.EqualTo("sn"));

            var featureWithRedefinition = new Feature();
            var namingFeature = new Feature { DeclaredShortName = "nfShort" };
            var redefinition = new Redefinition { RedefinedFeature = namingFeature };
            featureWithRedefinition.AssignOwnership(redefinition);

            Assert.That(featureWithRedefinition.ComputeRedefinedEffectiveShortNameOperation(), Is.EqualTo("nfShort"));

            var featureNoNaming = new Feature();
            var redefinitionNoTarget = new Redefinition { RedefinedFeature = new Feature() };
            featureNoNaming.AssignOwnership(redefinitionNoTarget);

            Assert.That(featureNoNaming.ComputeRedefinedEffectiveShortNameOperation(), Is.Null);
        }

        [Test]
        public void VerifyComputeRedefinedEffectiveNameOperation()
        {
            Assert.That(() => ((IFeature)null).ComputeRedefinedEffectiveNameOperation(), Throws.TypeOf<ArgumentNullException>());

            var feature = new Feature();

            Assert.That(feature.ComputeRedefinedEffectiveNameOperation(), Is.Null);

            var feature2 = new Feature { DeclaredShortName = "sn" };

            Assert.That(feature2.ComputeRedefinedEffectiveNameOperation(), Is.Null);

            var feature3 = new Feature { DeclaredName = "theName" };

            Assert.That(feature3.ComputeRedefinedEffectiveNameOperation(), Is.EqualTo("theName"));

            var featureWithRedefinition = new Feature();
            var namingFeature = new Feature { DeclaredName = "nfName" };
            var redefinition = new Redefinition { RedefinedFeature = namingFeature };
            featureWithRedefinition.AssignOwnership(redefinition);

            Assert.That(featureWithRedefinition.ComputeRedefinedEffectiveNameOperation(), Is.EqualTo("nfName"));

            var featureNoNaming = new Feature();
            var redefinitionNoTarget = new Redefinition { RedefinedFeature = new Feature() };
            featureNoNaming.AssignOwnership(redefinitionNoTarget);

            Assert.That(featureNoNaming.ComputeRedefinedEffectiveNameOperation(), Is.Null);
        }

        [Test]
        public void VerifyComputeIsOwnedCrossFeatureOperation()
        {
            Assert.That(() => ((IFeature)null).ComputeIsOwnedCrossFeatureOperation(), Throws.TypeOf<ArgumentNullException>());

            var feature = new Feature();

            Assert.That(feature.ComputeIsOwnedCrossFeatureOperation(), Is.False);

            var nonFeatureOwner = new Type();
            var membership = new OwningMembership();
            nonFeatureOwner.AssignOwnership(membership, feature);

            Assert.That(feature.ComputeIsOwnedCrossFeatureOperation(), Is.False);

            var endFeature = new Feature { IsEnd = true };
            var ownerType = new Type();
            var endMembership = new EndFeatureMembership();
            ownerType.AssignOwnership(endMembership, endFeature);

            var crossCandidate = new Feature();
            var innerMembership = new OwningMembership();
            endFeature.AssignOwnership(innerMembership, crossCandidate);

            Assert.That(crossCandidate.ComputeIsOwnedCrossFeatureOperation(), Is.True);
        }

        [Test]
        public void VerifyComputeRedefinesOperation()
        {
            Assert.That(() => ((IFeature)null).ComputeRedefinesOperation(null), Throws.TypeOf<ArgumentNullException>());

            var feature = new Feature();

            var targetFeature = new Feature();

            Assert.That(feature.ComputeRedefinesOperation(targetFeature), Is.False);

            var otherFeature = new Feature();
            var redefinition = new Redefinition { RedefinedFeature = otherFeature };
            feature.AssignOwnership(redefinition);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(feature.ComputeRedefinesOperation(targetFeature), Is.False);
                Assert.That(feature.ComputeRedefinesOperation(otherFeature), Is.True);
            }
        }

        [Test]
        public void VerifyComputeDirectionForOperation()
        {
            Assert.That(() => ((IFeature)null).ComputeDirectionForOperation(null), Throws.TypeOf<ArgumentNullException>());

            var feature = new Feature();

            using (Assert.EnterMultipleScope())
            {
                // null type → result is null per the OCL guard.
                Assert.That(feature.ComputeDirectionForOperation(null), Is.Null);

                // a Type that has no relationship to the feature returns null direction.
                Assert.That(feature.ComputeDirectionForOperation(new Type()), Is.Null);
            }

            // populated: a feature owned by a Type via FeatureMembership returns its declared Direction.
            var directedFeature = new Feature { Direction = FeatureDirectionKind.In };
            var owningType = new Type();
            var featureMembership = new FeatureMembership();
            owningType.AssignOwnership(featureMembership, directedFeature);

            Assert.That(directedFeature.ComputeDirectionForOperation(owningType), Is.EqualTo(FeatureDirectionKind.In));
        }

        [Test]
        public void VerifyComputeOwnedCrossFeatureOperation()
        {
            Assert.That(() => ((IFeature)null).ComputeOwnedCrossFeatureOperation(), Throws.TypeOf<ArgumentNullException>());

            var feature = new Feature();

            Assert.That(feature.ComputeOwnedCrossFeatureOperation(), Is.Null);

            var endFeatureNoOwner = new Feature { IsEnd = true };

            Assert.That(endFeatureNoOwner.ComputeOwnedCrossFeatureOperation(), Is.Null);

            var endFeature = new Feature { IsEnd = true };
            var ownerType = new Type();
            var endMembership = new EndFeatureMembership();
            ownerType.AssignOwnership(endMembership, endFeature);

            Assert.That(endFeature.ComputeOwnedCrossFeatureOperation(), Is.Null);

            var multiplicityMember = new Multiplicity();
            var multMembership = new OwningMembership();
            endFeature.AssignOwnership(multMembership, multiplicityMember);

            Assert.That(endFeature.ComputeOwnedCrossFeatureOperation(), Is.Null);

            var featureMemberChild = new Feature();
            var featureMembership = new FeatureMembership();
            endFeature.AssignOwnership(featureMembership, featureMemberChild);

            Assert.That(endFeature.ComputeOwnedCrossFeatureOperation(), Is.Null);

            var plainFeature = new Feature();
            var plainMembership = new OwningMembership();
            endFeature.AssignOwnership(plainMembership, plainFeature);

            Assert.That(endFeature.ComputeOwnedCrossFeatureOperation(), Is.SameAs(plainFeature));
        }

        [Test]
        public void VerifyComputeIsCartesianProductOperation()
        {
            Assert.That(() => ((IFeature)null).ComputeIsCartesianProductOperation(), Throws.TypeOf<ArgumentNullException>());

            var feature = new Feature();

            Assert.That(feature.ComputeIsCartesianProductOperation(), Is.False);

            var theType = new Type();
            var typing = new FeatureTyping { Type = theType };
            feature.AssignOwnership(typing);

            Assert.That(feature.ComputeIsCartesianProductOperation(), Is.False);

            var featuringType = new Type();
            var typeFeaturing = new TypeFeaturing { FeatureOfType = feature, FeaturingType = featuringType };
            feature.AssignOwnership(typeFeaturing);

            Assert.That(feature.ComputeIsCartesianProductOperation(), Is.True);
        }

        [Test]
        public void VerifyComputeAsCartesianProductOperation()
        {
            Assert.That(() => ((IFeature)null).ComputeAsCartesianProductOperation(), Throws.TypeOf<ArgumentNullException>());

            var feature = new Feature();

            var result = feature.ComputeAsCartesianProductOperation();

            Assert.That(result, Has.Count.EqualTo(0));

            var theType = new Type();
            var typing = new FeatureTyping { Type = theType };
            feature.AssignOwnership(typing);

            result = feature.ComputeAsCartesianProductOperation();

            Assert.That(result, Has.Count.EqualTo(1));
            Assert.That(result[0], Is.SameAs(theType));

            var featuringType = new Type();
            var typeFeaturing = new TypeFeaturing { FeatureOfType = feature, FeaturingType = featuringType };
            feature.AssignOwnership(typeFeaturing);

            result = feature.ComputeAsCartesianProductOperation();

            using (Assert.EnterMultipleScope())
            {
                Assert.That(result, Has.Count.EqualTo(2));
                Assert.That(result[0], Is.SameAs(featuringType));
                Assert.That(result[1], Is.SameAs(theType));
            }
        }

        [Test]
        public void VerifyComputeIsFeaturingTypeOperation()
        {
            Assert.That(() => ((IFeature)null).ComputeIsFeaturingTypeOperation(null), Throws.TypeOf<ArgumentNullException>());

            var feature = new Feature();

            Assert.That(feature.ComputeIsFeaturingTypeOperation(new Type()), Is.False);

            var ownerType = new Type();
            var featureMembership = new FeatureMembership();
            ownerType.AssignOwnership(featureMembership, feature);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(feature.ComputeIsFeaturingTypeOperation(ownerType), Is.True);
                Assert.That(feature.ComputeIsFeaturingTypeOperation(new Type()), Is.False);
            }
        }

        [Test]
        public void VerifyComputeIsFeaturedWithinOperation()
        {
            Assert.That(() => ((IFeature)null).ComputeIsFeaturedWithinOperation(null), Throws.TypeOf<ArgumentNullException>());

            var feature = new Feature();

            using (Assert.EnterMultipleScope())
            {
                // type = null: with no featuringType entries, forAll over the empty set is vacuously true.
                Assert.That(feature.ComputeIsFeaturedWithinOperation(null), Is.True);

                // type non-null: with no featuringType entries, type.IsCompatibleWith forall is vacuously
                // true → returns true.
                Assert.That(feature.ComputeIsFeaturedWithinOperation(new Type()), Is.True);
            }

            // a feature whose featuringType is a specific Type is featured within that Type.
            var owningType = new Type();
            var owningTypeFeaturing = new TypeFeaturing { FeatureOfType = feature, FeaturingType = owningType };
            feature.AssignOwnership(owningTypeFeaturing, new Feature());

            using (Assert.EnterMultipleScope())
            {
                Assert.That(feature.ComputeIsFeaturedWithinOperation(owningType), Is.True);

                // and not within an unrelated Type.
                Assert.That(feature.ComputeIsFeaturedWithinOperation(new Type()), Is.False);
            }
        }

        [Test]
        public void VerifyComputeCanAccessOperation()
        {
            Assert.That(() => ((IFeature)null).ComputeCanAccessOperation(null), Throws.TypeOf<ArgumentNullException>());

            var feature = new Feature();

            // empty case: no featuringType on the subject feature → visited stays empty → returns false.
            Assert.That(feature.ComputeCanAccessOperation(new Feature()), Is.False);

            // populated: subject feature is featured by a Type, and the target feature is featured within
            // that same Type — subject can access target.
            var sharedType = new Type();
            var subjectTypeFeaturing = new TypeFeaturing { FeatureOfType = feature, FeaturingType = sharedType };
            feature.AssignOwnership(subjectTypeFeaturing, new Feature());

            var target = new Feature();
            var targetTypeFeaturing = new TypeFeaturing { FeatureOfType = target, FeaturingType = sharedType };
            target.AssignOwnership(targetTypeFeaturing, new Feature());

            Assert.That(feature.ComputeCanAccessOperation(target), Is.True);
        }

        [Test]
        public void VerifyComputeAllRedefinedFeaturesOperation()
        {
            Assert.That(() => ((IFeature)null).ComputeAllRedefinedFeaturesOperation(), Throws.TypeOf<ArgumentNullException>());

            var feature = new Feature();

            var result = feature.ComputeAllRedefinedFeaturesOperation();

            using (Assert.EnterMultipleScope())
            {
                Assert.That(result, Has.Count.EqualTo(1));
                Assert.That(result[0], Is.SameAs(feature));
            }

            var redefinedB = new Feature();
            var redefinitionAB = new Redefinition { RedefinedFeature = redefinedB };
            feature.AssignOwnership(redefinitionAB);

            result = feature.ComputeAllRedefinedFeaturesOperation();

            using (Assert.EnterMultipleScope())
            {
                Assert.That(result, Has.Count.EqualTo(2));
                Assert.That(result[0], Is.SameAs(feature));
                Assert.That(result[1], Is.SameAs(redefinedB));
            }

            var redefinedC = new Feature();
            var redefinitionBC = new Redefinition { RedefinedFeature = redefinedC };
            redefinedB.AssignOwnership(redefinitionBC);

            result = feature.ComputeAllRedefinedFeaturesOperation();

            using (Assert.EnterMultipleScope())
            {
                Assert.That(result, Has.Count.EqualTo(3));
                Assert.That(result[0], Is.SameAs(feature));
                Assert.That(result[1], Is.SameAs(redefinedB));
                Assert.That(result[2], Is.SameAs(redefinedC));
            }
        }

        [Test]
        public void VerifyComputeRedefinedSupertypesOperation()
        {
            Assert.That(() => ((IFeature)null).ComputeRedefinedSupertypesOperation(false), Throws.TypeOf<ArgumentNullException>());

            var feature = new Feature();

            // empty case: no specializations, no chaining → empty supertypes list.
            Assert.That(feature.ComputeRedefinedSupertypesOperation(false), Has.Count.EqualTo(0));

            // populated: a Specialization adds its General to supertypes; an implied Specialization
            // is filtered when excludeImplied = true.
            var general = new Type();
            var specialization = new Specialization { Specific = feature, General = general };
            feature.AssignOwnership(specialization, general);

            var impliedGeneral = new Type();
            var impliedSpecialization = new Specialization { Specific = feature, General = impliedGeneral, IsImplied = true };
            feature.AssignOwnership(impliedSpecialization, impliedGeneral);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(feature.ComputeRedefinedSupertypesOperation(false), Is.EquivalentTo(new[] { general, impliedGeneral }));
                Assert.That(feature.ComputeRedefinedSupertypesOperation(true), Is.EquivalentTo(new[] { general }));
            }
        }

        [Test]
        public void VerifyComputeTypingFeaturesOperation()
        {
            Assert.That(() => ((IFeature)null).ComputeTypingFeaturesOperation(), Throws.TypeOf<ArgumentNullException>());

            var feature = new Feature();

            // empty case: not conjugated, no Subsetting, no chainingFeature → empty list.
            Assert.That(feature.ComputeTypingFeaturesOperation(), Has.Count.EqualTo(0));

            // populated (non-conjugated): a Subsetting contributes its SubsettedFeature; CrossSubsetting
            // is excluded.
            var subsettedFeature = new Feature();
            var subsetting = new Subsetting { SubsettedFeature = subsettedFeature };
            feature.AssignOwnership(subsetting, new Feature());

            var crossSubsettedFeature = new Feature();
            var crossSubsetting = new CrossSubsetting { CrossedFeature = crossSubsettedFeature };
            feature.AssignOwnership(crossSubsetting, new Feature());

            Assert.That(feature.ComputeTypingFeaturesOperation(), Is.EquivalentTo(new[] { subsettedFeature }));
        }

        [Test]
        public void VerifyComputeSubsetsChainOperation()
        {
            Assert.That(() => ((IFeature)null).ComputeSubsetsChainOperation(null, null), Throws.TypeOf<ArgumentNullException>());

            var feature = new Feature();
            var first = new Feature();
            var second = new Feature();

            // empty case: bare feature with no specializations and no chainingFeature → false.
            Assert.That(feature.ComputeSubsetsChainOperation(first, second), Is.False);

            // populated: a feature with chaining [first, second] satisfies the chain match.
            var matching = new Feature();
            var chaining1 = new FeatureChaining { ChainingFeature = first };
            matching.AssignOwnership(chaining1, new Feature());

            var chaining2 = new FeatureChaining { ChainingFeature = second };
            matching.AssignOwnership(chaining2, new Feature());

            // The subject specializes the matching feature so it shows up in the BFS visited set.
            var specialization = new Specialization { Specific = feature, General = matching };
            feature.AssignOwnership(specialization, matching);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(feature.ComputeSubsetsChainOperation(first, second), Is.True);
                Assert.That(feature.ComputeSubsetsChainOperation(second, first), Is.False);
            }
        }

        [Test]
        public void VerifyComputeRedefinedIsCompatibleWithOperation()
        {
            Assert.That(() => ((IFeature)null).ComputeRedefinedIsCompatibleWithOperation(null), Throws.TypeOf<ArgumentNullException>());

            var feature = new Feature();

            // empty case: bare feature has no Specialization to an unrelated Type and the otherType is not
            // a Feature → returns false.
            Assert.That(feature.ComputeRedefinedIsCompatibleWithOperation(new Type()), Is.False);

            // populated: a feature that specializes a Type is compatible with that Type.
            var supertype = new Type();
            var specialization = new Specialization { Specific = feature, General = supertype };
            feature.AssignOwnership(specialization, supertype);

            Assert.That(feature.ComputeRedefinedIsCompatibleWithOperation(supertype), Is.True);
        }

        [Test]
        public void VerifyComputeRedefinesFromLibraryOperation()
        {
            Assert.That(() => ((IFeature)null).ComputeRedefinesFromLibraryOperation(null), Throws.TypeOf<ArgumentNullException>());

            var bareFeature = new Feature();

            using (Assert.EnterMultipleScope())
            {
                // null/blank library names short-circuit through ResolveGlobal to false.
                Assert.That(bareFeature.ComputeRedefinesFromLibraryOperation(null), Is.False);
                Assert.That(bareFeature.ComputeRedefinesFromLibraryOperation("  "), Is.False);

                // a name that does not resolve in this fixture (no library wired) returns false.
                Assert.That(bareFeature.ComputeRedefinesFromLibraryOperation("Some::Library::Feature"), Is.False);
            }

            // positive case: build a small library namespace, place the subject feature in it,
            // and have the subject redefine the library feature.
            var rootNamespace = new Namespace();

            var libraryFeature = new Feature { DeclaredName = "LibFeature" };
            var libraryMembership = new OwningMembership { Visibility = VisibilityKind.Public };
            rootNamespace.AssignOwnership(libraryMembership, libraryFeature);

            var subject = new Feature();
            var subjectMembership = new OwningMembership { Visibility = VisibilityKind.Public };
            rootNamespace.AssignOwnership(subjectMembership, subject);

            var redefinition = new Redefinition { RedefinedFeature = libraryFeature };
            subject.AssignOwnership(redefinition, new Feature());

            using (Assert.EnterMultipleScope())
            {
                Assert.That(subject.ComputeRedefinesFromLibraryOperation("LibFeature"), Is.True);

                // resolves to a Feature the subject does NOT redefine → false.
                var unrelatedFeature = new Feature { DeclaredName = "UnrelatedFeature" };
                var unrelatedMembership = new OwningMembership { Visibility = VisibilityKind.Public };
                rootNamespace.AssignOwnership(unrelatedMembership, unrelatedFeature);

                Assert.That(subject.ComputeRedefinesFromLibraryOperation("UnrelatedFeature"), Is.False);

                // resolves to a non-Feature element → false.
                var libraryType = new Type { DeclaredName = "LibType" };
                var libraryTypeMembership = new OwningMembership { Visibility = VisibilityKind.Public };
                rootNamespace.AssignOwnership(libraryTypeMembership, libraryType);

                Assert.That(subject.ComputeRedefinesFromLibraryOperation("LibType"), Is.False);
            }
        }
    }
}
