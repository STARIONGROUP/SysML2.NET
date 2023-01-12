// -------------------------------------------------------------------------------------------------
// <copyright file="WriterTestFixture.cs" company="RHEA System S.A.">
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

        [SetUp]
        public void SetUp()
        {
            this.writer = new Writer();
        }
        
        [Test]
        public void Verify_that_the_writer_returns_a_dictionary_upon_write_Simplified_single_dataItem()
        {
            var element = new Element
            {
                Id = Guid.NewGuid(),
                AliasIds = new List<string> { "Element:Alias_1", "Element:Alias_2" },
                ElementId = "Element:ElementId",
                Name = "Element:Name",
                OwnedRelationship = new List<Guid> { Guid.NewGuid() },
                OwningRelationship = Guid.NewGuid(),
                ShortName = "Element:ShortName"
            };

            var dictionary = this.writer.Write(element, DictionaryKind.Simplified);

            Assert.That(dictionary, Is.Not.Null);

            dictionary.TryGetValue("@type", out var @type);
            Assert.That(@type, Is.EqualTo("Element"));

            dictionary.TryGetValue("@id", out var id);
            Assert.That(id, Is.EqualTo(element.Id.ToString()));

            dictionary.TryGetValue("aliasIds", out var aliasIds);
            Assert.That(aliasIds, Is.EqualTo(element.AliasIds));

            dictionary.TryGetValue("elementId", out var elementId);
            Assert.That(elementId, Is.EqualTo(element.ElementId));

            dictionary.TryGetValue("name", out var name);
            Assert.That(name, Is.EqualTo(element.Name));

            dictionary.TryGetValue("ownedRelationship", out var ownedRelationship);
            Assert.That(ownedRelationship, Is.EqualTo($"[ {string.Join(",", element.OwnedRelationship)} ]"));

            dictionary.TryGetValue("owningRelationship", out var owningRelationship);
            Assert.That(owningRelationship, Is.EqualTo(element.OwningRelationship.ToString()));
        }

        [Test]
        public void Verify_that_the_writer_returns_a_dictionary_upon_write_Complex_single_dataItem()
        {
            var element = new Element
            {
                Id = Guid.NewGuid(),
                AliasIds = new List<string> { "Element:Alias_1", "Element:Alias_2" },
                ElementId = "Element:ElementId",
                Name = "Element:Name",
                OwnedRelationship = new List<Guid> { Guid.NewGuid() },
                OwningRelationship = Guid.NewGuid(),
                ShortName = "Element:ShortName"
            };

            var dictionary = this.writer.Write(element, DictionaryKind.Complex);

            Assert.That(dictionary, Is.Not.Null);

            dictionary.TryGetValue("@type", out var @type);
            Assert.That(@type, Is.EqualTo("Element"));

            dictionary.TryGetValue("@id", out var id);
            Assert.That(id, Is.EqualTo(element.Id));

            dictionary.TryGetValue("aliasIds", out var aliasIds);
            Assert.That(aliasIds, Is.EqualTo(element.AliasIds));

            dictionary.TryGetValue("elementId", out var elementId);
            Assert.That(elementId, Is.EqualTo(element.ElementId));

            dictionary.TryGetValue("name", out var name);
            Assert.That(name, Is.EqualTo(element.Name));

            dictionary.TryGetValue("ownedRelationship", out var ownedRelationship);
            Assert.That(ownedRelationship, Is.EqualTo(element.OwnedRelationship));

            dictionary.TryGetValue("owningRelationship", out var owningRelationship);
            Assert.That(owningRelationship, Is.EqualTo(element.OwningRelationship));
        }

        [Test]
        public void Verify_that_the_writer_returns_a_list_of_dictionary_upon_write_multiple_dataItems()
        {
            var element = new Element
            {
                Id = Guid.NewGuid(),
                AliasIds = new List<string> { "Element:Alias_1", "Element:Alias_2" },
                ElementId = "Element:ElementId",
                Name = "Element:Name",
                OwnedRelationship = new List<Guid> { Guid.NewGuid() },
                OwningRelationship = Guid.NewGuid(),
                ShortName = "Element:ShortName"
            };

            var partDefinition = new PartDefinition
            {
                Id = Guid.NewGuid(),
                IsIndividual = true,
                IsVariation = false,
                IsAbstract = false,
                IsSufficient = false,
                AliasIds = new List<string> { "PartDefinition:Alias_1", "PartDefinition:Alias_2" },
                Name = "PartDefinition:Name",
                OwnedRelationship = new List<Guid> { Guid.NewGuid() },
                OwningRelationship = Guid.NewGuid(),
                ShortName = "PartDefinition:ShortName"
            };

            var dataItems = new List<IData> { element, partDefinition };

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
