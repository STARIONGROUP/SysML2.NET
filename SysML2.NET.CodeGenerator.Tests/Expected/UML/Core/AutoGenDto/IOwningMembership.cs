// -------------------------------------------------------------------------------------------------
// <copyright file="IOwningMembership.cs" company="Starion Group S.A.">
//
//    Copyright (C) 2022-2025 Starion Group S.A.
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

namespace SysML2.NET.Core.DTO
{
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.Generic;

    using SysML2.NET.Root.Namespaces;

    using SysML2.NET.Decorators;

    /// <summary>
    /// An OwningMembership is a Membership that owns its memberElement as a ownedRelatedElement. The
    /// ownedMemberElement becomes an ownedMember of the membershipOwningNamespace.
    /// </summary>
    [Class(xmiId: "_19_0_4_12e503d9_1648180804650_933390_31", isAbstract: false, isFinalSpecialization: false, isActive: false)]
    [GeneratedCode("SysML2.NET", "latest")]
    public partial interface IOwningMembership : IMembership
    {
        /// <summary>
        /// The Element that becomes an ownedMember of the membershipOwningNamespace due to this
        /// OwningMembership.
        /// </summary>
        [Property(xmiId: "_18_5_3_12e503d9_1533160674965_501750_43196", aggregation: AggregationKind.Composite, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1533160674986_59873_43302")]
        [RedefinedProperty(propertyName: "_18_5_3_12e503d9_1533160674964_819490_43195")]
        Guid OwnedMemberElement { get; }

        /// <summary>
        /// The elementId of the ownedMemberElement.
        /// </summary>
        [Property(xmiId: "_19_0_4_12e503d9_1651721234828_904219_244", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [RedefinedProperty(propertyName: "_19_0_4_12e503d9_1651721199802_246768_242")]
        string OwnedMemberElementId { get; }

        /// <summary>
        /// The name of the ownedMemberElement.
        /// </summary>
        [Property(xmiId: "_19_0_4_12e503d9_1648181616390_323441_387", aggregation: AggregationKind.None, lowerValue: 0, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [RedefinedProperty(propertyName: "_18_5_3_12e503d9_1533160674964_35293_43192")]
        string? OwnedMemberName { get; }

        /// <summary>
        /// The shortName of the ownedMemberElement.
        /// </summary>
        [Property(xmiId: "_19_0_4_12e503d9_1651721262092_909505_246", aggregation: AggregationKind.None, lowerValue: 0, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [RedefinedProperty(propertyName: "_19_0_4_12e503d9_1651721174176_601088_238")]
        string? OwnedMemberShortName { get; }

    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
