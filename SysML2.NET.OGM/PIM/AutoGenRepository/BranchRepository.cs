// -------------------------------------------------------------------------------------------------
// <copyright file="BranchRepository.cs" company="RHEA System S.A.">
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
    /// The purpose of the <see cref="BranchRepository"/> is to interact with the DGraph database
    /// </summary>
    public class BranchRepository : DataRepository, IBranchRepository
    {
        private readonly ILogger<BranchRepository> logger;

        /// <summary>
        /// Initializes new instance of the <see cref="BranchRepository"/> class.
        /// </summary>
        /// <param name="logger">
        /// The (injected) <see cref="ILogger{BranchRepository}"/>
        /// </param>
        public BranchRepository(ILogger<BranchRepository> logger)
        {
            this.logger = logger;
        }

        /// <summary>
        /// Create a new instance of <see cref="Branch"/> in the DGraph database
        /// </summary>
        /// <param name="branch">
        /// The subject <see cref="Branch"/> that is to be created.
        /// </param>
        /// <param name="cancellationToken">
        /// The <see cref="CancellationToken"/> that can be used to cancel the operation
        /// </param>
        /// <returns>
        /// A <see cref="Task"/>
        /// </returns>
        public async Task Create(Branch branch, CancellationToken cancellationToken)
        {
            var continueAfterHook = await this.BeforeCreate(branch, cancellationToken);

            if (!continueAfterHook)
            {
                return;
            }

            throw new NotImplementedException("implement Branch create on DGraph");

            await this.AfterCreate(branch, cancellationToken);
        }

        /// <summary>
        /// Reads <see cref="IEnumerable{IData}"/> from the DGraph database
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

            var sw = new Stopwatch();
            this.logger.LogDebug("begin transaction to read Branch objects from the DGraph");

            // open transaction

            this.logger.LogDebug("start reading Branch objects from DGraph");

            // read data from graph-db
            var branches = this.CreateData();

            List<IData> things = new List<IData>();

            things.AddRange(branches);

            this.logger.LogDebug("read {result} Project objects in {elapsed} [ms] from the graph", things.Count(), sw.ElapsedMilliseconds);

            // close transaction

            await this.AfterRead(things, identifier, page, count, cancellationToken);

            return await Task.FromResult(things);
        }

        /// <summary>
        /// Creates dummy data to test with until a proper DGraph connection can be made
        /// </summary>
        /// <returns>
        /// a list of <see cref="Branch"/>
        /// </returns>
        private List<Branch> CreateData()
        {
            var branch_1 = new Branch
            {
                Id = Guid.Parse("a6ba210d32a94c2986fe3b9a92f88133"),
                Alias = new List<string> { "branch alias 1", "branch alias 2" },
                CreationTimestamp = new DateTime(1976, 08, 20),
                //DefaultBranch = Guid.Parse("a910a705-7fbe-415f-9cbb-624bfadf6c20"),
                Description = "this is a description for branch 1",
                Name = "master",
            };

            var branch_2 = new Branch
            {
                Id = Guid.Parse("d9077028cf1e4845b8a2569ce8e31722"),
                Alias = new List<string> { "branch alias 1", "branch alias 2" },
                CreationTimestamp = new DateTime(1976, 08, 20),
                //DefaultBranch = Guid.Parse("8c65591a-5024-4aee-bb8d-3458a113fe7c"),
                Description = "this is a description for branch 2",
                Name = "development"
            };

            List<Branch> branches = new List<Branch> { branch_1, branch_2 };

            return branches;
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------