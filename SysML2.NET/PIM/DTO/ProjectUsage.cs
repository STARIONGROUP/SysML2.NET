// ------------------------------------------------------------------------------------------------
// <copyright file="ProjectUsage.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.PIM.DTO
{
    using System;

    using SysML2.NET.Common;
    using SysML2.NET.Decorators;

    /// <summary>
    /// a subclass of <see cref="Record"/> and a realization of <see cref="IData"/> that represents the use
    /// of a <see cref="Project"/> in the context of another <see cref="Project"/>. The constraint
    /// usedProject = usedProjectCommit.owningProject applies.
    /// </summary>
    public class ProjectUsage : IData
    {
        /// <summary>
        /// Gets or sets the unique identifier
        /// </summary>
        [Property(xmiId: "sysml2.net", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the reference to the <see cref="Commit"/> of the <see cref="Project"/> being used.
        /// </summary>
        [Property(xmiId: "_19_0_4_58901f1_1628380685338_895164_312", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        public Guid UsedProjectCommit { get; set; }

        /// <summary>
        /// Gets or sets the reference to the <see cref="Project"/> of the <see cref="Project"/> being used.
        /// </summary>
        [Property(xmiId: "sysml2.net", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        public Guid UsedProject { get; set; }
    }
}
