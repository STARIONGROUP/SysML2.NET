// ------------------------------------------------------------------------------------------------
// <copyright file="VariantMembershipExtensions.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.Core.POCO.Systems.DefinitionAndUsage
{
    using System;

    using SysML2.NET.Decorators;

    using SysML2.NET.Extensions;

    /// <summary>
    /// The <see cref="VariantMembershipExtensions" /> class provides extensions methods for
    /// the <see cref="IVariantMembership" /> interface
    /// </summary>
    internal static class VariantMembershipExtensions
    {
        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <param name="variantMembershipSubject">
        /// The subject <see cref="IVariantMembership" />
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [DerivedProperty(name: nameof(IVariantMembership.ownedVariantUsage))]
        internal static IUsage ComputeOwnedVariantUsage(this IVariantMembership variantMembershipSubject)
        {
            if (variantMembershipSubject == null)
            {
                throw new ArgumentNullException(nameof(variantMembershipSubject));
            }

            return variantMembershipSubject.OwnedRelatedElement.SingleStrict<IUsage>(nameof(variantMembershipSubject));
        }
    }
}
