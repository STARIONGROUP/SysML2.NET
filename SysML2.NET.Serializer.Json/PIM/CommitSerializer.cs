// ------------------------------------------------------------------------------------------------
// <copyright file="CommitSerializer.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.Serializer.Json.PIM.DTO
{
    using System;
    using System.Text.Json;

    using SysML2.NET.PIM.DTO;
    using SysML2.NET.Serializer.Json;

    /// <summary>
    /// The purpose of the <see cref="CommitSerializer"/> is to provide serialization capabilities
    /// </summary>
    internal static class CommitSerializer
    {
        /// <summary>
        /// Serializes an instance of <see cref="Commit"/> using an <see cref="Utf8JsonWriter"/>
        /// </summary>
        /// <param name="obj">
        /// The <see cref="Commit"/> to serialize
        /// </param>
        /// <param name="writer">
        /// The target <see cref="Utf8JsonWriter"/>
        /// </param>
        /// <param name="serializationModeKind">
        /// enumeration specifying what kind of serialization shall be used
        /// </param>
        /// <param name="includeDerivedProperties">
        /// Asserts that derived properties should also be part of the serialization
        /// </param>
        internal static void Serialize(object obj, Utf8JsonWriter writer, SerializationModeKind serializationModeKind, bool includeDerivedProperties)
        {
            if (!(obj is Commit commit))
            {
                throw new ArgumentException("The object shall be an Commit", nameof(obj));
            }

            switch (serializationModeKind)
            {
                case SerializationModeKind.JSON:

                    writer.WriteStartObject();
                    writer.WriteString("@type"u8, "Commit"u8);
                    writer.WriteString("@id"u8, commit.Id);
                    writer.WriteStartArray("alias"u8);
                    if (commit.Alias != null)
                    {
                        foreach (var item in commit.Alias)
                        {
                            writer.WriteStringValue(item);
                        }
                    }
                    writer.WriteEndArray();
                    writer.WriteString("created"u8, commit.Created);
                    writer.WriteString("description"u8, commit.Description);
                    writer.WriteStartObject("owningProject"u8);
                    writer.WriteString("@id"u8, commit.OwningProject);
                    writer.WriteEndObject();
                    writer.WriteString("resourceIdentifier"u8, commit.ResourceIdentifier);

                    writer.WriteStartArray("previousCommit"u8);

                    if (commit.PreviousCommits != null)
                    {
                        foreach (var previousCommit in commit.PreviousCommits)
                        {
                            writer.WriteStartObject();
                            writer.WriteString("@id"u8, previousCommit);
                            writer.WriteEndObject();
                        }
                    }

                    writer.WriteEndArray();

                    writer.WriteStartArray("change"u8);

                    if (commit.Change != null)
                    {
                        foreach (var change in commit.Change)
                        {
                            writer.WriteStartObject();
                            writer.WriteString("@id"u8, change);
                            writer.WriteEndObject();
                        }
                    }

                    writer.WriteEndArray();
                    writer.WriteEndObject();

                    break;
                case SerializationModeKind.JSONLD:

                    throw new NotImplementedException();
            }
        }
    }
}
