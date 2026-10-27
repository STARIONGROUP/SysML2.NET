// ------------------------------------------------------------------------------------------------
// <copyright file="RequirementVerificationMembershipExtensions.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.Core.POCO.Systems.VerificationCases
{
    using System;
    using System.Collections.Generic;

    using SysML2.NET.Decorators;

    using SysML2.NET.Core.Root.Namespaces;
    using SysML2.NET.Core.Systems.Requirements;
    using SysML2.NET.Core.POCO.Core.Features;
    using SysML2.NET.Core.POCO.Core.Types;
    using SysML2.NET.Core.POCO.Root.Annotations;
    using SysML2.NET.Core.POCO.Root.Elements;
    using SysML2.NET.Core.POCO.Root.Namespaces;
    using SysML2.NET.Core.POCO.Systems.Constraints;
    using SysML2.NET.Core.POCO.Systems.Requirements;
    using SysML2.NET.Extensions;

    /// <summary>
    /// The <see cref="RequirementVerificationMembershipExtensions"/> class provides extensions methods for
    /// the <see cref="IRequirementVerificationMembership"/> interface
    /// </summary>
    internal static class RequirementVerificationMembershipExtensions
    {
        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <param name="requirementVerificationMembershipSubject">
        /// The subject <see cref="IRequirementVerificationMembership"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [DerivedProperty(name: nameof(IRequirementVerificationMembership.ownedRequirement))]
        internal static IRequirementUsage ComputeOwnedRequirement(this IRequirementVerificationMembership requirementVerificationMembershipSubject)
        {
            if (requirementVerificationMembershipSubject == null)
            {
                throw new ArgumentNullException(nameof(requirementVerificationMembershipSubject));
            }

            return requirementVerificationMembershipSubject.OwnedRelatedElement.SingleStrict<IRequirementUsage>(nameof(requirementVerificationMembershipSubject));
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <param name="requirementVerificationMembershipSubject">
        /// The subject <see cref="IRequirementVerificationMembership"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [DerivedProperty(name: nameof(IRequirementVerificationMembership.verifiedRequirement))]
        internal static IRequirementUsage ComputeVerifiedRequirement(this IRequirementVerificationMembership requirementVerificationMembershipSubject)
        {
            if (requirementVerificationMembershipSubject == null)
            {
                throw new ArgumentNullException(nameof(requirementVerificationMembershipSubject));
            }

            var ownedRequirement = requirementVerificationMembershipSubject.ownedRequirement;

            var referencedFeature = ownedRequirement?.ReferencedFeatureTarget();

            if (referencedFeature == null)
            {
                return ownedRequirement;
            }

            return referencedFeature as IRequirementUsage;
        }

    }
}
