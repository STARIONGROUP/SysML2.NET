// ------------------------------------------------------------------------------------------------
// <copyright file="RelationshipOwnershipExtensionsTestFixture.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.Tests.Extensions
{
    using System;

    using NUnit.Framework;

    using SysML2.NET.Core.POCO.Core.Features;
    using SysML2.NET.Core.POCO.Core.Types;
    using SysML2.NET.Core.POCO.Kernel.Packages;
    using SysML2.NET.Core.POCO.Root.Annotations;
    using SysML2.NET.Core.POCO.Root.Elements;
    using SysML2.NET.Core.POCO.Root.Namespaces;
    using SysML2.NET.Core.POCO.Systems.Parts;
    using SysML2.NET.Extensions;

    [TestFixture]
    public class RelationshipOwnershipExtensionsTestFixture
    {
        [Test]
        public void VerifyMoveOwnedRelatedElement()
        {
            var type = new PartDefinition();
            var specialization = new Specialization();
            var otherSpecialization = new Specialization();
            var firstChain = new Feature();
            var secondChain = new Feature();
            var movedChain = new Feature();

            Assert.That(() => ((IRelationship)null).MoveOwnedRelatedElement(firstChain, 0), Throws.TypeOf<ArgumentNullException>());
            Assert.That(() => specialization.MoveOwnedRelatedElement(null, 0), Throws.TypeOf<ArgumentNullException>());
            Assert.That(() => new FeatureMembership().MoveOwnedRelatedElement(new Comment(), 0), Throws.TypeOf<InvalidOperationException>());

            type.AssignOwnership(specialization, firstChain);
            type.AssignOwnership(specialization, secondChain);
            type.AssignOwnership(otherSpecialization, movedChain);

            Assert.That(() => specialization.MoveOwnedRelatedElement(firstChain, 2), Throws.TypeOf<ArgumentOutOfRangeException>());

            specialization.MoveOwnedRelatedElement(secondChain, 0);

            Assert.That(specialization.OwnedRelatedElement, Is.EqualTo([secondChain, firstChain]));

            specialization.MoveOwnedRelatedElement(movedChain, 1);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(specialization.OwnedRelatedElement, Is.EqualTo([secondChain, movedChain, firstChain]));
                Assert.That(otherSpecialization.OwnedRelatedElement, Has.Count.EqualTo(0));
                Assert.That(movedChain.OwningRelationship, Is.SameAs(specialization));
            }

            Assert.That(() => specialization.MoveOwnedRelatedElement(type, 0), Throws.TypeOf<InvalidOperationException>().With.Message.Contains("Containment cycle"));

            var package = new Package();
            var membership = new OwningMembership();
            package.AssignOwnership(membership, new Package());

            Assert.That(() => membership.MoveOwnedRelatedElement(new Package(), 0), Throws.TypeOf<InvalidOperationException>().With.Message.Contains("already owns another element"));
        }

        [Test]
        public void VerifySetOwnedRelatedElements()
        {
            var type = new PartDefinition();
            var specialization = new Specialization();
            var otherSpecialization = new Specialization();
            var firstChain = new Feature();
            var secondChain = new Feature();
            var takenChain = new Feature();

            Assert.That(() => ((IRelationship)null).SetOwnedRelatedElements([]), Throws.TypeOf<ArgumentNullException>());
            Assert.That(() => specialization.SetOwnedRelatedElements(null), Throws.TypeOf<ArgumentNullException>());
            Assert.That(() => specialization.SetOwnedRelatedElements([firstChain, null]), Throws.TypeOf<ArgumentNullException>());

            type.AssignOwnership(specialization, firstChain);
            type.AssignOwnership(specialization, secondChain);
            type.AssignOwnership(otherSpecialization, takenChain);

            Assert.That(() => specialization.SetOwnedRelatedElements([firstChain, firstChain]), Throws.TypeOf<ArgumentException>());
            Assert.That(() => specialization.SetOwnedRelatedElements([secondChain, type]), Throws.TypeOf<InvalidOperationException>().With.Message.Contains("Containment cycle"));
            Assert.That(() => new FeatureMembership().SetOwnedRelatedElements([new Comment()]), Throws.TypeOf<InvalidOperationException>());
            Assert.That(specialization.OwnedRelatedElement, Is.EqualTo([firstChain, secondChain]));

            specialization.SetOwnedRelatedElements([takenChain, secondChain]);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(specialization.OwnedRelatedElement, Is.EqualTo([takenChain, secondChain]));
                Assert.That(firstChain.OwningRelationship, Is.Null);
                Assert.That(otherSpecialization.OwnedRelatedElement, Has.Count.EqualTo(0));
                Assert.That(takenChain.OwningRelationship, Is.SameAs(specialization));
            }

            var package = new Package();
            var membership = new OwningMembership();
            var member = new Package();
            var replacingMember = new Package();
            package.AssignOwnership(membership, member);

            Assert.That(() => membership.SetOwnedRelatedElements([member, replacingMember]), Throws.TypeOf<InvalidOperationException>().With.Message.Contains("owns a single element"));

            membership.SetOwnedRelatedElements([replacingMember]);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(membership.OwnedRelatedElement, Is.EqualTo([replacingMember]));
                Assert.That(member.OwningRelationship, Is.Null);
                Assert.That(package.ownedMember, Is.EqualTo([replacingMember]));
            }
        }

        [Test]
        public void VerifyRemoveOwnedRelatedElement()
        {
            var type = new PartDefinition();
            var specialization = new Specialization();
            var chain = new Feature();
            var otherChain = new Feature();

            Assert.That(() => ((IRelationship)null).RemoveOwnedRelatedElement(chain), Throws.TypeOf<ArgumentNullException>());
            Assert.That(() => specialization.RemoveOwnedRelatedElement(null), Throws.TypeOf<ArgumentNullException>());
            Assert.That(() => specialization.RemoveOwnedRelatedElement(chain), Throws.TypeOf<InvalidOperationException>());

            type.AssignOwnership(specialization, chain);
            type.AssignOwnership(specialization, otherChain);

            specialization.RemoveOwnedRelatedElement(chain);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(specialization.OwnedRelatedElement, Is.EqualTo([otherChain]));
                Assert.That(chain.OwningRelationship, Is.Null);
            }

            Assert.That(() => specialization.RemoveOwnedRelatedElement(chain), Throws.TypeOf<InvalidOperationException>());
        }
    }
}
