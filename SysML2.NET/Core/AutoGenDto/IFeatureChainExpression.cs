// -------------------------------------------------------------------------------------------------
// <copyright file="IFeatureChainExpression.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.Core.DTO.Kernel.Expressions
{
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.Generic;

    using SysML2.NET.Core.Core.Types;
    using SysML2.NET.Decorators;

    /// <summary>
    /// A FeatureChainExpression is an OperatorExpression whose operator is ".", which resolves to the
    /// Function ControlFunctions::'.' from the Kernel Functions Library. It evaluates to the result of
    /// chaining the result Feature of its single argument Expression with its targetFeature.
    /// </summary>
    [Class(xmiId: "Kernel-Expressions-FeatureChainExpression", isAbstract: false, isFinalSpecialization: false, isActive: false)]
    [GeneratedCode("SysML2.NET", "latest")]
    public partial interface IFeatureChainExpression : IOperatorExpression
    {
        /// <summary>
        /// </summary>
        [Property(xmiId: "Kernel-Expressions-FeatureChainExpression-operator", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: ".")]
        [RedefinedProperty(propertyName: "Kernel-Expressions-OperatorExpression-operator")]
        new string Operator { get; set; }

        /// <summary>
        /// The Feature that is accessed by this FeatureChainExpression, which is its first non-parameter
        /// member.
        /// </summary>
        [Property(xmiId: "Kernel-Expressions-FeatureChainExpression-targetFeature", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Root-Namespaces-Namespace-member")]
        Guid targetFeature { get; }

    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
