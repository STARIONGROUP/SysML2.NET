// -------------------------------------------------------------------------------------------------
// <copyright file="ProjectSerializer.cs" company="RHEA System S.A.">
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
    /// The purpose of the <see cref="ProjectSerializer"/> is to provide serialization capabilities
    /// </summary>
    internal static class ProjectSerializer
    {
        /// <summary>
        /// Serializes an instance of <see cref="Project"/> using an <see cref="Utf8JsonWriter"/>
        /// </summary>
        /// <param name="obj">
        /// The <see cref="Project"/> to serialize
        /// </param>
        /// <param name="writer">
        /// The target <see cref="Utf8JsonWriter"/>
        /// </param>
        /// <param name="serializationModeKind">
        /// enumeration specifying what kind of serialization shall be used
        /// </param>
        internal static void Serialize(object obj, Utf8JsonWriter writer, SerializationModeKind serializationModeKind)
        {
            if (!(obj is Project project))
            {
                throw new ArgumentException("The object shall be an Project", nameof(obj));
            }

            switch (serializationModeKind)
            {
                case SerializationModeKind.JSON:

                    writer.WriteStartObject();
                    writer.WriteString("@type"u8, "Project"u8);
                    writer.WriteString("@id"u8, project.Id);

                    writer.WriteStartArray("alias"u8);
                    foreach (var alias in project.Alias)
                    {
                        writer.WriteStringValue(alias);
                    }
                    writer.WriteEndArray();
                    writer.WriteStartObject("defaultBranch"u8);
                    writer.WriteString("@id"u8, project.DefaultBranch);
                    writer.WriteEndObject();
                    writer.WriteString("description"u8, project.Description);
                    writer.WriteString("name"u8, project.Name);
                    writer.WriteEndObject();

                    break;
                case SerializationModeKind.JSONLD:

                    throw new NotImplementedException();
            }
        }
    }
}