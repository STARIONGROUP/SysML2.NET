﻿// -------------------------------------------------------------------------------------------------
// <copyright file="Branch.cs" company="RHEA System S.A.">
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

namespace SysML2.NET.API.DTO
{
    using System;

    /// <summary>
    /// A <see cref="Branch"/> is a type of CommitReference. A <see cref="Branch"/> is a pointer to a <see cref="Commit"/> (Branch.head).
    /// The <see cref="Commit"/> history of a <see cref="Project"/> on a given branch can be computed by recursively navigating
    /// <see cref="Commit.PreviousCommit"/>, starting from the head <see cref="Commit"/> of the <see cref="Branch"/> (Branch.head)
    /// </summary>
    public class Branch : CommitReference
    {
        /// <summary>
        /// Gets or sets the timestamp at which the <see cref="Branch"/> was created.
        /// </summary>
        public DateTime CreationTimestamp { get; set; }

        /// <summary>
        /// Gets or sets the timestamp at which the <see cref="Branch"/> was deleted.
        /// </summary>
        public DateTime DeletionTimestamp { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="DateTime"/>
        /// </summary>
        public DateTime TimeStamp { get; set; }

        /// <summary>
        /// Gets or sets the commit to which the branch is currently pointing. It represents the latest state of the
        /// <see cref="Project"/> on the given branch
        /// </summary>
        public Guid Head { get; set; }

        /// <summary>
        /// Gets a human readable name for the <see cref="Branch"/>
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets <see cref="Project"/> that owns the given <see cref="Branch"/>
        /// </summary>
        public Guid OwningProject { get; set; }

        /// <summary>
        /// Gets or sets a reference to the referenced <see cref="Commit"/>
        /// </summary>
        public Guid ReferencedCommit { get; set; }
    }
}