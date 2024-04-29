// -------------------------------------------------------------------------------------------------
// <copyright file="LoginViewModel.cs" company="Starion Group S.A.">
// 
//   Copyright 2022-2023 Starion Group S.A.
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

    using Microsoft.AspNetCore.Components;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Logging.Abstractions;

    using ReactiveUI;

    using SySML2.NET.REST;
    using SysML2.NET.Viewer.Services.Authentication;

    /// <summary>
    /// The <see cref="LoginViewModel"/>
    /// </summary>
    public class LoginViewModel : ReactiveObject, ILoginViewModel
    {
        /// <summary>
        /// Backing field for <see cref="ErrorMessage" /> property
        /// </summary>
        private string errorMessage;

        /// <summary>
        /// Backing field for <see cref="UserName" /> property
        /// </summary>
        private string userName;

        /// <summary>
        /// Backing field for <see cref="Password" /> property
        /// </summary>
        private string password;

        /// <summary>
        /// Backing field for <see cref="Url" /> property
        /// </summary>
        private string url;

        /// <summary>
        /// Backing field for <see cref="AuthenticationStatusKind" />
        /// </summary>
        private AuthenticationStatusKind authenticationStatusKind;

        /// <summary>
        /// The (dependency) injected <see cref="IAuthenticationService"/>
        /// </summary>
        private readonly IAuthenticationService authenticationService;

        /// <summary>
        /// The (dependency) injected <see cref="NavigationManager"/>
        /// </summary>
        private readonly NavigationManager navigationManager;

        /// <summary>
        /// The <see cref="ILogger"/> used to log
        /// </summary>
        private readonly ILogger<LoginViewModel> logger;

        /// <summary>
        /// The <see cref="CancellationTokenSource"/> used to cancel asynchronous tasks.
        /// </summary>
        private CancellationTokenSource cancellationTokenSource;

        /// <summary>
        /// Initializes a new instance of the <see cref="LoginViewModel"/>
        /// </summary>
        /// <param name="authenticationService">
        /// The (dependency) injected <see cref="IAuthenticationService"/>
        /// </param>
        /// <param name="navigationManager">
        /// The (dependency) injected <see cref="NavigationManager"/>
        /// </param>
        /// <param name="loggerFactory">
        /// The (injected) <see cref="ILoggerFactory"/> used to setup logging
        /// </param>
        public LoginViewModel(IAuthenticationService authenticationService, NavigationManager navigationManager, ILoggerFactory loggerFactory = null)
        {
            this.UserName = "John Doe";
            this.Password = "secret";
            this.Url = "http://sysml2-sst.intercax.com:9000";
            
            this.authenticationService = authenticationService;
            this.navigationManager = navigationManager;
            this.logger = loggerFactory == null ? NullLogger<LoginViewModel>.Instance : loggerFactory.CreateLogger<LoginViewModel>();
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
        /// Gets or sets the username used to connect to the SysML V2 Model server with
        /// </summary>
        public string UserName
        {
            get => this.userName;
            set => this.RaiseAndSetIfChanged(ref this.userName, value);
        }

        /// <summary>
        /// Gets or sets the password used to connect to the SysML V2 Model server with
        /// </summary>
        public string Password
        {
            get => this.password;
            set => this.RaiseAndSetIfChanged(ref this.password, value);
        }

        /// <summary>
        /// Gets or sets the URl of the SysML V2 model server to connect to
        /// </summary>
        public string Url
        {
            get => this.url;
            set => this.RaiseAndSetIfChanged(ref this.url, value);
        }

        /// <summary>
        /// logs in to the SysML2 Model Server
        /// </summary>
        /// <returns>
        /// An awaitable <see cref="Task" />
        /// </returns>
        public async Task ExecuteLogin()
        {
            this.AuthenticationStatusKind = AuthenticationStatusKind.Authenticating;
            this.ErrorMessage = string.Empty;
            
            try
            {
                this.cancellationTokenSource = new CancellationTokenSource();

                this.AuthenticationStatusKind = await this.authenticationService.Login(this.UserName, this.Password, this.Url, this.cancellationTokenSource.Token);

                this.navigationManager.NavigateTo("/projects");
            }
            catch (Exception e)
            {
                this.cancellationTokenSource.Dispose();
                this.AuthenticationStatusKind = AuthenticationStatusKind.Fail;
                this.ErrorMessage = e.Message;

                this.logger.LogError("Login Failed", e);
            }
        }

        /// <summary>
        /// cancels the login operation
        /// </summary>
        public void ExecuteCancelLogin()
        {
            this.cancellationTokenSource?.Cancel();
        }
    }
}
