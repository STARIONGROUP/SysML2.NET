// ------------------------------------------------------------------------------------------------
// <copyright file="ConstraintValueReader.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.Serializer.Json.PSM
{
    using System.Text.Json;

    using SysML2.NET.PSM;
    using SysML2.NET.PSM.DTO;
    using SysML2.NET.Serializer.Json.Utility;

    /// <summary>
    /// The purpose of the <see cref="ConstraintValueReader"/> is to read a <see cref="ConstraintValue"/> from JSON
    /// </summary>
    /// <remarks>
    /// Each alternative occupies a distinct JSON token type, so the token alone discriminates.
    /// </remarks>
    internal static class ConstraintValueReader
    {
        /// <summary>
        /// Reads a <see cref="ConstraintValue"/> from the provided <see cref="Utf8JsonReader"/>
        /// </summary>
        /// <param name="reader">
        /// The <see cref="Utf8JsonReader"/> positioned on the value token
        /// </param>
        /// <returns>
        /// The <see cref="ConstraintValue"/> that the token represents
        /// </returns>
        /// <exception cref="JsonException">
        /// Thrown when the token represents none of the alternatives
        /// </exception>
        internal static ConstraintValue Read(ref Utf8JsonReader reader)
        {
            switch (reader.TokenType)
            {
                case JsonTokenType.Null:
                    return ConstraintValue.Null;

                case JsonTokenType.True:
                    return ConstraintValue.From(true);

                case JsonTokenType.False:
                    return ConstraintValue.From(false);

                case JsonTokenType.Number:
                    return ConstraintValue.From(reader.GetDouble());

                case JsonTokenType.String:
                    return ConstraintValue.From(reader.GetString());

                case JsonTokenType.StartObject:
                    return Utf8JsonReaderHelper.TryReadReferenceIdentifier(ref reader, out var identifier)
                        ? ConstraintValue.From(identifier)
                        : ConstraintValue.Null;

                default:
                    throw new JsonException($"A ConstraintValue cannot be read from a {reader.TokenType} token.");
            }
        }
    }
}
