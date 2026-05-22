// -------------------------------------------------------------------------------------------------
// <copyright file="StateUsageExtensions.cs" company="Starion Group S.A.">
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

    using SysML2.NET.Core.POCO.Core.Features;
    using SysML2.NET.Core.POCO.Kernel.Behaviors;
    using SysML2.NET.Core.POCO.Systems.Actions;
    using SysML2.NET.Core.Systems.States;

    /// <summary>
    /// The <see cref="StateUsageExtensions"/> class provides extensions methods for
    /// the <see cref="IStateUsage"/> interface
    /// </summary>
    internal static class StateUsageExtensions
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
        /// <param name="stateUsageSubject">
        /// The subject <see cref="IStateUsage"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        internal static IActionUsage ComputeDoAction(this IStateUsage stateUsageSubject)
        {
            if (stateUsageSubject == null)
            {
                throw new ArgumentNullException(nameof(stateUsageSubject));
            }

            return stateUsageSubject.ownedMembership
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
        /// <param name="stateUsageSubject">
        /// The subject <see cref="IStateUsage"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        internal static IActionUsage ComputeEntryAction(this IStateUsage stateUsageSubject)
        {
            if (stateUsageSubject == null)
            {
                throw new ArgumentNullException(nameof(stateUsageSubject));
            }

            return stateUsageSubject.ownedMembership
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
        /// <param name="stateUsageSubject">
        /// The subject <see cref="IStateUsage"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        internal static IActionUsage ComputeExitAction(this IStateUsage stateUsageSubject)
        {
            if (stateUsageSubject == null)
            {
                throw new ArgumentNullException(nameof(stateUsageSubject));
            }

            return stateUsageSubject.ownedMembership
                .OfType<IStateSubactionMembership>()
                .FirstOrDefault(membership => membership.Kind == StateSubactionKind.Exit)
                ?.action;
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <param name="stateUsageSubject">
        /// The subject <see cref="IStateUsage"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        internal static List<IBehavior> ComputeStateDefinition(this IStateUsage stateUsageSubject)
        {
            return stateUsageSubject == null
                ? throw new ArgumentNullException(nameof(stateUsageSubject))
                : [
                    .. stateUsageSubject.OwnedRelationship.OfType<IFeatureTyping>()
                    .Select(featureTyping => featureTyping.Type)
                    .OfType<IBehavior>()
                ];
        }

        /// <summary>
        /// Check if this StateUsage is composite and has an owningType that is a StateDefinition or StateUsage
        /// with the given value of isParallel, but is not an entryAction, doAction, or exitAction. If so, then
        /// it represents a StateAction that is a substate or exclusiveState (for isParallel = false) of another
        /// StateAction.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// isComposite and owningType &lt;&gt; null and
        ///                                 (owningType.oclIsKindOf(StateDefinition) and
        ///                                 owningType.oclAsType(StateDefinition).isParallel = isParallel or
        ///                                 owningType.oclIsKindOf(StateUsage) and
        ///                                 owningType.oclAsType(StateUsage).isParallel = isParallel) and
        ///                                 not owningFeatureMembership.oclIsKindOf(StateSubactionMembership)
        /// </code>
        /// </remarks>
        /// <param name="stateUsageSubject">
        /// The subject <see cref="IStateUsage"/>
        /// </param>
        /// <param name="isParallel">
        /// No documentation provided
        /// </param>
        /// <returns>
        /// The expected <see cref="bool" />
        /// </returns>
        internal static bool ComputeIsSubstateUsageOperation(this IStateUsage stateUsageSubject, bool isParallel)
        {
            if (stateUsageSubject == null)
            {
                throw new ArgumentNullException(nameof(stateUsageSubject));
            }

            return stateUsageSubject.IsComposite
                && stateUsageSubject.owningType != null
                && (stateUsageSubject.owningType is IStateDefinition stateDefinition && stateDefinition.IsParallel == isParallel
                    || stateUsageSubject.owningType is IStateUsage owningStateUsage && owningStateUsage.IsParallel == isParallel)
                && stateUsageSubject.owningFeatureMembership is not IStateSubactionMembership;
        }
    }
}
