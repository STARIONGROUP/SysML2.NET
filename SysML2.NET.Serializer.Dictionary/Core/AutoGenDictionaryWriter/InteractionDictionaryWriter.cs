// -------------------------------------------------------------------------------------------------
// <copyright file="InteractionDictionaryWriter.cs" company="Starion Group S.A.">
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
    /// The purpose of the <see cref="InteractionDictionaryWriter"/> is to write (convert) a <see cref="IInteraction"/>
    /// to a <see cref="Dictionary{String, Object}"/>.
    /// </summary>
    public static class InteractionDictionaryWriter
    {
        /// <summary>
        /// Writes a <see cref="IInteraction"/> to a <see cref="Dictionary{String, Object}"/> that contains a key-value-pair
        /// for each property. The type key is used to store type information (name of the Type).
        /// </summary>
        /// <param name="dataItem">
        /// The subject <see cref="IInteraction"/> that is to be written to a <see cref="Dictionary{String, Object}"/>.
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
            var interactionInstance = ThingNullAndTypeCheck(dataItem);

            switch (dictionaryKind)
            {
                case DictionaryKind.Complex:
                    return WriteComplex(interactionInstance);
                case DictionaryKind.Simplified:
                    return WriteSimplified(interactionInstance);
                default:
                    throw new NotSupportedException($"The dictionaryKind:{dictionaryKind} is not supported");
            }
        }

        /// <summary>
        /// Writes a <see cref="IInteraction"/> to a <see cref="Dictionary{String, Object}"/> that contains a key-value-pair
        /// for each property. The type key is used to store type information (name of the Type).
        /// </summary>
        /// <param name="interactionInstance">
        /// The subject <see cref="IInteraction"/> that is to be written to a <see cref="Dictionary{String, Object}"/>.
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
        private static Dictionary<string, object> WriteSimplified(IInteraction interactionInstance)
        {
            var dictionary = new Dictionary<string, object>
            {
                { "@type", "Interaction" },
                { "@id", interactionInstance.Id.ToString() }
            };

            dictionary.Add("aliasIds", interactionInstance.AliasIds);
            dictionary.Add("declaredName", interactionInstance.DeclaredName);
            dictionary.Add("declaredShortName", interactionInstance.DeclaredShortName);
            dictionary.Add("elementId", interactionInstance.ElementId);
            dictionary.Add("isAbstract", interactionInstance.IsAbstract);
            dictionary.Add("isImplied", interactionInstance.IsImplied);
            dictionary.Add("isImpliedIncluded", interactionInstance.IsImpliedIncluded);
            dictionary.Add("isSufficient", interactionInstance.IsSufficient);
            dictionary.Add("ownedRelatedElement", $"[ {string.Join(",", interactionInstance.OwnedRelatedElement)} ]");
            dictionary.Add("ownedRelationship", $"[ {string.Join(",", interactionInstance.OwnedRelationship)} ]");
            dictionary.Add("owningRelatedElement", interactionInstance.OwningRelatedElement.ToString());
            dictionary.Add("owningRelationship", interactionInstance.OwningRelationship.ToString());
            dictionary.Add("source", $"[ {string.Join(",", interactionInstance.Source)} ]");
            dictionary.Add("target", $"[ {string.Join(",", interactionInstance.Target)} ]");

            return dictionary;
        }

        /// <summary>
        /// Writes a <see cref="IInteraction"/> to a <see cref="Dictionary{String, Object}"/> that contains a key-value-pair
        /// for each property. The type key is used to store type information (name of the Type).
        /// </summary>
        /// <param name="interactionInstance">
        /// The subject <see cref="IInteraction"/> that is to be written to a <see cref="Dictionary{String, Object}"/>.
        /// </param>
        /// <returns>
        /// An instance of <see cref="Dictionary{String, Object}"/> that contains all the properties as key-value-pairs as well
        /// as a type key that is used to store type information (name of the Type).
        /// </returns>
        /// <remarks>
        /// All values are stored as is, no conversion is done
        /// </remarks>
        private static Dictionary<string, object> WriteComplex(IInteraction interactionInstance)
        {
            var dictionary = new Dictionary<string, object>
            {
                { "@type", "Interaction" },
                { "@id", interactionInstance.Id }
            };

            dictionary.Add("aliasIds", interactionInstance.AliasIds);
            dictionary.Add("declaredName", interactionInstance.DeclaredName);
            dictionary.Add("declaredShortName", interactionInstance.DeclaredShortName);
            dictionary.Add("elementId", interactionInstance.ElementId);
            dictionary.Add("isAbstract", interactionInstance.IsAbstract);
            dictionary.Add("isImplied", interactionInstance.IsImplied);
            dictionary.Add("isImpliedIncluded", interactionInstance.IsImpliedIncluded);
            dictionary.Add("isSufficient", interactionInstance.IsSufficient);
            dictionary.Add("ownedRelatedElement", interactionInstance.OwnedRelatedElement);
            dictionary.Add("ownedRelationship", interactionInstance.OwnedRelationship);
            dictionary.Add("owningRelatedElement", interactionInstance.OwningRelatedElement);
            dictionary.Add("owningRelationship", interactionInstance.OwningRelationship);
            dictionary.Add("source", interactionInstance.Source);
            dictionary.Add("target", interactionInstance.Target);

            return dictionary;
        }

        /// <summary>
        /// Checks whether the <see cref="IData"/> is not null and whether it is
        /// of type <see cref="IInteraction"/>
        /// </summary>
        /// <param name="dataItem">
        /// The subject <see cref="IData"/>
        /// </param>
        /// <returns>
        /// an instance of <see cref="IInteraction"/>
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="thing"/> is null
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown when <paramref name="thing"/> is not of type <see cref="IInteraction"/>
        /// </exception>
        private static IInteraction ThingNullAndTypeCheck(IData dataItem)
        {
            if (dataItem == null)
            {
                throw new ArgumentNullException("dataItem", "The dataItem may not be null");
            }

            if (!(dataItem is IInteraction interactionInstance))
            {
                throw new ArgumentException("The dataItem must be of Type IInteraction", "dataItem");
            }

            return interactionInstance;
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
