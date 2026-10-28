// ------------------------------------------------------------------------------------------------
// <copyright file="SubsettingExtensions.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.Core.POCO.Core.Features
{
    using System;

    using SysML2.NET.Decorators;

    /// <summary>
    /// The <see cref="SubsettingExtensions" /> class provides extensions methods for
    /// the <see cref="ISubsetting" /> interface
    /// </summary>
    internal static class SubsettingExtensions
    {
        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <param name="subsettingSubject">
        /// The subject <see cref="ISubsetting" />
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [DerivedProperty(name: nameof(ISubsetting.owningFeature))]
        internal static IFeature ComputeOwningFeature(this ISubsetting subsettingSubject)
        {
            return subsettingSubject == null
                ? throw new ArgumentNullException(nameof(subsettingSubject))
                : subsettingSubject.OwningRelatedElement as IFeature;
        }
    }
}
