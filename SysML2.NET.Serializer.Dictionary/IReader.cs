// -------------------------------------------------------------------------------------------------
// <copyright file="IReader.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.Serializer.Dictionary
{
    using System.Collections.Generic;

    using SysML2.NET.Common;

    /// <summary>
    /// The purpose of the <see cref="IReader"/> is to read an <see cref="IData"/> and <see cref="IEnumerable{IData}"/>
    /// from a <see cref="Dictionary{String, Object}"/>
    /// </summary>
    public interface IReader
    {
        /// <summary>
        /// Reads the <see cref="IData"/> from a <see cref="Dictionary{String, Object}"/>
        /// </summary>
        /// <param name="dictionary">
        /// The subject <see cref="Dictionary{String, Object}"/> instance
        /// </param>
        /// <param name="dictionaryKind">
        /// The <see cref="DictionaryKind"/> that specifies whether the source should be complex or simplified
        /// </param>
        /// <returns>
        /// the resulting <see cref="IData"/>
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
        IData Read(Dictionary<string, object> dictionary, DictionaryKind dictionaryKind);

        /// <summary>
        /// Reads the <see cref="IEnumerable{IData}"/> to from <see cref="IEnumerable{T}"/> of <see cref="Dictionary{String, Object}"/>
        /// </summary>
        /// <param name="dictionaries">
        /// The subject list of <see cref="Dictionary{String, Object}"/> instances
        /// </param>
        /// <param name="dictionaryKind">
        /// The <see cref="DictionaryKind"/> that specifies whether the source should be complex or simplified
        /// </param>
        /// <returns>
        /// the resulting <see cref="IEnumerable{IData}"/>
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
        IEnumerable<IData> Read(IEnumerable<Dictionary<string, object>> dictionaries, DictionaryKind dictionaryKind);
    }
}
