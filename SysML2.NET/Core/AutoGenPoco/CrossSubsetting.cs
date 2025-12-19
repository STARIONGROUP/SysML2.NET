// -------------------------------------------------------------------------------------------------
// <copyright file="CrossSubsetting.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.Core.POCO.Core.Features
{
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.Generic;

    using SysML2.NET.Core.POCO.Core.Types;
    using SysML2.NET.Core.POCO.Root.Annotations;
    using SysML2.NET.Core.POCO.Root.Elements;
    using SysML2.NET.Core.POCO.Root.Namespaces;
    using SysML2.NET.Decorators;

    /// <summary>
    /// CrossSubsetting is a kind of Subsetting for end Features, as identified by crossingFeature, to
    /// subset a chained Feature, identified by crossedFeature. It navigates to instances of the end
    /// Feature’s type from instances of other end Feature types on the same owningType (at least two end
    /// Features are required for any of them to have a CrossSubsetting).The crossedFeature of a
    /// CrossSubsetting must have a feature chain of exactly two Features. The second Feature in the chain
    /// is the crossFeature of the crossingFeature (end Feature), which has the same type as the
    /// crossingFeature. When the owningType of the crossingFeature has exactly two end Features, the first
    /// Feature in the chain of the crossedFeature is the other end Feature. The crossFeature’s
    /// featuringType in this case is the other end Feature. When the owningType has more than two end
    /// Features, the first Feature in the chain is a Feature that CrossMultiplies all the other end
    /// Features, which is also the featuringType of the crossFeature.A crossFeature must be owned by its
    /// featureCrossing (end Feature) when the featureCrossing owningType has more than two end Features.
    /// Otherwise, for exactly two end Features, the crossFeatures of each the ends can instead optionally
    /// be inherited by the other end from one of its types or a subsetted Feature.
    /// </summary>
    [Class(xmiId: "_19_0_4_b9102da_1689616180239_998062_127", isAbstract: false, isFinalSpecialization: false, isActive: false)]
    [GeneratedCode("SysML2.NET", "latest")]
    public partial class CrossSubsetting : ICrossSubsetting
    {
        /// <summary>
        /// Various alternative identifiers for this Element. Generally, these will be set by tools.
        /// </summary>
        [Property(xmiId: "_19_0_2_12e503d9_1594312532679_496267_4310", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        public
         List<string> AliasIds
        { get; set; }

        /// <summary>
        /// The chained Feature that is cross subset by the crossingFeature of this CrossSubsetting.
        /// </summary>
        [Property(xmiId: "_19_0_4_b9102da_1689616524877_131585_248", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [RedefinedProperty(propertyName: "_18_5_3_12e503d9_1533160674961_393191_43181")]
        public
         IFeature CrossedFeature
        { get; set; }

        /// <summary>
        /// The end Feature that owns this CrossSubsetting relationship and is also its subsettingFeature.
        /// </summary>
        [Property(xmiId: "_19_0_4_b9102da_1689616916594_477020_278", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [RedefinedProperty(propertyName: "_18_5_3_12e503d9_1533160674987_236250_43311")]
        [RedefinedProperty(propertyName: "_18_5_3_12e503d9_1533160674967_140305_43206")]
        public
         IFeature QueryCrossingFeature()
        {
            throw new NotImplementedException("Derived property CrossingFeature not yet supported");
        }

        /// <summary>
        /// The declared name of this Element.
        /// </summary>
        [Property(xmiId: "_18_5_3_12e503d9_1533160674987_737648_43307", aggregation: AggregationKind.None, lowerValue: 0, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        public
         string DeclaredName
        { get; set; }

        /// <summary>
        /// An optional alternative name for the Element that is intended to be shorter or in some way more
        /// succinct than its primary name. It may act as a modeler-specified identifier for the Element, though
        /// it is then the responsibility of the modeler to maintain the uniqueness of this identifier within a
        /// model or relative to some other context.
        /// </summary>
        [Property(xmiId: "_19_0_2_12e503d9_1594160442439_915308_4153", aggregation: AggregationKind.None, lowerValue: 0, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        public
         string DeclaredShortName
        { get; set; }

        /// <summary>
        /// The Documentation owned by this Element.
        /// </summary>
        [Property(xmiId: "_19_0_2_12e503d9_1594150061166_345630_1621", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_19_0_2_12e503d9_1594145755059_76214_87")]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1543092869879_112608_17278")]
        public
         List<IDocumentation> QueryDocumentation()
        {
            throw new NotImplementedException("Derived property Documentation not yet supported");
        }

        /// <summary>
        /// The globally unique identifier for this Element. This is intended to be set by tooling, and it must
        /// not change during the lifetime of the Element.
        /// </summary>
        [Property(xmiId: "_18_5_3_12e503d9_1533160674986_844338_43305", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        public
         string ElementId
        { get; set; }

        /// <summary>
        /// A Type with a superset of all instances of the specific Type, which might be the same set.
        /// </summary>
        [Property(xmiId: "_18_5_3_12e503d9_1533160674980_563969_43273", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [RedefinedProperty(propertyName: "_18_5_3_12e503d9_1533160674961_138197_43179")]
        [RedefinedByProperty("ISubsetting.SubsettedFeature")]
        public
         IType General
        { get; set; }

        /// <summary>
        /// Whether this Relationship was generated by tooling to meet semantic rules, rather than being
        /// directly created by a modeler.
        /// </summary>
        [Property(xmiId: "_19_0_4_12e503d9_1662070829631_521257_3623", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: "false")]
        public
         bool IsImplied
        { get; set; }

        /// <summary>
        /// Whether all necessary implied Relationships have been included in the ownedRelationships of this
        /// Element. This property may be true, even if there are not actually any ownedRelationships with
        /// isImplied = true, meaning that no such Relationships are actually implied for this Element. However,
        /// if it is false, then ownedRelationships may not contain any implied Relationships. That is, either
        /// all required implied Relationships must be included, or none of them.
        /// </summary>
        [Property(xmiId: "_19_0_4_12e503d9_1662070949317_79713_3658", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: "false")]
        public
         bool IsImpliedIncluded
        { get; set; }

        /// <summary>
        /// Whether this Element is contained in the ownership tree of a library model.
        /// </summary>
        [Property(xmiId: "_19_0_4_12e503d9_1665443500960_5561_723", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        public
         bool QueryIsLibraryElement()
        {
            throw new NotImplementedException("Derived property IsLibraryElement not yet supported");
        }

        /// <summary>
        /// The name to be used for this Element during name resolution within its owningNamespace. This is
        /// derived using the effectiveName() operation. By default, it is the same as the declaredName, but
        /// this is overridden for certain kinds of Elements to compute a name even when the declaredName is
        /// null.
        /// </summary>
        [Property(xmiId: "_19_0_4_12e503d9_1617485009541_709355_27528", aggregation: AggregationKind.None, lowerValue: 0, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        public
         string QueryName()
        {
            throw new NotImplementedException("Derived property Name not yet supported");
        }

        /// <summary>
        /// The ownedRelationships of this Element that are Annotations, for which this Element is the
        /// annotatedElement.
        /// </summary>
        [Property(xmiId: "_19_0_2_12e503d9_1594152527165_702130_2500", aggregation: AggregationKind.Composite, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1543092026091_217766_16748")]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1543094430277_599480_18543")]
        public
         List<IAnnotation> QueryOwnedAnnotation()
        {
            throw new NotImplementedException("Derived property OwnedAnnotation not yet supported");
        }

        /// <summary>
        /// The Elements owned by this Element, derived as the ownedRelatedElements of the ownedRelationships of
        /// this Element.
        /// </summary>
        [Property(xmiId: "_18_5_3_12e503d9_1543092869879_112608_17278", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        public
         List<IElement> QueryOwnedElement()
        {
            throw new NotImplementedException("Derived property OwnedElement not yet supported");
        }

        /// <summary>
        /// The relatedElements of this Relationship that are owned by the Relationship.
        /// </summary>
        [Property(xmiId: "_18_5_3_12e503d9_1533160674986_59873_43302", aggregation: AggregationKind.Composite, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1533160674961_132339_43177")]
        public
         List<IElement> OwnedRelatedElement
        { get; set; }

        /// <summary>
        /// The Relationships for which this Element is the owningRelatedElement.
        /// </summary>
        [Property(xmiId: "_18_5_3_12e503d9_1543092026091_217766_16748", aggregation: AggregationKind.Composite, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1533160674961_585972_43176")]
        public
         List<IRelationship> OwnedRelationship
        { get; set; }

        /// <summary>
        /// The owner of this Element, derived as the owningRelatedElement of the owningRelationship of this
        /// Element, if any.
        /// </summary>
        [Property(xmiId: "_18_5_3_12e503d9_1543092869879_744477_17277", aggregation: AggregationKind.None, lowerValue: 0, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        public
         IElement QueryOwner()
        {
            throw new NotImplementedException("Derived property Owner not yet supported");
        }

        /// <summary>
        /// A subsettingFeature that is also the owningRelatedElement of this Subsetting.
        /// </summary>
        [Property(xmiId: "_18_5_3_12e503d9_1533160674987_236250_43311", aggregation: AggregationKind.None, lowerValue: 0, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1533160674967_140305_43206")]
        [RedefinedProperty(propertyName: "_18_5_3_12e503d9_1533160674971_573157_43226")]
        [RedefinedByProperty("ICrossSubsetting.CrossingFeature")]
        public
         IFeature QueryOwningFeature()
        {
            throw new NotImplementedException("Derived property OwningFeature not yet supported");
        }

        /// <summary>
        /// The owningRelationship of this Element, if that Relationship is a Membership.
        /// </summary>
        [Property(xmiId: "_18_5_3_12e503d9_1533160674972_622493_43236", aggregation: AggregationKind.None, lowerValue: 0, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1533160674973_469277_43243")]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1533160674986_482273_43303")]
        public
         IOwningMembership QueryOwningMembership()
        {
            throw new NotImplementedException("Derived property OwningMembership not yet supported");
        }

        /// <summary>
        /// The Namespace that owns this Element, which is the membershipOwningNamespace of the owningMembership
        /// of this Element, if any.
        /// </summary>
        [Property(xmiId: "_18_5_3_12e503d9_1533160674986_474739_43306", aggregation: AggregationKind.None, lowerValue: 0, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1533160674980_717955_43271")]
        public
         INamespace QueryOwningNamespace()
        {
            throw new NotImplementedException("Derived property OwningNamespace not yet supported");
        }

        /// <summary>
        /// The relatedElement of this Relationship that owns the Relationship, if any.
        /// </summary>
        [Property(xmiId: "_18_5_3_12e503d9_1543092026091_693018_16749", aggregation: AggregationKind.None, lowerValue: 0, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1533160674961_132339_43177")]
        public
         IElement OwningRelatedElement
        { get; set; }

        /// <summary>
        /// The Relationship for which this Element is an ownedRelatedElement, if any.
        /// </summary>
        [Property(xmiId: "_18_5_3_12e503d9_1533160674986_482273_43303", aggregation: AggregationKind.None, lowerValue: 0, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1533160674961_585972_43176")]
        public
         IRelationship OwningRelationship
        { get; set; }

        /// <summary>
        /// The Type that is the specific Type of this Specialization and owns it as its owningRelatedElement.
        /// </summary>
        [Property(xmiId: "_18_5_3_12e503d9_1533160674971_573157_43226", aggregation: AggregationKind.None, lowerValue: 0, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1543092026091_693018_16749")]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1533160674982_253967_43281")]
        [RedefinedByProperty("ISubsetting.OwningFeature")]
        public
         IType QueryOwningType()
        {
            throw new NotImplementedException("Derived property OwningType not yet supported");
        }

        /// <summary>
        /// The full ownership-qualified name of this Element, represented in a form that is valid according to
        /// the KerML textual concrete syntax for qualified names (including use of unrestricted name notation
        /// and escaped characters, as necessary). The qualifiedName is null if this Element has no
        /// owningNamespace or if there is not a complete ownership chain of named Namespaces from a root
        /// Namespace to this Element. If the owningNamespace has other Elements with the same name as this one,
        /// then the qualifiedName is null for all such Elements other than the first.
        /// </summary>
        [Property(xmiId: "_19_0_4_12e503d9_1611356604987_900871_594", aggregation: AggregationKind.None, lowerValue: 0, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        public
         string QueryQualifiedName()
        {
            throw new NotImplementedException("Derived property QualifiedName not yet supported");
        }

        /// <summary>
        /// The Elements that are related by this Relationship, derived as the union of the source and target
        /// Elements of the Relationship.
        /// </summary>
        [Property(xmiId: "_18_5_3_12e503d9_1533160674961_132339_43177", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: false, defaultValue: null)]
        public
         List<IElement> QueryRelatedElement()
        {
            throw new NotImplementedException("Derived property RelatedElement not yet supported");
        }

        /// <summary>
        /// The short name to be used for this Element during name resolution within its owningNamespace. This
        /// is derived using the effectiveShortName() operation. By default, it is the same as the
        /// declaredShortName, but this is overridden for certain kinds of Elements to compute a shortName even
        /// when the declaredName is null.
        /// </summary>
        [Property(xmiId: "_19_0_4_12e503d9_1673496405504_544235_24", aggregation: AggregationKind.None, lowerValue: 0, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        public
         string QueryShortName()
        {
            throw new NotImplementedException("Derived property ShortName not yet supported");
        }

        /// <summary>
        /// The relatedElements from which this Relationship is considered to be directed.
        /// </summary>
        [Property(xmiId: "_18_5_3_12e503d9_1533160674971_696758_43228", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1533160674961_132339_43177")]
        [RedefinedByProperty("ISpecialization.Specific")]
        public
         List<IElement> Source
        { get; set; }

        /// <summary>
        /// A Type with a subset of all instances of the general Type, which might be the same set.
        /// </summary>
        [Property(xmiId: "_18_5_3_12e503d9_1533160674982_253967_43281", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [RedefinedProperty(propertyName: "_18_5_3_12e503d9_1533160674971_696758_43228")]
        [RedefinedByProperty("ISubsetting.SubsettingFeature")]
        public
         IType Specific
        { get; set; }

        /// <summary>
        /// The Feature that is subsetted by the subsettingFeature of this Subsetting.
        /// </summary>
        [Property(xmiId: "_18_5_3_12e503d9_1533160674961_393191_43181", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [RedefinedProperty(propertyName: "_18_5_3_12e503d9_1533160674980_563969_43273")]
        [RedefinedByProperty("ICrossSubsetting.CrossedFeature")]
        public
         IFeature SubsettedFeature
        { get; set; }

        /// <summary>
        /// The Feature that is a subset of the subsettedFeature of this Subsetting.
        /// </summary>
        [Property(xmiId: "_18_5_3_12e503d9_1533160674967_140305_43206", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [RedefinedProperty(propertyName: "_18_5_3_12e503d9_1533160674982_253967_43281")]
        [RedefinedByProperty("ICrossSubsetting.CrossingFeature")]
        public
         IFeature SubsettingFeature
        { get; set; }

        /// <summary>
        /// The relatedElements to which this Relationship is considered to be directed.
        /// </summary>
        [Property(xmiId: "_18_5_3_12e503d9_1533160674961_138197_43179", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1533160674961_132339_43177")]
        [RedefinedByProperty("ISpecialization.General")]
        public
         List<IElement> Target
        { get; set; }

        /// <summary>
        /// The TextualRepresentations that annotate this Element.
        /// </summary>
        [Property(xmiId: "_19_0_2_12e503d9_1594154758493_640290_3388", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_19_0_2_12e503d9_1594145755059_76214_87")]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1543092869879_112608_17278")]
        public
         List<ITextualRepresentation> QueryTextualRepresentation()
        {
            throw new NotImplementedException("Derived property TextualRepresentation not yet supported");
        }

    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
