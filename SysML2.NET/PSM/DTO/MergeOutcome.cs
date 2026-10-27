// ------------------------------------------------------------------------------------------------
// <copyright file="MergeOutcome.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.PSM.DTO
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// The outcome of a merge, which is either the resulting commit or the data identities that conflict
    /// </summary>
    /// <remarks>
    /// Systems Modeling API and Services, Clause 7.2.3 requires exactly one of the two alternatives, and Table 9
    /// maps each onto a REST/HTTP PSM model of its own, so no single PSM model corresponds to <c>MergeResult</c>.
    /// </remarks>
    public sealed class MergeOutcome
    {
        /// <summary>
        /// The commit that is held when <see cref="Kind"/> is <see cref="MergeOutcomeKind.Merged"/>
        /// </summary>
        private readonly Commit mergeCommit;

        /// <summary>
        /// The data identities that are held when <see cref="Kind"/> is <see cref="MergeOutcomeKind.Conflicted"/>
        /// </summary>
        private readonly IReadOnlyList<DataIdentity> conflict;

        /// <summary>
        /// Initializes a new instance of the <see cref="MergeOutcome"/> class
        /// </summary>
        /// <param name="kind">The alternative that is held.</param>
        /// <param name="mergeCommit">The commit that is held.</param>
        /// <param name="conflict">The data identities that are held.</param>
        private MergeOutcome(MergeOutcomeKind kind, Commit mergeCommit, IReadOnlyList<DataIdentity> conflict)
        {
            this.Kind = kind;
            this.mergeCommit = mergeCommit;
            this.conflict = conflict;
        }

        /// <summary>
        /// Gets the alternative that is held
        /// </summary>
        public MergeOutcomeKind Kind { get; }

        /// <summary>
        /// Gets a value indicating whether the merge succeeded
        /// </summary>
        public bool IsMerged => this.Kind == MergeOutcomeKind.Merged;

        /// <summary>
        /// Creates a <see cref="MergeOutcome"/> that holds the commit of a successful merge
        /// </summary>
        /// <param name="mergeCommit">The commit to hold.</param>
        /// <returns>The created <see cref="MergeOutcome"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the commit is <c>null</c>.</exception>
        public static MergeOutcome Merged(Commit mergeCommit)
        {
            return mergeCommit == null
                ? throw new ArgumentNullException(nameof(mergeCommit))
                : new MergeOutcome(MergeOutcomeKind.Merged, mergeCommit, []);
        }

        /// <summary>
        /// Creates a <see cref="MergeOutcome"/> that holds the data identities of an unsuccessful merge
        /// </summary>
        /// <param name="conflict">The conflicting data identities to hold.</param>
        /// <returns>The created <see cref="MergeOutcome"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the sequence is <c>null</c>.</exception>
        /// <exception cref="ArgumentException">Thrown when the sequence is empty, which denotes no conflict.</exception>
        public static MergeOutcome Conflicted(IEnumerable<DataIdentity> conflict)
        {
            if (conflict == null)
            {
                throw new ArgumentNullException(nameof(conflict));
            }

            IReadOnlyList<DataIdentity> conflicting = [.. conflict];

            return conflicting.Count == 0
                ? throw new ArgumentException("An unsuccessful merge holds at least one conflicting data identity.", nameof(conflict))
                : new MergeOutcome(MergeOutcomeKind.Conflicted, null, conflicting);
        }

        /// <summary>
        /// Queries the commit of a successful merge
        /// </summary>
        /// <param name="outMergeCommit">The commit that is held, which is <c>null</c> when the merge was unsuccessful.</param>
        /// <returns>True when the merge succeeded.</returns>
        public bool TryGetMergeCommit(out Commit outMergeCommit)
        {
            outMergeCommit = this.mergeCommit;

            return this.Kind == MergeOutcomeKind.Merged;
        }

        /// <summary>
        /// Queries the conflicting data identities of an unsuccessful merge
        /// </summary>
        /// <param name="conflicts">The data identities that are held, which is empty when the merge succeeded.</param>
        /// <returns>True when the merge was unsuccessful.</returns>
        public bool TryGetConflict(out IReadOnlyList<DataIdentity> conflicts)
        {
            conflicts = this.conflict;

            return this.Kind == MergeOutcomeKind.Conflicted;
        }

        /// <summary>
        /// Applies the function that corresponds to the alternative that is held
        /// </summary>
        /// <typeparam name="T">The type that the functions return.</typeparam>
        /// <param name="onMerged">The function applied to the commit of a successful merge.</param>
        /// <param name="onConflicted">The function applied to the data identities of an unsuccessful merge.</param>
        /// <returns>The result of the applied function.</returns>
        /// <exception cref="ArgumentNullException">Thrown when either of the functions is <c>null</c>.</exception>
        public T Match<T>(Func<Commit, T> onMerged, Func<IReadOnlyList<DataIdentity>, T> onConflicted)
        {
            if (onMerged == null)
            {
                throw new ArgumentNullException(nameof(onMerged));
            }

            if (onConflicted == null)
            {
                throw new ArgumentNullException(nameof(onConflicted));
            }

            return this.Kind == MergeOutcomeKind.Merged
                ? onMerged(this.mergeCommit)
                : onConflicted(this.conflict);
        }
    }
}
