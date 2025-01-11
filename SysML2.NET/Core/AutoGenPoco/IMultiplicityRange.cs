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

namespace SysML2.NET.Core.POCO
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
    /// let ownedMembers : Sequence(Element) =        
    /// ownedMembership->selectByKind(OwningMembership).ownedMember in    if ownedMembers->size() < 2 or    
    ///     not ownedMembers->first().oclIsKindOf(Expression) then null    else
    /// ownedMembers->first().oclAsType(Expression)    endifupperBound =    let ownedMembers :
    /// Sequence(Element) =         ownedMembership->selectByKind(OwningMembership).ownedMember in    if
    /// ownedMembers->isEmpty() or        not ownedMembers->last().oclIsKindOf(Expression)     then null   
    /// else ownedMembers->last().oclAsType(Expression)    endif
    /// </summary>
    public partial interface IMultiplicityRange : IMultiplicity
    {
        /// <summary>
        /// Queries the derived property Bound
        /// </summary>
        [EFeature(isChangeable: false, isVolatile: true, isTransient: true, isUnsettable: false, isDerived: true, isOrdered: true, isUnique: true, lowerBound: 1, upperBound: 2, isMany: false, isRequired: false, isContainment: false)]
        List<Expression> QueryBound();

        /// <summary>
        /// Queries the derived property LowerBound
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: true, isTransient: true, isUnsettable: false, isDerived: true, isOrdered: false, isUnique: true, lowerBound: 0, upperBound: 1, isMany: false, isRequired: false, isContainment: false)]
        Expression QueryLowerBound();

        /// <summary>
        /// Queries the derived property UpperBound
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: true, isTransient: true, isUnsettable: false, isDerived: true, isOrdered: false, isUnique: true, lowerBound: 1, upperBound: 1, isMany: false, isRequired: false, isContainment: false)]
        Expression QueryUpperBound();

    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
