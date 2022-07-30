// -------------------------------------------------------------------------------------------------
// <copyright file="FramedConcernMembershipSerializer.cs" company="RHEA System S.A.">
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
    /// The purpose of the <see cref="FramedConcernMembershipSerializer"/> is to provide serialization
    /// and deserialization capabilities
    /// </summary>
    internal static class FramedConcernMembershipSerializer
    {
        /// <summary>
        /// Serializes an instance of <see cref="IFramedConcernMembership"/> using an <see cref="Utf8JsonWriter"/>
        /// </summary>
        /// <param name="framedConcernMembership">
        /// The <see cref="IFramedConcernMembership"/> to serialize
        /// </param>
        /// <param name="writer">
        /// The target <see cref="Utf8JsonWriter"/>
        /// </param>
        /// <param name="serializationModeKind">
        /// enumeration specifying what kind of serialization shall be used
        /// </param>
        public static void Serialize(IFramedConcernMembership iFramedConcernMembership, Utf8JsonWriter writer, SerializationModeKind serializationModeKind)
        {
            writer.WriteStartObject();

            writer.WritePropertyName("@id");
            writer.WriteStringValue(iFramedConcernMembership.Id);

            writer.WritePropertyName("@type");
            writer.WriteStringValue("FramedConcernMembership");

            writer.WritePropertyName("kind");
            writer.WriteStringValue(iFramedConcernMembership.Kind.ToString().ToUpper());

            writer.WritePropertyName("memberElement");
            writer.WriteStringValue(iFramedConcernMembership.MemberElement);

            writer.WritePropertyName("memberName");
            writer.WriteStringValue(iFramedConcernMembership.MemberName);

            writer.WritePropertyName("memberShortName");
            writer.WriteStringValue(iFramedConcernMembership.MemberShortName);

            writer.WritePropertyName("visibility");
            writer.WriteStringValue(iFramedConcernMembership.Visibility.ToString().ToUpper());

            writer.WriteStartArray("ownedRelatedElement");
            foreach (var item in iFramedConcernMembership.OwnedRelatedElement)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("owningRelatedElement");
            if (iFramedConcernMembership.OwningRelatedElement.HasValue)
            {
                writer.WriteStringValue(iFramedConcernMembership.OwningRelatedElement.Value);
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WriteStartArray("source");
            foreach (var item in iFramedConcernMembership.Source)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WriteStartArray("target");
            foreach (var item in iFramedConcernMembership.Target)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WriteStartArray("aliasIds");
            foreach (var item in iFramedConcernMembership.AliasIds)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("elementId");
            writer.WriteStringValue(iFramedConcernMembership.ElementId);

            writer.WritePropertyName("name");
            writer.WriteStringValue(iFramedConcernMembership.Name);

            writer.WriteStartArray("ownedRelationship");
            foreach (var item in iFramedConcernMembership.OwnedRelationship)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("owningRelationship");
            if (iFramedConcernMembership.OwningRelationship.HasValue)
            {
                writer.WriteStringValue(iFramedConcernMembership.OwningRelationship.Value);
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("shortName");
            writer.WriteStringValue(iFramedConcernMembership.ShortName);

            writer.WritePropertyName("featureOfType");
            writer.WriteStringValue(iFramedConcernMembership.FeatureOfType);

            writer.WritePropertyName("featuringType");
            writer.WriteStringValue(iFramedConcernMembership.FeaturingType);

            writer.WriteEndObject();
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
