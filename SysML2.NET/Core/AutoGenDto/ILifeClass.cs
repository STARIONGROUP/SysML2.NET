// -------------------------------------------------------------------------------------------------
// <copyright file="ILifeClass.cs" company="Starion Group S.A.">
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
    /// A LifeClass is a Class that specializes both the Class Occurrences::Life from the Kernel Semantic
    /// Library and a single OccurrenceDefinition, and has a multiplicity of 0..1. This constrains the
    /// OccurrenceDefinition being specialized to have at most one instance that is a complete
    /// Life.specializesFromLibrary('Occurrences::Life')multiplicity <> null
    /// andmultiplicity.specializesFromLibrary('Base::zeroOrOne')specializes(individualDefinition)isSufficient
    /// </summary>
    public partial interface ILifeClass : IClass
    {
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
