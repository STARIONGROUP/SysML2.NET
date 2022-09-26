// -------------------------------------------------------------------------------------------------
// <copyright file="IConnectionDefinition.cs" company="RHEA System S.A.">
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
    /// A ConnectionDefinition is a PartDefinition that is also an AssociationStructure, with two or more
    /// end features. The associationEnds of a ConnectionDefinition must be Usages.A ConnectionDefinition
    /// must subclass, directly or indirectly, the base ConnectionDefinition Connection from the Systems
    /// model library.
    /// </summary>
    public partial interface IConnectionDefinition : IPartDefinition, IAssociationStructure
    {
        /// <summary>
        /// Queries the derived property ConnectionEnd
        /// </summary>
        List<Usage> QueryConnectionEnd();

    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
