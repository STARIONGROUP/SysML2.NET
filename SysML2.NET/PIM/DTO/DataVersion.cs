// ------------------------------------------------------------------------------------------------
// <copyright file="DataVersion.cs" company="Starion Group S.A.">
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
    /// A subclass of <see cref="Record"/> that represents <see cref="IData"/> at a specific version in its lifecycle.
    /// A <see cref="DataVersion"/> record is associated with only one (1) <see cref="DataIdentity"/> record. <see cref="DataVersion"/>
    /// serves as a wrapper for <see cref="IData"/> (payload) in the context of a <see cref="Commit"/> in a <see cref="Project"/>.
    /// </summary>
    public class DataVersion : Record
    {
        /// <summary>
        /// Gets or sets the <see cref="DTO.Project"/> <see cref="Commit"/> at which the wrapped data (payload) was created, modified, or deleted.
        /// </summary>
        [Property(xmiId: "_19_0_2_12e503d9_1577820469725_462619_66059", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        public Guid Commit { get; set; }

        /// <summary>
        /// Gets the <see cref="DTO.Project"/> that owns the <see cref="DataVersion"/>
        /// </summary>
        /// <remarks>
        /// Derived by Systems Modeling API and Services, Clause 7.1.2 from the owning project of <see cref="Commit"/>.
        /// </remarks>
        [Property(xmiId: "_19_0_2_12e503d9_1577820469730_358203_66069", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        public Guid project { get; internal set; }

        /// <summary>
        /// Gets or sets the contained <see cref="DataIdentity"/>
        /// </summary>
        [Property(xmiId: "_19_0_2_12e503d9_1577820469729_652900_66067", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        public DataIdentity Identity { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="IData"/> that is the payload
        /// </summary>
        /// <remarks>
        /// <c>null</c> when no data with the given identity is present at the <see cref="Commit"/>.
        /// </remarks>
        [Property(xmiId: "_19_0_2_12e503d9_1577820469731_381637_66070", aggregation: AggregationKind.None, lowerValue: 0, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        public IData Payload { get; set; }
    }
}
