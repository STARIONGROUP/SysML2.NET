// -------------------------------------------------------------------------------------------------
// <copyright file="IUsage.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.Core.DTO.Systems.DefinitionAndUsage
{
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.Generic;

    using SysML2.NET.Core.Core.Types;
    using SysML2.NET.Core.DTO.Core.Features;
    using SysML2.NET.Decorators;

    /// <summary>
    /// A Usage is a usage of a Definition.A Usage may have nestedUsages that model features that apply in
    /// the context of the owningUsage. A Usage may also have Definitions nested in it, but this has no
    /// semantic significance, other than the nested scoping resulting from the Usage being considered as a
    /// Namespace for any nested Definitions.However, if a Usage has isVariation = true, then it represents
    /// a variation point Usage. In this case, all of its members must be variant Usages, related to the
    /// Usage by VariantMembership Relationships. Rather than being features of the Usage, variant Usages
    /// model different concrete alternatives that can be chosen to fill in for the variation point Usage.
    /// </summary>
    [Class(xmiId: "_18_5_3_12e503d9_1565469997820_598571_19982", isAbstract: false, isFinalSpecialization: false, isActive: false)]
    [GeneratedCode("SysML2.NET", "latest")]
    public partial interface IUsage : IFeature
    {
        /// <summary>
        /// The Classifiers that are the types of this Usage. Nominally, these are Definitions, but other kinds
        /// of Kernel Classifiers are also allowed, to permit use of Classifiers from the Kernel Model
        /// Libraries.
        /// </summary>
        [Property(xmiId: "_19_0_2_12e503d9_1591477641252_179221_958", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [RedefinedProperty(propertyName: "_18_5_3_12e503d9_1533160674969_376003_43216")]
        List<Guid> Definition { get; }

        /// <summary>
        /// The usages of this Usage that are directedFeatures.
        /// </summary>
        [Property(xmiId: "_19_0_2_12e503d9_1591217699198_66279_508", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_19_0_4_12e503d9_1623952188842_882068_37169")]
        [SubsettedProperty(propertyName: "_19_0_2_12e503d9_1591217543254_26688_475")]
        List<Guid> DirectedUsage { get; }

        /// <summary>
        /// Whether this Usage is a referential Usage, that is, it has isComposite = false.
        /// </summary>
        [Property(xmiId: "_19_0_4_12e503d9_1624035114787_488767_41423", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        bool IsReference { get; }

        /// <summary>
        /// Whether this Usage is for a variation point or not. If true, then all the memberships of the Usage
        /// must be VariantMemberships.
        /// </summary>
        [Property(xmiId: "_19_0_2_12e503d9_1590978312364_290951_421", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        bool IsVariation { get; set; }

        /// <summary>
        /// Whether this Usage may be time varying (that is, whether it is featured by the snapshots of its
        /// owningType, rather than being featured by the owningType itself). However, if isConstant is also
        /// true, then the value of the Usage is nevertheless constant over the entire duration of an instance
        /// of its owningType (that is, it has the same value on all snapshots).The property mayTimeVary
        /// redefines the KerML property Feature::isVariable, making it derived. The property isConstant is
        /// inherited from Feature.
        /// </summary>
        [Property(xmiId: "_2022x_2_12e503d9_1737227200362_771035_69", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [RedefinedProperty(propertyName: "_2022x_2_12e503d9_1725998273002_23711_212")]
        bool MayTimeVary { get; }

        /// <summary>
        /// The ActionUsages that are nestedUsages of this Usage.
        /// </summary>
        [Property(xmiId: "_18_5_3_12e503d9_1565501745142_70952_31609", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_19_0_4_12e503d9_1618943853976_48759_278")]
        List<Guid> NestedAction { get; }

        /// <summary>
        /// The AllocationUsages that are nestedUsages of this Usage.
        /// </summary>
        [Property(xmiId: "_19_0_4_12e503d9_1611430983774_648557_1053", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_19_0_2_12e503d9_1591479754895_422988_1242")]
        List<Guid> NestedAllocation { get; }

        /// <summary>
        /// The AnalysisCaseUsages that are nestedUsages of this Usage.
        /// </summary>
        [Property(xmiId: "_19_0_2_12e503d9_1591152666850_226358_3749", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_19_0_2_12e503d9_1591135021853_494751_737")]
        List<Guid> NestedAnalysisCase { get; }

        /// <summary>
        /// The code>AttributeUsages that are nestedUsages of this Usage.
        /// </summary>
        [Property(xmiId: "_19_0_2_12e503d9_1591500785349_111324_4486", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1565472757327_162097_21259")]
        List<Guid> NestedAttribute { get; }

        /// <summary>
        /// The CalculationUsage that are nestedUsages of this Usage.
        /// </summary>
        [Property(xmiId: "_19_0_2_12e503d9_1588215112283_215964_632", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1565501745142_70952_31609")]
        List<Guid> NestedCalculation { get; }

        /// <summary>
        /// The CaseUsages that are nestedUsages of this Usage.
        /// </summary>
        [Property(xmiId: "_19_0_2_12e503d9_1591135021853_494751_737", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_19_0_2_12e503d9_1588215112283_215964_632")]
        List<Guid> NestedCase { get; }

        /// <summary>
        /// The ConcernUsages that are nestedUsages of this Usage.
        /// </summary>
        [Property(xmiId: "_19_0_4_12e503d9_1617051711833_106553_1460", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_19_0_2_12e503d9_1583000447195_878123_1244")]
        List<Guid> NestedConcern { get; }

        /// <summary>
        /// The ConnectorAsUsages that are nestedUsages of this Usage. Note that this list includes
        /// BindingConnectorAsUsages, SuccessionAsUsages, and FlowConnectionUsages because these are
        /// ConnectorAsUsages even though they are not ConnectionUsages.
        /// </summary>
        [Property(xmiId: "_19_0_2_12e503d9_1591479754895_422988_1242", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1565472757327_162097_21259")]
        List<Guid> NestedConnection { get; }

        /// <summary>
        /// The ConstraintUsages that are nestedUsages of this Usage.
        /// </summary>
        [Property(xmiId: "_19_0_2_12e503d9_1578067664051_434365_1774", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_19_0_4_12e503d9_1618943853976_48759_278")]
        List<Guid> NestedConstraint { get; }

        /// <summary>
        /// The code>EnumerationUsages that are nestedUsages of this Usage.
        /// </summary>
        [Property(xmiId: "_19_0_4_12e503d9_1606946589000_158124_239", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_19_0_2_12e503d9_1591500785349_111324_4486")]
        List<Guid> NestedEnumeration { get; }

        /// <summary>
        /// The code>FlowUsages that are nestedUsages of this Usage.
        /// </summary>
        [Property(xmiId: "_19_0_4_12e503d9_1624054938718_124518_1464", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_19_0_2_12e503d9_1591479754895_422988_1242")]
        List<Guid> NestedFlow { get; }

        /// <summary>
        /// The InterfaceUsages that are nestedUsages of this Usage.
        /// </summary>
        [Property(xmiId: "_19_0_2_12e503d9_1591498454569_383419_3839", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_19_0_2_12e503d9_1591479754895_422988_1242")]
        List<Guid> NestedInterface { get; }

        /// <summary>
        /// The ItemUsages that are nestedUsages of this Usage.
        /// </summary>
        [Property(xmiId: "_19_0_2_12e503d9_1591482421103_284620_2978", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_19_0_4_12e503d9_1618943853976_48759_278")]
        List<Guid> NestedItem { get; }

        /// <summary>
        /// The MetadataUsages that are nestedUsages of this of this Usage.
        /// </summary>
        [Property(xmiId: "_19_0_4_12e503d9_1661488589862_120785_2970", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_19_0_2_12e503d9_1591482421103_284620_2978")]
        List<Guid> NestedMetadata { get; }

        /// <summary>
        /// The OccurrenceUsages that are nestedUsages of this Usage.
        /// </summary>
        [Property(xmiId: "_19_0_4_12e503d9_1618943853976_48759_278", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1565472757327_162097_21259")]
        List<Guid> NestedOccurrence { get; }

        /// <summary>
        /// The PartUsages that are nestedUsages of this Usage.
        /// </summary>
        [Property(xmiId: "_19_0_2_12e503d9_1591496406876_479979_3188", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_19_0_2_12e503d9_1591482421103_284620_2978")]
        List<Guid> NestedPart { get; }

        /// <summary>
        /// The PortUsages that are nestedUsages of this Usage.
        /// </summary>
        [Property(xmiId: "_18_5_3_12e503d9_1565494459494_859367_26042", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1565472757327_162097_21259")]
        List<Guid> NestedPort { get; }

        /// <summary>
        /// The ReferenceUsages that are nestedUsages of this Usage.
        /// </summary>
        [Property(xmiId: "_19_0_2_12e503d9_1591477541360_47573_933", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1565472757327_162097_21259")]
        List<Guid> NestedReference { get; }

        /// <summary>
        /// The RenderingUsages that are nestedUsages of this Usage.
        /// </summary>
        [Property(xmiId: "_19_0_2_12e503d9_1596741501454_147708_6545", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_19_0_2_12e503d9_1591496406876_479979_3188")]
        List<Guid> NestedRendering { get; }

        /// <summary>
        /// The RequirementUsages that are nestedUsages of this Usage.
        /// </summary>
        [Property(xmiId: "_19_0_2_12e503d9_1583000447195_878123_1244", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_19_0_2_12e503d9_1578067664051_434365_1774")]
        List<Guid> NestedRequirement { get; }

        /// <summary>
        /// The StateUsages that are nestedUsages of this Usage.
        /// </summary>
        [Property(xmiId: "_19_0_2_12e503d9_1575587743891_973819_756", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1565501745142_70952_31609")]
        List<Guid> NestedState { get; }

        /// <summary>
        /// The TransitionUsages that are nestedUsages of this Usage.
        /// </summary>
        [Property(xmiId: "_19_0_2_12e503d9_1578597913303_768272_3894", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1565472757327_162097_21259")]
        List<Guid> NestedTransition { get; }

        /// <summary>
        /// The Usages that are ownedFeatures of this Usage.
        /// </summary>
        [Property(xmiId: "_18_5_3_12e503d9_1565472757327_162097_21259", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1533160674959_226999_43167")]
        [SubsettedProperty(propertyName: "_19_0_2_12e503d9_1591217543254_26688_475")]
        List<Guid> NestedUsage { get; }

        /// <summary>
        /// The UseCaseUsages that are nestedUsages of this Usage.
        /// </summary>
        [Property(xmiId: "_19_0_4_12e503d9_1621463992900_247262_1080", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_19_0_2_12e503d9_1591135021853_494751_737")]
        List<Guid> NestedUseCase { get; }

        /// <summary>
        /// The VerificationCaseUsages that are nestedUsages of this Usage.
        /// </summary>
        [Property(xmiId: "_19_0_2_12e503d9_1596821592100_42801_10499", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_19_0_2_12e503d9_1591135021853_494751_737")]
        List<Guid> NestedVerificationCase { get; }

        /// <summary>
        /// The ViewUsages that are nestedUsages of this Usage.
        /// </summary>
        [Property(xmiId: "_19_0_2_12e503d9_1596644669126_858176_809", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_19_0_2_12e503d9_1591496406876_479979_3188")]
        List<Guid> NestedView { get; }

        /// <summary>
        /// The ViewpointUsages that are nestedUsages of this Usage.
        /// </summary>
        [Property(xmiId: "_19_0_2_12e503d9_1596649930212_443356_3818", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_19_0_2_12e503d9_1583000447195_878123_1244")]
        List<Guid> NestedViewpoint { get; }

        /// <summary>
        /// The Definition that owns this Usage (if any).
        /// </summary>
        [Property(xmiId: "_18_5_3_12e503d9_1565479686638_420576_23237", aggregation: AggregationKind.None, lowerValue: 0, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1533160674965_592215_43200")]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1565498571495_18876_27787")]
        Guid? OwningDefinition { get; }

        /// <summary>
        /// The Usage in which this Usage is nested (if any).
        /// </summary>
        [Property(xmiId: "_18_5_3_12e503d9_1565472757327_504924_21260", aggregation: AggregationKind.None, lowerValue: 0, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1533160674965_592215_43200")]
        Guid? OwningUsage { get; }

        /// <summary>
        /// The Usages that are features of this Usage (not necessarily owned).
        /// </summary>
        [Property(xmiId: "_19_0_2_12e503d9_1591217543254_26688_475", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1533160674959_326391_43166")]
        List<Guid> Usage { get; }

        /// <summary>
        /// The Usages which represent the variants of this Usage as a variation point Usage, if isVariation =
        /// true. If isVariation = false, then there must be no variants.
        /// </summary>
        [Property(xmiId: "_19_0_2_12e503d9_1590979649160_380466_999", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1533160674979_259543_43268")]
        List<Guid> Variant { get; }

        /// <summary>
        /// The ownedMemberships of this Usage that are VariantMemberships. If isVariation = true, then this
        /// must be all memberships of the Usage. If isVariation = false, then variantMembershipmust be empty.
        /// </summary>
        [Property(xmiId: "_19_0_2_12e503d9_1590979136735_982171_914", aggregation: AggregationKind.Composite, lowerValue: 0, upperValue: int.MaxValue, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1533160674979_190614_43269")]
        List<Guid> VariantMembership { get; }

    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
