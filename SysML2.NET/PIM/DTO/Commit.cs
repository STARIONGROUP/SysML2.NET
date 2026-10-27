// -------------------------------------------------------------------------------------------------
// <copyright file="Commit.cs" company="Starion Group S.A.">
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
    /// A subclass of <see cref="Record"/> that represents the changes made to a <see cref="Project"/> at a
    /// specific point in time in its lifecycle, such as the creation, update, or deletion of data in
    /// a <see cref="Project"/>. A <see cref="Project"/> has 0 or more <see cref="Commit"/>s
    /// </summary>
    /// <remarks>
    /// <see cref="Commit"/>s are immutable. For a given <see cref="Commit"/> record, the value of <see cref="Change"/>
    /// cannot be modified after a <see cref="Commit"/> has been created. If a modification is required, a new <see cref="Commit"/>
    /// record can be created with a different value of <see cref="Change"/>.
    /// <see cref="Commit"/>s are not destructible1. A <see cref="Commit"/> record cannot be deleted during normal
    /// end-user operation. <see cref="Commit"/>s represent the history and evolution of a <see cref="Project"/>.
    /// Deleting and mutating <see cref="Commit"/> records must be disabled for the normal end-user operations
    /// to preserve <see cref="Project"/> history.
    /// </remarks>
    public class Commit : Record
    {
        /// <summary>
        /// Gets or sets the timestamp at which the <see cref="Commit"/> was created
        /// </summary>
        [Property(xmiId: "_19_0_2_12e503d9_1577820469727_54677_66063", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        public DateTime Created { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="Project"/> that owns the <see cref="Commit"/>.
        /// </summary>
        [Property(xmiId: "_19_0_2_12e503d9_1577820469728_610810_66065", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        public Guid OwningProject { get; set; }

        /// <summary>
        /// Gets or sets the set of immediately preceding <see cref="Commit"/>s
        /// </summary>
        [Property(xmiId: "_19_0_2_12e503d9_1577820469727_74119_66064", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        public List<Guid> PreviousCommits { get; set; } = [];

        /// <summary>
        /// Gets or sets the set of <see cref="DataVersion"/> records representing data that is created, updated,
        /// or deleted in the <see cref="Commit"/>
        /// </summary>
        /// <remarks>
        /// A deletion is recorded by a <see cref="DataVersion"/> whose payload is <c>null</c>.
        /// </remarks>
        [Property(xmiId: "_19_0_2_12e503d9_1577820469724_226337_66058", aggregation: AggregationKind.None, lowerValue: 1, upperValue: int.MaxValue, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        public List<DataVersion> Change { get; set; } = [];

        /// <summary>
        /// Gets or sets the set of cumulative <see cref="DataVersion"/> records in a <see cref="Project"/> at the <see cref="Commit"/>
        /// </summary>
        /// <remarks>
        /// Derived by Systems Modeling API and Services, Clause 7.1.2 from <see cref="Change"/> and the
        /// versioned data of <see cref="PreviousCommits"/>.
        /// </remarks>
        [Property(xmiId: "_19_0_4_58901f1_1629496059367_814933_41", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        public List<DataVersion> VersionedData { get; internal set; } = [];
    }
}
