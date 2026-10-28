// ------------------------------------------------------------------------------------------------
// <copyright file="NameCandidate.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.Serializer.TextualNotation.NameResolution
{
    using System.Collections.Generic;

    using SysML2.NET.Core.POCO.Root.Elements;

    /// <summary>
    /// One candidate spelling to verify: raw segments for probing, escaped segments for emission, and
    /// the element the FIRST segment is expected to resolve to (the target itself for a single
    /// segment, an anchor namespace for a qualified form).
    /// </summary>
    internal readonly struct NameCandidate
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NameCandidate" /> struct.
        /// </summary>
        /// <param name="rawSegments">The raw segment names, outermost first.</param>
        /// <param name="escapedSegments">The escaped segment forms, outermost first.</param>
        /// <param name="anchor">The element the first segment must resolve to.</param>
        internal NameCandidate(IReadOnlyList<string> rawSegments, IReadOnlyList<string> escapedSegments, IElement anchor)
        {
            this.RawSegments = rawSegments;
            this.EscapedSegments = escapedSegments;
            this.Anchor = anchor;
        }

        /// <summary>Gets the raw segment names, outermost first.</summary>
        internal IReadOnlyList<string> RawSegments { get; }

        /// <summary>Gets the escaped segment forms, outermost first.</summary>
        private IReadOnlyList<string> EscapedSegments { get; }

        /// <summary>Gets the element the first segment must resolve to.</summary>
        internal IElement Anchor { get; }

        /// <summary>Gets the emitted text.</summary>
        internal string Text => string.Join("::", this.EscapedSegments);
    }
}
