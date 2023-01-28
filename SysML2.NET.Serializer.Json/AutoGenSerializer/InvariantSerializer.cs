// -------------------------------------------------------------------------------------------------
// <copyright file="InvariantSerializer.cs" company="RHEA System S.A.">
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
    /// The purpose of the <see cref="InvariantSerializer"/> is to provide serialization
    /// and deserialization capabilities
    /// </summary>
    internal static class InvariantSerializer
    {
        /// <summary>
        /// Serializes an instance of <see cref="IInvariant"/> using an <see cref="Utf8JsonWriter"/>
        /// </summary>
        /// <param name="obj">
        /// The <see cref="IInvariant"/> to serialize
        /// </param>
        /// <param name="writer">
        /// The target <see cref="Utf8JsonWriter"/>
        /// </param>
        /// <param name="serializationModeKind">
        /// enumeration specifying what kind of serialization shall be used
        /// </param>
        internal static void Serialize(object obj, Utf8JsonWriter writer, SerializationModeKind serializationModeKind)
        {
            if (!(obj is IInvariant iInvariant))
            {
                throw new ArgumentException("The object shall be an IInvariant", nameof(obj));
            }

            writer.WriteStartObject();

            writer.WritePropertyName("@type");
            writer.WriteStringValue("Invariant");

            writer.WritePropertyName("@id");
            writer.WriteStringValue(iInvariant.Id);

            writer.WriteStartArray("aliasIds");
            foreach (var item in iInvariant.AliasIds)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("declaredName");
            writer.WriteStringValue(iInvariant.DeclaredName);
            writer.WritePropertyName("declaredShortName");
            writer.WriteStringValue(iInvariant.DeclaredShortName);
            writer.WritePropertyName("direction");
            if (iInvariant.Direction.HasValue)
            {
                writer.WriteStringValue(iInvariant.Direction.Value.ToString().ToLower());
            }
            else
            {
                writer.WriteNullValue();
            }
            writer.WritePropertyName("elementId");
            writer.WriteStringValue(iInvariant.ElementId);

            writer.WritePropertyName("isAbstract");
            writer.WriteBooleanValue(iInvariant.IsAbstract);

            writer.WritePropertyName("isComposite");
            writer.WriteBooleanValue(iInvariant.IsComposite);

            writer.WritePropertyName("isDerived");
            writer.WriteBooleanValue(iInvariant.IsDerived);

            writer.WritePropertyName("isEnd");
            writer.WriteBooleanValue(iInvariant.IsEnd);

            writer.WritePropertyName("isImpliedIncluded");
            writer.WriteBooleanValue(iInvariant.IsImpliedIncluded);

            writer.WritePropertyName("isNegated");
            writer.WriteBooleanValue(iInvariant.IsNegated);

            writer.WritePropertyName("isOrdered");
            writer.WriteBooleanValue(iInvariant.IsOrdered);

            writer.WritePropertyName("isPortion");
            writer.WriteBooleanValue(iInvariant.IsPortion);

            writer.WritePropertyName("isReadOnly");
            writer.WriteBooleanValue(iInvariant.IsReadOnly);

            writer.WritePropertyName("isSufficient");
            writer.WriteBooleanValue(iInvariant.IsSufficient);

            writer.WritePropertyName("isUnique");
            writer.WriteBooleanValue(iInvariant.IsUnique);

            writer.WriteStartArray("ownedRelationship");
            foreach (var item in iInvariant.OwnedRelationship)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("owningRelationship");
            if (iInvariant.OwningRelationship.HasValue)
            {
                writer.WriteStringValue(iInvariant.OwningRelationship.Value);
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
