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
    using System.Net;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;

    using Moq;

    using NUnit.Framework;

    using RichardSzalay.MockHttp;

    using SysML2.NET.REST.Exceptions;
    using SysML2.NET.Serializer.Json;

    [TestFixture]
    public class SessionTestFixture
    {
        private const string JsonMediaType = "application/json";

        private MockHttpMessageHandler mockHttp;

        private Mock<IHttpClientFactory> httpClientFactory;

        private Session session;

        [SetUp]
        public void SetUp()
        {
            this.mockHttp = new MockHttpMessageHandler();
            this.httpClientFactory = new Mock<IHttpClientFactory>();
            this.httpClientFactory.Setup(factory => factory.CreateClient(nameof(Session))).Returns(() => this.mockHttp.ToHttpClient());

            this.session = new Session(this.httpClientFactory.Object, new DeSerializer(), new Serializer());
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
                Assert.That(() => new Session(null, new DeSerializer(), new Serializer()), Throws.TypeOf<ArgumentNullException>());
                Assert.That(() => new Session(this.httpClientFactory.Object, null, new Serializer()), Throws.TypeOf<ArgumentNullException>());
                Assert.That(() => new Session(this.httpClientFactory.Object, new DeSerializer(), null), Throws.TypeOf<ArgumentNullException>());
                Assert.That(this.session.Client, Is.Null);
            }
        }

        [Test]
        public async Task VerifyOpen()
        {
            await Assert.ThatAsync(() => this.session.Open(null, CancellationToken.None), Throws.TypeOf<ArgumentNullException>());
            await Assert.ThatAsync(() => this.session.Open(new Uri("api", UriKind.Relative), CancellationToken.None), Throws.TypeOf<ArgumentException>());

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

            await Assert.ThatAsync(() => this.session.Open(new Uri("http://localhost/api/"), CancellationToken.None), Throws.TypeOf<PsmApiException>());

            Assert.That(this.session.Client, Is.Null);

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

            this.session.Close();

            Assert.That(this.session.Client, Is.Null);
        }
    }
}
