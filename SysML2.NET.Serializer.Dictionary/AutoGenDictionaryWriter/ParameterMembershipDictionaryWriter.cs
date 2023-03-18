// -------------------------------------------------------------------------------------------------
// <copyright file="ParameterMembershipDictionaryWriter.cs" company="RHEA System S.A.">
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
    /// The purpose of the <see cref="ParameterMembershipDictionaryWriter"/> is to write (convert) a <see cref="IParameterMembership"/>
    /// to a <see cref="Dictionary{String, Object}"/>.
    /// </summary>
    public static class ParameterMembershipDictionaryWriter
    {
        /// <summary>
        /// Writes a <see cref="IParameterMembership"/> to a <see cref="Dictionary{String, Object}"/> that contains a key-value-pair
        /// for each property. The type key is used to store type information (name of the Type).
        /// </summary>
        /// <param name="dataItem">
        /// The subject <see cref="IParameterMembership"/> that is to be written to a <see cref="Dictionary{String, Object}"/>.
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
            var parameterMembershipInstance = ThingNullAndTypeCheck(dataItem);

            switch (dictionaryKind)
            {
                case DictionaryKind.Complex:
                    return WriteComplex(parameterMembershipInstance);
                case DictionaryKind.Simplified:
                    return WriteSimplified(parameterMembershipInstance);
                default:
                    throw new NotSupportedException($"The dictionaryKind:{dictionaryKind} is not supported");
            }
        }

        /// <summary>
        /// Writes a <see cref="IParameterMembership"/> to a <see cref="Dictionary{String, Object}"/> that contains a key-value-pair
        /// for each property. The type key is used to store type information (name of the Type).
        /// </summary>
        /// <param name="parameterMembershipInstance">
        /// The subject <see cref="IParameterMembership"/> that is to be written to a <see cref="Dictionary{String, Object}"/>.
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
        private static Dictionary<string, object> WriteSimplified(IParameterMembership parameterMembershipInstance)
        {
            var dictionary = new Dictionary<string, object>
            {
                { "@type", "ParameterMembership" },
                { "@id", parameterMembershipInstance.Id.ToString() }
            };

            dictionary.Add("aliasIds", parameterMembershipInstance.AliasIds);
            dictionary.Add("elementId", parameterMembershipInstance.ElementId);
            dictionary.Add("feature", parameterMembershipInstance.Feature.ToString());
            dictionary.Add("isImplied", parameterMembershipInstance.IsImplied);
            dictionary.Add("isImpliedIncluded", parameterMembershipInstance.IsImpliedIncluded);
            dictionary.Add("memberElement", parameterMembershipInstance.MemberElement.ToString());
            dictionary.Add("memberName", parameterMembershipInstance.MemberName);
            dictionary.Add("memberShortName", parameterMembershipInstance.MemberShortName);
            dictionary.Add("name", parameterMembershipInstance.Name);
            dictionary.Add("ownedRelatedElement", $"[ {string.Join(",", parameterMembershipInstance.OwnedRelatedElement)} ]");
            dictionary.Add("ownedRelationship", $"[ {string.Join(",", parameterMembershipInstance.OwnedRelationship)} ]");
            dictionary.Add("owningRelatedElement", parameterMembershipInstance.OwningRelatedElement.ToString());
            dictionary.Add("owningRelationship", parameterMembershipInstance.OwningRelationship.ToString());
            dictionary.Add("shortName", parameterMembershipInstance.ShortName);
            dictionary.Add("source", $"[ {string.Join(",", parameterMembershipInstance.Source)} ]");
            dictionary.Add("target", $"[ {string.Join(",", parameterMembershipInstance.Target)} ]");
            dictionary.Add("type", parameterMembershipInstance.Type.ToString());
            dictionary.Add("visibility", parameterMembershipInstance.Visibility);

            return dictionary;
        }

        /// <summary>
        /// Writes a <see cref="IParameterMembership"/> to a <see cref="Dictionary{String, Object}"/> that contains a key-value-pair
        /// for each property. The type key is used to store type information (name of the Type).
        /// </summary>
        /// <param name="parameterMembershipInstance">
        /// The subject <see cref="IParameterMembership"/> that is to be written to a <see cref="Dictionary{String, Object}"/>.
        /// </param>
        /// <returns>
        /// An instance of <see cref="Dictionary{String, Object}"/> that contains all the properties as key-value-pairs as well
        /// as a type key that is used to store type information (name of the Type).
        /// </returns>
        /// <remarks>
        /// All values are stored as is, no conversion is done
        /// </remarks>
        private static Dictionary<string, object> WriteComplex(IParameterMembership parameterMembershipInstance)
        {
            var dictionary = new Dictionary<string, object>
            {
                { "@type", "ParameterMembership" },
                { "@id", parameterMembershipInstance.Id }
            };

            dictionary.Add("aliasIds", parameterMembershipInstance.AliasIds);
            dictionary.Add("elementId", parameterMembershipInstance.ElementId);
            dictionary.Add("feature", parameterMembershipInstance.Feature);
            dictionary.Add("isImplied", parameterMembershipInstance.IsImplied);
            dictionary.Add("isImpliedIncluded", parameterMembershipInstance.IsImpliedIncluded);
            dictionary.Add("memberElement", parameterMembershipInstance.MemberElement);
            dictionary.Add("memberName", parameterMembershipInstance.MemberName);
            dictionary.Add("memberShortName", parameterMembershipInstance.MemberShortName);
            dictionary.Add("name", parameterMembershipInstance.Name);
            dictionary.Add("ownedRelatedElement", parameterMembershipInstance.OwnedRelatedElement);
            dictionary.Add("ownedRelationship", parameterMembershipInstance.OwnedRelationship);
            dictionary.Add("owningRelatedElement", parameterMembershipInstance.OwningRelatedElement);
            dictionary.Add("owningRelationship", parameterMembershipInstance.OwningRelationship);
            dictionary.Add("shortName", parameterMembershipInstance.ShortName);
            dictionary.Add("source", parameterMembershipInstance.Source);
            dictionary.Add("target", parameterMembershipInstance.Target);
            dictionary.Add("type", parameterMembershipInstance.Type);
            dictionary.Add("visibility", parameterMembershipInstance.Visibility);

            return dictionary;
        }

        /// <summary>
        /// Checks whether the <see cref="IData"/> is not null and whether it is
        /// of type <see cref="IParameterMembership"/>
        /// </summary>
        /// <param name="dataItem">
        /// The subject <see cref="IData"/>
        /// </param>
        /// <returns>
        /// an instance of <see cref="IParameterMembership"/>
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="thing"/> is null
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown when <paramref name="thing"/> is not of type <see cref="IParameterMembership"/>
        /// </exception>
        private static IParameterMembership ThingNullAndTypeCheck(IData dataItem)
        {
            if (dataItem == null)
            {
                throw new ArgumentNullException("dataItem", "The dataItem may not be null");
            }

            if (!(dataItem is IParameterMembership parameterMembershipInstance))
            {
                throw new ArgumentException("The dataItem must be of Type IParameterMembership", "dataItem");
            }

            return parameterMembershipInstance;
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
