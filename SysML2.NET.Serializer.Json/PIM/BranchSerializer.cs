// ------------------------------------------------------------------------------------------------
// <copyright file="BranchSerializer.cs" company="Starion Group S.A.">
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
    /// The purpose of the <see cref="BranchSerializer"/> is to provide serialization capabilities
    /// </summary>
    internal static class BranchSerializer
    {
        /// <summary>
        /// Serializes an instance of <see cref="Branch"/> using an <see cref="Utf8JsonWriter"/>
        /// </summary>
        /// <param name="obj">
        /// The <see cref="Branch"/> to serialize
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
            if (!(obj is Branch branch))
            {
                throw new ArgumentException("The object shall be an Branch", nameof(obj));
            }

            switch (serializationModeKind)
            {
                case SerializationModeKind.JSON:

                    writer.WriteStartObject();
                    writer.WriteString("@type"u8, "Branch"u8);
                    writer.WriteString("@id"u8, branch.Id);
                    writer.WriteStartArray("alias"u8);
                    if (branch.Alias != null)
                    {
                        foreach (var item in branch.Alias)
                        {
                            writer.WriteStringValue(item);
                        }
                    }
                    writer.WriteEndArray();
                    writer.WriteString("created"u8, branch.Created);

                    if (branch.Deleted.HasValue)
                    {
                        writer.WriteString("deleted"u8, branch.Deleted.Value);
                    }
                    else
                    {
                        writer.WriteNull("deleted"u8);
                    }

                    writer.WriteString("description"u8, branch.Description);
                    writer.WriteStartObject("head"u8);
                    writer.WriteString("@id"u8, branch.Head);
                    writer.WriteEndObject();
                    writer.WriteString("name"u8, branch.Name);
                    writer.WriteStartObject("owningProject"u8);
                    writer.WriteString("@id"u8, branch.OwningProject);
                    writer.WriteEndObject();
                    writer.WriteStartObject("referencedCommit"u8);
                    writer.WriteString("@id"u8, branch.ReferencedCommit);
                    writer.WriteEndObject();
                    writer.WriteString("resourceIdentifier"u8, branch.ResourceIdentifier);
                    writer.WriteEndObject();

                    break;
                case SerializationModeKind.JSONLD:

                    throw new NotImplementedException();
            }
        }
    }
}
