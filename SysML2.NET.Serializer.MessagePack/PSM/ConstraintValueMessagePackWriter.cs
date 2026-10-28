// ------------------------------------------------------------------------------------------------
// <copyright file="ConstraintValueMessagePackWriter.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
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
