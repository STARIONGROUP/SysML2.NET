// -------------------------------------------------------------------------------------------------
// <copyright file="RedefinitionDictionaryReader.cs" company="Starion Group S.A.">
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
    /// The purpose of the <see cref="RedefinitionDictionaryReader"/> is to read (convert)
    /// a <see cref="Dictionary{String, Object}"/> from an <see cref="IRedefinition"/>
    /// </summary>
    public static class RedefinitionDictionaryReader
    {
        /// <summary>
        /// Reads a <see cref="IRedefinition"/> from a <see cref="Dictionary{String, Object}"/> that contains a key-value-pair
        /// for each property.
        /// </summary>
        /// <param name="dictionary">
        /// The subject <see cref="Dictionary{String, Object}"/> that is to be converted into a <see cref="IRedefinition"/>
        /// </param>
        /// <param name="dictionaryKind">
        /// The source <see cref="DictionaryKind"/> that is to be read from
        /// </param>
        /// <returns>
        /// An instance of <see cref="IRedefinition"/>
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
        /// Reads a <see cref="IRedefinition"/> from a <see cref="Dictionary{String, Object}"/> that contains a key-value-pair
        /// for each property.
        /// </summary>
        /// <param name="dictionary">
        /// The subject <see cref="Dictionary{String, Object}"/> that is to be converted into a <see cref="IRedefinition"/>
        /// </param>
        /// <returns>
        /// An instance of <see cref="IRedefinition"/>
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
        private static IRedefinition ReadSimplified(Dictionary<string, object> dictionary)
        {
            var redefinitionInstance = DictionaryNullAndTypeCheck(dictionary);

            if (!dictionary.TryGetValue("aliasIds", out object aliasIdsObject))
            {
                throw new ArgumentException("The aliasIds property is missing from the dictionary, the dictionary cannot be converted into a Redefinition");
            }
            List<string> aliasIdsFeature = aliasIdsObject as List<string>;

            if (!dictionary.TryGetValue("declaredName", out object declaredNameObject))
            {
                throw new ArgumentException("The declaredName property is missing from the dictionary, the dictionary cannot be converted into a Redefinition");
            }
            string declaredNameFeature = declaredNameObject == null ? null : Convert.ToString(declaredNameObject);

            if (!dictionary.TryGetValue("declaredShortName", out object declaredShortNameObject))
            {
                throw new ArgumentException("The declaredShortName property is missing from the dictionary, the dictionary cannot be converted into a Redefinition");
            }
            string declaredShortNameFeature = declaredShortNameObject == null ? null : Convert.ToString(declaredShortNameObject);

            if (!dictionary.TryGetValue("elementId", out object elementIdObject))
            {
                throw new ArgumentException("The elementId property is missing from the dictionary, the dictionary cannot be converted into a Redefinition");
            }
            string elementIdFeature = Convert.ToString(elementIdObject);

            if (!dictionary.TryGetValue("general", out object generalObject))
            {
                throw new ArgumentException("The general property is missing from the dictionary, the dictionary cannot be converted into a Redefinition");
            }
            Guid generalFeature = Guid.Parse(Convert.ToString(generalObject));

            if (!dictionary.TryGetValue("isImplied", out object isImpliedObject))
            {
                throw new ArgumentException("The isImplied property is missing from the dictionary, the dictionary cannot be converted into a Redefinition");
            }
            bool isImpliedFeature = Convert.ToBoolean(isImpliedObject);

            if (!dictionary.TryGetValue("isImpliedIncluded", out object isImpliedIncludedObject))
            {
                throw new ArgumentException("The isImpliedIncluded property is missing from the dictionary, the dictionary cannot be converted into a Redefinition");
            }
            bool isImpliedIncludedFeature = Convert.ToBoolean(isImpliedIncludedObject);

            if (!dictionary.TryGetValue("ownedRelatedElement", out object ownedRelatedElementObject))
            {
                throw new ArgumentException("The ownedRelatedElement property is missing from the dictionary, the dictionary cannot be converted into a Redefinition");
            }
            List<Guid> ownedRelatedElementFeature = (ownedRelatedElementObject as List<string>)?.Select(Guid.Parse).ToList();

            if (!dictionary.TryGetValue("ownedRelationship", out object ownedRelationshipObject))
            {
                throw new ArgumentException("The ownedRelationship property is missing from the dictionary, the dictionary cannot be converted into a Redefinition");
            }
            List<Guid> ownedRelationshipFeature = (ownedRelationshipObject as List<string>)?.Select(Guid.Parse).ToList();

            if (!dictionary.TryGetValue("owningRelatedElement", out object owningRelatedElementObject))
            {
                throw new ArgumentException("The owningRelatedElement property is missing from the dictionary, the dictionary cannot be converted into a Redefinition");
            }
            Guid? owningRelatedElementFeature = owningRelatedElementObject == null ? (Guid?)null : Guid.Parse(Convert.ToString(owningRelatedElementObject));

            if (!dictionary.TryGetValue("owningRelationship", out object owningRelationshipObject))
            {
                throw new ArgumentException("The owningRelationship property is missing from the dictionary, the dictionary cannot be converted into a Redefinition");
            }
            Guid? owningRelationshipFeature = owningRelationshipObject == null ? (Guid?)null : Guid.Parse(Convert.ToString(owningRelationshipObject));

            if (!dictionary.TryGetValue("redefinedFeature", out object redefinedFeatureObject))
            {
                throw new ArgumentException("The redefinedFeature property is missing from the dictionary, the dictionary cannot be converted into a Redefinition");
            }
            Guid redefinedFeatureFeature = Guid.Parse(Convert.ToString(redefinedFeatureObject));

            if (!dictionary.TryGetValue("redefiningFeature", out object redefiningFeatureObject))
            {
                throw new ArgumentException("The redefiningFeature property is missing from the dictionary, the dictionary cannot be converted into a Redefinition");
            }
            Guid redefiningFeatureFeature = Guid.Parse(Convert.ToString(redefiningFeatureObject));

            if (!dictionary.TryGetValue("source", out object sourceObject))
            {
                throw new ArgumentException("The source property is missing from the dictionary, the dictionary cannot be converted into a Redefinition");
            }
            List<Guid> sourceFeature = (sourceObject as List<string>)?.Select(Guid.Parse).ToList();

            if (!dictionary.TryGetValue("specific", out object specificObject))
            {
                throw new ArgumentException("The specific property is missing from the dictionary, the dictionary cannot be converted into a Redefinition");
            }
            Guid specificFeature = Guid.Parse(Convert.ToString(specificObject));

            if (!dictionary.TryGetValue("subsettedFeature", out object subsettedFeatureObject))
            {
                throw new ArgumentException("The subsettedFeature property is missing from the dictionary, the dictionary cannot be converted into a Redefinition");
            }
            Guid subsettedFeatureFeature = Guid.Parse(Convert.ToString(subsettedFeatureObject));

            if (!dictionary.TryGetValue("subsettingFeature", out object subsettingFeatureObject))
            {
                throw new ArgumentException("The subsettingFeature property is missing from the dictionary, the dictionary cannot be converted into a Redefinition");
            }
            Guid subsettingFeatureFeature = Guid.Parse(Convert.ToString(subsettingFeatureObject));

            if (!dictionary.TryGetValue("target", out object targetObject))
            {
                throw new ArgumentException("The target property is missing from the dictionary, the dictionary cannot be converted into a Redefinition");
            }
            List<Guid> targetFeature = (targetObject as List<string>)?.Select(Guid.Parse).ToList();


            redefinitionInstance.AliasIds = aliasIdsFeature ?? new List<string>();
            redefinitionInstance.DeclaredName = declaredNameFeature;
            redefinitionInstance.DeclaredShortName = declaredShortNameFeature;
            redefinitionInstance.ElementId = elementIdFeature;
            redefinitionInstance.General = generalFeature;
            redefinitionInstance.IsImplied = isImpliedFeature;
            redefinitionInstance.IsImpliedIncluded = isImpliedIncludedFeature;
            redefinitionInstance.OwnedRelatedElement = ownedRelatedElementFeature ?? new List<Guid>();
            redefinitionInstance.OwnedRelationship = ownedRelationshipFeature ?? new List<Guid>();
            redefinitionInstance.OwningRelatedElement = owningRelatedElementFeature;
            redefinitionInstance.OwningRelationship = owningRelationshipFeature;
            redefinitionInstance.RedefinedFeature = redefinedFeatureFeature;
            redefinitionInstance.RedefiningFeature = redefiningFeatureFeature;
            redefinitionInstance.Source = sourceFeature ?? new List<Guid>();
            redefinitionInstance.Specific = specificFeature;
            redefinitionInstance.SubsettedFeature = subsettedFeatureFeature;
            redefinitionInstance.SubsettingFeature = subsettingFeatureFeature;
            redefinitionInstance.Target = targetFeature ?? new List<Guid>();

            return redefinitionInstance;
        }

        /// <summary>
        /// Reads a <see cref="IRedefinition"/> from a <see cref="Dictionary{String, Object}"/> that contains a key-value-pair
        /// for each property.
        /// </summary>
        /// <param name="dictionary">
        /// The subject <see cref="Dictionary{String, Object}"/> that is to be converted into a <see cref="IRedefinition"/>
        /// </param>
        /// <returns>
        /// An instance of <see cref="IRedefinition"/>
        /// </returns>
        private static IRedefinition ReadComplex(Dictionary<string, object> dictionary)
        {
            var redefinitionInstance = DictionaryNullAndTypeCheck(dictionary);

            if (!dictionary.TryGetValue("aliasIds", out object aliasIdsObject))
            {
                throw new ArgumentException("The aliasIds property is missing from the dictionary, the dictionary cannot be converted into a Redefinition");
            }
            List<string> aliasIdsFeature = aliasIdsObject as List<string>;

            if (!dictionary.TryGetValue("declaredName", out object declaredNameObject))
            {
                throw new ArgumentException("The declaredName property is missing from the dictionary, the dictionary cannot be converted into a Redefinition");
            }
            string declaredNameFeature = declaredNameObject == null ? null : Convert.ToString(declaredNameObject);

            if (!dictionary.TryGetValue("declaredShortName", out object declaredShortNameObject))
            {
                throw new ArgumentException("The declaredShortName property is missing from the dictionary, the dictionary cannot be converted into a Redefinition");
            }
            string declaredShortNameFeature = declaredShortNameObject == null ? null : Convert.ToString(declaredShortNameObject);

            if (!dictionary.TryGetValue("elementId", out object elementIdObject))
            {
                throw new ArgumentException("The elementId property is missing from the dictionary, the dictionary cannot be converted into a Redefinition");
            }
            string elementIdFeature = Convert.ToString(elementIdObject);

            if (!dictionary.TryGetValue("general", out object generalObject))
            {
                throw new ArgumentException("The general property is missing from the dictionary, the dictionary cannot be converted into a Redefinition");
            }
            Guid generalFeature = Guid.Parse(Convert.ToString(generalObject));

            if (!dictionary.TryGetValue("isImplied", out object isImpliedObject))
            {
                throw new ArgumentException("The isImplied property is missing from the dictionary, the dictionary cannot be converted into a Redefinition");
            }
            bool isImpliedFeature = Convert.ToBoolean(isImpliedObject);

            if (!dictionary.TryGetValue("isImpliedIncluded", out object isImpliedIncludedObject))
            {
                throw new ArgumentException("The isImpliedIncluded property is missing from the dictionary, the dictionary cannot be converted into a Redefinition");
            }
            bool isImpliedIncludedFeature = Convert.ToBoolean(isImpliedIncludedObject);

            if (!dictionary.TryGetValue("ownedRelatedElement", out object ownedRelatedElementObject))
            {
                throw new ArgumentException("The ownedRelatedElement property is missing from the dictionary, the dictionary cannot be converted into a Redefinition");
            }
            List<Guid> ownedRelatedElementFeature = (ownedRelatedElementObject as List<Guid>);

            if (!dictionary.TryGetValue("ownedRelationship", out object ownedRelationshipObject))
            {
                throw new ArgumentException("The ownedRelationship property is missing from the dictionary, the dictionary cannot be converted into a Redefinition");
            }
            List<Guid> ownedRelationshipFeature = (ownedRelationshipObject as List<Guid>);

            if (!dictionary.TryGetValue("owningRelatedElement", out object owningRelatedElementObject))
            {
                throw new ArgumentException("The owningRelatedElement property is missing from the dictionary, the dictionary cannot be converted into a Redefinition");
            }
            Guid? owningRelatedElementFeature = (Guid?)owningRelatedElementObject;

            if (!dictionary.TryGetValue("owningRelationship", out object owningRelationshipObject))
            {
                throw new ArgumentException("The owningRelationship property is missing from the dictionary, the dictionary cannot be converted into a Redefinition");
            }
            Guid? owningRelationshipFeature = (Guid?)owningRelationshipObject;

            if (!dictionary.TryGetValue("redefinedFeature", out object redefinedFeatureObject))
            {
                throw new ArgumentException("The redefinedFeature property is missing from the dictionary, the dictionary cannot be converted into a Redefinition");
            }
            Guid redefinedFeatureFeature = Guid.Parse(Convert.ToString(redefinedFeatureObject));

            if (!dictionary.TryGetValue("redefiningFeature", out object redefiningFeatureObject))
            {
                throw new ArgumentException("The redefiningFeature property is missing from the dictionary, the dictionary cannot be converted into a Redefinition");
            }
            Guid redefiningFeatureFeature = Guid.Parse(Convert.ToString(redefiningFeatureObject));

            if (!dictionary.TryGetValue("source", out object sourceObject))
            {
                throw new ArgumentException("The source property is missing from the dictionary, the dictionary cannot be converted into a Redefinition");
            }
            List<Guid> sourceFeature = (sourceObject as List<Guid>);

            if (!dictionary.TryGetValue("specific", out object specificObject))
            {
                throw new ArgumentException("The specific property is missing from the dictionary, the dictionary cannot be converted into a Redefinition");
            }
            Guid specificFeature = Guid.Parse(Convert.ToString(specificObject));

            if (!dictionary.TryGetValue("subsettedFeature", out object subsettedFeatureObject))
            {
                throw new ArgumentException("The subsettedFeature property is missing from the dictionary, the dictionary cannot be converted into a Redefinition");
            }
            Guid subsettedFeatureFeature = Guid.Parse(Convert.ToString(subsettedFeatureObject));

            if (!dictionary.TryGetValue("subsettingFeature", out object subsettingFeatureObject))
            {
                throw new ArgumentException("The subsettingFeature property is missing from the dictionary, the dictionary cannot be converted into a Redefinition");
            }
            Guid subsettingFeatureFeature = Guid.Parse(Convert.ToString(subsettingFeatureObject));

            if (!dictionary.TryGetValue("target", out object targetObject))
            {
                throw new ArgumentException("The target property is missing from the dictionary, the dictionary cannot be converted into a Redefinition");
            }
            List<Guid> targetFeature = (targetObject as List<Guid>);


            redefinitionInstance.AliasIds = aliasIdsFeature ?? new List<string>();
            redefinitionInstance.DeclaredName = declaredNameFeature;
            redefinitionInstance.DeclaredShortName = declaredShortNameFeature;
            redefinitionInstance.ElementId = elementIdFeature;
            redefinitionInstance.General = generalFeature;
            redefinitionInstance.IsImplied = isImpliedFeature;
            redefinitionInstance.IsImpliedIncluded = isImpliedIncludedFeature;
            redefinitionInstance.OwnedRelatedElement = ownedRelatedElementFeature ?? new List<Guid>();
            redefinitionInstance.OwnedRelationship = ownedRelationshipFeature ?? new List<Guid>();
            redefinitionInstance.OwningRelatedElement = owningRelatedElementFeature;
            redefinitionInstance.OwningRelationship = owningRelationshipFeature;
            redefinitionInstance.RedefinedFeature = redefinedFeatureFeature;
            redefinitionInstance.RedefiningFeature = redefiningFeatureFeature;
            redefinitionInstance.Source = sourceFeature ?? new List<Guid>();
            redefinitionInstance.Specific = specificFeature;
            redefinitionInstance.SubsettedFeature = subsettedFeatureFeature;
            redefinitionInstance.SubsettingFeature = subsettingFeatureFeature;
            redefinitionInstance.Target = targetFeature ?? new List<Guid>();

            return redefinitionInstance;
        }

        /// <summary>
        /// Checks whether the <see cref="Dictionary{String, Object}"/> is not null and whether it is
        /// of type <see cref="Redefinition"/>
        /// </summary>
        /// <param name="dictionary">
        /// The subject <see cref="Dictionary{String, Object}"/> that contains the key-value pairs of
        /// properties and values.
        /// </param>
        /// <returns>
        /// an instance of <see cref="IRedefinition"/>
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="dictionary"/> is null
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown when <paramref name="dictionary"/> is not of type <see cref="Redefinition"/>
        /// </exception>
        private static IRedefinition DictionaryNullAndTypeCheck(Dictionary<string, object> dictionary)
        {
            if (dictionary == null)
            {
                throw new ArgumentNullException("dictionary", "The dictionary may not be null");
            }

            if (!dictionary.TryGetValue("@type", out object typeObject))
            {
                throw new ArgumentException("The type property is missing from the dictionary, the dictionary cannot be converted into a Redefinition");
            }

            var type = Convert.ToString(typeObject);

            if (type != "Redefinition")
            {
                throw new ArgumentException($"The dictionary contains an Object is of type {type} and can therefore not be converted into a Redefinition");
            }

            if (!dictionary.TryGetValue("@id", out object idObject))
            {
                throw new ArgumentException("The id property is missing from the dictionary, the dictionary cannot be converted into a Redefinition");
            }
            var id = Guid.Parse(Convert.ToString(idObject));

            var redefinitionInstance = new SysML2.NET.Core.DTO.Redefinition
            {
                Id = id
            };

            return redefinitionInstance;
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
