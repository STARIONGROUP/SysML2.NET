// -------------------------------------------------------------------------------------------------
// <copyright file="UseCaseUsageDictionaryReader.cs" company="Starion Group S.A.">
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
    /// The purpose of the <see cref="UseCaseUsageDictionaryReader"/> is to read (convert)
    /// a <see cref="Dictionary{String, Object}"/> from an <see cref="IUseCaseUsage"/>
    /// </summary>
    public static class UseCaseUsageDictionaryReader
    {
        /// <summary>
        /// Reads a <see cref="IUseCaseUsage"/> from a <see cref="Dictionary{String, Object}"/> that contains a key-value-pair
        /// for each property.
        /// </summary>
        /// <param name="dictionary">
        /// The subject <see cref="Dictionary{String, Object}"/> that is to be converted into a <see cref="IUseCaseUsage"/>
        /// </param>
        /// <param name="dictionaryKind">
        /// The source <see cref="DictionaryKind"/> that is to be read from
        /// </param>
        /// <returns>
        /// An instance of <see cref="IUseCaseUsage"/>
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
        /// Reads a <see cref="IUseCaseUsage"/> from a <see cref="Dictionary{String, Object}"/> that contains a key-value-pair
        /// for each property.
        /// </summary>
        /// <param name="dictionary">
        /// The subject <see cref="Dictionary{String, Object}"/> that is to be converted into a <see cref="IUseCaseUsage"/>
        /// </param>
        /// <returns>
        /// An instance of <see cref="IUseCaseUsage"/>
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
        private static IUseCaseUsage ReadSimplified(Dictionary<string, object> dictionary)
        {
            var useCaseUsageInstance = DictionaryNullAndTypeCheck(dictionary);

            if (!dictionary.TryGetValue("aliasIds", out object aliasIdsObject))
            {
                throw new ArgumentException("The aliasIds property is missing from the dictionary, the dictionary cannot be converted into a UseCaseUsage");
            }
            List<string> aliasIdsFeature = aliasIdsObject as List<string>;

            if (!dictionary.TryGetValue("declaredName", out object declaredNameObject))
            {
                throw new ArgumentException("The declaredName property is missing from the dictionary, the dictionary cannot be converted into a UseCaseUsage");
            }
            string declaredNameFeature = declaredNameObject == null ? null : Convert.ToString(declaredNameObject);

            if (!dictionary.TryGetValue("declaredShortName", out object declaredShortNameObject))
            {
                throw new ArgumentException("The declaredShortName property is missing from the dictionary, the dictionary cannot be converted into a UseCaseUsage");
            }
            string declaredShortNameFeature = declaredShortNameObject == null ? null : Convert.ToString(declaredShortNameObject);

            if (!dictionary.TryGetValue("direction", out object directionObject))
            {
                throw new ArgumentException("The direction property is missing from the dictionary, the dictionary cannot be converted into a UseCaseUsage");
            }
            FeatureDirectionKind? directionFeature = directionObject == null ? null : (FeatureDirectionKind?)Enum.Parse(typeof(FeatureDirectionKind), Convert.ToString(directionObject), true);

            if (!dictionary.TryGetValue("elementId", out object elementIdObject))
            {
                throw new ArgumentException("The elementId property is missing from the dictionary, the dictionary cannot be converted into a UseCaseUsage");
            }
            string elementIdFeature = Convert.ToString(elementIdObject);

            if (!dictionary.TryGetValue("isAbstract", out object isAbstractObject))
            {
                throw new ArgumentException("The isAbstract property is missing from the dictionary, the dictionary cannot be converted into a UseCaseUsage");
            }
            bool isAbstractFeature = Convert.ToBoolean(isAbstractObject);

            if (!dictionary.TryGetValue("isComposite", out object isCompositeObject))
            {
                throw new ArgumentException("The isComposite property is missing from the dictionary, the dictionary cannot be converted into a UseCaseUsage");
            }
            bool isCompositeFeature = Convert.ToBoolean(isCompositeObject);

            if (!dictionary.TryGetValue("isConstant", out object isConstantObject))
            {
                throw new ArgumentException("The isConstant property is missing from the dictionary, the dictionary cannot be converted into a UseCaseUsage");
            }
            bool isConstantFeature = Convert.ToBoolean(isConstantObject);

            if (!dictionary.TryGetValue("isDerived", out object isDerivedObject))
            {
                throw new ArgumentException("The isDerived property is missing from the dictionary, the dictionary cannot be converted into a UseCaseUsage");
            }
            bool isDerivedFeature = Convert.ToBoolean(isDerivedObject);

            if (!dictionary.TryGetValue("isEnd", out object isEndObject))
            {
                throw new ArgumentException("The isEnd property is missing from the dictionary, the dictionary cannot be converted into a UseCaseUsage");
            }
            bool isEndFeature = Convert.ToBoolean(isEndObject);

            if (!dictionary.TryGetValue("isImpliedIncluded", out object isImpliedIncludedObject))
            {
                throw new ArgumentException("The isImpliedIncluded property is missing from the dictionary, the dictionary cannot be converted into a UseCaseUsage");
            }
            bool isImpliedIncludedFeature = Convert.ToBoolean(isImpliedIncludedObject);

            if (!dictionary.TryGetValue("isIndividual", out object isIndividualObject))
            {
                throw new ArgumentException("The isIndividual property is missing from the dictionary, the dictionary cannot be converted into a UseCaseUsage");
            }
            bool isIndividualFeature = Convert.ToBoolean(isIndividualObject);

            if (!dictionary.TryGetValue("isOrdered", out object isOrderedObject))
            {
                throw new ArgumentException("The isOrdered property is missing from the dictionary, the dictionary cannot be converted into a UseCaseUsage");
            }
            bool isOrderedFeature = Convert.ToBoolean(isOrderedObject);

            if (!dictionary.TryGetValue("isPortion", out object isPortionObject))
            {
                throw new ArgumentException("The isPortion property is missing from the dictionary, the dictionary cannot be converted into a UseCaseUsage");
            }
            bool isPortionFeature = Convert.ToBoolean(isPortionObject);

            if (!dictionary.TryGetValue("isSufficient", out object isSufficientObject))
            {
                throw new ArgumentException("The isSufficient property is missing from the dictionary, the dictionary cannot be converted into a UseCaseUsage");
            }
            bool isSufficientFeature = Convert.ToBoolean(isSufficientObject);

            if (!dictionary.TryGetValue("isUnique", out object isUniqueObject))
            {
                throw new ArgumentException("The isUnique property is missing from the dictionary, the dictionary cannot be converted into a UseCaseUsage");
            }
            bool isUniqueFeature = Convert.ToBoolean(isUniqueObject);

            if (!dictionary.TryGetValue("isVariable", out object isVariableObject))
            {
                throw new ArgumentException("The isVariable property is missing from the dictionary, the dictionary cannot be converted into a UseCaseUsage");
            }
            bool isVariableFeature = Convert.ToBoolean(isVariableObject);

            if (!dictionary.TryGetValue("isVariation", out object isVariationObject))
            {
                throw new ArgumentException("The isVariation property is missing from the dictionary, the dictionary cannot be converted into a UseCaseUsage");
            }
            bool isVariationFeature = Convert.ToBoolean(isVariationObject);

            if (!dictionary.TryGetValue("ownedRelationship", out object ownedRelationshipObject))
            {
                throw new ArgumentException("The ownedRelationship property is missing from the dictionary, the dictionary cannot be converted into a UseCaseUsage");
            }
            List<Guid> ownedRelationshipFeature = (ownedRelationshipObject as List<string>)?.Select(Guid.Parse).ToList();

            if (!dictionary.TryGetValue("owningRelationship", out object owningRelationshipObject))
            {
                throw new ArgumentException("The owningRelationship property is missing from the dictionary, the dictionary cannot be converted into a UseCaseUsage");
            }
            Guid? owningRelationshipFeature = owningRelationshipObject == null ? (Guid?)null : Guid.Parse(Convert.ToString(owningRelationshipObject));

            if (!dictionary.TryGetValue("portionKind", out object portionKindObject))
            {
                throw new ArgumentException("The portionKind property is missing from the dictionary, the dictionary cannot be converted into a UseCaseUsage");
            }
            PortionKind? portionKindFeature = portionKindObject == null ? null : (PortionKind?)Enum.Parse(typeof(PortionKind), Convert.ToString(portionKindObject), true);


            useCaseUsageInstance.AliasIds = aliasIdsFeature ?? new List<string>();
            useCaseUsageInstance.DeclaredName = declaredNameFeature;
            useCaseUsageInstance.DeclaredShortName = declaredShortNameFeature;
            useCaseUsageInstance.Direction = directionFeature;
            useCaseUsageInstance.ElementId = elementIdFeature;
            useCaseUsageInstance.IsAbstract = isAbstractFeature;
            useCaseUsageInstance.IsComposite = isCompositeFeature;
            useCaseUsageInstance.IsConstant = isConstantFeature;
            useCaseUsageInstance.IsDerived = isDerivedFeature;
            useCaseUsageInstance.IsEnd = isEndFeature;
            useCaseUsageInstance.IsImpliedIncluded = isImpliedIncludedFeature;
            useCaseUsageInstance.IsIndividual = isIndividualFeature;
            useCaseUsageInstance.IsOrdered = isOrderedFeature;
            useCaseUsageInstance.IsPortion = isPortionFeature;
            useCaseUsageInstance.IsSufficient = isSufficientFeature;
            useCaseUsageInstance.IsUnique = isUniqueFeature;
            useCaseUsageInstance.IsVariable = isVariableFeature;
            useCaseUsageInstance.IsVariation = isVariationFeature;
            useCaseUsageInstance.OwnedRelationship = ownedRelationshipFeature ?? new List<Guid>();
            useCaseUsageInstance.OwningRelationship = owningRelationshipFeature;
            useCaseUsageInstance.PortionKind = portionKindFeature;

            return useCaseUsageInstance;
        }

        /// <summary>
        /// Reads a <see cref="IUseCaseUsage"/> from a <see cref="Dictionary{String, Object}"/> that contains a key-value-pair
        /// for each property.
        /// </summary>
        /// <param name="dictionary">
        /// The subject <see cref="Dictionary{String, Object}"/> that is to be converted into a <see cref="IUseCaseUsage"/>
        /// </param>
        /// <returns>
        /// An instance of <see cref="IUseCaseUsage"/>
        /// </returns>
        private static IUseCaseUsage ReadComplex(Dictionary<string, object> dictionary)
        {
            var useCaseUsageInstance = DictionaryNullAndTypeCheck(dictionary);

            if (!dictionary.TryGetValue("aliasIds", out object aliasIdsObject))
            {
                throw new ArgumentException("The aliasIds property is missing from the dictionary, the dictionary cannot be converted into a UseCaseUsage");
            }
            List<string> aliasIdsFeature = aliasIdsObject as List<string>;

            if (!dictionary.TryGetValue("declaredName", out object declaredNameObject))
            {
                throw new ArgumentException("The declaredName property is missing from the dictionary, the dictionary cannot be converted into a UseCaseUsage");
            }
            string declaredNameFeature = declaredNameObject == null ? null : Convert.ToString(declaredNameObject);

            if (!dictionary.TryGetValue("declaredShortName", out object declaredShortNameObject))
            {
                throw new ArgumentException("The declaredShortName property is missing from the dictionary, the dictionary cannot be converted into a UseCaseUsage");
            }
            string declaredShortNameFeature = declaredShortNameObject == null ? null : Convert.ToString(declaredShortNameObject);

            if (!dictionary.TryGetValue("direction", out object directionObject))
            {
                throw new ArgumentException("The direction property is missing from the dictionary, the dictionary cannot be converted into a UseCaseUsage");
            }
            FeatureDirectionKind? directionFeature = (FeatureDirectionKind?)directionObject;

            if (!dictionary.TryGetValue("elementId", out object elementIdObject))
            {
                throw new ArgumentException("The elementId property is missing from the dictionary, the dictionary cannot be converted into a UseCaseUsage");
            }
            string elementIdFeature = Convert.ToString(elementIdObject);

            if (!dictionary.TryGetValue("isAbstract", out object isAbstractObject))
            {
                throw new ArgumentException("The isAbstract property is missing from the dictionary, the dictionary cannot be converted into a UseCaseUsage");
            }
            bool isAbstractFeature = Convert.ToBoolean(isAbstractObject);

            if (!dictionary.TryGetValue("isComposite", out object isCompositeObject))
            {
                throw new ArgumentException("The isComposite property is missing from the dictionary, the dictionary cannot be converted into a UseCaseUsage");
            }
            bool isCompositeFeature = Convert.ToBoolean(isCompositeObject);

            if (!dictionary.TryGetValue("isConstant", out object isConstantObject))
            {
                throw new ArgumentException("The isConstant property is missing from the dictionary, the dictionary cannot be converted into a UseCaseUsage");
            }
            bool isConstantFeature = Convert.ToBoolean(isConstantObject);

            if (!dictionary.TryGetValue("isDerived", out object isDerivedObject))
            {
                throw new ArgumentException("The isDerived property is missing from the dictionary, the dictionary cannot be converted into a UseCaseUsage");
            }
            bool isDerivedFeature = Convert.ToBoolean(isDerivedObject);

            if (!dictionary.TryGetValue("isEnd", out object isEndObject))
            {
                throw new ArgumentException("The isEnd property is missing from the dictionary, the dictionary cannot be converted into a UseCaseUsage");
            }
            bool isEndFeature = Convert.ToBoolean(isEndObject);

            if (!dictionary.TryGetValue("isImpliedIncluded", out object isImpliedIncludedObject))
            {
                throw new ArgumentException("The isImpliedIncluded property is missing from the dictionary, the dictionary cannot be converted into a UseCaseUsage");
            }
            bool isImpliedIncludedFeature = Convert.ToBoolean(isImpliedIncludedObject);

            if (!dictionary.TryGetValue("isIndividual", out object isIndividualObject))
            {
                throw new ArgumentException("The isIndividual property is missing from the dictionary, the dictionary cannot be converted into a UseCaseUsage");
            }
            bool isIndividualFeature = Convert.ToBoolean(isIndividualObject);

            if (!dictionary.TryGetValue("isOrdered", out object isOrderedObject))
            {
                throw new ArgumentException("The isOrdered property is missing from the dictionary, the dictionary cannot be converted into a UseCaseUsage");
            }
            bool isOrderedFeature = Convert.ToBoolean(isOrderedObject);

            if (!dictionary.TryGetValue("isPortion", out object isPortionObject))
            {
                throw new ArgumentException("The isPortion property is missing from the dictionary, the dictionary cannot be converted into a UseCaseUsage");
            }
            bool isPortionFeature = Convert.ToBoolean(isPortionObject);

            if (!dictionary.TryGetValue("isSufficient", out object isSufficientObject))
            {
                throw new ArgumentException("The isSufficient property is missing from the dictionary, the dictionary cannot be converted into a UseCaseUsage");
            }
            bool isSufficientFeature = Convert.ToBoolean(isSufficientObject);

            if (!dictionary.TryGetValue("isUnique", out object isUniqueObject))
            {
                throw new ArgumentException("The isUnique property is missing from the dictionary, the dictionary cannot be converted into a UseCaseUsage");
            }
            bool isUniqueFeature = Convert.ToBoolean(isUniqueObject);

            if (!dictionary.TryGetValue("isVariable", out object isVariableObject))
            {
                throw new ArgumentException("The isVariable property is missing from the dictionary, the dictionary cannot be converted into a UseCaseUsage");
            }
            bool isVariableFeature = Convert.ToBoolean(isVariableObject);

            if (!dictionary.TryGetValue("isVariation", out object isVariationObject))
            {
                throw new ArgumentException("The isVariation property is missing from the dictionary, the dictionary cannot be converted into a UseCaseUsage");
            }
            bool isVariationFeature = Convert.ToBoolean(isVariationObject);

            if (!dictionary.TryGetValue("ownedRelationship", out object ownedRelationshipObject))
            {
                throw new ArgumentException("The ownedRelationship property is missing from the dictionary, the dictionary cannot be converted into a UseCaseUsage");
            }
            List<Guid> ownedRelationshipFeature = (ownedRelationshipObject as List<Guid>);

            if (!dictionary.TryGetValue("owningRelationship", out object owningRelationshipObject))
            {
                throw new ArgumentException("The owningRelationship property is missing from the dictionary, the dictionary cannot be converted into a UseCaseUsage");
            }
            Guid? owningRelationshipFeature = (Guid?)owningRelationshipObject;

            if (!dictionary.TryGetValue("portionKind", out object portionKindObject))
            {
                throw new ArgumentException("The portionKind property is missing from the dictionary, the dictionary cannot be converted into a UseCaseUsage");
            }
            PortionKind? portionKindFeature = (PortionKind?)portionKindObject;


            useCaseUsageInstance.AliasIds = aliasIdsFeature ?? new List<string>();
            useCaseUsageInstance.DeclaredName = declaredNameFeature;
            useCaseUsageInstance.DeclaredShortName = declaredShortNameFeature;
            useCaseUsageInstance.Direction = directionFeature;
            useCaseUsageInstance.ElementId = elementIdFeature;
            useCaseUsageInstance.IsAbstract = isAbstractFeature;
            useCaseUsageInstance.IsComposite = isCompositeFeature;
            useCaseUsageInstance.IsConstant = isConstantFeature;
            useCaseUsageInstance.IsDerived = isDerivedFeature;
            useCaseUsageInstance.IsEnd = isEndFeature;
            useCaseUsageInstance.IsImpliedIncluded = isImpliedIncludedFeature;
            useCaseUsageInstance.IsIndividual = isIndividualFeature;
            useCaseUsageInstance.IsOrdered = isOrderedFeature;
            useCaseUsageInstance.IsPortion = isPortionFeature;
            useCaseUsageInstance.IsSufficient = isSufficientFeature;
            useCaseUsageInstance.IsUnique = isUniqueFeature;
            useCaseUsageInstance.IsVariable = isVariableFeature;
            useCaseUsageInstance.IsVariation = isVariationFeature;
            useCaseUsageInstance.OwnedRelationship = ownedRelationshipFeature ?? new List<Guid>();
            useCaseUsageInstance.OwningRelationship = owningRelationshipFeature;
            useCaseUsageInstance.PortionKind = portionKindFeature;

            return useCaseUsageInstance;
        }

        /// <summary>
        /// Checks whether the <see cref="Dictionary{String, Object}"/> is not null and whether it is
        /// of type <see cref="UseCaseUsage"/>
        /// </summary>
        /// <param name="dictionary">
        /// The subject <see cref="Dictionary{String, Object}"/> that contains the key-value pairs of
        /// properties and values.
        /// </param>
        /// <returns>
        /// an instance of <see cref="IUseCaseUsage"/>
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="dictionary"/> is null
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown when <paramref name="dictionary"/> is not of type <see cref="UseCaseUsage"/>
        /// </exception>
        private static IUseCaseUsage DictionaryNullAndTypeCheck(Dictionary<string, object> dictionary)
        {
            if (dictionary == null)
            {
                throw new ArgumentNullException("dictionary", "The dictionary may not be null");
            }

            if (!dictionary.TryGetValue("@type", out object typeObject))
            {
                throw new ArgumentException("The type property is missing from the dictionary, the dictionary cannot be converted into a UseCaseUsage");
            }

            var type = Convert.ToString(typeObject);

            if (type != "UseCaseUsage")
            {
                throw new ArgumentException($"The dictionary contains an Object is of type {type} and can therefore not be converted into a UseCaseUsage");
            }

            if (!dictionary.TryGetValue("@id", out object idObject))
            {
                throw new ArgumentException("The id property is missing from the dictionary, the dictionary cannot be converted into a UseCaseUsage");
            }
            var id = Guid.Parse(Convert.ToString(idObject));

            var useCaseUsageInstance = new SysML2.NET.Core.DTO.UseCaseUsage
            {
                Id = id
            };

            return useCaseUsageInstance;
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
