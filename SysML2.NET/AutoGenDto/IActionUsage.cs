// -------------------------------------------------------------------------------------------------
// <copyright file="IActionUsage.cs" company="RHEA System S.A.">
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
    /// An ActionUsage is a Usage that is also a Step, and, so, is typed by a Behavior. Nominally, if the
    /// type is an ActionDefinition, an ActionUsage is a Usage of that ActionDefinition within a system.
    /// However, other kinds of kernel Behaviors are also allowed, to permit use of Behaviors from the
    /// Kernel Library.An ActionUsage must subset, directly or indirectly, the base ActionUsage actions from
    /// the Systems model library. if it is a feature of an ActionDefinition or ActionUsage, then it must
    /// subset, directly or indirectly, the ActionUsage Action::subactions.
    /// </summary>
    public interface IActionUsage : IOccurrenceUsage, IStep
    {
    }
}
