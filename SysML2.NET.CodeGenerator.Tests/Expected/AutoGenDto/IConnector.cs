// -------------------------------------------------------------------------------------------------
// <copyright file="IConnector.cs" company="Starion Group S.A.">
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
    /// A Connector is a usage of Associations, with links restricted according to instances of the Type in
    /// which they are used (domain of the Connector). The associations of the Connector restrict what kinds
    /// of things might be linked. The Connector further restricts these links to be between values of
    /// Features on instances of its domain.not isAbstract implies relatedFeature->size() >=
    /// 2connectorEnds->size() = 2 andassociation->exists(oclIsKindOf(AssociationStructure)) implies   
    /// specializesFromLibrary('Objects::binaryLinkObjects')sourceFeature =     if relatedFeature->isEmpty()
    /// then null     else relatedFeature->first()     endifconnectorEnds->size() > 2 implies    not
    /// specializesFromLibrary('Links::BinaryLink')relatedFeature->forAll(f |     if
    /// featuringType->isEmpty() then f.isFeaturedWithin(null)    else featuringType->forAll(t |
    /// f.isFeaturedWithin(t))    endif)relatedFeature = connectorEnd.ownedReferenceSubsetting->    select(s
    /// | s <> null).subsettedFeaturespecializesFromLibrary('Links::links')connectorEnd->size() = 2 implies 
    ///   specializesFromLibrary('Links::binaryLinks')association->exists(oclIsKindOf(AssociationStructure))
    /// implies    specializesFromLibrary('Objects::linkObjects')targetFeature =    if
    /// relatedFeature->size() < 2 then OrderedSet{}    else         relatedFeature->           
    /// subSequence(2, relatedFeature->size())->            asOrderedSet()    endif
    /// </summary>
    public partial interface IConnector : IFeature, IRelationship
    {
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
