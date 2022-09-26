// -------------------------------------------------------------------------------------------------
// <copyright file="IConnector.cs" company="RHEA System S.A.">
//
//   Copyright 2022 RHEA System S.A.
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

    /// <summary>
    /// A Connector is a usage of Associations, with links restricted to instances of the Type in which it
    /// is used (domain of the Connector). Associations restrict what kinds of things might be linked. The
    /// Connector further restricts these links to between values of two Features on instances of
    /// its domain.relatedFeature =
    /// connectorEnd.ownedReferenceSubsetting.subsettedFeaturerelatedFeature->forAll(f |     if
    /// featuringType->isEmpty() then f.isFeaturedWithin(null)    else featuringType->exists(t |
    /// f.isFeaturedWithin(t))    endif)sourceFeature =     if relatedFeature->size() = 2 then
    /// relatedFeature->at(1)     else null     endiftargetFeature =    if sourceFeature = null then
    /// relatedFeature    else relatedFeature->excluding(sourceFeature)    endifconnectorEnd =
    /// feature->select(isEnd)association->forAll(a |    a.associationEnd->forAll(ae |       
    /// connectorEnd->one(ce |             ce.ownedRedefinition.redefinedFeature->includes(ae))))
    /// </summary>
    public partial interface IConnector : IFeature, IRelationship
    {
        /// <summary>
        /// Queries the derived property Association
        /// </summary>
        List<Association> QueryAssociation();

        /// <summary>
        /// Queries the derived property ConnectorEnd
        /// </summary>
        List<Feature> QueryConnectorEnd();

        /// <summary>
        /// For a binary Connector, whether or not the Connector should be considered to have a direction from
        /// source to target.
        /// </summary>
        bool IsDirected { get; set; }

        /// <summary>
        /// Queries the derived property RelatedFeature
        /// </summary>
        List<Feature> QueryRelatedFeature();

        /// <summary>
        /// Queries the derived property SourceFeature
        /// </summary>
        Feature QuerySourceFeature();

        /// <summary>
        /// Queries the derived property TargetFeature
        /// </summary>
        List<Feature> QueryTargetFeature();

    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
