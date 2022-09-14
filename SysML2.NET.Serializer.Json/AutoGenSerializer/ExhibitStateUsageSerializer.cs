// -------------------------------------------------------------------------------------------------
// <copyright file="ExhibitStateUsageSerializer.cs" company="RHEA System S.A.">
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
    /// The purpose of the <see cref="ExhibitStateUsageSerializer"/> is to provide serialization
    /// and deserialization capabilities
    /// </summary>
    internal static class ExhibitStateUsageSerializer
    {
        /// <summary>
        /// Serializes an instance of <see cref="IExhibitStateUsage"/> using an <see cref="Utf8JsonWriter"/>
        /// </summary>
        /// <param name="obj">
        /// The <see cref="IExhibitStateUsage"/> to serialize
        /// </param>
        /// <param name="writer">
        /// The target <see cref="Utf8JsonWriter"/>
        /// </param>
        /// <param name="serializationModeKind">
        /// enumeration specifying what kind of serialization shall be used
        /// </param>
        internal static void Serialize(object obj, Utf8JsonWriter writer, SerializationModeKind serializationModeKind)
        {
            if (!(obj is IExhibitStateUsage iExhibitStateUsage))
            {
                throw new ArgumentException("The object shall be an IExhibitStateUsage", nameof(obj));
            }

            writer.WriteStartObject();

            writer.WritePropertyName("@type");
            writer.WriteStringValue("ExhibitStateUsage");

            writer.WritePropertyName("@id");
            writer.WriteStringValue(iExhibitStateUsage.Id);

            writer.WriteStartArray("aliasIds");
            foreach (var item in iExhibitStateUsage.AliasIds)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("direction");
            if (iExhibitStateUsage.Direction.HasValue)
            {
                writer.WriteStringValue(iExhibitStateUsage.Direction.Value.ToString().ToUpper());
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("elementId");
            writer.WriteStringValue(iExhibitStateUsage.ElementId);

            writer.WritePropertyName("isAbstract");
            writer.WriteBooleanValue(iExhibitStateUsage.IsAbstract);

            writer.WritePropertyName("isComposite");
            writer.WriteBooleanValue(iExhibitStateUsage.IsComposite);

            writer.WritePropertyName("isDerived");
            writer.WriteBooleanValue(iExhibitStateUsage.IsDerived);

            writer.WritePropertyName("isEnd");
            writer.WriteBooleanValue(iExhibitStateUsage.IsEnd);

            writer.WritePropertyName("isImpliedIncluded");
            writer.WriteBooleanValue(iExhibitStateUsage.IsImpliedIncluded);

            writer.WritePropertyName("isIndividual");
            writer.WriteBooleanValue(iExhibitStateUsage.IsIndividual);

            writer.WritePropertyName("isOrdered");
            writer.WriteBooleanValue(iExhibitStateUsage.IsOrdered);

            writer.WritePropertyName("isParallel");
            writer.WriteBooleanValue(iExhibitStateUsage.IsParallel);

            writer.WritePropertyName("isPortion");
            writer.WriteBooleanValue(iExhibitStateUsage.IsPortion);

            writer.WritePropertyName("isReadOnly");
            writer.WriteBooleanValue(iExhibitStateUsage.IsReadOnly);

            writer.WritePropertyName("isSufficient");
            writer.WriteBooleanValue(iExhibitStateUsage.IsSufficient);

            writer.WritePropertyName("isUnique");
            writer.WriteBooleanValue(iExhibitStateUsage.IsUnique);

            writer.WritePropertyName("isVariation");
            writer.WriteBooleanValue(iExhibitStateUsage.IsVariation);

            writer.WritePropertyName("name");
            writer.WriteStringValue(iExhibitStateUsage.Name);

            writer.WriteStartArray("ownedRelationship");
            foreach (var item in iExhibitStateUsage.OwnedRelationship)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("owningRelationship");
            if (iExhibitStateUsage.OwningRelationship.HasValue)
            {
                writer.WriteStringValue(iExhibitStateUsage.OwningRelationship.Value);
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("portionKind");
            if (iExhibitStateUsage.PortionKind.HasValue)
            {
                writer.WriteStringValue(iExhibitStateUsage.PortionKind.Value.ToString().ToUpper());
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("shortName");
            writer.WriteStringValue(iExhibitStateUsage.ShortName);

            writer.WriteEndObject();
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
