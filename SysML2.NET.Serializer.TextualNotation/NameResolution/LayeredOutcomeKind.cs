// ------------------------------------------------------------------------------------------------
// <copyright file="LayeredOutcomeKind.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.Serializer.TextualNotation.NameResolution
{
    /// <summary>
    /// The outcome kind of probing one name in one scope.
    /// </summary>
    internal enum LayeredOutcomeKind
    {
        /// <summary>The scope does not bind the name; the outward walk may continue.</summary>
        NotBound,

        /// <summary>The scope binds the name to exactly one election winner.</summary>
        Bound,

        /// <summary>The scope binds the name to several peers; a reader cannot pick one.</summary>
        Ambiguous
    }
}
