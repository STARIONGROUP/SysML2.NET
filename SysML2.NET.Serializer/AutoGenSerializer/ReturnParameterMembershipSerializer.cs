// -------------------------------------------------------------------------------------------------
// <copyright file="ReturnParameterMembershipSerializer.cs" company="RHEA System S.A.">
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
    /// The purpose of the <see cref="ReturnParameterMembershipSerializer"/> is to provide serialization
    /// and deserialization capabilities
    /// </summary>
    internal static class ReturnParameterMembershipSerializer
    {
        /// <summary>
        /// Serializes an instance of <see cref="IReturnParameterMembership"/> using an <see cref="Utf8JsonWriter"/>
        /// </summary>
        /// <param name="returnParameterMembership">
        /// The <see cref="IReturnParameterMembership"/> to serialize
        /// </param>
        /// <param name="writer">
        /// The target <see cref="Utf8JsonWriter"/>
        /// </param>
        /// <param name="serializationModeKind">
        /// enumeration specifying what kind of serialization shall be used
        /// </param>
        public static void Serialize(IReturnParameterMembership iReturnParameterMembership, Utf8JsonWriter writer, SerializationModeKind serializationModeKind)
        {
            writer.WriteStartObject();

            writer.WritePropertyName("@id");
            writer.WriteStringValue(iReturnParameterMembership.Id);

            writer.WritePropertyName("@type");
            writer.WriteStringValue("ReturnParameterMembership");

            writer.WritePropertyName("memberElement");
            writer.WriteStringValue(iReturnParameterMembership.MemberElement);

            writer.WritePropertyName("memberName");
            writer.WriteStringValue(iReturnParameterMembership.MemberName);

            writer.WritePropertyName("memberShortName");
            writer.WriteStringValue(iReturnParameterMembership.MemberShortName);

            writer.WritePropertyName("visibility");
            writer.WriteStringValue(iReturnParameterMembership.Visibility.ToString().ToUpper());

            writer.WriteStartArray("ownedRelatedElement");
            foreach (var item in iReturnParameterMembership.OwnedRelatedElement)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("owningRelatedElement");
            if (iReturnParameterMembership.OwningRelatedElement.HasValue)
            {
                writer.WriteStringValue(iReturnParameterMembership.OwningRelatedElement.Value);
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WriteStartArray("source");
            foreach (var item in iReturnParameterMembership.Source)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WriteStartArray("target");
            foreach (var item in iReturnParameterMembership.Target)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WriteStartArray("aliasIds");
            foreach (var item in iReturnParameterMembership.AliasIds)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("elementId");
            writer.WriteStringValue(iReturnParameterMembership.ElementId);

            writer.WritePropertyName("name");
            writer.WriteStringValue(iReturnParameterMembership.Name);

            writer.WriteStartArray("ownedRelationship");
            foreach (var item in iReturnParameterMembership.OwnedRelationship)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("owningRelationship");
            if (iReturnParameterMembership.OwningRelationship.HasValue)
            {
                writer.WriteStringValue(iReturnParameterMembership.OwningRelationship.Value);
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("shortName");
            writer.WriteStringValue(iReturnParameterMembership.ShortName);

            writer.WritePropertyName("featureOfType");
            writer.WriteStringValue(iReturnParameterMembership.FeatureOfType);

            writer.WritePropertyName("featuringType");
            writer.WriteStringValue(iReturnParameterMembership.FeaturingType);

            writer.WriteEndObject();
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
