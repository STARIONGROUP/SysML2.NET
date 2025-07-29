// -------------------------------------------------------------------------------------------------
// <copyright file="ViewRenderingMembershipDictionaryWriter.cs" company="Starion Group S.A.">
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
    /// The purpose of the <see cref="ViewRenderingMembershipDictionaryWriter"/> is to write (convert) a <see cref="IViewRenderingMembership"/>
    /// to a <see cref="Dictionary{String, Object}"/>.
    /// </summary>
    public static class ViewRenderingMembershipDictionaryWriter
    {
        /// <summary>
        /// Writes a <see cref="IViewRenderingMembership"/> to a <see cref="Dictionary{String, Object}"/> that contains a key-value-pair
        /// for each property. The type key is used to store type information (name of the Type).
        /// </summary>
        /// <param name="dataItem">
        /// The subject <see cref="IViewRenderingMembership"/> that is to be written to a <see cref="Dictionary{String, Object}"/>.
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
            var viewRenderingMembershipInstance = ThingNullAndTypeCheck(dataItem);

            switch (dictionaryKind)
            {
                case DictionaryKind.Complex:
                    return WriteComplex(viewRenderingMembershipInstance);
                case DictionaryKind.Simplified:
                    return WriteSimplified(viewRenderingMembershipInstance);
                default:
                    throw new NotSupportedException($"The dictionaryKind:{dictionaryKind} is not supported");
            }
        }

        /// <summary>
        /// Writes a <see cref="IViewRenderingMembership"/> to a <see cref="Dictionary{String, Object}"/> that contains a key-value-pair
        /// for each property. The type key is used to store type information (name of the Type).
        /// </summary>
        /// <param name="viewRenderingMembershipInstance">
        /// The subject <see cref="IViewRenderingMembership"/> that is to be written to a <see cref="Dictionary{String, Object}"/>.
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
        private static Dictionary<string, object> WriteSimplified(IViewRenderingMembership viewRenderingMembershipInstance)
        {
            var dictionary = new Dictionary<string, object>
            {
                { "@type", "ViewRenderingMembership" },
                { "@id", viewRenderingMembershipInstance.Id.ToString() }
            };

            dictionary.Add("aliasIds", viewRenderingMembershipInstance.AliasIds);
            dictionary.Add("declaredName", viewRenderingMembershipInstance.DeclaredName);
            dictionary.Add("declaredShortName", viewRenderingMembershipInstance.DeclaredShortName);
            dictionary.Add("elementId", viewRenderingMembershipInstance.ElementId);
            dictionary.Add("isImplied", viewRenderingMembershipInstance.IsImplied);
            dictionary.Add("isImpliedIncluded", viewRenderingMembershipInstance.IsImpliedIncluded);
            dictionary.Add("memberElement", viewRenderingMembershipInstance.MemberElement.ToString());
            dictionary.Add("memberName", viewRenderingMembershipInstance.MemberName);
            dictionary.Add("memberShortName", viewRenderingMembershipInstance.MemberShortName);
            dictionary.Add("ownedRelatedElement", $"[ {string.Join(",", viewRenderingMembershipInstance.OwnedRelatedElement)} ]");
            dictionary.Add("ownedRelationship", $"[ {string.Join(",", viewRenderingMembershipInstance.OwnedRelationship)} ]");
            dictionary.Add("owningRelatedElement", viewRenderingMembershipInstance.OwningRelatedElement.ToString());
            dictionary.Add("owningRelationship", viewRenderingMembershipInstance.OwningRelationship.ToString());
            dictionary.Add("source", $"[ {string.Join(",", viewRenderingMembershipInstance.Source)} ]");
            dictionary.Add("target", $"[ {string.Join(",", viewRenderingMembershipInstance.Target)} ]");
            dictionary.Add("visibility", viewRenderingMembershipInstance.Visibility);

            return dictionary;
        }

        /// <summary>
        /// Writes a <see cref="IViewRenderingMembership"/> to a <see cref="Dictionary{String, Object}"/> that contains a key-value-pair
        /// for each property. The type key is used to store type information (name of the Type).
        /// </summary>
        /// <param name="viewRenderingMembershipInstance">
        /// The subject <see cref="IViewRenderingMembership"/> that is to be written to a <see cref="Dictionary{String, Object}"/>.
        /// </param>
        /// <returns>
        /// An instance of <see cref="Dictionary{String, Object}"/> that contains all the properties as key-value-pairs as well
        /// as a type key that is used to store type information (name of the Type).
        /// </returns>
        /// <remarks>
        /// All values are stored as is, no conversion is done
        /// </remarks>
        private static Dictionary<string, object> WriteComplex(IViewRenderingMembership viewRenderingMembershipInstance)
        {
            var dictionary = new Dictionary<string, object>
            {
                { "@type", "ViewRenderingMembership" },
                { "@id", viewRenderingMembershipInstance.Id }
            };

            dictionary.Add("aliasIds", viewRenderingMembershipInstance.AliasIds);
            dictionary.Add("declaredName", viewRenderingMembershipInstance.DeclaredName);
            dictionary.Add("declaredShortName", viewRenderingMembershipInstance.DeclaredShortName);
            dictionary.Add("elementId", viewRenderingMembershipInstance.ElementId);
            dictionary.Add("isImplied", viewRenderingMembershipInstance.IsImplied);
            dictionary.Add("isImpliedIncluded", viewRenderingMembershipInstance.IsImpliedIncluded);
            dictionary.Add("memberElement", viewRenderingMembershipInstance.MemberElement);
            dictionary.Add("memberName", viewRenderingMembershipInstance.MemberName);
            dictionary.Add("memberShortName", viewRenderingMembershipInstance.MemberShortName);
            dictionary.Add("ownedRelatedElement", viewRenderingMembershipInstance.OwnedRelatedElement);
            dictionary.Add("ownedRelationship", viewRenderingMembershipInstance.OwnedRelationship);
            dictionary.Add("owningRelatedElement", viewRenderingMembershipInstance.OwningRelatedElement);
            dictionary.Add("owningRelationship", viewRenderingMembershipInstance.OwningRelationship);
            dictionary.Add("source", viewRenderingMembershipInstance.Source);
            dictionary.Add("target", viewRenderingMembershipInstance.Target);
            dictionary.Add("visibility", viewRenderingMembershipInstance.Visibility);

            return dictionary;
        }

        /// <summary>
        /// Checks whether the <see cref="IData"/> is not null and whether it is
        /// of type <see cref="IViewRenderingMembership"/>
        /// </summary>
        /// <param name="dataItem">
        /// The subject <see cref="IData"/>
        /// </param>
        /// <returns>
        /// an instance of <see cref="IViewRenderingMembership"/>
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="thing"/> is null
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown when <paramref name="thing"/> is not of type <see cref="IViewRenderingMembership"/>
        /// </exception>
        private static IViewRenderingMembership ThingNullAndTypeCheck(IData dataItem)
        {
            if (dataItem == null)
            {
                throw new ArgumentNullException("dataItem", "The dataItem may not be null");
            }

            if (!(dataItem is IViewRenderingMembership viewRenderingMembershipInstance))
            {
                throw new ArgumentException("The dataItem must be of Type IViewRenderingMembership", "dataItem");
            }

            return viewRenderingMembershipInstance;
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
