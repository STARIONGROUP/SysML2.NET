// -------------------------------------------------------------------------------------------------
// <copyright file="IComment.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.Core.DTO.Root.Annotations
{
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.Generic;

    using SysML2.NET.Decorators;

    /// <summary>
    /// A Comment is an AnnotatingElement whose body in some way describes its annotatedElements.
    /// </summary>
    [Class(xmiId: "_18_5_3_12e503d9_1533160651696_959404_42181", isAbstract: false, isFinalSpecialization: false, isActive: false)]
    [GeneratedCode("SysML2.NET", "latest")]
    public partial interface IComment : IAnnotatingElement
    {
        /// <summary>
        /// The annotation text for the Comment.
        /// </summary>
        [Property(xmiId: "_18_5_3_12e503d9_1533160674981_840045_43277", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        string Body { get; set; }

        /// <summary>
        /// Identification of the language of the body text and, optionally, the region and/or encoding. The
        /// format shall be a POSIX locale conformant to ISO/IEC 15897, with the format
        /// [language[_territory][.codeset][@modifier]].
        /// </summary>
        [Property(xmiId: "_19_0_4_12e503d9_1647722682836_708148_649", aggregation: AggregationKind.None, lowerValue: 0, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        string Locale { get; set; }

    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
