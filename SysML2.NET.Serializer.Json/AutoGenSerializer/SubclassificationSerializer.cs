// -------------------------------------------------------------------------------------------------
// <copyright file="SubclassificationSerializer.cs" company="RHEA System S.A.">
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
    /// The purpose of the <see cref="SubclassificationSerializer"/> is to provide serialization
    /// and deserialization capabilities
    /// </summary>
    internal static class SubclassificationSerializer
    {
        /// <summary>
        /// Serializes an instance of <see cref="ISubclassification"/> using an <see cref="Utf8JsonWriter"/>
        /// </summary>
        /// <param name="obj">
        /// The <see cref="ISubclassification"/> to serialize
        /// </param>
        /// <param name="writer">
        /// The target <see cref="Utf8JsonWriter"/>
        /// </param>
        /// <param name="serializationModeKind">
        /// enumeration specifying what kind of serialization shall be used
        /// </param>
        internal static void Serialize(object obj, Utf8JsonWriter writer, SerializationModeKind serializationModeKind)
        {
            if (!(obj is ISubclassification iSubclassification))
            {
                throw new ArgumentException("The object shall be an ISubclassification", nameof(obj));
            }

            writer.WriteStartObject();

            writer.WritePropertyName("@type");
            writer.WriteStringValue("Subclassification");

            writer.WritePropertyName("@id");
            writer.WriteStringValue(iSubclassification.Id);

            writer.WriteStartArray("aliasIds");
            foreach (var item in iSubclassification.AliasIds)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("declaredName");
            writer.WriteStringValue(iSubclassification.DeclaredName);
            writer.WritePropertyName("declaredShortName");
            writer.WriteStringValue(iSubclassification.DeclaredShortName);
            writer.WritePropertyName("elementId");
            writer.WriteStringValue(iSubclassification.ElementId);

            writer.WritePropertyName("general");
            writer.WriteStringValue(iSubclassification.General);

            writer.WritePropertyName("isImplied");
            writer.WriteBooleanValue(iSubclassification.IsImplied);

            writer.WritePropertyName("isImpliedIncluded");
            writer.WriteBooleanValue(iSubclassification.IsImpliedIncluded);

            writer.WriteStartArray("ownedRelatedElement");
            foreach (var item in iSubclassification.OwnedRelatedElement)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WriteStartArray("ownedRelationship");
            foreach (var item in iSubclassification.OwnedRelationship)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("owningRelatedElement");
            if (iSubclassification.OwningRelatedElement.HasValue)
            {
                writer.WriteStringValue(iSubclassification.OwningRelatedElement.Value);
            }
            else
            {
                writer.WriteNullValue();
            }
            writer.WritePropertyName("owningRelationship");
            if (iSubclassification.OwningRelationship.HasValue)
            {
                writer.WriteStringValue(iSubclassification.OwningRelationship.Value);
            }
            else
            {
                writer.WriteNullValue();
            }
            writer.WriteStartArray("source");
            foreach (var item in iSubclassification.Source)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("specific");
            writer.WriteStringValue(iSubclassification.Specific);

            writer.WritePropertyName("subclassifier");
            writer.WriteStringValue(iSubclassification.Subclassifier);

            writer.WritePropertyName("superclassifier");
            writer.WriteStringValue(iSubclassification.Superclassifier);

            writer.WriteStartArray("target");
            foreach (var item in iSubclassification.Target)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WriteEndObject();
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
