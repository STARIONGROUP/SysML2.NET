// -------------------------------------------------------------------------------------------------
// <copyright file="NamespaceExposeDictionaryReader.cs" company="Starion Group S.A.">
//
//   Copyright 2022-2024 Starion Group S.A.
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
    /// The purpose of the <see cref="NamespaceExposeDictionaryReader"/> is to read (convert)
    /// a <see cref="Dictionary{String, Object}"/> from an <see cref="INamespaceExpose"/>
    /// </summary>
    public static class NamespaceExposeDictionaryReader
    {
        /// <summary>
        /// Reads a <see cref="INamespaceExpose"/> from a <see cref="Dictionary{String, Object}"/> that contains a key-value-pair
        /// for each property.
        /// </summary>
        /// <param name="dictionary">
        /// The subject <see cref="Dictionary{String, Object}"/> that is to be converted into a <see cref="INamespaceExpose"/>
        /// </param>
        /// <param name="dictionaryKind">
        /// The source <see cref="DictionaryKind"/> that is to be read from
        /// </param>
        /// <returns>
        /// An instance of <see cref="INamespaceExpose"/>
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
        /// Reads a <see cref="INamespaceExpose"/> from a <see cref="Dictionary{String, Object}"/> that contains a key-value-pair
        /// for each property.
        /// </summary>
        /// <param name="dictionary">
        /// The subject <see cref="Dictionary{String, Object}"/> that is to be converted into a <see cref="INamespaceExpose"/>
        /// </param>
        /// <returns>
        /// An instance of <see cref="INamespaceExpose"/>
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
        private static INamespaceExpose ReadSimplified(Dictionary<string, object> dictionary)
        {
            var namespaceExposeInstance = DictionaryNullAndTypeCheck(dictionary);

            if (!dictionary.TryGetValue("aliasIds", out object aliasIdsObject))
            {
                throw new ArgumentException("The aliasIds property is missing from the dictionary, the dictionary cannot be converted into a NamespaceExpose");
            }
            List<string> aliasIdsFeature = aliasIdsObject as List<string>;

            if (!dictionary.TryGetValue("declaredName", out object declaredNameObject))
            {
                throw new ArgumentException("The declaredName property is missing from the dictionary, the dictionary cannot be converted into a NamespaceExpose");
            }
            string declaredNameFeature = declaredNameObject == null ? null : Convert.ToString(declaredNameObject);

            if (!dictionary.TryGetValue("declaredShortName", out object declaredShortNameObject))
            {
                throw new ArgumentException("The declaredShortName property is missing from the dictionary, the dictionary cannot be converted into a NamespaceExpose");
            }
            string declaredShortNameFeature = declaredShortNameObject == null ? null : Convert.ToString(declaredShortNameObject);

            if (!dictionary.TryGetValue("elementId", out object elementIdObject))
            {
                throw new ArgumentException("The elementId property is missing from the dictionary, the dictionary cannot be converted into a NamespaceExpose");
            }
            string elementIdFeature = Convert.ToString(elementIdObject);

            if (!dictionary.TryGetValue("importedNamespace", out object importedNamespaceObject))
            {
                throw new ArgumentException("The importedNamespace property is missing from the dictionary, the dictionary cannot be converted into a NamespaceExpose");
            }
            Guid importedNamespaceFeature = Guid.Parse(Convert.ToString(importedNamespaceObject));

            if (!dictionary.TryGetValue("isImplied", out object isImpliedObject))
            {
                throw new ArgumentException("The isImplied property is missing from the dictionary, the dictionary cannot be converted into a NamespaceExpose");
            }
            bool isImpliedFeature = Convert.ToBoolean(isImpliedObject);

            if (!dictionary.TryGetValue("isImpliedIncluded", out object isImpliedIncludedObject))
            {
                throw new ArgumentException("The isImpliedIncluded property is missing from the dictionary, the dictionary cannot be converted into a NamespaceExpose");
            }
            bool isImpliedIncludedFeature = Convert.ToBoolean(isImpliedIncludedObject);

            if (!dictionary.TryGetValue("isImportAll", out object isImportAllObject))
            {
                throw new ArgumentException("The isImportAll property is missing from the dictionary, the dictionary cannot be converted into a NamespaceExpose");
            }
            bool isImportAllFeature = Convert.ToBoolean(isImportAllObject);

            if (!dictionary.TryGetValue("isRecursive", out object isRecursiveObject))
            {
                throw new ArgumentException("The isRecursive property is missing from the dictionary, the dictionary cannot be converted into a NamespaceExpose");
            }
            bool isRecursiveFeature = Convert.ToBoolean(isRecursiveObject);

            if (!dictionary.TryGetValue("ownedRelatedElement", out object ownedRelatedElementObject))
            {
                throw new ArgumentException("The ownedRelatedElement property is missing from the dictionary, the dictionary cannot be converted into a NamespaceExpose");
            }
            List<Guid> ownedRelatedElementFeature = (ownedRelatedElementObject as List<string>)?.Select(Guid.Parse).ToList();

            if (!dictionary.TryGetValue("ownedRelationship", out object ownedRelationshipObject))
            {
                throw new ArgumentException("The ownedRelationship property is missing from the dictionary, the dictionary cannot be converted into a NamespaceExpose");
            }
            List<Guid> ownedRelationshipFeature = (ownedRelationshipObject as List<string>)?.Select(Guid.Parse).ToList();

            if (!dictionary.TryGetValue("owningRelatedElement", out object owningRelatedElementObject))
            {
                throw new ArgumentException("The owningRelatedElement property is missing from the dictionary, the dictionary cannot be converted into a NamespaceExpose");
            }
            Guid? owningRelatedElementFeature = owningRelatedElementObject == null ? (Guid?)null : Guid.Parse(Convert.ToString(owningRelatedElementObject));

            if (!dictionary.TryGetValue("owningRelationship", out object owningRelationshipObject))
            {
                throw new ArgumentException("The owningRelationship property is missing from the dictionary, the dictionary cannot be converted into a NamespaceExpose");
            }
            Guid? owningRelationshipFeature = owningRelationshipObject == null ? (Guid?)null : Guid.Parse(Convert.ToString(owningRelationshipObject));

            if (!dictionary.TryGetValue("source", out object sourceObject))
            {
                throw new ArgumentException("The source property is missing from the dictionary, the dictionary cannot be converted into a NamespaceExpose");
            }
            List<Guid> sourceFeature = (sourceObject as List<string>)?.Select(Guid.Parse).ToList();

            if (!dictionary.TryGetValue("target", out object targetObject))
            {
                throw new ArgumentException("The target property is missing from the dictionary, the dictionary cannot be converted into a NamespaceExpose");
            }
            List<Guid> targetFeature = (targetObject as List<string>)?.Select(Guid.Parse).ToList();

            if (!dictionary.TryGetValue("visibility", out object visibilityObject))
            {
                throw new ArgumentException("The visibility property is missing from the dictionary, the dictionary cannot be converted into a NamespaceExpose");
            }
            VisibilityKind visibilityFeature = (VisibilityKind)Enum.Parse(typeof(VisibilityKind), Convert.ToString(visibilityObject), true);


            namespaceExposeInstance.AliasIds = aliasIdsFeature ?? new List<string>();
            namespaceExposeInstance.DeclaredName = declaredNameFeature;
            namespaceExposeInstance.DeclaredShortName = declaredShortNameFeature;
            namespaceExposeInstance.ElementId = elementIdFeature;
            namespaceExposeInstance.ImportedNamespace = importedNamespaceFeature;
            namespaceExposeInstance.IsImplied = isImpliedFeature;
            namespaceExposeInstance.IsImpliedIncluded = isImpliedIncludedFeature;
            namespaceExposeInstance.IsImportAll = isImportAllFeature;
            namespaceExposeInstance.IsRecursive = isRecursiveFeature;
            namespaceExposeInstance.OwnedRelatedElement = ownedRelatedElementFeature ?? new List<Guid>();
            namespaceExposeInstance.OwnedRelationship = ownedRelationshipFeature ?? new List<Guid>();
            namespaceExposeInstance.OwningRelatedElement = owningRelatedElementFeature;
            namespaceExposeInstance.OwningRelationship = owningRelationshipFeature;
            namespaceExposeInstance.Source = sourceFeature ?? new List<Guid>();
            namespaceExposeInstance.Target = targetFeature ?? new List<Guid>();
            namespaceExposeInstance.Visibility = visibilityFeature;

            return namespaceExposeInstance;
        }

        /// <summary>
        /// Reads a <see cref="INamespaceExpose"/> from a <see cref="Dictionary{String, Object}"/> that contains a key-value-pair
        /// for each property.
        /// </summary>
        /// <param name="dictionary">
        /// The subject <see cref="Dictionary{String, Object}"/> that is to be converted into a <see cref="INamespaceExpose"/>
        /// </param>
        /// <returns>
        /// An instance of <see cref="INamespaceExpose"/>
        /// </returns>
        private static INamespaceExpose ReadComplex(Dictionary<string, object> dictionary)
        {
            var namespaceExposeInstance = DictionaryNullAndTypeCheck(dictionary);

            if (!dictionary.TryGetValue("aliasIds", out object aliasIdsObject))
            {
                throw new ArgumentException("The aliasIds property is missing from the dictionary, the dictionary cannot be converted into a NamespaceExpose");
            }
            List<string> aliasIdsFeature = aliasIdsObject as List<string>;

            if (!dictionary.TryGetValue("declaredName", out object declaredNameObject))
            {
                throw new ArgumentException("The declaredName property is missing from the dictionary, the dictionary cannot be converted into a NamespaceExpose");
            }
            string declaredNameFeature = declaredNameObject == null ? null : Convert.ToString(declaredNameObject);

            if (!dictionary.TryGetValue("declaredShortName", out object declaredShortNameObject))
            {
                throw new ArgumentException("The declaredShortName property is missing from the dictionary, the dictionary cannot be converted into a NamespaceExpose");
            }
            string declaredShortNameFeature = declaredShortNameObject == null ? null : Convert.ToString(declaredShortNameObject);

            if (!dictionary.TryGetValue("elementId", out object elementIdObject))
            {
                throw new ArgumentException("The elementId property is missing from the dictionary, the dictionary cannot be converted into a NamespaceExpose");
            }
            string elementIdFeature = Convert.ToString(elementIdObject);

            if (!dictionary.TryGetValue("importedNamespace", out object importedNamespaceObject))
            {
                throw new ArgumentException("The importedNamespace property is missing from the dictionary, the dictionary cannot be converted into a NamespaceExpose");
            }
            Guid importedNamespaceFeature = Guid.Parse(Convert.ToString(importedNamespaceObject));

            if (!dictionary.TryGetValue("isImplied", out object isImpliedObject))
            {
                throw new ArgumentException("The isImplied property is missing from the dictionary, the dictionary cannot be converted into a NamespaceExpose");
            }
            bool isImpliedFeature = Convert.ToBoolean(isImpliedObject);

            if (!dictionary.TryGetValue("isImpliedIncluded", out object isImpliedIncludedObject))
            {
                throw new ArgumentException("The isImpliedIncluded property is missing from the dictionary, the dictionary cannot be converted into a NamespaceExpose");
            }
            bool isImpliedIncludedFeature = Convert.ToBoolean(isImpliedIncludedObject);

            if (!dictionary.TryGetValue("isImportAll", out object isImportAllObject))
            {
                throw new ArgumentException("The isImportAll property is missing from the dictionary, the dictionary cannot be converted into a NamespaceExpose");
            }
            bool isImportAllFeature = Convert.ToBoolean(isImportAllObject);

            if (!dictionary.TryGetValue("isRecursive", out object isRecursiveObject))
            {
                throw new ArgumentException("The isRecursive property is missing from the dictionary, the dictionary cannot be converted into a NamespaceExpose");
            }
            bool isRecursiveFeature = Convert.ToBoolean(isRecursiveObject);

            if (!dictionary.TryGetValue("ownedRelatedElement", out object ownedRelatedElementObject))
            {
                throw new ArgumentException("The ownedRelatedElement property is missing from the dictionary, the dictionary cannot be converted into a NamespaceExpose");
            }
            List<Guid> ownedRelatedElementFeature = (ownedRelatedElementObject as List<Guid>);

            if (!dictionary.TryGetValue("ownedRelationship", out object ownedRelationshipObject))
            {
                throw new ArgumentException("The ownedRelationship property is missing from the dictionary, the dictionary cannot be converted into a NamespaceExpose");
            }
            List<Guid> ownedRelationshipFeature = (ownedRelationshipObject as List<Guid>);

            if (!dictionary.TryGetValue("owningRelatedElement", out object owningRelatedElementObject))
            {
                throw new ArgumentException("The owningRelatedElement property is missing from the dictionary, the dictionary cannot be converted into a NamespaceExpose");
            }
            Guid? owningRelatedElementFeature = (Guid?)owningRelatedElementObject;

            if (!dictionary.TryGetValue("owningRelationship", out object owningRelationshipObject))
            {
                throw new ArgumentException("The owningRelationship property is missing from the dictionary, the dictionary cannot be converted into a NamespaceExpose");
            }
            Guid? owningRelationshipFeature = (Guid?)owningRelationshipObject;

            if (!dictionary.TryGetValue("source", out object sourceObject))
            {
                throw new ArgumentException("The source property is missing from the dictionary, the dictionary cannot be converted into a NamespaceExpose");
            }
            List<Guid> sourceFeature = (sourceObject as List<Guid>);

            if (!dictionary.TryGetValue("target", out object targetObject))
            {
                throw new ArgumentException("The target property is missing from the dictionary, the dictionary cannot be converted into a NamespaceExpose");
            }
            List<Guid> targetFeature = (targetObject as List<Guid>);

            if (!dictionary.TryGetValue("visibility", out object visibilityObject))
            {
                throw new ArgumentException("The visibility property is missing from the dictionary, the dictionary cannot be converted into a NamespaceExpose");
            }
            VisibilityKind visibilityFeature = (VisibilityKind)visibilityObject;


            namespaceExposeInstance.AliasIds = aliasIdsFeature ?? new List<string>();
            namespaceExposeInstance.DeclaredName = declaredNameFeature;
            namespaceExposeInstance.DeclaredShortName = declaredShortNameFeature;
            namespaceExposeInstance.ElementId = elementIdFeature;
            namespaceExposeInstance.ImportedNamespace = importedNamespaceFeature;
            namespaceExposeInstance.IsImplied = isImpliedFeature;
            namespaceExposeInstance.IsImpliedIncluded = isImpliedIncludedFeature;
            namespaceExposeInstance.IsImportAll = isImportAllFeature;
            namespaceExposeInstance.IsRecursive = isRecursiveFeature;
            namespaceExposeInstance.OwnedRelatedElement = ownedRelatedElementFeature ?? new List<Guid>();
            namespaceExposeInstance.OwnedRelationship = ownedRelationshipFeature ?? new List<Guid>();
            namespaceExposeInstance.OwningRelatedElement = owningRelatedElementFeature;
            namespaceExposeInstance.OwningRelationship = owningRelationshipFeature;
            namespaceExposeInstance.Source = sourceFeature ?? new List<Guid>();
            namespaceExposeInstance.Target = targetFeature ?? new List<Guid>();
            namespaceExposeInstance.Visibility = visibilityFeature;

            return namespaceExposeInstance;
        }

        /// <summary>
        /// Checks whether the <see cref="Dictionary{String, Object}"/> is not null and whether it is
        /// of type <see cref="NamespaceExpose"/>
        /// </summary>
        /// <param name="dictionary">
        /// The subject <see cref="Dictionary{String, Object}"/> that contains the key-value pairs of
        /// properties and values.
        /// </param>
        /// <returns>
        /// an instance of <see cref="INamespaceExpose"/>
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="dictionary"/> is null
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown when <paramref name="dictionary"/> is not of type <see cref="NamespaceExpose"/>
        /// </exception>
        private static INamespaceExpose DictionaryNullAndTypeCheck(Dictionary<string, object> dictionary)
        {
            if (dictionary == null)
            {
                throw new ArgumentNullException("dictionary", "The dictionary may not be null");
            }

            if (!dictionary.TryGetValue("@type", out object typeObject))
            {
                throw new ArgumentException("The type property is missing from the dictionary, the dictionary cannot be converted into a NamespaceExpose");
            }

            var type = Convert.ToString(typeObject);

            if (type != "NamespaceExpose")
            {
                throw new ArgumentException($"The dictionary contains an Object is of type {type} and can therefore not be converted into a NamespaceExpose");
            }

            if (!dictionary.TryGetValue("@id", out object idObject))
            {
                throw new ArgumentException("The id property is missing from the dictionary, the dictionary cannot be converted into a NamespaceExpose");
            }
            var id = Guid.Parse(Convert.ToString(idObject));

            var namespaceExposeInstance = new SysML2.NET.Core.DTO.NamespaceExpose
            {
                Id = id
            };

            return namespaceExposeInstance;
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
