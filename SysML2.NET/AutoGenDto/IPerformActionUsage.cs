// -------------------------------------------------------------------------------------------------
// <copyright file="IPerformActionUsage.cs" company="RHEA System S.A.">
//
// Copyright 2022 RHEA System S.A.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.DTO
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// A PerformActionUsage is an ActionUsage that represents the performance of an ActionUsage. The
    /// ActionUsage to be performed (which may be the PerformActionUsage itself) is related to the
    /// PerformActionUsage by a Subsetting relationship.If the PerformActionUsage is owned by a
    /// PartDefinition or PartUsage, then it also subsets the ActionUsage Part::performedAction from the
    /// Systems model library.
    /// </summary>
    public partial interface IPerformActionUsage : IActionUsage, IEventOccurrenceUsage
    {
    }
}
