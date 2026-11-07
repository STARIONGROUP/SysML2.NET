// ------------------------------------------------------------------------------------------------
// <copyright file="ModelBuilderTestFixture.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SySML2.NET.REST.Tests.HelpersTests
{
    using System;
    using System.Linq;

    using NUnit.Framework;

    using SysML2.NET.Core.POCO.Kernel.Packages;
    using SysML2.NET.Dal;

    using SySML2.NET.REST.Tests.Helpers;

    using DtoElement = SysML2.NET.Core.DTO.Root.Elements.IElement;

    [TestFixture]
    public class ModelBuilderTestFixture
    {
        [Test]
        public void VerifyCreate()
        {
            var builder = new ModelBuilder("first");
            var package = builder.Create<Package>("Package", PackageExtensions.ToDto);
            var other = new ModelBuilder("second").Create<Package>("Package", PackageExtensions.ToDto);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(package.Id, Is.EqualTo(DeterministicGuid.Create("first/Package")));
                Assert.That(package.ElementId, Is.EqualTo(package.Id.ToString()));
                Assert.That(other.Id, Is.Not.EqualTo(package.Id));
                Assert.That(() => builder.Create<Package>("Package", PackageExtensions.ToDto), Throws.ArgumentException);
            }
        }

        [Test]
        public void VerifyToDtos()
        {
            var builder = new ModelBuilder("scope");
            var package = builder.Create<Package>("Package", PackageExtensions.ToDto);
            package.DeclaredName = "Package";

            var dtos = builder.ToDtos();

            using (Assert.EnterMultipleScope())
            {
                Assert.That(dtos, Has.Count.EqualTo(1));
                Assert.That(dtos[0], Is.TypeOf<SysML2.NET.Core.DTO.Kernel.Packages.Package>());
                Assert.That(dtos[0].Id, Is.EqualTo(package.Id));
                Assert.That(dtos[0].DeclaredName, Is.EqualTo("Package"));
            }

            var failing = new ModelBuilder("scope");
            failing.Create<Package>("Broken", (_, _) => throw new NotSupportedException("no conversion"));

            Assert.That(failing.ToDtos, Throws.InvalidOperationException
                .With.Message.Contains("scope/Broken").And.Message.Contains(nameof(Package))
                .And.InnerException.TypeOf<NotSupportedException>());
        }

        [Test]
        public void VerifyToCommitRequest()
        {
            var builder = new ModelBuilder("scope");
            builder.Create<Package>("First", PackageExtensions.ToDto);
            builder.Create<Package>("Second", PackageExtensions.ToDto);
            var dtos = builder.ToDtos();

            var request = ModelBuilder.ToCommitRequest(dtos, "description");
            var empty = ModelBuilder.ToCommitRequest(Enumerable.Empty<DtoElement>(), "empty");

            using (Assert.EnterMultipleScope())
            {
                Assert.That(request.Description, Is.EqualTo("description"));
                Assert.That(request.Change, Has.Count.EqualTo(2));
                Assert.That(request.Change.Select(change => change.Identity.Id), Is.EqualTo(dtos.Select(dto => dto.Id)));
                Assert.That(request.Change[0].Payload, Is.SameAs(dtos[0]));
                Assert.That(request.Change[1].Payload, Is.SameAs(dtos[1]));
                Assert.That(empty.Change, Is.Empty);
            }
        }
    }
}
