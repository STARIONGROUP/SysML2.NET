// -------------------------------------------------------------------------------------------------
// <copyright file="TransitionFeatureMembershipSerializer.cs" company="RHEA System S.A.">
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
    /// The purpose of the <see cref="TransitionFeatureMembershipSerializer"/> is to provide serialization
    /// and deserialization capabilities
    /// </summary>
    internal static class TransitionFeatureMembershipSerializer
    {
        /// <summary>
        /// Serializes an instance of <see cref="ITransitionFeatureMembership"/> using an <see cref="Utf8JsonWriter"/>
        /// </summary>
        /// <param name="obj">
        /// The <see cref="ITransitionFeatureMembership"/> to serialize
        /// </param>
        /// <param name="writer">
        /// The target <see cref="Utf8JsonWriter"/>
        /// </param>
        /// <param name="serializationModeKind">
        /// enumeration specifying what kind of serialization shall be used
        /// </param>
        internal static void Serialize(object obj, Utf8JsonWriter writer, SerializationModeKind serializationModeKind)
        {
            if (!(obj is ITransitionFeatureMembership iTransitionFeatureMembership))
            {
                throw new ArgumentException("The object shall be an ITransitionFeatureMembership", nameof(obj));
            }

            writer.WriteStartObject();

            writer.WritePropertyName("@type");
            writer.WriteStringValue("TransitionFeatureMembership");

            writer.WritePropertyName("@id");
            writer.WriteStringValue(iTransitionFeatureMembership.Id);

            writer.WriteStartArray("aliasIds");
            foreach (var item in iTransitionFeatureMembership.AliasIds)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("elementId");
            writer.WriteStringValue(iTransitionFeatureMembership.ElementId);

            writer.WritePropertyName("feature");
            writer.WriteStringValue(iTransitionFeatureMembership.Feature);

            writer.WritePropertyName("isImplied");
            writer.WriteBooleanValue(iTransitionFeatureMembership.IsImplied);

            writer.WritePropertyName("isImpliedIncluded");
            writer.WriteBooleanValue(iTransitionFeatureMembership.IsImpliedIncluded);

            writer.WritePropertyName("kind");
            writer.WriteStringValue(iTransitionFeatureMembership.Kind.ToString().ToLower());

            writer.WritePropertyName("memberElement");
            writer.WriteStringValue(iTransitionFeatureMembership.MemberElement);

            writer.WritePropertyName("memberName");
            writer.WriteStringValue(iTransitionFeatureMembership.MemberName);
            writer.WritePropertyName("memberShortName");
            writer.WriteStringValue(iTransitionFeatureMembership.MemberShortName);
            writer.WritePropertyName("name");
            writer.WriteStringValue(iTransitionFeatureMembership.Name);
            writer.WriteStartArray("ownedRelatedElement");
            foreach (var item in iTransitionFeatureMembership.OwnedRelatedElement)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WriteStartArray("ownedRelationship");
            foreach (var item in iTransitionFeatureMembership.OwnedRelationship)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("owningRelatedElement");
            if (iTransitionFeatureMembership.OwningRelatedElement.HasValue)
            {
                writer.WriteStringValue(iTransitionFeatureMembership.OwningRelatedElement.Value);
            }
            else
            {
                writer.WriteNullValue();
            }
            writer.WritePropertyName("owningRelationship");
            if (iTransitionFeatureMembership.OwningRelationship.HasValue)
            {
                writer.WriteStringValue(iTransitionFeatureMembership.OwningRelationship.Value);
            }
            else
            {
                writer.WriteNullValue();
            }
            writer.WritePropertyName("shortName");
            writer.WriteStringValue(iTransitionFeatureMembership.ShortName);
            writer.WriteStartArray("source");
            foreach (var item in iTransitionFeatureMembership.Source)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WriteStartArray("target");
            foreach (var item in iTransitionFeatureMembership.Target)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("type");
            writer.WriteStringValue(iTransitionFeatureMembership.Type);

            writer.WritePropertyName("visibility");
            writer.WriteStringValue(iTransitionFeatureMembership.Visibility.ToString().ToLower());

            writer.WriteEndObject();
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
