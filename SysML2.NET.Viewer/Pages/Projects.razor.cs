// -------------------------------------------------------------------------------------------------
// <copyright file="Projects.razor.cs" company="RHEA System S.A.">
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

namespace SysML2.NET.Viewer.Pages
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Components;

    using Serilog;

    using SysML2.NET.PIM.DTO;
    using SySML2.NET.REST;

    /// <summary>
    /// Code-behind for the <see cref="Projects"/> page
    /// </summary>
    public partial class Projects
    {
        /// <summary>
        /// The <see cref="Project"/>s that are loaded from the SysML2 model server
        /// </summary>
        private IEnumerable<Project> projects;

        /// <summary>
        /// a value indicating whether the page is loading or not
        /// </summary>
        private bool isLoading = true;

        /// <summary>
        /// a value that states the total amount of <see cref="Project"/>s in SysML2 model server
        /// </summary>
        private int count;

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

            await this.LoadData();
        }

        /// <summary>
        /// Gets or sets the injected <see cref="IRestClient"/> used to communicate with the SysML2 model server
        /// </summary>
        [Inject]
        public IRestClient RestClient { get; set; }

        /// <summary>
        /// Load the data
        /// </summary>
        protected async Task LoadData()
        {
            try
            {
                isLoading = true;

                var cts = new CancellationTokenSource();

                this.projects = await this.RestClient.RequestProjects(null, null, cts.Token);

                this.count = this.projects.Count();
            }
            catch (Exception e)
            {
                Log.ForContext<Projects>().Error(e, "LoadData Failed");
            }
            finally
            {
                isLoading = false;
                StateHasChanged();
            }
        }
    }
}
