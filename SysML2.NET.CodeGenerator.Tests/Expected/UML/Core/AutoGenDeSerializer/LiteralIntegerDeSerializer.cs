// -------------------------------------------------------------------------------------------------
// <copyright file="LiteralIntegerDeSerializer.cs" company="Starion Group S.A.">
//
//   Copyright 2022-2025 Starion Group S.A.
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
    using SysML2.NET.Core.DTO.Kernel.Expressions;
    using SysML2.NET.Serializer.Json;

    /// <summary>
    /// The purpose of the <see cref="LiteralIntegerDeSerializer"/> is to provide deserialization capabilities
    /// for the <see cref="ILiteralInteger"/> interface
    /// </summary>
    internal static class LiteralIntegerDeSerializer
    {
        /// <summary>
        /// Deserializes an instance of <see cref="ILiteralInteger"/> from the provided <see cref="JsonElement"/>
        /// </summary>
        /// <param name="jsonElement">
        /// The <see cref="JsonElement"/> that contains the <see cref="ILiteralInteger"/> json object
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
        /// an instance of <see cref="ILiteralInteger"/>
        /// </returns>
        internal static ILiteralInteger DeSerialize(JsonElement jsonElement, SerializationModeKind serializationModeKind, bool deserializeDerivedProperties, ILoggerFactory loggerFactory = null)
        {
            var logger = loggerFactory == null ? NullLogger.Instance : loggerFactory.CreateLogger("LiteralIntegerDeSerializer");

            if (!jsonElement.TryGetProperty("@type"u8, out var @type))
            {
                throw new InvalidOperationException("The @type property is not available, the LiteralIntegerDeSerializer cannot be used to deserialize this JsonElement");
            }

            if (@type.GetString() != "LiteralInteger")
            {
                throw new InvalidOperationException($"The LiteralIntegerDeSerializer can only be used to deserialize objects of type ILiteralInteger, a {@type.GetString()} was provided");
            }

            var dtoInstance = new SysML2.NET.Core.DTO.Kernel.Expressions.LiteralInteger();

            if (jsonElement.TryGetProperty("@id"u8, out var idProperty))
            {
                var propertyValue = idProperty.GetString();

                if (propertyValue == null)
                {
                    throw new JsonException("The @id property is not present, the LiteralInteger cannot be deserialized");
                }
                else
                {
                    dtoInstance.Id = Guid.Parse(propertyValue);
                }
            }

            if (deserializeDerivedProperties)
            {
                DeserializeDtoIncludingDerivedProperties(dtoInstance, jsonElement, logger);
            }
            else
            {
                DeserializeDtoExcludingDerivedProperties(dtoInstance, jsonElement, logger);
            }

            return dtoInstance;
        }

        /// <summary>
        /// Deserializes properties of a <see cref="LiteralInteger" />
        /// from a <see cref="JsonElement" />, including derived properties
        /// </summary>
        /// <param name="dtoInstance">
        /// The <see cref="LiteralInteger"/> instance holding deserialized values
        /// </param>
        /// <param name="jsonElement">
        /// The <see cref="JsonElement"/> that contains the <see cref="ILiteralInteger"/> json object
        /// </param>
        /// <param name="logger">
        /// The <see cref="ILogger"/> to produce logging statement
        /// </param>
        private static void DeserializeDtoIncludingDerivedProperties(SysML2.NET.Core.DTO.Kernel.Expressions.LiteralInteger dtoInstance, JsonElement jsonElement, ILogger logger)
        {
            if (jsonElement.TryGetProperty("aliasIds"u8, out var aliasIdsProperty))
            {
                foreach (var arrayItem in aliasIdsProperty.EnumerateArray())
                {
                    var propertyValue = arrayItem.GetString();

                    if (propertyValue != null)
                    {
                        dtoInstance.AliasIds.Add(propertyValue);
                    }
                }
            }
            else
            {
                logger.LogDebug("the aliasIds Json property was not found in the LiteralInteger: { Id }", dtoInstance.Id);
            }

            if (jsonElement.TryGetProperty("chainingFeature"u8, out var chainingFeatureProperty))
            {
                foreach (var arrayItem in chainingFeatureProperty.EnumerateArray())
                {
                    if (arrayItem.TryGetProperty("@id"u8, out var chainingFeatureExternalIdProperty))
                    {
                        var propertyValue = chainingFeatureExternalIdProperty.GetString();

                        if (propertyValue != null)
                        {
                            dtoInstance.chainingFeature.Add(Guid.Parse(propertyValue));
                        }
                    }
                }
            }
            else
            {
                logger.LogDebug("the chainingFeature Json property was not found in the LiteralInteger: { Id }", dtoInstance.Id);
            }

            if (jsonElement.TryGetProperty("crossFeature"u8, out var crossFeatureProperty))
            {
                if (crossFeatureProperty.ValueKind == JsonValueKind.Null)
                {
                    dtoInstance.crossFeature = null;
                }
                else
                {
                    if (crossFeatureProperty.TryGetProperty("@id"u8, out var crossFeatureExternalIdProperty))
                    {
                        var propertyValue = crossFeatureExternalIdProperty.GetString();

                        if (propertyValue != null)
                        {
                            dtoInstance.crossFeature = Guid.Parse(propertyValue);
                        }
                    }
                }
            }
            else
            {
                logger.LogDebug("the crossFeature Json property was not found in the LiteralInteger: { Id }", dtoInstance.Id);
            }

            if (jsonElement.TryGetProperty("declaredName"u8, out var declaredNameProperty))
            {
                dtoInstance.DeclaredName = declaredNameProperty.GetString();
            }
            else
            {
                logger.LogDebug("the declaredName Json property was not found in the LiteralInteger: { Id }", dtoInstance.Id);
            }

            if (jsonElement.TryGetProperty("declaredShortName"u8, out var declaredShortNameProperty))
            {
                dtoInstance.DeclaredShortName = declaredShortNameProperty.GetString();
            }
            else
            {
                logger.LogDebug("the declaredShortName Json property was not found in the LiteralInteger: { Id }", dtoInstance.Id);
            }

            if (jsonElement.TryGetProperty("differencingType"u8, out var differencingTypeProperty))
            {
                foreach (var arrayItem in differencingTypeProperty.EnumerateArray())
                {
                    if (arrayItem.TryGetProperty("@id"u8, out var differencingTypeExternalIdProperty))
                    {
                        var propertyValue = differencingTypeExternalIdProperty.GetString();

                        if (propertyValue != null)
                        {
                            dtoInstance.differencingType.Add(Guid.Parse(propertyValue));
                        }
                    }
                }
            }
            else
            {
                logger.LogDebug("the differencingType Json property was not found in the LiteralInteger: { Id }", dtoInstance.Id);
            }

            if (jsonElement.TryGetProperty("direction"u8, out var directionProperty))
            {
                dtoInstance.Direction = FeatureDirectionKindDeSerializer.DeserializeNullable(directionProperty.GetString());
            }
            else
            {
                logger.LogDebug("the direction Json property was not found in the LiteralInteger: { Id }", dtoInstance.Id);
            }

            if (jsonElement.TryGetProperty("documentation"u8, out var documentationProperty))
            {
                foreach (var arrayItem in documentationProperty.EnumerateArray())
                {
                    if (arrayItem.TryGetProperty("@id"u8, out var documentationExternalIdProperty))
                    {
                        var propertyValue = documentationExternalIdProperty.GetString();

                        if (propertyValue != null)
                        {
                            dtoInstance.documentation.Add(Guid.Parse(propertyValue));
                        }
                    }
                }
            }
            else
            {
                logger.LogDebug("the documentation Json property was not found in the LiteralInteger: { Id }", dtoInstance.Id);
            }

            if (jsonElement.TryGetProperty("elementId"u8, out var elementIdProperty))
            {
                var propertyValue = elementIdProperty.GetString();

                if (propertyValue != null)
                {
                    dtoInstance.ElementId = propertyValue;
                }
            }
            else
            {
                logger.LogDebug("the elementId Json property was not found in the LiteralInteger: { Id }", dtoInstance.Id);
            }

            if (jsonElement.TryGetProperty("endFeature"u8, out var endFeatureProperty))
            {
                foreach (var arrayItem in endFeatureProperty.EnumerateArray())
                {
                    if (arrayItem.TryGetProperty("@id"u8, out var endFeatureExternalIdProperty))
                    {
                        var propertyValue = endFeatureExternalIdProperty.GetString();

                        if (propertyValue != null)
                        {
                            dtoInstance.endFeature.Add(Guid.Parse(propertyValue));
                        }
                    }
                }
            }
            else
            {
                logger.LogDebug("the endFeature Json property was not found in the LiteralInteger: { Id }", dtoInstance.Id);
            }

            if (jsonElement.TryGetProperty("endOwningType"u8, out var endOwningTypeProperty))
            {
                if (endOwningTypeProperty.ValueKind == JsonValueKind.Null)
                {
                    dtoInstance.endOwningType = null;
                }
                else
                {
                    if (endOwningTypeProperty.TryGetProperty("@id"u8, out var endOwningTypeExternalIdProperty))
                    {
                        var propertyValue = endOwningTypeExternalIdProperty.GetString();

                        if (propertyValue != null)
                        {
                            dtoInstance.endOwningType = Guid.Parse(propertyValue);
                        }
                    }
                }
            }
            else
            {
                logger.LogDebug("the endOwningType Json property was not found in the LiteralInteger: { Id }", dtoInstance.Id);
            }

            if (jsonElement.TryGetProperty("feature"u8, out var featureProperty))
            {
                foreach (var arrayItem in featureProperty.EnumerateArray())
                {
                    if (arrayItem.TryGetProperty("@id"u8, out var featureExternalIdProperty))
                    {
                        var propertyValue = featureExternalIdProperty.GetString();

                        if (propertyValue != null)
                        {
                            dtoInstance.feature.Add(Guid.Parse(propertyValue));
                        }
                    }
                }
            }
            else
            {
                logger.LogDebug("the feature Json property was not found in the LiteralInteger: { Id }", dtoInstance.Id);
            }

            if (jsonElement.TryGetProperty("featureMembership"u8, out var featureMembershipProperty))
            {
                foreach (var arrayItem in featureMembershipProperty.EnumerateArray())
                {
                    if (arrayItem.TryGetProperty("@id"u8, out var featureMembershipExternalIdProperty))
                    {
                        var propertyValue = featureMembershipExternalIdProperty.GetString();

                        if (propertyValue != null)
                        {
                            dtoInstance.featureMembership.Add(Guid.Parse(propertyValue));
                        }
                    }
                }
            }
            else
            {
                logger.LogDebug("the featureMembership Json property was not found in the LiteralInteger: { Id }", dtoInstance.Id);
            }

            if (jsonElement.TryGetProperty("featureTarget"u8, out var featureTargetProperty))
            {
                if (featureTargetProperty.ValueKind == JsonValueKind.Null)
                {
                    dtoInstance.featureTarget = Guid.Empty;
                    logger.LogDebug($"the LiteralInteger.featureTarget property was not found in the Json. The value is set to Guid.Empty");
                }
                else
                {
                    if (featureTargetProperty.TryGetProperty("@id"u8, out var featureTargetExternalIdProperty))
                    {
                        var propertyValue = featureTargetExternalIdProperty.GetString();

                        if (propertyValue != null)
                        {
                            dtoInstance.featureTarget = Guid.Parse(propertyValue);
                        }
                    }
                }
            }
            else
            {
                logger.LogDebug("the featureTarget Json property was not found in the LiteralInteger: { Id }", dtoInstance.Id);
            }

            if (jsonElement.TryGetProperty("featuringType"u8, out var featuringTypeProperty))
            {
                foreach (var arrayItem in featuringTypeProperty.EnumerateArray())
                {
                    if (arrayItem.TryGetProperty("@id"u8, out var featuringTypeExternalIdProperty))
                    {
                        var propertyValue = featuringTypeExternalIdProperty.GetString();

                        if (propertyValue != null)
                        {
                            dtoInstance.featuringType.Add(Guid.Parse(propertyValue));
                        }
                    }
                }
            }
            else
            {
                logger.LogDebug("the featuringType Json property was not found in the LiteralInteger: { Id }", dtoInstance.Id);
            }

            if (jsonElement.TryGetProperty("function"u8, out var functionProperty))
            {
                if (functionProperty.ValueKind == JsonValueKind.Null)
                {
                    dtoInstance.function = null;
                }
                else
                {
                    if (functionProperty.TryGetProperty("@id"u8, out var functionExternalIdProperty))
                    {
                        var propertyValue = functionExternalIdProperty.GetString();

                        if (propertyValue != null)
                        {
                            dtoInstance.function = Guid.Parse(propertyValue);
                        }
                    }
                }
            }
            else
            {
                logger.LogDebug("the function Json property was not found in the LiteralInteger: { Id }", dtoInstance.Id);
            }

            if (jsonElement.TryGetProperty("importedMembership"u8, out var importedMembershipProperty))
            {
                foreach (var arrayItem in importedMembershipProperty.EnumerateArray())
                {
                    if (arrayItem.TryGetProperty("@id"u8, out var importedMembershipExternalIdProperty))
                    {
                        var propertyValue = importedMembershipExternalIdProperty.GetString();

                        if (propertyValue != null)
                        {
                            dtoInstance.importedMembership.Add(Guid.Parse(propertyValue));
                        }
                    }
                }
            }
            else
            {
                logger.LogDebug("the importedMembership Json property was not found in the LiteralInteger: { Id }", dtoInstance.Id);
            }

            if (jsonElement.TryGetProperty("inheritedFeature"u8, out var inheritedFeatureProperty))
            {
                foreach (var arrayItem in inheritedFeatureProperty.EnumerateArray())
                {
                    if (arrayItem.TryGetProperty("@id"u8, out var inheritedFeatureExternalIdProperty))
                    {
                        var propertyValue = inheritedFeatureExternalIdProperty.GetString();

                        if (propertyValue != null)
                        {
                            dtoInstance.inheritedFeature.Add(Guid.Parse(propertyValue));
                        }
                    }
                }
            }
            else
            {
                logger.LogDebug("the inheritedFeature Json property was not found in the LiteralInteger: { Id }", dtoInstance.Id);
            }

            if (jsonElement.TryGetProperty("inheritedMembership"u8, out var inheritedMembershipProperty))
            {
                foreach (var arrayItem in inheritedMembershipProperty.EnumerateArray())
                {
                    if (arrayItem.TryGetProperty("@id"u8, out var inheritedMembershipExternalIdProperty))
                    {
                        var propertyValue = inheritedMembershipExternalIdProperty.GetString();

                        if (propertyValue != null)
                        {
                            dtoInstance.inheritedMembership.Add(Guid.Parse(propertyValue));
                        }
                    }
                }
            }
            else
            {
                logger.LogDebug("the inheritedMembership Json property was not found in the LiteralInteger: { Id }", dtoInstance.Id);
            }

            if (jsonElement.TryGetProperty("input"u8, out var inputProperty))
            {
                foreach (var arrayItem in inputProperty.EnumerateArray())
                {
                    if (arrayItem.TryGetProperty("@id"u8, out var inputExternalIdProperty))
                    {
                        var propertyValue = inputExternalIdProperty.GetString();

                        if (propertyValue != null)
                        {
                            dtoInstance.input.Add(Guid.Parse(propertyValue));
                        }
                    }
                }
            }
            else
            {
                logger.LogDebug("the input Json property was not found in the LiteralInteger: { Id }", dtoInstance.Id);
            }

            if (jsonElement.TryGetProperty("intersectingType"u8, out var intersectingTypeProperty))
            {
                foreach (var arrayItem in intersectingTypeProperty.EnumerateArray())
                {
                    if (arrayItem.TryGetProperty("@id"u8, out var intersectingTypeExternalIdProperty))
                    {
                        var propertyValue = intersectingTypeExternalIdProperty.GetString();

                        if (propertyValue != null)
                        {
                            dtoInstance.intersectingType.Add(Guid.Parse(propertyValue));
                        }
                    }
                }
            }
            else
            {
                logger.LogDebug("the intersectingType Json property was not found in the LiteralInteger: { Id }", dtoInstance.Id);
            }

            if (jsonElement.TryGetProperty("isAbstract"u8, out var isAbstractProperty))
            {
                if (isAbstractProperty.ValueKind != JsonValueKind.Null)
                {
                    dtoInstance.IsAbstract = isAbstractProperty.GetBoolean();
                }
            }
            else
            {
                logger.LogDebug("the isAbstract Json property was not found in the LiteralInteger: { Id }", dtoInstance.Id);
            }

            if (jsonElement.TryGetProperty("isComposite"u8, out var isCompositeProperty))
            {
                if (isCompositeProperty.ValueKind != JsonValueKind.Null)
                {
                    dtoInstance.IsComposite = isCompositeProperty.GetBoolean();
                }
            }
            else
            {
                logger.LogDebug("the isComposite Json property was not found in the LiteralInteger: { Id }", dtoInstance.Id);
            }

            if (jsonElement.TryGetProperty("isConjugated"u8, out var isConjugatedProperty))
            {
                if (isConjugatedProperty.ValueKind != JsonValueKind.Null)
                {
                    dtoInstance.isConjugated = isConjugatedProperty.GetBoolean();
                }
            }
            else
            {
                logger.LogDebug("the isConjugated Json property was not found in the LiteralInteger: { Id }", dtoInstance.Id);
            }

            if (jsonElement.TryGetProperty("isConstant"u8, out var isConstantProperty))
            {
                if (isConstantProperty.ValueKind != JsonValueKind.Null)
                {
                    dtoInstance.IsConstant = isConstantProperty.GetBoolean();
                }
            }
            else
            {
                logger.LogDebug("the isConstant Json property was not found in the LiteralInteger: { Id }", dtoInstance.Id);
            }

            if (jsonElement.TryGetProperty("isDerived"u8, out var isDerivedProperty))
            {
                if (isDerivedProperty.ValueKind != JsonValueKind.Null)
                {
                    dtoInstance.IsDerived = isDerivedProperty.GetBoolean();
                }
            }
            else
            {
                logger.LogDebug("the isDerived Json property was not found in the LiteralInteger: { Id }", dtoInstance.Id);
            }

            if (jsonElement.TryGetProperty("isEnd"u8, out var isEndProperty))
            {
                if (isEndProperty.ValueKind != JsonValueKind.Null)
                {
                    dtoInstance.IsEnd = isEndProperty.GetBoolean();
                }
            }
            else
            {
                logger.LogDebug("the isEnd Json property was not found in the LiteralInteger: { Id }", dtoInstance.Id);
            }

            if (jsonElement.TryGetProperty("isImpliedIncluded"u8, out var isImpliedIncludedProperty))
            {
                if (isImpliedIncludedProperty.ValueKind != JsonValueKind.Null)
                {
                    dtoInstance.IsImpliedIncluded = isImpliedIncludedProperty.GetBoolean();
                }
            }
            else
            {
                logger.LogDebug("the isImpliedIncluded Json property was not found in the LiteralInteger: { Id }", dtoInstance.Id);
            }

            if (jsonElement.TryGetProperty("isLibraryElement"u8, out var isLibraryElementProperty))
            {
                if (isLibraryElementProperty.ValueKind != JsonValueKind.Null)
                {
                    dtoInstance.isLibraryElement = isLibraryElementProperty.GetBoolean();
                }
            }
            else
            {
                logger.LogDebug("the isLibraryElement Json property was not found in the LiteralInteger: { Id }", dtoInstance.Id);
            }

            if (jsonElement.TryGetProperty("isModelLevelEvaluable"u8, out var isModelLevelEvaluableProperty))
            {
                if (isModelLevelEvaluableProperty.ValueKind != JsonValueKind.Null)
                {
                    dtoInstance.isModelLevelEvaluable = isModelLevelEvaluableProperty.GetBoolean();
                }
            }
            else
            {
                logger.LogDebug("the isModelLevelEvaluable Json property was not found in the LiteralInteger: { Id }", dtoInstance.Id);
            }

            if (jsonElement.TryGetProperty("isOrdered"u8, out var isOrderedProperty))
            {
                if (isOrderedProperty.ValueKind != JsonValueKind.Null)
                {
                    dtoInstance.IsOrdered = isOrderedProperty.GetBoolean();
                }
            }
            else
            {
                logger.LogDebug("the isOrdered Json property was not found in the LiteralInteger: { Id }", dtoInstance.Id);
            }

            if (jsonElement.TryGetProperty("isPortion"u8, out var isPortionProperty))
            {
                if (isPortionProperty.ValueKind != JsonValueKind.Null)
                {
                    dtoInstance.IsPortion = isPortionProperty.GetBoolean();
                }
            }
            else
            {
                logger.LogDebug("the isPortion Json property was not found in the LiteralInteger: { Id }", dtoInstance.Id);
            }

            if (jsonElement.TryGetProperty("isSufficient"u8, out var isSufficientProperty))
            {
                if (isSufficientProperty.ValueKind != JsonValueKind.Null)
                {
                    dtoInstance.IsSufficient = isSufficientProperty.GetBoolean();
                }
            }
            else
            {
                logger.LogDebug("the isSufficient Json property was not found in the LiteralInteger: { Id }", dtoInstance.Id);
            }

            if (jsonElement.TryGetProperty("isUnique"u8, out var isUniqueProperty))
            {
                if (isUniqueProperty.ValueKind != JsonValueKind.Null)
                {
                    dtoInstance.IsUnique = isUniqueProperty.GetBoolean();
                }
            }
            else
            {
                logger.LogDebug("the isUnique Json property was not found in the LiteralInteger: { Id }", dtoInstance.Id);
            }

            if (jsonElement.TryGetProperty("isVariable"u8, out var isVariableProperty))
            {
                if (isVariableProperty.ValueKind != JsonValueKind.Null)
                {
                    dtoInstance.IsVariable = isVariableProperty.GetBoolean();
                }
            }
            else
            {
                logger.LogDebug("the isVariable Json property was not found in the LiteralInteger: { Id }", dtoInstance.Id);
            }

            if (jsonElement.TryGetProperty("member"u8, out var memberProperty))
            {
                foreach (var arrayItem in memberProperty.EnumerateArray())
                {
                    if (arrayItem.TryGetProperty("@id"u8, out var memberExternalIdProperty))
                    {
                        var propertyValue = memberExternalIdProperty.GetString();

                        if (propertyValue != null)
                        {
                            dtoInstance.member.Add(Guid.Parse(propertyValue));
                        }
                    }
                }
            }
            else
            {
                logger.LogDebug("the member Json property was not found in the LiteralInteger: { Id }", dtoInstance.Id);
            }

            if (jsonElement.TryGetProperty("membership"u8, out var membershipProperty))
            {
                foreach (var arrayItem in membershipProperty.EnumerateArray())
                {
                    if (arrayItem.TryGetProperty("@id"u8, out var membershipExternalIdProperty))
                    {
                        var propertyValue = membershipExternalIdProperty.GetString();

                        if (propertyValue != null)
                        {
                            dtoInstance.membership.Add(Guid.Parse(propertyValue));
                        }
                    }
                }
            }
            else
            {
                logger.LogDebug("the membership Json property was not found in the LiteralInteger: { Id }", dtoInstance.Id);
            }

            if (jsonElement.TryGetProperty("multiplicity"u8, out var multiplicityProperty))
            {
                if (multiplicityProperty.ValueKind == JsonValueKind.Null)
                {
                    dtoInstance.multiplicity = null;
                }
                else
                {
                    if (multiplicityProperty.TryGetProperty("@id"u8, out var multiplicityExternalIdProperty))
                    {
                        var propertyValue = multiplicityExternalIdProperty.GetString();

                        if (propertyValue != null)
                        {
                            dtoInstance.multiplicity = Guid.Parse(propertyValue);
                        }
                    }
                }
            }
            else
            {
                logger.LogDebug("the multiplicity Json property was not found in the LiteralInteger: { Id }", dtoInstance.Id);
            }

            if (jsonElement.TryGetProperty("name"u8, out var nameProperty))
            {
                dtoInstance.name = nameProperty.GetString();
            }
            else
            {
                logger.LogDebug("the name Json property was not found in the LiteralInteger: { Id }", dtoInstance.Id);
            }

            if (jsonElement.TryGetProperty("output"u8, out var outputProperty))
            {
                foreach (var arrayItem in outputProperty.EnumerateArray())
                {
                    if (arrayItem.TryGetProperty("@id"u8, out var outputExternalIdProperty))
                    {
                        var propertyValue = outputExternalIdProperty.GetString();

                        if (propertyValue != null)
                        {
                            dtoInstance.output.Add(Guid.Parse(propertyValue));
                        }
                    }
                }
            }
            else
            {
                logger.LogDebug("the output Json property was not found in the LiteralInteger: { Id }", dtoInstance.Id);
            }

            if (jsonElement.TryGetProperty("ownedAnnotation"u8, out var ownedAnnotationProperty))
            {
                foreach (var arrayItem in ownedAnnotationProperty.EnumerateArray())
                {
                    if (arrayItem.TryGetProperty("@id"u8, out var ownedAnnotationExternalIdProperty))
                    {
                        var propertyValue = ownedAnnotationExternalIdProperty.GetString();

                        if (propertyValue != null)
                        {
                            dtoInstance.ownedAnnotation.Add(Guid.Parse(propertyValue));
                        }
                    }
                }
            }
            else
            {
                logger.LogDebug("the ownedAnnotation Json property was not found in the LiteralInteger: { Id }", dtoInstance.Id);
            }

            if (jsonElement.TryGetProperty("ownedConjugator"u8, out var ownedConjugatorProperty))
            {
                if (ownedConjugatorProperty.ValueKind == JsonValueKind.Null)
                {
                    dtoInstance.ownedConjugator = null;
                }
                else
                {
                    if (ownedConjugatorProperty.TryGetProperty("@id"u8, out var ownedConjugatorExternalIdProperty))
                    {
                        var propertyValue = ownedConjugatorExternalIdProperty.GetString();

                        if (propertyValue != null)
                        {
                            dtoInstance.ownedConjugator = Guid.Parse(propertyValue);
                        }
                    }
                }
            }
            else
            {
                logger.LogDebug("the ownedConjugator Json property was not found in the LiteralInteger: { Id }", dtoInstance.Id);
            }

            if (jsonElement.TryGetProperty("ownedCrossSubsetting"u8, out var ownedCrossSubsettingProperty))
            {
                if (ownedCrossSubsettingProperty.ValueKind == JsonValueKind.Null)
                {
                    dtoInstance.ownedCrossSubsetting = null;
                }
                else
                {
                    if (ownedCrossSubsettingProperty.TryGetProperty("@id"u8, out var ownedCrossSubsettingExternalIdProperty))
                    {
                        var propertyValue = ownedCrossSubsettingExternalIdProperty.GetString();

                        if (propertyValue != null)
                        {
                            dtoInstance.ownedCrossSubsetting = Guid.Parse(propertyValue);
                        }
                    }
                }
            }
            else
            {
                logger.LogDebug("the ownedCrossSubsetting Json property was not found in the LiteralInteger: { Id }", dtoInstance.Id);
            }

            if (jsonElement.TryGetProperty("ownedDifferencing"u8, out var ownedDifferencingProperty))
            {
                foreach (var arrayItem in ownedDifferencingProperty.EnumerateArray())
                {
                    if (arrayItem.TryGetProperty("@id"u8, out var ownedDifferencingExternalIdProperty))
                    {
                        var propertyValue = ownedDifferencingExternalIdProperty.GetString();

                        if (propertyValue != null)
                        {
                            dtoInstance.ownedDifferencing.Add(Guid.Parse(propertyValue));
                        }
                    }
                }
            }
            else
            {
                logger.LogDebug("the ownedDifferencing Json property was not found in the LiteralInteger: { Id }", dtoInstance.Id);
            }

            if (jsonElement.TryGetProperty("ownedDisjoining"u8, out var ownedDisjoiningProperty))
            {
                foreach (var arrayItem in ownedDisjoiningProperty.EnumerateArray())
                {
                    if (arrayItem.TryGetProperty("@id"u8, out var ownedDisjoiningExternalIdProperty))
                    {
                        var propertyValue = ownedDisjoiningExternalIdProperty.GetString();

                        if (propertyValue != null)
                        {
                            dtoInstance.ownedDisjoining.Add(Guid.Parse(propertyValue));
                        }
                    }
                }
            }
            else
            {
                logger.LogDebug("the ownedDisjoining Json property was not found in the LiteralInteger: { Id }", dtoInstance.Id);
            }

            if (jsonElement.TryGetProperty("ownedElement"u8, out var ownedElementProperty))
            {
                foreach (var arrayItem in ownedElementProperty.EnumerateArray())
                {
                    if (arrayItem.TryGetProperty("@id"u8, out var ownedElementExternalIdProperty))
                    {
                        var propertyValue = ownedElementExternalIdProperty.GetString();

                        if (propertyValue != null)
                        {
                            dtoInstance.ownedElement.Add(Guid.Parse(propertyValue));
                        }
                    }
                }
            }
            else
            {
                logger.LogDebug("the ownedElement Json property was not found in the LiteralInteger: { Id }", dtoInstance.Id);
            }

            if (jsonElement.TryGetProperty("ownedEndFeature"u8, out var ownedEndFeatureProperty))
            {
                foreach (var arrayItem in ownedEndFeatureProperty.EnumerateArray())
                {
                    if (arrayItem.TryGetProperty("@id"u8, out var ownedEndFeatureExternalIdProperty))
                    {
                        var propertyValue = ownedEndFeatureExternalIdProperty.GetString();

                        if (propertyValue != null)
                        {
                            dtoInstance.ownedEndFeature.Add(Guid.Parse(propertyValue));
                        }
                    }
                }
            }
            else
            {
                logger.LogDebug("the ownedEndFeature Json property was not found in the LiteralInteger: { Id }", dtoInstance.Id);
            }

            if (jsonElement.TryGetProperty("ownedFeature"u8, out var ownedFeatureProperty))
            {
                foreach (var arrayItem in ownedFeatureProperty.EnumerateArray())
                {
                    if (arrayItem.TryGetProperty("@id"u8, out var ownedFeatureExternalIdProperty))
                    {
                        var propertyValue = ownedFeatureExternalIdProperty.GetString();

                        if (propertyValue != null)
                        {
                            dtoInstance.ownedFeature.Add(Guid.Parse(propertyValue));
                        }
                    }
                }
            }
            else
            {
                logger.LogDebug("the ownedFeature Json property was not found in the LiteralInteger: { Id }", dtoInstance.Id);
            }

            if (jsonElement.TryGetProperty("ownedFeatureChaining"u8, out var ownedFeatureChainingProperty))
            {
                foreach (var arrayItem in ownedFeatureChainingProperty.EnumerateArray())
                {
                    if (arrayItem.TryGetProperty("@id"u8, out var ownedFeatureChainingExternalIdProperty))
                    {
                        var propertyValue = ownedFeatureChainingExternalIdProperty.GetString();

                        if (propertyValue != null)
                        {
                            dtoInstance.ownedFeatureChaining.Add(Guid.Parse(propertyValue));
                        }
                    }
                }
            }
            else
            {
                logger.LogDebug("the ownedFeatureChaining Json property was not found in the LiteralInteger: { Id }", dtoInstance.Id);
            }

            if (jsonElement.TryGetProperty("ownedFeatureInverting"u8, out var ownedFeatureInvertingProperty))
            {
                foreach (var arrayItem in ownedFeatureInvertingProperty.EnumerateArray())
                {
                    if (arrayItem.TryGetProperty("@id"u8, out var ownedFeatureInvertingExternalIdProperty))
                    {
                        var propertyValue = ownedFeatureInvertingExternalIdProperty.GetString();

                        if (propertyValue != null)
                        {
                            dtoInstance.ownedFeatureInverting.Add(Guid.Parse(propertyValue));
                        }
                    }
                }
            }
            else
            {
                logger.LogDebug("the ownedFeatureInverting Json property was not found in the LiteralInteger: { Id }", dtoInstance.Id);
            }

            if (jsonElement.TryGetProperty("ownedFeatureMembership"u8, out var ownedFeatureMembershipProperty))
            {
                foreach (var arrayItem in ownedFeatureMembershipProperty.EnumerateArray())
                {
                    if (arrayItem.TryGetProperty("@id"u8, out var ownedFeatureMembershipExternalIdProperty))
                    {
                        var propertyValue = ownedFeatureMembershipExternalIdProperty.GetString();

                        if (propertyValue != null)
                        {
                            dtoInstance.ownedFeatureMembership.Add(Guid.Parse(propertyValue));
                        }
                    }
                }
            }
            else
            {
                logger.LogDebug("the ownedFeatureMembership Json property was not found in the LiteralInteger: { Id }", dtoInstance.Id);
            }

            if (jsonElement.TryGetProperty("ownedImport"u8, out var ownedImportProperty))
            {
                foreach (var arrayItem in ownedImportProperty.EnumerateArray())
                {
                    if (arrayItem.TryGetProperty("@id"u8, out var ownedImportExternalIdProperty))
                    {
                        var propertyValue = ownedImportExternalIdProperty.GetString();

                        if (propertyValue != null)
                        {
                            dtoInstance.ownedImport.Add(Guid.Parse(propertyValue));
                        }
                    }
                }
            }
            else
            {
                logger.LogDebug("the ownedImport Json property was not found in the LiteralInteger: { Id }", dtoInstance.Id);
            }

            if (jsonElement.TryGetProperty("ownedIntersecting"u8, out var ownedIntersectingProperty))
            {
                foreach (var arrayItem in ownedIntersectingProperty.EnumerateArray())
                {
                    if (arrayItem.TryGetProperty("@id"u8, out var ownedIntersectingExternalIdProperty))
                    {
                        var propertyValue = ownedIntersectingExternalIdProperty.GetString();

                        if (propertyValue != null)
                        {
                            dtoInstance.ownedIntersecting.Add(Guid.Parse(propertyValue));
                        }
                    }
                }
            }
            else
            {
                logger.LogDebug("the ownedIntersecting Json property was not found in the LiteralInteger: { Id }", dtoInstance.Id);
            }

            if (jsonElement.TryGetProperty("ownedMember"u8, out var ownedMemberProperty))
            {
                foreach (var arrayItem in ownedMemberProperty.EnumerateArray())
                {
                    if (arrayItem.TryGetProperty("@id"u8, out var ownedMemberExternalIdProperty))
                    {
                        var propertyValue = ownedMemberExternalIdProperty.GetString();

                        if (propertyValue != null)
                        {
                            dtoInstance.ownedMember.Add(Guid.Parse(propertyValue));
                        }
                    }
                }
            }
            else
            {
                logger.LogDebug("the ownedMember Json property was not found in the LiteralInteger: { Id }", dtoInstance.Id);
            }

            if (jsonElement.TryGetProperty("ownedMembership"u8, out var ownedMembershipProperty))
            {
                foreach (var arrayItem in ownedMembershipProperty.EnumerateArray())
                {
                    if (arrayItem.TryGetProperty("@id"u8, out var ownedMembershipExternalIdProperty))
                    {
                        var propertyValue = ownedMembershipExternalIdProperty.GetString();

                        if (propertyValue != null)
                        {
                            dtoInstance.ownedMembership.Add(Guid.Parse(propertyValue));
                        }
                    }
                }
            }
            else
            {
                logger.LogDebug("the ownedMembership Json property was not found in the LiteralInteger: { Id }", dtoInstance.Id);
            }

            if (jsonElement.TryGetProperty("ownedRedefinition"u8, out var ownedRedefinitionProperty))
            {
                foreach (var arrayItem in ownedRedefinitionProperty.EnumerateArray())
                {
                    if (arrayItem.TryGetProperty("@id"u8, out var ownedRedefinitionExternalIdProperty))
                    {
                        var propertyValue = ownedRedefinitionExternalIdProperty.GetString();

                        if (propertyValue != null)
                        {
                            dtoInstance.ownedRedefinition.Add(Guid.Parse(propertyValue));
                        }
                    }
                }
            }
            else
            {
                logger.LogDebug("the ownedRedefinition Json property was not found in the LiteralInteger: { Id }", dtoInstance.Id);
            }

            if (jsonElement.TryGetProperty("ownedReferenceSubsetting"u8, out var ownedReferenceSubsettingProperty))
            {
                if (ownedReferenceSubsettingProperty.ValueKind == JsonValueKind.Null)
                {
                    dtoInstance.ownedReferenceSubsetting = null;
                }
                else
                {
                    if (ownedReferenceSubsettingProperty.TryGetProperty("@id"u8, out var ownedReferenceSubsettingExternalIdProperty))
                    {
                        var propertyValue = ownedReferenceSubsettingExternalIdProperty.GetString();

                        if (propertyValue != null)
                        {
                            dtoInstance.ownedReferenceSubsetting = Guid.Parse(propertyValue);
                        }
                    }
                }
            }
            else
            {
                logger.LogDebug("the ownedReferenceSubsetting Json property was not found in the LiteralInteger: { Id }", dtoInstance.Id);
            }

            if (jsonElement.TryGetProperty("ownedRelationship"u8, out var ownedRelationshipProperty))
            {
                foreach (var arrayItem in ownedRelationshipProperty.EnumerateArray())
                {
                    if (arrayItem.TryGetProperty("@id"u8, out var ownedRelationshipExternalIdProperty))
                    {
                        var propertyValue = ownedRelationshipExternalIdProperty.GetString();

                        if (propertyValue != null)
                        {
                            dtoInstance.OwnedRelationship.Add(Guid.Parse(propertyValue));
                        }
                    }
                }
            }
            else
            {
                logger.LogDebug("the ownedRelationship Json property was not found in the LiteralInteger: { Id }", dtoInstance.Id);
            }

            if (jsonElement.TryGetProperty("ownedSpecialization"u8, out var ownedSpecializationProperty))
            {
                foreach (var arrayItem in ownedSpecializationProperty.EnumerateArray())
                {
                    if (arrayItem.TryGetProperty("@id"u8, out var ownedSpecializationExternalIdProperty))
                    {
                        var propertyValue = ownedSpecializationExternalIdProperty.GetString();

                        if (propertyValue != null)
                        {
                            dtoInstance.ownedSpecialization.Add(Guid.Parse(propertyValue));
                        }
                    }
                }
            }
            else
            {
                logger.LogDebug("the ownedSpecialization Json property was not found in the LiteralInteger: { Id }", dtoInstance.Id);
            }

            if (jsonElement.TryGetProperty("ownedSubsetting"u8, out var ownedSubsettingProperty))
            {
                foreach (var arrayItem in ownedSubsettingProperty.EnumerateArray())
                {
                    if (arrayItem.TryGetProperty("@id"u8, out var ownedSubsettingExternalIdProperty))
                    {
                        var propertyValue = ownedSubsettingExternalIdProperty.GetString();

                        if (propertyValue != null)
                        {
                            dtoInstance.ownedSubsetting.Add(Guid.Parse(propertyValue));
                        }
                    }
                }
            }
            else
            {
                logger.LogDebug("the ownedSubsetting Json property was not found in the LiteralInteger: { Id }", dtoInstance.Id);
            }

            if (jsonElement.TryGetProperty("ownedTypeFeaturing"u8, out var ownedTypeFeaturingProperty))
            {
                foreach (var arrayItem in ownedTypeFeaturingProperty.EnumerateArray())
                {
                    if (arrayItem.TryGetProperty("@id"u8, out var ownedTypeFeaturingExternalIdProperty))
                    {
                        var propertyValue = ownedTypeFeaturingExternalIdProperty.GetString();

                        if (propertyValue != null)
                        {
                            dtoInstance.ownedTypeFeaturing.Add(Guid.Parse(propertyValue));
                        }
                    }
                }
            }
            else
            {
                logger.LogDebug("the ownedTypeFeaturing Json property was not found in the LiteralInteger: { Id }", dtoInstance.Id);
            }

            if (jsonElement.TryGetProperty("ownedTyping"u8, out var ownedTypingProperty))
            {
                foreach (var arrayItem in ownedTypingProperty.EnumerateArray())
                {
                    if (arrayItem.TryGetProperty("@id"u8, out var ownedTypingExternalIdProperty))
                    {
                        var propertyValue = ownedTypingExternalIdProperty.GetString();

                        if (propertyValue != null)
                        {
                            dtoInstance.ownedTyping.Add(Guid.Parse(propertyValue));
                        }
                    }
                }
            }
            else
            {
                logger.LogDebug("the ownedTyping Json property was not found in the LiteralInteger: { Id }", dtoInstance.Id);
            }

            if (jsonElement.TryGetProperty("ownedUnioning"u8, out var ownedUnioningProperty))
            {
                foreach (var arrayItem in ownedUnioningProperty.EnumerateArray())
                {
                    if (arrayItem.TryGetProperty("@id"u8, out var ownedUnioningExternalIdProperty))
                    {
                        var propertyValue = ownedUnioningExternalIdProperty.GetString();

                        if (propertyValue != null)
                        {
                            dtoInstance.ownedUnioning.Add(Guid.Parse(propertyValue));
                        }
                    }
                }
            }
            else
            {
                logger.LogDebug("the ownedUnioning Json property was not found in the LiteralInteger: { Id }", dtoInstance.Id);
            }

            if (jsonElement.TryGetProperty("owner"u8, out var ownerProperty))
            {
                if (ownerProperty.ValueKind == JsonValueKind.Null)
                {
                    dtoInstance.owner = null;
                }
                else
                {
                    if (ownerProperty.TryGetProperty("@id"u8, out var ownerExternalIdProperty))
                    {
                        var propertyValue = ownerExternalIdProperty.GetString();

                        if (propertyValue != null)
                        {
                            dtoInstance.owner = Guid.Parse(propertyValue);
                        }
                    }
                }
            }
            else
            {
                logger.LogDebug("the owner Json property was not found in the LiteralInteger: { Id }", dtoInstance.Id);
            }

            if (jsonElement.TryGetProperty("owningFeatureMembership"u8, out var owningFeatureMembershipProperty))
            {
                if (owningFeatureMembershipProperty.ValueKind == JsonValueKind.Null)
                {
                    dtoInstance.owningFeatureMembership = null;
                }
                else
                {
                    if (owningFeatureMembershipProperty.TryGetProperty("@id"u8, out var owningFeatureMembershipExternalIdProperty))
                    {
                        var propertyValue = owningFeatureMembershipExternalIdProperty.GetString();

                        if (propertyValue != null)
                        {
                            dtoInstance.owningFeatureMembership = Guid.Parse(propertyValue);
                        }
                    }
                }
            }
            else
            {
                logger.LogDebug("the owningFeatureMembership Json property was not found in the LiteralInteger: { Id }", dtoInstance.Id);
            }

            if (jsonElement.TryGetProperty("owningMembership"u8, out var owningMembershipProperty))
            {
                if (owningMembershipProperty.ValueKind == JsonValueKind.Null)
                {
                    dtoInstance.owningMembership = null;
                }
                else
                {
                    if (owningMembershipProperty.TryGetProperty("@id"u8, out var owningMembershipExternalIdProperty))
                    {
                        var propertyValue = owningMembershipExternalIdProperty.GetString();

                        if (propertyValue != null)
                        {
                            dtoInstance.owningMembership = Guid.Parse(propertyValue);
                        }
                    }
                }
            }
            else
            {
                logger.LogDebug("the owningMembership Json property was not found in the LiteralInteger: { Id }", dtoInstance.Id);
            }

            if (jsonElement.TryGetProperty("owningNamespace"u8, out var owningNamespaceProperty))
            {
                if (owningNamespaceProperty.ValueKind == JsonValueKind.Null)
                {
                    dtoInstance.owningNamespace = null;
                }
                else
                {
                    if (owningNamespaceProperty.TryGetProperty("@id"u8, out var owningNamespaceExternalIdProperty))
                    {
                        var propertyValue = owningNamespaceExternalIdProperty.GetString();

                        if (propertyValue != null)
                        {
                            dtoInstance.owningNamespace = Guid.Parse(propertyValue);
                        }
                    }
                }
            }
            else
            {
                logger.LogDebug("the owningNamespace Json property was not found in the LiteralInteger: { Id }", dtoInstance.Id);
            }

            if (jsonElement.TryGetProperty("owningRelationship"u8, out var owningRelationshipProperty))
            {
                if (owningRelationshipProperty.ValueKind == JsonValueKind.Null)
                {
                    dtoInstance.OwningRelationship = null;
                }
                else
                {
                    if (owningRelationshipProperty.TryGetProperty("@id"u8, out var owningRelationshipExternalIdProperty))
                    {
                        var propertyValue = owningRelationshipExternalIdProperty.GetString();

                        if (propertyValue != null)
                        {
                            dtoInstance.OwningRelationship = Guid.Parse(propertyValue);
                        }
                    }
                }
            }
            else
            {
                logger.LogDebug("the owningRelationship Json property was not found in the LiteralInteger: { Id }", dtoInstance.Id);
            }

            if (jsonElement.TryGetProperty("owningType"u8, out var owningTypeProperty))
            {
                if (owningTypeProperty.ValueKind == JsonValueKind.Null)
                {
                    dtoInstance.owningType = null;
                }
                else
                {
                    if (owningTypeProperty.TryGetProperty("@id"u8, out var owningTypeExternalIdProperty))
                    {
                        var propertyValue = owningTypeExternalIdProperty.GetString();

                        if (propertyValue != null)
                        {
                            dtoInstance.owningType = Guid.Parse(propertyValue);
                        }
                    }
                }
            }
            else
            {
                logger.LogDebug("the owningType Json property was not found in the LiteralInteger: { Id }", dtoInstance.Id);
            }

            if (jsonElement.TryGetProperty("parameter"u8, out var parameterProperty))
            {
                foreach (var arrayItem in parameterProperty.EnumerateArray())
                {
                    if (arrayItem.TryGetProperty("@id"u8, out var parameterExternalIdProperty))
                    {
                        var propertyValue = parameterExternalIdProperty.GetString();

                        if (propertyValue != null)
                        {
                            dtoInstance.parameter.Add(Guid.Parse(propertyValue));
                        }
                    }
                }
            }
            else
            {
                logger.LogDebug("the parameter Json property was not found in the LiteralInteger: { Id }", dtoInstance.Id);
            }

            if (jsonElement.TryGetProperty("qualifiedName"u8, out var qualifiedNameProperty))
            {
                dtoInstance.qualifiedName = qualifiedNameProperty.GetString();
            }
            else
            {
                logger.LogDebug("the qualifiedName Json property was not found in the LiteralInteger: { Id }", dtoInstance.Id);
            }

            if (jsonElement.TryGetProperty("result"u8, out var resultProperty))
            {
                if (resultProperty.ValueKind == JsonValueKind.Null)
                {
                    dtoInstance.result = Guid.Empty;
                    logger.LogDebug($"the LiteralInteger.result property was not found in the Json. The value is set to Guid.Empty");
                }
                else
                {
                    if (resultProperty.TryGetProperty("@id"u8, out var resultExternalIdProperty))
                    {
                        var propertyValue = resultExternalIdProperty.GetString();

                        if (propertyValue != null)
                        {
                            dtoInstance.result = Guid.Parse(propertyValue);
                        }
                    }
                }
            }
            else
            {
                logger.LogDebug("the result Json property was not found in the LiteralInteger: { Id }", dtoInstance.Id);
            }

            if (jsonElement.TryGetProperty("shortName"u8, out var shortNameProperty))
            {
                dtoInstance.shortName = shortNameProperty.GetString();
            }
            else
            {
                logger.LogDebug("the shortName Json property was not found in the LiteralInteger: { Id }", dtoInstance.Id);
            }

            if (jsonElement.TryGetProperty("textualRepresentation"u8, out var textualRepresentationProperty))
            {
                foreach (var arrayItem in textualRepresentationProperty.EnumerateArray())
                {
                    if (arrayItem.TryGetProperty("@id"u8, out var textualRepresentationExternalIdProperty))
                    {
                        var propertyValue = textualRepresentationExternalIdProperty.GetString();

                        if (propertyValue != null)
                        {
                            dtoInstance.textualRepresentation.Add(Guid.Parse(propertyValue));
                        }
                    }
                }
            }
            else
            {
                logger.LogDebug("the textualRepresentation Json property was not found in the LiteralInteger: { Id }", dtoInstance.Id);
            }

            if (jsonElement.TryGetProperty("type"u8, out var typeProperty))
            {
                foreach (var arrayItem in typeProperty.EnumerateArray())
                {
                    if (arrayItem.TryGetProperty("@id"u8, out var typeExternalIdProperty))
                    {
                        var propertyValue = typeExternalIdProperty.GetString();

                        if (propertyValue != null)
                        {
                            dtoInstance.type.Add(Guid.Parse(propertyValue));
                        }
                    }
                }
            }
            else
            {
                logger.LogDebug("the type Json property was not found in the LiteralInteger: { Id }", dtoInstance.Id);
            }

            if (jsonElement.TryGetProperty("unioningType"u8, out var unioningTypeProperty))
            {
                foreach (var arrayItem in unioningTypeProperty.EnumerateArray())
                {
                    if (arrayItem.TryGetProperty("@id"u8, out var unioningTypeExternalIdProperty))
                    {
                        var propertyValue = unioningTypeExternalIdProperty.GetString();

                        if (propertyValue != null)
                        {
                            dtoInstance.unioningType.Add(Guid.Parse(propertyValue));
                        }
                    }
                }
            }
            else
            {
                logger.LogDebug("the unioningType Json property was not found in the LiteralInteger: { Id }", dtoInstance.Id);
            }

            if (jsonElement.TryGetProperty("value"u8, out var valueProperty))
            {
                dtoInstance.Value = valueProperty.GetInt32();
            }
            else
            {
                logger.LogDebug("the value Json property was not found in the LiteralInteger: { Id }", dtoInstance.Id);
            }

        }

        /// <summary>
        /// Deserializes properties of a <see cref="LiteralInteger" />
        /// from a <see cref="JsonElement" />, excluding derived properties
        /// </summary>
        /// <param name="dtoInstance">
        /// The <see cref="LiteralInteger"/> instance holding deserialized values
        /// </param>
        /// <param name="jsonElement">
        /// The <see cref="JsonElement"/> that contains the <see cref="ILiteralInteger"/> json object
        /// </param>
        /// <param name="logger">
        /// The <see cref="ILogger"/> to produce logging statement
        /// </param>
        private static void DeserializeDtoExcludingDerivedProperties(SysML2.NET.Core.DTO.Kernel.Expressions.LiteralInteger dtoInstance, JsonElement jsonElement, ILogger logger)
        {
            if (jsonElement.TryGetProperty("aliasIds"u8, out var aliasIdsProperty))
            {
                foreach (var arrayItem in aliasIdsProperty.EnumerateArray())
                {
                    var propertyValue = arrayItem.GetString();

                    if (propertyValue != null)
                    {
                        dtoInstance.AliasIds.Add(propertyValue);
                    }
                }
            }
            else
            {
                logger.LogDebug("the aliasIds Json property was not found in the LiteralInteger: { Id }", dtoInstance.Id);
            }

            if (jsonElement.TryGetProperty("declaredName"u8, out var declaredNameProperty))
            {
                dtoInstance.DeclaredName = declaredNameProperty.GetString();
            }
            else
            {
                logger.LogDebug("the declaredName Json property was not found in the LiteralInteger: { Id }", dtoInstance.Id);
            }

            if (jsonElement.TryGetProperty("declaredShortName"u8, out var declaredShortNameProperty))
            {
                dtoInstance.DeclaredShortName = declaredShortNameProperty.GetString();
            }
            else
            {
                logger.LogDebug("the declaredShortName Json property was not found in the LiteralInteger: { Id }", dtoInstance.Id);
            }

            if (jsonElement.TryGetProperty("direction"u8, out var directionProperty))
            {
                dtoInstance.Direction = FeatureDirectionKindDeSerializer.DeserializeNullable(directionProperty.GetString());
            }
            else
            {
                logger.LogDebug("the direction Json property was not found in the LiteralInteger: { Id }", dtoInstance.Id);
            }

            if (jsonElement.TryGetProperty("elementId"u8, out var elementIdProperty))
            {
                var propertyValue = elementIdProperty.GetString();

                if (propertyValue != null)
                {
                    dtoInstance.ElementId = propertyValue;
                }
            }
            else
            {
                logger.LogDebug("the elementId Json property was not found in the LiteralInteger: { Id }", dtoInstance.Id);
            }

            if (jsonElement.TryGetProperty("isAbstract"u8, out var isAbstractProperty))
            {
                if (isAbstractProperty.ValueKind != JsonValueKind.Null)
                {
                    dtoInstance.IsAbstract = isAbstractProperty.GetBoolean();
                }
            }
            else
            {
                logger.LogDebug("the isAbstract Json property was not found in the LiteralInteger: { Id }", dtoInstance.Id);
            }

            if (jsonElement.TryGetProperty("isComposite"u8, out var isCompositeProperty))
            {
                if (isCompositeProperty.ValueKind != JsonValueKind.Null)
                {
                    dtoInstance.IsComposite = isCompositeProperty.GetBoolean();
                }
            }
            else
            {
                logger.LogDebug("the isComposite Json property was not found in the LiteralInteger: { Id }", dtoInstance.Id);
            }

            if (jsonElement.TryGetProperty("isConstant"u8, out var isConstantProperty))
            {
                if (isConstantProperty.ValueKind != JsonValueKind.Null)
                {
                    dtoInstance.IsConstant = isConstantProperty.GetBoolean();
                }
            }
            else
            {
                logger.LogDebug("the isConstant Json property was not found in the LiteralInteger: { Id }", dtoInstance.Id);
            }

            if (jsonElement.TryGetProperty("isDerived"u8, out var isDerivedProperty))
            {
                if (isDerivedProperty.ValueKind != JsonValueKind.Null)
                {
                    dtoInstance.IsDerived = isDerivedProperty.GetBoolean();
                }
            }
            else
            {
                logger.LogDebug("the isDerived Json property was not found in the LiteralInteger: { Id }", dtoInstance.Id);
            }

            if (jsonElement.TryGetProperty("isEnd"u8, out var isEndProperty))
            {
                if (isEndProperty.ValueKind != JsonValueKind.Null)
                {
                    dtoInstance.IsEnd = isEndProperty.GetBoolean();
                }
            }
            else
            {
                logger.LogDebug("the isEnd Json property was not found in the LiteralInteger: { Id }", dtoInstance.Id);
            }

            if (jsonElement.TryGetProperty("isImpliedIncluded"u8, out var isImpliedIncludedProperty))
            {
                if (isImpliedIncludedProperty.ValueKind != JsonValueKind.Null)
                {
                    dtoInstance.IsImpliedIncluded = isImpliedIncludedProperty.GetBoolean();
                }
            }
            else
            {
                logger.LogDebug("the isImpliedIncluded Json property was not found in the LiteralInteger: { Id }", dtoInstance.Id);
            }

            if (jsonElement.TryGetProperty("isOrdered"u8, out var isOrderedProperty))
            {
                if (isOrderedProperty.ValueKind != JsonValueKind.Null)
                {
                    dtoInstance.IsOrdered = isOrderedProperty.GetBoolean();
                }
            }
            else
            {
                logger.LogDebug("the isOrdered Json property was not found in the LiteralInteger: { Id }", dtoInstance.Id);
            }

            if (jsonElement.TryGetProperty("isPortion"u8, out var isPortionProperty))
            {
                if (isPortionProperty.ValueKind != JsonValueKind.Null)
                {
                    dtoInstance.IsPortion = isPortionProperty.GetBoolean();
                }
            }
            else
            {
                logger.LogDebug("the isPortion Json property was not found in the LiteralInteger: { Id }", dtoInstance.Id);
            }

            if (jsonElement.TryGetProperty("isSufficient"u8, out var isSufficientProperty))
            {
                if (isSufficientProperty.ValueKind != JsonValueKind.Null)
                {
                    dtoInstance.IsSufficient = isSufficientProperty.GetBoolean();
                }
            }
            else
            {
                logger.LogDebug("the isSufficient Json property was not found in the LiteralInteger: { Id }", dtoInstance.Id);
            }

            if (jsonElement.TryGetProperty("isUnique"u8, out var isUniqueProperty))
            {
                if (isUniqueProperty.ValueKind != JsonValueKind.Null)
                {
                    dtoInstance.IsUnique = isUniqueProperty.GetBoolean();
                }
            }
            else
            {
                logger.LogDebug("the isUnique Json property was not found in the LiteralInteger: { Id }", dtoInstance.Id);
            }

            if (jsonElement.TryGetProperty("isVariable"u8, out var isVariableProperty))
            {
                if (isVariableProperty.ValueKind != JsonValueKind.Null)
                {
                    dtoInstance.IsVariable = isVariableProperty.GetBoolean();
                }
            }
            else
            {
                logger.LogDebug("the isVariable Json property was not found in the LiteralInteger: { Id }", dtoInstance.Id);
            }

            if (jsonElement.TryGetProperty("ownedRelationship"u8, out var ownedRelationshipProperty))
            {
                foreach (var arrayItem in ownedRelationshipProperty.EnumerateArray())
                {
                    if (arrayItem.TryGetProperty("@id"u8, out var ownedRelationshipExternalIdProperty))
                    {
                        var propertyValue = ownedRelationshipExternalIdProperty.GetString();

                        if (propertyValue != null)
                        {
                            dtoInstance.OwnedRelationship.Add(Guid.Parse(propertyValue));
                        }
                    }
                }
            }
            else
            {
                logger.LogDebug("the ownedRelationship Json property was not found in the LiteralInteger: { Id }", dtoInstance.Id);
            }

            if (jsonElement.TryGetProperty("owningRelationship"u8, out var owningRelationshipProperty))
            {
                if (owningRelationshipProperty.ValueKind == JsonValueKind.Null)
                {
                    dtoInstance.OwningRelationship = null;
                }
                else
                {
                    if (owningRelationshipProperty.TryGetProperty("@id"u8, out var owningRelationshipExternalIdProperty))
                    {
                        var propertyValue = owningRelationshipExternalIdProperty.GetString();

                        if (propertyValue != null)
                        {
                            dtoInstance.OwningRelationship = Guid.Parse(propertyValue);
                        }
                    }
                }
            }
            else
            {
                logger.LogDebug("the owningRelationship Json property was not found in the LiteralInteger: { Id }", dtoInstance.Id);
            }

            if (jsonElement.TryGetProperty("value"u8, out var valueProperty))
            {
                dtoInstance.Value = valueProperty.GetInt32();
            }
            else
            {
                logger.LogDebug("the value Json property was not found in the LiteralInteger: { Id }", dtoInstance.Id);
            }

        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
