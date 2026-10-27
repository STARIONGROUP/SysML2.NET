// ------------------------------------------------------------------------------------------------
// <copyright file="Tag.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.PIM.POCO
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
        /// Gets or sets a reference to the <see cref="Commit"/> which the Tag is pointing. It represents a
        /// <see cref="Commit"/>-of-interest in the owning <see cref="Project"/>.
        /// </summary>
        [Property(xmiId: "_19_0_4_58901f1_1628778728157_220677_662", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        public Commit TaggedCommit
        {
            get => this.ReferencedCommit;
            set => this.ReferencedCommit = value;
        }
    }
}
