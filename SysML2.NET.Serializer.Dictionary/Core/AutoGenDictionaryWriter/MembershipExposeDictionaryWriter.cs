// -------------------------------------------------------------------------------------------------
// <copyright file="MembershipExposeDictionaryWriter.cs" company="Starion Group S.A.">
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
    /// The purpose of the <see cref="MembershipExposeDictionaryWriter"/> is to write (convert) a <see cref="IMembershipExpose"/>
    /// to a <see cref="Dictionary{String, Object}"/>.
    /// </summary>
    public static class MembershipExposeDictionaryWriter
    {
        /// <summary>
        /// Writes a <see cref="IMembershipExpose"/> to a <see cref="Dictionary{String, Object}"/> that contains a key-value-pair
        /// for each property. The type key is used to store type information (name of the Type).
        /// </summary>
        /// <param name="dataItem">
        /// The subject <see cref="IMembershipExpose"/> that is to be written to a <see cref="Dictionary{String, Object}"/>.
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
            var membershipExposeInstance = ThingNullAndTypeCheck(dataItem);

            switch (dictionaryKind)
            {
                case DictionaryKind.Complex:
                    return WriteComplex(membershipExposeInstance);
                case DictionaryKind.Simplified:
                    return WriteSimplified(membershipExposeInstance);
                default:
                    throw new NotSupportedException($"The dictionaryKind:{dictionaryKind} is not supported");
            }
        }

        /// <summary>
        /// Writes a <see cref="IMembershipExpose"/> to a <see cref="Dictionary{String, Object}"/> that contains a key-value-pair
        /// for each property. The type key is used to store type information (name of the Type).
        /// </summary>
        /// <param name="membershipExposeInstance">
        /// The subject <see cref="IMembershipExpose"/> that is to be written to a <see cref="Dictionary{String, Object}"/>.
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
        private static Dictionary<string, object> WriteSimplified(IMembershipExpose membershipExposeInstance)
        {
            var dictionary = new Dictionary<string, object>
            {
                { "@type", "MembershipExpose" },
                { "@id", membershipExposeInstance.Id.ToString() }
            };

            dictionary.Add("aliasIds", membershipExposeInstance.AliasIds);
            dictionary.Add("declaredName", membershipExposeInstance.DeclaredName);
            dictionary.Add("declaredShortName", membershipExposeInstance.DeclaredShortName);
            dictionary.Add("elementId", membershipExposeInstance.ElementId);
            dictionary.Add("importedMembership", membershipExposeInstance.ImportedMembership.ToString());
            dictionary.Add("isImplied", membershipExposeInstance.IsImplied);
            dictionary.Add("isImpliedIncluded", membershipExposeInstance.IsImpliedIncluded);
            dictionary.Add("isImportAll", membershipExposeInstance.IsImportAll);
            dictionary.Add("isRecursive", membershipExposeInstance.IsRecursive);
            dictionary.Add("ownedRelatedElement", $"[ {string.Join(",", membershipExposeInstance.OwnedRelatedElement)} ]");
            dictionary.Add("ownedRelationship", $"[ {string.Join(",", membershipExposeInstance.OwnedRelationship)} ]");
            dictionary.Add("owningRelatedElement", membershipExposeInstance.OwningRelatedElement.ToString());
            dictionary.Add("owningRelationship", membershipExposeInstance.OwningRelationship.ToString());
            dictionary.Add("source", $"[ {string.Join(",", membershipExposeInstance.Source)} ]");
            dictionary.Add("target", $"[ {string.Join(",", membershipExposeInstance.Target)} ]");
            dictionary.Add("visibility", membershipExposeInstance.Visibility);

            return dictionary;
        }

        /// <summary>
        /// Writes a <see cref="IMembershipExpose"/> to a <see cref="Dictionary{String, Object}"/> that contains a key-value-pair
        /// for each property. The type key is used to store type information (name of the Type).
        /// </summary>
        /// <param name="membershipExposeInstance">
        /// The subject <see cref="IMembershipExpose"/> that is to be written to a <see cref="Dictionary{String, Object}"/>.
        /// </param>
        /// <returns>
        /// An instance of <see cref="Dictionary{String, Object}"/> that contains all the properties as key-value-pairs as well
        /// as a type key that is used to store type information (name of the Type).
        /// </returns>
        /// <remarks>
        /// All values are stored as is, no conversion is done
        /// </remarks>
        private static Dictionary<string, object> WriteComplex(IMembershipExpose membershipExposeInstance)
        {
            var dictionary = new Dictionary<string, object>
            {
                { "@type", "MembershipExpose" },
                { "@id", membershipExposeInstance.Id }
            };

            dictionary.Add("aliasIds", membershipExposeInstance.AliasIds);
            dictionary.Add("declaredName", membershipExposeInstance.DeclaredName);
            dictionary.Add("declaredShortName", membershipExposeInstance.DeclaredShortName);
            dictionary.Add("elementId", membershipExposeInstance.ElementId);
            dictionary.Add("importedMembership", membershipExposeInstance.ImportedMembership);
            dictionary.Add("isImplied", membershipExposeInstance.IsImplied);
            dictionary.Add("isImpliedIncluded", membershipExposeInstance.IsImpliedIncluded);
            dictionary.Add("isImportAll", membershipExposeInstance.IsImportAll);
            dictionary.Add("isRecursive", membershipExposeInstance.IsRecursive);
            dictionary.Add("ownedRelatedElement", membershipExposeInstance.OwnedRelatedElement);
            dictionary.Add("ownedRelationship", membershipExposeInstance.OwnedRelationship);
            dictionary.Add("owningRelatedElement", membershipExposeInstance.OwningRelatedElement);
            dictionary.Add("owningRelationship", membershipExposeInstance.OwningRelationship);
            dictionary.Add("source", membershipExposeInstance.Source);
            dictionary.Add("target", membershipExposeInstance.Target);
            dictionary.Add("visibility", membershipExposeInstance.Visibility);

            return dictionary;
        }

        /// <summary>
        /// Checks whether the <see cref="IData"/> is not null and whether it is
        /// of type <see cref="IMembershipExpose"/>
        /// </summary>
        /// <param name="dataItem">
        /// The subject <see cref="IData"/>
        /// </param>
        /// <returns>
        /// an instance of <see cref="IMembershipExpose"/>
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="thing"/> is null
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown when <paramref name="thing"/> is not of type <see cref="IMembershipExpose"/>
        /// </exception>
        private static IMembershipExpose ThingNullAndTypeCheck(IData dataItem)
        {
            if (dataItem == null)
            {
                throw new ArgumentNullException("dataItem", "The dataItem may not be null");
            }

            if (!(dataItem is IMembershipExpose membershipExposeInstance))
            {
                throw new ArgumentException("The dataItem must be of Type IMembershipExpose", "dataItem");
            }

            return membershipExposeInstance;
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
