// ------------------------------------------------------------------------------------------------
// <copyright file="TypeFeaturingExtensions.cs" company="Starion Group S.A.">
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
    /// The <see cref="TypeFeaturingExtensions" /> class provides extensions methods for
    /// the <see cref="ITypeFeaturing" /> interface
    /// </summary>
    internal static class TypeFeaturingExtensions
    {
        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <param name="typeFeaturingSubject">
        /// The subject <see cref="ITypeFeaturing" />
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [DerivedProperty(name: nameof(ITypeFeaturing.owningFeatureOfType))]
        internal static IFeature ComputeOwningFeatureOfType(this ITypeFeaturing typeFeaturingSubject)
        {
            return typeFeaturingSubject == null
                ? throw new ArgumentNullException(nameof(typeFeaturingSubject))
                : typeFeaturingSubject.OwningRelatedElement as IFeature;
        }
    }
}
