// ------------------------------------------------------------------------------------------------
// <copyright file="ContainerListTestFixture.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.Tests.Collections
{
    using System;

    using NUnit.Framework;

    using SysML2.NET.Core.POCO.Kernel.Packages;
    using SysML2.NET.Core.POCO.Root.Elements;
    using SysML2.NET.Core.POCO.Root.Namespaces;

    [TestFixture]
    public class ContainerListTestFixture
    {
        private Package owner;
        private Package otherOwner;
        private OwningMembership first;
        private OwningMembership second;
        private OwningMembership third;

        [SetUp]
        public void SetUp()
        {
            this.owner = new Package();
            this.otherOwner = new Package();
            this.first = new OwningMembership();
            this.second = new OwningMembership();
            this.third = new OwningMembership();
        }

        [Test]
        public void VerifyClearItems()
        {
            var ownedRelationships = ((IContainedElement)this.owner).OwnedRelationship;

            ownedRelationships.Clear();

            Assert.That(this.owner.OwnedRelationship, Has.Count.EqualTo(0));

            ownedRelationships.Add(this.first);
            ownedRelationships.Add(this.second);

            ownedRelationships.Clear();

            using (Assert.EnterMultipleScope())
            {
                Assert.That(this.owner.OwnedRelationship, Has.Count.EqualTo(0));
                Assert.That(this.first.OwningRelatedElement, Is.Null);
                Assert.That(this.second.OwningRelatedElement, Is.Null);
            }
        }

        [Test]
        public void VerifyContains()
        {
            var ownedRelationships = ((IContainedElement)this.owner).OwnedRelationship;

            Assert.That(ownedRelationships.Contains(this.first), Is.False);

            ownedRelationships.Add(this.first);
            ownedRelationships.Add(this.second);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(ownedRelationships.Contains(this.first), Is.True);
                Assert.That(ownedRelationships.Contains(this.third), Is.False);
            }

            ownedRelationships.Remove(this.first);

            Assert.That(ownedRelationships.Contains(this.first), Is.False);
            Assert.That(() => ownedRelationships.Add(this.first), Throws.Nothing);

            ownedRelationships[1] = this.third;

            using (Assert.EnterMultipleScope())
            {
                Assert.That(ownedRelationships.Contains(this.first), Is.False);
                Assert.That(ownedRelationships.Contains(this.third), Is.True);
                Assert.That(() => ownedRelationships.Add(this.first), Throws.Nothing);
            }

            ownedRelationships.Clear();

            using (Assert.EnterMultipleScope())
            {
                Assert.That(ownedRelationships.Contains(this.third), Is.False);
                Assert.That(() => ownedRelationships.Add(this.third), Throws.Nothing);
            }
        }

        [Test]
        public void VerifyIndexOf()
        {
            var ownedRelationships = ((IContainedElement)this.owner).OwnedRelationship;
            ownedRelationships.Add(this.first);
            ownedRelationships.Add(this.second);

            Assert.That(ownedRelationships.IndexOf(this.third), Is.EqualTo(-1));

            ownedRelationships.Move(0, 1);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(ownedRelationships.IndexOf(this.first), Is.EqualTo(1));
                Assert.That(ownedRelationships.IndexOf(this.second), Is.EqualTo(0));
            }
        }

        [Test]
        public void VerifyInsertItem()
        {
            var ownedRelationships = ((IContainedElement)this.owner).OwnedRelationship;

            Assert.That(() => ownedRelationships.Add(null), Throws.TypeOf<ArgumentNullException>());

            ownedRelationships.Add(this.first);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(() => ownedRelationships.Add(this.first), Throws.TypeOf<InvalidOperationException>());
                Assert.That(() => ((IContainedElement)this.otherOwner).OwnedRelationship.Add(this.first), Throws.TypeOf<InvalidOperationException>());
                Assert.That(this.owner.OwnedRelationship, Is.EqualTo([this.first]));
                Assert.That(this.first.OwningRelatedElement, Is.SameAs(this.owner));
            }

            ownedRelationships.Insert(0, this.second);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(this.owner.OwnedRelationship, Is.EqualTo([this.second, this.first]));
                Assert.That(this.second.OwningRelatedElement, Is.SameAs(this.owner));
            }
        }

        [Test]
        public void VerifyMove()
        {
            var ownedRelationships = ((IContainedElement)this.owner).OwnedRelationship;
            ownedRelationships.Add(this.first);
            ownedRelationships.Add(this.second);
            ownedRelationships.Add(this.third);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(() => ownedRelationships.Move(-1, 0), Throws.TypeOf<ArgumentOutOfRangeException>());
                Assert.That(() => ownedRelationships.Move(0, 3), Throws.TypeOf<ArgumentOutOfRangeException>());
                Assert.That(this.owner.OwnedRelationship, Is.EqualTo([this.first, this.second, this.third]));
            }

            ownedRelationships.Move(0, 2);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(this.owner.OwnedRelationship, Is.EqualTo([this.second, this.third, this.first]));
                Assert.That(this.first.OwningRelatedElement, Is.SameAs(this.owner));
            }

            ownedRelationships.Move(2, 1);

            Assert.That(this.owner.OwnedRelationship, Is.EqualTo([this.second, this.first, this.third]));
        }

        [Test]
        public void VerifySetItem()
        {
            var ownedRelationships = ((IContainedElement)this.owner).OwnedRelationship;
            ownedRelationships.Add(this.first);
            ownedRelationships.Add(this.second);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(() => ownedRelationships[0] = null, Throws.TypeOf<ArgumentNullException>());
                Assert.That(() => ownedRelationships[0] = this.second, Throws.TypeOf<InvalidOperationException>());
                Assert.That(() => ownedRelationships[0] = this.first, Throws.Nothing);
                Assert.That(this.owner.OwnedRelationship, Is.EqualTo([this.first, this.second]));
            }

            ownedRelationships[0] = this.third;

            using (Assert.EnterMultipleScope())
            {
                Assert.That(this.owner.OwnedRelationship, Is.EqualTo([this.third, this.second]));
                Assert.That(this.first.OwningRelatedElement, Is.Null);
                Assert.That(this.third.OwningRelatedElement, Is.SameAs(this.owner));
            }
        }
    }
}
