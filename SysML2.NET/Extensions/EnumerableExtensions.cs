// ------------------------------------------------------------------------------------------------
// <copyright file="EnumerableExtensions.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
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
