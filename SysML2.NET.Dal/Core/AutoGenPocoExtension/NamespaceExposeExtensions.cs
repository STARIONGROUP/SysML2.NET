// -------------------------------------------------------------------------------------------------
// <copyright file="NamespaceExposeExtensions.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.Dal
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;

    using Core.POCO.Systems.Views;

    /// <summary>
    /// A static class that provides extension methods for the <see cref="Core.POCO.Systems.Views.NamespaceExpose"/> class
    /// </summary>
    public static class NamespaceExposeExtensions
    {
        /// <summary>
        /// Updates the value properties of the <see cref="Core.POCO.Systems.Views.NamespaceExpose"/> by setting the value equal to that of the dto
        /// Removes deleted objects from the reference properties and returns the unique identifiers
        /// of the objects that have been removed from contained properties
        /// </summary>
        /// <param name="poco">
        /// The <see cref="Core.POCO.Systems.Views.NamespaceExpose"/> that is to be updated
        /// </param>
        /// <param name="dto">
        /// The DTO that is used to update the <see cref="Core.DTO.Systems.Views.NamespaceExpose"/> with
        /// </param>
        /// <returns>
        /// The unique identifiers of the objects that have been removed from contained properties
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when the <paramref name="poco"/> or <paramref name="dto"/> is null
        /// </exception>
        public static IEnumerable<Guid> UpdateValueAndRemoveDeletedReferenceProperties(this Core.POCO.Systems.Views.NamespaceExpose poco, Core.DTO.Systems.Views.NamespaceExpose dto)
        {
            if (poco == null)
            {
                throw new ArgumentNullException(nameof(poco), $"the {nameof(poco)} may not be null");
            }

            if (dto == null)
            {
                throw new ArgumentNullException(nameof(dto), $"the {nameof(dto)} may not be null");
            }

            var identifiersOfObjectsToDelete = new List<Guid>();

            poco.AliasIds = dto.AliasIds;

            poco.DeclaredName = dto.DeclaredName;

            poco.DeclaredShortName = dto.DeclaredShortName;

            poco.ElementId = dto.ElementId;

            poco.IsImplied = dto.IsImplied;

            poco.IsImpliedIncluded = dto.IsImpliedIncluded;

            poco.IsImportAll = dto.IsImportAll;

            poco.IsRecursive = dto.IsRecursive;

            var ownedRelatedElementToDelete = poco.OwnedRelatedElement.Select(x => x.Id).Except(dto.OwnedRelatedElement);

            foreach (var identifier in ownedRelatedElementToDelete)
            {
                poco.OwnedRelatedElement.Remove(poco.OwnedRelatedElement.Single(x => x.Id == identifier));
            }

            identifiersOfObjectsToDelete.AddRange(ownedRelatedElementToDelete);

            var ownedRelationshipToDelete = poco.OwnedRelationship.Select(x => x.Id).Except(dto.OwnedRelationship);

            foreach (var identifier in ownedRelationshipToDelete)
            {
                poco.OwnedRelationship.Remove(poco.OwnedRelationship.Single(x => x.Id == identifier));
            }

            identifiersOfObjectsToDelete.AddRange(ownedRelationshipToDelete);

            poco.Visibility = dto.Visibility;


            return identifiersOfObjectsToDelete;
        }

        /// <summary>
        /// Updates the Reference properties of the <see cref="Core.POCO.Systems.Views.NamespaceExpose"/> using the data (identifiers) encapsulated in the DTO
        /// and the provided cache to find the referenced object.
        /// </summary>
        /// <param name="poco">
        /// The <see cref="Core.POCO.Systems.Views.NamespaceExpose"/> that is to be updated
        /// </param>
        /// <param name="dto">
        /// The DTO that is used to update the <see cref="Core.DTO.Systems.Views.NamespaceExpose"/> with
        /// </param>
        /// <param name="cache">
        /// The <see cref="ConcurrentDictionary{Guid, Lazy}"/> that contains the
        /// <see cref="Core.POCO.Root.Elements.IElement"/>s that are know and cached.
        /// </param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void UpdateReferenceProperties(this Core.POCO.Systems.Views.NamespaceExpose poco, Core.DTO.Systems.Views.NamespaceExpose dto, ConcurrentDictionary<Guid, Lazy<Core.POCO.Root.Elements.IElement>> cache)
        {
            if (poco == null)
            {
                throw new ArgumentNullException(nameof(poco), $"the {nameof(poco)} may not be null");
            }

            if (dto == null)
            {
                throw new ArgumentNullException(nameof(dto), $"the {nameof(dto)} may not be null");
            }

            if (cache == null)
            {
                throw new ArgumentNullException(nameof(cache), $"the {nameof(cache)} may not be null");
            }

            Lazy<Core.POCO.Root.Elements.IElement> lazyPoco;

            if (cache.TryGetValue(dto.ImportedNamespace, out lazyPoco))
            {
                poco.ImportedNamespace = (Core.POCO.Root.Namespaces.Namespace)lazyPoco.Value;
            }
            else
            {
                poco.ImportedNamespace = null;
            }

            var ownedRelatedElementToAdd = dto.OwnedRelatedElement.Except(poco.OwnedRelatedElement.Select(x => x.Id));

            foreach (var identifier in ownedRelatedElementToAdd)
            {
                if (cache.TryGetValue(identifier, out lazyPoco))
                {
                    poco.OwnedRelatedElement.Add(lazyPoco.Value);
                }
            }

            var ownedRelationshipToAdd = dto.OwnedRelationship.Except(poco.OwnedRelationship.Select(x => x.Id));

            foreach (var identifier in ownedRelationshipToAdd)
            {
                if (cache.TryGetValue(identifier, out lazyPoco))
                {
                    poco.OwnedRelationship.Add((Core.POCO.Root.Elements.IRelationship)lazyPoco.Value);
                }
            }

            if (dto.OwningRelatedElement.HasValue && cache.TryGetValue(dto.OwningRelatedElement.Value, out lazyPoco))
            {
                poco.OwningRelatedElement = lazyPoco.Value;
            }
            else
            {
                poco.OwningRelatedElement = null;
            }

            if (dto.OwningRelationship.HasValue && cache.TryGetValue(dto.OwningRelationship.Value, out lazyPoco))
            {
                poco.OwningRelationship = (Core.POCO.Root.Elements.IRelationship)lazyPoco.Value;
            }
            else
            {
                poco.OwningRelationship = null;
            }

        }

        /// <summary>
        /// Creates a <see cref="Core.DTO.Systems.Views.NamespaceExpose"/> based on the provided POCO
        /// </summary>
        /// <param name="poco">
        /// The subject <see cref="Core.POCO.Systems.Views.NamespaceExpose"/> from which a DTO is to be created
        /// </param>
        /// <param name="includeDerivedProperties">
        /// Asserts that derived properties should also be mapped during the creation of the <see cref="Core.DTO.Systems.Views.NamespaceExpose"/>
        /// </param>
        /// <returns>
        /// An instance of <see cref="Core.POCO.Systems.Views.NamespaceExpose"/>
        /// </returns>
        public static Core.DTO.Systems.Views.NamespaceExpose ToDto(this Core.POCO.Systems.Views.NamespaceExpose poco, bool includeDerivedProperties = false)
        {
            var dto = new Core.DTO.Systems.Views.NamespaceExpose();

            dto.Id = poco.Id;
            dto.AliasIds = poco.AliasIds;
            dto.DeclaredName = poco.DeclaredName;
            dto.DeclaredShortName = poco.DeclaredShortName;
            dto.ElementId = poco.ElementId;
            dto.ImportedNamespace = poco.ImportedNamespace.Id;
            dto.IsImplied = poco.IsImplied;
            dto.IsImpliedIncluded = poco.IsImpliedIncluded;
            dto.IsImportAll = poco.IsImportAll;
            dto.IsRecursive = poco.IsRecursive;
            dto.OwnedRelatedElement = poco.OwnedRelatedElement.Select(x => x.Id).ToList();
            dto.OwnedRelationship = poco.OwnedRelationship.Select(x => x.Id).ToList();
            dto.OwningRelatedElement = poco.OwningRelatedElement?.Id;
            dto.OwningRelationship = poco.OwningRelationship?.Id;
            dto.Visibility = poco.Visibility;

            if (includeDerivedProperties)
            {
                dto.documentation = poco.documentation.Select(x => x.Id).ToList();
                dto.importedElement = poco.importedElement.Id;
                dto.importOwningNamespace = poco.importOwningNamespace.Id;
                dto.isLibraryElement = poco.isLibraryElement;
                dto.name = poco.name;
                dto.ownedAnnotation = poco.ownedAnnotation.Select(x => x.Id).ToList();
                dto.ownedElement = poco.ownedElement.Select(x => x.Id).ToList();
                dto.owner = poco.owner?.Id;
                dto.owningMembership = poco.owningMembership?.Id;
                dto.owningNamespace = poco.owningNamespace?.Id;
                dto.qualifiedName = poco.qualifiedName;
                dto.relatedElement = poco.relatedElement.Select(x => x.Id).ToList();
                dto.shortName = poco.shortName;
                dto.textualRepresentation = poco.textualRepresentation.Select(x => x.Id).ToList();
            }

            return dto;
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
