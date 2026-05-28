// -------------------------------------------------------------------------------------------------
// <copyright file="AnnotationExtensionsTestFixture.cs" company="Starion Group S.A.">
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

    [TestFixture]
    public class AnnotationExtensionsTestFixture
    {
        [Test]
        public void Verify_ComputeAnnotatingElement()
        {
            Assert.That(
                () => ((IAnnotation)null).ComputeAnnotatingElement(),
                Throws.TypeOf<ArgumentNullException>());

            var ownedSubject = new Annotation();
            var ownedAnnotating = new Comment();
            ((IContainedRelationship)ownedSubject).OwnedRelatedElement.Add(ownedAnnotating);

            Assert.That(ownedSubject.ComputeAnnotatingElement(), Is.SameAs(ownedAnnotating));

            var owningSubject = new Annotation();
            var owningAnnotating = new Comment();
            ((IContainedRelationship)owningSubject).OwningRelatedElement = owningAnnotating;

            Assert.That(owningSubject.ComputeAnnotatingElement(), Is.SameAs(owningAnnotating));

            var emptySubject = new Annotation();

            Assert.That(emptySubject.ComputeAnnotatingElement(), Is.Null);
        }

        [Test]
        public void Verify_ComputeOwnedAnnotatingElement()
        {
            Assert.That(
                () => ((IAnnotation)null).ComputeOwnedAnnotatingElement(),
                Throws.TypeOf<ArgumentNullException>());

            var emptySubject = new Annotation();

            Assert.That(emptySubject.ComputeOwnedAnnotatingElement(), Is.Null);

            var nonAnnotatingOnlySubject = new Annotation();
            ((IContainedRelationship)nonAnnotatingOnlySubject).OwnedRelatedElement.Add(new Package());

            Assert.That(nonAnnotatingOnlySubject.ComputeOwnedAnnotatingElement(), Is.Null);

            var mixedSubject = new Annotation();
            ((IContainedRelationship)mixedSubject).OwnedRelatedElement.Add(new Package());
            var ownedComment = new Comment();
            ((IContainedRelationship)mixedSubject).OwnedRelatedElement.Add(ownedComment);

            // Proves selectByKind, not positional [0].
            Assert.That(mixedSubject.ComputeOwnedAnnotatingElement(), Is.SameAs(ownedComment));
        }

        [Test]
        public void Verify_ComputeOwningAnnotatedElement()
        {
            Assert.That(
                () => ((IAnnotation)null).ComputeOwningAnnotatedElement(),
                Throws.TypeOf<ArgumentNullException>());

            var noOwningSubject = new Annotation();

            Assert.That(noOwningSubject.ComputeOwningAnnotatedElement(), Is.Null);

            var equalSubject = new Annotation();
            var annotated = new Package();
            equalSubject.AnnotatedElement = annotated;
            ((IContainedRelationship)equalSubject).OwningRelatedElement = annotated;

            Assert.That(equalSubject.ComputeOwningAnnotatedElement(), Is.SameAs(annotated));

            var unequalSubject = new Annotation();
            unequalSubject.AnnotatedElement = new Package();
            ((IContainedRelationship)unequalSubject).OwningRelatedElement = new Comment();

            // Load-bearing negative: OwningRelatedElement is set but does not equal AnnotatedElement → null.
            Assert.That(unequalSubject.ComputeOwningAnnotatedElement(), Is.Null);

            var nullAnnotatedSubject = new Annotation();
            ((IContainedRelationship)nullAnnotatedSubject).OwningRelatedElement = new Package();

            Assert.That(nullAnnotatedSubject.ComputeOwningAnnotatedElement(), Is.Null);
        }

        [Test]
        public void Verify_ComputeOwningAnnotatingElement()
        {
            Assert.That(
                () => ((IAnnotation)null).ComputeOwningAnnotatingElement(),
                Throws.TypeOf<ArgumentNullException>());

            var noOwningSubject = new Annotation();

            Assert.That(noOwningSubject.ComputeOwningAnnotatingElement(), Is.Null);

            var annotatingOwningSubject = new Annotation();
            var owningComment = new Comment();
            ((IContainedRelationship)annotatingOwningSubject).OwningRelatedElement = owningComment;

            Assert.That(annotatingOwningSubject.ComputeOwningAnnotatingElement(), Is.SameAs(owningComment));

            var nonAnnotatingOwningSubject = new Annotation();
            ((IContainedRelationship)nonAnnotatingOwningSubject).OwningRelatedElement = new Package();

            Assert.That(nonAnnotatingOwningSubject.ComputeOwningAnnotatingElement(), Is.Null);
        }
    }
}
