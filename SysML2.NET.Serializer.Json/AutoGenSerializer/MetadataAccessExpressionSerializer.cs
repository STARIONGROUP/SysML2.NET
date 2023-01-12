// -------------------------------------------------------------------------------------------------
// <copyright file="MetadataAccessExpressionSerializer.cs" company="RHEA System S.A.">
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
    /// The purpose of the <see cref="MetadataAccessExpressionSerializer"/> is to provide serialization
    /// and deserialization capabilities
    /// </summary>
    internal static class MetadataAccessExpressionSerializer
    {
        /// <summary>
        /// Serializes an instance of <see cref="IMetadataAccessExpression"/> using an <see cref="Utf8JsonWriter"/>
        /// </summary>
        /// <param name="obj">
        /// The <see cref="IMetadataAccessExpression"/> to serialize
        /// </param>
        /// <param name="writer">
        /// The target <see cref="Utf8JsonWriter"/>
        /// </param>
        /// <param name="serializationModeKind">
        /// enumeration specifying what kind of serialization shall be used
        /// </param>
        internal static void Serialize(object obj, Utf8JsonWriter writer, SerializationModeKind serializationModeKind)
        {
            if (!(obj is IMetadataAccessExpression iMetadataAccessExpression))
            {
                throw new ArgumentException("The object shall be an IMetadataAccessExpression", nameof(obj));
            }

            writer.WriteStartObject();

            writer.WritePropertyName("@type");
            writer.WriteStringValue("MetadataAccessExpression");

            writer.WritePropertyName("@id");
            writer.WriteStringValue(iMetadataAccessExpression.Id);

            writer.WriteStartArray("aliasIds");
            foreach (var item in iMetadataAccessExpression.AliasIds)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("direction");
            if (iMetadataAccessExpression.Direction.HasValue)
            {
                writer.WriteStringValue(iMetadataAccessExpression.Direction.Value.ToString().ToLower());
            }
            else
            {
                writer.WriteNullValue();
            }
            writer.WritePropertyName("elementId");
            writer.WriteStringValue(iMetadataAccessExpression.ElementId);

            writer.WritePropertyName("isAbstract");
            writer.WriteBooleanValue(iMetadataAccessExpression.IsAbstract);

            writer.WritePropertyName("isComposite");
            writer.WriteBooleanValue(iMetadataAccessExpression.IsComposite);

            writer.WritePropertyName("isDerived");
            writer.WriteBooleanValue(iMetadataAccessExpression.IsDerived);

            writer.WritePropertyName("isEnd");
            writer.WriteBooleanValue(iMetadataAccessExpression.IsEnd);

            writer.WritePropertyName("isImpliedIncluded");
            writer.WriteBooleanValue(iMetadataAccessExpression.IsImpliedIncluded);

            writer.WritePropertyName("isOrdered");
            writer.WriteBooleanValue(iMetadataAccessExpression.IsOrdered);

            writer.WritePropertyName("isPortion");
            writer.WriteBooleanValue(iMetadataAccessExpression.IsPortion);

            writer.WritePropertyName("isReadOnly");
            writer.WriteBooleanValue(iMetadataAccessExpression.IsReadOnly);

            writer.WritePropertyName("isSufficient");
            writer.WriteBooleanValue(iMetadataAccessExpression.IsSufficient);

            writer.WritePropertyName("isUnique");
            writer.WriteBooleanValue(iMetadataAccessExpression.IsUnique);

            writer.WritePropertyName("name");
            writer.WriteStringValue(iMetadataAccessExpression.Name);
            writer.WriteStartArray("ownedRelationship");
            foreach (var item in iMetadataAccessExpression.OwnedRelationship)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("owningRelationship");
            if (iMetadataAccessExpression.OwningRelationship.HasValue)
            {
                writer.WriteStringValue(iMetadataAccessExpression.OwningRelationship.Value);
            }
            else
            {
                writer.WriteNullValue();
            }
            writer.WritePropertyName("referencedElement");
            writer.WriteStringValue(iMetadataAccessExpression.ReferencedElement);

            writer.WritePropertyName("shortName");
            writer.WriteStringValue(iMetadataAccessExpression.ShortName);
            writer.WriteEndObject();
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
