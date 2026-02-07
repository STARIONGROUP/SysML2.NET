// -------------------------------------------------------------------------------------------------
// <copyright file="LogoutViewModelTextFixture.cs" company="Starion Group S.A.">
// 
//   Copyright 2022-2026 Starion Group S.A.
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

namespace SysML2.NET.Viewer.Tests.ViewModels.Components
{
    using System.Threading.Tasks;

    using Blazored.SessionStorage;

    using Bunit;
    using Bunit.TestDoubles;
    
    using TestContext = Bunit.TestContext;

    using Microsoft.Extensions.DependencyInjection;

    using Moq;

    using NUnit.Framework;

    using SySML2.NET.REST;
    using SysML2.NET.Viewer.Services.Authentication;
    using SysML2.NET.Viewer.ViewModels.Components;

    /// <summary>
    /// Suite of tests for the <see cref="LogoutViewModel"/>
    /// </summary>
    [TestFixture]
    public class LogoutViewModelTextFixture
    {
        private TestContext context;

        private FakeNavigationManager navigationManager;

        private LogoutViewModel logoutViewModel;

        private AuthenticationService authenticationService;

        private AnonymousAuthenticationStateProvider authenticationStateProvider;

        private Mock<ISessionStorageService> sessionStorageService;

        private Mock<IRestClient> restClient;

        [SetUp]
        public void SetUp()
        {
            this.context = new TestContext();

            this.sessionStorageService = new Mock<ISessionStorageService>();
            this.restClient = new Mock<IRestClient>();
            this.authenticationStateProvider = new AnonymousAuthenticationStateProvider(this.sessionStorageService.Object);
            
            this.authenticationService = new AuthenticationService(this.restClient.Object, this.authenticationStateProvider, this.sessionStorageService.Object);

            this.navigationManager = this.context.Services.GetRequiredService<FakeNavigationManager>();

            this.logoutViewModel = new LogoutViewModel(this.authenticationService, this.navigationManager);
        }

        [Test]
        public async Task Verify_that_logout_works_as_expected()
        {
            await this.logoutViewModel.ExecuteLogout();

            Assert.That(this.navigationManager.Uri, Is.EqualTo("http://localhost/"));
        }
    }
}
