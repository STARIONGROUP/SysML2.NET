// -------------------------------------------------------------------------------------------------
// <copyright file="IIfActionUsage.cs" company="Starion Group S.A.">
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
    /// An IfActionUsage is an ActionUsage that specifies that the thenAction ActionUsage should be
    /// performed if the result of the ifArgument Expression is true. It may also optionally specify an
    /// elseAction ActionUsage that is performed if the result of the ifArgument is false.
    /// </summary>
    [Class(xmiId: "_19_0_4_12e503d9_1624203546797_456808_3484", isAbstract: false, isFinalSpecialization: false, isActive: false)]
    [GeneratedCode("SysML2.NET", "latest")]
    public partial interface IIfActionUsage : IActionUsage
    {
        /// <summary>
        /// The ActionUsage that is to be performed if the result of the ifArgument is false. It is the
        /// (optional) third parameter of the IfActionUsage.
        /// </summary>
        [Property(xmiId: "_19_0_4_12e503d9_1624203816178_273125_3723", aggregation: AggregationKind.None, lowerValue: 0, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        Guid? ElseAction { get; }

        /// <summary>
        /// The Expression whose result determines whether the thenAction or (optionally) the elseAction is
        /// performed. It is the first parameter of the IfActionUsage.
        /// </summary>
        [Property(xmiId: "_19_0_4_12e503d9_1624203866872_328861_3821", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        Guid IfArgument { get; }

        /// <summary>
        /// The ActionUsage that is to be performed if the result of the ifArgument is true. It is the second
        /// parameter of the IfActionUsage.
        /// </summary>
        [Property(xmiId: "_19_0_4_12e503d9_1624203835062_413118_3748", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        Guid ThenAction { get; }

    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
