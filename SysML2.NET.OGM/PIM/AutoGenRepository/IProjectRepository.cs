// -------------------------------------------------------------------------------------------------
// <copyright file="IProjectRepository.cs" company="Starion Group S.A.">
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

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.OGM.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    using SysML2.NET.Common;
    using SysML2.NET.PIM.DTO;

    /// <summary>
    /// The purpose of the <see cref="IProjectRepository"/> is to interact with the DGraph database
    /// </summary>
    public interface IProjectRepository
    {
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
        /// A <see cref="Task"/>
        /// </returns>
        Task Create(Project project, CancellationToken cancellationToken);

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
        Task<List<IData>> Read(Guid identifier, int page, int count, CancellationToken cancellationToken);
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
