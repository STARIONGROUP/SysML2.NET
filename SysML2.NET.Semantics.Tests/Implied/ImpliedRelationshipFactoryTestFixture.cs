// -------------------------------------------------------------------------------------------------
// <copyright file="ImpliedRelationshipFactoryTestFixture.cs" company="Starion Group S.A.">
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

    using NUnit.Framework;

    using SysML2.NET.Core.POCO.Core.Classifiers;
    using SysML2.NET.Core.POCO.Core.Features;
    using SysML2.NET.Core.POCO.Core.Types;
    using SysML2.NET.Semantics.Implied;

    [TestFixture]
    public class ImpliedRelationshipFactoryTestFixture
    {
        private ImpliedRelationshipFactory factory;

        [SetUp]
        public void SetUp()
        {
            this.factory = new ImpliedRelationshipFactory();
        }

        [Test]
        public void VerifyCreateImpliedSubclassification()
        {
            var specific = new Classifier { Id = Guid.NewGuid(), DeclaredName = "Specific" };
            var general = new Classifier { Id = Guid.NewGuid(), DeclaredName = "General" };

            var subclassification = this.factory.CreateImpliedSubclassification(specific, general);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(subclassification.IsImplied, Is.True);
                Assert.That(subclassification.Subclassifier, Is.SameAs(specific));
                Assert.That(subclassification.Superclassifier, Is.SameAs(general));
                Assert.That(subclassification.Id, Is.Not.EqualTo(Guid.Empty));
                Assert.That((subclassification).General, Is.SameAs(general));
                Assert.That((subclassification).Specific, Is.SameAs(specific));

                // The product must stay detached: attaching it would oblige the Element to declare
                // isImpliedIncluded, which cannot be honoured while constraints remain uncovered.
                Assert.That(specific.OwnedRelationship, Is.Empty);
                Assert.That(general.OwnedRelationship, Is.Empty);

                Assert.That(() => this.factory.CreateImpliedSubclassification(null, general), Throws.TypeOf<ArgumentNullException>());
                Assert.That(() => this.factory.CreateImpliedSubclassification(specific, null), Throws.TypeOf<ArgumentNullException>());
            }
        }

        [Test]
        public void VerifyCreateImpliedSubsetting()
        {
            var specific = new Feature { Id = Guid.NewGuid(), DeclaredName = "specific" };
            var general = new Feature { Id = Guid.NewGuid(), DeclaredName = "general" };

            var subsetting = this.factory.CreateImpliedSubsetting(specific, general);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(subsetting.IsImplied, Is.True);
                Assert.That(subsetting.SubsettingFeature, Is.SameAs(specific));
                Assert.That(subsetting.SubsettedFeature, Is.SameAs(general));
                Assert.That(subsetting.Id, Is.Not.EqualTo(Guid.Empty));
                Assert.That(specific.OwnedRelationship, Is.Empty);

                Assert.That(() => this.factory.CreateImpliedSubsetting(null, general), Throws.TypeOf<ArgumentNullException>());
                Assert.That(() => this.factory.CreateImpliedSubsetting(specific, null), Throws.TypeOf<ArgumentNullException>());
            }
        }

        [Test]
        public void VerifyCreateImpliedRedefinition()
        {
            var specific = new Feature { Id = Guid.NewGuid(), DeclaredName = "specific" };
            var general = new Feature { Id = Guid.NewGuid(), DeclaredName = "general" };

            var redefinition = this.factory.CreateImpliedRedefinition(specific, general);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(redefinition.IsImplied, Is.True);
                Assert.That(redefinition.RedefiningFeature, Is.SameAs(specific));
                Assert.That(redefinition.RedefinedFeature, Is.SameAs(general));
                Assert.That(redefinition.Id, Is.Not.EqualTo(Guid.Empty));
                Assert.That(specific.OwnedRelationship, Is.Empty);

                Assert.That(() => this.factory.CreateImpliedRedefinition(null, general), Throws.TypeOf<ArgumentNullException>());
                Assert.That(() => this.factory.CreateImpliedRedefinition(specific, null), Throws.TypeOf<ArgumentNullException>());
            }
        }
    }
}
