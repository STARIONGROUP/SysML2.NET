// -------------------------------------------------------------------------------------------------
// <copyright file="ImpliedSpecializationReducerTestFixture.cs" company="Starion Group S.A.">
//
//    Copyright (C) 2022-2026 Starion Group S.A.
//
//    Licensed under the Apache License, Version 2.0 (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
//
//         http://www.apache.org/licenses/LICENSE-2.0
//
//    Unless required by applicable law or agreed to in writing, software
//    distributed under the License is distributed on an "AS IS" BASIS,
//    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//    See the License for the specific language governing permissions and
//    limitations under the License.
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.Semantics.Tests.Implied
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using NUnit.Framework;

    using SysML2.NET.Core.POCO.Core.Classifiers;
    using SysML2.NET.Core.POCO.Core.Types;
    using SysML2.NET.Core.POCO.Root.Elements;
    using SysML2.NET.Semantics.Implied;

    [TestFixture]
    public class ImpliedSpecializationReducerTestFixture
    {
        private ImpliedSpecializationReducer reducer;

        private ImpliedRelationshipFactory factory;

        private Classifier anything;

        private Classifier occurrence;

        private Classifier subject;

        [SetUp]
        public void SetUp()
        {
            this.reducer = new ImpliedSpecializationReducer();
            this.factory = new ImpliedRelationshipFactory();

            this.anything = new Classifier { Id = Guid.NewGuid(), DeclaredName = "Anything" };
            this.occurrence = new Classifier { Id = Guid.NewGuid(), DeclaredName = "Occurrence" };
            this.subject = new Classifier { Id = Guid.NewGuid(), DeclaredName = "Subject" };

            // Occurrence specializes Anything, so Occurrence is a strict subtype of Anything.
            Specialize(this.occurrence, this.anything);
        }

        [Test]
        public void VerifyReduce()
        {
            var impliedAnything = this.factory.CreateImpliedSubclassification(this.subject, this.anything);
            var impliedOccurrence = this.factory.CreateImpliedSubclassification(this.subject, this.occurrence);
            var duplicateOccurrence = this.factory.CreateImpliedSubclassification(this.subject, this.occurrence);

            using (Assert.EnterMultipleScope())
            {
                // Rule 1 within the implied set: Occurrence is a strict subtype of Anything, so specializing
                // Anything as well is redundant.
                var reduced = this.reducer.Reduce(this.subject, [impliedAnything, impliedOccurrence]);
                Assert.That(reduced.Select(specialization => specialization.General), Is.EqualTo(new IType[] { this.occurrence }));

                // Rule 2: two candidates with the same general Type collapse to one.
                var deduplicated = this.reducer.Reduce(this.subject, [impliedOccurrence, duplicateOccurrence]);
                Assert.That(deduplicated, Has.Count.EqualTo(1));
                Assert.That(deduplicated[0], Is.SameAs(impliedOccurrence));

                // A single candidate with no competitor survives — the self-comparison must not drop it.
                var single = this.reducer.Reduce(this.subject, [impliedOccurrence]);
                Assert.That(single, Has.Count.EqualTo(1));

                Assert.That(this.reducer.Reduce(this.subject, []), Is.Empty);
                Assert.That(() => this.reducer.Reduce(null, []), Throws.TypeOf<ArgumentNullException>());
                Assert.That(() => this.reducer.Reduce(this.subject, null), Throws.TypeOf<ArgumentNullException>());
            }
        }

        [Test]
        public void VerifyReduceAgainstDeclaredSpecializations()
        {
            var declaring = new Classifier { Id = Guid.NewGuid(), DeclaredName = "Declaring" };
            Specialize(declaring, this.occurrence);

            var impliedAnything = this.factory.CreateImpliedSubclassification(declaring, this.anything);
            var impliedOccurrence = this.factory.CreateImpliedSubclassification(declaring, this.occurrence);

            using (Assert.EnterMultipleScope())
            {
                // The declared Specialization to Occurrence already satisfies the looser Anything constraint.
                Assert.That(this.reducer.Reduce(declaring, [impliedAnything]), Is.Empty);

                // ... and it makes an implied Specialization with the SAME general Type redundant too.
                Assert.That(this.reducer.Reduce(declaring, [impliedOccurrence]), Is.Empty);
            }
        }

        private static void Specialize(IClassifier specific, IClassifier general)
        {
            var subclassification = new Subclassification
            {
                Id = Guid.NewGuid(),
                Subclassifier = specific,
                Superclassifier = general
            };

            ((IContainedElement)specific).OwnedRelationship.Add(subclassification);
        }
    }
}
