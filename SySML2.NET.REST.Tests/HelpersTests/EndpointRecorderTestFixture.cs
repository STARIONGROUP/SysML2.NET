// ------------------------------------------------------------------------------------------------
// <copyright file="EndpointRecorderTestFixture.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SySML2.NET.REST.Tests.HelpersTests
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;

    using NUnit.Framework;

    using RichardSzalay.MockHttp;

    using SySML2.NET.REST.Tests.Helpers;

    [TestFixture]
    public class EndpointRecorderTestFixture
    {
        private const string ProjectBody = "{\"@id\":\"3fa85f64-5717-4562-b3fc-2c963f66afa6\",\"@type\":\"Project\",\"alias\":[],\"created\":\"1970-01-01T00:00:00.000Z\",\"defaultBranch\":{\"@id\":\"3fa85f64-5717-4562-b3fc-2c963f66afa7\"},\"description\":null,\"name\":\"p\"}";

        private MockHttpMessageHandler mockHttp;

        private OpenApiDocument openApi;

        private EndpointRecorder recorder;

        private HttpClient httpClient;

        [SetUp]
        public void SetUp()
        {
            this.openApi = OpenApiDocument.Load(Path.Combine(TestContext.CurrentContext.TestDirectory, "Resources", "ptc-25-02-30.json"));
            this.mockHttp = new MockHttpMessageHandler();
            this.recorder = new EndpointRecorder(this.openApi, this.mockHttp);
            this.httpClient = new HttpClient(this.recorder) { BaseAddress = new Uri("http://localhost/") };
        }

        [TearDown]
        public void TearDown()
        {
            this.httpClient.Dispose();
        }

        [Test]
        public void VerifyEndpointRecorder()
        {
            Assert.That(() => new EndpointRecorder(null, this.mockHttp), Throws.ArgumentNullException);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(this.recorder.Calls, Is.Empty);
                Assert.That(this.recorder.EmptyGuidRequests, Is.Empty);
                Assert.That(this.recorder.LastCall, Is.Null);
            }
        }

        [Test]
        public async Task VerifySendAsync()
        {
            this.mockHttp.When(HttpMethod.Get, "http://localhost/projects/p").Respond(HttpStatusCode.NotFound);
            this.mockHttp.When(HttpMethod.Post, "http://localhost/projects").Respond(HttpStatusCode.Created, "application/json", ProjectBody);
            this.mockHttp.When(HttpMethod.Get, "http://localhost/projects").Respond("application/json", "[{}]");
            this.mockHttp.When(HttpMethod.Get, "http://localhost/unknown").Respond(HttpStatusCode.OK);

            var notFound = await this.httpClient.GetAsync("projects/p");
            var created = await this.httpClient.PostAsync("projects", new StringContent("{\"@type\":\"Project\",\"name\":\"p\",\"defaultBranch\":{\"@id\":\"00000000-0000-0000-0000-000000000000\"}}", Encoding.UTF8, "application/json"));
            await this.httpClient.PostAsync("projects?name=x", new StringContent("{\"@type\":\"Project\",\"name\":5}", Encoding.UTF8, "application/json"));
            await this.httpClient.GetAsync("projects?page%5Bsize%5D=10");
            await this.httpClient.GetAsync("unknown");

            var calls = this.recorder.Calls.ToList();

            using (Assert.EnterMultipleScope())
            {
                Assert.That(notFound.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
                Assert.That(await created.Content.ReadAsStringAsync(), Is.EqualTo(ProjectBody), "the client can still read the buffered body");
                Assert.That(calls, Has.Count.EqualTo(5));
                Assert.That(this.recorder.LastCall, Is.SameAs(calls[^1]));
                Assert.That(calls[0].Operation?.PathTemplate, Is.EqualTo("/projects/{projectId}"));
                Assert.That(calls[0].StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
                Assert.That(calls[0].RequestLine, Is.EqualTo("GET /projects/p"));
                Assert.That(calls[0].RequestViolations, Is.Empty);
                Assert.That(calls[0].ResponseViolations.Select(violation => violation.Kind), Is.EqualTo((ContractViolationKind[])[ContractViolationKind.UnexpectedBody]));
                Assert.That(calls[1].Operation?.ToString(), Is.EqualTo("POST /projects"));
                Assert.That(calls[1].RequestViolations, Is.Empty);
                Assert.That(calls[1].ResponseViolations, Is.Empty);
                Assert.That(calls[2].RequestViolations.Select(violation => violation.Kind), Is.EquivalentTo((ContractViolationKind[])[ContractViolationKind.UndeclaredParameter, ContractViolationKind.SchemaMismatch]));
                Assert.That(calls[3].RequestViolations, Is.Empty);
                Assert.That(calls[3].ResponseViolations.Select(violation => violation.Kind), Is.EqualTo((ContractViolationKind[])[ContractViolationKind.SchemaMismatch]));
                Assert.That(calls[4].Operation, Is.Null);
                Assert.That(calls[4].ResponseViolations, Is.Empty);
                Assert.That(this.recorder.EmptyGuidRequests, Is.EqualTo((string[])["POST /projects"]));
            }
        }
    }
}
