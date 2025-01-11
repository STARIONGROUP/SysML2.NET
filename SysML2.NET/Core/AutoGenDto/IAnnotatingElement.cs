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

namespace SysML2.NET.Core.DTO
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
    /// a.annotatingElement = self)
    /// </summary>
    public partial interface IAnnotatingElement : IElement
    {
        /// <summary>
        /// The Annotations that relate this AnnotatingElement to its annotatedElements.
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: false, isTransient: false, isUnsettable: false, isDerived: false, isOrdered: true, isUnique: true, lowerBound: 0, upperBound: -1, isMany: false, isRequired: false, isContainment: false)]
        List<Guid> Annotation { get; set; }

    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
