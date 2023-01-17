// -------------------------------------------------------------------------------------------------
// <copyright file="AuthenticationService.cs" company="RHEA System S.A.">
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

namespace SysML2.NET.Viewer.Services.Authentication
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using Blazored.SessionStorage;

    using Microsoft.AspNetCore.Components.Authorization;

    using SySML2.NET.REST;

    /// <summary>
    /// The purpose of the <see cref="AuthenticationService"/> is to authenticate against
    /// a SysML2 model server
    /// </summary>
    public class AuthenticationService : IAuthenticationService
    {
        /// <summary>
        /// The <see cref="ISessionStorageService" />
        /// </summary>
        private readonly ISessionStorageService sessionStorageService;

        /// <summary>
        /// The <see cref="AuthenticationStateProvider" />
        /// </summary>
        private readonly AuthenticationStateProvider authenticationStateProvider;

        /// <summary>
        /// The (injected) <see cref="IRestClient" /> used to communicate with the SysML2 model server
        /// </summary>
        private readonly IRestClient restClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthenticationService" /> class.
        /// </summary>
        /// <param name="restClient">
        /// The (injected) <see cref="IRestClient" /> used to communicate with the SysML2 model server
        /// </param>
        /// <param name="authenticationStateProvider">
        /// The (injected) <see cref="AuthenticationStateProvider" /> that Provides information about
        /// the authentication state of the current user.
        /// </param>
        /// <param name="sessionStorageService">
        /// The <see cref="ISessionStorageService" /> used to store and retrieve data from the browser session storage
        /// </param>
        public AuthenticationService(IRestClient restClient, AuthenticationStateProvider authenticationStateProvider, ISessionStorageService sessionStorageService)
        {
            this.restClient = restClient;
            this.authenticationStateProvider = authenticationStateProvider;
            this.sessionStorageService = sessionStorageService;
        }

        /// <summary>
        /// Authenticate against the specified SysML2 model server
        /// </summary>
        /// <param name="username">
        /// The username to authenticate with
        /// </param>
        /// <param name="password">
        /// The password (secret) to authenticate with
        /// </param>
        /// <param name="url">
        /// The URL of the SysML2 model server
        /// </param>
        /// <param name="cancellationToken">
        /// The <see cref="CancellationToken"/> used to cancel the operation
        /// </param>
        /// <returns>
        /// An <see cref="AuthenticationStatusKind"/>
        /// </returns>
        public async Task<AuthenticationStatusKind> Login(string username, string password, string url, CancellationToken cancellationToken)
        {
            try
            {
                var projects = await this.restClient.Open(username, password, url, cancellationToken);

                await this.sessionStorageService.SetItemAsync(AnonymousAuthenticationStateProvider.SessionStorageKey, username);

                ((AnonymousAuthenticationStateProvider)this.authenticationStateProvider).NotifyAuthenticationStateChanged();
                return AuthenticationStatusKind.Success;
            }
            catch (Exception e)
            {
                return AuthenticationStatusKind.Fail;
            }
        }

        /// <summary>
        /// Logout from SysML2 model server
        /// </summary>
        /// <returns>
        /// a <see cref="Task"/>
        /// </returns>
        public async Task Logout()
        {
            await this.sessionStorageService.RemoveItemAsync(AnonymousAuthenticationStateProvider.SessionStorageKey);
            ((AnonymousAuthenticationStateProvider)this.authenticationStateProvider).NotifyAuthenticationStateChanged();
        }
    }
}
