// -------------------------------------------------------------------------------------------------
// <copyright file="IMembership.cs" company="RHEA System S.A.">
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

namespace SysML2.NET.Core.POCO
{
    using System;
    using System.Collections.Generic;

    using SysML2.NET.Core;

    /// <summary>
    /// Membership is a Relationship between a Namespace and an Element that indicates the Element is a
    /// member of (i.e., is contained in) the Namespace. Any memberNames specify how the memberElement is
    /// identified in the Namespace and the visibility specifies whether or not the memberElement is
    /// publicly visible from outside the Namespace.If a Membership is an OwningMembership, then it owns its
    /// memberElement, which becomes an ownedMember of the membershipOwningNamespace. Otherwise, the
    /// memberNames of a Membership are effectively aliases within the membershipOwningNamespace for an
    /// Element with a separate OwningMembership in the same or a different Namespace.
    /// </summary>
    public partial interface IMembership : IRelationship
    {
        /// <summary>
        /// </summary>
        Element MemberElement { get; set; }

        /// <summary>
        /// Queries the derived property MemberElementId
        /// </summary>
        string QueryMemberElementId();

        /// <summary>
        /// The name of the memberElement relative to the membershipOwningNamespace.
        /// </summary>
        string MemberName { get; set; }

        /// <summary>
        /// Queries the derived property MembershipOwningNamespace
        /// </summary>
        Namespace QueryMembershipOwningNamespace();

        /// <summary>
        /// The short name of the memberElement relative to the membershipOwningNamespace.
        /// </summary>
        string MemberShortName { get; set; }

        /// <summary>
        /// Whether or not the Membership of the memberElement in the membershipOwningNamespace is publicly
        /// visible outside that Namespace.
        /// </summary>
        VisibilityKind Visibility { get; set; }

    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
