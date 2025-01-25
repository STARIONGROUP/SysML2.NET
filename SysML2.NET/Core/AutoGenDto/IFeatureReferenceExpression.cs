// -------------------------------------------------------------------------------------------------
// <copyright file="IFeatureReferenceExpression.cs" company="Starion Group S.A.">
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
    /// A FeatureReferenceExpression is an Expression whose result is bound to a referent Feature.referent =
    ///    let nonParameterMemberships : Sequence(Membership) = ownedMembership->       
    /// reject(oclIsKindOf(ParameterMembership)) in    if nonParameterMemberships->isEmpty() or       not
    /// nonParameterMemberships->first().memberElement.oclIsKindOf(Feature)    then null    else
    /// nonParameterMemberships->first().memberElement.oclAsType(Feature)   
    /// endifownedMember->selectByKind(BindingConnector)->exists(b |   
    /// b.relatedFeatures->includes(targetFeature) and    b.relatedFeatures->includes(result))let membership
    /// : Membership =     ownedMembership->reject(m | m.oclIsKindOf(ParameterMembership))
    /// inmembership->notEmpty() andmembership->at(1).memberElement.oclIsKindOf(Feature)result.owningType()
    /// = self and result.specializes(referent)
    /// </summary>
    public partial interface IFeatureReferenceExpression : IExpression
    {
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
