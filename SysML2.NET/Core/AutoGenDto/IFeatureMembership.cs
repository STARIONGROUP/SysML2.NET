// -------------------------------------------------------------------------------------------------
// <copyright file="IFeatureMembership.cs" company="Starion Group S.A.">
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
    /// A FeatureMembership is an OwningMembership between an ownedMemberFeature and an owningType. If the
    /// ownedMemberFeature has isVariable = false, then the FeatureMembership implies that the owningType is
    /// also a featuringType of the ownedMemberFeature. If the ownedMemberFeature has isVariable = true,
    /// then the FeatureMembership implies that the ownedMemberFeature is featured by the snapshots of the
    /// owningType, which must specialize the Kernel Semantic Library base class Occurrence.
    /// </summary>
    public partial interface IFeatureMembership : IOwningMembership
    {
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
