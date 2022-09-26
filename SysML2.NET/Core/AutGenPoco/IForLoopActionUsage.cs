// -------------------------------------------------------------------------------------------------
// <copyright file="IForLoopActionUsage.cs" company="RHEA System S.A.">
//
//   Copyright 2022 RHEA System S.A.
//
//   Licensed under the Apache License, Version 2.0 (the "License");
//   you may not use this file except in compliance with the License.
//   You may obtain a copy of the License at
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

namespace SysML2.NET.Core.POCO
{
    using System;
    using System.Collections.Generic;

    using SysML2.NET.Core;

    /// <summary>
    /// A ForLoopActionUsage is a LoopActionUsage that is typed, directly or indirectly, by the
    /// ActionDefinition ForLoopAction from the Systems model library. It specifies that the bodyClause
    /// ActionUsage should be performed once for each value, in order, from the sequence of values obtained
    /// as the result of the seqArgument Expression. The bodyAction must have one input parameter, which
    /// receives a value from the sequence on each iteration.
    /// </summary>
    public partial interface IForLoopActionUsage : ILoopActionUsage
    {
        /// <summary>
        /// Queries the derived property LoopVariable
        /// </summary>
        ReferenceUsage QueryLoopVariable();

        /// <summary>
        /// Queries the derived property SeqArgument
        /// </summary>
        Expression QuerySeqArgument();

    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
