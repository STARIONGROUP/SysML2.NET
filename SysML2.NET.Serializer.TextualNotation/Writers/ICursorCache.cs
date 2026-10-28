// ------------------------------------------------------------------------------------------------
// <copyright file="ICursorCache.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.Serializer.TextualNotation.Writers
{
    using System;
    using System.Collections.Generic;

    using SysML2.NET.Core.POCO.Root.Elements;

    /// <summary>
    /// The <see cref="ICursorCache"/> provides caching and new cursor creation feature 
    /// </summary>
    public interface ICursorCache: IDisposable
    {
        /// <summary>
        /// Gets or create a new cursor an <see cref="IElement"/> identified by its <see cref="Guid"/> based on collection identified by the given <paramref name="collectionName" />.
        /// </summary>
        /// <param name="elementId">The <see cref="Guid"/> of the <see cref="IElement"/></param>
        /// <param name="collectionName">The name the related collection</param>
        /// <param name="elements">The collection of <see cref="IElement"/> that is used if the <see cref="CollectionCursor{T}"/> is not yet created</param>
        /// <returns>The <see cref="CollectionCursor{T}"/></returns>
        CollectionCursor<IElement> GetOrCreateCursor(Guid elementId, string collectionName, IReadOnlyList<IElement> elements);
    }
}
