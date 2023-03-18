// -------------------------------------------------------------------------------------------------
// <copyright file="RenderingUsageSerializer.cs" company="RHEA System S.A.">
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
    /// The purpose of the <see cref="RenderingUsageSerializer"/> is to provide serialization
    /// and deserialization capabilities
    /// </summary>
    internal static class RenderingUsageSerializer
    {
        /// <summary>
        /// Serializes an instance of <see cref="IRenderingUsage"/> using an <see cref="Utf8JsonWriter"/>
        /// </summary>
        /// <param name="obj">
        /// The <see cref="IRenderingUsage"/> to serialize
        /// </param>
        /// <param name="writer">
        /// The target <see cref="Utf8JsonWriter"/>
        /// </param>
        /// <param name="serializationModeKind">
        /// enumeration specifying what kind of serialization shall be used
        /// </param>
        internal static void Serialize(object obj, Utf8JsonWriter writer, SerializationModeKind serializationModeKind)
        {
            if (!(obj is IRenderingUsage iRenderingUsage))
            {
                throw new ArgumentException("The object shall be an IRenderingUsage", nameof(obj));
            }

            writer.WriteStartObject();

            writer.WritePropertyName("@type");
            writer.WriteStringValue("RenderingUsage");

            writer.WritePropertyName("@id");
            writer.WriteStringValue(iRenderingUsage.Id);

            writer.WriteStartArray("aliasIds");
            foreach (var item in iRenderingUsage.AliasIds)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("declaredName");
            writer.WriteStringValue(iRenderingUsage.DeclaredName);
            writer.WritePropertyName("declaredShortName");
            writer.WriteStringValue(iRenderingUsage.DeclaredShortName);
            writer.WritePropertyName("direction");
            if (iRenderingUsage.Direction.HasValue)
            {
                writer.WriteStringValue(iRenderingUsage.Direction.Value.ToString().ToLower());
            }
            else
            {
                writer.WriteNullValue();
            }
            writer.WritePropertyName("elementId");
            writer.WriteStringValue(iRenderingUsage.ElementId);

            writer.WritePropertyName("isAbstract");
            writer.WriteBooleanValue(iRenderingUsage.IsAbstract);

            writer.WritePropertyName("isComposite");
            writer.WriteBooleanValue(iRenderingUsage.IsComposite);

            writer.WritePropertyName("isDerived");
            writer.WriteBooleanValue(iRenderingUsage.IsDerived);

            writer.WritePropertyName("isEnd");
            writer.WriteBooleanValue(iRenderingUsage.IsEnd);

            writer.WritePropertyName("isImpliedIncluded");
            writer.WriteBooleanValue(iRenderingUsage.IsImpliedIncluded);

            writer.WritePropertyName("isIndividual");
            writer.WriteBooleanValue(iRenderingUsage.IsIndividual);

            writer.WritePropertyName("isOrdered");
            writer.WriteBooleanValue(iRenderingUsage.IsOrdered);

            writer.WritePropertyName("isPortion");
            writer.WriteBooleanValue(iRenderingUsage.IsPortion);

            writer.WritePropertyName("isReadOnly");
            writer.WriteBooleanValue(iRenderingUsage.IsReadOnly);

            writer.WritePropertyName("isSufficient");
            writer.WriteBooleanValue(iRenderingUsage.IsSufficient);

            writer.WritePropertyName("isUnique");
            writer.WriteBooleanValue(iRenderingUsage.IsUnique);

            writer.WritePropertyName("isVariation");
            writer.WriteBooleanValue(iRenderingUsage.IsVariation);

            writer.WriteStartArray("ownedRelationship");
            foreach (var item in iRenderingUsage.OwnedRelationship)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("owningRelationship");
            if (iRenderingUsage.OwningRelationship.HasValue)
            {
                writer.WriteStringValue(iRenderingUsage.OwningRelationship.Value);
            }
            else
            {
                writer.WriteNullValue();
            }
            writer.WritePropertyName("portionKind");
            if (iRenderingUsage.PortionKind.HasValue)
            {
                writer.WriteStringValue(iRenderingUsage.PortionKind.Value.ToString().ToLower());
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
