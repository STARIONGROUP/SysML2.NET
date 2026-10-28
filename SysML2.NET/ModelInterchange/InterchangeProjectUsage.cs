// ------------------------------------------------------------------------------------------------
// <copyright file="InterchangeProjectUsage.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.ModelInterchange
{
    using System;

    /// <summary>
    /// Describes a used project entry in an <see cref="InterchangeProject"/> usage list.
    /// </summary>
    public class InterchangeProjectUsage : ProjectUsageBase
    {
        /// <summary>
        /// Gets or sets the IRI identifying the project being used.
        /// </summary>
        /// <remarks>
        /// Mandatory (within a usage).
        /// <para/>
        /// If the IRI is dereferenceable, it SHOULD resolve to a project interchange file
        /// for the used project.
        /// </remarks>
        public Uri Resource { get; set; } = new Uri("about:blank");
        
        /// <summary>
        /// A constraint on the allowable versions of a used project.
        /// </summary>
        public string VersionConstraint { get; set; }
    }
}
