// -------------------------------------------------------------------------------------------------
// <copyright file="ExpressionDictionaryWriter.cs" company="Starion Group S.A.">
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

    using SysML2.NET.Common;
    using SysML2.NET.Core.DTO;

    /// <summary>
    /// The purpose of the <see cref="ExpressionDictionaryWriter"/> is to write (convert) a <see cref="IExpression"/>
    /// to a <see cref="Dictionary{String, Object}"/>.
    /// </summary>
    public static class ExpressionDictionaryWriter
    {
        /// <summary>
        /// Writes a <see cref="IExpression"/> to a <see cref="Dictionary{String, Object}"/> that contains a key-value-pair
        /// for each property. The type key is used to store type information (name of the Type).
        /// </summary>
        /// <param name="dataItem">
        /// The subject <see cref="IExpression"/> that is to be written to a <see cref="Dictionary{String, Object}"/>.
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
            var expressionInstance = ThingNullAndTypeCheck(dataItem);

            switch (dictionaryKind)
            {
                case DictionaryKind.Complex:
                    return WriteComplex(expressionInstance);
                case DictionaryKind.Simplified:
                    return WriteSimplified(expressionInstance);
                default:
                    throw new NotSupportedException($"The dictionaryKind:{dictionaryKind} is not supported");
            }
        }

        /// <summary>
        /// Writes a <see cref="IExpression"/> to a <see cref="Dictionary{String, Object}"/> that contains a key-value-pair
        /// for each property. The type key is used to store type information (name of the Type).
        /// </summary>
        /// <param name="expressionInstance">
        /// The subject <see cref="IExpression"/> that is to be written to a <see cref="Dictionary{String, Object}"/>.
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
        private static Dictionary<string, object> WriteSimplified(IExpression expressionInstance)
        {
            var dictionary = new Dictionary<string, object>
            {
                { "@type", "Expression" },
                { "@id", expressionInstance.Id.ToString() }
            };

            dictionary.Add("aliasIds", expressionInstance.AliasIds);
            dictionary.Add("declaredName", expressionInstance.DeclaredName);
            dictionary.Add("declaredShortName", expressionInstance.DeclaredShortName);
            dictionary.Add("direction", expressionInstance.Direction);
            dictionary.Add("elementId", expressionInstance.ElementId);
            dictionary.Add("isAbstract", expressionInstance.IsAbstract);
            dictionary.Add("isComposite", expressionInstance.IsComposite);
            dictionary.Add("isConstant", expressionInstance.IsConstant);
            dictionary.Add("isDerived", expressionInstance.IsDerived);
            dictionary.Add("isEnd", expressionInstance.IsEnd);
            dictionary.Add("isImpliedIncluded", expressionInstance.IsImpliedIncluded);
            dictionary.Add("isOrdered", expressionInstance.IsOrdered);
            dictionary.Add("isPortion", expressionInstance.IsPortion);
            dictionary.Add("isSufficient", expressionInstance.IsSufficient);
            dictionary.Add("isUnique", expressionInstance.IsUnique);
            dictionary.Add("isVariable", expressionInstance.IsVariable);
            dictionary.Add("ownedRelationship", $"[ {string.Join(",", expressionInstance.OwnedRelationship)} ]");
            dictionary.Add("owningRelationship", expressionInstance.OwningRelationship.ToString());

            return dictionary;
        }

        /// <summary>
        /// Writes a <see cref="IExpression"/> to a <see cref="Dictionary{String, Object}"/> that contains a key-value-pair
        /// for each property. The type key is used to store type information (name of the Type).
        /// </summary>
        /// <param name="expressionInstance">
        /// The subject <see cref="IExpression"/> that is to be written to a <see cref="Dictionary{String, Object}"/>.
        /// </param>
        /// <returns>
        /// An instance of <see cref="Dictionary{String, Object}"/> that contains all the properties as key-value-pairs as well
        /// as a type key that is used to store type information (name of the Type).
        /// </returns>
        /// <remarks>
        /// All values are stored as is, no conversion is done
        /// </remarks>
        private static Dictionary<string, object> WriteComplex(IExpression expressionInstance)
        {
            var dictionary = new Dictionary<string, object>
            {
                { "@type", "Expression" },
                { "@id", expressionInstance.Id }
            };

            dictionary.Add("aliasIds", expressionInstance.AliasIds);
            dictionary.Add("declaredName", expressionInstance.DeclaredName);
            dictionary.Add("declaredShortName", expressionInstance.DeclaredShortName);
            dictionary.Add("direction", expressionInstance.Direction);
            dictionary.Add("elementId", expressionInstance.ElementId);
            dictionary.Add("isAbstract", expressionInstance.IsAbstract);
            dictionary.Add("isComposite", expressionInstance.IsComposite);
            dictionary.Add("isConstant", expressionInstance.IsConstant);
            dictionary.Add("isDerived", expressionInstance.IsDerived);
            dictionary.Add("isEnd", expressionInstance.IsEnd);
            dictionary.Add("isImpliedIncluded", expressionInstance.IsImpliedIncluded);
            dictionary.Add("isOrdered", expressionInstance.IsOrdered);
            dictionary.Add("isPortion", expressionInstance.IsPortion);
            dictionary.Add("isSufficient", expressionInstance.IsSufficient);
            dictionary.Add("isUnique", expressionInstance.IsUnique);
            dictionary.Add("isVariable", expressionInstance.IsVariable);
            dictionary.Add("ownedRelationship", expressionInstance.OwnedRelationship);
            dictionary.Add("owningRelationship", expressionInstance.OwningRelationship);

            return dictionary;
        }

        /// <summary>
        /// Checks whether the <see cref="IData"/> is not null and whether it is
        /// of type <see cref="IExpression"/>
        /// </summary>
        /// <param name="dataItem">
        /// The subject <see cref="IData"/>
        /// </param>
        /// <returns>
        /// an instance of <see cref="IExpression"/>
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="thing"/> is null
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown when <paramref name="thing"/> is not of type <see cref="IExpression"/>
        /// </exception>
        private static IExpression ThingNullAndTypeCheck(IData dataItem)
        {
            if (dataItem == null)
            {
                throw new ArgumentNullException("dataItem", "The dataItem may not be null");
            }

            if (!(dataItem is IExpression expressionInstance))
            {
                throw new ArgumentException("The dataItem must be of Type IExpression", "dataItem");
            }

            return expressionInstance;
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
