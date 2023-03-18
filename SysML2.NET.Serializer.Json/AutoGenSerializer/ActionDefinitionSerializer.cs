// -------------------------------------------------------------------------------------------------
// <copyright file="ActionDefinitionSerializer.cs" company="RHEA System S.A.">
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
    /// The purpose of the <see cref="ActionDefinitionSerializer"/> is to provide serialization
    /// and deserialization capabilities
    /// </summary>
    internal static class ActionDefinitionSerializer
    {
        /// <summary>
        /// Serializes an instance of <see cref="IActionDefinition"/> using an <see cref="Utf8JsonWriter"/>
        /// </summary>
        /// <param name="obj">
        /// The <see cref="IActionDefinition"/> to serialize
        /// </param>
        /// <param name="writer">
        /// The target <see cref="Utf8JsonWriter"/>
        /// </param>
        /// <param name="serializationModeKind">
        /// enumeration specifying what kind of serialization shall be used
        /// </param>
        internal static void Serialize(object obj, Utf8JsonWriter writer, SerializationModeKind serializationModeKind)
        {
            if (!(obj is IActionDefinition iActionDefinition))
            {
                throw new ArgumentException("The object shall be an IActionDefinition", nameof(obj));
            }

            writer.WriteStartObject();

            writer.WritePropertyName("@type");
            writer.WriteStringValue("ActionDefinition");

            writer.WritePropertyName("@id");
            writer.WriteStringValue(iActionDefinition.Id);

            writer.WriteStartArray("aliasIds");
            foreach (var item in iActionDefinition.AliasIds)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("declaredName");
            writer.WriteStringValue(iActionDefinition.DeclaredName);
            writer.WritePropertyName("declaredShortName");
            writer.WriteStringValue(iActionDefinition.DeclaredShortName);
            writer.WritePropertyName("elementId");
            writer.WriteStringValue(iActionDefinition.ElementId);

            writer.WritePropertyName("isAbstract");
            writer.WriteBooleanValue(iActionDefinition.IsAbstract);

            writer.WritePropertyName("isImpliedIncluded");
            writer.WriteBooleanValue(iActionDefinition.IsImpliedIncluded);

            writer.WritePropertyName("isIndividual");
            writer.WriteBooleanValue(iActionDefinition.IsIndividual);

            writer.WritePropertyName("isSufficient");
            writer.WriteBooleanValue(iActionDefinition.IsSufficient);

            writer.WritePropertyName("isVariation");
            writer.WriteBooleanValue(iActionDefinition.IsVariation);

            writer.WriteStartArray("ownedRelationship");
            foreach (var item in iActionDefinition.OwnedRelationship)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("owningRelationship");
            if (iActionDefinition.OwningRelationship.HasValue)
            {
                writer.WriteStringValue(iActionDefinition.OwningRelationship.Value);
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
