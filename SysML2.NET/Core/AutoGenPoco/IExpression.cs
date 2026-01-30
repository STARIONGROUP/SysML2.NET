// -------------------------------------------------------------------------------------------------
// <copyright file="IExpression.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.Core.POCO.Kernel.Functions
{
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.Generic;

    using SysML2.NET.Core.Core.Types;
    using SysML2.NET.Core.POCO.Core.Features;
    using SysML2.NET.Core.POCO.Core.Types;
    using SysML2.NET.Core.POCO.Kernel.Behaviors;
    using SysML2.NET.Core.POCO.Root.Annotations;
    using SysML2.NET.Core.POCO.Root.Elements;
    using SysML2.NET.Core.POCO.Root.Namespaces;
    using SysML2.NET.Decorators;

    /// <summary>
    /// An Expression is a Step that is typed by a Function. An Expression that also has a Function as its
    /// featuringType is a computational step within that Function. An Expression always has a single result
    /// parameter, which redefines the result parameter of its defining function. This allows Expressions to
    /// be interconnected in tree structures, in which inputs to each Expression in the tree are determined
    /// as the results of other Expression in the tree.
    /// </summary>
    [Class(xmiId: "Kernel-Functions-Expression", isAbstract: false, isFinalSpecialization: false, isActive: false)]
    [GeneratedCode("SysML2.NET", "latest")]
    public partial interface IExpression : IStep
    {
        /// <summary>
        /// The Function that types this Expression.
        /// </summary>
        [Property(xmiId: "Kernel-Functions-Expression-function", aggregation: AggregationKind.None, lowerValue: 0, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [RedefinedProperty(propertyName: "Kernel-Behaviors-Step-behavior")]
        IFunction function { get; }

        /// <summary>
        /// Whether this Expression meets the constraints necessary to be evaluated at model level, that is,
        /// using metadata within the model.
        /// </summary>
        [Property(xmiId: "Kernel-Functions-Expression-isModelLevelEvaluable", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        bool isModelLevelEvaluable { get; }

        /// <summary>
        /// An output parameter of the Expression whose value is the result of the Expression. The result of an
        /// Expression is either inherited from its function or it is related to the Expression via a
        /// ReturnParameterMembership, in which case it redefines the result parameter of its function.
        /// </summary>
        [Property(xmiId: "Kernel-Functions-Expression-result", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Core-Types-Type-output")]
        [SubsettedProperty(propertyName: "Kernel-Behaviors-Step-parameter")]
        IFeature result { get; }

    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
