// -------------------------------------------------------------------------------------------------
// <copyright file="EnumerableExtensions.cs" company="Starion Group S.A.">
// 
//   Copyright 2022-2026 Starion Group S.A.
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

namespace SysML2.NET.Extensions
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    using SysML2.NET.Core.POCO.Root.Elements;

    /// <summary>
    /// Extension methods for the <see cref="IEnumerable" /> interface
    /// </summary>
    internal static class EnumerableExtensions
    {
        /// <summary>
        /// Filters out all <see cref="IElement" /> where the type matches one of the requested
        /// </summary>
        /// <param name="collection">The collection to filters</param>
        /// <param name="elementTypes">A collection of <see cref="Type" /> that should be used for filtering</param>
        /// <returns>A collection of <see cref="IEnumerable{T}" /></returns>
        internal static IEnumerable<IElement> GetElementsOfType(this IEnumerable collection, params Type[] elementTypes)
        {
            return from object element in collection where elementTypes.Contains(element.GetType()) select element as IElement;
        }
    }
}
