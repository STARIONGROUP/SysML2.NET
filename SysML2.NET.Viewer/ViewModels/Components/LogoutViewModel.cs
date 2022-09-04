// -------------------------------------------------------------------------------------------------
// <copyright file="LogoutViewModel.cs" company="RHEA System S.A.">
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
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Components;

    using SysML2.NET.Viewer.Services.Authentication;
    
    /// <summary>
    /// View model for the <see cref="LogoutComponent" /> component
    /// </summary>
    public class LogoutViewModel : ILogoutViewModel
    {
        /// <summary>
        /// The <see cref="IAuthenticationService" />
        /// </summary>
        private readonly IAuthenticationService authenticationService;

        /// <summary>
        /// The <see cref="IAuthenticationService" />
        /// </summary>
        private readonly NavigationManager navigationManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="LogoutViewModel" /> class.
        /// </summary>
        /// <param name="authenticationService">The <see cref="IAuthenticationService" /></param>
        /// <param name="navigationManager">The <see cref="Microsoft.AspNetCore.Components.NavigationManager" /></param>
        public LogoutViewModel(IAuthenticationService authenticationService, NavigationManager navigationManager)
        {
            this.authenticationService = authenticationService;
            this.navigationManager = navigationManager;
        }

        /// <summary>
        /// Logout from the SysML2 Model server
        /// </summary>
        /// <returns>A <see cref="Task" /></returns>
        public async Task ExecuteLogout()
        {
            await this.authenticationService.Logout();
            this.navigationManager.NavigateTo("/");
        }
    }
}
