// ------------------------------------------------------------------------------------------------
// <copyright file="ElementFilterMembershipExtensions.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.Core.POCO.Kernel.Packages
{
    using System;

    using SysML2.NET.Decorators;

    using SysML2.NET.Core.POCO.Kernel.Functions;
    using SysML2.NET.Extensions;

    /// <summary>
    /// The <see cref="ElementFilterMembershipExtensions" /> class provides extensions methods for
    /// the <see cref="IElementFilterMembership" /> interface
    /// </summary>
    internal static class ElementFilterMembershipExtensions
    {
        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <param name="elementFilterMembershipSubject">
        /// The subject <see cref="IElementFilterMembership" />
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [DerivedProperty(name: nameof(IElementFilterMembership.condition))]
        internal static IExpression ComputeCondition(this IElementFilterMembership elementFilterMembershipSubject)
        {
            if (elementFilterMembershipSubject == null)
            {
                throw new ArgumentNullException(nameof(elementFilterMembershipSubject));
            }

            return elementFilterMembershipSubject.OwnedRelatedElement.SingleStrict<IExpression>(nameof(elementFilterMembershipSubject));
        }
    }
}
