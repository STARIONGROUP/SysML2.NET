// -------------------------------------------------------------------------------------------------
// <copyright file="ICommitHistoryService.cs" company="RHEA System S.A.">
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

namespace SysML2.NET.Viewer.Services.CommitHistory
{
    using System.Collections.Generic;

    using SysML2.NET.API.DTO;

    /// <summary>
    /// The purpose of the <see cref="CommitHistoryService"/> is to compute the
    /// <see cref="Commit"/> history of a branch
    /// </summary>
    public interface ICommitHistoryService
    {
        /// <summary>
        /// Queries the <see cref="Commit"/>s that belong to a <see cref="Branch"/> and
        /// returns them in array ordered using the commit history
        /// </summary>
        /// <param name="branch">
        /// The owning <see cref="Branch"/> of the <see cref="Commit"/>s
        /// </param>
        /// <param name="commits">
        /// A list of <see cref="Commit"/>s
        /// </param>
        /// <returns>
        /// An array of <see cref="Commit"/> where the <see cref="Commit"/>s are returned in historical order
        /// using the <see cref="Commit.PreviousCommit"/> property. The latest commit (Head) is returned first.
        /// </returns>
        Commit[] QueryCommitHistory(Branch branch, IEnumerable<Commit> commits);
    }
}
