// -------------------------------------------------------------------------------------------------
// <copyright file="ILifeClass.cs" company="RHEA System S.A.">
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
    /// A LifeClass is a Class that specializes both the Base::Life Class from the Kernel Library and a
    /// single OccurrenceDefinition, and has a multiplicity of 0..1. This constrains the
    /// OccurrenceDefinition to have at most one instance that is a complete Life.
    /// </summary>
    public partial interface ILifeClass : IClass
    {
    }
}
