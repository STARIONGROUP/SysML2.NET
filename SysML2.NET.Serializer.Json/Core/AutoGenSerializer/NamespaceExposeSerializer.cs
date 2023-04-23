// -------------------------------------------------------------------------------------------------
// <copyright file="NamespaceExposeSerializer.cs" company="RHEA System S.A.">
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

namespace SysML2.NET.Core.DTO.Serializer.Json
{
    using System;
    using System.Text.Json;

    using SysML2.NET.Common;
    using SysML2.NET.Core.DTO;
    using SysML2.NET.Serializer.Json;

    /// <summary>
    /// The purpose of the <see cref="NamespaceExposeSerializer"/> is to provide serialization capabilities
    /// capabilities for the <see cref="INamespaceExpose"/> interface
    /// </summary>
    internal static class NamespaceExposeSerializer
    {
        /// <summary>
        /// Serializes an instance of <see cref="INamespaceExpose"/> using an <see cref="Utf8JsonWriter"/>
        /// </summary>
        /// <param name="obj">
        /// The <see cref="INamespaceExpose"/> to serialize
        /// </param>
        /// <param name="writer">
        /// The target <see cref="Utf8JsonWriter"/>
        /// </param>
        /// <param name="serializationModeKind">
        /// enumeration specifying what kind of serialization shall be used
        /// </param>
        internal static void Serialize(object obj, Utf8JsonWriter writer, SerializationModeKind serializationModeKind)
        {
            if (!(obj is INamespaceExpose iNamespaceExpose))
            {
                throw new ArgumentException("The object shall be an INamespaceExpose", nameof(obj));
            }

            writer.WriteStartObject();

            writer.WritePropertyName("@type"u8);
            writer.WriteStringValue("NamespaceExpose"u8);

            writer.WritePropertyName("@id"u8);
            writer.WriteStringValue(iNamespaceExpose.Id);

            writer.WriteStartArray("aliasIds"u8);
            foreach (var item in iNamespaceExpose.AliasIds)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("declaredName"u8);
            writer.WriteStringValue(iNamespaceExpose.DeclaredName);
            writer.WritePropertyName("declaredShortName"u8);
            writer.WriteStringValue(iNamespaceExpose.DeclaredShortName);
            writer.WritePropertyName("elementId"u8);
            writer.WriteStringValue(iNamespaceExpose.ElementId);

            writer.WritePropertyName("importedNamespace"u8);
            writer.WriteStringValue(iNamespaceExpose.ImportedNamespace);

            writer.WritePropertyName("isImplied"u8);
            writer.WriteBooleanValue(iNamespaceExpose.IsImplied);

            writer.WritePropertyName("isImpliedIncluded"u8);
            writer.WriteBooleanValue(iNamespaceExpose.IsImpliedIncluded);

            writer.WritePropertyName("isImportAll"u8);
            writer.WriteBooleanValue(iNamespaceExpose.IsImportAll);

            writer.WritePropertyName("isRecursive"u8);
            writer.WriteBooleanValue(iNamespaceExpose.IsRecursive);

            writer.WriteStartArray("ownedRelatedElement"u8);
            foreach (var item in iNamespaceExpose.OwnedRelatedElement)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WriteStartArray("ownedRelationship"u8);
            foreach (var item in iNamespaceExpose.OwnedRelationship)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("owningRelatedElement"u8);
            if (iNamespaceExpose.OwningRelatedElement.HasValue)
            {
                writer.WriteStringValue(iNamespaceExpose.OwningRelatedElement.Value);
            }
            else
            {
                writer.WriteNullValue();
            }
            writer.WritePropertyName("owningRelationship"u8);
            if (iNamespaceExpose.OwningRelationship.HasValue)
            {
                writer.WriteStringValue(iNamespaceExpose.OwningRelationship.Value);
            }
            else
            {
                writer.WriteNullValue();
            }
            writer.WriteStartArray("source"u8);
            foreach (var item in iNamespaceExpose.Source)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WriteStartArray("target"u8);
            foreach (var item in iNamespaceExpose.Target)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("visibility"u8);
            writer.WriteStringValue(iNamespaceExpose.Visibility.ToString().ToLower());

            writer.WriteEndObject();
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
