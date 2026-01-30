// -------------------------------------------------------------------------------------------------
// <copyright file="IAnnotatingElement.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.Core.DTO.Root.Annotations
{
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.Generic;

    using SysML2.NET.Core.DTO.Root.Elements;
    using SysML2.NET.Decorators;

    /// <summary>
    /// An AnnotatingElement is an Element that provides additional description of or metadata on some other
    /// Element. An AnnotatingElement is either attached to its annotatedElements by Annotation
    /// Relationships, or it implicitly annotates its owningNamespace.
    /// </summary>
    [Class(xmiId: "Root-Annotations-AnnotatingElement", isAbstract: false, isFinalSpecialization: false, isActive: false)]
    [GeneratedCode("SysML2.NET", "latest")]
    public partial interface IAnnotatingElement : IElement
    {
        /// <summary>
        /// The Elements that are annotated by this AnnotatingElement. If annotation is not empty, these are the
        /// annotatedElements of the annotations. If annotation is empty, then it is the owningNamespace of the
        /// AnnotatingElement.
        /// </summary>
        [Property(xmiId: "Root-Annotations-AnnotatingElement-annotatedElement", aggregation: AggregationKind.None, lowerValue: 1, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        List<Guid> annotatedElement { get; }

        /// <summary>
        /// The Annotations that relate this AnnotatingElement to its annotatedElements. This includes the
        /// owningAnnotatingRelationship (if any) followed by all the ownedAnnotatingRelationshps.
        /// </summary>
        [Property(xmiId: "Root-Annotations-AnnotatingElement-annotation", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Root-Elements-A_source_sourceRelationship-sourceRelationship")]
        List<Guid> annotation { get; }

        /// <summary>
        /// The ownedRelationships of this AnnotatingElement that are Annotations, for which this
        /// AnnotatingElement is the annotatingElement.
        /// </summary>
        [Property(xmiId: "Root-Annotations-AnnotatingElement-ownedAnnotatingRelationship", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Root-Annotations-AnnotatingElement-annotation")]
        [SubsettedProperty(propertyName: "Root-Elements-Element-ownedRelationship")]
        List<Guid> ownedAnnotatingRelationship { get; }

        /// <summary>
        /// The owningRelationship of this AnnotatingRelationship, if it is an Annotation
        /// </summary>
        [Property(xmiId: "Root-Annotations-AnnotatingElement-owningAnnotatingRelationship", aggregation: AggregationKind.None, lowerValue: 0, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Root-Elements-Element-owningRelationship")]
        [SubsettedProperty(propertyName: "Root-Annotations-AnnotatingElement-annotation")]
        Guid? owningAnnotatingRelationship { get; }

    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
