// -------------------------------------------------------------------------------------------------
// <copyright file="PsmSerializerTestFixture.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.Serializer.Json.Tests
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using System.Text.Json;

    using NUnit.Framework;

    using SysML2.NET.Core.DTO.Core.Features;
    using SysML2.NET.PSM.DTO;
    using SysML2.NET.PSM.Enumerations;
    using SysML2.NET.Serializer.Json;

    [TestFixture]
    public class PsmSerializerTestFixture
    {
        private static readonly Guid ProjectIdentifier = Guid.Parse("9b0e1914-3241-461e-b9ee-a3ff5120de4e");

        private static readonly Guid BranchIdentifier = Guid.Parse("a910a705-7fbe-415f-9cbb-624bfadf6c20");

        private static readonly Guid DataVersionIdentifier = Guid.Parse("94e5b40e-741e-49ca-bd7f-f3138c071bf9");

        private static readonly Guid ElementIdentifier = Guid.Parse("00a6ef10-d3dc-4741-9029-2c9978c2f083");

        private ISerializer serializer;

        private JsonWriterOptions jsonWriterOptions;

        [SetUp]
        public void SetUp()
        {
            this.serializer = new Serializer();
            this.jsonWriterOptions = new JsonWriterOptions { Indented = false };
        }

        [Test]
        public void VerifySerializeResponse()
        {
            var project = new Project
            {
                Id = ProjectIdentifier,
                Alias = ["first", "second"],
                Created = new DateTime(1976, 8, 20, 0, 0, 0, DateTimeKind.Utc),
                DefaultBranch = BranchIdentifier,
                Description = "the description",
                Name = "the name"
            };

            var json = Serialize(stream => this.serializer.SerializeResponse(project, SerializationModeKind.JSON, false, stream, this.jsonWriterOptions));

            using var document = JsonDocument.Parse(json);
            var root = document.RootElement;

            using (Assert.EnterMultipleScope())
            {
                Assert.That(root.GetProperty("@type").GetString(), Is.EqualTo("Project"));
                Assert.That(root.GetProperty("@id").GetGuid(), Is.EqualTo(ProjectIdentifier));
                Assert.That(root.GetProperty("alias").GetArrayLength(), Is.EqualTo(2));
                Assert.That(root.GetProperty("alias")[0].GetString(), Is.EqualTo("first"));
                Assert.That(root.GetProperty("created").GetDateTime(), Is.EqualTo(new DateTime(1976, 8, 20, 0, 0, 0, DateTimeKind.Utc)));
                Assert.That(root.GetProperty("defaultBranch").GetProperty("@id").GetGuid(), Is.EqualTo(BranchIdentifier));
                Assert.That(root.GetProperty("name").GetString(), Is.EqualTo("the name"));
            }

            var collectionJson = Serialize(stream =>
                this.serializer.SerializeResponse(new List<IResponse> { project }, SerializationModeKind.JSON, false, stream, this.jsonWriterOptions));

            Assert.That(collectionJson, Does.StartWith("[{").And.EndWith("}]"));
        }

        [Test]
        public void VerifySerializeRequest()
        {
            var projectRequest = new ProjectRequest
            {
                Alias = [],
                DefaultBranch = BranchIdentifier,
                Description = null,
                Name = "the name"
            };

            var json = Serialize(stream => this.serializer.SerializeRequest(projectRequest, SerializationModeKind.JSON, false, stream, this.jsonWriterOptions));

            using var document = JsonDocument.Parse(json);
            var root = document.RootElement;

            using (Assert.EnterMultipleScope())
            {
                Assert.That(root.GetProperty("@type").GetString(), Is.EqualTo("Project"));
                Assert.That(root.TryGetProperty("@id", out _), Is.False);
                Assert.That(root.GetProperty("alias").GetArrayLength(), Is.EqualTo(0));
                Assert.That(root.GetProperty("defaultBranch").GetProperty("@id").GetGuid(), Is.EqualTo(BranchIdentifier));
                Assert.That(root.GetProperty("description").ValueKind, Is.EqualTo(JsonValueKind.Null));
            }

            var withoutBranch = new ProjectRequest { Name = "no branch" };

            var nullBranchJson = Serialize(stream => this.serializer.SerializeRequest(withoutBranch, SerializationModeKind.JSON, false, stream, this.jsonWriterOptions));

            Assert.That(nullBranchJson, Does.Contain("\"defaultBranch\":null"));
        }

        [Test]
        public void VerifySerializeConstraint()
        {
            var constraint = new PrimitiveConstraint
            {
                Inverse = true,
                Operator = PrimitiveConstraintOperator.lessthan,
                Property = "created",
                Value =
                [
                    ConstraintValue.From(true),
                    ConstraintValue.From(1.5),
                    ConstraintValue.From("text"),
                    ConstraintValue.From(ProjectIdentifier),
                    ConstraintValue.Null
                ]
            };

            var json = Serialize(stream => this.serializer.SerializeResponse(constraint, SerializationModeKind.JSON, false, stream, this.jsonWriterOptions));

            using var document = JsonDocument.Parse(json);
            var root = document.RootElement;
            var values = root.GetProperty("value");

            using (Assert.EnterMultipleScope())
            {
                Assert.That(root.GetProperty("@type").GetString(), Is.EqualTo("PrimitiveConstraint"));
                Assert.That(root.GetProperty("inverse").GetBoolean(), Is.True);
                Assert.That(root.GetProperty("operator").GetString(), Is.EqualTo("<"));
                Assert.That(values.GetArrayLength(), Is.EqualTo(5));
                Assert.That(values[0].GetBoolean(), Is.True);
                Assert.That(values[1].GetDouble(), Is.EqualTo(1.5));
                Assert.That(values[2].GetString(), Is.EqualTo("text"));
                Assert.That(values[3].GetProperty("@id").GetGuid(), Is.EqualTo(ProjectIdentifier));
                Assert.That(values[4].ValueKind, Is.EqualTo(JsonValueKind.Null));
            }

            var composite = new CompositeConstraint
            {
                Operator = CompositeConstraintOperator.and,
                Constraint = [constraint]
            };

            var compositeJson = Serialize(stream => this.serializer.SerializeResponse(composite, SerializationModeKind.JSON, false, stream, this.jsonWriterOptions));

            using var compositeDocument = JsonDocument.Parse(compositeJson);
            var compositeRoot = compositeDocument.RootElement;

            using (Assert.EnterMultipleScope())
            {
                Assert.That(compositeRoot.GetProperty("@type").GetString(), Is.EqualTo("CompositeConstraint"));
                Assert.That(compositeRoot.GetProperty("operator").GetString(), Is.EqualTo("and"));
                Assert.That(compositeRoot.GetProperty("constraint")[0].GetProperty("@type").GetString(), Is.EqualTo("PrimitiveConstraint"));
            }
        }

        [Test]
        public void VerifySerializeDataVersion()
        {
            var element = new Feature { Id = ElementIdentifier, ElementId = ElementIdentifier.ToString() };

            var dataVersion = new DataVersion
            {
                Id = DataVersionIdentifier,
                Identity = new DataIdentity { Id = ElementIdentifier, Name = "the identity" },
                Payload = element
            };

            var json = Serialize(stream => this.serializer.SerializeResponse(dataVersion, SerializationModeKind.JSON, false, stream, this.jsonWriterOptions));

            using var document = JsonDocument.Parse(json);
            var root = document.RootElement;

            using (Assert.EnterMultipleScope())
            {
                Assert.That(root.GetProperty("@type").GetString(), Is.EqualTo("DataVersion"));
                Assert.That(root.GetProperty("@id").GetGuid(), Is.EqualTo(DataVersionIdentifier));
                Assert.That(root.GetProperty("identity").GetProperty("@type").GetString(), Is.EqualTo("DataIdentity"));
                Assert.That(root.GetProperty("identity").GetProperty("name").GetString(), Is.EqualTo("the identity"));
                Assert.That(root.GetProperty("payload").GetProperty("@type").GetString(), Is.EqualTo("Feature"));
                Assert.That(root.GetProperty("payload").GetProperty("@id").GetGuid(), Is.EqualTo(ElementIdentifier));
            }

            var withApiPayload = new DataVersion
            {
                Id = DataVersionIdentifier,
                Payload = new ExternalData { Id = ElementIdentifier, ResourceIdentifier = new Uri("http://www.stariongroup.eu/external") }
            };

            var apiJson = Serialize(stream => this.serializer.SerializeResponse(withApiPayload, SerializationModeKind.JSON, false, stream, this.jsonWriterOptions));

            using var apiDocument = JsonDocument.Parse(apiJson);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(apiDocument.RootElement.GetProperty("payload").GetProperty("@type").GetString(), Is.EqualTo("ExternalData"));
                Assert.That(apiDocument.RootElement.GetProperty("payload").GetProperty("resourceIdentifier").GetString(), Is.EqualTo("http://www.stariongroup.eu/external"));
            }

            var withoutPayload = new DataVersion { Id = DataVersionIdentifier };

            var nullPayloadJson = Serialize(stream => this.serializer.SerializeResponse(withoutPayload, SerializationModeKind.JSON, false, stream, this.jsonWriterOptions));

            using var nullPayloadDocument = JsonDocument.Parse(nullPayloadJson);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(nullPayloadDocument.RootElement.GetProperty("payload").ValueKind, Is.EqualTo(JsonValueKind.Null));
                Assert.That(nullPayloadDocument.RootElement.GetProperty("identity").ValueKind, Is.EqualTo(JsonValueKind.Null));
            }

            var commitRequest = new CommitRequest
            {
                Description = "the commit",
                Change = [new DataVersionRequest { Payload = element }]
            };

            var commitJson = Serialize(stream => this.serializer.SerializeRequest(commitRequest, SerializationModeKind.JSON, false, stream, this.jsonWriterOptions));

            using var commitDocument = JsonDocument.Parse(commitJson);
            var change = commitDocument.RootElement.GetProperty("change");

            using (Assert.EnterMultipleScope())
            {
                Assert.That(commitDocument.RootElement.GetProperty("@type").GetString(), Is.EqualTo("Commit"));
                Assert.That(change.GetArrayLength(), Is.EqualTo(1));
                Assert.That(change[0].GetProperty("@type").GetString(), Is.EqualTo("DataVersion"));
                Assert.That(change[0].GetProperty("payload").GetProperty("@type").GetString(), Is.EqualTo("Feature"));
                Assert.That(change[0].GetProperty("payload").GetProperty("@id").GetGuid(), Is.EqualTo(ElementIdentifier));
            }

            var requestWithApiPayload = new DataVersionRequest
            {
                Payload = new ExternalDataRequest { Id = ElementIdentifier, ResourceIdentifier = new Uri("http://www.stariongroup.eu/external") }
            };

            var requestJson = Serialize(stream => this.serializer.SerializeRequest(requestWithApiPayload, SerializationModeKind.JSON, false, stream, this.jsonWriterOptions));

            using var requestDocument = JsonDocument.Parse(requestJson);
            var requestPayload = requestDocument.RootElement.GetProperty("payload");

            using (Assert.EnterMultipleScope())
            {
                Assert.That(requestDocument.RootElement.GetProperty("@type").GetString(), Is.EqualTo("DataVersion"));
                Assert.That(requestPayload.GetProperty("@type").GetString(), Is.EqualTo("ExternalData"));
                Assert.That(requestPayload.GetProperty("resourceIdentifier").GetString(), Is.EqualTo("http://www.stariongroup.eu/external"));
            }
        }

        [Test]
        public void VerifySerializeUnsupportedMode()
        {
            var project = new Project { Id = ProjectIdentifier, Name = "the name" };

            using var stream = new MemoryStream();

            Assert.That(() => this.serializer.SerializeResponse(project, SerializationModeKind.JSONLD, false, stream, this.jsonWriterOptions),
                Throws.TypeOf<NotSupportedException>());
        }

        private static string Serialize(Action<Stream> serialize)
        {
            using var stream = new MemoryStream();

            serialize(stream);

            return Encoding.UTF8.GetString(stream.ToArray());
        }
    }
}
