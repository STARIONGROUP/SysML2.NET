// -------------------------------------------------------------------------------------------------
// <copyright file="NamespaceImportSerializer.cs" company="RHEA System S.A.">
//
// Copyright 2022 RHEA System S.A.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
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
    /// The purpose of the <see cref="NamespaceImportSerializer"/> is to provide serialization
    /// and deserialization capabilities
    /// </summary>
    internal static class NamespaceImportSerializer
    {
        /// <summary>
        /// Serializes an instance of <see cref="INamespaceImport"/> using an <see cref="Utf8JsonWriter"/>
        /// </summary>
        /// <param name="obj">
        /// The <see cref="INamespaceImport"/> to serialize
        /// </param>
        /// <param name="writer">
        /// The target <see cref="Utf8JsonWriter"/>
        /// </param>
        /// <param name="serializationModeKind">
        /// enumeration specifying what kind of serialization shall be used
        /// </param>
        internal static void Serialize(object obj, Utf8JsonWriter writer, SerializationModeKind serializationModeKind)
        {
            if (!(obj is INamespaceImport iNamespaceImport))
            {
                throw new ArgumentException("The object shall be an INamespaceImport", nameof(obj));
            }

            writer.WriteStartObject();

            writer.WritePropertyName("@type");
            writer.WriteStringValue("NamespaceImport");

            writer.WritePropertyName("@id");
            writer.WriteStringValue(iNamespaceImport.Id);

            writer.WriteStartArray("aliasIds");
            foreach (var item in iNamespaceImport.AliasIds)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("elementId");
            writer.WriteStringValue(iNamespaceImport.ElementId);

            writer.WritePropertyName("importedNamespace");
            writer.WriteStringValue(iNamespaceImport.ImportedNamespace);

            writer.WritePropertyName("isImplied");
            writer.WriteBooleanValue(iNamespaceImport.IsImplied);

            writer.WritePropertyName("isImpliedIncluded");
            writer.WriteBooleanValue(iNamespaceImport.IsImpliedIncluded);

            writer.WritePropertyName("isImportAll");
            writer.WriteBooleanValue(iNamespaceImport.IsImportAll);

            writer.WritePropertyName("isRecursive");
            writer.WriteBooleanValue(iNamespaceImport.IsRecursive);

            writer.WritePropertyName("name");
            writer.WriteStringValue(iNamespaceImport.Name);

            writer.WriteStartArray("ownedRelatedElement");
            foreach (var item in iNamespaceImport.OwnedRelatedElement)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WriteStartArray("ownedRelationship");
            foreach (var item in iNamespaceImport.OwnedRelationship)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("owningRelatedElement");
            if (iNamespaceImport.OwningRelatedElement.HasValue)
            {
                writer.WriteStringValue(iNamespaceImport.OwningRelatedElement.Value);
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("owningRelationship");
            if (iNamespaceImport.OwningRelationship.HasValue)
            {
                writer.WriteStringValue(iNamespaceImport.OwningRelationship.Value);
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("shortName");
            writer.WriteStringValue(iNamespaceImport.ShortName);

            writer.WriteStartArray("source");
            foreach (var item in iNamespaceImport.Source)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WriteStartArray("target");
            foreach (var item in iNamespaceImport.Target)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("visibility");
            writer.WriteStringValue(iNamespaceImport.Visibility.ToString().ToLower());

            writer.WriteEndObject();
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
