// ------------------------------------------------------------------------------------------------
// <copyright file="ApiSerializerTestFixture.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.Serializer.Json.Tests
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Text.Json;
    using System.Threading;
    using System.Threading.Tasks;

    using NUnit.Framework;

    using SysML2.NET.Common;
    using SysML2.NET.Core.DTO.Kernel.Packages;
    using SysML2.NET.PIM.DTO;
    using SysML2.NET.Serializer.Json;

    [TestFixture]
    public class ApiSerializerTestFixture
    {
        private ISerializer serializer;

        private Project project;

        private Branch branch;

        private Commit commit;

        [SetUp]
        public void SetUp()
        {
            this.serializer = new Serializer();

            this.CreateTestData();
        }

        private void CreateTestData()
        {
            this.project = new Project
            {
                Id = Guid.Parse("9b0e1914-3241-461e-b9ee-a3ff5120de4e"),
                Alias = new List<string> { "project alias 1", "project alias 2"},
                Created = new DateTime(1976, 8, 20, 0, 0, 0, DateTimeKind.Utc),
                DefaultBranch = Guid.Parse("a910a705-7fbe-415f-9cbb-624bfadf6c20"),
                Description = "this is a description",
                Name = "test project",
                ResourceIdentifier = "http://www.stariongroup.eu/project",
            };

            this.commit = new Commit
            {
                Id = Guid.Parse("94e5b40e-741e-49ca-bd7f-f3138c071bf9"),
                Alias = new List<string> { "commit alias 1", "commit alias 2" },
                Created = new DateTime(1976, 8, 20, 0, 0, 0, DateTimeKind.Utc),
                Description = "",
                OwningProject = Guid.Parse("9b0e1914-3241-461e-b9ee-a3ff5120de4e"),
                PreviousCommits = [Guid.Parse("8d3c3f31-2b2f-4f4b-8f35-6b2e0f4a4a11")],
                ResourceIdentifier = "http://www.stariongroup.eu/commit",
            };

            this.branch = new Branch
            {
                Id = Guid.Parse("a910a705-7fbe-415f-9cbb-624bfadf6c20"),
                Alias = null,
                Created = new DateTime(1976, 8, 20, 0, 0, 0, DateTimeKind.Utc),
                Description = "branch description",
                Head = Guid.Parse("94e5b40e-741e-49ca-bd7f-f3138c071bf9"),
                Name = "branch name",
                OwningProject = Guid.Parse("9b0e1914-3241-461e-b9ee-a3ff5120de4e"),
                ResourceIdentifier = "http://http://www.stariongroup.eu/branch",
            };
        }

        [Test]
        [TestCase(false)]
        [TestCase(true)]
        public void Verify_that_PIM_IDatas_can_be_serialized(bool includeDerivedProperties)
        {
            var dataItems = new List<IIdentified> { this.project, this.commit, this.branch };

            var stream = new MemoryStream();
            var jsonWriterOptions = new JsonWriterOptions { Indented = true };

            Assert.That(() => this.serializer.Serialize(dataItems, SerializationModeKind.JSON, includeDerivedProperties, stream, jsonWriterOptions), Throws.Nothing);

            var json = Encoding.UTF8.GetString(stream.ToArray());
            Console.WriteLine(json);
        }

        [Test]
        [TestCase(false)]
        [TestCase(true)]
        public void Verify_that_PIM_IData_can_be_serialized(bool includeDerivedProperties)
        {
            var jsonWriterOptions = new JsonWriterOptions { Indented = true };

            var stream = new MemoryStream();
            Assert.That(() => this.serializer.Serialize(this.project, SerializationModeKind.JSON, includeDerivedProperties, stream, jsonWriterOptions), Throws.Nothing);

            var json = Encoding.UTF8.GetString(stream.ToArray());
            Console.WriteLine(json);

            stream = new MemoryStream();
            Assert.That(() => this.serializer.Serialize(this.project, SerializationModeKind.JSON, includeDerivedProperties, stream, jsonWriterOptions), Throws.Nothing);

            json = Encoding.UTF8.GetString(stream.ToArray());
            Console.WriteLine(json);
        }

        [Test]
        public void Verify_that_merge_commit_with_change_set_can_be_roundtripped()
        {
            var firstParent = Guid.Parse("11111111-1111-1111-1111-111111111111");
            var secondParent = Guid.Parse("22222222-2222-2222-2222-222222222222");
            List<Guid> expectedParents = [firstParent, secondParent];

            this.commit.PreviousCommits = [firstParent, secondParent];

            var dataVersionId = Guid.Parse("33333333-3333-3333-3333-333333333333");
            List<Guid> expectedChange = [dataVersionId];

            this.commit.Change = [dataVersionId];

            var stream = new MemoryStream();
            this.serializer.Serialize(this.commit, SerializationModeKind.JSON, false, stream, new JsonWriterOptions());

            stream.Position = 0;
            var deSerializer = new DeSerializer();
            var data = deSerializer.DeSerialize(stream, SerializationModeKind.JSON, SerializationTargetKind.PSM, false).ToList();

            var roundtrippedCommit = data.OfType<Commit>().Single();

            using (Assert.EnterMultipleScope())
            {
                Assert.That(roundtrippedCommit.Id, Is.EqualTo(this.commit.Id));
                Assert.That(roundtrippedCommit.PreviousCommits, Is.EqualTo(expectedParents));
                Assert.That(roundtrippedCommit.Change, Is.EqualTo(expectedChange), "the change set is a list of DataVersion references");
            }
        }

        [Test]
        public void VerifyBranchSerializer()
        {
            Assert.That(() => this.serializer.Serialize(this.branch, SerializationModeKind.JSONLD, false, new MemoryStream(), new JsonWriterOptions()), Throws.TypeOf<NotImplementedException>());

            // the shared branch carries no alias list and has not been deleted
            var json = this.SerializeToJsonElement(this.branch);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(json.GetProperty("@type").GetString(), Is.EqualTo("Branch"));
                Assert.That(json.GetProperty("@id").GetGuid(), Is.EqualTo(this.branch.Id));
                Assert.That(json.GetProperty("alias").GetArrayLength(), Is.EqualTo(0));
                Assert.That(json.GetProperty("created").GetDateTime(), Is.EqualTo(this.branch.Created));
                Assert.That(json.GetProperty("deleted").ValueKind, Is.EqualTo(JsonValueKind.Null));
                Assert.That(json.GetProperty("description").GetString(), Is.EqualTo("branch description"));
                Assert.That(json.GetProperty("head").GetProperty("@id").GetGuid(), Is.EqualTo(this.branch.Head));
                Assert.That(json.GetProperty("referencedCommit").GetProperty("@id").GetGuid(), Is.EqualTo(this.branch.Head), "head redefines referencedCommit, both carry the same commit");
                Assert.That(json.GetProperty("name").GetString(), Is.EqualTo("branch name"));
                Assert.That(json.GetProperty("owningProject").GetProperty("@id").GetGuid(), Is.EqualTo(this.branch.OwningProject));
                Assert.That(json.GetProperty("resourceIdentifier").GetString(), Is.EqualTo(this.branch.ResourceIdentifier));
            }

            List<string> expectedAlias = ["main", "trunk"];
            this.branch.Alias = expectedAlias;
            this.branch.Deleted = new DateTime(2024, 1, 2, 3, 4, 5, DateTimeKind.Utc);

            json = this.SerializeToJsonElement(this.branch);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(json.GetProperty("alias").EnumerateArray().Select(item => item.GetString()), Is.EqualTo(expectedAlias));
                Assert.That(json.GetProperty("deleted").GetDateTime(), Is.EqualTo(this.branch.Deleted.Value));
            }
        }

        [Test]
        public void VerifyTagSerializer()
        {
            var tag = new Tag
            {
                Id = Guid.Parse("5f0d2c1e-8a4b-4c3d-9e2f-1a2b3c4d5e6f"),
                Alias = null,
                Created = new DateTime(1976, 8, 20, 0, 0, 0, DateTimeKind.Utc),
                Description = "tag description",
                Name = "v1.0",
                OwningProject = this.project.Id,
                TaggedCommit = this.commit.Id,
                ResourceIdentifier = "http://www.stariongroup.eu/tag",
            };

            Assert.That(() => this.serializer.Serialize(tag, SerializationModeKind.JSONLD, false, new MemoryStream(), new JsonWriterOptions()), Throws.TypeOf<NotImplementedException>());

            var json = this.SerializeToJsonElement(tag);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(json.GetProperty("@type").GetString(), Is.EqualTo("Tag"));
                Assert.That(json.GetProperty("@id").GetGuid(), Is.EqualTo(tag.Id));
                Assert.That(json.GetProperty("alias").GetArrayLength(), Is.EqualTo(0));
                Assert.That(json.GetProperty("created").GetDateTime(), Is.EqualTo(tag.Created));
                Assert.That(json.GetProperty("deleted").ValueKind, Is.EqualTo(JsonValueKind.Null));
                Assert.That(json.GetProperty("description").GetString(), Is.EqualTo("tag description"));
                Assert.That(json.GetProperty("name").GetString(), Is.EqualTo("v1.0"));
                Assert.That(json.GetProperty("owningProject").GetProperty("@id").GetGuid(), Is.EqualTo(this.project.Id));
                Assert.That(json.GetProperty("taggedCommit").GetProperty("@id").GetGuid(), Is.EqualTo(this.commit.Id));
                Assert.That(json.GetProperty("referencedCommit").GetProperty("@id").GetGuid(), Is.EqualTo(this.commit.Id), "taggedCommit redefines referencedCommit, both carry the same commit");
                Assert.That(json.GetProperty("resourceIdentifier").GetString(), Is.EqualTo("http://www.stariongroup.eu/tag"));
            }

            List<string> expectedAlias = ["release", "v1"];
            tag.Alias = expectedAlias;
            tag.Deleted = new DateTime(2024, 1, 2, 3, 4, 5, DateTimeKind.Utc);

            json = this.SerializeToJsonElement(tag);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(json.GetProperty("alias").EnumerateArray().Select(item => item.GetString()), Is.EqualTo(expectedAlias));
                Assert.That(json.GetProperty("deleted").GetDateTime(), Is.EqualTo(tag.Deleted.Value));
            }
        }

        [Test]
        public void VerifyDataVersionSerializer()
        {
            // a deletion: no identity has been attached yet and the payload is null
            var dataVersion = new DataVersion
            {
                Id = Guid.Parse("00100657-7214-4199-ac74-66a250ef1f91"),
                Alias = null,
                Commit = this.commit.Id,
                Description = "the package is deleted",
                Identity = null,
                Payload = null,
                ResourceIdentifier = "http://www.stariongroup.eu/dataversion",
            };

            Assert.That(() => this.serializer.Serialize(dataVersion, SerializationModeKind.JSONLD, false, new MemoryStream(), new JsonWriterOptions()), Throws.TypeOf<NotImplementedException>());

            var json = this.SerializeToJsonElement(dataVersion);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(json.GetProperty("@type").GetString(), Is.EqualTo("DataVersion"));
                Assert.That(json.GetProperty("@id").GetGuid(), Is.EqualTo(dataVersion.Id));
                Assert.That(json.GetProperty("alias").GetArrayLength(), Is.EqualTo(0));
                Assert.That(json.GetProperty("commit").GetProperty("@id").GetGuid(), Is.EqualTo(this.commit.Id));
                Assert.That(json.GetProperty("description").GetString(), Is.EqualTo("the package is deleted"));
                Assert.That(json.GetProperty("identity").ValueKind, Is.EqualTo(JsonValueKind.Null));
                Assert.That(json.GetProperty("payload").ValueKind, Is.EqualTo(JsonValueKind.Null), "a null payload represents a deletion");
                Assert.That(json.GetProperty("resourceIdentifier").GetString(), Is.EqualTo("http://www.stariongroup.eu/dataversion"));
            }

            // a creation: the identity and the core payload are written as nested objects
            var identityId = Guid.Parse("34fbb3eb-9074-4bf6-ad0a-5a577e873943");
            List<string> expectedAlias = ["first version"];
            dataVersion.Alias = expectedAlias;
            dataVersion.Identity = new DataIdentity { Id = identityId, Description = "identity of the package" };
            dataVersion.Payload = new Package { Id = identityId, DeclaredName = "pkg" };

            json = this.SerializeToJsonElement(dataVersion);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(json.GetProperty("alias").EnumerateArray().Select(item => item.GetString()), Is.EqualTo(expectedAlias));
                Assert.That(json.GetProperty("identity").GetProperty("@type").GetString(), Is.EqualTo("DataIdentity"));
                Assert.That(json.GetProperty("identity").GetProperty("@id").GetGuid(), Is.EqualTo(identityId));
                Assert.That(json.GetProperty("identity").GetProperty("description").GetString(), Is.EqualTo("identity of the package"));
                Assert.That(json.GetProperty("payload").GetProperty("@type").GetString(), Is.EqualTo("Package"));
                Assert.That(json.GetProperty("payload").GetProperty("@id").GetGuid(), Is.EqualTo(identityId));
                Assert.That(json.GetProperty("payload").GetProperty("declaredName").GetString(), Is.EqualTo("pkg"));
            }
        }

        [Test]
        [TestCase(false)]
        [TestCase(true)]
        public async Task Verify_that_PIM_IDatas_can_be_serialized_async(bool includeDerivedProperties)
        {
            var dataItems = new List<IIdentified> { this.project, this.commit, this.branch };
            var stream = new MemoryStream();
            var jsonWriterOptions = new JsonWriterOptions { Indented = true };

            var cts = new CancellationTokenSource();

            await Assert.ThatAsync(() => this.serializer.SerializeAsync(dataItems, SerializationModeKind.JSON, includeDerivedProperties, stream, jsonWriterOptions, cts.Token), Throws.Nothing);

            var json = Encoding.UTF8.GetString(stream.ToArray());
            Console.WriteLine(json);
            cts.Dispose();
        }

        [Test]
        [TestCase(false)]
        [TestCase(true)]
        public async Task Verify_that_PIM_IData_can_be_serialized_async(bool includeDerivedProperties)
        {
            var stream = new MemoryStream();
            var jsonWriterOptions = new JsonWriterOptions { Indented = true };

            var cts = new CancellationTokenSource();

            await Assert.ThatAsync(() => this.serializer.SerializeAsync(this.project, SerializationModeKind.JSON, includeDerivedProperties, stream, jsonWriterOptions, cts.Token), Throws.Nothing);

            var json = Encoding.UTF8.GetString(stream.ToArray());
            Console.WriteLine(json);
            cts.Dispose();
        }

        /// <summary>
        /// Serializes the provided PIM record and parses the written json back into a <see cref="JsonElement"/>
        /// </summary>
        /// <param name="dataItem">
        /// The record to serialize
        /// </param>
        /// <returns>
        /// The root <see cref="JsonElement"/> of the written json
        /// </returns>
        private JsonElement SerializeToJsonElement(IIdentified dataItem)
        {
            var stream = new MemoryStream();
            this.serializer.Serialize(dataItem, SerializationModeKind.JSON, false, stream, new JsonWriterOptions());

            using var document = JsonDocument.Parse(stream.ToArray());

            return document.RootElement.Clone();
        }
    }
}
