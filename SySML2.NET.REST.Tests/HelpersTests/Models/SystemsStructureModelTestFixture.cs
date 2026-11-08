// ------------------------------------------------------------------------------------------------
// <copyright file="SystemsStructureModelTestFixture.cs" company="Starion Group S.A.">
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

    using SysML2.NET.Core.DTO.Core.Features;
    using SysML2.NET.Core.DTO.Root.Elements;
    using SysML2.NET.Core.DTO.Systems.DefinitionAndUsage;
    using SysML2.NET.Core.DTO.Systems.Parts;
    using SysML2.NET.Core.POCO.Kernel.Expressions;
    using SysML2.NET.Serializer.Json;

    using SySML2.NET.REST.Tests.Helpers;
    using SySML2.NET.REST.Tests.Helpers.Models;

    [TestFixture]
    public class SystemsStructureModelTestFixture
    {
        private static readonly string[] StructureMetaclasses =
        [
            "Definition", "Usage", "ReferenceUsage", "VariantMembership", "AttributeDefinition", "AttributeUsage", "EnumerationDefinition",
            "EnumerationUsage", "OccurrenceDefinition", "OccurrenceUsage", "EventOccurrenceUsage", "ItemDefinition", "ItemUsage",
            "PartDefinition", "PartUsage", "PortDefinition", "PortUsage", "ConjugatedPortDefinition", "ConjugatedPortTyping",
            "PortConjugation", "ConnectionDefinition", "ConnectionUsage", "BindingConnectorAsUsage", "SuccessionAsUsage",
            "InterfaceDefinition", "InterfaceUsage", "AllocationDefinition", "AllocationUsage", "FlowDefinition", "FlowUsage",
            "SuccessionFlowUsage"
        ];

        [Test]
        public void VerifySystemsStructureModel()
        {
            Assert.That(() => new SystemsStructureModel(null), Throws.ArgumentNullException);

            var root = new RootModel();
            var core = new CoreModel(root);
            var kernel = new KernelModel(root, core);
            _ = new KerMLUpdateModel(root, core, kernel);

            var atStep5 = root.Builder.ToDtos();
            var model = new SystemsStructureModel(root);

            var dtos = model.Builder.ToDtos();
            var changed = ModelBuilder.Changed(atStep5, dtos);

            using var stream = new MemoryStream();
            new Serializer().SerializeRequest(ModelBuilder.ToCommitRequest(changed, "systems structure"), SerializationModeKind.JSON, false, stream, new JsonWriterOptions());
            var json = Encoding.UTF8.GetString(stream.ToArray());

            using (Assert.EnterMultipleScope())
            {
                Assert.That(dtos.Select(dto => dto.Id), Is.Unique);
                Assert.That(dtos.Select(dto => dto.GetType().Name).Distinct(), Is.SupersetOf(StructureMetaclasses));
                Assert.That(changed.Select(dto => dto.Id), Does.Contain(root.Root.Id), "the root package owns the new package");
                Assert.That(changed.Where(dto => atStep5.Any(before => before.Id == dto.Id)).Select(dto => dto.Id), Is.EquivalentTo((Guid[])[root.Root.Id, root.Builder.Find<LiteralRational>("kernel/rational").Id]), "the root package, and the rational literal that its comparer reports as changed (#426)");
                Assert.That(json, Does.Not.Contain("00000000-0000-0000-0000-000000000000"));
                Assert.That(model.Builder.WithoutDerivedValues, Is.EquivalentTo((string[])["kernel/sum", "kernel/index", "kernel/select", "kernel/collect", "kernel/chain"]), "no structure element loses its derived values");
            }

            var variation = dtos.OfType<PartUsage>().Single(part => part.DeclaredName == "vu");
            var variant = dtos.OfType<PartUsage>().Single(part => part.DeclaredName == "vu1");
            var ownedByVariation = dtos.OfType<IRelationship>().Where(relationship => relationship.OwningRelatedElement == variation.Id).ToList();

            using (Assert.EnterMultipleScope())
            {
                Assert.That(variation.IsVariation, Is.True);
                Assert.That(variation.IsAbstract, Is.True, "a variation usage must be abstract");
                Assert.That(ownedByVariation, Has.Count.EqualTo(1).And.All.TypeOf<VariantMembership>(), "a variation usage owns only variant memberships");
                Assert.That(ownedByVariation[0].OwnedRelatedElement, Is.EqualTo((Guid[])[variant.Id]));
                Assert.That(variant.IsVariation, Is.False);
                Assert.That(dtos.OfType<Subsetting>().Where(subsetting => subsetting.OwningRelatedElement == variant.Id).Select(subsetting => subsetting.SubsettedFeature), Is.EqualTo((Guid[])[variation.Id]), "a variant specializes its variation usage");
            }
        }
    }
}
