// -------------------------------------------------------------------------------------------------
// <copyright file="INamespace.cs" company="RHEA System S.A.">
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
    /// A Namespace is an Element that contains other Elements, known as its members, via Membership
    /// Relationships with those Elements. The members of a Namespace may be owned by the Namespace, aliased
    /// in the Namespace, or imported into the Namespace via Import Relationships with other Namespaces.A
    /// Namespace can provide names for its members via the memberNames specified by the Memberships in the
    /// Namespace. If a Membership specifies a memberName, then that is the name of the corresponding
    /// memberElement relative to the Namespace. Note that the same Element may be the memberElement of
    /// multiple Memberships in a Namespace (though it may be owned at most once), each of which may define
    /// a separate alias for the Element relative to the Namespace.membership->forAll(m1 |
    /// membership->forAll(m2 | m1 <> m2 implies m1.isDistinguishableFrom(m2)))member =
    /// membership.memberElementownedMember =
    /// ownedMembership->selectByKind(OwningMembership).ownedMemberElementimportedMembership =
    /// importedMemberships(Set{})ownedImport = ownedRelationship->selectByKind(Import)ownedMembership =
    /// ownedRelationship->selectByKind(Membership)
    /// </summary>
    public partial interface INamespace : IElement
    {
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
