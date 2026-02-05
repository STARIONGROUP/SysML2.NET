// -------------------------------------------------------------------------------------------------
// <copyright file="SerializerTestFixture.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.Serializer.MessagePack.Tests
{
    using System.IO;
    using System.Linq;

    using NUnit.Framework;

    using SysML2.NET.Serializer.Json;
    using SysML2.NET.Extensions.Core.DTO.Comparers;

    using SerializationTargetKind = MessagePack.SerializationTargetKind;

    [TestFixture]
    public class SerializerTestFixture
    {
        private Json.DeSerializer jsonDeSerializer;

        private MessagePack.DeSerializer messagePackDeSerializer;

        private MessagePack.Serializer messagePackSerializer;

        [SetUp]
        public void SetUp()
        {
            this.jsonDeSerializer = new Json.DeSerializer();
            this.messagePackDeSerializer = new MessagePack.DeSerializer();
            this.messagePackSerializer = new MessagePack.Serializer();
        }

        [Test]
        public void Verify_that_iData_from_sysml2_core_json_can_be_deserialized()
        {
            var fileName = Path.Combine(TestContext.CurrentContext.WorkDirectory, "Data", "projects.000e9890-6935-43e6-a5d7-5d7cac601f4c.commits.6d7ad9fd-6520-4ff2-885b-8c5c129e6c27.elements.json");
            using var jsonStream = new FileStream(fileName, FileMode.Open, FileAccess.Read);

            var jsonData = this.jsonDeSerializer.DeSerialize(jsonStream, SerializationModeKind.JSON, Json.SerializationTargetKind.CORE,false).ToList();

            Assert.That(jsonData.Count(), Is.EqualTo(100));

            var messagePackStream = new MemoryStream();

            this.messagePackSerializer.Serialize(jsonData, messagePackStream);

            messagePackStream.Position = 0;

            var messagePackData= this.messagePackDeSerializer.DeSerialize(messagePackStream, SerializationTargetKind.CORE).ToList();

            Assert.That(messagePackData, Has.Count.EqualTo(jsonData.Count));

            var jsonById = jsonData.ToDictionary(x => x.Id);
            var msgById = messagePackData.ToDictionary(x => x.Id);

            Assert.That(jsonById.Keys, Is.EquivalentTo(msgById.Keys));

            using (Assert.EnterMultipleScope())
            {
                foreach (var id in jsonById.Keys)
                {
                    var left = jsonById[id];
                    var right = msgById[id];

                    Assert.That(
                        right.GetType(),
                        Is.EqualTo(left.GetType()),
                        $"Type mismatch for id {id}. Left={left.GetType().FullName}, Right={right.GetType().FullName}");

                    var comparer = ComparerProvider.Resolve(left);

                    Assert.That(
                        comparer.Equals(left, right),
                        Is.True,
                        $"Round-trip semantic mismatch for id {id} ({left.GetType().Name}).");
                }
            }
        }
    }
}
