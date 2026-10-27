// ------------------------------------------------------------------------------------------------
// <copyright file="StateSubactionMembershipExtensions.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.Core.POCO.Systems.States
{
    using System;

    using SysML2.NET.Decorators;

    using SysML2.NET.Core.POCO.Systems.Actions;
    using SysML2.NET.Extensions;

    /// <summary>
    /// The <see cref="StateSubactionMembershipExtensions" /> class provides extensions methods for
    /// the <see cref="IStateSubactionMembership" /> interface
    /// </summary>
    internal static class StateSubactionMembershipExtensions
    {
        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <param name="stateSubactionMembershipSubject">
        /// The subject <see cref="IStateSubactionMembership" />
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [DerivedProperty(name: nameof(IStateSubactionMembership.action))]
        internal static IActionUsage ComputeAction(this IStateSubactionMembership stateSubactionMembershipSubject)
        {
            if (stateSubactionMembershipSubject == null)
            {
                throw new ArgumentNullException(nameof(stateSubactionMembershipSubject));
            }

            return stateSubactionMembershipSubject.OwnedRelatedElement.SingleStrict<IActionUsage>(nameof(stateSubactionMembershipSubject));
        }
    }
}
