// -------------------------------------------------------------------------------------------------
// <copyright file="AnnotatingElementSerializer.cs" company="RHEA System S.A.">
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
    /// The purpose of the <see cref="AnnotatingElementSerializer"/> is to provide serialization
    /// and deserialization capabilities
    /// </summary>
    internal static class AnnotatingElementSerializer
    {
        /// <summary>
        /// Serializes an instance of <see cref="IAnnotatingElement"/> using an <see cref="Utf8JsonWriter"/>
        /// </summary>
        /// <param name="obj">
        /// The <see cref="IAnnotatingElement"/> to serialize
        /// </param>
        /// <param name="writer">
        /// The target <see cref="Utf8JsonWriter"/>
        /// </param>
        /// <param name="serializationModeKind">
        /// enumeration specifying what kind of serialization shall be used
        /// </param>
        internal static void Serialize(object obj, Utf8JsonWriter writer, SerializationModeKind serializationModeKind)
        {
            if (!(obj is IAnnotatingElement iAnnotatingElement))
            {
                throw new ArgumentException("The object shall be an IAnnotatingElement", nameof(obj));
            }

            writer.WriteStartObject();

            writer.WritePropertyName("@type");
            writer.WriteStringValue("AnnotatingElement");

            writer.WritePropertyName("@id");
            writer.WriteStringValue(iAnnotatingElement.Id);

            writer.WriteStartArray("aliasIds");
            foreach (var item in iAnnotatingElement.AliasIds)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WriteStartArray("annotation");
            foreach (var item in iAnnotatingElement.Annotation)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("declaredName");
            writer.WriteStringValue(iAnnotatingElement.DeclaredName);
            writer.WritePropertyName("declaredShortName");
            writer.WriteStringValue(iAnnotatingElement.DeclaredShortName);
            writer.WritePropertyName("elementId");
            writer.WriteStringValue(iAnnotatingElement.ElementId);

            writer.WritePropertyName("isImpliedIncluded");
            writer.WriteBooleanValue(iAnnotatingElement.IsImpliedIncluded);

            writer.WriteStartArray("ownedRelationship");
            foreach (var item in iAnnotatingElement.OwnedRelationship)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("owningRelationship");
            if (iAnnotatingElement.OwningRelationship.HasValue)
            {
                writer.WriteStringValue(iAnnotatingElement.OwningRelationship.Value);
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
