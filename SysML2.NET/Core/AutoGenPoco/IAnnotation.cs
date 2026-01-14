// -------------------------------------------------------------------------------------------------
// <copyright file="IAnnotation.cs" company="Starion Group S.A.">
//
//    Copyright (C) 2022-2025 Starion Group S.A.
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

namespace SysML2.NET.Core.POCO.Root.Annotations
{
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.Generic;

    using SysML2.NET.Core.POCO.Root.Elements;
    using SysML2.NET.Core.POCO.Root.Namespaces;
    using SysML2.NET.Decorators;

    /// <summary>
    /// An Annotation is a Relationship between an AnnotatingElement and the Element that is annotated by
    /// that AnnotatingElement.
    /// </summary>
    [Class(xmiId: "_18_5_3_12e503d9_1543093613150_792705_18263", isAbstract: false, isFinalSpecialization: false, isActive: false)]
    [GeneratedCode("SysML2.NET", "latest")]
    public partial interface IAnnotation : IRelationship
    {
        /// <summary>
        /// The Element that is annotated by the annotatingElement of this Annotation.
        /// </summary>
        [Property(xmiId: "_18_5_3_12e503d9_1543094430277_494140_18542", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [RedefinedProperty(propertyName: "_18_5_3_12e503d9_1533160674961_138197_43179")]
        IElement AnnotatedElement { get; set; }

        /// <summary>
        /// The AnnotatingElement that annotates the annotatedElement of this Annotation. This is always either
        /// the ownedAnnotatingElement or the owningAnnotatingElement.
        /// </summary>
        [Property(xmiId: "_18_5_3_12e503d9_1543094212714_638255_18408", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [RedefinedProperty(propertyName: "_18_5_3_12e503d9_1533160674971_696758_43228")]
        IAnnotatingElement annotatingElement { get; }

        /// <summary>
        /// The annotatingElement of this Annotation, when it is an ownedRelatedElement.
        /// </summary>
        [Property(xmiId: "_2022x_2_12e503d9_1735188506571_384269_375", aggregation: AggregationKind.Composite, lowerValue: 0, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1543094212714_638255_18408")]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1533160674986_59873_43302")]
        IAnnotatingElement ownedAnnotatingElement { get; }

        /// <summary>
        /// The annotatedElement of this Annotation, when it is also the owningRelatedElement.
        /// </summary>
        [Property(xmiId: "_19_0_2_12e503d9_1594152527165_104456_2501", aggregation: AggregationKind.None, lowerValue: 0, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1543094430277_494140_18542")]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1543092026091_693018_16749")]
        IElement owningAnnotatedElement { get; }

        /// <summary>
        /// The annotatingElement of this Annotation, when it is the owningRelatedElement.
        /// </summary>
        [Property(xmiId: "_19_0_4_12e503d9_1703019570939_266622_19", aggregation: AggregationKind.None, lowerValue: 0, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1543094212714_638255_18408")]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1543092026091_693018_16749")]
        IAnnotatingElement owningAnnotatingElement { get; }

    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
