// -------------------------------------------------------------------------------------------------
// <copyright file="FlowConnectionUsageDictionaryWriter.cs" company="RHEA System S.A.">
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

namespace SysML2.NET.Serializer.Dictionary
{
    using System;
    using System.Collections.Generic;

    using SysML2.NET.Common;
    using SysML2.NET.Core.DTO;

    /// <summary>
    /// The purpose of the <see cref="FlowConnectionUsageDictionaryWriter"/> is to write (convert) a <see cref="IFlowConnectionUsage"/>
    /// to a <see cref="Dictionary{String, Object}"/>.
    /// </summary>
    public static class FlowConnectionUsageDictionaryWriter
    {
        /// <summary>
        /// Writes a <see cref="IFlowConnectionUsage"/> to a <see cref="Dictionary{String, Object}"/> that contains a key-value-pair
        /// for each property. The type key is used to store type information (name of the Type).
        /// </summary>
        /// <param name="dataItem">
        /// The subject <see cref="IFlowConnectionUsage"/> that is to be written to a <see cref="Dictionary{String, Object}"/>.
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
            var flowConnectionUsageInstance = ThingNullAndTypeCheck(dataItem);

            switch (dictionaryKind)
            {
                case DictionaryKind.Complex:
                    return WriteComplex(flowConnectionUsageInstance);
                case DictionaryKind.Simplified:
                    return WriteSimplified(flowConnectionUsageInstance);
                default:
                    throw new NotSupportedException($"The dictionaryKind:{dictionaryKind} is not supported");
            }
        }

        /// <summary>
        /// Writes a <see cref="IFlowConnectionUsage"/> to a <see cref="Dictionary{String, Object}"/> that contains a key-value-pair
        /// for each property. The type key is used to store type information (name of the Type).
        /// </summary>
        /// <param name="flowConnectionUsageInstance">
        /// The subject <see cref="IFlowConnectionUsage"/> that is to be written to a <see cref="Dictionary{String, Object}"/>.
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
        private static Dictionary<string, object> WriteSimplified(IFlowConnectionUsage flowConnectionUsageInstance)
        {
            var dictionary = new Dictionary<string, object>
            {
                { "@type", "FlowConnectionUsage" },
                { "@id", flowConnectionUsageInstance.Id.ToString() }
            };

            dictionary.Add("aliasIds", flowConnectionUsageInstance.AliasIds);
            dictionary.Add("direction", flowConnectionUsageInstance.Direction);
            dictionary.Add("elementId", flowConnectionUsageInstance.ElementId);
            dictionary.Add("isAbstract", flowConnectionUsageInstance.IsAbstract);
            dictionary.Add("isComposite", flowConnectionUsageInstance.IsComposite);
            dictionary.Add("isDerived", flowConnectionUsageInstance.IsDerived);
            dictionary.Add("isDirected", flowConnectionUsageInstance.IsDirected);
            dictionary.Add("isEnd", flowConnectionUsageInstance.IsEnd);
            dictionary.Add("isImplied", flowConnectionUsageInstance.IsImplied);
            dictionary.Add("isImpliedIncluded", flowConnectionUsageInstance.IsImpliedIncluded);
            dictionary.Add("isIndividual", flowConnectionUsageInstance.IsIndividual);
            dictionary.Add("isOrdered", flowConnectionUsageInstance.IsOrdered);
            dictionary.Add("isPortion", flowConnectionUsageInstance.IsPortion);
            dictionary.Add("isReadOnly", flowConnectionUsageInstance.IsReadOnly);
            dictionary.Add("isSufficient", flowConnectionUsageInstance.IsSufficient);
            dictionary.Add("isUnique", flowConnectionUsageInstance.IsUnique);
            dictionary.Add("isVariation", flowConnectionUsageInstance.IsVariation);
            dictionary.Add("name", flowConnectionUsageInstance.Name);
            dictionary.Add("ownedRelatedElement", $"[ {string.Join(",", flowConnectionUsageInstance.OwnedRelatedElement)} ]");
            dictionary.Add("ownedRelationship", $"[ {string.Join(",", flowConnectionUsageInstance.OwnedRelationship)} ]");
            dictionary.Add("owningRelatedElement", flowConnectionUsageInstance.OwningRelatedElement.ToString());
            dictionary.Add("owningRelationship", flowConnectionUsageInstance.OwningRelationship.ToString());
            dictionary.Add("portionKind", flowConnectionUsageInstance.PortionKind);
            dictionary.Add("shortName", flowConnectionUsageInstance.ShortName);
            dictionary.Add("source", $"[ {string.Join(",", flowConnectionUsageInstance.Source)} ]");
            dictionary.Add("target", $"[ {string.Join(",", flowConnectionUsageInstance.Target)} ]");

            return dictionary;
        }

