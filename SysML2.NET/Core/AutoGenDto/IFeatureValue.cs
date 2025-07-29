﻿// -------------------------------------------------------------------------------------------------
// <copyright file="IFeatureValue.cs" company="Starion Group S.A.">
//
//   Copyright 2022-2025 Starion Group S.A.
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

namespace SysML2.NET.Core.DTO
{
    using System;
    using System.Collections.Generic;

    using SysML2.NET.Core;
    using SysML2.NET.Decorators;

    /// <summary>
    /// A FeatureValue is a Membership that identifies a particular member Expression that provides the
    /// value of the Feature that owns the FeatureValue. The value is specified as either a bound value or
    /// an initial value, and as either a concrete or default value. A Feature can have at most one
    /// FeatureValue.The result of the value Expression is bound to the featureWithValue using a
    /// BindingConnector. If isInitial = false, then the featuringType of the BindingConnector is the same
    /// as the featuringType of the featureWithValue. If isInitial = true, then the featuringType of the
    /// BindingConnector is restricted to its startShot.If isDefault = false, then the above semantics of
    /// the FeatureValue are realized for the given featureWithValue. Otherwise, the semantics are realized
    /// for any individual of the featuringType of the featureWithValue, unless another value is explicitly
    /// given for the featureWithValue for that individual.not isDefault implies   
    /// featureWithValue.ownedMember->        selectByKind(BindingConnector)->exists(b |           
    /// b.relatedFeature->includes(featureWithValue) and            b.relatedFeature->exists(f |            
    ///     f.chainingFeature = Sequence{value, value.result}) and            if not isInitial then         
    ///        b.featuringType = featureWithValue.featuringType            else                
    /// b.featuringType->exists(t |                    t.oclIsKindOf(Feature) and                   
    /// t.oclAsType(Feature).chainingFeature =                        Sequence{                           
    /// resolveGlobal('Base::things::that').                                memberElement,                  
    ///          resolveGlobal('Occurrences::Occurrence::startShot').                               
    /// memberElement                        }                )           
    /// endif)featureWithValue.redefinition.redefinedFeature->   
    /// closure(redefinition.redefinedFeature).valuation->    forAll(isDefault)isInitial implies
    /// featureWithValue.isVariable
    /// </summary>
    public partial interface IFeatureValue : IOwningMembership
    {
        /// <summary>
        /// Whether this FeatureValue is a concrete specification of the bound or initial value of the
        /// featureWithValue, or just a default value that may be overridden.
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: false, isTransient: false, isUnsettable: false, isDerived: false, isOrdered: false, isUnique: true, lowerBound: 1, upperBound: 1, isMany: false, isRequired: false, isContainment: false)]
        bool IsDefault { get; set; }

        /// <summary>
        /// Whether this FeatureValue specifies a bound value or an initial value for the featureWithValue.
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: false, isTransient: false, isUnsettable: false, isDerived: false, isOrdered: false, isUnique: true, lowerBound: 1, upperBound: 1, isMany: false, isRequired: false, isContainment: false)]
        bool IsInitial { get; set; }

    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
