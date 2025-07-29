// -------------------------------------------------------------------------------------------------
// <copyright file="IAnnotatingElement.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.Core.POCO
{
    using System;
    using System.Collections.Generic;

    using SysML2.NET.Core;
    using SysML2.NET.Decorators;

    /// <summary>
    /// An AnnotatingElement is an Element that provides additional description of or metadata on some other
    /// Element. An AnnotatingElement is either attached to its annotatedElements by Annotation
    /// Relationships, or it implicitly annotates its owningNamespace.annotatedElement =  if
    /// annotation->notEmpty() then annotation.annotatedElement else Sequence{owningNamespace}
    /// endifownedAnnotatingRelationship = ownedRelationship->    selectByKind(Annotation)->    select(a |
    /// a.annotatedElement <> self)annotation =     if owningAnnotatingRelationship = null then
    /// ownedAnnotatingRelationship    else
    /// owningAnnotatingRelationship->prepend(owningAnnotatingRelationship)    endif
    /// </summary>
    public partial interface IAnnotatingElement : IElement
    {
        /// <summary>
        /// Queries the derived property AnnotatedElement
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: true, isTransient: true, isUnsettable: false, isDerived: true, isOrdered: true, isUnique: true, lowerBound: 1, upperBound: -1, isMany: false, isRequired: false, isContainment: false)]
        List<IElement> QueryAnnotatedElement();

        /// <summary>
        /// Queries the derived property Annotation
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: true, isTransient: true, isUnsettable: false, isDerived: true, isOrdered: true, isUnique: true, lowerBound: 0, upperBound: -1, isMany: false, isRequired: false, isContainment: false)]
        List<Annotation> QueryAnnotation();

        /// <summary>
        /// Queries the derived property OwnedAnnotatingRelationship
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: true, isTransient: true, isUnsettable: false, isDerived: true, isOrdered: true, isUnique: true, lowerBound: 0, upperBound: -1, isMany: false, isRequired: false, isContainment: false)]
        List<Annotation> QueryOwnedAnnotatingRelationship();

        /// <summary>
        /// Queries the derived property OwningAnnotatingRelationship
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: true, isTransient: true, isUnsettable: false, isDerived: true, isOrdered: false, isUnique: true, lowerBound: 0, upperBound: 1, isMany: false, isRequired: false, isContainment: false)]
        Annotation QueryOwningAnnotatingRelationship();

    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
