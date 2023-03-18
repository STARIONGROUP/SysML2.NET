// -------------------------------------------------------------------------------------------------
// <copyright file="SerializerTestFixture.cs" company="RHEA System S.A.">
// 
//   Copyright 2022-2023 RHEA System S.A.
// 
//   Licensed under the Apache License, Version 2.0 (the "License");
//   you may not use this file except in compliance with the License.
//   You may obtain a copy of the License at
// 
//        http://www.apache.org/licenses/LICENSE-2.0
// 
//    Unless required by applicable law or agreed to in writing, software
//    distributed under the License is distributed on an "AS IS" BASIS,
//    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//    See the License for the specific language governing permissions and
//    limitations under the License.
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

    using NUnit.Framework;

    using SysML2.NET.Core.DTO;

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
		        Name = "PartDefinition:DeclaredName",
		        OwnedRelationship = new List<Guid> { Guid.NewGuid() },
		        OwningRelationship = Guid.NewGuid(),
		        ShortName = "PartDefinition:DeclaredShortName"
	        };

	        this.partDefinitionWithNullProperties = new PartDefinition
	        {
		        Id = Guid.NewGuid(),
		        IsIndividual = true,
		        IsVariation = false,
		        IsAbstract = false,
		        IsSufficient = false,
		        AliasIds = new List<string>(),
		        Name = null,
		        OwnedRelationship = new List<Guid>(),
		        OwningRelationship = Guid.NewGuid(),
		        ShortName = null
	        };
		}

        [Test]
        public void Verify_that_Elements_can_be_serialized()
        {
            var elements = new List<IElement> { this.partDefinition, this.partDefinitionWithNullProperties };
            var stream = new MemoryStream();
            var jsonWriterOptions = new JsonWriterOptions { Indented = true };

            Assert.That(() => this.serializer.Serialize(elements, SerializationModeKind.JSON, stream, jsonWriterOptions), Throws.Nothing); ;

            var json = Encoding.UTF8.GetString(stream.ToArray());
            Console.WriteLine(json);
        }

        [Test]
        public void Verify_that_Element_can_be_serialized()
        {
            var jsonWriterOptions = new JsonWriterOptions { Indented = true };
            
            var stream = new MemoryStream();
            Assert.That(() => this.serializer.Serialize(this.partDefinition, SerializationModeKind.JSON, stream, jsonWriterOptions), Throws.Nothing);

            var json = Encoding.UTF8.GetString(stream.ToArray());
            Console.WriteLine(json);
            
            stream = new MemoryStream();
            Assert.That(() => this.serializer.Serialize(partDefinition, SerializationModeKind.JSON, stream, jsonWriterOptions), Throws.Nothing);

            json = Encoding.UTF8.GetString(stream.ToArray());
            Console.WriteLine(json);
        }

        [Test]
        public void Verify_that_Elements_can_be_serialized_async()
        {
            var elements = new List<IElement> { this.partDefinition , partDefinitionWithNullProperties };
            var stream = new MemoryStream();
            var jsonWriterOptions = new JsonWriterOptions { Indented = true };

            var cts = new CancellationTokenSource();

            Assert.That(async () => await this.serializer.SerializeAsync(elements, SerializationModeKind.JSON, stream, jsonWriterOptions, cts.Token), Throws.Nothing); ;

            var json = Encoding.UTF8.GetString(stream.ToArray());
            Console.WriteLine(json);
        }

        [Test]
        public void Verify_that_Element_can_be_serialized_async()
        {
            var stream = new MemoryStream();
            var jsonWriterOptions = new JsonWriterOptions { Indented = true };

            var cts = new CancellationTokenSource();

            Assert.That(async () => await this.serializer.SerializeAsync(this.partDefinition, SerializationModeKind.JSON, stream, jsonWriterOptions, cts.Token), Throws.Nothing); ;

            var json = Encoding.UTF8.GetString(stream.ToArray());
            Console.WriteLine(json);
        }
    }
}
