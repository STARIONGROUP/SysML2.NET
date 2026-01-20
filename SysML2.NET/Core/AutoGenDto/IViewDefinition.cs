// -------------------------------------------------------------------------------------------------
// <copyright file="IViewDefinition.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.Core.DTO.Systems.Views
{
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.Generic;

    using SysML2.NET.Core.DTO.Systems.Parts;
    using SysML2.NET.Decorators;

    /// <summary>
    /// A ViewDefinition is a PartDefinition that specifies how a view artifact is constructed to satisfy a
    /// viewpoint. It specifies a viewConditions to define the model content to be presented and a
    /// viewRendering to define how the model content is presented.
    /// </summary>
    [Class(xmiId: "_19_0_2_59601fc_1583087286915_926479_556", isAbstract: false, isFinalSpecialization: false, isActive: false)]
    [GeneratedCode("SysML2.NET", "latest")]
    public partial interface IViewDefinition : IPartDefinition
    {
        /// <summary>
        /// The composite ownedRequirements of this ViewDefinition that are ViewpointUsages for viewpoints
        /// satisfied by the ViewDefinition.
        /// </summary>
        [Property(xmiId: "_19_0_2_12e503d9_1596645596183_374903_1209", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_19_0_2_12e503d9_1583000559760_444344_1273")]
        List<Guid> satisfiedViewpoint { get; }

        /// <summary>
        /// The usages of this ViewDefinition that are ViewUsages.
        /// </summary>
        [Property(xmiId: "_19_0_2_12e503d9_1596644452170_21813_753", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1565498571495_48981_27786")]
        List<Guid> view { get; }

        /// <summary>
        /// The Expressions related to this ViewDefinition by ElementFilterMemberships, which specify conditions
        /// on Elements to be rendered in a view.
        /// </summary>
        [Property(xmiId: "_19_0_4_12e503d9_1606938929077_183245_4796", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1533160674979_259543_43268")]
        List<Guid> viewCondition { get; }

        /// <summary>
        /// The RenderingUsage to be used to render views defined by this ViewDefinition, which is the
        /// referencedRendering of the ViewRenderingMembership of the ViewDefinition.
        /// </summary>
        [Property(xmiId: "_19_0_2_12e503d9_1596657187664_758418_4914", aggregation: AggregationKind.None, lowerValue: 0, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        Guid? viewRendering { get; }

    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
