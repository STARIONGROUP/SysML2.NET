// ------------------------------------------------------------------------------------------------
// <copyright file="SerializerTestFixture.cs" company="Starion Group S.A.">
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
    using System.Text;
    using System.Text.Json;
    using System.Threading;
    using System.Threading.Tasks;

    using NUnit.Framework;

    using SysML2.NET.Core.DTO;
    using SysML2.NET.Core.DTO.Root.Elements;
    using SysML2.NET.Core.DTO.Systems.Parts;
    using SysML2.NET.Serializer.Json;

    [TestFixture]
    public class SerializerTestFixture
    {
        private ISerializer serializer;

        private PartDefinition partDefinition;

        private PartDefinition partDefinitionWithNullProperties;

        [SetUp]
        public void SetUp()
        {
            this.serializer = new Serializer();

            this.CreateTestData();
        }

        private void CreateTestData()
        {
            this.partDefinition = new PartDefinition
            {
                Id = Guid.NewGuid(),
                IsIndividual = true,
                IsVariation = false,
                IsAbstract = false,
                IsSufficient = false,
                AliasIds = new List<string> { "PartDefinition:Alias_1", "PartDefinition:Alias_2" },
                DeclaredName = "PartDefinition:DeclaredName",
                OwnedRelationship = new List<Guid> { Guid.NewGuid() },
                OwningRelationship = Guid.NewGuid(),
                DeclaredShortName = "PartDefinition:DeclaredShortName"
            };

            this.partDefinitionWithNullProperties = new PartDefinition
            {
                Id = Guid.NewGuid(),
                IsIndividual = true,
                IsVariation = false,
                IsAbstract = false,
                IsSufficient = false,
                AliasIds = new List<string>(),
                DeclaredName = null,
                OwnedRelationship = new List<Guid>(),
                OwningRelationship = Guid.NewGuid(),
                DeclaredShortName = null
            };
        }

        [Test]
        [TestCase(false)]
        [TestCase(true)]
        public void Verify_that_Elements_can_be_serialized(bool includeDerivedProperties)
        {
            var elements = new List<IElement> { this.partDefinition, this.partDefinitionWithNullProperties };
            var stream = new MemoryStream();
            var jsonWriterOptions = new JsonWriterOptions { Indented = true };

            Assert.That(() => this.serializer.Serialize(elements, SerializationModeKind.JSON, includeDerivedProperties,stream, jsonWriterOptions), Throws.Nothing);

            var json = Encoding.UTF8.GetString(stream.ToArray());
            Console.WriteLine(json);
        }

        [Test]
        [TestCase(false)]
        [TestCase(true)]
        public void Verify_that_Element_can_be_serialized(bool includeDerivedProperties)
        {
            var jsonWriterOptions = new JsonWriterOptions { Indented = true };
            
            var stream = new MemoryStream();
            Assert.That(() => this.serializer.Serialize(this.partDefinition, SerializationModeKind.JSON, includeDerivedProperties, stream, jsonWriterOptions), Throws.Nothing);

            var json = Encoding.UTF8.GetString(stream.ToArray());
            Console.WriteLine(json);
            
            stream = new MemoryStream();
            Assert.That(() => this.serializer.Serialize(this.partDefinition, SerializationModeKind.JSON,includeDerivedProperties , stream, jsonWriterOptions), Throws.Nothing);

            json = Encoding.UTF8.GetString(stream.ToArray());
            Console.WriteLine(json);
        }

        [Test]
        [TestCase(false)]
        [TestCase(true)]
        public async Task Verify_that_Elements_can_be_serialized_async(bool includeDerivedProperties)
        {
            var elements = new List<IElement> { this.partDefinition , partDefinitionWithNullProperties };
            var stream = new MemoryStream();
            var jsonWriterOptions = new JsonWriterOptions { Indented = true };

            var cts = new CancellationTokenSource();

            await Assert.ThatAsync(() => this.serializer.SerializeAsync(elements, SerializationModeKind.JSON,  includeDerivedProperties,stream, jsonWriterOptions, cts.Token), Throws.Nothing);

            var json = Encoding.UTF8.GetString(stream.ToArray());
            Console.WriteLine(json);
        }

        [Test]
        [TestCase(false)]
        [TestCase(true)]
        public async Task Verify_that_Element_can_be_serialized_async(bool includeDerivedProperties)
        {
            var stream = new MemoryStream();
            var jsonWriterOptions = new JsonWriterOptions { Indented = true };

            var cts = new CancellationTokenSource();

            await Assert.ThatAsync(() => this.serializer.SerializeAsync(this.partDefinition, SerializationModeKind.JSON, includeDerivedProperties, stream, jsonWriterOptions, cts.Token), Throws.Nothing);

            var json = Encoding.UTF8.GetString(stream.ToArray());
            Console.WriteLine(json);
        }
    }
}
