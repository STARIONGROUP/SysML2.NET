// -------------------------------------------------------------------------------------------------
// <copyright file="IMultiplicityRange.cs" company="Starion Group S.A.">
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
    /// A MultiplicityRange is a Multiplicity whose value is defined to be the (inclusive) range of natural
    /// numbers given by the result of a lowerBound Expression and the result of an upperBound Expression.
    /// The result of these Expressions shall be of type Natural. If the result of the upperBound Expression
    /// is the unbounded value *, then the specified range includes all natural numbers greater than or
    /// equal to the lowerBound value. If no lowerBound Expression, then the default is that the lower bound
    /// has the same value as the upper bound, except if the upperBound evaluates to *, in which case the
    /// default for the lower bound is 0.bound->forAll(b | b.featuringType =
    /// self.featuringType)bound->forAll(b |    b.result.specializesFromLibrary('ScalarValues::Integer') and
    ///    let value : UnlimitedNatural = valueOf(b) in    value <> null implies value >= 0)lowerBound =   
    /// let ownedExpressions : Sequence(Expression) =        ownedMember->selectByKind(Expression) in    if
    /// ownedExpressions->size() < 2 then null    else ownedExpressions->first()    endifupperBound =    let
    /// ownedExpressions : Sequence(Expression) =        ownedMember->selectByKind(Expression) in    if
    /// ownedExpressions->isEmpty() then null    else if ownedExpressions->size() = 1 then
    /// ownedExpressions->at(1)    else ownedExpressions->at(2)    endif endif bound =    if upperBound =
    /// null then Sequence{}    else if lowerBound = null then Sequence{upperBound}    else
    /// Sequence{lowerBound, upperBound}    endif endifif lowerBound = null then    ownedMember->notEmpty()
    /// and    ownedMember->at(1) = upperBoundelse    ownedMember->size() > 1 and    ownedMember->at(1) =
    /// lowerBound and    ownedMember->at(2) = upperBoundendif
    /// </summary>
    public partial interface IMultiplicityRange : IMultiplicity
    {
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
