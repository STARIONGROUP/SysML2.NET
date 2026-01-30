// -------------------------------------------------------------------------------------------------
// <copyright file="RelationshipExtensions.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.Core.POCO.Root.Elements
{
    using System;
    using System.Collections.Generic;

    using SysML2.NET.Core.POCO.Root.Annotations;
    using SysML2.NET.Core.POCO.Root.Namespaces;

    /// <summary>
    /// The <see cref="RelationshipExtensions"/> class provides extensions methods for
    /// the <see cref="IRelationship"/> interface
    /// </summary>
    internal static class RelationshipExtensions
    {
        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <param name="relationship">
        /// The subject <see cref="IRelationship"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        internal static List<IElement> ComputeRelatedElement(this IRelationship relationship)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }

    }
}
