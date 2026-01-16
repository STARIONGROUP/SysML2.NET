// -------------------------------------------------------------------------------------------------
// <copyright file="IFunction.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.Core.POCO.Kernel.Functions
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
    using SysML2.NET.Decorators;

    /// <summary>
    /// A Function is a Behavior that has an out parameter that is identified as its result. A Function
    /// represents the performance of a calculation that produces the values of its result parameter. This
    /// calculation may be decomposed into Expressions that are steps of the Function.
    /// </summary>
    [Class(xmiId: "_18_5_3_12e503d9_1533160651697_513473_42183", isAbstract: false, isFinalSpecialization: false, isActive: false)]
    [GeneratedCode("SysML2.NET", "latest")]
    public partial interface IFunction : IBehavior
    {
        /// <summary>
        /// The Expressions that are steps in the calculation of the result of this Function.
        /// </summary>
        [Property(xmiId: "_18_5_3_12e503d9_1543948400639_301251_20841", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_b9102da_1536346067212_587255_17343")]
        List<IExpression> expression { get; }

        /// <summary>
        /// Whether this Function can be used as the function of a model-level evaluable InvocationExpression.
        /// Certain Functions from the Kernel Functions Library are considered to have isModelLevelEvaluable =
        /// true. For all other Functions it is false.<strong>Note:</strong> See the specification of the KerML
        /// concrete syntax notation for Expressions for an identification of which library Functions are
        /// model-level evaluable.
        /// </summary>
        [Property(xmiId: "_19_0_4_12e503d9_1617395221463_139517_26381", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        bool isModelLevelEvaluable { get; }

        /// <summary>
        /// The object or value that is the result of evaluating the Function.
        /// </summary>
        [Property(xmiId: "_18_5_3_12e503d9_1543948912268_88159_21323", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1533160674960_365618_43170")]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1543948010065_362066_20413")]
        IFeature result { get; }

    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
