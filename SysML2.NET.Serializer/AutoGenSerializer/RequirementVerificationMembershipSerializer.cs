// -------------------------------------------------------------------------------------------------
// <copyright file="RequirementVerificationMembershipSerializer.cs" company="RHEA System S.A.">
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
    /// The purpose of the <see cref="RequirementVerificationMembershipSerializer"/> is to provide serialization
    /// and deserialization capabilities
    /// </summary>
    internal static class RequirementVerificationMembershipSerializer
    {
        /// <summary>
        /// Serializes an instance of <see cref="IRequirementVerificationMembership"/> using an <see cref="Utf8JsonWriter"/>
        /// </summary>
        /// <param name="requirementVerificationMembership">
        /// The <see cref="IRequirementVerificationMembership"/> to serialize
        /// </param>
        /// <param name="writer">
        /// The target <see cref="Utf8JsonWriter"/>
        /// </param>
        /// <param name="serializationModeKind">
        /// enumeration specifying what kind of serialization shall be used
        /// </param>
        public static void Serialize(IRequirementVerificationMembership iRequirementVerificationMembership, Utf8JsonWriter writer, SerializationModeKind serializationModeKind)
        {
            writer.WriteStartObject();

            writer.WritePropertyName("@id");
            writer.WriteStringValue(iRequirementVerificationMembership.Id);

            writer.WritePropertyName("@type");
            writer.WriteStringValue("RequirementVerificationMembership");

            writer.WritePropertyName("kind");
            writer.WriteStringValue(iRequirementVerificationMembership.Kind.ToString().ToUpper());

            writer.WritePropertyName("memberElement");
            writer.WriteStringValue(iRequirementVerificationMembership.MemberElement);

            writer.WritePropertyName("memberName");
            writer.WriteStringValue(iRequirementVerificationMembership.MemberName);

            writer.WritePropertyName("memberShortName");
            writer.WriteStringValue(iRequirementVerificationMembership.MemberShortName);

            writer.WritePropertyName("visibility");
            writer.WriteStringValue(iRequirementVerificationMembership.Visibility.ToString().ToUpper());

            writer.WriteStartArray("ownedRelatedElement");
            foreach (var item in iRequirementVerificationMembership.OwnedRelatedElement)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("owningRelatedElement");
            if (iRequirementVerificationMembership.OwningRelatedElement.HasValue)
            {
                writer.WriteStringValue(iRequirementVerificationMembership.OwningRelatedElement.Value);
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WriteStartArray("source");
            foreach (var item in iRequirementVerificationMembership.Source)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WriteStartArray("target");
            foreach (var item in iRequirementVerificationMembership.Target)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WriteStartArray("aliasIds");
            foreach (var item in iRequirementVerificationMembership.AliasIds)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("elementId");
            writer.WriteStringValue(iRequirementVerificationMembership.ElementId);

            writer.WritePropertyName("name");
            writer.WriteStringValue(iRequirementVerificationMembership.Name);

            writer.WriteStartArray("ownedRelationship");
            foreach (var item in iRequirementVerificationMembership.OwnedRelationship)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("owningRelationship");
            if (iRequirementVerificationMembership.OwningRelationship.HasValue)
            {
                writer.WriteStringValue(iRequirementVerificationMembership.OwningRelationship.Value);
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("shortName");
            writer.WriteStringValue(iRequirementVerificationMembership.ShortName);

            writer.WritePropertyName("featureOfType");
            writer.WriteStringValue(iRequirementVerificationMembership.FeatureOfType);

            writer.WritePropertyName("featuringType");
            writer.WriteStringValue(iRequirementVerificationMembership.FeaturingType);

            writer.WriteEndObject();
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
