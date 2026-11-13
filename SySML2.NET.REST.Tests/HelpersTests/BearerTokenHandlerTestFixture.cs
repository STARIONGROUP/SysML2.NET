// ------------------------------------------------------------------------------------------------
// <copyright file="BearerTokenHandlerTestFixture.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SySML2.NET.REST.Tests.HelpersTests
{
    using System;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading.Tasks;

    using NUnit.Framework;

    using RichardSzalay.MockHttp;

    using SySML2.NET.REST.Tests.Helpers;

    [TestFixture]
    public class BearerTokenHandlerTestFixture
    {
        [Test]
        public void VerifyBearerTokenHandler()
        {
            using var inner = new MockHttpMessageHandler();

            using (Assert.EnterMultipleScope())
            {
                Assert.That(() => new BearerTokenHandler(null, inner), Throws.ArgumentException.With.Property(nameof(ArgumentException.ParamName)).EqualTo("token"));
                Assert.That(() => new BearerTokenHandler(string.Empty, inner), Throws.ArgumentException);
                Assert.That(() => new BearerTokenHandler(" ", inner), Throws.ArgumentException);
                Assert.That(() => new BearerTokenHandler("abc", inner).Dispose(), Throws.Nothing);
            }
        }

        [Test]
        public async Task VerifySendAsync()
        {
            using var inner = new MockHttpMessageHandler();
            inner.When("http://localhost/*").WithHeaders("Authorization", "Bearer abc").Respond("application/json", "{}");
            inner.Fallback.Respond(System.Net.HttpStatusCode.Unauthorized);

            using var httpClient = new HttpClient(new BearerTokenHandler("abc", inner)) { BaseAddress = new Uri("http://localhost/") };

            using var withoutHeader = new HttpRequestMessage(HttpMethod.Get, "projects");
            using var withOtherHeader = new HttpRequestMessage(HttpMethod.Get, "projects");
            withOtherHeader.Headers.Authorization = new AuthenticationHeaderValue("Basic", "x");

            using var first = await httpClient.SendAsync(withoutHeader);
            using var second = await httpClient.SendAsync(withOtherHeader);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(first.IsSuccessStatusCode, Is.True);
                Assert.That(second.IsSuccessStatusCode, Is.True);
                Assert.That(withOtherHeader.Headers.Authorization?.ToString(), Is.EqualTo("Bearer abc"));
            }
        }
    }
}
