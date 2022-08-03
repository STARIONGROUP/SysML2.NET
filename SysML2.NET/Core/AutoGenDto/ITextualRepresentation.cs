// -------------------------------------------------------------------------------------------------
// <copyright file="ITextualRepresentation.cs" company="RHEA System S.A.">
//
//   Copyright 2022 RHEA System S.A.
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
    /// A TextualRepresentation is an AnnotatingElement whose body represents the representedElement in a
    /// given language. The representedElement must be the owner of the TextualRepresentation. The named
    /// language can be a natural language, in which case the body is an informal representation, or an
    /// artifical language, in which case the body is expected to be a formal, machine-parsable
    /// representation.
    /// </summary>
    public partial interface ITextualRepresentation : IAnnotatingElement
    {
        /// <summary>
        /// The annotation text for the Comment.
        /// </summary>
        string Body { get; set; }

        /// <summary>
        /// The natural or artifical language in which the body text is written.
        /// </summary>
        string Language { get; set; }

    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
