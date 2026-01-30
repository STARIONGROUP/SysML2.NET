// -------------------------------------------------------------------------------------------------
// <copyright file="IndexExpressionSerializer.cs" company="Starion Group S.A.">
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
    /// The purpose of the <see cref="IndexExpressionSerializer"/> is to provide serialization capabilities
    /// capabilities for the <see cref="IIndexExpression"/> interface
    /// </summary>
    internal static class IndexExpressionSerializer
    {
        /// <summary>
        /// Serializes an instance of <see cref="IIndexExpression"/> using an <see cref="Utf8JsonWriter"/>
        /// </summary>
        /// <param name="obj">
        /// The <see cref="IIndexExpression"/> to serialize
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
            if (obj is not IIndexExpression iIndexExpression)
            {
                throw new ArgumentException("The object shall be an IIndexExpression", nameof(obj));
            }

            writer.WriteStartObject();

            writer.WritePropertyName("@type"u8);
            writer.WriteStringValue("IndexExpression"u8);

            writer.WritePropertyName("@id"u8);
            writer.WriteStringValue(iIndexExpression.Id);

            if (includeDerivedProperties)
            {
                SerializeAsJsonWithDerivedProperties(iIndexExpression, writer);
            }
            else
            {
                SerializeAsJsonWithoutDerivedProperties(iIndexExpression, writer);
            }

            writer.WriteEndObject();
        }

        /// <summary>
        /// Serializes an instance of <see cref="IIndexExpression"/> using an <see cref="Utf8JsonWriter"/> as JSON including derived properties
        /// </summary>
        /// <param name=" iIndexExpression">
        /// The <see cref="IIndexExpression"/> to serialize
        /// </param>
        /// <param name="writer">
        /// The target <see cref="Utf8JsonWriter"/>
        /// </param>
        private static void SerializeAsJsonWithDerivedProperties(IIndexExpression iIndexExpression, Utf8JsonWriter writer)
        {
            writer.WriteStartArray("aliasIds"u8);

            foreach (var item in iIndexExpression.AliasIds)
            {
                writer.WriteStringValue(item);
            }

            writer.WriteEndArray();

            writer.WriteStartArray("argument"u8);

            foreach (var item in iIndexExpression.argument)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WriteStartArray("behavior"u8);

            foreach (var item in iIndexExpression.behavior)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WriteStartArray("chainingFeature"u8);

            foreach (var item in iIndexExpression.chainingFeature)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WritePropertyName("crossFeature"u8);

            if (iIndexExpression.crossFeature.HasValue)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(iIndexExpression.crossFeature.Value);
                writer.WriteEndObject();
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("declaredName"u8);
            writer.WriteStringValue(iIndexExpression.DeclaredName);

            writer.WritePropertyName("declaredShortName"u8);
            writer.WriteStringValue(iIndexExpression.DeclaredShortName);

            writer.WriteStartArray("differencingType"u8);

            foreach (var item in iIndexExpression.differencingType)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WriteStartArray("directedFeature"u8);

            foreach (var item in iIndexExpression.directedFeature)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WritePropertyName("direction"u8);

            if (iIndexExpression.Direction.HasValue)
            {
                writer.WriteStringValue(iIndexExpression.Direction.Value.ToString().ToLower());
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WriteStartArray("documentation"u8);

            foreach (var item in iIndexExpression.documentation)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WritePropertyName("elementId"u8);
            writer.WriteStringValue(iIndexExpression.ElementId);

            writer.WriteStartArray("endFeature"u8);

            foreach (var item in iIndexExpression.endFeature)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WritePropertyName("endOwningType"u8);

            if (iIndexExpression.endOwningType.HasValue)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(iIndexExpression.endOwningType.Value);
                writer.WriteEndObject();
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WriteStartArray("feature"u8);

            foreach (var item in iIndexExpression.feature)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WriteStartArray("featureMembership"u8);

            foreach (var item in iIndexExpression.featureMembership)
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
            writer.WriteStringValue(iIndexExpression.featureTarget);
            writer.WriteEndObject();

            writer.WriteStartArray("featuringType"u8);

            foreach (var item in iIndexExpression.featuringType)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WritePropertyName("function"u8);

            if (iIndexExpression.function.HasValue)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(iIndexExpression.function.Value);
                writer.WriteEndObject();
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WriteStartArray("importedMembership"u8);

            foreach (var item in iIndexExpression.importedMembership)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WriteStartArray("inheritedFeature"u8);

            foreach (var item in iIndexExpression.inheritedFeature)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WriteStartArray("inheritedMembership"u8);

            foreach (var item in iIndexExpression.inheritedMembership)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WriteStartArray("input"u8);

            foreach (var item in iIndexExpression.input)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WritePropertyName("instantiatedType"u8);
            writer.WriteStartObject();
            writer.WritePropertyName("@id"u8);
            writer.WriteStringValue(iIndexExpression.instantiatedType);
            writer.WriteEndObject();

            writer.WriteStartArray("intersectingType"u8);

            foreach (var item in iIndexExpression.intersectingType)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WritePropertyName("isAbstract"u8);
            writer.WriteBooleanValue(iIndexExpression.IsAbstract);

            writer.WritePropertyName("isComposite"u8);
            writer.WriteBooleanValue(iIndexExpression.IsComposite);

            writer.WritePropertyName("isConjugated"u8);
            writer.WriteBooleanValue(iIndexExpression.isConjugated);

            writer.WritePropertyName("isConstant"u8);
            writer.WriteBooleanValue(iIndexExpression.IsConstant);

            writer.WritePropertyName("isDerived"u8);
            writer.WriteBooleanValue(iIndexExpression.IsDerived);

            writer.WritePropertyName("isEnd"u8);
            writer.WriteBooleanValue(iIndexExpression.IsEnd);

            writer.WritePropertyName("isImpliedIncluded"u8);
            writer.WriteBooleanValue(iIndexExpression.IsImpliedIncluded);

            writer.WritePropertyName("isLibraryElement"u8);
            writer.WriteBooleanValue(iIndexExpression.isLibraryElement);

            writer.WritePropertyName("isModelLevelEvaluable"u8);
            writer.WriteBooleanValue(iIndexExpression.isModelLevelEvaluable);

            writer.WritePropertyName("isOrdered"u8);
            writer.WriteBooleanValue(iIndexExpression.IsOrdered);

            writer.WritePropertyName("isPortion"u8);
            writer.WriteBooleanValue(iIndexExpression.IsPortion);

            writer.WritePropertyName("isSufficient"u8);
            writer.WriteBooleanValue(iIndexExpression.IsSufficient);

            writer.WritePropertyName("isUnique"u8);
            writer.WriteBooleanValue(iIndexExpression.IsUnique);

            writer.WritePropertyName("isVariable"u8);
            writer.WriteBooleanValue(iIndexExpression.IsVariable);

            writer.WriteStartArray("member"u8);

            foreach (var item in iIndexExpression.member)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WriteStartArray("membership"u8);

            foreach (var item in iIndexExpression.membership)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WritePropertyName("multiplicity"u8);

            if (iIndexExpression.multiplicity.HasValue)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(iIndexExpression.multiplicity.Value);
                writer.WriteEndObject();
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("name"u8);
            writer.WriteStringValue(iIndexExpression.name);

            writer.WritePropertyName("operator"u8);
            writer.WriteStringValue(iIndexExpression.Operator);

            writer.WriteStartArray("output"u8);

            foreach (var item in iIndexExpression.output)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WriteStartArray("ownedAnnotation"u8);

            foreach (var item in iIndexExpression.ownedAnnotation)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WritePropertyName("ownedConjugator"u8);

            if (iIndexExpression.ownedConjugator.HasValue)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(iIndexExpression.ownedConjugator.Value);
                writer.WriteEndObject();
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("ownedCrossSubsetting"u8);

            if (iIndexExpression.ownedCrossSubsetting.HasValue)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(iIndexExpression.ownedCrossSubsetting.Value);
                writer.WriteEndObject();
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WriteStartArray("ownedDifferencing"u8);

            foreach (var item in iIndexExpression.ownedDifferencing)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WriteStartArray("ownedDisjoining"u8);

            foreach (var item in iIndexExpression.ownedDisjoining)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WriteStartArray("ownedElement"u8);

            foreach (var item in iIndexExpression.ownedElement)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WriteStartArray("ownedEndFeature"u8);

            foreach (var item in iIndexExpression.ownedEndFeature)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WriteStartArray("ownedFeature"u8);

            foreach (var item in iIndexExpression.ownedFeature)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WriteStartArray("ownedFeatureChaining"u8);

            foreach (var item in iIndexExpression.ownedFeatureChaining)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WriteStartArray("ownedFeatureInverting"u8);

            foreach (var item in iIndexExpression.ownedFeatureInverting)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WriteStartArray("ownedFeatureMembership"u8);

            foreach (var item in iIndexExpression.ownedFeatureMembership)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WriteStartArray("ownedImport"u8);

            foreach (var item in iIndexExpression.ownedImport)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WriteStartArray("ownedIntersecting"u8);

            foreach (var item in iIndexExpression.ownedIntersecting)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WriteStartArray("ownedMember"u8);

            foreach (var item in iIndexExpression.ownedMember)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WriteStartArray("ownedMembership"u8);

            foreach (var item in iIndexExpression.ownedMembership)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WriteStartArray("ownedRedefinition"u8);

            foreach (var item in iIndexExpression.ownedRedefinition)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WritePropertyName("ownedReferenceSubsetting"u8);

            if (iIndexExpression.ownedReferenceSubsetting.HasValue)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(iIndexExpression.ownedReferenceSubsetting.Value);
                writer.WriteEndObject();
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WriteStartArray("ownedRelationship"u8);

            foreach (var item in iIndexExpression.OwnedRelationship)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WriteStartArray("ownedSpecialization"u8);

            foreach (var item in iIndexExpression.ownedSpecialization)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WriteStartArray("ownedSubsetting"u8);

            foreach (var item in iIndexExpression.ownedSubsetting)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WriteStartArray("ownedTypeFeaturing"u8);

            foreach (var item in iIndexExpression.ownedTypeFeaturing)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WriteStartArray("ownedTyping"u8);

            foreach (var item in iIndexExpression.ownedTyping)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WriteStartArray("ownedUnioning"u8);

            foreach (var item in iIndexExpression.ownedUnioning)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WritePropertyName("owner"u8);

            if (iIndexExpression.owner.HasValue)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(iIndexExpression.owner.Value);
                writer.WriteEndObject();
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("owningFeatureMembership"u8);

            if (iIndexExpression.owningFeatureMembership.HasValue)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(iIndexExpression.owningFeatureMembership.Value);
                writer.WriteEndObject();
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("owningMembership"u8);

            if (iIndexExpression.owningMembership.HasValue)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(iIndexExpression.owningMembership.Value);
                writer.WriteEndObject();
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("owningNamespace"u8);

            if (iIndexExpression.owningNamespace.HasValue)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(iIndexExpression.owningNamespace.Value);
                writer.WriteEndObject();
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("owningRelationship"u8);

            if (iIndexExpression.OwningRelationship.HasValue)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(iIndexExpression.OwningRelationship.Value);
                writer.WriteEndObject();
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("owningType"u8);

            if (iIndexExpression.owningType.HasValue)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(iIndexExpression.owningType.Value);
                writer.WriteEndObject();
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WriteStartArray("parameter"u8);

            foreach (var item in iIndexExpression.parameter)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WritePropertyName("qualifiedName"u8);
            writer.WriteStringValue(iIndexExpression.qualifiedName);

            writer.WritePropertyName("result"u8);
            writer.WriteStartObject();
            writer.WritePropertyName("@id"u8);
            writer.WriteStringValue(iIndexExpression.result);
            writer.WriteEndObject();

            writer.WritePropertyName("shortName"u8);
            writer.WriteStringValue(iIndexExpression.shortName);

            writer.WriteStartArray("textualRepresentation"u8);

            foreach (var item in iIndexExpression.textualRepresentation)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WriteStartArray("type"u8);

            foreach (var item in iIndexExpression.type)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WriteStartArray("unioningType"u8);

            foreach (var item in iIndexExpression.unioningType)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

        }

        /// <summary>
        /// Serializes an instance of <see cref="IIndexExpression"/> using an <see cref="Utf8JsonWriter"/> as JSON excluding derived properties
        /// </summary>
        /// <param name=" iIndexExpression">
        /// The <see cref="IIndexExpression"/> to serialize
        /// </param>
        /// <param name="writer">
        /// The target <see cref="Utf8JsonWriter"/>
        /// </param>
        private static void SerializeAsJsonWithoutDerivedProperties(IIndexExpression iIndexExpression, Utf8JsonWriter writer)
        {
            writer.WriteStartArray("aliasIds"u8);

            foreach (var item in iIndexExpression.AliasIds)
            {
                writer.WriteStringValue(item);
            }

            writer.WriteEndArray();

            writer.WritePropertyName("declaredName"u8);
            writer.WriteStringValue(iIndexExpression.DeclaredName);

            writer.WritePropertyName("declaredShortName"u8);
            writer.WriteStringValue(iIndexExpression.DeclaredShortName);

            writer.WritePropertyName("direction"u8);

            if (iIndexExpression.Direction.HasValue)
            {
                writer.WriteStringValue(iIndexExpression.Direction.Value.ToString().ToLower());
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("elementId"u8);
            writer.WriteStringValue(iIndexExpression.ElementId);

            writer.WritePropertyName("isAbstract"u8);
            writer.WriteBooleanValue(iIndexExpression.IsAbstract);

            writer.WritePropertyName("isComposite"u8);
            writer.WriteBooleanValue(iIndexExpression.IsComposite);

            writer.WritePropertyName("isConstant"u8);
            writer.WriteBooleanValue(iIndexExpression.IsConstant);

            writer.WritePropertyName("isDerived"u8);
            writer.WriteBooleanValue(iIndexExpression.IsDerived);

            writer.WritePropertyName("isEnd"u8);
            writer.WriteBooleanValue(iIndexExpression.IsEnd);

            writer.WritePropertyName("isImpliedIncluded"u8);
            writer.WriteBooleanValue(iIndexExpression.IsImpliedIncluded);

            writer.WritePropertyName("isOrdered"u8);
            writer.WriteBooleanValue(iIndexExpression.IsOrdered);

            writer.WritePropertyName("isPortion"u8);
            writer.WriteBooleanValue(iIndexExpression.IsPortion);

            writer.WritePropertyName("isSufficient"u8);
            writer.WriteBooleanValue(iIndexExpression.IsSufficient);

            writer.WritePropertyName("isUnique"u8);
            writer.WriteBooleanValue(iIndexExpression.IsUnique);

            writer.WritePropertyName("isVariable"u8);
            writer.WriteBooleanValue(iIndexExpression.IsVariable);

            writer.WritePropertyName("operator"u8);
            writer.WriteStringValue(iIndexExpression.Operator);

            writer.WriteStartArray("ownedRelationship"u8);

            foreach (var item in iIndexExpression.OwnedRelationship)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WritePropertyName("owningRelationship"u8);

            if (iIndexExpression.OwningRelationship.HasValue)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(iIndexExpression.OwningRelationship.Value);
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
