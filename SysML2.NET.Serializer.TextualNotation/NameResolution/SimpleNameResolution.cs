// ------------------------------------------------------------------------------------------------
// <copyright file="SimpleNameResolution.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.Serializer.TextualNotation.NameResolution
{
    /// <summary>
    /// Tri-state result of probing one scope for one lexical form.
    /// </summary>
    internal enum SimpleNameResolution
    {
        /// <summary>Name not bound in this scope — keep walking outward.</summary>
        NotBound,

        /// <summary>Name bound uniquely to the target — emit the simple name.</summary>
        Matched,

        /// <summary>Name bound to something else — stop; the qualified form is required.</summary>
        Shadowed
    }
}
