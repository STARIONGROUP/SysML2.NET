// ------------------------------------------------------------------------------------------------
// <copyright file="RootModelTestFixture.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SySML2.NET.REST.Tests.HelpersTests.Models
{
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Text.Json;

    using NUnit.Framework;

    using SysML2.NET.Core.DTO.Kernel.Packages;
    using SysML2.NET.Core.DTO.Root.Namespaces;
    using SysML2.NET.Serializer.Json;

    using SySML2.NET.REST.Tests.Helpers;
    using SySML2.NET.REST.Tests.Helpers.Models;

    [TestFixture]
    public class RootModelTestFixture
    {
        [Test]
        public void VerifyRootModel()
        {
            var model = new RootModel();

            var dtos = model.Builder.ToDtos();

            var owningMemberships = dtos.OfType<IOwningMembership>().ToList();
            var commitRequest = ModelBuilder.ToCommitRequest(dtos, "root model");

            using var stream = new MemoryStream();
            new Serializer().SerializeRequest(commitRequest, SerializationModeKind.JSON, false, stream, new JsonWriterOptions());
            var json = Encoding.UTF8.GetString(stream.ToArray());

            using (Assert.EnterMultipleScope())
            {
                Assert.That(dtos.Select(dto => dto.Id), Is.Unique);
                Assert.That(dtos.OfType<Package>().Select(package => package.DeclaredName), Is.EquivalentTo(["CoverageModel", "Nested"]));
                Assert.That(owningMemberships, Has.Count.EqualTo(10));
                Assert.That(owningMemberships.Select(membership => ((IMembership)membership).MemberElement), Is.EquivalentTo(owningMemberships.Select(membership => membership.OwnedRelatedElement[0])));
                Assert.That(commitRequest.Change, Has.Count.EqualTo(dtos.Count));
                Assert.That(json, Does.Not.Contain("00000000-0000-0000-0000-000000000000"));
            }

            Assert.That(new RootModel().Builder.ToDtos().Select(dto => dto.Id), Is.EqualTo(dtos.Select(dto => dto.Id)));
        }
    }
}
