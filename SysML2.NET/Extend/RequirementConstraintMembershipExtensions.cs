// ------------------------------------------------------------------------------------------------
// <copyright file="RequirementConstraintMembershipExtensions.cs" company="Starion Group S.A.">
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

    using SysML2.NET.Core.POCO.Systems.Constraints;
    using SysML2.NET.Extensions;

    /// <summary>
    /// The <see cref="RequirementConstraintMembershipExtensions" /> class provides extensions methods for
    /// the <see cref="IRequirementConstraintMembership" /> interface
    /// </summary>
    internal static class RequirementConstraintMembershipExtensions
    {
        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <param name="requirementConstraintMembershipSubject">
        /// The subject <see cref="IRequirementConstraintMembership" />
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [DerivedProperty(name: nameof(IRequirementConstraintMembership.ownedConstraint))]
        internal static IConstraintUsage ComputeOwnedConstraint(this IRequirementConstraintMembership requirementConstraintMembershipSubject)
        {
            if (requirementConstraintMembershipSubject == null)
            {
                throw new ArgumentNullException(nameof(requirementConstraintMembershipSubject));
            }

            return requirementConstraintMembershipSubject.OwnedRelatedElement.SingleStrict<IConstraintUsage>(nameof(requirementConstraintMembershipSubject));
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// referencedConstraint =
        ///                             let referencedFeature : Feature =
        ///                             ownedConstraint.referencedFeatureTarget() in
        ///                             if referencedFeature = null then ownedConstraint
        ///                             else if referencedFeature.oclIsKindOf(ConstraintUsage) then
        ///                             refrencedFeature.oclAsType(ConstraintUsage)
        ///                             else null
        ///                             endif endif
        /// </code>
        /// </remarks>
        /// <param name="requirementConstraintMembershipSubject">
        /// The subject <see cref="IRequirementConstraintMembership" />
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [DerivedProperty(name: nameof(IRequirementConstraintMembership.referencedConstraint))]
        internal static IConstraintUsage ComputeReferencedConstraint(this IRequirementConstraintMembership requirementConstraintMembershipSubject)
        {
            if (requirementConstraintMembershipSubject == null)
            {
                throw new ArgumentNullException(nameof(requirementConstraintMembershipSubject));
            }

            var ownedConstraint = requirementConstraintMembershipSubject.ownedConstraint;
            var referencedFeature = ownedConstraint?.ReferencedFeatureTarget();

            if (referencedFeature == null)
            {
                return ownedConstraint;
            }

            return referencedFeature as IConstraintUsage;
        }
    }
}
