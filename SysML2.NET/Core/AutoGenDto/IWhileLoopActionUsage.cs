// -------------------------------------------------------------------------------------------------
// <copyright file="IWhileLoopActionUsage.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.Core.DTO.Systems.Actions
{
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.Generic;

    using SysML2.NET.Core.Core.Types;
    using SysML2.NET.Core.Systems.Occurrences;
    using SysML2.NET.Decorators;

    /// <summary>
    /// A WhileLoopActionUsage is a LoopActionUsage that specifies that the bodyAction ActionUsage should be
    /// performed repeatedly while the result of the whileArgument Expression is true or until the result of
    /// the untilArgument Expression (if provided) is true. The whileArgument Expression is evaluated before
    /// each (possible) performance of the bodyAction, and the untilArgument Expression is evaluated after
    /// each performance of the bodyAction.
    /// </summary>
    [Class(xmiId: "_19_0_4_12e503d9_1624306821108_998562_5594", isAbstract: false, isFinalSpecialization: false, isActive: false)]
    [GeneratedCode("SysML2.NET", "latest")]
    public partial interface IWhileLoopActionUsage : ILoopActionUsage
    {
        /// <summary>
        /// The Expression whose result, if false, determines that the bodyAction should continue to be
        /// performed. It is the (optional) third owned parameter of the WhileLoopActionUsage.
        /// </summary>
        [Property(xmiId: "_19_0_4_12e503d9_1624290717721_449719_4195", aggregation: AggregationKind.None, lowerValue: 0, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        Guid? untilArgument { get; }

        /// <summary>
        /// The Expression whose result, if true, determines that the bodyAction should continue to be
        /// performed. It is the first owned parameter of the WhileLoopActionUsage.
        /// </summary>
        [Property(xmiId: "_19_0_4_12e503d9_1624203871924_371126_3842", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        Guid whileArgument { get; }

    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
