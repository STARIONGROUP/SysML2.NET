// -------------------------------------------------------------------------------------------------
// <copyright file="TransitionFeatureKindDeSerializer.cs" company="RHEA System S.A.">
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

namespace SysML2.NET.Serializer.Json.Core.DTO
{
    using System;

    using SysML2.NET.Core;

    /// <summary>
    /// The purpose of the <see cref="TransitionFeatureKindDeSerializer"/> is to provide deserialization capabilities
    /// for the <see cref="TransitionFeatureKind"/> Enum
    /// </summary>
    internal static class TransitionFeatureKindDeSerializer
    {
        /// <summary>
        /// Deserializes a string value to a <see cref="TransitionFeatureKind"/>
        /// </summary>
        /// <param name="value">
        /// The string representation of the <see cref="TransitionFeatureKind"/>
        /// </param>
        /// <returns>
        /// The value of the <see cref="TransitionFeatureKind"/>
        /// </returns>
        internal static TransitionFeatureKind Deserialize(string value)
        {
            value = value.ToUpper();

            switch (value)
            {
                case "TRIGGER":
                    return TransitionFeatureKind.Trigger;
                case "GUARD":
                    return TransitionFeatureKind.Guard;
                case "EFFECT":
                    return TransitionFeatureKind.Effect;
                default:
                    throw new ArgumentException($"{value} is not a valid TransitionFeatureKind", nameof(value));
            }
        }

        /// <summary>
        /// Deserializes a string value to a nullable <see cref="TransitionFeatureKind"/>
        /// </summary>
        /// <param name="value">
        /// The string representation of the <see cref="TransitionFeatureKind"/>
        /// </param>
        /// <returns>
        /// The value of the nullable <see cref="TransitionFeatureKind"/>
        /// </returns>
        internal static TransitionFeatureKind? DeserializeNullable(string value)
        {
            if (value == null)
            {
                return null;
            }

            value = value.ToUpper();

            switch (value)
            {
                case "TRIGGER":
                    return TransitionFeatureKind.Trigger;
                case "GUARD":
                    return TransitionFeatureKind.Guard;
                case "EFFECT":
                    return TransitionFeatureKind.Effect;
                default:
                    throw new ArgumentException($"{value} is not a valid TransitionFeatureKind", nameof(value));
            }
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
