// -------------------------------------------------------------------------------------------------
// <copyright file="IFeatureInverting.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.Core.DTO.Core.Features
{
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.Generic;

    using SysML2.NET.Core.DTO.Root.Elements;
    using SysML2.NET.Decorators;

    /// <summary>
    /// A FeatureInverting is a Relationship between Features asserting that their interpretations
    /// (sequences) are the reverse of each other, identified as featureInverted and invertingFeature. For
    /// example, a Feature identifying each person&#39;s parents is the inverse of a Feature identifying
    /// each person&#39;s children. A person identified as a parent of another will identify that other as
    /// one of their children.
    /// </summary>
    [Class(xmiId: "_19_0_4_b9102da_1623178487957_761743_77", isAbstract: false, isFinalSpecialization: false, isActive: false)]
    [GeneratedCode("SysML2.NET", "latest")]
    public partial interface IFeatureInverting : IRelationship
    {
        /// <summary>
        /// The Feature that is an inverse of the invertingFeature.
        /// </summary>
        [Property(xmiId: "_19_0_4_b9102da_1623178838862_842173_146", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [RedefinedProperty(propertyName: "_18_5_3_12e503d9_1533160674971_696758_43228")]
        Guid FeatureInverted { get; set; }

        /// <summary>
        /// The Feature that is an inverse of the invertedFeature.
        /// </summary>
        [Property(xmiId: "_19_0_4_b9102da_1623178854941_627588_162", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [RedefinedProperty(propertyName: "_18_5_3_12e503d9_1533160674961_138197_43179")]
        Guid InvertingFeature { get; set; }

        /// <summary>
        /// A featureInverted that is also the owningRelatedElement of this FeatureInverting.
        /// </summary>
        [Property(xmiId: "_19_0_4_b9102da_1653567738671_122613_44", aggregation: AggregationKind.None, lowerValue: 0, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_19_0_4_b9102da_1623178838862_842173_146")]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1543092026091_693018_16749")]
        Guid? OwningFeature { get; }

    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
