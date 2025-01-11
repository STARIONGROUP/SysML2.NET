// -------------------------------------------------------------------------------------------------
// <copyright file="IFunction.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.Core.POCO
{
    using System;
    using System.Collections.Generic;

    using SysML2.NET.Core;
    using SysML2.NET.Decorators;

    /// <summary>
    /// A Function is a Behavior that has an out parameter that is identified as its result. A Function
    /// represents the performance of a calculation that produces the values of its result parameter. This
    /// calculation may be decomposed into Expressions that are steps of the
    /// Function.ownedMembership.selectByKind(ResultExpressionMembership)->    forAll(mem |
    /// ownedFeature.selectByKind(BindingConnector)->        exists(binding |           
    /// binding.relatedFeature->includes(result) and           
    /// binding.relatedFeature->includes(mem.ownedResultExpression.result)))specializesFromLibrary('Performances::Evaluation')result
    /// =    let resultParams : Sequence(Feature) =        ownedFeatureMemberships->           
    /// selectByKind(ReturnParameterMembership).            ownedParameterMember in    if
    /// resultParams->notEmpty() then resultParams->first()    else null    endifownedFeatureMembership->   
    /// selectByKind(ReturnParameterMembership)->    size() <=
    /// 1membership->selectByKind(ResultExpressionMembership)->size() <= 1
    /// </summary>
    public partial interface IFunction : IBehavior
    {
        /// <summary>
        /// Queries the derived property Expression
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: true, isTransient: true, isUnsettable: false, isDerived: true, isOrdered: false, isUnique: true, lowerBound: 0, upperBound: -1, isMany: false, isRequired: false, isContainment: false)]
        List<Expression> QueryExpression();

        /// <summary>
        /// Queries the derived property IsModelLevelEvaluable
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: true, isTransient: true, isUnsettable: false, isDerived: true, isOrdered: false, isUnique: true, lowerBound: 1, upperBound: 1, isMany: false, isRequired: false, isContainment: false)]
        bool QueryIsModelLevelEvaluable();

        /// <summary>
        /// Queries the derived property Result
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: true, isTransient: true, isUnsettable: false, isDerived: true, isOrdered: false, isUnique: true, lowerBound: 1, upperBound: 1, isMany: false, isRequired: false, isContainment: false)]
        Feature QueryResult();

    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
