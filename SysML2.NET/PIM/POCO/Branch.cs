// ------------------------------------------------------------------------------------------------
// <copyright file="Branch.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.PIM.POCO
{
    using SysML2.NET.Decorators;

    /// <summary>
    /// A <see cref="Branch"/> is a type of <see cref="CommitReference"/>. A <see cref="Branch"/> is a pointer to a <see cref="Commit"/> (Branch.head).
    /// The <see cref="Commit"/> history of a <see cref="Project"/> on a given <see cref="Branch"/> can be computed by recursively navigating
    /// <see cref="Commit.PreviousCommits"/>, starting from the head <see cref="Commit"/> of the <see cref="Branch"/> (Branch.head)
    /// </summary>
    public class Branch : CommitReference
    {
        /// <summary>
        /// Gets or sets the commit to which the branch is currently pointing. It represents the latest state of the
        /// <see cref="Project"/> on the given branch. Redefines <see cref="CommitReference.ReferencedCommit"/> —
        /// both properties expose the same underlying value.
        /// </summary>
        [Property(xmiId: "_19_0_4_58901f1_1614695877725_676676_515", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        public Commit Head
        {
            get => this.ReferencedCommit;
            set => this.ReferencedCommit = value;
        }
    }
}
