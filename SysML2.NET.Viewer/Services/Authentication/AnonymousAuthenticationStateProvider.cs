// -------------------------------------------------------------------------------------------------
// <copyright file="AnonymousAuthenticationStateProvider.cs" company="Starion Group S.A.">
// 
//   Copyright 2022-2025 Starion Group S.A.
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
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Logging.Abstractions;

	/// <summary>
	/// Provides information about the authentication state of the current user.
	/// </summary>
	public class AnonymousAuthenticationStateProvider : AuthenticationStateProvider
	{
		/// <summary>
		/// The <see cref="ILogger"/> used to log
		/// </summary>
		private readonly ILogger<AnonymousAuthenticationStateProvider> logger;

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
		/// <param name="sessionStorage">The <see cref="ISessionStorageService" />
		/// The (injected) <see cref="ISessionStorageService"/> that provides access to the browser session cache
		/// </param>
		/// <param name="loggerFactory">
		/// The (injected) <see cref="ILoggerFactory"/> used to setup logging
		/// </param>
		public AnonymousAuthenticationStateProvider(ISessionStorageService sessionStorage, ILoggerFactory loggerFactory = null)
        {
	        this.logger = loggerFactory == null ? NullLogger<AnonymousAuthenticationStateProvider>.Instance : loggerFactory.CreateLogger<AnonymousAuthenticationStateProvider>();

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

			this.logger.LogDebug("Authentication token retrieved from Session Storage");

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
