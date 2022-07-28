// -------------------------------------------------------------------------------------------------
// <copyright file="IItemDefinition.cs" company="RHEA System S.A.">
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
    /// An ItemDefinition is an OccurrenceDefinition of the Structure of things that may be acted on by a
    /// system or parts of a system, which do not necessarily perform actions themselves. This includes
    /// items that can be exchanged between parts of a system, such as water or electrical signals.An
    /// ItemDefinition must subclass, directly or indirectly, the base ItemDefinition Item from the Systems
    /// model library.
    /// </summary>
    public partial interface IItemDefinition : IOccurrenceDefinition, IStructure
    {
    }
}
