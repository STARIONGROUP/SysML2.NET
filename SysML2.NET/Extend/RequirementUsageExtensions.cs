// -------------------------------------------------------------------------------------------------
// <copyright file="RequirementUsageExtensions.cs" company="Starion Group S.A.">
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
    using SysML2.NET.Core.POCO.Core.Features;
    using SysML2.NET.Core.POCO.Core.Types;
    using SysML2.NET.Core.POCO.Root.Annotations;
    using SysML2.NET.Core.POCO.Systems.Constraints;
    using SysML2.NET.Core.POCO.Systems.DefinitionAndUsage;
    using SysML2.NET.Core.POCO.Systems.Parts;
    using SysML2.NET.Exceptions;
    using SysML2.NET.Extensions;

    /// <summary>
    /// The <see cref="RequirementUsageExtensions"/> class provides extensions methods for
    /// the <see cref="IRequirementUsage"/> interface
    /// </summary>
    internal static class RequirementUsageExtensions
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
        /// <param name="requirementUsageSubject">
        /// The subject <see cref="IRequirementUsage"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        internal static List<IPartUsage> ComputeActorParameter(this IRequirementUsage requirementUsageSubject)
        {
            return requirementUsageSubject == null
                ? throw new ArgumentNullException(nameof(requirementUsageSubject))
                : [..requirementUsageSubject.featureMembership.OfType<IActorMembership>().Select(actorMembership => actorMembership.ownedActorParameter)];
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
        /// <param name="requirementUsageSubject">
        /// The subject <see cref="IRequirementUsage"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        internal static List<IConstraintUsage> ComputeAssumedConstraint(this IRequirementUsage requirementUsageSubject)
        {
            return requirementUsageSubject == null
                ? throw new ArgumentNullException(nameof(requirementUsageSubject))
                : [..requirementUsageSubject.ownedFeatureMembership
                      .OfType<IRequirementConstraintMembership>()
                      .Where(requirementConstraintMembership => requirementConstraintMembership.Kind == RequirementConstraintKind.Assumption)
                      .Select(requirementConstraintMembership => requirementConstraintMembership.ownedConstraint)];
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
        /// <param name="requirementUsageSubject">
        /// The subject <see cref="IRequirementUsage"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        internal static List<IConcernUsage> ComputeFramedConcern(this IRequirementUsage requirementUsageSubject)
        {
            return requirementUsageSubject == null
                ? throw new ArgumentNullException(nameof(requirementUsageSubject))
                : [..requirementUsageSubject.featureMembership.OfType<IFramedConcernMembership>().Select(framedConcernMembership => framedConcernMembership.ownedConcern)];
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
        /// <param name="requirementUsageSubject">
        /// The subject <see cref="IRequirementUsage"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        internal static List<IConstraintUsage> ComputeRequiredConstraint(this IRequirementUsage requirementUsageSubject)
        {
            return requirementUsageSubject == null
                ? throw new ArgumentNullException(nameof(requirementUsageSubject))
                : [..requirementUsageSubject.ownedFeatureMembership
                      .OfType<IRequirementConstraintMembership>()
                      .Where(requirementConstraintMembership => requirementConstraintMembership.Kind == RequirementConstraintKind.Requirement)
                      .Select(requirementConstraintMembership => requirementConstraintMembership.ownedConstraint)];
        }

        /// <summary>
        /// Computes the derived <c>requirementDefinition</c> property: the
        /// <see cref="IRequirementDefinition"/> targeted by the single <see cref="IFeatureTyping"/>
        /// owned by <paramref name="requirementUsageSubject"/>.
        /// </summary>
        /// <param name="requirementUsageSubject">
        /// The subject <see cref="IRequirementUsage"/>
        /// </param>
        /// <returns>
        /// The matching <see cref="IRequirementDefinition"/>, or <c>null</c> when no such typing exists.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="requirementUsageSubject"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="MultiplicityViolationException">
        /// Thrown when more than one <see cref="IFeatureTyping"/> targets an
        /// <see cref="IRequirementDefinition"/> (upper-bound violation against the derived
        /// <c>[0..1]</c> property).
        /// </exception>
        internal static IRequirementDefinition ComputeRequirementDefinition(this IRequirementUsage requirementUsageSubject)
        {
            if (requirementUsageSubject is null)
            {
                throw new ArgumentNullException(nameof(requirementUsageSubject));
            }

            return requirementUsageSubject.definition.SingleOrDefaultStrict<IRequirementDefinition>(nameof(requirementUsageSubject));
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// stakeholderParameter = featureMembership-&gt;
        ///                             selectByKind(AStakholderMembership).
        ///                             ownedStakeholderParameter
        /// </code>
        /// </remarks>
        /// <param name="requirementUsageSubject">
        /// The subject <see cref="IRequirementUsage"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        internal static List<IPartUsage> ComputeStakeholderParameter(this IRequirementUsage requirementUsageSubject)
        {
            // The OCL uses "AStakholderMembership" which is a typo in the XMI source; the correct C# type is IStakeholderMembership.
            return requirementUsageSubject == null
                ? throw new ArgumentNullException(nameof(requirementUsageSubject))
                : [..requirementUsageSubject.featureMembership.OfType<IStakeholderMembership>().Select(stakeholderMembership => stakeholderMembership.ownedStakeholderParameter)];
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
        /// <param name="requirementUsageSubject">
        /// The subject <see cref="IRequirementUsage"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        internal static IUsage ComputeSubjectParameter(this IRequirementUsage requirementUsageSubject)
        {
            if (requirementUsageSubject == null)
            {
                throw new ArgumentNullException(nameof(requirementUsageSubject));
            }

            var subjects = requirementUsageSubject.featureMembership.OfType<ISubjectMembership>().ToList();

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
        /// <param name="requirementUsageSubject">
        /// The subject <see cref="IRequirementUsage"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        internal static List<string> ComputeText(this IRequirementUsage requirementUsageSubject)
        {
            return requirementUsageSubject == null
                ? throw new ArgumentNullException(nameof(requirementUsageSubject))
                : [..requirementUsageSubject.documentation.Select(documentation => documentation.Body)];
        }

    }
}
