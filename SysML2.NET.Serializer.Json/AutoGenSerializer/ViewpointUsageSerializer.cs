// -------------------------------------------------------------------------------------------------
// <copyright file="ViewpointUsageSerializer.cs" company="RHEA System S.A.">
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

    using SysML2.NET.Common;
    using SysML2.NET.Core.DTO;

    /// <summary>
    /// The purpose of the <see cref="ViewpointUsageSerializer"/> is to provide serialization
    /// and deserialization capabilities
    /// </summary>
    internal static class ViewpointUsageSerializer
    {
        /// <summary>
        /// Serializes an instance of <see cref="IViewpointUsage"/> using an <see cref="Utf8JsonWriter"/>
        /// </summary>
        /// <param name="obj">
        /// The <see cref="IViewpointUsage"/> to serialize
        /// </param>
        /// <param name="writer">
        /// The target <see cref="Utf8JsonWriter"/>
        /// </param>
        /// <param name="serializationModeKind">
        /// enumeration specifying what kind of serialization shall be used
        /// </param>
        internal static void Serialize(object obj, Utf8JsonWriter writer, SerializationModeKind serializationModeKind)
        {
            if (!(obj is IViewpointUsage iViewpointUsage))
            {
                throw new ArgumentException("The object shall be an IViewpointUsage", nameof(obj));
            }

            writer.WriteStartObject();

            writer.WritePropertyName("@type");
            writer.WriteStringValue("ViewpointUsage");

            writer.WritePropertyName("@id");
            writer.WriteStringValue(iViewpointUsage.Id);

            writer.WriteStartArray("aliasIds");
            foreach (var item in iViewpointUsage.AliasIds)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("direction");
            if (iViewpointUsage.Direction.HasValue)
            {
                writer.WriteStringValue(iViewpointUsage.Direction.Value.ToString().ToUpper());
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("elementId");
            writer.WriteStringValue(iViewpointUsage.ElementId);

            writer.WritePropertyName("isAbstract");
            writer.WriteBooleanValue(iViewpointUsage.IsAbstract);

            writer.WritePropertyName("isComposite");
            writer.WriteBooleanValue(iViewpointUsage.IsComposite);

            writer.WritePropertyName("isDerived");
            writer.WriteBooleanValue(iViewpointUsage.IsDerived);

            writer.WritePropertyName("isEnd");
            writer.WriteBooleanValue(iViewpointUsage.IsEnd);

            writer.WritePropertyName("isIndividual");
            writer.WriteBooleanValue(iViewpointUsage.IsIndividual);

            writer.WritePropertyName("isOrdered");
            writer.WriteBooleanValue(iViewpointUsage.IsOrdered);

            writer.WritePropertyName("isPortion");
            writer.WriteBooleanValue(iViewpointUsage.IsPortion);

            writer.WritePropertyName("isReadOnly");
            writer.WriteBooleanValue(iViewpointUsage.IsReadOnly);

            writer.WritePropertyName("isSufficient");
            writer.WriteBooleanValue(iViewpointUsage.IsSufficient);

            writer.WritePropertyName("isUnique");
            writer.WriteBooleanValue(iViewpointUsage.IsUnique);

            writer.WritePropertyName("isVariation");
            writer.WriteBooleanValue(iViewpointUsage.IsVariation);

            writer.WritePropertyName("name");
            writer.WriteStringValue(iViewpointUsage.Name);

            writer.WriteStartArray("ownedRelationship");
            foreach (var item in iViewpointUsage.OwnedRelationship)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("owningRelationship");
            if (iViewpointUsage.OwningRelationship.HasValue)
            {
                writer.WriteStringValue(iViewpointUsage.OwningRelationship.Value);
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("portionKind");
            if (iViewpointUsage.PortionKind.HasValue)
            {
                writer.WriteStringValue(iViewpointUsage.PortionKind.Value.ToString().ToUpper());
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("reqId");
            writer.WriteStringValue(iViewpointUsage.ReqId);

            writer.WritePropertyName("shortName");
            writer.WriteStringValue(iViewpointUsage.ShortName);

            writer.WriteEndObject();
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
