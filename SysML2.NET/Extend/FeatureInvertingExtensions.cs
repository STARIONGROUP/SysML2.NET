// ------------------------------------------------------------------------------------------------
// <copyright file="FeatureInvertingExtensions.cs" company="Starion Group S.A.">
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
    /// The <see cref="FeatureInvertingExtensions" /> class provides extensions methods for
    /// the <see cref="IFeatureInverting" /> interface
    /// </summary>
    internal static class FeatureInvertingExtensions
    {
        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <param name="featureInvertingSubject">
        /// The subject <see cref="IFeatureInverting" />
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [DerivedProperty(nameof(IFeatureInverting.owningFeature))]
        internal static IFeature ComputeOwningFeature(this IFeatureInverting featureInvertingSubject)
        {
            if (featureInvertingSubject == null)
            {
                throw new ArgumentNullException(nameof(featureInvertingSubject));
            }

            var owningRelatedElement = featureInvertingSubject.OwningRelatedElement;

            return owningRelatedElement is IFeature owningFeature
                   && ReferenceEquals(owningRelatedElement, featureInvertingSubject.FeatureInverted)
                ? owningFeature
                : null;
        }
    }
}
