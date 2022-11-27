// -------------------------------------------------------------------------------------------------
// <copyright file="Import.cs" company="RHEA System S.A.">
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

namespace SysML2.NET.Core.POCO
{
    using System;
    using System.Collections.Generic;

    using SysML2.NET.Core;

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
    public partial class Import : IImport
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Import"/> class.
        /// </summary>
        public Import()
        {
            this.AliasIds = new List<string>();
            this.IsImplied = false;
            this.IsImpliedIncluded = false;
            this.IsImportAll = false;
            this.IsRecursive = false;
            this.OwnedRelatedElement = new List<Element>();
            this.OwnedRelationship = new List<Relationship>();
            this.Source = new List<Element>();
            this.Target = new List<Element>();
            this.Visibility = VisibilityKind.Public;
        }

        /// <summary>
        /// Gets or sets the unique identifier
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Various alternative identifiers for this Element. Generally, these will be set by tools.
        /// </summary>
        public List<string> AliasIds { get; set; }

        /// <summary>
        /// Queries the derived property Documentation
        /// </summary>
        public List<Documentation> QueryDocumentation()
        {
            throw new NotImplementedException("Derived property Documentation not yet supported");
        }

        /// <summary>
        /// Queries the derived property EffectiveName
        /// </summary>
        public string QueryEffectiveName()
        {
            throw new NotImplementedException("Derived property EffectiveName not yet supported");
        }

        /// <summary>
        /// The globally unique identifier for this Element. This is intended to be set by tooling, and it must
        /// not change during the lifetime of the Element.
        /// </summary>
        public string ElementId { get; set; }

        /// <summary>
        /// The effectiveMemberName of the Membership of the importedNamspace to be imported. If not given, all
        /// public Memberships of the importedNamespace are imported.
        /// </summary>
        public string ImportedMemberName { get; set; }

        /// <summary>
        /// </summary>
        public Namespace ImportedNamespace { get; set; }

        /// <summary>
        /// Queries the derived property ImportOwningNamespace
        /// </summary>
        public Namespace QueryImportOwningNamespace()
        {
            throw new NotImplementedException("Derived property ImportOwningNamespace not yet supported");
        }

        /// <summary>
        /// Whether this Relationship was generated by tooling to meet semantic rules, rather than being
        /// directly created by a modeler.
        /// </summary>
        public bool IsImplied { get; set; }

        /// <summary>
        /// Whether all necessary implied Relationships have been included in the ownedRelationships of this
        /// Element. This property may be true, even if there are not actually any ownedRelationships with
        /// isImplied = true, meaning that no such Relationships are actually implied for this Element. However,
        /// if it is false, then ownedRelationships may not contain any implied Relationships. That is, either
        /// all required implied Relationships must be included, or none of them.
        /// </summary>
        public bool IsImpliedIncluded { get; set; }

        /// <summary>
        /// Whether to import memberships without regard to declared visibility.
        /// </summary>
        public bool IsImportAll { get; set; }

        /// <summary>
        /// Queries the derived property IsLibraryElement
        /// </summary>
        public bool QueryIsLibraryElement()
        {
            throw new NotImplementedException("Derived property IsLibraryElement not yet supported");
        }

        /// <summary>
        /// Whether to recursively import Memberships from visible, owned sub-namespaces.
        /// </summary>
        public bool IsRecursive { get; set; }

        /// <summary>
        /// The primary name of this Element.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Queries the derived property OwnedAnnotation
        /// </summary>
        public List<Annotation> QueryOwnedAnnotation()
        {
            throw new NotImplementedException("Derived property OwnedAnnotation not yet supported");
        }

        /// <summary>
        /// Queries the derived property OwnedElement
        /// </summary>
        public List<Element> QueryOwnedElement()
        {
            throw new NotImplementedException("Derived property OwnedElement not yet supported");
        }

        /// <summary>
        /// The relatedElements of this Relationship that are owned by the Relationship.
        /// </summary>
        public List<Element> OwnedRelatedElement { get; set; }

        /// <summary>
        /// The Relationships for which this Element is the owningRelatedElement.
        /// </summary>
        public List<Relationship> OwnedRelationship { get; set; }

        /// <summary>
        /// Queries the derived property Owner
        /// </summary>
        public Element QueryOwner()
        {
            throw new NotImplementedException("Derived property Owner not yet supported");
        }

        /// <summary>
        /// Queries the derived property OwningMembership
        /// </summary>
        public OwningMembership QueryOwningMembership()
        {
            throw new NotImplementedException("Derived property OwningMembership not yet supported");
        }

        /// <summary>
        /// Queries the derived property OwningNamespace
        /// </summary>
        public Namespace QueryOwningNamespace()
        {
            throw new NotImplementedException("Derived property OwningNamespace not yet supported");
        }

        /// <summary>
        /// The relatedElement of this Relationship that owns the Relationship, if any.
        /// </summary>
        public Element OwningRelatedElement { get; set; }

        /// <summary>
        /// The Relationship for which this Element is an ownedRelatedElement, if any.
        /// </summary>
        public Relationship OwningRelationship { get; set; }

        /// <summary>
        /// Queries the derived property QualifiedName
        /// </summary>
        public string QueryQualifiedName()
        {
            throw new NotImplementedException("Derived property QualifiedName not yet supported");
        }

        /// <summary>
        /// Queries the derived property RelatedElement
        /// </summary>
        public List<Element> QueryRelatedElement()
        {
            throw new NotImplementedException("Derived property RelatedElement not yet supported");
        }

        /// <summary>
        /// An optional alternative name for the Element that is intended to be shorter or in some way more
        /// succinct than its primary name. It may act as a modeler-specified identifier for the Element, though
        /// it is then the responsibility of the modeler to maintain the uniqueness of this identifier within a
        /// model or relative to some other context.
        /// </summary>
        public string ShortName { get; set; }

        /// <summary>
        /// </summary>
        public List<Element> Source { get; set; }

        /// <summary>
        /// </summary>
        public List<Element> Target { get; set; }

        /// <summary>
        /// Queries the derived property TextualRepresentation
        /// </summary>
        public List<TextualRepresentation> QueryTextualRepresentation()
        {
            throw new NotImplementedException("Derived property TextualRepresentation not yet supported");
        }

        /// <summary>
        /// The visibility level of the imported members from this Import relative to the importOwningNamespace.
        /// </summary>
        public VisibilityKind Visibility { get; set; }

    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
