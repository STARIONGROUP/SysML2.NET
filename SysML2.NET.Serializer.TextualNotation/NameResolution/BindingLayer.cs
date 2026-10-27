// ------------------------------------------------------------------------------------------------
// <copyright file="BindingLayer.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.Serializer.TextualNotation.NameResolution
{
    /// <summary>
    /// The source a Membership enters a Namespace through. KerML §8.2.3.5.3: the local resolution of a
    /// Namespace covers its owned, imported and (for a Type) inherited Memberships.
    /// </summary>
    internal enum BindingLayer
    {
        /// <summary>Bound by an owned Membership of the scope.</summary>
        Owned,

        /// <summary>Bound by an inherited Membership (including through implied Specializations).</summary>
        Inherited,

        /// <summary>Bound through an owned Import of the scope.</summary>
        Imported,
    }
}
