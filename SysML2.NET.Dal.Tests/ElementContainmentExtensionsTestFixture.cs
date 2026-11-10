// ------------------------------------------------------------------------------------------------
// <copyright file="ElementContainmentExtensionsTestFixture.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.Dal.Tests
{
    using System;
    using System.Collections.Concurrent;

    using NUnit.Framework;

    using SysML2.NET.Dal;

    [TestFixture]
    public class ElementContainmentExtensionsTestFixture
    {
        [Test]
        public void VerifyUpdateContainment()
        {
            var cache = new ConcurrentDictionary<Guid, Lazy<Core.POCO.Root.Elements.IElement>>();
            var package = Cached(new Core.POCO.Kernel.Packages.Package { Id = Guid.NewGuid() }, cache);
            var otherPackage = Cached(new Core.POCO.Kernel.Packages.Package { Id = Guid.NewGuid() }, cache);
            var firstMembership = Cached(new Core.POCO.Root.Namespaces.OwningMembership { Id = Guid.NewGuid() }, cache);
            var secondMembership = Cached(new Core.POCO.Root.Namespaces.OwningMembership { Id = Guid.NewGuid() }, cache);
            var member = Cached(new Core.POCO.Kernel.Packages.Package { Id = Guid.NewGuid() }, cache);

            var packageDto = new Core.DTO.Kernel.Packages.Package { Id = package.Id };
            var firstMembershipDto = new Core.DTO.Root.Namespaces.OwningMembership { Id = firstMembership.Id };
            var secondMembershipDto = new Core.DTO.Root.Namespaces.OwningMembership { Id = secondMembership.Id };

            Assert.That(() => ((Core.POCO.Root.Elements.IElement)null).UpdateContainment(packageDto, cache), Throws.TypeOf<ArgumentNullException>());
            Assert.That(() => package.UpdateContainment(null, cache), Throws.TypeOf<ArgumentNullException>());
            Assert.That(() => package.UpdateContainment(packageDto, null), Throws.TypeOf<ArgumentNullException>());

            package.UpdateContainment(packageDto, cache);

            Assert.That(package.OwnedRelationship, Has.Count.EqualTo(0));

            packageDto.OwnedRelationship = [firstMembership.Id, Guid.NewGuid(), secondMembership.Id];
            firstMembershipDto.OwnedRelatedElement = [member.Id];

            package.UpdateContainment(packageDto, cache);
            firstMembership.UpdateContainment(firstMembershipDto, cache);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(package.OwnedRelationship, Is.EqualTo([firstMembership, secondMembership]));
                Assert.That(firstMembership.OwningRelatedElement, Is.SameAs(package));
                Assert.That(member.OwningRelationship, Is.SameAs(firstMembership));
            }

            packageDto.OwnedRelationship = [secondMembership.Id, firstMembership.Id];

            package.UpdateContainment(packageDto, cache);

            Assert.That(package.OwnedRelationship, Is.EqualTo([secondMembership, firstMembership]));

            secondMembershipDto.OwningRelatedElement = otherPackage.Id;

            secondMembership.UpdateContainment(secondMembershipDto, cache);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(package.OwnedRelationship, Is.EqualTo([firstMembership]));
                Assert.That(otherPackage.OwnedRelationship, Is.EqualTo([secondMembership]));
            }

            firstMembershipDto.OwningRelatedElement = null;

            firstMembership.UpdateContainment(firstMembershipDto, cache);

            Assert.That(firstMembership.OwningRelatedElement, Is.SameAs(package));

            cache.TryRemove(package.Id, out _);

            firstMembership.UpdateContainment(firstMembershipDto, cache);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(firstMembership.OwningRelatedElement, Is.Null);
                Assert.That(package.OwnedRelationship, Has.Count.EqualTo(0));
                Assert.That(firstMembership.OwnedRelatedElement, Is.EqualTo([member]));
            }

            var otherPackageDto = new Core.DTO.Kernel.Packages.Package { Id = otherPackage.Id, OwnedRelationship = [member.Id] };

            Assert.That(() => otherPackage.UpdateContainment(otherPackageDto, cache), Throws.TypeOf<InvalidOperationException>());
        }

        private static T Cached<T>(T poco, ConcurrentDictionary<Guid, Lazy<Core.POCO.Root.Elements.IElement>> cache) where T : Core.POCO.Root.Elements.IElement
        {
            cache[poco.Id] = new Lazy<Core.POCO.Root.Elements.IElement>(() => poco);
            return poco;
        }
    }
}
