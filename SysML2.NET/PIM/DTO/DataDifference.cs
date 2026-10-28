// ------------------------------------------------------------------------------------------------
// <copyright file="DataDifference.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.PIM.DTO
{
    using System;

    using SysML2.NET.Decorators;

    /// <summary>
    /// Represents a single difference between two compared versions of the same <see cref="DataIdentity"/>
    /// </summary>
    public class DataDifference
    {
        /// <summary>
        /// Gets or sets the <see cref="DataVersion"/> taken as the baseline of the comparison, or <c>null</c> when the data was added
        /// </summary>
        [Property(xmiId: "_19_0_4_58901f1_1667597388218_19169_136", aggregation: AggregationKind.None, lowerValue: 0, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        public Guid? BaseData { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="DataVersion"/> compared against the baseline, or <c>null</c> when the data was deleted
        /// </summary>
        [Property(xmiId: "_19_0_4_58901f1_1667597418425_384059_139", aggregation: AggregationKind.None, lowerValue: 0, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        public Guid? CompareData { get; set; }
    }
}
