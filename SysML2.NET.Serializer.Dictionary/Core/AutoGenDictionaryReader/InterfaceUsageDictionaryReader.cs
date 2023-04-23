// -------------------------------------------------------------------------------------------------
// <copyright file="InterfaceUsageDictionaryReader.cs" company="RHEA System S.A.">
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

namespace SysML2.NET.Serializer.Dictionary.Core.DTO
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using SysML2.NET.Common;
    using SysML2.NET.Core;
    using SysML2.NET.Core.DTO;

    /// <summary>
    /// The purpose of the <see cref="InterfaceUsageDictionaryReader"/> is to read (convert)
    /// a <see cref="Dictionary{String, Object}"/> from an <see cref="IInterfaceUsage"/>
    /// </summary>
    public static class InterfaceUsageDictionaryReader
    {
        /// <summary>
        /// Reads a <see cref="IInterfaceUsage"/> from a <see cref="Dictionary{String, Object}"/> that contains a key-value-pair
        /// for each property.
        /// </summary>
        /// <param name="dictionary">
        /// The subject <see cref="Dictionary{String, Object}"/> that is to be converted into a <see cref="IInterfaceUsage"/>
        /// </param>
        /// <param name="dictionaryKind">
        /// The source <see cref="DictionaryKind"/> that is to be read from
        /// </param>
        /// <returns>
        /// An instance of <see cref="IInterfaceUsage"/>
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
        /// Reads a <see cref="IInterfaceUsage"/> from a <see cref="Dictionary{String, Object}"/> that contains a key-value-pair
        /// for each property.
        /// </summary>
        /// <param name="dictionary">
        /// The subject <see cref="Dictionary{String, Object}"/> that is to be converted into a <see cref="IInterfaceUsage"/>
        /// </param>
        /// <returns>
        /// An instance of <see cref="IInterfaceUsage"/>
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
        private static IInterfaceUsage ReadSimplified(Dictionary<string, object> dictionary)
        {
            var interfaceUsageInstance = DictionaryNullAndTypeCheck(dictionary);

            if (!dictionary.TryGetValue("aliasIds", out object aliasIdsObject))
            {
                throw new ArgumentException("The aliasIds property is missing from the dictionary, the dictionary cannot be converted into a InterfaceUsage");
            }
            List<string> aliasIdsFeature = aliasIdsObject as List<string>;

            if (!dictionary.TryGetValue("declaredName", out object declaredNameObject))
            {
                throw new ArgumentException("The declaredName property is missing from the dictionary, the dictionary cannot be converted into a InterfaceUsage");
            }
            string declaredNameFeature = declaredNameObject == null ? null : Convert.ToString(declaredNameObject);

            if (!dictionary.TryGetValue("declaredShortName", out object declaredShortNameObject))
            {
                throw new ArgumentException("The declaredShortName property is missing from the dictionary, the dictionary cannot be converted into a InterfaceUsage");
            }
            string declaredShortNameFeature = declaredShortNameObject == null ? null : Convert.ToString(declaredShortNameObject);

            if (!dictionary.TryGetValue("direction", out object directionObject))
            {
                throw new ArgumentException("The direction property is missing from the dictionary, the dictionary cannot be converted into a InterfaceUsage");
            }
            FeatureDirectionKind? directionFeature = directionObject == null ? null : (FeatureDirectionKind?)Enum.Parse(typeof(FeatureDirectionKind), Convert.ToString(directionObject), true);

            if (!dictionary.TryGetValue("elementId", out object elementIdObject))
            {
                throw new ArgumentException("The elementId property is missing from the dictionary, the dictionary cannot be converted into a InterfaceUsage");
            }
            string elementIdFeature = Convert.ToString(elementIdObject);

            if (!dictionary.TryGetValue("isAbstract", out object isAbstractObject))
            {
                throw new ArgumentException("The isAbstract property is missing from the dictionary, the dictionary cannot be converted into a InterfaceUsage");
            }
            bool isAbstractFeature = Convert.ToBoolean(isAbstractObject);

            if (!dictionary.TryGetValue("isComposite", out object isCompositeObject))
            {
                throw new ArgumentException("The isComposite property is missing from the dictionary, the dictionary cannot be converted into a InterfaceUsage");
            }
            bool isCompositeFeature = Convert.ToBoolean(isCompositeObject);

            if (!dictionary.TryGetValue("isDerived", out object isDerivedObject))
            {
                throw new ArgumentException("The isDerived property is missing from the dictionary, the dictionary cannot be converted into a InterfaceUsage");
            }
            bool isDerivedFeature = Convert.ToBoolean(isDerivedObject);

            if (!dictionary.TryGetValue("isDirected", out object isDirectedObject))
            {
                throw new ArgumentException("The isDirected property is missing from the dictionary, the dictionary cannot be converted into a InterfaceUsage");
            }
            bool isDirectedFeature = Convert.ToBoolean(isDirectedObject);

            if (!dictionary.TryGetValue("isEnd", out object isEndObject))
            {
                throw new ArgumentException("The isEnd property is missing from the dictionary, the dictionary cannot be converted into a InterfaceUsage");
            }
            bool isEndFeature = Convert.ToBoolean(isEndObject);

            if (!dictionary.TryGetValue("isImplied", out object isImpliedObject))
            {
                throw new ArgumentException("The isImplied property is missing from the dictionary, the dictionary cannot be converted into a InterfaceUsage");
            }
            bool isImpliedFeature = Convert.ToBoolean(isImpliedObject);

            if (!dictionary.TryGetValue("isImpliedIncluded", out object isImpliedIncludedObject))
            {
                throw new ArgumentException("The isImpliedIncluded property is missing from the dictionary, the dictionary cannot be converted into a InterfaceUsage");
            }
            bool isImpliedIncludedFeature = Convert.ToBoolean(isImpliedIncludedObject);

            if (!dictionary.TryGetValue("isIndividual", out object isIndividualObject))
            {
                throw new ArgumentException("The isIndividual property is missing from the dictionary, the dictionary cannot be converted into a InterfaceUsage");
            }
            bool isIndividualFeature = Convert.ToBoolean(isIndividualObject);

            if (!dictionary.TryGetValue("isOrdered", out object isOrderedObject))
            {
                throw new ArgumentException("The isOrdered property is missing from the dictionary, the dictionary cannot be converted into a InterfaceUsage");
            }
            bool isOrderedFeature = Convert.ToBoolean(isOrderedObject);

            if (!dictionary.TryGetValue("isPortion", out object isPortionObject))
            {
                throw new ArgumentException("The isPortion property is missing from the dictionary, the dictionary cannot be converted into a InterfaceUsage");
            }
            bool isPortionFeature = Convert.ToBoolean(isPortionObject);

            if (!dictionary.TryGetValue("isReadOnly", out object isReadOnlyObject))
            {
                throw new ArgumentException("The isReadOnly property is missing from the dictionary, the dictionary cannot be converted into a InterfaceUsage");
            }
            bool isReadOnlyFeature = Convert.ToBoolean(isReadOnlyObject);

            if (!dictionary.TryGetValue("isSufficient", out object isSufficientObject))
            {
                throw new ArgumentException("The isSufficient property is missing from the dictionary, the dictionary cannot be converted into a InterfaceUsage");
            }
            bool isSufficientFeature = Convert.ToBoolean(isSufficientObject);

            if (!dictionary.TryGetValue("isUnique", out object isUniqueObject))
            {
                throw new ArgumentException("The isUnique property is missing from the dictionary, the dictionary cannot be converted into a InterfaceUsage");
            }
            bool isUniqueFeature = Convert.ToBoolean(isUniqueObject);

            if (!dictionary.TryGetValue("isVariation", out object isVariationObject))
            {
                throw new ArgumentException("The isVariation property is missing from the dictionary, the dictionary cannot be converted into a InterfaceUsage");
            }
            bool isVariationFeature = Convert.ToBoolean(isVariationObject);

            if (!dictionary.TryGetValue("ownedRelatedElement", out object ownedRelatedElementObject))
            {
                throw new ArgumentException("The ownedRelatedElement property is missing from the dictionary, the dictionary cannot be converted into a InterfaceUsage");
            }
            List<Guid> ownedRelatedElementFeature = (ownedRelatedElementObject as List<string>)?.Select(Guid.Parse).ToList();

            if (!dictionary.TryGetValue("ownedRelationship", out object ownedRelationshipObject))
            {
                throw new ArgumentException("The ownedRelationship property is missing from the dictionary, the dictionary cannot be converted into a InterfaceUsage");
            }
            List<Guid> ownedRelationshipFeature = (ownedRelationshipObject as List<string>)?.Select(Guid.Parse).ToList();

            if (!dictionary.TryGetValue("owningRelatedElement", out object owningRelatedElementObject))
            {
                throw new ArgumentException("The owningRelatedElement property is missing from the dictionary, the dictionary cannot be converted into a InterfaceUsage");
            }
            Guid? owningRelatedElementFeature = owningRelatedElementObject == null ? (Guid?)null : Guid.Parse(Convert.ToString(owningRelatedElementObject));

            if (!dictionary.TryGetValue("owningRelationship", out object owningRelationshipObject))
            {
                throw new ArgumentException("The owningRelationship property is missing from the dictionary, the dictionary cannot be converted into a InterfaceUsage");
            }
            Guid? owningRelationshipFeature = owningRelationshipObject == null ? (Guid?)null : Guid.Parse(Convert.ToString(owningRelationshipObject));

            if (!dictionary.TryGetValue("portionKind", out object portionKindObject))
            {
                throw new ArgumentException("The portionKind property is missing from the dictionary, the dictionary cannot be converted into a InterfaceUsage");
            }
            PortionKind? portionKindFeature = portionKindObject == null ? null : (PortionKind?)Enum.Parse(typeof(PortionKind), Convert.ToString(portionKindObject), true);

            if (!dictionary.TryGetValue("source", out object sourceObject))
            {
                throw new ArgumentException("The source property is missing from the dictionary, the dictionary cannot be converted into a InterfaceUsage");
            }
            List<Guid> sourceFeature = (sourceObject as List<string>)?.Select(Guid.Parse).ToList();

            if (!dictionary.TryGetValue("target", out object targetObject))
            {
                throw new ArgumentException("The target property is missing from the dictionary, the dictionary cannot be converted into a InterfaceUsage");
            }
            List<Guid> targetFeature = (targetObject as List<string>)?.Select(Guid.Parse).ToList();


            interfaceUsageInstance.AliasIds = aliasIdsFeature ?? new List<string>();
            interfaceUsageInstance.DeclaredName = declaredNameFeature;
            interfaceUsageInstance.DeclaredShortName = declaredShortNameFeature;
            interfaceUsageInstance.Direction = directionFeature;
            interfaceUsageInstance.ElementId = elementIdFeature;
            interfaceUsageInstance.IsAbstract = isAbstractFeature;
            interfaceUsageInstance.IsComposite = isCompositeFeature;
            interfaceUsageInstance.IsDerived = isDerivedFeature;
            interfaceUsageInstance.IsDirected = isDirectedFeature;
            interfaceUsageInstance.IsEnd = isEndFeature;
            interfaceUsageInstance.IsImplied = isImpliedFeature;
            interfaceUsageInstance.IsImpliedIncluded = isImpliedIncludedFeature;
            interfaceUsageInstance.IsIndividual = isIndividualFeature;
            interfaceUsageInstance.IsOrdered = isOrderedFeature;
            interfaceUsageInstance.IsPortion = isPortionFeature;
            interfaceUsageInstance.IsReadOnly = isReadOnlyFeature;
            interfaceUsageInstance.IsSufficient = isSufficientFeature;
            interfaceUsageInstance.IsUnique = isUniqueFeature;
            interfaceUsageInstance.IsVariation = isVariationFeature;
            interfaceUsageInstance.OwnedRelatedElement = ownedRelatedElementFeature ?? new List<Guid>();
            interfaceUsageInstance.OwnedRelationship = ownedRelationshipFeature ?? new List<Guid>();
            interfaceUsageInstance.OwningRelatedElement = owningRelatedElementFeature;
            interfaceUsageInstance.OwningRelationship = owningRelationshipFeature;
            interfaceUsageInstance.PortionKind = portionKindFeature;
            interfaceUsageInstance.Source = sourceFeature ?? new List<Guid>();
            interfaceUsageInstance.Target = targetFeature ?? new List<Guid>();

            return interfaceUsageInstance;
        }

        /// <summary>
        /// Reads a <see cref="IInterfaceUsage"/> from a <see cref="Dictionary{String, Object}"/> that contains a key-value-pair
        /// for each property.
        /// </summary>
        /// <param name="dictionary">
        /// The subject <see cref="Dictionary{String, Object}"/> that is to be converted into a <see cref="IInterfaceUsage"/>
        /// </param>
        /// <returns>
        /// An instance of <see cref="IInterfaceUsage"/>
        /// </returns>
        private static IInterfaceUsage ReadComplex(Dictionary<string, object> dictionary)
        {
            var interfaceUsageInstance = DictionaryNullAndTypeCheck(dictionary);

            if (!dictionary.TryGetValue("aliasIds", out object aliasIdsObject))
            {
                throw new ArgumentException("The aliasIds property is missing from the dictionary, the dictionary cannot be converted into a InterfaceUsage");
            }
            List<string> aliasIdsFeature = aliasIdsObject as List<string>;

            if (!dictionary.TryGetValue("declaredName", out object declaredNameObject))
            {
                throw new ArgumentException("The declaredName property is missing from the dictionary, the dictionary cannot be converted into a InterfaceUsage");
            }
            string declaredNameFeature = declaredNameObject == null ? null : Convert.ToString(declaredNameObject);

            if (!dictionary.TryGetValue("declaredShortName", out object declaredShortNameObject))
            {
                throw new ArgumentException("The declaredShortName property is missing from the dictionary, the dictionary cannot be converted into a InterfaceUsage");
            }
            string declaredShortNameFeature = declaredShortNameObject == null ? null : Convert.ToString(declaredShortNameObject);

            if (!dictionary.TryGetValue("direction", out object directionObject))
            {
                throw new ArgumentException("The direction property is missing from the dictionary, the dictionary cannot be converted into a InterfaceUsage");
            }
            FeatureDirectionKind? directionFeature = (FeatureDirectionKind?)directionObject;

            if (!dictionary.TryGetValue("elementId", out object elementIdObject))
            {
                throw new ArgumentException("The elementId property is missing from the dictionary, the dictionary cannot be converted into a InterfaceUsage");
            }
            string elementIdFeature = Convert.ToString(elementIdObject);

            if (!dictionary.TryGetValue("isAbstract", out object isAbstractObject))
            {
                throw new ArgumentException("The isAbstract property is missing from the dictionary, the dictionary cannot be converted into a InterfaceUsage");
            }
            bool isAbstractFeature = Convert.ToBoolean(isAbstractObject);

            if (!dictionary.TryGetValue("isComposite", out object isCompositeObject))
            {
                throw new ArgumentException("The isComposite property is missing from the dictionary, the dictionary cannot be converted into a InterfaceUsage");
            }
            bool isCompositeFeature = Convert.ToBoolean(isCompositeObject);

            if (!dictionary.TryGetValue("isDerived", out object isDerivedObject))
            {
                throw new ArgumentException("The isDerived property is missing from the dictionary, the dictionary cannot be converted into a InterfaceUsage");
            }
            bool isDerivedFeature = Convert.ToBoolean(isDerivedObject);

            if (!dictionary.TryGetValue("isDirected", out object isDirectedObject))
            {
                throw new ArgumentException("The isDirected property is missing from the dictionary, the dictionary cannot be converted into a InterfaceUsage");
            }
            bool isDirectedFeature = Convert.ToBoolean(isDirectedObject);

            if (!dictionary.TryGetValue("isEnd", out object isEndObject))
            {
                throw new ArgumentException("The isEnd property is missing from the dictionary, the dictionary cannot be converted into a InterfaceUsage");
            }
            bool isEndFeature = Convert.ToBoolean(isEndObject);

            if (!dictionary.TryGetValue("isImplied", out object isImpliedObject))
            {
                throw new ArgumentException("The isImplied property is missing from the dictionary, the dictionary cannot be converted into a InterfaceUsage");
            }
            bool isImpliedFeature = Convert.ToBoolean(isImpliedObject);

            if (!dictionary.TryGetValue("isImpliedIncluded", out object isImpliedIncludedObject))
            {
                throw new ArgumentException("The isImpliedIncluded property is missing from the dictionary, the dictionary cannot be converted into a InterfaceUsage");
            }
            bool isImpliedIncludedFeature = Convert.ToBoolean(isImpliedIncludedObject);

            if (!dictionary.TryGetValue("isIndividual", out object isIndividualObject))
            {
                throw new ArgumentException("The isIndividual property is missing from the dictionary, the dictionary cannot be converted into a InterfaceUsage");
            }
            bool isIndividualFeature = Convert.ToBoolean(isIndividualObject);

            if (!dictionary.TryGetValue("isOrdered", out object isOrderedObject))
            {
                throw new ArgumentException("The isOrdered property is missing from the dictionary, the dictionary cannot be converted into a InterfaceUsage");
            }
            bool isOrderedFeature = Convert.ToBoolean(isOrderedObject);

            if (!dictionary.TryGetValue("isPortion", out object isPortionObject))
            {
                throw new ArgumentException("The isPortion property is missing from the dictionary, the dictionary cannot be converted into a InterfaceUsage");
            }
            bool isPortionFeature = Convert.ToBoolean(isPortionObject);

            if (!dictionary.TryGetValue("isReadOnly", out object isReadOnlyObject))
            {
                throw new ArgumentException("The isReadOnly property is missing from the dictionary, the dictionary cannot be converted into a InterfaceUsage");
            }
            bool isReadOnlyFeature = Convert.ToBoolean(isReadOnlyObject);

            if (!dictionary.TryGetValue("isSufficient", out object isSufficientObject))
            {
                throw new ArgumentException("The isSufficient property is missing from the dictionary, the dictionary cannot be converted into a InterfaceUsage");
            }
            bool isSufficientFeature = Convert.ToBoolean(isSufficientObject);

            if (!dictionary.TryGetValue("isUnique", out object isUniqueObject))
            {
                throw new ArgumentException("The isUnique property is missing from the dictionary, the dictionary cannot be converted into a InterfaceUsage");
            }
            bool isUniqueFeature = Convert.ToBoolean(isUniqueObject);

            if (!dictionary.TryGetValue("isVariation", out object isVariationObject))
            {
                throw new ArgumentException("The isVariation property is missing from the dictionary, the dictionary cannot be converted into a InterfaceUsage");
            }
            bool isVariationFeature = Convert.ToBoolean(isVariationObject);

            if (!dictionary.TryGetValue("ownedRelatedElement", out object ownedRelatedElementObject))
            {
                throw new ArgumentException("The ownedRelatedElement property is missing from the dictionary, the dictionary cannot be converted into a InterfaceUsage");
            }
            List<Guid> ownedRelatedElementFeature = (ownedRelatedElementObject as List<Guid>);

            if (!dictionary.TryGetValue("ownedRelationship", out object ownedRelationshipObject))
            {
                throw new ArgumentException("The ownedRelationship property is missing from the dictionary, the dictionary cannot be converted into a InterfaceUsage");
            }
            List<Guid> ownedRelationshipFeature = (ownedRelationshipObject as List<Guid>);

            if (!dictionary.TryGetValue("owningRelatedElement", out object owningRelatedElementObject))
            {
                throw new ArgumentException("The owningRelatedElement property is missing from the dictionary, the dictionary cannot be converted into a InterfaceUsage");
            }
            Guid? owningRelatedElementFeature = (Guid?)owningRelatedElementObject;

            if (!dictionary.TryGetValue("owningRelationship", out object owningRelationshipObject))
            {
                throw new ArgumentException("The owningRelationship property is missing from the dictionary, the dictionary cannot be converted into a InterfaceUsage");
            }
            Guid? owningRelationshipFeature = (Guid?)owningRelationshipObject;

            if (!dictionary.TryGetValue("portionKind", out object portionKindObject))
            {
                throw new ArgumentException("The portionKind property is missing from the dictionary, the dictionary cannot be converted into a InterfaceUsage");
            }
            PortionKind? portionKindFeature = (PortionKind?)portionKindObject;

            if (!dictionary.TryGetValue("source", out object sourceObject))
            {
                throw new ArgumentException("The source property is missing from the dictionary, the dictionary cannot be converted into a InterfaceUsage");
            }
            List<Guid> sourceFeature = (sourceObject as List<Guid>);

            if (!dictionary.TryGetValue("target", out object targetObject))
            {
                throw new ArgumentException("The target property is missing from the dictionary, the dictionary cannot be converted into a InterfaceUsage");
            }
            List<Guid> targetFeature = (targetObject as List<Guid>);


            interfaceUsageInstance.AliasIds = aliasIdsFeature ?? new List<string>();
            interfaceUsageInstance.DeclaredName = declaredNameFeature;
            interfaceUsageInstance.DeclaredShortName = declaredShortNameFeature;
            interfaceUsageInstance.Direction = directionFeature;
            interfaceUsageInstance.ElementId = elementIdFeature;
            interfaceUsageInstance.IsAbstract = isAbstractFeature;
            interfaceUsageInstance.IsComposite = isCompositeFeature;
            interfaceUsageInstance.IsDerived = isDerivedFeature;
            interfaceUsageInstance.IsDirected = isDirectedFeature;
            interfaceUsageInstance.IsEnd = isEndFeature;
            interfaceUsageInstance.IsImplied = isImpliedFeature;
            interfaceUsageInstance.IsImpliedIncluded = isImpliedIncludedFeature;
            interfaceUsageInstance.IsIndividual = isIndividualFeature;
            interfaceUsageInstance.IsOrdered = isOrderedFeature;
            interfaceUsageInstance.IsPortion = isPortionFeature;
            interfaceUsageInstance.IsReadOnly = isReadOnlyFeature;
            interfaceUsageInstance.IsSufficient = isSufficientFeature;
            interfaceUsageInstance.IsUnique = isUniqueFeature;
            interfaceUsageInstance.IsVariation = isVariationFeature;
            interfaceUsageInstance.OwnedRelatedElement = ownedRelatedElementFeature ?? new List<Guid>();
            interfaceUsageInstance.OwnedRelationship = ownedRelationshipFeature ?? new List<Guid>();
            interfaceUsageInstance.OwningRelatedElement = owningRelatedElementFeature;
            interfaceUsageInstance.OwningRelationship = owningRelationshipFeature;
            interfaceUsageInstance.PortionKind = portionKindFeature;
            interfaceUsageInstance.Source = sourceFeature ?? new List<Guid>();
            interfaceUsageInstance.Target = targetFeature ?? new List<Guid>();

            return interfaceUsageInstance;
        }

        /// <summary>
        /// Checks whether the <see cref="Dictionary{String, Object}"/> is not null and whether it is
        /// of type <see cref="InterfaceUsage"/>
        /// </summary>
        /// <param name="dictionary">
        /// The subject <see cref="Dictionary{String, Object}"/> that contains the key-value pairs of
        /// properties and values.
        /// </param>
        /// <returns>
        /// an instance of <see cref="IInterfaceUsage"/>
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="dictionary"/> is null
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown when <paramref name="dictionary"/> is not of type <see cref="InterfaceUsage"/>
        /// </exception>
        private static IInterfaceUsage DictionaryNullAndTypeCheck(Dictionary<string, object> dictionary)
        {
            if (dictionary == null)
            {
                throw new ArgumentNullException("dictionary", "The dictionary may not be null");
            }

            if (!dictionary.TryGetValue("@type", out object typeObject))
            {
                throw new ArgumentException("The type property is missing from the dictionary, the dictionary cannot be converted into a InterfaceUsage");
            }

            var type = Convert.ToString(typeObject);

            if (type != "InterfaceUsage")
            {
                throw new ArgumentException($"The dictionary contains an Object is of type {type} and can therefore not be converted into a InterfaceUsage");
            }

            if (!dictionary.TryGetValue("@id", out object idObject))
            {
                throw new ArgumentException("The id property is missing from the dictionary, the dictionary cannot be converted into a InterfaceUsage");
            }
            var id = Guid.Parse(Convert.ToString(idObject));

            var interfaceUsageInstance = new SysML2.NET.Core.DTO.InterfaceUsage
            {
                Id = id
            };

            return interfaceUsageInstance;
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
