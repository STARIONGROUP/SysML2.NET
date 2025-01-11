// -------------------------------------------------------------------------------------------------
// <copyright file="Branch.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.PIM.POCO
{
    /// <summary>
    /// A <see cref="Branch"/> is a type of <see cref="CommitReference"/>. A <see cref="Branch"/> is a pointer to a <see cref="Commit"/> (Branch.head).
    /// The <see cref="Commit"/> history of a <see cref="Project"/> on a given <see cref="Branch"/> can be computed by recursively navigating
    /// <see cref="Commit.PreviousCommit"/>, starting from the head <see cref="Commit"/> of the <see cref="Branch"/> (Branch.head)
    /// </summary>
    public class Branch : CommitReference
    {
        /// <summary>
        /// Gets or sets the commit to which the branch is currently pointing. It represents the latest state of the
        /// <see cref="Project"/> on the given branch
        /// </summary>
        public Commit Head { get; set; }
    }
}