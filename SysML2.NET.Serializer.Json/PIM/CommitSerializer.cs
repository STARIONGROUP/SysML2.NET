// -------------------------------------------------------------------------------------------------
// <copyright file="CommitSerializer.cs" company="RHEA System S.A.">
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
        internal static void Serialize(object obj, Utf8JsonWriter writer, SerializationModeKind serializationModeKind)
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
                    writer.WriteStartArray("previousCommits"u8);
                    if (commit.PreviousCommits != null)
                    {
                        foreach (var previousCommitId in commit.PreviousCommits)
                        {
                            writer.WriteStartObject();
                            writer.WriteString("@id"u8, previousCommitId);
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
