// ------------------------------------------------------------------------------------------------
// <copyright file="XmiWriterOptions.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.Serializer.Xmi.Writers
{
    /// <summary>
    /// The <see cref="XmiWriterOptions"/> provides centralized option provided to <see cref="XmiDataWriter{TData}"/> to customize output format
    /// </summary>
    public class XmiWriterOptions
    {
        /// <summary>
        /// Asserts if derived properties have to be included or not.
        /// </summary>
        public bool IncludeDerivedProperties { get; set; }

        /// <summary>
        /// Asserts if implied properties have to be included or not.
        /// The project-level includesImplied flag as defined in KerML Clause 10, Note 5.
        /// When <c>true</c>, all implied relationships are serialized and every element's isImpliedIncluded
        /// attribute is written as "true". When <c>false</c>, implied relationships (where
        /// <see cref="SysML2.NET.Core.POCO.Root.Elements.IRelationship.IsImplied"/> is true) are excluded
        /// and no element's isImpliedIncluded attribute is written.
        /// </summary>
        public bool IncludeImplied { get; set; }

        /// <summary>
        /// Asserts if Id Reference values have to be written as attribute, when applicable.
        /// Default value is <c>true</c>
        /// </summary>
        public bool WriteIdRefAsAttribute { get; set; } = true;
    }
}
