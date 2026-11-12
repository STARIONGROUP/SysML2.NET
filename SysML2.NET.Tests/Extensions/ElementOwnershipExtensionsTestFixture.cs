// ------------------------------------------------------------------------------------------------
// <copyright file="ElementOwnershipExtensionsTestFixture.cs" company="Starion Group S.A.">
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

    using SysML2.NET.Core.POCO.Core.Types;
    using SysML2.NET.Core.POCO.Kernel.Packages;
    using SysML2.NET.Core.POCO.Root.Elements;
    using SysML2.NET.Core.POCO.Root.Namespaces;
    using SysML2.NET.Extensions;

    [TestFixture]
    public class ElementOwnershipExtensionsTestFixture
    {
        [Test]
        public void VerifyMoveOwnedRelationship()
        {
            var package = new Package();
            var otherPackage = new Package();
            var first = new OwningMembership();
            var second = new OwningMembership();
            var third = new OwningMembership();
            var moved = new OwningMembership();
            var movedMember = new Package();

            Assert.That(() => ((IElement)null).MoveOwnedRelationship(first, 0), Throws.TypeOf<ArgumentNullException>());
            Assert.That(() => package.MoveOwnedRelationship(null, 0), Throws.TypeOf<ArgumentNullException>());
            Assert.That(() => package.MoveOwnedRelationship(new FeatureMembership(), 0), Throws.TypeOf<InvalidOperationException>());

            package.AssignOwnership(first, new Package());
            package.AssignOwnership(second, new Package());
            package.AssignOwnership(third, new Package());

            Assert.That(() => package.MoveOwnedRelationship(first, 3), Throws.TypeOf<ArgumentOutOfRangeException>());

            package.MoveOwnedRelationship(third, 0);

            Assert.That(package.OwnedRelationship, Is.EqualTo([third, first, second]));

            package.MoveOwnedRelationship(third, 2);

            Assert.That(package.OwnedRelationship, Is.EqualTo([first, second, third]));

            otherPackage.AssignOwnership(moved, movedMember);

            Assert.That(() => package.MoveOwnedRelationship(moved, 4), Throws.TypeOf<ArgumentOutOfRangeException>());
            Assert.That(otherPackage.OwnedRelationship, Is.EqualTo([moved]));

            package.MoveOwnedRelationship(moved, 1);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(package.OwnedRelationship, Is.EqualTo([first, moved, second, third]));
                Assert.That(otherPackage.OwnedRelationship, Has.Count.EqualTo(0));
                Assert.That(moved.OwningRelatedElement, Is.SameAs(package));
                Assert.That(moved.OwnedRelatedElement, Is.EqualTo([movedMember]));
            }

            Assert.That(() => movedMember.MoveOwnedRelationship(moved, 0), Throws.TypeOf<InvalidOperationException>().With.Message.Contains("Containment cycle"));
        }

        [Test]
        public void VerifySetOwnedRelationships()
        {
            var package = new Package();
            var otherPackage = new Package();
            var first = new OwningMembership();
            var second = new OwningMembership();
            var taken = new OwningMembership();
            var takenMember = new Package();

            Assert.That(() => ((IElement)null).SetOwnedRelationships([]), Throws.TypeOf<ArgumentNullException>());
            Assert.That(() => package.SetOwnedRelationships(null), Throws.TypeOf<ArgumentNullException>());
            Assert.That(() => package.SetOwnedRelationships([first, null]), Throws.TypeOf<ArgumentNullException>());

            package.AssignOwnership(first, new Package());
            package.AssignOwnership(second, new Package());
            otherPackage.AssignOwnership(taken, takenMember);

            Assert.That(() => package.SetOwnedRelationships([first, first]), Throws.TypeOf<ArgumentException>());
            Assert.That(() => package.SetOwnedRelationships([second, new FeatureMembership()]), Throws.TypeOf<InvalidOperationException>());
            Assert.That(() => takenMember.SetOwnedRelationships([taken]), Throws.TypeOf<InvalidOperationException>().With.Message.Contains("Containment cycle"));
            Assert.That(package.OwnedRelationship, Is.EqualTo([first, second]));

            package.SetOwnedRelationships([second, taken]);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(package.OwnedRelationship, Is.EqualTo([second, taken]));
                Assert.That(first.OwningRelatedElement, Is.Null);
                Assert.That(otherPackage.OwnedRelationship, Has.Count.EqualTo(0));
                Assert.That(taken.OwningRelatedElement, Is.SameAs(package));
                Assert.That(taken.OwnedRelatedElement, Is.EqualTo([takenMember]));
            }

            package.SetOwnedRelationships([]);

            Assert.That(package.OwnedRelationship, Has.Count.EqualTo(0));
        }

        [Test]
        public void VerifyRemoveOwnedRelationship()
        {
            var package = new Package();
            var other = new Package();
            var membership = new OwningMembership();
            var member = new Package();

            Assert.That(() => ((IElement)null).RemoveOwnedRelationship(membership), Throws.TypeOf<ArgumentNullException>());
            Assert.That(() => package.RemoveOwnedRelationship(null), Throws.TypeOf<ArgumentNullException>());
            Assert.That(() => package.RemoveOwnedRelationship(membership), Throws.TypeOf<InvalidOperationException>());

            package.AssignOwnership(membership, member);

            Assert.That(() => other.RemoveOwnedRelationship(membership), Throws.TypeOf<InvalidOperationException>());

            package.RemoveOwnedRelationship(membership);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(package.OwnedRelationship, Has.Count.EqualTo(0));
                Assert.That(package.ownedMember, Has.Count.EqualTo(0));
                Assert.That(membership.OwningRelatedElement, Is.Null);
                Assert.That(membership.OwnedRelatedElement, Is.EqualTo([member]));
                Assert.That(member.OwningRelationship, Is.SameAs(membership));
            }
        }
    }
}
