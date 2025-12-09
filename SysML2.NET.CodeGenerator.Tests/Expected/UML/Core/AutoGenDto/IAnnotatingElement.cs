// -------------------------------------------------------------------------------------------------
// <copyright file="IAnnotatingElement.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.Core.DTO
{
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.Generic;


    using SysML2.NET.Decorators;

    /// <summary>
    /// An AnnotatingElement is an Element that provides additional description of or metadata on some other
    /// Element. An AnnotatingElement is either attached to its annotatedElements by Annotation
    /// Relationships, or it implicitly annotates its owningNamespace.
    /// </summary>
    [Class(xmiId: "_19_0_2_12e503d9_1594145576693_532940_27", isAbstract: false, isFinalSpecialization: false, isActive: false)]
    [GeneratedCode("SysML2.NET", "latest")]
    public partial interface IAnnotatingElement : IElement
    {
        /// <summary>
        /// The Elements that are annotated by this AnnotatingElement. If annotation is not empty, these are the
        /// annotatedElements of the annotations. If annotation is empty, then it is the owningNamespace of the
        /// AnnotatingElement.
        /// </summary>
        [Property(xmiId: "_19_0_2_12e503d9_1594145755058_99428_86", aggregation: AggregationKind.None, lowerValue: 1, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        List<Guid> AnnotatedElement { get; }

        /// <summary>
        /// The Annotations that relate this AnnotatingElement to its annotatedElements. This includes the
        /// owningAnnotatingRelationship (if any) followed by all the ownedAnnotatingRelationshps.
        /// </summary>
        [Property(xmiId: "_18_5_3_12e503d9_1543094212714_953084_18407", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1533160674971_80547_43227")]
        List<Guid> Annotation { get; }

        /// <summary>
        /// The ownedRelationships of this AnnotatingElement that are Annotations, for which this
        /// AnnotatingElement is the annotatingElement.
        /// </summary>
        [Property(xmiId: "_19_0_4_12e503d9_1703019570915_375100_18", aggregation: AggregationKind.Composite, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1543094212714_953084_18407")]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1543092026091_217766_16748")]
        List<Guid> OwnedAnnotatingRelationship { get; }

        /// <summary>
        /// The owningRelationship of this AnnotatingRelationship, if it is an Annotation
        /// </summary>
        [Property(xmiId: "_2022x_2_12e503d9_1735188506571_308678_376", aggregation: AggregationKind.None, lowerValue: 0, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1533160674986_482273_43303")]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1543094212714_953084_18407")]
        Guid? OwningAnnotatingRelationship { get; }

    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
