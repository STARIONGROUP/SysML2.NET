// -------------------------------------------------------------------------------------------------
// <copyright file="LiteralBooleanSerializer.cs" company="Starion Group S.A.">
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
    using SysML2.NET.Core.DTO.Kernel.Expressions;
    using SysML2.NET.Serializer.Json;

    /// <summary>
    /// The purpose of the <see cref="LiteralBooleanSerializer"/> is to provide serialization capabilities
    /// capabilities for the <see cref="ILiteralBoolean"/> interface
    /// </summary>
    internal static class LiteralBooleanSerializer
    {
        /// <summary>
        /// Serializes an instance of <see cref="ILiteralBoolean"/> using an <see cref="Utf8JsonWriter"/>
        /// </summary>
        /// <param name="obj">
        /// The <see cref="ILiteralBoolean"/> to serialize
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
            if (obj is not ILiteralBoolean iLiteralBoolean)
            {
                throw new ArgumentException("The object shall be an ILiteralBoolean", nameof(obj));
            }

            writer.WriteStartObject();

            writer.WritePropertyName("@type"u8);
            writer.WriteStringValue("LiteralBoolean"u8);

            writer.WritePropertyName("@id"u8);
            writer.WriteStringValue(iLiteralBoolean.Id);

            if (includeDerivedProperties)
            {
                SerializeAsJsonWithDerivedProperties(iLiteralBoolean, writer);
            }
            else
            {
                SerializeAsJsonWithoutDerivedProperties(iLiteralBoolean, writer);
            }

            writer.WriteEndObject();
        }

        /// <summary>
        /// Serializes an instance of <see cref="ILiteralBoolean"/> using an <see cref="Utf8JsonWriter"/> as JSON including derived properties
        /// </summary>
        /// <param name=" iLiteralBoolean">
        /// The <see cref="ILiteralBoolean"/> to serialize
        /// </param>
        /// <param name="writer">
        /// The target <see cref="Utf8JsonWriter"/>
        /// </param>
        private static void SerializeAsJsonWithDerivedProperties(ILiteralBoolean iLiteralBoolean, Utf8JsonWriter writer)
        {
            writer.WriteStartArray("aliasIds"u8);

            foreach (var item in iLiteralBoolean.AliasIds)
            {
                writer.WriteStringValue(item);
            }

            writer.WriteEndArray();

            writer.WriteStartArray("behavior"u8);

            foreach (var item in iLiteralBoolean.behavior)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WriteStartArray("chainingFeature"u8);

            foreach (var item in iLiteralBoolean.chainingFeature)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WritePropertyName("crossFeature"u8);

            if (iLiteralBoolean.crossFeature.HasValue)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(iLiteralBoolean.crossFeature.Value);
                writer.WriteEndObject();
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("declaredName"u8);
            writer.WriteStringValue(iLiteralBoolean.DeclaredName);

            writer.WritePropertyName("declaredShortName"u8);
            writer.WriteStringValue(iLiteralBoolean.DeclaredShortName);

            writer.WriteStartArray("differencingType"u8);

            foreach (var item in iLiteralBoolean.differencingType)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WriteStartArray("directedFeature"u8);

            foreach (var item in iLiteralBoolean.directedFeature)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WritePropertyName("direction"u8);

            if (iLiteralBoolean.Direction.HasValue)
            {
                writer.WriteStringValue(iLiteralBoolean.Direction.Value.ToString().ToLower());
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WriteStartArray("documentation"u8);

            foreach (var item in iLiteralBoolean.documentation)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WritePropertyName("elementId"u8);
            writer.WriteStringValue(iLiteralBoolean.ElementId);

            writer.WriteStartArray("endFeature"u8);

            foreach (var item in iLiteralBoolean.endFeature)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WritePropertyName("endOwningType"u8);

            if (iLiteralBoolean.endOwningType.HasValue)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(iLiteralBoolean.endOwningType.Value);
                writer.WriteEndObject();
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WriteStartArray("feature"u8);

            foreach (var item in iLiteralBoolean.feature)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WriteStartArray("featureMembership"u8);

            foreach (var item in iLiteralBoolean.featureMembership)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WritePropertyName("featureTarget"u8);
            writer.WriteStartObject();
            writer.WritePropertyName("@id"u8);
            writer.WriteStringValue(iLiteralBoolean.featureTarget);
            writer.WriteEndObject();

            writer.WriteStartArray("featuringType"u8);

            foreach (var item in iLiteralBoolean.featuringType)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WritePropertyName("function"u8);

            if (iLiteralBoolean.function.HasValue)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(iLiteralBoolean.function.Value);
                writer.WriteEndObject();
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WriteStartArray("importedMembership"u8);

            foreach (var item in iLiteralBoolean.importedMembership)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WriteStartArray("inheritedFeature"u8);

            foreach (var item in iLiteralBoolean.inheritedFeature)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WriteStartArray("inheritedMembership"u8);

            foreach (var item in iLiteralBoolean.inheritedMembership)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WriteStartArray("input"u8);

            foreach (var item in iLiteralBoolean.input)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WriteStartArray("intersectingType"u8);

            foreach (var item in iLiteralBoolean.intersectingType)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WritePropertyName("isAbstract"u8);
            writer.WriteBooleanValue(iLiteralBoolean.IsAbstract);

            writer.WritePropertyName("isComposite"u8);
            writer.WriteBooleanValue(iLiteralBoolean.IsComposite);

            writer.WritePropertyName("isConjugated"u8);
            writer.WriteBooleanValue(iLiteralBoolean.isConjugated);

            writer.WritePropertyName("isConstant"u8);
            writer.WriteBooleanValue(iLiteralBoolean.IsConstant);

            writer.WritePropertyName("isDerived"u8);
            writer.WriteBooleanValue(iLiteralBoolean.IsDerived);

            writer.WritePropertyName("isEnd"u8);
            writer.WriteBooleanValue(iLiteralBoolean.IsEnd);

            writer.WritePropertyName("isImpliedIncluded"u8);
            writer.WriteBooleanValue(iLiteralBoolean.IsImpliedIncluded);

            writer.WritePropertyName("isLibraryElement"u8);
            writer.WriteBooleanValue(iLiteralBoolean.isLibraryElement);

            writer.WritePropertyName("isModelLevelEvaluable"u8);
            writer.WriteBooleanValue(iLiteralBoolean.isModelLevelEvaluable);

            writer.WritePropertyName("isOrdered"u8);
            writer.WriteBooleanValue(iLiteralBoolean.IsOrdered);

            writer.WritePropertyName("isPortion"u8);
            writer.WriteBooleanValue(iLiteralBoolean.IsPortion);

            writer.WritePropertyName("isSufficient"u8);
            writer.WriteBooleanValue(iLiteralBoolean.IsSufficient);

            writer.WritePropertyName("isUnique"u8);
            writer.WriteBooleanValue(iLiteralBoolean.IsUnique);

            writer.WritePropertyName("isVariable"u8);
            writer.WriteBooleanValue(iLiteralBoolean.IsVariable);

            writer.WriteStartArray("member"u8);

            foreach (var item in iLiteralBoolean.member)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WriteStartArray("membership"u8);

            foreach (var item in iLiteralBoolean.membership)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WritePropertyName("multiplicity"u8);

            if (iLiteralBoolean.multiplicity.HasValue)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(iLiteralBoolean.multiplicity.Value);
                writer.WriteEndObject();
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("name"u8);
            writer.WriteStringValue(iLiteralBoolean.name);

            writer.WriteStartArray("output"u8);

            foreach (var item in iLiteralBoolean.output)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WriteStartArray("ownedAnnotation"u8);

            foreach (var item in iLiteralBoolean.ownedAnnotation)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WritePropertyName("ownedConjugator"u8);

            if (iLiteralBoolean.ownedConjugator.HasValue)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(iLiteralBoolean.ownedConjugator.Value);
                writer.WriteEndObject();
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("ownedCrossSubsetting"u8);

            if (iLiteralBoolean.ownedCrossSubsetting.HasValue)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(iLiteralBoolean.ownedCrossSubsetting.Value);
                writer.WriteEndObject();
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WriteStartArray("ownedDifferencing"u8);

            foreach (var item in iLiteralBoolean.ownedDifferencing)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WriteStartArray("ownedDisjoining"u8);

            foreach (var item in iLiteralBoolean.ownedDisjoining)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WriteStartArray("ownedElement"u8);

            foreach (var item in iLiteralBoolean.ownedElement)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WriteStartArray("ownedEndFeature"u8);

            foreach (var item in iLiteralBoolean.ownedEndFeature)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WriteStartArray("ownedFeature"u8);

            foreach (var item in iLiteralBoolean.ownedFeature)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WriteStartArray("ownedFeatureChaining"u8);

            foreach (var item in iLiteralBoolean.ownedFeatureChaining)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WriteStartArray("ownedFeatureInverting"u8);

            foreach (var item in iLiteralBoolean.ownedFeatureInverting)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WriteStartArray("ownedFeatureMembership"u8);

            foreach (var item in iLiteralBoolean.ownedFeatureMembership)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WriteStartArray("ownedImport"u8);

            foreach (var item in iLiteralBoolean.ownedImport)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WriteStartArray("ownedIntersecting"u8);

            foreach (var item in iLiteralBoolean.ownedIntersecting)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WriteStartArray("ownedMember"u8);

            foreach (var item in iLiteralBoolean.ownedMember)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WriteStartArray("ownedMembership"u8);

            foreach (var item in iLiteralBoolean.ownedMembership)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WriteStartArray("ownedRedefinition"u8);

            foreach (var item in iLiteralBoolean.ownedRedefinition)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WritePropertyName("ownedReferenceSubsetting"u8);

            if (iLiteralBoolean.ownedReferenceSubsetting.HasValue)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(iLiteralBoolean.ownedReferenceSubsetting.Value);
                writer.WriteEndObject();
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WriteStartArray("ownedRelationship"u8);

            foreach (var item in iLiteralBoolean.OwnedRelationship)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WriteStartArray("ownedSpecialization"u8);

            foreach (var item in iLiteralBoolean.ownedSpecialization)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WriteStartArray("ownedSubsetting"u8);

            foreach (var item in iLiteralBoolean.ownedSubsetting)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WriteStartArray("ownedTypeFeaturing"u8);

            foreach (var item in iLiteralBoolean.ownedTypeFeaturing)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WriteStartArray("ownedTyping"u8);

            foreach (var item in iLiteralBoolean.ownedTyping)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WriteStartArray("ownedUnioning"u8);

            foreach (var item in iLiteralBoolean.ownedUnioning)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WritePropertyName("owner"u8);

            if (iLiteralBoolean.owner.HasValue)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(iLiteralBoolean.owner.Value);
                writer.WriteEndObject();
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("owningFeatureMembership"u8);

            if (iLiteralBoolean.owningFeatureMembership.HasValue)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(iLiteralBoolean.owningFeatureMembership.Value);
                writer.WriteEndObject();
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("owningMembership"u8);

            if (iLiteralBoolean.owningMembership.HasValue)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(iLiteralBoolean.owningMembership.Value);
                writer.WriteEndObject();
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("owningNamespace"u8);

            if (iLiteralBoolean.owningNamespace.HasValue)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(iLiteralBoolean.owningNamespace.Value);
                writer.WriteEndObject();
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("owningRelationship"u8);

            if (iLiteralBoolean.OwningRelationship.HasValue)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(iLiteralBoolean.OwningRelationship.Value);
                writer.WriteEndObject();
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("owningType"u8);

            if (iLiteralBoolean.owningType.HasValue)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(iLiteralBoolean.owningType.Value);
                writer.WriteEndObject();
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WriteStartArray("parameter"u8);

            foreach (var item in iLiteralBoolean.parameter)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WritePropertyName("qualifiedName"u8);
            writer.WriteStringValue(iLiteralBoolean.qualifiedName);

            writer.WritePropertyName("result"u8);
            writer.WriteStartObject();
            writer.WritePropertyName("@id"u8);
            writer.WriteStringValue(iLiteralBoolean.result);
            writer.WriteEndObject();

            writer.WritePropertyName("shortName"u8);
            writer.WriteStringValue(iLiteralBoolean.shortName);

            writer.WriteStartArray("textualRepresentation"u8);

            foreach (var item in iLiteralBoolean.textualRepresentation)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WriteStartArray("type"u8);

            foreach (var item in iLiteralBoolean.type)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WriteStartArray("unioningType"u8);

            foreach (var item in iLiteralBoolean.unioningType)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WritePropertyName("value"u8);
            writer.WriteBooleanValue(iLiteralBoolean.Value);

        }

        /// <summary>
        /// Serializes an instance of <see cref="ILiteralBoolean"/> using an <see cref="Utf8JsonWriter"/> as JSON excluding derived properties
        /// </summary>
        /// <param name=" iLiteralBoolean">
        /// The <see cref="ILiteralBoolean"/> to serialize
        /// </param>
        /// <param name="writer">
        /// The target <see cref="Utf8JsonWriter"/>
        /// </param>
        private static void SerializeAsJsonWithoutDerivedProperties(ILiteralBoolean iLiteralBoolean, Utf8JsonWriter writer)
        {
            writer.WriteStartArray("aliasIds"u8);

            foreach (var item in iLiteralBoolean.AliasIds)
            {
                writer.WriteStringValue(item);
            }

            writer.WriteEndArray();

            writer.WritePropertyName("declaredName"u8);
            writer.WriteStringValue(iLiteralBoolean.DeclaredName);

            writer.WritePropertyName("declaredShortName"u8);
            writer.WriteStringValue(iLiteralBoolean.DeclaredShortName);

            writer.WritePropertyName("direction"u8);

            if (iLiteralBoolean.Direction.HasValue)
            {
                writer.WriteStringValue(iLiteralBoolean.Direction.Value.ToString().ToLower());
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("elementId"u8);
            writer.WriteStringValue(iLiteralBoolean.ElementId);

            writer.WritePropertyName("isAbstract"u8);
            writer.WriteBooleanValue(iLiteralBoolean.IsAbstract);

            writer.WritePropertyName("isComposite"u8);
            writer.WriteBooleanValue(iLiteralBoolean.IsComposite);

            writer.WritePropertyName("isConstant"u8);
            writer.WriteBooleanValue(iLiteralBoolean.IsConstant);

            writer.WritePropertyName("isDerived"u8);
            writer.WriteBooleanValue(iLiteralBoolean.IsDerived);

            writer.WritePropertyName("isEnd"u8);
            writer.WriteBooleanValue(iLiteralBoolean.IsEnd);

            writer.WritePropertyName("isImpliedIncluded"u8);
            writer.WriteBooleanValue(iLiteralBoolean.IsImpliedIncluded);

            writer.WritePropertyName("isOrdered"u8);
            writer.WriteBooleanValue(iLiteralBoolean.IsOrdered);

            writer.WritePropertyName("isPortion"u8);
            writer.WriteBooleanValue(iLiteralBoolean.IsPortion);

            writer.WritePropertyName("isSufficient"u8);
            writer.WriteBooleanValue(iLiteralBoolean.IsSufficient);

            writer.WritePropertyName("isUnique"u8);
            writer.WriteBooleanValue(iLiteralBoolean.IsUnique);

            writer.WritePropertyName("isVariable"u8);
            writer.WriteBooleanValue(iLiteralBoolean.IsVariable);

            writer.WriteStartArray("ownedRelationship"u8);

            foreach (var item in iLiteralBoolean.OwnedRelationship)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WritePropertyName("owningRelationship"u8);

            if (iLiteralBoolean.OwningRelationship.HasValue)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(iLiteralBoolean.OwningRelationship.Value);
                writer.WriteEndObject();
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("value"u8);
            writer.WriteBooleanValue(iLiteralBoolean.Value);

        }
    }
}
// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
