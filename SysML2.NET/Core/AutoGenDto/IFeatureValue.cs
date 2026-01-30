// -------------------------------------------------------------------------------------------------
// <copyright file="IFeatureValue.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.Core.DTO.Kernel.FeatureValues
{
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.Generic;

    using SysML2.NET.Core.Root.Namespaces;
    using SysML2.NET.Core.DTO.Root.Namespaces;
    using SysML2.NET.Decorators;

    /// <summary>
    /// A FeatureValue is a Membership that identifies a particular member Expression that provides the
    /// value of the Feature that owns the FeatureValue. The value is specified as either a bound value or
    /// an initial value, and as either a concrete or default value. A Feature can have at most one
    /// FeatureValue.  The result of the value Expression is bound to the featureWithValue using a
    /// BindingConnector. If isInitial = false, then the featuringType of the BindingConnector is the same
    /// as the featuringType of the featureWithValue. If isInitial = true, then the featuringType of the
    /// BindingConnector is restricted to its startShot.  If isDefault = false, then the above semantics of
    /// the FeatureValue are realized for the given featureWithValue. Otherwise, the semantics are realized
    /// for any individual of the featuringType of the featureWithValue, unless another value is explicitly
    /// given for the featureWithValue for that individual.
    /// </summary>
    [Class(xmiId: "Kernel-FeatureValues-FeatureValue", isAbstract: false, isFinalSpecialization: false, isActive: false)]
    [GeneratedCode("SysML2.NET", "latest")]
    public partial interface IFeatureValue : IOwningMembership
    {
        /// <summary>
        /// The Feature to be provided a value.
        /// </summary>
        [Property(xmiId: "Kernel-FeatureValues-FeatureValue-featureWithValue", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Root-Namespaces-Membership-membershipOwningNamespace")]
        Guid featureWithValue { get; }

        /// <summary>
        /// Whether this FeatureValue is a concrete specification of the bound or initial value of the
        /// featureWithValue, or just a default value that may be overridden.
        /// </summary>
        [Property(xmiId: "Kernel-FeatureValues-FeatureValue-isDefault", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: "false")]
        bool IsDefault { get; set; }

        /// <summary>
        /// Whether this FeatureValue specifies a bound value or an initial value for the featureWithValue.
        /// </summary>
        [Property(xmiId: "Kernel-FeatureValues-FeatureValue-isInitial", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: "false")]
        bool IsInitial { get; set; }

        /// <summary>
        /// The Expression that provides the value as a result.
        /// </summary>
        [Property(xmiId: "Kernel-FeatureValues-FeatureValue-value", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [RedefinedProperty(propertyName: "Root-Namespaces-OwningMembership-ownedMemberElement")]
        Guid value { get; }

    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
