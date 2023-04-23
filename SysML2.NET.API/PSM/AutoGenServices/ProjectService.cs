// -------------------------------------------------------------------------------------------------
// <copyright file="ProjectService.cs" company="RHEA System S.A.">
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

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.API.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using Microsoft.Extensions.Logging;

    using OGM.Repository;

    using SysML2.NET.API.DataService;
    using SysML2.NET.API.PSM.AutoGenServices;
    using SysML2.NET.Common;
    using SysML2.NET.PIM.DTO;

    /// <summary>
    /// The purpose of the <see cref="ProjectService"/> is to perform CRUD operations and to provide
    /// before and after hooks to inject custom service logic
    /// </summary>
    public partial class ProjectService : DataService, IProjectService
    {
        /// <summary>
        /// The (injected) <see cref="ILogger"/>
        /// </summary>
        private readonly ILogger<ProjectService> logger;

        /// <summary>
        /// The (injected) <see cref="IProjectRepository"/> used to access the DGraph database
        /// </summary>
        private readonly IProjectRepository projectRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectService"/> class
        /// </summary>
        /// <param name="logger">
        /// The (injected) <see cref="ILogger{ProjectService}"/>
        /// </param>
        /// <param name="projectRepository">
        /// The (injected) <see cref="IProjectRepository"/> used to access the DGraph database
        /// </param>
        public ProjectService(ILogger<ProjectService> logger, IProjectRepository projectRepository)
        {
            this.logger = logger;
            this.projectRepository = projectRepository;
        }

        /// <summary>
        /// Create a new instance of <see cref="Project"/>
        /// </summary>
        /// <param name="project">
        /// The subject <see cref="Project"/> that is to be created.
        /// </param>
        /// <param name="cancellationToken">
        /// The <see cref="CancellationToken"/> that can be used to cancel the operation
        /// </param>
        /// <returns>
        /// A <see cref="Task"/>
        /// </returns>
        public async Task Create(Project project, CancellationToken cancellationToken)
        {
            var continueAfterHook = await this.BeforeCreate(project, cancellationToken);
            
            if (!continueAfterHook)
            {
                return;
            }

            await this.projectRepository.Create(project, cancellationToken);

            await this.AfterCreate(project, cancellationToken);
        }

        /// <summary>
        /// Reads <see cref="List{IData}"/>
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
        /// An <see cref="IEnumerable{IData}"/>
        /// </remarks>
        public async Task<List<IData>> Read(Guid identifier, int page, int count, CancellationToken cancellationToken)
        {
            var continueAfterHook = await this.BeforeRead(identifier, page, count, cancellationToken);

            if (!continueAfterHook)
            {
                return await Task.FromResult(Enumerable.Empty<IData>().ToList());
            }
            
            var dataItems = await this.projectRepository.Read(identifier, page, count, cancellationToken);
            
            await this.AfterRead(dataItems, identifier, page, count, cancellationToken);

            return dataItems;
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
