// ------------------------------------------------------------------------------------------------
// <copyright file="ResultExpressionMembershipExtensions.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.Core.POCO.Kernel.Functions
{
    using System;

    using SysML2.NET.Decorators;

    using SysML2.NET.Extensions;

    /// <summary>
    /// The <see cref="ResultExpressionMembershipExtensions" /> class provides extensions methods for
    /// the <see cref="IResultExpressionMembership" /> interface
    /// </summary>
    internal static class ResultExpressionMembershipExtensions
    {
        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <param name="resultExpressionMembershipSubject">
        /// The subject <see cref="IResultExpressionMembership" />
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [DerivedProperty(name: nameof(IResultExpressionMembership.ownedResultExpression))]
        internal static IExpression ComputeOwnedResultExpression(this IResultExpressionMembership resultExpressionMembershipSubject)
        {
            if (resultExpressionMembershipSubject == null)
            {
                throw new ArgumentNullException(nameof(resultExpressionMembershipSubject));
            }

            return resultExpressionMembershipSubject.OwnedRelatedElement.SingleStrict<IExpression>(nameof(resultExpressionMembershipSubject));
        }
    }
}
