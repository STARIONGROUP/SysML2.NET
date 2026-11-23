// ------------------------------------------------------------------------------------------------
// <copyright file="CrossSubsettingExtensions.cs" company="Starion Group S.A.">
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
    /// The <see cref="CrossSubsettingExtensions" /> class provides extensions methods for
    /// the <see cref="ICrossSubsetting" /> interface
    /// </summary>
    internal static class CrossSubsettingExtensions
    {
        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <param name="crossSubsettingSubject">
        /// The subject <see cref="ICrossSubsetting" />
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [DerivedProperty(nameof(ICrossSubsetting.crossingFeature))]
        internal static IFeature ComputeCrossingFeature(this ICrossSubsetting crossSubsettingSubject)
        {
            if (crossSubsettingSubject == null)
            {
                throw new ArgumentNullException(nameof(crossSubsettingSubject));
            }

            return crossSubsettingSubject.OwningRelatedElement as IFeature;
        }
    }
}
