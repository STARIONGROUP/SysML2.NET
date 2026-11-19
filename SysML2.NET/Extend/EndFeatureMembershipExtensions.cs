// ------------------------------------------------------------------------------------------------
// <copyright file="EndFeatureMembershipExtensions.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.Core.POCO.Core.Features
{
    using System;
    using System.Linq;

    using SysML2.NET.Decorators;


    /// <summary>
    /// The <see cref="EndFeatureMembershipExtensions"/> class provides extensions methods for
    /// the <see cref="IEndFeatureMembership"/> interface
    /// </summary>
    internal static class EndFeatureMembershipExtensions
    {
        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <param name="endFeatureMembershipSubject">
        /// The subject <see cref="IEndFeatureMembership"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [DerivedProperty(name: nameof(IEndFeatureMembership.ownedMemberFeature))]
        internal static IFeature ComputeOwnedMemberFeature(this IEndFeatureMembership endFeatureMembershipSubject)
        {
            if (endFeatureMembershipSubject == null)
            {
                throw new ArgumentNullException(nameof(endFeatureMembershipSubject));
            }

            return endFeatureMembershipSubject.OwnedRelatedElement.OfType<IFeature>().FirstOrDefault();
        }
    }
}
