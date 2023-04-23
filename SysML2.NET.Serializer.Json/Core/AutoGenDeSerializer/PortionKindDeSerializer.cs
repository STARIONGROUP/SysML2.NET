// -------------------------------------------------------------------------------------------------
// <copyright file="PortionKindDeSerializer.cs" company="RHEA System S.A.">
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

namespace SysML2.NET.Core.DTO.Serializer.Json
{
    using System;

    using SysML2.NET.Core;

    /// <summary>
    /// The purpose of the <see cref="PortionKindDeSerializer"/> is to provide deserialization capabilities
    /// for the <see cref="PortionKind"/> Enum
    /// </summary>
    internal static class PortionKindDeSerializer
    {
        /// <summary>
        /// Deserializes a string value to a <see cref="PortionKind"/>
        /// </summary>
        /// <param name="value">
        /// The string representation of the <see cref="PortionKind"/>
        /// </param>
        /// <returns>
        /// The value of the <see cref="PortionKind"/>
        /// </returns>
        internal static PortionKind Deserialize(string value)
        {
            value = value.ToUpper();

            switch (value)
            {
                case "TIMESLICE":
                    return PortionKind.Timeslice;
                case "SNAPSHOT":
                    return PortionKind.Snapshot;
                default:
                    throw new ArgumentException($"{value} is not a valid PortionKind", nameof(value));
            }
        }

        /// <summary>
        /// Deserializes a string value to a nullable <see cref="PortionKind"/>
        /// </summary>
        /// <param name="value">
        /// The string representation of the <see cref="PortionKind"/>
        /// </param>
        /// <returns>
        /// The value of the nullable <see cref="PortionKind"/>
        /// </returns>
        internal static PortionKind? DeserializeNullable(string value)
        {
            if (value == null)
            {
                return null;
            }

            value = value.ToUpper();

            switch (value)
            {
                case "TIMESLICE":
                    return PortionKind.Timeslice;
                case "SNAPSHOT":
                    return PortionKind.Snapshot;
                default:
                    throw new ArgumentException($"{value} is not a valid PortionKind", nameof(value));
            }
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
