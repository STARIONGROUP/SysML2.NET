// -------------------------------------------------------------------------------------------------
// <copyright file="IAuthenticationService.cs" company="RHEA System S.A.">
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
    using SySML2.NET.REST;
    using System.Threading;
    using System.Threading.Tasks;
    
    /// <summary>
    /// The purpose of the <see cref="IAuthenticationService"/> is to authenticate against
    /// a SysML2 model server
    /// </summary>
    public interface IAuthenticationService
    {
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
        Task<AuthenticationStatusKind> Login(string username, string password, string url, CancellationToken cancellationToken);

        /// <summary>
        /// Logout from SysML2 model server
        /// </summary>
        /// <returns>
        /// a <see cref="Task"/>
        /// </returns>
        Task Logout();
    }
}
