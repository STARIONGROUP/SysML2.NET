// -------------------------------------------------------------------------------------------------
// <copyright file="PsmSerializerTestFixture.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
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
    using System.Buffers;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using NUnit.Framework;

    using SysML2.NET.Core.Core.Types;
    using SysML2.NET.Core.DTO.Core.Features;
    using SysML2.NET.PSM.DTO;
    using SysML2.NET.PSM.Enumerations;

    [TestFixture]
    public class PsmSerializerTestFixture
    {
        private static readonly Guid ProjectIdentifier = Guid.Parse("9b0e1914-3241-461e-b9ee-a3ff5120de4e");

        private static readonly Guid BranchIdentifier = Guid.Parse("a910a705-7fbe-415f-9cbb-624bfadf6c20");

        private static readonly Guid DataVersionIdentifier = Guid.Parse("94e5b40e-741e-49ca-bd7f-f3138c071bf9");

        private static readonly Guid ElementIdentifier = Guid.Parse("00a6ef10-d3dc-4741-9029-2c9978c2f083");

        private MessagePack.Serializer serializer;

        private MessagePack.DeSerializer deSerializer;

        [SetUp]
        public void SetUp()
        {
            this.serializer = new MessagePack.Serializer();
            this.deSerializer = new MessagePack.DeSerializer();
        }

        [Test]
        public void VerifyRoundTrip()
        {
            var dataVersion = new DataVersion
            {
                Id = DataVersionIdentifier,
                Alias = ["first", "second"],
                Description = "the description",
                Name = "the name",
                Identity = new DataIdentity { Id = ElementIdentifier, Alias = ["identity alias"], Name = "the identity" },
                Payload = CreateCoreFeature()
            };

            var project = new Project
            {
                Id = ProjectIdentifier,
                Alias = ["alias"],
                Created = new DateTime(1976, 8, 20, 13, 45, 56, DateTimeKind.Utc),
                DefaultBranch = BranchIdentifier,
                Description = "the project",
                Name = "the project name"
            };

            var constraint = new PrimitiveConstraint
            {
                Inverse = true,
                Operator = PrimitiveConstraintOperator.lessthanorequalto,
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

            var composite = new CompositeConstraint
            {
                Operator = CompositeConstraintOperator.and,
                Constraint = [constraint]
            };

            List<IResponse> responses = [project, dataVersion, constraint, composite];

            using var responseStream = new MemoryStream();

            this.serializer.SerializeResponse(responses, responseStream);
            responseStream.Position = 0;

            var roundTrippedResponses = this.deSerializer.DeSerializeResponse(responseStream).ToList();

            var roundTrippedDataVersion = roundTrippedResponses.OfType<DataVersion>().Single();

            using (Assert.EnterMultipleScope())
            {
                Assert.That(roundTrippedResponses, Has.Count.EqualTo(4));
                Assert.That(roundTrippedResponses.OfType<Project>().Single(), Is.EqualTo(project).UsingPropertiesComparer());
                Assert.That(roundTrippedDataVersion, Is.EqualTo(dataVersion).UsingPropertiesComparer());
                Assert.That(roundTrippedResponses.OfType<PrimitiveConstraint>().Single(), Is.EqualTo(constraint).UsingPropertiesComparer());
                Assert.That(roundTrippedResponses.OfType<CompositeConstraint>().Single(), Is.EqualTo(composite).UsingPropertiesComparer());

                Assert.That(roundTrippedDataVersion.Payload, Is.TypeOf<Feature>());
                Assert.That(roundTrippedDataVersion.Payload, Is.EqualTo(dataVersion.Payload).UsingPropertiesComparer());
                Assert.That(roundTrippedDataVersion.Identity, Is.EqualTo(dataVersion.Identity).UsingPropertiesComparer());
            }

            var dataVersionRequest = new DataVersionRequest
            {
                Identity = new DataIdentityRequest { Alias = ["identity alias"], Name = "the identity" },
                Payload = CreateCoreFeature()
            };

            var projectRequest = new ProjectRequest
            {
                Alias = ["only"],
                DefaultBranch = BranchIdentifier,
                Name = "the name"
            };

            List<IRequest> requests = [projectRequest, dataVersionRequest];

            using var requestStream = new MemoryStream();

            this.serializer.SerializeRequest(requests, requestStream);
            requestStream.Position = 0;

            var roundTrippedRequests = this.deSerializer.DeSerializeRequest(requestStream).ToList();

            var roundTrippedDataVersionRequest = roundTrippedRequests.OfType<DataVersionRequest>().Single();

            using (Assert.EnterMultipleScope())
            {
                Assert.That(roundTrippedRequests, Has.Count.EqualTo(2));
                Assert.That(roundTrippedRequests.OfType<ProjectRequest>().Single(), Is.EqualTo(projectRequest).UsingPropertiesComparer());
                Assert.That(roundTrippedDataVersionRequest, Is.EqualTo(dataVersionRequest).UsingPropertiesComparer());

                Assert.That(roundTrippedDataVersionRequest.Payload, Is.TypeOf<Feature>());
                Assert.That(roundTrippedDataVersionRequest.Payload, Is.EqualTo(dataVersionRequest.Payload).UsingPropertiesComparer());
            }

            var divergent = this.deSerializer.DeSerializeResponse(this.Rewind(responses)).ToList();
            ((Feature)divergent.OfType<DataVersion>().Single().Payload).DeclaredName = "a different name";

            var divergentIdentity = this.deSerializer.DeSerializeResponse(this.Rewind(responses)).ToList();
            divergentIdentity.OfType<DataVersion>().Single().Identity.Alias.Add("an extra alias");

            using (Assert.EnterMultipleScope())
            {
                Assert.That(divergent.OfType<DataVersion>().Single(), Is.Not.EqualTo(dataVersion).UsingPropertiesComparer());
                Assert.That(divergentIdentity.OfType<DataVersion>().Single(), Is.Not.EqualTo(dataVersion).UsingPropertiesComparer());
            }
        }

        [Test]
        public async Task VerifyRoundTripAsync()
        {
            var project = new Project { Id = ProjectIdentifier, Name = "the name" };
            var projectRequest = new ProjectRequest { Name = "the name" };

            using var responseStream = new MemoryStream();

            await this.serializer.SerializeResponseAsync([project], responseStream, CancellationToken.None);
            responseStream.Position = 0;

            var roundTrippedResponses = await this.deSerializer.DeSerializeResponseAsync(responseStream, CancellationToken.None);

            using var requestStream = new MemoryStream();

            await this.serializer.SerializeRequestAsync([projectRequest], requestStream, CancellationToken.None);
            requestStream.Position = 0;

            var roundTrippedRequests = await this.deSerializer.DeSerializeRequestAsync(requestStream, CancellationToken.None);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(() => this.deSerializer.DeSerializeResponse(null), Throws.TypeOf<ArgumentNullException>());
                Assert.That(() => this.deSerializer.DeSerializeRequest(null), Throws.TypeOf<ArgumentNullException>());
                Assert.That(roundTrippedResponses.OfType<Project>().Single(), Is.EqualTo(project).UsingPropertiesComparer());
                Assert.That(roundTrippedRequests.OfType<ProjectRequest>().Single(), Is.EqualTo(projectRequest).UsingPropertiesComparer());
            }
        }

        private static Feature CreateCoreFeature()
        {
            return new Feature
            {
                Id = ElementIdentifier,
                AliasIds = ["core alias"],
                DeclaredName = "the feature",
                DeclaredShortName = "f",
                Direction = FeatureDirectionKind.Inout,
                ElementId = ElementIdentifier.ToString(),
                IsAbstract = true,
                IsUnique = false,
                OwnedRelationship = [BranchIdentifier],
                OwningRelationship = ProjectIdentifier
            };
        }

        private MemoryStream Rewind(IEnumerable<IResponse> responses)
        {
            var stream = new MemoryStream();

            this.serializer.SerializeResponse(responses, stream);
            stream.Position = 0;

            return stream;
        }

        [Test]
        public void VerifySerializeResponse()
        {
            var project = new Project
            {
                Id = ProjectIdentifier,
                Alias = ["first", "second"],
                Created = new DateTime(1976, 8, 20, 13, 45, 56, DateTimeKind.Utc),
                DefaultBranch = BranchIdentifier,
                Description = "the description",
                Name = "the name"
            };

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

            var composite = new CompositeConstraint
            {
                Operator = CompositeConstraintOperator.and,
                Constraint = [constraint]
            };

            using (Assert.EnterMultipleScope())
            {
                Assert.That(() => this.serializer.SerializeResponse(null, new MemoryStream()), Throws.TypeOf<ArgumentNullException>());
                Assert.That(() => this.serializer.SerializeResponse([project], null), Throws.TypeOf<ArgumentNullException>());
            }

            using var stream = new MemoryStream();

            Assert.That(() => this.serializer.SerializeResponse([project, constraint, composite], stream), Throws.Nothing);

            Assert.That(stream.Length, Is.GreaterThan(0));
        }

        [Test]
        public void VerifySerializeRequest()
        {
            var projectRequest = new ProjectRequest
            {
                Alias = ["only"],
                DefaultBranch = BranchIdentifier,
                Description = null,
                Name = "the name"
            };

            var withoutBranch = new ProjectRequest { Name = "no branch" };

            using (Assert.EnterMultipleScope())
            {
                Assert.That(() => this.serializer.SerializeRequest(null, new MemoryStream()), Throws.TypeOf<ArgumentNullException>());
                Assert.That(() => this.serializer.SerializeRequest([projectRequest], null), Throws.TypeOf<ArgumentNullException>());
            }

            using var stream = new MemoryStream();

            Assert.That(() => this.serializer.SerializeRequest([projectRequest, withoutBranch], stream), Throws.Nothing);

            Assert.That(stream.Length, Is.GreaterThan(0));
        }

        [Test]
        public void VerifySerializeResponseCarryingCoreData()
        {
            var dataVersion = new DataVersion
            {
                Id = DataVersionIdentifier,
                Alias = ["first"],
                Description = "the description",
                Name = "the name",
                Identity = new DataIdentity { Id = ElementIdentifier, Name = "the identity" },
                Payload = new Feature
                {
                    Id = ElementIdentifier,
                    AliasIds = ["core alias"],
                    DeclaredName = "the feature",
                    Direction = FeatureDirectionKind.Inout,
                    ElementId = ElementIdentifier.ToString(),
                    IsAbstract = true,
                    OwnedRelationship = [BranchIdentifier],
                    OwningRelationship = ProjectIdentifier
                }
            };

            using var coreStream = new MemoryStream();

            Assert.That(() => this.serializer.SerializeResponse([dataVersion], coreStream), Throws.Nothing);

            var coreJson = global::MessagePack.MessagePackSerializer.ConvertToJson(coreStream.ToArray());

            using (Assert.EnterMultipleScope())
            {
                Assert.That(coreJson, Does.Contain(typeof(Feature).FullName));
                Assert.That(coreJson, Does.Contain("the feature"));
                Assert.That(coreJson, Does.Contain("the identity"));
                Assert.That(coreJson, Does.Contain("inout"));
            }

            var withApiPayload = new DataVersion
            {
                Id = DataVersionIdentifier,
                Payload = new ExternalData { Id = ElementIdentifier, ResourceIdentifier = new Uri("http://www.stariongroup.eu/external") }
            };

            using var apiStream = new MemoryStream();

            Assert.That(() => this.serializer.SerializeResponse([withApiPayload], apiStream), Throws.Nothing);

            var apiJson = global::MessagePack.MessagePackSerializer.ConvertToJson(apiStream.ToArray());

            using (Assert.EnterMultipleScope())
            {
                Assert.That(apiJson, Does.Contain(typeof(ExternalData).FullName));
                Assert.That(apiJson, Does.Contain("http://www.stariongroup.eu/external"));
                Assert.That(apiJson, Does.Not.Contain(typeof(Feature).FullName));
            }

            var withoutPayload = new DataVersion { Id = DataVersionIdentifier };

            using var emptyStream = new MemoryStream();

            Assert.That(() => this.serializer.SerializeResponse([withoutPayload], emptyStream), Throws.Nothing);

            var emptyJson = global::MessagePack.MessagePackSerializer.ConvertToJson(emptyStream.ToArray());

            using (Assert.EnterMultipleScope())
            {
                Assert.That(emptyJson, Does.Not.Contain(typeof(Feature).FullName));
                Assert.That(coreStream.Length, Is.GreaterThan(emptyStream.Length));
                Assert.That(apiStream.Length, Is.GreaterThan(emptyStream.Length));
            }
        }

        [Test]
        public void VerifySerializeResponseToBufferWriter()
        {
            var project = new Project { Id = ProjectIdentifier, Name = "the name" };
            var projectRequest = new ProjectRequest { Name = "the name" };

            var responseWriter = new ArrayBufferWriter<byte>();
            var requestWriter = new ArrayBufferWriter<byte>();

            using (Assert.EnterMultipleScope())
            {
                Assert.That(() => this.serializer.SerializeResponseToBufferWriter(null, responseWriter), Throws.TypeOf<ArgumentNullException>());
                Assert.That(() => this.serializer.SerializeResponseToBufferWriter([project], null), Throws.TypeOf<ArgumentNullException>());
                Assert.That(() => this.serializer.SerializeRequestToBufferWriter(null, requestWriter), Throws.TypeOf<ArgumentNullException>());
                Assert.That(() => this.serializer.SerializeRequestToBufferWriter([projectRequest], null), Throws.TypeOf<ArgumentNullException>());
            }

            this.serializer.SerializeResponseToBufferWriter([project], responseWriter);
            this.serializer.SerializeRequestToBufferWriter([projectRequest], requestWriter);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(responseWriter.WrittenCount, Is.GreaterThan(0));
                Assert.That(requestWriter.WrittenCount, Is.GreaterThan(0));
            }
        }

        [Test]
        public async Task VerifySerializeResponseAsync()
        {
            var project = new Project { Id = ProjectIdentifier, Name = "the name" };
            var projectRequest = new ProjectRequest { Name = "the name" };

            using (Assert.EnterMultipleScope())
            {
                Assert.That(() => this.serializer.SerializeResponseAsync(null, new MemoryStream(), CancellationToken.None), Throws.TypeOf<ArgumentNullException>());
                Assert.That(() => this.serializer.SerializeRequestAsync(null, new MemoryStream(), CancellationToken.None), Throws.TypeOf<ArgumentNullException>());
            }

            using var responseStream = new MemoryStream();
            using var requestStream = new MemoryStream();

            await this.serializer.SerializeResponseAsync([project], responseStream, CancellationToken.None);
            await this.serializer.SerializeRequestAsync([projectRequest], requestStream, CancellationToken.None);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(responseStream.Length, Is.GreaterThan(0));
                Assert.That(requestStream.Length, Is.GreaterThan(0));
            }
        }

        [Test]
        public void VerifySerializeRejectsForeignDataItem()
        {
            var alien = new List<IResponse> { new AlienResponse() };

            using var stream = new MemoryStream();

            Assert.That(() => this.serializer.SerializeResponse(alien, stream), Throws.TypeOf<NotSupportedException>());
        }

        /// <summary>
        /// A response that the generated envelope does not carry
        /// </summary>
        private sealed class AlienResponse : IResponse
        {
        }
    }
}
