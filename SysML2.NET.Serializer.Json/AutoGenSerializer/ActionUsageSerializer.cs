﻿// -------------------------------------------------------------------------------------------------
// <copyright file="ActionUsageSerializer.cs" company="RHEA System S.A.">
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
    /// The purpose of the <see cref="ActionUsageSerializer"/> is to provide serialization
    /// and deserialization capabilities
    /// </summary>
    internal static class ActionUsageSerializer
    {
        /// <summary>
        /// Serializes an instance of <see cref="IActionUsage"/> using an <see cref="Utf8JsonWriter"/>
        /// </summary>
        /// <param name="obj">
        /// The <see cref="IActionUsage"/> to serialize
        /// </param>
        /// <param name="writer">
        /// The target <see cref="Utf8JsonWriter"/>
        /// </param>
        /// <param name="serializationModeKind">
        /// enumeration specifying what kind of serialization shall be used
        /// </param>
        internal static void Serialize(object obj, Utf8JsonWriter writer, SerializationModeKind serializationModeKind)
        {
            if (!(obj is IActionUsage iActionUsage))
            {
                throw new ArgumentException("The object shall be an IActionUsage", nameof(obj));
            }

            writer.WriteStartObject();

            writer.WritePropertyName("@type");
            writer.WriteStringValue("ActionUsage");

            writer.WritePropertyName("@id");
            writer.WriteStringValue(iActionUsage.Id);

            writer.WriteStartArray("aliasIds");
            foreach (var item in iActionUsage.AliasIds)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("declaredName");
            writer.WriteStringValue(iActionUsage.DeclaredName);
            writer.WritePropertyName("declaredShortName");
            writer.WriteStringValue(iActionUsage.DeclaredShortName);
            writer.WritePropertyName("direction");
            if (iActionUsage.Direction.HasValue)
            {
                writer.WriteStringValue(iActionUsage.Direction.Value.ToString().ToLower());
            }
            else
            {
                writer.WriteNullValue();
            }
            writer.WritePropertyName("elementId");
            writer.WriteStringValue(iActionUsage.ElementId);

            writer.WritePropertyName("isAbstract");
            writer.WriteBooleanValue(iActionUsage.IsAbstract);

            writer.WritePropertyName("isComposite");
            writer.WriteBooleanValue(iActionUsage.IsComposite);

            writer.WritePropertyName("isDerived");
            writer.WriteBooleanValue(iActionUsage.IsDerived);

            writer.WritePropertyName("isEnd");
            writer.WriteBooleanValue(iActionUsage.IsEnd);

            writer.WritePropertyName("isImpliedIncluded");
            writer.WriteBooleanValue(iActionUsage.IsImpliedIncluded);

            writer.WritePropertyName("isIndividual");
            writer.WriteBooleanValue(iActionUsage.IsIndividual);

            writer.WritePropertyName("isOrdered");
            writer.WriteBooleanValue(iActionUsage.IsOrdered);

            writer.WritePropertyName("isPortion");
            writer.WriteBooleanValue(iActionUsage.IsPortion);

            writer.WritePropertyName("isReadOnly");
            writer.WriteBooleanValue(iActionUsage.IsReadOnly);

            writer.WritePropertyName("isSufficient");
            writer.WriteBooleanValue(iActionUsage.IsSufficient);

            writer.WritePropertyName("isUnique");
            writer.WriteBooleanValue(iActionUsage.IsUnique);

            writer.WritePropertyName("isVariation");
            writer.WriteBooleanValue(iActionUsage.IsVariation);

            writer.WriteStartArray("ownedRelationship");
            foreach (var item in iActionUsage.OwnedRelationship)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("owningRelationship");
            if (iActionUsage.OwningRelationship.HasValue)
            {
                writer.WriteStringValue(iActionUsage.OwningRelationship.Value);
            }
            else
            {
                writer.WriteNullValue();
            }
            writer.WritePropertyName("portionKind");
            if (iActionUsage.PortionKind.HasValue)
            {
                writer.WriteStringValue(iActionUsage.PortionKind.Value.ToString().ToLower());
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
