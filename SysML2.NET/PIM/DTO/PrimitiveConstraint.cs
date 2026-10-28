// ------------------------------------------------------------------------------------------------
// <copyright file="PrimitiveConstraint.cs" company="Starion Group S.A.">
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
    /// a concrete subtype of <see cref="Constraint"/> that represents simple conditions that be modeled using the
    /// property-operator -value tuple. e.g. mass &lt;= 4 kg, or type instanceOf Generalization
    /// </summary>
    public class PrimitiveConstraint : Constraint
    {
        /// <summary>
        /// Gets or sets the name of a property of Element or its subtypes that is being constrained
        /// </summary>
        [Property(xmiId: "_19_0_4_58901f1_1597377425985_159367_499", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        public string Property { get; set; }

        /// <summary>
        /// Gets or sets the mathematical operators
        /// </summary>
        [Property(xmiId: "_19_0_4_58901f1_1597377427373_325175_501", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        public Operator Operator { get; set; }

        /// <summary>
        /// Gets or sets list of primitive objects, such as String, Boolean, Integer, Double, and UUID
        /// </summary>
        [Property(xmiId: "_19_0_4_58901f1_1597377431228_229452_503", aggregation: AggregationKind.None, lowerValue: 1, upperValue: int.MaxValue, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        public List<string> Value { get; set;} = [];

        /// <summary>
        /// Gets or sets a value indicating a logical NOT operator is applied to the <see cref="PrimitiveConstraint"/>
        /// </summary>
        [Property(xmiId: "_19_0_4_58901f1_1597377338703_200864_497", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        public bool Inverse { get; set; }
    }
}
