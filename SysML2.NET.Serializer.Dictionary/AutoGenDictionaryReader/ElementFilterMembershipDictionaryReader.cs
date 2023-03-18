// -------------------------------------------------------------------------------------------------
// <copyright file="ElementFilterMembershipDictionaryReader.cs" company="RHEA System S.A.">
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
    using System.Linq;

    using SysML2.NET.Common;
    using SysML2.NET.Core;
    using SysML2.NET.Core.DTO;

    /// <summary>
    /// The purpose of the <see cref="ElementFilterMembershipDictionaryReader"/> is to read (convert)
    /// a <see cref="Dictionary{String, Object}"/> from an <see cref="IElementFilterMembership"/>
    /// </summary>
    public static class ElementFilterMembershipDictionaryReader
    {
        /// <summary>
        /// Reads a <see cref="IElementFilterMembership"/> from a <see cref="Dictionary{String, Object}"/> that contains a key-value-pair
        /// for each property.
        /// </summary>
        /// <param name="dictionary">
        /// The subject <see cref="Dictionary{String, Object}"/> that is to be converted into a <see cref="IElementFilterMembership"/>
        /// </param>
        /// <param name="dictionaryKind">
        /// The source <see cref="DictionaryKind"/> that is to be read from
        /// </param>
        /// <returns>
        /// An instance of <see cref="IElementFilterMembership"/>
        /// </returns>
        /// <remarks>
        /// When the <see cref="DictionaryKind"/> is <see cref="DictionaryKind.Complex"/> then the values that are read from the
        /// <see cref="Dictionary{String, Object}"/> are read as is. When the <see cref="DictionaryKind"/> is <see cref="DictionaryKind.Simplified"/>
        /// then the following applies:
        /// The values that are of the following types are read as is:
        ///   - Number, an abstract type, which has the subtypes Integer and Float
        ///   - String
        ///   - Boolean
        ///   - The spatial type Point
        ///   - Temporal types: Date, Time, LocalTime, DateTime, LocalDateTime and Duration
        /// values of other types are converted from string, in case these are an <see cref="IEnumerable{T}"/> then
        /// the values are converted from an Array of String using JSON notation, i.e. [ value_1, ..., value_n ]
        /// </remarks>
        public static IData Read(Dictionary<string, object> dictionary, DictionaryKind dictionaryKind)
        {
            switch (dictionaryKind)
            {
                case DictionaryKind.Complex:
                    return ReadComplex(dictionary);
                case DictionaryKind.Simplified:
                    return ReadSimplified(dictionary);
                default:
                    throw new NotSupportedException($"The dictionaryKind:{dictionaryKind} is not supported");
            }
        }

        /// <summary>
        /// Reads a <see cref="IElementFilterMembership"/> from a <see cref="Dictionary{String, Object}"/> that contains a key-value-pair
        /// for each property.
        /// </summary>
        /// <param name="dictionary">
        /// The subject <see cref="Dictionary{String, Object}"/> that is to be converted into a <see cref="IElementFilterMembership"/>
        /// </param>
        /// <returns>
        /// An instance of <see cref="IElementFilterMembership"/>
        /// </returns>
        /// <remarks>
        /// When the <see cref="DictionaryKind"/> is <see cref="DictionaryKind.Complex"/> then the values that are read from the
        /// <see cref="Dictionary{String, Object}"/> are read as is. When the <see cref="DictionaryKind"/> is <see cref="DictionaryKind.Simplified"/>
        /// then the following applies:
        /// The values that are of the following types are read as is:
        ///   - Number, an abstract type, which has the subtypes Integer and Float
        ///   - String
        ///   - Boolean
        ///   - The spatial type Point
        ///   - Temporal types: Date, Time, LocalTime, DateTime, LocalDateTime and Duration
        /// values of other types are converted from string, in case these are an <see cref="IEnumerable{T}"/> then
        /// the values are converted from an Array of String using JSON notation, i.e. [ value_1, ..., value_n ]
        /// </remarks>
        private static IElementFilterMembership ReadSimplified(Dictionary<string, object> dictionary)
        {
            var elementFilterMembershipInstance = DictionaryNullAndTypeCheck(dictionary);

            if (!dictionary.TryGetValue("aliasIds", out object aliasIdsObject))
            {
                throw new ArgumentException("The aliasIds property is missing from the dictionary, the dictionary cannot be converted into a ElementFilterMembership");
            }
            List<string> aliasIdsFeature = aliasIdsObject as List<string>;

            if (!dictionary.TryGetValue("declaredName", out object declaredNameObject))
            {
                throw new ArgumentException("The declaredName property is missing from the dictionary, the dictionary cannot be converted into a ElementFilterMembership");
            }
            string declaredNameFeature = declaredNameObject == null ? null : Convert.ToString(declaredNameObject);

            if (!dictionary.TryGetValue("declaredShortName", out object declaredShortNameObject))
            {
                throw new ArgumentException("The declaredShortName property is missing from the dictionary, the dictionary cannot be converted into a ElementFilterMembership");
            }
            string declaredShortNameFeature = declaredShortNameObject == null ? null : Convert.ToString(declaredShortNameObject);

            if (!dictionary.TryGetValue("elementId", out object elementIdObject))
            {
                throw new ArgumentException("The elementId property is missing from the dictionary, the dictionary cannot be converted into a ElementFilterMembership");
            }
            string elementIdFeature = Convert.ToString(elementIdObject);

            if (!dictionary.TryGetValue("isImplied", out object isImpliedObject))
            {
                throw new ArgumentException("The isImplied property is missing from the dictionary, the dictionary cannot be converted into a ElementFilterMembership");
            }
            bool isImpliedFeature = Convert.ToBoolean(isImpliedObject);

            if (!dictionary.TryGetValue("isImpliedIncluded", out object isImpliedIncludedObject))
            {
                throw new ArgumentException("The isImpliedIncluded property is missing from the dictionary, the dictionary cannot be converted into a ElementFilterMembership");
            }
            bool isImpliedIncludedFeature = Convert.ToBoolean(isImpliedIncludedObject);

            if (!dictionary.TryGetValue("memberElement", out object memberElementObject))
            {
                throw new ArgumentException("The memberElement property is missing from the dictionary, the dictionary cannot be converted into a ElementFilterMembership");
            }
            Guid memberElementFeature = Guid.Parse(Convert.ToString(memberElementObject));

            if (!dictionary.TryGetValue("memberName", out object memberNameObject))
            {
                throw new ArgumentException("The memberName property is missing from the dictionary, the dictionary cannot be converted into a ElementFilterMembership");
            }
            string memberNameFeature = memberNameObject == null ? null : Convert.ToString(memberNameObject);

            if (!dictionary.TryGetValue("memberShortName", out object memberShortNameObject))
            {
                throw new ArgumentException("The memberShortName property is missing from the dictionary, the dictionary cannot be converted into a ElementFilterMembership");
            }
            string memberShortNameFeature = memberShortNameObject == null ? null : Convert.ToString(memberShortNameObject);

            if (!dictionary.TryGetValue("ownedRelatedElement", out object ownedRelatedElementObject))
            {
                throw new ArgumentException("The ownedRelatedElement property is missing from the dictionary, the dictionary cannot be converted into a ElementFilterMembership");
            }
            List<Guid> ownedRelatedElementFeature = (ownedRelatedElementObject as List<string>)?.Select(Guid.Parse).ToList();

            if (!dictionary.TryGetValue("ownedRelationship", out object ownedRelationshipObject))
            {
                throw new ArgumentException("The ownedRelationship property is missing from the dictionary, the dictionary cannot be converted into a ElementFilterMembership");
            }
            List<Guid> ownedRelationshipFeature = (ownedRelationshipObject as List<string>)?.Select(Guid.Parse).ToList();

            if (!dictionary.TryGetValue("owningRelatedElement", out object owningRelatedElementObject))
            {
                throw new ArgumentException("The owningRelatedElement property is missing from the dictionary, the dictionary cannot be converted into a ElementFilterMembership");
            }
            Guid? owningRelatedElementFeature = owningRelatedElementObject == null ? (Guid?)null : Guid.Parse(Convert.ToString(owningRelatedElementObject));

            if (!dictionary.TryGetValue("owningRelationship", out object owningRelationshipObject))
            {
                throw new ArgumentException("The owningRelationship property is missing from the dictionary, the dictionary cannot be converted into a ElementFilterMembership");
            }
            Guid? owningRelationshipFeature = owningRelationshipObject == null ? (Guid?)null : Guid.Parse(Convert.ToString(owningRelationshipObject));

            if (!dictionary.TryGetValue("source", out object sourceObject))
            {
                throw new ArgumentException("The source property is missing from the dictionary, the dictionary cannot be converted into a ElementFilterMembership");
            }
            List<Guid> sourceFeature = (sourceObject as List<string>)?.Select(Guid.Parse).ToList();

            if (!dictionary.TryGetValue("target", out object targetObject))
            {
                throw new ArgumentException("The target property is missing from the dictionary, the dictionary cannot be converted into a ElementFilterMembership");
            }
            List<Guid> targetFeature = (targetObject as List<string>)?.Select(Guid.Parse).ToList();

            if (!dictionary.TryGetValue("visibility", out object visibilityObject))
            {
                throw new ArgumentException("The visibility property is missing from the dictionary, the dictionary cannot be converted into a ElementFilterMembership");
            }
            VisibilityKind visibilityFeature = (VisibilityKind)Enum.Parse(typeof(VisibilityKind), Convert.ToString(visibilityObject), true);


            elementFilterMembershipInstance.AliasIds = aliasIdsFeature ?? new List<string>();
            elementFilterMembershipInstance.DeclaredName = declaredNameFeature;
            elementFilterMembershipInstance.DeclaredShortName = declaredShortNameFeature;
            elementFilterMembershipInstance.ElementId = elementIdFeature;
            elementFilterMembershipInstance.IsImplied = isImpliedFeature;
            elementFilterMembershipInstance.IsImpliedIncluded = isImpliedIncludedFeature;
            elementFilterMembershipInstance.MemberElement = memberElementFeature;
            elementFilterMembershipInstance.MemberName = memberNameFeature;
            elementFilterMembershipInstance.MemberShortName = memberShortNameFeature;
            elementFilterMembershipInstance.OwnedRelatedElement = ownedRelatedElementFeature ?? new List<Guid>();
            elementFilterMembershipInstance.OwnedRelationship = ownedRelationshipFeature ?? new List<Guid>();
            elementFilterMembershipInstance.OwningRelatedElement = owningRelatedElementFeature;
            elementFilterMembershipInstance.OwningRelationship = owningRelationshipFeature;
            elementFilterMembershipInstance.Source = sourceFeature ?? new List<Guid>();
            elementFilterMembershipInstance.Target = targetFeature ?? new List<Guid>();
            elementFilterMembershipInstance.Visibility = visibilityFeature;

            return elementFilterMembershipInstance;
        }

        /// <summary>
        /// Reads a <see cref="IElementFilterMembership"/> from a <see cref="Dictionary{String, Object}"/> that contains a key-value-pair
        /// for each property.
        /// </summary>
        /// <param name="dictionary">
        /// The subject <see cref="Dictionary{String, Object}"/> that is to be converted into a <see cref="IElementFilterMembership"/>
        /// </param>
        /// <returns>
        /// An instance of <see cref="IElementFilterMembership"/>
        /// </returns>
        private static IElementFilterMembership ReadComplex(Dictionary<string, object> dictionary)
        {
            var elementFilterMembershipInstance = DictionaryNullAndTypeCheck(dictionary);

            if (!dictionary.TryGetValue("aliasIds", out object aliasIdsObject))
            {
                throw new ArgumentException("The aliasIds property is missing from the dictionary, the dictionary cannot be converted into a ElementFilterMembership");
            }
            List<string> aliasIdsFeature = aliasIdsObject as List<string>;

            if (!dictionary.TryGetValue("declaredName", out object declaredNameObject))
            {
                throw new ArgumentException("The declaredName property is missing from the dictionary, the dictionary cannot be converted into a ElementFilterMembership");
            }
            string declaredNameFeature = declaredNameObject == null ? null : Convert.ToString(declaredNameObject);

            if (!dictionary.TryGetValue("declaredShortName", out object declaredShortNameObject))
            {
                throw new ArgumentException("The declaredShortName property is missing from the dictionary, the dictionary cannot be converted into a ElementFilterMembership");
            }
            string declaredShortNameFeature = declaredShortNameObject == null ? null : Convert.ToString(declaredShortNameObject);

            if (!dictionary.TryGetValue("elementId", out object elementIdObject))
            {
                throw new ArgumentException("The elementId property is missing from the dictionary, the dictionary cannot be converted into a ElementFilterMembership");
            }
            string elementIdFeature = Convert.ToString(elementIdObject);

            if (!dictionary.TryGetValue("isImplied", out object isImpliedObject))
            {
                throw new ArgumentException("The isImplied property is missing from the dictionary, the dictionary cannot be converted into a ElementFilterMembership");
            }
            bool isImpliedFeature = Convert.ToBoolean(isImpliedObject);

            if (!dictionary.TryGetValue("isImpliedIncluded", out object isImpliedIncludedObject))
            {
                throw new ArgumentException("The isImpliedIncluded property is missing from the dictionary, the dictionary cannot be converted into a ElementFilterMembership");
            }
            bool isImpliedIncludedFeature = Convert.ToBoolean(isImpliedIncludedObject);

            if (!dictionary.TryGetValue("memberElement", out object memberElementObject))
            {
                throw new ArgumentException("The memberElement property is missing from the dictionary, the dictionary cannot be converted into a ElementFilterMembership");
            }
            Guid memberElementFeature = Guid.Parse(Convert.ToString(memberElementObject));

            if (!dictionary.TryGetValue("memberName", out object memberNameObject))
            {
                throw new ArgumentException("The memberName property is missing from the dictionary, the dictionary cannot be converted into a ElementFilterMembership");
            }
            string memberNameFeature = memberNameObject == null ? null : Convert.ToString(memberNameObject);

            if (!dictionary.TryGetValue("memberShortName", out object memberShortNameObject))
            {
                throw new ArgumentException("The memberShortName property is missing from the dictionary, the dictionary cannot be converted into a ElementFilterMembership");
            }
            string memberShortNameFeature = memberShortNameObject == null ? null : Convert.ToString(memberShortNameObject);

            if (!dictionary.TryGetValue("ownedRelatedElement", out object ownedRelatedElementObject))
            {
                throw new ArgumentException("The ownedRelatedElement property is missing from the dictionary, the dictionary cannot be converted into a ElementFilterMembership");
            }
            List<Guid> ownedRelatedElementFeature = (ownedRelatedElementObject as List<Guid>);

            if (!dictionary.TryGetValue("ownedRelationship", out object ownedRelationshipObject))
            {
                throw new ArgumentException("The ownedRelationship property is missing from the dictionary, the dictionary cannot be converted into a ElementFilterMembership");
            }
            List<Guid> ownedRelationshipFeature = (ownedRelationshipObject as List<Guid>);

            if (!dictionary.TryGetValue("owningRelatedElement", out object owningRelatedElementObject))
            {
                throw new ArgumentException("The owningRelatedElement property is missing from the dictionary, the dictionary cannot be converted into a ElementFilterMembership");
            }
            Guid? owningRelatedElementFeature = (Guid?)owningRelatedElementObject;

            if (!dictionary.TryGetValue("owningRelationship", out object owningRelationshipObject))
            {
                throw new ArgumentException("The owningRelationship property is missing from the dictionary, the dictionary cannot be converted into a ElementFilterMembership");
            }
            Guid? owningRelationshipFeature = (Guid?)owningRelationshipObject;

            if (!dictionary.TryGetValue("source", out object sourceObject))
            {
                throw new ArgumentException("The source property is missing from the dictionary, the dictionary cannot be converted into a ElementFilterMembership");
            }
            List<Guid> sourceFeature = (sourceObject as List<Guid>);

            if (!dictionary.TryGetValue("target", out object targetObject))
            {
                throw new ArgumentException("The target property is missing from the dictionary, the dictionary cannot be converted into a ElementFilterMembership");
            }
            List<Guid> targetFeature = (targetObject as List<Guid>);

            if (!dictionary.TryGetValue("visibility", out object visibilityObject))
            {
                throw new ArgumentException("The visibility property is missing from the dictionary, the dictionary cannot be converted into a ElementFilterMembership");
            }
            VisibilityKind visibilityFeature = (VisibilityKind)visibilityObject;


            elementFilterMembershipInstance.AliasIds = aliasIdsFeature ?? new List<string>();
            elementFilterMembershipInstance.DeclaredName = declaredNameFeature;
            elementFilterMembershipInstance.DeclaredShortName = declaredShortNameFeature;
            elementFilterMembershipInstance.ElementId = elementIdFeature;
            elementFilterMembershipInstance.IsImplied = isImpliedFeature;
            elementFilterMembershipInstance.IsImpliedIncluded = isImpliedIncludedFeature;
            elementFilterMembershipInstance.MemberElement = memberElementFeature;
            elementFilterMembershipInstance.MemberName = memberNameFeature;
            elementFilterMembershipInstance.MemberShortName = memberShortNameFeature;
            elementFilterMembershipInstance.OwnedRelatedElement = ownedRelatedElementFeature ?? new List<Guid>();
            elementFilterMembershipInstance.OwnedRelationship = ownedRelationshipFeature ?? new List<Guid>();
            elementFilterMembershipInstance.OwningRelatedElement = owningRelatedElementFeature;
            elementFilterMembershipInstance.OwningRelationship = owningRelationshipFeature;
            elementFilterMembershipInstance.Source = sourceFeature ?? new List<Guid>();
            elementFilterMembershipInstance.Target = targetFeature ?? new List<Guid>();
            elementFilterMembershipInstance.Visibility = visibilityFeature;

            return elementFilterMembershipInstance;
        }

        /// <summary>
        /// Checks whether the <see cref="Dictionary{String, Object}"/> is not null and whether it is
        /// of type <see cref="ElementFilterMembership"/>
        /// </summary>
        /// <param name="dictionary">
        /// The subject <see cref="Dictionary{String, Object}"/> that contains the key-value pairs of
        /// properties and values.
        /// </param>
        /// <returns>
        /// an instance of <see cref="IElementFilterMembership"/>
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="dictionary"/> is null
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown when <paramref name="dictionary"/> is not of type <see cref="ElementFilterMembership"/>
        /// </exception>
        private static IElementFilterMembership DictionaryNullAndTypeCheck(Dictionary<string, object> dictionary)
        {
            if (dictionary == null)
            {
                throw new ArgumentNullException("dictionary", "The dictionary may not be null");
            }

            if (!dictionary.TryGetValue("@type", out object typeObject))
            {
                throw new ArgumentException("The type property is missing from the dictionary, the dictionary cannot be converted into a ElementFilterMembership");
            }

            var type = Convert.ToString(typeObject);

            if (type != "ElementFilterMembership")
            {
                throw new ArgumentException($"The dictionary contains an Object is of type {type} and can therefore not be converted into a ElementFilterMembership");
            }

            if (!dictionary.TryGetValue("@id", out object idObject))
            {
                throw new ArgumentException("The id property is missing from the dictionary, the dictionary cannot be converted into a ElementFilterMembership");
            }
            var id = Guid.Parse(Convert.ToString(idObject));

            var elementFilterMembershipInstance = new Core.DTO.ElementFilterMembership
            {
                Id = id
            };

            return elementFilterMembershipInstance;
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
