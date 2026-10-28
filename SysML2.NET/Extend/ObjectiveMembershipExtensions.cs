// ------------------------------------------------------------------------------------------------
// <copyright file="ObjectiveMembershipExtensions.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.Core.POCO.Systems.Cases
{
    using System;

    using SysML2.NET.Decorators;

    using SysML2.NET.Core.POCO.Systems.Requirements;
    using SysML2.NET.Extensions;

    /// <summary>
    /// The <see cref="ObjectiveMembershipExtensions" /> class provides extensions methods for
    /// the <see cref="IObjectiveMembership" /> interface
    /// </summary>
    internal static class ObjectiveMembershipExtensions
    {
        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <param name="objectiveMembershipSubject">
        /// The subject <see cref="IObjectiveMembership" />
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [DerivedProperty(name: nameof(IObjectiveMembership.ownedObjectiveRequirement))]
        internal static IRequirementUsage ComputeOwnedObjectiveRequirement(this IObjectiveMembership objectiveMembershipSubject)
        {
            if (objectiveMembershipSubject == null)
            {
                throw new ArgumentNullException(nameof(objectiveMembershipSubject));
            }

            return objectiveMembershipSubject.OwnedRelatedElement.SingleStrict<IRequirementUsage>(nameof(objectiveMembershipSubject));
        }
    }
}
