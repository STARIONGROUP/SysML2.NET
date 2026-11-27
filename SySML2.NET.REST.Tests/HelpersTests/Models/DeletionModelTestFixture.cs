// ------------------------------------------------------------------------------------------------
// <copyright file="DeletionModelTestFixture.cs" company="Starion Group S.A.">
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

    using SySML2.NET.REST.Tests.Helpers;
    using SySML2.NET.REST.Tests.Helpers.Models;

    using SysML2.NET.Core.DTO.Root.Namespaces;

    [TestFixture]
    public class DeletionModelTestFixture
    {
        [Test]
        public void VerifyDeletionModel()
        {
            Assert.That(() => new DeletionModel(null), Throws.ArgumentNullException);

            var root = new RootModel();
            var atStep2 = root.Builder.ToDtos();
            var model = new DeletionModel(root);
            var dtos = model.Builder.ToDtos();
            var changed = ModelBuilder.Changed(atStep2, dtos);

            Guid[] MembersOf(Guid ownerId) => [.. dtos.OfType<OwningMembership>().Where(membership => membership.OwningRelatedElement == ownerId).SelectMany(membership => membership.OwnedRelatedElement)];

            using (Assert.EnterMultipleScope())
            {
                Assert.That(dtos.Select(dto => dto.Id), Is.Unique);
                Assert.That(changed.Where(dto => atStep2.Any(before => before.Id == dto.Id)).Select(dto => dto.Id), Is.EqualTo((Guid[])[root.Root.Id]), "only the root package changes, to own the new package");
                Assert.That(MembersOf(root.Root.Id), Does.Contain(model.Package.Id));
                Assert.That(MembersOf(model.Package.Id), Is.EquivalentTo((Guid[])[model.Single.Id, model.Emptied.Id, model.Cascade.Id]));
                Assert.That(MembersOf(model.Single.Id), Is.Empty, "D1 deletes a package without content");
                Assert.That(MembersOf(model.Emptied.Id), Is.EquivalentTo((Guid[])[model.First.Id, model.Second.Id]));
                Assert.That(MembersOf(model.Cascade.Id), Is.EqualTo((Guid[])[model.Inner.Id]));
                Assert.That(MembersOf(model.Inner.Id), Is.EqualTo((Guid[])[model.Note.Id]), "D3 deletes an owner whose content is two levels deep");
                Assert.That(model.Note.Body, Is.Not.Empty);
            }
        }
    }
}
