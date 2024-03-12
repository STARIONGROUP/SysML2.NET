// -------------------------------------------------------------------------------------------------
// <copyright file="IRedefinition.cs" company="RHEA System S.A.">
//
//   Copyright 2022-2024 RHEA System S.A.
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
    /// Redefinition is a kind of Subsetting that requires the redefinedFeature and the redefiningFeature to
    /// have the same values (on each instance of the domain of the redefiningFeature). This means any
    /// restrictions on the redefiningFeature, such as type or multiplicity, also apply to the
    /// redefinedFeature (on each instance of the domain of the redefiningFeature), and vice versa. The
    /// redefinedFeature might have values for instances of the domain of the redefiningFeature, but only as
    /// instances of the domain of the redefinedFeature that happen to also be instances of the domain of
    /// the redefiningFeature. This is supported by the constraints inherited from Subsetting on the domains
    /// of the redefiningFeature and redefinedFeature. However, these constraints are narrowed for
    /// Redefinition to require the owningTypes of the redefiningFeature and redefinedFeature to be
    /// different and the redefinedFeature to not be inherited into the owningNamespace of the
    /// redefiningFeature.This enables the redefiningFeature to have the same name as the redefinedFeature,
    /// if desired.let anythingType: Type =   
    /// redefiningFeature.resolveGlobal('Base::Anything').modelElement.oclAsType(Type) in -- Including
    /// "Anything" accounts for implicit featuringType of Features-- with no explicit featuringType.let
    /// redefiningFeaturingTypes: Set(Type) =   
    /// redefiningFeature.featuringTypes->asSet()->including(anythingType) inlet redefinedFeaturingTypes:
    /// Set(Type) =    redefinedFeature.featuringTypes->asSet()->including(anythingType)
    /// inredefiningFeaturingTypes <> redefinedFeaturingTypefeaturingType->forAll(t |    let direction :
    /// FeatureDirectionKind = t.directionOf(redefinedFeature) in    ((direction =
    /// FeatureDirectionKind::_'in' or       direction = FeatureDirectionKind::out) implies        
    /// redefiningFeature.direction = direction)    and     (direction = FeatureDirectionKind::inout implies
    /// redefiningFeature.direction <> null))
    /// </summary>
    public partial interface IRedefinition : ISubsetting
    {
        /// <summary>
        /// The Feature that is redefined by the redefiningFeature of this Redefinition.
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: false, isTransient: false, isUnsettable: false, isDerived: false, isOrdered: false, isUnique: true, lowerBound: 1, upperBound: 1, isMany: false, isRequired: false, isContainment: false)]
        Guid RedefinedFeature { get; set; }

        /// <summary>
        /// The Feature that is redefining the redefinedFeature of this Redefinition.
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: false, isTransient: false, isUnsettable: false, isDerived: false, isOrdered: false, isUnique: true, lowerBound: 1, upperBound: 1, isMany: false, isRequired: false, isContainment: false)]
        Guid RedefiningFeature { get; set; }

    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
