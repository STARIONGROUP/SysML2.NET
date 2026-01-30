// -------------------------------------------------------------------------------------------------
// <copyright file="AnnotatingElement.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.Core.POCO.Root.Annotations
{
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.Generic;
    using System.Linq;

    using SysML2.NET.Core.POCO.Root.Elements;
    using SysML2.NET.Core.POCO.Root.Namespaces;
    using SysML2.NET.Decorators;

    /// <summary>
    /// An AnnotatingElement is an Element that provides additional description of or metadata on some other
    /// Element. An AnnotatingElement is either attached to its annotatedElements by Annotation
    /// Relationships, or it implicitly annotates its owningNamespace.
    /// </summary>
    [Class(xmiId: "Root-Annotations-AnnotatingElement", isAbstract: false, isFinalSpecialization: false, isActive: false)]
    [GeneratedCode("SysML2.NET", "latest")]
    public partial class AnnotatingElement : IAnnotatingElement
    {
        /// <summary>
        /// Gets or sets the unique identifier
        /// </summary>
        [Property(xmiId: "sysml2.net", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [Implements(implementation: "IData.Id")]
        public Guid Id { get; set; }

        /// <summary>
        /// Various alternative identifiers for this Element. Generally, these will be set by tools.
        /// </summary>
        [Property(xmiId: "Root-Elements-Element-aliasIds", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [Implements(implementation: "IElement.AliasIds")]
        public List<string> AliasIds { get; set; } = [];

        /// <summary>
        /// The Elements that are annotated by this AnnotatingElement. If annotation is not empty, these are the
        /// annotatedElements of the annotations. If annotation is empty, then it is the owningNamespace of the
        /// AnnotatingElement.
        /// </summary>
        [Property(xmiId: "Root-Annotations-AnnotatingElement-annotatedElement", aggregation: AggregationKind.None, lowerValue: 1, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [Implements(implementation: "IAnnotatingElement.AnnotatedElement")]
        public List<IElement> annotatedElement => this.ComputeAnnotatedElement();

        /// <summary>
        /// The Annotations that relate this AnnotatingElement to its annotatedElements. This includes the
        /// owningAnnotatingRelationship (if any) followed by all the ownedAnnotatingRelationshps.
        /// </summary>
        [Property(xmiId: "Root-Annotations-AnnotatingElement-annotation", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Root-Elements-A_source_sourceRelationship-sourceRelationship")]
        [Implements(implementation: "IAnnotatingElement.Annotation")]
        public List<IAnnotation> annotation => this.ComputeAnnotation();

        /// <summary>
        /// The declared name of this Element.
        /// </summary>
        [Property(xmiId: "Root-Elements-Element-declaredName", aggregation: AggregationKind.None, lowerValue: 0, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [Implements(implementation: "IElement.DeclaredName")]
        public string DeclaredName { get; set; }

        /// <summary>
        /// An optional alternative name for the Element that is intended to be shorter or in some way more
        /// succinct than its primary name. It may act as a modeler-specified identifier for the Element, though
        /// it is then the responsibility of the modeler to maintain the uniqueness of this identifier within a
        /// model or relative to some other context.
        /// </summary>
        [Property(xmiId: "Root-Elements-Element-declaredShortName", aggregation: AggregationKind.None, lowerValue: 0, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [Implements(implementation: "IElement.DeclaredShortName")]
        public string DeclaredShortName { get; set; }

        /// <summary>
        /// The Documentation owned by this Element.
        /// </summary>
        [Property(xmiId: "Root-Elements-Element-documentation", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Root-Annotations-A_annotatedElement_annotatingElement-annotatingElement")]
        [SubsettedProperty(propertyName: "Root-Elements-Element-ownedElement")]
        [Implements(implementation: "IElement.Documentation")]
        public List<IDocumentation> documentation => this.ComputeDocumentation();

        /// <summary>
        /// The globally unique identifier for this Element. This is intended to be set by tooling, and it must
        /// not change during the lifetime of the Element.
        /// </summary>
        [Property(xmiId: "Root-Elements-Element-elementId", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [Implements(implementation: "IElement.ElementId")]
        public string ElementId { get; set; }

        /// <summary>
        /// Whether all necessary implied Relationships have been included in the ownedRelationships of this
        /// Element. This property may be true, even if there are not actually any ownedRelationships with
        /// isImplied = true, meaning that no such Relationships are actually implied for this Element. However,
        /// if it is false, then ownedRelationships may not contain any implied Relationships. That is, either
        /// all required implied Relationships must be included, or none of them.
        /// </summary>
        [Property(xmiId: "Root-Elements-Element-isImpliedIncluded", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: "false")]
        [Implements(implementation: "IElement.IsImpliedIncluded")]
        public bool IsImpliedIncluded { get; set; }

        /// <summary>
        /// Whether this Element is contained in the ownership tree of a library model.
        /// </summary>
        [Property(xmiId: "Root-Elements-Element-isLibraryElement", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [Implements(implementation: "IElement.IsLibraryElement")]
        public bool isLibraryElement => this.ComputeIsLibraryElement();

        /// <summary>
        /// The name to be used for this Element during name resolution within its owningNamespace. This is
        /// derived using the effectiveName() operation. By default, it is the same as the declaredName, but
        /// this is overridden for certain kinds of Elements to compute a name even when the declaredName is
        /// null.
        /// </summary>
        [Property(xmiId: "Root-Elements-Element-name", aggregation: AggregationKind.None, lowerValue: 0, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [Implements(implementation: "IElement.Name")]
        public string name => this.ComputeName();

        /// <summary>
        /// The ownedRelationships of this AnnotatingElement that are Annotations, for which this
        /// AnnotatingElement is the annotatingElement.
        /// </summary>
        [Property(xmiId: "Root-Annotations-AnnotatingElement-ownedAnnotatingRelationship", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Root-Annotations-AnnotatingElement-annotation")]
        [SubsettedProperty(propertyName: "Root-Elements-Element-ownedRelationship")]
        [Implements(implementation: "IAnnotatingElement.OwnedAnnotatingRelationship")]
        public List<IAnnotation> ownedAnnotatingRelationship => this.ComputeOwnedAnnotatingRelationship();

        /// <summary>
        /// The ownedRelationships of this Element that are Annotations, for which this Element is the
        /// annotatedElement.
        /// </summary>
        [Property(xmiId: "Root-Elements-Element-ownedAnnotation", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Root-Elements-Element-ownedRelationship")]
        [SubsettedProperty(propertyName: "Root-Annotations-A_annotatedElement_annotation-annotation")]
        [Implements(implementation: "IElement.OwnedAnnotation")]
        public List<IAnnotation> ownedAnnotation => this.ComputeOwnedAnnotation();

        /// <summary>
        /// The Elements owned by this Element, derived as the ownedRelatedElements of the ownedRelationships of
        /// this Element.
        /// </summary>
        [Property(xmiId: "Root-Elements-Element-ownedElement", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [Implements(implementation: "IElement.OwnedElement")]
        public List<IElement> ownedElement => this.ComputeOwnedElement();

        /// <summary>
        /// The Relationships for which this Element is the owningRelatedElement.
        /// </summary>
        [Property(xmiId: "Root-Elements-Element-ownedRelationship", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Root-Elements-A_relatedElement_relationship-relationship")]
        [Implements(implementation: "IElement.OwnedRelationship")]
        public List<IRelationship> OwnedRelationship { get; set; } = [];

        /// <summary>
        /// The owner of this Element, derived as the owningRelatedElement of the owningRelationship of this
        /// Element, if any.
        /// </summary>
        [Property(xmiId: "Root-Elements-Element-owner", aggregation: AggregationKind.None, lowerValue: 0, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [Implements(implementation: "IElement.Owner")]
        public IElement owner => this.ComputeOwner();

        /// <summary>
        /// The owningRelationship of this AnnotatingRelationship, if it is an Annotation
        /// </summary>
        [Property(xmiId: "Root-Annotations-AnnotatingElement-owningAnnotatingRelationship", aggregation: AggregationKind.None, lowerValue: 0, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Root-Elements-Element-owningRelationship")]
        [SubsettedProperty(propertyName: "Root-Annotations-AnnotatingElement-annotation")]
        [Implements(implementation: "IAnnotatingElement.OwningAnnotatingRelationship")]
        public IAnnotation owningAnnotatingRelationship => this.ComputeOwningAnnotatingRelationship();

        /// <summary>
        /// The owningRelationship of this Element, if that Relationship is a Membership.
        /// </summary>
        [Property(xmiId: "Root-Elements-Element-owningMembership", aggregation: AggregationKind.None, lowerValue: 0, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Root-Namespaces-A_memberElement_membership-membership")]
        [SubsettedProperty(propertyName: "Root-Elements-Element-owningRelationship")]
        [Implements(implementation: "IElement.OwningMembership")]
        public IOwningMembership owningMembership => this.ComputeOwningMembership();

        /// <summary>
        /// The Namespace that owns this Element, which is the membershipOwningNamespace of the owningMembership
        /// of this Element, if any.
        /// </summary>
        [Property(xmiId: "Root-Elements-Element-owningNamespace", aggregation: AggregationKind.None, lowerValue: 0, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Root-Namespaces-A_member_namespace-namespace")]
        [Implements(implementation: "IElement.OwningNamespace")]
        public INamespace owningNamespace => this.ComputeOwningNamespace();

        /// <summary>
        /// The Relationship for which this Element is an ownedRelatedElement, if any.
        /// </summary>
        [Property(xmiId: "Root-Elements-Element-owningRelationship", aggregation: AggregationKind.None, lowerValue: 0, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Root-Elements-A_relatedElement_relationship-relationship")]
        [Implements(implementation: "IElement.OwningRelationship")]
        public IRelationship OwningRelationship { get; set; }

        /// <summary>
        /// The full ownership-qualified name of this Element, represented in a form that is valid according to
        /// the KerML textual concrete syntax for qualified names (including use of unrestricted name notation
        /// and escaped characters, as necessary). The qualifiedName is null if this Element has no
        /// owningNamespace or if there is not a complete ownership chain of named Namespaces from a root
        /// Namespace to this Element. If the owningNamespace has other Elements with the same name as this one,
        /// then the qualifiedName is null for all such Elements other than the first.
        /// </summary>
        [Property(xmiId: "Root-Elements-Element-qualifiedName", aggregation: AggregationKind.None, lowerValue: 0, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [Implements(implementation: "IElement.QualifiedName")]
        public string qualifiedName => this.ComputeQualifiedName();

        /// <summary>
        /// The short name to be used for this Element during name resolution within its owningNamespace. This
        /// is derived using the effectiveShortName() operation. By default, it is the same as the
        /// declaredShortName, but this is overridden for certain kinds of Elements to compute a shortName even
        /// when the declaredName is null.
        /// </summary>
        [Property(xmiId: "Root-Elements-Element-shortName", aggregation: AggregationKind.None, lowerValue: 0, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [Implements(implementation: "IElement.ShortName")]
        public string shortName => this.ComputeShortName();

        /// <summary>
        /// The TextualRepresentations that annotate this Element.
        /// </summary>
        [Property(xmiId: "Root-Elements-Element-textualRepresentation", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Root-Annotations-A_annotatedElement_annotatingElement-annotatingElement")]
        [SubsettedProperty(propertyName: "Root-Elements-Element-ownedElement")]
        [Implements(implementation: "IElement.TextualRepresentation")]
        public List<ITextualRepresentation> textualRepresentation => this.ComputeTextualRepresentation();

    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
