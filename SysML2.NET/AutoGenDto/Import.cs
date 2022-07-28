// -------------------------------------------------------------------------------------------------
// <copyright file="Import.cs" company="RHEA System S.A.">
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
    /// An Import is a Relationship between an importOwningNamespace in which one or more of the visible
    /// Memberships of the importedNamespace become importedMemberships of the importOwningNamespace. If
    /// isImportAll = false (the default), then only public Memberships are considered &quot;visible&quot;.
    /// If isImportAll = true, then all Memberships are considered &quot;visible&quot;, regardless of their
    /// declared visibility.If no importedMemberName is given, then all visible Memberships are imported
    /// from the importedNamespace. If isRecursive = true, then visible Memberships are also recursively
    /// imported from all visible ownedMembers of the Namespace that are also Namespaces.If an 
    /// importedMemberName is given, then the Membership whose effectiveMemberName is that name is imported
    /// from the importedNamespace, if it is visible. If isRecursive = true and the imported memberElement
    /// is a Namespace, then visible Memberships are also recursively imported from that Namespace and its
    /// owned sub-Namespaces.
    /// </summary>
    public class Import : IRelationship
    {
        /// <summary>
        /// The effectiveMemberName of the Membership of the importedNamspace to be imported. If not given, all
        /// public Memberships of the importedNamespace are imported.
        /// </summary>
        public string ImportedMemberName { get; set; }

        /// <summary>
        /// </summary>
        public Guid ImportedNamespace { get; set; }

        /// <summary>
        /// Whether to import memberships without regard to declared visibility.
        /// </summary>
        public bool IsImportAll { get; set; }

        /// <summary>
        /// Whether to recursively import Memberships from visible, owned sub-namespaces.
        /// </summary>
        public bool IsRecursive { get; set; }

        /// <summary>
        /// The visibility level of the imported members from this Import relative to the importOwningNamespace.
        /// </summary>
        public VisibilityKind Visibility { get; set; }

        /// <summary>
        /// The relatedElements of this Relationship that are owned by the Relationship.
        /// </summary>
        public List<Guid> OwnedRelatedElement { get; set; }

        /// <summary>
        /// The relatedElement of this Relationship that owns the Relationship, if any.
        /// </summary>
        public Guid? OwningRelatedElement { get; set; }

        /// <summary>
        /// </summary>
        public List<Guid> Source { get; set; }

        /// <summary>
        /// </summary>
        public List<Guid> Target { get; set; }

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
