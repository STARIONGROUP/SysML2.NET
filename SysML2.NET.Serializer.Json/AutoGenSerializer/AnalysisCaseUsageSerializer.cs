// -------------------------------------------------------------------------------------------------
// <copyright file="AnalysisCaseUsageSerializer.cs" company="RHEA System S.A.">
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
    /// The purpose of the <see cref="AnalysisCaseUsageSerializer"/> is to provide serialization
    /// and deserialization capabilities
    /// </summary>
    internal static class AnalysisCaseUsageSerializer
    {
        /// <summary>
        /// Serializes an instance of <see cref="IAnalysisCaseUsage"/> using an <see cref="Utf8JsonWriter"/>
        /// </summary>
        /// <param name="obj">
        /// The <see cref="IAnalysisCaseUsage"/> to serialize
        /// </param>
        /// <param name="writer">
        /// The target <see cref="Utf8JsonWriter"/>
        /// </param>
        /// <param name="serializationModeKind">
        /// enumeration specifying what kind of serialization shall be used
        /// </param>
        internal static void Serialize(object obj, Utf8JsonWriter writer, SerializationModeKind serializationModeKind)
        {
            if (!(obj is IAnalysisCaseUsage iAnalysisCaseUsage))
            {
                throw new ArgumentException("The object shall be an IAnalysisCaseUsage", nameof(obj));
            }

            writer.WriteStartObject();

            writer.WritePropertyName("@type");
            writer.WriteStringValue("AnalysisCaseUsage");

            writer.WritePropertyName("@id");
            writer.WriteStringValue(iAnalysisCaseUsage.Id);

            writer.WriteStartArray("aliasIds");
            foreach (var item in iAnalysisCaseUsage.AliasIds)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("direction");
            if (iAnalysisCaseUsage.Direction.HasValue)
            {
                writer.WriteStringValue(iAnalysisCaseUsage.Direction.Value.ToString().ToLower());
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("elementId");
            writer.WriteStringValue(iAnalysisCaseUsage.ElementId);

            writer.WritePropertyName("isAbstract");
            writer.WriteBooleanValue(iAnalysisCaseUsage.IsAbstract);

            writer.WritePropertyName("isComposite");
            writer.WriteBooleanValue(iAnalysisCaseUsage.IsComposite);

            writer.WritePropertyName("isDerived");
            writer.WriteBooleanValue(iAnalysisCaseUsage.IsDerived);

            writer.WritePropertyName("isEnd");
            writer.WriteBooleanValue(iAnalysisCaseUsage.IsEnd);

            writer.WritePropertyName("isImpliedIncluded");
            writer.WriteBooleanValue(iAnalysisCaseUsage.IsImpliedIncluded);

            writer.WritePropertyName("isIndividual");
            writer.WriteBooleanValue(iAnalysisCaseUsage.IsIndividual);

            writer.WritePropertyName("isOrdered");
            writer.WriteBooleanValue(iAnalysisCaseUsage.IsOrdered);

            writer.WritePropertyName("isPortion");
            writer.WriteBooleanValue(iAnalysisCaseUsage.IsPortion);

            writer.WritePropertyName("isReadOnly");
            writer.WriteBooleanValue(iAnalysisCaseUsage.IsReadOnly);

            writer.WritePropertyName("isSufficient");
            writer.WriteBooleanValue(iAnalysisCaseUsage.IsSufficient);

            writer.WritePropertyName("isUnique");
            writer.WriteBooleanValue(iAnalysisCaseUsage.IsUnique);

            writer.WritePropertyName("isVariation");
            writer.WriteBooleanValue(iAnalysisCaseUsage.IsVariation);

            writer.WritePropertyName("name");
            writer.WriteStringValue(iAnalysisCaseUsage.Name);

            writer.WriteStartArray("ownedRelationship");
            foreach (var item in iAnalysisCaseUsage.OwnedRelationship)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("owningRelationship");
            if (iAnalysisCaseUsage.OwningRelationship.HasValue)
            {
                writer.WriteStringValue(iAnalysisCaseUsage.OwningRelationship.Value);
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("portionKind");
            if (iAnalysisCaseUsage.PortionKind.HasValue)
            {
                writer.WriteStringValue(iAnalysisCaseUsage.PortionKind.Value.ToString().ToLower());
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("shortName");
            writer.WriteStringValue(iAnalysisCaseUsage.ShortName);

            writer.WriteEndObject();
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
