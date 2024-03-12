// -------------------------------------------------------------------------------------------------
// <copyright file="Writer.cs" company="RHEA System S.A.">
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

namespace SysML2.NET.Serializer.Dictionary
{
    using System.Collections.Generic;

    using SysML2.NET.Common;

    /// <summary>
    /// The purpose of the <see cref="Writer"/> is to write an <see cref="IData"/> and <see cref="IEnumerable{IData}"/>
    /// to a <see cref="Dictionary{String, Object}"/>
    /// </summary>
    public class Writer : IWriter
    {
        /// <summary>
        /// Writes the <see cref="IData"/> to a <see cref="Dictionary{String, Object}"/>
        /// </summary>
        /// <param name="dataItem">
        /// The subject <see cref="IData"/> instance
        /// </param>
        /// <param name="dictionaryKind">
        /// The <see cref="DictionaryKind"/> that specifies whether the result should be complex or simplified
        /// </param>
        /// <returns>
        /// the resulting <see cref="Dictionary{String, Object}"/>
        /// </returns>
        public Dictionary<string, object> Write(IData dataItem, DictionaryKind dictionaryKind)
        {
            var writer = SysML2.NET.Serializer.Dictionary.Core.DTO.DictionaryWriterProvider.Provide(dataItem.GetType());

            return writer(dataItem, dictionaryKind);
        }

        /// <summary>
        /// Writes the <see cref="IEnumerable{IData}"/> to an <see cref="IEnumerable{T}"/> of <see cref="Dictionary{String, Object}"/>
        /// </summary>
        /// <param name="dataItems">
        /// The subject <see cref="IEnumerable{IData}"/> instance
        /// </param>
        /// <param name="dictionaryKind">
        /// The <see cref="DictionaryKind"/> that specifies whether the result should be complex or simplified
        /// </param>
        /// <returns>
        /// the resulting <see cref="IEnumerable{T}"/> where T is an <see cref="Dictionary{String, Object}"/>
        /// </returns>
        public IEnumerable<Dictionary<string, object>> Write(IEnumerable<IData> dataItems, DictionaryKind dictionaryKind)
        {
            var result = new List<Dictionary<string, object>>();

            foreach (var dataItem in dataItems)
            {
                var dictionary = this.Write(dataItem, dictionaryKind);
                result.Add(dictionary);
            }

            return result;
        }
    }
}
