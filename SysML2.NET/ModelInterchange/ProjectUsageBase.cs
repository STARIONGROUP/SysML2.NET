// ------------------------------------------------------------------------------------------------
// <copyright file="ProjectUsageBase.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.ModelInterchange
{
    /// <summary>
    /// Base type for representing a “project usage” entry (a used project) in an interchange project.
    /// </summary>
    /// <remarks>
    /// Each usage entry identifies a used project via an IRI and may constrain which versions of that project are acceptable.
    /// </remarks>
    public abstract class ProjectUsageBase
    {
        /// <summary>
        /// TBD from Kerml Spec
        /// </summary>
        public ProjectBase UsingProject { get; set; }
        
        /// <summary>
        /// TBD from Kerml Spec
        /// </summary>
        public ProjectBase UsedProject { get; set; }
    }
}

