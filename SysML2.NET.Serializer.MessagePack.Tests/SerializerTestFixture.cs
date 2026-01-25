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
    using System;
    using System.IO;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using NUnit.Framework;

    using SysML2.NET.PIM.DTO;

    using SysML2.NET.Core;
    using SysML2.NET.Core.DTO;
    using SysML2.NET.Serializer.Json;
    using SysML2.NET.Serializer.MessagePack;
    using SysML2.NET.Core.Core.Types;
    using SysML2.NET.Core.DTO.Core.Features;
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
            using var stream = new FileStream(fileName, FileMode.Open, FileAccess.Read);

            var jsonData = this.jsonDeSerializer.DeSerialize(stream, SerializationModeKind.JSON, Json.SerializationTargetKind.CORE,false);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(jsonData.Count(), Is.EqualTo(100));
            }

            var messagePackStream = new MemoryStream();

            this.messagePackSerializer.Serialize(jsonData, messagePackStream);

            messagePackStream.Position = 0;

            var messagePackData= this.messagePackDeSerializer.DeSerialize(messagePackStream, SerializationTargetKind.CORE);

            var jsonBasedDictionary = jsonData.ToDictionary(x => x.Id);
            var messagePackBasedDictionary = messagePackData.ToDictionary(x => x.Id);

            Assert.That(jsonBasedDictionary.Keys, Is.EquivalentTo(messagePackBasedDictionary.Keys));
        }
    }
}
