// -------------------------------------------------------------------------------------------------
// <copyright file="RequirementDefinitionExtensions.cs" company="Starion Group S.A.">
//
//    Copyright (C) 2022-2026 Starion Group S.A.
//
//    Licensed under the Apache License, Version 2.0 (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
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
    using System.Collections.Generic;
    using System.Linq;

    using SysML2.NET.Core.Systems.Requirements;
    using SysML2.NET.Core.POCO.Root.Annotations;
    using SysML2.NET.Core.POCO.Systems.Constraints;
    using SysML2.NET.Core.POCO.Systems.DefinitionAndUsage;
    using SysML2.NET.Core.POCO.Systems.Parts;

    /// <summary>
    /// The <see cref="RequirementDefinitionExtensions"/> class provides extensions methods for
    /// the <see cref="IRequirementDefinition"/> interface
    /// </summary>
    internal static class RequirementDefinitionExtensions
    {
        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// actorParameter = featureMembership-&gt;
        ///                             selectByKind(ActorMembership).
        ///                             ownedActorParameter
        /// </code>
        /// </remarks>
        /// <param name="requirementDefinitionSubject">
        /// The subject <see cref="IRequirementDefinition"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        internal static List<IPartUsage> ComputeActorParameter(this IRequirementDefinition requirementDefinitionSubject)
        {
            return requirementDefinitionSubject == null
                ? throw new ArgumentNullException(nameof(requirementDefinitionSubject))
                : [..requirementDefinitionSubject.featureMembership.OfType<IActorMembership>().Select(actorMembership => actorMembership.ownedActorParameter)];
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// assumedConstraint = ownedFeatureMembership-&gt;
        ///                             selectByKind(RequirementConstraintMembership)-&gt;
        ///                             select(kind = RequirementConstraintKind::assumption).
        ///                             ownedConstraint
        /// </code>
        /// </remarks>
        /// <param name="requirementDefinitionSubject">
        /// The subject <see cref="IRequirementDefinition"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        internal static List<IConstraintUsage> ComputeAssumedConstraint(this IRequirementDefinition requirementDefinitionSubject)
        {
            return requirementDefinitionSubject == null
                ? throw new ArgumentNullException(nameof(requirementDefinitionSubject))
                : [
                    ..requirementDefinitionSubject.ownedFeatureMembership
                      .OfType<IRequirementConstraintMembership>()
                      .Where(requirementConstraintMembership => requirementConstraintMembership.Kind == RequirementConstraintKind.Assumption)
                      .Select(requirementConstraintMembership => requirementConstraintMembership.ownedConstraint)
                ];
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// framedConcern = featureMembership-&gt;
        ///                             selectByKind(FramedConcernMembership).
        ///                             ownedConcern
        /// </code>
        /// </remarks>
        /// <param name="requirementDefinitionSubject">
        /// The subject <see cref="IRequirementDefinition"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        internal static List<IConcernUsage> ComputeFramedConcern(this IRequirementDefinition requirementDefinitionSubject)
        {
            return requirementDefinitionSubject == null
                ? throw new ArgumentNullException(nameof(requirementDefinitionSubject))
                : [..requirementDefinitionSubject.featureMembership.OfType<IFramedConcernMembership>().Select(framedConcernMembership => framedConcernMembership.ownedConcern)];
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// requiredConstraint = ownedFeatureMembership-&gt;
        ///                             selectByKind(RequirementConstraintMembership)-&gt;
        ///                             select(kind = RequirementConstraintKind::requirement).
        ///                             ownedConstraint
        /// </code>
        /// </remarks>
        /// <param name="requirementDefinitionSubject">
        /// The subject <see cref="IRequirementDefinition"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        internal static List<IConstraintUsage> ComputeRequiredConstraint(this IRequirementDefinition requirementDefinitionSubject)
        {
            return requirementDefinitionSubject == null
                ? throw new ArgumentNullException(nameof(requirementDefinitionSubject))
                : [
                    ..requirementDefinitionSubject.ownedFeatureMembership
                      .OfType<IRequirementConstraintMembership>()
                      .Where(requirementConstraintMembership => requirementConstraintMembership.Kind == RequirementConstraintKind.Requirement)
                      .Select(requirementConstraintMembership => requirementConstraintMembership.ownedConstraint)
                ];
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// stakeholderParameter = featureMembership-&gt;
        ///                             selectByKind(StakholderMembership).
        ///                             ownedStakeholderParameter
        /// </code>
        /// </remarks>
        /// <param name="requirementDefinitionSubject">
        /// The subject <see cref="IRequirementDefinition"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        internal static List<IPartUsage> ComputeStakeholderParameter(this IRequirementDefinition requirementDefinitionSubject)
        {
            // The OCL uses "StakholderMembership" which is a typo in the XMI source; the correct C# type is IStakeholderMembership.
            return requirementDefinitionSubject == null
                ? throw new ArgumentNullException(nameof(requirementDefinitionSubject))
                : [..requirementDefinitionSubject.featureMembership.OfType<IStakeholderMembership>().Select(stakeholderMembership => stakeholderMembership.ownedStakeholderParameter)];
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// subjectParameter =
        ///                             let subjects : OrderedSet(SubjectMembership) =
        ///                             featureMembership-&gt;selectByKind(SubjectMembership) in
        ///                             if subjects-&gt;isEmpty() then null
        ///                             else subjects-&gt;first().ownedSubjectParameter
        ///                             endif
        /// </code>
        /// </remarks>
        /// <param name="requirementDefinitionSubject">
        /// The subject <see cref="IRequirementDefinition"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        internal static IUsage ComputeSubjectParameter(this IRequirementDefinition requirementDefinitionSubject)
        {
            if (requirementDefinitionSubject == null)
            {
                throw new ArgumentNullException(nameof(requirementDefinitionSubject));
            }

            var subjects = requirementDefinitionSubject.featureMembership.OfType<ISubjectMembership>().ToList();

            return subjects.Count == 0
                ? null
                : subjects[0].ownedSubjectParameter;
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// text = documentation.body
        /// </code>
        /// </remarks>
        /// <param name="requirementDefinitionSubject">
        /// The subject <see cref="IRequirementDefinition"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        internal static List<string> ComputeText(this IRequirementDefinition requirementDefinitionSubject)
        {
            return requirementDefinitionSubject == null
                ? throw new ArgumentNullException(nameof(requirementDefinitionSubject))
                : [..requirementDefinitionSubject.documentation.Select(documentation => documentation.Body)];
        }
    }
}
