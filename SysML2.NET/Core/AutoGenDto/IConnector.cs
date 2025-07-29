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
    /// Features on instances of its domain.relatedFeature = connectorEnd.ownedReferenceSubsetting->   
    /// select(s | s <> null).subsettedFeaturerelatedFeature->forAll(f |     if featuringType->isEmpty()
    /// then f.isFeaturedWithin(null)    else featuringType->forAll(t | f.isFeaturedWithin(t))   
    /// endif)sourceFeature =     if relatedFeature->isEmpty() then null     else relatedFeature->first()   
    ///  endiftargetFeature =    if relatedFeature->size() < 2 then OrderedSet{}    else        
    /// relatedFeature->            subSequence(2, relatedFeature->size())->            asOrderedSet()   
    /// endifnot isAbstract implies relatedFeature->size() >=
    /// 2specializesFromLibrary('Links::links')association->exists(oclIsKindOf(AssociationStructure))
    /// implies    specializesFromLibrary('Objects::linkObjects')connectorEnds->size() = 2
    /// andassociation->exists(oclIsKindOf(AssociationStructure)) implies   
    /// specializesFromLibrary('Objects::binaryLinkObjects')connectorEnd->size() = 2 implies   
    /// specializesFromLibrary('Links::binaryLinks')connectorEnds->size() > 2 implies    not
    /// specializesFromLibrary('Links::BinaryLink')let commonFeaturingTypes : OrderedSet(Type) =    
    /// relatedFeature->closure(featuringType)->select(t |         relatedFeature->forAll(f |
    /// f.isFeaturedWithin(t))    ) inlet nearestCommonFeaturingTypes : OrderedSet(Type) =   
    /// commonFeaturingTypes->reject(t1 |         commonFeaturingTypes->exists(t2 |             t2 <> t1 and
    /// t2->closure(featuringType)->contains(t1)    )) inif nearestCommonFeaturingTypes->isEmpty() then
    /// nullelse nearestCommonFeaturingTypes->first()endif
    /// </summary>
    public partial interface IConnector : IFeature, IRelationship
    {
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
