// -------------------------------------------------------------------------------------------------
// <copyright file="ExhibitStateUsageDictionaryReader.cs" company="RHEA System S.A.">
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
    /// The purpose of the <see cref="ExhibitStateUsageDictionaryReader"/> is to read (convert)
    /// a <see cref="Dictionary{String, Object}"/> from an <see cref="IExhibitStateUsage"/>
    /// </summary>
    public static class ExhibitStateUsageDictionaryReader
    {
        /// <summary>
        /// Reads a <see cref="IExhibitStateUsage"/> from a <see cref="Dictionary{String, Object}"/> that contains a key-value-pair
        /// for each property.
        /// </summary>
        /// <param name="dictionary">
        /// The subject <see cref="Dictionary{String, Object}"/> that is to be converted into a <see cref="IExhibitStateUsage"/>
        /// </param>
        /// <param name="dictionaryKind">
        /// The source <see cref="DictionaryKind"/> that is to be read from
        /// </param>
        /// <returns>
        /// An instance of <see cref="IExhibitStateUsage"/>
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
        /// Reads a <see cref="IExhibitStateUsage"/> from a <see cref="Dictionary{String, Object}"/> that contains a key-value-pair
        /// for each property.
        /// </summary>
        /// <param name="dictionary">
        /// The subject <see cref="Dictionary{String, Object}"/> that is to be converted into a <see cref="IExhibitStateUsage"/>
        /// </param>
        /// <returns>
        /// An instance of <see cref="IExhibitStateUsage"/>
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
        private static IExhibitStateUsage ReadSimplified(Dictionary<string, object> dictionary)
        {
            var exhibitStateUsageInstance = DictionaryNullAndTypeCheck(dictionary);

            if (!dictionary.TryGetValue("aliasIds", out object aliasIdsObject))
            {
                throw new ArgumentException("The aliasIds property is missing from the dictionary, the dictionary cannot be converted into a ExhibitStateUsage");
            }
            List<string> aliasIdsFeature = aliasIdsObject as List<string>;

            if (!dictionary.TryGetValue("declaredName", out object declaredNameObject))
            {
                throw new ArgumentException("The declaredName property is missing from the dictionary, the dictionary cannot be converted into a ExhibitStateUsage");
            }
            string declaredNameFeature = declaredNameObject == null ? null : Convert.ToString(declaredNameObject);

            if (!dictionary.TryGetValue("declaredShortName", out object declaredShortNameObject))
            {
                throw new ArgumentException("The declaredShortName property is missing from the dictionary, the dictionary cannot be converted into a ExhibitStateUsage");
            }
            string declaredShortNameFeature = declaredShortNameObject == null ? null : Convert.ToString(declaredShortNameObject);

            if (!dictionary.TryGetValue("direction", out object directionObject))
            {
                throw new ArgumentException("The direction property is missing from the dictionary, the dictionary cannot be converted into a ExhibitStateUsage");
            }
            FeatureDirectionKind? directionFeature = directionObject == null ? null : (FeatureDirectionKind?)Enum.Parse(typeof(FeatureDirectionKind), Convert.ToString(directionObject), true);

            if (!dictionary.TryGetValue("elementId", out object elementIdObject))
            {
                throw new ArgumentException("The elementId property is missing from the dictionary, the dictionary cannot be converted into a ExhibitStateUsage");
            }
            string elementIdFeature = Convert.ToString(elementIdObject);

            if (!dictionary.TryGetValue("isAbstract", out object isAbstractObject))
            {
                throw new ArgumentException("The isAbstract property is missing from the dictionary, the dictionary cannot be converted into a ExhibitStateUsage");
            }
            bool isAbstractFeature = Convert.ToBoolean(isAbstractObject);

            if (!dictionary.TryGetValue("isComposite", out object isCompositeObject))
            {
                throw new ArgumentException("The isComposite property is missing from the dictionary, the dictionary cannot be converted into a ExhibitStateUsage");
            }
            bool isCompositeFeature = Convert.ToBoolean(isCompositeObject);

            if (!dictionary.TryGetValue("isDerived", out object isDerivedObject))
            {
                throw new ArgumentException("The isDerived property is missing from the dictionary, the dictionary cannot be converted into a ExhibitStateUsage");
            }
            bool isDerivedFeature = Convert.ToBoolean(isDerivedObject);

            if (!dictionary.TryGetValue("isEnd", out object isEndObject))
            {
                throw new ArgumentException("The isEnd property is missing from the dictionary, the dictionary cannot be converted into a ExhibitStateUsage");
            }
            bool isEndFeature = Convert.ToBoolean(isEndObject);

            if (!dictionary.TryGetValue("isImpliedIncluded", out object isImpliedIncludedObject))
            {
                throw new ArgumentException("The isImpliedIncluded property is missing from the dictionary, the dictionary cannot be converted into a ExhibitStateUsage");
            }
            bool isImpliedIncludedFeature = Convert.ToBoolean(isImpliedIncludedObject);

            if (!dictionary.TryGetValue("isIndividual", out object isIndividualObject))
            {
                throw new ArgumentException("The isIndividual property is missing from the dictionary, the dictionary cannot be converted into a ExhibitStateUsage");
            }
            bool isIndividualFeature = Convert.ToBoolean(isIndividualObject);

            if (!dictionary.TryGetValue("isOrdered", out object isOrderedObject))
            {
                throw new ArgumentException("The isOrdered property is missing from the dictionary, the dictionary cannot be converted into a ExhibitStateUsage");
            }
            bool isOrderedFeature = Convert.ToBoolean(isOrderedObject);

            if (!dictionary.TryGetValue("isParallel", out object isParallelObject))
            {
                throw new ArgumentException("The isParallel property is missing from the dictionary, the dictionary cannot be converted into a ExhibitStateUsage");
            }
            bool isParallelFeature = Convert.ToBoolean(isParallelObject);

            if (!dictionary.TryGetValue("isPortion", out object isPortionObject))
            {
                throw new ArgumentException("The isPortion property is missing from the dictionary, the dictionary cannot be converted into a ExhibitStateUsage");
            }
            bool isPortionFeature = Convert.ToBoolean(isPortionObject);

            if (!dictionary.TryGetValue("isReadOnly", out object isReadOnlyObject))
            {
                throw new ArgumentException("The isReadOnly property is missing from the dictionary, the dictionary cannot be converted into a ExhibitStateUsage");
            }
            bool isReadOnlyFeature = Convert.ToBoolean(isReadOnlyObject);

            if (!dictionary.TryGetValue("isSufficient", out object isSufficientObject))
            {
                throw new ArgumentException("The isSufficient property is missing from the dictionary, the dictionary cannot be converted into a ExhibitStateUsage");
            }
            bool isSufficientFeature = Convert.ToBoolean(isSufficientObject);

            if (!dictionary.TryGetValue("isUnique", out object isUniqueObject))
            {
                throw new ArgumentException("The isUnique property is missing from the dictionary, the dictionary cannot be converted into a ExhibitStateUsage");
            }
            bool isUniqueFeature = Convert.ToBoolean(isUniqueObject);

            if (!dictionary.TryGetValue("isVariation", out object isVariationObject))
            {
                throw new ArgumentException("The isVariation property is missing from the dictionary, the dictionary cannot be converted into a ExhibitStateUsage");
            }
            bool isVariationFeature = Convert.ToBoolean(isVariationObject);

            if (!dictionary.TryGetValue("ownedRelationship", out object ownedRelationshipObject))
            {
                throw new ArgumentException("The ownedRelationship property is missing from the dictionary, the dictionary cannot be converted into a ExhibitStateUsage");
            }
            List<Guid> ownedRelationshipFeature = (ownedRelationshipObject as List<string>)?.Select(Guid.Parse).ToList();

            if (!dictionary.TryGetValue("owningRelationship", out object owningRelationshipObject))
            {
                throw new ArgumentException("The owningRelationship property is missing from the dictionary, the dictionary cannot be converted into a ExhibitStateUsage");
            }
            Guid? owningRelationshipFeature = owningRelationshipObject == null ? (Guid?)null : Guid.Parse(Convert.ToString(owningRelationshipObject));

            if (!dictionary.TryGetValue("portionKind", out object portionKindObject))
            {
                throw new ArgumentException("The portionKind property is missing from the dictionary, the dictionary cannot be converted into a ExhibitStateUsage");
            }
            PortionKind? portionKindFeature = portionKindObject == null ? null : (PortionKind?)Enum.Parse(typeof(PortionKind), Convert.ToString(portionKindObject), true);


            exhibitStateUsageInstance.AliasIds = aliasIdsFeature ?? new List<string>();
            exhibitStateUsageInstance.DeclaredName = declaredNameFeature;
            exhibitStateUsageInstance.DeclaredShortName = declaredShortNameFeature;
            exhibitStateUsageInstance.Direction = directionFeature;
            exhibitStateUsageInstance.ElementId = elementIdFeature;
            exhibitStateUsageInstance.IsAbstract = isAbstractFeature;
            exhibitStateUsageInstance.IsComposite = isCompositeFeature;
            exhibitStateUsageInstance.IsDerived = isDerivedFeature;
            exhibitStateUsageInstance.IsEnd = isEndFeature;
            exhibitStateUsageInstance.IsImpliedIncluded = isImpliedIncludedFeature;
            exhibitStateUsageInstance.IsIndividual = isIndividualFeature;
            exhibitStateUsageInstance.IsOrdered = isOrderedFeature;
            exhibitStateUsageInstance.IsParallel = isParallelFeature;
            exhibitStateUsageInstance.IsPortion = isPortionFeature;
            exhibitStateUsageInstance.IsReadOnly = isReadOnlyFeature;
            exhibitStateUsageInstance.IsSufficient = isSufficientFeature;
            exhibitStateUsageInstance.IsUnique = isUniqueFeature;
            exhibitStateUsageInstance.IsVariation = isVariationFeature;
            exhibitStateUsageInstance.OwnedRelationship = ownedRelationshipFeature ?? new List<Guid>();
            exhibitStateUsageInstance.OwningRelationship = owningRelationshipFeature;
            exhibitStateUsageInstance.PortionKind = portionKindFeature;

            return exhibitStateUsageInstance;
        }

        /// <summary>
        /// Reads a <see cref="IExhibitStateUsage"/> from a <see cref="Dictionary{String, Object}"/> that contains a key-value-pair
        /// for each property.
        /// </summary>
        /// <param name="dictionary">
        /// The subject <see cref="Dictionary{String, Object}"/> that is to be converted into a <see cref="IExhibitStateUsage"/>
        /// </param>
        /// <returns>
        /// An instance of <see cref="IExhibitStateUsage"/>
        /// </returns>
        private static IExhibitStateUsage ReadComplex(Dictionary<string, object> dictionary)
        {
            var exhibitStateUsageInstance = DictionaryNullAndTypeCheck(dictionary);

            if (!dictionary.TryGetValue("aliasIds", out object aliasIdsObject))
            {
                throw new ArgumentException("The aliasIds property is missing from the dictionary, the dictionary cannot be converted into a ExhibitStateUsage");
            }
            List<string> aliasIdsFeature = aliasIdsObject as List<string>;

            if (!dictionary.TryGetValue("declaredName", out object declaredNameObject))
            {
                throw new ArgumentException("The declaredName property is missing from the dictionary, the dictionary cannot be converted into a ExhibitStateUsage");
            }
            string declaredNameFeature = declaredNameObject == null ? null : Convert.ToString(declaredNameObject);

            if (!dictionary.TryGetValue("declaredShortName", out object declaredShortNameObject))
            {
                throw new ArgumentException("The declaredShortName property is missing from the dictionary, the dictionary cannot be converted into a ExhibitStateUsage");
            }
            string declaredShortNameFeature = declaredShortNameObject == null ? null : Convert.ToString(declaredShortNameObject);

            if (!dictionary.TryGetValue("direction", out object directionObject))
            {
                throw new ArgumentException("The direction property is missing from the dictionary, the dictionary cannot be converted into a ExhibitStateUsage");
            }
            FeatureDirectionKind? directionFeature = (FeatureDirectionKind?)directionObject;

            if (!dictionary.TryGetValue("elementId", out object elementIdObject))
            {
                throw new ArgumentException("The elementId property is missing from the dictionary, the dictionary cannot be converted into a ExhibitStateUsage");
            }
            string elementIdFeature = Convert.ToString(elementIdObject);

            if (!dictionary.TryGetValue("isAbstract", out object isAbstractObject))
            {
                throw new ArgumentException("The isAbstract property is missing from the dictionary, the dictionary cannot be converted into a ExhibitStateUsage");
            }
            bool isAbstractFeature = Convert.ToBoolean(isAbstractObject);

            if (!dictionary.TryGetValue("isComposite", out object isCompositeObject))
            {
                throw new ArgumentException("The isComposite property is missing from the dictionary, the dictionary cannot be converted into a ExhibitStateUsage");
            }
            bool isCompositeFeature = Convert.ToBoolean(isCompositeObject);

            if (!dictionary.TryGetValue("isDerived", out object isDerivedObject))
            {
                throw new ArgumentException("The isDerived property is missing from the dictionary, the dictionary cannot be converted into a ExhibitStateUsage");
            }
            bool isDerivedFeature = Convert.ToBoolean(isDerivedObject);

            if (!dictionary.TryGetValue("isEnd", out object isEndObject))
            {
                throw new ArgumentException("The isEnd property is missing from the dictionary, the dictionary cannot be converted into a ExhibitStateUsage");
            }
            bool isEndFeature = Convert.ToBoolean(isEndObject);

            if (!dictionary.TryGetValue("isImpliedIncluded", out object isImpliedIncludedObject))
            {
                throw new ArgumentException("The isImpliedIncluded property is missing from the dictionary, the dictionary cannot be converted into a ExhibitStateUsage");
            }
            bool isImpliedIncludedFeature = Convert.ToBoolean(isImpliedIncludedObject);

            if (!dictionary.TryGetValue("isIndividual", out object isIndividualObject))
            {
                throw new ArgumentException("The isIndividual property is missing from the dictionary, the dictionary cannot be converted into a ExhibitStateUsage");
            }
            bool isIndividualFeature = Convert.ToBoolean(isIndividualObject);

            if (!dictionary.TryGetValue("isOrdered", out object isOrderedObject))
            {
                throw new ArgumentException("The isOrdered property is missing from the dictionary, the dictionary cannot be converted into a ExhibitStateUsage");
            }
            bool isOrderedFeature = Convert.ToBoolean(isOrderedObject);

            if (!dictionary.TryGetValue("isParallel", out object isParallelObject))
            {
                throw new ArgumentException("The isParallel property is missing from the dictionary, the dictionary cannot be converted into a ExhibitStateUsage");
            }
            bool isParallelFeature = Convert.ToBoolean(isParallelObject);

            if (!dictionary.TryGetValue("isPortion", out object isPortionObject))
            {
                throw new ArgumentException("The isPortion property is missing from the dictionary, the dictionary cannot be converted into a ExhibitStateUsage");
            }
            bool isPortionFeature = Convert.ToBoolean(isPortionObject);

            if (!dictionary.TryGetValue("isReadOnly", out object isReadOnlyObject))
            {
                throw new ArgumentException("The isReadOnly property is missing from the dictionary, the dictionary cannot be converted into a ExhibitStateUsage");
            }
            bool isReadOnlyFeature = Convert.ToBoolean(isReadOnlyObject);

            if (!dictionary.TryGetValue("isSufficient", out object isSufficientObject))
            {
                throw new ArgumentException("The isSufficient property is missing from the dictionary, the dictionary cannot be converted into a ExhibitStateUsage");
            }
            bool isSufficientFeature = Convert.ToBoolean(isSufficientObject);

            if (!dictionary.TryGetValue("isUnique", out object isUniqueObject))
            {
                throw new ArgumentException("The isUnique property is missing from the dictionary, the dictionary cannot be converted into a ExhibitStateUsage");
            }
            bool isUniqueFeature = Convert.ToBoolean(isUniqueObject);

            if (!dictionary.TryGetValue("isVariation", out object isVariationObject))
            {
                throw new ArgumentException("The isVariation property is missing from the dictionary, the dictionary cannot be converted into a ExhibitStateUsage");
            }
            bool isVariationFeature = Convert.ToBoolean(isVariationObject);

            if (!dictionary.TryGetValue("ownedRelationship", out object ownedRelationshipObject))
            {
                throw new ArgumentException("The ownedRelationship property is missing from the dictionary, the dictionary cannot be converted into a ExhibitStateUsage");
            }
            List<Guid> ownedRelationshipFeature = (ownedRelationshipObject as List<Guid>);

            if (!dictionary.TryGetValue("owningRelationship", out object owningRelationshipObject))
            {
                throw new ArgumentException("The owningRelationship property is missing from the dictionary, the dictionary cannot be converted into a ExhibitStateUsage");
            }
            Guid? owningRelationshipFeature = (Guid?)owningRelationshipObject;

            if (!dictionary.TryGetValue("portionKind", out object portionKindObject))
            {
                throw new ArgumentException("The portionKind property is missing from the dictionary, the dictionary cannot be converted into a ExhibitStateUsage");
            }
            PortionKind? portionKindFeature = (PortionKind?)portionKindObject;


            exhibitStateUsageInstance.AliasIds = aliasIdsFeature ?? new List<string>();
            exhibitStateUsageInstance.DeclaredName = declaredNameFeature;
            exhibitStateUsageInstance.DeclaredShortName = declaredShortNameFeature;
            exhibitStateUsageInstance.Direction = directionFeature;
            exhibitStateUsageInstance.ElementId = elementIdFeature;
            exhibitStateUsageInstance.IsAbstract = isAbstractFeature;
            exhibitStateUsageInstance.IsComposite = isCompositeFeature;
            exhibitStateUsageInstance.IsDerived = isDerivedFeature;
            exhibitStateUsageInstance.IsEnd = isEndFeature;
            exhibitStateUsageInstance.IsImpliedIncluded = isImpliedIncludedFeature;
            exhibitStateUsageInstance.IsIndividual = isIndividualFeature;
            exhibitStateUsageInstance.IsOrdered = isOrderedFeature;
            exhibitStateUsageInstance.IsParallel = isParallelFeature;
            exhibitStateUsageInstance.IsPortion = isPortionFeature;
            exhibitStateUsageInstance.IsReadOnly = isReadOnlyFeature;
            exhibitStateUsageInstance.IsSufficient = isSufficientFeature;
            exhibitStateUsageInstance.IsUnique = isUniqueFeature;
            exhibitStateUsageInstance.IsVariation = isVariationFeature;
            exhibitStateUsageInstance.OwnedRelationship = ownedRelationshipFeature ?? new List<Guid>();
            exhibitStateUsageInstance.OwningRelationship = owningRelationshipFeature;
            exhibitStateUsageInstance.PortionKind = portionKindFeature;

            return exhibitStateUsageInstance;
        }

        /// <summary>
        /// Checks whether the <see cref="Dictionary{String, Object}"/> is not null and whether it is
        /// of type <see cref="ExhibitStateUsage"/>
        /// </summary>
        /// <param name="dictionary">
        /// The subject <see cref="Dictionary{String, Object}"/> that contains the key-value pairs of
        /// properties and values.
        /// </param>
        /// <returns>
        /// an instance of <see cref="IExhibitStateUsage"/>
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="dictionary"/> is null
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown when <paramref name="dictionary"/> is not of type <see cref="ExhibitStateUsage"/>
        /// </exception>
        private static IExhibitStateUsage DictionaryNullAndTypeCheck(Dictionary<string, object> dictionary)
        {
            if (dictionary == null)
            {
                throw new ArgumentNullException("dictionary", "The dictionary may not be null");
            }

            if (!dictionary.TryGetValue("@type", out object typeObject))
            {
                throw new ArgumentException("The type property is missing from the dictionary, the dictionary cannot be converted into a ExhibitStateUsage");
            }

            var type = Convert.ToString(typeObject);

            if (type != "ExhibitStateUsage")
            {
                throw new ArgumentException($"The dictionary contains an Object is of type {type} and can therefore not be converted into a ExhibitStateUsage");
            }

            if (!dictionary.TryGetValue("@id", out object idObject))
            {
                throw new ArgumentException("The id property is missing from the dictionary, the dictionary cannot be converted into a ExhibitStateUsage");
            }
            var id = Guid.Parse(Convert.ToString(idObject));

            var exhibitStateUsageInstance = new SysML2.NET.Core.DTO.ExhibitStateUsage
            {
                Id = id
            };

            return exhibitStateUsageInstance;
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
