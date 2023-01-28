// -------------------------------------------------------------------------------------------------
// <copyright file="EndFeatureMembershipSerializer.cs" company="RHEA System S.A.">
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
    /// The purpose of the <see cref="EndFeatureMembershipSerializer"/> is to provide serialization
    /// and deserialization capabilities
    /// </summary>
    internal static class EndFeatureMembershipSerializer
    {
        /// <summary>
        /// Serializes an instance of <see cref="IEndFeatureMembership"/> using an <see cref="Utf8JsonWriter"/>
        /// </summary>
        /// <param name="obj">
        /// The <see cref="IEndFeatureMembership"/> to serialize
        /// </param>
        /// <param name="writer">
        /// The target <see cref="Utf8JsonWriter"/>
        /// </param>
        /// <param name="serializationModeKind">
        /// enumeration specifying what kind of serialization shall be used
        /// </param>
        internal static void Serialize(object obj, Utf8JsonWriter writer, SerializationModeKind serializationModeKind)
        {
            if (!(obj is IEndFeatureMembership iEndFeatureMembership))
            {
                throw new ArgumentException("The object shall be an IEndFeatureMembership", nameof(obj));
            }

            writer.WriteStartObject();

            writer.WritePropertyName("@type");
            writer.WriteStringValue("EndFeatureMembership");

            writer.WritePropertyName("@id");
            writer.WriteStringValue(iEndFeatureMembership.Id);

            writer.WriteStartArray("aliasIds");
            foreach (var item in iEndFeatureMembership.AliasIds)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("declaredName");
            writer.WriteStringValue(iEndFeatureMembership.DeclaredName);
            writer.WritePropertyName("declaredShortName");
            writer.WriteStringValue(iEndFeatureMembership.DeclaredShortName);
            writer.WritePropertyName("elementId");
            writer.WriteStringValue(iEndFeatureMembership.ElementId);

            writer.WritePropertyName("feature");
            writer.WriteStringValue(iEndFeatureMembership.Feature);

            writer.WritePropertyName("isImplied");
            writer.WriteBooleanValue(iEndFeatureMembership.IsImplied);

            writer.WritePropertyName("isImpliedIncluded");
            writer.WriteBooleanValue(iEndFeatureMembership.IsImpliedIncluded);

            writer.WritePropertyName("memberElement");
            writer.WriteStringValue(iEndFeatureMembership.MemberElement);

            writer.WritePropertyName("memberName");
            writer.WriteStringValue(iEndFeatureMembership.MemberName);
            writer.WritePropertyName("memberShortName");
            writer.WriteStringValue(iEndFeatureMembership.MemberShortName);
            writer.WriteStartArray("ownedRelatedElement");
            foreach (var item in iEndFeatureMembership.OwnedRelatedElement)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WriteStartArray("ownedRelationship");
            foreach (var item in iEndFeatureMembership.OwnedRelationship)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("owningRelatedElement");
            if (iEndFeatureMembership.OwningRelatedElement.HasValue)
            {
                writer.WriteStringValue(iEndFeatureMembership.OwningRelatedElement.Value);
            }
            else
            {
                writer.WriteNullValue();
            }
            writer.WritePropertyName("owningRelationship");
            if (iEndFeatureMembership.OwningRelationship.HasValue)
            {
                writer.WriteStringValue(iEndFeatureMembership.OwningRelationship.Value);
            }
            else
            {
                writer.WriteNullValue();
            }
            writer.WriteStartArray("source");
            foreach (var item in iEndFeatureMembership.Source)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WriteStartArray("target");
            foreach (var item in iEndFeatureMembership.Target)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("type");
            writer.WriteStringValue(iEndFeatureMembership.Type);

            writer.WritePropertyName("visibility");
            writer.WriteStringValue(iEndFeatureMembership.Visibility.ToString().ToLower());

            writer.WriteEndObject();
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
