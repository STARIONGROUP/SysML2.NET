// ------------------------------------------------------------------------------------------------
// <copyright file="DtoMetadataTestFixture.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SySML2.NET.REST.Tests.HelpersTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using NUnit.Framework;

    using SysML2.NET.Core.DTO.Root.Namespaces;
    using SysML2.NET.Extensions.Core.DTO.Comparers;

    using SySML2.NET.REST.Tests.Helpers;

    [TestFixture]
    public class DtoMetadataTestFixture
    {
        [Test]
        public void VerifyConcreteClasses()
        {
            Assert.That(() => DtoMetadata.ConcreteClasses.Select(ComparerProvider.Resolve).ToList(), Throws.Nothing, "every concrete DTO class has a generated comparer");

            using (Assert.EnterMultipleScope())
            {
                Assert.That(DtoMetadata.ConcreteClasses, Does.Contain(typeof(OwningMembership)));
                Assert.That(DtoMetadata.ConcreteClasses, Has.None.Matches<Type>(type => type.IsAbstract || type.IsInterface));
            }
        }

        [Test]
        public void VerifyDeclaredProperties()
        {
            var names = DtoMetadata.DeclaredProperties.Select(property => property.Name).ToList();

            using (Assert.EnterMultipleScope())
            {
                Assert.That(names, Is.Unique);
                Assert.That(names, Does.Contain("Membership.MemberElement"));
                Assert.That(names, Does.Contain("Element.DeclaredName"));
                Assert.That(names, Does.Not.Contain("Element.name"));
                Assert.That(names, Does.Not.Contain("OwningMembership.ownedMemberElement"));
            }
        }

        [Test]
        public void VerifyDerivedPropertyNames()
        {
            using (Assert.EnterMultipleScope())
            {
                Assert.That(DtoMetadata.DerivedPropertyNames, Does.Contain("owner").And.Contain("qualifiedName").And.Contain("ownedMemberElement"));
                Assert.That(DtoMetadata.DerivedPropertyNames, Does.Not.Contain("declaredName").And.Not.Contain("alias").And.Not.Contain("aliasIds"));
                Assert.That(DtoMetadata.DerivedPropertyNames, Has.None.Matches<string>(name => char.IsUpper(name[0])));
            }
        }

        [Test]
        public void VerifyPropertiesOf()
        {
            var names = DtoMetadata.PropertiesOf(typeof(OwningMembership)).Select(property => property.Name).ToList();

            using (Assert.EnterMultipleScope())
            {
                Assert.That(names, Does.Contain("Membership.MemberElement"));
                Assert.That(names, Does.Contain("Relationship.OwnedRelatedElement"));
                Assert.That(names, Does.Not.Contain("Dependency.Client"));
            }
        }

        [Test]
        public void VerifyValueEquals()
        {
            using (Assert.EnterMultipleScope())
            {
                Assert.That(DtoMetadata.ValueEquals((List<int>)[1, 2], (int[])[1, 2]), Is.True);
                Assert.That(DtoMetadata.ValueEquals((List<int>)[1, 2], (int[])[2, 1]), Is.False);
                Assert.That(DtoMetadata.ValueEquals("abc", "abc"), Is.True);
                Assert.That(DtoMetadata.ValueEquals("abc", "abd"), Is.False);
                Assert.That(DtoMetadata.ValueEquals(null, null), Is.True);
                Assert.That(DtoMetadata.ValueEquals(null, false), Is.False);
            }
        }
    }
}
