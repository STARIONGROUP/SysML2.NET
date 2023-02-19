// -------------------------------------------------------------------------------------------------
// <copyright file="IConnector.cs" company="RHEA System S.A.">
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
    /// A Connector is a usage of Associations, with links restricted according to instances of the Type in
    /// which they are used (domain of the Connector). Associations restrict what kinds of things might be
    /// linked. The Connector further restricts these links to between values of two Features on instances
    /// of its domain.relatedFeature =
    /// connectorEnd.ownedReferenceSubsetting.subsettedFeaturerelatedFeature->forAll(f |     if
    /// featuringType->isEmpty() then f.isFeaturedWithin(null)    else featuringType->exists(t |
    /// f.isFeaturedWithin(t))    endif)sourceFeature =     if relatedFeature->size() = 2 then
    /// relatedFeature->at(1)     else null     endiftargetFeature =    if sourceFeature = null then
    /// relatedFeature    else relatedFeature->excluding(sourceFeature)    endifconnectorEnd =
    /// feature->select(isEnd)not isAbstract implies relatedFeature->size() >=
    /// 2specializesFromLibrary("Links::links")connectorEnd->size() = 2 implies   
    /// specializesFromLibrary('Links::binaryLinks')association->exists(oclIsKindOf(AssociationStructure))
    /// implies    specializesFromLibrary("Objects::linkObjects")connectorEnds->size() = 2
    /// andassociation->exists(oclIsKindOf(AssocationStructure)) implies   
    /// specializesFromLibrary("Objects::binaryLinkObjects")connectorEnds->size() > 2 implies    not
    /// specializesFromLibrary("Links::BinaryLink")
    /// </summary>
    public partial interface IConnector : IFeature, IRelationship
    {
        /// <summary>
        /// For a binary Connector, whether or not the Connector should be considered to have a direction from
        /// source to target.
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: false, isTransient: false, isUnsettable: false, isDerived: false, isOrdered: false, isUnique: true, lowerBound: 1, upperBound: 1, isMany: false, isRequired: false, isContainment: false)]
        bool IsDirected { get; set; }

    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
