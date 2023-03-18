﻿// -------------------------------------------------------------------------------------------------
// <copyright file="FeatureSerializer.cs" company="RHEA System S.A.">
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
    /// The purpose of the <see cref="FeatureSerializer"/> is to provide serialization
    /// and deserialization capabilities
    /// </summary>
    internal static class FeatureSerializer
    {
        /// <summary>
        /// Serializes an instance of <see cref="IFeature"/> using an <see cref="Utf8JsonWriter"/>
        /// </summary>
        /// <param name="obj">
        /// The <see cref="IFeature"/> to serialize
        /// </param>
        /// <param name="writer">
        /// The target <see cref="Utf8JsonWriter"/>
        /// </param>
        /// <param name="serializationModeKind">
        /// enumeration specifying what kind of serialization shall be used
        /// </param>
        internal static void Serialize(object obj, Utf8JsonWriter writer, SerializationModeKind serializationModeKind)
        {
            if (!(obj is IFeature iFeature))
            {
                throw new ArgumentException("The object shall be an IFeature", nameof(obj));
            }

            writer.WriteStartObject();

            writer.WritePropertyName("@type");
            writer.WriteStringValue("Feature");

            writer.WritePropertyName("@id");
            writer.WriteStringValue(iFeature.Id);

            writer.WriteStartArray("aliasIds");
            foreach (var item in iFeature.AliasIds)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("declaredName");
            writer.WriteStringValue(iFeature.DeclaredName);
            writer.WritePropertyName("declaredShortName");
            writer.WriteStringValue(iFeature.DeclaredShortName);
            writer.WritePropertyName("direction");
            if (iFeature.Direction.HasValue)
            {
                writer.WriteStringValue(iFeature.Direction.Value.ToString().ToLower());
            }
            else
            {
                writer.WriteNullValue();
            }
            writer.WritePropertyName("elementId");
            writer.WriteStringValue(iFeature.ElementId);

            writer.WritePropertyName("isAbstract");
            writer.WriteBooleanValue(iFeature.IsAbstract);

            writer.WritePropertyName("isComposite");
            writer.WriteBooleanValue(iFeature.IsComposite);

            writer.WritePropertyName("isDerived");
            writer.WriteBooleanValue(iFeature.IsDerived);

            writer.WritePropertyName("isEnd");
            writer.WriteBooleanValue(iFeature.IsEnd);

            writer.WritePropertyName("isImpliedIncluded");
            writer.WriteBooleanValue(iFeature.IsImpliedIncluded);

            writer.WritePropertyName("isOrdered");
            writer.WriteBooleanValue(iFeature.IsOrdered);

            writer.WritePropertyName("isPortion");
            writer.WriteBooleanValue(iFeature.IsPortion);

            writer.WritePropertyName("isReadOnly");
            writer.WriteBooleanValue(iFeature.IsReadOnly);

            writer.WritePropertyName("isSufficient");
            writer.WriteBooleanValue(iFeature.IsSufficient);

            writer.WritePropertyName("isUnique");
            writer.WriteBooleanValue(iFeature.IsUnique);

            writer.WriteStartArray("ownedRelationship");
            foreach (var item in iFeature.OwnedRelationship)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("owningRelationship");
            if (iFeature.OwningRelationship.HasValue)
            {
                writer.WriteStringValue(iFeature.OwningRelationship.Value);
            }
            else
            {
                writer.WriteNullValue();
            }
            writer.WriteEndObject();
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
