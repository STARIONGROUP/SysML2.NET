// -------------------------------------------------------------------------------------------------
// <copyright file="Record.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.PIM.DTO
{
    using System;
    using System.Collections.Generic;

    using SysML2.NET.Decorators;
    using SysML2.NET.PIM;

    /// <summary>
    /// A subclass of <see cref="Record"/> that represents a container for other <see cref="Record"/>s and
    /// an entry point for version management and data navigation
    /// </summary>
    public class Project : Record
    {
        /// <summary>
        /// Gets or sets the collection of <see cref="Commit" />
        /// </summary>
        [Property(lowerValue: 0, upperValue: int.MaxValue)]
        public List<Guid> Commits { get; set; } = [];

        /// <summary>
        /// Gets or sets the set of all <see cref="CommitReference"/>s in the <see cref="Project"/>
        /// </summary>
        [Property(lowerValue: 0, upperValue: int.MaxValue)]
        public List<Guid> CommitReferences { get; set; } = [];

        /// <summary>
        /// Gets or sets the set of all <see cref="Branch"/>es in the <see cref="Project"/>
        /// </summary>
        /// <remarks>
        /// A <see cref="Project"/> has one or more branches; a default branch is created with the <see cref="Project"/>.
        /// </remarks>
        [Property(lowerValue: 1, upperValue: int.MaxValue)]
        [SubsettedProperty(propertyName: "Project.CommitReferences")]
        public List<Guid> Branches { get; set; } = [];

        /// <summary>
        /// Gets or sets the set of all <see cref="Tag"/>s in the <see cref="Project"/>
        /// </summary>
        [Property(lowerValue: 0, upperValue: int.MaxValue)]
        [SubsettedProperty(propertyName: "Project.CommitReferences")]
        public List<Guid> Tags { get; set; } = [];

        /// <summary>
        /// Gets or sets the <see cref="DateTime"/> when the project was created
        /// </summary>
        [Property(lowerValue: 1, upperValue: 1)]
        public DateTime Created { get; set; }

        /// <summary>
        /// Gets or sets the human readable name
        /// </summary>
        [Property(lowerValue: 1, upperValue: 1)]
        [RedefinedProperty(propertyName: "Record.Name")]
        public new string Name { get; set; }

        /// <summary>
        /// Gets or sets the default <see cref="Branch"/> in the <see cref="Project"/> which is a subset of <see cref="Branch"/>
        /// </summary>
        [Property(lowerValue: 1, upperValue: 1)]
        [SubsettedProperty(propertyName: "Project.Branches")]
        public Guid DefaultBranch { get; set; }

        /// <summary>
        /// Gets or sets the collection of <see cref="Query" />
        /// </summary>
        [Property(lowerValue: 0, upperValue: int.MaxValue)]
        public List<Guid> Queries { get; set; } = [];
    }
}
