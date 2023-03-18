// -------------------------------------------------------------------------------------------------
// <copyright file="DifferencingSerializer.cs" company="RHEA System S.A.">
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
    /// The purpose of the <see cref="DifferencingSerializer"/> is to provide serialization
    /// and deserialization capabilities
    /// </summary>
    internal static class DifferencingSerializer
    {
        /// <summary>
        /// Serializes an instance of <see cref="IDifferencing"/> using an <see cref="Utf8JsonWriter"/>
        /// </summary>
        /// <param name="obj">
        /// The <see cref="IDifferencing"/> to serialize
        /// </param>
        /// <param name="writer">
        /// The target <see cref="Utf8JsonWriter"/>
        /// </param>
        /// <param name="serializationModeKind">
        /// enumeration specifying what kind of serialization shall be used
        /// </param>
        internal static void Serialize(object obj, Utf8JsonWriter writer, SerializationModeKind serializationModeKind)
        {
            if (!(obj is IDifferencing iDifferencing))
            {
                throw new ArgumentException("The object shall be an IDifferencing", nameof(obj));
            }

            writer.WriteStartObject();

            writer.WritePropertyName("@type");
            writer.WriteStringValue("Differencing");

            writer.WritePropertyName("@id");
            writer.WriteStringValue(iDifferencing.Id);

            writer.WriteStartArray("aliasIds");
            foreach (var item in iDifferencing.AliasIds)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("declaredName");
            writer.WriteStringValue(iDifferencing.DeclaredName);
            writer.WritePropertyName("declaredShortName");
            writer.WriteStringValue(iDifferencing.DeclaredShortName);
            writer.WritePropertyName("differencingType");
            writer.WriteStringValue(iDifferencing.DifferencingType);

            writer.WritePropertyName("elementId");
            writer.WriteStringValue(iDifferencing.ElementId);

            writer.WritePropertyName("isImplied");
            writer.WriteBooleanValue(iDifferencing.IsImplied);

            writer.WritePropertyName("isImpliedIncluded");
            writer.WriteBooleanValue(iDifferencing.IsImpliedIncluded);

            writer.WriteStartArray("ownedRelatedElement");
            foreach (var item in iDifferencing.OwnedRelatedElement)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WriteStartArray("ownedRelationship");
            foreach (var item in iDifferencing.OwnedRelationship)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("owningRelatedElement");
            if (iDifferencing.OwningRelatedElement.HasValue)
            {
                writer.WriteStringValue(iDifferencing.OwningRelatedElement.Value);
            }
            else
            {
                writer.WriteNullValue();
            }
            writer.WritePropertyName("owningRelationship");
            if (iDifferencing.OwningRelationship.HasValue)
            {
                writer.WriteStringValue(iDifferencing.OwningRelationship.Value);
            }
            else
            {
                writer.WriteNullValue();
            }
            writer.WriteStartArray("source");
            foreach (var item in iDifferencing.Source)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WriteStartArray("target");
            foreach (var item in iDifferencing.Target)
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
