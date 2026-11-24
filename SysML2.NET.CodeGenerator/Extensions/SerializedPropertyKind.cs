// ------------------------------------------------------------------------------------------------
// <copyright file="SerializedPropertyKind.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.CodeGenerator.Extensions
{
    /// <summary>
    /// The role a property plays when an instance of a class is exchanged by a serializer.
    /// </summary>
    public enum SerializedPropertyKind
    {
        /// <summary>
        /// A non-derived property that the class does not redefine; every format exchanges it.
        /// </summary>
        Stored,

        /// <summary>
        /// A non-derived property that the class redefines under another name with a non-derived property; its value is the one of the redefining property.
        /// </summary>
        StoredAlias,

        /// <summary>
        /// A derived property that the class does not redefine.
        /// </summary>
        Derived,

        /// <summary>
        /// A non-derived property that the class redefines under another name with a derived property, which makes it derived (KerML §10.3 Note 4).
        /// </summary>
        DerivedAlias,

        /// <summary>
        /// A derived property that the class redefines under another name; it repeats the value of the redefining property and cannot be assigned.
        /// </summary>
        RedefinedDerived
    }
}
