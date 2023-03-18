// -------------------------------------------------------------------------------------------------
// <copyright file="MultiplicityRangeSerializer.cs" company="RHEA System S.A.">
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
    /// The purpose of the <see cref="MultiplicityRangeSerializer"/> is to provide serialization
    /// and deserialization capabilities
    /// </summary>
    internal static class MultiplicityRangeSerializer
    {
        /// <summary>
        /// Serializes an instance of <see cref="IMultiplicityRange"/> using an <see cref="Utf8JsonWriter"/>
        /// </summary>
        /// <param name="obj">
        /// The <see cref="IMultiplicityRange"/> to serialize
        /// </param>
        /// <param name="writer">
        /// The target <see cref="Utf8JsonWriter"/>
        /// </param>
        /// <param name="serializationModeKind">
        /// enumeration specifying what kind of serialization shall be used
        /// </param>
        internal static void Serialize(object obj, Utf8JsonWriter writer, SerializationModeKind serializationModeKind)
        {
            if (!(obj is IMultiplicityRange iMultiplicityRange))
            {
                throw new ArgumentException("The object shall be an IMultiplicityRange", nameof(obj));
            }

            writer.WriteStartObject();

            writer.WritePropertyName("@type");
            writer.WriteStringValue("MultiplicityRange");

            writer.WritePropertyName("@id");
            writer.WriteStringValue(iMultiplicityRange.Id);

            writer.WriteStartArray("aliasIds");
            foreach (var item in iMultiplicityRange.AliasIds)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("declaredName");
            writer.WriteStringValue(iMultiplicityRange.DeclaredName);
            writer.WritePropertyName("declaredShortName");
            writer.WriteStringValue(iMultiplicityRange.DeclaredShortName);
            writer.WritePropertyName("direction");
            if (iMultiplicityRange.Direction.HasValue)
            {
                writer.WriteStringValue(iMultiplicityRange.Direction.Value.ToString().ToLower());
            }
            else
            {
                writer.WriteNullValue();
            }
            writer.WritePropertyName("elementId");
            writer.WriteStringValue(iMultiplicityRange.ElementId);

            writer.WritePropertyName("isAbstract");
            writer.WriteBooleanValue(iMultiplicityRange.IsAbstract);

            writer.WritePropertyName("isComposite");
            writer.WriteBooleanValue(iMultiplicityRange.IsComposite);

            writer.WritePropertyName("isDerived");
            writer.WriteBooleanValue(iMultiplicityRange.IsDerived);

            writer.WritePropertyName("isEnd");
            writer.WriteBooleanValue(iMultiplicityRange.IsEnd);

            writer.WritePropertyName("isImpliedIncluded");
            writer.WriteBooleanValue(iMultiplicityRange.IsImpliedIncluded);

            writer.WritePropertyName("isOrdered");
            writer.WriteBooleanValue(iMultiplicityRange.IsOrdered);

            writer.WritePropertyName("isPortion");
            writer.WriteBooleanValue(iMultiplicityRange.IsPortion);

            writer.WritePropertyName("isReadOnly");
            writer.WriteBooleanValue(iMultiplicityRange.IsReadOnly);

            writer.WritePropertyName("isSufficient");
            writer.WriteBooleanValue(iMultiplicityRange.IsSufficient);

            writer.WritePropertyName("isUnique");
            writer.WriteBooleanValue(iMultiplicityRange.IsUnique);

            writer.WriteStartArray("ownedRelationship");
            foreach (var item in iMultiplicityRange.OwnedRelationship)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("owningRelationship");
            if (iMultiplicityRange.OwningRelationship.HasValue)
            {
                writer.WriteStringValue(iMultiplicityRange.OwningRelationship.Value);
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
