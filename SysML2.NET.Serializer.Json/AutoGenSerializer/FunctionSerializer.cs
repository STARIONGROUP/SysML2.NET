// -------------------------------------------------------------------------------------------------
// <copyright file="FunctionSerializer.cs" company="RHEA System S.A.">
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
    /// The purpose of the <see cref="FunctionSerializer"/> is to provide serialization
    /// and deserialization capabilities
    /// </summary>
    internal static class FunctionSerializer
    {
        /// <summary>
        /// Serializes an instance of <see cref="IFunction"/> using an <see cref="Utf8JsonWriter"/>
        /// </summary>
        /// <param name="obj">
        /// The <see cref="IFunction"/> to serialize
        /// </param>
        /// <param name="writer">
        /// The target <see cref="Utf8JsonWriter"/>
        /// </param>
        /// <param name="serializationModeKind">
        /// enumeration specifying what kind of serialization shall be used
        /// </param>
        internal static void Serialize(object obj, Utf8JsonWriter writer, SerializationModeKind serializationModeKind)
        {
            if (!(obj is IFunction iFunction))
            {
                throw new ArgumentException("The object shall be an IFunction", nameof(obj));
            }

            writer.WriteStartObject();

            writer.WritePropertyName("@type");
            writer.WriteStringValue("Function");

            writer.WritePropertyName("@id");
            writer.WriteStringValue(iFunction.Id);

            writer.WriteStartArray("aliasIds");
            foreach (var item in iFunction.AliasIds)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("elementId");
            writer.WriteStringValue(iFunction.ElementId);

            writer.WritePropertyName("isAbstract");
            writer.WriteBooleanValue(iFunction.IsAbstract);

            writer.WritePropertyName("isImpliedIncluded");
            writer.WriteBooleanValue(iFunction.IsImpliedIncluded);

            writer.WritePropertyName("isSufficient");
            writer.WriteBooleanValue(iFunction.IsSufficient);

            writer.WritePropertyName("name");
            writer.WriteStringValue(iFunction.Name);
            writer.WriteStartArray("ownedRelationship");
            foreach (var item in iFunction.OwnedRelationship)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("owningRelationship");
            if (iFunction.OwningRelationship.HasValue)
            {
                writer.WriteStringValue(iFunction.OwningRelationship.Value);
            }
            else
            {
                writer.WriteNullValue();
            }
            writer.WritePropertyName("shortName");
            writer.WriteStringValue(iFunction.ShortName);
            writer.WriteEndObject();
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
