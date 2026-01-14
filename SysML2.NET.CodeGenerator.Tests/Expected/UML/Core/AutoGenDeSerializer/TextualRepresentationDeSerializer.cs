// -------------------------------------------------------------------------------------------------
// <copyright file="TextualRepresentationDeSerializer.cs" company="Starion Group S.A.">
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
    using SysML2.NET.Core.DTO.Root.Annotations;
    using SysML2.NET.Serializer.Json;

    /// <summary>
    /// The purpose of the <see cref="TextualRepresentationDeSerializer"/> is to provide deserialization capabilities
    /// for the <see cref="ITextualRepresentation"/> interface
    /// </summary>
    internal static class TextualRepresentationDeSerializer
    {
        /// <summary>
        /// Deserializes an instance of <see cref="ITextualRepresentation"/> from the provided <see cref="JsonElement"/>
        /// </summary>
        /// <param name="jsonElement">
        /// The <see cref="JsonElement"/> that contains the <see cref="ITextualRepresentation"/> json object
        /// </param>
        /// <param name="serializationModeKind">
        /// enumeration specifying what kind of serialization shall be used
        /// </param>
        /// <param name="loggerFactory">
        /// The <see cref="ILoggerFactory"/> used to setup logging
        /// </param>
        /// <returns>
        /// an instance of <see cref="ITextualRepresentation"/>
        /// </returns>
        internal static ITextualRepresentation DeSerialize(JsonElement jsonElement, SerializationModeKind serializationModeKind, ILoggerFactory loggerFactory = null)
        {
            var logger = loggerFactory == null ? NullLogger.Instance : loggerFactory.CreateLogger("TextualRepresentationDeSerializer");

            if (!jsonElement.TryGetProperty("@type"u8, out var @type))
            {
                throw new InvalidOperationException("The @type property is not available, the TextualRepresentationDeSerializer cannot be used to deserialize this JsonElement");
            }

            if (@type.GetString() != "TextualRepresentation")
            {
                throw new InvalidOperationException($"The TextualRepresentationDeSerializer can only be used to deserialize objects of type ITextualRepresentation, a {@type.GetString()} was provided");
            }

            var dtoInstance = new SysML2.NET.Core.DTO.Root.Annotations.TextualRepresentation();

            if (jsonElement.TryGetProperty("@id"u8, out var idProperty))
            {
                var propertyValue = idProperty.GetString();

                if (propertyValue == null)
                {
                    throw new JsonException("The @id property is not present, the TextualRepresentation cannot be deserialized");
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
                logger.LogDebug("the aliasIds Json property was not found in the TextualRepresentation: { Id }", dtoInstance.Id);
            }

            if (jsonElement.TryGetProperty("annotatedElement"u8, out var annotatedElementProperty))
            {
                foreach (var arrayItem in annotatedElementProperty.EnumerateArray())
                {
                    if (arrayItem.TryGetProperty("@id"u8, out var annotatedElementExternalIdProperty))
                    {
                        var propertyValue = annotatedElementExternalIdProperty.GetString();

                        if (propertyValue != null)
                        {
                            dtoInstance.annotatedElement.Add(Guid.Parse(propertyValue));
                        }
                    }
                }
            }
            else
            {
                logger.LogDebug("the annotatedElement Json property was not found in the TextualRepresentation: { Id }", dtoInstance.Id);
            }

            if (jsonElement.TryGetProperty("annotation"u8, out var annotationProperty))
            {
                foreach (var arrayItem in annotationProperty.EnumerateArray())
                {
                    if (arrayItem.TryGetProperty("@id"u8, out var annotationExternalIdProperty))
                    {
                        var propertyValue = annotationExternalIdProperty.GetString();

                        if (propertyValue != null)
                        {
                            dtoInstance.annotation.Add(Guid.Parse(propertyValue));
                        }
                    }
                }
            }
            else
            {
                logger.LogDebug("the annotation Json property was not found in the TextualRepresentation: { Id }", dtoInstance.Id);
            }

            if (jsonElement.TryGetProperty("body"u8, out var bodyProperty))
            {
                var propertyValue = bodyProperty.GetString();

                if (propertyValue != null)
                {
                    dtoInstance.Body = propertyValue;
                }
            }
            else
            {
                logger.LogDebug("the body Json property was not found in the TextualRepresentation: { Id }", dtoInstance.Id);
            }

            if (jsonElement.TryGetProperty("declaredName"u8, out var declaredNameProperty))
            {
                dtoInstance.DeclaredName = declaredNameProperty.GetString();
            }
            else
            {
                logger.LogDebug("the declaredName Json property was not found in the TextualRepresentation: { Id }", dtoInstance.Id);
            }

            if (jsonElement.TryGetProperty("declaredShortName"u8, out var declaredShortNameProperty))
            {
                dtoInstance.DeclaredShortName = declaredShortNameProperty.GetString();
            }
            else
            {
                logger.LogDebug("the declaredShortName Json property was not found in the TextualRepresentation: { Id }", dtoInstance.Id);
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
                logger.LogDebug("the documentation Json property was not found in the TextualRepresentation: { Id }", dtoInstance.Id);
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
                logger.LogDebug("the elementId Json property was not found in the TextualRepresentation: { Id }", dtoInstance.Id);
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
                logger.LogDebug("the isImpliedIncluded Json property was not found in the TextualRepresentation: { Id }", dtoInstance.Id);
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
                logger.LogDebug("the isLibraryElement Json property was not found in the TextualRepresentation: { Id }", dtoInstance.Id);
            }

            if (jsonElement.TryGetProperty("language"u8, out var languageProperty))
            {
                var propertyValue = languageProperty.GetString();

                if (propertyValue != null)
                {
                    dtoInstance.Language = propertyValue;
                }
            }
            else
            {
                logger.LogDebug("the language Json property was not found in the TextualRepresentation: { Id }", dtoInstance.Id);
            }

            if (jsonElement.TryGetProperty("name"u8, out var nameProperty))
            {
                dtoInstance.name = nameProperty.GetString();
            }
            else
            {
                logger.LogDebug("the name Json property was not found in the TextualRepresentation: { Id }", dtoInstance.Id);
            }

            if (jsonElement.TryGetProperty("ownedAnnotatingRelationship"u8, out var ownedAnnotatingRelationshipProperty))
            {
                foreach (var arrayItem in ownedAnnotatingRelationshipProperty.EnumerateArray())
                {
                    if (arrayItem.TryGetProperty("@id"u8, out var ownedAnnotatingRelationshipExternalIdProperty))
                    {
                        var propertyValue = ownedAnnotatingRelationshipExternalIdProperty.GetString();

                        if (propertyValue != null)
                        {
                            dtoInstance.ownedAnnotatingRelationship.Add(Guid.Parse(propertyValue));
                        }
                    }
                }
            }
            else
            {
                logger.LogDebug("the ownedAnnotatingRelationship Json property was not found in the TextualRepresentation: { Id }", dtoInstance.Id);
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
                logger.LogDebug("the ownedAnnotation Json property was not found in the TextualRepresentation: { Id }", dtoInstance.Id);
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
                logger.LogDebug("the ownedElement Json property was not found in the TextualRepresentation: { Id }", dtoInstance.Id);
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
                logger.LogDebug("the ownedRelationship Json property was not found in the TextualRepresentation: { Id }", dtoInstance.Id);
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
                logger.LogDebug("the owner Json property was not found in the TextualRepresentation: { Id }", dtoInstance.Id);
            }

            if (jsonElement.TryGetProperty("owningAnnotatingRelationship"u8, out var owningAnnotatingRelationshipProperty))
            {
                if (owningAnnotatingRelationshipProperty.ValueKind == JsonValueKind.Null)
                {
                    dtoInstance.owningAnnotatingRelationship = null;
                }
                else
                {
                    if (owningAnnotatingRelationshipProperty.TryGetProperty("@id"u8, out var owningAnnotatingRelationshipExternalIdProperty))
                    {
                        var propertyValue = owningAnnotatingRelationshipExternalIdProperty.GetString();

                        if (propertyValue != null)
                        {
                            dtoInstance.owningAnnotatingRelationship = Guid.Parse(propertyValue);
                        }
                    }
                }
            }
            else
            {
                logger.LogDebug("the owningAnnotatingRelationship Json property was not found in the TextualRepresentation: { Id }", dtoInstance.Id);
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
                logger.LogDebug("the owningMembership Json property was not found in the TextualRepresentation: { Id }", dtoInstance.Id);
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
                logger.LogDebug("the owningNamespace Json property was not found in the TextualRepresentation: { Id }", dtoInstance.Id);
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
                logger.LogDebug("the owningRelationship Json property was not found in the TextualRepresentation: { Id }", dtoInstance.Id);
            }

            if (jsonElement.TryGetProperty("qualifiedName"u8, out var qualifiedNameProperty))
            {
                dtoInstance.qualifiedName = qualifiedNameProperty.GetString();
            }
            else
            {
                logger.LogDebug("the qualifiedName Json property was not found in the TextualRepresentation: { Id }", dtoInstance.Id);
            }

            if (jsonElement.TryGetProperty("representedElement"u8, out var representedElementProperty))
            {
                if (representedElementProperty.ValueKind == JsonValueKind.Null)
                {
                    dtoInstance.representedElement = Guid.Empty;
                    logger.LogDebug($"the TextualRepresentation.representedElement property was not found in the Json. The value is set to Guid.Empty");
                }
                else
                {
                    if (representedElementProperty.TryGetProperty("@id"u8, out var representedElementExternalIdProperty))
                    {
                        var propertyValue = representedElementExternalIdProperty.GetString();

                        if (propertyValue != null)
                        {
                            dtoInstance.representedElement = Guid.Parse(propertyValue);
                        }
                    }
                }
            }
            else
            {
                logger.LogDebug("the representedElement Json property was not found in the TextualRepresentation: { Id }", dtoInstance.Id);
            }

            if (jsonElement.TryGetProperty("shortName"u8, out var shortNameProperty))
            {
                dtoInstance.shortName = shortNameProperty.GetString();
            }
            else
            {
                logger.LogDebug("the shortName Json property was not found in the TextualRepresentation: { Id }", dtoInstance.Id);
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
                logger.LogDebug("the textualRepresentation Json property was not found in the TextualRepresentation: { Id }", dtoInstance.Id);
            }


            return dtoInstance;
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
