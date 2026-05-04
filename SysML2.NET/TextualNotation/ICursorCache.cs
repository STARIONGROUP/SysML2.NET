// -------------------------------------------------------------------------------------------------
// <copyright file="ICursorCache.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.TextualNotation
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
