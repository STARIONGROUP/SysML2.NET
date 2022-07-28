// -------------------------------------------------------------------------------------------------
// <copyright file="Namespace.cs" company="RHEA System S.A.">
//
// Copyright 2022 RHEA System S.A.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.DTO
{
    using System;
    using System.Collections.Generic;

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
    public class Namespace : IElement
    {
        /// <summary>
        /// Various alternative identifiers for this Element. Generally, these will be set by tools.
        /// </summary>
        public List<string> AliasIds { get; set; }

        /// <summary>
        /// The globally unique identifier for this Element. This is intended to be set by tooling, and it must
        /// not change during the lifetime of the Element.
        /// </summary>
        public string ElementId { get; set; }

        /// <summary>
        /// The primary name of this Element.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The Relationships for which this Element is the owningRelatedElement.
        /// </summary>
        public List<Guid> OwnedRelationship { get; set; }

        /// <summary>
        /// The Relationship for which this Element is an ownedRelatedElement, if any.
        /// </summary>
        public Guid? OwningRelationship { get; set; }

        /// <summary>
        /// An optional alternative name for the Element that is intended to be shorter or in some way more
        /// succinct than its primary name. It may act as a modeler-specified identifier for the Element, though
        /// it is then the responsibility of the modeler to maintain the uniqueness of this identifier within a
        /// model or relative to some other context. 
        /// </summary>
        public string ShortName { get; set; }

    }
}
