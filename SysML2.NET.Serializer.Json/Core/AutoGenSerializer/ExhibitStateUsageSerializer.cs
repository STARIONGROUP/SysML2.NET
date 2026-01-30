// -------------------------------------------------------------------------------------------------
// <copyright file="ExhibitStateUsageSerializer.cs" company="Starion Group S.A.">
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
    using SysML2.NET.Core.DTO.Systems.States;
    using SysML2.NET.Serializer.Json;

    /// <summary>
    /// The purpose of the <see cref="ExhibitStateUsageSerializer"/> is to provide serialization capabilities
    /// capabilities for the <see cref="IExhibitStateUsage"/> interface
    /// </summary>
    internal static class ExhibitStateUsageSerializer
    {
        /// <summary>
        /// Serializes an instance of <see cref="IExhibitStateUsage"/> using an <see cref="Utf8JsonWriter"/>
        /// </summary>
        /// <param name="obj">
        /// The <see cref="IExhibitStateUsage"/> to serialize
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
            if (obj is not IExhibitStateUsage iExhibitStateUsage)
            {
                throw new ArgumentException("The object shall be an IExhibitStateUsage", nameof(obj));
            }

            writer.WriteStartObject();

            writer.WritePropertyName("@type"u8);
            writer.WriteStringValue("ExhibitStateUsage"u8);

            writer.WritePropertyName("@id"u8);
            writer.WriteStringValue(iExhibitStateUsage.Id);

            if (includeDerivedProperties)
            {
                SerializeAsJsonWithDerivedProperties(iExhibitStateUsage, writer);
            }
            else
            {
                SerializeAsJsonWithoutDerivedProperties(iExhibitStateUsage, writer);
            }

            writer.WriteEndObject();
        }

        /// <summary>
        /// Serializes an instance of <see cref="IExhibitStateUsage"/> using an <see cref="Utf8JsonWriter"/> as JSON including derived properties
        /// </summary>
        /// <param name=" iExhibitStateUsage">
        /// The <see cref="IExhibitStateUsage"/> to serialize
        /// </param>
        /// <param name="writer">
        /// The target <see cref="Utf8JsonWriter"/>
        /// </param>
        private static void SerializeAsJsonWithDerivedProperties(IExhibitStateUsage iExhibitStateUsage, Utf8JsonWriter writer)
        {
            writer.WriteStartArray("actionDefinition"u8);

            foreach (var item in iExhibitStateUsage.actionDefinition)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WriteStartArray("aliasIds"u8);

            foreach (var item in iExhibitStateUsage.AliasIds)
            {
                writer.WriteStringValue(item);
            }

            writer.WriteEndArray();

            writer.WriteStartArray("behavior"u8);

            foreach (var item in iExhibitStateUsage.behavior)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WriteStartArray("chainingFeature"u8);

            foreach (var item in iExhibitStateUsage.chainingFeature)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WritePropertyName("crossFeature"u8);

            if (iExhibitStateUsage.crossFeature.HasValue)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(iExhibitStateUsage.crossFeature.Value);
                writer.WriteEndObject();
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("declaredName"u8);
            writer.WriteStringValue(iExhibitStateUsage.DeclaredName);

            writer.WritePropertyName("declaredShortName"u8);
            writer.WriteStringValue(iExhibitStateUsage.DeclaredShortName);

            writer.WriteStartArray("definition"u8);

            foreach (var item in iExhibitStateUsage.definition)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WriteStartArray("differencingType"u8);

            foreach (var item in iExhibitStateUsage.differencingType)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WriteStartArray("directedFeature"u8);

            foreach (var item in iExhibitStateUsage.directedFeature)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WriteStartArray("directedUsage"u8);

            foreach (var item in iExhibitStateUsage.directedUsage)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WritePropertyName("direction"u8);

            if (iExhibitStateUsage.Direction.HasValue)
            {
                writer.WriteStringValue(iExhibitStateUsage.Direction.Value.ToString().ToLower());
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("doAction"u8);

            if (iExhibitStateUsage.doAction.HasValue)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(iExhibitStateUsage.doAction.Value);
                writer.WriteEndObject();
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WriteStartArray("documentation"u8);

            foreach (var item in iExhibitStateUsage.documentation)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WritePropertyName("elementId"u8);
            writer.WriteStringValue(iExhibitStateUsage.ElementId);

            writer.WriteStartArray("endFeature"u8);

            foreach (var item in iExhibitStateUsage.endFeature)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WritePropertyName("endOwningType"u8);

            if (iExhibitStateUsage.endOwningType.HasValue)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(iExhibitStateUsage.endOwningType.Value);
                writer.WriteEndObject();
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("entryAction"u8);

            if (iExhibitStateUsage.entryAction.HasValue)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(iExhibitStateUsage.entryAction.Value);
                writer.WriteEndObject();
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("eventOccurrence"u8);
            writer.WriteStartObject();
            writer.WritePropertyName("@id"u8);
            writer.WriteStringValue(iExhibitStateUsage.eventOccurrence);
            writer.WriteEndObject();

            writer.WritePropertyName("exhibitedState"u8);
            writer.WriteStartObject();
            writer.WritePropertyName("@id"u8);
            writer.WriteStringValue(iExhibitStateUsage.exhibitedState);
            writer.WriteEndObject();

            writer.WritePropertyName("exitAction"u8);

            if (iExhibitStateUsage.exitAction.HasValue)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(iExhibitStateUsage.exitAction.Value);
                writer.WriteEndObject();
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WriteStartArray("feature"u8);

            foreach (var item in iExhibitStateUsage.feature)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WriteStartArray("featureMembership"u8);

            foreach (var item in iExhibitStateUsage.featureMembership)
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
            writer.WriteStringValue(iExhibitStateUsage.featureTarget);
            writer.WriteEndObject();

            writer.WriteStartArray("featuringType"u8);

            foreach (var item in iExhibitStateUsage.featuringType)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WriteStartArray("importedMembership"u8);

            foreach (var item in iExhibitStateUsage.importedMembership)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WritePropertyName("individualDefinition"u8);

            if (iExhibitStateUsage.individualDefinition.HasValue)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(iExhibitStateUsage.individualDefinition.Value);
                writer.WriteEndObject();
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WriteStartArray("inheritedFeature"u8);

            foreach (var item in iExhibitStateUsage.inheritedFeature)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WriteStartArray("inheritedMembership"u8);

            foreach (var item in iExhibitStateUsage.inheritedMembership)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WriteStartArray("input"u8);

            foreach (var item in iExhibitStateUsage.input)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WriteStartArray("intersectingType"u8);

            foreach (var item in iExhibitStateUsage.intersectingType)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WritePropertyName("isAbstract"u8);
            writer.WriteBooleanValue(iExhibitStateUsage.IsAbstract);

            writer.WritePropertyName("isComposite"u8);
            writer.WriteBooleanValue(iExhibitStateUsage.IsComposite);

            writer.WritePropertyName("isConjugated"u8);
            writer.WriteBooleanValue(iExhibitStateUsage.isConjugated);

            writer.WritePropertyName("isConstant"u8);
            writer.WriteBooleanValue(iExhibitStateUsage.IsConstant);

            writer.WritePropertyName("isDerived"u8);
            writer.WriteBooleanValue(iExhibitStateUsage.IsDerived);

            writer.WritePropertyName("isEnd"u8);
            writer.WriteBooleanValue(iExhibitStateUsage.IsEnd);

            writer.WritePropertyName("isImpliedIncluded"u8);
            writer.WriteBooleanValue(iExhibitStateUsage.IsImpliedIncluded);

            writer.WritePropertyName("isIndividual"u8);
            writer.WriteBooleanValue(iExhibitStateUsage.IsIndividual);

            writer.WritePropertyName("isLibraryElement"u8);
            writer.WriteBooleanValue(iExhibitStateUsage.isLibraryElement);

            writer.WritePropertyName("isOrdered"u8);
            writer.WriteBooleanValue(iExhibitStateUsage.IsOrdered);

            writer.WritePropertyName("isParallel"u8);
            writer.WriteBooleanValue(iExhibitStateUsage.IsParallel);

            writer.WritePropertyName("isPortion"u8);
            writer.WriteBooleanValue(iExhibitStateUsage.IsPortion);

            writer.WritePropertyName("isReference"u8);
            writer.WriteBooleanValue(iExhibitStateUsage.isReference);

            writer.WritePropertyName("isSufficient"u8);
            writer.WriteBooleanValue(iExhibitStateUsage.IsSufficient);

            writer.WritePropertyName("isUnique"u8);
            writer.WriteBooleanValue(iExhibitStateUsage.IsUnique);

            writer.WritePropertyName("isVariable"u8);
            writer.WriteBooleanValue(iExhibitStateUsage.IsVariable);

            writer.WritePropertyName("isVariation"u8);
            writer.WriteBooleanValue(iExhibitStateUsage.IsVariation);

            writer.WritePropertyName("mayTimeVary"u8);
            writer.WriteBooleanValue(iExhibitStateUsage.mayTimeVary);

            writer.WriteStartArray("member"u8);

            foreach (var item in iExhibitStateUsage.member)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WriteStartArray("membership"u8);

            foreach (var item in iExhibitStateUsage.membership)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WritePropertyName("multiplicity"u8);

            if (iExhibitStateUsage.multiplicity.HasValue)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(iExhibitStateUsage.multiplicity.Value);
                writer.WriteEndObject();
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("name"u8);
            writer.WriteStringValue(iExhibitStateUsage.name);

            writer.WriteStartArray("nestedAction"u8);

            foreach (var item in iExhibitStateUsage.nestedAction)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WriteStartArray("nestedAllocation"u8);

            foreach (var item in iExhibitStateUsage.nestedAllocation)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WriteStartArray("nestedAnalysisCase"u8);

            foreach (var item in iExhibitStateUsage.nestedAnalysisCase)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WriteStartArray("nestedAttribute"u8);

            foreach (var item in iExhibitStateUsage.nestedAttribute)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WriteStartArray("nestedCalculation"u8);

            foreach (var item in iExhibitStateUsage.nestedCalculation)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WriteStartArray("nestedCase"u8);

            foreach (var item in iExhibitStateUsage.nestedCase)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WriteStartArray("nestedConcern"u8);

            foreach (var item in iExhibitStateUsage.nestedConcern)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WriteStartArray("nestedConnection"u8);

            foreach (var item in iExhibitStateUsage.nestedConnection)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WriteStartArray("nestedConstraint"u8);

            foreach (var item in iExhibitStateUsage.nestedConstraint)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WriteStartArray("nestedEnumeration"u8);

            foreach (var item in iExhibitStateUsage.nestedEnumeration)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WriteStartArray("nestedFlow"u8);

            foreach (var item in iExhibitStateUsage.nestedFlow)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WriteStartArray("nestedInterface"u8);

            foreach (var item in iExhibitStateUsage.nestedInterface)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WriteStartArray("nestedItem"u8);

            foreach (var item in iExhibitStateUsage.nestedItem)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WriteStartArray("nestedMetadata"u8);

            foreach (var item in iExhibitStateUsage.nestedMetadata)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WriteStartArray("nestedOccurrence"u8);

            foreach (var item in iExhibitStateUsage.nestedOccurrence)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WriteStartArray("nestedPart"u8);

            foreach (var item in iExhibitStateUsage.nestedPart)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WriteStartArray("nestedPort"u8);

            foreach (var item in iExhibitStateUsage.nestedPort)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WriteStartArray("nestedReference"u8);

            foreach (var item in iExhibitStateUsage.nestedReference)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WriteStartArray("nestedRendering"u8);

            foreach (var item in iExhibitStateUsage.nestedRendering)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WriteStartArray("nestedRequirement"u8);

            foreach (var item in iExhibitStateUsage.nestedRequirement)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WriteStartArray("nestedState"u8);

            foreach (var item in iExhibitStateUsage.nestedState)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WriteStartArray("nestedTransition"u8);

            foreach (var item in iExhibitStateUsage.nestedTransition)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WriteStartArray("nestedUsage"u8);

            foreach (var item in iExhibitStateUsage.nestedUsage)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WriteStartArray("nestedUseCase"u8);

            foreach (var item in iExhibitStateUsage.nestedUseCase)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WriteStartArray("nestedVerificationCase"u8);

            foreach (var item in iExhibitStateUsage.nestedVerificationCase)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WriteStartArray("nestedView"u8);

            foreach (var item in iExhibitStateUsage.nestedView)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WriteStartArray("nestedViewpoint"u8);

            foreach (var item in iExhibitStateUsage.nestedViewpoint)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WriteStartArray("occurrenceDefinition"u8);

            foreach (var item in iExhibitStateUsage.occurrenceDefinition)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WriteStartArray("output"u8);

            foreach (var item in iExhibitStateUsage.output)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WriteStartArray("ownedAnnotation"u8);

            foreach (var item in iExhibitStateUsage.ownedAnnotation)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WritePropertyName("ownedConjugator"u8);

            if (iExhibitStateUsage.ownedConjugator.HasValue)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(iExhibitStateUsage.ownedConjugator.Value);
                writer.WriteEndObject();
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("ownedCrossSubsetting"u8);

            if (iExhibitStateUsage.ownedCrossSubsetting.HasValue)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(iExhibitStateUsage.ownedCrossSubsetting.Value);
                writer.WriteEndObject();
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WriteStartArray("ownedDifferencing"u8);

            foreach (var item in iExhibitStateUsage.ownedDifferencing)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WriteStartArray("ownedDisjoining"u8);

            foreach (var item in iExhibitStateUsage.ownedDisjoining)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WriteStartArray("ownedElement"u8);

            foreach (var item in iExhibitStateUsage.ownedElement)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WriteStartArray("ownedEndFeature"u8);

            foreach (var item in iExhibitStateUsage.ownedEndFeature)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WriteStartArray("ownedFeature"u8);

            foreach (var item in iExhibitStateUsage.ownedFeature)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WriteStartArray("ownedFeatureChaining"u8);

            foreach (var item in iExhibitStateUsage.ownedFeatureChaining)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WriteStartArray("ownedFeatureInverting"u8);

            foreach (var item in iExhibitStateUsage.ownedFeatureInverting)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WriteStartArray("ownedFeatureMembership"u8);

            foreach (var item in iExhibitStateUsage.ownedFeatureMembership)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WriteStartArray("ownedImport"u8);

            foreach (var item in iExhibitStateUsage.ownedImport)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WriteStartArray("ownedIntersecting"u8);

            foreach (var item in iExhibitStateUsage.ownedIntersecting)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WriteStartArray("ownedMember"u8);

            foreach (var item in iExhibitStateUsage.ownedMember)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WriteStartArray("ownedMembership"u8);

            foreach (var item in iExhibitStateUsage.ownedMembership)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WriteStartArray("ownedRedefinition"u8);

            foreach (var item in iExhibitStateUsage.ownedRedefinition)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WritePropertyName("ownedReferenceSubsetting"u8);

            if (iExhibitStateUsage.ownedReferenceSubsetting.HasValue)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(iExhibitStateUsage.ownedReferenceSubsetting.Value);
                writer.WriteEndObject();
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WriteStartArray("ownedRelationship"u8);

            foreach (var item in iExhibitStateUsage.OwnedRelationship)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WriteStartArray("ownedSpecialization"u8);

            foreach (var item in iExhibitStateUsage.ownedSpecialization)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WriteStartArray("ownedSubsetting"u8);

            foreach (var item in iExhibitStateUsage.ownedSubsetting)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WriteStartArray("ownedTypeFeaturing"u8);

            foreach (var item in iExhibitStateUsage.ownedTypeFeaturing)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WriteStartArray("ownedTyping"u8);

            foreach (var item in iExhibitStateUsage.ownedTyping)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WriteStartArray("ownedUnioning"u8);

            foreach (var item in iExhibitStateUsage.ownedUnioning)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WritePropertyName("owner"u8);

            if (iExhibitStateUsage.owner.HasValue)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(iExhibitStateUsage.owner.Value);
                writer.WriteEndObject();
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("owningDefinition"u8);

            if (iExhibitStateUsage.owningDefinition.HasValue)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(iExhibitStateUsage.owningDefinition.Value);
                writer.WriteEndObject();
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("owningFeatureMembership"u8);

            if (iExhibitStateUsage.owningFeatureMembership.HasValue)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(iExhibitStateUsage.owningFeatureMembership.Value);
                writer.WriteEndObject();
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("owningMembership"u8);

            if (iExhibitStateUsage.owningMembership.HasValue)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(iExhibitStateUsage.owningMembership.Value);
                writer.WriteEndObject();
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("owningNamespace"u8);

            if (iExhibitStateUsage.owningNamespace.HasValue)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(iExhibitStateUsage.owningNamespace.Value);
                writer.WriteEndObject();
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("owningRelationship"u8);

            if (iExhibitStateUsage.OwningRelationship.HasValue)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(iExhibitStateUsage.OwningRelationship.Value);
                writer.WriteEndObject();
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("owningType"u8);

            if (iExhibitStateUsage.owningType.HasValue)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(iExhibitStateUsage.owningType.Value);
                writer.WriteEndObject();
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("owningUsage"u8);

            if (iExhibitStateUsage.owningUsage.HasValue)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(iExhibitStateUsage.owningUsage.Value);
                writer.WriteEndObject();
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WriteStartArray("parameter"u8);

            foreach (var item in iExhibitStateUsage.parameter)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WritePropertyName("performedAction"u8);
            writer.WriteStartObject();
            writer.WritePropertyName("@id"u8);
            writer.WriteStringValue(iExhibitStateUsage.performedAction);
            writer.WriteEndObject();

            writer.WritePropertyName("portionKind"u8);

            if (iExhibitStateUsage.PortionKind.HasValue)
            {
                writer.WriteStringValue(iExhibitStateUsage.PortionKind.Value.ToString().ToLower());
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("qualifiedName"u8);
            writer.WriteStringValue(iExhibitStateUsage.qualifiedName);

            writer.WritePropertyName("shortName"u8);
            writer.WriteStringValue(iExhibitStateUsage.shortName);

            writer.WriteStartArray("stateDefinition"u8);

            foreach (var item in iExhibitStateUsage.stateDefinition)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WriteStartArray("textualRepresentation"u8);

            foreach (var item in iExhibitStateUsage.textualRepresentation)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WriteStartArray("type"u8);

            foreach (var item in iExhibitStateUsage.type)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WriteStartArray("unioningType"u8);

            foreach (var item in iExhibitStateUsage.unioningType)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WriteStartArray("usage"u8);

            foreach (var item in iExhibitStateUsage.usage)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WriteStartArray("variant"u8);

            foreach (var item in iExhibitStateUsage.variant)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WriteStartArray("variantMembership"u8);

            foreach (var item in iExhibitStateUsage.variantMembership)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

        }

        /// <summary>
        /// Serializes an instance of <see cref="IExhibitStateUsage"/> using an <see cref="Utf8JsonWriter"/> as JSON excluding derived properties
        /// </summary>
        /// <param name=" iExhibitStateUsage">
        /// The <see cref="IExhibitStateUsage"/> to serialize
        /// </param>
        /// <param name="writer">
        /// The target <see cref="Utf8JsonWriter"/>
        /// </param>
        private static void SerializeAsJsonWithoutDerivedProperties(IExhibitStateUsage iExhibitStateUsage, Utf8JsonWriter writer)
        {
            writer.WriteStartArray("aliasIds"u8);

            foreach (var item in iExhibitStateUsage.AliasIds)
            {
                writer.WriteStringValue(item);
            }

            writer.WriteEndArray();

            writer.WritePropertyName("declaredName"u8);
            writer.WriteStringValue(iExhibitStateUsage.DeclaredName);

            writer.WritePropertyName("declaredShortName"u8);
            writer.WriteStringValue(iExhibitStateUsage.DeclaredShortName);

            writer.WritePropertyName("direction"u8);

            if (iExhibitStateUsage.Direction.HasValue)
            {
                writer.WriteStringValue(iExhibitStateUsage.Direction.Value.ToString().ToLower());
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("elementId"u8);
            writer.WriteStringValue(iExhibitStateUsage.ElementId);

            writer.WritePropertyName("isAbstract"u8);
            writer.WriteBooleanValue(iExhibitStateUsage.IsAbstract);

            writer.WritePropertyName("isComposite"u8);
            writer.WriteBooleanValue(iExhibitStateUsage.IsComposite);

            writer.WritePropertyName("isConstant"u8);
            writer.WriteBooleanValue(iExhibitStateUsage.IsConstant);

            writer.WritePropertyName("isDerived"u8);
            writer.WriteBooleanValue(iExhibitStateUsage.IsDerived);

            writer.WritePropertyName("isEnd"u8);
            writer.WriteBooleanValue(iExhibitStateUsage.IsEnd);

            writer.WritePropertyName("isImpliedIncluded"u8);
            writer.WriteBooleanValue(iExhibitStateUsage.IsImpliedIncluded);

            writer.WritePropertyName("isIndividual"u8);
            writer.WriteBooleanValue(iExhibitStateUsage.IsIndividual);

            writer.WritePropertyName("isOrdered"u8);
            writer.WriteBooleanValue(iExhibitStateUsage.IsOrdered);

            writer.WritePropertyName("isParallel"u8);
            writer.WriteBooleanValue(iExhibitStateUsage.IsParallel);

            writer.WritePropertyName("isPortion"u8);
            writer.WriteBooleanValue(iExhibitStateUsage.IsPortion);

            writer.WritePropertyName("isSufficient"u8);
            writer.WriteBooleanValue(iExhibitStateUsage.IsSufficient);

            writer.WritePropertyName("isUnique"u8);
            writer.WriteBooleanValue(iExhibitStateUsage.IsUnique);

            writer.WritePropertyName("isVariable"u8);
            writer.WriteBooleanValue(iExhibitStateUsage.IsVariable);

            writer.WritePropertyName("isVariation"u8);
            writer.WriteBooleanValue(iExhibitStateUsage.IsVariation);

            writer.WriteStartArray("ownedRelationship"u8);

            foreach (var item in iExhibitStateUsage.OwnedRelationship)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WritePropertyName("owningRelationship"u8);

            if (iExhibitStateUsage.OwningRelationship.HasValue)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(iExhibitStateUsage.OwningRelationship.Value);
                writer.WriteEndObject();
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("portionKind"u8);

            if (iExhibitStateUsage.PortionKind.HasValue)
            {
                writer.WriteStringValue(iExhibitStateUsage.PortionKind.Value.ToString().ToLower());
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
