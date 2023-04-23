// -------------------------------------------------------------------------------------------------
// <copyright file="ActionUsageDictionaryWriter.cs" company="RHEA System S.A.">
//
//   Copyright 2022-2023 RHEA System S.A.
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
    /// The purpose of the <see cref="ActionUsageDictionaryWriter"/> is to write (convert) a <see cref="IActionUsage"/>
    /// to a <see cref="Dictionary{String, Object}"/>.
    /// </summary>
    public static class ActionUsageDictionaryWriter
    {
        /// <summary>
        /// Writes a <see cref="IActionUsage"/> to a <see cref="Dictionary{String, Object}"/> that contains a key-value-pair
        /// for each property. The type key is used to store type information (name of the Type).
        /// </summary>
        /// <param name="dataItem">
        /// The subject <see cref="IActionUsage"/> that is to be written to a <see cref="Dictionary{String, Object}"/>.
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
            var actionUsageInstance = ThingNullAndTypeCheck(dataItem);

            switch (dictionaryKind)
            {
                case DictionaryKind.Complex:
                    return WriteComplex(actionUsageInstance);
                case DictionaryKind.Simplified:
                    return WriteSimplified(actionUsageInstance);
                default:
                    throw new NotSupportedException($"The dictionaryKind:{dictionaryKind} is not supported");
            }
        }

        /// <summary>
        /// Writes a <see cref="IActionUsage"/> to a <see cref="Dictionary{String, Object}"/> that contains a key-value-pair
        /// for each property. The type key is used to store type information (name of the Type).
        /// </summary>
        /// <param name="actionUsageInstance">
        /// The subject <see cref="IActionUsage"/> that is to be written to a <see cref="Dictionary{String, Object}"/>.
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
        private static Dictionary<string, object> WriteSimplified(IActionUsage actionUsageInstance)
        {
            var dictionary = new Dictionary<string, object>
            {
                { "@type", "ActionUsage" },
                { "@id", actionUsageInstance.Id.ToString() }
            };

            dictionary.Add("aliasIds", actionUsageInstance.AliasIds);
            dictionary.Add("declaredName", actionUsageInstance.DeclaredName);
            dictionary.Add("declaredShortName", actionUsageInstance.DeclaredShortName);
            dictionary.Add("direction", actionUsageInstance.Direction);
            dictionary.Add("elementId", actionUsageInstance.ElementId);
            dictionary.Add("isAbstract", actionUsageInstance.IsAbstract);
            dictionary.Add("isComposite", actionUsageInstance.IsComposite);
            dictionary.Add("isDerived", actionUsageInstance.IsDerived);
            dictionary.Add("isEnd", actionUsageInstance.IsEnd);
            dictionary.Add("isImpliedIncluded", actionUsageInstance.IsImpliedIncluded);
            dictionary.Add("isIndividual", actionUsageInstance.IsIndividual);
            dictionary.Add("isOrdered", actionUsageInstance.IsOrdered);
            dictionary.Add("isPortion", actionUsageInstance.IsPortion);
            dictionary.Add("isReadOnly", actionUsageInstance.IsReadOnly);
            dictionary.Add("isSufficient", actionUsageInstance.IsSufficient);
            dictionary.Add("isUnique", actionUsageInstance.IsUnique);
            dictionary.Add("isVariation", actionUsageInstance.IsVariation);
            dictionary.Add("ownedRelationship", $"[ {string.Join(",", actionUsageInstance.OwnedRelationship)} ]");
            dictionary.Add("owningRelationship", actionUsageInstance.OwningRelationship.ToString());
            dictionary.Add("portionKind", actionUsageInstance.PortionKind);

            return dictionary;
        }

        /// <summary>
        /// Writes a <see cref="IActionUsage"/> to a <see cref="Dictionary{String, Object}"/> that contains a key-value-pair
        /// for each property. The type key is used to store type information (name of the Type).
        /// </summary>
        /// <param name="actionUsageInstance">
        /// The subject <see cref="IActionUsage"/> that is to be written to a <see cref="Dictionary{String, Object}"/>.
        /// </param>
        /// <returns>
        /// An instance of <see cref="Dictionary{String, Object}"/> that contains all the properties as key-value-pairs as well
        /// as a type key that is used to store type information (name of the Type).
        /// </returns>
        /// <remarks>
        /// All values are stored as is, no conversion is done
        /// </remarks>
        private static Dictionary<string, object> WriteComplex(IActionUsage actionUsageInstance)
        {
            var dictionary = new Dictionary<string, object>
            {
                { "@type", "ActionUsage" },
                { "@id", actionUsageInstance.Id }
            };

            dictionary.Add("aliasIds", actionUsageInstance.AliasIds);
            dictionary.Add("declaredName", actionUsageInstance.DeclaredName);
            dictionary.Add("declaredShortName", actionUsageInstance.DeclaredShortName);
            dictionary.Add("direction", actionUsageInstance.Direction);
            dictionary.Add("elementId", actionUsageInstance.ElementId);
            dictionary.Add("isAbstract", actionUsageInstance.IsAbstract);
            dictionary.Add("isComposite", actionUsageInstance.IsComposite);
            dictionary.Add("isDerived", actionUsageInstance.IsDerived);
            dictionary.Add("isEnd", actionUsageInstance.IsEnd);
            dictionary.Add("isImpliedIncluded", actionUsageInstance.IsImpliedIncluded);
            dictionary.Add("isIndividual", actionUsageInstance.IsIndividual);
            dictionary.Add("isOrdered", actionUsageInstance.IsOrdered);
            dictionary.Add("isPortion", actionUsageInstance.IsPortion);
            dictionary.Add("isReadOnly", actionUsageInstance.IsReadOnly);
            dictionary.Add("isSufficient", actionUsageInstance.IsSufficient);
            dictionary.Add("isUnique", actionUsageInstance.IsUnique);
            dictionary.Add("isVariation", actionUsageInstance.IsVariation);
            dictionary.Add("ownedRelationship", actionUsageInstance.OwnedRelationship);
            dictionary.Add("owningRelationship", actionUsageInstance.OwningRelationship);
            dictionary.Add("portionKind", actionUsageInstance.PortionKind);

            return dictionary;
        }

        /// <summary>
        /// Checks whether the <see cref="IData"/> is not null and whether it is
        /// of type <see cref="IActionUsage"/>
        /// </summary>
        /// <param name="dataItem">
        /// The subject <see cref="IData"/>
        /// </param>
        /// <returns>
        /// an instance of <see cref="IActionUsage"/>
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="thing"/> is null
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown when <paramref name="thing"/> is not of type <see cref="IActionUsage"/>
        /// </exception>
        private static IActionUsage ThingNullAndTypeCheck(IData dataItem)
        {
            if (dataItem == null)
            {
                throw new ArgumentNullException("dataItem", "The dataItem may not be null");
            }

            if (!(dataItem is IActionUsage actionUsageInstance))
            {
                throw new ArgumentException("The dataItem must be of Type IActionUsage", "dataItem");
            }

            return actionUsageInstance;
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
