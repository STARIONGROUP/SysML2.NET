// -------------------------------------------------------------------------------------------------
// <copyright file="ProjectDetais.razor.cs" company="RHEA System S.A.">
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

namespace SysML2.NET.Viewer.Pages
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Components;

    /// <summary>
    /// Code-behind for the <see cref="ProjectDetails"/> page
    /// </summary>
    public partial class ProjectDetails
    {
        [Parameter]
        public string Id { get; set; }

        /// <summary>
        /// a value indicating whether the page is loading or not
        /// </summary>
        private bool isLoading = true;

        /// <summary>
        /// Method invoked when the component is ready to start, having received its
        /// initial parameters from its parent in the render tree.
        /// Override this method if you will perform an asynchronous operation and
        /// want the component to refresh when that operation is completed.
        /// </summary>
        /// <returns>A <see cref="Task" /> representing any asynchronous operation.</returns>
        protected override async Task OnInitializedAsync()
        {
            isLoading = false;
        }
    }
}
