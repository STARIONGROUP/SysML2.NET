// -------------------------------------------------------------------------------------------------
// <copyright file="TypeExtensionsTestFixture.cs" company="Starion Group S.A.">
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
    using System.Collections.Generic;

    using NUnit.Framework;

    using SysML2.NET.Core.Core.Types;
    using SysML2.NET.Core.POCO.Core.Features;
    using SysML2.NET.Core.POCO.Core.Types;
    using SysML2.NET.Core.POCO.Root.Namespaces;
    using SysML2.NET.Core.Root.Namespaces;
    using SysML2.NET.Extensions;

    using Type = SysML2.NET.Core.POCO.Core.Types.Type;

    [TestFixture]
    public class TypeExtensionsTestFixture
    {
        [Test]
        public void VerifyComputeOwnedDifferencing()
        {
            Assert.That(() => ((IType)null).ComputeOwnedDifferencing(), Throws.TypeOf<ArgumentNullException>());

            var subject = new Type();

            Assert.That(subject.ComputeOwnedDifferencing(), Has.Count.EqualTo(0));

            var differencingType = new Type();
            var differencing = new Differencing { DifferencingType = differencingType };
            subject.AssignOwnership(differencing);

            var unrelatedSpecialization = new Specialization();
            subject.AssignOwnership(unrelatedSpecialization);

            Assert.That(subject.ComputeOwnedDifferencing(), Is.EquivalentTo([differencing]));
        }

        [Test]
        public void VerifyComputeOwnedDisjoining()
        {
            Assert.That(() => ((IType)null).ComputeOwnedDisjoining(), Throws.TypeOf<ArgumentNullException>());

            var subject = new Type();

            Assert.That(subject.ComputeOwnedDisjoining(), Has.Count.EqualTo(0));

            var disjoining = new Disjoining();
            subject.AssignOwnership(disjoining);

            var unrelatedSpecialization = new Specialization();
            subject.AssignOwnership(unrelatedSpecialization);

            Assert.That(subject.ComputeOwnedDisjoining(), Is.EquivalentTo([disjoining]));
        }

        [Test]
        public void VerifyComputeOwnedFeatureMembership()
        {
            Assert.That(() => ((IType)null).ComputeOwnedFeatureMembership(), Throws.TypeOf<ArgumentNullException>());

            var subject = new Type();

            Assert.That(subject.ComputeOwnedFeatureMembership(), Has.Count.EqualTo(0));

            var feature = new Feature();
            var featureMembership = new FeatureMembership();
            subject.AssignOwnership(featureMembership, feature);

            var owningMembership = new OwningMembership();
            subject.AssignOwnership(owningMembership, new Feature());

            Assert.That(subject.ComputeOwnedFeatureMembership(), Is.EquivalentTo([featureMembership]));
        }

        [Test]
        public void VerifyComputeOwnedIntersecting()
        {
            Assert.That(() => ((IType)null).ComputeOwnedIntersecting(), Throws.TypeOf<ArgumentNullException>());

            var subject = new Type();

            Assert.That(subject.ComputeOwnedIntersecting(), Has.Count.EqualTo(0));

            var intersectingType = new Type();
            var intersecting = new Intersecting { IntersectingType = intersectingType };
            subject.AssignOwnership(intersecting);

            var unrelatedSpecialization = new Specialization();
            subject.AssignOwnership(unrelatedSpecialization);

            Assert.That(subject.ComputeOwnedIntersecting(), Is.EquivalentTo([intersecting]));
        }

        [Test]
        public void VerifyComputeOwnedUnioning()
        {
            Assert.That(() => ((IType)null).ComputeOwnedUnioning(), Throws.TypeOf<ArgumentNullException>());

            var subject = new Type();

            Assert.That(subject.ComputeOwnedUnioning(), Has.Count.EqualTo(0));

            var unioningType = new Type();
            var unioning = new Unioning { UnioningType = unioningType };
            subject.AssignOwnership(unioning);

            var unrelatedSpecialization = new Specialization();
            subject.AssignOwnership(unrelatedSpecialization);

            Assert.That(subject.ComputeOwnedUnioning(), Is.EquivalentTo([unioning]));
        }

        [Test]
        public void VerifyComputeOwnedSpecialization()
        {
            Assert.That(() => ((IType)null).ComputeOwnedSpecialization(), Throws.TypeOf<ArgumentNullException>());

            var subject = new Type();

            Assert.That(subject.ComputeOwnedSpecialization(), Has.Count.EqualTo(0));

            var general = new Type();
            var specialization = new Specialization { Specific = subject, General = general };
            subject.AssignOwnership(specialization);

            // a Specialization owned by subject but whose Specific points elsewhere must be filtered out
            var elsewhere = new Type();
            var foreignSpecialization = new Specialization { Specific = elsewhere, General = new Type() };
            subject.AssignOwnership(foreignSpecialization);

            Assert.That(subject.ComputeOwnedSpecialization(), Is.EquivalentTo([specialization]));
        }

        [Test]
        public void VerifyComputeOwnedConjugator()
        {
            Assert.That(() => ((IType)null).ComputeOwnedConjugator(), Throws.TypeOf<ArgumentNullException>());

            var subject = new Type();

            Assert.That(subject.ComputeOwnedConjugator(), Is.Null);

            var originalType = new Type();
            var conjugation = new Conjugation { ConjugatedType = subject, OriginalType = originalType };
            subject.AssignOwnership(conjugation);

            Assert.That(subject.ComputeOwnedConjugator(), Is.SameAs(conjugation));
        }

        [Test]
        public void VerifyComputeMultiplicity()
        {
            Assert.That(() => ((IType)null).ComputeMultiplicity(), Throws.TypeOf<ArgumentNullException>());

            var subject = new Type();

            Assert.That(subject.ComputeMultiplicity(), Is.Null);

            var multiplicity = new Multiplicity();
            var owningMembership = new OwningMembership();
            subject.AssignOwnership(owningMembership, multiplicity);

            Assert.That(subject.ComputeMultiplicity(), Is.SameAs(multiplicity));
        }

        [Test]
        public void VerifyComputeIsConjugated()
        {
            Assert.That(() => ((IType)null).ComputeIsConjugated(), Throws.TypeOf<ArgumentNullException>());

            var subject = new Type();

            Assert.That(subject.ComputeIsConjugated(), Is.False);

            var originalType = new Type();
            var conjugation = new Conjugation { ConjugatedType = subject, OriginalType = originalType };
            subject.AssignOwnership(conjugation);

            Assert.That(subject.ComputeIsConjugated(), Is.True);
        }

        [Test]
        public void VerifyComputeDifferencingType()
        {
            Assert.That(() => ((IType)null).ComputeDifferencingType(), Throws.TypeOf<ArgumentNullException>());

            var subject = new Type();

            Assert.That(subject.ComputeDifferencingType(), Has.Count.EqualTo(0));

            var differencingType = new Type();
            var differencing = new Differencing { DifferencingType = differencingType };
            subject.AssignOwnership(differencing);

            var nullDifferencing = new Differencing();
            subject.AssignOwnership(nullDifferencing);

            Assert.That(subject.ComputeDifferencingType(), Is.EquivalentTo([differencingType]));
        }

        [Test]
        public void VerifyComputeIntersectingType()
        {
            Assert.That(() => ((IType)null).ComputeIntersectingType(), Throws.TypeOf<ArgumentNullException>());

            var subject = new Type();

            Assert.That(subject.ComputeIntersectingType(), Has.Count.EqualTo(0));

            var intersectingType = new Type();
            var intersecting = new Intersecting { IntersectingType = intersectingType };
            subject.AssignOwnership(intersecting);

            var nullIntersecting = new Intersecting();
            subject.AssignOwnership(nullIntersecting);

            Assert.That(subject.ComputeIntersectingType(), Is.EquivalentTo([intersectingType]));
        }

        [Test]
        public void VerifyComputeUnioningType()
        {
            Assert.That(() => ((IType)null).ComputeUnioningType(), Throws.TypeOf<ArgumentNullException>());

            var subject = new Type();

            Assert.That(subject.ComputeUnioningType(), Has.Count.EqualTo(0));

            var unioningType = new Type();
            var unioning = new Unioning { UnioningType = unioningType };
            subject.AssignOwnership(unioning);

            var nullUnioning = new Unioning();
            subject.AssignOwnership(nullUnioning);

            Assert.That(subject.ComputeUnioningType(), Is.EquivalentTo([unioningType]));
        }

        [Test]
        public void VerifyComputeOwnedFeature()
        {
            Assert.That(() => ((IType)null).ComputeOwnedFeature(), Throws.TypeOf<ArgumentNullException>());

            var subject = new Type();

            Assert.That(subject.ComputeOwnedFeature(), Has.Count.EqualTo(0));

            var feature = new Feature();
            var featureMembership = new FeatureMembership();
            subject.AssignOwnership(featureMembership, feature);

            // An OwningMembership (non-FeatureMembership) sibling must be excluded.
            var nonFeature = new Feature();
            var owningMembership = new OwningMembership();
            subject.AssignOwnership(owningMembership, nonFeature);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(subject.ComputeOwnedFeature(), Has.Count.EqualTo(1));
                Assert.That(subject.ComputeOwnedFeature(), Does.Contain(feature));
                Assert.That(subject.ComputeOwnedFeature(), Does.Not.Contain(nonFeature));
            }
        }

        [Test]
        public void VerifyComputeFeature()
        {
            Assert.That(() => ((IType)null).ComputeFeature(), Throws.TypeOf<ArgumentNullException>());

            var subject = new Type();

            Assert.That(subject.ComputeFeature(), Has.Count.EqualTo(0));

            var feature = new Feature();
            var featureMembership = new FeatureMembership();
            subject.AssignOwnership(featureMembership, feature);

            Assert.That(subject.ComputeFeature(), Is.EquivalentTo([feature]));

            // Inherited FeatureMembership via Specialization must be INCLUDED in feature.
            var supertype = new Type();
            var specialization = new Specialization { Specific = subject, General = supertype };
            subject.AssignOwnership(specialization);

            var inheritedFeature = new Feature();
            var inheritedFeatureMembership = new FeatureMembership { Visibility = VisibilityKind.Public };
            supertype.AssignOwnership(inheritedFeatureMembership, inheritedFeature);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(subject.ComputeFeature(), Does.Contain(feature));
                Assert.That(subject.ComputeFeature(), Does.Contain(inheritedFeature));
            }
        }

        [Test]
        public void VerifyComputeOwnedEndFeature()
        {
            Assert.That(() => ((IType)null).ComputeOwnedEndFeature(), Throws.TypeOf<ArgumentNullException>());

            var subject = new Type();

            Assert.That(subject.ComputeOwnedEndFeature(), Has.Count.EqualTo(0));

            var endFeature = new Feature { IsEnd = true };
            var endMembership = new FeatureMembership();
            subject.AssignOwnership(endMembership, endFeature);

            // A non-end (IsEnd = false) feature must be EXCLUDED.
            var nonEndFeature = new Feature { IsEnd = false };
            var nonEndMembership = new FeatureMembership();
            subject.AssignOwnership(nonEndMembership, nonEndFeature);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(subject.ComputeOwnedEndFeature(), Has.Count.EqualTo(1));
                Assert.That(subject.ComputeOwnedEndFeature(), Does.Contain(endFeature));
                Assert.That(subject.ComputeOwnedEndFeature(), Does.Not.Contain(nonEndFeature));
            }
        }

        [Test]
        public void VerifyComputeEndFeature()
        {
            Assert.That(() => ((IType)null).ComputeEndFeature(), Throws.TypeOf<ArgumentNullException>());

            var subject = new Type();

            Assert.That(subject.ComputeEndFeature(), Has.Count.EqualTo(0));

            var endFeature = new Feature { IsEnd = true };
            var endMembership = new FeatureMembership();
            subject.AssignOwnership(endMembership, endFeature);

            // A non-end (IsEnd = false) feature must be EXCLUDED.
            var nonEndFeature = new Feature { IsEnd = false };
            var nonEndMembership = new FeatureMembership();
            subject.AssignOwnership(nonEndMembership, nonEndFeature);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(subject.ComputeEndFeature(), Has.Count.EqualTo(1));
                Assert.That(subject.ComputeEndFeature(), Does.Contain(endFeature));
                Assert.That(subject.ComputeEndFeature(), Does.Not.Contain(nonEndFeature));
            }

            // An inherited end feature (via Specialization) must also be INCLUDED.
            var supertype = new Type();
            var specialization = new Specialization { Specific = subject, General = supertype };
            subject.AssignOwnership(specialization);

            var inheritedEndFeature = new Feature { IsEnd = true };
            var inheritedEndMembership = new FeatureMembership { Visibility = VisibilityKind.Public };
            supertype.AssignOwnership(inheritedEndMembership, inheritedEndFeature);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(subject.ComputeEndFeature(), Does.Contain(endFeature));
                Assert.That(subject.ComputeEndFeature(), Does.Contain(inheritedEndFeature));
                Assert.That(subject.ComputeEndFeature(), Does.Not.Contain(nonEndFeature));
            }
        }

        [Test]
        public void VerifyComputeSupertypesOperation()
        {
            Assert.That(() => ((IType)null).ComputeSupertypesOperation(false), Throws.TypeOf<ArgumentNullException>());

            var subject = new Type();

            Assert.That(subject.ComputeSupertypesOperation(false), Has.Count.EqualTo(0));

            var general = new Type();
            var specialization = new Specialization { Specific = subject, General = general };
            subject.AssignOwnership(specialization);

            var impliedGeneral = new Type();
            var impliedSpecialization = new Specialization { Specific = subject, General = impliedGeneral, IsImplied = true };
            subject.AssignOwnership(impliedSpecialization);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(subject.ComputeSupertypesOperation(false), Is.EquivalentTo([general, impliedGeneral]));
                Assert.That(subject.ComputeSupertypesOperation(true), Is.EquivalentTo([general]));
            }

            // conjugated branch returns the OriginalType of the conjugator.
            var conjugatedSubject = new Type();
            var originalType = new Type();
            var conjugation = new Conjugation { ConjugatedType = conjugatedSubject, OriginalType = originalType };
            conjugatedSubject.AssignOwnership(conjugation);

            Assert.That(conjugatedSubject.ComputeSupertypesOperation(false), Is.EquivalentTo([originalType]));
        }

        [Test]
        public void VerifyComputeAllSupertypesOperation()
        {
            Assert.That(() => ((IType)null).ComputeAllSupertypesOperation(), Throws.TypeOf<ArgumentNullException>());

            var subject = new Type();

            using (Assert.EnterMultipleScope())
            {
                // OCL: OrderedSet{self}->closure(supertypes(false)) — result includes self.
                Assert.That(subject.ComputeAllSupertypesOperation(), Has.Count.EqualTo(1));
                Assert.That(subject.ComputeAllSupertypesOperation(), Does.Contain(subject));
            }

            var middle = new Type();
            var top = new Type();

            var subjectToMiddle = new Specialization { Specific = subject, General = middle };
            subject.AssignOwnership(subjectToMiddle);

            var middleToTop = new Specialization { Specific = middle, General = top };
            middle.AssignOwnership(middleToTop);

            // 3-level chain plus a self-loop to demonstrate cycle safety.
            var cycleSpecialization = new Specialization { Specific = top, General = subject };
            top.AssignOwnership(cycleSpecialization);

            var result = subject.ComputeAllSupertypesOperation();

            using (Assert.EnterMultipleScope())
            {
                Assert.That(result, Is.EquivalentTo([subject, middle, top]));
                Assert.That(result, Has.Count.EqualTo(3));
            }
        }

        [Test]
        public void VerifyComputeSpecializesOperation()
        {
            Assert.That(() => ((IType)null).ComputeSpecializesOperation(new Type()), Throws.TypeOf<ArgumentNullException>());

            var subject = new Type();

            Assert.That(() => subject.ComputeSpecializesOperation(null), Throws.TypeOf<ArgumentNullException>());

            var unrelated = new Type();

            Assert.That(subject.ComputeSpecializesOperation(unrelated), Is.False);

            var supertype = new Type();
            var specialization = new Specialization { Specific = subject, General = supertype };
            subject.AssignOwnership(specialization);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(subject.ComputeSpecializesOperation(supertype), Is.True);
                Assert.That(subject.ComputeSpecializesOperation(unrelated), Is.False);
            }

            // conjugated branch: subject specializes whatever its OriginalType specializes.
            var conjugatedSubject = new Type();
            var originalType = new Type();
            var conjugation = new Conjugation { ConjugatedType = conjugatedSubject, OriginalType = originalType };
            conjugatedSubject.AssignOwnership(conjugation);

            var originalSupertype = new Type();
            var originalSpecialization = new Specialization { Specific = originalType, General = originalSupertype };
            originalType.AssignOwnership(originalSpecialization);

            Assert.That(conjugatedSubject.ComputeSpecializesOperation(originalSupertype), Is.True);
        }

        [Test]
        public void VerifyComputeIsCompatibleWithOperation()
        {
            Assert.That(() => ((IType)null).ComputeIsCompatibleWithOperation(new Type()), Throws.TypeOf<ArgumentNullException>());

            var subject = new Type();

            Assert.That(() => subject.ComputeIsCompatibleWithOperation(null), Throws.TypeOf<ArgumentNullException>());

            var unrelated = new Type();

            Assert.That(subject.ComputeIsCompatibleWithOperation(unrelated), Is.False);

            var supertype = new Type();
            var specialization = new Specialization { Specific = subject, General = supertype };
            subject.AssignOwnership(specialization);

            Assert.That(subject.ComputeIsCompatibleWithOperation(supertype), Is.True);
        }

        [Test]
        public void VerifyComputeDirectionOfOperation()
        {
            Assert.That(() => ((IType)null).ComputeDirectionOfOperation(new Feature()), Throws.TypeOf<ArgumentNullException>());

            var subject = new Type();

            Assert.That(() => subject.ComputeDirectionOfOperation(null), Throws.TypeOf<ArgumentNullException>());

            // a feature whose owningType is unrelated and unreachable returns null direction.
            var unrelatedFeature = new Feature { Direction = FeatureDirectionKind.In };

            Assert.That(subject.ComputeDirectionOfOperation(unrelatedFeature), Is.Null);

            // populated branch: feature owned via FeatureMembership has owningType == subject and direction is propagated.
            var ownedFeature = new Feature { Direction = FeatureDirectionKind.In };
            var featureMembership = new FeatureMembership();
            subject.AssignOwnership(featureMembership, ownedFeature);

            Assert.That(subject.ComputeDirectionOfOperation(ownedFeature), Is.EqualTo(FeatureDirectionKind.In));
        }

        [Test]
        public void VerifyComputeDirectionOfExcludingOperation()
        {
            Assert.That(() => ((IType)null).ComputeDirectionOfExcludingOperation(new Feature(), []), Throws.TypeOf<ArgumentNullException>());

            var subject = new Type();

            Assert.That(() => subject.ComputeDirectionOfExcludingOperation(null, []), Throws.TypeOf<ArgumentNullException>());

            // unrelated feature with unrelated owningType: result is null.
            var unrelatedFeature = new Feature { Direction = FeatureDirectionKind.In };

            Assert.That(subject.ComputeDirectionOfExcludingOperation(unrelatedFeature, null), Is.Null);

            // direct branch: feature.owningType == subject → returns feature.Direction.
            var ownedFeature = new Feature { Direction = FeatureDirectionKind.In };
            var featureMembership = new FeatureMembership();
            subject.AssignOwnership(featureMembership, ownedFeature);

            Assert.That(subject.ComputeDirectionOfExcludingOperation(ownedFeature, []), Is.EqualTo(FeatureDirectionKind.In));

            // conjugation flip: a conjugated subject inherits the feature direction from its OriginalType
            // and flips In↔Out at the current level.
            var conjugatedSubject = new Type();
            var originalType = new Type();
            var conjugation = new Conjugation { ConjugatedType = conjugatedSubject, OriginalType = originalType };
            conjugatedSubject.AssignOwnership(conjugation);

            var originalSpecialization = new Specialization { Specific = originalType, General = subject };
            originalType.AssignOwnership(originalSpecialization);

            // For conjugatedSubject:
            //   supertypes(false) = [originalType]
            //   originalType.directionOfExcluding(ownedFeature, ...) traverses to subject and yields In
            //   then conjugatedSubject is conjugated → flip In to Out.
            Assert.That(conjugatedSubject.ComputeDirectionOfExcludingOperation(ownedFeature, []), Is.EqualTo(FeatureDirectionKind.Out));
        }

        [Test]
        public void VerifyComputeDirectedFeature()
        {
            Assert.That(() => ((IType)null).ComputeDirectedFeature(), Throws.TypeOf<ArgumentNullException>());

            var subject = new Type();

            Assert.That(subject.ComputeDirectedFeature(), Has.Count.EqualTo(0));

            var directedFeature = new Feature { Direction = FeatureDirectionKind.In };
            var directedMembership = new FeatureMembership();
            subject.AssignOwnership(directedMembership, directedFeature);

            // An undirected feature (Direction = null) must be EXCLUDED.
            var undirectedFeature = new Feature { Direction = null };
            var undirectedMembership = new FeatureMembership();
            subject.AssignOwnership(undirectedMembership, undirectedFeature);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(subject.ComputeDirectedFeature(), Has.Count.EqualTo(1));
                Assert.That(subject.ComputeDirectedFeature(), Does.Contain(directedFeature));
                Assert.That(subject.ComputeDirectedFeature(), Does.Not.Contain(undirectedFeature));
            }
        }

        [Test]
        public void VerifyComputeInput()
        {
            Assert.That(() => ((IType)null).ComputeInput(), Throws.TypeOf<ArgumentNullException>());

            var subject = new Type();

            Assert.That(subject.ComputeInput(), Has.Count.EqualTo(0));

            var inFeature = new Feature { Direction = FeatureDirectionKind.In };
            var inMembership = new FeatureMembership();
            subject.AssignOwnership(inMembership, inFeature);

            // An Inout feature must also be INCLUDED (the `or` second branch).
            var inoutFeature = new Feature { Direction = FeatureDirectionKind.Inout };
            var inoutMembership = new FeatureMembership();
            subject.AssignOwnership(inoutMembership, inoutFeature);

            // An Out feature must be EXCLUDED.
            var outFeature = new Feature { Direction = FeatureDirectionKind.Out };
            var outMembership = new FeatureMembership();
            subject.AssignOwnership(outMembership, outFeature);

            // An undirected feature must be EXCLUDED.
            var undirectedFeature = new Feature { Direction = null };
            var undirectedMembership = new FeatureMembership();
            subject.AssignOwnership(undirectedMembership, undirectedFeature);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(subject.ComputeInput(), Has.Count.EqualTo(2));
                Assert.That(subject.ComputeInput(), Does.Contain(inFeature));
                Assert.That(subject.ComputeInput(), Does.Contain(inoutFeature));
                Assert.That(subject.ComputeInput(), Does.Not.Contain(outFeature));
                Assert.That(subject.ComputeInput(), Does.Not.Contain(undirectedFeature));
            }
        }

        [Test]
        public void VerifyComputeOutput()
        {
            Assert.That(() => ((IType)null).ComputeOutput(), Throws.TypeOf<ArgumentNullException>());

            var subject = new Type();

            Assert.That(subject.ComputeOutput(), Has.Count.EqualTo(0));

            var outFeature = new Feature { Direction = FeatureDirectionKind.Out };
            var outMembership = new FeatureMembership();
            subject.AssignOwnership(outMembership, outFeature);

            // An Inout feature must also be INCLUDED (the `or` second branch).
            var inoutFeature = new Feature { Direction = FeatureDirectionKind.Inout };
            var inoutMembership = new FeatureMembership();
            subject.AssignOwnership(inoutMembership, inoutFeature);

            // An In feature must be EXCLUDED.
            var inFeature = new Feature { Direction = FeatureDirectionKind.In };
            var inMembership = new FeatureMembership();
            subject.AssignOwnership(inMembership, inFeature);

            // An undirected feature must be EXCLUDED.
            var undirectedFeature = new Feature { Direction = null };
            var undirectedMembership = new FeatureMembership();
            subject.AssignOwnership(undirectedMembership, undirectedFeature);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(subject.ComputeOutput(), Has.Count.EqualTo(2));
                Assert.That(subject.ComputeOutput(), Does.Contain(outFeature));
                Assert.That(subject.ComputeOutput(), Does.Contain(inoutFeature));
                Assert.That(subject.ComputeOutput(), Does.Not.Contain(inFeature));
                Assert.That(subject.ComputeOutput(), Does.Not.Contain(undirectedFeature));
            }
        }

        [Test]
        public void VerifyComputeAllRedefinedFeaturesOfOperation()
        {
            Assert.That(() => ((IType)null).ComputeAllRedefinedFeaturesOfOperation(new OwningMembership()), Throws.TypeOf<ArgumentNullException>());

            var subject = new Type();

            Assert.That(() => subject.ComputeAllRedefinedFeaturesOfOperation(null), Throws.TypeOf<ArgumentNullException>());

            // membership whose memberElement is not a Feature returns an empty collection.
            var nonFeatureMembership = new Membership { MemberElement = new Type() };

            Assert.That(subject.ComputeAllRedefinedFeaturesOfOperation(nonFeatureMembership), Has.Count.EqualTo(0));

            // populated case: a Feature with a direct redefinition chain — result includes the Feature
            // itself plus the redefined target (per IFeature.AllRedefinedFeatures()).
            var redefinedTarget = new Feature();
            var redefiningFeature = new Feature();
            var redefinition = new Redefinition { RedefinedFeature = redefinedTarget };
            redefiningFeature.AssignOwnership(redefinition);

            var featureMembership = new Membership { MemberElement = redefiningFeature };

            var result = subject.ComputeAllRedefinedFeaturesOfOperation(featureMembership);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(result, Has.Count.EqualTo(2));
                Assert.That(result, Does.Contain(redefiningFeature));
                Assert.That(result, Does.Contain(redefinedTarget));
            }
        }

        [Test]
        public void VerifyComputeRemoveRedefinedFeaturesOperation()
        {
            Assert.That(() => ((IType)null).ComputeRemoveRedefinedFeaturesOperation([]), Throws.TypeOf<ArgumentNullException>());

            var subject = new Type();

            Assert.That(() => subject.ComputeRemoveRedefinedFeaturesOperation(null), Throws.TypeOf<ArgumentNullException>());

            // empty input → empty output.
            Assert.That(subject.ComputeRemoveRedefinedFeaturesOperation([]), Has.Count.EqualTo(0));

            // populated: two memberships where one carries a Feature that redefines the Feature in the other.
            // The redefining-feature membership should drop the redefined-feature membership.
            var baseFeature = new Feature();
            var derivedFeature = new Feature();
            var derivedRedefinesBase = new Redefinition { RedefinedFeature = baseFeature };
            derivedFeature.AssignOwnership(derivedRedefinesBase);

            var baseMembership = new Membership { MemberElement = baseFeature };
            var derivedMembership = new Membership { MemberElement = derivedFeature };

            var result = subject.ComputeRemoveRedefinedFeaturesOperation([baseMembership, derivedMembership]);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(result, Has.Count.EqualTo(1));
                Assert.That(result, Does.Contain(derivedMembership));
                Assert.That(result, Does.Not.Contain(baseMembership));
            }
        }

        [Test]
        public void VerifyComputeInheritableMembershipsOperation()
        {
            Assert.That(() => ((IType)null).ComputeInheritableMembershipsOperation(null, null, false), Throws.TypeOf<ArgumentNullException>());

            var subject = new Type();

            // empty case: no supertypes → no inheritable memberships.
            Assert.That(subject.ComputeInheritableMembershipsOperation(null, null, false), Has.Count.EqualTo(0));

            // populated: subject specializes a supertype that has a public Type membership →
            // the public membership surfaces; private members are excluded.
            var supertype = new Type();
            var specialization = new Specialization { Specific = subject, General = supertype };
            subject.AssignOwnership(specialization);

            var publicElement = new Type();
            var publicMembership = new OwningMembership { Visibility = VisibilityKind.Public };
            supertype.AssignOwnership(publicMembership, publicElement);

            var privateElement = new Type();
            var privateMembership = new OwningMembership { Visibility = VisibilityKind.Private };
            supertype.AssignOwnership(privateMembership, privateElement);

            var result = subject.ComputeInheritableMembershipsOperation(null, null, false);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(result, Does.Contain(publicMembership));
                Assert.That(result, Does.Not.Contain(privateMembership));
            }
        }

        [Test]
        public void VerifyComputeInheritedMembershipsOperation()
        {
            Assert.That(() => ((IType)null).ComputeInheritedMembershipsOperation(null, null, false), Throws.TypeOf<ArgumentNullException>());

            var subject = new Type();

            Assert.That(subject.ComputeInheritedMembershipsOperation(null, null, false), Has.Count.EqualTo(0));

            // populated: subject specializes a supertype that exposes a public Type membership.
            // RemoveRedefinedFeatures (which wraps InheritableMemberships) leaves Type-typed memberships
            // alone, so the public membership surfaces.
            var supertype = new Type();
            var specialization = new Specialization { Specific = subject, General = supertype };
            subject.AssignOwnership(specialization);

            var publicElement = new Type();
            var publicMembership = new OwningMembership { Visibility = VisibilityKind.Public };
            supertype.AssignOwnership(publicMembership, publicElement);

            Assert.That(subject.ComputeInheritedMembershipsOperation(null, null, false), Does.Contain(publicMembership));
        }

        [Test]
        public void VerifyComputeNonPrivateMembershipsOperation()
        {
            Assert.That(() => ((IType)null).ComputeNonPrivateMembershipsOperation(null, null, false), Throws.TypeOf<ArgumentNullException>());

            var subject = new Type();

            Assert.That(subject.ComputeNonPrivateMembershipsOperation(null, null, false), Has.Count.EqualTo(0));

            var publicElement = new Type();
            var publicMembership = new OwningMembership { Visibility = VisibilityKind.Public };
            subject.AssignOwnership(publicMembership, publicElement);

            var privateElement = new Type();
            var privateMembership = new OwningMembership { Visibility = VisibilityKind.Private };
            subject.AssignOwnership(privateMembership, privateElement);

            // private memberships must be excluded; the public one remains.
            var result = subject.ComputeNonPrivateMembershipsOperation(null, null, false);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(result, Does.Contain(publicMembership));
                Assert.That(result, Does.Not.Contain(privateMembership));
            }
        }

        [Test]
        public void VerifyComputeRedefinedVisibleMembershipsOperation()
        {
            Assert.That(() => ((IType)null).ComputeRedefinedVisibleMembershipsOperation(null, false, false), Throws.TypeOf<ArgumentNullException>());

            var subject = new Type();

            Assert.That(subject.ComputeRedefinedVisibleMembershipsOperation(null, false, false), Has.Count.EqualTo(0));

            var publicElement = new Type();
            var publicMembership = new OwningMembership { Visibility = VisibilityKind.Public };
            subject.AssignOwnership(publicMembership, publicElement);

            var privateElement = new Type();
            var privateMembership = new OwningMembership { Visibility = VisibilityKind.Private };
            subject.AssignOwnership(privateMembership, privateElement);

            var includeAllResult = subject.ComputeRedefinedVisibleMembershipsOperation(null, false, true);
            var publicOnlyResult = subject.ComputeRedefinedVisibleMembershipsOperation(null, false, false);

            using (Assert.EnterMultipleScope())
            {
                // includeAll = true → both public and private surface.
                Assert.That(includeAllResult, Does.Contain(publicMembership));
                Assert.That(includeAllResult, Does.Contain(privateMembership));

                // includeAll = false → only public.
                Assert.That(publicOnlyResult, Does.Contain(publicMembership));
                Assert.That(publicOnlyResult, Does.Not.Contain(privateMembership));
            }
        }

        [Test]
        public void VerifyComputeFeatureMembership()
        {
            Assert.That(() => ((IType)null).ComputeFeatureMembership(), Throws.TypeOf<ArgumentNullException>());

            var subject = new Type();

            Assert.That(subject.ComputeFeatureMembership(), Has.Count.EqualTo(0));

            var feature = new Feature();
            var featureMembership = new FeatureMembership();
            subject.AssignOwnership(featureMembership, feature);

            // An OwningMembership (non-FeatureMembership) owned by the subject must be EXCLUDED.
            var nonFeature = new Feature();
            var owningMembership = new OwningMembership();
            subject.AssignOwnership(owningMembership, nonFeature);

            Assert.That(subject.ComputeFeatureMembership(), Is.EquivalentTo([featureMembership]));

            // Inherited FeatureMembership via Specialization must be INCLUDED.
            var supertype = new Type();
            var specialization = new Specialization { Specific = subject, General = supertype };
            subject.AssignOwnership(specialization);

            var inheritedFeature = new Feature();
            var inheritedFeatureMembership = new FeatureMembership { Visibility = VisibilityKind.Public };
            supertype.AssignOwnership(inheritedFeatureMembership, inheritedFeature);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(subject.ComputeFeatureMembership(), Does.Contain(featureMembership));
                Assert.That(subject.ComputeFeatureMembership(), Does.Contain(inheritedFeatureMembership));
                Assert.That(subject.ComputeFeatureMembership(), Does.Not.Contain(owningMembership));
            }
        }

        [Test]
        public void VerifyComputeInheritedFeature()
        {
            Assert.That(() => ((IType)null).ComputeInheritedFeature(), Throws.TypeOf<ArgumentNullException>());

            var subject = new Type();

            // empty case: no inherited memberships → no inherited features.
            Assert.That(subject.ComputeInheritedFeature(), Has.Count.EqualTo(0));

            var supertype = new Type();
            var specialization = new Specialization { Specific = subject, General = supertype };
            subject.AssignOwnership(specialization);

            var inheritedFeature = new Feature();
            var inheritedFeatureMembership = new FeatureMembership { Visibility = VisibilityKind.Public };
            supertype.AssignOwnership(inheritedFeatureMembership, inheritedFeature);

            // An inherited non-FM OwningMembership on the supertype must be EXCLUDED.
            var nonFmElement = new Feature();
            var inheritedOwningMembership = new OwningMembership { Visibility = VisibilityKind.Public };
            supertype.AssignOwnership(inheritedOwningMembership, nonFmElement);

            // Own features of the subject must NOT surface in inheritedFeature.
            var ownFeature = new Feature();
            var ownFeatureMembership = new FeatureMembership();
            subject.AssignOwnership(ownFeatureMembership, ownFeature);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(subject.ComputeInheritedFeature(), Does.Contain(inheritedFeature));
                Assert.That(subject.ComputeInheritedFeature(), Does.Not.Contain(nonFmElement));
                Assert.That(subject.ComputeInheritedFeature(), Does.Not.Contain(ownFeature));
            }
        }

        [Test]
        public void VerifyComputeInheritedMembership()
        {
            Assert.That(() => ((IType)null).ComputeInheritedMembership(), Throws.TypeOf<ArgumentNullException>());

            var subject = new Type();

            Assert.That(subject.ComputeInheritedMembership(), Has.Count.EqualTo(0));

            // populated: subject specializes a supertype with a public Type membership →
            // the public membership surfaces in inheritedMembership.
            var supertype = new Type();
            var specialization = new Specialization { Specific = subject, General = supertype };
            subject.AssignOwnership(specialization);

            var publicElement = new Type();
            var publicMembership = new OwningMembership { Visibility = VisibilityKind.Public };
            supertype.AssignOwnership(publicMembership, publicElement);

            Assert.That(subject.ComputeInheritedMembership(), Does.Contain(publicMembership));
        }

        [Test]
        public void VerifyComputeSpecializesFromLibraryOperation()
        {
            Assert.That(() => ((IType)null).ComputeSpecializesFromLibraryOperation("Some::Type"), Throws.TypeOf<ArgumentNullException>());

            var bareSubject = new Type();

            using (Assert.EnterMultipleScope())
            {
                // null/blank library names short-circuit to false.
                Assert.That(bareSubject.ComputeSpecializesFromLibraryOperation(null), Is.False);
                Assert.That(bareSubject.ComputeSpecializesFromLibraryOperation("  "), Is.False);

                // unresolvable name → ResolveGlobal returns null → false.
                Assert.That(bareSubject.ComputeSpecializesFromLibraryOperation("Nonexistent::Type"), Is.False);
            }

            // positive case: build a small library namespace, place the subject type in it,
            // and have the subject specialize the library type.
            var rootNamespace = new Namespace();

            var libraryType = new Type { DeclaredName = "LibType" };
            var libraryMembership = new OwningMembership { Visibility = VisibilityKind.Public };
            rootNamespace.AssignOwnership(libraryMembership, libraryType);

            var subject = new Type();
            var subjectMembership = new OwningMembership { Visibility = VisibilityKind.Public };
            rootNamespace.AssignOwnership(subjectMembership, subject);

            var specialization = new Specialization { Specific = subject, General = libraryType };
            subject.AssignOwnership(specialization);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(subject.ComputeSpecializesFromLibraryOperation("LibType"), Is.True);

                // resolves to a Type the subject does NOT specialize → false.
                var unrelatedType = new Type { DeclaredName = "UnrelatedType" };
                var unrelatedMembership = new OwningMembership { Visibility = VisibilityKind.Public };
                rootNamespace.AssignOwnership(unrelatedMembership, unrelatedType);

                Assert.That(subject.ComputeSpecializesFromLibraryOperation("UnrelatedType"), Is.False);
            }
        }

        [Test]
        public void VerifyComputeMultiplicitiesOperation()
        {
            Assert.That(() => ((IType)null).ComputeMultiplicitiesOperation(), Throws.TypeOf<ArgumentNullException>());

            var subject = new Type();

            // empty case: no own multiplicity, no supertypes → empty result.
            Assert.That(subject.ComputeMultiplicitiesOperation(), Has.Count.EqualTo(0));

            // populated case: subject owns a Multiplicity directly → result is just that one.
            var multiplicity = new Multiplicity();
            var multiplicityMembership = new OwningMembership();
            subject.AssignOwnership(multiplicityMembership, multiplicity);

            var result = subject.ComputeMultiplicitiesOperation();

            using (Assert.EnterMultipleScope())
            {
                Assert.That(result, Has.Count.EqualTo(1));
                Assert.That(result[0], Is.SameAs(multiplicity));
            }
        }
    }
}
