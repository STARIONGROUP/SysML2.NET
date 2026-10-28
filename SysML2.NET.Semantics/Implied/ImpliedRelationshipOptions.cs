// ------------------------------------------------------------------------------------------------
// <copyright file="ImpliedRelationshipOptions.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.Semantics.Implied
{
    /// <summary>
    /// Configures which families of semantic constraint the provider computes.
    /// </summary>
    public class ImpliedRelationshipOptions
    {
        /// <summary>
        /// Gets or sets a value indicating whether the library-specialization constraints are computed.
        /// </summary>
        /// <remarks>
        /// Off by default. These constraints attach the bulk of the Kernel Semantic Library to a model and
        /// change what inheritance yields for almost every Type, so enabling them re-baselines any output
        /// derived from inheritance. Turn on deliberately, in a change of its own.
        /// </remarks>
        public bool EnableLibrarySpecializations { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether redundant implied Specializations are dropped per
        /// KerML 8.4.2.
        /// </summary>
        /// <remarks>
        /// On by default. Switching it off is a diagnostic aid for seeing every constraint a Type triggers,
        /// not a supported production configuration.
        /// </remarks>
        public bool ReduceRedundantSpecializations { get; set; } = true;
    }
}
