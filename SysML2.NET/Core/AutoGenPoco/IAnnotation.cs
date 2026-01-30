// -------------------------------------------------------------------------------------------------
// <copyright file="IAnnotation.cs" company="Starion Group S.A.">
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
    [Class(xmiId: "Root-Annotations-Annotation", isAbstract: false, isFinalSpecialization: false, isActive: false)]
    [GeneratedCode("SysML2.NET", "latest")]
    public partial interface IAnnotation : IRelationship
    {
        /// <summary>
        /// The Element that is annotated by the annotatingElement of this Annotation.
        /// </summary>
        [Property(xmiId: "Root-Annotations-Annotation-annotatedElement", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [RedefinedProperty(propertyName: "Root-Elements-Relationship-target")]
        IElement AnnotatedElement { get; set; }

        /// <summary>
        /// The AnnotatingElement that annotates the annotatedElement of this Annotation. This is always either
        /// the ownedAnnotatingElement or the owningAnnotatingElement.
        /// </summary>
        [Property(xmiId: "Root-Annotations-Annotation-annotatingElement", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [RedefinedProperty(propertyName: "Root-Elements-Relationship-source")]
        IAnnotatingElement annotatingElement { get; }

        /// <summary>
        /// The annotatingElement of this Annotation, when it is an ownedRelatedElement.
        /// </summary>
        [Property(xmiId: "Root-Annotations-Annotation-ownedAnnotatingElement", aggregation: AggregationKind.None, lowerValue: 0, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Root-Annotations-Annotation-annotatingElement")]
        [SubsettedProperty(propertyName: "Root-Elements-Relationship-ownedRelatedElement")]
        IAnnotatingElement ownedAnnotatingElement { get; }

        /// <summary>
        /// The annotatedElement of this Annotation, when it is also the owningRelatedElement.
        /// </summary>
        [Property(xmiId: "Root-Annotations-Annotation-owningAnnotatedElement", aggregation: AggregationKind.None, lowerValue: 0, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Root-Annotations-Annotation-annotatedElement")]
        [SubsettedProperty(propertyName: "Root-Elements-Relationship-owningRelatedElement")]
        IElement owningAnnotatedElement { get; }

        /// <summary>
        /// The annotatingElement of this Annotation, when it is the owningRelatedElement.
        /// </summary>
        [Property(xmiId: "Root-Annotations-Annotation-owningAnnotatingElement", aggregation: AggregationKind.None, lowerValue: 0, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Root-Annotations-Annotation-annotatingElement")]
        [SubsettedProperty(propertyName: "Root-Elements-Relationship-owningRelatedElement")]
        IAnnotatingElement owningAnnotatingElement { get; }

    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
