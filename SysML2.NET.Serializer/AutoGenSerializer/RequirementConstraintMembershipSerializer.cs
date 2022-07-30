// -------------------------------------------------------------------------------------------------
// <copyright file="RequirementConstraintMembershipSerializer.cs" company="RHEA System S.A.">
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
    /// The purpose of the <see cref="RequirementConstraintMembershipSerializer"/> is to provide serialization
    /// and deserialization capabilities
    /// </summary>
    internal static class RequirementConstraintMembershipSerializer
    {
        /// <summary>
        /// Serializes an instance of <see cref="IRequirementConstraintMembership"/> using an <see cref="Utf8JsonWriter"/>
        /// </summary>
        /// <param name="requirementConstraintMembership">
        /// The <see cref="IRequirementConstraintMembership"/> to serialize
        /// </param>
        /// <param name="writer">
        /// The target <see cref="Utf8JsonWriter"/>
        /// </param>
        /// <param name="serializationModeKind">
        /// enumeration specifying what kind of serialization shall be used
        /// </param>
        public static void Serialize(IRequirementConstraintMembership iRequirementConstraintMembership, Utf8JsonWriter writer, SerializationModeKind serializationModeKind)
        {
            writer.WriteStartObject();

            writer.WritePropertyName("@id");
            writer.WriteStringValue(iRequirementConstraintMembership.Id);

            writer.WritePropertyName("@type");
            writer.WriteStringValue("RequirementConstraintMembership");

            writer.WritePropertyName("kind");
            writer.WriteStringValue(iRequirementConstraintMembership.Kind.ToString().ToUpper());

            writer.WritePropertyName("memberElement");
            writer.WriteStringValue(iRequirementConstraintMembership.MemberElement);

            writer.WritePropertyName("memberName");
            writer.WriteStringValue(iRequirementConstraintMembership.MemberName);

            writer.WritePropertyName("memberShortName");
            writer.WriteStringValue(iRequirementConstraintMembership.MemberShortName);

            writer.WritePropertyName("visibility");
            writer.WriteStringValue(iRequirementConstraintMembership.Visibility.ToString().ToUpper());

            writer.WriteStartArray("ownedRelatedElement");
            foreach (var item in iRequirementConstraintMembership.OwnedRelatedElement)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("owningRelatedElement");
            if (iRequirementConstraintMembership.OwningRelatedElement.HasValue)
            {
                writer.WriteStringValue(iRequirementConstraintMembership.OwningRelatedElement.Value);
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WriteStartArray("source");
            foreach (var item in iRequirementConstraintMembership.Source)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WriteStartArray("target");
            foreach (var item in iRequirementConstraintMembership.Target)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WriteStartArray("aliasIds");
            foreach (var item in iRequirementConstraintMembership.AliasIds)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("elementId");
            writer.WriteStringValue(iRequirementConstraintMembership.ElementId);

            writer.WritePropertyName("name");
            writer.WriteStringValue(iRequirementConstraintMembership.Name);

            writer.WriteStartArray("ownedRelationship");
            foreach (var item in iRequirementConstraintMembership.OwnedRelationship)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("owningRelationship");
            if (iRequirementConstraintMembership.OwningRelationship.HasValue)
            {
                writer.WriteStringValue(iRequirementConstraintMembership.OwningRelationship.Value);
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("shortName");
            writer.WriteStringValue(iRequirementConstraintMembership.ShortName);

            writer.WritePropertyName("featureOfType");
            writer.WriteStringValue(iRequirementConstraintMembership.FeatureOfType);

            writer.WritePropertyName("featuringType");
            writer.WriteStringValue(iRequirementConstraintMembership.FeaturingType);

            writer.WriteEndObject();
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
