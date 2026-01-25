// -------------------------------------------------------------------------------------------------
// <copyright file="Payload.cs" company="Starion Group S.A.">
//
//    Copyright 2022-2026 Starion Group S.A.
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

/* ------------------------------------------- |
 | index | property name                       |
 | ------------------------------------------- |
 |  0    | Created                             |
 | ------------------------------------------- |
 | 1     | AnnotatingElement                   |
 * ------------------------------------------- | */

namespace SysML2.NET.Serializer.MessagePack.Core
{
    using System;
    using System.Collections.Generic;

    using NET.Core.DTO.Root.Annotations;

    /// <summary>
    /// The <see cref="Payload"/> acts as envelope around the Core DTO classes and is used as
    /// construct to transport the objects using MessagePack
    /// </summary>
    internal class Payload
    {
        /// <summary>
        /// Gets or sets the <see cref="DateTime"/> at which the <see cref="Payload"/> was created
        /// </summary>
        internal DateTime Created { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Gets or sets the list of <see cref="AnnotatingElement"/>.
        /// </summary>
        internal List<AnnotatingElement> AnnotatingElement { get; set; } = [];
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------