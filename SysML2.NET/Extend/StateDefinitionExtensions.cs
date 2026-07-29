// -------------------------------------------------------------------------------------------------
// <copyright file="StateDefinitionExtensions.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.Core.POCO.Systems.States
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using SysML2.NET.Core.Core.Types;
    using SysML2.NET.Core.Root.Namespaces;
    using SysML2.NET.Core.POCO.Core.Classifiers;
    using SysML2.NET.Core.POCO.Core.Features;
    using SysML2.NET.Core.POCO.Core.Types;
    using SysML2.NET.Core.POCO.Kernel.Behaviors;
    using SysML2.NET.Core.POCO.Root.Annotations;
    using SysML2.NET.Core.POCO.Root.Elements;
    using SysML2.NET.Core.POCO.Root.Namespaces;
    using SysML2.NET.Core.POCO.Systems.Actions;
    using SysML2.NET.Core.POCO.Systems.Allocations;
    using SysML2.NET.Core.POCO.Systems.AnalysisCases;
    using SysML2.NET.Core.POCO.Systems.Attributes;
    using SysML2.NET.Core.POCO.Systems.Calculations;
    using SysML2.NET.Core.POCO.Systems.Cases;
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
    using SysML2.NET.Core.POCO.Systems.UseCases;
    using SysML2.NET.Core.POCO.Systems.VerificationCases;
    using SysML2.NET.Core.POCO.Systems.Views;
    using SysML2.NET.Core.Systems.States;

    /// <summary>
    /// The <see cref="StateDefinitionExtensions"/> class provides extensions methods for
    /// the <see cref="IStateDefinition"/> interface
    /// </summary>
    internal static class StateDefinitionExtensions
    {
        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// doAction =
        ///                             let doMemberships : Sequence(StateSubactionMembership) =
        ///                             ownedMembership-&gt;
        ///                             selectByKind(StateSubactionMembership)-&gt;
        ///                             select(kind = StateSubactionKind::do) in
        ///                             if doMemberships-&gt;isEmpty() then null
        ///                             else doMemberships-&gt;at(1)
        ///                             endif
        /// </code>
        /// </remarks>
        /// <param name="stateDefinitionSubject">
        /// The subject <see cref="IStateDefinition"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        internal static IActionUsage ComputeDoAction(this IStateDefinition stateDefinitionSubject)
        {
            if (stateDefinitionSubject == null)
            {
                throw new ArgumentNullException(nameof(stateDefinitionSubject));
            }

            return stateDefinitionSubject.ownedMembership
                .OfType<IStateSubactionMembership>()
                .FirstOrDefault(membership => membership.Kind == StateSubactionKind.Do)
                ?.action;
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// entryAction =
        ///                             let entryMemberships : Sequence(StateSubactionMembership) =
        ///                             ownedMembership-&gt;
        ///                             selectByKind(StateSubactionMembership)-&gt;
        ///                             select(kind = StateSubactionKind::entry) in
        ///                             if entryMemberships-&gt;isEmpty() then null
        ///                             else entryMemberships-&gt;at(1)
        ///                             endif
        /// </code>
        /// </remarks>
        /// <param name="stateDefinitionSubject">
        /// The subject <see cref="IStateDefinition"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        internal static IActionUsage ComputeEntryAction(this IStateDefinition stateDefinitionSubject)
        {
            if (stateDefinitionSubject == null)
            {
                throw new ArgumentNullException(nameof(stateDefinitionSubject));
            }

            return stateDefinitionSubject.ownedMembership
                .OfType<IStateSubactionMembership>()
                .FirstOrDefault(membership => membership.Kind == StateSubactionKind.Entry)
                ?.action;
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// exitAction =
        ///                             let exitMemberships : Sequence(StateSubactionMembership) =
        ///                             ownedMembership-&gt;
        ///                             selectByKind(StateSubactionMembership)-&gt;
        ///                             select(kind = StateSubactionKind::exit) in
        ///                             if exitMemberships-&gt;isEmpty() then null
        ///                             else exitMemberships-&gt;at(1)
        ///                             endif
        /// </code>
        /// </remarks>
        /// <param name="stateDefinitionSubject">
        /// The subject <see cref="IStateDefinition"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        internal static IActionUsage ComputeExitAction(this IStateDefinition stateDefinitionSubject)
        {
            if (stateDefinitionSubject == null)
            {
                throw new ArgumentNullException(nameof(stateDefinitionSubject));
            }

            return stateDefinitionSubject.ownedMembership
                .OfType<IStateSubactionMembership>()
                .FirstOrDefault(membership => membership.Kind == StateSubactionKind.Exit)
                ?.action;
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// state = action-&gt;selectByKind(StateUsage)
        /// </code>
        /// </remarks>
        /// <param name="stateDefinitionSubject">
        /// The subject <see cref="IStateDefinition"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        internal static List<IStateUsage> ComputeState(this IStateDefinition stateDefinitionSubject)
        {
            return stateDefinitionSubject == null
                ? throw new ArgumentNullException(nameof(stateDefinitionSubject))
                : [.. stateDefinitionSubject.action.OfType<IStateUsage>()];
        }

    }
}
