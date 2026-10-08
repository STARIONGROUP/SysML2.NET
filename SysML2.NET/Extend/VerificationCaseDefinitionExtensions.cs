// -------------------------------------------------------------------------------------------------
// <copyright file="VerificationCaseDefinitionExtensions.cs" company="Starion Group S.A.">
// 
//   Copyright (C) 2022-2026 Starion Group S.A.
// 
//   Licensed under the Apache License, Version 2.0 (the "License");
//   you may not use this file except in compliance with the License.
//   You may obtain a copy of the License at
// 
//        http://www.apache.org/licenses/LICENSE-2.0
// 
//    Unless required by applicable law or agreed to in writing, software
//    distributed under the License is distributed on an "AS IS" BASIS,
//    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//    See the License for the specific language governing permissions and
//    limitations under the License.
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
