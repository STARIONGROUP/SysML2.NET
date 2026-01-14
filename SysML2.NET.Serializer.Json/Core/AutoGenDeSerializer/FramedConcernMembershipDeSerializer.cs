// -------------------------------------------------------------------------------------------------
// <copyright file="FramedConcernMembershipDeSerializer.cs" company="Starion Group S.A.">
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
    using SysML2.NET.Core.DTO.Systems.Requirements;
    using SysML2.NET.Serializer.Json;

    /// <summary>
    /// The purpose of the <see cref="FramedConcernMembershipDeSerializer"/> is to provide deserialization capabilities
    /// for the <see cref="IFramedConcernMembership"/> interface
    /// </summary>
    internal static class FramedConcernMembershipDeSerializer
    {
        /// <summary>
        /// Deserializes an instance of <see cref="IFramedConcernMembership"/> from the provided <see cref="JsonElement"/>
        /// </summary>
        /// <param name="jsonElement">
        /// The <see cref="JsonElement"/> that contains the <see cref="IFramedConcernMembership"/> json object
        /// </param>
        /// <param name="serializationModeKind">
        /// enumeration specifying what kind of serialization shall be used
        /// </param>
        /// <param name="loggerFactory">
        /// The <see cref="ILoggerFactory"/> used to setup logging
        /// </param>
        /// <returns>
        /// an instance of <see cref="IFramedConcernMembership"/>
        /// </returns>
        internal static IFramedConcernMembership DeSerialize(JsonElement jsonElement, SerializationModeKind serializationModeKind, ILoggerFactory loggerFactory = null)
        {
            var logger = loggerFactory == null ? NullLogger.Instance : loggerFactory.CreateLogger("FramedConcernMembershipDeSerializer");

            if (!jsonElement.TryGetProperty("@type"u8, out var @type))
            {
                throw new InvalidOperationException("The @type property is not available, the FramedConcernMembershipDeSerializer cannot be used to deserialize this JsonElement");
            }

            if (@type.GetString() != "FramedConcernMembership")
            {
                throw new InvalidOperationException($"The FramedConcernMembershipDeSerializer can only be used to deserialize objects of type IFramedConcernMembership, a {@type.GetString()} was provided");
            }

            var dtoInstance = new SysML2.NET.Core.DTO.Systems.Requirements.FramedConcernMembership();

            if (jsonElement.TryGetProperty("@id"u8, out var idProperty))
            {
                var propertyValue = idProperty.GetString();

                if (propertyValue == null)
                {
                    throw new JsonException("The @id property is not present, the FramedConcernMembership cannot be deserialized");
                }
                else
                {
                    dtoInstance.Id = Guid.Parse(propertyValue);
                }
            }

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
                logger.LogDebug("the aliasIds Json property was not found in the FramedConcernMembership: { Id }", dtoInstance.Id);
            }

            if (jsonElement.TryGetProperty("declaredName"u8, out var declaredNameProperty))
            {
                dtoInstance.DeclaredName = declaredNameProperty.GetString();
            }
            else
            {
                logger.LogDebug("the declaredName Json property was not found in the FramedConcernMembership: { Id }", dtoInstance.Id);
            }

            if (jsonElement.TryGetProperty("declaredShortName"u8, out var declaredShortNameProperty))
            {
                dtoInstance.DeclaredShortName = declaredShortNameProperty.GetString();
            }
            else
            {
                logger.LogDebug("the declaredShortName Json property was not found in the FramedConcernMembership: { Id }", dtoInstance.Id);
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
                logger.LogDebug("the documentation Json property was not found in the FramedConcernMembership: { Id }", dtoInstance.Id);
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
                logger.LogDebug("the elementId Json property was not found in the FramedConcernMembership: { Id }", dtoInstance.Id);
            }

            if (jsonElement.TryGetProperty("isImplied"u8, out var isImpliedProperty))
            {
                if (isImpliedProperty.ValueKind != JsonValueKind.Null)
                {
                    dtoInstance.IsImplied = isImpliedProperty.GetBoolean();
                }
            }
            else
            {
                logger.LogDebug("the isImplied Json property was not found in the FramedConcernMembership: { Id }", dtoInstance.Id);
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
                logger.LogDebug("the isImpliedIncluded Json property was not found in the FramedConcernMembership: { Id }", dtoInstance.Id);
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
                logger.LogDebug("the isLibraryElement Json property was not found in the FramedConcernMembership: { Id }", dtoInstance.Id);
            }

            if (jsonElement.TryGetProperty("kind"u8, out var kindProperty))
            {
                dtoInstance.Kind = RequirementConstraintKindDeSerializer.Deserialize(kindProperty.GetString());
            }
            else
            {
                logger.LogDebug("the kind Json property was not found in the FramedConcernMembership: { Id }", dtoInstance.Id);
            }

            if (jsonElement.TryGetProperty("memberElement"u8, out var memberElementProperty))
            {
                if (memberElementProperty.ValueKind == JsonValueKind.Null)
                {
                    dtoInstance.MemberElement = Guid.Empty;
                    logger.LogDebug($"the FramedConcernMembership.MemberElement property was not found in the Json. The value is set to Guid.Empty");
                }
                else
                {
                    if (memberElementProperty.TryGetProperty("@id"u8, out var memberElementExternalIdProperty))
                    {
                        var propertyValue = memberElementExternalIdProperty.GetString();

                        if (propertyValue != null)
                        {
                            dtoInstance.MemberElement = Guid.Parse(propertyValue);
                        }
                    }
                }
            }
            else
            {
                logger.LogDebug("the memberElement Json property was not found in the FramedConcernMembership: { Id }", dtoInstance.Id);
            }

            if (jsonElement.TryGetProperty("memberElementId"u8, out var memberElementIdProperty))
            {
                var propertyValue = memberElementIdProperty.GetString();

                if (propertyValue != null)
                {
                    dtoInstance.memberElementId = propertyValue;
                }
            }
            else
            {
                logger.LogDebug("the memberElementId Json property was not found in the FramedConcernMembership: { Id }", dtoInstance.Id);
            }

            if (jsonElement.TryGetProperty("memberName"u8, out var memberNameProperty))
            {
                dtoInstance.MemberName = memberNameProperty.GetString();
            }
            else
            {
                logger.LogDebug("the memberName Json property was not found in the FramedConcernMembership: { Id }", dtoInstance.Id);
            }

            if (jsonElement.TryGetProperty("membershipOwningNamespace"u8, out var membershipOwningNamespaceProperty))
            {
                if (membershipOwningNamespaceProperty.ValueKind == JsonValueKind.Null)
                {
                    dtoInstance.membershipOwningNamespace = Guid.Empty;
                    logger.LogDebug($"the FramedConcernMembership.membershipOwningNamespace property was not found in the Json. The value is set to Guid.Empty");
                }
                else
                {
                    if (membershipOwningNamespaceProperty.TryGetProperty("@id"u8, out var membershipOwningNamespaceExternalIdProperty))
                    {
                        var propertyValue = membershipOwningNamespaceExternalIdProperty.GetString();

                        if (propertyValue != null)
                        {
                            dtoInstance.membershipOwningNamespace = Guid.Parse(propertyValue);
                        }
                    }
                }
            }
            else
            {
                logger.LogDebug("the membershipOwningNamespace Json property was not found in the FramedConcernMembership: { Id }", dtoInstance.Id);
            }

            if (jsonElement.TryGetProperty("memberShortName"u8, out var memberShortNameProperty))
            {
                dtoInstance.MemberShortName = memberShortNameProperty.GetString();
            }
            else
            {
                logger.LogDebug("the memberShortName Json property was not found in the FramedConcernMembership: { Id }", dtoInstance.Id);
            }

            if (jsonElement.TryGetProperty("name"u8, out var nameProperty))
            {
                dtoInstance.name = nameProperty.GetString();
            }
            else
            {
                logger.LogDebug("the name Json property was not found in the FramedConcernMembership: { Id }", dtoInstance.Id);
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
                logger.LogDebug("the ownedAnnotation Json property was not found in the FramedConcernMembership: { Id }", dtoInstance.Id);
            }

            if (jsonElement.TryGetProperty("ownedConcern"u8, out var ownedConcernProperty))
            {
                if (ownedConcernProperty.ValueKind == JsonValueKind.Null)
                {
                    dtoInstance.ownedConcern = Guid.Empty;
                    logger.LogDebug($"the FramedConcernMembership.ownedConcern property was not found in the Json. The value is set to Guid.Empty");
                }
                else
                {
                    if (ownedConcernProperty.TryGetProperty("@id"u8, out var ownedConcernExternalIdProperty))
                    {
                        var propertyValue = ownedConcernExternalIdProperty.GetString();

                        if (propertyValue != null)
                        {
                            dtoInstance.ownedConcern = Guid.Parse(propertyValue);
                        }
                    }
                }
            }
            else
            {
                logger.LogDebug("the ownedConcern Json property was not found in the FramedConcernMembership: { Id }", dtoInstance.Id);
            }

            if (jsonElement.TryGetProperty("ownedConstraint"u8, out var ownedConstraintProperty))
            {
                if (ownedConstraintProperty.ValueKind == JsonValueKind.Null)
                {
                    dtoInstance.ownedConstraint = Guid.Empty;
                    logger.LogDebug($"the FramedConcernMembership.ownedConstraint property was not found in the Json. The value is set to Guid.Empty");
                }
                else
                {
                    if (ownedConstraintProperty.TryGetProperty("@id"u8, out var ownedConstraintExternalIdProperty))
                    {
                        var propertyValue = ownedConstraintExternalIdProperty.GetString();

                        if (propertyValue != null)
                        {
                            dtoInstance.ownedConstraint = Guid.Parse(propertyValue);
                        }
                    }
                }
            }
            else
            {
                logger.LogDebug("the ownedConstraint Json property was not found in the FramedConcernMembership: { Id }", dtoInstance.Id);
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
                logger.LogDebug("the ownedElement Json property was not found in the FramedConcernMembership: { Id }", dtoInstance.Id);
            }

            if (jsonElement.TryGetProperty("ownedMemberElement"u8, out var ownedMemberElementProperty))
            {
                if (ownedMemberElementProperty.ValueKind == JsonValueKind.Null)
                {
                    dtoInstance.ownedMemberElement = Guid.Empty;
                    logger.LogDebug($"the FramedConcernMembership.ownedMemberElement property was not found in the Json. The value is set to Guid.Empty");
                }
                else
                {
                    if (ownedMemberElementProperty.TryGetProperty("@id"u8, out var ownedMemberElementExternalIdProperty))
                    {
                        var propertyValue = ownedMemberElementExternalIdProperty.GetString();

                        if (propertyValue != null)
                        {
                            dtoInstance.ownedMemberElement = Guid.Parse(propertyValue);
                        }
                    }
                }
            }
            else
            {
                logger.LogDebug("the ownedMemberElement Json property was not found in the FramedConcernMembership: { Id }", dtoInstance.Id);
            }

            if (jsonElement.TryGetProperty("ownedMemberElementId"u8, out var ownedMemberElementIdProperty))
            {
                var propertyValue = ownedMemberElementIdProperty.GetString();

                if (propertyValue != null)
                {
                    dtoInstance.ownedMemberElementId = propertyValue;
                }
            }
            else
            {
                logger.LogDebug("the ownedMemberElementId Json property was not found in the FramedConcernMembership: { Id }", dtoInstance.Id);
            }

            if (jsonElement.TryGetProperty("ownedMemberFeature"u8, out var ownedMemberFeatureProperty))
            {
                if (ownedMemberFeatureProperty.ValueKind == JsonValueKind.Null)
                {
                    dtoInstance.ownedMemberFeature = Guid.Empty;
                    logger.LogDebug($"the FramedConcernMembership.ownedMemberFeature property was not found in the Json. The value is set to Guid.Empty");
                }
                else
                {
                    if (ownedMemberFeatureProperty.TryGetProperty("@id"u8, out var ownedMemberFeatureExternalIdProperty))
                    {
                        var propertyValue = ownedMemberFeatureExternalIdProperty.GetString();

                        if (propertyValue != null)
                        {
                            dtoInstance.ownedMemberFeature = Guid.Parse(propertyValue);
                        }
                    }
                }
            }
            else
            {
                logger.LogDebug("the ownedMemberFeature Json property was not found in the FramedConcernMembership: { Id }", dtoInstance.Id);
            }

            if (jsonElement.TryGetProperty("ownedMemberName"u8, out var ownedMemberNameProperty))
            {
                dtoInstance.ownedMemberName = ownedMemberNameProperty.GetString();
            }
            else
            {
                logger.LogDebug("the ownedMemberName Json property was not found in the FramedConcernMembership: { Id }", dtoInstance.Id);
            }

            if (jsonElement.TryGetProperty("ownedMemberShortName"u8, out var ownedMemberShortNameProperty))
            {
                dtoInstance.ownedMemberShortName = ownedMemberShortNameProperty.GetString();
            }
            else
            {
                logger.LogDebug("the ownedMemberShortName Json property was not found in the FramedConcernMembership: { Id }", dtoInstance.Id);
            }

            if (jsonElement.TryGetProperty("ownedRelatedElement"u8, out var ownedRelatedElementProperty))
            {
                foreach (var arrayItem in ownedRelatedElementProperty.EnumerateArray())
                {
                    if (arrayItem.TryGetProperty("@id"u8, out var ownedRelatedElementExternalIdProperty))
                    {
                        var propertyValue = ownedRelatedElementExternalIdProperty.GetString();

                        if (propertyValue != null)
                        {
                            dtoInstance.OwnedRelatedElement.Add(Guid.Parse(propertyValue));
                        }
                    }
                }
            }
            else
            {
                logger.LogDebug("the ownedRelatedElement Json property was not found in the FramedConcernMembership: { Id }", dtoInstance.Id);
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
                logger.LogDebug("the ownedRelationship Json property was not found in the FramedConcernMembership: { Id }", dtoInstance.Id);
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
                logger.LogDebug("the owner Json property was not found in the FramedConcernMembership: { Id }", dtoInstance.Id);
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
                logger.LogDebug("the owningMembership Json property was not found in the FramedConcernMembership: { Id }", dtoInstance.Id);
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
                logger.LogDebug("the owningNamespace Json property was not found in the FramedConcernMembership: { Id }", dtoInstance.Id);
            }

            if (jsonElement.TryGetProperty("owningRelatedElement"u8, out var owningRelatedElementProperty))
            {
                if (owningRelatedElementProperty.ValueKind == JsonValueKind.Null)
                {
                    dtoInstance.OwningRelatedElement = null;
                }
                else
                {
                    if (owningRelatedElementProperty.TryGetProperty("@id"u8, out var owningRelatedElementExternalIdProperty))
                    {
                        var propertyValue = owningRelatedElementExternalIdProperty.GetString();

                        if (propertyValue != null)
                        {
                            dtoInstance.OwningRelatedElement = Guid.Parse(propertyValue);
                        }
                    }
                }
            }
            else
            {
                logger.LogDebug("the owningRelatedElement Json property was not found in the FramedConcernMembership: { Id }", dtoInstance.Id);
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
                logger.LogDebug("the owningRelationship Json property was not found in the FramedConcernMembership: { Id }", dtoInstance.Id);
            }

            if (jsonElement.TryGetProperty("owningType"u8, out var owningTypeProperty))
            {
                if (owningTypeProperty.ValueKind == JsonValueKind.Null)
                {
                    dtoInstance.owningType = Guid.Empty;
                    logger.LogDebug($"the FramedConcernMembership.owningType property was not found in the Json. The value is set to Guid.Empty");
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
                logger.LogDebug("the owningType Json property was not found in the FramedConcernMembership: { Id }", dtoInstance.Id);
            }

            if (jsonElement.TryGetProperty("qualifiedName"u8, out var qualifiedNameProperty))
            {
                dtoInstance.qualifiedName = qualifiedNameProperty.GetString();
            }
            else
            {
                logger.LogDebug("the qualifiedName Json property was not found in the FramedConcernMembership: { Id }", dtoInstance.Id);
            }

            if (jsonElement.TryGetProperty("referencedConcern"u8, out var referencedConcernProperty))
            {
                if (referencedConcernProperty.ValueKind == JsonValueKind.Null)
                {
                    dtoInstance.referencedConcern = Guid.Empty;
                    logger.LogDebug($"the FramedConcernMembership.referencedConcern property was not found in the Json. The value is set to Guid.Empty");
                }
                else
                {
                    if (referencedConcernProperty.TryGetProperty("@id"u8, out var referencedConcernExternalIdProperty))
                    {
                        var propertyValue = referencedConcernExternalIdProperty.GetString();

                        if (propertyValue != null)
                        {
                            dtoInstance.referencedConcern = Guid.Parse(propertyValue);
                        }
                    }
                }
            }
            else
            {
                logger.LogDebug("the referencedConcern Json property was not found in the FramedConcernMembership: { Id }", dtoInstance.Id);
            }

            if (jsonElement.TryGetProperty("referencedConstraint"u8, out var referencedConstraintProperty))
            {
                if (referencedConstraintProperty.ValueKind == JsonValueKind.Null)
                {
                    dtoInstance.referencedConstraint = Guid.Empty;
                    logger.LogDebug($"the FramedConcernMembership.referencedConstraint property was not found in the Json. The value is set to Guid.Empty");
                }
                else
                {
                    if (referencedConstraintProperty.TryGetProperty("@id"u8, out var referencedConstraintExternalIdProperty))
                    {
                        var propertyValue = referencedConstraintExternalIdProperty.GetString();

                        if (propertyValue != null)
                        {
                            dtoInstance.referencedConstraint = Guid.Parse(propertyValue);
                        }
                    }
                }
            }
            else
            {
                logger.LogDebug("the referencedConstraint Json property was not found in the FramedConcernMembership: { Id }", dtoInstance.Id);
            }

            if (jsonElement.TryGetProperty("relatedElement"u8, out var relatedElementProperty))
            {
                foreach (var arrayItem in relatedElementProperty.EnumerateArray())
                {
                    if (arrayItem.TryGetProperty("@id"u8, out var relatedElementExternalIdProperty))
                    {
                        var propertyValue = relatedElementExternalIdProperty.GetString();

                        if (propertyValue != null)
                        {
                            dtoInstance.relatedElement.Add(Guid.Parse(propertyValue));
                        }
                    }
                }
            }
            else
            {
                logger.LogDebug("the relatedElement Json property was not found in the FramedConcernMembership: { Id }", dtoInstance.Id);
            }

            if (jsonElement.TryGetProperty("shortName"u8, out var shortNameProperty))
            {
                dtoInstance.shortName = shortNameProperty.GetString();
            }
            else
            {
                logger.LogDebug("the shortName Json property was not found in the FramedConcernMembership: { Id }", dtoInstance.Id);
            }

            if (jsonElement.TryGetProperty("source"u8, out var sourceProperty))
            {
                foreach (var arrayItem in sourceProperty.EnumerateArray())
                {
                    if (arrayItem.TryGetProperty("@id"u8, out var sourceExternalIdProperty))
                    {
                        var propertyValue = sourceExternalIdProperty.GetString();

                        if (propertyValue != null)
                        {
                            dtoInstance.Source.Add(Guid.Parse(propertyValue));
                        }
                    }
                }
            }
            else
            {
                logger.LogDebug("the source Json property was not found in the FramedConcernMembership: { Id }", dtoInstance.Id);
            }

            if (jsonElement.TryGetProperty("target"u8, out var targetProperty))
            {
                foreach (var arrayItem in targetProperty.EnumerateArray())
                {
                    if (arrayItem.TryGetProperty("@id"u8, out var targetExternalIdProperty))
                    {
                        var propertyValue = targetExternalIdProperty.GetString();

                        if (propertyValue != null)
                        {
                            dtoInstance.Target.Add(Guid.Parse(propertyValue));
                        }
                    }
                }
            }
            else
            {
                logger.LogDebug("the target Json property was not found in the FramedConcernMembership: { Id }", dtoInstance.Id);
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
                logger.LogDebug("the textualRepresentation Json property was not found in the FramedConcernMembership: { Id }", dtoInstance.Id);
            }

            if (jsonElement.TryGetProperty("visibility"u8, out var visibilityProperty))
            {
                dtoInstance.Visibility = VisibilityKindDeSerializer.Deserialize(visibilityProperty.GetString());
            }
            else
            {
                logger.LogDebug("the visibility Json property was not found in the FramedConcernMembership: { Id }", dtoInstance.Id);
            }


            return dtoInstance;
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
