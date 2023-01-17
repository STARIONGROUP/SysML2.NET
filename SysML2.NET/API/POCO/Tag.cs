// -------------------------------------------------------------------------------------------------
// <copyright file="Tag.cs" company="RHEA System S.A.">
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

namespace SysML2.NET.API.POCO
{
    using System;

    /// <summary>
    /// a <see cref="Tag"/> is used for annotating specific commits-of-interest during <see cref="Project"/> development,
    /// such as for representing <see cref="Project"/> milestones, releases, baselines, or snapshots. A <see cref="Project"/>
    /// can have 0 or more <see cref="Tag"/>s
    /// </summary>
    public class Tag : CommitReference
    {
        /// <summary>
        /// Gets or sets a reference to the tagged <see cref="Commit"/>
        /// </summary>
        public Commit TaggedCommit { get; set; }

        /// <summary>
        /// Gets a human readable name for the <see cref="Tag"/>
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets a reference to the <see cref="Project"/> that owns the <see cref="Tag"/>
        /// </summary>
        public Project OwningProject { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="DateTime"/> the <see cref="Tag"/> was created
        /// </summary>
        public DateTime TimeStamp { get; set; }
    }
}
