// ------------------------------------------------------------------------------------------------
// <copyright file="InterchangeProjectDeSerializerTestFixture.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.Serializer.Json.Tests.ModelInterchange
{
    using System.IO;
    using System.Text.Json;
    
    using SysML2.NET.Serializer.Json.ModelInterchange;
    using SysML2.NET.ModelInterchange;
    
    using NUnit.Framework;
    
    /// <summary>
    /// Suite of tests for the <see cref="InterchangeProjectDeSerializer"/>
    /// </summary>
    [TestFixture]
    public class InterchangeProjectDeSerializerTestFixture
    {
        [Test]
        public void Verify_that_project_dot_json_file_can_be_deserialized()
        {
            var fileName = Path.Combine(TestContext.CurrentContext.WorkDirectory, "Data", ".project.json");
            using var stream = new FileStream(fileName, FileMode.Open, FileAccess.Read);

            var json = File.ReadAllBytes(fileName);
            
            var interchangeProject = DeserializeFromJson(json);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(interchangeProject, Is.Not.Null);
                Assert.That(interchangeProject.Name, Is.EqualTo("Kernel Semantic Library"));
                Assert.That(interchangeProject.Description, Is.EqualTo("Standard semantic library for the Kernel Modeling Language (KerML)"));
                Assert.That(interchangeProject.Version, Is.EqualTo("1.0.0"));
                Assert.That(interchangeProject.License, Is.EqualTo("LGPL"));
                Assert.That(interchangeProject.Maintainer.Count, Is.EqualTo(1));
                Assert.That(interchangeProject.Maintainer[0], Is.EqualTo("OMG"));
                Assert.That(interchangeProject.Website, Is.EqualTo("https://www.omg.org/spec/KerML"));
                Assert.That(interchangeProject.Topic.Count, Is.EqualTo(2));
                Assert.That(interchangeProject.Topic[0], Is.EqualTo("Kerml"));
                Assert.That(interchangeProject.Topic[1], Is.EqualTo("OMG"));
                Assert.That(interchangeProject.Usage.Count, Is.EqualTo(2));
                Assert.That(interchangeProject.Usage[0].Resource, Is.EqualTo("https://www.omg.org/spec/KerML/20250201/Data-Type-Library.kpar"));
                Assert.That(interchangeProject.Usage[0].VersionConstraint, Is.EqualTo("1.0.0"));
                Assert.That(interchangeProject.Usage[1].Resource, Is.EqualTo("https://www.omg.org/spec/KerML/20250201/Function-Library.kpar"));
                Assert.That(interchangeProject.Usage[1].VersionConstraint, Is.EqualTo("1.0.0"));
            }
        }
        
        /// <summary>
        /// Deserializes an <see cref="InterchangeProject"/> instance from a UTF-8 encoded JSON payload.
        /// </summary>
        /// <param name="json">
        /// A byte array containing a valid UTF-8 encoded JSON representation of an
        /// <see cref="InterchangeProject"/> document.
        /// </param>
        /// <returns>
        /// The deserialized <see cref="InterchangeProject"/> instance.
        /// </returns>
        private static InterchangeProject DeserializeFromJson(byte[] json)
        {
            var reader = new Utf8JsonReader(json, isFinalBlock: true, state: default);

            Assert.That(reader.Read(), Is.True, "JSON did not yield any tokens.");
            Assert.That(reader.TokenType, Is.EqualTo(JsonTokenType.StartObject), "Reader not positioned on StartObject.");

            return InterchangeProjectDeSerializer.DeSerialize(ref reader);
        }
    }
}
