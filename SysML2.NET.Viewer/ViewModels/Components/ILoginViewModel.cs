// -------------------------------------------------------------------------------------------------
// <copyright file="ILoginViewModel.cs" company="RHEA System S.A.">
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
        /// logs in to the model server
        /// </summary>
        /// <returns>
        /// An awaitable <see cref="Task" />
        /// </returns>
        Task ExecuteLogin();
    }
}
