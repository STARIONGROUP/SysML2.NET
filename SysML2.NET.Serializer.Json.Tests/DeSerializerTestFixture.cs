// -------------------------------------------------------------------------------------------------
// <copyright file="DeSerializerTestFixture.cs" company="RHEA System S.A.">
// 
//   Copyright 2022 RHEA System S.A.
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
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using NUnit.Framework;

    using SysML2.NET.Core.DTO;

    /// <summary>
    /// Suite of tests for the <see cref="DeSerializer"/>
    /// </summary>
    [TestFixture]
    public class DeSerializerTestFixture
    {
        private IDeSerializer deSerializer;

        [SetUp]
        public void SetUp()
        {
            this.deSerializer = new DeSerializer();
        }
        
        [Test]
        public void Verify_that_elements_json_can_be_deserialized()
        {
            var serializationModeKind = SerializationModeKind.JSON;

            var fileName = Path.Combine(TestContext.CurrentContext.WorkDirectory, "Data", "projects.29845a29-b25b-4bab-b8cc-f46a021b7f5a.commits.ec39c63a-fdaa-4a47-98a5-8e8f56b3a986.elements.json");
            using (var stream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            {
                var elements = this.deSerializer.DeSerialize(stream, serializationModeKind);

                Assert.That(elements.Count(), Is.EqualTo(100));

                Assert.That(elements.OfType<IPartDefinition>().Count(), Is.EqualTo(4));

                var partDefinition = elements.OfType<IPartDefinition>().Single(x => x.Id == Guid.Parse("07bd19e6-4587-4bdf-b274-1bbdb4b17707"));

                Assert.That(partDefinition.AliasIds, Is.Empty);
                Assert.That(partDefinition.ElementId, Is.EqualTo("07bd19e6-4587-4bdf-b274-1bbdb4b17707"));
                Assert.That(partDefinition.IsAbstract, Is.False);
                Assert.That(partDefinition.IsIndividual, Is.False);
                Assert.That(partDefinition.IsSufficient, Is.False);
                Assert.That(partDefinition.IsVariation, Is.False);
                Assert.That(partDefinition.Name, Is.EqualTo("AutomaticClutch"));
                Assert.That(partDefinition.OwnedRelationship, Is.EquivalentTo(new List<Guid> { Guid.Parse("d53e1d54-913d-43c1-aa11-e40f87420a5c") }));
                Assert.That(partDefinition.OwningRelationship, Is.EqualTo(Guid.Parse("11322389-ecab-42e0-8730-9802f2032d75")));
                Assert.That(partDefinition.ShortName, Is.Null);
            }
        }

        [Test]
        public async Task Verify_that_elements_json_can_be_deserialized_async()
        {
            var serializationModeKind = SerializationModeKind.JSON;

            var fileName = Path.Combine(TestContext.CurrentContext.WorkDirectory, "Data", "projects.29845a29-b25b-4bab-b8cc-f46a021b7f5a.commits.ec39c63a-fdaa-4a47-98a5-8e8f56b3a986.elements.json");
            using (var stream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            {
                var cts = new CancellationTokenSource();

                var elements = await this.deSerializer.DeSerializeAsync(stream, serializationModeKind, cts.Token);

                Assert.That(elements.Count(), Is.EqualTo(100));

                Assert.That(elements.OfType<IPartDefinition>().Count(), Is.EqualTo(4));

                var partDefinition = elements.OfType<IPartDefinition>().Single(x => x.Id == Guid.Parse("07bd19e6-4587-4bdf-b274-1bbdb4b17707"));

                Assert.That(partDefinition.AliasIds, Is.Empty);
                Assert.That(partDefinition.ElementId, Is.EqualTo("07bd19e6-4587-4bdf-b274-1bbdb4b17707"));
                Assert.That(partDefinition.IsAbstract, Is.False);
                Assert.That(partDefinition.IsIndividual, Is.False);
                Assert.That(partDefinition.IsSufficient, Is.False);
                Assert.That(partDefinition.IsVariation, Is.False);
                Assert.That(partDefinition.Name, Is.EqualTo("AutomaticClutch"));
                Assert.That(partDefinition.OwnedRelationship, Is.EquivalentTo(new List<Guid> { Guid.Parse("d53e1d54-913d-43c1-aa11-e40f87420a5c") }));
                Assert.That(partDefinition.OwningRelationship, Is.EqualTo(Guid.Parse("11322389-ecab-42e0-8730-9802f2032d75")));
                Assert.That(partDefinition.ShortName, Is.Null);
            }
        }
    }
}
