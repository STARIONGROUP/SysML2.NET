// -------------------------------------------------------------------------------------------------
// <copyright file="DictionaryKind.cs" company="RHEA System S.A.">
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

namespace SysML2.NET.Serializer.Dictionary
{
    using System.Collections.Generic;

    using SysML2.NET.Common;

    /// <summary>
    /// An enumeration data-type data that defines the structure (content) of a <see cref="IData"/> <see cref="Dictionary{String, Object}"/>
    /// </summary>
    public enum DictionaryKind
    {
        /// <summary>
        /// The values of the dictionary are simplified (as string) C# data-types.
        /// </summary>
        /// <remarks>
        /// The following data-types are converted to string:
        ///   - Guid
        /// </remarks>
        Simplified,

        /// <summary>
        /// The values of the dictionary are complex C# data-types
        /// </summary>
        Complex
    }
}
