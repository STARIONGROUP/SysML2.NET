// ------------------------------------------------------------------------------------------------
// <copyright file="FeatureMembershipExtensions.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.Core.POCO.Core.Types
{
    using System;
    using System.Collections.Generic;

    using SysML2.NET.Decorators;

    using SysML2.NET.Core.Root.Namespaces;
    using SysML2.NET.Core.POCO.Core.Features;
    using SysML2.NET.Core.POCO.Root.Annotations;
    using SysML2.NET.Core.POCO.Root.Elements;
    using SysML2.NET.Core.POCO.Root.Namespaces;
    using SysML2.NET.Extensions;

    /// <summary>
    /// The <see cref="FeatureMembershipExtensions"/> class provides extensions methods for
    /// the <see cref="IFeatureMembership"/> interface
    /// </summary>
    internal static class FeatureMembershipExtensions
    {
        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <param name="featureMembershipSubject">
        /// The subject <see cref="IFeatureMembership"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [DerivedProperty(name: nameof(IFeatureMembership.ownedMemberFeature))]
        internal static IFeature ComputeOwnedMemberFeature(this IFeatureMembership featureMembershipSubject)
        {
            if (featureMembershipSubject == null)
            {
                throw new ArgumentNullException(nameof(featureMembershipSubject));
            }

            return featureMembershipSubject.OwnedRelatedElement.SingleStrict<IFeature>(nameof(featureMembershipSubject));
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <param name="featureMembershipSubject">
        /// The subject <see cref="IFeatureMembership"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [DerivedProperty(name: nameof(IFeatureMembership.owningType))]
        internal static IType ComputeOwningType(this IFeatureMembership featureMembershipSubject)
        {
            return featureMembershipSubject == null
                ? throw new ArgumentNullException(nameof(featureMembershipSubject))
                : featureMembershipSubject.OwningRelatedElement as IType;
        }
    }
}
