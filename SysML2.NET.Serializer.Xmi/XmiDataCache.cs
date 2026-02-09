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
        /// Initializes a new instance of <see cref="XmiDataCache"/>
        /// </summary>
        /// <param name="logger">the injected <see cref="ILogger{TCategoryName}"/> used to produce logs</param>
        public XmiDataCache(ILogger<XmiDataCache> logger)
        {
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
                    if (!this.TryGetData(existingReference.Value, out var referencedData) && this.logger.IsEnabled(LogLevel.Warning))
                    {
                        this.logger.LogWarning("The reference to [{Reference}] for property [{Key}] on element type [{Element}] with id [{Id}] was not found in the cache, probably because its type is not supported.",
                            existingReference.Value, existingReference.Key, data.GetType().Name, data.Id);

                        continue;
                    }

                    var targetProperty = FindPropertyWithAttribute(data, existingReference.Key, referencedData.GetType());

                    if (targetProperty is null)
                    {
                        throw new InvalidOperationException($"The target property {existingReference.Key} was not found on {data.GetType().Name} or the property type doesn't match the referenced {nameof(data)} type");
                    }

                    targetProperty.SetValue(data, referencedData);
                }
            }

            if (!this.multipleReferencesCache.TryGetValue(data.Id, out var existingMultipleReferenceValues))
            {
                return;
            }

            foreach (var existingReference in existingMultipleReferenceValues)
            {
                var targetProperty = FindPropertyWithAttribute(data, existingReference.Key);
                var underlyingType = targetProperty?.PropertyType.GetGenericArguments().FirstOrDefault();

                if (targetProperty is null || underlyingType is null)
                {
                    throw new KeyNotFoundException($"The target property {existingReference.Key} was not found on {data.GetType().Name} or the type is null");
                }

                var resolvedReferences = this.ResolveMultiValueReferences(existingReference.Value, existingReference.Key, underlyingType);

                if (targetProperty.GetValue(data) is not IList list)
                {
                    continue;
                }

                foreach (var resolvedReference in resolvedReferences)
                {
                    list.Add(resolvedReference);
                }
            }
        }

        /// <summary>
        /// Resolved multi-values reference property
        /// </summary>
        /// <param name="referencedValues">A readonly collection of <see cref="Guid"/> idenfying referenced-values</param>
        /// <param name="propertyName">The name of the current property that are being resolved</param>
        /// <param name="underlyingType">The expected type</param>
        /// <returns>A collection of found referenced values</returns>
        private IReadOnlyList<IData> ResolveMultiValueReferences(IReadOnlyCollection<Guid> referencedValues, string propertyName, Type underlyingType)
        {
            var resolvedReferences = new List<IData>();

            foreach (var referencedValue in referencedValues)
            {
                if (!this.TryGetData(referencedValue, out var data) || !underlyingType.IsInstanceOfType(data))
                {
                    this.logger.LogWarning("The reference with the id [{Key}] to [{PropertyValue}] was not found in the cache, probably because its type is not supported.", propertyName, referencedValue);
                    continue;
                }

                resolvedReferences.Add(data);
            }

            return resolvedReferences;
        }

        /// <summary>
        /// Finds a property in the given element that has the <see cref="PropertyAttribute"/> and matches the specified property name and type.
        /// </summary>
        /// <param name="data">The <see cref="IData"/> whose properties are to be searched.</param>
        /// <param name="propertyName">The name of the property to find.</param>
        /// <param name="expectedType">The expected type of the property.  If null, type checking is skipped.</param>
        /// <returns>The <see cref="PropertyInfo"/> of the found property, or null if no matching property is found.</returns>
        private static PropertyInfo FindPropertyWithAttribute(IData data, string propertyName, Type expectedType = null)
        {
            return data.GetType().GetProperties()
                .FirstOrDefault(x => Attribute.IsDefined(x, typeof(PropertyAttribute))
                                     && x.Name.Equals(propertyName, StringComparison.InvariantCultureIgnoreCase)
                                     && (expectedType == null || x.PropertyType.IsAssignableFrom(expectedType)));
        }
    }
}
