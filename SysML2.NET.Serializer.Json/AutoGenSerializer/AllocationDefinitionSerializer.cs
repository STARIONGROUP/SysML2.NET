// -------------------------------------------------------------------------------------------------
// <copyright file="AllocationDefinitionSerializer.cs" company="RHEA System S.A.">
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
    /// The purpose of the <see cref="AllocationDefinitionSerializer"/> is to provide serialization
    /// and deserialization capabilities
    /// </summary>
    internal static class AllocationDefinitionSerializer
    {
        /// <summary>
        /// Serializes an instance of <see cref="IAllocationDefinition"/> using an <see cref="Utf8JsonWriter"/>
        /// </summary>
        /// <param name="obj">
        /// The <see cref="IAllocationDefinition"/> to serialize
        /// </param>
        /// <param name="writer">
        /// The target <see cref="Utf8JsonWriter"/>
        /// </param>
        /// <param name="serializationModeKind">
        /// enumeration specifying what kind of serialization shall be used
        /// </param>
        internal static void Serialize(object obj, Utf8JsonWriter writer, SerializationModeKind serializationModeKind)
        {
            if (!(obj is IAllocationDefinition iAllocationDefinition))
            {
                throw new ArgumentException("The object shall be an IAllocationDefinition", nameof(obj));
            }

            writer.WriteStartObject();

            writer.WritePropertyName("@type");
            writer.WriteStringValue("AllocationDefinition");

            writer.WritePropertyName("@id");
            writer.WriteStringValue(iAllocationDefinition.Id);

            writer.WriteStartArray("aliasIds");
            foreach (var item in iAllocationDefinition.AliasIds)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("declaredName");
            writer.WriteStringValue(iAllocationDefinition.DeclaredName);
            writer.WritePropertyName("declaredShortName");
            writer.WriteStringValue(iAllocationDefinition.DeclaredShortName);
            writer.WritePropertyName("elementId");
            writer.WriteStringValue(iAllocationDefinition.ElementId);

            writer.WritePropertyName("isAbstract");
            writer.WriteBooleanValue(iAllocationDefinition.IsAbstract);

            writer.WritePropertyName("isImplied");
            writer.WriteBooleanValue(iAllocationDefinition.IsImplied);

            writer.WritePropertyName("isImpliedIncluded");
            writer.WriteBooleanValue(iAllocationDefinition.IsImpliedIncluded);

            writer.WritePropertyName("isIndividual");
            writer.WriteBooleanValue(iAllocationDefinition.IsIndividual);

            writer.WritePropertyName("isSufficient");
            writer.WriteBooleanValue(iAllocationDefinition.IsSufficient);

            writer.WritePropertyName("isVariation");
            writer.WriteBooleanValue(iAllocationDefinition.IsVariation);

            writer.WriteStartArray("ownedRelatedElement");
            foreach (var item in iAllocationDefinition.OwnedRelatedElement)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WriteStartArray("ownedRelationship");
            foreach (var item in iAllocationDefinition.OwnedRelationship)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("owningRelatedElement");
            if (iAllocationDefinition.OwningRelatedElement.HasValue)
            {
                writer.WriteStringValue(iAllocationDefinition.OwningRelatedElement.Value);
            }
            else
            {
                writer.WriteNullValue();
            }
            writer.WritePropertyName("owningRelationship");
            if (iAllocationDefinition.OwningRelationship.HasValue)
            {
                writer.WriteStringValue(iAllocationDefinition.OwningRelationship.Value);
            }
            else
            {
                writer.WriteNullValue();
            }
            writer.WriteStartArray("source");
            foreach (var item in iAllocationDefinition.Source)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WriteStartArray("target");
            foreach (var item in iAllocationDefinition.Target)
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
