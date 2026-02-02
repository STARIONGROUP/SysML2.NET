// -------------------------------------------------------------------------------------------------
// <copyright file="IInstantiationExpression.cs" company="Starion Group S.A.">
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
    using SysML2.NET.Core.DTO.Kernel.Functions;
    using SysML2.NET.Decorators;

    /// <summary>
    /// An InstantiationExpression is an Expression that instantiates its instantiatedType, binding some or
    /// all of the features of that Type to the results of its arguments.                       
    /// InstantiationExpression is abstract, with concrete subclasses InvocationExpression and
    /// ConstructorExpression.
    /// </summary>
    [Class(xmiId: "_2022x_2_12e503d9_1739136879941_579104_183", isAbstract: true, isFinalSpecialization: false, isActive: false)]
    [GeneratedCode("SysML2.NET", "latest")]
    public partial interface IInstantiationExpression : IExpression
    {
        /// <summary>
        /// The Expressions whose results are bound to features of the instantiatedType. The arguments are
        /// ordered consistent with the order of the features, though they may not be one-to-one with all the
        /// features.                            <strong>Note.</strong> The derivation of argument is given in
        /// the concrete subclasses of InstantiationExpression.
        /// </summary>
        [Property(xmiId: "_2022x_2_12e503d9_1739134437590_328753_108", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        List<Guid> argument { get; }

        /// <summary>
        /// The Type that is being instantiated.
        /// </summary>
        [Property(xmiId: "_2022x_2_12e503d9_1739134352572_416088_80", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1533160674979_644335_43267")]
        Guid instantiatedType { get; }

    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
