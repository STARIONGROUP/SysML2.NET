// -------------------------------------------------------------------------------------------------
// <copyright file="ExposeSerializer.cs" company="RHEA System S.A.">
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
    /// The purpose of the <see cref="ExposeSerializer"/> is to provide serialization
    /// and deserialization capabilities
    /// </summary>
    internal static class ExposeSerializer
    {
        /// <summary>
        /// Serializes an instance of <see cref="IExpose"/> using an <see cref="Utf8JsonWriter"/>
        /// </summary>
        /// <param name="obj">
        /// The <see cref="IExpose"/> to serialize
        /// </param>
        /// <param name="writer">
        /// The target <see cref="Utf8JsonWriter"/>
        /// </param>
        /// <param name="serializationModeKind">
        /// enumeration specifying what kind of serialization shall be used
        /// </param>
        internal static void Serialize(object obj, Utf8JsonWriter writer, SerializationModeKind serializationModeKind)
        {
            if (!(obj is IExpose iExpose))
            {
                throw new ArgumentException("The object shall be an IExpose", nameof(obj));
            }

            writer.WriteStartObject();

            writer.WritePropertyName("@type");
            writer.WriteStringValue("Expose");

            writer.WritePropertyName("@id");
            writer.WriteStringValue(iExpose.Id);

            writer.WriteStartArray("aliasIds");
            foreach (var item in iExpose.AliasIds)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("elementId");
            writer.WriteStringValue(iExpose.ElementId);

            writer.WritePropertyName("importedMemberName");
            writer.WriteStringValue(iExpose.ImportedMemberName);
            writer.WritePropertyName("importedNamespace");
            writer.WriteStringValue(iExpose.ImportedNamespace);

            writer.WritePropertyName("isImplied");
            writer.WriteBooleanValue(iExpose.IsImplied);

            writer.WritePropertyName("isImpliedIncluded");
            writer.WriteBooleanValue(iExpose.IsImpliedIncluded);

            writer.WritePropertyName("isImportAll");
            writer.WriteBooleanValue(iExpose.IsImportAll);

            writer.WritePropertyName("isRecursive");
            writer.WriteBooleanValue(iExpose.IsRecursive);

            writer.WritePropertyName("name");
            writer.WriteStringValue(iExpose.Name);
            writer.WriteStartArray("ownedRelatedElement");
            foreach (var item in iExpose.OwnedRelatedElement)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WriteStartArray("ownedRelationship");
            foreach (var item in iExpose.OwnedRelationship)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("owningRelatedElement");
            if (iExpose.OwningRelatedElement.HasValue)
            {
                writer.WriteStringValue(iExpose.OwningRelatedElement.Value);
            }
            else
            {
                writer.WriteNullValue();
            }
            writer.WritePropertyName("owningRelationship");
            if (iExpose.OwningRelationship.HasValue)
            {
                writer.WriteStringValue(iExpose.OwningRelationship.Value);
            }
            else
            {
                writer.WriteNullValue();
            }
            writer.WritePropertyName("shortName");
            writer.WriteStringValue(iExpose.ShortName);
            writer.WriteStartArray("source");
            foreach (var item in iExpose.Source)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WriteStartArray("target");
            foreach (var item in iExpose.Target)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("visibility");
            writer.WriteStringValue(iExpose.Visibility.ToString().ToLower());

            writer.WriteEndObject();
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
