// -------------------------------------------------------------------------------------------------
// <copyright file="SatisfyRequirementUsageDictionaryReader.cs" company="RHEA System S.A.">
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
    using System.Linq;

    using SysML2.NET.Common;
    using SysML2.NET.Core;
    using SysML2.NET.Core.DTO;

    /// <summary>
    /// The purpose of the <see cref="SatisfyRequirementUsageDictionaryReader"/> is to read (convert)
    /// a <see cref="Dictionary{String, Object}"/> from an <see cref="ISatisfyRequirementUsage"/>
    /// </summary>
    public static class SatisfyRequirementUsageDictionaryReader
    {
        /// <summary>
        /// Reads a <see cref="ISatisfyRequirementUsage"/> from a <see cref="Dictionary{String, Object}"/> that contains a key-value-pair
        /// for each property.
        /// </summary>
        /// <param name="dictionary">
        /// The subject <see cref="Dictionary{String, Object}"/> that is to be converted into a <see cref="ISatisfyRequirementUsage"/>
        /// </param>
        /// <param name="dictionaryKind">
        /// The source <see cref="DictionaryKind"/> that is to be read from
        /// </param>
        /// <returns>
        /// An instance of <see cref="ISatisfyRequirementUsage"/>
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
        /// Reads a <see cref="ISatisfyRequirementUsage"/> from a <see cref="Dictionary{String, Object}"/> that contains a key-value-pair
        /// for each property.
        /// </summary>
        /// <param name="dictionary">
        /// The subject <see cref="Dictionary{String, Object}"/> that is to be converted into a <see cref="ISatisfyRequirementUsage"/>
        /// </param>
        /// <returns>
        /// An instance of <see cref="ISatisfyRequirementUsage"/>
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
        private static ISatisfyRequirementUsage ReadSimplified(Dictionary<string, object> dictionary)
        {
            var satisfyRequirementUsageInstance = DictionaryNullAndTypeCheck(dictionary);

            if (!dictionary.TryGetValue("aliasIds", out object aliasIdsObject))
            {
                throw new ArgumentException("The aliasIds property is missing from the dictionary, the dictionary cannot be converted into a SatisfyRequirementUsage");
            }
            List<string> aliasIdsFeature = aliasIdsObject as List<string>;

            if (!dictionary.TryGetValue("declaredName", out object declaredNameObject))
            {
                throw new ArgumentException("The declaredName property is missing from the dictionary, the dictionary cannot be converted into a SatisfyRequirementUsage");
            }
            string declaredNameFeature = declaredNameObject == null ? null : Convert.ToString(declaredNameObject);

            if (!dictionary.TryGetValue("declaredShortName", out object declaredShortNameObject))
            {
                throw new ArgumentException("The declaredShortName property is missing from the dictionary, the dictionary cannot be converted into a SatisfyRequirementUsage");
            }
            string declaredShortNameFeature = declaredShortNameObject == null ? null : Convert.ToString(declaredShortNameObject);

            if (!dictionary.TryGetValue("direction", out object directionObject))
            {
                throw new ArgumentException("The direction property is missing from the dictionary, the dictionary cannot be converted into a SatisfyRequirementUsage");
            }
            FeatureDirectionKind? directionFeature = directionObject == null ? null : (FeatureDirectionKind?)Enum.Parse(typeof(FeatureDirectionKind), Convert.ToString(directionObject), true);

            if (!dictionary.TryGetValue("elementId", out object elementIdObject))
            {
                throw new ArgumentException("The elementId property is missing from the dictionary, the dictionary cannot be converted into a SatisfyRequirementUsage");
            }
            string elementIdFeature = Convert.ToString(elementIdObject);

            if (!dictionary.TryGetValue("isAbstract", out object isAbstractObject))
            {
                throw new ArgumentException("The isAbstract property is missing from the dictionary, the dictionary cannot be converted into a SatisfyRequirementUsage");
            }
            bool isAbstractFeature = Convert.ToBoolean(isAbstractObject);

            if (!dictionary.TryGetValue("isComposite", out object isCompositeObject))
            {
                throw new ArgumentException("The isComposite property is missing from the dictionary, the dictionary cannot be converted into a SatisfyRequirementUsage");
            }
            bool isCompositeFeature = Convert.ToBoolean(isCompositeObject);

            if (!dictionary.TryGetValue("isDerived", out object isDerivedObject))
            {
                throw new ArgumentException("The isDerived property is missing from the dictionary, the dictionary cannot be converted into a SatisfyRequirementUsage");
            }
            bool isDerivedFeature = Convert.ToBoolean(isDerivedObject);

            if (!dictionary.TryGetValue("isEnd", out object isEndObject))
            {
                throw new ArgumentException("The isEnd property is missing from the dictionary, the dictionary cannot be converted into a SatisfyRequirementUsage");
            }
            bool isEndFeature = Convert.ToBoolean(isEndObject);

            if (!dictionary.TryGetValue("isImpliedIncluded", out object isImpliedIncludedObject))
            {
                throw new ArgumentException("The isImpliedIncluded property is missing from the dictionary, the dictionary cannot be converted into a SatisfyRequirementUsage");
            }
            bool isImpliedIncludedFeature = Convert.ToBoolean(isImpliedIncludedObject);

            if (!dictionary.TryGetValue("isIndividual", out object isIndividualObject))
            {
                throw new ArgumentException("The isIndividual property is missing from the dictionary, the dictionary cannot be converted into a SatisfyRequirementUsage");
            }
            bool isIndividualFeature = Convert.ToBoolean(isIndividualObject);

            if (!dictionary.TryGetValue("isNegated", out object isNegatedObject))
            {
                throw new ArgumentException("The isNegated property is missing from the dictionary, the dictionary cannot be converted into a SatisfyRequirementUsage");
            }
            bool isNegatedFeature = Convert.ToBoolean(isNegatedObject);

            if (!dictionary.TryGetValue("isOrdered", out object isOrderedObject))
            {
                throw new ArgumentException("The isOrdered property is missing from the dictionary, the dictionary cannot be converted into a SatisfyRequirementUsage");
            }
            bool isOrderedFeature = Convert.ToBoolean(isOrderedObject);

            if (!dictionary.TryGetValue("isPortion", out object isPortionObject))
            {
                throw new ArgumentException("The isPortion property is missing from the dictionary, the dictionary cannot be converted into a SatisfyRequirementUsage");
            }
            bool isPortionFeature = Convert.ToBoolean(isPortionObject);

            if (!dictionary.TryGetValue("isReadOnly", out object isReadOnlyObject))
            {
                throw new ArgumentException("The isReadOnly property is missing from the dictionary, the dictionary cannot be converted into a SatisfyRequirementUsage");
            }
            bool isReadOnlyFeature = Convert.ToBoolean(isReadOnlyObject);

            if (!dictionary.TryGetValue("isSufficient", out object isSufficientObject))
            {
                throw new ArgumentException("The isSufficient property is missing from the dictionary, the dictionary cannot be converted into a SatisfyRequirementUsage");
            }
            bool isSufficientFeature = Convert.ToBoolean(isSufficientObject);

            if (!dictionary.TryGetValue("isUnique", out object isUniqueObject))
            {
                throw new ArgumentException("The isUnique property is missing from the dictionary, the dictionary cannot be converted into a SatisfyRequirementUsage");
            }
            bool isUniqueFeature = Convert.ToBoolean(isUniqueObject);

            if (!dictionary.TryGetValue("isVariation", out object isVariationObject))
            {
                throw new ArgumentException("The isVariation property is missing from the dictionary, the dictionary cannot be converted into a SatisfyRequirementUsage");
            }
            bool isVariationFeature = Convert.ToBoolean(isVariationObject);

            if (!dictionary.TryGetValue("ownedRelationship", out object ownedRelationshipObject))
            {
                throw new ArgumentException("The ownedRelationship property is missing from the dictionary, the dictionary cannot be converted into a SatisfyRequirementUsage");
            }
            List<Guid> ownedRelationshipFeature = (ownedRelationshipObject as List<string>)?.Select(Guid.Parse).ToList();

            if (!dictionary.TryGetValue("owningRelationship", out object owningRelationshipObject))
            {
                throw new ArgumentException("The owningRelationship property is missing from the dictionary, the dictionary cannot be converted into a SatisfyRequirementUsage");
            }
            Guid? owningRelationshipFeature = owningRelationshipObject == null ? (Guid?)null : Guid.Parse(Convert.ToString(owningRelationshipObject));

            if (!dictionary.TryGetValue("portionKind", out object portionKindObject))
            {
                throw new ArgumentException("The portionKind property is missing from the dictionary, the dictionary cannot be converted into a SatisfyRequirementUsage");
            }
            PortionKind? portionKindFeature = portionKindObject == null ? null : (PortionKind?)Enum.Parse(typeof(PortionKind), Convert.ToString(portionKindObject), true);

            if (!dictionary.TryGetValue("reqId", out object reqIdObject))
            {
                throw new ArgumentException("The reqId property is missing from the dictionary, the dictionary cannot be converted into a SatisfyRequirementUsage");
            }
            string reqIdFeature = reqIdObject == null ? null : Convert.ToString(reqIdObject);


            satisfyRequirementUsageInstance.AliasIds = aliasIdsFeature ?? new List<string>();
            satisfyRequirementUsageInstance.DeclaredName = declaredNameFeature;
            satisfyRequirementUsageInstance.DeclaredShortName = declaredShortNameFeature;
            satisfyRequirementUsageInstance.Direction = directionFeature;
            satisfyRequirementUsageInstance.ElementId = elementIdFeature;
            satisfyRequirementUsageInstance.IsAbstract = isAbstractFeature;
            satisfyRequirementUsageInstance.IsComposite = isCompositeFeature;
            satisfyRequirementUsageInstance.IsDerived = isDerivedFeature;
            satisfyRequirementUsageInstance.IsEnd = isEndFeature;
            satisfyRequirementUsageInstance.IsImpliedIncluded = isImpliedIncludedFeature;
            satisfyRequirementUsageInstance.IsIndividual = isIndividualFeature;
            satisfyRequirementUsageInstance.IsNegated = isNegatedFeature;
            satisfyRequirementUsageInstance.IsOrdered = isOrderedFeature;
            satisfyRequirementUsageInstance.IsPortion = isPortionFeature;
            satisfyRequirementUsageInstance.IsReadOnly = isReadOnlyFeature;
            satisfyRequirementUsageInstance.IsSufficient = isSufficientFeature;
            satisfyRequirementUsageInstance.IsUnique = isUniqueFeature;
            satisfyRequirementUsageInstance.IsVariation = isVariationFeature;
            satisfyRequirementUsageInstance.OwnedRelationship = ownedRelationshipFeature ?? new List<Guid>();
            satisfyRequirementUsageInstance.OwningRelationship = owningRelationshipFeature;
            satisfyRequirementUsageInstance.PortionKind = portionKindFeature;
            satisfyRequirementUsageInstance.ReqId = reqIdFeature;

            return satisfyRequirementUsageInstance;
        }

        /// <summary>
        /// Reads a <see cref="ISatisfyRequirementUsage"/> from a <see cref="Dictionary{String, Object}"/> that contains a key-value-pair
        /// for each property.
        /// </summary>
        /// <param name="dictionary">
        /// The subject <see cref="Dictionary{String, Object}"/> that is to be converted into a <see cref="ISatisfyRequirementUsage"/>
        /// </param>
        /// <returns>
        /// An instance of <see cref="ISatisfyRequirementUsage"/>
        /// </returns>
        private static ISatisfyRequirementUsage ReadComplex(Dictionary<string, object> dictionary)
        {
            var satisfyRequirementUsageInstance = DictionaryNullAndTypeCheck(dictionary);

            if (!dictionary.TryGetValue("aliasIds", out object aliasIdsObject))
            {
                throw new ArgumentException("The aliasIds property is missing from the dictionary, the dictionary cannot be converted into a SatisfyRequirementUsage");
            }
            List<string> aliasIdsFeature = aliasIdsObject as List<string>;

            if (!dictionary.TryGetValue("declaredName", out object declaredNameObject))
            {
                throw new ArgumentException("The declaredName property is missing from the dictionary, the dictionary cannot be converted into a SatisfyRequirementUsage");
            }
            string declaredNameFeature = declaredNameObject == null ? null : Convert.ToString(declaredNameObject);

            if (!dictionary.TryGetValue("declaredShortName", out object declaredShortNameObject))
            {
                throw new ArgumentException("The declaredShortName property is missing from the dictionary, the dictionary cannot be converted into a SatisfyRequirementUsage");
            }
            string declaredShortNameFeature = declaredShortNameObject == null ? null : Convert.ToString(declaredShortNameObject);

            if (!dictionary.TryGetValue("direction", out object directionObject))
            {
                throw new ArgumentException("The direction property is missing from the dictionary, the dictionary cannot be converted into a SatisfyRequirementUsage");
            }
            FeatureDirectionKind? directionFeature = (FeatureDirectionKind?)directionObject;

            if (!dictionary.TryGetValue("elementId", out object elementIdObject))
            {
                throw new ArgumentException("The elementId property is missing from the dictionary, the dictionary cannot be converted into a SatisfyRequirementUsage");
            }
            string elementIdFeature = Convert.ToString(elementIdObject);

            if (!dictionary.TryGetValue("isAbstract", out object isAbstractObject))
            {
                throw new ArgumentException("The isAbstract property is missing from the dictionary, the dictionary cannot be converted into a SatisfyRequirementUsage");
            }
            bool isAbstractFeature = Convert.ToBoolean(isAbstractObject);

            if (!dictionary.TryGetValue("isComposite", out object isCompositeObject))
            {
                throw new ArgumentException("The isComposite property is missing from the dictionary, the dictionary cannot be converted into a SatisfyRequirementUsage");
            }
            bool isCompositeFeature = Convert.ToBoolean(isCompositeObject);

            if (!dictionary.TryGetValue("isDerived", out object isDerivedObject))
            {
                throw new ArgumentException("The isDerived property is missing from the dictionary, the dictionary cannot be converted into a SatisfyRequirementUsage");
            }
            bool isDerivedFeature = Convert.ToBoolean(isDerivedObject);

            if (!dictionary.TryGetValue("isEnd", out object isEndObject))
            {
                throw new ArgumentException("The isEnd property is missing from the dictionary, the dictionary cannot be converted into a SatisfyRequirementUsage");
            }
            bool isEndFeature = Convert.ToBoolean(isEndObject);

            if (!dictionary.TryGetValue("isImpliedIncluded", out object isImpliedIncludedObject))
            {
                throw new ArgumentException("The isImpliedIncluded property is missing from the dictionary, the dictionary cannot be converted into a SatisfyRequirementUsage");
            }
            bool isImpliedIncludedFeature = Convert.ToBoolean(isImpliedIncludedObject);

            if (!dictionary.TryGetValue("isIndividual", out object isIndividualObject))
            {
                throw new ArgumentException("The isIndividual property is missing from the dictionary, the dictionary cannot be converted into a SatisfyRequirementUsage");
            }
            bool isIndividualFeature = Convert.ToBoolean(isIndividualObject);

            if (!dictionary.TryGetValue("isNegated", out object isNegatedObject))
            {
                throw new ArgumentException("The isNegated property is missing from the dictionary, the dictionary cannot be converted into a SatisfyRequirementUsage");
            }
            bool isNegatedFeature = Convert.ToBoolean(isNegatedObject);

            if (!dictionary.TryGetValue("isOrdered", out object isOrderedObject))
            {
                throw new ArgumentException("The isOrdered property is missing from the dictionary, the dictionary cannot be converted into a SatisfyRequirementUsage");
            }
            bool isOrderedFeature = Convert.ToBoolean(isOrderedObject);

            if (!dictionary.TryGetValue("isPortion", out object isPortionObject))
            {
                throw new ArgumentException("The isPortion property is missing from the dictionary, the dictionary cannot be converted into a SatisfyRequirementUsage");
            }
            bool isPortionFeature = Convert.ToBoolean(isPortionObject);

            if (!dictionary.TryGetValue("isReadOnly", out object isReadOnlyObject))
            {
                throw new ArgumentException("The isReadOnly property is missing from the dictionary, the dictionary cannot be converted into a SatisfyRequirementUsage");
            }
            bool isReadOnlyFeature = Convert.ToBoolean(isReadOnlyObject);

            if (!dictionary.TryGetValue("isSufficient", out object isSufficientObject))
            {
                throw new ArgumentException("The isSufficient property is missing from the dictionary, the dictionary cannot be converted into a SatisfyRequirementUsage");
            }
            bool isSufficientFeature = Convert.ToBoolean(isSufficientObject);

            if (!dictionary.TryGetValue("isUnique", out object isUniqueObject))
            {
                throw new ArgumentException("The isUnique property is missing from the dictionary, the dictionary cannot be converted into a SatisfyRequirementUsage");
            }
            bool isUniqueFeature = Convert.ToBoolean(isUniqueObject);

            if (!dictionary.TryGetValue("isVariation", out object isVariationObject))
            {
                throw new ArgumentException("The isVariation property is missing from the dictionary, the dictionary cannot be converted into a SatisfyRequirementUsage");
            }
            bool isVariationFeature = Convert.ToBoolean(isVariationObject);

            if (!dictionary.TryGetValue("ownedRelationship", out object ownedRelationshipObject))
            {
                throw new ArgumentException("The ownedRelationship property is missing from the dictionary, the dictionary cannot be converted into a SatisfyRequirementUsage");
            }
            List<Guid> ownedRelationshipFeature = (ownedRelationshipObject as List<Guid>);

            if (!dictionary.TryGetValue("owningRelationship", out object owningRelationshipObject))
            {
                throw new ArgumentException("The owningRelationship property is missing from the dictionary, the dictionary cannot be converted into a SatisfyRequirementUsage");
            }
            Guid? owningRelationshipFeature = (Guid?)owningRelationshipObject;

            if (!dictionary.TryGetValue("portionKind", out object portionKindObject))
            {
                throw new ArgumentException("The portionKind property is missing from the dictionary, the dictionary cannot be converted into a SatisfyRequirementUsage");
            }
            PortionKind? portionKindFeature = (PortionKind?)portionKindObject;

            if (!dictionary.TryGetValue("reqId", out object reqIdObject))
            {
                throw new ArgumentException("The reqId property is missing from the dictionary, the dictionary cannot be converted into a SatisfyRequirementUsage");
            }
            string reqIdFeature = reqIdObject == null ? null : Convert.ToString(reqIdObject);


            satisfyRequirementUsageInstance.AliasIds = aliasIdsFeature ?? new List<string>();
            satisfyRequirementUsageInstance.DeclaredName = declaredNameFeature;
            satisfyRequirementUsageInstance.DeclaredShortName = declaredShortNameFeature;
            satisfyRequirementUsageInstance.Direction = directionFeature;
            satisfyRequirementUsageInstance.ElementId = elementIdFeature;
            satisfyRequirementUsageInstance.IsAbstract = isAbstractFeature;
            satisfyRequirementUsageInstance.IsComposite = isCompositeFeature;
            satisfyRequirementUsageInstance.IsDerived = isDerivedFeature;
            satisfyRequirementUsageInstance.IsEnd = isEndFeature;
            satisfyRequirementUsageInstance.IsImpliedIncluded = isImpliedIncludedFeature;
            satisfyRequirementUsageInstance.IsIndividual = isIndividualFeature;
            satisfyRequirementUsageInstance.IsNegated = isNegatedFeature;
            satisfyRequirementUsageInstance.IsOrdered = isOrderedFeature;
            satisfyRequirementUsageInstance.IsPortion = isPortionFeature;
            satisfyRequirementUsageInstance.IsReadOnly = isReadOnlyFeature;
            satisfyRequirementUsageInstance.IsSufficient = isSufficientFeature;
            satisfyRequirementUsageInstance.IsUnique = isUniqueFeature;
            satisfyRequirementUsageInstance.IsVariation = isVariationFeature;
            satisfyRequirementUsageInstance.OwnedRelationship = ownedRelationshipFeature ?? new List<Guid>();
            satisfyRequirementUsageInstance.OwningRelationship = owningRelationshipFeature;
            satisfyRequirementUsageInstance.PortionKind = portionKindFeature;
            satisfyRequirementUsageInstance.ReqId = reqIdFeature;

            return satisfyRequirementUsageInstance;
        }

        /// <summary>
        /// Checks whether the <see cref="Dictionary{String, Object}"/> is not null and whether it is
        /// of type <see cref="SatisfyRequirementUsage"/>
        /// </summary>
        /// <param name="dictionary">
        /// The subject <see cref="Dictionary{String, Object}"/> that contains the key-value pairs of
        /// properties and values.
        /// </param>
        /// <returns>
        /// an instance of <see cref="ISatisfyRequirementUsage"/>
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="dictionary"/> is null
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown when <paramref name="dictionary"/> is not of type <see cref="SatisfyRequirementUsage"/>
        /// </exception>
        private static ISatisfyRequirementUsage DictionaryNullAndTypeCheck(Dictionary<string, object> dictionary)
        {
            if (dictionary == null)
            {
                throw new ArgumentNullException("dictionary", "The dictionary may not be null");
            }

            if (!dictionary.TryGetValue("@type", out object typeObject))
            {
                throw new ArgumentException("The type property is missing from the dictionary, the dictionary cannot be converted into a SatisfyRequirementUsage");
            }

            var type = Convert.ToString(typeObject);

            if (type != "SatisfyRequirementUsage")
            {
                throw new ArgumentException($"The dictionary contains an Object is of type {type} and can therefore not be converted into a SatisfyRequirementUsage");
            }

            if (!dictionary.TryGetValue("@id", out object idObject))
            {
                throw new ArgumentException("The id property is missing from the dictionary, the dictionary cannot be converted into a SatisfyRequirementUsage");
            }
            var id = Guid.Parse(Convert.ToString(idObject));

            var satisfyRequirementUsageInstance = new SysML2.NET.Core.DTO.SatisfyRequirementUsage
            {
                Id = id
            };

            return satisfyRequirementUsageInstance;
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
