// -------------------------------------------------------------------------------------------------
// <copyright file="AnnotatingElementExtensionsTestFixture.cs" company="Starion Group S.A.">
// 
//   Copyright (C) 2022-2026 Starion Group S.A.
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

    using SysML2.NET.Core.POCO.Kernel.Packages;
    using SysML2.NET.Core.POCO.Root.Annotations;
    using SysML2.NET.Core.POCO.Root.Elements;
    using SysML2.NET.Core.POCO.Root.Namespaces;
    using SysML2.NET.Extensions;

    [TestFixture]
    public class AnnotatingElementExtensionsTestFixture
    {
        [Test]
        public void Verify_ComputeAnnotatedElement()
        {
            Assert.That(
                () => ((IAnnotatingElement)null).ComputeAnnotatedElement(),
                Throws.TypeOf<ArgumentNullException>());

            // No annotations, no owningNamespace → result is [null] (singleton containing null).
            var orphanSubject = new Comment();

            Assert.That(orphanSubject.ComputeAnnotatedElement(), Is.EqualTo(new IElement[] { null }));

            // No annotations, but subject placed inside a Package → result is [package].
            var ownedNamespaceSubject = new Comment();
            var hostPackage = new Package();
            hostPackage.AssignOwnership(new OwningMembership(), ownedNamespaceSubject);

            Assert.That(ownedNamespaceSubject.ComputeAnnotatedElement(), Is.EqualTo(new[] { hostPackage }));

            // Single owning annotation: annotation owns subject; annotation.AnnotatedElement = annotated.
            var owningAnnotatedSubject = new Comment();
            var owningAnnotation = new Annotation();
            var annotated = new Package();
            owningAnnotation.AnnotatedElement = annotated;
            ((IContainedRelationship)owningAnnotation).OwnedRelatedElement.Add(owningAnnotatedSubject);

            Assert.That(owningAnnotatedSubject.ComputeAnnotatedElement(), Is.EqualTo(new[] { annotated }));

            // Multiple owned annotations, none target self.
            var multiOwnedSubject = new Comment();
            var elementA = new Package();
            var elementB = new Package();
            var ownedAnnotationA = new Annotation { AnnotatedElement = elementA };
            var ownedAnnotationB = new Annotation { AnnotatedElement = elementB };
            ((IContainedElement)multiOwnedSubject).OwnedRelationship.Add(ownedAnnotationA);
            ((IContainedElement)multiOwnedSubject).OwnedRelationship.Add(ownedAnnotationB);

            Assert.That(multiOwnedSubject.ComputeAnnotatedElement(), Is.EqualTo(new[] { elementA, elementB }));

            // Owning + 2 owned.
            var mixedSubject = new Comment();
            var owningTarget = new Package();
            var owningAnnotation2 = new Annotation { AnnotatedElement = owningTarget };
            ((IContainedRelationship)owningAnnotation2).OwnedRelatedElement.Add(mixedSubject);

            var ownedTarget1 = new Package();
            var ownedTarget2 = new Package();
            var ownedAnn1 = new Annotation { AnnotatedElement = ownedTarget1 };
            var ownedAnn2 = new Annotation { AnnotatedElement = ownedTarget2 };
            ((IContainedElement)mixedSubject).OwnedRelationship.Add(ownedAnn1);
            ((IContainedElement)mixedSubject).OwnedRelationship.Add(ownedAnn2);

            Assert.That(mixedSubject.ComputeAnnotatedElement(), Is.EqualTo(new[] { owningTarget, ownedTarget1, ownedTarget2 }));
        }

        [Test]
        public void Verify_ComputeAnnotation()
        {
            Assert.That(
                () => ((IAnnotatingElement)null).ComputeAnnotation(),
                Throws.TypeOf<ArgumentNullException>());

            var emptySubject = new Comment();

            Assert.That(emptySubject.ComputeAnnotation(), Is.Empty);

            // Only owned annotations (no owning).
            var ownedOnlySubject = new Comment();
            var owned1 = new Annotation();
            var owned2 = new Annotation();
            ((IContainedElement)ownedOnlySubject).OwnedRelationship.Add(owned1);
            ((IContainedElement)ownedOnlySubject).OwnedRelationship.Add(owned2);

            Assert.That(ownedOnlySubject.ComputeAnnotation(), Is.EqualTo(new[] { owned1, owned2 }));

            // Owning + 2 owned → owning prepended.
            var bothSubject = new Comment();
            var owningAnn = new Annotation();
            ((IContainedRelationship)owningAnn).OwnedRelatedElement.Add(bothSubject);
            var bothOwned1 = new Annotation();
            var bothOwned2 = new Annotation();
            ((IContainedElement)bothSubject).OwnedRelationship.Add(bothOwned1);
            ((IContainedElement)bothSubject).OwnedRelationship.Add(bothOwned2);

            Assert.That(bothSubject.ComputeAnnotation(), Is.EqualTo(new[] { owningAnn, bothOwned1, bothOwned2 }));
        }

        [Test]
        public void Verify_ComputeOwnedAnnotatingRelationship()
        {
            Assert.That(
                () => ((IAnnotatingElement)null).ComputeOwnedAnnotatingRelationship(),
                Throws.TypeOf<ArgumentNullException>());

            var emptySubject = new Comment();

            Assert.That(emptySubject.ComputeOwnedAnnotatingRelationship(), Is.Empty);

            // Annotation targeting self → filtered out (self is the annotated side, not annotating).
            var selfTargetSubject = new Comment();
            var selfAnnotation = new Annotation { AnnotatedElement = selfTargetSubject };
            ((IContainedElement)selfTargetSubject).OwnedRelationship.Add(selfAnnotation);

            Assert.That(selfTargetSubject.ComputeOwnedAnnotatingRelationship(), Is.Empty);

            // Two annotations: one targeting self (filtered), one targeting other (kept).
            var mixedSubject = new Comment();
            var selfTargetingAnn = new Annotation { AnnotatedElement = mixedSubject };
            var otherTargetingAnn = new Annotation { AnnotatedElement = new Package() };
            ((IContainedElement)mixedSubject).OwnedRelationship.Add(selfTargetingAnn);
            ((IContainedElement)mixedSubject).OwnedRelationship.Add(otherTargetingAnn);

            Assert.That(mixedSubject.ComputeOwnedAnnotatingRelationship(), Is.EqualTo(new[] { otherTargetingAnn }));
        }

        [Test]
        public void Verify_ComputeOwningAnnotatingRelationship()
        {
            Assert.That(
                () => ((IAnnotatingElement)null).ComputeOwningAnnotatingRelationship(),
                Throws.TypeOf<ArgumentNullException>());

            var noOwningSubject = new Comment();

            Assert.That(noOwningSubject.ComputeOwningAnnotatingRelationship(), Is.Null);

            // Owning IS an Annotation → returns it.
            var annotationOwnedSubject = new Comment();
            var owningAnnotation = new Annotation();
            ((IContainedRelationship)owningAnnotation).OwnedRelatedElement.Add(annotationOwnedSubject);

            Assert.That(annotationOwnedSubject.ComputeOwningAnnotatingRelationship(), Is.SameAs(owningAnnotation));

            // Owning is a non-Annotation IRelationship (e.g., OwningMembership) → null.
            var membershipOwnedSubject = new Comment();
            var hostPackage = new Package();
            hostPackage.AssignOwnership(new OwningMembership(), membershipOwnedSubject);

            Assert.That(membershipOwnedSubject.ComputeOwningAnnotatingRelationship(), Is.Null);
        }
    }
}
