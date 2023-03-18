// -------------------------------------------------------------------------------------------------
// <copyright file="BindingConnectorAsUsageSerializer.cs" company="RHEA System S.A.">
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
    /// The purpose of the <see cref="BindingConnectorAsUsageSerializer"/> is to provide serialization
    /// and deserialization capabilities
    /// </summary>
    internal static class BindingConnectorAsUsageSerializer
    {
        /// <summary>
        /// Serializes an instance of <see cref="IBindingConnectorAsUsage"/> using an <see cref="Utf8JsonWriter"/>
        /// </summary>
        /// <param name="obj">
        /// The <see cref="IBindingConnectorAsUsage"/> to serialize
        /// </param>
        /// <param name="writer">
        /// The target <see cref="Utf8JsonWriter"/>
        /// </param>
        /// <param name="serializationModeKind">
        /// enumeration specifying what kind of serialization shall be used
        /// </param>
        internal static void Serialize(object obj, Utf8JsonWriter writer, SerializationModeKind serializationModeKind)
        {
            if (!(obj is IBindingConnectorAsUsage iBindingConnectorAsUsage))
            {
                throw new ArgumentException("The object shall be an IBindingConnectorAsUsage", nameof(obj));
            }

            writer.WriteStartObject();

            writer.WritePropertyName("@type");
            writer.WriteStringValue("BindingConnectorAsUsage");

            writer.WritePropertyName("@id");
            writer.WriteStringValue(iBindingConnectorAsUsage.Id);

            writer.WriteStartArray("aliasIds");
            foreach (var item in iBindingConnectorAsUsage.AliasIds)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("direction");
            if (iBindingConnectorAsUsage.Direction.HasValue)
            {
                writer.WriteStringValue(iBindingConnectorAsUsage.Direction.Value.ToString().ToLower());
            }
            else
            {
                writer.WriteNullValue();
            }
            writer.WritePropertyName("elementId");
            writer.WriteStringValue(iBindingConnectorAsUsage.ElementId);

            writer.WritePropertyName("isAbstract");
            writer.WriteBooleanValue(iBindingConnectorAsUsage.IsAbstract);

            writer.WritePropertyName("isComposite");
            writer.WriteBooleanValue(iBindingConnectorAsUsage.IsComposite);

            writer.WritePropertyName("isDerived");
            writer.WriteBooleanValue(iBindingConnectorAsUsage.IsDerived);

            writer.WritePropertyName("isDirected");
            writer.WriteBooleanValue(iBindingConnectorAsUsage.IsDirected);

            writer.WritePropertyName("isEnd");
            writer.WriteBooleanValue(iBindingConnectorAsUsage.IsEnd);

            writer.WritePropertyName("isImplied");
            writer.WriteBooleanValue(iBindingConnectorAsUsage.IsImplied);

            writer.WritePropertyName("isImpliedIncluded");
            writer.WriteBooleanValue(iBindingConnectorAsUsage.IsImpliedIncluded);

            writer.WritePropertyName("isOrdered");
            writer.WriteBooleanValue(iBindingConnectorAsUsage.IsOrdered);

            writer.WritePropertyName("isPortion");
            writer.WriteBooleanValue(iBindingConnectorAsUsage.IsPortion);

            writer.WritePropertyName("isReadOnly");
            writer.WriteBooleanValue(iBindingConnectorAsUsage.IsReadOnly);

            writer.WritePropertyName("isSufficient");
            writer.WriteBooleanValue(iBindingConnectorAsUsage.IsSufficient);

            writer.WritePropertyName("isUnique");
            writer.WriteBooleanValue(iBindingConnectorAsUsage.IsUnique);

            writer.WritePropertyName("isVariation");
            writer.WriteBooleanValue(iBindingConnectorAsUsage.IsVariation);

            writer.WritePropertyName("name");
            writer.WriteStringValue(iBindingConnectorAsUsage.Name);
            writer.WriteStartArray("ownedRelatedElement");
            foreach (var item in iBindingConnectorAsUsage.OwnedRelatedElement)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WriteStartArray("ownedRelationship");
            foreach (var item in iBindingConnectorAsUsage.OwnedRelationship)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("owningRelatedElement");
            if (iBindingConnectorAsUsage.OwningRelatedElement.HasValue)
            {
                writer.WriteStringValue(iBindingConnectorAsUsage.OwningRelatedElement.Value);
            }
            else
            {
                writer.WriteNullValue();
            }
            writer.WritePropertyName("owningRelationship");
            if (iBindingConnectorAsUsage.OwningRelationship.HasValue)
            {
                writer.WriteStringValue(iBindingConnectorAsUsage.OwningRelationship.Value);
            }
            else
            {
                writer.WriteNullValue();
            }
            writer.WritePropertyName("shortName");
            writer.WriteStringValue(iBindingConnectorAsUsage.ShortName);
            writer.WriteStartArray("source");
            foreach (var item in iBindingConnectorAsUsage.Source)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WriteStartArray("target");
            foreach (var item in iBindingConnectorAsUsage.Target)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WriteEndObject();
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
