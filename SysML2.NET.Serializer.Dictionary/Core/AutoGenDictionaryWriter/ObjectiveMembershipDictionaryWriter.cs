// -------------------------------------------------------------------------------------------------
// <copyright file="ObjectiveMembershipDictionaryWriter.cs" company="RHEA System S.A.">
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
    /// The purpose of the <see cref="ObjectiveMembershipDictionaryWriter"/> is to write (convert) a <see cref="IObjectiveMembership"/>
    /// to a <see cref="Dictionary{String, Object}"/>.
    /// </summary>
    public static class ObjectiveMembershipDictionaryWriter
    {
        /// <summary>
        /// Writes a <see cref="IObjectiveMembership"/> to a <see cref="Dictionary{String, Object}"/> that contains a key-value-pair
        /// for each property. The type key is used to store type information (name of the Type).
        /// </summary>
        /// <param name="dataItem">
        /// The subject <see cref="IObjectiveMembership"/> that is to be written to a <see cref="Dictionary{String, Object}"/>.
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
            var objectiveMembershipInstance = ThingNullAndTypeCheck(dataItem);

            switch (dictionaryKind)
            {
                case DictionaryKind.Complex:
                    return WriteComplex(objectiveMembershipInstance);
                case DictionaryKind.Simplified:
                    return WriteSimplified(objectiveMembershipInstance);
                default:
                    throw new NotSupportedException($"The dictionaryKind:{dictionaryKind} is not supported");
            }
        }

        /// <summary>
        /// Writes a <see cref="IObjectiveMembership"/> to a <see cref="Dictionary{String, Object}"/> that contains a key-value-pair
        /// for each property. The type key is used to store type information (name of the Type).
        /// </summary>
        /// <param name="objectiveMembershipInstance">
        /// The subject <see cref="IObjectiveMembership"/> that is to be written to a <see cref="Dictionary{String, Object}"/>.
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
        private static Dictionary<string, object> WriteSimplified(IObjectiveMembership objectiveMembershipInstance)
        {
            var dictionary = new Dictionary<string, object>
            {
                { "@type", "ObjectiveMembership" },
                { "@id", objectiveMembershipInstance.Id.ToString() }
            };

            dictionary.Add("aliasIds", objectiveMembershipInstance.AliasIds);
            dictionary.Add("declaredName", objectiveMembershipInstance.DeclaredName);
            dictionary.Add("declaredShortName", objectiveMembershipInstance.DeclaredShortName);
            dictionary.Add("elementId", objectiveMembershipInstance.ElementId);
            dictionary.Add("feature", objectiveMembershipInstance.Feature.ToString());
            dictionary.Add("isImplied", objectiveMembershipInstance.IsImplied);
            dictionary.Add("isImpliedIncluded", objectiveMembershipInstance.IsImpliedIncluded);
            dictionary.Add("memberElement", objectiveMembershipInstance.MemberElement.ToString());
            dictionary.Add("memberName", objectiveMembershipInstance.MemberName);
            dictionary.Add("memberShortName", objectiveMembershipInstance.MemberShortName);
            dictionary.Add("ownedRelatedElement", $"[ {string.Join(",", objectiveMembershipInstance.OwnedRelatedElement)} ]");
            dictionary.Add("ownedRelationship", $"[ {string.Join(",", objectiveMembershipInstance.OwnedRelationship)} ]");
            dictionary.Add("owningRelatedElement", objectiveMembershipInstance.OwningRelatedElement.ToString());
            dictionary.Add("owningRelationship", objectiveMembershipInstance.OwningRelationship.ToString());
            dictionary.Add("source", $"[ {string.Join(",", objectiveMembershipInstance.Source)} ]");
            dictionary.Add("target", $"[ {string.Join(",", objectiveMembershipInstance.Target)} ]");
            dictionary.Add("type", objectiveMembershipInstance.Type.ToString());
            dictionary.Add("visibility", objectiveMembershipInstance.Visibility);

            return dictionary;
        }

        /// <summary>
        /// Writes a <see cref="IObjectiveMembership"/> to a <see cref="Dictionary{String, Object}"/> that contains a key-value-pair
        /// for each property. The type key is used to store type information (name of the Type).
        /// </summary>
        /// <param name="objectiveMembershipInstance">
        /// The subject <see cref="IObjectiveMembership"/> that is to be written to a <see cref="Dictionary{String, Object}"/>.
        /// </param>
        /// <returns>
        /// An instance of <see cref="Dictionary{String, Object}"/> that contains all the properties as key-value-pairs as well
        /// as a type key that is used to store type information (name of the Type).
        /// </returns>
        /// <remarks>
        /// All values are stored as is, no conversion is done
        /// </remarks>
        private static Dictionary<string, object> WriteComplex(IObjectiveMembership objectiveMembershipInstance)
        {
            var dictionary = new Dictionary<string, object>
            {
                { "@type", "ObjectiveMembership" },
                { "@id", objectiveMembershipInstance.Id }
            };

            dictionary.Add("aliasIds", objectiveMembershipInstance.AliasIds);
            dictionary.Add("declaredName", objectiveMembershipInstance.DeclaredName);
            dictionary.Add("declaredShortName", objectiveMembershipInstance.DeclaredShortName);
            dictionary.Add("elementId", objectiveMembershipInstance.ElementId);
            dictionary.Add("feature", objectiveMembershipInstance.Feature);
            dictionary.Add("isImplied", objectiveMembershipInstance.IsImplied);
            dictionary.Add("isImpliedIncluded", objectiveMembershipInstance.IsImpliedIncluded);
            dictionary.Add("memberElement", objectiveMembershipInstance.MemberElement);
            dictionary.Add("memberName", objectiveMembershipInstance.MemberName);
            dictionary.Add("memberShortName", objectiveMembershipInstance.MemberShortName);
            dictionary.Add("ownedRelatedElement", objectiveMembershipInstance.OwnedRelatedElement);
            dictionary.Add("ownedRelationship", objectiveMembershipInstance.OwnedRelationship);
            dictionary.Add("owningRelatedElement", objectiveMembershipInstance.OwningRelatedElement);
            dictionary.Add("owningRelationship", objectiveMembershipInstance.OwningRelationship);
            dictionary.Add("source", objectiveMembershipInstance.Source);
            dictionary.Add("target", objectiveMembershipInstance.Target);
            dictionary.Add("type", objectiveMembershipInstance.Type);
            dictionary.Add("visibility", objectiveMembershipInstance.Visibility);

            return dictionary;
        }

        /// <summary>
        /// Checks whether the <see cref="IData"/> is not null and whether it is
        /// of type <see cref="IObjectiveMembership"/>
        /// </summary>
        /// <param name="dataItem">
        /// The subject <see cref="IData"/>
        /// </param>
        /// <returns>
        /// an instance of <see cref="IObjectiveMembership"/>
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="thing"/> is null
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown when <paramref name="thing"/> is not of type <see cref="IObjectiveMembership"/>
        /// </exception>
        private static IObjectiveMembership ThingNullAndTypeCheck(IData dataItem)
        {
            if (dataItem == null)
            {
                throw new ArgumentNullException("dataItem", "The dataItem may not be null");
            }

            if (!(dataItem is IObjectiveMembership objectiveMembershipInstance))
            {
                throw new ArgumentException("The dataItem must be of Type IObjectiveMembership", "dataItem");
            }

            return objectiveMembershipInstance;
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
