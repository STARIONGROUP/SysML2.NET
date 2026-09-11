// -------------------------------------------------------------------------------------------------
// <copyright file="ConstraintValueMessagePackWriter.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.Serializer.MessagePack.PSM
{
    using System;

    using SysML2.NET.PSM.DTO;

    using global::MessagePack;

    /// <summary>
    /// The purpose of the <see cref="ConstraintValueMessagePackWriter"/> is to write a
    /// <see cref="ConstraintValue"/> as MessagePack
    /// </summary>
    /// <remarks>
    /// Each alternative occupies a distinct MessagePack type, so no type discriminator is written.
    /// </remarks>
    internal static class ConstraintValueMessagePackWriter
    {
        /// <summary>
        /// Writes a <see cref="ConstraintValue"/> using a <see cref="MessagePackWriter"/>
        /// </summary>
        /// <param name="writer">
        /// The target <see cref="MessagePackWriter"/>
        /// </param>
        /// <param name="value">
        /// The <see cref="ConstraintValue"/> to write
        /// </param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when the value holds an alternative that is not known
        /// </exception>
        internal static void Write(ref MessagePackWriter writer, ConstraintValue value)
        {
            switch (value.Kind)
            {
                case ConstraintValueKind.Null:
                    writer.WriteNil();
                    break;

                case ConstraintValueKind.Boolean:
                    value.TryGetBoolean(out var booleanValue);
                    writer.Write(booleanValue);
                    break;

                case ConstraintValueKind.Number:
                    value.TryGetNumber(out var numberValue);
                    writer.Write(numberValue);
                    break;

                case ConstraintValueKind.String:
                    value.TryGetString(out var stringValue);
                    writer.Write(stringValue);
                    break;

                case ConstraintValueKind.Guid:
                    value.TryGetGuid(out var guidValue);
                    writer.Write(guidValue.ToByteArray());
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(value));
            }
        }
    }
}
