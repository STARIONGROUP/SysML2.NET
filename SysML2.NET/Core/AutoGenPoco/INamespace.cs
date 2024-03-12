// -------------------------------------------------------------------------------------------------
// <copyright file="INamespace.cs" company="RHEA System S.A.">
//
//   Copyright 2022-2024 RHEA System S.A.
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
    using SysML2.NET.Decorators;

    /// <summary>
    /// A Namespace is an Element that contains other Elements, known as its members, via Membership
    /// Relationships with those Elements. The members of a Namespace may be owned by the Namespace, aliased
    /// in the Namespace, or imported into the Namespace via Import Relationships.A Namespace can provide
    /// names for its members via the memberNames and memberShortNames specified by the Memberships in the
    /// Namespace. If a Membership specifies a memberName and/or memberShortName, then those are names of
    /// the corresponding memberElement relative to the Namespace. For an OwningMembership, the
    /// owningMemberName and owningMemberShortName are given by the Element name and shortName. Note that
    /// the same Element may be the memberElement of multiple Memberships in a Namespace (though it may be
    /// owned at most once), each of which may define a separate alias for the Element relative to the
    /// Namespace.membership->forAll(m1 |     membership->forAll(m2 |         m1 <> m2 implies
    /// m1.isDistinguishableFrom(m2)))member = membership.memberElementownedMember =
    /// ownedMembership->selectByKind(OwningMembership).ownedMemberElementimportedMembership =
    /// importedMemberships(Set{})ownedImport = ownedRelationship->selectByKind(Import)ownedMembership =
    /// ownedRelationship->selectByKind(Membership)
    /// </summary>
    public partial interface INamespace : IElement
    {
        /// <summary>
        /// Queries the derived property ImportedMembership
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: true, isTransient: true, isUnsettable: false, isDerived: true, isOrdered: true, isUnique: true, lowerBound: 0, upperBound: -1, isMany: false, isRequired: false, isContainment: false)]
        List<Membership> QueryImportedMembership();

        /// <summary>
        /// Queries the derived property Member
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: true, isTransient: true, isUnsettable: false, isDerived: true, isOrdered: true, isUnique: true, lowerBound: 0, upperBound: -1, isMany: false, isRequired: false, isContainment: false)]
        List<IElement> QueryMember();

        /// <summary>
        /// Queries the derived property Membership
        /// </summary>
        [EFeature(isChangeable: false, isVolatile: true, isTransient: true, isUnsettable: false, isDerived: true, isOrdered: true, isUnique: true, lowerBound: 0, upperBound: -1, isMany: false, isRequired: false, isContainment: false)]
        List<Membership> QueryMembership();

        /// <summary>
        /// Queries the derived property OwnedImport
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: true, isTransient: true, isUnsettable: false, isDerived: true, isOrdered: true, isUnique: true, lowerBound: 0, upperBound: -1, isMany: false, isRequired: false, isContainment: false)]
        List<IImport> QueryOwnedImport();

        /// <summary>
        /// Queries the derived property OwnedMember
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: true, isTransient: true, isUnsettable: false, isDerived: true, isOrdered: true, isUnique: true, lowerBound: 0, upperBound: -1, isMany: false, isRequired: false, isContainment: false)]
        List<IElement> QueryOwnedMember();

        /// <summary>
        /// Queries the derived property OwnedMembership
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: true, isTransient: true, isUnsettable: false, isDerived: true, isOrdered: true, isUnique: true, lowerBound: 0, upperBound: -1, isMany: false, isRequired: false, isContainment: false)]
        List<Membership> QueryOwnedMembership();

    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
