﻿// -------------------------------------------------------------------------------------------------
// <copyright file="SuccessionDictionaryWriter.cs" company="Starion Group S.A.">
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
    /// The purpose of the <see cref="SuccessionDictionaryWriter"/> is to write (convert) a <see cref="ISuccession"/>
    /// to a <see cref="Dictionary{String, Object}"/>.
    /// </summary>
    public static class SuccessionDictionaryWriter
    {
        /// <summary>
        /// Writes a <see cref="ISuccession"/> to a <see cref="Dictionary{String, Object}"/> that contains a key-value-pair
        /// for each property. The type key is used to store type information (name of the Type).
        /// </summary>
        /// <param name="dataItem">
        /// The subject <see cref="ISuccession"/> that is to be written to a <see cref="Dictionary{String, Object}"/>.
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
            var successionInstance = ThingNullAndTypeCheck(dataItem);

            switch (dictionaryKind)
            {
                case DictionaryKind.Complex:
                    return WriteComplex(successionInstance);
                case DictionaryKind.Simplified:
                    return WriteSimplified(successionInstance);
                default:
                    throw new NotSupportedException($"The dictionaryKind:{dictionaryKind} is not supported");
            }
        }

        /// <summary>
        /// Writes a <see cref="ISuccession"/> to a <see cref="Dictionary{String, Object}"/> that contains a key-value-pair
        /// for each property. The type key is used to store type information (name of the Type).
        /// </summary>
        /// <param name="successionInstance">
        /// The subject <see cref="ISuccession"/> that is to be written to a <see cref="Dictionary{String, Object}"/>.
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
        private static Dictionary<string, object> WriteSimplified(ISuccession successionInstance)
        {
            var dictionary = new Dictionary<string, object>
            {
                { "@type", "Succession" },
                { "@id", successionInstance.Id.ToString() }
            };

            dictionary.Add("aliasIds", successionInstance.AliasIds);
            dictionary.Add("declaredName", successionInstance.DeclaredName);
            dictionary.Add("declaredShortName", successionInstance.DeclaredShortName);
            dictionary.Add("direction", successionInstance.Direction);
            dictionary.Add("elementId", successionInstance.ElementId);
            dictionary.Add("isAbstract", successionInstance.IsAbstract);
            dictionary.Add("isComposite", successionInstance.IsComposite);
            dictionary.Add("isDerived", successionInstance.IsDerived);
            dictionary.Add("isEnd", successionInstance.IsEnd);
            dictionary.Add("isImplied", successionInstance.IsImplied);
            dictionary.Add("isImpliedIncluded", successionInstance.IsImpliedIncluded);
            dictionary.Add("isOrdered", successionInstance.IsOrdered);
            dictionary.Add("isPortion", successionInstance.IsPortion);
            dictionary.Add("isReadOnly", successionInstance.IsReadOnly);
            dictionary.Add("isSufficient", successionInstance.IsSufficient);
            dictionary.Add("isUnique", successionInstance.IsUnique);
            dictionary.Add("ownedRelatedElement", $"[ {string.Join(",", successionInstance.OwnedRelatedElement)} ]");
            dictionary.Add("ownedRelationship", $"[ {string.Join(",", successionInstance.OwnedRelationship)} ]");
            dictionary.Add("owningRelatedElement", successionInstance.OwningRelatedElement.ToString());
            dictionary.Add("owningRelationship", successionInstance.OwningRelationship.ToString());
            dictionary.Add("source", $"[ {string.Join(",", successionInstance.Source)} ]");
            dictionary.Add("target", $"[ {string.Join(",", successionInstance.Target)} ]");

            return dictionary;
        }

        /// <summary>
        /// Writes a <see cref="ISuccession"/> to a <see cref="Dictionary{String, Object}"/> that contains a key-value-pair
        /// for each property. The type key is used to store type information (name of the Type).
        /// </summary>
        /// <param name="successionInstance">
        /// The subject <see cref="ISuccession"/> that is to be written to a <see cref="Dictionary{String, Object}"/>.
        /// </param>
        /// <returns>
        /// An instance of <see cref="Dictionary{String, Object}"/> that contains all the properties as key-value-pairs as well
        /// as a type key that is used to store type information (name of the Type).
        /// </returns>
        /// <remarks>
        /// All values are stored as is, no conversion is done
        /// </remarks>
        private static Dictionary<string, object> WriteComplex(ISuccession successionInstance)
        {
            var dictionary = new Dictionary<string, object>
            {
                { "@type", "Succession" },
                { "@id", successionInstance.Id }
            };

            dictionary.Add("aliasIds", successionInstance.AliasIds);
            dictionary.Add("declaredName", successionInstance.DeclaredName);
            dictionary.Add("declaredShortName", successionInstance.DeclaredShortName);
            dictionary.Add("direction", successionInstance.Direction);
            dictionary.Add("elementId", successionInstance.ElementId);
            dictionary.Add("isAbstract", successionInstance.IsAbstract);
            dictionary.Add("isComposite", successionInstance.IsComposite);
            dictionary.Add("isDerived", successionInstance.IsDerived);
            dictionary.Add("isEnd", successionInstance.IsEnd);
            dictionary.Add("isImplied", successionInstance.IsImplied);
            dictionary.Add("isImpliedIncluded", successionInstance.IsImpliedIncluded);
            dictionary.Add("isOrdered", successionInstance.IsOrdered);
            dictionary.Add("isPortion", successionInstance.IsPortion);
            dictionary.Add("isReadOnly", successionInstance.IsReadOnly);
            dictionary.Add("isSufficient", successionInstance.IsSufficient);
            dictionary.Add("isUnique", successionInstance.IsUnique);
            dictionary.Add("ownedRelatedElement", successionInstance.OwnedRelatedElement);
            dictionary.Add("ownedRelationship", successionInstance.OwnedRelationship);
            dictionary.Add("owningRelatedElement", successionInstance.OwningRelatedElement);
            dictionary.Add("owningRelationship", successionInstance.OwningRelationship);
            dictionary.Add("source", successionInstance.Source);
            dictionary.Add("target", successionInstance.Target);

            return dictionary;
        }

        /// <summary>
        /// Checks whether the <see cref="IData"/> is not null and whether it is
        /// of type <see cref="ISuccession"/>
        /// </summary>
        /// <param name="dataItem">
        /// The subject <see cref="IData"/>
        /// </param>
        /// <returns>
        /// an instance of <see cref="ISuccession"/>
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="thing"/> is null
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown when <paramref name="thing"/> is not of type <see cref="ISuccession"/>
        /// </exception>
        private static ISuccession ThingNullAndTypeCheck(IData dataItem)
        {
            if (dataItem == null)
            {
                throw new ArgumentNullException("dataItem", "The dataItem may not be null");
            }

            if (!(dataItem is ISuccession successionInstance))
            {
                throw new ArgumentException("The dataItem must be of Type ISuccession", "dataItem");
            }

            return successionInstance;
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
