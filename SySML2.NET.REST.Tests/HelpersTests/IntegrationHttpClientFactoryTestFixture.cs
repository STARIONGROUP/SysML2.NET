// ------------------------------------------------------------------------------------------------
// <copyright file="IntegrationHttpClientFactoryTestFixture.cs" company="Starion Group S.A.">
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
    using System.Net.Http;
    using System.Threading.Tasks;

    using NUnit.Framework;

    using SySML2.NET.REST.Tests.Helpers;

    [TestFixture]
    public class IntegrationHttpClientFactoryTestFixture
    {
        private static readonly Uri Unreachable = new("http://127.0.0.1:1/projects");

        private OpenApiDocument openApi;

        [SetUp]
        public void SetUp()
        {
            this.openApi = OpenApiDocument.Load(Path.Combine(TestContext.CurrentContext.TestDirectory, "Resources", "ptc-25-02-30.json"));
        }

        [Test]
        public async Task VerifyCreateClient()
        {
            using var factory = new IntegrationHttpClientFactory("token", this.openApi);

            var first = factory.CreateClient("first");
            var second = factory.CreateClient("second");

            using (Assert.EnterMultipleScope())
            {
                Assert.That(second, Is.Not.SameAs(first));
                Assert.That(factory.Recorder, Is.Not.Null);
            }

            first.Dispose();

            await Assert.ThatAsync(() => second.GetAsync(Unreachable), Throws.InstanceOf<HttpRequestException>(), "disposing one client leaves the shared chain intact");
            Assert.That(factory.Recorder.Calls, Is.Empty);

            second.Dispose();
        }

        [Test]
        public async Task VerifyDispose()
        {
            var factory = new IntegrationHttpClientFactory("token", this.openApi);
            using var client = factory.CreateClient(string.Empty);

            factory.Dispose();

            await Assert.ThatAsync(() => client.GetAsync(Unreachable), Throws.InstanceOf<ObjectDisposedException>());
        }

        [Test]
        public void VerifyIntegrationHttpClientFactory()
        {
            using (Assert.EnterMultipleScope())
            {
                Assert.That(() => new IntegrationHttpClientFactory(" ", this.openApi), Throws.ArgumentException);
                Assert.That(() => new IntegrationHttpClientFactory("token", null), Throws.ArgumentNullException);
            }
        }
    }
}
