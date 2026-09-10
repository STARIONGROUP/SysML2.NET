// -------------------------------------------------------------------------------------------------
// <copyright file="PsmDeSerializerTestFixture.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.Serializer.Json.Tests
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text.Json;
    using System.Threading;
    using System.Threading.Tasks;

    using NUnit.Framework;

    using SysML2.NET.Core.Core.Types;
    using SysML2.NET.Core.DTO.Core.Features;
    using SysML2.NET.PSM.DTO;
    using SysML2.NET.PSM.Enumerations;
    using SysML2.NET.Serializer.Json;

    [TestFixture]
    public class PsmDeSerializerTestFixture
    {
        private static readonly Guid ProjectIdentifier = Guid.Parse("9b0e1914-3241-461e-b9ee-a3ff5120de4e");

        private static readonly Guid BranchIdentifier = Guid.Parse("a910a705-7fbe-415f-9cbb-624bfadf6c20");

        private static readonly Guid DataVersionIdentifier = Guid.Parse("94e5b40e-741e-49ca-bd7f-f3138c071bf9");

        private static readonly Guid ElementIdentifier = Guid.Parse("00a6ef10-d3dc-4741-9029-2c9978c2f083");

        private Serializer serializer;

        private DeSerializer deSerializer;

        private JsonWriterOptions jsonWriterOptions;

        [SetUp]
        public void SetUp()
        {
            this.serializer = new Serializer();
            this.deSerializer = new DeSerializer();
            this.jsonWriterOptions = new JsonWriterOptions { Indented = false };
        }

        [Test]
        public void VerifyDeSerializeResponse()
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

            using var stream = this.SerializeResponse(project);

            var roundTripped = this.deSerializer.DeSerializeResponse<Project>(stream, SerializationModeKind.JSON, false);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(roundTripped.Id, Is.EqualTo(ProjectIdentifier));
                Assert.That(roundTripped.Alias, Is.EqualTo(new List<string> { "first", "second" }));
                Assert.That(roundTripped.Created, Is.EqualTo(project.Created));
                Assert.That(roundTripped.DefaultBranch, Is.EqualTo(BranchIdentifier));
                Assert.That(roundTripped.Description, Is.EqualTo("the description"));
                Assert.That(roundTripped.Name, Is.EqualTo("the name"));
            }

            using var mismatched = this.SerializeRequest(new ProjectRequest { Name = "the name" });

            Assert.That(() => this.deSerializer.DeSerializeResponse<Branch>(mismatched, SerializationModeKind.JSON, false),
                Throws.TypeOf<JsonException>());

            using var notAnObject = ToStream("[]");

            Assert.That(() => this.deSerializer.DeSerializeResponse<Project>(notAnObject, SerializationModeKind.JSON, false),
                Throws.TypeOf<JsonException>());
        }

        [Test]
        public void VerifyDeSerializeRequest()
        {
            var projectRequest = new ProjectRequest
            {
                Alias = ["only"],
                DefaultBranch = BranchIdentifier,
                Description = null,
                Name = "the name"
            };

            using var stream = this.SerializeRequest(projectRequest);

            var roundTripped = this.deSerializer.DeSerializeRequest<ProjectRequest>(stream, SerializationModeKind.JSON, false);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(roundTripped.Alias, Is.EqualTo(new List<string> { "only" }));
                Assert.That(roundTripped.DefaultBranch, Is.EqualTo(BranchIdentifier));
                Assert.That(roundTripped.Description, Is.Null);
                Assert.That(roundTripped.Name, Is.EqualTo("the name"));
            }

            using var withoutBranch = this.SerializeRequest(new ProjectRequest { Name = "no branch" });

            var withoutBranchRoundTripped = this.deSerializer.DeSerializeRequest<ProjectRequest>(withoutBranch, SerializationModeKind.JSON, false);

            Assert.That(withoutBranchRoundTripped.DefaultBranch, Is.Null);

            using var mismatched = this.SerializeRequest(projectRequest);

            Assert.That(() => this.deSerializer.DeSerializeRequest<BranchRequest>(mismatched, SerializationModeKind.JSON, false),
                Throws.TypeOf<JsonException>());
        }

        [Test]
        public void VerifyDeSerializeConstraint()
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

            using var stream = this.SerializeResponse(constraint);

            var roundTripped = this.deSerializer.DeSerializeResponse<PrimitiveConstraint>(stream, SerializationModeKind.JSON, false);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(roundTripped.Inverse, Is.True);
                Assert.That(roundTripped.Operator, Is.EqualTo(PrimitiveConstraintOperator.lessthan));
                Assert.That(roundTripped.Property, Is.EqualTo("created"));
                Assert.That(roundTripped.Value, Has.Count.EqualTo(5));
                Assert.That(roundTripped.Value, Is.EqualTo(constraint.Value));
            }

            var composite = new CompositeConstraint
            {
                Operator = CompositeConstraintOperator.and,
                Constraint = [constraint]
            };

            using var compositeStream = this.SerializeResponse(composite);

            var compositeRoundTripped = this.deSerializer.DeSerializeResponse<CompositeConstraint>(compositeStream, SerializationModeKind.JSON, false);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(compositeRoundTripped.Operator, Is.EqualTo(CompositeConstraintOperator.and));
                Assert.That(compositeRoundTripped.Constraint, Has.Count.EqualTo(1));
                Assert.That(compositeRoundTripped.Constraint[0], Is.TypeOf<PrimitiveConstraint>());
                Assert.That(((PrimitiveConstraint)compositeRoundTripped.Constraint[0]).Operator, Is.EqualTo(PrimitiveConstraintOperator.lessthan));
            }
        }

        [Test]
        public void VerifyDeSerializeDataVersion()
        {
            var dataVersion = new DataVersion
            {
                Id = DataVersionIdentifier,
                Identity = new DataIdentity { Id = ElementIdentifier, Name = "the identity" },
                Payload = new Feature { Id = ElementIdentifier, ElementId = ElementIdentifier.ToString(), DeclaredName = "the feature" }
            };

            using var stream = this.SerializeResponse(dataVersion);

            var roundTripped = this.deSerializer.DeSerializeResponse<DataVersion>(stream, SerializationModeKind.JSON, false);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(roundTripped.Id, Is.EqualTo(DataVersionIdentifier));
                Assert.That(roundTripped.Identity.Name, Is.EqualTo("the identity"));
                Assert.That(roundTripped.Payload, Is.TypeOf<Feature>());
                Assert.That(((Feature)roundTripped.Payload).Id, Is.EqualTo(ElementIdentifier));
                Assert.That(((Feature)roundTripped.Payload).DeclaredName, Is.EqualTo("the feature"));
            }

            var withApiPayload = new DataVersion
            {
                Id = DataVersionIdentifier,
                Payload = new ExternalData { Id = ElementIdentifier, ResourceIdentifier = new Uri("http://www.stariongroup.eu/external") }
            };

            using var apiStream = this.SerializeResponse(withApiPayload);

            var apiRoundTripped = this.deSerializer.DeSerializeResponse<DataVersion>(apiStream, SerializationModeKind.JSON, false);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(apiRoundTripped.Payload, Is.TypeOf<ExternalData>());
                Assert.That(((ExternalData)apiRoundTripped.Payload).ResourceIdentifier, Is.EqualTo(new Uri("http://www.stariongroup.eu/external")));
                Assert.That(apiRoundTripped.Identity, Is.Null);
            }

            using var withoutPayload = this.SerializeResponse(new DataVersion { Id = DataVersionIdentifier });

            var withoutPayloadRoundTripped = this.deSerializer.DeSerializeResponse<DataVersion>(withoutPayload, SerializationModeKind.JSON, false);

            Assert.That(withoutPayloadRoundTripped.Payload, Is.Null);
        }

        [Test]
        public void VerifyDeSerializeResponses()
        {
            var project = new Project { Id = ProjectIdentifier, Name = "the name" };
            var branch = new Branch { Id = BranchIdentifier, Name = "the branch" };

            using var stream = this.SerializeResponses([project, branch]);

            var roundTripped = this.deSerializer.DeSerializeResponses<IResponse>(stream, SerializationModeKind.JSON, false).ToList();

            using (Assert.EnterMultipleScope())
            {
                Assert.That(roundTripped, Has.Count.EqualTo(2));
                Assert.That(roundTripped[0], Is.TypeOf<Project>());
                Assert.That(roundTripped[1], Is.TypeOf<Branch>());
            }

            using var homogeneous = this.SerializeResponses([project]);

            Assert.That(this.deSerializer.DeSerializeResponses<Project>(homogeneous, SerializationModeKind.JSON, false), Has.Count.EqualTo(1));

            using var heterogeneous = this.SerializeResponses([project, branch]);

            Assert.That(() => this.deSerializer.DeSerializeResponses<Project>(heterogeneous, SerializationModeKind.JSON, false),
                Throws.TypeOf<JsonException>());
        }

        [Test]
        public void VerifyDeSerializeRequests()
        {
            var projectRequest = new ProjectRequest { Name = "the name" };
            var branchRequest = new BranchRequest { Name = "the branch" };

            using var stream = this.SerializeRequests([projectRequest, branchRequest]);

            var roundTripped = this.deSerializer.DeSerializeRequests<IRequest>(stream, SerializationModeKind.JSON, false).ToList();

            using (Assert.EnterMultipleScope())
            {
                Assert.That(roundTripped, Has.Count.EqualTo(2));
                Assert.That(roundTripped[0], Is.TypeOf<ProjectRequest>());
                Assert.That(roundTripped[1], Is.TypeOf<BranchRequest>());
            }

            using var heterogeneous = this.SerializeRequests([projectRequest, branchRequest]);

            Assert.That(() => this.deSerializer.DeSerializeRequests<ProjectRequest>(heterogeneous, SerializationModeKind.JSON, false),
                Throws.TypeOf<JsonException>());
        }

        [Test]
        public async Task VerifyDeSerializeResponseAsync()
        {
            var project = new Project { Id = ProjectIdentifier, Name = "the name" };

            using var stream = this.SerializeResponse(project);

            var roundTripped = await this.deSerializer.DeSerializeResponseAsync<Project>(stream, SerializationModeKind.JSON, false, CancellationToken.None);

            Assert.That(roundTripped.Id, Is.EqualTo(ProjectIdentifier));

            using var collection = this.SerializeResponses([project]);

            var responses = await this.deSerializer.DeSerializeResponsesAsync<Project>(collection, SerializationModeKind.JSON, false, CancellationToken.None);

            Assert.That(responses, Has.Count.EqualTo(1));

            using var mismatched = this.SerializeResponse(project);

            await Assert.ThatAsync(() => this.deSerializer.DeSerializeResponseAsync<Branch>(mismatched, SerializationModeKind.JSON, false, CancellationToken.None),
                Throws.TypeOf<JsonException>());
        }

        [Test]
        public async Task VerifyDeSerializeRequestAsync()
        {
            var projectRequest = new ProjectRequest { Name = "the name" };

            using var stream = this.SerializeRequest(projectRequest);

            var roundTripped = await this.deSerializer.DeSerializeRequestAsync<ProjectRequest>(stream, SerializationModeKind.JSON, false, CancellationToken.None);

            Assert.That(roundTripped.Name, Is.EqualTo("the name"));

            using var collection = this.SerializeRequests([projectRequest]);

            var requests = await this.deSerializer.DeSerializeRequestsAsync<ProjectRequest>(collection, SerializationModeKind.JSON, false, CancellationToken.None);

            Assert.That(requests, Has.Count.EqualTo(1));

            using var mismatched = this.SerializeRequest(projectRequest);

            await Assert.ThatAsync(() => this.deSerializer.DeSerializeRequestAsync<BranchRequest>(mismatched, SerializationModeKind.JSON, false, CancellationToken.None),
                Throws.TypeOf<JsonException>());
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

            using (Assert.EnterMultipleScope())
            {
                Assert.That(this.RoundTripResponse(dataVersion), Is.EqualTo(dataVersion).UsingPropertiesComparer());
                Assert.That(this.RoundTripResponse(project), Is.EqualTo(project).UsingPropertiesComparer());
                Assert.That(this.RoundTripResponse(constraint), Is.EqualTo(constraint).UsingPropertiesComparer());
            }

            var divergentPayload = this.RoundTripResponse(dataVersion);
            ((Feature)divergentPayload.Payload).DeclaredName = "a different name";

            var divergentIdentity = this.RoundTripResponse(dataVersion);
            divergentIdentity.Identity.Alias.Add("an extra alias");

            using (Assert.EnterMultipleScope())
            {
                Assert.That(divergentPayload, Is.Not.EqualTo(dataVersion).UsingPropertiesComparer());
                Assert.That(divergentIdentity, Is.Not.EqualTo(dataVersion).UsingPropertiesComparer());
            }

            List<IResponse> responses = [project, dataVersion, constraint];

            using var collectionStream = this.SerializeResponses(responses);

            var roundTrippedCollection = this.deSerializer.DeSerializeResponses<IResponse>(collectionStream, SerializationModeKind.JSON, false).ToList();

            using (Assert.EnterMultipleScope())
            {
                Assert.That(roundTrippedCollection, Has.Count.EqualTo(3));
                Assert.That(roundTrippedCollection, Is.EqualTo(responses).UsingPropertiesComparer());
            }

            var dataVersionRequest = new DataVersionRequest
            {
                Identity = new DataIdentityRequest { Alias = ["identity alias"], Name = "the identity" },
                Payload = CreateCoreFeature()
            };

            using var requestStream = this.SerializeRequest(dataVersionRequest);

            var roundTrippedRequest = this.deSerializer.DeSerializeRequest<DataVersionRequest>(requestStream, SerializationModeKind.JSON, false);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(roundTrippedRequest.Payload, Is.TypeOf<Feature>());
                Assert.That(roundTrippedRequest, Is.EqualTo(dataVersionRequest).UsingPropertiesComparer());
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

        private T RoundTripResponse<T>(T response) where T : IResponse
        {
            using var stream = this.SerializeResponse(response);

            return this.deSerializer.DeSerializeResponse<T>(stream, SerializationModeKind.JSON, false);
        }

        private MemoryStream SerializeResponse(IResponse response)
        {
            var stream = new MemoryStream();

            this.serializer.SerializeResponse(response, SerializationModeKind.JSON, false, stream, this.jsonWriterOptions);
            stream.Position = 0;

            return stream;
        }

        private MemoryStream SerializeResponses(IEnumerable<IResponse> responses)
        {
            var stream = new MemoryStream();

            this.serializer.SerializeResponse(responses, SerializationModeKind.JSON, false, stream, this.jsonWriterOptions);
            stream.Position = 0;

            return stream;
        }

        private MemoryStream SerializeRequest(IRequest request)
        {
            var stream = new MemoryStream();

            this.serializer.SerializeRequest(request, SerializationModeKind.JSON, false, stream, this.jsonWriterOptions);
            stream.Position = 0;

            return stream;
        }

        private MemoryStream SerializeRequests(IEnumerable<IRequest> requests)
        {
            var stream = new MemoryStream();

            this.serializer.SerializeRequest(requests, SerializationModeKind.JSON, false, stream, this.jsonWriterOptions);
            stream.Position = 0;

            return stream;
        }

        private static MemoryStream ToStream(string json)
        {
            return new MemoryStream(System.Text.Encoding.UTF8.GetBytes(json));
        }
    }
}
