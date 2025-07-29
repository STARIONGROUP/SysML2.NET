// -------------------------------------------------------------------------------------------------
// <copyright file="ReturnParameterMembershipDictionaryWriter.cs" company="Starion Group S.A.">
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
    /// The purpose of the <see cref="ReturnParameterMembershipDictionaryWriter"/> is to write (convert) a <see cref="IReturnParameterMembership"/>
    /// to a <see cref="Dictionary{String, Object}"/>.
    /// </summary>
    public static class ReturnParameterMembershipDictionaryWriter
    {
        /// <summary>
        /// Writes a <see cref="IReturnParameterMembership"/> to a <see cref="Dictionary{String, Object}"/> that contains a key-value-pair
        /// for each property. The type key is used to store type information (name of the Type).
        /// </summary>
        /// <param name="dataItem">
        /// The subject <see cref="IReturnParameterMembership"/> that is to be written to a <see cref="Dictionary{String, Object}"/>.
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
            var returnParameterMembershipInstance = ThingNullAndTypeCheck(dataItem);

            switch (dictionaryKind)
            {
                case DictionaryKind.Complex:
                    return WriteComplex(returnParameterMembershipInstance);
                case DictionaryKind.Simplified:
                    return WriteSimplified(returnParameterMembershipInstance);
                default:
                    throw new NotSupportedException($"The dictionaryKind:{dictionaryKind} is not supported");
            }
        }

        /// <summary>
        /// Writes a <see cref="IReturnParameterMembership"/> to a <see cref="Dictionary{String, Object}"/> that contains a key-value-pair
        /// for each property. The type key is used to store type information (name of the Type).
        /// </summary>
        /// <param name="returnParameterMembershipInstance">
        /// The subject <see cref="IReturnParameterMembership"/> that is to be written to a <see cref="Dictionary{String, Object}"/>.
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
        private static Dictionary<string, object> WriteSimplified(IReturnParameterMembership returnParameterMembershipInstance)
        {
            var dictionary = new Dictionary<string, object>
            {
                { "@type", "ReturnParameterMembership" },
                { "@id", returnParameterMembershipInstance.Id.ToString() }
            };

            dictionary.Add("aliasIds", returnParameterMembershipInstance.AliasIds);
            dictionary.Add("declaredName", returnParameterMembershipInstance.DeclaredName);
            dictionary.Add("declaredShortName", returnParameterMembershipInstance.DeclaredShortName);
            dictionary.Add("elementId", returnParameterMembershipInstance.ElementId);
            dictionary.Add("isImplied", returnParameterMembershipInstance.IsImplied);
            dictionary.Add("isImpliedIncluded", returnParameterMembershipInstance.IsImpliedIncluded);
            dictionary.Add("memberElement", returnParameterMembershipInstance.MemberElement.ToString());
            dictionary.Add("memberName", returnParameterMembershipInstance.MemberName);
            dictionary.Add("memberShortName", returnParameterMembershipInstance.MemberShortName);
            dictionary.Add("ownedRelatedElement", $"[ {string.Join(",", returnParameterMembershipInstance.OwnedRelatedElement)} ]");
            dictionary.Add("ownedRelationship", $"[ {string.Join(",", returnParameterMembershipInstance.OwnedRelationship)} ]");
            dictionary.Add("owningRelatedElement", returnParameterMembershipInstance.OwningRelatedElement.ToString());
            dictionary.Add("owningRelationship", returnParameterMembershipInstance.OwningRelationship.ToString());
            dictionary.Add("source", $"[ {string.Join(",", returnParameterMembershipInstance.Source)} ]");
            dictionary.Add("target", $"[ {string.Join(",", returnParameterMembershipInstance.Target)} ]");
            dictionary.Add("visibility", returnParameterMembershipInstance.Visibility);

            return dictionary;
        }

        /// <summary>
        /// Writes a <see cref="IReturnParameterMembership"/> to a <see cref="Dictionary{String, Object}"/> that contains a key-value-pair
        /// for each property. The type key is used to store type information (name of the Type).
        /// </summary>
        /// <param name="returnParameterMembershipInstance">
        /// The subject <see cref="IReturnParameterMembership"/> that is to be written to a <see cref="Dictionary{String, Object}"/>.
        /// </param>
        /// <returns>
        /// An instance of <see cref="Dictionary{String, Object}"/> that contains all the properties as key-value-pairs as well
        /// as a type key that is used to store type information (name of the Type).
        /// </returns>
        /// <remarks>
        /// All values are stored as is, no conversion is done
        /// </remarks>
        private static Dictionary<string, object> WriteComplex(IReturnParameterMembership returnParameterMembershipInstance)
        {
            var dictionary = new Dictionary<string, object>
            {
                { "@type", "ReturnParameterMembership" },
                { "@id", returnParameterMembershipInstance.Id }
            };

            dictionary.Add("aliasIds", returnParameterMembershipInstance.AliasIds);
            dictionary.Add("declaredName", returnParameterMembershipInstance.DeclaredName);
            dictionary.Add("declaredShortName", returnParameterMembershipInstance.DeclaredShortName);
            dictionary.Add("elementId", returnParameterMembershipInstance.ElementId);
            dictionary.Add("isImplied", returnParameterMembershipInstance.IsImplied);
            dictionary.Add("isImpliedIncluded", returnParameterMembershipInstance.IsImpliedIncluded);
            dictionary.Add("memberElement", returnParameterMembershipInstance.MemberElement);
            dictionary.Add("memberName", returnParameterMembershipInstance.MemberName);
            dictionary.Add("memberShortName", returnParameterMembershipInstance.MemberShortName);
            dictionary.Add("ownedRelatedElement", returnParameterMembershipInstance.OwnedRelatedElement);
            dictionary.Add("ownedRelationship", returnParameterMembershipInstance.OwnedRelationship);
            dictionary.Add("owningRelatedElement", returnParameterMembershipInstance.OwningRelatedElement);
            dictionary.Add("owningRelationship", returnParameterMembershipInstance.OwningRelationship);
            dictionary.Add("source", returnParameterMembershipInstance.Source);
            dictionary.Add("target", returnParameterMembershipInstance.Target);
            dictionary.Add("visibility", returnParameterMembershipInstance.Visibility);

            return dictionary;
        }

        /// <summary>
        /// Checks whether the <see cref="IData"/> is not null and whether it is
        /// of type <see cref="IReturnParameterMembership"/>
        /// </summary>
        /// <param name="dataItem">
        /// The subject <see cref="IData"/>
        /// </param>
        /// <returns>
        /// an instance of <see cref="IReturnParameterMembership"/>
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="thing"/> is null
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown when <paramref name="thing"/> is not of type <see cref="IReturnParameterMembership"/>
        /// </exception>
        private static IReturnParameterMembership ThingNullAndTypeCheck(IData dataItem)
        {
            if (dataItem == null)
            {
                throw new ArgumentNullException("dataItem", "The dataItem may not be null");
            }

            if (!(dataItem is IReturnParameterMembership returnParameterMembershipInstance))
            {
                throw new ArgumentException("The dataItem must be of Type IReturnParameterMembership", "dataItem");
            }

            return returnParameterMembershipInstance;
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
