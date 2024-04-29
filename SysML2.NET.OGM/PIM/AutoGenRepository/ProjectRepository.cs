// -------------------------------------------------------------------------------------------------
// <copyright file="ProjectRepository.cs" company="Starion Group S.A.">
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

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.OGM.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    
    using Microsoft.Extensions.Logging;
    
    using SysML2.NET.Common;
    using SysML2.NET.PIM.DTO;

    /// <summary>
    /// The purpose of the <see cref="ProjectRepository"/> is to interact with the DGraph database
    /// </summary>
    public class ProjectRepository : DataRepository, IProjectRepository
    {
        private readonly ILogger<ProjectRepository> logger;

        /// <summary>
        /// Initializes new instance of the <see cref="ProjectRepository"/> class.
        /// </summary>
        /// <param name="logger">
        /// The (injected) <see cref="ILogger{ProjectRepository}"/>
        /// </param>
        public ProjectRepository(ILogger<ProjectRepository> logger)
        {
            this.logger = logger;
        }

        /// <summary>
        /// Create a new instance of <see cref="Project"/> in the DGraph database
        /// </summary>
        /// <param name="project">
        /// The subject <see cref="Project"/> that is to be created.
        /// </param>
        /// <param name="cancellationToken">
        /// The <see cref="CancellationToken"/> that can be used to cancel the operation
        /// </param>
        /// <returns>
        /// A awaitable <see cref="Task"/>
        /// </returns>
        public async Task Create(Project project, CancellationToken cancellationToken)
        {
            var continueAfterHook = await this.BeforeCreate(project, cancellationToken);

            if (!continueAfterHook)
            {
                return;
            }
            
            throw new NotImplementedException("implement Project create on DGraph");

            await this.AfterCreate(project, cancellationToken);
        }

        /// <summary>
        /// Reads <see cref="IEnumerable{Ting}"/> from the DGraph database
        /// </summary>
        /// <param name="identifier">
        /// The list of <see cref="Guid"/> to read from the database (the list acts as a filter).
        /// If the list is empty, then all <see cref="Project"/> objects are returned.
        /// </param>
        /// <param name="page">
        /// the page number used for pagination. The default value is -1, which means no pagination is used
        /// </param>
        /// <param name="count">
        /// The number of results per page
        /// </param>
        /// <param name="cancellationToken">
        /// The <see cref="CancellationToken"/> that can be used to cancel the operation
        /// </param>
        /// <remarks>
        /// An <see cref="List{IData}"/>
        /// </remarks>
        public async Task<List<IData>> Read(Guid identifier, int page, int count, CancellationToken cancellationToken)
        {
            var continueAfterHook = await this.BeforeRead(identifier, page, count, cancellationToken);

            if (!continueAfterHook)
            {
                return await Task.FromResult(Enumerable.Empty<IData>().ToList());
            }
            
            var sw = new Stopwatch();
            this.logger.LogDebug("begin transaction to read Project objects from the DGraph");

            // open transaction

            this.logger.LogDebug("start reading Project objects from DGraph");

            // read data from graph-db
            var projects = this.CreateData();

            List<IData> things = new List<IData>();
            
            things.AddRange(projects);

            this.logger.LogDebug("read {result} Project objects in {elapsed} [ms] from the graph", things.Count(), sw.ElapsedMilliseconds);

            // close transaction

            await this.AfterRead(things, identifier, page, count, cancellationToken);

            return await Task.FromResult(things);
        }

        /// <summary>
        /// Creates dummy data to test with until a proper DGraph connection can be made
        /// </summary>
        /// <returns>
        /// a list of <see cref="Project"/>
        /// </returns>
        private List<Project> CreateData()
        {
            var project_1 = new Project
            {
                Id = Guid.Parse("9b0e1914-3241-461e-b9ee-a3ff5120de4e"),
                Alias = new List<string> { "project alias 1", "project alias 2" },
                DefaultBranch = Guid.Parse("a910a705-7fbe-415f-9cbb-624bfadf6c20"),
                Description = "this is a description",
                Name = "test project",
            };
            
            var project_2 = new Project
            {
                Id = Guid.Parse("0cde5143-0ac1-47c8-950f-050f1cdef3a5"),
                Alias = new List<string> { "project alias 1", "project alias 2" },
                DefaultBranch = Guid.Parse("8c65591a-5024-4aee-bb8d-3458a113fe7c"),
                Description = "this is a description",
                Name = "project 2"
            };

            List<Project> projects = new List<Project> { project_1, project_2 };

            return projects;
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
