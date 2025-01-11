// -------------------------------------------------------------------------------------------------
// <copyright file="ILoginViewModel.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.Viewer.ViewModels.Components
{
    using System.ComponentModel.DataAnnotations;
    using System.Threading.Tasks;

    using SySML2.NET.REST;

    /// <summary>
    /// Interface definition for <see cref="LoginViewModel" />
    /// </summary>
    public interface ILoginViewModel
    {
        /// <summary>
        /// Gets or sets the <see cref="AuthenticationStatusKind" />
        /// </summary>
        AuthenticationStatusKind AuthenticationStatusKind { get; set; }

        /// <summary>
        /// Gets or sets the login error message
        /// </summary>
        string ErrorMessage { get; set; }

        /// <summary>
        /// Gets or sets the username used to connect to the SysML V2 Model server with
        /// </summary>
        [Required]
        string UserName { get; set; }

        /// <summary>
        /// Gets or sets the password used to connect to the SysML V2 Model server with
        /// </summary>
        [Required]
        string Password { get; set; }

        /// <summary>
        /// Gets or sets the URl of the SysML V2 model server to connect to
        /// </summary>
        [Required]
        string Url { get; set; }

        /// <summary>
        /// logs in to the SysML2 Model Server
        /// </summary>
        /// <returns>
        /// An awaitable <see cref="Task" />
        /// </returns>
        Task ExecuteLogin();

        /// <summary>
        /// cancels the login operation
        /// </summary>
        void ExecuteCancelLogin();
    }
}
