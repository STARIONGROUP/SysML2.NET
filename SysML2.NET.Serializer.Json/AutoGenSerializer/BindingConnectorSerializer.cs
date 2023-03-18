// -------------------------------------------------------------------------------------------------
// <copyright file="BindingConnectorSerializer.cs" company="RHEA System S.A.">
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
    /// The purpose of the <see cref="BindingConnectorSerializer"/> is to provide serialization
    /// and deserialization capabilities
    /// </summary>
    internal static class BindingConnectorSerializer
    {
        /// <summary>
        /// Serializes an instance of <see cref="IBindingConnector"/> using an <see cref="Utf8JsonWriter"/>
        /// </summary>
        /// <param name="obj">
        /// The <see cref="IBindingConnector"/> to serialize
        /// </param>
        /// <param name="writer">
        /// The target <see cref="Utf8JsonWriter"/>
        /// </param>
        /// <param name="serializationModeKind">
        /// enumeration specifying what kind of serialization shall be used
        /// </param>
        internal static void Serialize(object obj, Utf8JsonWriter writer, SerializationModeKind serializationModeKind)
        {
            if (!(obj is IBindingConnector iBindingConnector))
            {
                throw new ArgumentException("The object shall be an IBindingConnector", nameof(obj));
            }

            writer.WriteStartObject();

            writer.WritePropertyName("@type");
            writer.WriteStringValue("BindingConnector");

            writer.WritePropertyName("@id");
            writer.WriteStringValue(iBindingConnector.Id);

            writer.WriteStartArray("aliasIds");
            foreach (var item in iBindingConnector.AliasIds)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("direction");
            if (iBindingConnector.Direction.HasValue)
            {
                writer.WriteStringValue(iBindingConnector.Direction.Value.ToString().ToLower());
            }
            else
            {
                writer.WriteNullValue();
            }
            writer.WritePropertyName("elementId");
            writer.WriteStringValue(iBindingConnector.ElementId);

            writer.WritePropertyName("isAbstract");
            writer.WriteBooleanValue(iBindingConnector.IsAbstract);

            writer.WritePropertyName("isComposite");
            writer.WriteBooleanValue(iBindingConnector.IsComposite);

            writer.WritePropertyName("isDerived");
            writer.WriteBooleanValue(iBindingConnector.IsDerived);

            writer.WritePropertyName("isDirected");
            writer.WriteBooleanValue(iBindingConnector.IsDirected);

            writer.WritePropertyName("isEnd");
            writer.WriteBooleanValue(iBindingConnector.IsEnd);

            writer.WritePropertyName("isImplied");
            writer.WriteBooleanValue(iBindingConnector.IsImplied);

            writer.WritePropertyName("isImpliedIncluded");
            writer.WriteBooleanValue(iBindingConnector.IsImpliedIncluded);

            writer.WritePropertyName("isOrdered");
            writer.WriteBooleanValue(iBindingConnector.IsOrdered);

            writer.WritePropertyName("isPortion");
            writer.WriteBooleanValue(iBindingConnector.IsPortion);

            writer.WritePropertyName("isReadOnly");
            writer.WriteBooleanValue(iBindingConnector.IsReadOnly);

            writer.WritePropertyName("isSufficient");
            writer.WriteBooleanValue(iBindingConnector.IsSufficient);

            writer.WritePropertyName("isUnique");
            writer.WriteBooleanValue(iBindingConnector.IsUnique);

            writer.WritePropertyName("name");
            writer.WriteStringValue(iBindingConnector.Name);
            writer.WriteStartArray("ownedRelatedElement");
            foreach (var item in iBindingConnector.OwnedRelatedElement)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WriteStartArray("ownedRelationship");
            foreach (var item in iBindingConnector.OwnedRelationship)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("owningRelatedElement");
            if (iBindingConnector.OwningRelatedElement.HasValue)
            {
                writer.WriteStringValue(iBindingConnector.OwningRelatedElement.Value);
            }
            else
            {
                writer.WriteNullValue();
            }
            writer.WritePropertyName("owningRelationship");
            if (iBindingConnector.OwningRelationship.HasValue)
            {
                writer.WriteStringValue(iBindingConnector.OwningRelationship.Value);
            }
            else
            {
                writer.WriteNullValue();
            }
            writer.WritePropertyName("shortName");
            writer.WriteStringValue(iBindingConnector.ShortName);
            writer.WriteStartArray("source");
            foreach (var item in iBindingConnector.Source)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WriteStartArray("target");
            foreach (var item in iBindingConnector.Target)
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
