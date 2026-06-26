// -------------------------------------------------------------------------------------------------
// <copyright file="CaseUsageExtensions.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.Core.POCO.Systems.Cases
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using SysML2.NET.Core.Core.Types;
    using SysML2.NET.Core.Root.Namespaces;
    using SysML2.NET.Core.Systems.Occurrences;
    using SysML2.NET.Core.POCO.Core.Classifiers;
    using SysML2.NET.Core.POCO.Core.Features;
    using SysML2.NET.Core.POCO.Core.Types;
    using SysML2.NET.Core.POCO.Kernel.Behaviors;
    using SysML2.NET.Core.POCO.Kernel.Classes;
    using SysML2.NET.Core.POCO.Kernel.Functions;
    using SysML2.NET.Core.POCO.Root.Annotations;
    using SysML2.NET.Core.POCO.Root.Elements;
    using SysML2.NET.Core.POCO.Root.Namespaces;
    using SysML2.NET.Core.POCO.Systems.Actions;
    using SysML2.NET.Core.POCO.Systems.Allocations;
    using SysML2.NET.Core.POCO.Systems.AnalysisCases;
    using SysML2.NET.Core.POCO.Systems.Attributes;
    using SysML2.NET.Core.POCO.Systems.Calculations;
    using SysML2.NET.Core.POCO.Systems.Connections;
    using SysML2.NET.Core.POCO.Systems.Constraints;
    using SysML2.NET.Core.POCO.Systems.DefinitionAndUsage;
    using SysML2.NET.Core.POCO.Systems.Enumerations;
    using SysML2.NET.Core.POCO.Systems.Flows;
    using SysML2.NET.Core.POCO.Systems.Interfaces;
    using SysML2.NET.Core.POCO.Systems.Items;
    using SysML2.NET.Core.POCO.Systems.Metadata;
    using SysML2.NET.Core.POCO.Systems.Occurrences;
    using SysML2.NET.Core.POCO.Systems.Parts;
    using SysML2.NET.Core.POCO.Systems.Ports;
    using SysML2.NET.Core.POCO.Systems.Requirements;
    using SysML2.NET.Core.POCO.Systems.States;
    using SysML2.NET.Core.POCO.Systems.UseCases;
    using SysML2.NET.Core.POCO.Systems.VerificationCases;
    using SysML2.NET.Core.POCO.Systems.Views;
    using SysML2.NET.Exceptions;
    using SysML2.NET.Extensions;

    /// <summary>
    /// The <see cref="CaseUsageExtensions"/> class provides extensions methods for
    /// the <see cref="ICaseUsage"/> interface
    /// </summary>
    internal static class CaseUsageExtensions
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
        /// <param name="caseUsageSubject">
        /// The subject <see cref="ICaseUsage"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        internal static List<IPartUsage> ComputeActorParameter(this ICaseUsage caseUsageSubject)
        {
            return caseUsageSubject == null
                ? throw new ArgumentNullException(nameof(caseUsageSubject))
                : [..caseUsageSubject.featureMembership.OfType<IActorMembership>().Select(actorMembership => actorMembership.ownedActorParameter)];
        }

        /// <summary>
        /// Computes the derived <c>caseDefinition</c> property: the <see cref="ICaseDefinition"/>
        /// targeted by the single <see cref="IFeatureTyping"/> owned by
        /// <paramref name="caseUsageSubject"/>.
        /// </summary>
        /// <param name="caseUsageSubject">
        /// The subject <see cref="ICaseUsage"/>
        /// </param>
        /// <returns>
        /// The matching <see cref="ICaseDefinition"/>, or <c>null</c> when no such typing exists.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="caseUsageSubject"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="MultiplicityViolationException">
        /// Thrown when more than one <see cref="IFeatureTyping"/> targets an
        /// <see cref="ICaseDefinition"/> (upper-bound violation against the derived
        /// <c>[0..1]</c> property).
        /// </exception>
        internal static ICaseDefinition ComputeCaseDefinition(this ICaseUsage caseUsageSubject)
        {
            if (caseUsageSubject is null)
            {
                throw new ArgumentNullException(nameof(caseUsageSubject));
            }

            return caseUsageSubject.type.SingleOrDefaultStrict<ICaseDefinition>(nameof(caseUsageSubject));
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// objectiveRequirement =
        ///                             let objectives: OrderedSet(RequirementUsage) =
        ///                             featureMembership-&gt;
        ///                             selectByKind(ObjectiveMembership).
        ///                             ownedRequirement in
        ///                             if objectives-&gt;isEmpty() then null
        ///                             else objectives-&gt;first().ownedObjectiveRequirement
        ///                             endif
        /// </code>
        /// </remarks>
        /// <param name="caseUsageSubject">
        /// The subject <see cref="ICaseUsage"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        internal static IRequirementUsage ComputeObjectiveRequirement(this ICaseUsage caseUsageSubject)
        {
            if (caseUsageSubject == null)
            {
                throw new ArgumentNullException(nameof(caseUsageSubject));
            }

            return caseUsageSubject.featureMembership
                .OfType<IObjectiveMembership>()
                .FirstOrDefault()
                ?.ownedObjectiveRequirement;
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
        /// <param name="caseUsageSubject">
        /// The subject <see cref="ICaseUsage"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        internal static IUsage ComputeSubjectParameter(this ICaseUsage caseUsageSubject)
        {
            if (caseUsageSubject == null)
            {
                throw new ArgumentNullException(nameof(caseUsageSubject));
            }

            var subjects = caseUsageSubject.featureMembership.OfType<ISubjectMembership>().ToList();

            return subjects.Count == 0
                ? null
                : subjects[0].ownedSubjectParameter;
        }

    }
}
