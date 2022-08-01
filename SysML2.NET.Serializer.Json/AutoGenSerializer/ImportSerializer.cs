// -------------------------------------------------------------------------------------------------
// <copyright file="ImportSerializer.cs" company="RHEA System S.A.">
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

    using SysML2.NET.DTO;

    /// <summary>
    /// The purpose of the <see cref="ImportSerializer"/> is to provide serialization
    /// and deserialization capabilities
    /// </summary>
    internal static class ImportSerializer
    {
        /// <summary>
        /// Serializes an instance of <see cref="IImport"/> using an <see cref="Utf8JsonWriter"/>
        /// </summary>
        /// <param name="obj">
        /// The <see cref="IImport"/> to serialize
        /// </param>
        /// <param name="writer">
        /// The target <see cref="Utf8JsonWriter"/>
        /// </param>
        /// <param name="serializationModeKind">
        /// enumeration specifying what kind of serialization shall be used
        /// </param>
        internal static void Serialize(object obj, Utf8JsonWriter writer, SerializationModeKind serializationModeKind)
        {
            if (!(obj is IImport iImport))
            {
                throw new ArgumentException("The object shall be an IImport", nameof(obj));
            }

            writer.WriteStartObject();

            writer.WritePropertyName("@type");
            writer.WriteStringValue("Import");

            writer.WritePropertyName("@id");
            writer.WriteStringValue(iImport.Id);

            writer.WriteStartArray("aliasIds");
            foreach (var item in iImport.AliasIds)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("elementId");
            writer.WriteStringValue(iImport.ElementId);

            writer.WritePropertyName("importedMemberName");
            writer.WriteStringValue(iImport.ImportedMemberName);

            writer.WritePropertyName("importedNamespace");
            writer.WriteStringValue(iImport.ImportedNamespace);

            writer.WritePropertyName("isImportAll");
            writer.WriteBooleanValue(iImport.IsImportAll);

            writer.WritePropertyName("isRecursive");
            writer.WriteBooleanValue(iImport.IsRecursive);

            writer.WritePropertyName("name");
            writer.WriteStringValue(iImport.Name);

            writer.WriteStartArray("ownedRelatedElement");
            foreach (var item in iImport.OwnedRelatedElement)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WriteStartArray("ownedRelationship");
            foreach (var item in iImport.OwnedRelationship)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("owningRelatedElement");
            if (iImport.OwningRelatedElement.HasValue)
            {
                writer.WriteStringValue(iImport.OwningRelatedElement.Value);
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("owningRelationship");
            if (iImport.OwningRelationship.HasValue)
            {
                writer.WriteStringValue(iImport.OwningRelationship.Value);
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("shortName");
            writer.WriteStringValue(iImport.ShortName);

            writer.WriteStartArray("source");
            foreach (var item in iImport.Source)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WriteStartArray("target");
            foreach (var item in iImport.Target)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("visibility");
            writer.WriteStringValue(iImport.Visibility.ToString().ToUpper());

            writer.WriteEndObject();
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
