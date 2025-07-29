// -------------------------------------------------------------------------------------------------
// <copyright file="IConstructorExpression.cs" company="Starion Group S.A.">
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
    /// A ConstructorExpression is an InstantiationExpression whose result specializes its instantiatedType,
    /// binding some or all of the features of the instantiatedType to the results of its argument
    /// Expressions.instantiatedType.feature->collect(f |    
    /// result.ownedFeatures->select(redefines(f)).valuation->    select(v | v <> null).value)let features :
    /// OrderedSet(Feature) = instantiatedType.feature->    select(visibility = VisibilityKind::public)
    /// inresult.ownedFeature->forAll(f1 | result.ownedFeature->forAll(f2 |    f1 <> f2 implies       
    /// f1.ownedRedefinition.redefinedFeature->           
    /// intersection(f2.ownedRedefinition.redefinedFeature)->           
    /// intersection(features)->isEmpty()))let features : OrderedSet(Feature) = instantiatedType.feature->  
    ///  select(owningMembership.visibility = VisibilityKind::public) inresult.ownedFeature->forAll(f |    
    /// f.ownedRedefinition.redefinedFeature->        intersection(features)->size() =
    /// 1)TBDspecializes('Performances::constructorEvaluations')result.specializes(instantiatedType)ownedFeatures->excluding(result)->isEmpty()
    /// </summary>
    public partial interface IConstructorExpression : IInstantiationExpression
    {
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
