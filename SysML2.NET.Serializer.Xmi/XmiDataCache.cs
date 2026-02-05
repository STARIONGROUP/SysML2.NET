// -------------------------------------------------------------------------------------------------
// <copyright file="XmiDataCache.cs" company="Starion Group S.A.">
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
    /// Represents a cache for storing and retrieving XMI <see cref="IData"/> during the reading process.
    /// </summary>
    public class XmiDataCache: IXmiDataCache
    {
        /// <summary>
        /// Gets the cached <see cref="Dictionary{TKey,TValue}"/> of read <see cref="IData"/>. The <see cref="IData.Id"/> is used as key,
        /// the read <see cref="IData"/> as value
        /// </summary>
        private readonly Dictionary<Guid, IData> cache = [];

        /// <summary>
        /// Gets the cached <see cref="Dictionary{TKey,TValue}"/> of read multiple values references per read <see cref="IData"/>
        /// </summary>
        private readonly Dictionary<Guid, Dictionary<string, List<Guid>>> multipleReferencesCache = [];
        
        /// <summary>
        /// Gets the cached <see cref="Dictionary{TKey,TValue}"/> of read single value reference per read <see cref="IData"/>
        /// </summary>
        private readonly Dictionary<Guid, Dictionary<string, Guid>> singleReferenceCache = [];
        
        /// <summary>
        /// Adds the specified <see cref="IData"/> to the cache using the <see cref="IData.Id"/> as key 
        /// </summary>
        /// <param name="data">The <see cref="IData"/> to be added to the cache</param>
        /// <returns>True if the provided <see cref="IData"/> was added, false if already present in the cache and could not
        /// be added again</returns>
        public bool TryAdd(IData data)
        {
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }

            if (this.cache.ContainsKey(data.Id))
            {
                return false;
            }
            
            this.cache[data.Id] = data;
            return true;
        }

        /// <summary>
        /// Adds read references for a specific <see cref="IData"/>
        /// </summary>
        /// <param name="dataId">The identifier <see cref="Guid"/> of the <see cref="IData"/> that is currently being read</param>
        /// <param name="propertyName">The name of multiple value reference property</param>
        /// <param name="references">Read references identifiers to add</param>
        public void AddMultipleValueReferencePropertyIdentifiers(Guid dataId, string propertyName, IReadOnlyCollection<Guid> references)
        {
            if (this.multipleReferencesCache.TryGetValue(dataId, out var existingMultipleReferencesValues))
            {
                if (existingMultipleReferencesValues.TryGetValue(propertyName, out var referencesValues))
                {
                    referencesValues.AddRange(references);
                }
                else
                {
                    existingMultipleReferencesValues[propertyName] = [..references];
                }
            }
            else
            {
                var multipleReferences = new Dictionary<string, List<Guid>>
                {
                    [propertyName] = [..references]
                };

                this.multipleReferencesCache[dataId] = multipleReferences;
            }
        }

        /// <summary>
        /// Adds a read reference for a specific <see cref="IData"/>
        /// </summary>
        /// <param name="dataId">The identifier <see cref="Guid"/> of the <see cref="IData"/> that is currently being read</param>
        /// <param name="propertyName">The name of multiple value reference property</param>
        /// <param name="reference">Read reference identifier to add</param>
        public void AddMultipleValueReferencePropertyIdentifiers(Guid dataId, string propertyName, Guid reference)
        {
            this.AddMultipleValueReferencePropertyIdentifiers(dataId, propertyName, [reference]);
        }

        /// <summary>
        /// Adds read reference for a specific <see cref="IData"/>
        /// </summary>
        /// <param name="dataId">The identifier <see cref="Guid"/> of the <see cref="IData"/> that is currently being read</param>
        /// <param name="propertyName">The name of multiple value reference property</param>
        /// <param name="reference">The read reference identifier</param>
        public void AddSingleValueReferencePropertyIdentifier(Guid dataId, string propertyName, Guid reference)
        {
            if (this.singleReferenceCache.TryGetValue(dataId, out var existingSingleReferenceValue))
            {
                existingSingleReferenceValue[propertyName] = reference;
            }
            else
            {
                var singleReference = new Dictionary<string,Guid>
                {
                    [propertyName] = reference
                };

                this.singleReferenceCache[dataId] = singleReference;
            }
        }
    }
}
