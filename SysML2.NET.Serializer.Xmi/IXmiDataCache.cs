// -------------------------------------------------------------------------------------------------
// <copyright file="IXmiDataCache.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.Serializer.Xmi
{
    using System;
    using System.Collections.Generic;

    using SysML2.NET.Common;

    /// <summary>
    /// Represents a cache for storing and retrieving XMI <see cref="IData"/> during the reading process and defined references.
    /// </summary>
    public interface IXmiDataCache
    {
        /// <summary>
        /// Adds the specified <see cref="IData"/> to the cache using the <see cref="IData.Id"/> as key 
        /// </summary>
        /// <param name="data">The <see cref="IData"/> to be added to the cache</param>
        /// <returns>True if the provided <see cref="IData"/> was added, false if already present in the cache and could not
        /// be added again</returns>
        bool TryAdd(IData data);

        /// <summary>
        /// Adds read references for a specific <see cref="IData"/>
        /// </summary>
        /// <param name="dataId">The identifier <see cref="Guid"/> of the <see cref="IData"/> that is currently being read</param>
        /// <param name="propertyName">The name of multiple value reference property</param>
        /// <param name="references">Read references identifiers to add</param>
        void AddMultipleValueReferencePropertyIdentifiers(Guid dataId, string propertyName, IReadOnlyCollection<Guid> references);
        
        /// <summary>
        /// Adds a read reference for a specific <see cref="IData"/>
        /// </summary>
        /// <param name="dataId">The identifier <see cref="Guid"/> of the <see cref="IData"/> that is currently being read</param>
        /// <param name="propertyName">The name of multiple value reference property</param>
        /// <param name="reference">Read reference identifier to add</param>
        void AddMultipleValueReferencePropertyIdentifiers(Guid dataId, string propertyName, Guid reference);
        
        /// <summary>
        /// Adds read reference for a specific <see cref="IData"/>
        /// </summary>
        /// <param name="dataId">The identifier <see cref="Guid"/> of the <see cref="IData"/> that is currently being read</param>
        /// <param name="propertyName">The name of multiple value reference property</param>
        /// <param name="reference">The read reference identifier</param>
        void AddSingleValueReferencePropertyIdentifier(Guid dataId, string propertyName, Guid reference);
        
        /// <summary>
        /// Tries to get a cached <see cref="IData"/> from its identifier
        /// </summary>
        /// <param name="dataId">The <see cref="Guid"/> identifier</param>
        /// <param name="data">The retrieved <see cref="IData"/> if applicable</param>
        /// <returns>True if the <see cref="IData"/> could was present in the cache and could be retrieved</returns>
        bool TryGetData(Guid dataId, out IData data);
    }
}
