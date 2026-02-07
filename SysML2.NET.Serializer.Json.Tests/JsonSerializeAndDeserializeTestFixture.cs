// -------------------------------------------------------------------------------------------------
// <copyright file="JsonSerializeAndDeserializeTestFixture.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.Serializer.Json.Tests
{
    using System.IO;
    using System.Linq;
    using System.Text.Json;

    using NUnit.Framework;

    using SysML2.NET.Extensions.Core.DTO.Comparers;

    using SysML2.NET.Serializer.Json;

    /// <summary>
    /// Suite of tests for the <see cref="DeSerializer"/>
    /// </summary>
    [TestFixture]
    public class JsonSerializeAndDeserializeTestFixture
    {
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
