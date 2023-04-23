// -------------------------------------------------------------------------------------------------
// <copyright file="DocumentationSerializer.cs" company="RHEA System S.A.">
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

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.Serializer.Json
{
    using System;
    using System.Text.Json;

    using SysML2.NET.Common;
    using SysML2.NET.Core.DTO;

    /// <summary>
    /// The purpose of the <see cref="DocumentationSerializer"/> is to provide serialization capabilities
    /// capabilities for the <see cref="IDocumentation"/> interface
    /// </summary>
    internal static class DocumentationSerializer
    {
        /// <summary>
        /// Serializes an instance of <see cref="IDocumentation"/> using an <see cref="Utf8JsonWriter"/>
        /// </summary>
        /// <param name="obj">
        /// The <see cref="IDocumentation"/> to serialize
        /// </param>
        /// <param name="writer">
        /// The target <see cref="Utf8JsonWriter"/>
        /// </param>
        /// <param name="serializationModeKind">
        /// enumeration specifying what kind of serialization shall be used
        /// </param>
        internal static void Serialize(object obj, Utf8JsonWriter writer, SerializationModeKind serializationModeKind)
        {
            if (!(obj is IDocumentation iDocumentation))
            {
                throw new ArgumentException("The object shall be an IDocumentation", nameof(obj));
            }

            writer.WriteStartObject();

            writer.WritePropertyName("@type"u8);
            writer.WriteStringValue("Documentation"u8);

            writer.WritePropertyName("@id"u8);
            writer.WriteStringValue(iDocumentation.Id);

            writer.WriteStartArray("aliasIds"u8);
            foreach (var item in iDocumentation.AliasIds)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WriteStartArray("annotation"u8);
            foreach (var item in iDocumentation.Annotation)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("body"u8);
            writer.WriteStringValue(iDocumentation.Body);

            writer.WritePropertyName("declaredName"u8);
            writer.WriteStringValue(iDocumentation.DeclaredName);
            writer.WritePropertyName("declaredShortName"u8);
            writer.WriteStringValue(iDocumentation.DeclaredShortName);
            writer.WritePropertyName("elementId"u8);
            writer.WriteStringValue(iDocumentation.ElementId);

            writer.WritePropertyName("isImpliedIncluded"u8);
            writer.WriteBooleanValue(iDocumentation.IsImpliedIncluded);

            writer.WritePropertyName("locale"u8);
            writer.WriteStringValue(iDocumentation.Locale);
            writer.WriteStartArray("ownedRelationship"u8);
            foreach (var item in iDocumentation.OwnedRelationship)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("owningRelationship"u8);
            if (iDocumentation.OwningRelationship.HasValue)
            {
                writer.WriteStringValue(iDocumentation.OwningRelationship.Value);
            }
            else
            {
                writer.WriteNullValue();
            }
            writer.WriteEndObject();
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