        /// <summary>
        /// Writes a <see cref="IFlowConnectionUsage"/> to a <see cref="Dictionary{String, Object}"/> that contains a key-value-pair
        /// for each property. The type key is used to store type information (name of the Type).
        /// </summary>
        /// <param name="flowConnectionUsageInstance">
        /// The subject <see cref="IFlowConnectionUsage"/> that is to be written to a <see cref="Dictionary{String, Object}"/>.
        /// </param>
        /// <returns>
        /// An instance of <see cref="Dictionary{String, Object}"/> that contains all the properties as key-value-pairs as well
        /// as a type key that is used to store type information (name of the Type).
        /// </returns>
        /// <remarks>
        /// All values are stored as is, no conversion is done
        /// </remarks>
        private static Dictionary<string, object> WriteComplex(IFlowConnectionUsage flowConnectionUsageInstance)
        {
            var dictionary = new Dictionary<string, object>
            {
                { "@type", "FlowConnectionUsage" },
                { "@id", flowConnectionUsageInstance.Id }
            };

            dictionary.Add("aliasIds", flowConnectionUsageInstance.AliasIds);
            dictionary.Add("direction", flowConnectionUsageInstance.Direction);
            dictionary.Add("elementId", flowConnectionUsageInstance.ElementId);
            dictionary.Add("isAbstract", flowConnectionUsageInstance.IsAbstract);
            dictionary.Add("isComposite", flowConnectionUsageInstance.IsComposite);
            dictionary.Add("isDerived", flowConnectionUsageInstance.IsDerived);
            dictionary.Add("isDirected", flowConnectionUsageInstance.IsDirected);
            dictionary.Add("isEnd", flowConnectionUsageInstance.IsEnd);
            dictionary.Add("isImplied", flowConnectionUsageInstance.IsImplied);
            dictionary.Add("isImpliedIncluded", flowConnectionUsageInstance.IsImpliedIncluded);
            dictionary.Add("isIndividual", flowConnectionUsageInstance.IsIndividual);
            dictionary.Add("isOrdered", flowConnectionUsageInstance.IsOrdered);
            dictionary.Add("isPortion", flowConnectionUsageInstance.IsPortion);
            dictionary.Add("isReadOnly", flowConnectionUsageInstance.IsReadOnly);
            dictionary.Add("isSufficient", flowConnectionUsageInstance.IsSufficient);
            dictionary.Add("isUnique", flowConnectionUsageInstance.IsUnique);
            dictionary.Add("isVariation", flowConnectionUsageInstance.IsVariation);
            dictionary.Add("name", flowConnectionUsageInstance.Name);
            dictionary.Add("ownedRelatedElement", flowConnectionUsageInstance.OwnedRelatedElement);
            dictionary.Add("ownedRelationship", flowConnectionUsageInstance.OwnedRelationship);
            dictionary.Add("owningRelatedElement", flowConnectionUsageInstance.OwningRelatedElement);
            dictionary.Add("owningRelationship", flowConnectionUsageInstance.OwningRelationship);
            dictionary.Add("portionKind", flowConnectionUsageInstance.PortionKind);
            dictionary.Add("shortName", flowConnectionUsageInstance.ShortName);
            dictionary.Add("source", flowConnectionUsageInstance.Source);
            dictionary.Add("target", flowConnectionUsageInstance.Target);

            return dictionary;
        }

        /// <summary>
        /// Checks whether the <see cref="IData"/> is not null and whether it is
        /// of type <see cref="IFlowConnectionUsage"/>
        /// </summary>
        /// <param name="dataItem">
        /// The subject <see cref="IData"/>
        /// </param>
        /// <returns>
        /// an instance of <see cref="IFlowConnectionUsage"/>
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="thing"/> is null
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown when <paramref name="thing"/> is not of type <see cref="IFlowConnectionUsage"/>
        /// </exception>
        private static IFlowConnectionUsage ThingNullAndTypeCheck(IData dataItem)
        {
            if (dataItem == null)
            {
                throw new ArgumentNullException("dataItem", "The dataItem may not be null");
            }

            if (!(dataItem is IFlowConnectionUsage flowConnectionUsageInstance))
            {
                throw new ArgumentException("The dataItem must be of Type IFlowConnectionUsage", "dataItem");
            }

            return flowConnectionUsageInstance;
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
