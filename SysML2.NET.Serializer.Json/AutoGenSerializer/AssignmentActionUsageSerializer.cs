// -------------------------------------------------------------------------------------------------
// <copyright file="AssignmentActionUsageSerializer.cs" company="RHEA System S.A.">
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
    /// The purpose of the <see cref="AssignmentActionUsageSerializer"/> is to provide serialization
    /// and deserialization capabilities
    /// </summary>
    internal static class AssignmentActionUsageSerializer
    {
        /// <summary>
        /// Serializes an instance of <see cref="IAssignmentActionUsage"/> using an <see cref="Utf8JsonWriter"/>
        /// </summary>
        /// <param name="obj">
        /// The <see cref="IAssignmentActionUsage"/> to serialize
        /// </param>
        /// <param name="writer">
        /// The target <see cref="Utf8JsonWriter"/>
        /// </param>
        /// <param name="serializationModeKind">
        /// enumeration specifying what kind of serialization shall be used
        /// </param>
        internal static void Serialize(object obj, Utf8JsonWriter writer, SerializationModeKind serializationModeKind)
        {
            if (!(obj is IAssignmentActionUsage iAssignmentActionUsage))
            {
                throw new ArgumentException("The object shall be an IAssignmentActionUsage", nameof(obj));
            }

            writer.WriteStartObject();

            writer.WritePropertyName("@type");
            writer.WriteStringValue("AssignmentActionUsage");

            writer.WritePropertyName("@id");
            writer.WriteStringValue(iAssignmentActionUsage.Id);

            writer.WriteStartArray("aliasIds");
            foreach (var item in iAssignmentActionUsage.AliasIds)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("direction");
            if (iAssignmentActionUsage.Direction.HasValue)
            {
                writer.WriteStringValue(iAssignmentActionUsage.Direction.Value.ToString().ToLower());
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("elementId");
            writer.WriteStringValue(iAssignmentActionUsage.ElementId);

            writer.WritePropertyName("isAbstract");
            writer.WriteBooleanValue(iAssignmentActionUsage.IsAbstract);

            writer.WritePropertyName("isComposite");
            writer.WriteBooleanValue(iAssignmentActionUsage.IsComposite);

            writer.WritePropertyName("isDerived");
            writer.WriteBooleanValue(iAssignmentActionUsage.IsDerived);

            writer.WritePropertyName("isEnd");
            writer.WriteBooleanValue(iAssignmentActionUsage.IsEnd);

            writer.WritePropertyName("isImpliedIncluded");
            writer.WriteBooleanValue(iAssignmentActionUsage.IsImpliedIncluded);

            writer.WritePropertyName("isIndividual");
            writer.WriteBooleanValue(iAssignmentActionUsage.IsIndividual);

            writer.WritePropertyName("isOrdered");
            writer.WriteBooleanValue(iAssignmentActionUsage.IsOrdered);

            writer.WritePropertyName("isPortion");
            writer.WriteBooleanValue(iAssignmentActionUsage.IsPortion);

            writer.WritePropertyName("isReadOnly");
            writer.WriteBooleanValue(iAssignmentActionUsage.IsReadOnly);

            writer.WritePropertyName("isSufficient");
            writer.WriteBooleanValue(iAssignmentActionUsage.IsSufficient);

            writer.WritePropertyName("isUnique");
            writer.WriteBooleanValue(iAssignmentActionUsage.IsUnique);

            writer.WritePropertyName("isVariation");
            writer.WriteBooleanValue(iAssignmentActionUsage.IsVariation);

            writer.WritePropertyName("name");
            writer.WriteStringValue(iAssignmentActionUsage.Name);

            writer.WriteStartArray("ownedRelationship");
            foreach (var item in iAssignmentActionUsage.OwnedRelationship)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("owningRelationship");
            if (iAssignmentActionUsage.OwningRelationship.HasValue)
            {
                writer.WriteStringValue(iAssignmentActionUsage.OwningRelationship.Value);
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("portionKind");
            if (iAssignmentActionUsage.PortionKind.HasValue)
            {
                writer.WriteStringValue(iAssignmentActionUsage.PortionKind.Value.ToString().ToLower());
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("shortName");
            writer.WriteStringValue(iAssignmentActionUsage.ShortName);

            writer.WriteEndObject();
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
