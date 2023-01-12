// -------------------------------------------------------------------------------------------------
// <copyright file="IFeature.cs" company="RHEA System S.A.">
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

    /// <summary>
    /// A Feature is a Type that classifies sequences of multiple things (in the universe). These must
    /// concatenate a sequence drawn from the intersection of the Feature&#39;s featuringTypes (domain) with
    /// a sequence drawn from the intersection of its types (co-domain), treating (co)domains as sets of
    /// sequences. The domain of Features that do not have any featuringTypes is the same as if it were the
    /// library Type Anything. A Feature&#39;s types include at least Anything, which can be narrowed to
    /// other Classifiers by Redefinition.In the simplest cases, a Feature&#39;s featuringTypes and types
    /// are Classifiers, its sequences being pairs (length = 2), with the first element drawn from the
    /// Feature&#39;s domain and the second element from its co-domain (the Feature &quot;value&quot;).
    /// Examples include cars paired with wheels, people paired with other people, and cars paired with
    /// numbers representing the car length.Since Features are Types, their featuringTypes and types
    /// can be Features. When both are, Features classify sequences of at least four elements (length &gt;
    /// 3), otherwise at least three (length &gt; 2). The featuringTypes of nested Features are Features.The
    /// values of a Feature with chainingFeatures are the same as values of the last Feature in the chain,
    /// which can be found by starting with values of the first Feature, then from those values to values of
    /// the second feature, and so on, to values of the last feature.ownedRedefinition =
    /// ownedSubsetting->selectByKind(Redefinition)ownedTypeFeaturing =
    /// ownedRelationship->selectByKind(TypeFeaturing)->    select(tf | tf.featureOfType =
    /// self)ownedSubsetting = ownedGeneralization->selectByKind(Subsetting)isComposite =
    /// owningFeatureMembership <> null and owningFeatureMembership.isCompositeownedTyping =
    /// ownedGeneralization->selectByKind(FeatureTyping)isEnd = owningFeatureMembership <> null and
    /// owningFeatureMembership.oclIsKindOf(EndFeatureMembership)multiplicity <> null implies
    /// multiplicity.featuringType = featuringType
    /// allSupertypes()->includes(resolve("Base::things"))chainingFeatures->excludes(self)ownedFeatureChaining
    /// = ownedRelationship->selectByKind(FeatureChaining)chainingFeature =
    /// ownedFeatureChaining.chainingFeaturechainingFeatures->size() <> 1inverseFeature =
    /// invertingFeatureInverting.featureInverseinvertedFeature =
    /// invertedFeatureInverting.featureInvertedownedTyping.type->exists(selectByKind(DataType)) implies   
    /// allSupertypes()->includes(resolve("Base::dataValues"))ownedTyping.type->exists(selectByKind(Class))
    /// implies    allSupertypes()->includes(resolve("Occurrences::occurrences"))
    /// </summary>
    public partial interface IFeature : IType
    {
        /// <summary>
        /// Determines how values of this Feature are determined or used (see FeatureDirectionKind).
        /// </summary>
        FeatureDirectionKind? Direction { get; set; }

        /// <summary>
        /// Whether the Feature is a composite feature of its featuringType. If so, the values of the Feature
        /// cannot exist after the instance of the featuringType no longer does..
        /// </summary>
        bool IsComposite { get; set; }

        /// <summary>
        /// Whether the values of this Feature can always be computed from the values of other Features.
        /// </summary>
        bool IsDerived { get; set; }

        /// <summary>
        /// Whether or not the this Feature is an end Feature, requiring a different interpretation of the
        /// multiplicity of the Feature.An end Feature is always considered to map each domain entity to a
        /// single co-domain entity, whether or not a Multiplicity is given for it. If a Multiplicity is given
        /// for an end Feature, rather than giving the co-domain cardinality for the Feature as usual, it
        /// specifies a cardinality constraint for navigating across the endFeatures of the featuringType of the
        /// end Feature. That is, if a Type has n endFeatures, then the Multiplicity of any one of those end
        /// Features constrains the cardinality of the set of values of that Feature when the values of the
        /// other n-1 end Features are held fixed.
        /// </summary>
        bool IsEnd { get; set; }

        /// <summary>
        /// Whether an order exists for the values of this Feature or not.
        /// </summary>
        bool IsOrdered { get; set; }

        /// <summary>
        /// Whether the values of this Feature are contained in the space and time of instances of the
        /// Feature&#39;s domain.
        /// </summary>
        bool IsPortion { get; set; }

        /// <summary>
        /// Whether the values of this Feature can change over the lifetime of an instance of the domain.
        /// </summary>
        bool IsReadOnly { get; set; }

        /// <summary>
        /// Whether or not values for this Feature must have no duplicates or not.
        /// </summary>
        bool IsUnique { get; set; }

    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
