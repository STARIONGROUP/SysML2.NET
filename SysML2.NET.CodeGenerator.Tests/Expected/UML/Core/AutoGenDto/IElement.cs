// -------------------------------------------------------------------------------------------------
// <copyright file="IElement.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.Core.DTO.Root.Elements
{
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.Generic;

    using SysML2.NET.Decorators;

    /// <summary>
    /// An Element is a constituent of a model that is uniquely identified relative to all other Elements.
    /// It can have Relationships with other Elements. Some of these Relationships might imply ownership of
    /// other Elements, which means that if an Element is deleted from a model, then so are all the Elements
    /// that it owns.
    /// </summary>
    [Class(xmiId: "Root-Elements-Element", isAbstract: true, isFinalSpecialization: false, isActive: false)]
    [GeneratedCode("SysML2.NET", "latest")]
    public partial interface IElement
    {
        /// <summary>
        /// Various alternative identifiers for this Element. Generally, these will be set by tools.
        /// </summary>
        [Property(xmiId: "Root-Elements-Element-aliasIds", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        List<string> AliasIds { get; set; }

        /// <summary>
        /// The declared name of this Element.
        /// </summary>
        [Property(xmiId: "Root-Elements-Element-declaredName", aggregation: AggregationKind.None, lowerValue: 0, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        string DeclaredName { get; set; }

        /// <summary>
        /// An optional alternative name for the Element that is intended to be shorter or in some way more
        /// succinct than its primary name. It may act as a modeler-specified identifier for the Element, though
        /// it is then the responsibility of the modeler to maintain the uniqueness of this identifier within a
        /// model or relative to some other context.
        /// </summary>
        [Property(xmiId: "Root-Elements-Element-declaredShortName", aggregation: AggregationKind.None, lowerValue: 0, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        string DeclaredShortName { get; set; }

        /// <summary>
        /// The Documentation owned by this Element.
        /// </summary>
        [Property(xmiId: "Root-Elements-Element-documentation", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Root-Annotations-A_annotatedElement_annotatingElement-annotatingElement")]
        [SubsettedProperty(propertyName: "Root-Elements-Element-ownedElement")]
        List<Guid> documentation { get; }

        /// <summary>
        /// The globally unique identifier for this Element. This is intended to be set by tooling, and it must
        /// not change during the lifetime of the Element.
        /// </summary>
        [Property(xmiId: "Root-Elements-Element-elementId", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        string ElementId { get; set; }

        /// <summary>
        /// Whether all necessary implied Relationships have been included in the ownedRelationships of this
        /// Element. This property may be true, even if there are not actually any ownedRelationships with
        /// isImplied = true, meaning that no such Relationships are actually implied for this Element. However,
        /// if it is false, then ownedRelationships may not contain any implied Relationships. That is, either
        /// all required implied Relationships must be included, or none of them.
        /// </summary>
        [Property(xmiId: "Root-Elements-Element-isImpliedIncluded", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: "false")]
        bool IsImpliedIncluded { get; set; }

        /// <summary>
        /// Whether this Element is contained in the ownership tree of a library model.
        /// </summary>
        [Property(xmiId: "Root-Elements-Element-isLibraryElement", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        bool isLibraryElement { get; }

        /// <summary>
        /// The name to be used for this Element during name resolution within its owningNamespace. This is
        /// derived using the effectiveName() operation. By default, it is the same as the declaredName, but
        /// this is overridden for certain kinds of Elements to compute a name even when the declaredName is
        /// null.
        /// </summary>
        [Property(xmiId: "Root-Elements-Element-name", aggregation: AggregationKind.None, lowerValue: 0, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        string name { get; }

        /// <summary>
        /// The ownedRelationships of this Element that are Annotations, for which this Element is the
        /// annotatedElement.
        /// </summary>
        [Property(xmiId: "Root-Elements-Element-ownedAnnotation", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Root-Elements-Element-ownedRelationship")]
        [SubsettedProperty(propertyName: "Root-Annotations-A_annotatedElement_annotation-annotation")]
        List<Guid> ownedAnnotation { get; }

        /// <summary>
        /// The Elements owned by this Element, derived as the ownedRelatedElements of the ownedRelationships of
        /// this Element.
        /// </summary>
        [Property(xmiId: "Root-Elements-Element-ownedElement", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        List<Guid> ownedElement { get; }

        /// <summary>
        /// The Relationships for which this Element is the owningRelatedElement.
        /// </summary>
        [Property(xmiId: "Root-Elements-Element-ownedRelationship", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Root-Elements-A_relatedElement_relationship-relationship")]
        List<Guid> OwnedRelationship { get; set; }

        /// <summary>
        /// The owner of this Element, derived as the owningRelatedElement of the owningRelationship of this
        /// Element, if any.
        /// </summary>
        [Property(xmiId: "Root-Elements-Element-owner", aggregation: AggregationKind.None, lowerValue: 0, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        Guid? owner { get; }

        /// <summary>
        /// The owningRelationship of this Element, if that Relationship is a Membership.
        /// </summary>
        [Property(xmiId: "Root-Elements-Element-owningMembership", aggregation: AggregationKind.None, lowerValue: 0, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Root-Namespaces-A_memberElement_membership-membership")]
        [SubsettedProperty(propertyName: "Root-Elements-Element-owningRelationship")]
        Guid? owningMembership { get; }

        /// <summary>
        /// The Namespace that owns this Element, which is the membershipOwningNamespace of the owningMembership
        /// of this Element, if any.
        /// </summary>
        [Property(xmiId: "Root-Elements-Element-owningNamespace", aggregation: AggregationKind.None, lowerValue: 0, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Root-Namespaces-A_member_namespace-namespace")]
        Guid? owningNamespace { get; }

        /// <summary>
        /// The Relationship for which this Element is an ownedRelatedElement, if any.
        /// </summary>
        [Property(xmiId: "Root-Elements-Element-owningRelationship", aggregation: AggregationKind.None, lowerValue: 0, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Root-Elements-A_relatedElement_relationship-relationship")]
        Guid? OwningRelationship { get; set; }

        /// <summary>
        /// The full ownership-qualified name of this Element, represented in a form that is valid according to
        /// the KerML textual concrete syntax for qualified names (including use of unrestricted name notation
        /// and escaped characters, as necessary). The qualifiedName is null if this Element has no
        /// owningNamespace or if there is not a complete ownership chain of named Namespaces from a root
        /// Namespace to this Element. If the owningNamespace has other Elements with the same name as this one,
        /// then the qualifiedName is null for all such Elements other than the first.
        /// </summary>
        [Property(xmiId: "Root-Elements-Element-qualifiedName", aggregation: AggregationKind.None, lowerValue: 0, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        string qualifiedName { get; }

        /// <summary>
        /// The short name to be used for this Element during name resolution within its owningNamespace. This
        /// is derived using the effectiveShortName() operation. By default, it is the same as the
        /// declaredShortName, but this is overridden for certain kinds of Elements to compute a shortName even
        /// when the declaredName is null.
        /// </summary>
        [Property(xmiId: "Root-Elements-Element-shortName", aggregation: AggregationKind.None, lowerValue: 0, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        string shortName { get; }

        /// <summary>
        /// The TextualRepresentations that annotate this Element.
        /// </summary>
        [Property(xmiId: "Root-Elements-Element-textualRepresentation", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Root-Annotations-A_annotatedElement_annotatingElement-annotatingElement")]
        [SubsettedProperty(propertyName: "Root-Elements-Element-ownedElement")]
        List<Guid> textualRepresentation { get; }

    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
