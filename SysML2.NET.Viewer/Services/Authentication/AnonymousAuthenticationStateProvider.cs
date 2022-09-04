// -------------------------------------------------------------------------------------------------
// <copyright file="AnonymousAuthenticationStateProvider.cs" company="RHEA System S.A.">
// 
//   Copyright 2022 RHEA System S.A.
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
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Blazored.SessionStorage;

    using Microsoft.AspNetCore.Components.Authorization;
    
    /// <summary>
    /// Provides information about the authentication state of the current user.
    /// </summary>
    public class AnonymousAuthenticationStateProvider : AuthenticationStateProvider
	{
        /// <summary>
        /// Value for the <see cref="ISessionStorageService" /> key for the JWT
        /// </summary>
        public const string SessionStorageKey = "authenticationToken";
        
        /// <summary>
        /// The <see cref="ISessionStorageService" /> to retrieve saved JWT
        /// </summary>
        private readonly ISessionStorageService sessionStorage;

        /// <summary>
        /// Initializes a new <see cref="AnonymousAuthenticationStateProvider" />
        /// </summary>
        /// <param name="sessionStorage">The <see cref="ISessionStorageService" /></param>
        public AnonymousAuthenticationStateProvider(ISessionStorageService sessionStorage)
        {
            this.sessionStorage = sessionStorage;
        }

        /// <summary>
        /// Asynchronously gets an <see cref="AuthenticationState" /> that describes the current user.
        /// </summary>
        /// <returns>
        /// A task that, when resolved, gives an <see cref="AuthenticationState" /> instance that describes the current user.
        /// </returns>
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var token = await this.sessionStorage.GetItemAsync<string>(SessionStorageKey);

            if (string.IsNullOrEmpty(token))
            {
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            }

            var identity = CreateClaimsIdentity();
            
            return await Task.FromResult(new AuthenticationState(new ClaimsPrincipal(identity)));
        }

        /// <summary>
        /// Creates an anonymous <see cref="ClaimsIdentity"/>
        /// </summary>
        /// <returns>
        /// an instance of <see cref="ClaimsIdentity"/>
        /// </returns>
        private static ClaimsIdentity CreateClaimsIdentity()
        {
            ClaimsIdentity identity;
            
            var claim = new List<Claim>
            {
                new Claim(ClaimTypes.Name, "John Doe")
            };

            identity = new ClaimsIdentity(claim, "SysML V2 Authenticated");
            
            return identity;
        }

        /// <summary>
        /// Force the <see cref="NotifyAuthenticationStateChanged"/> event to be raised
        /// </summary>
        public void NotifyAuthenticationStateChanged()
        {
            this.NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }
    }
}
