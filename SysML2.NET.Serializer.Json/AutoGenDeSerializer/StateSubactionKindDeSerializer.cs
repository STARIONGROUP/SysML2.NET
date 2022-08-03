// -------------------------------------------------------------------------------------------------
// <copyright file="StateSubactionKindDeSerializer.cs" company="RHEA System S.A.">
//
//   Copyright 2022 RHEA System S.A.
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

namespace SysML2.NET.Serializer.Json
{
    using System;

    using SysML2.NET.Core;

    /// <summary>
    /// The purpose of the <see cref="StateSubactionKindDeSerializer"/> is to provide deserialization capabilities
    /// for the <see cref="StateSubactionKind"/> Enum
    /// </summary>
    internal static class StateSubactionKindDeSerializer
    {
        /// <summary>
        /// Deserializes a string value to a <see cref="StateSubactionKind"/>
        /// </summary>
        /// <param name="value">
        /// The string representation of the <see cref="StateSubactionKind"/>
        /// </param>
        /// <returns>
        /// The value of the <see cref="StateSubactionKind"/>
        /// </returns>
        internal static StateSubactionKind Deserialize(string value)
        {
            switch (value)
            {
                case "ENTRY":
                    return StateSubactionKind.Entry;
                case "DO":
                    return StateSubactionKind.Do;
                case "EXIT":
                    return StateSubactionKind.Exit;
                default:
                    throw new ArgumentException($"{value} is not a valid StateSubactionKind", nameof(value));
            }
        }

        /// <summary>
        /// Deserializes a string value to a nullable <see cref="StateSubactionKind"/>
        /// </summary>
        /// <param name="value">
        /// The string representation of the <see cref="StateSubactionKind"/>
        /// </param>
        /// <returns>
        /// The value of the nullable <see cref="StateSubactionKind"/>
        /// </returns>
        internal static StateSubactionKind? DeserializeNullable(string value)
        {
            switch (value)
            {
                case "ENTRY":
                    return StateSubactionKind.Entry;
                case "DO":
                    return StateSubactionKind.Do;
                case "EXIT":
                    return StateSubactionKind.Exit;
                case null:
                    return null;
                default:
                    throw new ArgumentException($"{value} is not a valid StateSubactionKind", nameof(value));
            }
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
