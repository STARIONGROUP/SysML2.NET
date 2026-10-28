// ------------------------------------------------------------------------------------------------
// <copyright file="FeatureTypingExtensions.cs" company="Starion Group S.A.">
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
    /// The <see cref="FeatureTypingExtensions" /> class provides extensions methods for
    /// the <see cref="IFeatureTyping" /> interface
    /// </summary>
    internal static class FeatureTypingExtensions
    {
        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <param name="featureTypingSubject">
        /// The subject <see cref="IFeatureTyping" />
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [DerivedProperty(nameof(IFeatureTyping.owningFeature))]
        internal static IFeature ComputeOwningFeature(this IFeatureTyping featureTypingSubject)
        {
            if (featureTypingSubject == null)
            {
                throw new ArgumentNullException(nameof(featureTypingSubject));
            }

            var owningRelatedElement = featureTypingSubject.OwningRelatedElement;

            return owningRelatedElement is IFeature owningFeature
                   && ReferenceEquals(owningRelatedElement, featureTypingSubject.TypedFeature)
                ? owningFeature
                : null;
        }
    }
}
