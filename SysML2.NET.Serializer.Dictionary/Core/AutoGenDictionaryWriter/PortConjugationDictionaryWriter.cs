// -------------------------------------------------------------------------------------------------
// <copyright file="PortConjugationDictionaryWriter.cs" company="RHEA System S.A.">
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
    /// The purpose of the <see cref="PortConjugationDictionaryWriter"/> is to write (convert) a <see cref="IPortConjugation"/>
    /// to a <see cref="Dictionary{String, Object}"/>.
    /// </summary>
    public static class PortConjugationDictionaryWriter
    {
        /// <summary>
        /// Writes a <see cref="IPortConjugation"/> to a <see cref="Dictionary{String, Object}"/> that contains a key-value-pair
        /// for each property. The type key is used to store type information (name of the Type).
        /// </summary>
        /// <param name="dataItem">
        /// The subject <see cref="IPortConjugation"/> that is to be written to a <see cref="Dictionary{String, Object}"/>.
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
            var portConjugationInstance = ThingNullAndTypeCheck(dataItem);

            switch (dictionaryKind)
            {
                case DictionaryKind.Complex:
                    return WriteComplex(portConjugationInstance);
                case DictionaryKind.Simplified:
                    return WriteSimplified(portConjugationInstance);
                default:
                    throw new NotSupportedException($"The dictionaryKind:{dictionaryKind} is not supported");
            }
        }

        /// <summary>
        /// Writes a <see cref="IPortConjugation"/> to a <see cref="Dictionary{String, Object}"/> that contains a key-value-pair
        /// for each property. The type key is used to store type information (name of the Type).
        /// </summary>
        /// <param name="portConjugationInstance">
        /// The subject <see cref="IPortConjugation"/> that is to be written to a <see cref="Dictionary{String, Object}"/>.
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
        private static Dictionary<string, object> WriteSimplified(IPortConjugation portConjugationInstance)
        {
            var dictionary = new Dictionary<string, object>
            {
                { "@type", "PortConjugation" },
                { "@id", portConjugationInstance.Id.ToString() }
            };

            dictionary.Add("aliasIds", portConjugationInstance.AliasIds);
            dictionary.Add("conjugatedType", portConjugationInstance.ConjugatedType.ToString());
            dictionary.Add("declaredName", portConjugationInstance.DeclaredName);
            dictionary.Add("declaredShortName", portConjugationInstance.DeclaredShortName);
            dictionary.Add("elementId", portConjugationInstance.ElementId);
            dictionary.Add("isImplied", portConjugationInstance.IsImplied);
            dictionary.Add("isImpliedIncluded", portConjugationInstance.IsImpliedIncluded);
            dictionary.Add("originalPortDefinition", portConjugationInstance.OriginalPortDefinition.ToString());
            dictionary.Add("originalType", portConjugationInstance.OriginalType.ToString());
            dictionary.Add("ownedRelatedElement", $"[ {string.Join(",", portConjugationInstance.OwnedRelatedElement)} ]");
            dictionary.Add("ownedRelationship", $"[ {string.Join(",", portConjugationInstance.OwnedRelationship)} ]");
            dictionary.Add("owningRelatedElement", portConjugationInstance.OwningRelatedElement.ToString());
            dictionary.Add("owningRelationship", portConjugationInstance.OwningRelationship.ToString());
            dictionary.Add("source", $"[ {string.Join(",", portConjugationInstance.Source)} ]");
            dictionary.Add("target", $"[ {string.Join(",", portConjugationInstance.Target)} ]");

            return dictionary;
        }

        /// <summary>
        /// Writes a <see cref="IPortConjugation"/> to a <see cref="Dictionary{String, Object}"/> that contains a key-value-pair
        /// for each property. The type key is used to store type information (name of the Type).
        /// </summary>
        /// <param name="portConjugationInstance">
        /// The subject <see cref="IPortConjugation"/> that is to be written to a <see cref="Dictionary{String, Object}"/>.
        /// </param>
        /// <returns>
        /// An instance of <see cref="Dictionary{String, Object}"/> that contains all the properties as key-value-pairs as well
        /// as a type key that is used to store type information (name of the Type).
        /// </returns>
        /// <remarks>
        /// All values are stored as is, no conversion is done
        /// </remarks>
        private static Dictionary<string, object> WriteComplex(IPortConjugation portConjugationInstance)
        {
            var dictionary = new Dictionary<string, object>
            {
                { "@type", "PortConjugation" },
                { "@id", portConjugationInstance.Id }
            };

            dictionary.Add("aliasIds", portConjugationInstance.AliasIds);
            dictionary.Add("conjugatedType", portConjugationInstance.ConjugatedType);
            dictionary.Add("declaredName", portConjugationInstance.DeclaredName);
            dictionary.Add("declaredShortName", portConjugationInstance.DeclaredShortName);
            dictionary.Add("elementId", portConjugationInstance.ElementId);
            dictionary.Add("isImplied", portConjugationInstance.IsImplied);
            dictionary.Add("isImpliedIncluded", portConjugationInstance.IsImpliedIncluded);
            dictionary.Add("originalPortDefinition", portConjugationInstance.OriginalPortDefinition);
            dictionary.Add("originalType", portConjugationInstance.OriginalType);
            dictionary.Add("ownedRelatedElement", portConjugationInstance.OwnedRelatedElement);
            dictionary.Add("ownedRelationship", portConjugationInstance.OwnedRelationship);
            dictionary.Add("owningRelatedElement", portConjugationInstance.OwningRelatedElement);
            dictionary.Add("owningRelationship", portConjugationInstance.OwningRelationship);
            dictionary.Add("source", portConjugationInstance.Source);
            dictionary.Add("target", portConjugationInstance.Target);

            return dictionary;
        }

        /// <summary>
        /// Checks whether the <see cref="IData"/> is not null and whether it is
        /// of type <see cref="IPortConjugation"/>
        /// </summary>
        /// <param name="dataItem">
        /// The subject <see cref="IData"/>
        /// </param>
        /// <returns>
        /// an instance of <see cref="IPortConjugation"/>
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="thing"/> is null
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown when <paramref name="thing"/> is not of type <see cref="IPortConjugation"/>
        /// </exception>
        private static IPortConjugation ThingNullAndTypeCheck(IData dataItem)
        {
            if (dataItem == null)
            {
                throw new ArgumentNullException("dataItem", "The dataItem may not be null");
            }

            if (!(dataItem is IPortConjugation portConjugationInstance))
            {
                throw new ArgumentException("The dataItem must be of Type IPortConjugation", "dataItem");
            }

            return portConjugationInstance;
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
