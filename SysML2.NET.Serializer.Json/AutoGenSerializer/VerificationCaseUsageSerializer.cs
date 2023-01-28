// -------------------------------------------------------------------------------------------------
// <copyright file="VerificationCaseUsageSerializer.cs" company="RHEA System S.A.">
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
    /// The purpose of the <see cref="VerificationCaseUsageSerializer"/> is to provide serialization
    /// and deserialization capabilities
    /// </summary>
    internal static class VerificationCaseUsageSerializer
    {
        /// <summary>
        /// Serializes an instance of <see cref="IVerificationCaseUsage"/> using an <see cref="Utf8JsonWriter"/>
        /// </summary>
        /// <param name="obj">
        /// The <see cref="IVerificationCaseUsage"/> to serialize
        /// </param>
        /// <param name="writer">
        /// The target <see cref="Utf8JsonWriter"/>
        /// </param>
        /// <param name="serializationModeKind">
        /// enumeration specifying what kind of serialization shall be used
        /// </param>
        internal static void Serialize(object obj, Utf8JsonWriter writer, SerializationModeKind serializationModeKind)
        {
            if (!(obj is IVerificationCaseUsage iVerificationCaseUsage))
            {
                throw new ArgumentException("The object shall be an IVerificationCaseUsage", nameof(obj));
            }

            writer.WriteStartObject();

            writer.WritePropertyName("@type");
            writer.WriteStringValue("VerificationCaseUsage");

            writer.WritePropertyName("@id");
            writer.WriteStringValue(iVerificationCaseUsage.Id);

            writer.WriteStartArray("aliasIds");
            foreach (var item in iVerificationCaseUsage.AliasIds)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("declaredName");
            writer.WriteStringValue(iVerificationCaseUsage.DeclaredName);
            writer.WritePropertyName("declaredShortName");
            writer.WriteStringValue(iVerificationCaseUsage.DeclaredShortName);
            writer.WritePropertyName("direction");
            if (iVerificationCaseUsage.Direction.HasValue)
            {
                writer.WriteStringValue(iVerificationCaseUsage.Direction.Value.ToString().ToLower());
            }
            else
            {
                writer.WriteNullValue();
            }
            writer.WritePropertyName("elementId");
            writer.WriteStringValue(iVerificationCaseUsage.ElementId);

            writer.WritePropertyName("isAbstract");
            writer.WriteBooleanValue(iVerificationCaseUsage.IsAbstract);

            writer.WritePropertyName("isComposite");
            writer.WriteBooleanValue(iVerificationCaseUsage.IsComposite);

            writer.WritePropertyName("isDerived");
            writer.WriteBooleanValue(iVerificationCaseUsage.IsDerived);

            writer.WritePropertyName("isEnd");
            writer.WriteBooleanValue(iVerificationCaseUsage.IsEnd);

            writer.WritePropertyName("isImpliedIncluded");
            writer.WriteBooleanValue(iVerificationCaseUsage.IsImpliedIncluded);

            writer.WritePropertyName("isIndividual");
            writer.WriteBooleanValue(iVerificationCaseUsage.IsIndividual);

            writer.WritePropertyName("isOrdered");
            writer.WriteBooleanValue(iVerificationCaseUsage.IsOrdered);

            writer.WritePropertyName("isPortion");
            writer.WriteBooleanValue(iVerificationCaseUsage.IsPortion);

            writer.WritePropertyName("isReadOnly");
            writer.WriteBooleanValue(iVerificationCaseUsage.IsReadOnly);

            writer.WritePropertyName("isSufficient");
            writer.WriteBooleanValue(iVerificationCaseUsage.IsSufficient);

            writer.WritePropertyName("isUnique");
            writer.WriteBooleanValue(iVerificationCaseUsage.IsUnique);

            writer.WritePropertyName("isVariation");
            writer.WriteBooleanValue(iVerificationCaseUsage.IsVariation);

            writer.WriteStartArray("ownedRelationship");
            foreach (var item in iVerificationCaseUsage.OwnedRelationship)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("owningRelationship");
            if (iVerificationCaseUsage.OwningRelationship.HasValue)
            {
                writer.WriteStringValue(iVerificationCaseUsage.OwningRelationship.Value);
            }
            else
            {
                writer.WriteNullValue();
            }
            writer.WritePropertyName("portionKind");
            if (iVerificationCaseUsage.PortionKind.HasValue)
            {
                writer.WriteStringValue(iVerificationCaseUsage.PortionKind.Value.ToString().ToLower());
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
