// ------------------------------------------------------------------------------------------------
// <copyright file="VerificationCaseDefinitionExtensions.cs" company="Starion Group S.A.">
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

    using SysML2.NET.Core.POCO.Systems.Requirements;
    using SysML2.NET.Decorators;

    /// <summary>
    /// The <see cref="VerificationCaseDefinitionExtensions" /> class provides extensions methods for
    /// the <see cref="IVerificationCaseDefinition" /> interface
    /// </summary>
    internal static class VerificationCaseDefinitionExtensions
    {
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
        /// <param name="verificationCaseDefinitionSubject">
        /// The subject <see cref="IVerificationCaseDefinition" />
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [DerivedProperty(nameof(IVerificationCaseDefinition.verifiedRequirement))]
        internal static List<IRequirementUsage> ComputeVerifiedRequirement(this IVerificationCaseDefinition verificationCaseDefinitionSubject)
        {
            if (verificationCaseDefinitionSubject == null)
            {
                throw new ArgumentNullException(nameof(verificationCaseDefinitionSubject));
            }

            var objective = verificationCaseDefinitionSubject.objectiveRequirement;

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
