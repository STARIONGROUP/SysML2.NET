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
            subject.AssignOwnership(differencing, differencingType);

            var unrelatedSpecialization = new Specialization();
            subject.AssignOwnership(unrelatedSpecialization, new Type());

            Assert.That(subject.ComputeOwnedDifferencing(), Is.EquivalentTo(new[] { differencing }));
        }

        [Test]
        public void VerifyComputeOwnedDisjoining()
        {
            Assert.That(() => ((IType)null).ComputeOwnedDisjoining(), Throws.TypeOf<ArgumentNullException>());

            var subject = new Type();

            Assert.That(subject.ComputeOwnedDisjoining(), Has.Count.EqualTo(0));

            var disjoining = new Disjoining();
            subject.AssignOwnership(disjoining, new Type());

            var unrelatedSpecialization = new Specialization();
            subject.AssignOwnership(unrelatedSpecialization, new Type());

            Assert.That(subject.ComputeOwnedDisjoining(), Is.EquivalentTo(new[] { disjoining }));
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

            Assert.That(subject.ComputeOwnedFeatureMembership(), Is.EquivalentTo(new[] { featureMembership }));
        }

        [Test]
        public void VerifyComputeOwnedIntersecting()
        {
            Assert.That(() => ((IType)null).ComputeOwnedIntersecting(), Throws.TypeOf<ArgumentNullException>());

            var subject = new Type();

            Assert.That(subject.ComputeOwnedIntersecting(), Has.Count.EqualTo(0));

            var intersectingType = new Type();
            var intersecting = new Intersecting { IntersectingType = intersectingType };
            subject.AssignOwnership(intersecting, intersectingType);

            var unrelatedSpecialization = new Specialization();
            subject.AssignOwnership(unrelatedSpecialization, new Type());

            Assert.That(subject.ComputeOwnedIntersecting(), Is.EquivalentTo(new[] { intersecting }));
        }

        [Test]
        public void VerifyComputeOwnedUnioning()
        {
            Assert.That(() => ((IType)null).ComputeOwnedUnioning(), Throws.TypeOf<ArgumentNullException>());

            var subject = new Type();

            Assert.That(subject.ComputeOwnedUnioning(), Has.Count.EqualTo(0));

            var unioningType = new Type();
            var unioning = new Unioning { UnioningType = unioningType };
            subject.AssignOwnership(unioning, unioningType);

            var unrelatedSpecialization = new Specialization();
            subject.AssignOwnership(unrelatedSpecialization, new Type());

            Assert.That(subject.ComputeOwnedUnioning(), Is.EquivalentTo(new[] { unioning }));
        }

        [Test]
        public void VerifyComputeOwnedSpecialization()
        {
            Assert.That(() => ((IType)null).ComputeOwnedSpecialization(), Throws.TypeOf<ArgumentNullException>());

            var subject = new Type();

            Assert.That(subject.ComputeOwnedSpecialization(), Has.Count.EqualTo(0));

            var general = new Type();
            var specialization = new Specialization { Specific = subject, General = general };
            subject.AssignOwnership(specialization, general);

            // a Specialization owned by subject but whose Specific points elsewhere must be filtered out
            var elsewhere = new Type();
            var foreignSpecialization = new Specialization { Specific = elsewhere, General = new Type() };
            subject.AssignOwnership(foreignSpecialization, new Type());

            Assert.That(subject.ComputeOwnedSpecialization(), Is.EquivalentTo(new[] { specialization }));
        }

        [Test]
        public void VerifyComputeOwnedConjugator()
        {
            Assert.That(() => ((IType)null).ComputeOwnedConjugator(), Throws.TypeOf<ArgumentNullException>());

            var subject = new Type();

            Assert.That(subject.ComputeOwnedConjugator(), Is.Null);

            var originalType = new Type();
            var conjugation = new Conjugation { ConjugatedType = subject, OriginalType = originalType };
            subject.AssignOwnership(conjugation, originalType);

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
            subject.AssignOwnership(conjugation, originalType);

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
            subject.AssignOwnership(differencing, differencingType);

            var nullDifferencing = new Differencing();
            subject.AssignOwnership(nullDifferencing, new Type());

            Assert.That(subject.ComputeDifferencingType(), Is.EquivalentTo(new[] { differencingType }));
        }

        [Test]
        public void VerifyComputeIntersectingType()
        {
            Assert.That(() => ((IType)null).ComputeIntersectingType(), Throws.TypeOf<ArgumentNullException>());

            var subject = new Type();

            Assert.That(subject.ComputeIntersectingType(), Has.Count.EqualTo(0));

            var intersectingType = new Type();
            var intersecting = new Intersecting { IntersectingType = intersectingType };
            subject.AssignOwnership(intersecting, intersectingType);

            var nullIntersecting = new Intersecting();
            subject.AssignOwnership(nullIntersecting, new Type());

            Assert.That(subject.ComputeIntersectingType(), Is.EquivalentTo(new[] { intersectingType }));
        }

        [Test]
        public void VerifyComputeUnioningType()
        {
            Assert.That(() => ((IType)null).ComputeUnioningType(), Throws.TypeOf<ArgumentNullException>());

            var subject = new Type();

            Assert.That(subject.ComputeUnioningType(), Has.Count.EqualTo(0));

            var unioningType = new Type();
            var unioning = new Unioning { UnioningType = unioningType };
            subject.AssignOwnership(unioning, unioningType);

            var nullUnioning = new Unioning();
            subject.AssignOwnership(nullUnioning, new Type());

            Assert.That(subject.ComputeUnioningType(), Is.EquivalentTo(new[] { unioningType }));
        }

        [Test]
        public void VerifyComputeOwnedFeature()
        {
            Assert.That(() => ((IType)null).ComputeOwnedFeature(), Throws.TypeOf<ArgumentNullException>());

            var subject = new Type();

            Assert.That(subject.ComputeOwnedFeature(), Has.Count.EqualTo(0));

            // populated case depends on FeatureMembershipExtensions.ComputeOwnedMemberFeature, which is still a stub.
            var feature = new Feature();
            var featureMembership = new FeatureMembership();
            subject.AssignOwnership(featureMembership, feature);

            Assert.That(() => subject.ComputeOwnedFeature(), Throws.TypeOf<NotSupportedException>());
        }

        [Test]
        public void VerifyComputeFeature()
        {
            Assert.That(() => ((IType)null).ComputeFeature(), Throws.TypeOf<ArgumentNullException>());

            var subject = new Type();

            Assert.That(subject.ComputeFeature(), Has.Count.EqualTo(0));

            // populated case depends on FeatureMembershipExtensions.ComputeOwnedMemberFeature, which is still a stub.
            var feature = new Feature();
            var featureMembership = new FeatureMembership();
            subject.AssignOwnership(featureMembership, feature);

            Assert.That(() => subject.ComputeFeature(), Throws.TypeOf<NotSupportedException>());
        }

        [Test]
        public void VerifyComputeOwnedEndFeature()
        {
            Assert.That(() => ((IType)null).ComputeOwnedEndFeature(), Throws.TypeOf<ArgumentNullException>());

            var subject = new Type();

            Assert.That(subject.ComputeOwnedEndFeature(), Has.Count.EqualTo(0));

            // populated case depends on FeatureMembershipExtensions.ComputeOwnedMemberFeature, which is still a stub.
            var endFeature = new Feature { IsEnd = true };
            var endMembership = new FeatureMembership();
            subject.AssignOwnership(endMembership, endFeature);

            Assert.That(() => subject.ComputeOwnedEndFeature(), Throws.TypeOf<NotSupportedException>());
        }

        [Test]
        public void VerifyComputeEndFeature()
        {
            Assert.That(() => ((IType)null).ComputeEndFeature(), Throws.TypeOf<ArgumentNullException>());

            var subject = new Type();

            Assert.That(subject.ComputeEndFeature(), Has.Count.EqualTo(0));

            // populated case depends on FeatureMembershipExtensions.ComputeOwnedMemberFeature, which is still a stub.
            var endFeature = new Feature { IsEnd = true };
            var endMembership = new FeatureMembership();
            subject.AssignOwnership(endMembership, endFeature);

            Assert.That(() => subject.ComputeEndFeature(), Throws.TypeOf<NotSupportedException>());
        }

        [Test]
        public void VerifyComputeSupertypesOperation()
        {
            Assert.That(() => ((IType)null).ComputeSupertypesOperation(false), Throws.TypeOf<ArgumentNullException>());

            var subject = new Type();

            Assert.That(subject.ComputeSupertypesOperation(false), Has.Count.EqualTo(0));

            var general = new Type();
            var specialization = new Specialization { Specific = subject, General = general };
            subject.AssignOwnership(specialization, general);

            var impliedGeneral = new Type();
            var impliedSpecialization = new Specialization { Specific = subject, General = impliedGeneral, IsImplied = true };
            subject.AssignOwnership(impliedSpecialization, impliedGeneral);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(subject.ComputeSupertypesOperation(false), Is.EquivalentTo(new[] { general, impliedGeneral }));
                Assert.That(subject.ComputeSupertypesOperation(true), Is.EquivalentTo(new[] { general }));
            }

            // conjugated branch returns the OriginalType of the conjugator.
            var conjugatedSubject = new Type();
            var originalType = new Type();
            var conjugation = new Conjugation { ConjugatedType = conjugatedSubject, OriginalType = originalType };
            conjugatedSubject.AssignOwnership(conjugation, originalType);

            Assert.That(conjugatedSubject.ComputeSupertypesOperation(false), Is.EquivalentTo(new[] { originalType }));
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
            subject.AssignOwnership(subjectToMiddle, middle);

            var middleToTop = new Specialization { Specific = middle, General = top };
            middle.AssignOwnership(middleToTop, top);

            // 3-level chain plus a self-loop to demonstrate cycle safety.
            var cycleSpecialization = new Specialization { Specific = top, General = subject };
            top.AssignOwnership(cycleSpecialization, subject);

            var result = subject.ComputeAllSupertypesOperation();

            using (Assert.EnterMultipleScope())
            {
                Assert.That(result, Is.EquivalentTo(new[] { subject, middle, top }));
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
            subject.AssignOwnership(specialization, supertype);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(subject.ComputeSpecializesOperation(supertype), Is.True);
                Assert.That(subject.ComputeSpecializesOperation(unrelated), Is.False);
            }

            // conjugated branch: subject specializes whatever its OriginalType specializes.
            var conjugatedSubject = new Type();
            var originalType = new Type();
            var conjugation = new Conjugation { ConjugatedType = conjugatedSubject, OriginalType = originalType };
            conjugatedSubject.AssignOwnership(conjugation, originalType);

            var originalSupertype = new Type();
            var originalSpecialization = new Specialization { Specific = originalType, General = originalSupertype };
            originalType.AssignOwnership(originalSpecialization, originalSupertype);

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
            subject.AssignOwnership(specialization, supertype);

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
            Assert.That(() => ((IType)null).ComputeDirectionOfExcludingOperation(new Feature(), new System.Collections.Generic.List<IType>()), Throws.TypeOf<ArgumentNullException>());

            var subject = new Type();

            Assert.That(() => subject.ComputeDirectionOfExcludingOperation(null, new System.Collections.Generic.List<IType>()), Throws.TypeOf<ArgumentNullException>());

            // unrelated feature with unrelated owningType: result is null.
            var unrelatedFeature = new Feature { Direction = FeatureDirectionKind.In };

            Assert.That(subject.ComputeDirectionOfExcludingOperation(unrelatedFeature, null), Is.Null);

            // direct branch: feature.owningType == subject → returns feature.Direction.
            var ownedFeature = new Feature { Direction = FeatureDirectionKind.In };
            var featureMembership = new FeatureMembership();
            subject.AssignOwnership(featureMembership, ownedFeature);

            Assert.That(subject.ComputeDirectionOfExcludingOperation(ownedFeature, new System.Collections.Generic.List<IType>()), Is.EqualTo(FeatureDirectionKind.In));

            // conjugation flip: a conjugated subject inherits the feature direction from its OriginalType
            // and flips In↔Out at the current level.
            var conjugatedSubject = new Type();
            var originalType = new Type();
            var conjugation = new Conjugation { ConjugatedType = conjugatedSubject, OriginalType = originalType };
            conjugatedSubject.AssignOwnership(conjugation, originalType);

            var originalSpecialization = new Specialization { Specific = originalType, General = subject };
            originalType.AssignOwnership(originalSpecialization, subject);

            // For conjugatedSubject:
            //   supertypes(false) = [originalType]
            //   originalType.directionOfExcluding(ownedFeature, ...) traverses to subject and yields In
            //   then conjugatedSubject is conjugated → flip In to Out.
            Assert.That(conjugatedSubject.ComputeDirectionOfExcludingOperation(ownedFeature, new System.Collections.Generic.List<IType>()), Is.EqualTo(FeatureDirectionKind.Out));
        }

        [Test]
        public void VerifyComputeDirectedFeature()
        {
            Assert.That(() => ((IType)null).ComputeDirectedFeature(), Throws.TypeOf<ArgumentNullException>());

            var subject = new Type();

            Assert.That(subject.ComputeDirectedFeature(), Has.Count.EqualTo(0));

            // populated case depends on FeatureMembershipExtensions.ComputeOwnedMemberFeature, which is still a stub.
            var directedFeature = new Feature { Direction = FeatureDirectionKind.In };
            var directedMembership = new FeatureMembership();
            subject.AssignOwnership(directedMembership, directedFeature);

            Assert.That(() => subject.ComputeDirectedFeature(), Throws.TypeOf<NotSupportedException>());
        }

        [Test]
        public void VerifyComputeInput()
        {
            Assert.That(() => ((IType)null).ComputeInput(), Throws.TypeOf<ArgumentNullException>());

            var subject = new Type();

            Assert.That(subject.ComputeInput(), Has.Count.EqualTo(0));

            // populated case depends on FeatureMembershipExtensions.ComputeOwnedMemberFeature, which is still a stub.
            var inFeature = new Feature { Direction = FeatureDirectionKind.In };
            var inMembership = new FeatureMembership();
            subject.AssignOwnership(inMembership, inFeature);

            Assert.That(() => subject.ComputeInput(), Throws.TypeOf<NotSupportedException>());
        }

        [Test]
        public void VerifyComputeOutput()
        {
            Assert.That(() => ((IType)null).ComputeOutput(), Throws.TypeOf<ArgumentNullException>());

            var subject = new Type();

            Assert.That(subject.ComputeOutput(), Has.Count.EqualTo(0));

            // populated case depends on FeatureMembershipExtensions.ComputeOwnedMemberFeature, which is still a stub.
            var outFeature = new Feature { Direction = FeatureDirectionKind.Out };
            var outMembership = new FeatureMembership();
            subject.AssignOwnership(outMembership, outFeature);

            Assert.That(() => subject.ComputeOutput(), Throws.TypeOf<NotSupportedException>());
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
            redefiningFeature.AssignOwnership(redefinition, new Feature());

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
            Assert.That(() => ((IType)null).ComputeRemoveRedefinedFeaturesOperation(new System.Collections.Generic.List<IMembership>()), Throws.TypeOf<ArgumentNullException>());

            var subject = new Type();

            Assert.That(() => subject.ComputeRemoveRedefinedFeaturesOperation(null), Throws.TypeOf<ArgumentNullException>());

            // empty input → empty output.
            Assert.That(subject.ComputeRemoveRedefinedFeaturesOperation(new System.Collections.Generic.List<IMembership>()), Has.Count.EqualTo(0));

            // populated: two memberships where one carries a Feature that redefines the Feature in the other.
            // The redefining-feature membership should drop the redefined-feature membership.
            var baseFeature = new Feature();
            var derivedFeature = new Feature();
            var derivedRedefinesBase = new Redefinition { RedefinedFeature = baseFeature };
            derivedFeature.AssignOwnership(derivedRedefinesBase, new Feature());

            var baseMembership = new Membership { MemberElement = baseFeature };
            var derivedMembership = new Membership { MemberElement = derivedFeature };

            var result = subject.ComputeRemoveRedefinedFeaturesOperation(new System.Collections.Generic.List<IMembership> { baseMembership, derivedMembership });

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
            subject.AssignOwnership(specialization, supertype);

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
            subject.AssignOwnership(specialization, supertype);

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

            using (Assert.EnterMultipleScope())
            {
                // includeAll = true → both public and private surface.
                Assert.That(subject.ComputeRedefinedVisibleMembershipsOperation(null, false, true), Does.Contain(publicMembership));
                Assert.That(subject.ComputeRedefinedVisibleMembershipsOperation(null, false, true), Does.Contain(privateMembership));

                // includeAll = false → only public.
                Assert.That(subject.ComputeRedefinedVisibleMembershipsOperation(null, false, false), Does.Contain(publicMembership));
                Assert.That(subject.ComputeRedefinedVisibleMembershipsOperation(null, false, false), Does.Not.Contain(privateMembership));
            }
        }

        [Test]
        public void VerifyComputeFeatureMembership()
        {
            Assert.That(() => ((IType)null).ComputeFeatureMembership(), Throws.TypeOf<ArgumentNullException>());

            var subject = new Type();

            Assert.That(subject.ComputeFeatureMembership(), Has.Count.EqualTo(0));

            // populated case depends on FeatureMembershipExtensions.ComputeOwnedMemberFeature, which is still a stub.
            var feature = new Feature();
            var featureMembership = new FeatureMembership();
            subject.AssignOwnership(featureMembership, feature);

            Assert.That(() => subject.ComputeFeatureMembership(), Throws.TypeOf<NotSupportedException>());
        }

        [Test]
        public void VerifyComputeInheritedFeature()
        {
            Assert.That(() => ((IType)null).ComputeInheritedFeature(), Throws.TypeOf<ArgumentNullException>());

            var subject = new Type();

            // empty case: no inherited memberships → no inherited features.
            Assert.That(subject.ComputeInheritedFeature(), Has.Count.EqualTo(0));

            // populated case depends on FeatureMembershipExtensions.ComputeOwnedMemberFeature, which is still
            // a stub: the OCL is `inheritedMembership->selectByKind(FeatureMembership).memberFeature`, and
            // resolving `memberFeature` on a populated FeatureMembership reads `ownedMemberFeature` → stub.
            var supertype = new Type();
            var specialization = new Specialization { Specific = subject, General = supertype };
            subject.AssignOwnership(specialization, supertype);

            var inheritedFeature = new Feature();
            var inheritedFeatureMembership = new FeatureMembership();
            supertype.AssignOwnership(inheritedFeatureMembership, inheritedFeature);

            Assert.That(() => subject.ComputeInheritedFeature(), Throws.TypeOf<NotSupportedException>());
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
            subject.AssignOwnership(specialization, supertype);

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
            subject.AssignOwnership(specialization, new Type());

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
