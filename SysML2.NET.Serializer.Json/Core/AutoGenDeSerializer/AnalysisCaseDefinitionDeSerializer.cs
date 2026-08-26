// -------------------------------------------------------------------------------------------------
// <copyright file="AnalysisCaseDefinitionDeSerializer.cs" company="Starion Group S.A.">
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

    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Logging.Abstractions;

    using SysML2.NET.Common;
    using SysML2.NET.Core.DTO.Systems.AnalysisCases;
    using SysML2.NET.Serializer.Json;
    using SysML2.NET.Serializer.Json.Utility;

    /// <summary>
    /// The purpose of the <see cref="AnalysisCaseDefinitionDeSerializer"/> is to provide deserialization capabilities
    /// for the <see cref="IAnalysisCaseDefinition"/> interface
    /// </summary>
    internal static class AnalysisCaseDefinitionDeSerializer
    {
        /// <summary>
        /// Deserializes an instance of <see cref="IAnalysisCaseDefinition"/> from the provided <see cref="Utf8JsonReader"/>
        /// </summary>
        /// <param name="reader">
        /// The <see cref="Utf8JsonReader"/> positioned on the <see cref="JsonTokenType.StartObject"/> of the
        /// <see cref="IAnalysisCaseDefinition"/> json object. On return the reader is positioned on the matching
        /// <see cref="JsonTokenType.EndObject"/>
        /// </param>
        /// <param name="serializationModeKind">
        /// enumeration specifying what kind of serialization shall be used
        /// </param>
        /// <param name="deserializeDerivedProperties">
        /// Asserts that the deserializer should deserialize derived properties if present or if they are ignored
        /// </param>
        /// <param name="loggerFactory">
        /// The <see cref="ILoggerFactory"/> used to setup logging
        /// </param>
        /// <returns>
        /// an instance of <see cref="IAnalysisCaseDefinition"/>
        /// </returns>
        /// <remarks>
        /// The <c>@type</c> property is the discriminator that the caller dispatched on, so it is skipped rather
        /// than re-validated here
        /// </remarks>
        internal static IAnalysisCaseDefinition DeSerialize(ref Utf8JsonReader reader, SerializationModeKind serializationModeKind, bool deserializeDerivedProperties, ILoggerFactory loggerFactory = null)
        {
            var logger = loggerFactory == null ? NullLogger.Instance : loggerFactory.CreateLogger("AnalysisCaseDefinitionDeSerializer");

            Utf8JsonReaderHelper.Expect(ref reader, JsonTokenType.StartObject);

            var dtoInstance = new SysML2.NET.Core.DTO.Systems.AnalysisCases.AnalysisCaseDefinition();

            if (deserializeDerivedProperties)
            {
                DeserializeDtoIncludingDerivedProperties(dtoInstance, ref reader, logger);
            }
            else
            {
                DeserializeDtoExcludingDerivedProperties(dtoInstance, ref reader, logger);
            }

            return dtoInstance;
        }

        /// <summary>
        /// Deserializes properties of a <see cref="AnalysisCaseDefinition" />
        /// from a <see cref="Utf8JsonReader" />, including derived properties
        /// </summary>
        /// <param name="dtoInstance">
        /// The <see cref="AnalysisCaseDefinition"/> instance holding deserialized values
        /// </param>
        /// <param name="reader">
        /// The <see cref="Utf8JsonReader"/> positioned on the <see cref="JsonTokenType.StartObject"/> of the
        /// <see cref="IAnalysisCaseDefinition"/> json object
        /// </param>
        /// <param name="logger">
        /// The <see cref="ILogger"/> to produce logging statement
        /// </param>
        private static void DeserializeDtoIncludingDerivedProperties(SysML2.NET.Core.DTO.Systems.AnalysisCases.AnalysisCaseDefinition dtoInstance, ref Utf8JsonReader reader, ILogger logger)
        {
            var actionSeen = false;
            var actorParameterSeen = false;
            var aliasIdsSeen = false;
            var calculationSeen = false;
            var declaredNameSeen = false;
            var declaredShortNameSeen = false;
            var differencingTypeSeen = false;
            var directedUsageSeen = false;
            var documentationSeen = false;
            var elementIdSeen = false;
            var endFeatureSeen = false;
            var expressionSeen = false;
            var featureSeen = false;
            var featureMembershipSeen = false;
            var importedMembershipSeen = false;
            var inheritedFeatureSeen = false;
            var inheritedMembershipSeen = false;
            var inputSeen = false;
            var intersectingTypeSeen = false;
            var isAbstractSeen = false;
            var isConjugatedSeen = false;
            var isImpliedIncludedSeen = false;
            var isIndividualSeen = false;
            var isLibraryElementSeen = false;
            var isModelLevelEvaluableSeen = false;
            var isSufficientSeen = false;
            var isVariationSeen = false;
            var memberSeen = false;
            var membershipSeen = false;
            var multiplicitySeen = false;
            var nameSeen = false;
            var objectiveRequirementSeen = false;
            var outputSeen = false;
            var ownedActionSeen = false;
            var ownedAllocationSeen = false;
            var ownedAnalysisCaseSeen = false;
            var ownedAnnotationSeen = false;
            var ownedAttributeSeen = false;
            var ownedCalculationSeen = false;
            var ownedCaseSeen = false;
            var ownedConcernSeen = false;
            var ownedConjugatorSeen = false;
            var ownedConnectionSeen = false;
            var ownedConstraintSeen = false;
            var ownedDifferencingSeen = false;
            var ownedDisjoiningSeen = false;
            var ownedElementSeen = false;
            var ownedEndFeatureSeen = false;
            var ownedEnumerationSeen = false;
            var ownedFeatureSeen = false;
            var ownedFeatureMembershipSeen = false;
            var ownedFlowSeen = false;
            var ownedImportSeen = false;
            var ownedInterfaceSeen = false;
            var ownedIntersectingSeen = false;
            var ownedItemSeen = false;
            var ownedMemberSeen = false;
            var ownedMembershipSeen = false;
            var ownedMetadataSeen = false;
            var ownedOccurrenceSeen = false;
            var ownedPartSeen = false;
            var ownedPortSeen = false;
            var ownedReferenceSeen = false;
            var ownedRelationshipSeen = false;
            var ownedRenderingSeen = false;
            var ownedRequirementSeen = false;
            var ownedSpecializationSeen = false;
            var ownedStateSeen = false;
            var ownedSubclassificationSeen = false;
            var ownedTransitionSeen = false;
            var ownedUnioningSeen = false;
            var ownedUsageSeen = false;
            var ownedUseCaseSeen = false;
            var ownedVerificationCaseSeen = false;
            var ownedViewSeen = false;
            var ownedViewpointSeen = false;
            var ownerSeen = false;
            var owningMembershipSeen = false;
            var owningNamespaceSeen = false;
            var owningRelationshipSeen = false;
            var parameterSeen = false;
            var qualifiedNameSeen = false;
            var resultSeen = false;
            var resultExpressionSeen = false;
            var shortNameSeen = false;
            var stepSeen = false;
            var subjectParameterSeen = false;
            var textualRepresentationSeen = false;
            var unioningTypeSeen = false;
            var usageSeen = false;
            var variantSeen = false;
            var variantMembershipSeen = false;

            while (reader.Read() && reader.TokenType != JsonTokenType.EndObject)
            {
                if (reader.TokenType != JsonTokenType.PropertyName)
                {
                    throw new JsonException("Expected a property name in the AnalysisCaseDefinition json object.");
                }

                if (reader.ValueTextEquals("@id"u8))
                {
                    reader.Read();

                    if (reader.TokenType == JsonTokenType.Null)
                    {
                        throw new JsonException("The @id property is not present, the AnalysisCaseDefinition cannot be deserialized");
                    }

                    dtoInstance.Id = Utf8JsonReaderHelper.ReadGuid(ref reader);
                    continue;
                }

                if (reader.ValueTextEquals("action"u8))
                {
                    actionSeen = true;
                    reader.Read();

                    Utf8JsonReaderHelper.ExpectArrayStart(ref reader);

                    while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
                    {
                        if (Utf8JsonReaderHelper.TryReadReferenceIdentifier(ref reader, out var actionValue))
                        {
                            dtoInstance.action.Add(actionValue);
                        }
                    }

                    continue;
                }

                if (reader.ValueTextEquals("actorParameter"u8))
                {
                    actorParameterSeen = true;
                    reader.Read();

                    Utf8JsonReaderHelper.ExpectArrayStart(ref reader);

                    while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
                    {
                        if (Utf8JsonReaderHelper.TryReadReferenceIdentifier(ref reader, out var actorParameterValue))
                        {
                            dtoInstance.actorParameter.Add(actorParameterValue);
                        }
                    }

                    continue;
                }

                if (reader.ValueTextEquals("aliasIds"u8))
                {
                    aliasIdsSeen = true;
                    reader.Read();

                    Utf8JsonReaderHelper.ExpectArrayStart(ref reader);

                    while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
                    {
                        var aliasIdsValue = reader.GetString();

                        if (aliasIdsValue != null)
                        {
                            dtoInstance.AliasIds.Add(aliasIdsValue);
                        }
                    }

                    continue;
                }

                if (reader.ValueTextEquals("calculation"u8))
                {
                    calculationSeen = true;
                    reader.Read();

                    Utf8JsonReaderHelper.ExpectArrayStart(ref reader);

                    while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
                    {
                        if (Utf8JsonReaderHelper.TryReadReferenceIdentifier(ref reader, out var calculationValue))
                        {
                            dtoInstance.calculation.Add(calculationValue);
                        }
                    }

                    continue;
                }

                if (reader.ValueTextEquals("declaredName"u8))
                {
                    declaredNameSeen = true;
                    reader.Read();

                    dtoInstance.DeclaredName = reader.GetString();

                    continue;
                }

                if (reader.ValueTextEquals("declaredShortName"u8))
                {
                    declaredShortNameSeen = true;
                    reader.Read();

                    dtoInstance.DeclaredShortName = reader.GetString();

                    continue;
                }

                if (reader.ValueTextEquals("differencingType"u8))
                {
                    differencingTypeSeen = true;
                    reader.Read();

                    Utf8JsonReaderHelper.ExpectArrayStart(ref reader);

                    while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
                    {
                        if (Utf8JsonReaderHelper.TryReadReferenceIdentifier(ref reader, out var differencingTypeValue))
                        {
                            dtoInstance.differencingType.Add(differencingTypeValue);
                        }
                    }

                    continue;
                }

                if (reader.ValueTextEquals("directedUsage"u8))
                {
                    directedUsageSeen = true;
                    reader.Read();

                    Utf8JsonReaderHelper.ExpectArrayStart(ref reader);

                    while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
                    {
                        if (Utf8JsonReaderHelper.TryReadReferenceIdentifier(ref reader, out var directedUsageValue))
                        {
                            dtoInstance.directedUsage.Add(directedUsageValue);
                        }
                    }

                    continue;
                }

                if (reader.ValueTextEquals("documentation"u8))
                {
                    documentationSeen = true;
                    reader.Read();

                    Utf8JsonReaderHelper.ExpectArrayStart(ref reader);

                    while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
                    {
                        if (Utf8JsonReaderHelper.TryReadReferenceIdentifier(ref reader, out var documentationValue))
                        {
                            dtoInstance.documentation.Add(documentationValue);
                        }
                    }

                    continue;
                }

                if (reader.ValueTextEquals("elementId"u8))
                {
                    elementIdSeen = true;
                    reader.Read();

                    var elementIdValue = reader.GetString();

                    if (elementIdValue != null)
                    {
                        dtoInstance.ElementId = elementIdValue;
                    }

                    continue;
                }

                if (reader.ValueTextEquals("endFeature"u8))
                {
                    endFeatureSeen = true;
                    reader.Read();

                    Utf8JsonReaderHelper.ExpectArrayStart(ref reader);

                    while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
                    {
                        if (Utf8JsonReaderHelper.TryReadReferenceIdentifier(ref reader, out var endFeatureValue))
                        {
                            dtoInstance.endFeature.Add(endFeatureValue);
                        }
                    }

                    continue;
                }

                if (reader.ValueTextEquals("expression"u8))
                {
                    expressionSeen = true;
                    reader.Read();

                    Utf8JsonReaderHelper.ExpectArrayStart(ref reader);

                    while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
                    {
                        if (Utf8JsonReaderHelper.TryReadReferenceIdentifier(ref reader, out var expressionValue))
                        {
                            dtoInstance.expression.Add(expressionValue);
                        }
                    }

                    continue;
                }

                if (reader.ValueTextEquals("feature"u8))
                {
                    featureSeen = true;
                    reader.Read();

                    Utf8JsonReaderHelper.ExpectArrayStart(ref reader);

                    while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
                    {
                        if (Utf8JsonReaderHelper.TryReadReferenceIdentifier(ref reader, out var featureValue))
                        {
                            dtoInstance.feature.Add(featureValue);
                        }
                    }

                    continue;
                }

                if (reader.ValueTextEquals("featureMembership"u8))
                {
                    featureMembershipSeen = true;
                    reader.Read();

                    Utf8JsonReaderHelper.ExpectArrayStart(ref reader);

                    while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
                    {
                        if (Utf8JsonReaderHelper.TryReadReferenceIdentifier(ref reader, out var featureMembershipValue))
                        {
                            dtoInstance.featureMembership.Add(featureMembershipValue);
                        }
                    }

                    continue;
                }

                if (reader.ValueTextEquals("importedMembership"u8))
                {
                    importedMembershipSeen = true;
                    reader.Read();

                    Utf8JsonReaderHelper.ExpectArrayStart(ref reader);

                    while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
                    {
                        if (Utf8JsonReaderHelper.TryReadReferenceIdentifier(ref reader, out var importedMembershipValue))
                        {
                            dtoInstance.importedMembership.Add(importedMembershipValue);
                        }
                    }

                    continue;
                }

                if (reader.ValueTextEquals("inheritedFeature"u8))
                {
                    inheritedFeatureSeen = true;
                    reader.Read();

                    Utf8JsonReaderHelper.ExpectArrayStart(ref reader);

                    while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
                    {
                        if (Utf8JsonReaderHelper.TryReadReferenceIdentifier(ref reader, out var inheritedFeatureValue))
                        {
                            dtoInstance.inheritedFeature.Add(inheritedFeatureValue);
                        }
                    }

                    continue;
                }

                if (reader.ValueTextEquals("inheritedMembership"u8))
                {
                    inheritedMembershipSeen = true;
                    reader.Read();

                    Utf8JsonReaderHelper.ExpectArrayStart(ref reader);

                    while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
                    {
                        if (Utf8JsonReaderHelper.TryReadReferenceIdentifier(ref reader, out var inheritedMembershipValue))
                        {
                            dtoInstance.inheritedMembership.Add(inheritedMembershipValue);
                        }
                    }

                    continue;
                }

                if (reader.ValueTextEquals("input"u8))
                {
                    inputSeen = true;
                    reader.Read();

                    Utf8JsonReaderHelper.ExpectArrayStart(ref reader);

                    while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
                    {
                        if (Utf8JsonReaderHelper.TryReadReferenceIdentifier(ref reader, out var inputValue))
                        {
                            dtoInstance.input.Add(inputValue);
                        }
                    }

                    continue;
                }

                if (reader.ValueTextEquals("intersectingType"u8))
                {
                    intersectingTypeSeen = true;
                    reader.Read();

                    Utf8JsonReaderHelper.ExpectArrayStart(ref reader);

                    while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
                    {
                        if (Utf8JsonReaderHelper.TryReadReferenceIdentifier(ref reader, out var intersectingTypeValue))
                        {
                            dtoInstance.intersectingType.Add(intersectingTypeValue);
                        }
                    }

                    continue;
                }

                if (reader.ValueTextEquals("isAbstract"u8))
                {
                    isAbstractSeen = true;
                    reader.Read();

                    if (reader.TokenType != JsonTokenType.Null)
                    {
                        dtoInstance.IsAbstract = reader.GetBoolean();
                    }

                    continue;
                }

                if (reader.ValueTextEquals("isConjugated"u8))
                {
                    isConjugatedSeen = true;
                    reader.Read();

                    if (reader.TokenType != JsonTokenType.Null)
                    {
                        dtoInstance.isConjugated = reader.GetBoolean();
                    }

                    continue;
                }

                if (reader.ValueTextEquals("isImpliedIncluded"u8))
                {
                    isImpliedIncludedSeen = true;
                    reader.Read();

                    if (reader.TokenType != JsonTokenType.Null)
                    {
                        dtoInstance.IsImpliedIncluded = reader.GetBoolean();
                    }

                    continue;
                }

                if (reader.ValueTextEquals("isIndividual"u8))
                {
                    isIndividualSeen = true;
                    reader.Read();

                    if (reader.TokenType != JsonTokenType.Null)
                    {
                        dtoInstance.IsIndividual = reader.GetBoolean();
                    }

                    continue;
                }

                if (reader.ValueTextEquals("isLibraryElement"u8))
                {
                    isLibraryElementSeen = true;
                    reader.Read();

                    if (reader.TokenType != JsonTokenType.Null)
                    {
                        dtoInstance.isLibraryElement = reader.GetBoolean();
                    }

                    continue;
                }

                if (reader.ValueTextEquals("isModelLevelEvaluable"u8))
                {
                    isModelLevelEvaluableSeen = true;
                    reader.Read();

                    if (reader.TokenType != JsonTokenType.Null)
                    {
                        dtoInstance.isModelLevelEvaluable = reader.GetBoolean();
                    }

                    continue;
                }

                if (reader.ValueTextEquals("isSufficient"u8))
                {
                    isSufficientSeen = true;
                    reader.Read();

                    if (reader.TokenType != JsonTokenType.Null)
                    {
                        dtoInstance.IsSufficient = reader.GetBoolean();
                    }

                    continue;
                }

                if (reader.ValueTextEquals("isVariation"u8))
                {
                    isVariationSeen = true;
                    reader.Read();

                    if (reader.TokenType != JsonTokenType.Null)
                    {
                        dtoInstance.IsVariation = reader.GetBoolean();
                    }

                    continue;
                }

                if (reader.ValueTextEquals("member"u8))
                {
                    memberSeen = true;
                    reader.Read();

                    Utf8JsonReaderHelper.ExpectArrayStart(ref reader);

                    while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
                    {
                        if (Utf8JsonReaderHelper.TryReadReferenceIdentifier(ref reader, out var memberValue))
                        {
                            dtoInstance.member.Add(memberValue);
                        }
                    }

                    continue;
                }

                if (reader.ValueTextEquals("membership"u8))
                {
                    membershipSeen = true;
                    reader.Read();

                    Utf8JsonReaderHelper.ExpectArrayStart(ref reader);

                    while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
                    {
                        if (Utf8JsonReaderHelper.TryReadReferenceIdentifier(ref reader, out var membershipValue))
                        {
                            dtoInstance.membership.Add(membershipValue);
                        }
                    }

                    continue;
                }

                if (reader.ValueTextEquals("multiplicity"u8))
                {
                    multiplicitySeen = true;
                    reader.Read();

                    if (reader.TokenType == JsonTokenType.Null)
                    {
                        dtoInstance.multiplicity = null;
                    }
                    else if (Utf8JsonReaderHelper.TryReadReferenceIdentifier(ref reader, out var multiplicityValue))
                    {
                        dtoInstance.multiplicity = multiplicityValue;
                    }

                    continue;
                }

                if (reader.ValueTextEquals("name"u8))
                {
                    nameSeen = true;
                    reader.Read();

                    dtoInstance.name = reader.GetString();

                    continue;
                }

                if (reader.ValueTextEquals("objectiveRequirement"u8))
                {
                    objectiveRequirementSeen = true;
                    reader.Read();

                    if (reader.TokenType == JsonTokenType.Null)
                    {
                        dtoInstance.objectiveRequirement = null;
                    }
                    else if (Utf8JsonReaderHelper.TryReadReferenceIdentifier(ref reader, out var objectiveRequirementValue))
                    {
                        dtoInstance.objectiveRequirement = objectiveRequirementValue;
                    }

                    continue;
                }

                if (reader.ValueTextEquals("output"u8))
                {
                    outputSeen = true;
                    reader.Read();

                    Utf8JsonReaderHelper.ExpectArrayStart(ref reader);

                    while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
                    {
                        if (Utf8JsonReaderHelper.TryReadReferenceIdentifier(ref reader, out var outputValue))
                        {
                            dtoInstance.output.Add(outputValue);
                        }
                    }

                    continue;
                }

                if (reader.ValueTextEquals("ownedAction"u8))
                {
                    ownedActionSeen = true;
                    reader.Read();

                    Utf8JsonReaderHelper.ExpectArrayStart(ref reader);

                    while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
                    {
                        if (Utf8JsonReaderHelper.TryReadReferenceIdentifier(ref reader, out var ownedActionValue))
                        {
                            dtoInstance.ownedAction.Add(ownedActionValue);
                        }
                    }

                    continue;
                }

                if (reader.ValueTextEquals("ownedAllocation"u8))
                {
                    ownedAllocationSeen = true;
                    reader.Read();

                    Utf8JsonReaderHelper.ExpectArrayStart(ref reader);

                    while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
                    {
                        if (Utf8JsonReaderHelper.TryReadReferenceIdentifier(ref reader, out var ownedAllocationValue))
                        {
                            dtoInstance.ownedAllocation.Add(ownedAllocationValue);
                        }
                    }

                    continue;
                }

                if (reader.ValueTextEquals("ownedAnalysisCase"u8))
                {
                    ownedAnalysisCaseSeen = true;
                    reader.Read();

                    Utf8JsonReaderHelper.ExpectArrayStart(ref reader);

                    while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
                    {
                        if (Utf8JsonReaderHelper.TryReadReferenceIdentifier(ref reader, out var ownedAnalysisCaseValue))
                        {
                            dtoInstance.ownedAnalysisCase.Add(ownedAnalysisCaseValue);
                        }
                    }

                    continue;
                }

                if (reader.ValueTextEquals("ownedAnnotation"u8))
                {
                    ownedAnnotationSeen = true;
                    reader.Read();

                    Utf8JsonReaderHelper.ExpectArrayStart(ref reader);

                    while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
                    {
                        if (Utf8JsonReaderHelper.TryReadReferenceIdentifier(ref reader, out var ownedAnnotationValue))
                        {
                            dtoInstance.ownedAnnotation.Add(ownedAnnotationValue);
                        }
                    }

                    continue;
                }

                if (reader.ValueTextEquals("ownedAttribute"u8))
                {
                    ownedAttributeSeen = true;
                    reader.Read();

                    Utf8JsonReaderHelper.ExpectArrayStart(ref reader);

                    while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
                    {
                        if (Utf8JsonReaderHelper.TryReadReferenceIdentifier(ref reader, out var ownedAttributeValue))
                        {
                            dtoInstance.ownedAttribute.Add(ownedAttributeValue);
                        }
                    }

                    continue;
                }

                if (reader.ValueTextEquals("ownedCalculation"u8))
                {
                    ownedCalculationSeen = true;
                    reader.Read();

                    Utf8JsonReaderHelper.ExpectArrayStart(ref reader);

                    while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
                    {
                        if (Utf8JsonReaderHelper.TryReadReferenceIdentifier(ref reader, out var ownedCalculationValue))
                        {
                            dtoInstance.ownedCalculation.Add(ownedCalculationValue);
                        }
                    }

                    continue;
                }

                if (reader.ValueTextEquals("ownedCase"u8))
                {
                    ownedCaseSeen = true;
                    reader.Read();

                    Utf8JsonReaderHelper.ExpectArrayStart(ref reader);

                    while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
                    {
                        if (Utf8JsonReaderHelper.TryReadReferenceIdentifier(ref reader, out var ownedCaseValue))
                        {
                            dtoInstance.ownedCase.Add(ownedCaseValue);
                        }
                    }

                    continue;
                }

                if (reader.ValueTextEquals("ownedConcern"u8))
                {
                    ownedConcernSeen = true;
                    reader.Read();

                    Utf8JsonReaderHelper.ExpectArrayStart(ref reader);

                    while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
                    {
                        if (Utf8JsonReaderHelper.TryReadReferenceIdentifier(ref reader, out var ownedConcernValue))
                        {
                            dtoInstance.ownedConcern.Add(ownedConcernValue);
                        }
                    }

                    continue;
                }

                if (reader.ValueTextEquals("ownedConjugator"u8))
                {
                    ownedConjugatorSeen = true;
                    reader.Read();

                    if (reader.TokenType == JsonTokenType.Null)
                    {
                        dtoInstance.ownedConjugator = null;
                    }
                    else if (Utf8JsonReaderHelper.TryReadReferenceIdentifier(ref reader, out var ownedConjugatorValue))
                    {
                        dtoInstance.ownedConjugator = ownedConjugatorValue;
                    }

                    continue;
                }

                if (reader.ValueTextEquals("ownedConnection"u8))
                {
                    ownedConnectionSeen = true;
                    reader.Read();

                    Utf8JsonReaderHelper.ExpectArrayStart(ref reader);

                    while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
                    {
                        if (Utf8JsonReaderHelper.TryReadReferenceIdentifier(ref reader, out var ownedConnectionValue))
                        {
                            dtoInstance.ownedConnection.Add(ownedConnectionValue);
                        }
                    }

                    continue;
                }

                if (reader.ValueTextEquals("ownedConstraint"u8))
                {
                    ownedConstraintSeen = true;
                    reader.Read();

                    Utf8JsonReaderHelper.ExpectArrayStart(ref reader);

                    while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
                    {
                        if (Utf8JsonReaderHelper.TryReadReferenceIdentifier(ref reader, out var ownedConstraintValue))
                        {
                            dtoInstance.ownedConstraint.Add(ownedConstraintValue);
                        }
                    }

                    continue;
                }

                if (reader.ValueTextEquals("ownedDifferencing"u8))
                {
                    ownedDifferencingSeen = true;
                    reader.Read();

                    Utf8JsonReaderHelper.ExpectArrayStart(ref reader);

                    while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
                    {
                        if (Utf8JsonReaderHelper.TryReadReferenceIdentifier(ref reader, out var ownedDifferencingValue))
                        {
                            dtoInstance.ownedDifferencing.Add(ownedDifferencingValue);
                        }
                    }

                    continue;
                }

                if (reader.ValueTextEquals("ownedDisjoining"u8))
                {
                    ownedDisjoiningSeen = true;
                    reader.Read();

                    Utf8JsonReaderHelper.ExpectArrayStart(ref reader);

                    while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
                    {
                        if (Utf8JsonReaderHelper.TryReadReferenceIdentifier(ref reader, out var ownedDisjoiningValue))
                        {
                            dtoInstance.ownedDisjoining.Add(ownedDisjoiningValue);
                        }
                    }

                    continue;
                }

                if (reader.ValueTextEquals("ownedElement"u8))
                {
                    ownedElementSeen = true;
                    reader.Read();

                    Utf8JsonReaderHelper.ExpectArrayStart(ref reader);

                    while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
                    {
                        if (Utf8JsonReaderHelper.TryReadReferenceIdentifier(ref reader, out var ownedElementValue))
                        {
                            dtoInstance.ownedElement.Add(ownedElementValue);
                        }
                    }

                    continue;
                }

                if (reader.ValueTextEquals("ownedEndFeature"u8))
                {
                    ownedEndFeatureSeen = true;
                    reader.Read();

                    Utf8JsonReaderHelper.ExpectArrayStart(ref reader);

                    while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
                    {
                        if (Utf8JsonReaderHelper.TryReadReferenceIdentifier(ref reader, out var ownedEndFeatureValue))
                        {
                            dtoInstance.ownedEndFeature.Add(ownedEndFeatureValue);
                        }
                    }

                    continue;
                }

                if (reader.ValueTextEquals("ownedEnumeration"u8))
                {
                    ownedEnumerationSeen = true;
                    reader.Read();

                    Utf8JsonReaderHelper.ExpectArrayStart(ref reader);

                    while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
                    {
                        if (Utf8JsonReaderHelper.TryReadReferenceIdentifier(ref reader, out var ownedEnumerationValue))
                        {
                            dtoInstance.ownedEnumeration.Add(ownedEnumerationValue);
                        }
                    }

                    continue;
                }

                if (reader.ValueTextEquals("ownedFeature"u8))
                {
                    ownedFeatureSeen = true;
                    reader.Read();

                    Utf8JsonReaderHelper.ExpectArrayStart(ref reader);

                    while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
                    {
                        if (Utf8JsonReaderHelper.TryReadReferenceIdentifier(ref reader, out var ownedFeatureValue))
                        {
                            dtoInstance.ownedFeature.Add(ownedFeatureValue);
                        }
                    }

                    continue;
                }

                if (reader.ValueTextEquals("ownedFeatureMembership"u8))
                {
                    ownedFeatureMembershipSeen = true;
                    reader.Read();

                    Utf8JsonReaderHelper.ExpectArrayStart(ref reader);

                    while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
                    {
                        if (Utf8JsonReaderHelper.TryReadReferenceIdentifier(ref reader, out var ownedFeatureMembershipValue))
                        {
                            dtoInstance.ownedFeatureMembership.Add(ownedFeatureMembershipValue);
                        }
                    }

                    continue;
                }

                if (reader.ValueTextEquals("ownedFlow"u8))
                {
                    ownedFlowSeen = true;
                    reader.Read();

                    Utf8JsonReaderHelper.ExpectArrayStart(ref reader);

                    while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
                    {
                        if (Utf8JsonReaderHelper.TryReadReferenceIdentifier(ref reader, out var ownedFlowValue))
                        {
                            dtoInstance.ownedFlow.Add(ownedFlowValue);
                        }
                    }

                    continue;
                }

                if (reader.ValueTextEquals("ownedImport"u8))
                {
                    ownedImportSeen = true;
                    reader.Read();

                    Utf8JsonReaderHelper.ExpectArrayStart(ref reader);

                    while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
                    {
                        if (Utf8JsonReaderHelper.TryReadReferenceIdentifier(ref reader, out var ownedImportValue))
                        {
                            dtoInstance.ownedImport.Add(ownedImportValue);
                        }
                    }

                    continue;
                }

                if (reader.ValueTextEquals("ownedInterface"u8))
                {
                    ownedInterfaceSeen = true;
                    reader.Read();

                    Utf8JsonReaderHelper.ExpectArrayStart(ref reader);

                    while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
                    {
                        if (Utf8JsonReaderHelper.TryReadReferenceIdentifier(ref reader, out var ownedInterfaceValue))
                        {
                            dtoInstance.ownedInterface.Add(ownedInterfaceValue);
                        }
                    }

                    continue;
                }

                if (reader.ValueTextEquals("ownedIntersecting"u8))
                {
                    ownedIntersectingSeen = true;
                    reader.Read();

                    Utf8JsonReaderHelper.ExpectArrayStart(ref reader);

                    while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
                    {
                        if (Utf8JsonReaderHelper.TryReadReferenceIdentifier(ref reader, out var ownedIntersectingValue))
                        {
                            dtoInstance.ownedIntersecting.Add(ownedIntersectingValue);
                        }
                    }

                    continue;
                }

                if (reader.ValueTextEquals("ownedItem"u8))
                {
                    ownedItemSeen = true;
                    reader.Read();

                    Utf8JsonReaderHelper.ExpectArrayStart(ref reader);

                    while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
                    {
                        if (Utf8JsonReaderHelper.TryReadReferenceIdentifier(ref reader, out var ownedItemValue))
                        {
                            dtoInstance.ownedItem.Add(ownedItemValue);
                        }
                    }

                    continue;
                }

                if (reader.ValueTextEquals("ownedMember"u8))
                {
                    ownedMemberSeen = true;
                    reader.Read();

                    Utf8JsonReaderHelper.ExpectArrayStart(ref reader);

                    while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
                    {
                        if (Utf8JsonReaderHelper.TryReadReferenceIdentifier(ref reader, out var ownedMemberValue))
                        {
                            dtoInstance.ownedMember.Add(ownedMemberValue);
                        }
                    }

                    continue;
                }

                if (reader.ValueTextEquals("ownedMembership"u8))
                {
                    ownedMembershipSeen = true;
                    reader.Read();

                    Utf8JsonReaderHelper.ExpectArrayStart(ref reader);

                    while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
                    {
                        if (Utf8JsonReaderHelper.TryReadReferenceIdentifier(ref reader, out var ownedMembershipValue))
                        {
                            dtoInstance.ownedMembership.Add(ownedMembershipValue);
                        }
                    }

                    continue;
                }

                if (reader.ValueTextEquals("ownedMetadata"u8))
                {
                    ownedMetadataSeen = true;
                    reader.Read();

                    Utf8JsonReaderHelper.ExpectArrayStart(ref reader);

                    while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
                    {
                        if (Utf8JsonReaderHelper.TryReadReferenceIdentifier(ref reader, out var ownedMetadataValue))
                        {
                            dtoInstance.ownedMetadata.Add(ownedMetadataValue);
                        }
                    }

                    continue;
                }

                if (reader.ValueTextEquals("ownedOccurrence"u8))
                {
                    ownedOccurrenceSeen = true;
                    reader.Read();

                    Utf8JsonReaderHelper.ExpectArrayStart(ref reader);

                    while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
                    {
                        if (Utf8JsonReaderHelper.TryReadReferenceIdentifier(ref reader, out var ownedOccurrenceValue))
                        {
                            dtoInstance.ownedOccurrence.Add(ownedOccurrenceValue);
                        }
                    }

                    continue;
                }

                if (reader.ValueTextEquals("ownedPart"u8))
                {
                    ownedPartSeen = true;
                    reader.Read();

                    Utf8JsonReaderHelper.ExpectArrayStart(ref reader);

                    while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
                    {
                        if (Utf8JsonReaderHelper.TryReadReferenceIdentifier(ref reader, out var ownedPartValue))
                        {
                            dtoInstance.ownedPart.Add(ownedPartValue);
                        }
                    }

                    continue;
                }

                if (reader.ValueTextEquals("ownedPort"u8))
                {
                    ownedPortSeen = true;
                    reader.Read();

                    Utf8JsonReaderHelper.ExpectArrayStart(ref reader);

                    while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
                    {
                        if (Utf8JsonReaderHelper.TryReadReferenceIdentifier(ref reader, out var ownedPortValue))
                        {
                            dtoInstance.ownedPort.Add(ownedPortValue);
                        }
                    }

                    continue;
                }

                if (reader.ValueTextEquals("ownedReference"u8))
                {
                    ownedReferenceSeen = true;
                    reader.Read();

                    Utf8JsonReaderHelper.ExpectArrayStart(ref reader);

                    while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
                    {
                        if (Utf8JsonReaderHelper.TryReadReferenceIdentifier(ref reader, out var ownedReferenceValue))
                        {
                            dtoInstance.ownedReference.Add(ownedReferenceValue);
                        }
                    }

                    continue;
                }

                if (reader.ValueTextEquals("ownedRelationship"u8))
                {
                    ownedRelationshipSeen = true;
                    reader.Read();

                    Utf8JsonReaderHelper.ExpectArrayStart(ref reader);

                    while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
                    {
                        if (Utf8JsonReaderHelper.TryReadReferenceIdentifier(ref reader, out var ownedRelationshipValue))
                        {
                            dtoInstance.OwnedRelationship.Add(ownedRelationshipValue);
                        }
                    }

                    continue;
                }

                if (reader.ValueTextEquals("ownedRendering"u8))
                {
                    ownedRenderingSeen = true;
                    reader.Read();

                    Utf8JsonReaderHelper.ExpectArrayStart(ref reader);

                    while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
                    {
                        if (Utf8JsonReaderHelper.TryReadReferenceIdentifier(ref reader, out var ownedRenderingValue))
                        {
                            dtoInstance.ownedRendering.Add(ownedRenderingValue);
                        }
                    }

                    continue;
                }

                if (reader.ValueTextEquals("ownedRequirement"u8))
                {
                    ownedRequirementSeen = true;
                    reader.Read();

                    Utf8JsonReaderHelper.ExpectArrayStart(ref reader);

                    while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
                    {
                        if (Utf8JsonReaderHelper.TryReadReferenceIdentifier(ref reader, out var ownedRequirementValue))
                        {
                            dtoInstance.ownedRequirement.Add(ownedRequirementValue);
                        }
                    }

                    continue;
                }

                if (reader.ValueTextEquals("ownedSpecialization"u8))
                {
                    ownedSpecializationSeen = true;
                    reader.Read();

                    Utf8JsonReaderHelper.ExpectArrayStart(ref reader);

                    while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
                    {
                        if (Utf8JsonReaderHelper.TryReadReferenceIdentifier(ref reader, out var ownedSpecializationValue))
                        {
                            dtoInstance.ownedSpecialization.Add(ownedSpecializationValue);
                        }
                    }

                    continue;
                }

                if (reader.ValueTextEquals("ownedState"u8))
                {
                    ownedStateSeen = true;
                    reader.Read();

                    Utf8JsonReaderHelper.ExpectArrayStart(ref reader);

                    while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
                    {
                        if (Utf8JsonReaderHelper.TryReadReferenceIdentifier(ref reader, out var ownedStateValue))
                        {
                            dtoInstance.ownedState.Add(ownedStateValue);
                        }
                    }

                    continue;
                }

                if (reader.ValueTextEquals("ownedSubclassification"u8))
                {
                    ownedSubclassificationSeen = true;
                    reader.Read();

                    Utf8JsonReaderHelper.ExpectArrayStart(ref reader);

                    while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
                    {
                        if (Utf8JsonReaderHelper.TryReadReferenceIdentifier(ref reader, out var ownedSubclassificationValue))
                        {
                            dtoInstance.ownedSubclassification.Add(ownedSubclassificationValue);
                        }
                    }

                    continue;
                }

                if (reader.ValueTextEquals("ownedTransition"u8))
                {
                    ownedTransitionSeen = true;
                    reader.Read();

                    Utf8JsonReaderHelper.ExpectArrayStart(ref reader);

                    while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
                    {
                        if (Utf8JsonReaderHelper.TryReadReferenceIdentifier(ref reader, out var ownedTransitionValue))
                        {
                            dtoInstance.ownedTransition.Add(ownedTransitionValue);
                        }
                    }

                    continue;
                }

                if (reader.ValueTextEquals("ownedUnioning"u8))
                {
                    ownedUnioningSeen = true;
                    reader.Read();

                    Utf8JsonReaderHelper.ExpectArrayStart(ref reader);

                    while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
                    {
                        if (Utf8JsonReaderHelper.TryReadReferenceIdentifier(ref reader, out var ownedUnioningValue))
                        {
                            dtoInstance.ownedUnioning.Add(ownedUnioningValue);
                        }
                    }

                    continue;
                }

                if (reader.ValueTextEquals("ownedUsage"u8))
                {
                    ownedUsageSeen = true;
                    reader.Read();

                    Utf8JsonReaderHelper.ExpectArrayStart(ref reader);

                    while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
                    {
                        if (Utf8JsonReaderHelper.TryReadReferenceIdentifier(ref reader, out var ownedUsageValue))
                        {
                            dtoInstance.ownedUsage.Add(ownedUsageValue);
                        }
                    }

                    continue;
                }

                if (reader.ValueTextEquals("ownedUseCase"u8))
                {
                    ownedUseCaseSeen = true;
                    reader.Read();

                    Utf8JsonReaderHelper.ExpectArrayStart(ref reader);

                    while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
                    {
                        if (Utf8JsonReaderHelper.TryReadReferenceIdentifier(ref reader, out var ownedUseCaseValue))
                        {
                            dtoInstance.ownedUseCase.Add(ownedUseCaseValue);
                        }
                    }

                    continue;
                }

                if (reader.ValueTextEquals("ownedVerificationCase"u8))
                {
                    ownedVerificationCaseSeen = true;
                    reader.Read();

                    Utf8JsonReaderHelper.ExpectArrayStart(ref reader);

                    while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
                    {
                        if (Utf8JsonReaderHelper.TryReadReferenceIdentifier(ref reader, out var ownedVerificationCaseValue))
                        {
                            dtoInstance.ownedVerificationCase.Add(ownedVerificationCaseValue);
                        }
                    }

                    continue;
                }

                if (reader.ValueTextEquals("ownedView"u8))
                {
                    ownedViewSeen = true;
                    reader.Read();

                    Utf8JsonReaderHelper.ExpectArrayStart(ref reader);

                    while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
                    {
                        if (Utf8JsonReaderHelper.TryReadReferenceIdentifier(ref reader, out var ownedViewValue))
                        {
                            dtoInstance.ownedView.Add(ownedViewValue);
                        }
                    }

                    continue;
                }

                if (reader.ValueTextEquals("ownedViewpoint"u8))
                {
                    ownedViewpointSeen = true;
                    reader.Read();

                    Utf8JsonReaderHelper.ExpectArrayStart(ref reader);

                    while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
                    {
                        if (Utf8JsonReaderHelper.TryReadReferenceIdentifier(ref reader, out var ownedViewpointValue))
                        {
                            dtoInstance.ownedViewpoint.Add(ownedViewpointValue);
                        }
                    }

                    continue;
                }

                if (reader.ValueTextEquals("owner"u8))
                {
                    ownerSeen = true;
                    reader.Read();

                    if (reader.TokenType == JsonTokenType.Null)
                    {
                        dtoInstance.owner = null;
                    }
                    else if (Utf8JsonReaderHelper.TryReadReferenceIdentifier(ref reader, out var ownerValue))
                    {
                        dtoInstance.owner = ownerValue;
                    }

                    continue;
                }

                if (reader.ValueTextEquals("owningMembership"u8))
                {
                    owningMembershipSeen = true;
                    reader.Read();

                    if (reader.TokenType == JsonTokenType.Null)
                    {
                        dtoInstance.owningMembership = null;
                    }
                    else if (Utf8JsonReaderHelper.TryReadReferenceIdentifier(ref reader, out var owningMembershipValue))
                    {
                        dtoInstance.owningMembership = owningMembershipValue;
                    }

                    continue;
                }

                if (reader.ValueTextEquals("owningNamespace"u8))
                {
                    owningNamespaceSeen = true;
                    reader.Read();

                    if (reader.TokenType == JsonTokenType.Null)
                    {
                        dtoInstance.owningNamespace = null;
                    }
                    else if (Utf8JsonReaderHelper.TryReadReferenceIdentifier(ref reader, out var owningNamespaceValue))
                    {
                        dtoInstance.owningNamespace = owningNamespaceValue;
                    }

                    continue;
                }

                if (reader.ValueTextEquals("owningRelationship"u8))
                {
                    owningRelationshipSeen = true;
                    reader.Read();

                    if (reader.TokenType == JsonTokenType.Null)
                    {
                        dtoInstance.OwningRelationship = null;
                    }
                    else if (Utf8JsonReaderHelper.TryReadReferenceIdentifier(ref reader, out var owningRelationshipValue))
                    {
                        dtoInstance.OwningRelationship = owningRelationshipValue;
                    }

                    continue;
                }

                if (reader.ValueTextEquals("parameter"u8))
                {
                    parameterSeen = true;
                    reader.Read();

                    Utf8JsonReaderHelper.ExpectArrayStart(ref reader);

                    while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
                    {
                        if (Utf8JsonReaderHelper.TryReadReferenceIdentifier(ref reader, out var parameterValue))
                        {
                            dtoInstance.parameter.Add(parameterValue);
                        }
                    }

                    continue;
                }

                if (reader.ValueTextEquals("qualifiedName"u8))
                {
                    qualifiedNameSeen = true;
                    reader.Read();

                    dtoInstance.qualifiedName = reader.GetString();

                    continue;
                }

                if (reader.ValueTextEquals("result"u8))
                {
                    resultSeen = true;
                    reader.Read();

                    if (reader.TokenType == JsonTokenType.Null)
                    {
                        dtoInstance.result = Guid.Empty;

                        if (logger.IsEnabled(LogLevel.Debug))
                        {
                            logger.LogDebug("the AnalysisCaseDefinition.result property was not found in the Json. The value is set to Guid.Empty");
                        }
                    }
                    else if (Utf8JsonReaderHelper.TryReadReferenceIdentifier(ref reader, out var resultValue))
                    {
                        dtoInstance.result = resultValue;
                    }

                    continue;
                }

                if (reader.ValueTextEquals("resultExpression"u8))
                {
                    resultExpressionSeen = true;
                    reader.Read();

                    if (reader.TokenType == JsonTokenType.Null)
                    {
                        dtoInstance.resultExpression = null;
                    }
                    else if (Utf8JsonReaderHelper.TryReadReferenceIdentifier(ref reader, out var resultExpressionValue))
                    {
                        dtoInstance.resultExpression = resultExpressionValue;
                    }

                    continue;
                }

                if (reader.ValueTextEquals("shortName"u8))
                {
                    shortNameSeen = true;
                    reader.Read();

                    dtoInstance.shortName = reader.GetString();

                    continue;
                }

                if (reader.ValueTextEquals("step"u8))
                {
                    stepSeen = true;
                    reader.Read();

                    Utf8JsonReaderHelper.ExpectArrayStart(ref reader);

                    while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
                    {
                        if (Utf8JsonReaderHelper.TryReadReferenceIdentifier(ref reader, out var stepValue))
                        {
                            dtoInstance.step.Add(stepValue);
                        }
                    }

                    continue;
                }

                if (reader.ValueTextEquals("subjectParameter"u8))
                {
                    subjectParameterSeen = true;
                    reader.Read();

                    if (reader.TokenType == JsonTokenType.Null)
                    {
                        dtoInstance.subjectParameter = Guid.Empty;

                        if (logger.IsEnabled(LogLevel.Debug))
                        {
                            logger.LogDebug("the AnalysisCaseDefinition.subjectParameter property was not found in the Json. The value is set to Guid.Empty");
                        }
                    }
                    else if (Utf8JsonReaderHelper.TryReadReferenceIdentifier(ref reader, out var subjectParameterValue))
                    {
                        dtoInstance.subjectParameter = subjectParameterValue;
                    }

                    continue;
                }

                if (reader.ValueTextEquals("textualRepresentation"u8))
                {
                    textualRepresentationSeen = true;
                    reader.Read();

                    Utf8JsonReaderHelper.ExpectArrayStart(ref reader);

                    while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
                    {
                        if (Utf8JsonReaderHelper.TryReadReferenceIdentifier(ref reader, out var textualRepresentationValue))
                        {
                            dtoInstance.textualRepresentation.Add(textualRepresentationValue);
                        }
                    }

                    continue;
                }

                if (reader.ValueTextEquals("unioningType"u8))
                {
                    unioningTypeSeen = true;
                    reader.Read();

                    Utf8JsonReaderHelper.ExpectArrayStart(ref reader);

                    while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
                    {
                        if (Utf8JsonReaderHelper.TryReadReferenceIdentifier(ref reader, out var unioningTypeValue))
                        {
                            dtoInstance.unioningType.Add(unioningTypeValue);
                        }
                    }

                    continue;
                }

                if (reader.ValueTextEquals("usage"u8))
                {
                    usageSeen = true;
                    reader.Read();

                    Utf8JsonReaderHelper.ExpectArrayStart(ref reader);

                    while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
                    {
                        if (Utf8JsonReaderHelper.TryReadReferenceIdentifier(ref reader, out var usageValue))
                        {
                            dtoInstance.usage.Add(usageValue);
                        }
                    }

                    continue;
                }

                if (reader.ValueTextEquals("variant"u8))
                {
                    variantSeen = true;
                    reader.Read();

                    Utf8JsonReaderHelper.ExpectArrayStart(ref reader);

                    while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
                    {
                        if (Utf8JsonReaderHelper.TryReadReferenceIdentifier(ref reader, out var variantValue))
                        {
                            dtoInstance.variant.Add(variantValue);
                        }
                    }

                    continue;
                }

                if (reader.ValueTextEquals("variantMembership"u8))
                {
                    variantMembershipSeen = true;
                    reader.Read();

                    Utf8JsonReaderHelper.ExpectArrayStart(ref reader);

                    while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
                    {
                        if (Utf8JsonReaderHelper.TryReadReferenceIdentifier(ref reader, out var variantMembershipValue))
                        {
                            dtoInstance.variantMembership.Add(variantMembershipValue);
                        }
                    }

                    continue;
                }


                reader.Read();
                reader.Skip();
            }

            if (logger.IsEnabled(LogLevel.Debug))
            {
                if (!actionSeen)
                {
                    logger.LogDebug("the action Json property was not found in the AnalysisCaseDefinition: {Id}", dtoInstance.Id);
                }
                if (!actorParameterSeen)
                {
                    logger.LogDebug("the actorParameter Json property was not found in the AnalysisCaseDefinition: {Id}", dtoInstance.Id);
                }
                if (!aliasIdsSeen)
                {
                    logger.LogDebug("the aliasIds Json property was not found in the AnalysisCaseDefinition: {Id}", dtoInstance.Id);
                }
                if (!calculationSeen)
                {
                    logger.LogDebug("the calculation Json property was not found in the AnalysisCaseDefinition: {Id}", dtoInstance.Id);
                }
                if (!declaredNameSeen)
                {
                    logger.LogDebug("the declaredName Json property was not found in the AnalysisCaseDefinition: {Id}", dtoInstance.Id);
                }
                if (!declaredShortNameSeen)
                {
                    logger.LogDebug("the declaredShortName Json property was not found in the AnalysisCaseDefinition: {Id}", dtoInstance.Id);
                }
                if (!differencingTypeSeen)
                {
                    logger.LogDebug("the differencingType Json property was not found in the AnalysisCaseDefinition: {Id}", dtoInstance.Id);
                }
                if (!directedUsageSeen)
                {
                    logger.LogDebug("the directedUsage Json property was not found in the AnalysisCaseDefinition: {Id}", dtoInstance.Id);
                }
                if (!documentationSeen)
                {
                    logger.LogDebug("the documentation Json property was not found in the AnalysisCaseDefinition: {Id}", dtoInstance.Id);
                }
                if (!elementIdSeen)
                {
                    logger.LogDebug("the elementId Json property was not found in the AnalysisCaseDefinition: {Id}", dtoInstance.Id);
                }
                if (!endFeatureSeen)
                {
                    logger.LogDebug("the endFeature Json property was not found in the AnalysisCaseDefinition: {Id}", dtoInstance.Id);
                }
                if (!expressionSeen)
                {
                    logger.LogDebug("the expression Json property was not found in the AnalysisCaseDefinition: {Id}", dtoInstance.Id);
                }
                if (!featureSeen)
                {
                    logger.LogDebug("the feature Json property was not found in the AnalysisCaseDefinition: {Id}", dtoInstance.Id);
                }
                if (!featureMembershipSeen)
                {
                    logger.LogDebug("the featureMembership Json property was not found in the AnalysisCaseDefinition: {Id}", dtoInstance.Id);
                }
                if (!importedMembershipSeen)
                {
                    logger.LogDebug("the importedMembership Json property was not found in the AnalysisCaseDefinition: {Id}", dtoInstance.Id);
                }
                if (!inheritedFeatureSeen)
                {
                    logger.LogDebug("the inheritedFeature Json property was not found in the AnalysisCaseDefinition: {Id}", dtoInstance.Id);
                }
                if (!inheritedMembershipSeen)
                {
                    logger.LogDebug("the inheritedMembership Json property was not found in the AnalysisCaseDefinition: {Id}", dtoInstance.Id);
                }
                if (!inputSeen)
                {
                    logger.LogDebug("the input Json property was not found in the AnalysisCaseDefinition: {Id}", dtoInstance.Id);
                }
                if (!intersectingTypeSeen)
                {
                    logger.LogDebug("the intersectingType Json property was not found in the AnalysisCaseDefinition: {Id}", dtoInstance.Id);
                }
                if (!isAbstractSeen)
                {
                    logger.LogDebug("the isAbstract Json property was not found in the AnalysisCaseDefinition: {Id}", dtoInstance.Id);
                }
                if (!isConjugatedSeen)
                {
                    logger.LogDebug("the isConjugated Json property was not found in the AnalysisCaseDefinition: {Id}", dtoInstance.Id);
                }
                if (!isImpliedIncludedSeen)
                {
                    logger.LogDebug("the isImpliedIncluded Json property was not found in the AnalysisCaseDefinition: {Id}", dtoInstance.Id);
                }
                if (!isIndividualSeen)
                {
                    logger.LogDebug("the isIndividual Json property was not found in the AnalysisCaseDefinition: {Id}", dtoInstance.Id);
                }
                if (!isLibraryElementSeen)
                {
                    logger.LogDebug("the isLibraryElement Json property was not found in the AnalysisCaseDefinition: {Id}", dtoInstance.Id);
                }
                if (!isModelLevelEvaluableSeen)
                {
                    logger.LogDebug("the isModelLevelEvaluable Json property was not found in the AnalysisCaseDefinition: {Id}", dtoInstance.Id);
                }
                if (!isSufficientSeen)
                {
                    logger.LogDebug("the isSufficient Json property was not found in the AnalysisCaseDefinition: {Id}", dtoInstance.Id);
                }
                if (!isVariationSeen)
                {
                    logger.LogDebug("the isVariation Json property was not found in the AnalysisCaseDefinition: {Id}", dtoInstance.Id);
                }
                if (!memberSeen)
                {
                    logger.LogDebug("the member Json property was not found in the AnalysisCaseDefinition: {Id}", dtoInstance.Id);
                }
                if (!membershipSeen)
                {
                    logger.LogDebug("the membership Json property was not found in the AnalysisCaseDefinition: {Id}", dtoInstance.Id);
                }
                if (!multiplicitySeen)
                {
                    logger.LogDebug("the multiplicity Json property was not found in the AnalysisCaseDefinition: {Id}", dtoInstance.Id);
                }
                if (!nameSeen)
                {
                    logger.LogDebug("the name Json property was not found in the AnalysisCaseDefinition: {Id}", dtoInstance.Id);
                }
                if (!objectiveRequirementSeen)
                {
                    logger.LogDebug("the objectiveRequirement Json property was not found in the AnalysisCaseDefinition: {Id}", dtoInstance.Id);
                }
                if (!outputSeen)
                {
                    logger.LogDebug("the output Json property was not found in the AnalysisCaseDefinition: {Id}", dtoInstance.Id);
                }
                if (!ownedActionSeen)
                {
                    logger.LogDebug("the ownedAction Json property was not found in the AnalysisCaseDefinition: {Id}", dtoInstance.Id);
                }
                if (!ownedAllocationSeen)
                {
                    logger.LogDebug("the ownedAllocation Json property was not found in the AnalysisCaseDefinition: {Id}", dtoInstance.Id);
                }
                if (!ownedAnalysisCaseSeen)
                {
                    logger.LogDebug("the ownedAnalysisCase Json property was not found in the AnalysisCaseDefinition: {Id}", dtoInstance.Id);
                }
                if (!ownedAnnotationSeen)
                {
                    logger.LogDebug("the ownedAnnotation Json property was not found in the AnalysisCaseDefinition: {Id}", dtoInstance.Id);
                }
                if (!ownedAttributeSeen)
                {
                    logger.LogDebug("the ownedAttribute Json property was not found in the AnalysisCaseDefinition: {Id}", dtoInstance.Id);
                }
                if (!ownedCalculationSeen)
                {
                    logger.LogDebug("the ownedCalculation Json property was not found in the AnalysisCaseDefinition: {Id}", dtoInstance.Id);
                }
                if (!ownedCaseSeen)
                {
                    logger.LogDebug("the ownedCase Json property was not found in the AnalysisCaseDefinition: {Id}", dtoInstance.Id);
                }
                if (!ownedConcernSeen)
                {
                    logger.LogDebug("the ownedConcern Json property was not found in the AnalysisCaseDefinition: {Id}", dtoInstance.Id);
                }
                if (!ownedConjugatorSeen)
                {
                    logger.LogDebug("the ownedConjugator Json property was not found in the AnalysisCaseDefinition: {Id}", dtoInstance.Id);
                }
                if (!ownedConnectionSeen)
                {
                    logger.LogDebug("the ownedConnection Json property was not found in the AnalysisCaseDefinition: {Id}", dtoInstance.Id);
                }
                if (!ownedConstraintSeen)
                {
                    logger.LogDebug("the ownedConstraint Json property was not found in the AnalysisCaseDefinition: {Id}", dtoInstance.Id);
                }
                if (!ownedDifferencingSeen)
                {
                    logger.LogDebug("the ownedDifferencing Json property was not found in the AnalysisCaseDefinition: {Id}", dtoInstance.Id);
                }
                if (!ownedDisjoiningSeen)
                {
                    logger.LogDebug("the ownedDisjoining Json property was not found in the AnalysisCaseDefinition: {Id}", dtoInstance.Id);
                }
                if (!ownedElementSeen)
                {
                    logger.LogDebug("the ownedElement Json property was not found in the AnalysisCaseDefinition: {Id}", dtoInstance.Id);
                }
                if (!ownedEndFeatureSeen)
                {
                    logger.LogDebug("the ownedEndFeature Json property was not found in the AnalysisCaseDefinition: {Id}", dtoInstance.Id);
                }
                if (!ownedEnumerationSeen)
                {
                    logger.LogDebug("the ownedEnumeration Json property was not found in the AnalysisCaseDefinition: {Id}", dtoInstance.Id);
                }
                if (!ownedFeatureSeen)
                {
                    logger.LogDebug("the ownedFeature Json property was not found in the AnalysisCaseDefinition: {Id}", dtoInstance.Id);
                }
                if (!ownedFeatureMembershipSeen)
                {
                    logger.LogDebug("the ownedFeatureMembership Json property was not found in the AnalysisCaseDefinition: {Id}", dtoInstance.Id);
                }
                if (!ownedFlowSeen)
                {
                    logger.LogDebug("the ownedFlow Json property was not found in the AnalysisCaseDefinition: {Id}", dtoInstance.Id);
                }
                if (!ownedImportSeen)
                {
                    logger.LogDebug("the ownedImport Json property was not found in the AnalysisCaseDefinition: {Id}", dtoInstance.Id);
                }
                if (!ownedInterfaceSeen)
                {
                    logger.LogDebug("the ownedInterface Json property was not found in the AnalysisCaseDefinition: {Id}", dtoInstance.Id);
                }
                if (!ownedIntersectingSeen)
                {
                    logger.LogDebug("the ownedIntersecting Json property was not found in the AnalysisCaseDefinition: {Id}", dtoInstance.Id);
                }
                if (!ownedItemSeen)
                {
                    logger.LogDebug("the ownedItem Json property was not found in the AnalysisCaseDefinition: {Id}", dtoInstance.Id);
                }
                if (!ownedMemberSeen)
                {
                    logger.LogDebug("the ownedMember Json property was not found in the AnalysisCaseDefinition: {Id}", dtoInstance.Id);
                }
                if (!ownedMembershipSeen)
                {
                    logger.LogDebug("the ownedMembership Json property was not found in the AnalysisCaseDefinition: {Id}", dtoInstance.Id);
                }
                if (!ownedMetadataSeen)
                {
                    logger.LogDebug("the ownedMetadata Json property was not found in the AnalysisCaseDefinition: {Id}", dtoInstance.Id);
                }
                if (!ownedOccurrenceSeen)
                {
                    logger.LogDebug("the ownedOccurrence Json property was not found in the AnalysisCaseDefinition: {Id}", dtoInstance.Id);
                }
                if (!ownedPartSeen)
                {
                    logger.LogDebug("the ownedPart Json property was not found in the AnalysisCaseDefinition: {Id}", dtoInstance.Id);
                }
                if (!ownedPortSeen)
                {
                    logger.LogDebug("the ownedPort Json property was not found in the AnalysisCaseDefinition: {Id}", dtoInstance.Id);
                }
                if (!ownedReferenceSeen)
                {
                    logger.LogDebug("the ownedReference Json property was not found in the AnalysisCaseDefinition: {Id}", dtoInstance.Id);
                }
                if (!ownedRelationshipSeen)
                {
                    logger.LogDebug("the ownedRelationship Json property was not found in the AnalysisCaseDefinition: {Id}", dtoInstance.Id);
                }
                if (!ownedRenderingSeen)
                {
                    logger.LogDebug("the ownedRendering Json property was not found in the AnalysisCaseDefinition: {Id}", dtoInstance.Id);
                }
                if (!ownedRequirementSeen)
                {
                    logger.LogDebug("the ownedRequirement Json property was not found in the AnalysisCaseDefinition: {Id}", dtoInstance.Id);
                }
                if (!ownedSpecializationSeen)
                {
                    logger.LogDebug("the ownedSpecialization Json property was not found in the AnalysisCaseDefinition: {Id}", dtoInstance.Id);
                }
                if (!ownedStateSeen)
                {
                    logger.LogDebug("the ownedState Json property was not found in the AnalysisCaseDefinition: {Id}", dtoInstance.Id);
                }
                if (!ownedSubclassificationSeen)
                {
                    logger.LogDebug("the ownedSubclassification Json property was not found in the AnalysisCaseDefinition: {Id}", dtoInstance.Id);
                }
                if (!ownedTransitionSeen)
                {
                    logger.LogDebug("the ownedTransition Json property was not found in the AnalysisCaseDefinition: {Id}", dtoInstance.Id);
                }
                if (!ownedUnioningSeen)
                {
                    logger.LogDebug("the ownedUnioning Json property was not found in the AnalysisCaseDefinition: {Id}", dtoInstance.Id);
                }
                if (!ownedUsageSeen)
                {
                    logger.LogDebug("the ownedUsage Json property was not found in the AnalysisCaseDefinition: {Id}", dtoInstance.Id);
                }
                if (!ownedUseCaseSeen)
                {
                    logger.LogDebug("the ownedUseCase Json property was not found in the AnalysisCaseDefinition: {Id}", dtoInstance.Id);
                }
                if (!ownedVerificationCaseSeen)
                {
                    logger.LogDebug("the ownedVerificationCase Json property was not found in the AnalysisCaseDefinition: {Id}", dtoInstance.Id);
                }
                if (!ownedViewSeen)
                {
                    logger.LogDebug("the ownedView Json property was not found in the AnalysisCaseDefinition: {Id}", dtoInstance.Id);
                }
                if (!ownedViewpointSeen)
                {
                    logger.LogDebug("the ownedViewpoint Json property was not found in the AnalysisCaseDefinition: {Id}", dtoInstance.Id);
                }
                if (!ownerSeen)
                {
                    logger.LogDebug("the owner Json property was not found in the AnalysisCaseDefinition: {Id}", dtoInstance.Id);
                }
                if (!owningMembershipSeen)
                {
                    logger.LogDebug("the owningMembership Json property was not found in the AnalysisCaseDefinition: {Id}", dtoInstance.Id);
                }
                if (!owningNamespaceSeen)
                {
                    logger.LogDebug("the owningNamespace Json property was not found in the AnalysisCaseDefinition: {Id}", dtoInstance.Id);
                }
                if (!owningRelationshipSeen)
                {
                    logger.LogDebug("the owningRelationship Json property was not found in the AnalysisCaseDefinition: {Id}", dtoInstance.Id);
                }
                if (!parameterSeen)
                {
                    logger.LogDebug("the parameter Json property was not found in the AnalysisCaseDefinition: {Id}", dtoInstance.Id);
                }
                if (!qualifiedNameSeen)
                {
                    logger.LogDebug("the qualifiedName Json property was not found in the AnalysisCaseDefinition: {Id}", dtoInstance.Id);
                }
                if (!resultSeen)
                {
                    logger.LogDebug("the result Json property was not found in the AnalysisCaseDefinition: {Id}", dtoInstance.Id);
                }
                if (!resultExpressionSeen)
                {
                    logger.LogDebug("the resultExpression Json property was not found in the AnalysisCaseDefinition: {Id}", dtoInstance.Id);
                }
                if (!shortNameSeen)
                {
                    logger.LogDebug("the shortName Json property was not found in the AnalysisCaseDefinition: {Id}", dtoInstance.Id);
                }
                if (!stepSeen)
                {
                    logger.LogDebug("the step Json property was not found in the AnalysisCaseDefinition: {Id}", dtoInstance.Id);
                }
                if (!subjectParameterSeen)
                {
                    logger.LogDebug("the subjectParameter Json property was not found in the AnalysisCaseDefinition: {Id}", dtoInstance.Id);
                }
                if (!textualRepresentationSeen)
                {
                    logger.LogDebug("the textualRepresentation Json property was not found in the AnalysisCaseDefinition: {Id}", dtoInstance.Id);
                }
                if (!unioningTypeSeen)
                {
                    logger.LogDebug("the unioningType Json property was not found in the AnalysisCaseDefinition: {Id}", dtoInstance.Id);
                }
                if (!usageSeen)
                {
                    logger.LogDebug("the usage Json property was not found in the AnalysisCaseDefinition: {Id}", dtoInstance.Id);
                }
                if (!variantSeen)
                {
                    logger.LogDebug("the variant Json property was not found in the AnalysisCaseDefinition: {Id}", dtoInstance.Id);
                }
                if (!variantMembershipSeen)
                {
                    logger.LogDebug("the variantMembership Json property was not found in the AnalysisCaseDefinition: {Id}", dtoInstance.Id);
                }
            }
        }

        /// <summary>
        /// Deserializes properties of a <see cref="AnalysisCaseDefinition" />
        /// from a <see cref="Utf8JsonReader" />, excluding derived properties
        /// </summary>
        /// <param name="dtoInstance">
        /// The <see cref="AnalysisCaseDefinition"/> instance holding deserialized values
        /// </param>
        /// <param name="reader">
        /// The <see cref="Utf8JsonReader"/> positioned on the <see cref="JsonTokenType.StartObject"/> of the
        /// <see cref="IAnalysisCaseDefinition"/> json object
        /// </param>
        /// <param name="logger">
        /// The <see cref="ILogger"/> to produce logging statement
        /// </param>
        private static void DeserializeDtoExcludingDerivedProperties(SysML2.NET.Core.DTO.Systems.AnalysisCases.AnalysisCaseDefinition dtoInstance, ref Utf8JsonReader reader, ILogger logger)
        {
            var aliasIdsSeen = false;
            var declaredNameSeen = false;
            var declaredShortNameSeen = false;
            var elementIdSeen = false;
            var isAbstractSeen = false;
            var isImpliedIncludedSeen = false;
            var isIndividualSeen = false;
            var isSufficientSeen = false;
            var isVariationSeen = false;
            var ownedRelationshipSeen = false;
            var owningRelationshipSeen = false;

            while (reader.Read() && reader.TokenType != JsonTokenType.EndObject)
            {
                if (reader.TokenType != JsonTokenType.PropertyName)
                {
                    throw new JsonException("Expected a property name in the AnalysisCaseDefinition json object.");
                }

                if (reader.ValueTextEquals("@id"u8))
                {
                    reader.Read();

                    if (reader.TokenType == JsonTokenType.Null)
                    {
                        throw new JsonException("The @id property is not present, the AnalysisCaseDefinition cannot be deserialized");
                    }

                    dtoInstance.Id = Utf8JsonReaderHelper.ReadGuid(ref reader);
                    continue;
                }

                if (reader.ValueTextEquals("aliasIds"u8))
                {
                    aliasIdsSeen = true;
                    reader.Read();

                    Utf8JsonReaderHelper.ExpectArrayStart(ref reader);

                    while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
                    {
                        var aliasIdsValue = reader.GetString();

                        if (aliasIdsValue != null)
                        {
                            dtoInstance.AliasIds.Add(aliasIdsValue);
                        }
                    }

                    continue;
                }

                if (reader.ValueTextEquals("declaredName"u8))
                {
                    declaredNameSeen = true;
                    reader.Read();

                    dtoInstance.DeclaredName = reader.GetString();

                    continue;
                }

                if (reader.ValueTextEquals("declaredShortName"u8))
                {
                    declaredShortNameSeen = true;
                    reader.Read();

                    dtoInstance.DeclaredShortName = reader.GetString();

                    continue;
                }

                if (reader.ValueTextEquals("elementId"u8))
                {
                    elementIdSeen = true;
                    reader.Read();

                    var elementIdValue = reader.GetString();

                    if (elementIdValue != null)
                    {
                        dtoInstance.ElementId = elementIdValue;
                    }

                    continue;
                }

                if (reader.ValueTextEquals("isAbstract"u8))
                {
                    isAbstractSeen = true;
                    reader.Read();

                    if (reader.TokenType != JsonTokenType.Null)
                    {
                        dtoInstance.IsAbstract = reader.GetBoolean();
                    }

                    continue;
                }

                if (reader.ValueTextEquals("isImpliedIncluded"u8))
                {
                    isImpliedIncludedSeen = true;
                    reader.Read();

                    if (reader.TokenType != JsonTokenType.Null)
                    {
                        dtoInstance.IsImpliedIncluded = reader.GetBoolean();
                    }

                    continue;
                }

                if (reader.ValueTextEquals("isIndividual"u8))
                {
                    isIndividualSeen = true;
                    reader.Read();

                    if (reader.TokenType != JsonTokenType.Null)
                    {
                        dtoInstance.IsIndividual = reader.GetBoolean();
                    }

                    continue;
                }

                if (reader.ValueTextEquals("isSufficient"u8))
                {
                    isSufficientSeen = true;
                    reader.Read();

                    if (reader.TokenType != JsonTokenType.Null)
                    {
                        dtoInstance.IsSufficient = reader.GetBoolean();
                    }

                    continue;
                }

                if (reader.ValueTextEquals("isVariation"u8))
                {
                    isVariationSeen = true;
                    reader.Read();

                    if (reader.TokenType != JsonTokenType.Null)
                    {
                        dtoInstance.IsVariation = reader.GetBoolean();
                    }

                    continue;
                }

                if (reader.ValueTextEquals("ownedRelationship"u8))
                {
                    ownedRelationshipSeen = true;
                    reader.Read();

                    Utf8JsonReaderHelper.ExpectArrayStart(ref reader);

                    while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
                    {
                        if (Utf8JsonReaderHelper.TryReadReferenceIdentifier(ref reader, out var ownedRelationshipValue))
                        {
                            dtoInstance.OwnedRelationship.Add(ownedRelationshipValue);
                        }
                    }

                    continue;
                }

                if (reader.ValueTextEquals("owningRelationship"u8))
                {
                    owningRelationshipSeen = true;
                    reader.Read();

                    if (reader.TokenType == JsonTokenType.Null)
                    {
                        dtoInstance.OwningRelationship = null;
                    }
                    else if (Utf8JsonReaderHelper.TryReadReferenceIdentifier(ref reader, out var owningRelationshipValue))
                    {
                        dtoInstance.OwningRelationship = owningRelationshipValue;
                    }

                    continue;
                }


                reader.Read();
                reader.Skip();
            }

            if (logger.IsEnabled(LogLevel.Debug))
            {
                if (!aliasIdsSeen)
                {
                    logger.LogDebug("the aliasIds Json property was not found in the AnalysisCaseDefinition: {Id}", dtoInstance.Id);
                }
                if (!declaredNameSeen)
                {
                    logger.LogDebug("the declaredName Json property was not found in the AnalysisCaseDefinition: {Id}", dtoInstance.Id);
                }
                if (!declaredShortNameSeen)
                {
                    logger.LogDebug("the declaredShortName Json property was not found in the AnalysisCaseDefinition: {Id}", dtoInstance.Id);
                }
                if (!elementIdSeen)
                {
                    logger.LogDebug("the elementId Json property was not found in the AnalysisCaseDefinition: {Id}", dtoInstance.Id);
                }
                if (!isAbstractSeen)
                {
                    logger.LogDebug("the isAbstract Json property was not found in the AnalysisCaseDefinition: {Id}", dtoInstance.Id);
                }
                if (!isImpliedIncludedSeen)
                {
                    logger.LogDebug("the isImpliedIncluded Json property was not found in the AnalysisCaseDefinition: {Id}", dtoInstance.Id);
                }
                if (!isIndividualSeen)
                {
                    logger.LogDebug("the isIndividual Json property was not found in the AnalysisCaseDefinition: {Id}", dtoInstance.Id);
                }
                if (!isSufficientSeen)
                {
                    logger.LogDebug("the isSufficient Json property was not found in the AnalysisCaseDefinition: {Id}", dtoInstance.Id);
                }
                if (!isVariationSeen)
                {
                    logger.LogDebug("the isVariation Json property was not found in the AnalysisCaseDefinition: {Id}", dtoInstance.Id);
                }
                if (!ownedRelationshipSeen)
                {
                    logger.LogDebug("the ownedRelationship Json property was not found in the AnalysisCaseDefinition: {Id}", dtoInstance.Id);
                }
                if (!owningRelationshipSeen)
                {
                    logger.LogDebug("the owningRelationship Json property was not found in the AnalysisCaseDefinition: {Id}", dtoInstance.Id);
                }
            }
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
