// -------------------------------------------------------------------------------------------------
// <copyright file="IStateDefinition.cs" company="Starion Group S.A.">
//
//    Copyright (C) 2022-2025 Starion Group S.A.
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

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.Core.POCO.Systems.States
{
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.Generic;

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
    using SysML2.NET.Decorators;

    /// <summary>
    /// A StateDefinition is the Definition of the Behavior of a system or part of a system in a certain
    /// state condition.A StateDefinition may be related to up to three of its ownedFeatures by
    /// StateBehaviorMembership Relationships, all of different kinds, corresponding to the entry, do and
    /// exit actions of the StateDefinition.
    /// </summary>
    [Class(xmiId: "_19_0_2_12e503d9_1575587534200_898246_600", isAbstract: false, isFinalSpecialization: false, isActive: false)]
    [GeneratedCode("SysML2.NET", "latest")]
    public partial interface IStateDefinition : IActionDefinition
    {
        /// <summary>
        /// The ActionUsage of this StateDefinition to be performed while in the state defined by the
        /// StateDefinition. It is the owned ActionUsage related to the StateDefinition by a
        /// StateSubactionMembership  with kind = do.
        /// </summary>
        [Property(xmiId: "_19_0_2_12e503d9_1582975916386_388324_339", aggregation: AggregationKind.None, lowerValue: 0, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        IActionUsage QueryDoAction();

        /// <summary>
        /// The ActionUsage of this StateDefinition to be performed on entry to the state defined by the
        /// StateDefinition. It is the owned ActionUsage related to the StateDefinition by a
        /// StateSubactionMembership  with kind = entry.
        /// </summary>
        [Property(xmiId: "_19_0_2_12e503d9_1582975902339_513804_312", aggregation: AggregationKind.None, lowerValue: 0, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        IActionUsage QueryEntryAction();

        /// <summary>
        /// The ActionUsage of this StateDefinition to be performed on exit to the state defined by the
        /// StateDefinition. It is the owned ActionUsage related to the StateDefinition by a
        /// StateSubactionMembership  with kind = exit.
        /// </summary>
        [Property(xmiId: "_19_0_2_12e503d9_1582975927011_696894_352", aggregation: AggregationKind.None, lowerValue: 0, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        IActionUsage QueryExitAction();

        /// <summary>
        /// Whether the ownedStates of this StateDefinition are to all be performed in parallel. If true, none
        /// of the ownedActions (which includes ownedStates) may have any incoming or outgoing Transitions. If
        /// false, only one ownedState may be performed at a time.
        /// </summary>
        [Property(xmiId: "_19_0_4_12e503d9_1624025670323_266174_37704", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: "false")]
        bool IsParallel { get; set; }

        /// <summary>
        /// The StateUsages, which are actions in the StateDefinition, that specify the discrete states in the
        /// behavior defined by the StateDefinition.
        /// </summary>
        [Property(xmiId: "_19_0_2_12e503d9_1575588190693_949879_1156", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1565500809065_170841_30688")]
        List<IStateUsage> QueryState();

    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
