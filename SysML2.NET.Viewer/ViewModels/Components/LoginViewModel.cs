// -------------------------------------------------------------------------------------------------
// <copyright file="LoginViewModel.cs" company="RHEA System S.A.">
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

namespace SysML2.NET.Viewer.ViewModels.Components
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using ReactiveUI;

    using SySML2.NET.REST;

    /// <summary>
    /// The <see cref="LoginViewModel"/>
    /// </summary>
    public class LoginViewModel : ReactiveObject, ILoginViewModel
    {
        /// <summary>
        /// Backing field for <see cref="ErrorMessage" />
        /// </summary>
        private string errorMessage;

        /// <summary>
        /// Backing field for <see cref="AuthenticationStatusKind" />
        /// </summary>
        private AuthenticationStatusKind authenticationStatusKind;

        /// <summary>
        /// The (dependency) injected <see cref="IRestClient"/>
        /// </summary>
        private readonly IRestClient restClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="LoginViewModel"/>
        /// </summary>
        /// <param name="restClient">
        /// The (dependency) injected <see cref="IRestClient"/>
        /// </param>
        public LoginViewModel(IRestClient restClient)
        {
            this.restClient = restClient;
        }

        /// <summary>
        /// Gets or sets the <see cref="AuthenticationStatusKind" />
        /// </summary>
        public AuthenticationStatusKind AuthenticationStatusKind
        {
            get => this.authenticationStatusKind;
            set => this.RaiseAndSetIfChanged(ref this.authenticationStatusKind, value);
        }

        /// <summary>
        /// Gets or sets the login error message
        /// </summary>
        public string ErrorMessage
        {
            get => this.errorMessage;
            set => this.RaiseAndSetIfChanged(ref this.errorMessage, value);
        }

        /// <summary>
        /// logs in to the model server
        /// </summary>
        /// <returns>
        /// An awaitable <see cref="Task" />
        /// </returns>
        public async Task ExecuteLogin()
        {
            this.AuthenticationStatusKind = AuthenticationStatusKind.Authenticating;
            this.ErrorMessage = string.Empty;

            var uri = "http://sysml2-sst.intercax.com:9000";
            var cts = new CancellationTokenSource();

            try
            {
                await this.restClient.Open(null, null, uri, cts.Token);
                this.AuthenticationStatusKind = AuthenticationStatusKind.Success;
            }
            catch (Exception e)
            {
                this.AuthenticationStatusKind = AuthenticationStatusKind.Fail;
                this.ErrorMessage = e.Message;
            }
        }
    }
}
