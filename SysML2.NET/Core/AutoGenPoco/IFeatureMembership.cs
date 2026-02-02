// -------------------------------------------------------------------------------------------------
// <copyright file="IFeatureMembership.cs" company="Starion Group S.A.">
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

    using SysML2.NET.Core.Root.Namespaces;
    using SysML2.NET.Core.POCO.Core.Features;
    using SysML2.NET.Core.POCO.Root.Annotations;
    using SysML2.NET.Core.POCO.Root.Elements;
    using SysML2.NET.Core.POCO.Root.Namespaces;
    using SysML2.NET.Decorators;

    /// <summary>
    /// A FeatureMembership is an OwningMembership between an ownedMemberFeature and an owningType. If the
    /// ownedMemberFeature has isVariable = false, then the FeatureMembership implies that the owningType is
    /// also a featuringType of the ownedMemberFeature. If the ownedMemberFeature has isVariable = true,
    /// then the FeatureMembership implies that the ownedMemberFeature is featured by the snapshots of the
    /// owningType, which must specialize the Kernel Semantic Library base class Occurrence.
    /// </summary>
    [Class(xmiId: "_18_5_3_12e503d9_1533160651715_740575_42237", isAbstract: false, isFinalSpecialization: false, isActive: false)]
    [GeneratedCode("SysML2.NET", "latest")]
    public partial interface IFeatureMembership : IOwningMembership
    {
        /// <summary>
        /// The Feature that this FeatureMembership relates to its owningType, making it an ownedFeature of the
        /// owningType.
        /// </summary>
        [Property(xmiId: "_18_5_3_12e503d9_1533160674993_898044_43344", aggregation: AggregationKind.Composite, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [RedefinedProperty(propertyName: "_18_5_3_12e503d9_1533160674965_501750_43196")]
        IFeature ownedMemberFeature { get; }

        /// <summary>
        /// The Type that owns this FeatureMembership.
        /// </summary>
        [Property(xmiId: "_18_5_3_12e503d9_1533160674992_418504_43339", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_19_0_4_12e503d9_1651076866524_738482_486")]
        [RedefinedProperty(propertyName: "_18_5_3_12e503d9_1533160674965_193857_43197")]
        IType owningType { get; }

    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
