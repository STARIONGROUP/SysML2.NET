// -------------------------------------------------------------------------------------------------
// <copyright file="XmiDataCache.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.Serializer.Xmi
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    using Microsoft.Extensions.Logging;

    using SysML2.NET.Common;
    using SysML2.NET.Decorators;
    using SysML2.NET.Serializer.Xmi.Extensions;

    /// <summary>
    /// Represents a cache for storing and retrieving XMI <see cref="IData"/> during the reading process.
    /// </summary>
    public class XmiDataCache : IXmiDataCache
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
        /// Gets the injected <see cref="ILogger{TCategoryName}"/> used to produce logs
        /// </summary>
        private readonly ILogger<XmiDataCache> logger;

        /// <summary>
        /// Gets the injected <see cref="IPocoReferenceResolveExtensionsFacade" /> to provide generic access to POCO extensions
        /// </summary>
        private readonly IPocoReferenceResolveExtensionsFacade pocoReferenceResolveExtensionsFacade;

        /// <summary>
        /// Initializes a new instance of <see cref="XmiDataCache"/>
        /// </summary>
        /// <param name="pocoReferenceResolveExtensionsFacade">the injected <see cref="IPocoReferenceResolveExtensionsFacade" /> to provide generic access to POCO extensions</param>
        /// <param name="logger">the injected <see cref="ILogger{TCategoryName}"/> used to produce logs</param>
        public XmiDataCache(IPocoReferenceResolveExtensionsFacade pocoReferenceResolveExtensionsFacade, ILogger<XmiDataCache> logger)
        {
            this.pocoReferenceResolveExtensionsFacade = pocoReferenceResolveExtensionsFacade;
            this.logger = logger;
        }

        /// <summary>
        /// Adds the specified <see cref="IData"/> to the cache using the <see cref="IData.Id"/> as key 
        /// </summary>
        /// <param name="data">The <see cref="IData"/> to be added to the cache</param>
        /// <returns>True if the provided <see cref="IData"/> was added, false if already present in the cache and could not
        /// be added again</returns>
        public bool TryAdd(IData data)
        {
            return data == null ? throw new ArgumentNullException(nameof(data)) : this.cache.TryAdd(data.Id, data);
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
                var singleReference = new Dictionary<string, Guid>
                {
                    [propertyName] = reference
                };

                this.singleReferenceCache[dataId] = singleReference;
            }
        }

        /// <summary>
        /// Tries to get a cached <see cref="IData"/> from its identifier
        /// </summary>
        /// <param name="dataId">The <see cref="Guid"/> identifier</param>
        /// <param name="data">The retrieved <see cref="IData"/> if applicable</param>
        /// <returns>True if the <see cref="IData"/> could was present in the cache and could be retrieved</returns>
        public bool TryGetData(Guid dataId, out IData data)
        {
            data = null;
            return this.cache.TryGetValue(dataId, out data);
        }

        /// <summary>
        /// Synchronize references that has been stored into the cache
        /// </summary>
        public void SynchronizeReferences()
        {
            foreach (var data in this.cache.Values)
            {
                this.ResolveReferences(data);
            }
        }

        /// <summary>
        /// Resolve all references data for a given <see cref="IData"/>
        /// </summary>
        /// <param name="data">The <see cref="IData"/> that should resolve his reference</param>
        /// <exception cref="ArgumentNullException">If the <paramref name="data"/> is null</exception>
        /// <exception cref="InvalidOperationException">If one of the target property set on the <see cref="singleReferenceCache"/> or <see cref="multipleReferencesCache"/> is not found</exception>
        /// <exception cref="KeyNotFoundException">If an expected generic collection can not found</exception>
        private void ResolveReferences(IData data)
        {
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }

            if (this.singleReferenceCache.TryGetValue(data.Id, out var existingSingleReferenceValue))
            {
                foreach (var existingReference in existingSingleReferenceValue)
                {
                    this.pocoReferenceResolveExtensionsFacade.ResolveAndAssignSingleValueReference(data, existingReference.Key, existingReference.Value, this, this.logger);
                }
            }

            if (!this.multipleReferencesCache.TryGetValue(data.Id, out var existingMultipleReferenceValues))
            {
                return;
            }

            foreach (var existingReference in existingMultipleReferenceValues)
            {
                this.pocoReferenceResolveExtensionsFacade.ResolveAndAssignMultipleValueReferences(data, existingReference.Key, existingReference.Value, this, this.logger);
            }
        }
    }
}
