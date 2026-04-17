// -------------------------------------------------------------------------------------------------
// <copyright file="Disjoining.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.Core.POCO.Core.Types
{
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.Generic;
    using System.Linq;

    using SysML2.NET.Core.POCO.Root.Annotations;
    using SysML2.NET.Core.POCO.Root.Elements;
    using SysML2.NET.Core.POCO.Root.Namespaces;
    using SysML2.NET.Collections;
    using SysML2.NET.Decorators;

    /// <summary>
    /// A Disjoining is a Relationship between Types asserted to have interpretations that are not shared
    /// (disjoint) between them, identified as typeDisjoined and disjoiningType. For example, a Classifier
    /// for mammals is disjoint from a Classifier for minerals, and a Feature for people&#39;s parents is
    /// disjoint from a Feature for their children.
    /// </summary>
    [Class(xmiId: "_19_0_4_b9102da_1623182941809_239395_557", isAbstract: false, isFinalSpecialization: false, isActive: false)]
    [GeneratedCode("SysML2.NET", "latest")]
    public partial class Disjoining : IDisjoining, IContainedRelationship
    {
        /// <summary>
        /// Initialize a new instance of <see cref="Disjoining" />
        /// </summary>
        public Disjoining()
        {
            ((IContainedRelationship)this).OwnedRelatedElement = new ContainerList<IRelationship, IElement>(this,
            (element, relationship) => ((IContainedElement)element).OwningRelationship = relationship,
            element => element.OwningRelationship);

            ((IContainedElement)this).OwnedRelationship = new ContainerList<IElement, IRelationship>(this,
            (relationship, element) => ((IContainedRelationship)relationship).OwningRelatedElement = element,
            relationship => relationship.OwningRelatedElement);

        }

        /// <summary>
        /// Gets or sets the unique identifier
        /// </summary>
        [Property(xmiId: "sysml2.net", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [Implements(implementation: "IData.Id")]
        public Guid Id { get; set; }

        /// <summary>
        /// Various alternative identifiers for this Element. Generally, these will be set by tools.
        /// </summary>
        [Property(xmiId: "_19_0_2_12e503d9_1594312532679_496267_4310", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [Implements(implementation: "IElement.AliasIds")]
        public List<string> AliasIds { get; set; } = [];

        /// <summary>
        /// The declared name of this Element.
        /// </summary>
        [Property(xmiId: "_18_5_3_12e503d9_1533160674987_737648_43307", aggregation: AggregationKind.None, lowerValue: 0, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [Implements(implementation: "IElement.DeclaredName")]
        public string DeclaredName { get; set; }

        /// <summary>
        /// An optional alternative name for the Element that is intended to be shorter or in some way more
        /// succinct than its primary name. It may act as a modeler-specified identifier for the Element, though
        /// it is then the responsibility of the modeler to maintain the uniqueness of this identifier within a
        /// model or relative to some other context.
        /// </summary>
        [Property(xmiId: "_19_0_2_12e503d9_1594160442439_915308_4153", aggregation: AggregationKind.None, lowerValue: 0, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [Implements(implementation: "IElement.DeclaredShortName")]
        public string DeclaredShortName { get; set; }

        /// <summary>
        /// Type asserted to be disjoint with the typeDisjoined.
        /// </summary>
        [Property(xmiId: "_19_0_4_b9102da_1623183201866_537160_629", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [RedefinedProperty(propertyName: "_18_5_3_12e503d9_1533160674961_138197_43179")]
        [Implements(implementation: "IDisjoining.DisjoiningType")]
        public IType DisjoiningType { get; set; }

        /// <summary>
        /// The Documentation owned by this Element.
        /// </summary>
        [Property(xmiId: "_19_0_2_12e503d9_1594150061166_345630_1621", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_19_0_2_12e503d9_1594145755059_76214_87")]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1543092869879_112608_17278")]
        [Implements(implementation: "IElement.Documentation")]
        public List<IDocumentation> documentation => this.ComputeDocumentation();

        /// <summary>
        /// The globally unique identifier for this Element. This is intended to be set by tooling, and it must
        /// not change during the lifetime of the Element.
        /// </summary>
        [Property(xmiId: "_18_5_3_12e503d9_1533160674986_844338_43305", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [Implements(implementation: "IElement.ElementId")]
        public string ElementId { get; set; }

        /// <summary>
        /// Whether this Relationship was generated by tooling to meet semantic rules, rather than being
        /// directly created by a modeler.
        /// </summary>
        [Property(xmiId: "_19_0_4_12e503d9_1662070829631_521257_3623", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: "false")]
        [Implements(implementation: "IRelationship.IsImplied")]
        public bool IsImplied { get; set; }

        /// <summary>
        /// Whether all necessary implied Relationships have been included in the ownedRelationships of this
        /// Element. This property may be true, even if there are not actually any ownedRelationships with
        /// isImplied = true, meaning that no such Relationships are actually implied for this Element. However,
        /// if it is false, then ownedRelationships may not contain any implied Relationships. That is, either
        /// all required implied Relationships must be included, or none of them.
        /// </summary>
        [Property(xmiId: "_19_0_4_12e503d9_1662070949317_79713_3658", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: "false")]
        [Implements(implementation: "IElement.IsImpliedIncluded")]
        public bool IsImpliedIncluded { get; set; }

        /// <summary>
        /// Whether this Element is contained in the ownership tree of a library model.
        /// </summary>
        [Property(xmiId: "_19_0_4_12e503d9_1665443500960_5561_723", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [Implements(implementation: "IElement.IsLibraryElement")]
        public bool isLibraryElement => this.ComputeIsLibraryElement();

        /// <summary>
        /// The name to be used for this Element during name resolution within its owningNamespace. This is
        /// derived using the effectiveName() operation. By default, it is the same as the declaredName, but
        /// this is overridden for certain kinds of Elements to compute a name even when the declaredName is
        /// null.
        /// </summary>
        [Property(xmiId: "_19_0_4_12e503d9_1617485009541_709355_27528", aggregation: AggregationKind.None, lowerValue: 0, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [Implements(implementation: "IElement.Name")]
        public string name => this.ComputeName();

        /// <summary>
        /// The ownedRelationships of this Element that are Annotations, for which this Element is the
        /// annotatedElement.
        /// </summary>
        [Property(xmiId: "_19_0_2_12e503d9_1594152527165_702130_2500", aggregation: AggregationKind.Composite, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1543092026091_217766_16748")]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1543094430277_599480_18543")]
        [Implements(implementation: "IElement.OwnedAnnotation")]
        public List<IAnnotation> ownedAnnotation => this.ComputeOwnedAnnotation();

        /// <summary>
        /// The Elements owned by this Element, derived as the ownedRelatedElements of the ownedRelationships of
        /// this Element.
        /// </summary>
        [Property(xmiId: "_18_5_3_12e503d9_1543092869879_112608_17278", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [Implements(implementation: "IElement.OwnedElement")]
        public List<IElement> ownedElement => this.ComputeOwnedElement();

        /// <summary>
        /// The relatedElements of this Relationship that are owned by the Relationship.
        /// </summary>
        [Property(xmiId: "_18_5_3_12e503d9_1533160674986_59873_43302", aggregation: AggregationKind.Composite, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1533160674961_132339_43177")]
        [Implements(implementation: "IRelationship.OwnedRelatedElement")]
        public IReadOnlyList<IElement> OwnedRelatedElement => ((IContainedRelationship)this).OwnedRelatedElement;

        /// <summary>Backing field for IRelationship.OwnedRelatedElement</summary>
        ContainerList<IRelationship, IElement> IContainedRelationship.OwnedRelatedElement { get; set; }

        /// <summary>
        /// The Relationships for which this Element is the owningRelatedElement.
        /// </summary>
        [Property(xmiId: "_18_5_3_12e503d9_1543092026091_217766_16748", aggregation: AggregationKind.Composite, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1533160674961_585972_43176")]
        [Implements(implementation: "IElement.OwnedRelationship")]
        public IReadOnlyList<IRelationship> OwnedRelationship => ((IContainedElement)this).OwnedRelationship;

        /// <summary>Backing field for IElement.OwnedRelationship</summary>
        ContainerList<IElement, IRelationship> IContainedElement.OwnedRelationship { get; set; }

        /// <summary>
        /// The owner of this Element, derived as the owningRelatedElement of the owningRelationship of this
        /// Element, if any.
        /// </summary>
        [Property(xmiId: "_18_5_3_12e503d9_1543092869879_744477_17277", aggregation: AggregationKind.None, lowerValue: 0, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [Implements(implementation: "IElement.Owner")]
        public IElement owner => this.ComputeOwner();

        /// <summary>
        /// The owningRelationship of this Element, if that Relationship is a Membership.
        /// </summary>
        [Property(xmiId: "_18_5_3_12e503d9_1533160674972_622493_43236", aggregation: AggregationKind.None, lowerValue: 0, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1533160674973_469277_43243")]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1533160674986_482273_43303")]
        [Implements(implementation: "IElement.OwningMembership")]
        public IOwningMembership owningMembership => this.ComputeOwningMembership();

        /// <summary>
        /// The Namespace that owns this Element, which is the membershipOwningNamespace of the owningMembership
        /// of this Element, if any.
        /// </summary>
        [Property(xmiId: "_18_5_3_12e503d9_1533160674986_474739_43306", aggregation: AggregationKind.None, lowerValue: 0, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1533160674980_717955_43271")]
        [Implements(implementation: "IElement.OwningNamespace")]
        public INamespace owningNamespace => this.ComputeOwningNamespace();

        /// <summary>
        /// The relatedElement of this Relationship that owns the Relationship, if any.
        /// </summary>
        [Property(xmiId: "_18_5_3_12e503d9_1543092026091_693018_16749", aggregation: AggregationKind.None, lowerValue: 0, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1533160674961_132339_43177")]
        [Implements(implementation: "IRelationship.OwningRelatedElement")]
        public IElement OwningRelatedElement => ((IContainedRelationship)this).OwningRelatedElement;

        /// <summary>Backing field for IRelationship.OwningRelatedElement</summary>
        IElement IContainedRelationship.OwningRelatedElement { get; set; }

        /// <summary>
        /// The Relationship for which this Element is an ownedRelatedElement, if any.
        /// </summary>
        [Property(xmiId: "_18_5_3_12e503d9_1533160674986_482273_43303", aggregation: AggregationKind.None, lowerValue: 0, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1533160674961_585972_43176")]
        [Implements(implementation: "IElement.OwningRelationship")]
        public IRelationship OwningRelationship => ((IContainedElement)this).OwningRelationship;

        /// <summary>Backing field for IElement.OwningRelationship</summary>
        IRelationship IContainedElement.OwningRelationship { get; set; }

        /// <summary>
        /// A typeDisjoined that is also an owningRelatedElement.
        /// </summary>
        [Property(xmiId: "_19_0_4_12e503d9_1627447519614_499771_371", aggregation: AggregationKind.None, lowerValue: 0, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1543092026091_693018_16749")]
        [SubsettedProperty(propertyName: "_19_0_4_b9102da_1623183194914_955906_617")]
        [Implements(implementation: "IDisjoining.OwningType")]
        public IType owningType => this.ComputeOwningType();

        /// <summary>
        /// The full ownership-qualified name of this Element, represented in a form that is valid according to
        /// the KerML textual concrete syntax for qualified names (including use of unrestricted name notation
        /// and escaped characters, as necessary). The qualifiedName is null if this Element has no
        /// owningNamespace or if there is not a complete ownership chain of named Namespaces from a root
        /// Namespace to this Element. If the owningNamespace has other Elements with the same name as this one,
        /// then the qualifiedName is null for all such Elements other than the first.
        /// </summary>
        [Property(xmiId: "_19_0_4_12e503d9_1611356604987_900871_594", aggregation: AggregationKind.None, lowerValue: 0, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [Implements(implementation: "IElement.QualifiedName")]
        public string qualifiedName => this.ComputeQualifiedName();

        /// <summary>
        /// The Elements that are related by this Relationship, derived as the union of the source and target
        /// Elements of the Relationship.
        /// </summary>
        [Property(xmiId: "_18_5_3_12e503d9_1533160674961_132339_43177", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: false, defaultValue: null)]
        [Implements(implementation: "IRelationship.RelatedElement")]
        public List<IElement> relatedElement => this.ComputeRelatedElement();

        /// <summary>
        /// The short name to be used for this Element during name resolution within its owningNamespace. This
        /// is derived using the effectiveShortName() operation. By default, it is the same as the
        /// declaredShortName, but this is overridden for certain kinds of Elements to compute a shortName even
        /// when the declaredName is null.
        /// </summary>
        [Property(xmiId: "_19_0_4_12e503d9_1673496405504_544235_24", aggregation: AggregationKind.None, lowerValue: 0, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [Implements(implementation: "IElement.ShortName")]
        public string shortName => this.ComputeShortName();

        /// <summary>
        /// The relatedElements from which this Relationship is considered to be directed.
        /// </summary>
        [Property(xmiId: "_18_5_3_12e503d9_1533160674971_696758_43228", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1533160674961_132339_43177")]
        [RedefinedByProperty("IDisjoining.TypeDisjoined")]
        [Implements(implementation: "IRelationship.Source")]
        List<IElement> Root.Elements.IRelationship.Source
        {
            get => this.TypeDisjoined != null ? [this.TypeDisjoined] : [];
            set
            {
                if (value.OfType<IType>().FirstOrDefault() is { } firstValue)
                {
                    this.TypeDisjoined = firstValue;
                }
            }
        }

        /// <summary>
        /// The relatedElements to which this Relationship is considered to be directed.
        /// </summary>
        [Property(xmiId: "_18_5_3_12e503d9_1533160674961_138197_43179", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1533160674961_132339_43177")]
        [RedefinedByProperty("IDisjoining.DisjoiningType")]
        [Implements(implementation: "IRelationship.Target")]
        List<IElement> Root.Elements.IRelationship.Target
        {
            get => this.DisjoiningType != null ? [this.DisjoiningType] : [];
            set
            {
                if (value.OfType<IType>().FirstOrDefault() is { } firstValue)
                {
                    this.DisjoiningType = firstValue;
                }
            }
        }

        /// <summary>
        /// The TextualRepresentations that annotate this Element.
        /// </summary>
        [Property(xmiId: "_19_0_2_12e503d9_1594154758493_640290_3388", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_19_0_2_12e503d9_1594145755059_76214_87")]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1543092869879_112608_17278")]
        [Implements(implementation: "IElement.TextualRepresentation")]
        public List<ITextualRepresentation> textualRepresentation => this.ComputeTextualRepresentation();

        /// <summary>
        /// Type asserted to be disjoint with the disjoiningType.
        /// </summary>
        [Property(xmiId: "_19_0_4_b9102da_1623183194914_955906_617", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [RedefinedProperty(propertyName: "_18_5_3_12e503d9_1533160674971_696758_43228")]
        [Implements(implementation: "IDisjoining.TypeDisjoined")]
        public IType TypeDisjoined { get; set; }


        /// <summary>
        /// Return an effective name for this Element. By default this is the same as its declaredName.
        /// </summary>
        /// <returns>
        /// The expected <see cref="string" />
        /// </returns>
        public string EffectiveName() => this.ComputeEffectiveNameOperation();

        /// <summary>
        /// Return an effective shortName for this Element. By default this is the same as its
        /// declaredShortName.
        /// </summary>
        /// <returns>
        /// The expected <see cref="string" />
        /// </returns>
        public string EffectiveShortName() => this.ComputeEffectiveShortNameOperation();

        /// <summary>
        /// Return name, if that is not null, otherwise the shortName, if that is not null, otherwise null. If
        /// the returned value is non-null, it is returned as-is if it has the form of a basic name, or,
        /// otherwise, represented as a restricted name according to the lexical structure of the KerML textual
        /// notation (i.e., surrounded by single quote characters and with special characters escaped).
        /// </summary>
        /// <returns>
        /// The expected <see cref="string" />
        /// </returns>
        public string EscapedName() => this.ComputeEscapedNameOperation();

        /// <summary>
        /// Return whether this Relationship has either an owningRelatedElement or owningRelationship that is a
        /// library element.
        /// </summary>
        /// <returns>
        /// The expected <see cref="INamespace" />
        /// </returns>
        public INamespace LibraryNamespace() => this.ComputeRedefinedLibraryNamespaceOperation();

        /// <summary>
        /// By default, return the library Namespace of the owningRelationship of this Element, if it has one.
        /// </summary>
        /// <returns>
        /// The expected <see cref="INamespace" />
        /// </returns>
        INamespace IElement.LibraryNamespace() => this.LibraryNamespace();

        /// <summary>
        /// If the owningRelationship of the Relationship is null but its owningRelatedElement is non-null,
        /// construct the path using the position of the Relationship in the list of ownedRelationships of its
        /// owningRelatedElement. Otherwise, return the path of the Relationship as specified for an Element in
        /// general.
        /// </summary>
        /// <returns>
        /// The expected <see cref="string" />
        /// </returns>
        public string Path() => this.ComputeRedefinedPathOperation();

        /// <summary>
        /// Return a unique description of the location of this Element in the containment structure rooted in a
        /// root Namespace. If the Element has a non-null qualifiedName, then return that. Otherwise, if it has
        /// an owningRelationship, then return the string constructed by appending to the path of it's
        /// owningRelationship the character / followed by the string representation of its position in the list
        /// of ownedRelatedElements of the owningRelationship (indexed starting at 1). Otherwise, return the
        /// empty string.                            (Note that this operation is overridden for Relationships
        /// to use owningRelatedElement when appropriate.)
        /// </summary>
        /// <returns>
        /// The expected <see cref="string" />
        /// </returns>
        string IElement.Path() => this.Path();
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
