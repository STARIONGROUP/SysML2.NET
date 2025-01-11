// -------------------------------------------------------------------------------------------------
// <copyright file="WriterTestFixture.cs" company="Starion Group S.A.">
// 
//   Copyright 2022-2025 Starion Group S.A.
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

namespace SysML2.NET.Serializer.Dictionary.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using NUnit.Framework;

    using SysML2.NET.Common;
    using SysML2.NET.Core.DTO;
    using SysML2.NET.Serializer.Dictionary;

    /// <summary>
    /// Suite of tests for the <see cref="Writer"/> class
    /// </summary>
    [TestFixture]
    public class WriterTestFixture
    {
        private IWriter writer;

        private PartDefinition partDefinition;

        private PartDefinition partDefinitionWithNullProperties;

        [SetUp]
        public void SetUp()
        {
            this.writer = new Writer();

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
                ElementId = "PartDefinition:ElementId",
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
                ElementId = "PartDefinition:ElementId",
                OwnedRelationship = new List<Guid>(),
                OwningRelationship = Guid.NewGuid(),
                DeclaredShortName = null
            };
        }

        [Test]
        public void Verify_that_the_writer_returns_a_dictionary_upon_write_Simplified_single_dataItem()
        {
            var dictionary = this.writer.Write(this.partDefinition, DictionaryKind.Simplified);

            Assert.That(dictionary, Is.Not.Null);

            dictionary.TryGetValue("@type", out var @type);
            Assert.That(@type, Is.EqualTo("PartDefinition"));

            dictionary.TryGetValue("@id", out var id);
            Assert.That(id, Is.EqualTo(this.partDefinition.Id.ToString()));

            dictionary.TryGetValue("aliasIds", out var aliasIds);
            Assert.That(aliasIds, Is.EqualTo(this.partDefinition.AliasIds));

            dictionary.TryGetValue("elementId", out var elementId);
            Assert.That(elementId, Is.EqualTo(this.partDefinition.ElementId));

            dictionary.TryGetValue("declaredName", out var name);
            Assert.That(name, Is.EqualTo(this.partDefinition.DeclaredName));

            dictionary.TryGetValue("ownedRelationship", out var ownedRelationship);
            Assert.That(ownedRelationship, Is.EqualTo($"[ {string.Join(",", this.partDefinition.OwnedRelationship)} ]"));

            dictionary.TryGetValue("owningRelationship", out var owningRelationship);
            Assert.That(owningRelationship, Is.EqualTo(this.partDefinition.OwningRelationship.ToString()));
        }

        [Test]
        public void Verify_that_the_writer_returns_a_dictionary_upon_write_Complex_single_dataItem()
        {
            var dictionary = this.writer.Write(this.partDefinition, DictionaryKind.Complex);

            Assert.That(dictionary, Is.Not.Null);

            dictionary.TryGetValue("@type", out var @type);
            Assert.That(@type, Is.EqualTo("PartDefinition"));

            dictionary.TryGetValue("@id", out var id);
            Assert.That(id, Is.EqualTo(this.partDefinition.Id));

            dictionary.TryGetValue("aliasIds", out var aliasIds);
            Assert.That(aliasIds, Is.EqualTo(this.partDefinition.AliasIds));

            dictionary.TryGetValue("elementId", out var elementId);
            Assert.That(elementId, Is.EqualTo(this.partDefinition.ElementId));

            dictionary.TryGetValue("declaredName", out var name);
            Assert.That(name, Is.EqualTo(this.partDefinition.DeclaredName));

            dictionary.TryGetValue("ownedRelationship", out var ownedRelationship);
            Assert.That(ownedRelationship, Is.EqualTo(this.partDefinition.OwnedRelationship));

            dictionary.TryGetValue("owningRelationship", out var owningRelationship);
            Assert.That(owningRelationship, Is.EqualTo(this.partDefinition.OwningRelationship));
        }

        [Test]
        public void Verify_that_the_writer_returns_a_list_of_dictionary_upon_write_multiple_dataItems()
        {
            var dataItems = new List<IData> { this.partDefinition, this.partDefinitionWithNullProperties };

            var dictionaries = this.writer.Write(dataItems, DictionaryKind.Simplified);

            Assert.That(dictionaries.Count(), Is.EqualTo(2));

            dictionaries = this.writer.Write(dataItems, DictionaryKind.Complex);

            Assert.That(dictionaries.Count(), Is.EqualTo(2));
        }

        [Test]
        public void Verify_that_when_an_unsupported_type_is_provided_an_Exception_is_thrown()
        {
            var testClass = new TestClass();

            Assert.That(() => this.writer.Write(testClass, DictionaryKind.Simplified), Throws.Exception.TypeOf<NotSupportedException>());
        }

        private class TestClass : IData
        {
            public Guid Id { get; set; }
        }
    }
}
