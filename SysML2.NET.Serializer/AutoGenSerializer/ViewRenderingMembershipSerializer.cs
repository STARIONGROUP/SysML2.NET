// -------------------------------------------------------------------------------------------------
// <copyright file="ViewRenderingMembershipSerializer.cs" company="RHEA System S.A.">
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
    /// The purpose of the <see cref="ViewRenderingMembershipSerializer"/> is to provide serialization
    /// and deserialization capabilities
    /// </summary>
    internal static class ViewRenderingMembershipSerializer
    {
        /// <summary>
        /// Serializes an instance of <see cref="IViewRenderingMembership"/> using an <see cref="Utf8JsonWriter"/>
        /// </summary>
        /// <param name="viewRenderingMembership">
        /// The <see cref="IViewRenderingMembership"/> to serialize
        /// </param>
        /// <param name="writer">
        /// The target <see cref="Utf8JsonWriter"/>
        /// </param>
        /// <param name="serializationModeKind">
        /// enumeration specifying what kind of serialization shall be used
        /// </param>
        public static void Serialize(IViewRenderingMembership iViewRenderingMembership, Utf8JsonWriter writer, SerializationModeKind serializationModeKind)
        {
            writer.WriteStartObject();

            writer.WritePropertyName("@id");
            writer.WriteStringValue(iViewRenderingMembership.Id);

            writer.WritePropertyName("@type");
            writer.WriteStringValue("ViewRenderingMembership");

            writer.WritePropertyName("memberElement");
            writer.WriteStringValue(iViewRenderingMembership.MemberElement);

            writer.WritePropertyName("memberName");
            writer.WriteStringValue(iViewRenderingMembership.MemberName);

            writer.WritePropertyName("memberShortName");
            writer.WriteStringValue(iViewRenderingMembership.MemberShortName);

            writer.WritePropertyName("visibility");
            writer.WriteStringValue(iViewRenderingMembership.Visibility.ToString().ToUpper());

            writer.WriteStartArray("ownedRelatedElement");
            foreach (var item in iViewRenderingMembership.OwnedRelatedElement)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("owningRelatedElement");
            if (iViewRenderingMembership.OwningRelatedElement.HasValue)
            {
                writer.WriteStringValue(iViewRenderingMembership.OwningRelatedElement.Value);
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WriteStartArray("source");
            foreach (var item in iViewRenderingMembership.Source)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WriteStartArray("target");
            foreach (var item in iViewRenderingMembership.Target)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WriteStartArray("aliasIds");
            foreach (var item in iViewRenderingMembership.AliasIds)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("elementId");
            writer.WriteStringValue(iViewRenderingMembership.ElementId);

            writer.WritePropertyName("name");
            writer.WriteStringValue(iViewRenderingMembership.Name);

            writer.WriteStartArray("ownedRelationship");
            foreach (var item in iViewRenderingMembership.OwnedRelationship)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("owningRelationship");
            if (iViewRenderingMembership.OwningRelationship.HasValue)
            {
                writer.WriteStringValue(iViewRenderingMembership.OwningRelationship.Value);
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("shortName");
            writer.WriteStringValue(iViewRenderingMembership.ShortName);

            writer.WritePropertyName("featureOfType");
            writer.WriteStringValue(iViewRenderingMembership.FeatureOfType);

            writer.WritePropertyName("featuringType");
            writer.WriteStringValue(iViewRenderingMembership.FeaturingType);

            writer.WriteEndObject();
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
