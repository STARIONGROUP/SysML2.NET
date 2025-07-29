// -------------------------------------------------------------------------------------------------
// <copyright file="EndFeatureMembershipDictionaryReader.cs" company="Starion Group S.A.">
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
    using System.Linq;

    using SysML2.NET.Common;
    using SysML2.NET.Core;
    using SysML2.NET.Core.DTO;

    /// <summary>
    /// The purpose of the <see cref="EndFeatureMembershipDictionaryReader"/> is to read (convert)
    /// a <see cref="Dictionary{String, Object}"/> from an <see cref="IEndFeatureMembership"/>
    /// </summary>
    public static class EndFeatureMembershipDictionaryReader
    {
        /// <summary>
        /// Reads a <see cref="IEndFeatureMembership"/> from a <see cref="Dictionary{String, Object}"/> that contains a key-value-pair
        /// for each property.
        /// </summary>
        /// <param name="dictionary">
        /// The subject <see cref="Dictionary{String, Object}"/> that is to be converted into a <see cref="IEndFeatureMembership"/>
        /// </param>
        /// <param name="dictionaryKind">
        /// The source <see cref="DictionaryKind"/> that is to be read from
        /// </param>
        /// <returns>
        /// An instance of <see cref="IEndFeatureMembership"/>
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
        /// Reads a <see cref="IEndFeatureMembership"/> from a <see cref="Dictionary{String, Object}"/> that contains a key-value-pair
        /// for each property.
        /// </summary>
        /// <param name="dictionary">
        /// The subject <see cref="Dictionary{String, Object}"/> that is to be converted into a <see cref="IEndFeatureMembership"/>
        /// </param>
        /// <returns>
        /// An instance of <see cref="IEndFeatureMembership"/>
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
        private static IEndFeatureMembership ReadSimplified(Dictionary<string, object> dictionary)
        {
            var endFeatureMembershipInstance = DictionaryNullAndTypeCheck(dictionary);

            if (!dictionary.TryGetValue("aliasIds", out object aliasIdsObject))
            {
                throw new ArgumentException("The aliasIds property is missing from the dictionary, the dictionary cannot be converted into a EndFeatureMembership");
            }
            List<string> aliasIdsFeature = aliasIdsObject as List<string>;

            if (!dictionary.TryGetValue("declaredName", out object declaredNameObject))
            {
                throw new ArgumentException("The declaredName property is missing from the dictionary, the dictionary cannot be converted into a EndFeatureMembership");
            }
            string declaredNameFeature = declaredNameObject == null ? null : Convert.ToString(declaredNameObject);

            if (!dictionary.TryGetValue("declaredShortName", out object declaredShortNameObject))
            {
                throw new ArgumentException("The declaredShortName property is missing from the dictionary, the dictionary cannot be converted into a EndFeatureMembership");
            }
            string declaredShortNameFeature = declaredShortNameObject == null ? null : Convert.ToString(declaredShortNameObject);

            if (!dictionary.TryGetValue("elementId", out object elementIdObject))
            {
                throw new ArgumentException("The elementId property is missing from the dictionary, the dictionary cannot be converted into a EndFeatureMembership");
            }
            string elementIdFeature = Convert.ToString(elementIdObject);

            if (!dictionary.TryGetValue("isImplied", out object isImpliedObject))
            {
                throw new ArgumentException("The isImplied property is missing from the dictionary, the dictionary cannot be converted into a EndFeatureMembership");
            }
            bool isImpliedFeature = Convert.ToBoolean(isImpliedObject);

            if (!dictionary.TryGetValue("isImpliedIncluded", out object isImpliedIncludedObject))
            {
                throw new ArgumentException("The isImpliedIncluded property is missing from the dictionary, the dictionary cannot be converted into a EndFeatureMembership");
            }
            bool isImpliedIncludedFeature = Convert.ToBoolean(isImpliedIncludedObject);

            if (!dictionary.TryGetValue("memberElement", out object memberElementObject))
            {
                throw new ArgumentException("The memberElement property is missing from the dictionary, the dictionary cannot be converted into a EndFeatureMembership");
            }
            Guid memberElementFeature = Guid.Parse(Convert.ToString(memberElementObject));

            if (!dictionary.TryGetValue("memberName", out object memberNameObject))
            {
                throw new ArgumentException("The memberName property is missing from the dictionary, the dictionary cannot be converted into a EndFeatureMembership");
            }
            string memberNameFeature = memberNameObject == null ? null : Convert.ToString(memberNameObject);

            if (!dictionary.TryGetValue("memberShortName", out object memberShortNameObject))
            {
                throw new ArgumentException("The memberShortName property is missing from the dictionary, the dictionary cannot be converted into a EndFeatureMembership");
            }
            string memberShortNameFeature = memberShortNameObject == null ? null : Convert.ToString(memberShortNameObject);

            if (!dictionary.TryGetValue("ownedRelatedElement", out object ownedRelatedElementObject))
            {
                throw new ArgumentException("The ownedRelatedElement property is missing from the dictionary, the dictionary cannot be converted into a EndFeatureMembership");
            }
            List<Guid> ownedRelatedElementFeature = (ownedRelatedElementObject as List<string>)?.Select(Guid.Parse).ToList();

            if (!dictionary.TryGetValue("ownedRelationship", out object ownedRelationshipObject))
            {
                throw new ArgumentException("The ownedRelationship property is missing from the dictionary, the dictionary cannot be converted into a EndFeatureMembership");
            }
            List<Guid> ownedRelationshipFeature = (ownedRelationshipObject as List<string>)?.Select(Guid.Parse).ToList();

            if (!dictionary.TryGetValue("owningRelatedElement", out object owningRelatedElementObject))
            {
                throw new ArgumentException("The owningRelatedElement property is missing from the dictionary, the dictionary cannot be converted into a EndFeatureMembership");
            }
            Guid? owningRelatedElementFeature = owningRelatedElementObject == null ? (Guid?)null : Guid.Parse(Convert.ToString(owningRelatedElementObject));

            if (!dictionary.TryGetValue("owningRelationship", out object owningRelationshipObject))
            {
                throw new ArgumentException("The owningRelationship property is missing from the dictionary, the dictionary cannot be converted into a EndFeatureMembership");
            }
            Guid? owningRelationshipFeature = owningRelationshipObject == null ? (Guid?)null : Guid.Parse(Convert.ToString(owningRelationshipObject));

            if (!dictionary.TryGetValue("source", out object sourceObject))
            {
                throw new ArgumentException("The source property is missing from the dictionary, the dictionary cannot be converted into a EndFeatureMembership");
            }
            List<Guid> sourceFeature = (sourceObject as List<string>)?.Select(Guid.Parse).ToList();

            if (!dictionary.TryGetValue("target", out object targetObject))
            {
                throw new ArgumentException("The target property is missing from the dictionary, the dictionary cannot be converted into a EndFeatureMembership");
            }
            List<Guid> targetFeature = (targetObject as List<string>)?.Select(Guid.Parse).ToList();

            if (!dictionary.TryGetValue("visibility", out object visibilityObject))
            {
                throw new ArgumentException("The visibility property is missing from the dictionary, the dictionary cannot be converted into a EndFeatureMembership");
            }
            VisibilityKind visibilityFeature = (VisibilityKind)Enum.Parse(typeof(VisibilityKind), Convert.ToString(visibilityObject), true);


            endFeatureMembershipInstance.AliasIds = aliasIdsFeature ?? new List<string>();
            endFeatureMembershipInstance.DeclaredName = declaredNameFeature;
            endFeatureMembershipInstance.DeclaredShortName = declaredShortNameFeature;
            endFeatureMembershipInstance.ElementId = elementIdFeature;
            endFeatureMembershipInstance.IsImplied = isImpliedFeature;
            endFeatureMembershipInstance.IsImpliedIncluded = isImpliedIncludedFeature;
            endFeatureMembershipInstance.MemberElement = memberElementFeature;
            endFeatureMembershipInstance.MemberName = memberNameFeature;
            endFeatureMembershipInstance.MemberShortName = memberShortNameFeature;
            endFeatureMembershipInstance.OwnedRelatedElement = ownedRelatedElementFeature ?? new List<Guid>();
            endFeatureMembershipInstance.OwnedRelationship = ownedRelationshipFeature ?? new List<Guid>();
            endFeatureMembershipInstance.OwningRelatedElement = owningRelatedElementFeature;
            endFeatureMembershipInstance.OwningRelationship = owningRelationshipFeature;
            endFeatureMembershipInstance.Source = sourceFeature ?? new List<Guid>();
            endFeatureMembershipInstance.Target = targetFeature ?? new List<Guid>();
            endFeatureMembershipInstance.Visibility = visibilityFeature;

            return endFeatureMembershipInstance;
        }

        /// <summary>
        /// Reads a <see cref="IEndFeatureMembership"/> from a <see cref="Dictionary{String, Object}"/> that contains a key-value-pair
        /// for each property.
        /// </summary>
        /// <param name="dictionary">
        /// The subject <see cref="Dictionary{String, Object}"/> that is to be converted into a <see cref="IEndFeatureMembership"/>
        /// </param>
        /// <returns>
        /// An instance of <see cref="IEndFeatureMembership"/>
        /// </returns>
        private static IEndFeatureMembership ReadComplex(Dictionary<string, object> dictionary)
        {
            var endFeatureMembershipInstance = DictionaryNullAndTypeCheck(dictionary);

            if (!dictionary.TryGetValue("aliasIds", out object aliasIdsObject))
            {
                throw new ArgumentException("The aliasIds property is missing from the dictionary, the dictionary cannot be converted into a EndFeatureMembership");
            }
            List<string> aliasIdsFeature = aliasIdsObject as List<string>;

            if (!dictionary.TryGetValue("declaredName", out object declaredNameObject))
            {
                throw new ArgumentException("The declaredName property is missing from the dictionary, the dictionary cannot be converted into a EndFeatureMembership");
            }
            string declaredNameFeature = declaredNameObject == null ? null : Convert.ToString(declaredNameObject);

            if (!dictionary.TryGetValue("declaredShortName", out object declaredShortNameObject))
            {
                throw new ArgumentException("The declaredShortName property is missing from the dictionary, the dictionary cannot be converted into a EndFeatureMembership");
            }
            string declaredShortNameFeature = declaredShortNameObject == null ? null : Convert.ToString(declaredShortNameObject);

            if (!dictionary.TryGetValue("elementId", out object elementIdObject))
            {
                throw new ArgumentException("The elementId property is missing from the dictionary, the dictionary cannot be converted into a EndFeatureMembership");
            }
            string elementIdFeature = Convert.ToString(elementIdObject);

            if (!dictionary.TryGetValue("isImplied", out object isImpliedObject))
            {
                throw new ArgumentException("The isImplied property is missing from the dictionary, the dictionary cannot be converted into a EndFeatureMembership");
            }
            bool isImpliedFeature = Convert.ToBoolean(isImpliedObject);

            if (!dictionary.TryGetValue("isImpliedIncluded", out object isImpliedIncludedObject))
            {
                throw new ArgumentException("The isImpliedIncluded property is missing from the dictionary, the dictionary cannot be converted into a EndFeatureMembership");
            }
            bool isImpliedIncludedFeature = Convert.ToBoolean(isImpliedIncludedObject);

            if (!dictionary.TryGetValue("memberElement", out object memberElementObject))
            {
                throw new ArgumentException("The memberElement property is missing from the dictionary, the dictionary cannot be converted into a EndFeatureMembership");
            }
            Guid memberElementFeature = Guid.Parse(Convert.ToString(memberElementObject));

            if (!dictionary.TryGetValue("memberName", out object memberNameObject))
            {
                throw new ArgumentException("The memberName property is missing from the dictionary, the dictionary cannot be converted into a EndFeatureMembership");
            }
            string memberNameFeature = memberNameObject == null ? null : Convert.ToString(memberNameObject);

            if (!dictionary.TryGetValue("memberShortName", out object memberShortNameObject))
            {
                throw new ArgumentException("The memberShortName property is missing from the dictionary, the dictionary cannot be converted into a EndFeatureMembership");
            }
            string memberShortNameFeature = memberShortNameObject == null ? null : Convert.ToString(memberShortNameObject);

            if (!dictionary.TryGetValue("ownedRelatedElement", out object ownedRelatedElementObject))
            {
                throw new ArgumentException("The ownedRelatedElement property is missing from the dictionary, the dictionary cannot be converted into a EndFeatureMembership");
            }
            List<Guid> ownedRelatedElementFeature = (ownedRelatedElementObject as List<Guid>);

            if (!dictionary.TryGetValue("ownedRelationship", out object ownedRelationshipObject))
            {
                throw new ArgumentException("The ownedRelationship property is missing from the dictionary, the dictionary cannot be converted into a EndFeatureMembership");
            }
            List<Guid> ownedRelationshipFeature = (ownedRelationshipObject as List<Guid>);

            if (!dictionary.TryGetValue("owningRelatedElement", out object owningRelatedElementObject))
            {
                throw new ArgumentException("The owningRelatedElement property is missing from the dictionary, the dictionary cannot be converted into a EndFeatureMembership");
            }
            Guid? owningRelatedElementFeature = (Guid?)owningRelatedElementObject;

            if (!dictionary.TryGetValue("owningRelationship", out object owningRelationshipObject))
            {
                throw new ArgumentException("The owningRelationship property is missing from the dictionary, the dictionary cannot be converted into a EndFeatureMembership");
            }
            Guid? owningRelationshipFeature = (Guid?)owningRelationshipObject;

            if (!dictionary.TryGetValue("source", out object sourceObject))
            {
                throw new ArgumentException("The source property is missing from the dictionary, the dictionary cannot be converted into a EndFeatureMembership");
            }
            List<Guid> sourceFeature = (sourceObject as List<Guid>);

            if (!dictionary.TryGetValue("target", out object targetObject))
            {
                throw new ArgumentException("The target property is missing from the dictionary, the dictionary cannot be converted into a EndFeatureMembership");
            }
            List<Guid> targetFeature = (targetObject as List<Guid>);

            if (!dictionary.TryGetValue("visibility", out object visibilityObject))
            {
                throw new ArgumentException("The visibility property is missing from the dictionary, the dictionary cannot be converted into a EndFeatureMembership");
            }
            VisibilityKind visibilityFeature = (VisibilityKind)visibilityObject;


            endFeatureMembershipInstance.AliasIds = aliasIdsFeature ?? new List<string>();
            endFeatureMembershipInstance.DeclaredName = declaredNameFeature;
            endFeatureMembershipInstance.DeclaredShortName = declaredShortNameFeature;
            endFeatureMembershipInstance.ElementId = elementIdFeature;
            endFeatureMembershipInstance.IsImplied = isImpliedFeature;
            endFeatureMembershipInstance.IsImpliedIncluded = isImpliedIncludedFeature;
            endFeatureMembershipInstance.MemberElement = memberElementFeature;
            endFeatureMembershipInstance.MemberName = memberNameFeature;
            endFeatureMembershipInstance.MemberShortName = memberShortNameFeature;
            endFeatureMembershipInstance.OwnedRelatedElement = ownedRelatedElementFeature ?? new List<Guid>();
            endFeatureMembershipInstance.OwnedRelationship = ownedRelationshipFeature ?? new List<Guid>();
            endFeatureMembershipInstance.OwningRelatedElement = owningRelatedElementFeature;
            endFeatureMembershipInstance.OwningRelationship = owningRelationshipFeature;
            endFeatureMembershipInstance.Source = sourceFeature ?? new List<Guid>();
            endFeatureMembershipInstance.Target = targetFeature ?? new List<Guid>();
            endFeatureMembershipInstance.Visibility = visibilityFeature;

            return endFeatureMembershipInstance;
        }

        /// <summary>
        /// Checks whether the <see cref="Dictionary{String, Object}"/> is not null and whether it is
        /// of type <see cref="EndFeatureMembership"/>
        /// </summary>
        /// <param name="dictionary">
        /// The subject <see cref="Dictionary{String, Object}"/> that contains the key-value pairs of
        /// properties and values.
        /// </param>
        /// <returns>
        /// an instance of <see cref="IEndFeatureMembership"/>
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="dictionary"/> is null
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown when <paramref name="dictionary"/> is not of type <see cref="EndFeatureMembership"/>
        /// </exception>
        private static IEndFeatureMembership DictionaryNullAndTypeCheck(Dictionary<string, object> dictionary)
        {
            if (dictionary == null)
            {
                throw new ArgumentNullException("dictionary", "The dictionary may not be null");
            }

            if (!dictionary.TryGetValue("@type", out object typeObject))
            {
                throw new ArgumentException("The type property is missing from the dictionary, the dictionary cannot be converted into a EndFeatureMembership");
            }

            var type = Convert.ToString(typeObject);

            if (type != "EndFeatureMembership")
            {
                throw new ArgumentException($"The dictionary contains an Object is of type {type} and can therefore not be converted into a EndFeatureMembership");
            }

            if (!dictionary.TryGetValue("@id", out object idObject))
            {
                throw new ArgumentException("The id property is missing from the dictionary, the dictionary cannot be converted into a EndFeatureMembership");
            }
            var id = Guid.Parse(Convert.ToString(idObject));

            var endFeatureMembershipInstance = new SysML2.NET.Core.DTO.EndFeatureMembership
            {
                Id = id
            };

            return endFeatureMembershipInstance;
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
