// -------------------------------------------------------------------------------------------------
// <copyright file="IMembership.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.Core.DTO.Root.Namespaces
{
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.Generic;

    using SysML2.NET.Core.Root.Namespaces;
    using SysML2.NET.Core.DTO.Root.Elements;
    using SysML2.NET.Decorators;

    /// <summary>
    /// A Membership is a Relationship between a Namespace and an Element that indicates the Element is a
    /// member of (i.e., is contained in) the Namespace. Any memberNames specify how the memberElement is
    /// identified in the Namespace and the visibility specifies whether or not the memberElement is
    /// publicly visible from outside the Namespace.If a Membership is an OwningMembership, then it owns its
    /// memberElement, which becomes an ownedMember of the membershipOwningNamespace. Otherwise, the
    /// memberNames of a Membership are effectively aliases within the membershipOwningNamespace for an
    /// Element with a separate OwningMembership in the same or a different Namespace. 
    /// </summary>
    [Class(xmiId: "_18_5_3_12e503d9_1533160651680_888716_42152", isAbstract: false, isFinalSpecialization: false, isActive: false)]
    [GeneratedCode("SysML2.NET", "latest")]
    public partial interface IMembership : IRelationship
    {
        /// <summary>
        /// The Element that becomes a member of the membershipOwningNamespace due to this Membership.
        /// </summary>
        [Property(xmiId: "_18_5_3_12e503d9_1533160674964_819490_43195", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [RedefinedProperty(propertyName: "_18_5_3_12e503d9_1533160674961_138197_43179")]
        Guid MemberElement { get; set; }

        /// <summary>
        /// The elementId of the memberElement.
        /// </summary>
        [Property(xmiId: "_19_0_4_12e503d9_1651721199802_246768_242", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        string memberElementId { get; }

        /// <summary>
        /// The name of the memberElement relative to the membershipOwningNamespace.
        /// </summary>
        [Property(xmiId: "_18_5_3_12e503d9_1533160674964_35293_43192", aggregation: AggregationKind.None, lowerValue: 0, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        string MemberName { get; set; }

        /// <summary>
        /// The Namespace of which the memberElement becomes a member due to this Membership.
        /// </summary>
        [Property(xmiId: "_18_5_3_12e503d9_1533160674965_193857_43197", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1533160674962_531296_43182")]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1543092026091_693018_16749")]
        [RedefinedProperty(propertyName: "_18_5_3_12e503d9_1533160674971_696758_43228")]
        Guid membershipOwningNamespace { get; }

        /// <summary>
        /// The short name of the memberElement relative to the membershipOwningNamespace.
        /// </summary>
        [Property(xmiId: "_19_0_4_12e503d9_1651721174176_601088_238", aggregation: AggregationKind.None, lowerValue: 0, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        string MemberShortName { get; set; }

        /// <summary>
        /// Whether or not the Membership of the memberElement in the membershipOwningNamespace is publicly
        /// visible outside that Namespace.
        /// </summary>
        [Property(xmiId: "_18_5_3_12e503d9_1533160674964_42975_43193", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: "public")]
        VisibilityKind Visibility { get; set; }

    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
