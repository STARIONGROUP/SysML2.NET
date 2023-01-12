// -------------------------------------------------------------------------------------------------
// <copyright file="LibraryPackageSerializer.cs" company="RHEA System S.A.">
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
    /// The purpose of the <see cref="LibraryPackageSerializer"/> is to provide serialization
    /// and deserialization capabilities
    /// </summary>
    internal static class LibraryPackageSerializer
    {
        /// <summary>
        /// Serializes an instance of <see cref="ILibraryPackage"/> using an <see cref="Utf8JsonWriter"/>
        /// </summary>
        /// <param name="obj">
        /// The <see cref="ILibraryPackage"/> to serialize
        /// </param>
        /// <param name="writer">
        /// The target <see cref="Utf8JsonWriter"/>
        /// </param>
        /// <param name="serializationModeKind">
        /// enumeration specifying what kind of serialization shall be used
        /// </param>
        internal static void Serialize(object obj, Utf8JsonWriter writer, SerializationModeKind serializationModeKind)
        {
            if (!(obj is ILibraryPackage iLibraryPackage))
            {
                throw new ArgumentException("The object shall be an ILibraryPackage", nameof(obj));
            }

            writer.WriteStartObject();

            writer.WritePropertyName("@type");
            writer.WriteStringValue("LibraryPackage");

            writer.WritePropertyName("@id");
            writer.WriteStringValue(iLibraryPackage.Id);

            writer.WriteStartArray("aliasIds");
            foreach (var item in iLibraryPackage.AliasIds)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("elementId");
            writer.WriteStringValue(iLibraryPackage.ElementId);

            writer.WritePropertyName("isImpliedIncluded");
            writer.WriteBooleanValue(iLibraryPackage.IsImpliedIncluded);

            writer.WritePropertyName("isStandard");
            writer.WriteBooleanValue(iLibraryPackage.IsStandard);

            writer.WritePropertyName("name");
            writer.WriteStringValue(iLibraryPackage.Name);
            writer.WriteStartArray("ownedRelationship");
            foreach (var item in iLibraryPackage.OwnedRelationship)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("owningRelationship");
            if (iLibraryPackage.OwningRelationship.HasValue)
            {
                writer.WriteStringValue(iLibraryPackage.OwningRelationship.Value);
            }
            else
            {
                writer.WriteNullValue();
            }
            writer.WritePropertyName("shortName");
            writer.WriteStringValue(iLibraryPackage.ShortName);
            writer.WriteEndObject();
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
