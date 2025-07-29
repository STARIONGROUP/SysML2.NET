// -------------------------------------------------------------------------------------------------
// <copyright file="FlowDefinitionDictionaryWriter.cs" company="Starion Group S.A.">
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
    /// The purpose of the <see cref="FlowDefinitionDictionaryWriter"/> is to write (convert) a <see cref="IFlowDefinition"/>
    /// to a <see cref="Dictionary{String, Object}"/>.
    /// </summary>
    public static class FlowDefinitionDictionaryWriter
    {
        /// <summary>
        /// Writes a <see cref="IFlowDefinition"/> to a <see cref="Dictionary{String, Object}"/> that contains a key-value-pair
        /// for each property. The type key is used to store type information (name of the Type).
        /// </summary>
        /// <param name="dataItem">
        /// The subject <see cref="IFlowDefinition"/> that is to be written to a <see cref="Dictionary{String, Object}"/>.
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
            var flowDefinitionInstance = ThingNullAndTypeCheck(dataItem);

            switch (dictionaryKind)
            {
                case DictionaryKind.Complex:
                    return WriteComplex(flowDefinitionInstance);
                case DictionaryKind.Simplified:
                    return WriteSimplified(flowDefinitionInstance);
                default:
                    throw new NotSupportedException($"The dictionaryKind:{dictionaryKind} is not supported");
            }
        }

        /// <summary>
        /// Writes a <see cref="IFlowDefinition"/> to a <see cref="Dictionary{String, Object}"/> that contains a key-value-pair
        /// for each property. The type key is used to store type information (name of the Type).
        /// </summary>
        /// <param name="flowDefinitionInstance">
        /// The subject <see cref="IFlowDefinition"/> that is to be written to a <see cref="Dictionary{String, Object}"/>.
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
        private static Dictionary<string, object> WriteSimplified(IFlowDefinition flowDefinitionInstance)
        {
            var dictionary = new Dictionary<string, object>
            {
                { "@type", "FlowDefinition" },
                { "@id", flowDefinitionInstance.Id.ToString() }
            };

            dictionary.Add("aliasIds", flowDefinitionInstance.AliasIds);
            dictionary.Add("declaredName", flowDefinitionInstance.DeclaredName);
            dictionary.Add("declaredShortName", flowDefinitionInstance.DeclaredShortName);
            dictionary.Add("elementId", flowDefinitionInstance.ElementId);
            dictionary.Add("isAbstract", flowDefinitionInstance.IsAbstract);
            dictionary.Add("isImplied", flowDefinitionInstance.IsImplied);
            dictionary.Add("isImpliedIncluded", flowDefinitionInstance.IsImpliedIncluded);
            dictionary.Add("isIndividual", flowDefinitionInstance.IsIndividual);
            dictionary.Add("isSufficient", flowDefinitionInstance.IsSufficient);
            dictionary.Add("isVariation", flowDefinitionInstance.IsVariation);
            dictionary.Add("ownedRelatedElement", $"[ {string.Join(",", flowDefinitionInstance.OwnedRelatedElement)} ]");
            dictionary.Add("ownedRelationship", $"[ {string.Join(",", flowDefinitionInstance.OwnedRelationship)} ]");
            dictionary.Add("owningRelatedElement", flowDefinitionInstance.OwningRelatedElement.ToString());
            dictionary.Add("owningRelationship", flowDefinitionInstance.OwningRelationship.ToString());
            dictionary.Add("source", $"[ {string.Join(",", flowDefinitionInstance.Source)} ]");
            dictionary.Add("target", $"[ {string.Join(",", flowDefinitionInstance.Target)} ]");

            return dictionary;
        }

        /// <summary>
        /// Writes a <see cref="IFlowDefinition"/> to a <see cref="Dictionary{String, Object}"/> that contains a key-value-pair
        /// for each property. The type key is used to store type information (name of the Type).
        /// </summary>
        /// <param name="flowDefinitionInstance">
        /// The subject <see cref="IFlowDefinition"/> that is to be written to a <see cref="Dictionary{String, Object}"/>.
        /// </param>
        /// <returns>
        /// An instance of <see cref="Dictionary{String, Object}"/> that contains all the properties as key-value-pairs as well
        /// as a type key that is used to store type information (name of the Type).
        /// </returns>
        /// <remarks>
        /// All values are stored as is, no conversion is done
        /// </remarks>
        private static Dictionary<string, object> WriteComplex(IFlowDefinition flowDefinitionInstance)
        {
            var dictionary = new Dictionary<string, object>
            {
                { "@type", "FlowDefinition" },
                { "@id", flowDefinitionInstance.Id }
            };

            dictionary.Add("aliasIds", flowDefinitionInstance.AliasIds);
            dictionary.Add("declaredName", flowDefinitionInstance.DeclaredName);
            dictionary.Add("declaredShortName", flowDefinitionInstance.DeclaredShortName);
            dictionary.Add("elementId", flowDefinitionInstance.ElementId);
            dictionary.Add("isAbstract", flowDefinitionInstance.IsAbstract);
            dictionary.Add("isImplied", flowDefinitionInstance.IsImplied);
            dictionary.Add("isImpliedIncluded", flowDefinitionInstance.IsImpliedIncluded);
            dictionary.Add("isIndividual", flowDefinitionInstance.IsIndividual);
            dictionary.Add("isSufficient", flowDefinitionInstance.IsSufficient);
            dictionary.Add("isVariation", flowDefinitionInstance.IsVariation);
            dictionary.Add("ownedRelatedElement", flowDefinitionInstance.OwnedRelatedElement);
            dictionary.Add("ownedRelationship", flowDefinitionInstance.OwnedRelationship);
            dictionary.Add("owningRelatedElement", flowDefinitionInstance.OwningRelatedElement);
            dictionary.Add("owningRelationship", flowDefinitionInstance.OwningRelationship);
            dictionary.Add("source", flowDefinitionInstance.Source);
            dictionary.Add("target", flowDefinitionInstance.Target);

            return dictionary;
        }

        /// <summary>
        /// Checks whether the <see cref="IData"/> is not null and whether it is
        /// of type <see cref="IFlowDefinition"/>
        /// </summary>
        /// <param name="dataItem">
        /// The subject <see cref="IData"/>
        /// </param>
        /// <returns>
        /// an instance of <see cref="IFlowDefinition"/>
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="thing"/> is null
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown when <paramref name="thing"/> is not of type <see cref="IFlowDefinition"/>
        /// </exception>
        private static IFlowDefinition ThingNullAndTypeCheck(IData dataItem)
        {
            if (dataItem == null)
            {
                throw new ArgumentNullException("dataItem", "The dataItem may not be null");
            }

            if (!(dataItem is IFlowDefinition flowDefinitionInstance))
            {
                throw new ArgumentException("The dataItem must be of Type IFlowDefinition", "dataItem");
            }

            return flowDefinitionInstance;
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
