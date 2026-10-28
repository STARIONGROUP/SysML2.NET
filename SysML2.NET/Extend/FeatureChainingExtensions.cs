// ------------------------------------------------------------------------------------------------
// <copyright file="FeatureChainingExtensions.cs" company="Starion Group S.A.">
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
    using SysML2.NET.Exceptions;

    /// <summary>
    /// The <see cref="FeatureChainingExtensions" /> class provides extensions methods for
    /// the <see cref="IFeatureChaining" /> interface
    /// </summary>
    internal static class FeatureChainingExtensions
    {
        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <param name="featureChainingSubject">
        /// The subject <see cref="IFeatureChaining" />
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        /// <exception cref="IncompleteModelException">
        /// Thrown when the owning related element is null or is not an <see cref="IFeature" />.
        /// </exception>
        [DerivedProperty(nameof(IFeatureChaining.featureChained))]
        internal static IFeature ComputeFeatureChained(this IFeatureChaining featureChainingSubject)
        {
            if (featureChainingSubject == null)
            {
                throw new ArgumentNullException(nameof(featureChainingSubject));
            }

            return featureChainingSubject.OwningRelatedElement as IFeature
                   ?? throw new IncompleteModelException($"{nameof(featureChainingSubject)} must have an owning related element of type {nameof(IFeature)}");
        }
    }
}
