// -------------------------------------------------------------------------------------------------
// <copyright file="IInvocationExpression.cs" company="Starion Group S.A.">
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
    /// An InvocationExpression is an InstantiationExpression whose instantiatedType must be a Behavior or a
    /// Feature typed by a single Behavior (such as a Step). Each of the input parameters of the
    /// instantiatedType are bound to the result of an argument Expression. If the instantiatedType is a
    /// Function or a Feature typed by a Function, then the result of the InvocationExpression is the result
    /// of the invoked Function. Otherwise, the result is an instance of the instantiatedType (essentially
    /// like a behavioral ConstructorExpression).not instantiatedType.oclIsKindOf(Function) andnot
    /// (instantiatedType.oclIsKindOf(Feature) and     
    /// instantiatedType.oclAsType(Feature).type->exists(oclIsKindOf(Function))) implies   
    /// ownedFeature.selectByKind(BindingConnector)->exists(        relatedFeature->includes(self) and      
    ///  relatedFeature->includes(result))TBDinstantiatedType.input->collect(inp |    
    /// ownedFeatures->select(redefines(inp)).valuation->    select(v | v <> null).value)let parameters :
    /// OrderedSet(Feature) = instantiatedType.input ininput->forAll(inp |    
    /// inp.ownedRedefinition.redefinedFeature->        intersection(parameters)->size() = 1)let features :
    /// OrderedSet(Feature) = instantiatedType.feature ininput->forAll(inp1 | input->forAll(inp2 |    inp1
    /// <> inp2 implies        inp1.ownedRedefinition.redefinedFeature->           
    /// intersection(inp2.ownedRedefinition.redefinedFeature)->           
    /// intersection(features)->isEmpty()))not instantiatedType.oclIsKindOf(Function) andnot
    /// (instantiatedType.oclIsKindOf(Feature) and     
    /// instantiatedType.oclAsType(Feature).type->exists(oclIsKindOf(Function))) implies   
    /// result.specializes(instantiatedType)specializes(instantiatedType)instantiatedType.oclIsKindOf(Behavior)
    /// orinstantiatedType.oclIsKindOf(Feature) and    instantiatedType.type->exists(oclIsKindOf(Behavior))
    /// and    instantiatedType.type->size(1)ownedFeature->forAll(f |    f <> result implies        
    /// f.direction = FeatureDirectionKind::_'in')
    /// </summary>
    public partial interface IInvocationExpression : IInstantiationExpression
    {
        /// <summary>
        /// Queries the derived property Operand
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: true, isTransient: true, isUnsettable: false, isDerived: true, isOrdered: true, isUnique: true, lowerBound: 0, upperBound: -1, isMany: false, isRequired: false, isContainment: true)]
        List<Expression> QueryOperand();

    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
