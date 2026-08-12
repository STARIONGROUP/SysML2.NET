// ------------------------------------------------------------------------------------------------
// <copyright file="DataIdentity.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.PIM.POCO
{
    using System.Collections.Generic;

    using SysML2.NET.Decorators;

    /// <summary>
    /// A subclass of <see cref="Record"/> that represents a unique, version-independent representation of <see cref="Common.IData"/>
    /// through its lifecycle. A <see cref="DataIdentity"/> is associated with 1 or more <see cref="Common.IData"/> Version
    /// records that represent different versions of the same <see cref="Common.IData"/>.
    /// </summary>
    public class DataIdentity : Record
    {
        /// <summary>
        /// Gets or sets the the set of <see cref="DataVersion"/> records representing all versions of the given <see cref="DataIdentity"/>
        /// </summary>
        [Property(xmiId: "_19_0_2_12e503d9_1577820469729_822777_66066", aggregation: AggregationKind.None, lowerValue: 1, upperValue: int.MaxValue, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        public List<DataVersion> Version { get; set; } = [];

        /// <summary>
        /// Gets or sets the derived reference to the <see cref="Commit"/> in a <see cref="Project"/> in
        /// which the identified data was created, or null when not computed
        /// </summary>
        public Commit CreatedAt { get; set; }

        /// <summary>
        /// Gets or sets the derived reference to the <see cref="Commit"/> in a <see cref="Project"/> in
        /// which the identified data was deleted, or null when the data is not deleted or the value is
        /// not computed
        /// </summary>
        public Commit DeletedAt { get; set; }
    }
}
