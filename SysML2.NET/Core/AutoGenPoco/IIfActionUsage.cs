// -------------------------------------------------------------------------------------------------
// <copyright file="IIfActionUsage.cs" company="RHEA System S.A.">
//
//   Copyright 2022-2023 RHEA System S.A.
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
    /// An IfActionUsage is an ActionUsage that is typed, directly or indirectly, by the ActionDefinition
    /// IfThenAction from the Systems model library, or, more specifically, by IfThenElseAction, if it has
    /// an elseAction. It specifies that the thenAction ActionUsage should be performed if the result of the
    /// ifArgument Expression is true. It may also optionally specify a elseAction ActionUsage that is
    /// performed if the result of the ifArgument is false.
    /// </summary>
    public partial interface IIfActionUsage : IActionUsage
    {
        /// <summary>
        /// Queries the derived property ElseAction
        /// </summary>
        ActionUsage QueryElseAction();

        /// <summary>
        /// Queries the derived property IfArgument
        /// </summary>
        Expression QueryIfArgument();

        /// <summary>
        /// Queries the derived property ThenAction
        /// </summary>
        ActionUsage QueryThenAction();

    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
