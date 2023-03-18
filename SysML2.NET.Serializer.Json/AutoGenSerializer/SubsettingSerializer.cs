// -------------------------------------------------------------------------------------------------
// <copyright file="SubsettingSerializer.cs" company="RHEA System S.A.">
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
    /// The purpose of the <see cref="SubsettingSerializer"/> is to provide serialization
    /// and deserialization capabilities
    /// </summary>
    internal static class SubsettingSerializer
    {
        /// <summary>
        /// Serializes an instance of <see cref="ISubsetting"/> using an <see cref="Utf8JsonWriter"/>
        /// </summary>
        /// <param name="obj">
        /// The <see cref="ISubsetting"/> to serialize
        /// </param>
        /// <param name="writer">
        /// The target <see cref="Utf8JsonWriter"/>
        /// </param>
        /// <param name="serializationModeKind">
        /// enumeration specifying what kind of serialization shall be used
        /// </param>
        internal static void Serialize(object obj, Utf8JsonWriter writer, SerializationModeKind serializationModeKind)
        {
            if (!(obj is ISubsetting iSubsetting))
            {
                throw new ArgumentException("The object shall be an ISubsetting", nameof(obj));
            }

            writer.WriteStartObject();

            writer.WritePropertyName("@type");
            writer.WriteStringValue("Subsetting");

            writer.WritePropertyName("@id");
            writer.WriteStringValue(iSubsetting.Id);

            writer.WriteStartArray("aliasIds");
            foreach (var item in iSubsetting.AliasIds)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("declaredName");
            writer.WriteStringValue(iSubsetting.DeclaredName);
            writer.WritePropertyName("declaredShortName");
            writer.WriteStringValue(iSubsetting.DeclaredShortName);
            writer.WritePropertyName("elementId");
            writer.WriteStringValue(iSubsetting.ElementId);

            writer.WritePropertyName("general");
            writer.WriteStringValue(iSubsetting.General);

            writer.WritePropertyName("isImplied");
            writer.WriteBooleanValue(iSubsetting.IsImplied);

            writer.WritePropertyName("isImpliedIncluded");
            writer.WriteBooleanValue(iSubsetting.IsImpliedIncluded);

            writer.WriteStartArray("ownedRelatedElement");
            foreach (var item in iSubsetting.OwnedRelatedElement)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WriteStartArray("ownedRelationship");
            foreach (var item in iSubsetting.OwnedRelationship)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("owningRelatedElement");
            if (iSubsetting.OwningRelatedElement.HasValue)
            {
                writer.WriteStringValue(iSubsetting.OwningRelatedElement.Value);
            }
            else
            {
                writer.WriteNullValue();
            }
            writer.WritePropertyName("owningRelationship");
            if (iSubsetting.OwningRelationship.HasValue)
            {
                writer.WriteStringValue(iSubsetting.OwningRelationship.Value);
            }
            else
            {
                writer.WriteNullValue();
            }
            writer.WriteStartArray("source");
            foreach (var item in iSubsetting.Source)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("specific");
            writer.WriteStringValue(iSubsetting.Specific);

            writer.WritePropertyName("subsettedFeature");
            writer.WriteStringValue(iSubsetting.SubsettedFeature);

            writer.WritePropertyName("subsettingFeature");
            writer.WriteStringValue(iSubsetting.SubsettingFeature);

            writer.WriteStartArray("target");
            foreach (var item in iSubsetting.Target)
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
