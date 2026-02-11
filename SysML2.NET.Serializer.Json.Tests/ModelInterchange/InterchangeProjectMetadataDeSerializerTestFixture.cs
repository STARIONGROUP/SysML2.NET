// -------------------------------------------------------------------------------------------------
// <copyright file="InterchangeProjectMetadataDeSerializerTestFixture.cs" company="Starion Group S.A.">
// 
//   Copyright 2022-2026 Starion Group S.A.
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

namespace SysML2.NET.Serializer.Json.Tests.ModelInterchange
{
    using System;
    using System.IO;
    using System.Text.Json;
    
    using SysML2.NET.Serializer.Json.ModelInterchange;
    using SysML2.NET.ModelInterchange;
    
    using NUnit.Framework;
    
    /// <summary>
    /// Suite of tests for the <see cref="InterchangeProjectMetadataDeSerializer"/>
    /// </summary>
    [TestFixture]
    public class InterchangeProjectMetadataDeSerializerTestFixture
    {
        [Test]
        public void Verify_that_meta_dot_json_file_can_be_deserialized()
        {
            var fileName = Path.Combine(TestContext.CurrentContext.WorkDirectory, "Data", ".meta.json");
            using var stream = new FileStream(fileName, FileMode.Open, FileAccess.Read);

            var json = File.ReadAllBytes(fileName);
            
            var interchangeProjectMetadata = DeserializeFromJson(json);

            Assert.That(interchangeProjectMetadata, Is.Not.Null);
            
            Assert.Multiple(() =>
            {
                Assert.That(interchangeProjectMetadata.Metamodel, Is.EqualTo("https://www.omg.org/spec/KerML/20250201"));
                Assert.That(interchangeProjectMetadata.Created, Is.EqualTo(DateTimeOffset.Parse("2025-03-13T00:00:00Z", null, System.Globalization.DateTimeStyles.AssumeUniversal)));
                Assert.That(interchangeProjectMetadata.IncludesDerived, Is.False);
                Assert.That(interchangeProjectMetadata.IncludesImplied, Is.False);
                
                Assert.That(interchangeProjectMetadata.Index, Is.Not.Null);
                Assert.That(interchangeProjectMetadata.Checksum, Is.Not.Null);

                Assert.That(interchangeProjectMetadata.Index, Has.Count.EqualTo(16));
                Assert.That(interchangeProjectMetadata.Index["Base"], Is.EqualTo("Base.kerml"));
                Assert.That(interchangeProjectMetadata.Index["KerML"], Is.EqualTo("KerML.kerml"));
                Assert.That(interchangeProjectMetadata.Index["Triggers"], Is.EqualTo("Triggers.kerml"));

                Assert.That(interchangeProjectMetadata.Checksum, Has.Count.EqualTo(16));

                Assert.That(interchangeProjectMetadata.Checksum.ContainsKey("Base.kerml"), Is.True);
                Assert.That(interchangeProjectMetadata.Checksum["Base.kerml"].Algorithm, Is.EqualTo(ChecksumKind.SHA256));
                Assert.That(interchangeProjectMetadata.Checksum["Base.kerml"].Value, Is.EqualTo("56df84cda67f62c63d4e79e2786fc26046cfa361a958c4fcf0843d32a5707e09"));

                Assert.That(interchangeProjectMetadata.Checksum.ContainsKey("Triggers.kerml"), Is.True);
                Assert.That(interchangeProjectMetadata.Checksum["Triggers.kerml"].Algorithm, Is.EqualTo(ChecksumKind.SHA256));
                Assert.That(interchangeProjectMetadata.Checksum["Triggers.kerml"].Value, Is.EqualTo("124cad3625935e078d1363e6100ee12537ca9c51445a18108e056db8b4885609"));

                Assert.That(interchangeProjectMetadata.Checksum.ContainsKey("SpatialFrames.kerml"), Is.True);
                Assert.That(interchangeProjectMetadata.Checksum["SpatialFrames.kerml"].Algorithm, Is.EqualTo(ChecksumKind.SHA256));
                Assert.That(interchangeProjectMetadata.Checksum["SpatialFrames.kerml"].Value, Is.EqualTo("2a7790ebc2afacbd64eb781567906921e38eff385c917be03b090b8289353de7"));
            });
        }
        
        /// <summary>
        /// Deserializes an <see cref="InterchangeProjectMetadata"/> instance from a UTF-8 encoded JSON payload.
        /// </summary>
        /// <param name="json">
        /// The UTF-8 encoded JSON byte array representing a <c>.meta.json</c> interchange metadata document.
        /// </param>
        /// <returns>
        /// The deserialized <see cref="InterchangeProjectMetadata"/> instance.
        /// </returns>
        private static InterchangeProjectMetadata DeserializeFromJson(byte[] json)
        {
            var reader = new Utf8JsonReader(json, isFinalBlock: true, state: default);

            Assert.That(reader.Read(), Is.True, "JSON did not yield any tokens.");
            Assert.That(reader.TokenType, Is.EqualTo(JsonTokenType.StartObject), "Reader not positioned on StartObject.");

            return InterchangeProjectMetadataDeSerializer.DeSerialize(ref reader);
        }
    }
}