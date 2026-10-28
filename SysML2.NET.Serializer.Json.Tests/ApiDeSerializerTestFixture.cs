// ------------------------------------------------------------------------------------------------
// <copyright file="ApiDeSerializerTestFixture.cs" company="Starion Group S.A.">
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

    using Microsoft.Extensions.Logging;

    using NUnit.Framework;

    using SysML2.NET.Common;
    using SysML2.NET.Core.DTO.Kernel.Packages;
    using SysML2.NET.PIM.DTO;

    /// <summary>
    /// Suite of tests for the PIM deserializers (<see cref="Branch"/>, <see cref="Tag"/>, <see cref="Commit"/>
    /// and <see cref="DataVersion"/>) reached through the <see cref="DeSerializer"/>
    /// </summary>
    [TestFixture]
    public class ApiDeSerializerTestFixture
    {
        private static readonly Guid ProjectId = Guid.Parse("000e9890-6935-43e6-a5d7-5d7cac601f4c");

        private static readonly Guid CommitId = Guid.Parse("6d7ad9fd-6520-4ff2-885b-8c5c129e6c27");

        private static readonly Guid OtherCommitId = Guid.Parse("1e4c2a06-0f2f-4a3c-9d64-0f4b2f6d9a11");

        private static readonly Guid IdentityId = Guid.Parse("34fbb3eb-9074-4bf6-ad0a-5a577e873943");

        private ILoggerFactory loggerFactory;

        private DeSerializer deSerializer;

        [SetUp]
        public void SetUp()
        {
            // a trace-level logger so that the diagnostic branches of the deserializers execute as well
            this.loggerFactory = LoggerFactory.Create(builder => builder.SetMinimumLevel(LogLevel.Trace).AddConsole());
            this.deSerializer = new DeSerializer(this.loggerFactory);
        }

        [TearDown]
        public void TearDown()
        {
            this.loggerFactory.Dispose();
        }

        [Test]
        public void VerifyBranchDeserializer()
        {
            Assert.That(() => this.DeSerializeSingle<Branch>("""{"@type":"Branch","@id":null}"""), Throws.TypeOf<JsonException>());

            var branch = this.DeSerializeSingle<Branch>(
                """
                {"@type":"Branch","@id":"c294a463-6c9c-47a8-b592-01252c5ab2a7","alias":["main","trunk"],
                 "created":"2023-03-13T21:53:50Z","deleted":"2024-01-02T03:04:05Z","description":"the main line",
                 "head":{"@id":"6d7ad9fd-6520-4ff2-885b-8c5c129e6c27"},"name":"main",
                 "owningProject":{"@id":"000e9890-6935-43e6-a5d7-5d7cac601f4c"},"resourceIdentifier":"http://www.stariongroup.eu/branch"}
                """);

            List<string> expectedAlias = ["main", "trunk"];

            using (Assert.EnterMultipleScope())
            {
                Assert.That(branch.Id, Is.EqualTo(Guid.Parse("c294a463-6c9c-47a8-b592-01252c5ab2a7")));
                Assert.That(branch.Alias, Is.EqualTo(expectedAlias));
                Assert.That(branch.Created, Is.EqualTo(new DateTime(2023, 3, 13, 21, 53, 50, DateTimeKind.Utc)));
                Assert.That(branch.Deleted, Is.EqualTo(new DateTime(2024, 1, 2, 3, 4, 5, DateTimeKind.Utc)));
                Assert.That(branch.Description, Is.EqualTo("the main line"));
                Assert.That(branch.Head, Is.EqualTo(CommitId));
                Assert.That(branch.ReferencedCommit, Is.EqualTo(CommitId), "head redefines referencedCommit");
                Assert.That(branch.Name, Is.EqualTo("main"));
                Assert.That(branch.OwningProject, Is.EqualTo(ProjectId));
                Assert.That(branch.ResourceIdentifier, Is.EqualTo("http://www.stariongroup.eu/branch"));
            }

            // the reference implementation emits referencedCommit as well; it is accepted when head is absent
            branch = this.DeSerializeSingle<Branch>("""{"@type":"Branch","@id":"c294a463-6c9c-47a8-b592-01252c5ab2a7","referencedCommit":{"@id":"6d7ad9fd-6520-4ff2-885b-8c5c129e6c27"}}""");

            Assert.That(branch.Head, Is.EqualTo(CommitId));

            // when both are present, head takes precedence
            branch = this.DeSerializeSingle<Branch>("""{"@type":"Branch","@id":"c294a463-6c9c-47a8-b592-01252c5ab2a7","head":{"@id":"6d7ad9fd-6520-4ff2-885b-8c5c129e6c27"},"referencedCommit":{"@id":"1e4c2a06-0f2f-4a3c-9d64-0f4b2f6d9a11"}}""");

            Assert.That(branch.Head, Is.EqualTo(CommitId));

            // null references and null strings leave the defaults in place
            branch = this.DeSerializeSingle<Branch>("""{"@type":"Branch","@id":"c294a463-6c9c-47a8-b592-01252c5ab2a7","deleted":null,"description":null,"head":null,"name":null,"owningProject":null}""");

            using (Assert.EnterMultipleScope())
            {
                Assert.That(branch.Deleted, Is.Null);
                Assert.That(branch.Description, Is.Null);
                Assert.That(branch.Head, Is.EqualTo(Guid.Empty));
                Assert.That(branch.Name, Is.Null);
                Assert.That(branch.OwningProject, Is.EqualTo(Guid.Empty));
            }

            // every optional property absent: nothing throws and the defaults remain
            branch = this.DeSerializeSingle<Branch>("""{"@type":"Branch","@id":"c294a463-6c9c-47a8-b592-01252c5ab2a7"}""");

            using (Assert.EnterMultipleScope())
            {
                Assert.That(branch.Alias, Is.Empty);
                Assert.That(branch.Created, Is.EqualTo(default(DateTime)));
                Assert.That(branch.Deleted, Is.Null);
                Assert.That(branch.Head, Is.EqualTo(Guid.Empty));
                Assert.That(branch.OwningProject, Is.EqualTo(Guid.Empty));
                Assert.That(branch.ResourceIdentifier, Is.Null);
            }
        }

        [Test]
        public void VerifyTagDeserializer()
        {
            Assert.That(() => this.DeSerializeSingle<Tag>("""{"@type":"Tag","@id":null}"""), Throws.TypeOf<JsonException>());

            var tag = this.DeSerializeSingle<Tag>(
                """
                {"@type":"Tag","@id":"5f0d2c1e-8a4b-4c3d-9e2f-1a2b3c4d5e6f","alias":["release","v1"],
                 "created":"2023-03-13T21:53:50Z","deleted":"2024-01-02T03:04:05Z","description":"first release",
                 "name":"v1.0","owningProject":{"@id":"000e9890-6935-43e6-a5d7-5d7cac601f4c"},
                 "resourceIdentifier":"http://www.stariongroup.eu/tag","taggedCommit":{"@id":"6d7ad9fd-6520-4ff2-885b-8c5c129e6c27"}}
                """);

            List<string> expectedAlias = ["release", "v1"];

            using (Assert.EnterMultipleScope())
            {
                Assert.That(tag.Id, Is.EqualTo(Guid.Parse("5f0d2c1e-8a4b-4c3d-9e2f-1a2b3c4d5e6f")));
                Assert.That(tag.Alias, Is.EqualTo(expectedAlias));
                Assert.That(tag.Created, Is.EqualTo(new DateTime(2023, 3, 13, 21, 53, 50, DateTimeKind.Utc)));
                Assert.That(tag.Deleted, Is.EqualTo(new DateTime(2024, 1, 2, 3, 4, 5, DateTimeKind.Utc)));
                Assert.That(tag.Description, Is.EqualTo("first release"));
                Assert.That(tag.Name, Is.EqualTo("v1.0"));
                Assert.That(tag.OwningProject, Is.EqualTo(ProjectId));
                Assert.That(tag.ResourceIdentifier, Is.EqualTo("http://www.stariongroup.eu/tag"));
                Assert.That(tag.TaggedCommit, Is.EqualTo(CommitId));
                Assert.That(tag.ReferencedCommit, Is.EqualTo(CommitId), "taggedCommit redefines referencedCommit");
            }

            // referencedCommit is accepted when taggedCommit is absent
            tag = this.DeSerializeSingle<Tag>("""{"@type":"Tag","@id":"5f0d2c1e-8a4b-4c3d-9e2f-1a2b3c4d5e6f","referencedCommit":{"@id":"6d7ad9fd-6520-4ff2-885b-8c5c129e6c27"}}""");

            Assert.That(tag.TaggedCommit, Is.EqualTo(CommitId));

            // when both are present, taggedCommit takes precedence
            tag = this.DeSerializeSingle<Tag>("""{"@type":"Tag","@id":"5f0d2c1e-8a4b-4c3d-9e2f-1a2b3c4d5e6f","taggedCommit":{"@id":"6d7ad9fd-6520-4ff2-885b-8c5c129e6c27"},"referencedCommit":{"@id":"1e4c2a06-0f2f-4a3c-9d64-0f4b2f6d9a11"}}""");

            Assert.That(tag.TaggedCommit, Is.EqualTo(CommitId));

            // null references and null strings leave the defaults in place
            tag = this.DeSerializeSingle<Tag>("""{"@type":"Tag","@id":"5f0d2c1e-8a4b-4c3d-9e2f-1a2b3c4d5e6f","deleted":null,"description":null,"name":null,"owningProject":null,"taggedCommit":null}""");

            using (Assert.EnterMultipleScope())
            {
                Assert.That(tag.Deleted, Is.Null);
                Assert.That(tag.Description, Is.Null);
                Assert.That(tag.Name, Is.Null);
                Assert.That(tag.OwningProject, Is.EqualTo(Guid.Empty));
                Assert.That(tag.TaggedCommit, Is.EqualTo(Guid.Empty));
            }

            // every optional property absent: nothing throws and the defaults remain
            tag = this.DeSerializeSingle<Tag>("""{"@type":"Tag","@id":"5f0d2c1e-8a4b-4c3d-9e2f-1a2b3c4d5e6f"}""");

            using (Assert.EnterMultipleScope())
            {
                Assert.That(tag.Alias, Is.Empty);
                Assert.That(tag.Created, Is.EqualTo(default(DateTime)));
                Assert.That(tag.Deleted, Is.Null);
                Assert.That(tag.OwningProject, Is.EqualTo(Guid.Empty));
                Assert.That(tag.ResourceIdentifier, Is.Null);
                Assert.That(tag.TaggedCommit, Is.EqualTo(Guid.Empty));
            }
        }

        [Test]
        public void VerifyCommitDeserializer()
        {
            Assert.That(() => this.DeSerializeSingle<Commit>("""{"@type":"Commit","@id":null}"""), Throws.TypeOf<JsonException>());

            var commit = this.DeSerializeSingle<Commit>(
                """
                {"@type":"Commit","@id":"6d7ad9fd-6520-4ff2-885b-8c5c129e6c27","alias":["initial"],"created":"2023-03-13T21:53:59Z",
                 "description":"initial commit","owningProject":{"@id":"000e9890-6935-43e6-a5d7-5d7cac601f4c"},
                 "previousCommit":[{"@id":"1e4c2a06-0f2f-4a3c-9d64-0f4b2f6d9a11"},{"@id":"2b7d5e18-6a4f-4d1e-8c32-7d5a1c8e4b22"}],
                 "change":[
                   {"@id":"00100657-7214-4199-ac74-66a250ef1f91"},
                   {"@type":"DataVersion","@id":"00200657-7214-4199-ac74-66a250ef1f91","identity":{"@type":"DataIdentity","@id":"44444444-4444-4444-4444-444444444444"},"payload":null},
                   {"@type":"DataVersion"}
                 ],
                 "resourceIdentifier":"http://www.stariongroup.eu/commit"}
                """);

            List<string> expectedAlias = ["initial"];
            List<Guid> expectedParents = [OtherCommitId, Guid.Parse("2b7d5e18-6a4f-4d1e-8c32-7d5a1c8e4b22")];
            List<Guid> expectedChange = [Guid.Parse("00100657-7214-4199-ac74-66a250ef1f91"), Guid.Parse("00200657-7214-4199-ac74-66a250ef1f91")];

            using (Assert.EnterMultipleScope())
            {
                Assert.That(commit.Id, Is.EqualTo(CommitId));
                Assert.That(commit.Alias, Is.EqualTo(expectedAlias));
                Assert.That(commit.Created, Is.EqualTo(new DateTime(2023, 3, 13, 21, 53, 59, DateTimeKind.Utc)));
                Assert.That(commit.Description, Is.EqualTo("initial commit"));
                Assert.That(commit.OwningProject, Is.EqualTo(ProjectId));
                Assert.That(commit.PreviousCommits, Is.EqualTo(expectedParents));
                Assert.That(commit.Change, Is.EqualTo(expectedChange), "a bare reference and an embedded DataVersion both contribute their identifier, an item without @id is skipped");
                Assert.That(commit.ResourceIdentifier, Is.EqualTo("http://www.stariongroup.eu/commit"));
            }

            // the reference implementation emits a single object instead of an array for previousCommit
            commit = this.DeSerializeSingle<Commit>("""{"@type":"Commit","@id":"6d7ad9fd-6520-4ff2-885b-8c5c129e6c27","previousCommit":{"@id":"1e4c2a06-0f2f-4a3c-9d64-0f4b2f6d9a11"}}""");

            List<Guid> expectedSingleParent = [OtherCommitId];

            Assert.That(commit.PreviousCommits, Is.EqualTo(expectedSingleParent));

            // null references, null strings and a non-array change set leave the defaults in place
            commit = this.DeSerializeSingle<Commit>("""{"@type":"Commit","@id":"6d7ad9fd-6520-4ff2-885b-8c5c129e6c27","description":null,"owningProject":null,"previousCommit":null,"change":null}""");

            using (Assert.EnterMultipleScope())
            {
                Assert.That(commit.Description, Is.Null);
                Assert.That(commit.OwningProject, Is.EqualTo(Guid.Empty));
                Assert.That(commit.PreviousCommits, Is.Empty);
                Assert.That(commit.Change, Is.Empty);
            }

            // every optional property absent: nothing throws and the defaults remain
            commit = this.DeSerializeSingle<Commit>("""{"@type":"Commit","@id":"6d7ad9fd-6520-4ff2-885b-8c5c129e6c27"}""");

            using (Assert.EnterMultipleScope())
            {
                Assert.That(commit.Alias, Is.Empty);
                Assert.That(commit.Created, Is.EqualTo(default(DateTime)));
                Assert.That(commit.OwningProject, Is.EqualTo(Guid.Empty));
                Assert.That(commit.PreviousCommits, Is.Empty);
                Assert.That(commit.Change, Is.Empty);
                Assert.That(commit.ResourceIdentifier, Is.Null);
            }
        }

        [Test]
        public void VerifyDataVersionDeserializer()
        {
            Assert.That(() => this.DeSerializeSingle<DataVersion>("""{"@type":"DataVersion","@id":null}"""), Throws.TypeOf<JsonException>());

            var dataVersion = this.DeSerializeSingle<DataVersion>(
                """
                {"@type":"DataVersion","@id":"00100657-7214-4199-ac74-66a250ef1f91","alias":["first version"],
                 "commit":{"@id":"6d7ad9fd-6520-4ff2-885b-8c5c129e6c27"},"description":"the package is created",
                 "identity":{"@type":"DataIdentity","@id":"34fbb3eb-9074-4bf6-ad0a-5a577e873943","description":"identity of the package"},
                 "payload":{"@type":"Package","@id":"34fbb3eb-9074-4bf6-ad0a-5a577e873943","declaredName":"pkg"},
                 "resourceIdentifier":"http://www.stariongroup.eu/dataversion"}
                """);

            List<string> expectedAlias = ["first version"];

            using (Assert.EnterMultipleScope())
            {
                Assert.That(dataVersion.Id, Is.EqualTo(Guid.Parse("00100657-7214-4199-ac74-66a250ef1f91")));
                Assert.That(dataVersion.Alias, Is.EqualTo(expectedAlias));
                Assert.That(dataVersion.Commit, Is.EqualTo(CommitId));
                Assert.That(dataVersion.Description, Is.EqualTo("the package is created"));
                Assert.That(dataVersion.Identity.Id, Is.EqualTo(IdentityId));
                Assert.That(dataVersion.Identity.Description, Is.EqualTo("identity of the package"));
                Assert.That(dataVersion.Payload, Is.TypeOf<Package>());
                Assert.That(((Package)dataVersion.Payload).DeclaredName, Is.EqualTo("pkg"));
                Assert.That(dataVersion.ResourceIdentifier, Is.EqualTo("http://www.stariongroup.eu/dataversion"));
            }

            // a deletion: the payload is null, and null references leave the defaults in place
            dataVersion = this.DeSerializeSingle<DataVersion>("""{"@type":"DataVersion","@id":"00100657-7214-4199-ac74-66a250ef1f91","commit":null,"description":null,"identity":null,"payload":null}""");

            using (Assert.EnterMultipleScope())
            {
                Assert.That(dataVersion.Commit, Is.EqualTo(Guid.Empty));
                Assert.That(dataVersion.Description, Is.Null);
                Assert.That(dataVersion.Identity, Is.Null);
                Assert.That(dataVersion.Payload, Is.Null);
            }

            // a payload without @type cannot be dispatched to a core deserializer and is ignored
            dataVersion = this.DeSerializeSingle<DataVersion>("""{"@type":"DataVersion","@id":"00100657-7214-4199-ac74-66a250ef1f91","payload":{"@id":"34fbb3eb-9074-4bf6-ad0a-5a577e873943"}}""");

            Assert.That(dataVersion.Payload, Is.Null);

            // every optional property absent: nothing throws and the defaults remain
            dataVersion = this.DeSerializeSingle<DataVersion>("""{"@type":"DataVersion","@id":"00100657-7214-4199-ac74-66a250ef1f91"}""");

            using (Assert.EnterMultipleScope())
            {
                Assert.That(dataVersion.Alias, Is.Empty);
                Assert.That(dataVersion.Commit, Is.EqualTo(Guid.Empty));
                Assert.That(dataVersion.Identity, Is.Null);
                Assert.That(dataVersion.Payload, Is.Null);
                Assert.That(dataVersion.ResourceIdentifier, Is.Null);
            }
        }

        /// <summary>
        /// Deserializes a single PIM record from the provided json text
        /// </summary>
        /// <typeparam name="T">
        /// The expected type of the record
        /// </typeparam>
        /// <param name="json">
        /// The json text that carries exactly one record
        /// </param>
        /// <returns>
        /// The deserialized record
        /// </returns>
        private T DeSerializeSingle<T>(string json) where T : IIdentified
        {
            using var stream = new MemoryStream(Encoding.UTF8.GetBytes(json));

            return this.deSerializer.DeSerialize(stream, SerializationModeKind.JSON, SerializationTargetKind.PSM, false).OfType<T>().Single();
        }
    }
}
