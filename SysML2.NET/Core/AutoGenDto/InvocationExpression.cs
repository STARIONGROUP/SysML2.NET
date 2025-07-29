// -------------------------------------------------------------------------------------------------
// <copyright file="InvocationExpression.cs" company="Starion Group S.A.">
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
    /// An InvocationExpression is an InstantiationExpression whose instantiatedType must be a Behavior or a
    /// Feature typed by a single Behavior (such as a Step). Each of the input parameters of the
    /// instantiatedType are bound to the result of an argument Expression. If the instantiatedType is a
    /// Function or a Feature typed by a Function, then the result of the InvocationExpression is the result
    /// of the invoked Function. Otherwise, the result is an instance of the instantiatedType (essentially
    /// like a behavioral ConstructorExpression).not instantiatedType.oclIsKindOf(Function) andnot
    /// (instantiatedType.oclIsKindOf(Feature) and     
    /// instantiatedType.oclAsType(Feature).type->exists(oclIsKindOf(Function))) implies   
    /// ownedFeature.selectByKind(BindingConnector)->exists(        relatedFeature->includes(self) and      
    ///  relatedFeature->includes(result))TBDinstantiatedType.input->collect(inp |    
    /// ownedFeatures->select(redefines(inp)).valuation->    select(v | v <> null).value)let parameters :
    /// OrderedSet(Feature) = instantiatedType.input ininput->forAll(inp |    
    /// inp.ownedRedefinition.redefinedFeature->        intersection(parameters)->size() = 1)let features :
    /// OrderedSet(Feature) = instantiatedType.feature ininput->forAll(inp1 | input->forAll(inp2 |    inp1
    /// <> inp2 implies        inp1.ownedRedefinition.redefinedFeature->           
    /// intersection(inp2.ownedRedefinition.redefinedFeature)->           
    /// intersection(features)->isEmpty()))not instantiatedType.oclIsKindOf(Function) andnot
    /// (instantiatedType.oclIsKindOf(Feature) and     
    /// instantiatedType.oclAsType(Feature).type->exists(oclIsKindOf(Function))) implies   
    /// result.specializes(instantiatedType)specializes(instantiatedType)instantiatedType.oclIsKindOf(Behavior)
    /// orinstantiatedType.oclIsKindOf(Feature) and    instantiatedType.type->exists(oclIsKindOf(Behavior))
    /// and    instantiatedType.type->size(1)ownedFeature->forAll(f |    f <> result implies        
    /// f.direction = FeatureDirectionKind::_'in')
    /// </summary>
    public partial class InvocationExpression : IInvocationExpression
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvocationExpression"/> class.
        /// </summary>
        public InvocationExpression()
        {
            this.AliasIds = new List<string>();
            this.IsAbstract = false;
            this.IsComposite = false;
            this.IsConstant = false;
            this.IsDerived = false;
            this.IsEnd = false;
            this.IsImpliedIncluded = false;
            this.IsOrdered = false;
            this.IsPortion = false;
            this.IsSufficient = false;
            this.IsUnique = true;
            this.IsVariable = false;
            this.OwnedRelationship = new List<Guid>();
        }

        /// <summary>
        /// Gets or sets the unique identifier
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: false, isTransient: false, isUnsettable: false, isDerived: false, isOrdered: true, isUnique: true, lowerBound: 1, upperBound: 1, isMany: false, isRequired: false, isContainment: false)]
        public Guid Id { get; set; }

        /// <summary>
        /// Various alternative identifiers for this Element. Generally, these will be set by tools.
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: false, isTransient: false, isUnsettable: false, isDerived: false, isOrdered: true, isUnique: true, lowerBound: 0, upperBound: -1, isMany: false, isRequired: false, isContainment: false)]
        public List<string> AliasIds { get; set; }

        /// <summary>
        /// The declared name of this Element.
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: false, isTransient: false, isUnsettable: false, isDerived: false, isOrdered: false, isUnique: true, lowerBound: 0, upperBound: 1, isMany: false, isRequired: false, isContainment: false)]
        public string DeclaredName { get; set; }

        /// <summary>
        /// An optional alternative name for the Element that is intended to be shorter or in some way more
        /// succinct than its primary name. It may act as a modeler-specified identifier for the Element, though
        /// it is then the responsibility of the modeler to maintain the uniqueness of this identifier within a
        /// model or relative to some other context.
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: false, isTransient: false, isUnsettable: false, isDerived: false, isOrdered: false, isUnique: true, lowerBound: 0, upperBound: 1, isMany: false, isRequired: false, isContainment: false)]
        public string DeclaredShortName { get; set; }

        /// <summary>
        /// Indicates how values of this Feature are determined or used (as specified for the
        /// FeatureDirectionKind).
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: false, isTransient: false, isUnsettable: false, isDerived: false, isOrdered: false, isUnique: true, lowerBound: 0, upperBound: 1, isMany: false, isRequired: false, isContainment: false)]
        public FeatureDirectionKind? Direction { get; set; }

        /// <summary>
        /// The globally unique identifier for this Element. This is intended to be set by tooling, and it must
        /// not change during the lifetime of the Element.
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: false, isTransient: false, isUnsettable: false, isDerived: false, isOrdered: false, isUnique: true, lowerBound: 1, upperBound: 1, isMany: false, isRequired: false, isContainment: false)]
        public string ElementId { get; set; }

        /// <summary>
        /// Indicates whether instances of this Type must also be instances of at least one of its specialized
        /// Types.
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: false, isTransient: false, isUnsettable: false, isDerived: false, isOrdered: false, isUnique: true, lowerBound: 1, upperBound: 1, isMany: false, isRequired: false, isContainment: false)]
        public bool IsAbstract { get; set; }

        /// <summary>
        /// Whether the Feature is a composite feature of its featuringType. If so, the values of the Feature
        /// cannot exist after its featuring instance no longer does and cannot be values of another composite
        /// feature that is not on the same featuring instance.
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: false, isTransient: false, isUnsettable: false, isDerived: false, isOrdered: false, isUnique: true, lowerBound: 1, upperBound: 1, isMany: false, isRequired: false, isContainment: false)]
        public bool IsComposite { get; set; }

        /// <summary>
        /// If isVariable is true, then whether the value of this Feature nevertheless does not change over all
        /// snapshots of its owningType.
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: false, isTransient: false, isUnsettable: false, isDerived: false, isOrdered: false, isUnique: true, lowerBound: 1, upperBound: 1, isMany: false, isRequired: false, isContainment: false)]
        public bool IsConstant { get; set; }

        /// <summary>
        /// Whether the values of this Feature can always be computed from the values of other Features.
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: false, isTransient: false, isUnsettable: false, isDerived: false, isOrdered: false, isUnique: true, lowerBound: 1, upperBound: 1, isMany: false, isRequired: false, isContainment: false)]
        public bool IsDerived { get; set; }

        /// <summary>
        /// Whether or not this Feature is an end Feature. An end Feature always has multiplicity 1, mapping
        /// each of its domain instances to a single co-domain instance. However, it may have a crossFeature, in
        /// which case values of the crossFeature must be the same as those found by navigation across instances
        /// of the owningType from values of other end Features to values of this Feature. If the owningType has
        /// n end Features, then the multiplicity, ordering, and uniqueness declared for the crossFeature of any
        /// one of these end Features constrains the cardinality, ordering, and uniqueness of the collection of
        /// values of that Feature reached by navigation when the values of the other n-1 end Features are held
        /// fixed.
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: false, isTransient: false, isUnsettable: false, isDerived: false, isOrdered: false, isUnique: true, lowerBound: 1, upperBound: 1, isMany: false, isRequired: false, isContainment: false)]
        public bool IsEnd { get; set; }

        /// <summary>
        /// Whether all necessary implied Relationships have been included in the ownedRelationships of this
        /// Element. This property may be true, even if there are not actually any ownedRelationships with
        /// isImplied = true, meaning that no such Relationships are actually implied for this Element. However,
        /// if it is false, then ownedRelationships may not contain any implied Relationships. That is, either
        /// all required implied Relationships must be included, or none of them.
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: false, isTransient: false, isUnsettable: false, isDerived: false, isOrdered: false, isUnique: true, lowerBound: 1, upperBound: 1, isMany: false, isRequired: false, isContainment: false)]
        public bool IsImpliedIncluded { get; set; }

        /// <summary>
        /// Whether an order exists for the values of this Feature or not.
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: false, isTransient: false, isUnsettable: false, isDerived: false, isOrdered: false, isUnique: true, lowerBound: 1, upperBound: 1, isMany: false, isRequired: false, isContainment: false)]
        public bool IsOrdered { get; set; }

        /// <summary>
        /// Whether the values of this Feature are contained in the space and time of instances of the domain of
        /// the Feature and represent the same thing as those instances.
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: false, isTransient: false, isUnsettable: false, isDerived: false, isOrdered: false, isUnique: true, lowerBound: 1, upperBound: 1, isMany: false, isRequired: false, isContainment: false)]
        public bool IsPortion { get; set; }

        /// <summary>
        /// Whether all things that meet the classification conditions of this Type must be classified by the
        /// Type.(A Type gives conditions that must be met by whatever it classifies, but when isSufficient
        /// is false, things may meet those conditions but still not be classified by the Type. For example, a
        /// Type Car that is not sufficient could require everything it classifies to have four wheels, but not
        /// all four wheeled things would classify as cars. However, if the Type Car were sufficient, it would
        /// classify all four-wheeled things.)
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: false, isTransient: false, isUnsettable: false, isDerived: false, isOrdered: false, isUnique: true, lowerBound: 1, upperBound: 1, isMany: false, isRequired: false, isContainment: false)]
        public bool IsSufficient { get; set; }

        /// <summary>
        /// Whether or not values for this Feature must have no duplicates or not.
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: false, isTransient: false, isUnsettable: false, isDerived: false, isOrdered: false, isUnique: true, lowerBound: 1, upperBound: 1, isMany: false, isRequired: false, isContainment: false)]
        public bool IsUnique { get; set; }

        /// <summary>
        /// Whether the value of this Feature might vary over time. That is, whether the Feature may have a
        /// different value for each snapshot of an owningType that is an Occurrence.
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: false, isTransient: false, isUnsettable: false, isDerived: false, isOrdered: false, isUnique: true, lowerBound: 1, upperBound: 1, isMany: false, isRequired: false, isContainment: false)]
        public bool IsVariable { get; set; }

        /// <summary>
        /// The Relationships for which this Element is the owningRelatedElement.
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: false, isTransient: false, isUnsettable: false, isDerived: false, isOrdered: true, isUnique: true, lowerBound: 0, upperBound: -1, isMany: false, isRequired: false, isContainment: true)]
        public List<Guid> OwnedRelationship { get; set; }

        /// <summary>
        /// The Relationship for which this Element is an ownedRelatedElement, if any.
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: false, isTransient: false, isUnsettable: false, isDerived: false, isOrdered: false, isUnique: true, lowerBound: 0, upperBound: 1, isMany: false, isRequired: false, isContainment: false)]
        public Guid? OwningRelationship { get; set; }

    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
