// ------------------------------------------------------------------------------------------------
// <copyright file="OwningMembershipExtensionsTestFixture.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.Dal.Tests
{
    using System;

    using NUnit.Framework;

    using SysML2.NET.Core.POCO.Kernel.Packages;
    using SysML2.NET.Core.POCO.Root.Namespaces;
    using SysML2.NET.Dal;
    using SysML2.NET.Extensions;

    using DtoElements = SysML2.NET.Core.DTO.Root.Elements;
    using DtoNamespaces = SysML2.NET.Core.DTO.Root.Namespaces;

    [TestFixture]
    public class OwningMembershipExtensionsTestFixture
    {
        [Test]
        public void VerifyToDto()
        {
            var detached = new OwningMembership { Id = Guid.NewGuid() };

            Assert.That(() => detached.ToDto(), Throws.Nothing);

            var detachedDto = detached.ToDto();

            using (Assert.EnterMultipleScope())
            {
                Assert.That(((DtoNamespaces.IMembership)detachedDto).MemberElement, Is.EqualTo(Guid.Empty));
                Assert.That(((DtoNamespaces.IMembership)detachedDto).MemberName, Is.Null);
            }

            var owningNamespace = new Namespace { Id = Guid.NewGuid() };
            var membership = new OwningMembership { Id = Guid.NewGuid() };
            var member = new Package { Id = Guid.NewGuid(), DeclaredName = "Nested" };
            owningNamespace.AssignOwnership(membership, member);

            var withoutDerived = membership.ToDto();

            using (Assert.EnterMultipleScope())
            {
                Assert.That(((DtoNamespaces.IMembership)withoutDerived).MemberElement, Is.EqualTo(Guid.Empty));
                Assert.That(withoutDerived.OwnedRelatedElement, Is.EqualTo([member.Id]));
                Assert.That(withoutDerived.OwningRelatedElement, Is.EqualTo(owningNamespace.Id));
            }

            var withDerived = membership.ToDto(true);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(((DtoNamespaces.IMembership)withDerived).MemberElement, Is.EqualTo(member.Id));
                Assert.That(((DtoNamespaces.IMembership)withDerived).MemberName, Is.EqualTo("Nested"));
                Assert.That(((DtoElements.IRelationship)withDerived).Source, Is.EqualTo([owningNamespace.Id]));
                Assert.That(((DtoElements.IRelationship)withDerived).Target, Is.EqualTo([member.Id]));
            }
        }
    }
}
