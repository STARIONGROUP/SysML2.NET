// -------------------------------------------------------------------------------------------------
// <copyright file="ClassSerializer.cs" company="Starion Group S.A.">
//
//   Copyright 2022-2026 Starion Group S.A.
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

namespace SysML2.NET.Serializer.Json.Core.DTO
{
    using System;
    using System.Text.Json;

    using SysML2.NET.Common;
    using SysML2.NET.Core.DTO.Kernel.Classes;
    using SysML2.NET.Serializer.Json;

    /// <summary>
    /// The purpose of the <see cref="ClassSerializer"/> is to provide serialization capabilities
    /// capabilities for the <see cref="IClass"/> interface
    /// </summary>
    internal static class ClassSerializer
    {
        /// <summary>
        /// Serializes an instance of <see cref="IClass"/> using an <see cref="Utf8JsonWriter"/>
        /// </summary>
        /// <param name="obj">
        /// The <see cref="IClass"/> to serialize
        /// </param>
        /// <param name="writer">
        /// The target <see cref="Utf8JsonWriter"/>
        /// </param>
        /// <param name="serializationModeKind">
        /// enumeration specifying what kind of serialization shall be used
        /// </param>
        /// <param name="includeDerivedProperties">
        /// Asserts that derived properties should also be part of the serialization
        /// </param>
        internal static void Serialize(object obj, Utf8JsonWriter writer, SerializationModeKind serializationModeKind, bool includeDerivedProperties)
        {
            if (obj is not IClass iClass)
            {
                throw new ArgumentException("The object shall be an IClass", nameof(obj));
            }

            writer.WriteStartObject();

            writer.WritePropertyName("@type"u8);
            writer.WriteStringValue("Class"u8);

            writer.WritePropertyName("@id"u8);
            writer.WriteStringValue(iClass.Id);

            if (includeDerivedProperties)
            {
                SerializeAsJsonWithDerivedProperties(iClass, writer);
            }
            else
            {
                SerializeAsJsonWithoutDerivedProperties(iClass, writer);
            }

            writer.WriteEndObject();
        }

