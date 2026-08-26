// -------------------------------------------------------------------------------------------------
// <copyright file="PerformActionUsageDeSerializer.cs" company="Starion Group S.A.">
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
    using SysML2.NET.Core.DTO.Systems.Actions;
    using SysML2.NET.Serializer.Json;
    using SysML2.NET.Serializer.Json.Utility;

    /// <summary>
    /// The purpose of the <see cref="PerformActionUsageDeSerializer"/> is to provide deserialization capabilities
    /// for the <see cref="IPerformActionUsage"/> interface
    /// </summary>
    internal static class PerformActionUsageDeSerializer
    {
        /// <summary>
        /// Deserializes an instance of <see cref="IPerformActionUsage"/> from the provided <see cref="Utf8JsonReader"/>
        /// </summary>
        /// <param name="reader">
        /// The <see cref="Utf8JsonReader"/> positioned on the <see cref="JsonTokenType.StartObject"/> of the
        /// <see cref="IPerformActionUsage"/> json object. On return the reader is positioned on the matching
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
        /// an instance of <see cref="IPerformActionUsage"/>
        /// </returns>
        /// <remarks>
        /// The <c>@type</c> property is the discriminator that the caller dispatched on, so it is skipped rather
        /// than re-validated here
        /// </remarks>
        internal static IPerformActionUsage DeSerialize(ref Utf8JsonReader reader, SerializationModeKind serializationModeKind, bool deserializeDerivedProperties, ILoggerFactory loggerFactory = null)
        {
            var logger = loggerFactory == null ? NullLogger.Instance : loggerFactory.CreateLogger("PerformActionUsageDeSerializer");

            Utf8JsonReaderHelper.Expect(ref reader, JsonTokenType.StartObject);

            var dtoInstance = new SysML2.NET.Core.DTO.Systems.Actions.PerformActionUsage();

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
        /// Deserializes properties of a <see cref="PerformActionUsage" />
        /// from a <see cref="Utf8JsonReader" />, including derived properties
        /// </summary>
        /// <param name="dtoInstance">
        /// The <see cref="PerformActionUsage"/> instance holding deserialized values
        /// </param>
        /// <param name="reader">
        /// The <see cref="Utf8JsonReader"/> positioned on the <see cref="JsonTokenType.StartObject"/> of the
        /// <see cref="IPerformActionUsage"/> json object
        /// </param>
        /// <param name="logger">
        /// The <see cref="ILogger"/> to produce logging statement
        /// </param>
        private static void DeserializeDtoIncludingDerivedProperties(SysML2.NET.Core.DTO.Systems.Actions.PerformActionUsage dtoInstance, ref Utf8JsonReader reader, ILogger logger)
        {
            var actionDefinitionSeen = false;
            var aliasIdsSeen = false;
            var chainingFeatureSeen = false;
            var crossFeatureSeen = false;
            var declaredNameSeen = false;
            var declaredShortNameSeen = false;
            var differencingTypeSeen = false;
            var directedUsageSeen = false;
            var directionSeen = false;
            var documentationSeen = false;
            var elementIdSeen = false;
            var endFeatureSeen = false;
            var endOwningTypeSeen = false;
            var featureSeen = false;
            var featureMembershipSeen = false;
            var featureTargetSeen = false;
            var featuringTypeSeen = false;
            var importedMembershipSeen = false;
            var individualDefinitionSeen = false;
            var inheritedFeatureSeen = false;
            var inheritedMembershipSeen = false;
            var inputSeen = false;
            var intersectingTypeSeen = false;
            var isAbstractSeen = false;
            var isCompositeSeen = false;
            var isConjugatedSeen = false;
            var isConstantSeen = false;
            var isDerivedSeen = false;
            var isEndSeen = false;
            var isImpliedIncludedSeen = false;
            var isIndividualSeen = false;
            var isLibraryElementSeen = false;
            var isOrderedSeen = false;
            var isPortionSeen = false;
            var isReferenceSeen = false;
            var isSufficientSeen = false;
            var isUniqueSeen = false;
            var isVariationSeen = false;
            var mayTimeVarySeen = false;
            var memberSeen = false;
            var membershipSeen = false;
            var multiplicitySeen = false;
            var nameSeen = false;
            var nestedActionSeen = false;
            var nestedAllocationSeen = false;
            var nestedAnalysisCaseSeen = false;
            var nestedAttributeSeen = false;
            var nestedCalculationSeen = false;
            var nestedCaseSeen = false;
            var nestedConcernSeen = false;
            var nestedConnectionSeen = false;
            var nestedConstraintSeen = false;
            var nestedEnumerationSeen = false;
            var nestedFlowSeen = false;
            var nestedInterfaceSeen = false;
            var nestedItemSeen = false;
            var nestedMetadataSeen = false;
            var nestedOccurrenceSeen = false;
            var nestedPartSeen = false;
            var nestedPortSeen = false;
            var nestedReferenceSeen = false;
            var nestedRenderingSeen = false;
            var nestedRequirementSeen = false;
            var nestedStateSeen = false;
            var nestedTransitionSeen = false;
            var nestedUsageSeen = false;
            var nestedUseCaseSeen = false;
            var nestedVerificationCaseSeen = false;
            var nestedViewSeen = false;
            var nestedViewpointSeen = false;
            var outputSeen = false;
            var ownedAnnotationSeen = false;
            var ownedConjugatorSeen = false;
            var ownedCrossSubsettingSeen = false;
            var ownedDifferencingSeen = false;
            var ownedDisjoiningSeen = false;
            var ownedElementSeen = false;
            var ownedEndFeatureSeen = false;
            var ownedFeatureSeen = false;
            var ownedFeatureChainingSeen = false;
            var ownedFeatureInvertingSeen = false;
            var ownedFeatureMembershipSeen = false;
            var ownedImportSeen = false;
            var ownedIntersectingSeen = false;
            var ownedMemberSeen = false;
            var ownedMembershipSeen = false;
            var ownedRedefinitionSeen = false;
            var ownedReferenceSubsettingSeen = false;
            var ownedRelationshipSeen = false;
            var ownedSpecializationSeen = false;
            var ownedSubsettingSeen = false;
            var ownedTypeFeaturingSeen = false;
            var ownedTypingSeen = false;
            var ownedUnioningSeen = false;
            var ownerSeen = false;
            var owningDefinitionSeen = false;
            var owningFeatureMembershipSeen = false;
            var owningMembershipSeen = false;
            var owningNamespaceSeen = false;
            var owningRelationshipSeen = false;
            var owningTypeSeen = false;
            var owningUsageSeen = false;
            var parameterSeen = false;
            var performedActionSeen = false;
            var portionKindSeen = false;
            var qualifiedNameSeen = false;
            var shortNameSeen = false;
            var textualRepresentationSeen = false;
            var unioningTypeSeen = false;
            var usageSeen = false;
            var variantSeen = false;
            var variantMembershipSeen = false;

            while (reader.Read() && reader.TokenType != JsonTokenType.EndObject)
            {
                if (reader.TokenType != JsonTokenType.PropertyName)
                {
                    throw new JsonException("Expected a property name in the PerformActionUsage json object.");
                }

                if (reader.ValueTextEquals("@id"u8))
                {
                    reader.Read();

                    if (reader.TokenType == JsonTokenType.Null)
                    {
                        throw new JsonException("The @id property is not present, the PerformActionUsage cannot be deserialized");
                    }

                    dtoInstance.Id = Utf8JsonReaderHelper.ReadGuid(ref reader);
                    continue;
                }

                if (reader.ValueTextEquals("actionDefinition"u8))
                {
                    actionDefinitionSeen = true;
                    reader.Read();

                    Utf8JsonReaderHelper.ExpectArrayStart(ref reader);

                    while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
                    {
                        if (Utf8JsonReaderHelper.TryReadReferenceIdentifier(ref reader, out var actionDefinitionValue))
                        {
                            dtoInstance.actionDefinition.Add(actionDefinitionValue);
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

                if (reader.ValueTextEquals("chainingFeature"u8))
                {
                    chainingFeatureSeen = true;
                    reader.Read();

                    Utf8JsonReaderHelper.ExpectArrayStart(ref reader);

                    while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
                    {
                        if (Utf8JsonReaderHelper.TryReadReferenceIdentifier(ref reader, out var chainingFeatureValue))
                        {
                            dtoInstance.chainingFeature.Add(chainingFeatureValue);
                        }
                    }

                    continue;
                }

                if (reader.ValueTextEquals("crossFeature"u8))
                {
                    crossFeatureSeen = true;
                    reader.Read();

                    if (reader.TokenType == JsonTokenType.Null)
                    {
                        dtoInstance.crossFeature = null;
                    }
                    else if (Utf8JsonReaderHelper.TryReadReferenceIdentifier(ref reader, out var crossFeatureValue))
                    {
                        dtoInstance.crossFeature = crossFeatureValue;
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

                if (reader.ValueTextEquals("direction"u8))
                {
                    directionSeen = true;
                    reader.Read();

                    dtoInstance.Direction = FeatureDirectionKindDeSerializer.DeserializeNullable(reader.GetString());

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

                if (reader.ValueTextEquals("endOwningType"u8))
                {
                    endOwningTypeSeen = true;
                    reader.Read();

                    if (reader.TokenType == JsonTokenType.Null)
                    {
                        dtoInstance.endOwningType = null;
                    }
                    else if (Utf8JsonReaderHelper.TryReadReferenceIdentifier(ref reader, out var endOwningTypeValue))
                    {
                        dtoInstance.endOwningType = endOwningTypeValue;
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

                if (reader.ValueTextEquals("featureTarget"u8))
                {
                    featureTargetSeen = true;
                    reader.Read();

                    if (reader.TokenType == JsonTokenType.Null)
                    {
                        dtoInstance.featureTarget = Guid.Empty;

                        if (logger.IsEnabled(LogLevel.Debug))
                        {
                            logger.LogDebug("the PerformActionUsage.featureTarget property was not found in the Json. The value is set to Guid.Empty");
                        }
                    }
                    else if (Utf8JsonReaderHelper.TryReadReferenceIdentifier(ref reader, out var featureTargetValue))
                    {
                        dtoInstance.featureTarget = featureTargetValue;
                    }

                    continue;
                }

                if (reader.ValueTextEquals("featuringType"u8))
                {
                    featuringTypeSeen = true;
                    reader.Read();

                    Utf8JsonReaderHelper.ExpectArrayStart(ref reader);

                    while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
                    {
                        if (Utf8JsonReaderHelper.TryReadReferenceIdentifier(ref reader, out var featuringTypeValue))
                        {
                            dtoInstance.featuringType.Add(featuringTypeValue);
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

                if (reader.ValueTextEquals("individualDefinition"u8))
                {
                    individualDefinitionSeen = true;
                    reader.Read();

                    if (reader.TokenType == JsonTokenType.Null)
                    {
                        dtoInstance.individualDefinition = null;
                    }
                    else if (Utf8JsonReaderHelper.TryReadReferenceIdentifier(ref reader, out var individualDefinitionValue))
                    {
                        dtoInstance.individualDefinition = individualDefinitionValue;
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

                if (reader.ValueTextEquals("isComposite"u8))
                {
                    isCompositeSeen = true;
                    reader.Read();

                    if (reader.TokenType != JsonTokenType.Null)
                    {
                        dtoInstance.IsComposite = reader.GetBoolean();
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

                if (reader.ValueTextEquals("isConstant"u8))
                {
                    isConstantSeen = true;
                    reader.Read();

                    if (reader.TokenType != JsonTokenType.Null)
                    {
                        dtoInstance.IsConstant = reader.GetBoolean();
                    }

                    continue;
                }

                if (reader.ValueTextEquals("isDerived"u8))
                {
                    isDerivedSeen = true;
                    reader.Read();

                    if (reader.TokenType != JsonTokenType.Null)
                    {
                        dtoInstance.IsDerived = reader.GetBoolean();
                    }

                    continue;
                }

                if (reader.ValueTextEquals("isEnd"u8))
                {
                    isEndSeen = true;
                    reader.Read();

                    if (reader.TokenType != JsonTokenType.Null)
                    {
                        dtoInstance.IsEnd = reader.GetBoolean();
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

                if (reader.ValueTextEquals("isOrdered"u8))
                {
                    isOrderedSeen = true;
                    reader.Read();

                    if (reader.TokenType != JsonTokenType.Null)
                    {
                        dtoInstance.IsOrdered = reader.GetBoolean();
                    }

                    continue;
                }

                if (reader.ValueTextEquals("isPortion"u8))
                {
                    isPortionSeen = true;
                    reader.Read();

                    if (reader.TokenType != JsonTokenType.Null)
                    {
                        dtoInstance.IsPortion = reader.GetBoolean();
                    }

                    continue;
                }

                if (reader.ValueTextEquals("isReference"u8))
                {
                    isReferenceSeen = true;
                    reader.Read();

                    if (reader.TokenType != JsonTokenType.Null)
                    {
                        dtoInstance.isReference = reader.GetBoolean();
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

                if (reader.ValueTextEquals("isUnique"u8))
                {
                    isUniqueSeen = true;
                    reader.Read();

                    if (reader.TokenType != JsonTokenType.Null)
                    {
                        dtoInstance.IsUnique = reader.GetBoolean();
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

                if (reader.ValueTextEquals("mayTimeVary"u8))
                {
                    mayTimeVarySeen = true;
                    reader.Read();

                    if (reader.TokenType != JsonTokenType.Null)
                    {
                        dtoInstance.mayTimeVary = reader.GetBoolean();
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

                if (reader.ValueTextEquals("nestedAction"u8))
                {
                    nestedActionSeen = true;
                    reader.Read();

                    Utf8JsonReaderHelper.ExpectArrayStart(ref reader);

                    while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
                    {
                        if (Utf8JsonReaderHelper.TryReadReferenceIdentifier(ref reader, out var nestedActionValue))
                        {
                            dtoInstance.nestedAction.Add(nestedActionValue);
                        }
                    }

                    continue;
                }

                if (reader.ValueTextEquals("nestedAllocation"u8))
                {
                    nestedAllocationSeen = true;
                    reader.Read();

                    Utf8JsonReaderHelper.ExpectArrayStart(ref reader);

                    while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
                    {
                        if (Utf8JsonReaderHelper.TryReadReferenceIdentifier(ref reader, out var nestedAllocationValue))
                        {
                            dtoInstance.nestedAllocation.Add(nestedAllocationValue);
                        }
                    }

                    continue;
                }

                if (reader.ValueTextEquals("nestedAnalysisCase"u8))
                {
                    nestedAnalysisCaseSeen = true;
                    reader.Read();

                    Utf8JsonReaderHelper.ExpectArrayStart(ref reader);

                    while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
                    {
                        if (Utf8JsonReaderHelper.TryReadReferenceIdentifier(ref reader, out var nestedAnalysisCaseValue))
                        {
                            dtoInstance.nestedAnalysisCase.Add(nestedAnalysisCaseValue);
                        }
                    }

                    continue;
                }

                if (reader.ValueTextEquals("nestedAttribute"u8))
                {
                    nestedAttributeSeen = true;
                    reader.Read();

                    Utf8JsonReaderHelper.ExpectArrayStart(ref reader);

                    while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
                    {
                        if (Utf8JsonReaderHelper.TryReadReferenceIdentifier(ref reader, out var nestedAttributeValue))
                        {
                            dtoInstance.nestedAttribute.Add(nestedAttributeValue);
                        }
                    }

                    continue;
                }

                if (reader.ValueTextEquals("nestedCalculation"u8))
                {
                    nestedCalculationSeen = true;
                    reader.Read();

                    Utf8JsonReaderHelper.ExpectArrayStart(ref reader);

                    while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
                    {
                        if (Utf8JsonReaderHelper.TryReadReferenceIdentifier(ref reader, out var nestedCalculationValue))
                        {
                            dtoInstance.nestedCalculation.Add(nestedCalculationValue);
                        }
                    }

                    continue;
                }

                if (reader.ValueTextEquals("nestedCase"u8))
                {
                    nestedCaseSeen = true;
                    reader.Read();

                    Utf8JsonReaderHelper.ExpectArrayStart(ref reader);

                    while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
                    {
                        if (Utf8JsonReaderHelper.TryReadReferenceIdentifier(ref reader, out var nestedCaseValue))
                        {
                            dtoInstance.nestedCase.Add(nestedCaseValue);
                        }
                    }

                    continue;
                }

                if (reader.ValueTextEquals("nestedConcern"u8))
                {
                    nestedConcernSeen = true;
                    reader.Read();

                    Utf8JsonReaderHelper.ExpectArrayStart(ref reader);

                    while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
                    {
                        if (Utf8JsonReaderHelper.TryReadReferenceIdentifier(ref reader, out var nestedConcernValue))
                        {
                            dtoInstance.nestedConcern.Add(nestedConcernValue);
                        }
                    }

                    continue;
                }

                if (reader.ValueTextEquals("nestedConnection"u8))
                {
                    nestedConnectionSeen = true;
                    reader.Read();

                    Utf8JsonReaderHelper.ExpectArrayStart(ref reader);

                    while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
                    {
                        if (Utf8JsonReaderHelper.TryReadReferenceIdentifier(ref reader, out var nestedConnectionValue))
                        {
                            dtoInstance.nestedConnection.Add(nestedConnectionValue);
                        }
                    }

                    continue;
                }

                if (reader.ValueTextEquals("nestedConstraint"u8))
                {
                    nestedConstraintSeen = true;
                    reader.Read();

                    Utf8JsonReaderHelper.ExpectArrayStart(ref reader);

                    while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
                    {
                        if (Utf8JsonReaderHelper.TryReadReferenceIdentifier(ref reader, out var nestedConstraintValue))
                        {
                            dtoInstance.nestedConstraint.Add(nestedConstraintValue);
                        }
                    }

                    continue;
                }

                if (reader.ValueTextEquals("nestedEnumeration"u8))
                {
                    nestedEnumerationSeen = true;
                    reader.Read();

                    Utf8JsonReaderHelper.ExpectArrayStart(ref reader);

                    while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
                    {
                        if (Utf8JsonReaderHelper.TryReadReferenceIdentifier(ref reader, out var nestedEnumerationValue))
                        {
                            dtoInstance.nestedEnumeration.Add(nestedEnumerationValue);
                        }
                    }

                    continue;
                }

                if (reader.ValueTextEquals("nestedFlow"u8))
                {
                    nestedFlowSeen = true;
                    reader.Read();

                    Utf8JsonReaderHelper.ExpectArrayStart(ref reader);

                    while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
                    {
                        if (Utf8JsonReaderHelper.TryReadReferenceIdentifier(ref reader, out var nestedFlowValue))
                        {
                            dtoInstance.nestedFlow.Add(nestedFlowValue);
                        }
                    }

                    continue;
                }

                if (reader.ValueTextEquals("nestedInterface"u8))
                {
                    nestedInterfaceSeen = true;
                    reader.Read();

                    Utf8JsonReaderHelper.ExpectArrayStart(ref reader);

                    while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
                    {
                        if (Utf8JsonReaderHelper.TryReadReferenceIdentifier(ref reader, out var nestedInterfaceValue))
                        {
                            dtoInstance.nestedInterface.Add(nestedInterfaceValue);
                        }
                    }

                    continue;
                }

                if (reader.ValueTextEquals("nestedItem"u8))
                {
                    nestedItemSeen = true;
                    reader.Read();

                    Utf8JsonReaderHelper.ExpectArrayStart(ref reader);

                    while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
                    {
                        if (Utf8JsonReaderHelper.TryReadReferenceIdentifier(ref reader, out var nestedItemValue))
                        {
                            dtoInstance.nestedItem.Add(nestedItemValue);
                        }
                    }

                    continue;
                }

                if (reader.ValueTextEquals("nestedMetadata"u8))
                {
                    nestedMetadataSeen = true;
                    reader.Read();

                    Utf8JsonReaderHelper.ExpectArrayStart(ref reader);

                    while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
                    {
                        if (Utf8JsonReaderHelper.TryReadReferenceIdentifier(ref reader, out var nestedMetadataValue))
                        {
                            dtoInstance.nestedMetadata.Add(nestedMetadataValue);
                        }
                    }

                    continue;
                }

                if (reader.ValueTextEquals("nestedOccurrence"u8))
                {
                    nestedOccurrenceSeen = true;
                    reader.Read();

                    Utf8JsonReaderHelper.ExpectArrayStart(ref reader);

                    while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
                    {
                        if (Utf8JsonReaderHelper.TryReadReferenceIdentifier(ref reader, out var nestedOccurrenceValue))
                        {
                            dtoInstance.nestedOccurrence.Add(nestedOccurrenceValue);
                        }
                    }

                    continue;
                }

                if (reader.ValueTextEquals("nestedPart"u8))
                {
                    nestedPartSeen = true;
                    reader.Read();

                    Utf8JsonReaderHelper.ExpectArrayStart(ref reader);

                    while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
                    {
                        if (Utf8JsonReaderHelper.TryReadReferenceIdentifier(ref reader, out var nestedPartValue))
                        {
                            dtoInstance.nestedPart.Add(nestedPartValue);
                        }
                    }

                    continue;
                }

                if (reader.ValueTextEquals("nestedPort"u8))
                {
                    nestedPortSeen = true;
                    reader.Read();

                    Utf8JsonReaderHelper.ExpectArrayStart(ref reader);

                    while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
                    {
                        if (Utf8JsonReaderHelper.TryReadReferenceIdentifier(ref reader, out var nestedPortValue))
                        {
                            dtoInstance.nestedPort.Add(nestedPortValue);
                        }
                    }

                    continue;
                }

                if (reader.ValueTextEquals("nestedReference"u8))
                {
                    nestedReferenceSeen = true;
                    reader.Read();

                    Utf8JsonReaderHelper.ExpectArrayStart(ref reader);

                    while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
                    {
                        if (Utf8JsonReaderHelper.TryReadReferenceIdentifier(ref reader, out var nestedReferenceValue))
                        {
                            dtoInstance.nestedReference.Add(nestedReferenceValue);
                        }
                    }

                    continue;
                }

                if (reader.ValueTextEquals("nestedRendering"u8))
                {
                    nestedRenderingSeen = true;
                    reader.Read();

                    Utf8JsonReaderHelper.ExpectArrayStart(ref reader);

                    while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
                    {
                        if (Utf8JsonReaderHelper.TryReadReferenceIdentifier(ref reader, out var nestedRenderingValue))
                        {
                            dtoInstance.nestedRendering.Add(nestedRenderingValue);
                        }
                    }

                    continue;
                }

                if (reader.ValueTextEquals("nestedRequirement"u8))
                {
                    nestedRequirementSeen = true;
                    reader.Read();

                    Utf8JsonReaderHelper.ExpectArrayStart(ref reader);

                    while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
                    {
                        if (Utf8JsonReaderHelper.TryReadReferenceIdentifier(ref reader, out var nestedRequirementValue))
                        {
                            dtoInstance.nestedRequirement.Add(nestedRequirementValue);
                        }
                    }

                    continue;
                }

                if (reader.ValueTextEquals("nestedState"u8))
                {
                    nestedStateSeen = true;
                    reader.Read();

                    Utf8JsonReaderHelper.ExpectArrayStart(ref reader);

                    while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
                    {
                        if (Utf8JsonReaderHelper.TryReadReferenceIdentifier(ref reader, out var nestedStateValue))
                        {
                            dtoInstance.nestedState.Add(nestedStateValue);
                        }
                    }

                    continue;
                }

                if (reader.ValueTextEquals("nestedTransition"u8))
                {
                    nestedTransitionSeen = true;
                    reader.Read();

                    Utf8JsonReaderHelper.ExpectArrayStart(ref reader);

                    while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
                    {
                        if (Utf8JsonReaderHelper.TryReadReferenceIdentifier(ref reader, out var nestedTransitionValue))
                        {
                            dtoInstance.nestedTransition.Add(nestedTransitionValue);
                        }
                    }

                    continue;
                }

                if (reader.ValueTextEquals("nestedUsage"u8))
                {
                    nestedUsageSeen = true;
                    reader.Read();

                    Utf8JsonReaderHelper.ExpectArrayStart(ref reader);

                    while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
                    {
                        if (Utf8JsonReaderHelper.TryReadReferenceIdentifier(ref reader, out var nestedUsageValue))
                        {
                            dtoInstance.nestedUsage.Add(nestedUsageValue);
                        }
                    }

                    continue;
                }

                if (reader.ValueTextEquals("nestedUseCase"u8))
                {
                    nestedUseCaseSeen = true;
                    reader.Read();

                    Utf8JsonReaderHelper.ExpectArrayStart(ref reader);

                    while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
                    {
                        if (Utf8JsonReaderHelper.TryReadReferenceIdentifier(ref reader, out var nestedUseCaseValue))
                        {
                            dtoInstance.nestedUseCase.Add(nestedUseCaseValue);
                        }
                    }

                    continue;
                }

                if (reader.ValueTextEquals("nestedVerificationCase"u8))
                {
                    nestedVerificationCaseSeen = true;
                    reader.Read();

                    Utf8JsonReaderHelper.ExpectArrayStart(ref reader);

                    while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
                    {
                        if (Utf8JsonReaderHelper.TryReadReferenceIdentifier(ref reader, out var nestedVerificationCaseValue))
                        {
                            dtoInstance.nestedVerificationCase.Add(nestedVerificationCaseValue);
                        }
                    }

                    continue;
                }

                if (reader.ValueTextEquals("nestedView"u8))
                {
                    nestedViewSeen = true;
                    reader.Read();

                    Utf8JsonReaderHelper.ExpectArrayStart(ref reader);

                    while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
                    {
                        if (Utf8JsonReaderHelper.TryReadReferenceIdentifier(ref reader, out var nestedViewValue))
                        {
                            dtoInstance.nestedView.Add(nestedViewValue);
                        }
                    }

                    continue;
                }

                if (reader.ValueTextEquals("nestedViewpoint"u8))
                {
                    nestedViewpointSeen = true;
                    reader.Read();

                    Utf8JsonReaderHelper.ExpectArrayStart(ref reader);

                    while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
                    {
                        if (Utf8JsonReaderHelper.TryReadReferenceIdentifier(ref reader, out var nestedViewpointValue))
                        {
                            dtoInstance.nestedViewpoint.Add(nestedViewpointValue);
                        }
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

                if (reader.ValueTextEquals("ownedCrossSubsetting"u8))
                {
                    ownedCrossSubsettingSeen = true;
                    reader.Read();

                    if (reader.TokenType == JsonTokenType.Null)
                    {
                        dtoInstance.ownedCrossSubsetting = null;
                    }
                    else if (Utf8JsonReaderHelper.TryReadReferenceIdentifier(ref reader, out var ownedCrossSubsettingValue))
                    {
                        dtoInstance.ownedCrossSubsetting = ownedCrossSubsettingValue;
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

                if (reader.ValueTextEquals("ownedFeatureChaining"u8))
                {
                    ownedFeatureChainingSeen = true;
                    reader.Read();

                    Utf8JsonReaderHelper.ExpectArrayStart(ref reader);

                    while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
                    {
                        if (Utf8JsonReaderHelper.TryReadReferenceIdentifier(ref reader, out var ownedFeatureChainingValue))
                        {
                            dtoInstance.ownedFeatureChaining.Add(ownedFeatureChainingValue);
                        }
                    }

                    continue;
                }

                if (reader.ValueTextEquals("ownedFeatureInverting"u8))
                {
                    ownedFeatureInvertingSeen = true;
                    reader.Read();

                    Utf8JsonReaderHelper.ExpectArrayStart(ref reader);

                    while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
                    {
                        if (Utf8JsonReaderHelper.TryReadReferenceIdentifier(ref reader, out var ownedFeatureInvertingValue))
                        {
                            dtoInstance.ownedFeatureInverting.Add(ownedFeatureInvertingValue);
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

                if (reader.ValueTextEquals("ownedRedefinition"u8))
                {
                    ownedRedefinitionSeen = true;
                    reader.Read();

                    Utf8JsonReaderHelper.ExpectArrayStart(ref reader);

                    while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
                    {
                        if (Utf8JsonReaderHelper.TryReadReferenceIdentifier(ref reader, out var ownedRedefinitionValue))
                        {
                            dtoInstance.ownedRedefinition.Add(ownedRedefinitionValue);
                        }
                    }

                    continue;
                }

                if (reader.ValueTextEquals("ownedReferenceSubsetting"u8))
                {
                    ownedReferenceSubsettingSeen = true;
                    reader.Read();

                    if (reader.TokenType == JsonTokenType.Null)
                    {
                        dtoInstance.ownedReferenceSubsetting = null;
                    }
                    else if (Utf8JsonReaderHelper.TryReadReferenceIdentifier(ref reader, out var ownedReferenceSubsettingValue))
                    {
                        dtoInstance.ownedReferenceSubsetting = ownedReferenceSubsettingValue;
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

                if (reader.ValueTextEquals("ownedSubsetting"u8))
                {
                    ownedSubsettingSeen = true;
                    reader.Read();

                    Utf8JsonReaderHelper.ExpectArrayStart(ref reader);

                    while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
                    {
                        if (Utf8JsonReaderHelper.TryReadReferenceIdentifier(ref reader, out var ownedSubsettingValue))
                        {
                            dtoInstance.ownedSubsetting.Add(ownedSubsettingValue);
                        }
                    }

                    continue;
                }

                if (reader.ValueTextEquals("ownedTypeFeaturing"u8))
                {
                    ownedTypeFeaturingSeen = true;
                    reader.Read();

                    Utf8JsonReaderHelper.ExpectArrayStart(ref reader);

                    while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
                    {
                        if (Utf8JsonReaderHelper.TryReadReferenceIdentifier(ref reader, out var ownedTypeFeaturingValue))
                        {
                            dtoInstance.ownedTypeFeaturing.Add(ownedTypeFeaturingValue);
                        }
                    }

                    continue;
                }

                if (reader.ValueTextEquals("ownedTyping"u8))
                {
                    ownedTypingSeen = true;
                    reader.Read();

                    Utf8JsonReaderHelper.ExpectArrayStart(ref reader);

                    while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
                    {
                        if (Utf8JsonReaderHelper.TryReadReferenceIdentifier(ref reader, out var ownedTypingValue))
                        {
                            dtoInstance.ownedTyping.Add(ownedTypingValue);
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

                if (reader.ValueTextEquals("owningDefinition"u8))
                {
                    owningDefinitionSeen = true;
                    reader.Read();

                    if (reader.TokenType == JsonTokenType.Null)
                    {
                        dtoInstance.owningDefinition = null;
                    }
                    else if (Utf8JsonReaderHelper.TryReadReferenceIdentifier(ref reader, out var owningDefinitionValue))
                    {
                        dtoInstance.owningDefinition = owningDefinitionValue;
                    }

                    continue;
                }

                if (reader.ValueTextEquals("owningFeatureMembership"u8))
                {
                    owningFeatureMembershipSeen = true;
                    reader.Read();

                    if (reader.TokenType == JsonTokenType.Null)
                    {
                        dtoInstance.owningFeatureMembership = null;
                    }
                    else if (Utf8JsonReaderHelper.TryReadReferenceIdentifier(ref reader, out var owningFeatureMembershipValue))
                    {
                        dtoInstance.owningFeatureMembership = owningFeatureMembershipValue;
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

                if (reader.ValueTextEquals("owningType"u8))
                {
                    owningTypeSeen = true;
                    reader.Read();

                    if (reader.TokenType == JsonTokenType.Null)
                    {
                        dtoInstance.owningType = null;
                    }
                    else if (Utf8JsonReaderHelper.TryReadReferenceIdentifier(ref reader, out var owningTypeValue))
                    {
                        dtoInstance.owningType = owningTypeValue;
                    }

                    continue;
                }

                if (reader.ValueTextEquals("owningUsage"u8))
                {
                    owningUsageSeen = true;
                    reader.Read();

                    if (reader.TokenType == JsonTokenType.Null)
                    {
                        dtoInstance.owningUsage = null;
                    }
                    else if (Utf8JsonReaderHelper.TryReadReferenceIdentifier(ref reader, out var owningUsageValue))
                    {
                        dtoInstance.owningUsage = owningUsageValue;
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

                if (reader.ValueTextEquals("performedAction"u8))
                {
                    performedActionSeen = true;
                    reader.Read();

                    if (reader.TokenType == JsonTokenType.Null)
                    {
                        dtoInstance.performedAction = Guid.Empty;

                        if (logger.IsEnabled(LogLevel.Debug))
                        {
                            logger.LogDebug("the PerformActionUsage.performedAction property was not found in the Json. The value is set to Guid.Empty");
                        }
                    }
                    else if (Utf8JsonReaderHelper.TryReadReferenceIdentifier(ref reader, out var performedActionValue))
                    {
                        dtoInstance.performedAction = performedActionValue;
                    }

                    continue;
                }

                if (reader.ValueTextEquals("portionKind"u8))
                {
                    portionKindSeen = true;
                    reader.Read();

                    dtoInstance.PortionKind = PortionKindDeSerializer.DeserializeNullable(reader.GetString());

                    continue;
                }

                if (reader.ValueTextEquals("qualifiedName"u8))
                {
                    qualifiedNameSeen = true;
                    reader.Read();

                    dtoInstance.qualifiedName = reader.GetString();

                    continue;
                }

                if (reader.ValueTextEquals("shortName"u8))
                {
                    shortNameSeen = true;
                    reader.Read();

                    dtoInstance.shortName = reader.GetString();

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
                if (!actionDefinitionSeen)
                {
                    logger.LogDebug("the actionDefinition Json property was not found in the PerformActionUsage: {Id}", dtoInstance.Id);
                }
                if (!aliasIdsSeen)
                {
                    logger.LogDebug("the aliasIds Json property was not found in the PerformActionUsage: {Id}", dtoInstance.Id);
                }
                if (!chainingFeatureSeen)
                {
                    logger.LogDebug("the chainingFeature Json property was not found in the PerformActionUsage: {Id}", dtoInstance.Id);
                }
                if (!crossFeatureSeen)
                {
                    logger.LogDebug("the crossFeature Json property was not found in the PerformActionUsage: {Id}", dtoInstance.Id);
                }
                if (!declaredNameSeen)
                {
                    logger.LogDebug("the declaredName Json property was not found in the PerformActionUsage: {Id}", dtoInstance.Id);
                }
                if (!declaredShortNameSeen)
                {
                    logger.LogDebug("the declaredShortName Json property was not found in the PerformActionUsage: {Id}", dtoInstance.Id);
                }
                if (!differencingTypeSeen)
                {
                    logger.LogDebug("the differencingType Json property was not found in the PerformActionUsage: {Id}", dtoInstance.Id);
                }
                if (!directedUsageSeen)
                {
                    logger.LogDebug("the directedUsage Json property was not found in the PerformActionUsage: {Id}", dtoInstance.Id);
                }
                if (!directionSeen)
                {
                    logger.LogDebug("the direction Json property was not found in the PerformActionUsage: {Id}", dtoInstance.Id);
                }
                if (!documentationSeen)
                {
                    logger.LogDebug("the documentation Json property was not found in the PerformActionUsage: {Id}", dtoInstance.Id);
                }
                if (!elementIdSeen)
                {
                    logger.LogDebug("the elementId Json property was not found in the PerformActionUsage: {Id}", dtoInstance.Id);
                }
                if (!endFeatureSeen)
                {
                    logger.LogDebug("the endFeature Json property was not found in the PerformActionUsage: {Id}", dtoInstance.Id);
                }
                if (!endOwningTypeSeen)
                {
                    logger.LogDebug("the endOwningType Json property was not found in the PerformActionUsage: {Id}", dtoInstance.Id);
                }
                if (!featureSeen)
                {
                    logger.LogDebug("the feature Json property was not found in the PerformActionUsage: {Id}", dtoInstance.Id);
                }
                if (!featureMembershipSeen)
                {
                    logger.LogDebug("the featureMembership Json property was not found in the PerformActionUsage: {Id}", dtoInstance.Id);
                }
                if (!featureTargetSeen)
                {
                    logger.LogDebug("the featureTarget Json property was not found in the PerformActionUsage: {Id}", dtoInstance.Id);
                }
                if (!featuringTypeSeen)
                {
                    logger.LogDebug("the featuringType Json property was not found in the PerformActionUsage: {Id}", dtoInstance.Id);
                }
                if (!importedMembershipSeen)
                {
                    logger.LogDebug("the importedMembership Json property was not found in the PerformActionUsage: {Id}", dtoInstance.Id);
                }
                if (!individualDefinitionSeen)
                {
                    logger.LogDebug("the individualDefinition Json property was not found in the PerformActionUsage: {Id}", dtoInstance.Id);
                }
                if (!inheritedFeatureSeen)
                {
                    logger.LogDebug("the inheritedFeature Json property was not found in the PerformActionUsage: {Id}", dtoInstance.Id);
                }
                if (!inheritedMembershipSeen)
                {
                    logger.LogDebug("the inheritedMembership Json property was not found in the PerformActionUsage: {Id}", dtoInstance.Id);
                }
                if (!inputSeen)
                {
                    logger.LogDebug("the input Json property was not found in the PerformActionUsage: {Id}", dtoInstance.Id);
                }
                if (!intersectingTypeSeen)
                {
                    logger.LogDebug("the intersectingType Json property was not found in the PerformActionUsage: {Id}", dtoInstance.Id);
                }
                if (!isAbstractSeen)
                {
                    logger.LogDebug("the isAbstract Json property was not found in the PerformActionUsage: {Id}", dtoInstance.Id);
                }
                if (!isCompositeSeen)
                {
                    logger.LogDebug("the isComposite Json property was not found in the PerformActionUsage: {Id}", dtoInstance.Id);
                }
                if (!isConjugatedSeen)
                {
                    logger.LogDebug("the isConjugated Json property was not found in the PerformActionUsage: {Id}", dtoInstance.Id);
                }
                if (!isConstantSeen)
                {
                    logger.LogDebug("the isConstant Json property was not found in the PerformActionUsage: {Id}", dtoInstance.Id);
                }
                if (!isDerivedSeen)
                {
                    logger.LogDebug("the isDerived Json property was not found in the PerformActionUsage: {Id}", dtoInstance.Id);
                }
                if (!isEndSeen)
                {
                    logger.LogDebug("the isEnd Json property was not found in the PerformActionUsage: {Id}", dtoInstance.Id);
                }
                if (!isImpliedIncludedSeen)
                {
                    logger.LogDebug("the isImpliedIncluded Json property was not found in the PerformActionUsage: {Id}", dtoInstance.Id);
                }
                if (!isIndividualSeen)
                {
                    logger.LogDebug("the isIndividual Json property was not found in the PerformActionUsage: {Id}", dtoInstance.Id);
                }
                if (!isLibraryElementSeen)
                {
                    logger.LogDebug("the isLibraryElement Json property was not found in the PerformActionUsage: {Id}", dtoInstance.Id);
                }
                if (!isOrderedSeen)
                {
                    logger.LogDebug("the isOrdered Json property was not found in the PerformActionUsage: {Id}", dtoInstance.Id);
                }
                if (!isPortionSeen)
                {
                    logger.LogDebug("the isPortion Json property was not found in the PerformActionUsage: {Id}", dtoInstance.Id);
                }
                if (!isReferenceSeen)
                {
                    logger.LogDebug("the isReference Json property was not found in the PerformActionUsage: {Id}", dtoInstance.Id);
                }
                if (!isSufficientSeen)
                {
                    logger.LogDebug("the isSufficient Json property was not found in the PerformActionUsage: {Id}", dtoInstance.Id);
                }
                if (!isUniqueSeen)
                {
                    logger.LogDebug("the isUnique Json property was not found in the PerformActionUsage: {Id}", dtoInstance.Id);
                }
                if (!isVariationSeen)
                {
                    logger.LogDebug("the isVariation Json property was not found in the PerformActionUsage: {Id}", dtoInstance.Id);
                }
                if (!mayTimeVarySeen)
                {
                    logger.LogDebug("the mayTimeVary Json property was not found in the PerformActionUsage: {Id}", dtoInstance.Id);
                }
                if (!memberSeen)
                {
                    logger.LogDebug("the member Json property was not found in the PerformActionUsage: {Id}", dtoInstance.Id);
                }
                if (!membershipSeen)
                {
                    logger.LogDebug("the membership Json property was not found in the PerformActionUsage: {Id}", dtoInstance.Id);
                }
                if (!multiplicitySeen)
                {
                    logger.LogDebug("the multiplicity Json property was not found in the PerformActionUsage: {Id}", dtoInstance.Id);
                }
                if (!nameSeen)
                {
                    logger.LogDebug("the name Json property was not found in the PerformActionUsage: {Id}", dtoInstance.Id);
                }
                if (!nestedActionSeen)
                {
                    logger.LogDebug("the nestedAction Json property was not found in the PerformActionUsage: {Id}", dtoInstance.Id);
                }
                if (!nestedAllocationSeen)
                {
                    logger.LogDebug("the nestedAllocation Json property was not found in the PerformActionUsage: {Id}", dtoInstance.Id);
                }
                if (!nestedAnalysisCaseSeen)
                {
                    logger.LogDebug("the nestedAnalysisCase Json property was not found in the PerformActionUsage: {Id}", dtoInstance.Id);
                }
                if (!nestedAttributeSeen)
                {
                    logger.LogDebug("the nestedAttribute Json property was not found in the PerformActionUsage: {Id}", dtoInstance.Id);
                }
                if (!nestedCalculationSeen)
                {
                    logger.LogDebug("the nestedCalculation Json property was not found in the PerformActionUsage: {Id}", dtoInstance.Id);
                }
                if (!nestedCaseSeen)
                {
                    logger.LogDebug("the nestedCase Json property was not found in the PerformActionUsage: {Id}", dtoInstance.Id);
                }
                if (!nestedConcernSeen)
                {
                    logger.LogDebug("the nestedConcern Json property was not found in the PerformActionUsage: {Id}", dtoInstance.Id);
                }
                if (!nestedConnectionSeen)
                {
                    logger.LogDebug("the nestedConnection Json property was not found in the PerformActionUsage: {Id}", dtoInstance.Id);
                }
                if (!nestedConstraintSeen)
                {
                    logger.LogDebug("the nestedConstraint Json property was not found in the PerformActionUsage: {Id}", dtoInstance.Id);
                }
                if (!nestedEnumerationSeen)
                {
                    logger.LogDebug("the nestedEnumeration Json property was not found in the PerformActionUsage: {Id}", dtoInstance.Id);
                }
                if (!nestedFlowSeen)
                {
                    logger.LogDebug("the nestedFlow Json property was not found in the PerformActionUsage: {Id}", dtoInstance.Id);
                }
                if (!nestedInterfaceSeen)
                {
                    logger.LogDebug("the nestedInterface Json property was not found in the PerformActionUsage: {Id}", dtoInstance.Id);
                }
                if (!nestedItemSeen)
                {
                    logger.LogDebug("the nestedItem Json property was not found in the PerformActionUsage: {Id}", dtoInstance.Id);
                }
                if (!nestedMetadataSeen)
                {
                    logger.LogDebug("the nestedMetadata Json property was not found in the PerformActionUsage: {Id}", dtoInstance.Id);
                }
                if (!nestedOccurrenceSeen)
                {
                    logger.LogDebug("the nestedOccurrence Json property was not found in the PerformActionUsage: {Id}", dtoInstance.Id);
                }
                if (!nestedPartSeen)
                {
                    logger.LogDebug("the nestedPart Json property was not found in the PerformActionUsage: {Id}", dtoInstance.Id);
                }
                if (!nestedPortSeen)
                {
                    logger.LogDebug("the nestedPort Json property was not found in the PerformActionUsage: {Id}", dtoInstance.Id);
                }
                if (!nestedReferenceSeen)
                {
                    logger.LogDebug("the nestedReference Json property was not found in the PerformActionUsage: {Id}", dtoInstance.Id);
                }
                if (!nestedRenderingSeen)
                {
                    logger.LogDebug("the nestedRendering Json property was not found in the PerformActionUsage: {Id}", dtoInstance.Id);
                }
                if (!nestedRequirementSeen)
                {
                    logger.LogDebug("the nestedRequirement Json property was not found in the PerformActionUsage: {Id}", dtoInstance.Id);
                }
                if (!nestedStateSeen)
                {
                    logger.LogDebug("the nestedState Json property was not found in the PerformActionUsage: {Id}", dtoInstance.Id);
                }
                if (!nestedTransitionSeen)
                {
                    logger.LogDebug("the nestedTransition Json property was not found in the PerformActionUsage: {Id}", dtoInstance.Id);
                }
                if (!nestedUsageSeen)
                {
                    logger.LogDebug("the nestedUsage Json property was not found in the PerformActionUsage: {Id}", dtoInstance.Id);
                }
                if (!nestedUseCaseSeen)
                {
                    logger.LogDebug("the nestedUseCase Json property was not found in the PerformActionUsage: {Id}", dtoInstance.Id);
                }
                if (!nestedVerificationCaseSeen)
                {
                    logger.LogDebug("the nestedVerificationCase Json property was not found in the PerformActionUsage: {Id}", dtoInstance.Id);
                }
                if (!nestedViewSeen)
                {
                    logger.LogDebug("the nestedView Json property was not found in the PerformActionUsage: {Id}", dtoInstance.Id);
                }
                if (!nestedViewpointSeen)
                {
                    logger.LogDebug("the nestedViewpoint Json property was not found in the PerformActionUsage: {Id}", dtoInstance.Id);
                }
                if (!outputSeen)
                {
                    logger.LogDebug("the output Json property was not found in the PerformActionUsage: {Id}", dtoInstance.Id);
                }
                if (!ownedAnnotationSeen)
                {
                    logger.LogDebug("the ownedAnnotation Json property was not found in the PerformActionUsage: {Id}", dtoInstance.Id);
                }
                if (!ownedConjugatorSeen)
                {
                    logger.LogDebug("the ownedConjugator Json property was not found in the PerformActionUsage: {Id}", dtoInstance.Id);
                }
                if (!ownedCrossSubsettingSeen)
                {
                    logger.LogDebug("the ownedCrossSubsetting Json property was not found in the PerformActionUsage: {Id}", dtoInstance.Id);
                }
                if (!ownedDifferencingSeen)
                {
                    logger.LogDebug("the ownedDifferencing Json property was not found in the PerformActionUsage: {Id}", dtoInstance.Id);
                }
                if (!ownedDisjoiningSeen)
                {
                    logger.LogDebug("the ownedDisjoining Json property was not found in the PerformActionUsage: {Id}", dtoInstance.Id);
                }
                if (!ownedElementSeen)
                {
                    logger.LogDebug("the ownedElement Json property was not found in the PerformActionUsage: {Id}", dtoInstance.Id);
                }
                if (!ownedEndFeatureSeen)
                {
                    logger.LogDebug("the ownedEndFeature Json property was not found in the PerformActionUsage: {Id}", dtoInstance.Id);
                }
                if (!ownedFeatureSeen)
                {
                    logger.LogDebug("the ownedFeature Json property was not found in the PerformActionUsage: {Id}", dtoInstance.Id);
                }
                if (!ownedFeatureChainingSeen)
                {
                    logger.LogDebug("the ownedFeatureChaining Json property was not found in the PerformActionUsage: {Id}", dtoInstance.Id);
                }
                if (!ownedFeatureInvertingSeen)
                {
                    logger.LogDebug("the ownedFeatureInverting Json property was not found in the PerformActionUsage: {Id}", dtoInstance.Id);
                }
                if (!ownedFeatureMembershipSeen)
                {
                    logger.LogDebug("the ownedFeatureMembership Json property was not found in the PerformActionUsage: {Id}", dtoInstance.Id);
                }
                if (!ownedImportSeen)
                {
                    logger.LogDebug("the ownedImport Json property was not found in the PerformActionUsage: {Id}", dtoInstance.Id);
                }
                if (!ownedIntersectingSeen)
                {
                    logger.LogDebug("the ownedIntersecting Json property was not found in the PerformActionUsage: {Id}", dtoInstance.Id);
                }
                if (!ownedMemberSeen)
                {
                    logger.LogDebug("the ownedMember Json property was not found in the PerformActionUsage: {Id}", dtoInstance.Id);
                }
                if (!ownedMembershipSeen)
                {
                    logger.LogDebug("the ownedMembership Json property was not found in the PerformActionUsage: {Id}", dtoInstance.Id);
                }
                if (!ownedRedefinitionSeen)
                {
                    logger.LogDebug("the ownedRedefinition Json property was not found in the PerformActionUsage: {Id}", dtoInstance.Id);
                }
                if (!ownedReferenceSubsettingSeen)
                {
                    logger.LogDebug("the ownedReferenceSubsetting Json property was not found in the PerformActionUsage: {Id}", dtoInstance.Id);
                }
                if (!ownedRelationshipSeen)
                {
                    logger.LogDebug("the ownedRelationship Json property was not found in the PerformActionUsage: {Id}", dtoInstance.Id);
                }
                if (!ownedSpecializationSeen)
                {
                    logger.LogDebug("the ownedSpecialization Json property was not found in the PerformActionUsage: {Id}", dtoInstance.Id);
                }
                if (!ownedSubsettingSeen)
                {
                    logger.LogDebug("the ownedSubsetting Json property was not found in the PerformActionUsage: {Id}", dtoInstance.Id);
                }
                if (!ownedTypeFeaturingSeen)
                {
                    logger.LogDebug("the ownedTypeFeaturing Json property was not found in the PerformActionUsage: {Id}", dtoInstance.Id);
                }
                if (!ownedTypingSeen)
                {
                    logger.LogDebug("the ownedTyping Json property was not found in the PerformActionUsage: {Id}", dtoInstance.Id);
                }
                if (!ownedUnioningSeen)
                {
                    logger.LogDebug("the ownedUnioning Json property was not found in the PerformActionUsage: {Id}", dtoInstance.Id);
                }
                if (!ownerSeen)
                {
                    logger.LogDebug("the owner Json property was not found in the PerformActionUsage: {Id}", dtoInstance.Id);
                }
                if (!owningDefinitionSeen)
                {
                    logger.LogDebug("the owningDefinition Json property was not found in the PerformActionUsage: {Id}", dtoInstance.Id);
                }
                if (!owningFeatureMembershipSeen)
                {
                    logger.LogDebug("the owningFeatureMembership Json property was not found in the PerformActionUsage: {Id}", dtoInstance.Id);
                }
                if (!owningMembershipSeen)
                {
                    logger.LogDebug("the owningMembership Json property was not found in the PerformActionUsage: {Id}", dtoInstance.Id);
                }
                if (!owningNamespaceSeen)
                {
                    logger.LogDebug("the owningNamespace Json property was not found in the PerformActionUsage: {Id}", dtoInstance.Id);
                }
                if (!owningRelationshipSeen)
                {
                    logger.LogDebug("the owningRelationship Json property was not found in the PerformActionUsage: {Id}", dtoInstance.Id);
                }
                if (!owningTypeSeen)
                {
                    logger.LogDebug("the owningType Json property was not found in the PerformActionUsage: {Id}", dtoInstance.Id);
                }
                if (!owningUsageSeen)
                {
                    logger.LogDebug("the owningUsage Json property was not found in the PerformActionUsage: {Id}", dtoInstance.Id);
                }
                if (!parameterSeen)
                {
                    logger.LogDebug("the parameter Json property was not found in the PerformActionUsage: {Id}", dtoInstance.Id);
                }
                if (!performedActionSeen)
                {
                    logger.LogDebug("the performedAction Json property was not found in the PerformActionUsage: {Id}", dtoInstance.Id);
                }
                if (!portionKindSeen)
                {
                    logger.LogDebug("the portionKind Json property was not found in the PerformActionUsage: {Id}", dtoInstance.Id);
                }
                if (!qualifiedNameSeen)
                {
                    logger.LogDebug("the qualifiedName Json property was not found in the PerformActionUsage: {Id}", dtoInstance.Id);
                }
                if (!shortNameSeen)
                {
                    logger.LogDebug("the shortName Json property was not found in the PerformActionUsage: {Id}", dtoInstance.Id);
                }
                if (!textualRepresentationSeen)
                {
                    logger.LogDebug("the textualRepresentation Json property was not found in the PerformActionUsage: {Id}", dtoInstance.Id);
                }
                if (!unioningTypeSeen)
                {
                    logger.LogDebug("the unioningType Json property was not found in the PerformActionUsage: {Id}", dtoInstance.Id);
                }
                if (!usageSeen)
                {
                    logger.LogDebug("the usage Json property was not found in the PerformActionUsage: {Id}", dtoInstance.Id);
                }
                if (!variantSeen)
                {
                    logger.LogDebug("the variant Json property was not found in the PerformActionUsage: {Id}", dtoInstance.Id);
                }
                if (!variantMembershipSeen)
                {
                    logger.LogDebug("the variantMembership Json property was not found in the PerformActionUsage: {Id}", dtoInstance.Id);
                }
            }
        }

        /// <summary>
        /// Deserializes properties of a <see cref="PerformActionUsage" />
        /// from a <see cref="Utf8JsonReader" />, excluding derived properties
        /// </summary>
        /// <param name="dtoInstance">
        /// The <see cref="PerformActionUsage"/> instance holding deserialized values
        /// </param>
        /// <param name="reader">
        /// The <see cref="Utf8JsonReader"/> positioned on the <see cref="JsonTokenType.StartObject"/> of the
        /// <see cref="IPerformActionUsage"/> json object
        /// </param>
        /// <param name="logger">
        /// The <see cref="ILogger"/> to produce logging statement
        /// </param>
        private static void DeserializeDtoExcludingDerivedProperties(SysML2.NET.Core.DTO.Systems.Actions.PerformActionUsage dtoInstance, ref Utf8JsonReader reader, ILogger logger)
        {
            var aliasIdsSeen = false;
            var declaredNameSeen = false;
            var declaredShortNameSeen = false;
            var directionSeen = false;
            var elementIdSeen = false;
            var isAbstractSeen = false;
            var isCompositeSeen = false;
            var isConstantSeen = false;
            var isDerivedSeen = false;
            var isEndSeen = false;
            var isImpliedIncludedSeen = false;
            var isIndividualSeen = false;
            var isOrderedSeen = false;
            var isPortionSeen = false;
            var isSufficientSeen = false;
            var isUniqueSeen = false;
            var isVariationSeen = false;
            var ownedRelationshipSeen = false;
            var owningRelationshipSeen = false;
            var portionKindSeen = false;

            while (reader.Read() && reader.TokenType != JsonTokenType.EndObject)
            {
                if (reader.TokenType != JsonTokenType.PropertyName)
                {
                    throw new JsonException("Expected a property name in the PerformActionUsage json object.");
                }

                if (reader.ValueTextEquals("@id"u8))
                {
                    reader.Read();

                    if (reader.TokenType == JsonTokenType.Null)
                    {
                        throw new JsonException("The @id property is not present, the PerformActionUsage cannot be deserialized");
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

                if (reader.ValueTextEquals("direction"u8))
                {
                    directionSeen = true;
                    reader.Read();

                    dtoInstance.Direction = FeatureDirectionKindDeSerializer.DeserializeNullable(reader.GetString());

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

                if (reader.ValueTextEquals("isComposite"u8))
                {
                    isCompositeSeen = true;
                    reader.Read();

                    if (reader.TokenType != JsonTokenType.Null)
                    {
                        dtoInstance.IsComposite = reader.GetBoolean();
                    }

                    continue;
                }

                if (reader.ValueTextEquals("isConstant"u8))
                {
                    isConstantSeen = true;
                    reader.Read();

                    if (reader.TokenType != JsonTokenType.Null)
                    {
                        dtoInstance.IsConstant = reader.GetBoolean();
                    }

                    continue;
                }

                if (reader.ValueTextEquals("isDerived"u8))
                {
                    isDerivedSeen = true;
                    reader.Read();

                    if (reader.TokenType != JsonTokenType.Null)
                    {
                        dtoInstance.IsDerived = reader.GetBoolean();
                    }

                    continue;
                }

                if (reader.ValueTextEquals("isEnd"u8))
                {
                    isEndSeen = true;
                    reader.Read();

                    if (reader.TokenType != JsonTokenType.Null)
                    {
                        dtoInstance.IsEnd = reader.GetBoolean();
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

                if (reader.ValueTextEquals("isOrdered"u8))
                {
                    isOrderedSeen = true;
                    reader.Read();

                    if (reader.TokenType != JsonTokenType.Null)
                    {
                        dtoInstance.IsOrdered = reader.GetBoolean();
                    }

                    continue;
                }

                if (reader.ValueTextEquals("isPortion"u8))
                {
                    isPortionSeen = true;
                    reader.Read();

                    if (reader.TokenType != JsonTokenType.Null)
                    {
                        dtoInstance.IsPortion = reader.GetBoolean();
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

                if (reader.ValueTextEquals("isUnique"u8))
                {
                    isUniqueSeen = true;
                    reader.Read();

                    if (reader.TokenType != JsonTokenType.Null)
                    {
                        dtoInstance.IsUnique = reader.GetBoolean();
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

                if (reader.ValueTextEquals("portionKind"u8))
                {
                    portionKindSeen = true;
                    reader.Read();

                    dtoInstance.PortionKind = PortionKindDeSerializer.DeserializeNullable(reader.GetString());

                    continue;
                }


                reader.Read();
                reader.Skip();
            }

            if (logger.IsEnabled(LogLevel.Debug))
            {
                if (!aliasIdsSeen)
                {
                    logger.LogDebug("the aliasIds Json property was not found in the PerformActionUsage: {Id}", dtoInstance.Id);
                }
                if (!declaredNameSeen)
                {
                    logger.LogDebug("the declaredName Json property was not found in the PerformActionUsage: {Id}", dtoInstance.Id);
                }
                if (!declaredShortNameSeen)
                {
                    logger.LogDebug("the declaredShortName Json property was not found in the PerformActionUsage: {Id}", dtoInstance.Id);
                }
                if (!directionSeen)
                {
                    logger.LogDebug("the direction Json property was not found in the PerformActionUsage: {Id}", dtoInstance.Id);
                }
                if (!elementIdSeen)
                {
                    logger.LogDebug("the elementId Json property was not found in the PerformActionUsage: {Id}", dtoInstance.Id);
                }
                if (!isAbstractSeen)
                {
                    logger.LogDebug("the isAbstract Json property was not found in the PerformActionUsage: {Id}", dtoInstance.Id);
                }
                if (!isCompositeSeen)
                {
                    logger.LogDebug("the isComposite Json property was not found in the PerformActionUsage: {Id}", dtoInstance.Id);
                }
                if (!isConstantSeen)
                {
                    logger.LogDebug("the isConstant Json property was not found in the PerformActionUsage: {Id}", dtoInstance.Id);
                }
                if (!isDerivedSeen)
                {
                    logger.LogDebug("the isDerived Json property was not found in the PerformActionUsage: {Id}", dtoInstance.Id);
                }
                if (!isEndSeen)
                {
                    logger.LogDebug("the isEnd Json property was not found in the PerformActionUsage: {Id}", dtoInstance.Id);
                }
                if (!isImpliedIncludedSeen)
                {
                    logger.LogDebug("the isImpliedIncluded Json property was not found in the PerformActionUsage: {Id}", dtoInstance.Id);
                }
                if (!isIndividualSeen)
                {
                    logger.LogDebug("the isIndividual Json property was not found in the PerformActionUsage: {Id}", dtoInstance.Id);
                }
                if (!isOrderedSeen)
                {
                    logger.LogDebug("the isOrdered Json property was not found in the PerformActionUsage: {Id}", dtoInstance.Id);
                }
                if (!isPortionSeen)
                {
                    logger.LogDebug("the isPortion Json property was not found in the PerformActionUsage: {Id}", dtoInstance.Id);
                }
                if (!isSufficientSeen)
                {
                    logger.LogDebug("the isSufficient Json property was not found in the PerformActionUsage: {Id}", dtoInstance.Id);
                }
                if (!isUniqueSeen)
                {
                    logger.LogDebug("the isUnique Json property was not found in the PerformActionUsage: {Id}", dtoInstance.Id);
                }
                if (!isVariationSeen)
                {
                    logger.LogDebug("the isVariation Json property was not found in the PerformActionUsage: {Id}", dtoInstance.Id);
                }
                if (!ownedRelationshipSeen)
                {
                    logger.LogDebug("the ownedRelationship Json property was not found in the PerformActionUsage: {Id}", dtoInstance.Id);
                }
                if (!owningRelationshipSeen)
                {
                    logger.LogDebug("the owningRelationship Json property was not found in the PerformActionUsage: {Id}", dtoInstance.Id);
                }
                if (!portionKindSeen)
                {
                    logger.LogDebug("the portionKind Json property was not found in the PerformActionUsage: {Id}", dtoInstance.Id);
                }
            }
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
