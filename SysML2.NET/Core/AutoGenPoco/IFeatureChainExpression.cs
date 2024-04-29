// -------------------------------------------------------------------------------------------------
// <copyright file="IFeatureChainExpression.cs" company="Starion Group S.A.">
//
//   Copyright 2022-2024 Starion Group S.A.
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

namespace SysML2.NET.Core.POCO
{
    using System;
    using System.Collections.Generic;

    using SysML2.NET.Core;
    using SysML2.NET.Decorators;

    /// <summary>
    /// A FeatureChainExpression is an OperatorExpression whose operator is ".", which resolves to the
    /// Function ControlFunctions::'.' from the Kernel Functions Library. It evaluates to the result of
    /// chaining the result Feature of its single argument Expression with its targetFeature.let
    /// sourceParameter : Feature = sourceTargetFeature() insourceTargetFeature <> null
    /// andsourceTargetFeature.redefinesFromLibrary('ControlFunctions::\'.\'::source::target')let
    /// sourceParameter : Feature = sourceTargetFeature() insourceTargetFeature <> null
    /// andsourceTargetFeature.redefines(targetFeature)targetFeature =    let nonParameterMemberships :
    /// Sequence(Membership) = ownedMembership->        reject(oclIsKindOf(ParameterMembership)) in    if
    /// nonParameterMemberships->isEmpty() or       not
    /// nonParameterMemberships->first().memberElement.oclIsKindOf(Feature)    then null    else
    /// nonParameterMemberships->first().memberElement.oclAsType(Feature)    endifargument->notEmpty()
    /// implies    targetFeature.featuringType->forAll(t |         t.specializes(argument->at(1).result))
    /// </summary>
    public partial interface IFeatureChainExpression : IOperatorExpression
    {
        /// <summary>
        /// Queries the derived property TargetFeature
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: true, isTransient: true, isUnsettable: false, isDerived: true, isOrdered: false, isUnique: true, lowerBound: 1, upperBound: 1, isMany: false, isRequired: false, isContainment: false)]
        Feature QueryTargetFeature();

    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
