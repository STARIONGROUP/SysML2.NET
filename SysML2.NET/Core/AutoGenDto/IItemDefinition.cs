// -------------------------------------------------------------------------------------------------
// <copyright file="IItemDefinition.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.Core.DTO.Systems.Items
{
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.Generic;

    using SysML2.NET.Core.DTO.Kernel.Structures;
    using SysML2.NET.Core.DTO.Systems.Occurrences;
    using SysML2.NET.Decorators;

    /// <summary>
    /// An ItemDefinition is an OccurrenceDefinition of the Structure of things that may themselves be
    /// systems or parts of systems, but may also be things that are acted on by a system or parts of a
    /// system, but which do not necessarily perform actions themselves. This includes items that can be
    /// exchanged between parts of a system, such as water or electrical signals.
    /// </summary>
    [Class(xmiId: "Systems-Items-ItemDefinition", isAbstract: false, isFinalSpecialization: false, isActive: false)]
    [GeneratedCode("SysML2.NET", "latest")]
    public partial interface IItemDefinition : IStructure, IOccurrenceDefinition
    {
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
