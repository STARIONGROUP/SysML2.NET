// -------------------------------------------------------------------------------------------------
// <copyright file="RequirementConstraintMembershipExtensions.cs" company="Starion Group S.A.">
// 
//   Copyright 2022-2026 Starion Group S.A.
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

namespace SysML2.NET.Core.POCO.Systems.Requirements
{
    using System;

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