        /// <summary>
        /// Serializes an instance of <see cref="IClass"/> using an <see cref="Utf8JsonWriter"/> as JSON including derived properties
        /// </summary>
        /// <param name=" iClass">
        /// The <see cref="IClass"/> to serialize
        /// </param>
        /// <param name="writer">
        /// The target <see cref="Utf8JsonWriter"/>
        /// </param>
        private static void SerializeAsJsonWithDerivedProperties(IClass iClass, Utf8JsonWriter writer)
        {
            writer.WriteStartArray("aliasIds"u8);

            foreach (var item in iClass.AliasIds)
            {
                writer.WriteStringValue(item);
            }

            writer.WriteEndArray();

            writer.WritePropertyName("declaredName"u8);
            writer.WriteStringValue(iClass.DeclaredName);

            writer.WritePropertyName("declaredShortName"u8);
            writer.WriteStringValue(iClass.DeclaredShortName);

            writer.WriteStartArray("differencingType"u8);

            foreach (var item in iClass.differencingType)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WriteStartArray("directedFeature"u8);

            foreach (var item in iClass.directedFeature)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WriteStartArray("documentation"u8);

            foreach (var item in iClass.documentation)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WritePropertyName("elementId"u8);
            writer.WriteStringValue(iClass.ElementId);

            writer.WriteStartArray("endFeature"u8);

            foreach (var item in iClass.endFeature)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WriteStartArray("feature"u8);

            foreach (var item in iClass.feature)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WriteStartArray("featureMembership"u8);

            foreach (var item in iClass.featureMembership)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WriteStartArray("importedMembership"u8);

            foreach (var item in iClass.importedMembership)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WriteStartArray("inheritedFeature"u8);

            foreach (var item in iClass.inheritedFeature)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WriteStartArray("inheritedMembership"u8);

            foreach (var item in iClass.inheritedMembership)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WriteStartArray("input"u8);

            foreach (var item in iClass.input)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WriteStartArray("intersectingType"u8);

            foreach (var item in iClass.intersectingType)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WritePropertyName("isAbstract"u8);
            writer.WriteBooleanValue(iClass.IsAbstract);

            writer.WritePropertyName("isConjugated"u8);
            writer.WriteBooleanValue(iClass.isConjugated);

            writer.WritePropertyName("isImpliedIncluded"u8);
            writer.WriteBooleanValue(iClass.IsImpliedIncluded);

            writer.WritePropertyName("isLibraryElement"u8);
            writer.WriteBooleanValue(iClass.isLibraryElement);

            writer.WritePropertyName("isSufficient"u8);
            writer.WriteBooleanValue(iClass.IsSufficient);

            writer.WriteStartArray("member"u8);

            foreach (var item in iClass.member)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WriteStartArray("membership"u8);

            foreach (var item in iClass.membership)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WritePropertyName("multiplicity"u8);

            if (iClass.multiplicity.HasValue)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(iClass.multiplicity.Value);
                writer.WriteEndObject();
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("name"u8);
            writer.WriteStringValue(iClass.name);

            writer.WriteStartArray("output"u8);

            foreach (var item in iClass.output)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WriteStartArray("ownedAnnotation"u8);

            foreach (var item in iClass.ownedAnnotation)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WritePropertyName("ownedConjugator"u8);

            if (iClass.ownedConjugator.HasValue)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(iClass.ownedConjugator.Value);
                writer.WriteEndObject();
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WriteStartArray("ownedDifferencing"u8);

            foreach (var item in iClass.ownedDifferencing)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WriteStartArray("ownedDisjoining"u8);

            foreach (var item in iClass.ownedDisjoining)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WriteStartArray("ownedElement"u8);

            foreach (var item in iClass.ownedElement)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WriteStartArray("ownedEndFeature"u8);

            foreach (var item in iClass.ownedEndFeature)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WriteStartArray("ownedFeature"u8);

            foreach (var item in iClass.ownedFeature)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WriteStartArray("ownedFeatureMembership"u8);

            foreach (var item in iClass.ownedFeatureMembership)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WriteStartArray("ownedImport"u8);

            foreach (var item in iClass.ownedImport)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WriteStartArray("ownedIntersecting"u8);

            foreach (var item in iClass.ownedIntersecting)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WriteStartArray("ownedMember"u8);

            foreach (var item in iClass.ownedMember)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WriteStartArray("ownedMembership"u8);

            foreach (var item in iClass.ownedMembership)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WriteStartArray("ownedRelationship"u8);

            foreach (var item in iClass.OwnedRelationship)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WriteStartArray("ownedSpecialization"u8);

            foreach (var item in iClass.ownedSpecialization)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WriteStartArray("ownedSubclassification"u8);

            foreach (var item in iClass.ownedSubclassification)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WriteStartArray("ownedUnioning"u8);

            foreach (var item in iClass.ownedUnioning)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WritePropertyName("owner"u8);

            if (iClass.owner.HasValue)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(iClass.owner.Value);
                writer.WriteEndObject();
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("owningMembership"u8);

            if (iClass.owningMembership.HasValue)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(iClass.owningMembership.Value);
                writer.WriteEndObject();
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("owningNamespace"u8);

            if (iClass.owningNamespace.HasValue)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(iClass.owningNamespace.Value);
                writer.WriteEndObject();
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("owningRelationship"u8);

            if (iClass.OwningRelationship.HasValue)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(iClass.OwningRelationship.Value);
                writer.WriteEndObject();
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("qualifiedName"u8);
            writer.WriteStringValue(iClass.qualifiedName);

            writer.WritePropertyName("shortName"u8);
            writer.WriteStringValue(iClass.shortName);

            writer.WriteStartArray("textualRepresentation"u8);

            foreach (var item in iClass.textualRepresentation)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WriteStartArray("unioningType"u8);

            foreach (var item in iClass.unioningType)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

        }

        /// <summary>
        /// Serializes an instance of <see cref="IClass"/> using an <see cref="Utf8JsonWriter"/> as JSON excluding derived properties
        /// </summary>
        /// <param name=" iClass">
        /// The <see cref="IClass"/> to serialize
        /// </param>
        /// <param name="writer">
        /// The target <see cref="Utf8JsonWriter"/>
        /// </param>
        private static void SerializeAsJsonWithoutDerivedProperties(IClass iClass, Utf8JsonWriter writer)
        {
            writer.WriteStartArray("aliasIds"u8);

            foreach (var item in iClass.AliasIds)
            {
                writer.WriteStringValue(item);
            }

            writer.WriteEndArray();

            writer.WritePropertyName("declaredName"u8);
            writer.WriteStringValue(iClass.DeclaredName);

            writer.WritePropertyName("declaredShortName"u8);
            writer.WriteStringValue(iClass.DeclaredShortName);

            writer.WritePropertyName("elementId"u8);
            writer.WriteStringValue(iClass.ElementId);

            writer.WritePropertyName("isAbstract"u8);
            writer.WriteBooleanValue(iClass.IsAbstract);

            writer.WritePropertyName("isImpliedIncluded"u8);
            writer.WriteBooleanValue(iClass.IsImpliedIncluded);

            writer.WritePropertyName("isSufficient"u8);
            writer.WriteBooleanValue(iClass.IsSufficient);

            writer.WriteStartArray("ownedRelationship"u8);

            foreach (var item in iClass.OwnedRelationship)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WritePropertyName("owningRelationship"u8);

            if (iClass.OwningRelationship.HasValue)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(iClass.OwningRelationship.Value);
                writer.WriteEndObject();
            }
            else
            {
                writer.WriteNullValue();
            }

        }
    }
}
// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
