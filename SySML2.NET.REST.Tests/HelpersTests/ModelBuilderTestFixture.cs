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
        public void VerifyBeginStep()
        {
            var builder = new ModelBuilder("first");
            var first = builder.Create<Package>("Package", PackageExtensions.ToDto);

            builder.BeginStep("second");

            var second = builder.Create<Package>("Package", PackageExtensions.ToDto);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(second.Id, Is.EqualTo(DeterministicGuid.Create("second/Package")));
                Assert.That(second.Id, Is.Not.EqualTo(first.Id));
                Assert.That(builder.ToDtos().Select(dto => dto.Id), Is.EqualTo((Guid[])[first.Id, second.Id]));
                Assert.That(() => builder.Create<Package>("Package", PackageExtensions.ToDto), Throws.ArgumentException.With.Message.Contains("second"));
            }
        }

        [Test]
        public void VerifyChanged()
        {
            var builder = new ModelBuilder("scope");
            var owner = builder.Create<Package>("Owner", PackageExtensions.ToDto);
            var untouched = builder.Create<Package>("Untouched", PackageExtensions.ToDto);
            owner.DeclaredName = "Owner";

            var before = builder.ToDtos();

            Assert.That(ModelBuilder.Changed([], before), Is.EqualTo(before));
            Assert.That(ModelBuilder.Changed(before, builder.ToDtos()), Is.Empty);

            owner.DeclaredName = "Renamed";
            var added = builder.Create<Package>("Added", PackageExtensions.ToDto);

            var after = builder.ToDtos();
            var changed = ModelBuilder.Changed(before, after);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(changed.Select(dto => dto.Id), Is.EqualTo((Guid[])[owner.Id, added.Id]));
                Assert.That(changed[0], Is.SameAs(after[0]));
                Assert.That(changed.Select(dto => dto.Id), Does.Not.Contain(untouched.Id));
            }
        }

        [Test]
        public void VerifyCopy()
        {
            Assert.That(() => ModelBuilder.Copy<DtoElement>(null), Throws.ArgumentNullException);

            var builder = new ModelBuilder("scope");
            var package = builder.Create<Package>("Package", PackageExtensions.ToDto);
            package.DeclaredName = "Original";
            package.AliasIds = ["first"];
            var dto = builder.ToDtos()[0];

            var copy = ModelBuilder.Copy(dto);
            copy.DeclaredName = "Copy";
            copy.AliasIds.Add("second");

            using (Assert.EnterMultipleScope())
            {
                Assert.That(copy, Is.Not.SameAs(dto));
                Assert.That(copy, Is.TypeOf(dto.GetType()));
                Assert.That(copy.Id, Is.EqualTo(dto.Id));
                Assert.That(dto.DeclaredName, Is.EqualTo("Original"));
                Assert.That(dto.AliasIds, Is.EqualTo((string[])["first"]));
            }
        }

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

            package.AliasIds = ["first"];

            var dtos = builder.ToDtos();

            package.AliasIds.Add("second");

            using (Assert.EnterMultipleScope())
            {
                Assert.That(dtos[0].AliasIds, Is.EqualTo((string[])["first"]), "a DTO keeps the aliases it was converted with");
                Assert.That(dtos, Has.Count.EqualTo(1));
                Assert.That(dtos[0], Is.TypeOf<SysML2.NET.Core.DTO.Kernel.Packages.Package>());
                Assert.That(dtos[0].Id, Is.EqualTo(package.Id));
                Assert.That(dtos[0].DeclaredName, Is.EqualTo("Package"));
            }

            var failing = new ModelBuilder("scope");
            failing.Create<Package>("Broken", (_, _) => throw new NotSupportedException("no conversion"));

            var partial = new ModelBuilder("scope");
            var withoutDerived = partial.Create<Package>("WithoutDerived", (poco, includeDerived) => includeDerived ? throw new NullReferenceException("no library") : PackageExtensions.ToDto(poco, false));
            partial.Create<Package>("Complete", PackageExtensions.ToDto);

            var partialDtos = partial.ToDtos();

            using (Assert.EnterMultipleScope())
            {
                Assert.That(builder.WithoutDerivedValues, Is.Empty);
                Assert.That(partialDtos.Select(dto => dto.Id), Does.Contain(withoutDerived.Id));
                Assert.That(partial.WithoutDerivedValues, Is.EqualTo((string[])["scope/WithoutDerived"]));
            }

            Assert.That(failing.ToDtos, Throws.InvalidOperationException
                .With.Message.Contains("scope/Broken").And.Message.Contains(nameof(Package))
                .And.InnerException.TypeOf<NotSupportedException>());
        }

        [Test]
        public void VerifyElements()
        {
            var builder = new ModelBuilder("scope");
            var first = builder.Create<Package>("First", PackageExtensions.ToDto);
            var second = builder.Create<Package>("Second", PackageExtensions.ToDto);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(builder.Elements.Select(entry => entry.Name), Is.EqualTo((string[])["scope/First", "scope/Second"]));
                Assert.That(builder.Elements[0].Element, Is.SameAs(first));
                Assert.That(builder.Elements[1].Element, Is.SameAs(second));
                Assert.That(new ModelBuilder("empty").Elements, Is.Empty);
            }
        }

        [Test]
        public void VerifyFind()
        {
            var builder = new ModelBuilder("scope");
            var package = builder.Create<Package>("Package", PackageExtensions.ToDto);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(builder.Find<Package>("scope/Package"), Is.SameAs(package));
                Assert.That(() => builder.Find<Package>("scope/Missing"), Throws.ArgumentException.With.Message.Contains("scope/Missing"));
                Assert.That(() => builder.Find<SysML2.NET.Core.POCO.Root.Namespaces.Namespace>("scope/Package"), Throws.ArgumentException);
            }
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
