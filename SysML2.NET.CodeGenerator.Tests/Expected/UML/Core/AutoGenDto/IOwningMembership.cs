// -------------------------------------------------------------------------------------------------
// <copyright file="IOwningMembership.cs" company="Starion Group S.A.">
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
    using SysML2.NET.Decorators;

    /// <summary>
    /// An OwningMembership is a Membership that owns its memberElement as a ownedRelatedElement. The
    /// ownedMemberElement becomes an ownedMember of the membershipOwningNamespace.
    /// </summary>
    [Class(xmiId: "Root-Namespaces-OwningMembership", isAbstract: false, isFinalSpecialization: false, isActive: false)]
    [GeneratedCode("SysML2.NET", "latest")]
    public partial interface IOwningMembership : IMembership
    {
        /// <summary>
        /// The Element that becomes an ownedMember of the membershipOwningNamespace due to this
        /// OwningMembership.
        /// </summary>
        [Property(xmiId: "Root-Namespaces-OwningMembership-ownedMemberElement", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Root-Elements-Relationship-ownedRelatedElement")]
        [RedefinedProperty(propertyName: "Root-Namespaces-Membership-memberElement")]
        Guid ownedMemberElement { get; }

        /// <summary>
        /// The elementId of the ownedMemberElement.
        /// </summary>
        [Property(xmiId: "Root-Namespaces-OwningMembership-ownedMemberElementId", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [RedefinedProperty(propertyName: "Root-Namespaces-Membership-memberElementId")]
        string ownedMemberElementId { get; }

        /// <summary>
        /// The name of the ownedMemberElement.
        /// </summary>
        [Property(xmiId: "Root-Namespaces-OwningMembership-ownedMemberName", aggregation: AggregationKind.None, lowerValue: 0, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [RedefinedProperty(propertyName: "Root-Namespaces-Membership-memberName")]
        string ownedMemberName { get; }

        /// <summary>
        /// The shortName of the ownedMemberElement.
        /// </summary>
        [Property(xmiId: "Root-Namespaces-OwningMembership-ownedMemberShortName", aggregation: AggregationKind.None, lowerValue: 0, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [RedefinedProperty(propertyName: "Root-Namespaces-Membership-memberShortName")]
        string ownedMemberShortName { get; }

    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
