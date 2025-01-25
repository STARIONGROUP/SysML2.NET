// -------------------------------------------------------------------------------------------------
// <copyright file="ITerminateActionUsage.cs" company="Starion Group S.A.">
//
//   Copyright 2022-2025 Starion Group S.A.
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

namespace SysML2.NET.Core.DTO
{
    using System;
    using System.Collections.Generic;

    using SysML2.NET.Core;
    using SysML2.NET.Decorators;

    /// <summary>
    /// A TerminateActionUsage is an ActionUsage that directly or indirectly specializes the
    /// ActionDefinition TerminateAction from the Systems Model Library, which causes a given
    /// terminatedOccurrence to end during its performance. By default, the terminatedOccurrence is the
    /// featuring instance (that) of the performance of the TerminateActionUsage, generally the performance
    /// of its immediately containing ActionDefinition or
    /// ActionUsage.specializesFromLibrary('Actions::terminateActions')terminatedOccurrenceArgument =
    /// argument(1)isSubactionUsage() implies   
    /// specializesFromLibrary('Actions::Action::terminateSubactions')
    /// </summary>
    public partial interface ITerminateActionUsage : IActionUsage
    {
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
