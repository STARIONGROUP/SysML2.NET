// -------------------------------------------------------------------------------------------------
// <copyright file="AuthenticationServiceTestFixture.cs" company="RHEA System S.A.">
// 
//   Copyright 2022-2023 RHEA System S.A.
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

namespace SysML2.NET.Viewer.Tests.Services.Authentication
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using Blazored.SessionStorage;
    
    using Moq;

    using NUnit.Framework;

    using SySML2.NET.REST;
    using SysML2.NET.Viewer.Services.Authentication;

    /// <summary>
    /// Suite of tests for the <see cref="AuthenticationService"/>
    /// </summary>
    [TestFixture]
    public class AuthenticationServiceTestFixture
    {
        private Mock<ISessionStorageService> sessionStorageService;

        private Mock<IRestClient> restClient;

        private AnonymousAuthenticationStateProvider authenticationStateProvider;

        private AuthenticationService authenticationService;

        [SetUp]
        public void SetUp()
        {
            this.sessionStorageService = new Mock<ISessionStorageService>();
            this.restClient = new Mock<IRestClient>();
            this.authenticationStateProvider = new AnonymousAuthenticationStateProvider(this.sessionStorageService.Object);

            this.authenticationService = new AuthenticationService(this.restClient.Object, this.authenticationStateProvider,this.sessionStorageService.Object);
        }

        [Test]
        public async Task Verify_that_when_server_returns_with_success_authentication_returns_success()
        {
            var cts = new CancellationTokenSource();
            
            var result = await this.authenticationService.Login("John", "Doe", "http:www.rheagroup.com", cts.Token);

            Assert.That(result, Is.EqualTo(AuthenticationStatusKind.Success));
        }

        [Test]
        public async Task Verify_that_when_server_returns_without_success_authentication_returns_success()
        {
            var cts = new CancellationTokenSource();

            this.restClient.Setup(x =>
                    x.Open(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .Throws<Exception>();

            var result = await this.authenticationService.Login("John", "Doe", "http:www.rheagroup.com", cts.Token);

            Assert.That(result, Is.EqualTo(AuthenticationStatusKind.Fail));
        }

        [Test]
        public async Task Verify_that_when_logout_session_storage_is_called_to_remove_session_key()
        {
            var cts = new CancellationTokenSource();

            await this.authenticationService.Logout(cts.Token);

            this.sessionStorageService.Verify(x => x.RemoveItemAsync(AnonymousAuthenticationStateProvider.SessionStorageKey, cts.Token), Times.Once);
        }
    }
}
