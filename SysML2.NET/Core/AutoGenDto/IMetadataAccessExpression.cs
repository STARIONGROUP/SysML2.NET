// -------------------------------------------------------------------------------------------------
// <copyright file="IMetadataAccessExpression.cs" company="Starion Group S.A.">
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
    /// A MetadataAccessExpression is an Expression whose result is a sequence of instances of Metaclasses
    /// representing all the MetadataFeature annotations of the referencedElement. In addition, the sequence
    /// includes an instance of the reflective Metaclass corresponding to the MOF class of the
    /// referencedElement, with values for all the abstract syntax properties of the
    /// referencedElement.specializesFromLibrary('Performances::metadataAccessEvaluations')ownedMembership->exists(not
    /// oclIsKindOf(FeatureMembership))referencedElement =    let elements : Sequence(Element) =
    /// ownedMembership->        reject(oclIsKindOf(FeatureMembership)).memberElement in    if
    /// elements->isEmpty() then null    else elements->first()    endif
    /// </summary>
    public partial interface IMetadataAccessExpression : IExpression
    {
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
