// -------------------------------------------------------------------------------------------------
// <copyright file="OwningMembershipDictionaryWriter.cs" company="RHEA System S.A.">
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
    /// The purpose of the <see cref="OwningMembershipDictionaryWriter"/> is to write (convert) a <see cref="IOwningMembership"/>
    /// to a <see cref="Dictionary{String, Object}"/>.
    /// </summary>
    public static class OwningMembershipDictionaryWriter
    {
        /// <summary>
        /// Writes a <see cref="IOwningMembership"/> to a <see cref="Dictionary{String, Object}"/> that contains a key-value-pair
        /// for each property. The type key is used to store type information (name of the Type).
        /// </summary>
        /// <param name="dataItem">
        /// The subject <see cref="IOwningMembership"/> that is to be written to a <see cref="Dictionary{String, Object}"/>.
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
            var owningMembershipInstance = ThingNullAndTypeCheck(dataItem);

            switch (dictionaryKind)
            {
                case DictionaryKind.Complex:
                    return WriteComplex(owningMembershipInstance);
                case DictionaryKind.Simplified:
                    return WriteSimplified(owningMembershipInstance);
                default:
                    throw new NotSupportedException($"The dictionaryKind:{dictionaryKind} is not supported");
            }
        }

        /// <summary>
        /// Writes a <see cref="IOwningMembership"/> to a <see cref="Dictionary{String, Object}"/> that contains a key-value-pair
        /// for each property. The type key is used to store type information (name of the Type).
        /// </summary>
        /// <param name="owningMembershipInstance">
        /// The subject <see cref="IOwningMembership"/> that is to be written to a <see cref="Dictionary{String, Object}"/>.
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
        private static Dictionary<string, object> WriteSimplified(IOwningMembership owningMembershipInstance)
        {
            var dictionary = new Dictionary<string, object>
            {
                { "@type", "OwningMembership" },
                { "@id", owningMembershipInstance.Id.ToString() }
            };

            dictionary.Add("aliasIds", owningMembershipInstance.AliasIds);
            dictionary.Add("elementId", owningMembershipInstance.ElementId);
            dictionary.Add("isImplied", owningMembershipInstance.IsImplied);
            dictionary.Add("isImpliedIncluded", owningMembershipInstance.IsImpliedIncluded);
            dictionary.Add("memberElement", owningMembershipInstance.MemberElement.ToString());
            dictionary.Add("memberName", owningMembershipInstance.MemberName);
            dictionary.Add("memberShortName", owningMembershipInstance.MemberShortName);
            dictionary.Add("name", owningMembershipInstance.Name);
            dictionary.Add("ownedRelatedElement", $"[ {string.Join(",", owningMembershipInstance.OwnedRelatedElement)} ]");
            dictionary.Add("ownedRelationship", $"[ {string.Join(",", owningMembershipInstance.OwnedRelationship)} ]");
            dictionary.Add("owningRelatedElement", owningMembershipInstance.OwningRelatedElement.ToString());
            dictionary.Add("owningRelationship", owningMembershipInstance.OwningRelationship.ToString());
            dictionary.Add("shortName", owningMembershipInstance.ShortName);
            dictionary.Add("source", $"[ {string.Join(",", owningMembershipInstance.Source)} ]");
            dictionary.Add("target", $"[ {string.Join(",", owningMembershipInstance.Target)} ]");
            dictionary.Add("visibility", owningMembershipInstance.Visibility);

            return dictionary;
        }

        /// <summary>
        /// Writes a <see cref="IOwningMembership"/> to a <see cref="Dictionary{String, Object}"/> that contains a key-value-pair
        /// for each property. The type key is used to store type information (name of the Type).
        /// </summary>
        /// <param name="owningMembershipInstance">
        /// The subject <see cref="IOwningMembership"/> that is to be written to a <see cref="Dictionary{String, Object}"/>.
        /// </param>
        /// <returns>
        /// An instance of <see cref="Dictionary{String, Object}"/> that contains all the properties as key-value-pairs as well
        /// as a type key that is used to store type information (name of the Type).
        /// </returns>
        /// <remarks>
        /// All values are stored as is, no conversion is done
        /// </remarks>
        private static Dictionary<string, object> WriteComplex(IOwningMembership owningMembershipInstance)
        {
            var dictionary = new Dictionary<string, object>
            {
                { "@type", "OwningMembership" },
                { "@id", owningMembershipInstance.Id }
            };

            dictionary.Add("aliasIds", owningMembershipInstance.AliasIds);
            dictionary.Add("elementId", owningMembershipInstance.ElementId);
            dictionary.Add("isImplied", owningMembershipInstance.IsImplied);
            dictionary.Add("isImpliedIncluded", owningMembershipInstance.IsImpliedIncluded);
            dictionary.Add("memberElement", owningMembershipInstance.MemberElement);
            dictionary.Add("memberName", owningMembershipInstance.MemberName);
            dictionary.Add("memberShortName", owningMembershipInstance.MemberShortName);
            dictionary.Add("name", owningMembershipInstance.Name);
            dictionary.Add("ownedRelatedElement", owningMembershipInstance.OwnedRelatedElement);
            dictionary.Add("ownedRelationship", owningMembershipInstance.OwnedRelationship);
            dictionary.Add("owningRelatedElement", owningMembershipInstance.OwningRelatedElement);
            dictionary.Add("owningRelationship", owningMembershipInstance.OwningRelationship);
            dictionary.Add("shortName", owningMembershipInstance.ShortName);
            dictionary.Add("source", owningMembershipInstance.Source);
            dictionary.Add("target", owningMembershipInstance.Target);
            dictionary.Add("visibility", owningMembershipInstance.Visibility);

            return dictionary;
        }

        /// <summary>
        /// Checks whether the <see cref="IData"/> is not null and whether it is
        /// of type <see cref="IOwningMembership"/>
        /// </summary>
        /// <param name="dataItem">
        /// The subject <see cref="IData"/>
        /// </param>
        /// <returns>
        /// an instance of <see cref="IOwningMembership"/>
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="thing"/> is null
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown when <paramref name="thing"/> is not of type <see cref="IOwningMembership"/>
        /// </exception>
        private static IOwningMembership ThingNullAndTypeCheck(IData dataItem)
        {
            if (dataItem == null)
            {
                throw new ArgumentNullException("dataItem", "The dataItem may not be null");
            }

            if (!(dataItem is IOwningMembership owningMembershipInstance))
            {
                throw new ArgumentException("The dataItem must be of Type IOwningMembership", "dataItem");
            }

            return owningMembershipInstance;
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
