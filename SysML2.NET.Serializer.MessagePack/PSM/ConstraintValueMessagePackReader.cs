// ------------------------------------------------------------------------------------------------
// <copyright file="ConstraintValueMessagePackReader.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.Serializer.MessagePack.PSM
{
    using System;
    using System.Buffers;

    using SysML2.NET.PSM.DTO;

    using global::MessagePack;

    /// <summary>
    /// The purpose of the <see cref="ConstraintValueMessagePackReader"/> is to read a
    /// <see cref="ConstraintValue"/> from MessagePack
    /// </summary>
    /// <remarks>
    /// Each alternative occupies a distinct MessagePack type, so the type alone discriminates.
    /// </remarks>
    internal static class ConstraintValueMessagePackReader
    {
        /// <summary>
        /// Reads a <see cref="ConstraintValue"/> using a <see cref="MessagePackReader"/>
        /// </summary>
        /// <param name="reader">
        /// The <see cref="MessagePackReader"/> positioned on the value
        /// </param>
        /// <returns>
        /// The <see cref="ConstraintValue"/> that the value represents
        /// </returns>
        /// <exception cref="MessagePackSerializationException">
        /// Thrown when the value represents none of the alternatives
        /// </exception>
        internal static ConstraintValue Read(ref MessagePackReader reader)
        {
            switch (reader.NextMessagePackType)
            {
                case MessagePackType.Nil:
                    reader.ReadNil();
                    return ConstraintValue.Null;

                case MessagePackType.Boolean:
                    return ConstraintValue.From(reader.ReadBoolean());

                case MessagePackType.Integer:
                case MessagePackType.Float:
                    return ConstraintValue.From(reader.ReadDouble());

                case MessagePackType.String:
                    return ConstraintValue.From(reader.ReadString());

                case MessagePackType.Binary:
                    var bytes = reader.ReadBytes();

                    if (bytes is not { Length: 16 })
                    {
                        throw new MessagePackSerializationException("Expected a unique identifier as 16 bytes.");
                    }

                    Span<byte> identifier = stackalloc byte[16];
                    bytes.Value.CopyTo(identifier);

                    return ConstraintValue.From(new Guid(identifier));

                default:
                    throw new MessagePackSerializationException($"A ConstraintValue cannot be read from a {reader.NextMessagePackType} value.");
            }
        }
    }
}
