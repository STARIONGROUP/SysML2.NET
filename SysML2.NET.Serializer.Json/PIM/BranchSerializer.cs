// -------------------------------------------------------------------------------------------------
// <copyright file="BranchSerializer.cs" company="Starion Group S.A.">
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
                    writer.WriteString("description"u8, branch.Description);
                    writer.WriteStartObject("head"u8);
                    writer.WriteString("@id"u8, branch.Head);
                    writer.WriteEndObject();
                    writer.WriteString("name"u8, branch.Name);
                    writer.WriteStartObject("owningProject"u8);
                    writer.WriteString("@id"u8, branch.OwningProject);
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
