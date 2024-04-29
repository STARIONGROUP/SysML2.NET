// -------------------------------------------------------------------------------------------------
// <copyright file="VerificationCaseDefinitionDictionaryWriter.cs" company="Starion Group S.A.">
//
//   Copyright 2022-2024 Starion Group S.A.
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
    /// The purpose of the <see cref="VerificationCaseDefinitionDictionaryWriter"/> is to write (convert) a <see cref="IVerificationCaseDefinition"/>
    /// to a <see cref="Dictionary{String, Object}"/>.
    /// </summary>
    public static class VerificationCaseDefinitionDictionaryWriter
    {
        /// <summary>
        /// Writes a <see cref="IVerificationCaseDefinition"/> to a <see cref="Dictionary{String, Object}"/> that contains a key-value-pair
        /// for each property. The type key is used to store type information (name of the Type).
        /// </summary>
        /// <param name="dataItem">
        /// The subject <see cref="IVerificationCaseDefinition"/> that is to be written to a <see cref="Dictionary{String, Object}"/>.
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
            var verificationCaseDefinitionInstance = ThingNullAndTypeCheck(dataItem);

            switch (dictionaryKind)
            {
                case DictionaryKind.Complex:
                    return WriteComplex(verificationCaseDefinitionInstance);
                case DictionaryKind.Simplified:
                    return WriteSimplified(verificationCaseDefinitionInstance);
                default:
                    throw new NotSupportedException($"The dictionaryKind:{dictionaryKind} is not supported");
            }
        }

        /// <summary>
        /// Writes a <see cref="IVerificationCaseDefinition"/> to a <see cref="Dictionary{String, Object}"/> that contains a key-value-pair
        /// for each property. The type key is used to store type information (name of the Type).
        /// </summary>
        /// <param name="verificationCaseDefinitionInstance">
        /// The subject <see cref="IVerificationCaseDefinition"/> that is to be written to a <see cref="Dictionary{String, Object}"/>.
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
        private static Dictionary<string, object> WriteSimplified(IVerificationCaseDefinition verificationCaseDefinitionInstance)
        {
            var dictionary = new Dictionary<string, object>
            {
                { "@type", "VerificationCaseDefinition" },
                { "@id", verificationCaseDefinitionInstance.Id.ToString() }
            };

            dictionary.Add("aliasIds", verificationCaseDefinitionInstance.AliasIds);
            dictionary.Add("declaredName", verificationCaseDefinitionInstance.DeclaredName);
            dictionary.Add("declaredShortName", verificationCaseDefinitionInstance.DeclaredShortName);
            dictionary.Add("elementId", verificationCaseDefinitionInstance.ElementId);
            dictionary.Add("isAbstract", verificationCaseDefinitionInstance.IsAbstract);
            dictionary.Add("isImpliedIncluded", verificationCaseDefinitionInstance.IsImpliedIncluded);
            dictionary.Add("isIndividual", verificationCaseDefinitionInstance.IsIndividual);
            dictionary.Add("isSufficient", verificationCaseDefinitionInstance.IsSufficient);
            dictionary.Add("isVariation", verificationCaseDefinitionInstance.IsVariation);
            dictionary.Add("ownedRelationship", $"[ {string.Join(",", verificationCaseDefinitionInstance.OwnedRelationship)} ]");
            dictionary.Add("owningRelationship", verificationCaseDefinitionInstance.OwningRelationship.ToString());

            return dictionary;
        }

        /// <summary>
        /// Writes a <see cref="IVerificationCaseDefinition"/> to a <see cref="Dictionary{String, Object}"/> that contains a key-value-pair
        /// for each property. The type key is used to store type information (name of the Type).
        /// </summary>
        /// <param name="verificationCaseDefinitionInstance">
        /// The subject <see cref="IVerificationCaseDefinition"/> that is to be written to a <see cref="Dictionary{String, Object}"/>.
        /// </param>
        /// <returns>
        /// An instance of <see cref="Dictionary{String, Object}"/> that contains all the properties as key-value-pairs as well
        /// as a type key that is used to store type information (name of the Type).
        /// </returns>
        /// <remarks>
        /// All values are stored as is, no conversion is done
        /// </remarks>
        private static Dictionary<string, object> WriteComplex(IVerificationCaseDefinition verificationCaseDefinitionInstance)
        {
            var dictionary = new Dictionary<string, object>
            {
                { "@type", "VerificationCaseDefinition" },
                { "@id", verificationCaseDefinitionInstance.Id }
            };

            dictionary.Add("aliasIds", verificationCaseDefinitionInstance.AliasIds);
            dictionary.Add("declaredName", verificationCaseDefinitionInstance.DeclaredName);
            dictionary.Add("declaredShortName", verificationCaseDefinitionInstance.DeclaredShortName);
            dictionary.Add("elementId", verificationCaseDefinitionInstance.ElementId);
            dictionary.Add("isAbstract", verificationCaseDefinitionInstance.IsAbstract);
            dictionary.Add("isImpliedIncluded", verificationCaseDefinitionInstance.IsImpliedIncluded);
            dictionary.Add("isIndividual", verificationCaseDefinitionInstance.IsIndividual);
            dictionary.Add("isSufficient", verificationCaseDefinitionInstance.IsSufficient);
            dictionary.Add("isVariation", verificationCaseDefinitionInstance.IsVariation);
            dictionary.Add("ownedRelationship", verificationCaseDefinitionInstance.OwnedRelationship);
            dictionary.Add("owningRelationship", verificationCaseDefinitionInstance.OwningRelationship);

            return dictionary;
        }

        /// <summary>
        /// Checks whether the <see cref="IData"/> is not null and whether it is
        /// of type <see cref="IVerificationCaseDefinition"/>
        /// </summary>
        /// <param name="dataItem">
        /// The subject <see cref="IData"/>
        /// </param>
        /// <returns>
        /// an instance of <see cref="IVerificationCaseDefinition"/>
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="thing"/> is null
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown when <paramref name="thing"/> is not of type <see cref="IVerificationCaseDefinition"/>
        /// </exception>
        private static IVerificationCaseDefinition ThingNullAndTypeCheck(IData dataItem)
        {
            if (dataItem == null)
            {
                throw new ArgumentNullException("dataItem", "The dataItem may not be null");
            }

            if (!(dataItem is IVerificationCaseDefinition verificationCaseDefinitionInstance))
            {
                throw new ArgumentException("The dataItem must be of Type IVerificationCaseDefinition", "dataItem");
            }

            return verificationCaseDefinitionInstance;
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
