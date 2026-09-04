// -------------------------------------------------------------------------------------------------
// <copyright file="NameCandidate.cs" company="Starion Group S.A.">
// 
//   Copyright (C) 2022-2026 Starion Group S.A.
// 
//   Licensed under the Apache License, Version 2.0 (the "License");
//   you may not use this file except in compliance with the License.
//   You may obtain a copy of the License at
// 
//        http://www.apache.org/licenses/LICENSE-2.0
// 
//    Unless required by applicable law or agreed to in writing, software
//    distributed under the License is distributed on an "AS IS" BASIS,
//    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//    See the License for the specific language governing permissions and
//    limitations under the License.
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
