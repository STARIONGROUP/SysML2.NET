// ------------------------------------------------------------------------------------------------
// <copyright file="LibraryModelTestFixture.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SySML2.NET.REST.Tests.HelpersTests.Models
{
    using System;
    using System.Linq;

    using NUnit.Framework;

    using SySML2.NET.REST.Tests.Helpers.Models;

    using SysML2.NET.Core.DTO.Kernel.Packages;
    using SysML2.NET.Core.DTO.Root.Namespaces;

    [TestFixture]
    public class LibraryModelTestFixture
    {
        [Test]
        public void VerifyLibraryModel()
        {
            var model = new LibraryModel();
            var dtos = model.Builder.ToDtos();
            var membership = dtos.OfType<OwningMembership>().Single();

            using (Assert.EnterMultipleScope())
            {
                Assert.That(dtos.Select(dto => dto.GetType()), Is.EquivalentTo((Type[])[typeof(LibraryPackage), typeof(OwningMembership), typeof(Package)]));
                Assert.That(dtos.Select(dto => dto.Id), Is.Unique);
                Assert.That(model.Root.DeclaredName, Is.EqualTo("UsedLibrary"));
                Assert.That(membership.OwningRelatedElement, Is.EqualTo(model.Root.Id));
                Assert.That(membership.OwnedRelatedElement, Is.EqualTo((Guid[])[model.Used.Id]));
                Assert.That(new LibraryModel().Used.Id, Is.EqualTo(model.Used.Id), "the ids are the same in every run");
                Assert.That(model.Used.Id, Is.Not.EqualTo(new RootModel().Nested.Id));
            }
        }
    }
}
