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

namespace SysML2.NET.Core.POCO
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
    /// then f.isFeaturedWithin(null)    else featuringType->exists(t | f.isFeaturedWithin(t))   
    /// endif)sourceFeature =     if relatedFeature->isEmpty() then null     else relatedFeature->first()   
    ///  endiftargetFeature =    if relatedFeature->size() < 2 then OrderedSet{}    else        
    /// relatedFeature->            subSequence(2, relatedFeature->size())->            asOrderedSet()   
    /// endifnot isAbstract implies relatedFeature->size() >=
    /// 2specializesFromLibrary("Links::links")association->exists(oclIsKindOf(AssociationStructure))
    /// implies    specializesFromLibrary("Objects::linkObjects")connectorEnds->size() = 2
    /// andassociation->exists(oclIsKindOf(AssocationStructure)) implies   
    /// specializesFromLibrary("Objects::binaryLinkObjects")connectorEnd->size() = 2 implies   
    /// specializesFromLibrary("Links::binaryLinks")connectorEnds->size() > 2 implies    not
    /// specializesFromLibrary("Links::BinaryLink")
    /// </summary>
    public partial interface IConnector : IFeature, IRelationship
    {
        /// <summary>
        /// Queries the derived property Association
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: true, isTransient: true, isUnsettable: false, isDerived: true, isOrdered: true, isUnique: true, lowerBound: 0, upperBound: -1, isMany: false, isRequired: false, isContainment: false)]
        List<Association> QueryAssociation();

        /// <summary>
        /// Queries the derived property ConnectorEnd
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: true, isTransient: true, isUnsettable: false, isDerived: true, isOrdered: true, isUnique: true, lowerBound: 0, upperBound: -1, isMany: false, isRequired: false, isContainment: false)]
        List<Feature> QueryConnectorEnd();

        /// <summary>
        /// For a binary Connector, whether or not the Connector should be considered to have a direction from
        /// sourceFeature to targetFeature.
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: false, isTransient: false, isUnsettable: false, isDerived: false, isOrdered: false, isUnique: true, lowerBound: 1, upperBound: 1, isMany: false, isRequired: false, isContainment: false)]
        bool IsDirected { get; set; }

        /// <summary>
        /// Queries the derived property RelatedFeature
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: true, isTransient: true, isUnsettable: false, isDerived: true, isOrdered: true, isUnique: false, lowerBound: 0, upperBound: -1, isMany: false, isRequired: false, isContainment: false)]
        List<Feature> QueryRelatedFeature();

        /// <summary>
        /// Queries the derived property SourceFeature
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: true, isTransient: true, isUnsettable: false, isDerived: true, isOrdered: true, isUnique: true, lowerBound: 0, upperBound: 1, isMany: false, isRequired: false, isContainment: false)]
        Feature QuerySourceFeature();

        /// <summary>
        /// Queries the derived property TargetFeature
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: true, isTransient: true, isUnsettable: false, isDerived: true, isOrdered: true, isUnique: true, lowerBound: 0, upperBound: -1, isMany: false, isRequired: false, isContainment: false)]
        List<Feature> QueryTargetFeature();

    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
