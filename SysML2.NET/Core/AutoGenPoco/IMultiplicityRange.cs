// -------------------------------------------------------------------------------------------------
// <copyright file="IMultiplicityRange.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.Core.POCO.Kernel.Multiplicities
{
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.Generic;

    using SysML2.NET.Core.Core.Types;
    using SysML2.NET.Core.POCO.Core.Features;
    using SysML2.NET.Core.POCO.Core.Types;
    using SysML2.NET.Core.POCO.Kernel.Functions;
    using SysML2.NET.Core.POCO.Root.Annotations;
    using SysML2.NET.Core.POCO.Root.Elements;
    using SysML2.NET.Core.POCO.Root.Namespaces;
    using SysML2.NET.Decorators;

    /// <summary>
    /// A MultiplicityRange is a Multiplicity whose value is defined to be the (inclusive) range of natural
    /// numbers given by the result of a lowerBound Expression and the result of an upperBound Expression.
    /// The result of these Expressions shall be of type Natural. If the result of the upperBound Expression
    /// is the unbounded value *, then the specified range includes all natural numbers greater than or
    /// equal to the lowerBound value. If no lowerBound Expression, then the default is that the lower bound
    /// has the same value as the upper bound, except if the upperBound evaluates to *, in which case the
    /// default for the lower bound is 0.
    /// </summary>
    [Class(xmiId: "_19_0_2_12e503d9_1573086225407_540120_4572", isAbstract: false, isFinalSpecialization: false, isActive: false)]
    [GeneratedCode("SysML2.NET", "latest")]
    public partial interface IMultiplicityRange : IMultiplicity
    {
        /// <summary>
        /// The owned Expressions of the MultiplicityRange whose results provide its bounds. These must be the
        /// first ownedMembers of the MultiplicityRange.
        /// </summary>
        [Property(xmiId: "_19_0_2_12e503d9_1573095221994_519580_5095", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 2, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1533160674979_259543_43268")]
        List<IExpression> QueryBound();

        /// <summary>
        /// The Expression whose result provides the lower bound of the MultiplicityRange. If no lowerBound
        /// Expression is given, then the lower bound shall have the same value as the upper bound, unless the
        /// upper bound is unbounded (*), in which case the lower bound shall be 0.
        /// </summary>
        [Property(xmiId: "_19_0_2_12e503d9_1573094905677_801324_4744", aggregation: AggregationKind.None, lowerValue: 0, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_19_0_2_12e503d9_1573095221994_519580_5095")]
        IExpression QueryLowerBound();

        /// <summary>
        /// The Expression whose result is the upper bound of the MultiplicityRange.
        /// </summary>
        [Property(xmiId: "_19_0_2_12e503d9_1573094947427_797440_4796", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_19_0_2_12e503d9_1573095221994_519580_5095")]
        IExpression QueryUpperBound();

    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
