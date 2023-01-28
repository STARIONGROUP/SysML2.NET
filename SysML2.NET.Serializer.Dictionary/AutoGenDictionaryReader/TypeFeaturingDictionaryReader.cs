// -------------------------------------------------------------------------------------------------
// <copyright file="TypeFeaturingDictionaryReader.cs" company="RHEA System S.A.">
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
    /// The purpose of the <see cref="TypeFeaturingDictionaryReader"/> is to read (convert)
    /// a <see cref="Dictionary{String, Object}"/> from an <see cref="ITypeFeaturing"/>
    /// </summary>
    public static class TypeFeaturingDictionaryReader
    {
        /// <summary>
        /// Reads a <see cref="ITypeFeaturing"/> from a <see cref="Dictionary{String, Object}"/> that contains a key-value-pair
        /// for each property.
        /// </summary>
        /// <param name="dictionary">
        /// The subject <see cref="Dictionary{String, Object}"/> that is to be converted into a <see cref="ITypeFeaturing"/>
        /// </param>
        /// <param name="dictionaryKind">
        /// The source <see cref="DictionaryKind"/> that is to be read from
        /// </param>
        /// <returns>
        /// An instance of <see cref="ITypeFeaturing"/>
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
        /// Reads a <see cref="ITypeFeaturing"/> from a <see cref="Dictionary{String, Object}"/> that contains a key-value-pair
        /// for each property.
        /// </summary>
        /// <param name="dictionary">
        /// The subject <see cref="Dictionary{String, Object}"/> that is to be converted into a <see cref="ITypeFeaturing"/>
        /// </param>
        /// <returns>
        /// An instance of <see cref="ITypeFeaturing"/>
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
        private static ITypeFeaturing ReadSimplified(Dictionary<string, object> dictionary)
        {
            var typeFeaturingInstance = DictionaryNullAndTypeCheck(dictionary);

            if (!dictionary.TryGetValue("aliasIds", out object aliasIdsObject))
            {
                throw new ArgumentException("The aliasIds property is missing from the dictionary, the dictionary cannot be converted into a TypeFeaturing");
            }
            List<string> aliasIdsFeature = aliasIdsObject as List<string>;

            if (!dictionary.TryGetValue("declaredName", out object declaredNameObject))
            {
                throw new ArgumentException("The declaredName property is missing from the dictionary, the dictionary cannot be converted into a TypeFeaturing");
            }
            string declaredNameFeature = declaredNameObject == null ? null : Convert.ToString(declaredNameObject);

            if (!dictionary.TryGetValue("declaredShortName", out object declaredShortNameObject))
            {
                throw new ArgumentException("The declaredShortName property is missing from the dictionary, the dictionary cannot be converted into a TypeFeaturing");
            }
            string declaredShortNameFeature = declaredShortNameObject == null ? null : Convert.ToString(declaredShortNameObject);

            if (!dictionary.TryGetValue("elementId", out object elementIdObject))
            {
                throw new ArgumentException("The elementId property is missing from the dictionary, the dictionary cannot be converted into a TypeFeaturing");
            }
            string elementIdFeature = Convert.ToString(elementIdObject);

            if (!dictionary.TryGetValue("feature", out object featureObject))
            {
                throw new ArgumentException("The feature property is missing from the dictionary, the dictionary cannot be converted into a TypeFeaturing");
            }
            Guid featureFeature = Guid.Parse(Convert.ToString(featureObject));

            if (!dictionary.TryGetValue("featureOfType", out object featureOfTypeObject))
            {
                throw new ArgumentException("The featureOfType property is missing from the dictionary, the dictionary cannot be converted into a TypeFeaturing");
            }
            Guid featureOfTypeFeature = Guid.Parse(Convert.ToString(featureOfTypeObject));

            if (!dictionary.TryGetValue("featuringType", out object featuringTypeObject))
            {
                throw new ArgumentException("The featuringType property is missing from the dictionary, the dictionary cannot be converted into a TypeFeaturing");
            }
            Guid featuringTypeFeature = Guid.Parse(Convert.ToString(featuringTypeObject));

            if (!dictionary.TryGetValue("isImplied", out object isImpliedObject))
            {
                throw new ArgumentException("The isImplied property is missing from the dictionary, the dictionary cannot be converted into a TypeFeaturing");
            }
            bool isImpliedFeature = Convert.ToBoolean(isImpliedObject);

            if (!dictionary.TryGetValue("isImpliedIncluded", out object isImpliedIncludedObject))
            {
                throw new ArgumentException("The isImpliedIncluded property is missing from the dictionary, the dictionary cannot be converted into a TypeFeaturing");
            }
            bool isImpliedIncludedFeature = Convert.ToBoolean(isImpliedIncludedObject);

            if (!dictionary.TryGetValue("ownedRelatedElement", out object ownedRelatedElementObject))
            {
                throw new ArgumentException("The ownedRelatedElement property is missing from the dictionary, the dictionary cannot be converted into a TypeFeaturing");
            }
            List<Guid> ownedRelatedElementFeature = (ownedRelatedElementObject as List<string>)?.Select(Guid.Parse).ToList();

            if (!dictionary.TryGetValue("ownedRelationship", out object ownedRelationshipObject))
            {
                throw new ArgumentException("The ownedRelationship property is missing from the dictionary, the dictionary cannot be converted into a TypeFeaturing");
            }
            List<Guid> ownedRelationshipFeature = (ownedRelationshipObject as List<string>)?.Select(Guid.Parse).ToList();

            if (!dictionary.TryGetValue("owningRelatedElement", out object owningRelatedElementObject))
            {
                throw new ArgumentException("The owningRelatedElement property is missing from the dictionary, the dictionary cannot be converted into a TypeFeaturing");
            }
            Guid? owningRelatedElementFeature = owningRelatedElementObject == null ? (Guid?)null : Guid.Parse(Convert.ToString(owningRelatedElementObject));

            if (!dictionary.TryGetValue("owningRelationship", out object owningRelationshipObject))
            {
                throw new ArgumentException("The owningRelationship property is missing from the dictionary, the dictionary cannot be converted into a TypeFeaturing");
            }
            Guid? owningRelationshipFeature = owningRelationshipObject == null ? (Guid?)null : Guid.Parse(Convert.ToString(owningRelationshipObject));

            if (!dictionary.TryGetValue("source", out object sourceObject))
            {
                throw new ArgumentException("The source property is missing from the dictionary, the dictionary cannot be converted into a TypeFeaturing");
            }
            List<Guid> sourceFeature = (sourceObject as List<string>)?.Select(Guid.Parse).ToList();

            if (!dictionary.TryGetValue("target", out object targetObject))
            {
                throw new ArgumentException("The target property is missing from the dictionary, the dictionary cannot be converted into a TypeFeaturing");
            }
            List<Guid> targetFeature = (targetObject as List<string>)?.Select(Guid.Parse).ToList();

            if (!dictionary.TryGetValue("type", out object typeObject))
            {
                throw new ArgumentException("The type property is missing from the dictionary, the dictionary cannot be converted into a TypeFeaturing");
            }
            Guid typeFeature = Guid.Parse(Convert.ToString(typeObject));


            typeFeaturingInstance.AliasIds = aliasIdsFeature ?? new List<string>();
            typeFeaturingInstance.DeclaredName = declaredNameFeature;
            typeFeaturingInstance.DeclaredShortName = declaredShortNameFeature;
            typeFeaturingInstance.ElementId = elementIdFeature;
            typeFeaturingInstance.Feature = featureFeature;
            typeFeaturingInstance.FeatureOfType = featureOfTypeFeature;
            typeFeaturingInstance.FeaturingType = featuringTypeFeature;
            typeFeaturingInstance.IsImplied = isImpliedFeature;
            typeFeaturingInstance.IsImpliedIncluded = isImpliedIncludedFeature;
            typeFeaturingInstance.OwnedRelatedElement = ownedRelatedElementFeature ?? new List<Guid>();
            typeFeaturingInstance.OwnedRelationship = ownedRelationshipFeature ?? new List<Guid>();
            typeFeaturingInstance.OwningRelatedElement = owningRelatedElementFeature;
            typeFeaturingInstance.OwningRelationship = owningRelationshipFeature;
            typeFeaturingInstance.Source = sourceFeature ?? new List<Guid>();
            typeFeaturingInstance.Target = targetFeature ?? new List<Guid>();
            typeFeaturingInstance.Type = typeFeature;

            return typeFeaturingInstance;
        }

        /// <summary>
        /// Reads a <see cref="ITypeFeaturing"/> from a <see cref="Dictionary{String, Object}"/> that contains a key-value-pair
        /// for each property.
        /// </summary>
        /// <param name="dictionary">
        /// The subject <see cref="Dictionary{String, Object}"/> that is to be converted into a <see cref="ITypeFeaturing"/>
        /// </param>
        /// <returns>
        /// An instance of <see cref="ITypeFeaturing"/>
        /// </returns>
        private static ITypeFeaturing ReadComplex(Dictionary<string, object> dictionary)
        {
            var typeFeaturingInstance = DictionaryNullAndTypeCheck(dictionary);

            if (!dictionary.TryGetValue("aliasIds", out object aliasIdsObject))
            {
                throw new ArgumentException("The aliasIds property is missing from the dictionary, the dictionary cannot be converted into a TypeFeaturing");
            }
            List<string> aliasIdsFeature = aliasIdsObject as List<string>;

            if (!dictionary.TryGetValue("declaredName", out object declaredNameObject))
            {
                throw new ArgumentException("The declaredName property is missing from the dictionary, the dictionary cannot be converted into a TypeFeaturing");
            }
            string declaredNameFeature = declaredNameObject == null ? null : Convert.ToString(declaredNameObject);

            if (!dictionary.TryGetValue("declaredShortName", out object declaredShortNameObject))
            {
                throw new ArgumentException("The declaredShortName property is missing from the dictionary, the dictionary cannot be converted into a TypeFeaturing");
            }
            string declaredShortNameFeature = declaredShortNameObject == null ? null : Convert.ToString(declaredShortNameObject);

            if (!dictionary.TryGetValue("elementId", out object elementIdObject))
            {
                throw new ArgumentException("The elementId property is missing from the dictionary, the dictionary cannot be converted into a TypeFeaturing");
            }
            string elementIdFeature = Convert.ToString(elementIdObject);

            if (!dictionary.TryGetValue("feature", out object featureObject))
            {
                throw new ArgumentException("The feature property is missing from the dictionary, the dictionary cannot be converted into a TypeFeaturing");
            }
            Guid featureFeature = Guid.Parse(Convert.ToString(featureObject));

            if (!dictionary.TryGetValue("featureOfType", out object featureOfTypeObject))
            {
                throw new ArgumentException("The featureOfType property is missing from the dictionary, the dictionary cannot be converted into a TypeFeaturing");
            }
            Guid featureOfTypeFeature = Guid.Parse(Convert.ToString(featureOfTypeObject));

            if (!dictionary.TryGetValue("featuringType", out object featuringTypeObject))
            {
                throw new ArgumentException("The featuringType property is missing from the dictionary, the dictionary cannot be converted into a TypeFeaturing");
            }
            Guid featuringTypeFeature = Guid.Parse(Convert.ToString(featuringTypeObject));

            if (!dictionary.TryGetValue("isImplied", out object isImpliedObject))
            {
                throw new ArgumentException("The isImplied property is missing from the dictionary, the dictionary cannot be converted into a TypeFeaturing");
            }
            bool isImpliedFeature = Convert.ToBoolean(isImpliedObject);

            if (!dictionary.TryGetValue("isImpliedIncluded", out object isImpliedIncludedObject))
            {
                throw new ArgumentException("The isImpliedIncluded property is missing from the dictionary, the dictionary cannot be converted into a TypeFeaturing");
            }
            bool isImpliedIncludedFeature = Convert.ToBoolean(isImpliedIncludedObject);

            if (!dictionary.TryGetValue("ownedRelatedElement", out object ownedRelatedElementObject))
            {
                throw new ArgumentException("The ownedRelatedElement property is missing from the dictionary, the dictionary cannot be converted into a TypeFeaturing");
            }
            List<Guid> ownedRelatedElementFeature = (ownedRelatedElementObject as List<Guid>);

            if (!dictionary.TryGetValue("ownedRelationship", out object ownedRelationshipObject))
            {
                throw new ArgumentException("The ownedRelationship property is missing from the dictionary, the dictionary cannot be converted into a TypeFeaturing");
            }
            List<Guid> ownedRelationshipFeature = (ownedRelationshipObject as List<Guid>);

            if (!dictionary.TryGetValue("owningRelatedElement", out object owningRelatedElementObject))
            {
                throw new ArgumentException("The owningRelatedElement property is missing from the dictionary, the dictionary cannot be converted into a TypeFeaturing");
            }
            Guid? owningRelatedElementFeature = (Guid?)owningRelatedElementObject;

            if (!dictionary.TryGetValue("owningRelationship", out object owningRelationshipObject))
            {
                throw new ArgumentException("The owningRelationship property is missing from the dictionary, the dictionary cannot be converted into a TypeFeaturing");
            }
            Guid? owningRelationshipFeature = (Guid?)owningRelationshipObject;

            if (!dictionary.TryGetValue("source", out object sourceObject))
            {
                throw new ArgumentException("The source property is missing from the dictionary, the dictionary cannot be converted into a TypeFeaturing");
            }
            List<Guid> sourceFeature = (sourceObject as List<Guid>);

            if (!dictionary.TryGetValue("target", out object targetObject))
            {
                throw new ArgumentException("The target property is missing from the dictionary, the dictionary cannot be converted into a TypeFeaturing");
            }
            List<Guid> targetFeature = (targetObject as List<Guid>);

            if (!dictionary.TryGetValue("type", out object typeObject))
            {
                throw new ArgumentException("The type property is missing from the dictionary, the dictionary cannot be converted into a TypeFeaturing");
            }
            Guid typeFeature = Guid.Parse(Convert.ToString(typeObject));


            typeFeaturingInstance.AliasIds = aliasIdsFeature ?? new List<string>();
            typeFeaturingInstance.DeclaredName = declaredNameFeature;
            typeFeaturingInstance.DeclaredShortName = declaredShortNameFeature;
            typeFeaturingInstance.ElementId = elementIdFeature;
            typeFeaturingInstance.Feature = featureFeature;
            typeFeaturingInstance.FeatureOfType = featureOfTypeFeature;
            typeFeaturingInstance.FeaturingType = featuringTypeFeature;
            typeFeaturingInstance.IsImplied = isImpliedFeature;
            typeFeaturingInstance.IsImpliedIncluded = isImpliedIncludedFeature;
            typeFeaturingInstance.OwnedRelatedElement = ownedRelatedElementFeature ?? new List<Guid>();
            typeFeaturingInstance.OwnedRelationship = ownedRelationshipFeature ?? new List<Guid>();
            typeFeaturingInstance.OwningRelatedElement = owningRelatedElementFeature;
            typeFeaturingInstance.OwningRelationship = owningRelationshipFeature;
            typeFeaturingInstance.Source = sourceFeature ?? new List<Guid>();
            typeFeaturingInstance.Target = targetFeature ?? new List<Guid>();
            typeFeaturingInstance.Type = typeFeature;

            return typeFeaturingInstance;
        }

        /// <summary>
        /// Checks whether the <see cref="Dictionary{String, Object}"/> is not null and whether it is
        /// of type <see cref="TypeFeaturing"/>
        /// </summary>
        /// <param name="dictionary">
        /// The subject <see cref="Dictionary{String, Object}"/> that contains the key-value pairs of
        /// properties and values.
        /// </param>
        /// <returns>
        /// an instance of <see cref="ITypeFeaturing"/>
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="dictionary"/> is null
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown when <paramref name="dictionary"/> is not of type <see cref="TypeFeaturing"/>
        /// </exception>
        private static ITypeFeaturing DictionaryNullAndTypeCheck(Dictionary<string, object> dictionary)
        {
            if (dictionary == null)
            {
                throw new ArgumentNullException("dictionary", "The dictionary may not be null");
            }

            if (!dictionary.TryGetValue("@type", out object typeObject))
            {
                throw new ArgumentException("The type property is missing from the dictionary, the dictionary cannot be converted into a TypeFeaturing");
            }

            var type = Convert.ToString(typeObject);

            if (type != "TypeFeaturing")
            {
                throw new ArgumentException($"The dictionary contains an Object is of type {type} and can therefore not be converted into a TypeFeaturing");
            }

            if (!dictionary.TryGetValue("@id", out object idObject))
            {
                throw new ArgumentException("The id property is missing from the dictionary, the dictionary cannot be converted into a TypeFeaturing");
            }
            var id = Guid.Parse(Convert.ToString(idObject));

            var typeFeaturingInstance = new Core.DTO.TypeFeaturing
            {
                Id = id
            };

            return typeFeaturingInstance;
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
