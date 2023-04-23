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

namespace SysML2.NET.API.Services
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Common;

    using PIM.DTO;
    using SysML2.NET.API.PSM.AutoGenServices;

    /// <summary>
    /// The purpose of the <see cref="ProjectService"/> is to perform CRUD operations and to provide
    /// before and after hooks to inject custom service logic
    /// </summary>
    public partial class ProjectService
    {
        /// <summary>
        /// The (injected) <see cref="IBranchService"/>.
        /// </summary>
        public IBranchService BranchService { get; set; }

        /// <summary>
        /// Executes additional before the create method is executed. The provided <see cref="IData"/> instance
        /// may be altered in this method.
        /// </summary>
        /// <param name="data">
        /// The <see cref="IData"/> that is to be created
        /// </param>
        /// <param name="cancellationToken">
        /// The <see cref="CancellationToken"/> that can be used to cancel the operation
        /// </param>>
        /// <returns>
        /// returns true if the hook executed successfully and that the hooked operation can continue, returns
        /// false when the hooked operation needs to be exited
        /// </returns>
        public override async Task<bool> BeforeCreate(IData data, CancellationToken cancellationToken)
        {
            if (data is not Project project)
            {
                throw new ArgumentException("This is not a Project", nameof(data));
            }

            var commit = new Commit
            {
                Id = Guid.Parse("04969904ad5b4cc695042f0fba68fb3b"),

            };

            var branch = new Branch
            {
                Id = Guid.Parse("2deabbda92a140f5a2ff8af3e12f050e"),
                OwningProject = project.Id
            };

            project.Description = "this is an updated description updated in the before create";
            
            await this.BranchService.Create(branch, cancellationToken);
            
            return false;
        }
    }
}
