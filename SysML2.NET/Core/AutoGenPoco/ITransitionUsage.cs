// -------------------------------------------------------------------------------------------------
// <copyright file="ITransitionUsage.cs" company="Starion Group S.A.">
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

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.Core.POCO.Systems.States
{
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.Generic;

    using SysML2.NET.Core.Core.Types;
    using SysML2.NET.Core.Systems.Occurrences;
    using SysML2.NET.Core.POCO.Core.Classifiers;
    using SysML2.NET.Core.POCO.Core.Features;
    using SysML2.NET.Core.POCO.Core.Types;
    using SysML2.NET.Core.POCO.Kernel.Behaviors;
    using SysML2.NET.Core.POCO.Kernel.Classes;
    using SysML2.NET.Core.POCO.Kernel.Connectors;
    using SysML2.NET.Core.POCO.Kernel.Functions;
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
    /// A TransitionUsage is an ActionUsage representing a triggered transition between ActionUsages or
    /// StateUsages. When triggered by a triggerAction, when its guardExpression is true, the
    /// TransitionUsage asserts that its source is exited, then its effectAction (if any) is performed, and
    /// then its target is entered.  A TransitionUsage can be related to some of its ownedFeatures using
    /// TransitionFeatureMembership Relationships, corresponding to the triggerAction, guardExpression and
    /// effectAction of the TransitionUsage.
    /// </summary>
    [Class(xmiId: "Systems-States-TransitionUsage", isAbstract: false, isFinalSpecialization: false, isActive: false)]
    [GeneratedCode("SysML2.NET", "latest")]
    public partial interface ITransitionUsage : IActionUsage
    {
        /// <summary>
        /// The ActionUsages that define the effects of this TransitionUsage, which are the ownedFeatures of the
        /// TransitionUsage related to it by TransitionFeatureMemberships with kind = effect, which must all be
        /// ActionUsages.
        /// </summary>
        [Property(xmiId: "Systems-States-TransitionUsage-effectAction", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Core-Types-Type-feature")]
        List<IActionUsage> effectAction { get; }

        /// <summary>
        /// The Expressions that define the guards of this TransitionUsage, which are the ownedFeatures of the
        /// TransitionUsage related to it by TransitionFeatureMemberships with kind = guard, which must all be
        /// Expressions.
        /// </summary>
        [Property(xmiId: "Systems-States-TransitionUsage-guardExpression", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Core-Types-Type-ownedFeature")]
        List<IExpression> guardExpression { get; }

        /// <summary>
        /// The source ActionUsage of this TransitionUsage, which becomes the source of the succession for the
        /// TransitionUsage.
        /// </summary>
        [Property(xmiId: "Systems-States-TransitionUsage-source", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        IActionUsage source { get; }

        /// <summary>
        /// The Succession that is the ownedFeature of this TransitionUsage, which, if the TransitionUsage is
        /// triggered, asserts the temporal ordering of the source and target.
        /// </summary>
        [Property(xmiId: "Systems-States-TransitionUsage-succession", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Root-Namespaces-Namespace-ownedMember")]
        ISuccession succession { get; }

        /// <summary>
        /// The target ActionUsage of this TransitionUsage, which is the targetFeature of the succession for the
        /// TransitionUsage.
        /// </summary>
        [Property(xmiId: "Systems-States-TransitionUsage-target", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        IActionUsage target { get; }

        /// <summary>
        /// The AcceptActionUsages that define the triggers of this TransitionUsage, which are the ownedFeatures
        /// of the TransitionUsage related to it by TransitionFeatureMemberships with kind = trigger, which must
        /// all be AcceptActionUsages.
        /// </summary>
        [Property(xmiId: "Systems-States-TransitionUsage-triggerAction", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Core-Types-Type-ownedFeature")]
        List<IAcceptActionUsage> triggerAction { get; }

    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
