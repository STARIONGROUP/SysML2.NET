// -------------------------------------------------------------------------------------------------
// <copyright file="IInterfaceDefinition.cs" company="RHEA System S.A.">
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
    /// An InterfaceDefinition is a ConnectionDefinition all of whose ends are PortUsages, defining an
    /// interface between elements that interact through such
    /// ports.specializesFromLibrary("Interfaces::Interface")ownedEndFeature->size() = 2 implies   
    /// specializesFromLibrary("Interfaces::BinaryInterface")
    /// </summary>
    public partial interface IInterfaceDefinition : IConnectionDefinition
    {
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
