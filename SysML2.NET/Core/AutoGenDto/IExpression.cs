// -------------------------------------------------------------------------------------------------
// <copyright file="IExpression.cs" company="Starion Group S.A.">
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
    /// An Expression is a Step that is typed by a Function. An Expression that also has a Function as its
    /// featuringType is a computational step within that Function. An Expression always has a single result
    /// parameter, which redefines the result parameter of its defining function. This allows Expressions to
    /// be interconnected in tree structures, in which inputs to each Expression in the tree are determined
    /// as the results of other Expression in the tree.featureMembership->   
    /// selectByKind(ReturnParameterMembership)->    size() = 1isModelLevelEvaluable =
    /// modelLevelEvaluable(Set(Element){})owningMembership <> null and
    /// owningMembership.oclIsKindOf(FeatureValue) implies    let featureWithValue : Feature =        
    /// owningMembership.oclAsType(FeatureValue).featureWithValue in    featuringType =
    /// featureWithValue.featuringTyperesult =    let resultParams : Sequence(Feature) =       
    /// featureMemberships->            selectByKind(ReturnParameterMembership).           
    /// ownedParameterMember in    if resultParams->notEmpty() then resultParams->first()    else null   
    /// endifownedMembership.selectByKind(ResultExpressionMembership)->    forAll(mem |
    /// ownedFeature.selectByKind(BindingConnector)->        exists(binding |           
    /// binding.relatedFeature->includes(result) and           
    /// binding.relatedFeature->includes(mem.ownedResultExpression.result)))membership->selectByKind(ResultExpressionMembership)->size()
    /// <= 1specializesFromLibrary('Performances::evaluations')
    /// </summary>
    public partial interface IExpression : IStep
    {
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
