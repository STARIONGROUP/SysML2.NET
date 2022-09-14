// -------------------------------------------------------------------------------------------------
// <copyright file="IFeaturing.cs" company="RHEA System S.A.">
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

namespace SysML2.NET.Core.DTO
{
    using System;
    using System.Collections.Generic;

    using SysML2.NET.Core;

    /// <summary>
    /// A Featuring is a Relationship between a Type and a Feature that is featured by that Type. Every
    /// instance in the domain of the featureOfType must be classified by the featuringType. This means that
    /// sequences that are classified by the featureOfType must have a prefix subsequence that is classified
    /// by the featuringType.Featuring is abstract and does not commit to which of featureOfType or
    /// featuredType are the source or target. This commitment is made in the subclasses of Featuring,
    /// TypeFeaturing and FeatureMembership, which are directed differently.
    /// </summary>
    public partial interface IFeaturing : IRelationship
    {
        /// <summary>
        /// </summary>
        Guid Feature { get; set; }

        /// <summary>
        /// </summary>
        Guid Type { get; set; }

    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
