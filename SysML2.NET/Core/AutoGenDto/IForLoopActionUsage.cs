// -------------------------------------------------------------------------------------------------
// <copyright file="IForLoopActionUsage.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.Core.DTO.Systems.Actions
{
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.Generic;

    using SysML2.NET.Core.Core.Types;
    using SysML2.NET.Core.Systems.Occurrences;
    using SysML2.NET.Decorators;

    /// <summary>
    /// A ForLoopActionUsage is a LoopActionUsage that specifies that its bodyAction ActionUsage should be
    /// performed once for each value, in order, from the sequence of values obtained as the result of the
    /// seqArgument Expression, with the loopVariable set to the value for each iteration.
    /// </summary>
    [Class(xmiId: "Systems-Actions-ForLoopActionUsage", isAbstract: false, isFinalSpecialization: false, isActive: false)]
    [GeneratedCode("SysML2.NET", "latest")]
    public partial interface IForLoopActionUsage : ILoopActionUsage
    {
        /// <summary>
        /// The ownedFeature of this <co>ForLoopActionUsage that acts as the loop variable, which is assigned
        /// the successive values of the input sequence on each iteration. It is the ownedFeature that redefines
        /// ForLoopAction::var.</co>
        /// </summary>
        [Property(xmiId: "Systems-Actions-ForLoopActionUsage-loopVariable", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        Guid loopVariable { get; }

        /// <summary>
        /// The Expression whose result provides the sequence of values to which the loopVariable is set for
        /// each iterative performance of the bodyAction. It is the Expression whose result is bound to the seq
        /// input parameter of this ForLoopActionUsage.
        /// </summary>
        [Property(xmiId: "Systems-Actions-ForLoopActionUsage-seqArgument", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        Guid seqArgument { get; }

    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
