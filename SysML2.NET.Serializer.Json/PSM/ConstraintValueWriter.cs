// -------------------------------------------------------------------------------------------------
// <copyright file="ConstraintValueWriter.cs" company="Starion Group S.A.">
//
//   Copyright 2022-2026 Starion Group S.A.
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

namespace SysML2.NET.Serializer.Json.PSM
{
    using System;
    using System.Text.Json;

    using SysML2.NET.PSM.DTO;

    /// <summary>
    /// The purpose of the <see cref="ConstraintValueWriter"/> is to write a <see cref="ConstraintValue"/> as JSON
    /// </summary>
    /// <remarks>
    /// Each alternative occupies a distinct JSON token type, so no type discriminator is written.
    /// </remarks>
    internal static class ConstraintValueWriter
    {
        /// <summary>
        /// Writes a <see cref="ConstraintValue"/> using an <see cref="Utf8JsonWriter"/>
        /// </summary>
        /// <param name="value">
        /// The <see cref="ConstraintValue"/> to write
        /// </param>
        /// <param name="writer">
        /// The target <see cref="Utf8JsonWriter"/>
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when the writer is null
        /// </exception>
        internal static void Write(ConstraintValue value, Utf8JsonWriter writer)
        {
            if (writer is null)
            {
                throw new ArgumentNullException(nameof(writer));
            }

            switch (value.Kind)
            {
                case ConstraintValueKind.Boolean:
                    value.TryGetBoolean(out var booleanValue);
                    writer.WriteBooleanValue(booleanValue);
                    break;

                case ConstraintValueKind.Number:
                    value.TryGetNumber(out var numberValue);
                    writer.WriteNumberValue(numberValue);
                    break;

                case ConstraintValueKind.String:
                    value.TryGetString(out var stringValue);
                    writer.WriteStringValue(stringValue);
                    break;

                case ConstraintValueKind.Guid:
                    value.TryGetGuid(out var guidValue);
                    writer.WriteStartObject();
                    writer.WriteString("@id"u8, guidValue);
                    writer.WriteEndObject();
                    break;

                default:
                    writer.WriteNullValue();
                    break;
            }
        }
    }
}
