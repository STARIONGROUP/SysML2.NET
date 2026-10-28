// ------------------------------------------------------------------------------------------------
// <copyright file="MergeResult.cs" company="Starion Group S.A.">
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

    /// <summary>
    /// Represents the outcome of merging one <see cref="Commit"/> into another
    /// </summary>
    public class MergeResult
    {
        /// <summary>
        /// Gets or sets the <see cref="DataIdentity"/> records that could not be merged automatically
        /// </summary>
        [Property(xmiId: "_19_0_4_58901f1_1667600196883_169912_162", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        public List<Guid> Conflict { get; set; } = [];

        /// <summary>
        /// Gets or sets the <see cref="Commit"/> that records the merge, or <c>null</c> when the merge was unsuccessful
        /// </summary>
        [Property(xmiId: "_19_0_4_58901f1_1667600196883_581172_161", aggregation: AggregationKind.None, lowerValue: 0, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        public Guid? MergeCommit { get; set; }
    }
}