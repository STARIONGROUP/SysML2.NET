// -------------------------------------------------------------------------------------------------
// <copyright file="IAssociation.cs" company="Starion Group S.A.">
//
//    Copyright (C) 2022-2026 Starion Group S.A.
//
//    Licensed under the Apache License, Version 2.0 (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
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

namespace SysML2.NET.Core.DTO.Kernel.Associations
{
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.Generic;

    using SysML2.NET.Core.DTO.Core.Classifiers;
    using SysML2.NET.Core.DTO.Root.Elements;
    using SysML2.NET.Decorators;

    /// <summary>
    /// An Association is a Relationship and a Classifier to enable classification of links between things
    /// (in the universe). The co-domains (types) of the associationEnd Features are the relatedTypes, as
    /// co-domain and participants (linked things) of an Association identify each other.
    /// </summary>
    [Class(xmiId: "Kernel-Associations-Association", isAbstract: false, isFinalSpecialization: false, isActive: false)]
    [GeneratedCode("SysML2.NET", "latest")]
    public partial interface IAssociation : IClassifier, IRelationship
    {
        /// <summary>
        /// The features of the Association that identify the things that can be related by it. A concrete
        /// Association must have at least two associationEnds. When it has exactly two, the Association is
        /// called a binary Association.
        /// </summary>
        [Property(xmiId: "Kernel-Associations-Association-associationEnd", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [RedefinedProperty(propertyName: "Core-Types-Type-endFeature")]
        List<Guid> associationEnd { get; }

        /// <summary>
        /// The types of the associationEnds of the Association, which are the relatedElements of the
        /// Association considered as a Relationship.
        /// </summary>
        [Property(xmiId: "Kernel-Associations-Association-relatedType", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: false, defaultValue: null)]
        [RedefinedProperty(propertyName: "Root-Elements-Relationship-relatedElement")]
        List<Guid> relatedType { get; }

        /// <summary>
        /// The source relatedType for this Association. It is the first relatedType of the Association.
        /// </summary>
        [Property(xmiId: "Kernel-Associations-Association-sourceType", aggregation: AggregationKind.None, lowerValue: 0, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Kernel-Associations-Association-relatedType")]
        [RedefinedProperty(propertyName: "Root-Elements-Relationship-source")]
        Guid? sourceType { get; }

        /// <summary>
        /// The target relatedTypes for this Association. This includes all the relatedTypes other than the
        /// sourceType.
        /// </summary>
        [Property(xmiId: "Kernel-Associations-Association-targetType", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Kernel-Associations-Association-relatedType")]
        [RedefinedProperty(propertyName: "Root-Elements-Relationship-target")]
        List<Guid> targetType { get; }

    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
