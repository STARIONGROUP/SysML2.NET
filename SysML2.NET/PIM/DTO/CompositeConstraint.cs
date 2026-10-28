// ------------------------------------------------------------------------------------------------
// <copyright file="CompositeConstraint.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.PIM.DTO
{
    using System.Collections.Generic;

    using SysML2.NET.Decorators;

    /// <summary>
    /// a concrete subtype of <see cref="Constraint"/> that represents complex conditions composed of two or more
    /// <see cref="CompositeConstraint"/>s or <see cref="PrimitiveConstraint"/>s using logical AND or OR operators
    /// </summary>
    public class CompositeConstraint : Constraint
    {
        /// <summary>
        /// Gets or sets the set of <see cref="Constraint"/>s being composed
        /// </summary>
        /// <remarks>
        /// There must be at least 2
        /// </remarks>
        [Property(xmiId: "_19_0_4_58901f1_1597377207847_92212_477", aggregation: AggregationKind.None, lowerValue: 2, upperValue: int.MaxValue, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        public List<Constraint> Constraint { get; set; } = [];

        /// <summary>
        /// Gets or sets the logical operator for composing the <see cref="Constraint"/>s
        /// </summary>
        [Property(xmiId: "_19_0_4_58901f1_1597377244778_333596_491", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        public JoinOperator Operator { get; set; }
    }
}
