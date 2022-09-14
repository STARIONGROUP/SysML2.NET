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
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Components;

    using Serilog;

    using SysML2.NET.API.DTO;
    using SySML2.NET.REST;

    using SysML2.NET.Viewer.Services.CommitHistory;

    /// <summary>
    /// Code-behind for the <see cref="ProjectDetails"/> page
    /// </summary>
    public partial class ProjectDetails
    {
        /// <summary>
        /// Gets or sets the unique identifier of the <see cref="project"/> that
        /// is the subject of the <see cref="ProjectDetails"/> page
        /// </summary>
        [Parameter]
        public string Id { get; set; }

        /// <summary>
        /// a value indicating whether the page is loading or not
        /// </summary>
        private bool isLoading = true;

        /// <summary>
        /// The selected <see cref="Project"/>
        /// </summary>
        private Project project;

        /// <summary>
        /// The <see cref="Branch"/>es that are contained by the
        /// selected <see cref="Project"/> loaded from the SysML2 model server
        /// </summary>
        private IEnumerable<Branch> branches;

        /// <summary>
        /// The <see cref="Commit"/>s that are contained by the
        /// selected <see cref="Project"/> loaded from the SysML2 model server
        /// </summary>
        private IEnumerable<Commit> commits;

        /// <summary>
        /// The <see cref="Commit"/> history for the selected Branch
        /// </summary>
        private Commit[] commitHistory;

        /// <summary>
        /// The <see cref="Tag"/>s that are contained by the
        /// selected <see cref="Project"/> loaded from the SysML2 model server
        /// </summary>
        private IEnumerable<Tag> tags;

        /// <summary>
        /// The default <see cref="Branch"/> of the current <see cref="Project"/>
        /// </summary>
        private Branch defaultBranch;

        /// <summary>
        /// The selected Branch for which the <see cref="Commit"/> history is displayed
        /// </summary>
        private Guid selectedBranchId;

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
        /// Gets or sets the injected <see cref="ICommitHistoryService"/> used to compute the <see cref="Commit"/>
        /// history of a <see cref="Branch"/>
        /// </summary>
        [Inject]
        public ICommitHistoryService CommitHistoryService { get; set; }

        /// <summary>
        /// Load the data
        /// </summary>
        protected async Task LoadData()
        {
            try
            {
                isLoading = true;

                var cts = new CancellationTokenSource();
                var projects = await this.RestClient.RequestProjects(Guid.Parse(this.Id), null, cts.Token);

                this.project = projects.Single();
                
                this.branches = await this.RestClient.RequestBranches(this.project.Id, null, null, cts.Token);

                this.defaultBranch = this.branches.SingleOrDefault(x => x.Id == this.project.DefaultBranch);
                if (this.defaultBranch != null)
                {
                    this.selectedBranchId = this.defaultBranch.Id;
                }
                else
                {
                    this.selectedBranchId = this.branches.FirstOrDefault()!.Id;
                }
                
                this.commits = await this.RestClient.RequestCommits(this.project.Id, null, null, cts.Token);
                this.tags = await this.RestClient.RequestTags(this.project.Id, null, null, cts.Token);
                
                this.commitHistory = this.CommitHistoryService.QueryCommitHistory(this.branches.Single(x => x.Id == this.selectedBranchId), this.commits);
            }
            catch (Exception e)
            {
                Log.ForContext<ProjectDetails>().Error(e, "LoadData Failed");
            }
            finally
            {
                isLoading = false;
                StateHasChanged();
            }
        }

        /// <summary>
        /// event handler for the change event of the branch combo-box
        /// </summary>
        /// <param name="value">
        /// The identifier of the selected <see cref="Branch"/>
        /// </param>
        private void BranchSelectionChange(object value)
        {
            var identifier = value.ToString();

            if (!string.IsNullOrEmpty(identifier))
            {
                this.selectedBranchId = Guid.Parse(identifier);
                this.commitHistory = this.CommitHistoryService.QueryCommitHistory(this.branches.Single(x => x.Id == this.selectedBranchId), this.commits);
            }
        }
    }
}
