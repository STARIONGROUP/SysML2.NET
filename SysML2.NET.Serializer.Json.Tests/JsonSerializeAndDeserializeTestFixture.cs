// ------------------------------------------------------------------------------------------------
// <copyright file="JsonSerializeAndDeserializeTestFixture.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.Serializer.Json.Tests
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Text.Json;

    using NUnit.Framework;

    using SysML2.NET.Common;
    using SysML2.NET.Core.DTO.Core.Features;
    using SysML2.NET.Core.DTO.Core.Types;
    using SysML2.NET.Core.DTO.Kernel.Connectors;
    using SysML2.NET.Core.DTO.Root.Elements;
    using SysML2.NET.Core.DTO.Root.Namespaces;
    using SysML2.NET.Core.DTO.Systems.Parts;
    using SysML2.NET.Extensions.Core.DTO.Comparers;

    using SysML2.NET.Serializer.Json;

    /// <summary>
    /// Suite of tests for the <see cref="DeSerializer"/>
    /// </summary>
    [TestFixture]
    public class JsonSerializeAndDeserializeTestFixture
    {
        private static readonly bool[] IncludeDerivedPropertiesModes = [false, true];

        private DeSerializer deSerializer;
        private Serializer serializer;

        [SetUp]
        public void SetUp()
        {
            this.deSerializer = new DeSerializer();
            this.serializer = new Serializer();
        }

        [Test]
        public void Verify_that_Deserialize_Serialize_and_Deserialize_yields_equal_objects()
        {
            var fileName = Path.Combine(TestContext.CurrentContext.WorkDirectory, "Data", "projects.000e9890-6935-43e6-a5d7-5d7cac601f4c.commits.6d7ad9fd-6520-4ff2-885b-8c5c129e6c27.elements.json");
            using var stream = new FileStream(fileName, FileMode.Open, FileAccess.Read);

            var jsonFileData = this.deSerializer.DeSerialize(stream, SerializationModeKind.JSON, SerializationTargetKind.CORE, false).ToList();

            var jsonWriterOptions = new JsonWriterOptions { Indented = true };

            var targetStream = new MemoryStream();

            this.serializer.Serialize(jsonFileData, SerializationModeKind.JSON, false, targetStream, jsonWriterOptions);

            targetStream.Position = 0;

            var memoryStreamData = this.deSerializer.DeSerialize(targetStream, SerializationModeKind.JSON, SerializationTargetKind.CORE, false).ToList();

            Assert.That(jsonFileData, Has.Count.EqualTo(memoryStreamData.Count));

            var jsonFileById = jsonFileData.ToDictionary(x => x.Id);
            var memoryStreamById = memoryStreamData.ToDictionary(x => x.Id);

            Assert.That(jsonFileById.Keys, Is.EquivalentTo(memoryStreamById.Keys));

            using (Assert.EnterMultipleScope())
            {
                foreach (var id in jsonFileById.Keys)
                {
                    var left = jsonFileById[id];
                    var right = memoryStreamById[id];

                    Assert.That(
                        right.GetType(),
                        Is.EqualTo(left.GetType()),
                        $"Type mismatch for id {id}. Left={left.GetType().FullName}, Right={right.GetType().FullName}");

                    var comparer = ComparerProvider.Resolve((IData)left);

                    Assert.That(
                        comparer.Equals((IData)left, (IData)right),
                        Is.True,
                        $"Round-trip semantic mismatch for id {id} ({left.GetType().Name}).");
                }
            }
        }

        [Test]
        public void SerializeAndDeSerialize_WithPropertiesRedefinedByDerivedOnes_KeepsTheirValues()
        {
            var owningNamespace = Guid.NewGuid();
            var member = Guid.NewGuid();

            var owningMembership = new OwningMembership { Id = Guid.NewGuid() };
            ((IMembership)owningMembership).MemberElement = member;
            ((IMembership)owningMembership).MemberName = "Nested";
            ((IRelationship)owningMembership).Source = [owningNamespace];

            var featureMembership = new FeatureMembership { Id = Guid.NewGuid() };
            ((IMembership)featureMembership).MemberElement = member;

            var namespaceImport = new NamespaceImport { Id = Guid.NewGuid(), ImportedNamespace = Guid.NewGuid() };
            ((IRelationship)namespaceImport).Source = [owningNamespace];

            var connector = new Connector { Id = Guid.NewGuid() };
            ((IRelationship)connector).Target = [member, owningNamespace];

            var referenceSubsetting = new ReferenceSubsetting { Id = Guid.NewGuid(), ReferencedFeature = Guid.NewGuid() };
            ((ISubsetting)referenceSubsetting).SubsettingFeature = member;

            var partUsage = new PartUsage { Id = Guid.NewGuid() };
            ((IFeature)partUsage).IsVariable = true;

            List<IData> elements = [owningMembership, featureMembership, namespaceImport, connector, referenceSubsetting, partUsage];

            foreach (var includeDerivedProperties in IncludeDerivedPropertiesModes)
            {
                var stream = new MemoryStream();
                this.serializer.Serialize(elements, SerializationModeKind.JSON, includeDerivedProperties, stream, new JsonWriterOptions());
                stream.Position = 0;

                var roundTripped = this.deSerializer.DeSerialize(stream, SerializationModeKind.JSON, SerializationTargetKind.CORE, includeDerivedProperties).ToDictionary(element => element.Id);

                using (Assert.EnterMultipleScope())
                {
                    Assert.That(((IMembership)roundTripped[owningMembership.Id]).MemberElement, Is.EqualTo(member));
                    Assert.That(((IMembership)roundTripped[owningMembership.Id]).MemberName, Is.EqualTo("Nested"));
                    Assert.That(((IRelationship)roundTripped[owningMembership.Id]).Source, Is.EqualTo([owningNamespace]));
                    Assert.That(((IMembership)roundTripped[featureMembership.Id]).MemberElement, Is.EqualTo(member));
                    Assert.That(((IRelationship)roundTripped[namespaceImport.Id]).Source, Is.EqualTo([owningNamespace]));
                    Assert.That(((IRelationship)roundTripped[connector.Id]).Target, Is.EqualTo([member, owningNamespace]));
                    Assert.That(((ISubsetting)roundTripped[referenceSubsetting.Id]).SubsettingFeature, Is.EqualTo(member));
                    Assert.That(((IFeature)roundTripped[partUsage.Id]).IsVariable, Is.True);
                    Assert.That(elements.All(element => ComparerProvider.Resolve(element).Equals(element, (IData)roundTripped[element.Id])), Is.True);
                }
            }

            var renamed = new OwningMembership { Id = owningMembership.Id };
            ((IMembership)renamed).MemberElement = member;
            ((IMembership)renamed).MemberName = "Renamed";
            ((IRelationship)renamed).Source = [owningNamespace];

            Assert.That(ComparerProvider.Resolve(owningMembership).Equals(owningMembership, renamed), Is.False);
        }

        [Test]
        public void DeSerialize_WithRedefinedAndRedefiningKeysThatDisagree_KeepsTheRedefiningValue()
        {
            var membershipId = Guid.NewGuid();
            var ownedMember = Guid.NewGuid();
            var json = $$$"""
                [{"@id": "{{{membershipId}}}", "@type": "OwningMembership", "memberElement": {"@id": "{{{Guid.NewGuid()}}}"}, "ownedMemberElement": {"@id": "{{{ownedMember}}}"}}]
                """;

            using var stream = new MemoryStream(Encoding.UTF8.GetBytes(json));

            var membership = (IMembership)this.deSerializer.DeSerialize(stream, SerializationModeKind.JSON, SerializationTargetKind.CORE, true).Single();

            Assert.That(membership.MemberElement, Is.EqualTo(ownedMember));
        }
    }
}
