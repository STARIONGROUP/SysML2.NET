// -------------------------------------------------------------------------------------------------
// <copyright file="ResultExpressionMembershipSerializer.cs" company="RHEA System S.A.">
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
    /// The purpose of the <see cref="ResultExpressionMembershipSerializer"/> is to provide serialization
    /// and deserialization capabilities
    /// </summary>
    internal static class ResultExpressionMembershipSerializer
    {
        /// <summary>
        /// Serializes an instance of <see cref="IResultExpressionMembership"/> using an <see cref="Utf8JsonWriter"/>
        /// </summary>
        /// <param name="obj">
        /// The <see cref="IResultExpressionMembership"/> to serialize
        /// </param>
        /// <param name="writer">
        /// The target <see cref="Utf8JsonWriter"/>
        /// </param>
        /// <param name="serializationModeKind">
        /// enumeration specifying what kind of serialization shall be used
        /// </param>
        internal static void Serialize(object obj, Utf8JsonWriter writer, SerializationModeKind serializationModeKind)
        {
            if (!(obj is IResultExpressionMembership iResultExpressionMembership))
            {
                throw new ArgumentException("The object shall be an IResultExpressionMembership", nameof(obj));
            }

            writer.WriteStartObject();

            writer.WritePropertyName("@type");
            writer.WriteStringValue("ResultExpressionMembership");

            writer.WritePropertyName("@id");
            writer.WriteStringValue(iResultExpressionMembership.Id);

            writer.WriteStartArray("aliasIds");
            foreach (var item in iResultExpressionMembership.AliasIds)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("elementId");
            writer.WriteStringValue(iResultExpressionMembership.ElementId);

            writer.WritePropertyName("feature");
            writer.WriteStringValue(iResultExpressionMembership.Feature);

            writer.WritePropertyName("isImplied");
            writer.WriteBooleanValue(iResultExpressionMembership.IsImplied);

            writer.WritePropertyName("isImpliedIncluded");
            writer.WriteBooleanValue(iResultExpressionMembership.IsImpliedIncluded);

            writer.WritePropertyName("memberElement");
            writer.WriteStringValue(iResultExpressionMembership.MemberElement);

            writer.WritePropertyName("memberName");
            writer.WriteStringValue(iResultExpressionMembership.MemberName);
            writer.WritePropertyName("memberShortName");
            writer.WriteStringValue(iResultExpressionMembership.MemberShortName);
            writer.WritePropertyName("name");
            writer.WriteStringValue(iResultExpressionMembership.Name);
            writer.WriteStartArray("ownedRelatedElement");
            foreach (var item in iResultExpressionMembership.OwnedRelatedElement)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WriteStartArray("ownedRelationship");
            foreach (var item in iResultExpressionMembership.OwnedRelationship)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("owningRelatedElement");
            if (iResultExpressionMembership.OwningRelatedElement.HasValue)
            {
                writer.WriteStringValue(iResultExpressionMembership.OwningRelatedElement.Value);
            }
            else
            {
                writer.WriteNullValue();
            }
            writer.WritePropertyName("owningRelationship");
            if (iResultExpressionMembership.OwningRelationship.HasValue)
            {
                writer.WriteStringValue(iResultExpressionMembership.OwningRelationship.Value);
            }
            else
            {
                writer.WriteNullValue();
            }
            writer.WritePropertyName("shortName");
            writer.WriteStringValue(iResultExpressionMembership.ShortName);
            writer.WriteStartArray("source");
            foreach (var item in iResultExpressionMembership.Source)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WriteStartArray("target");
            foreach (var item in iResultExpressionMembership.Target)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("type");
            writer.WriteStringValue(iResultExpressionMembership.Type);

            writer.WritePropertyName("visibility");
            writer.WriteStringValue(iResultExpressionMembership.Visibility.ToString().ToLower());

            writer.WriteEndObject();
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
