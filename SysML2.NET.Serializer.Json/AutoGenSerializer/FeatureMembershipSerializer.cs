// -------------------------------------------------------------------------------------------------
// <copyright file="FeatureMembershipSerializer.cs" company="RHEA System S.A.">
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

    using SysML2.NET.DTO;

    /// <summary>
    /// The purpose of the <see cref="FeatureMembershipSerializer"/> is to provide serialization
    /// and deserialization capabilities
    /// </summary>
    internal static class FeatureMembershipSerializer
    {
        /// <summary>
        /// Serializes an instance of <see cref="IFeatureMembership"/> using an <see cref="Utf8JsonWriter"/>
        /// </summary>
        /// <param name="obj">
        /// The <see cref="IFeatureMembership"/> to serialize
        /// </param>
        /// <param name="writer">
        /// The target <see cref="Utf8JsonWriter"/>
        /// </param>
        /// <param name="serializationModeKind">
        /// enumeration specifying what kind of serialization shall be used
        /// </param>
        internal static void Serialize(object obj, Utf8JsonWriter writer, SerializationModeKind serializationModeKind)
        {
            if (!(obj is IFeatureMembership iFeatureMembership))
            {
                throw new ArgumentException("The object shall be an IFeatureMembership", nameof(obj));
            }

            writer.WriteStartObject();

            writer.WritePropertyName("@id");
            writer.WriteStringValue(iFeatureMembership.Id);

            writer.WritePropertyName("@type");
            writer.WriteStringValue("FeatureMembership");

            writer.WritePropertyName("memberElement");
            writer.WriteStringValue(iFeatureMembership.MemberElement);

            writer.WritePropertyName("memberName");
            writer.WriteStringValue(iFeatureMembership.MemberName);

            writer.WritePropertyName("memberShortName");
            writer.WriteStringValue(iFeatureMembership.MemberShortName);

            writer.WritePropertyName("visibility");
            writer.WriteStringValue(iFeatureMembership.Visibility.ToString().ToUpper());

            writer.WriteStartArray("ownedRelatedElement");
            foreach (var item in iFeatureMembership.OwnedRelatedElement)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("owningRelatedElement");
            if (iFeatureMembership.OwningRelatedElement.HasValue)
            {
                writer.WriteStringValue(iFeatureMembership.OwningRelatedElement.Value);
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WriteStartArray("source");
            foreach (var item in iFeatureMembership.Source)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WriteStartArray("target");
            foreach (var item in iFeatureMembership.Target)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WriteStartArray("aliasIds");
            foreach (var item in iFeatureMembership.AliasIds)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("elementId");
            writer.WriteStringValue(iFeatureMembership.ElementId);

            writer.WritePropertyName("name");
            writer.WriteStringValue(iFeatureMembership.Name);

            writer.WriteStartArray("ownedRelationship");
            foreach (var item in iFeatureMembership.OwnedRelationship)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("owningRelationship");
            if (iFeatureMembership.OwningRelationship.HasValue)
            {
                writer.WriteStringValue(iFeatureMembership.OwningRelationship.Value);
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("shortName");
            writer.WriteStringValue(iFeatureMembership.ShortName);

            writer.WritePropertyName("featureOfType");
            writer.WriteStringValue(iFeatureMembership.FeatureOfType);

            writer.WritePropertyName("featuringType");
            writer.WriteStringValue(iFeatureMembership.FeaturingType);

            writer.WriteEndObject();
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
