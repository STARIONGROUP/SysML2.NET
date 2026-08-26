// -------------------------------------------------------------------------------------------------
// <copyright file="DeSerializerTestFixture.cs" company="Starion Group S.A.">
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
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Runtime.Serialization;
    using System.Text;
    using System.Text.Json;
    using System.Threading;
    using System.Threading.Tasks;

    using NUnit.Framework;

    using SysML2.NET.Common;
    using SysML2.NET.Core.Core.Types;
    using SysML2.NET.Core.DTO.Core.Features;
    using SysML2.NET.PIM.DTO;
    using SysML2.NET.Serializer.Json;

    /// <summary>
    /// Suite of tests for the <see cref="DeSerializer"/>
    /// </summary>
    [TestFixture]
    public class DeSerializerTestFixture
    {
        /// <summary>
        /// A single element json object
        /// </summary>
        private const string Element = """{"@type":"Feature","@id":"00a6ef10-d3dc-4741-9029-2c9978c2f083","elementId":"00a6ef10-d3dc-4741-9029-2c9978c2f083"}""";

        /// <summary>
        /// The identifier that <see cref="Element"/> carries
        /// </summary>
        private static readonly Guid ElementIdentifier = Guid.Parse("00a6ef10-d3dc-4741-9029-2c9978c2f083");

        /// <summary>
        /// The number of elements of the payload that forces the pooled input buffer to grow
        /// </summary>
        /// <remarks>
        /// Each element serializes to roughly 130 bytes, so 4000 of them is about 520 kB. A non seekable
        /// stream starts on an 81 920 byte buffer, which therefore has to double three times.
        /// </remarks>
        private const int LargePayloadElementCount = 4000;

        private DeSerializer deSerializer;

        [SetUp]
        public void SetUp()
        {
            this.deSerializer = new DeSerializer();
        }
        
        [Test]
        [TestCase(false)]
        [TestCase(true)]
        public void Verify_that_iData_from_sysmlcore_json_can_be_deserialized(bool shouldDeserializeDerivedProperties)
        {
            var fileName = Path.Combine(TestContext.CurrentContext.WorkDirectory, "Data", "projects.000e9890-6935-43e6-a5d7-5d7cac601f4c.commits.6d7ad9fd-6520-4ff2-885b-8c5c129e6c27.elements.json");
            using var stream = new FileStream(fileName, FileMode.Open, FileAccess.Read);

            var data = this.deSerializer.DeSerialize(stream, SerializationModeKind.JSON, SerializationTargetKind.CORE, shouldDeserializeDerivedProperties);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(data.Count(), Is.EqualTo(100));
                Assert.That(data.OfType<IFeature>().Count(), Is.EqualTo(30));
            }

            var feature = data.OfType<IFeature>().Single(x => x.Id == Guid.Parse("00a6ef10-d3dc-4741-9029-2c9978c2f083"));

            using (Assert.EnterMultipleScope())
            {
                Assert.That(feature.AliasIds, Is.Empty);
                Assert.That(feature.ElementId, Is.EqualTo("00a6ef10-d3dc-4741-9029-2c9978c2f083"));
                Assert.That(feature.IsAbstract, Is.False);
                Assert.That(feature.IsComposite, Is.False);
                Assert.That(feature.IsSufficient, Is.False);
                Assert.That(feature.IsEnd, Is.False);
                Assert.That(feature.IsUnique, Is.True);
                Assert.That(feature.DeclaredName, Is.Null);
                Assert.That(feature.OwnedRelationship, Is.Empty);
                Assert.That(feature.OwningRelationship, Is.EqualTo(Guid.Parse("8a780d8b-61a6-472b-8b80-2564aa9f7c36")));
                Assert.That(feature.DeclaredShortName, Is.Null);
                Assert.That(feature.Direction, Is.EqualTo(FeatureDirectionKind.Out));
            }
        }

        [Test]
        [TestCase(false)]
        [TestCase(true)]
        public async Task Verify_that_iData_from_sysmlcore_json_can_be_deserialized_async(bool shouldDeserializeDerivedProperties)
        {
            var fileName = Path.Combine(TestContext.CurrentContext.WorkDirectory, "Data", "projects.000e9890-6935-43e6-a5d7-5d7cac601f4c.commits.6d7ad9fd-6520-4ff2-885b-8c5c129e6c27.elements.json");

            await using var stream = new FileStream(fileName, FileMode.Open, FileAccess.Read);

            var cts = new CancellationTokenSource();

            var data = await this.deSerializer.DeSerializeAsync(stream, SerializationModeKind.JSON, SerializationTargetKind.CORE, shouldDeserializeDerivedProperties, cts.Token);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(data.Count(), Is.EqualTo(100));

                Assert.That(data.OfType<IFeature>().Count(), Is.EqualTo(30));
            }

            var feature = data.OfType<IFeature>().Single(x => x.Id == Guid.Parse("00a6ef10-d3dc-4741-9029-2c9978c2f083"));

            using (Assert.EnterMultipleScope())
            {
                Assert.That(feature.AliasIds, Is.Empty);
                Assert.That(feature.ElementId, Is.EqualTo("00a6ef10-d3dc-4741-9029-2c9978c2f083"));
                Assert.That(feature.IsAbstract, Is.False);
                Assert.That(feature.IsComposite, Is.False);
                Assert.That(feature.IsSufficient, Is.False);
                Assert.That(feature.IsEnd, Is.False);
                Assert.That(feature.IsUnique, Is.True);
                Assert.That(feature.DeclaredName, Is.Null);
                Assert.That(feature.OwnedRelationship, Is.Empty);
                Assert.That(feature.OwningRelationship, Is.EqualTo(Guid.Parse("8a780d8b-61a6-472b-8b80-2564aa9f7c36")));
                Assert.That(feature.DeclaredShortName, Is.Null);
                Assert.That(feature.Direction, Is.EqualTo(FeatureDirectionKind.Out));
            }
        }

        [Test]
        [TestCase(false)]
        [TestCase(true)]
        public void Verify_that_projects_from_restapi_json_can_be_deserialized(bool shouldDeserializeDerivedProperties)
        {
            var fileName = Path.Combine(TestContext.CurrentContext.WorkDirectory, "Data", "projects.json");
            using var stream = new FileStream(fileName, FileMode.Open, FileAccess.Read);

            var data = this.deSerializer.DeSerialize(stream, SerializationModeKind.JSON, SerializationTargetKind.PSM, shouldDeserializeDerivedProperties);

            Assert.That(data.Count(), Is.EqualTo(43));

            var project = data.OfType<Project>().Single(x => x.Id == Guid.Parse("000e9890-6935-43e6-a5d7-5d7cac601f4c"));

            using (Assert.EnterMultipleScope())
            {
                Assert.That(project.DefaultBranch, Is.EqualTo(Guid.Parse("c294a463-6c9c-47a8-b592-01252c5ab2a7")));
                Assert.That(project.Name, Is.EqualTo("7b-Variant Configurations Mon Mar 13 17:54:29 EDT 2023"));
                Assert.That(project.Description, Is.Null);
            }
        }

        [Test]
        [TestCase(false)]
        [TestCase(true)]
        public void Verify_that_particular_project_from_restapi_json_can_be_deserialized(bool shouldDeserializeDerivedProperties)
        {
            var fileName = Path.Combine(TestContext.CurrentContext.WorkDirectory, "Data", "projects.000e9890-6935-43e6-a5d7-5d7cac601f4c.json");
            using var stream = new FileStream(fileName, FileMode.Open, FileAccess.Read);

            var data = this.deSerializer.DeSerialize(stream, SerializationModeKind.JSON, SerializationTargetKind.PSM, shouldDeserializeDerivedProperties);

            Assert.That(data.Count(), Is.EqualTo(1));

            var project = data.OfType<Project>().Single(x => x.Id == Guid.Parse("000e9890-6935-43e6-a5d7-5d7cac601f4c"));

            using (Assert.EnterMultipleScope())
            {
                Assert.That(project.DefaultBranch, Is.EqualTo(Guid.Parse("c294a463-6c9c-47a8-b592-01252c5ab2a7")));
                Assert.That(project.Name, Is.EqualTo("7b-Variant Configurations Mon Mar 13 17:54:29 EDT 2023"));
                Assert.That(project.Description, Is.Null);
            }
        }

        [Test]
        [TestCase(false)]
        [TestCase(true)]
        public void Verify_that_particular_project_and_commits_from_restapi_json_can_be_deserialized(bool shouldDeserializeDerivedProperties)
        {
            var fileName = Path.Combine(TestContext.CurrentContext.WorkDirectory, "Data", "projects.000e9890-6935-43e6-a5d7-5d7cac601f4c.commits.json");
            using var stream = new FileStream(fileName, FileMode.Open, FileAccess.Read);

            var data = this.deSerializer.DeSerialize(stream, SerializationModeKind.JSON, SerializationTargetKind.PSM, shouldDeserializeDerivedProperties);

            Assert.That(data.Count(), Is.EqualTo(1));

            var firstCommit = data.OfType<Commit>().Single(x => x.Id == Guid.Parse("6d7ad9fd-6520-4ff2-885b-8c5c129e6c27"));

            using (Assert.EnterMultipleScope())
            {
                Assert.That(firstCommit.OwningProject, Is.EqualTo(Guid.Parse("000e9890-6935-43e6-a5d7-5d7cac601f4c")));
                Assert.That(firstCommit.PreviousCommit, Is.EqualTo(Guid.Empty));
                Assert.That(firstCommit.Description, Is.Null);
                Assert.That(firstCommit.Created, Is.EqualTo(DateTime.Parse("2023-03-13T17:53:59.111354-04:00")));
            }
        }

        [Test]
        [TestCase(false)]
        [TestCase(true)]
        public void Verify_that_particular_project_and_particular_commit_from_restapi_json_can_be_deserialized(bool shouldDeserializeDerivedProperties)
        {
            var fileName = Path.Combine(TestContext.CurrentContext.WorkDirectory, "Data", "projects.000e9890-6935-43e6-a5d7-5d7cac601f4c.commits.6d7ad9fd-6520-4ff2-885b-8c5c129e6c27.json");
            using var stream = new FileStream(fileName, FileMode.Open, FileAccess.Read);

            var data = this.deSerializer.DeSerialize(stream, SerializationModeKind.JSON, SerializationTargetKind.PSM, shouldDeserializeDerivedProperties);

            Assert.That(data.Count(), Is.EqualTo(1));

            var firstCommit = data.OfType<Commit>().Single(x => x.Id == Guid.Parse("6d7ad9fd-6520-4ff2-885b-8c5c129e6c27"));

            using (Assert.EnterMultipleScope())
            {
                Assert.That(firstCommit.OwningProject, Is.EqualTo(Guid.Parse("000e9890-6935-43e6-a5d7-5d7cac601f4c")));
                Assert.That(firstCommit.PreviousCommit, Is.EqualTo(Guid.Empty));
                Assert.That(firstCommit.Description, Is.Null);
                Assert.That(firstCommit.Created, Is.EqualTo(DateTime.Parse("2023-03-13T17:53:59.111354-04:00")));
            }
        }

        [Test]
        [TestCase(false)]
        [TestCase(true)]
        public void Verify_that_particular_project_and_branches_from_restapi_json_can_be_deserialized(bool shouldDeserializeDerivedProperties)
        {
            var fileName = Path.Combine(TestContext.CurrentContext.WorkDirectory, "Data", "projects.000e9890-6935-43e6-a5d7-5d7cac601f4c.branches.json");
            using var stream = new FileStream(fileName, FileMode.Open, FileAccess.Read);

            var data = this.deSerializer.DeSerialize(stream, SerializationModeKind.JSON, SerializationTargetKind.PSM, shouldDeserializeDerivedProperties);

            Assert.That(data.Count(), Is.EqualTo(1));

            var branch = data.OfType<Branch>().Single(x => x.Id == Guid.Parse("c294a463-6c9c-47a8-b592-01252c5ab2a7"));

            using (Assert.EnterMultipleScope())
            {
                Assert.That(branch.OwningProject, Is.EqualTo(Guid.Parse("000e9890-6935-43e6-a5d7-5d7cac601f4c")));
                Assert.That(branch.Name, Is.EqualTo("main"));
                Assert.That(branch.Description, Is.Null);
                Assert.That(branch.Head, Is.EqualTo(Guid.Parse("6d7ad9fd-6520-4ff2-885b-8c5c129e6c27")));
                Assert.That(branch.Created, Is.EqualTo(DateTime.Parse("2023-03-13T17:53:50.188295-04:00")));
            }
        }

        [Test]
        public void VerifyDeSerialize()
        {
            using (Assert.EnterMultipleScope())
            {
                Assert.That(() => this.DeSerializeJson(string.Empty), Throws.InstanceOf<JsonException>(), "empty stream");
                Assert.That(() => this.DeSerializeJson("   "), Throws.InstanceOf<JsonException>(), "white space only");
                Assert.That(() => this.DeSerializeJson($"[{Element}] [{Element}]"), Throws.InstanceOf<JsonException>(), "trailing content");
                Assert.That(() => this.DeSerializeJson("42"), Throws.TypeOf<SerializationException>(), "scalar root");
                Assert.That(() => this.DeSerializeJson("[{}]"), Throws.TypeOf<SerializationException>(), "element without an @type");
            }

            using (Assert.EnterMultipleScope())
            {
                Assert.That(this.DeSerializeJson("[]"), Has.Count.EqualTo(0), "empty array");
                Assert.That(this.DeSerializeJson($"[{Element}]"), Has.Count.EqualTo(1), "array root");
                Assert.That(this.DeSerializeJson(Element), Has.Count.EqualTo(1), "single object root");
                Assert.That(this.DeSerializeJson($"  [ {Element} ]  "), Has.Count.EqualTo(1), "surrounding white space");
                Assert.That(this.DeSerializeJson($"﻿[{Element}]"), Has.Count.EqualTo(1), "utf-8 byte order mark");
                Assert.That(this.DeSerializeJson($"[{Element}]", dribble: true), Has.Count.EqualTo(1), "non seekable stream that dribbles bytes");
            }

            var feature = (IFeature)this.DeSerializeJson($"﻿[{Element}]")[0];

            using (Assert.EnterMultipleScope())
            {
                Assert.That(feature.Id, Is.EqualTo(ElementIdentifier));
                Assert.That(feature.ElementId, Is.EqualTo("00a6ef10-d3dc-4741-9029-2c9978c2f083"));
            }

            var grown = this.DeSerializeJson(CreateLargePayload(LargePayloadElementCount), dribble: true, chunkSize: 8192);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(grown, Has.Count.EqualTo(LargePayloadElementCount));
                Assert.That(grown[0].Id, Is.EqualTo(LargePayloadIdentifier(0)), "the first element survives the buffer growth");
                Assert.That(grown[^1].Id, Is.EqualTo(LargePayloadIdentifier(LargePayloadElementCount - 1)), "the last element survives the buffer growth");
                Assert.That(((IFeature)grown[LargePayloadElementCount / 2]).ElementId, Is.EqualTo(LargePayloadIdentifier(LargePayloadElementCount / 2).ToString()), "an element spanning a growth boundary is not corrupted");
            }
        }

        [Test]
        public async Task VerifyDeSerializeAsync()
        {
            await Assert.ThatAsync(async () => { await this.DeSerializeJsonAsync(string.Empty); }, Throws.InstanceOf<JsonException>());

            await Assert.ThatAsync(async () => { await this.DeSerializeJsonAsync($"[{Element}] [{Element}]"); }, Throws.InstanceOf<JsonException>());

            await Assert.ThatAsync(async () => { await this.DeSerializeJsonAsync("42"); }, Throws.TypeOf<SerializationException>());

            var emptyArray = await this.DeSerializeJsonAsync("[]");
            var arrayRoot = await this.DeSerializeJsonAsync($"[{Element}]");
            var singleObject = await this.DeSerializeJsonAsync(Element);
            var byteOrderMark = await this.DeSerializeJsonAsync($"﻿[{Element}]");
            var dribbled = await this.DeSerializeJsonAsync($"[{Element}]", dribble: true);
            var grown = await this.DeSerializeJsonAsync(CreateLargePayload(LargePayloadElementCount), dribble: true, chunkSize: 8192);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(emptyArray, Has.Count.EqualTo(0), "empty array");
                Assert.That(arrayRoot, Has.Count.EqualTo(1), "array root");
                Assert.That(singleObject, Has.Count.EqualTo(1), "single object root");
                Assert.That(byteOrderMark, Has.Count.EqualTo(1), "utf-8 byte order mark");
                Assert.That(dribbled, Has.Count.EqualTo(1), "non seekable stream that dribbles bytes");
                Assert.That(((IFeature)arrayRoot[0]).Id, Is.EqualTo(ElementIdentifier));
                Assert.That(grown, Has.Count.EqualTo(LargePayloadElementCount), "payload larger than the initial buffer");
                Assert.That(grown[^1].Id, Is.EqualTo(LargePayloadIdentifier(LargePayloadElementCount - 1)), "the last element survives the buffer growth");
            }
        }

        /// <summary>
        /// Deserializes the provided json
        /// </summary>
        /// <param name="json">
        /// the json to deserialize
        /// </param>
        /// <param name="dribble">
        /// when true the json is presented through a non seekable stream
        /// </param>
        /// <param name="chunkSize">
        /// the number of bytes that the non seekable stream yields per read
        /// </param>
        /// <returns>
        /// the deserialized <see cref="IData"/> items
        /// </returns>
        private List<IData> DeSerializeJson(string json, bool dribble = false, int chunkSize = 1)
        {
            using var stream = CreateStream(json, dribble, chunkSize);

            return this.deSerializer.DeSerialize(stream, SerializationModeKind.JSON, SerializationTargetKind.CORE, false).ToList();
        }

        /// <summary>
        /// Asynchronously deserializes the provided json
        /// </summary>
        /// <param name="json">
        /// the json to deserialize
        /// </param>
        /// <param name="dribble">
        /// when true the json is presented through a non seekable stream
        /// </param>
        /// <param name="chunkSize">
        /// the number of bytes that the non seekable stream yields per read
        /// </param>
        /// <returns>
        /// the deserialized <see cref="IData"/> items
        /// </returns>
        private async Task<List<IData>> DeSerializeJsonAsync(string json, bool dribble = false, int chunkSize = 1)
        {
            await using var stream = CreateStream(json, dribble, chunkSize);

            var data = await this.deSerializer.DeSerializeAsync(stream, SerializationModeKind.JSON, SerializationTargetKind.CORE, false, CancellationToken.None);

            return data.ToList();
        }

        /// <summary>
        /// Creates the input <see cref="Stream"/> for the provided json
        /// </summary>
        /// <param name="json">
        /// the json that the stream exposes
        /// </param>
        /// <param name="dribble">
        /// when true a non seekable stream is returned
        /// </param>
        /// <param name="chunkSize">
        /// the number of bytes that the non seekable stream yields per read
        /// </param>
        /// <returns>
        /// the input <see cref="Stream"/>
        /// </returns>
        private static Stream CreateStream(string json, bool dribble, int chunkSize)
        {
            var bytes = new UTF8Encoding(false).GetBytes(json);

            return dribble ? new DribblingStream(bytes, chunkSize) : new MemoryStream(bytes, false);
        }

        /// <summary>
        /// Builds a json array of <paramref name="elementCount"/> distinct elements
        /// </summary>
        /// <param name="elementCount">
        /// the number of elements the array carries
        /// </param>
        /// <returns>
        /// the json payload
        /// </returns>
        /// <remarks>
        /// Every element carries its own identifier so that a payload that was assembled across several
        /// growths of the pooled input buffer can be shown to be free of corruption, rather than merely
        /// having the expected number of elements.
        /// </remarks>
        private static string CreateLargePayload(int elementCount)
        {
            var builder = new StringBuilder();

            builder.Append('[');

            for (var elementIndex = 0; elementIndex < elementCount; elementIndex++)
            {
                if (elementIndex > 0)
                {
                    builder.Append(',');
                }

                var identifier = LargePayloadIdentifier(elementIndex);

                builder.Append("{\"@type\":\"Feature\",\"@id\":\"").Append(identifier).Append("\",\"elementId\":\"").Append(identifier).Append("\"}");
            }

            builder.Append(']');

            return builder.ToString();
        }

        /// <summary>
        /// Computes the identifier that the element at <paramref name="elementIndex"/> of the payload built by
        /// <see cref="CreateLargePayload"/> carries
        /// </summary>
        /// <param name="elementIndex">
        /// the index of the element
        /// </param>
        /// <returns>
        /// the identifier of the element
        /// </returns>
        private static Guid LargePayloadIdentifier(int elementIndex)
        {
            return new Guid(elementIndex, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
        }

        /// <summary>
        /// A non seekable <see cref="Stream"/> that yields a single byte per read, so that the buffer growth and
        /// continuation handling of the deserializer's read loop is exercised
        /// </summary>
        private sealed class DribblingStream : Stream
        {
            /// <summary>
            /// The bytes that the stream exposes
            /// </summary>
            private readonly byte[] bytes;

            /// <summary>
            /// The number of bytes yielded per read
            /// </summary>
            private readonly int chunkSize;

            /// <summary>
            /// The index of the next byte to yield
            /// </summary>
            private int index;

            /// <summary>
            /// Initializes a new instance of the <see cref="DribblingStream"/> class.
            /// </summary>
            /// <param name="bytes">
            /// The bytes that the stream exposes
            /// </param>
            /// <param name="chunkSize">
            /// The number of bytes yielded per read
            /// </param>
            public DribblingStream(byte[] bytes, int chunkSize)
            {
                this.bytes = bytes;
                this.chunkSize = chunkSize;
            }

            /// <inheritdoc/>
            public override bool CanRead => true;

            /// <inheritdoc/>
            public override bool CanSeek => false;

            /// <inheritdoc/>
            public override bool CanWrite => false;

            /// <inheritdoc/>
            public override long Length => throw new NotSupportedException();

            /// <inheritdoc/>
            public override long Position
            {
                get => throw new NotSupportedException();
                set => throw new NotSupportedException();
            }

            /// <inheritdoc/>
            public override void Flush()
            {
            }

            /// <inheritdoc/>
            public override int Read(byte[] buffer, int offset, int count)
            {
                if (this.index == this.bytes.Length || count == 0)
                {
                    return 0;
                }

                var read = Math.Min(Math.Min(this.chunkSize, count), this.bytes.Length - this.index);

                Buffer.BlockCopy(this.bytes, this.index, buffer, offset, read);

                this.index += read;

                return read;
            }

            /// <inheritdoc/>
            public override long Seek(long offset, SeekOrigin origin) => throw new NotSupportedException();

            /// <inheritdoc/>
            public override void SetLength(long value) => throw new NotSupportedException();

            /// <inheritdoc/>
            public override void Write(byte[] buffer, int offset, int count) => throw new NotSupportedException();
        }
    }
}
