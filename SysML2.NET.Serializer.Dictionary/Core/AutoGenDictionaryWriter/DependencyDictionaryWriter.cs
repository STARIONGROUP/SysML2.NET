// -------------------------------------------------------------------------------------------------
// <copyright file="DependencyDictionaryWriter.cs" company="RHEA System S.A.">
//
//   Copyright 2022-2024 RHEA System S.A.
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

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.Serializer.Dictionary.Core.DTO
{
    using System;
    using System.Collections.Generic;

    using SysML2.NET.Common;
    using SysML2.NET.Core.DTO;

    /// <summary>
    /// The purpose of the <see cref="DependencyDictionaryWriter"/> is to write (convert) a <see cref="IDependency"/>
    /// to a <see cref="Dictionary{String, Object}"/>.
    /// </summary>
    public static class DependencyDictionaryWriter
    {
        /// <summary>
        /// Writes a <see cref="IDependency"/> to a <see cref="Dictionary{String, Object}"/> that contains a key-value-pair
        /// for each property. The type key is used to store type information (name of the Type).
        /// </summary>
        /// <param name="dataItem">
        /// The subject <see cref="IDependency"/> that is to be written to a <see cref="Dictionary{String, Object}"/>.
        /// </param>
        /// <param name="dictionaryKind">
        /// The target <see cref="DictionaryKind"/> that is to be created
        /// </param>
        /// <returns>
        /// An instance of <see cref="Dictionary{String, Object}"/> that contains all the properties as key-value-pairs as well
        /// as a type key that is used to store type information (name of the Type).
        /// </returns>
        /// <remarks>
        /// When the <see cref="DictionaryKind"/> is <see cref="DictionaryKind.Complex"/> then the values are written to the
        /// <see cref="Dictionary{String, Object}"/> as is. When the <see cref="DictionaryKind"/> is <see cref="DictionaryKind.Simplified"/>
        /// then the following applies:
        /// The values that are of the following types are stored as is:
        ///   - Number, an abstract type, which has the subtypes Integer and Float
        ///   - String
        ///   - Boolean
        ///   - The spatial type Point
        ///   - Temporal types: Date, Time, LocalTime, DateTime, LocalDateTime and Duration
        /// values of other types are converted to string, in case these are an <see cref="IEnumerable{T}"/> then
        /// the values are converted to an Array of String using JSON notation, i.e. [ value_1, ..., value_n ]
        /// </remarks>
        public static Dictionary<string, object> Write(IData dataItem, DictionaryKind dictionaryKind)
        {
            var dependencyInstance = ThingNullAndTypeCheck(dataItem);

            switch (dictionaryKind)
            {
                case DictionaryKind.Complex:
                    return WriteComplex(dependencyInstance);
                case DictionaryKind.Simplified:
                    return WriteSimplified(dependencyInstance);
                default:
                    throw new NotSupportedException($"The dictionaryKind:{dictionaryKind} is not supported");
            }
        }

        /// <summary>
        /// Writes a <see cref="IDependency"/> to a <see cref="Dictionary{String, Object}"/> that contains a key-value-pair
        /// for each property. The type key is used to store type information (name of the Type).
        /// </summary>
        /// <param name="dependencyInstance">
        /// The subject <see cref="IDependency"/> that is to be written to a <see cref="Dictionary{String, Object}"/>.
        /// </param>
        /// <returns>
        /// An instance of <see cref="Dictionary{String, Object}"/> that contains all the properties as key-value-pairs as well
        /// as a type key that is used to store type information (name of the Type).
        /// </returns>
        /// <remarks>
        /// The values that are of the following types are stored as is:
        ///   - Number, an abstract type, which has the subtypes Integer and Float
        ///   - String
        ///   - Boolean
        ///   - The spatial type Point
        ///   - Temporal types: Date, Time, LocalTime, DateTime, LocalDateTime and Duration
        /// values of other types are converted to string, in case these are an <see cref="IEnumerable{T}"/> then
        /// the values are converted to an Array of String using JSON notation, i.e. [ value_1, ..., value_n ]
        /// </remarks>
        private static Dictionary<string, object> WriteSimplified(IDependency dependencyInstance)
        {
            var dictionary = new Dictionary<string, object>
            {
                { "@type", "Dependency" },
                { "@id", dependencyInstance.Id.ToString() }
            };

            dictionary.Add("aliasIds", dependencyInstance.AliasIds);
            dictionary.Add("client", $"[ {string.Join(",", dependencyInstance.Client)} ]");
            dictionary.Add("declaredName", dependencyInstance.DeclaredName);
            dictionary.Add("declaredShortName", dependencyInstance.DeclaredShortName);
            dictionary.Add("elementId", dependencyInstance.ElementId);
            dictionary.Add("isImplied", dependencyInstance.IsImplied);
            dictionary.Add("isImpliedIncluded", dependencyInstance.IsImpliedIncluded);
            dictionary.Add("ownedRelatedElement", $"[ {string.Join(",", dependencyInstance.OwnedRelatedElement)} ]");
            dictionary.Add("ownedRelationship", $"[ {string.Join(",", dependencyInstance.OwnedRelationship)} ]");
            dictionary.Add("owningRelatedElement", dependencyInstance.OwningRelatedElement.ToString());
            dictionary.Add("owningRelationship", dependencyInstance.OwningRelationship.ToString());
            dictionary.Add("source", $"[ {string.Join(",", dependencyInstance.Source)} ]");
            dictionary.Add("supplier", $"[ {string.Join(",", dependencyInstance.Supplier)} ]");
            dictionary.Add("target", $"[ {string.Join(",", dependencyInstance.Target)} ]");

            return dictionary;
        }

        /// <summary>
        /// Writes a <see cref="IDependency"/> to a <see cref="Dictionary{String, Object}"/> that contains a key-value-pair
        /// for each property. The type key is used to store type information (name of the Type).
        /// </summary>
        /// <param name="dependencyInstance">
        /// The subject <see cref="IDependency"/> that is to be written to a <see cref="Dictionary{String, Object}"/>.
        /// </param>
        /// <returns>
        /// An instance of <see cref="Dictionary{String, Object}"/> that contains all the properties as key-value-pairs as well
        /// as a type key that is used to store type information (name of the Type).
        /// </returns>
        /// <remarks>
        /// All values are stored as is, no conversion is done
        /// </remarks>
        private static Dictionary<string, object> WriteComplex(IDependency dependencyInstance)
        {
            var dictionary = new Dictionary<string, object>
            {
                { "@type", "Dependency" },
                { "@id", dependencyInstance.Id }
            };

            dictionary.Add("aliasIds", dependencyInstance.AliasIds);
            dictionary.Add("client", dependencyInstance.Client);
            dictionary.Add("declaredName", dependencyInstance.DeclaredName);
            dictionary.Add("declaredShortName", dependencyInstance.DeclaredShortName);
            dictionary.Add("elementId", dependencyInstance.ElementId);
            dictionary.Add("isImplied", dependencyInstance.IsImplied);
            dictionary.Add("isImpliedIncluded", dependencyInstance.IsImpliedIncluded);
            dictionary.Add("ownedRelatedElement", dependencyInstance.OwnedRelatedElement);
            dictionary.Add("ownedRelationship", dependencyInstance.OwnedRelationship);
            dictionary.Add("owningRelatedElement", dependencyInstance.OwningRelatedElement);
            dictionary.Add("owningRelationship", dependencyInstance.OwningRelationship);
            dictionary.Add("source", dependencyInstance.Source);
            dictionary.Add("supplier", dependencyInstance.Supplier);
            dictionary.Add("target", dependencyInstance.Target);

            return dictionary;
        }

        /// <summary>
        /// Checks whether the <see cref="IData"/> is not null and whether it is
        /// of type <see cref="IDependency"/>
        /// </summary>
        /// <param name="dataItem">
        /// The subject <see cref="IData"/>
        /// </param>
        /// <returns>
        /// an instance of <see cref="IDependency"/>
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="thing"/> is null
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown when <paramref name="thing"/> is not of type <see cref="IDependency"/>
        /// </exception>
        private static IDependency ThingNullAndTypeCheck(IData dataItem)
        {
            if (dataItem == null)
            {
                throw new ArgumentNullException("dataItem", "The dataItem may not be null");
            }

            if (!(dataItem is IDependency dependencyInstance))
            {
                throw new ArgumentException("The dataItem must be of Type IDependency", "dataItem");
            }

            return dependencyInstance;
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
