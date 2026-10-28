// ------------------------------------------------------------------------------------------------
// <copyright file="ReferenceSubsettingExtensions.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.Core.POCO.Core.Features
{
    using System;
    using System.Collections.Generic;

    using SysML2.NET.Decorators;

    using SysML2.NET.Core.POCO.Core.Types;
    using SysML2.NET.Core.POCO.Root.Annotations;
    using SysML2.NET.Core.POCO.Root.Elements;
    using SysML2.NET.Core.POCO.Root.Namespaces;

    /// <summary>
    /// The <see cref="ReferenceSubsettingExtensions"/> class provides extensions methods for
    /// the <see cref="IReferenceSubsetting"/> interface
    /// </summary>
    internal static class ReferenceSubsettingExtensions
    {
        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <param name="referenceSubsettingSubject">
        /// The subject <see cref="IReferenceSubsetting"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        [DerivedProperty(name: nameof(IReferenceSubsetting.referencingFeature))]
        internal static IFeature ComputeReferencingFeature(this IReferenceSubsetting referenceSubsettingSubject)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }

    }
}
