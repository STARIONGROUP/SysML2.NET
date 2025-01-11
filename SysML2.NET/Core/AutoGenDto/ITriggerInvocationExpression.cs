// -------------------------------------------------------------------------------------------------
// <copyright file="ITriggerInvocationExpression.cs" company="Starion Group S.A.">
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
    /// A TriggerInvocationExpression is an InvocationExpression that invokes one of the trigger Functions
    /// from the Kernel Semantic Library Triggers package, as indicated by its kind.specializesFromLibrary( 
    ///   if kind = TriggerKind::when then        'Triggers::TriggerWhen'    else if kind = TriggerKind::at
    /// then        'Triggers::TriggerAt'    else         'Triggers::TriggerAfter'    endif endif)kind =
    /// TriggerKind::after implies    argument->notEmpty() and   
    /// argument->at(1).result.specializesFromLibrary('Quantities::ScalarQuantityValue') and    let mRef :
    /// Element =         resolveGlobal('Quantities::TensorQuantityValue::mRef').ownedMemberElement in   
    /// argument->at(1).result.feature->        select(ownedRedefinition.redefinedFeature->          
    /// closure(ownedRedefinition.redefinedFeature)->           includes(mRef))->       
    /// exists(specializesFromLibrary('ISQBase::DurationUnit'))kind = TriggerKind::at implies   
    /// argument->notEmpty() and   
    /// argument->at(1).result.specializesFromLibrary('Time::TimeInstantValue')kind = TriggerKind::when
    /// implies    argument->notEmpty() and    argument->at(1).oclIsKindOf(FeatureReferenceExpression) and  
    ///  let referent : Feature =         argument->at(1).oclAsType(FeatureReferenceExpression).referent in 
    ///   referent.oclIsKindOf(Expression) and   
    /// referent.oclAsType(Expression).result.specializesFromLibrary('ScalarValues::Boolean')
    /// </summary>
    public partial interface ITriggerInvocationExpression : IInvocationExpression
    {
        /// <summary>
        /// Indicates which of the Functions from the Triggers model in the Kernel Semantic Library is to be
        /// invoked by this TriggerInvocationExpression.
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: false, isTransient: false, isUnsettable: false, isDerived: false, isOrdered: false, isUnique: true, lowerBound: 1, upperBound: 1, isMany: false, isRequired: false, isContainment: false)]
        TriggerKind Kind { get; set; }

    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
