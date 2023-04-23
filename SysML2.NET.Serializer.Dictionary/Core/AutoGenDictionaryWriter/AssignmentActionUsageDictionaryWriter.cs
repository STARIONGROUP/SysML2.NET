// -------------------------------------------------------------------------------------------------
// <copyright file="AssignmentActionUsageDictionaryWriter.cs" company="RHEA System S.A.">
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

    using SysML2.NET.Common;
    using SysML2.NET.Core.DTO;

    /// <summary>
    /// The purpose of the <see cref="AssignmentActionUsageDictionaryWriter"/> is to write (convert) a <see cref="IAssignmentActionUsage"/>
    /// to a <see cref="Dictionary{String, Object}"/>.
    /// </summary>
    public static class AssignmentActionUsageDictionaryWriter
    {
        /// <summary>
        /// Writes a <see cref="IAssignmentActionUsage"/> to a <see cref="Dictionary{String, Object}"/> that contains a key-value-pair
        /// for each property. The type key is used to store type information (name of the Type).
        /// </summary>
        /// <param name="dataItem">
        /// The subject <see cref="IAssignmentActionUsage"/> that is to be written to a <see cref="Dictionary{String, Object}"/>.
        /// </param>
        /// <param name="dictionaryKind">
        /// The target <see cref="DictionaryKind"/> that is to be created
        /// </param>
        /// <returns>
        /// An instance of <see cref="Dictionary{String, Object}"/> that contains all the properties as key-value-pairs as well
        /// as a type key that is used to store type information (name of the Type).
        /// </returns>
        /// <remarks>
        /// When the <see cref="DictionaryKind"/> is <see cref="DictionaryKind.Complex"/> then the values are written to the
        /// <see cref="Dictionary{String, Object}"/> as is. When the <see cref="DictionaryKind"/> is <see cref="DictionaryKind.Simplified"/>
        /// then the following applies:
        /// The values that are of the following types are stored as is:
        ///   - Number, an abstract type, which has the subtypes Integer and Float
        ///   - String
        ///   - Boolean
        ///   - The spatial type Point
        ///   - Temporal types: Date, Time, LocalTime, DateTime, LocalDateTime and Duration
        /// values of other types are converted to string, in case these are an <see cref="IEnumerable{T}"/> then
        /// the values are converted to an Array of String using JSON notation, i.e. [ value_1, ..., value_n ]
        /// </remarks>
        public static Dictionary<string, object> Write(IData dataItem, DictionaryKind dictionaryKind)
        {
            var assignmentActionUsageInstance = ThingNullAndTypeCheck(dataItem);

            switch (dictionaryKind)
            {
                case DictionaryKind.Complex:
                    return WriteComplex(assignmentActionUsageInstance);
                case DictionaryKind.Simplified:
                    return WriteSimplified(assignmentActionUsageInstance);
                default:
                    throw new NotSupportedException($"The dictionaryKind:{dictionaryKind} is not supported");
            }
        }

        /// <summary>
        /// Writes a <see cref="IAssignmentActionUsage"/> to a <see cref="Dictionary{String, Object}"/> that contains a key-value-pair
        /// for each property. The type key is used to store type information (name of the Type).
        /// </summary>
        /// <param name="assignmentActionUsageInstance">
        /// The subject <see cref="IAssignmentActionUsage"/> that is to be written to a <see cref="Dictionary{String, Object}"/>.
        /// </param>
        /// <returns>
        /// An instance of <see cref="Dictionary{String, Object}"/> that contains all the properties as key-value-pairs as well
        /// as a type key that is used to store type information (name of the Type).
        /// </returns>
        /// <remarks>
        /// The values that are of the following types are stored as is:
        ///   - Number, an abstract type, which has the subtypes Integer and Float
        ///   - String
        ///   - Boolean
        ///   - The spatial type Point
        ///   - Temporal types: Date, Time, LocalTime, DateTime, LocalDateTime and Duration
        /// values of other types are converted to string, in case these are an <see cref="IEnumerable{T}"/> then
        /// the values are converted to an Array of String using JSON notation, i.e. [ value_1, ..., value_n ]
        /// </remarks>
        private static Dictionary<string, object> WriteSimplified(IAssignmentActionUsage assignmentActionUsageInstance)
        {
            var dictionary = new Dictionary<string, object>
            {
                { "@type", "AssignmentActionUsage" },
                { "@id", assignmentActionUsageInstance.Id.ToString() }
            };

            dictionary.Add("aliasIds", assignmentActionUsageInstance.AliasIds);
            dictionary.Add("declaredName", assignmentActionUsageInstance.DeclaredName);
            dictionary.Add("declaredShortName", assignmentActionUsageInstance.DeclaredShortName);
            dictionary.Add("direction", assignmentActionUsageInstance.Direction);
            dictionary.Add("elementId", assignmentActionUsageInstance.ElementId);
            dictionary.Add("isAbstract", assignmentActionUsageInstance.IsAbstract);
            dictionary.Add("isComposite", assignmentActionUsageInstance.IsComposite);
            dictionary.Add("isDerived", assignmentActionUsageInstance.IsDerived);
            dictionary.Add("isEnd", assignmentActionUsageInstance.IsEnd);
            dictionary.Add("isImpliedIncluded", assignmentActionUsageInstance.IsImpliedIncluded);
            dictionary.Add("isIndividual", assignmentActionUsageInstance.IsIndividual);
            dictionary.Add("isOrdered", assignmentActionUsageInstance.IsOrdered);
            dictionary.Add("isPortion", assignmentActionUsageInstance.IsPortion);
            dictionary.Add("isReadOnly", assignmentActionUsageInstance.IsReadOnly);
            dictionary.Add("isSufficient", assignmentActionUsageInstance.IsSufficient);
            dictionary.Add("isUnique", assignmentActionUsageInstance.IsUnique);
            dictionary.Add("isVariation", assignmentActionUsageInstance.IsVariation);
            dictionary.Add("ownedRelationship", $"[ {string.Join(",", assignmentActionUsageInstance.OwnedRelationship)} ]");
            dictionary.Add("owningRelationship", assignmentActionUsageInstance.OwningRelationship.ToString());
            dictionary.Add("portionKind", assignmentActionUsageInstance.PortionKind);

            return dictionary;
        }

        /// <summary>
        /// Writes a <see cref="IAssignmentActionUsage"/> to a <see cref="Dictionary{String, Object}"/> that contains a key-value-pair
        /// for each property. The type key is used to store type information (name of the Type).
        /// </summary>
        /// <param name="assignmentActionUsageInstance">
        /// The subject <see cref="IAssignmentActionUsage"/> that is to be written to a <see cref="Dictionary{String, Object}"/>.
        /// </param>
        /// <returns>
        /// An instance of <see cref="Dictionary{String, Object}"/> that contains all the properties as key-value-pairs as well
        /// as a type key that is used to store type information (name of the Type).
        /// </returns>
        /// <remarks>
        /// All values are stored as is, no conversion is done
        /// </remarks>
        private static Dictionary<string, object> WriteComplex(IAssignmentActionUsage assignmentActionUsageInstance)
        {
            var dictionary = new Dictionary<string, object>
            {
                { "@type", "AssignmentActionUsage" },
                { "@id", assignmentActionUsageInstance.Id }
            };

            dictionary.Add("aliasIds", assignmentActionUsageInstance.AliasIds);
            dictionary.Add("declaredName", assignmentActionUsageInstance.DeclaredName);
            dictionary.Add("declaredShortName", assignmentActionUsageInstance.DeclaredShortName);
            dictionary.Add("direction", assignmentActionUsageInstance.Direction);
            dictionary.Add("elementId", assignmentActionUsageInstance.ElementId);
            dictionary.Add("isAbstract", assignmentActionUsageInstance.IsAbstract);
            dictionary.Add("isComposite", assignmentActionUsageInstance.IsComposite);
            dictionary.Add("isDerived", assignmentActionUsageInstance.IsDerived);
            dictionary.Add("isEnd", assignmentActionUsageInstance.IsEnd);
            dictionary.Add("isImpliedIncluded", assignmentActionUsageInstance.IsImpliedIncluded);
            dictionary.Add("isIndividual", assignmentActionUsageInstance.IsIndividual);
            dictionary.Add("isOrdered", assignmentActionUsageInstance.IsOrdered);
            dictionary.Add("isPortion", assignmentActionUsageInstance.IsPortion);
            dictionary.Add("isReadOnly", assignmentActionUsageInstance.IsReadOnly);
            dictionary.Add("isSufficient", assignmentActionUsageInstance.IsSufficient);
            dictionary.Add("isUnique", assignmentActionUsageInstance.IsUnique);
            dictionary.Add("isVariation", assignmentActionUsageInstance.IsVariation);
            dictionary.Add("ownedRelationship", assignmentActionUsageInstance.OwnedRelationship);
            dictionary.Add("owningRelationship", assignmentActionUsageInstance.OwningRelationship);
            dictionary.Add("portionKind", assignmentActionUsageInstance.PortionKind);

            return dictionary;
        }

        /// <summary>
        /// Checks whether the <see cref="IData"/> is not null and whether it is
        /// of type <see cref="IAssignmentActionUsage"/>
        /// </summary>
        /// <param name="dataItem">
        /// The subject <see cref="IData"/>
        /// </param>
        /// <returns>
        /// an instance of <see cref="IAssignmentActionUsage"/>
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="thing"/> is null
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown when <paramref name="thing"/> is not of type <see cref="IAssignmentActionUsage"/>
        /// </exception>
        private static IAssignmentActionUsage ThingNullAndTypeCheck(IData dataItem)
        {
            if (dataItem == null)
            {
                throw new ArgumentNullException("dataItem", "The dataItem may not be null");
            }

            if (!(dataItem is IAssignmentActionUsage assignmentActionUsageInstance))
            {
                throw new ArgumentException("The dataItem must be of Type IAssignmentActionUsage", "dataItem");
            }

            return assignmentActionUsageInstance;
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
