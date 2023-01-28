// -------------------------------------------------------------------------------------------------
// <copyright file="EnumerationUsageSerializer.cs" company="RHEA System S.A.">
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
    /// The purpose of the <see cref="EnumerationUsageSerializer"/> is to provide serialization
    /// and deserialization capabilities
    /// </summary>
    internal static class EnumerationUsageSerializer
    {
        /// <summary>
        /// Serializes an instance of <see cref="IEnumerationUsage"/> using an <see cref="Utf8JsonWriter"/>
        /// </summary>
        /// <param name="obj">
        /// The <see cref="IEnumerationUsage"/> to serialize
        /// </param>
        /// <param name="writer">
        /// The target <see cref="Utf8JsonWriter"/>
        /// </param>
        /// <param name="serializationModeKind">
        /// enumeration specifying what kind of serialization shall be used
        /// </param>
        internal static void Serialize(object obj, Utf8JsonWriter writer, SerializationModeKind serializationModeKind)
        {
            if (!(obj is IEnumerationUsage iEnumerationUsage))
            {
                throw new ArgumentException("The object shall be an IEnumerationUsage", nameof(obj));
            }

            writer.WriteStartObject();

            writer.WritePropertyName("@type");
            writer.WriteStringValue("EnumerationUsage");

            writer.WritePropertyName("@id");
            writer.WriteStringValue(iEnumerationUsage.Id);

            writer.WriteStartArray("aliasIds");
            foreach (var item in iEnumerationUsage.AliasIds)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("declaredName");
            writer.WriteStringValue(iEnumerationUsage.DeclaredName);
            writer.WritePropertyName("declaredShortName");
            writer.WriteStringValue(iEnumerationUsage.DeclaredShortName);
            writer.WritePropertyName("direction");
            if (iEnumerationUsage.Direction.HasValue)
            {
                writer.WriteStringValue(iEnumerationUsage.Direction.Value.ToString().ToLower());
            }
            else
            {
                writer.WriteNullValue();
            }
            writer.WritePropertyName("elementId");
            writer.WriteStringValue(iEnumerationUsage.ElementId);

            writer.WritePropertyName("isAbstract");
            writer.WriteBooleanValue(iEnumerationUsage.IsAbstract);

            writer.WritePropertyName("isComposite");
            writer.WriteBooleanValue(iEnumerationUsage.IsComposite);

            writer.WritePropertyName("isDerived");
            writer.WriteBooleanValue(iEnumerationUsage.IsDerived);

            writer.WritePropertyName("isEnd");
            writer.WriteBooleanValue(iEnumerationUsage.IsEnd);

            writer.WritePropertyName("isImpliedIncluded");
            writer.WriteBooleanValue(iEnumerationUsage.IsImpliedIncluded);

            writer.WritePropertyName("isOrdered");
            writer.WriteBooleanValue(iEnumerationUsage.IsOrdered);

            writer.WritePropertyName("isPortion");
            writer.WriteBooleanValue(iEnumerationUsage.IsPortion);

            writer.WritePropertyName("isReadOnly");
            writer.WriteBooleanValue(iEnumerationUsage.IsReadOnly);

            writer.WritePropertyName("isSufficient");
            writer.WriteBooleanValue(iEnumerationUsage.IsSufficient);

            writer.WritePropertyName("isUnique");
            writer.WriteBooleanValue(iEnumerationUsage.IsUnique);

            writer.WritePropertyName("isVariation");
            writer.WriteBooleanValue(iEnumerationUsage.IsVariation);

            writer.WriteStartArray("ownedRelationship");
            foreach (var item in iEnumerationUsage.OwnedRelationship)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("owningRelationship");
            if (iEnumerationUsage.OwningRelationship.HasValue)
            {
                writer.WriteStringValue(iEnumerationUsage.OwningRelationship.Value);
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
