// -------------------------------------------------------------------------------------------------
// <copyright file="RedefinitionSerializer.cs" company="RHEA System S.A.">
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
    /// The purpose of the <see cref="RedefinitionSerializer"/> is to provide serialization
    /// and deserialization capabilities
    /// </summary>
    internal static class RedefinitionSerializer
    {
        /// <summary>
        /// Serializes an instance of <see cref="IRedefinition"/> using an <see cref="Utf8JsonWriter"/>
        /// </summary>
        /// <param name="obj">
        /// The <see cref="IRedefinition"/> to serialize
        /// </param>
        /// <param name="writer">
        /// The target <see cref="Utf8JsonWriter"/>
        /// </param>
        /// <param name="serializationModeKind">
        /// enumeration specifying what kind of serialization shall be used
        /// </param>
        internal static void Serialize(object obj, Utf8JsonWriter writer, SerializationModeKind serializationModeKind)
        {
            if (!(obj is IRedefinition iRedefinition))
            {
                throw new ArgumentException("The object shall be an IRedefinition", nameof(obj));
            }

            writer.WriteStartObject();

            writer.WritePropertyName("@type");
            writer.WriteStringValue("Redefinition");

            writer.WritePropertyName("@id");
            writer.WriteStringValue(iRedefinition.Id);

            writer.WriteStartArray("aliasIds");
            foreach (var item in iRedefinition.AliasIds)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("declaredName");
            writer.WriteStringValue(iRedefinition.DeclaredName);
            writer.WritePropertyName("declaredShortName");
            writer.WriteStringValue(iRedefinition.DeclaredShortName);
            writer.WritePropertyName("elementId");
            writer.WriteStringValue(iRedefinition.ElementId);

            writer.WritePropertyName("general");
            writer.WriteStringValue(iRedefinition.General);

            writer.WritePropertyName("isImplied");
            writer.WriteBooleanValue(iRedefinition.IsImplied);

            writer.WritePropertyName("isImpliedIncluded");
            writer.WriteBooleanValue(iRedefinition.IsImpliedIncluded);

            writer.WriteStartArray("ownedRelatedElement");
            foreach (var item in iRedefinition.OwnedRelatedElement)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WriteStartArray("ownedRelationship");
            foreach (var item in iRedefinition.OwnedRelationship)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("owningRelatedElement");
            if (iRedefinition.OwningRelatedElement.HasValue)
            {
                writer.WriteStringValue(iRedefinition.OwningRelatedElement.Value);
            }
            else
            {
                writer.WriteNullValue();
            }
            writer.WritePropertyName("owningRelationship");
            if (iRedefinition.OwningRelationship.HasValue)
            {
                writer.WriteStringValue(iRedefinition.OwningRelationship.Value);
            }
            else
            {
                writer.WriteNullValue();
            }
            writer.WritePropertyName("redefinedFeature");
            writer.WriteStringValue(iRedefinition.RedefinedFeature);

            writer.WritePropertyName("redefiningFeature");
            writer.WriteStringValue(iRedefinition.RedefiningFeature);

            writer.WriteStartArray("source");
            foreach (var item in iRedefinition.Source)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("specific");
            writer.WriteStringValue(iRedefinition.Specific);

            writer.WritePropertyName("subsettedFeature");
            writer.WriteStringValue(iRedefinition.SubsettedFeature);

            writer.WritePropertyName("subsettingFeature");
            writer.WriteStringValue(iRedefinition.SubsettingFeature);

            writer.WriteStartArray("target");
            foreach (var item in iRedefinition.Target)
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
