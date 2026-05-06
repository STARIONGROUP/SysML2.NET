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

    using SysML2.NET.Core.POCO.Core.Features;
    using SysML2.NET.Core.POCO.Core.Types;
    using SysML2.NET.Core.POCO.Root.Namespaces;
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
            feature.AssignOwnership(subsetting, new Feature());

            Assert.That(feature.ComputeOwnedFeatureChaining(), Has.Count.EqualTo(0));

            var chainingTarget1 = new Feature();
            var chaining1 = new FeatureChaining { ChainingFeature = chainingTarget1 };
            feature.AssignOwnership(chaining1, new Feature());

            var chainingTarget2 = new Feature();
            var chaining2 = new FeatureChaining { ChainingFeature = chainingTarget2 };
            feature.AssignOwnership(chaining2, new Feature());

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
            feature.AssignOwnership(invertingPointingElsewhere, new Feature());

            Assert.That(feature.ComputeOwnedFeatureInverting(), Has.Count.EqualTo(0));

            var invertingPointingSelf = new FeatureInverting { FeatureInverted = feature };
            feature.AssignOwnership(invertingPointingSelf, new Feature());

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
            feature.AssignOwnership(typeFeaturingPointingElsewhere, new Feature());

            Assert.That(feature.ComputeOwnedTypeFeaturing(), Has.Count.EqualTo(0));

            var featuringType = new Type();
            var typeFeaturingPointingSelf = new TypeFeaturing { FeatureOfType = feature, FeaturingType = featuringType };
            feature.AssignOwnership(typeFeaturingPointingSelf, new Feature());

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
            feature.AssignOwnership(specialization, new Feature());

            Assert.That(feature.ComputeOwnedSubsetting(), Has.Count.EqualTo(0));

            var subsetting = new Subsetting();
            feature.AssignOwnership(subsetting, new Feature());

            var redefinition = new Redefinition();
            feature.AssignOwnership(redefinition, new Feature());

            var referenceSubsetting = new ReferenceSubsetting();
            feature.AssignOwnership(referenceSubsetting, new Feature());

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
            feature.AssignOwnership(subsetting, new Feature());

            Assert.That(feature.ComputeOwnedRedefinition(), Has.Count.EqualTo(0));

            var redefinition1 = new Redefinition();
            feature.AssignOwnership(redefinition1, new Feature());

            var redefinition2 = new Redefinition();
            feature.AssignOwnership(redefinition2, new Feature());

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
            feature.AssignOwnership(referenceSubsetting, new Feature());

            Assert.That(feature.ComputeOwnedReferenceSubsetting(), Is.SameAs(referenceSubsetting));
        }

        [Test]
        public void VerifyComputeOwnedCrossSubsetting()
        {
            Assert.That(() => ((IFeature)null).ComputeOwnedCrossSubsetting(), Throws.TypeOf<ArgumentNullException>());

            var feature = new Feature();

            Assert.That(feature.ComputeOwnedCrossSubsetting(), Is.Null);

            var crossSubsetting = new CrossSubsetting();
            feature.AssignOwnership(crossSubsetting, new Feature());

            Assert.That(feature.ComputeOwnedCrossSubsetting(), Is.SameAs(crossSubsetting));
        }

        [Test]
        public void VerifyComputeOwnedTyping()
        {
            Assert.That(() => ((IFeature)null).ComputeOwnedTyping(), Throws.TypeOf<ArgumentNullException>());

            var feature = new Feature();

            Assert.That(feature.ComputeOwnedTyping(), Has.Count.EqualTo(0));

            var specialization = new Specialization();
            feature.AssignOwnership(specialization, new Feature());

            Assert.That(feature.ComputeOwnedTyping(), Has.Count.EqualTo(0));

            var type1 = new Type();
            var typing1 = new FeatureTyping { Type = type1 };
            feature.AssignOwnership(typing1, new Feature());

            var type2 = new Type();
            var typing2 = new FeatureTyping { Type = type2 };
            feature.AssignOwnership(typing2, new Feature());

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
            feature.AssignOwnership(chaining1, new Feature());

            var target2 = new Feature();
            var chaining2 = new FeatureChaining { ChainingFeature = target2 };
            feature.AssignOwnership(chaining2, new Feature());

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
            feature.AssignOwnership(chaining1, new Feature());

            var lastTarget = new Feature();
            var chaining2 = new FeatureChaining { ChainingFeature = lastTarget };
            feature.AssignOwnership(chaining2, new Feature());

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
            feature.AssignOwnership(crossSubsetting, new Feature());

            Assert.That(feature.ComputeCrossFeature(), Is.Null);

            var chainingTarget1 = new Feature();
            var chaining1 = new FeatureChaining { ChainingFeature = chainingTarget1 };
            crossedFeature.AssignOwnership(chaining1, new Feature());

            Assert.That(feature.ComputeCrossFeature(), Is.Null);

            var chainingTarget2 = new Feature();
            var chaining2 = new FeatureChaining { ChainingFeature = chainingTarget2 };
            crossedFeature.AssignOwnership(chaining2, new Feature());

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
            feature.AssignOwnership(typeFeaturing, new Feature());

            var result = feature.ComputeFeaturingType();

            using (Assert.EnterMultipleScope())
            {
                Assert.That(result, Has.Count.EqualTo(1));
                Assert.That(result[0], Is.SameAs(theType));
            }

            var chainingTarget = new Feature();
            var chainingTargetType = new Type();
            var chainingTypeFeaturing = new TypeFeaturing { FeatureOfType = chainingTarget, FeaturingType = chainingTargetType };
            chainingTarget.AssignOwnership(chainingTypeFeaturing, new Feature());

            var chaining = new FeatureChaining { ChainingFeature = chainingTarget };
            feature.AssignOwnership(chaining, new Feature());

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
            feature.AssignOwnership(typing1, new Feature());

            var type2 = new Type();
            var typing2 = new FeatureTyping { Type = type2 };
            feature.AssignOwnership(typing2, new Feature());

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
            feature.AssignOwnership(redefinition, new Feature());

            Assert.That(feature.ComputeNamingFeatureOperation(), Is.SameAs(redefinedFeature));

            var redefinedFeature2 = new Feature { DeclaredName = "redefined2" };
            var redefinition2 = new Redefinition { RedefinedFeature = redefinedFeature2 };
            feature.AssignOwnership(redefinition2, new Feature());

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
            featureWithRedefinition.AssignOwnership(redefinition, new Feature());

            Assert.That(featureWithRedefinition.ComputeRedefinedEffectiveShortNameOperation(), Is.EqualTo("nfShort"));

            var featureNoNaming = new Feature();
            var redefinitionNoTarget = new Redefinition { RedefinedFeature = new Feature() };
            featureNoNaming.AssignOwnership(redefinitionNoTarget, new Feature());

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
            featureWithRedefinition.AssignOwnership(redefinition, new Feature());

            Assert.That(featureWithRedefinition.ComputeRedefinedEffectiveNameOperation(), Is.EqualTo("nfName"));

            var featureNoNaming = new Feature();
            var redefinitionNoTarget = new Redefinition { RedefinedFeature = new Feature() };
            featureNoNaming.AssignOwnership(redefinitionNoTarget, new Feature());

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
            feature.AssignOwnership(redefinition, new Feature());

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

            Assert.That(() => feature.ComputeDirectionForOperation(new Type()), Throws.TypeOf<NotSupportedException>());
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
            feature.AssignOwnership(typing, new Feature());

            Assert.That(feature.ComputeIsCartesianProductOperation(), Is.False);

            var featuringType = new Type();
            var typeFeaturing = new TypeFeaturing { FeatureOfType = feature, FeaturingType = featuringType };
            feature.AssignOwnership(typeFeaturing, new Feature());

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
            feature.AssignOwnership(typing, new Feature());

            result = feature.ComputeAsCartesianProductOperation();

            Assert.That(result, Has.Count.EqualTo(1));
            Assert.That(result[0], Is.SameAs(theType));

            var featuringType = new Type();
            var typeFeaturing = new TypeFeaturing { FeatureOfType = feature, FeaturingType = featuringType };
            feature.AssignOwnership(typeFeaturing, new Feature());

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

            // all body cases blocked by TypeExtensions.ComputeRedefinedVisibleMembershipsOperation
            // (reached via ResolveGlobal path even for type=null and empty featuringTypes,
            // because the impl resolves Base::Anything to check implicit featuring).
            var feature = new Feature();

            Assert.That(() => feature.ComputeIsFeaturedWithinOperation(null), Throws.TypeOf<NotSupportedException>());
        }

        [Test]
        public void VerifyComputeCanAccessOperation()
        {
            Assert.That(() => ((IFeature)null).ComputeCanAccessOperation(null), Throws.TypeOf<ArgumentNullException>());

            // all body cases blocked by TypeExtensions.ComputeRedefinedVisibleMembershipsOperation
            // (reached via IsFeaturedWithin → ResolveGlobal path even for empty featuringTypes).
            var feature = new Feature();

            Assert.That(() => feature.ComputeCanAccessOperation(new Feature()), Throws.TypeOf<NotSupportedException>());
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
            feature.AssignOwnership(redefinitionAB, new Feature());

            result = feature.ComputeAllRedefinedFeaturesOperation();

            using (Assert.EnterMultipleScope())
            {
                Assert.That(result, Has.Count.EqualTo(2));
                Assert.That(result[0], Is.SameAs(feature));
                Assert.That(result[1], Is.SameAs(redefinedB));
            }

            var redefinedC = new Feature();
            var redefinitionBC = new Redefinition { RedefinedFeature = redefinedC };
            redefinedB.AssignOwnership(redefinitionBC, new Feature());

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

            Assert.That(() => feature.ComputeRedefinedSupertypesOperation(false), Throws.TypeOf<NotSupportedException>());
        }

        [Test]
        public void VerifyComputeTypingFeaturesOperation()
        {
            Assert.That(() => ((IFeature)null).ComputeTypingFeaturesOperation(), Throws.TypeOf<ArgumentNullException>());

            // body cases (non-conjugated subsetting filtering, CrossSubsetting exclusion,
            // chainingFeature append) are blocked by TypeExtensions.ComputeIsConjugated stub
            // which is called via featureSubject.isConjugated at the start of the implementation
            var feature = new Feature();

            Assert.That(() => feature.ComputeTypingFeaturesOperation(), Throws.TypeOf<NotSupportedException>());
        }

        [Test]
        public void VerifyComputeSubsetsChainOperation()
        {
            Assert.That(() => ((IFeature)null).ComputeSubsetsChainOperation(null, null), Throws.TypeOf<ArgumentNullException>());

            var feature = new Feature();

            Assert.That(() => feature.ComputeSubsetsChainOperation(new Feature(), new Feature()), Throws.TypeOf<NotSupportedException>());
        }

        [Test]
        public void VerifyComputeRedefinedIsCompatibleWithOperation()
        {
            Assert.That(() => ((IFeature)null).ComputeRedefinedIsCompatibleWithOperation(null), Throws.TypeOf<ArgumentNullException>());

            var feature = new Feature();

            Assert.That(() => feature.ComputeRedefinedIsCompatibleWithOperation(new Type()), Throws.TypeOf<NotSupportedException>());
        }

        [Test]
        public void VerifyComputeRedefinesFromLibraryOperation()
        {
            Assert.That(() => ((IFeature)null).ComputeRedefinesFromLibraryOperation(null), Throws.TypeOf<ArgumentNullException>());

            // positive verification of library redefinition requires a loaded library model
            // and is blocked by TypeExtensions.ComputeRedefinedVisibleMembershipsOperation stub
            // (via ResolveGlobal → VisibleMemberships chain)
            var feature = new Feature();

            Assert.That(() => feature.ComputeRedefinesFromLibraryOperation("Some::Library::Feature"), Throws.TypeOf<NotSupportedException>());
        }
    }
}
