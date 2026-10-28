// ------------------------------------------------------------------------------------------------
// <copyright file="Tag.cs" company="Starion Group S.A.">
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
    /// a <see cref="Tag"/> is used for annotating specific commits-of-interest during <see cref="Project"/> development,
    /// such as for representing <see cref="Project"/> milestones, releases, baselines, or snapshots. A <see cref="Project"/>
    /// can have 0 or more <see cref="Tag"/>s
    /// </summary>
    public class Tag : CommitReference
    {
        /// <summary>
        /// Gets or sets a reference to the tagged <see cref="Commit"/>. Redefines
        /// <see cref="CommitReference.ReferencedCommit"/> — both properties expose the same underlying value.
        /// </summary>
        [Property(xmiId: "_19_0_4_58901f1_1628778728157_220677_662", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        public Guid TaggedCommit
        {
            get => this.ReferencedCommit;
            set => this.ReferencedCommit = value;
        }
    }
}
