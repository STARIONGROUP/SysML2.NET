// ------------------------------------------------------------------------------------------------
// <copyright file="StakeholderMembershipExtensions.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.Core.POCO.Systems.Requirements
{
    using System;

    using SysML2.NET.Decorators;

    using SysML2.NET.Core.POCO.Systems.Parts;
    using SysML2.NET.Extensions;

    /// <summary>
    /// The <see cref="StakeholderMembershipExtensions" /> class provides extensions methods for
    /// the <see cref="IStakeholderMembership" /> interface
    /// </summary>
    internal static class StakeholderMembershipExtensions
    {
        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <param name="stakeholderMembershipSubject">
        /// The subject <see cref="IStakeholderMembership" />
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [DerivedProperty(name: nameof(IStakeholderMembership.ownedStakeholderParameter))]
        internal static IPartUsage ComputeOwnedStakeholderParameter(this IStakeholderMembership stakeholderMembershipSubject)
        {
            if (stakeholderMembershipSubject == null)
            {
                throw new ArgumentNullException(nameof(stakeholderMembershipSubject));
            }

            return stakeholderMembershipSubject.OwnedRelatedElement.SingleStrict<IPartUsage>(nameof(stakeholderMembershipSubject));
        }
    }
}
