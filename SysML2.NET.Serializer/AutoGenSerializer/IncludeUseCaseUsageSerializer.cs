// -------------------------------------------------------------------------------------------------
// <copyright file="IncludeUseCaseUsageSerializer.cs" company="RHEA System S.A.">
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

namespace SysML2.NET.Serializer
{
    using System.Text.Json;

    using SysML2.NET.DTO;

    /// <summary>
    /// The purpose of the <see cref="IncludeUseCaseUsageSerializer"/> is to provide serialization
    /// and deserialization capabilities
    /// </summary>
    internal static class IncludeUseCaseUsageSerializer
    {
        /// <summary>
        /// Serializes an instance of <see cref="IIncludeUseCaseUsage"/> using an <see cref="Utf8JsonWriter"/>
        /// </summary>
        /// <param name="includeUseCaseUsage">
        /// The <see cref="IIncludeUseCaseUsage"/> to serialize
        /// </param>
        /// <param name="writer">
        /// The target <see cref="Utf8JsonWriter"/>
        /// </param>
        /// <param name="serializationModeKind">
        /// enumeration specifying what kind of serialization shall be used
        /// </param>
        public static void Serialize(IIncludeUseCaseUsage iIncludeUseCaseUsage, Utf8JsonWriter writer, SerializationModeKind serializationModeKind)
        {
            writer.WriteStartObject();

            writer.WritePropertyName("@id");
            writer.WriteStringValue(iIncludeUseCaseUsage.Id);

            writer.WritePropertyName("@type");
            writer.WriteStringValue("IncludeUseCaseUsage");

            writer.WritePropertyName("isIndividual");
            writer.WriteBooleanValue(iIncludeUseCaseUsage.IsIndividual);

            writer.WritePropertyName("portionKind");
            if (iIncludeUseCaseUsage.PortionKind.HasValue)
            {
                writer.WriteStringValue(iIncludeUseCaseUsage.PortionKind.Value.ToString().ToUpper());
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("isVariation");
            writer.WriteBooleanValue(iIncludeUseCaseUsage.IsVariation);

            writer.WritePropertyName("direction");
            if (iIncludeUseCaseUsage.Direction.HasValue)
            {
                writer.WriteStringValue(iIncludeUseCaseUsage.Direction.Value.ToString().ToUpper());
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("isComposite");
            writer.WriteBooleanValue(iIncludeUseCaseUsage.IsComposite);

            writer.WritePropertyName("isDerived");
            writer.WriteBooleanValue(iIncludeUseCaseUsage.IsDerived);

            writer.WritePropertyName("isEnd");
            writer.WriteBooleanValue(iIncludeUseCaseUsage.IsEnd);

            writer.WritePropertyName("isOrdered");
            writer.WriteBooleanValue(iIncludeUseCaseUsage.IsOrdered);

            writer.WritePropertyName("isPortion");
            writer.WriteBooleanValue(iIncludeUseCaseUsage.IsPortion);

            writer.WritePropertyName("isReadOnly");
            writer.WriteBooleanValue(iIncludeUseCaseUsage.IsReadOnly);

            writer.WritePropertyName("isUnique");
            writer.WriteBooleanValue(iIncludeUseCaseUsage.IsUnique);

            writer.WritePropertyName("isAbstract");
            writer.WriteBooleanValue(iIncludeUseCaseUsage.IsAbstract);

            writer.WritePropertyName("isSufficient");
            writer.WriteBooleanValue(iIncludeUseCaseUsage.IsSufficient);

            writer.WriteStartArray("aliasIds");
            foreach (var item in iIncludeUseCaseUsage.AliasIds)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("elementId");
            writer.WriteStringValue(iIncludeUseCaseUsage.ElementId);

            writer.WritePropertyName("name");
            writer.WriteStringValue(iIncludeUseCaseUsage.Name);

            writer.WriteStartArray("ownedRelationship");
            foreach (var item in iIncludeUseCaseUsage.OwnedRelationship)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("owningRelationship");
            if (iIncludeUseCaseUsage.OwningRelationship.HasValue)
            {
                writer.WriteStringValue(iIncludeUseCaseUsage.OwningRelationship.Value);
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("shortName");
            writer.WriteStringValue(iIncludeUseCaseUsage.ShortName);

            writer.WriteEndObject();
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
