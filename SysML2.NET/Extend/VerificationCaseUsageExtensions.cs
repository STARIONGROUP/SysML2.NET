// ------------------------------------------------------------------------------------------------
// <copyright file="VerificationCaseUsageExtensions.cs" company="Starion Group S.A.">
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
    using System.Linq;

    using SysML2.NET.Core.POCO.Core.Features;
    using SysML2.NET.Core.POCO.Systems.Requirements;
    using SysML2.NET.Decorators;
    using SysML2.NET.Exceptions;
    using SysML2.NET.Extensions;

    /// <summary>
    /// The <see cref="VerificationCaseUsageExtensions" /> class provides extensions methods for
    /// the <see cref="IVerificationCaseUsage" /> interface
    /// </summary>
    internal static class VerificationCaseUsageExtensions
    {
        /// <summary>
        /// Computes the derived <c>verificationCaseDefinition</c> property: the
        /// <see cref="IVerificationCaseDefinition" /> targeted by the single
        /// <see cref="IFeatureTyping" /> owned by <paramref name="verificationCaseUsageSubject" />.
        /// </summary>
        /// <param name="verificationCaseUsageSubject">
        /// The subject <see cref="IVerificationCaseUsage" />
        /// </param>
        /// <returns>
        /// The matching <see cref="IVerificationCaseDefinition" />, or <c>null</c> when no such typing exists.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="verificationCaseUsageSubject" /> is <c>null</c>.
        /// </exception>
        /// <exception cref="MultiplicityViolationException">
        /// Thrown when more than one <see cref="IFeatureTyping" /> targets an
        /// <see cref="IVerificationCaseDefinition" /> (upper-bound violation against the derived
        /// <c>[0..1]</c> property).
        /// </exception>
        [DerivedProperty(nameof(IVerificationCaseUsage.verificationCaseDefinition))]
        internal static IVerificationCaseDefinition ComputeVerificationCaseDefinition(this IVerificationCaseUsage verificationCaseUsageSubject)
        {
            return verificationCaseUsageSubject == null
                ? throw new ArgumentNullException(nameof(verificationCaseUsageSubject))
                : verificationCaseUsageSubject.ComputeType().SingleOrDefaultStrict<IVerificationCaseDefinition>(nameof(verificationCaseUsageSubject));
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// verifiedRequirement =
        ///                             if objectiveRequirement = null then OrderedSet{}
        ///                             else
        ///                             objectiveRequirement.featureMembership-&gt;
        ///                             selectByKind(RequirementVerificationMembership).
        ///                             verifiedRequirement-&gt;asOrderedSet()
        ///                             endif
        /// </code>
        /// </remarks>
        /// <param name="verificationCaseUsageSubject">
        /// The subject <see cref="IVerificationCaseUsage" />
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [DerivedProperty(nameof(IVerificationCaseUsage.verifiedRequirement))]
        internal static List<IRequirementUsage> ComputeVerifiedRequirement(this IVerificationCaseUsage verificationCaseUsageSubject)
        {
            if (verificationCaseUsageSubject == null)
            {
                throw new ArgumentNullException(nameof(verificationCaseUsageSubject));
            }

            var objective = verificationCaseUsageSubject.objectiveRequirement;

            return objective == null
                ? []
                :
                [
                    ..objective.featureMembership
                        .OfType<IRequirementVerificationMembership>()
                        .Select(requirementVerificationMembership => requirementVerificationMembership.verifiedRequirement)
                        .Distinct()
                ];
        }
    }
}
