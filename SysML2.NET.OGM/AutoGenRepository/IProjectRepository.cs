﻿// -------------------------------------------------------------------------------------------------
// <copyright file="IProjectRepository.cs" company="RHEA System S.A.">
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
    using System.Threading.Tasks;

    using SysML2.NET.Common;
    using SysML2.NET.PIM.DTO;

    /// <summary>
    /// The purpose of the <see cref="IProjectRepository"/> is to interact with the DGraph database
    /// </summary>
    public interface IProjectRepository
    {
        /// <summary>
        /// Create a new instance of <see cref="Project"/> in the Neo4j database
        /// </summary>
        /// <param name="project">
        /// The subject <see cref="Project"/> that is to be created.
        /// </param>
        /// <returns>
        /// A <see cref="Task"/>
        /// </returns>
        Task Create(Project project);

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
        Task<IEnumerable<IData>> Read(Guid identifier, int page, int count);
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
