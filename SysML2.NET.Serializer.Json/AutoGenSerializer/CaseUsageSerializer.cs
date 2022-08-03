// -------------------------------------------------------------------------------------------------
// <copyright file="CaseUsageSerializer.cs" company="RHEA System S.A.">
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
    /// The purpose of the <see cref="CaseUsageSerializer"/> is to provide serialization
    /// and deserialization capabilities
    /// </summary>
    internal static class CaseUsageSerializer
    {
        /// <summary>
        /// Serializes an instance of <see cref="ICaseUsage"/> using an <see cref="Utf8JsonWriter"/>
        /// </summary>
        /// <param name="obj">
        /// The <see cref="ICaseUsage"/> to serialize
        /// </param>
        /// <param name="writer">
        /// The target <see cref="Utf8JsonWriter"/>
        /// </param>
        /// <param name="serializationModeKind">
        /// enumeration specifying what kind of serialization shall be used
        /// </param>
        internal static void Serialize(object obj, Utf8JsonWriter writer, SerializationModeKind serializationModeKind)
        {
            if (!(obj is ICaseUsage iCaseUsage))
            {
                throw new ArgumentException("The object shall be an ICaseUsage", nameof(obj));
            }

            writer.WriteStartObject();

            writer.WritePropertyName("@type");
            writer.WriteStringValue("CaseUsage");

            writer.WritePropertyName("@id");
            writer.WriteStringValue(iCaseUsage.Id);

            writer.WriteStartArray("aliasIds");
            foreach (var item in iCaseUsage.AliasIds)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("direction");
            if (iCaseUsage.Direction.HasValue)
            {
                writer.WriteStringValue(iCaseUsage.Direction.Value.ToString().ToUpper());
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("elementId");
            writer.WriteStringValue(iCaseUsage.ElementId);

            writer.WritePropertyName("isAbstract");
            writer.WriteBooleanValue(iCaseUsage.IsAbstract);

            writer.WritePropertyName("isComposite");
            writer.WriteBooleanValue(iCaseUsage.IsComposite);

            writer.WritePropertyName("isDerived");
            writer.WriteBooleanValue(iCaseUsage.IsDerived);

            writer.WritePropertyName("isEnd");
            writer.WriteBooleanValue(iCaseUsage.IsEnd);

            writer.WritePropertyName("isIndividual");
            writer.WriteBooleanValue(iCaseUsage.IsIndividual);

            writer.WritePropertyName("isOrdered");
            writer.WriteBooleanValue(iCaseUsage.IsOrdered);

            writer.WritePropertyName("isPortion");
            writer.WriteBooleanValue(iCaseUsage.IsPortion);

            writer.WritePropertyName("isReadOnly");
            writer.WriteBooleanValue(iCaseUsage.IsReadOnly);

            writer.WritePropertyName("isSufficient");
            writer.WriteBooleanValue(iCaseUsage.IsSufficient);

            writer.WritePropertyName("isUnique");
            writer.WriteBooleanValue(iCaseUsage.IsUnique);

            writer.WritePropertyName("isVariation");
            writer.WriteBooleanValue(iCaseUsage.IsVariation);

            writer.WritePropertyName("name");
            writer.WriteStringValue(iCaseUsage.Name);

            writer.WriteStartArray("ownedRelationship");
            foreach (var item in iCaseUsage.OwnedRelationship)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("owningRelationship");
            if (iCaseUsage.OwningRelationship.HasValue)
            {
                writer.WriteStringValue(iCaseUsage.OwningRelationship.Value);
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("portionKind");
            if (iCaseUsage.PortionKind.HasValue)
            {
                writer.WriteStringValue(iCaseUsage.PortionKind.Value.ToString().ToUpper());
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("shortName");
            writer.WriteStringValue(iCaseUsage.ShortName);

            writer.WriteEndObject();
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
