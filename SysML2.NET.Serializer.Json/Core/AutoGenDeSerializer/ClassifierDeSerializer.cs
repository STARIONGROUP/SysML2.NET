// -------------------------------------------------------------------------------------------------
// <copyright file="ClassifierDeSerializer.cs" company="Starion Group S.A.">
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
    using SysML2.NET.Core.DTO.Core.Classifiers;
    using SysML2.NET.Serializer.Json;
    using SysML2.NET.Serializer.Json.Utility;

    /// <summary>
    /// The purpose of the <see cref="ClassifierDeSerializer"/> is to provide deserialization capabilities
    /// for the <see cref="IClassifier"/> interface
    /// </summary>
    internal static class ClassifierDeSerializer
    {
        /// <summary>
        /// Deserializes an instance of <see cref="IClassifier"/> from the provided <see cref="Utf8JsonReader"/>
        /// </summary>
        /// <param name="reader">
        /// The <see cref="Utf8JsonReader"/> positioned on the <see cref="JsonTokenType.StartObject"/> of the
        /// <see cref="IClassifier"/> json object. On return the reader is positioned on the matching
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
        /// an instance of <see cref="IClassifier"/>
        /// </returns>
        /// <remarks>
        /// The <c>@type</c> property is the discriminator that the caller dispatched on, so it is skipped rather
        /// than re-validated here
        /// </remarks>
        internal static IClassifier DeSerialize(ref Utf8JsonReader reader, SerializationModeKind serializationModeKind, bool deserializeDerivedProperties, ILoggerFactory loggerFactory = null)
        {
            var logger = loggerFactory == null ? NullLogger.Instance : loggerFactory.CreateLogger("ClassifierDeSerializer");

            Utf8JsonReaderHelper.Expect(ref reader, JsonTokenType.StartObject);

            var dtoInstance = new SysML2.NET.Core.DTO.Core.Classifiers.Classifier();

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
        /// Deserializes properties of a <see cref="Classifier" />
        /// from a <see cref="Utf8JsonReader" />, including derived properties
        /// </summary>
        /// <param name="dtoInstance">
        /// The <see cref="Classifier"/> instance holding deserialized values
        /// </param>
        /// <param name="reader">
        /// The <see cref="Utf8JsonReader"/> positioned on the <see cref="JsonTokenType.StartObject"/> of the
        /// <see cref="IClassifier"/> json object
        /// </param>
        /// <param name="logger">
        /// The <see cref="ILogger"/> to produce logging statement
        /// </param>
        private static void DeserializeDtoIncludingDerivedProperties(SysML2.NET.Core.DTO.Core.Classifiers.Classifier dtoInstance, ref Utf8JsonReader reader, ILogger logger)
        {
            var aliasIdsSeen = false;
            var declaredNameSeen = false;
            var declaredShortNameSeen = false;
            var differencingTypeSeen = false;
            var directedFeatureSeen = false;
            var documentationSeen = false;
            var elementIdSeen = false;
            var endFeatureSeen = false;
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
            var isLibraryElementSeen = false;
            var isSufficientSeen = false;
            var memberSeen = false;
            var membershipSeen = false;
            var multiplicitySeen = false;
            var nameSeen = false;
            var outputSeen = false;
            var ownedAnnotationSeen = false;
            var ownedConjugatorSeen = false;
            var ownedDifferencingSeen = false;
            var ownedDisjoiningSeen = false;
            var ownedElementSeen = false;
            var ownedEndFeatureSeen = false;
            var ownedFeatureSeen = false;
            var ownedFeatureMembershipSeen = false;
            var ownedImportSeen = false;
            var ownedIntersectingSeen = false;
            var ownedMemberSeen = false;
            var ownedMembershipSeen = false;
            var ownedRelationshipSeen = false;
            var ownedSpecializationSeen = false;
            var ownedSubclassificationSeen = false;
            var ownedUnioningSeen = false;
            var ownerSeen = false;
            var owningMembershipSeen = false;
            var owningNamespaceSeen = false;
            var owningRelationshipSeen = false;
            var qualifiedNameSeen = false;
            var shortNameSeen = false;
            var textualRepresentationSeen = false;
            var unioningTypeSeen = false;

            while (reader.Read() && reader.TokenType != JsonTokenType.EndObject)
            {
                if (reader.TokenType != JsonTokenType.PropertyName)
                {
                    throw new JsonException("Expected a property name in the Classifier json object.");
                }

                if (reader.ValueTextEquals("@id"u8))
                {
                    reader.Read();

                    if (reader.TokenType == JsonTokenType.Null)
                    {
                        throw new JsonException("The @id property is not present, the Classifier cannot be deserialized");
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

                if (reader.ValueTextEquals("directedFeature"u8))
                {
                    directedFeatureSeen = true;
                    reader.Read();

                    Utf8JsonReaderHelper.ExpectArrayStart(ref reader);

                    while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
                    {
                        if (Utf8JsonReaderHelper.TryReadReferenceIdentifier(ref reader, out var directedFeatureValue))
                        {
                            dtoInstance.directedFeature.Add(directedFeatureValue);
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


                reader.Read();
                reader.Skip();
            }

            if (logger.IsEnabled(LogLevel.Debug))
            {
                if (!aliasIdsSeen)
                {
                    logger.LogDebug("the aliasIds Json property was not found in the Classifier: {Id}", dtoInstance.Id);
                }
                if (!declaredNameSeen)
                {
                    logger.LogDebug("the declaredName Json property was not found in the Classifier: {Id}", dtoInstance.Id);
                }
                if (!declaredShortNameSeen)
                {
                    logger.LogDebug("the declaredShortName Json property was not found in the Classifier: {Id}", dtoInstance.Id);
                }
                if (!differencingTypeSeen)
                {
                    logger.LogDebug("the differencingType Json property was not found in the Classifier: {Id}", dtoInstance.Id);
                }
                if (!directedFeatureSeen)
                {
                    logger.LogDebug("the directedFeature Json property was not found in the Classifier: {Id}", dtoInstance.Id);
                }
                if (!documentationSeen)
                {
                    logger.LogDebug("the documentation Json property was not found in the Classifier: {Id}", dtoInstance.Id);
                }
                if (!elementIdSeen)
                {
                    logger.LogDebug("the elementId Json property was not found in the Classifier: {Id}", dtoInstance.Id);
                }
                if (!endFeatureSeen)
                {
                    logger.LogDebug("the endFeature Json property was not found in the Classifier: {Id}", dtoInstance.Id);
                }
                if (!featureSeen)
                {
                    logger.LogDebug("the feature Json property was not found in the Classifier: {Id}", dtoInstance.Id);
                }
                if (!featureMembershipSeen)
                {
                    logger.LogDebug("the featureMembership Json property was not found in the Classifier: {Id}", dtoInstance.Id);
                }
                if (!importedMembershipSeen)
                {
                    logger.LogDebug("the importedMembership Json property was not found in the Classifier: {Id}", dtoInstance.Id);
                }
                if (!inheritedFeatureSeen)
                {
                    logger.LogDebug("the inheritedFeature Json property was not found in the Classifier: {Id}", dtoInstance.Id);
                }
                if (!inheritedMembershipSeen)
                {
                    logger.LogDebug("the inheritedMembership Json property was not found in the Classifier: {Id}", dtoInstance.Id);
                }
                if (!inputSeen)
                {
                    logger.LogDebug("the input Json property was not found in the Classifier: {Id}", dtoInstance.Id);
                }
                if (!intersectingTypeSeen)
                {
                    logger.LogDebug("the intersectingType Json property was not found in the Classifier: {Id}", dtoInstance.Id);
                }
                if (!isAbstractSeen)
                {
                    logger.LogDebug("the isAbstract Json property was not found in the Classifier: {Id}", dtoInstance.Id);
                }
                if (!isConjugatedSeen)
                {
                    logger.LogDebug("the isConjugated Json property was not found in the Classifier: {Id}", dtoInstance.Id);
                }
                if (!isImpliedIncludedSeen)
                {
                    logger.LogDebug("the isImpliedIncluded Json property was not found in the Classifier: {Id}", dtoInstance.Id);
                }
                if (!isLibraryElementSeen)
                {
                    logger.LogDebug("the isLibraryElement Json property was not found in the Classifier: {Id}", dtoInstance.Id);
                }
                if (!isSufficientSeen)
                {
                    logger.LogDebug("the isSufficient Json property was not found in the Classifier: {Id}", dtoInstance.Id);
                }
                if (!memberSeen)
                {
                    logger.LogDebug("the member Json property was not found in the Classifier: {Id}", dtoInstance.Id);
                }
                if (!membershipSeen)
                {
                    logger.LogDebug("the membership Json property was not found in the Classifier: {Id}", dtoInstance.Id);
                }
                if (!multiplicitySeen)
                {
                    logger.LogDebug("the multiplicity Json property was not found in the Classifier: {Id}", dtoInstance.Id);
                }
                if (!nameSeen)
                {
                    logger.LogDebug("the name Json property was not found in the Classifier: {Id}", dtoInstance.Id);
                }
                if (!outputSeen)
                {
                    logger.LogDebug("the output Json property was not found in the Classifier: {Id}", dtoInstance.Id);
                }
                if (!ownedAnnotationSeen)
                {
                    logger.LogDebug("the ownedAnnotation Json property was not found in the Classifier: {Id}", dtoInstance.Id);
                }
                if (!ownedConjugatorSeen)
                {
                    logger.LogDebug("the ownedConjugator Json property was not found in the Classifier: {Id}", dtoInstance.Id);
                }
                if (!ownedDifferencingSeen)
                {
                    logger.LogDebug("the ownedDifferencing Json property was not found in the Classifier: {Id}", dtoInstance.Id);
                }
                if (!ownedDisjoiningSeen)
                {
                    logger.LogDebug("the ownedDisjoining Json property was not found in the Classifier: {Id}", dtoInstance.Id);
                }
                if (!ownedElementSeen)
                {
                    logger.LogDebug("the ownedElement Json property was not found in the Classifier: {Id}", dtoInstance.Id);
                }
                if (!ownedEndFeatureSeen)
                {
                    logger.LogDebug("the ownedEndFeature Json property was not found in the Classifier: {Id}", dtoInstance.Id);
                }
                if (!ownedFeatureSeen)
                {
                    logger.LogDebug("the ownedFeature Json property was not found in the Classifier: {Id}", dtoInstance.Id);
                }
                if (!ownedFeatureMembershipSeen)
                {
                    logger.LogDebug("the ownedFeatureMembership Json property was not found in the Classifier: {Id}", dtoInstance.Id);
                }
                if (!ownedImportSeen)
                {
                    logger.LogDebug("the ownedImport Json property was not found in the Classifier: {Id}", dtoInstance.Id);
                }
                if (!ownedIntersectingSeen)
                {
                    logger.LogDebug("the ownedIntersecting Json property was not found in the Classifier: {Id}", dtoInstance.Id);
                }
                if (!ownedMemberSeen)
                {
                    logger.LogDebug("the ownedMember Json property was not found in the Classifier: {Id}", dtoInstance.Id);
                }
                if (!ownedMembershipSeen)
                {
                    logger.LogDebug("the ownedMembership Json property was not found in the Classifier: {Id}", dtoInstance.Id);
                }
                if (!ownedRelationshipSeen)
                {
                    logger.LogDebug("the ownedRelationship Json property was not found in the Classifier: {Id}", dtoInstance.Id);
                }
                if (!ownedSpecializationSeen)
                {
                    logger.LogDebug("the ownedSpecialization Json property was not found in the Classifier: {Id}", dtoInstance.Id);
                }
                if (!ownedSubclassificationSeen)
                {
                    logger.LogDebug("the ownedSubclassification Json property was not found in the Classifier: {Id}", dtoInstance.Id);
                }
                if (!ownedUnioningSeen)
                {
                    logger.LogDebug("the ownedUnioning Json property was not found in the Classifier: {Id}", dtoInstance.Id);
                }
                if (!ownerSeen)
                {
                    logger.LogDebug("the owner Json property was not found in the Classifier: {Id}", dtoInstance.Id);
                }
                if (!owningMembershipSeen)
                {
                    logger.LogDebug("the owningMembership Json property was not found in the Classifier: {Id}", dtoInstance.Id);
                }
                if (!owningNamespaceSeen)
                {
                    logger.LogDebug("the owningNamespace Json property was not found in the Classifier: {Id}", dtoInstance.Id);
                }
                if (!owningRelationshipSeen)
                {
                    logger.LogDebug("the owningRelationship Json property was not found in the Classifier: {Id}", dtoInstance.Id);
                }
                if (!qualifiedNameSeen)
                {
                    logger.LogDebug("the qualifiedName Json property was not found in the Classifier: {Id}", dtoInstance.Id);
                }
                if (!shortNameSeen)
                {
                    logger.LogDebug("the shortName Json property was not found in the Classifier: {Id}", dtoInstance.Id);
                }
                if (!textualRepresentationSeen)
                {
                    logger.LogDebug("the textualRepresentation Json property was not found in the Classifier: {Id}", dtoInstance.Id);
                }
                if (!unioningTypeSeen)
                {
                    logger.LogDebug("the unioningType Json property was not found in the Classifier: {Id}", dtoInstance.Id);
                }
            }
        }

        /// <summary>
        /// Deserializes properties of a <see cref="Classifier" />
        /// from a <see cref="Utf8JsonReader" />, excluding derived properties
        /// </summary>
        /// <param name="dtoInstance">
        /// The <see cref="Classifier"/> instance holding deserialized values
        /// </param>
        /// <param name="reader">
        /// The <see cref="Utf8JsonReader"/> positioned on the <see cref="JsonTokenType.StartObject"/> of the
        /// <see cref="IClassifier"/> json object
        /// </param>
        /// <param name="logger">
        /// The <see cref="ILogger"/> to produce logging statement
        /// </param>
        private static void DeserializeDtoExcludingDerivedProperties(SysML2.NET.Core.DTO.Core.Classifiers.Classifier dtoInstance, ref Utf8JsonReader reader, ILogger logger)
        {
            var aliasIdsSeen = false;
            var declaredNameSeen = false;
            var declaredShortNameSeen = false;
            var elementIdSeen = false;
            var isAbstractSeen = false;
            var isImpliedIncludedSeen = false;
            var isSufficientSeen = false;
            var ownedRelationshipSeen = false;
            var owningRelationshipSeen = false;

            while (reader.Read() && reader.TokenType != JsonTokenType.EndObject)
            {
                if (reader.TokenType != JsonTokenType.PropertyName)
                {
                    throw new JsonException("Expected a property name in the Classifier json object.");
                }

                if (reader.ValueTextEquals("@id"u8))
                {
                    reader.Read();

                    if (reader.TokenType == JsonTokenType.Null)
                    {
                        throw new JsonException("The @id property is not present, the Classifier cannot be deserialized");
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
                    logger.LogDebug("the aliasIds Json property was not found in the Classifier: {Id}", dtoInstance.Id);
                }
                if (!declaredNameSeen)
                {
                    logger.LogDebug("the declaredName Json property was not found in the Classifier: {Id}", dtoInstance.Id);
                }
                if (!declaredShortNameSeen)
                {
                    logger.LogDebug("the declaredShortName Json property was not found in the Classifier: {Id}", dtoInstance.Id);
                }
                if (!elementIdSeen)
                {
                    logger.LogDebug("the elementId Json property was not found in the Classifier: {Id}", dtoInstance.Id);
                }
                if (!isAbstractSeen)
                {
                    logger.LogDebug("the isAbstract Json property was not found in the Classifier: {Id}", dtoInstance.Id);
                }
                if (!isImpliedIncludedSeen)
                {
                    logger.LogDebug("the isImpliedIncluded Json property was not found in the Classifier: {Id}", dtoInstance.Id);
                }
                if (!isSufficientSeen)
                {
                    logger.LogDebug("the isSufficient Json property was not found in the Classifier: {Id}", dtoInstance.Id);
                }
                if (!ownedRelationshipSeen)
                {
                    logger.LogDebug("the ownedRelationship Json property was not found in the Classifier: {Id}", dtoInstance.Id);
                }
                if (!owningRelationshipSeen)
                {
                    logger.LogDebug("the owningRelationship Json property was not found in the Classifier: {Id}", dtoInstance.Id);
                }
            }
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
