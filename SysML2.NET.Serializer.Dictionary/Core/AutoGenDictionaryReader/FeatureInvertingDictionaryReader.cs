// -------------------------------------------------------------------------------------------------
// <copyright file="FeatureInvertingDictionaryReader.cs" company="Starion Group S.A.">
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
    /// The purpose of the <see cref="FeatureInvertingDictionaryReader"/> is to read (convert)
    /// a <see cref="Dictionary{String, Object}"/> from an <see cref="IFeatureInverting"/>
    /// </summary>
    public static class FeatureInvertingDictionaryReader
    {
        /// <summary>
        /// Reads a <see cref="IFeatureInverting"/> from a <see cref="Dictionary{String, Object}"/> that contains a key-value-pair
        /// for each property.
        /// </summary>
        /// <param name="dictionary">
        /// The subject <see cref="Dictionary{String, Object}"/> that is to be converted into a <see cref="IFeatureInverting"/>
        /// </param>
        /// <param name="dictionaryKind">
        /// The source <see cref="DictionaryKind"/> that is to be read from
        /// </param>
        /// <returns>
        /// An instance of <see cref="IFeatureInverting"/>
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
        /// Reads a <see cref="IFeatureInverting"/> from a <see cref="Dictionary{String, Object}"/> that contains a key-value-pair
        /// for each property.
        /// </summary>
        /// <param name="dictionary">
        /// The subject <see cref="Dictionary{String, Object}"/> that is to be converted into a <see cref="IFeatureInverting"/>
        /// </param>
        /// <returns>
        /// An instance of <see cref="IFeatureInverting"/>
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
        private static IFeatureInverting ReadSimplified(Dictionary<string, object> dictionary)
        {
            var featureInvertingInstance = DictionaryNullAndTypeCheck(dictionary);

            if (!dictionary.TryGetValue("aliasIds", out object aliasIdsObject))
            {
                throw new ArgumentException("The aliasIds property is missing from the dictionary, the dictionary cannot be converted into a FeatureInverting");
            }
            List<string> aliasIdsFeature = aliasIdsObject as List<string>;

            if (!dictionary.TryGetValue("declaredName", out object declaredNameObject))
            {
                throw new ArgumentException("The declaredName property is missing from the dictionary, the dictionary cannot be converted into a FeatureInverting");
            }
            string declaredNameFeature = declaredNameObject == null ? null : Convert.ToString(declaredNameObject);

            if (!dictionary.TryGetValue("declaredShortName", out object declaredShortNameObject))
            {
                throw new ArgumentException("The declaredShortName property is missing from the dictionary, the dictionary cannot be converted into a FeatureInverting");
            }
            string declaredShortNameFeature = declaredShortNameObject == null ? null : Convert.ToString(declaredShortNameObject);

            if (!dictionary.TryGetValue("elementId", out object elementIdObject))
            {
                throw new ArgumentException("The elementId property is missing from the dictionary, the dictionary cannot be converted into a FeatureInverting");
            }
            string elementIdFeature = Convert.ToString(elementIdObject);

            if (!dictionary.TryGetValue("featureInverted", out object featureInvertedObject))
            {
                throw new ArgumentException("The featureInverted property is missing from the dictionary, the dictionary cannot be converted into a FeatureInverting");
            }
            Guid featureInvertedFeature = Guid.Parse(Convert.ToString(featureInvertedObject));

            if (!dictionary.TryGetValue("invertingFeature", out object invertingFeatureObject))
            {
                throw new ArgumentException("The invertingFeature property is missing from the dictionary, the dictionary cannot be converted into a FeatureInverting");
            }
            Guid invertingFeatureFeature = Guid.Parse(Convert.ToString(invertingFeatureObject));

            if (!dictionary.TryGetValue("isImplied", out object isImpliedObject))
            {
                throw new ArgumentException("The isImplied property is missing from the dictionary, the dictionary cannot be converted into a FeatureInverting");
            }
            bool isImpliedFeature = Convert.ToBoolean(isImpliedObject);

            if (!dictionary.TryGetValue("isImpliedIncluded", out object isImpliedIncludedObject))
            {
                throw new ArgumentException("The isImpliedIncluded property is missing from the dictionary, the dictionary cannot be converted into a FeatureInverting");
            }
            bool isImpliedIncludedFeature = Convert.ToBoolean(isImpliedIncludedObject);

            if (!dictionary.TryGetValue("ownedRelatedElement", out object ownedRelatedElementObject))
            {
                throw new ArgumentException("The ownedRelatedElement property is missing from the dictionary, the dictionary cannot be converted into a FeatureInverting");
            }
            List<Guid> ownedRelatedElementFeature = (ownedRelatedElementObject as List<string>)?.Select(Guid.Parse).ToList();

            if (!dictionary.TryGetValue("ownedRelationship", out object ownedRelationshipObject))
            {
                throw new ArgumentException("The ownedRelationship property is missing from the dictionary, the dictionary cannot be converted into a FeatureInverting");
            }
            List<Guid> ownedRelationshipFeature = (ownedRelationshipObject as List<string>)?.Select(Guid.Parse).ToList();

            if (!dictionary.TryGetValue("owningRelatedElement", out object owningRelatedElementObject))
            {
                throw new ArgumentException("The owningRelatedElement property is missing from the dictionary, the dictionary cannot be converted into a FeatureInverting");
            }
            Guid? owningRelatedElementFeature = owningRelatedElementObject == null ? (Guid?)null : Guid.Parse(Convert.ToString(owningRelatedElementObject));

            if (!dictionary.TryGetValue("owningRelationship", out object owningRelationshipObject))
            {
                throw new ArgumentException("The owningRelationship property is missing from the dictionary, the dictionary cannot be converted into a FeatureInverting");
            }
            Guid? owningRelationshipFeature = owningRelationshipObject == null ? (Guid?)null : Guid.Parse(Convert.ToString(owningRelationshipObject));

            if (!dictionary.TryGetValue("source", out object sourceObject))
            {
                throw new ArgumentException("The source property is missing from the dictionary, the dictionary cannot be converted into a FeatureInverting");
            }
            List<Guid> sourceFeature = (sourceObject as List<string>)?.Select(Guid.Parse).ToList();

            if (!dictionary.TryGetValue("target", out object targetObject))
            {
                throw new ArgumentException("The target property is missing from the dictionary, the dictionary cannot be converted into a FeatureInverting");
            }
            List<Guid> targetFeature = (targetObject as List<string>)?.Select(Guid.Parse).ToList();


            featureInvertingInstance.AliasIds = aliasIdsFeature ?? new List<string>();
            featureInvertingInstance.DeclaredName = declaredNameFeature;
            featureInvertingInstance.DeclaredShortName = declaredShortNameFeature;
            featureInvertingInstance.ElementId = elementIdFeature;
            featureInvertingInstance.FeatureInverted = featureInvertedFeature;
            featureInvertingInstance.InvertingFeature = invertingFeatureFeature;
            featureInvertingInstance.IsImplied = isImpliedFeature;
            featureInvertingInstance.IsImpliedIncluded = isImpliedIncludedFeature;
            featureInvertingInstance.OwnedRelatedElement = ownedRelatedElementFeature ?? new List<Guid>();
            featureInvertingInstance.OwnedRelationship = ownedRelationshipFeature ?? new List<Guid>();
            featureInvertingInstance.OwningRelatedElement = owningRelatedElementFeature;
            featureInvertingInstance.OwningRelationship = owningRelationshipFeature;
            featureInvertingInstance.Source = sourceFeature ?? new List<Guid>();
            featureInvertingInstance.Target = targetFeature ?? new List<Guid>();

            return featureInvertingInstance;
        }

        /// <summary>
        /// Reads a <see cref="IFeatureInverting"/> from a <see cref="Dictionary{String, Object}"/> that contains a key-value-pair
        /// for each property.
        /// </summary>
        /// <param name="dictionary">
        /// The subject <see cref="Dictionary{String, Object}"/> that is to be converted into a <see cref="IFeatureInverting"/>
        /// </param>
        /// <returns>
        /// An instance of <see cref="IFeatureInverting"/>
        /// </returns>
        private static IFeatureInverting ReadComplex(Dictionary<string, object> dictionary)
        {
            var featureInvertingInstance = DictionaryNullAndTypeCheck(dictionary);

            if (!dictionary.TryGetValue("aliasIds", out object aliasIdsObject))
            {
                throw new ArgumentException("The aliasIds property is missing from the dictionary, the dictionary cannot be converted into a FeatureInverting");
            }
            List<string> aliasIdsFeature = aliasIdsObject as List<string>;

            if (!dictionary.TryGetValue("declaredName", out object declaredNameObject))
            {
                throw new ArgumentException("The declaredName property is missing from the dictionary, the dictionary cannot be converted into a FeatureInverting");
            }
            string declaredNameFeature = declaredNameObject == null ? null : Convert.ToString(declaredNameObject);

            if (!dictionary.TryGetValue("declaredShortName", out object declaredShortNameObject))
            {
                throw new ArgumentException("The declaredShortName property is missing from the dictionary, the dictionary cannot be converted into a FeatureInverting");
            }
            string declaredShortNameFeature = declaredShortNameObject == null ? null : Convert.ToString(declaredShortNameObject);

            if (!dictionary.TryGetValue("elementId", out object elementIdObject))
            {
                throw new ArgumentException("The elementId property is missing from the dictionary, the dictionary cannot be converted into a FeatureInverting");
            }
            string elementIdFeature = Convert.ToString(elementIdObject);

            if (!dictionary.TryGetValue("featureInverted", out object featureInvertedObject))
            {
                throw new ArgumentException("The featureInverted property is missing from the dictionary, the dictionary cannot be converted into a FeatureInverting");
            }
            Guid featureInvertedFeature = Guid.Parse(Convert.ToString(featureInvertedObject));

            if (!dictionary.TryGetValue("invertingFeature", out object invertingFeatureObject))
            {
                throw new ArgumentException("The invertingFeature property is missing from the dictionary, the dictionary cannot be converted into a FeatureInverting");
            }
            Guid invertingFeatureFeature = Guid.Parse(Convert.ToString(invertingFeatureObject));

            if (!dictionary.TryGetValue("isImplied", out object isImpliedObject))
            {
                throw new ArgumentException("The isImplied property is missing from the dictionary, the dictionary cannot be converted into a FeatureInverting");
            }
            bool isImpliedFeature = Convert.ToBoolean(isImpliedObject);

            if (!dictionary.TryGetValue("isImpliedIncluded", out object isImpliedIncludedObject))
            {
                throw new ArgumentException("The isImpliedIncluded property is missing from the dictionary, the dictionary cannot be converted into a FeatureInverting");
            }
            bool isImpliedIncludedFeature = Convert.ToBoolean(isImpliedIncludedObject);

            if (!dictionary.TryGetValue("ownedRelatedElement", out object ownedRelatedElementObject))
            {
                throw new ArgumentException("The ownedRelatedElement property is missing from the dictionary, the dictionary cannot be converted into a FeatureInverting");
            }
            List<Guid> ownedRelatedElementFeature = (ownedRelatedElementObject as List<Guid>);

            if (!dictionary.TryGetValue("ownedRelationship", out object ownedRelationshipObject))
            {
                throw new ArgumentException("The ownedRelationship property is missing from the dictionary, the dictionary cannot be converted into a FeatureInverting");
            }
            List<Guid> ownedRelationshipFeature = (ownedRelationshipObject as List<Guid>);

            if (!dictionary.TryGetValue("owningRelatedElement", out object owningRelatedElementObject))
            {
                throw new ArgumentException("The owningRelatedElement property is missing from the dictionary, the dictionary cannot be converted into a FeatureInverting");
            }
            Guid? owningRelatedElementFeature = (Guid?)owningRelatedElementObject;

            if (!dictionary.TryGetValue("owningRelationship", out object owningRelationshipObject))
            {
                throw new ArgumentException("The owningRelationship property is missing from the dictionary, the dictionary cannot be converted into a FeatureInverting");
            }
            Guid? owningRelationshipFeature = (Guid?)owningRelationshipObject;

            if (!dictionary.TryGetValue("source", out object sourceObject))
            {
                throw new ArgumentException("The source property is missing from the dictionary, the dictionary cannot be converted into a FeatureInverting");
            }
            List<Guid> sourceFeature = (sourceObject as List<Guid>);

            if (!dictionary.TryGetValue("target", out object targetObject))
            {
                throw new ArgumentException("The target property is missing from the dictionary, the dictionary cannot be converted into a FeatureInverting");
            }
            List<Guid> targetFeature = (targetObject as List<Guid>);


            featureInvertingInstance.AliasIds = aliasIdsFeature ?? new List<string>();
            featureInvertingInstance.DeclaredName = declaredNameFeature;
            featureInvertingInstance.DeclaredShortName = declaredShortNameFeature;
            featureInvertingInstance.ElementId = elementIdFeature;
            featureInvertingInstance.FeatureInverted = featureInvertedFeature;
            featureInvertingInstance.InvertingFeature = invertingFeatureFeature;
            featureInvertingInstance.IsImplied = isImpliedFeature;
            featureInvertingInstance.IsImpliedIncluded = isImpliedIncludedFeature;
            featureInvertingInstance.OwnedRelatedElement = ownedRelatedElementFeature ?? new List<Guid>();
            featureInvertingInstance.OwnedRelationship = ownedRelationshipFeature ?? new List<Guid>();
            featureInvertingInstance.OwningRelatedElement = owningRelatedElementFeature;
            featureInvertingInstance.OwningRelationship = owningRelationshipFeature;
            featureInvertingInstance.Source = sourceFeature ?? new List<Guid>();
            featureInvertingInstance.Target = targetFeature ?? new List<Guid>();

            return featureInvertingInstance;
        }

        /// <summary>
        /// Checks whether the <see cref="Dictionary{String, Object}"/> is not null and whether it is
        /// of type <see cref="FeatureInverting"/>
        /// </summary>
        /// <param name="dictionary">
        /// The subject <see cref="Dictionary{String, Object}"/> that contains the key-value pairs of
        /// properties and values.
        /// </param>
        /// <returns>
        /// an instance of <see cref="IFeatureInverting"/>
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="dictionary"/> is null
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown when <paramref name="dictionary"/> is not of type <see cref="FeatureInverting"/>
        /// </exception>
        private static IFeatureInverting DictionaryNullAndTypeCheck(Dictionary<string, object> dictionary)
        {
            if (dictionary == null)
            {
                throw new ArgumentNullException("dictionary", "The dictionary may not be null");
            }

            if (!dictionary.TryGetValue("@type", out object typeObject))
            {
                throw new ArgumentException("The type property is missing from the dictionary, the dictionary cannot be converted into a FeatureInverting");
            }

            var type = Convert.ToString(typeObject);

            if (type != "FeatureInverting")
            {
                throw new ArgumentException($"The dictionary contains an Object is of type {type} and can therefore not be converted into a FeatureInverting");
            }

            if (!dictionary.TryGetValue("@id", out object idObject))
            {
                throw new ArgumentException("The id property is missing from the dictionary, the dictionary cannot be converted into a FeatureInverting");
            }
            var id = Guid.Parse(Convert.ToString(idObject));

            var featureInvertingInstance = new SysML2.NET.Core.DTO.FeatureInverting
            {
                Id = id
            };

            return featureInvertingInstance;
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
