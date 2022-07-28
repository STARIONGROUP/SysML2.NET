// -------------------------------------------------------------------------------------------------
// <copyright file="IPartDefinition.cs" company="RHEA System S.A.">
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
    /// A PartDefinition is a ItemDefinition of a Class of systems or parts of systems. Note that all parts
    /// may be considered items for certain purposes, but not all items are parts that can perform actions
    /// within a system.A PartDefinition must subclass, directly or indirectly, the base PartDefinition Part
    /// from the Systems model library.
    /// </summary>
    public partial interface IPartDefinition : IItemDefinition
    {
    }
}
