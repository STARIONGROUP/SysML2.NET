// ------------------------------------------------------------------------------------------------
// <copyright file="Record.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
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
        [Property(xmiId: "_19_0_2_12e503d9_1577820469733_493524_66075", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        public List<Guid> Commits { get; set; } = [];

        /// <summary>
        /// Gets or sets the set of all <see cref="CommitReference"/>s in the <see cref="Project"/>
        /// </summary>
        [Property(xmiId: "_19_0_4_58901f1_1628779129336_898482_712", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        public List<Guid> CommitReferences { get; set; } = [];

        /// <summary>
        /// Gets or sets the set of all <see cref="Branch"/>es in the <see cref="Project"/>
        /// </summary>
        /// <remarks>
        /// A <see cref="Project"/> has one or more branches; a default branch is created with the <see cref="Project"/>.
        /// </remarks>
        [Property(xmiId: "_19_0_4_58901f1_1614694201382_729306_245", aggregation: AggregationKind.None, lowerValue: 1, upperValue: int.MaxValue, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Project.CommitReferences")]
        public List<Guid> Branches { get; set; } = [];

        /// <summary>
        /// Gets or sets the set of all <see cref="Tag"/>s in the <see cref="Project"/>
        /// </summary>
        [Property(xmiId: "_19_0_4_58901f1_1628779424605_974596_740", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Project.CommitReferences")]
        public List<Guid> Tags { get; set; } = [];

        /// <summary>
        /// Gets or sets the <see cref="DateTime"/> when the project was created
        /// </summary>
        [Property(xmiId: "_19_0_4_f940371_1694730728649_158871_419", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        public DateTime Created { get; set; }

        /// <summary>
        /// Gets or sets the human readable name
        /// </summary>
        [Property(xmiId: "_19_0_4_58901f1_1671293441918_865671_7736", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [RedefinedProperty(propertyName: "Record.Name")]
        public new string Name { get; set; }

        /// <summary>
        /// Gets or sets the default <see cref="Branch"/> in the <see cref="Project"/> which is a subset of <see cref="Branch"/>
        /// </summary>
        [Property(xmiId: "_19_0_4_58901f1_1614694296040_474053_271", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Project.Branches")]
        public Guid DefaultBranch { get; set; }

        /// <summary>
        /// Gets or sets the collection of <see cref="Query" />
        /// </summary>
        [Property(xmiId: "_19_0_4_58901f1_1597396233220_492135_901", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        public List<Guid> Queries { get; set; } = [];
    }
}
