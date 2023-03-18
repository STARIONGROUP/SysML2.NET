// -------------------------------------------------------------------------------------------------
// <copyright file="IAssociation.cs" company="RHEA System S.A.">
//
//   Copyright 2022-2023 RHEA System S.A.
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
    /// An Association is a Relationship and a Classifier to enable classification of links between things
    /// (in the universe). The co-domains (types) of the associationEnd Features are the relatedTypes, as
    /// co-domain and participants (linked things) of an Association identify each other.relatedType =
    /// associationEnd.typespecializesFromLibrary("Links::Link")oclIsKindOf(Structure) =
    /// oclIsKindOf(AssociationStructure)ownedEndFeature->size() = 2 implies   
    /// specializesFromLibrary("Links::BinaryLink)not isAbstract implies relatedType->size() >=
    /// 2associationEnds->size() > 2 implies    not specializesFromLibrary("Links::BinaryLink")sourceType = 
    ///   if relatedType->isEmpty() then null    else relatedType->first() endiftargetType =    if
    /// relatedType->size() < 2 then OrderedSet{}    else         relatedType->            subSequence(2,
    /// relatedType->size())->            asOrderedSet()     endif
    /// </summary>
    public partial interface IAssociation : IClassifier, IRelationship
    {
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
