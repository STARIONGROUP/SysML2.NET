// ------------------------------------------------------------------------------------------------
// <copyright file="FeatureValueExtensions.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.Core.POCO.Kernel.FeatureValues
{
    using System;
    using System.Collections.Generic;

    using SysML2.NET.Decorators;

    using SysML2.NET.Core.Root.Namespaces;
    using SysML2.NET.Core.POCO.Core.Features;
    using SysML2.NET.Core.POCO.Kernel.Functions;
    using SysML2.NET.Core.POCO.Root.Annotations;
    using SysML2.NET.Core.POCO.Root.Elements;
    using SysML2.NET.Core.POCO.Root.Namespaces;
    using SysML2.NET.Extensions;

    /// <summary>
    /// The <see cref="FeatureValueExtensions"/> class provides extensions methods for
    /// the <see cref="IFeatureValue"/> interface
    /// </summary>
    internal static class FeatureValueExtensions
    {
        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <param name="featureValueSubject">
        /// The subject <see cref="IFeatureValue"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [DerivedProperty(name: nameof(IFeatureValue.featureWithValue))]
        internal static IFeature ComputeFeatureWithValue(this IFeatureValue featureValueSubject)
        {
            return featureValueSubject == null
                ? throw new ArgumentNullException(nameof(featureValueSubject))
                : featureValueSubject.OwningRelatedElement as IFeature;
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <param name="featureValueSubject">
        /// The subject <see cref="IFeatureValue"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [DerivedProperty(name: nameof(IFeatureValue.value))]
        internal static IExpression ComputeValue(this IFeatureValue featureValueSubject)
        {
            if (featureValueSubject == null)
            {
                throw new ArgumentNullException(nameof(featureValueSubject));
            }

            return featureValueSubject.OwnedRelatedElement.SingleStrict<IExpression>(nameof(featureValueSubject));
        }
    }
}
