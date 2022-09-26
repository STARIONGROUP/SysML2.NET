// -------------------------------------------------------------------------------------------------
// <copyright file="IMetadataUsage.cs" company="RHEA System S.A.">
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
    /// An MetadataUsage is a Usage and a MetadataFeature, used to annotate other Elements in a system model
    /// with metadata. As a MetadataFeature, its type must be a Metaclass, which will nominally be a
    /// MetadataDefinition. However, any Kernel Metaclass is also allowed, to permit use of Metaclasses from
    /// the Kernel Library.A MetadataUsage must subset, directly or indirectly, the base MetadataUsage
    /// metadataItems from the Systems model library.
    /// </summary>
    public partial interface IMetadataUsage : IItemUsage, IMetadataFeature
    {
        /// <summary>
        /// Queries the derived property MetadataDefinition
        /// </summary>
        Metaclass QueryMetadataDefinition();

    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
