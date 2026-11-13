// ------------------------------------------------------------------------------------------------
// <copyright file="CoreModelTestFixture.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SySML2.NET.REST.Tests.HelpersTests.Models
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Text.Json;

    using NUnit.Framework;

    using SysML2.NET.Core.DTO.Root.Elements;
    using SysML2.NET.Serializer.Json;

    using SySML2.NET.REST.Tests.Helpers;
    using SySML2.NET.REST.Tests.Helpers.Models;

    using PocoFeature = SysML2.NET.Core.POCO.Core.Features.Feature;

    [TestFixture]
    public class CoreModelTestFixture
    {
        private static readonly string[] CoreMetaclasses =
        [
            "Type", "Classifier", "Feature", "Specialization", "Subclassification", "FeatureTyping", "Subsetting", "Redefinition",
            "ReferenceSubsetting", "CrossSubsetting", "Conjugation", "Disjoining", "Differencing", "Intersecting", "Unioning",
            "FeatureChaining", "FeatureInverting", "TypeFeaturing", "FeatureMembership", "EndFeatureMembership", "Multiplicity"
        ];

        [Test]
        public void VerifyCoreModel()
        {
            Assert.That(() => new CoreModel(null), Throws.ArgumentNullException);

            var root = new RootModel();
            var atStep2 = root.Builder.ToDtos();
            var model = new CoreModel(root);

            var dtos = model.Builder.ToDtos();
            var changed = ModelBuilder.Changed(atStep2, dtos);

            using var stream = new MemoryStream();
            new Serializer().SerializeRequest(ModelBuilder.ToCommitRequest(changed, "core model"), SerializationModeKind.JSON, false, stream, new JsonWriterOptions());
            var json = Encoding.UTF8.GetString(stream.ToArray());

            using (Assert.EnterMultipleScope())
            {
                Assert.That(dtos.Select(dto => dto.Id), Is.Unique);
                Assert.That(dtos.Select(dto => dto.GetType().Name).Distinct(), Is.SupersetOf(CoreMetaclasses));
                Assert.That(changed.Select(dto => dto.Id), Does.Contain(root.Root.Id), "the root package owns the new package");
                Assert.That(changed.Select(dto => dto.Id), Does.Not.Contain(root.Nested.Id));
                Assert.That(changed, Has.Count.EqualTo(dtos.Count - atStep2.Count + 1));
                Assert.That(json, Does.Not.Contain("00000000-0000-0000-0000-000000000000"));
                Assert.That(model.Builder.WithoutDerivedValues, Is.Empty);
                Assert.That(model.FirstEnd.IsEnd && model.SecondEnd.IsEnd, Is.True);
                Assert.That(dtos.OfType<IRelationship>().Where(relationship => relationship.IsImplied).Select(relationship => relationship.OwningRelatedElement), Is.EqualTo((Guid?[])[model.Builder.Find<PocoFeature>("core/f").Id]), "one implied relationship, owned by f");
                Assert.That(dtos.Where(dto => dto.IsImpliedIncluded).Select(dto => dto.DeclaredName), Is.EqualTo((string[])["f"]), "the owner of an implied relationship includes its implied relationships");
            }

            Assert.That(new CoreModel(new RootModel()).Builder.ToDtos().Select(dto => dto.Id), Is.EqualTo(dtos.Select(dto => dto.Id)));
        }
    }
}
