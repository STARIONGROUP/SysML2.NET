// -------------------------------------------------------------------------------------------------
// <copyright file="ConjugatedPortTyping.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.Core.POCO.Systems.Ports
{
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.Generic;
    using System.Linq;

    using SysML2.NET.Core.POCO.Core.Features;
    using SysML2.NET.Core.POCO.Core.Types;
    using SysML2.NET.Core.POCO.Root.Annotations;
    using SysML2.NET.Core.POCO.Root.Elements;
    using SysML2.NET.Core.POCO.Root.Namespaces;
    using SysML2.NET.Decorators;

    /// <summary>
    /// A ConjugatedPortTyping is a FeatureTyping whose type is a ConjugatedPortDefinition. (This
    /// relationship is intended to be an abstract-syntax marker for a special surface notation for
    /// conjugated typing of ports.)
    /// </summary>
    [Class(xmiId: "Systems-Ports-ConjugatedPortTyping", isAbstract: false, isFinalSpecialization: false, isActive: false)]
    [GeneratedCode("SysML2.NET", "latest")]
    public partial class ConjugatedPortTyping : IConjugatedPortTyping
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
        /// The type of this ConjugatedPortTyping considered as a FeatureTyping, which must be a
        /// ConjugatedPortDefinition.
        /// </summary>
        [Property(xmiId: "Systems-Ports-ConjugatedPortTyping-conjugatedPortDefinition", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [RedefinedProperty(propertyName: "Core-Features-FeatureTyping-type")]
        [Implements(implementation: "IConjugatedPortTyping.ConjugatedPortDefinition")]
        public IConjugatedPortDefinition ConjugatedPortDefinition { get; set; }

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
        /// A Type with a superset of all instances of the specific Type, which might be the same set.
        /// </summary>
        [Property(xmiId: "Core-Types-Specialization-general", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [RedefinedProperty(propertyName: "Root-Elements-Relationship-target")]
        [RedefinedByProperty("IFeatureTyping.Type")]
        [Implements(implementation: "ISpecialization.General")]
        IType Core.Types.ISpecialization.General
        {
            get => ((SysML2.NET.Core.POCO.Core.Features.IFeatureTyping)this).Type;
            set
            {
                ((SysML2.NET.Core.POCO.Core.Features.IFeatureTyping)this).Type = value;
            }
        }

        /// <summary>
        /// Whether this Relationship was generated by tooling to meet semantic rules, rather than being
        /// directly created by a modeler.
        /// </summary>
        [Property(xmiId: "Root-Elements-Relationship-isImplied", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: "false")]
        [Implements(implementation: "IRelationship.IsImplied")]
        public bool IsImplied { get; set; }

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
        /// The relatedElements of this Relationship that are owned by the Relationship.
        /// </summary>
        [Property(xmiId: "Root-Elements-Relationship-ownedRelatedElement", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Root-Elements-Relationship-relatedElement")]
        [Implements(implementation: "IRelationship.OwnedRelatedElement")]
        public List<IElement> OwnedRelatedElement { get; set; } = [];

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
        /// A typedFeature that is also the owningRelatedElement of this FeatureTyping.
        /// </summary>
        [Property(xmiId: "Core-Features-FeatureTyping-owningFeature", aggregation: AggregationKind.None, lowerValue: 0, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Core-Features-FeatureTyping-typedFeature")]
        [RedefinedProperty(propertyName: "Core-Types-Specialization-owningType")]
        [Implements(implementation: "IFeatureTyping.OwningFeature")]
        public IFeature owningFeature => this.ComputeOwningFeature();

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
        /// The relatedElement of this Relationship that owns the Relationship, if any.
        /// </summary>
        [Property(xmiId: "Root-Elements-Relationship-owningRelatedElement", aggregation: AggregationKind.None, lowerValue: 0, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Root-Elements-Relationship-relatedElement")]
        [Implements(implementation: "IRelationship.OwningRelatedElement")]
        public IElement OwningRelatedElement { get; set; }

        /// <summary>
        /// The Relationship for which this Element is an ownedRelatedElement, if any.
        /// </summary>
        [Property(xmiId: "Root-Elements-Element-owningRelationship", aggregation: AggregationKind.None, lowerValue: 0, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Root-Elements-A_relatedElement_relationship-relationship")]
        [Implements(implementation: "IElement.OwningRelationship")]
        public IRelationship OwningRelationship { get; set; }

        /// <summary>
        /// The Type that is the specific Type of this Specialization and owns it as its owningRelatedElement.
        /// </summary>
        [Property(xmiId: "Core-Types-Specialization-owningType", aggregation: AggregationKind.None, lowerValue: 0, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Root-Elements-Relationship-owningRelatedElement")]
        [SubsettedProperty(propertyName: "Core-Types-Specialization-specific")]
        [RedefinedByProperty("IFeatureTyping.OwningFeature")]
        [Implements(implementation: "ISpecialization.OwningType")]
        IType Core.Types.ISpecialization.owningType => this.owningFeature;

        /// <summary>
        /// The originalPortDefinition of the conjugatedPortDefinition of this ConjugatedPortTyping.
        /// </summary>
        [Property(xmiId: "Systems-Ports-ConjugatedPortTyping-portDefinition", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [Implements(implementation: "IConjugatedPortTyping.PortDefinition")]
        public IPortDefinition portDefinition => this.ComputePortDefinition();

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
        /// The Elements that are related by this Relationship, derived as the union of the source and target
        /// Elements of the Relationship.
        /// </summary>
        [Property(xmiId: "Root-Elements-Relationship-relatedElement", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: false, defaultValue: null)]
        [Implements(implementation: "IRelationship.RelatedElement")]
        public List<IElement> relatedElement => this.ComputeRelatedElement();

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
        /// The relatedElements from which this Relationship is considered to be directed.
        /// </summary>
        [Property(xmiId: "Root-Elements-Relationship-source", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Root-Elements-Relationship-relatedElement")]
        [RedefinedByProperty("ISpecialization.Specific")]
        [Implements(implementation: "IRelationship.Source")]
        List<IElement> Root.Elements.IRelationship.Source
        {
            get => ((SysML2.NET.Core.POCO.Core.Types.ISpecialization)this).Specific != null ? [((SysML2.NET.Core.POCO.Core.Types.ISpecialization)this).Specific] : [];
            set
            {
                if (value.OfType<IType>().FirstOrDefault() is { } firstValue)
                {
                    ((SysML2.NET.Core.POCO.Core.Types.ISpecialization)this).Specific = firstValue;
                }
            }
        }

        /// <summary>
        /// A Type with a subset of all instances of the general Type, which might be the same set.
        /// </summary>
        [Property(xmiId: "Core-Types-Specialization-specific", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [RedefinedProperty(propertyName: "Root-Elements-Relationship-source")]
        [RedefinedByProperty("IFeatureTyping.TypedFeature")]
        [Implements(implementation: "ISpecialization.Specific")]
        IType Core.Types.ISpecialization.Specific
        {
            get => this.TypedFeature;
            set
            {
                if (value is IFeature castedValue)
                {
                    this.TypedFeature = castedValue;
                }
            }
        }

        /// <summary>
        /// The relatedElements to which this Relationship is considered to be directed.
        /// </summary>
        [Property(xmiId: "Root-Elements-Relationship-target", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Root-Elements-Relationship-relatedElement")]
        [RedefinedByProperty("ISpecialization.General")]
        [Implements(implementation: "IRelationship.Target")]
        List<IElement> Root.Elements.IRelationship.Target
        {
            get => ((SysML2.NET.Core.POCO.Core.Types.ISpecialization)this).General != null ? [((SysML2.NET.Core.POCO.Core.Types.ISpecialization)this).General] : [];
            set
            {
                if (value.OfType<IType>().FirstOrDefault() is { } firstValue)
                {
                    ((SysML2.NET.Core.POCO.Core.Types.ISpecialization)this).General = firstValue;
                }
            }
        }

        /// <summary>
        /// The TextualRepresentations that annotate this Element.
        /// </summary>
        [Property(xmiId: "Root-Elements-Element-textualRepresentation", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Root-Annotations-A_annotatedElement_annotatingElement-annotatingElement")]
        [SubsettedProperty(propertyName: "Root-Elements-Element-ownedElement")]
        [Implements(implementation: "IElement.TextualRepresentation")]
        public List<ITextualRepresentation> textualRepresentation => this.ComputeTextualRepresentation();

        /// <summary>
        /// The Type that is being applied by this FeatureTyping.
        /// </summary>
        [Property(xmiId: "Core-Features-FeatureTyping-type", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [RedefinedProperty(propertyName: "Core-Types-Specialization-general")]
        [RedefinedByProperty("IConjugatedPortTyping.ConjugatedPortDefinition")]
        [Implements(implementation: "IFeatureTyping.Type")]
        IType Core.Features.IFeatureTyping.Type
        {
            get => this.ConjugatedPortDefinition;
            set
            {
                if (value is IConjugatedPortDefinition castedValue)
                {
                    this.ConjugatedPortDefinition = castedValue;
                }
            }
        }

        /// <summary>
        /// The Feature that has a type determined by this FeatureTyping.
        /// </summary>
        [Property(xmiId: "Core-Features-FeatureTyping-typedFeature", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [RedefinedProperty(propertyName: "Core-Types-Specialization-specific")]
        [Implements(implementation: "IFeatureTyping.TypedFeature")]
        public IFeature TypedFeature { get; set; }

    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
