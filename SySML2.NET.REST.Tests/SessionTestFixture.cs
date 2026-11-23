// ------------------------------------------------------------------------------------------------
// <copyright file="SessionTestFixture.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SySML2.NET.REST.Tests
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net;
    using System.Net.Http;
    using System.Text;
    using System.Text.Json;
    using System.Threading;
    using System.Threading.Tasks;

    using Moq;

    using NUnit.Framework;

    using RichardSzalay.MockHttp;

    using SysML2.NET.Dal;
    using SysML2.NET.REST.Exceptions;
    using SysML2.NET.Serializer.Json;

    [TestFixture]
    public class SessionTestFixture
    {
        private const string JsonMediaType = "application/json";

        private static readonly Guid ProjectId = Guid.Parse("000e9890-6935-43e6-a5d7-5d7cac601f4c");

        private static readonly Guid BranchId = Guid.Parse("c294a463-6c9c-47a8-b592-01252c5ab2a7");

        private static readonly Guid EmptyBranchId = Guid.Parse("5b1c3e7a-2f4d-4c8e-9a6b-1d2e3f4a5b6c");

        private static readonly Guid CommitId = Guid.Parse("6d7ad9fd-6520-4ff2-885b-8c5c129e6c27");

        private static readonly Guid OtherCommitId = Guid.Parse("2f1a0e1c-6b2a-4d5e-9c3f-8a7b6d5e4c3b");

        private static readonly Guid LoopingCommitId = Guid.Parse("9c8b7a6d-5e4f-4a3b-8c2d-1e0f9a8b7c6d");

        private static readonly Guid RootNamespaceId = Guid.Parse("864d5403-c87b-4679-9d3a-b09c7fde1dd9");

        private static readonly Guid FunctionId = Guid.Parse("761f87bf-e06e-55f7-8830-ebcc3f619e2d");

        private static readonly Guid[] RootIds = [RootNamespaceId, FunctionId];

        private static readonly string ElementsUri = $"http://localhost/api/projects/{ProjectId}/commits/";

        private MockHttpMessageHandler mockHttp;

        private Mock<IHttpClientFactory> httpClientFactory;

        private Assembler assembler;

        private Session session;

        [SetUp]
        public void SetUp()
        {
            this.mockHttp = new MockHttpMessageHandler();
            this.httpClientFactory = new Mock<IHttpClientFactory>();
            this.httpClientFactory.Setup(factory => factory.CreateClient(nameof(Session))).Returns(() => this.mockHttp.ToHttpClient());

            this.assembler = new Assembler();
            this.session = new Session(this.httpClientFactory.Object, new DeSerializer(), new Serializer(), this.assembler);
        }

        [TearDown]
        public void TearDown()
        {
            this.mockHttp.Dispose();
        }

        [Test]
        public void VerifyConstructor()
        {
            using (Assert.EnterMultipleScope())
            {
                Assert.That(() => new Session(null, new DeSerializer(), new Serializer(), this.assembler), Throws.TypeOf<ArgumentNullException>());
                Assert.That(() => new Session(this.httpClientFactory.Object, null, new Serializer(), this.assembler), Throws.TypeOf<ArgumentNullException>());
                Assert.That(() => new Session(this.httpClientFactory.Object, new DeSerializer(), null, this.assembler), Throws.TypeOf<ArgumentNullException>());
                Assert.That(() => new Session(this.httpClientFactory.Object, new DeSerializer(), new Serializer(), null), Throws.TypeOf<ArgumentNullException>());
                Assert.That(this.session.Assembler, Is.SameAs(this.assembler));
                Assert.That(this.session.Client, Is.Null);
            }
        }

        [Test]
        public async Task VerifyOpen()
        {
            using (Assert.EnterMultipleScope())
            {
                await Assert.ThatAsync(() => this.session.Open(null, CancellationToken.None), Throws.TypeOf<ArgumentNullException>());
                await Assert.ThatAsync(() => this.session.Open(new Uri("api", UriKind.Relative), CancellationToken.None), Throws.TypeOf<ArgumentException>());
            }

            this.mockHttp.Expect(HttpMethod.Get, "http://localhost/api/projects").Respond(JsonMediaType, "[]");

            var projects = await this.session.Open(new Uri("http://localhost/api"), CancellationToken.None);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(projects.Records, Has.Count.EqualTo(0));
                Assert.That(this.session.Client, Is.Not.Null);
            }

            var firstClient = this.session.Client;

            this.mockHttp.Expect(HttpMethod.Get, "http://otherhost/projects").Respond(JsonMediaType, "[]");

            await this.session.Open(new Uri("http://otherhost/"), CancellationToken.None);

            Assert.That(this.session.Client, Is.Not.SameAs(firstClient));

            this.mockHttp.Expect(HttpMethod.Get, "http://localhost/api/projects").Respond(HttpStatusCode.InternalServerError);

            using (Assert.EnterMultipleScope())
            {
                await Assert.ThatAsync(() => this.session.Open(new Uri("http://localhost/api/"), CancellationToken.None), Throws.TypeOf<PsmApiException>());
                Assert.That(this.session.Client, Is.Null);
                Assert.That(this.assembler.Cache, Is.Empty);
            }

            this.mockHttp.VerifyNoOutstandingExpectation();
            this.httpClientFactory.Verify(factory => factory.CreateClient(nameof(Session)), Times.Exactly(3));
        }

        [Test]
        public async Task VerifyClose()
        {
            this.session.Close();

            Assert.That(this.session.Client, Is.Null);

            this.mockHttp.Expect(HttpMethod.Get, "http://localhost/api/projects").Respond(JsonMediaType, "[]");

            await this.session.Open(new Uri("http://localhost/api/"), CancellationToken.None);

            Assert.That(this.session.Client, Is.Not.Null);

            this.mockHttp.Expect(HttpMethod.Get, $"{ElementsUri}{CommitId}/elements").Respond(JsonMediaType, ReadPayload("roots.json"));
            await this.session.LoadCommit(ProjectId, CommitId, CancellationToken.None);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(this.session.CurrentCommit, Is.EqualTo((ProjectId, CommitId)));
                Assert.That(this.assembler.Cache.Keys, Is.EquivalentTo(RootIds));
            }

            this.session.Close();

            using (Assert.EnterMultipleScope())
            {
                Assert.That(this.session.Client, Is.Null);
                Assert.That(this.session.CurrentCommit, Is.Null);
                Assert.That(this.assembler.Cache, Is.Empty);
            }

            this.mockHttp.Expect(HttpMethod.Get, "http://localhost/api/projects").Respond(JsonMediaType, "[]");
            this.mockHttp.Expect(HttpMethod.Get, $"{ElementsUri}{CommitId}/elements").Respond(JsonMediaType, ReadPayload("roots.json"));

            await this.session.Open(new Uri("http://localhost/api/"), CancellationToken.None);
            await this.session.LoadCommit(ProjectId, CommitId, CancellationToken.None);

            Assert.That(this.assembler.Cache.Keys, Is.EquivalentTo(RootIds));

            this.mockHttp.VerifyNoOutstandingExpectation();
        }

        [Test]
        public async Task VerifyLoadCommit()
        {
            await Assert.ThatAsync(() => this.session.LoadCommit(ProjectId, CommitId, CancellationToken.None), Throws.TypeOf<InvalidOperationException>().With.Message.Contains("closed"));

            using var roots = JsonDocument.Parse(ReadPayload("roots.json"));
            var namespaceElement = roots.RootElement[0].GetRawText();
            var functionElement = roots.RootElement[1].GetRawText();

            this.mockHttp.Expect(HttpMethod.Get, "http://localhost/api/projects").Respond(JsonMediaType, "[]");
            await this.session.Open(new Uri("http://localhost/api/"), CancellationToken.None);

            this.mockHttp.Expect(HttpMethod.Get, $"{ElementsUri}{CommitId}/elements").WithExactQueryString(string.Empty)
                .Respond(HttpStatusCode.OK, [new KeyValuePair<string, string>("Link", $"<{ElementsUri}{CommitId}/elements?page[after]=second>; rel=\"next\"")], JsonMediaType, $"[{namespaceElement}]");
            this.mockHttp.Expect(HttpMethod.Get, $"{ElementsUri}{CommitId}/elements").WithExactQueryString("page[after]=second").Respond(JsonMediaType, $"[{functionElement}]");

            await this.session.LoadCommit(ProjectId, CommitId, CancellationToken.None);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(this.assembler.Cache.Keys, Is.EquivalentTo(RootIds));
                Assert.That(this.session.CurrentCommit, Is.EqualTo((ProjectId, CommitId)));
            }

            var function = this.assembler.Cache[FunctionId].Value;

            await this.session.LoadCommit(ProjectId, CommitId, CancellationToken.None);

            Assert.That(this.assembler.Cache[FunctionId].Value, Is.SameAs(function));

            this.mockHttp.Expect(HttpMethod.Get, $"{ElementsUri}{OtherCommitId}/elements").Respond(JsonMediaType, $"[{functionElement}]");

            await this.session.LoadCommit(ProjectId, OtherCommitId, CancellationToken.None);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(this.assembler.Cache.Keys, Is.EquivalentTo([FunctionId]));
                Assert.That(this.assembler.Cache[FunctionId].Value, Is.SameAs(function));
                Assert.That(this.session.CurrentCommit, Is.EqualTo((ProjectId, OtherCommitId)));
            }

            var repeatingLink = new KeyValuePair<string, string>("Link", $"<{ElementsUri}{LoopingCommitId}/elements?page[after]=again>; rel=\"next\"");
            this.mockHttp.When(HttpMethod.Get, $"{ElementsUri}{LoopingCommitId}/elements").Respond(HttpStatusCode.OK, [repeatingLink], JsonMediaType, "[]");

            using (Assert.EnterMultipleScope())
            {
                await Assert.ThatAsync(() => this.session.LoadCommit(ProjectId, LoopingCommitId, CancellationToken.None), Throws.TypeOf<InvalidOperationException>().With.Message.Contains("already returned"));
                Assert.That(this.session.CurrentCommit, Is.EqualTo((ProjectId, OtherCommitId)));
            }

            this.mockHttp.Expect(HttpMethod.Get, $"{ElementsUri}{CommitId}/elements").Respond(_ =>
            {
                this.session.Close();
                return JsonResponse(ReadPayload("roots.json"));
            });

            await this.session.LoadCommit(ProjectId, CommitId, CancellationToken.None);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(this.assembler.Cache, Is.Empty);
                Assert.That(this.session.CurrentCommit, Is.Null);
            }

            this.mockHttp.VerifyNoOutstandingExpectation();
        }

        [Test]
        public async Task VerifyLoadBranchHead()
        {
            await Assert.ThatAsync(() => this.session.LoadBranchHead(ProjectId, BranchId, CancellationToken.None), Throws.TypeOf<InvalidOperationException>().With.Message.Contains("closed"));

            this.mockHttp.Expect(HttpMethod.Get, "http://localhost/api/projects").Respond(JsonMediaType, "[]");
            await this.session.Open(new Uri("http://localhost/api/"), CancellationToken.None);

            this.mockHttp.Expect(HttpMethod.Get, $"http://localhost/api/projects/{ProjectId}/branches/{BranchId}").Respond(JsonMediaType, ReadPayload("branch.json"));
            this.mockHttp.Expect(HttpMethod.Get, $"{ElementsUri}{CommitId}/elements").Respond(JsonMediaType, ReadPayload("roots.json"));
            this.mockHttp.Expect(HttpMethod.Get, $"http://localhost/api/projects/{ProjectId}/branches/{BranchId}").Respond(JsonMediaType, ReadPayload("branch.json"));

            await this.session.LoadBranchHead(ProjectId, BranchId, CancellationToken.None);
            await this.session.LoadBranchHead(ProjectId, BranchId, CancellationToken.None);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(this.assembler.Cache.Keys, Is.EquivalentTo(RootIds));
                Assert.That(this.session.CurrentCommit, Is.EqualTo((ProjectId, CommitId)));
            }

            this.mockHttp.Expect(HttpMethod.Get, $"http://localhost/api/projects/{ProjectId}/branches/{EmptyBranchId}")
                .Respond(JsonMediaType, $"{{\"@id\":\"{EmptyBranchId}\",\"@type\":\"Branch\",\"created\":\"2023-03-13T17:53:50.188295-04:00\",\"head\":null,\"name\":\"empty\",\"owningProject\":{{\"@id\":\"{ProjectId}\"}},\"referencedCommit\":null}}");

            await this.session.LoadBranchHead(ProjectId, EmptyBranchId, CancellationToken.None);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(this.assembler.Cache, Is.Empty);
                Assert.That(this.session.CurrentCommit, Is.Null);
            }

            this.mockHttp.VerifyNoOutstandingExpectation();
        }

        private static HttpResponseMessage JsonResponse(string content)
        {
            return new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent(content, Encoding.UTF8, JsonMediaType) };
        }

        private static string ReadPayload(string fileName)
        {
            return File.ReadAllText(Path.Combine(TestContext.CurrentContext.TestDirectory, "Data", fileName));
        }
    }
}
