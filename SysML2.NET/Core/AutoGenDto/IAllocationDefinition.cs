// -------------------------------------------------------------------------------------------------
// <copyright file="IAllocationDefinition.cs" company="RHEA System S.A.">
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

namespace SysML2.NET.Core.DTO
{
    using System;
    using System.Collections.Generic;

    using SysML2.NET.Core;
    using SysML2.NET.Decorators;

    /// <summary>
    /// An AllocationDefinition is a ConnectionDefinition that specifies that some or all of the
    /// responsibility to realize the intent of the source is allocated to the target instances. Such
    /// allocations define mappings across the various structures and hierarchies of a system model, perhaps
    /// as a precursor to more rigorous specifications and implementations. An AllocationDefinition can
    /// itself be refined using nested allocations that give a finer-grained decomposition of the containing
    /// allocation mapping.An AllocationDefinition must subclass, directly or indirectly, the base
    /// AllocationDefinition Allocation from the Systems model library.
    /// </summary>
    public partial interface IAllocationDefinition : IConnectionDefinition
    {
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
