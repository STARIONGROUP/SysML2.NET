// -------------------------------------------------------------------------------------------------
// <copyright file="IAttributeDefinition.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.Core.DTO.Systems.Attributes
{
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.Generic;

    using SysML2.NET.Core.DTO.Kernel.DataTypes;
    using SysML2.NET.Core.DTO.Systems.DefinitionAndUsage;
    using SysML2.NET.Decorators;

    /// <summary>
    /// An AttributeDefinition is a Definition and a DataType of information about a quality or
    /// characteristic of a system or part of a system that has no independent identity other than its
    /// value. All features of an AttributeDefinition must be referential (non-composite).  As a DataType,
    /// an AttributeDefinition must specialize, directly or indirectly, the base DataType Base::DataValue
    /// from the Kernel Semantic Library.
    /// </summary>
    [Class(xmiId: "Systems-Attributes-AttributeDefinition", isAbstract: false, isFinalSpecialization: false, isActive: false)]
    [GeneratedCode("SysML2.NET", "latest")]
    public partial interface IAttributeDefinition : IDataType, IDefinition
    {
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
