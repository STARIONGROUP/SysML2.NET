// -------------------------------------------------------------------------------------------------
// <copyright file="IElementFilterMembership.cs" company="RHEA System S.A.">
//
//   Copyright 2022 RHEA System S.A.
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

namespace SysML2.NET.Core.POCO
{
    using System;
    using System.Collections.Generic;

    using SysML2.NET.Core;

    /// <summary>
    /// ElementFilterMembership is a Mambership between a Namespace and a model-level evaluable Boolean
    /// Expression, asserting that imported members of the Namespace should be filtered using the condition
    /// Expression. A general Namespace does not define any specific filtering behavior, but such behavior
    /// may be defined for various specialized kinds of Namespaces.condition.isModelLevelEvaluable
    /// </summary>
    public partial interface IElementFilterMembership : IOwningMembership
    {
        /// <summary>
        /// Queries the derived property Condition
        /// </summary>
        Expression QueryCondition();

    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
