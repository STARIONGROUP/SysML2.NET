// -------------------------------------------------------------------------------------------------
// <copyright file="AssociationStructureExtensions.cs" company="Starion Group S.A.">
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

    using Core.POCO.Kernel.Associations;

    /// <summary>
    /// A static class that provides extension methods for the <see cref="Core.POCO.Kernel.Associations.AssociationStructure"/> class
    /// </summary>
    public static class AssociationStructureExtensions
    {
        /// <summary>
        /// Updates the value properties of the <see cref="Core.POCO.Kernel.Associations.AssociationStructure"/> by setting the value equal to that of the dto
        /// Removes deleted objects from the reference properties and returns the unique identifiers
        /// of the objects that have been removed from contained properties
        /// </summary>
        /// <param name="poco">
        /// The <see cref="Core.POCO.Kernel.Associations.AssociationStructure"/> that is to be updated
        /// </param>
        /// <param name="dto">
        /// The DTO that is used to update the <see cref="Core.DTO.Kernel.Associations.AssociationStructure"/> with
        /// </param>
        /// <returns>
        /// The unique identifiers of the objects that have been removed from contained properties
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when the <paramref name="poco"/> or <paramref name="dto"/> is null
        /// </exception>
        public static IEnumerable<Guid> UpdateValueAndRemoveDeletedReferenceProperties(this Core.POCO.Kernel.Associations.AssociationStructure poco, Core.DTO.Kernel.Associations.AssociationStructure dto)
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

            poco.IsAbstract = dto.IsAbstract;

            poco.IsImplied = dto.IsImplied;

            poco.IsImpliedIncluded = dto.IsImpliedIncluded;

            poco.IsSufficient = dto.IsSufficient;

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


            return identifiersOfObjectsToDelete;
        }

        /// <summary>
        /// Updates the Reference properties of the <see cref="Core.POCO.Kernel.Associations.AssociationStructure"/> using the data (identifiers) encapsulated in the DTO
        /// and the provided cache to find the referenced object.
        /// </summary>
        /// <param name="poco">
        /// The <see cref="Core.POCO.Kernel.Associations.AssociationStructure"/> that is to be updated
        /// </param>
        /// <param name="dto">
        /// The DTO that is used to update the <see cref="Core.DTO.Kernel.Associations.AssociationStructure"/> with
        /// </param>
        /// <param name="cache">
        /// The <see cref="ConcurrentDictionary{Guid, Lazy}"/> that contains the
        /// <see cref="Core.POCO.Root.Elements.IElement"/>s that are know and cached.
        /// </param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void UpdateReferenceProperties(this Core.POCO.Kernel.Associations.AssociationStructure poco, Core.DTO.Kernel.Associations.AssociationStructure dto, ConcurrentDictionary<Guid, Lazy<Core.POCO.Root.Elements.IElement>> cache)
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
        /// Creates a <see cref="Core.DTO.Kernel.Associations.AssociationStructure"/> based on the provided POCO
        /// </summary>
        /// <param name="poco">
        /// The subject <see cref="Core.POCO.Kernel.Associations.AssociationStructure"/> from which a DTO is to be created
        /// </param>
        /// <param name="includeDerivedProperties">
        /// Asserts that derived properties should also be mapped during the creation of the <see cref="Core.DTO.Kernel.Associations.AssociationStructure"/>
        /// </param>
        /// <returns>
        /// An instance of <see cref="Core.POCO.Kernel.Associations.AssociationStructure"/>
        /// </returns>
        public static Core.DTO.Kernel.Associations.AssociationStructure ToDto(this Core.POCO.Kernel.Associations.AssociationStructure poco, bool includeDerivedProperties = false)
        {
            var dto = new Core.DTO.Kernel.Associations.AssociationStructure();

            dto.Id = poco.Id;
            dto.AliasIds = poco.AliasIds;
            dto.DeclaredName = poco.DeclaredName;
            dto.DeclaredShortName = poco.DeclaredShortName;
            dto.ElementId = poco.ElementId;
            dto.IsAbstract = poco.IsAbstract;
            dto.IsImplied = poco.IsImplied;
            dto.IsImpliedIncluded = poco.IsImpliedIncluded;
            dto.IsSufficient = poco.IsSufficient;
            dto.OwnedRelatedElement = poco.OwnedRelatedElement.Select(x => x.Id).ToList();
            dto.OwnedRelationship = poco.OwnedRelationship.Select(x => x.Id).ToList();
            dto.OwningRelatedElement = poco.OwningRelatedElement?.Id;
            dto.OwningRelationship = poco.OwningRelationship?.Id;

            if (includeDerivedProperties)
            {
                dto.associationEnd = poco.associationEnd.Select(x => x.Id).ToList();
                dto.differencingType = poco.differencingType.Select(x => x.Id).ToList();
                dto.directedFeature = poco.directedFeature.Select(x => x.Id).ToList();
                dto.documentation = poco.documentation.Select(x => x.Id).ToList();
                dto.feature = poco.feature.Select(x => x.Id).ToList();
                dto.featureMembership = poco.featureMembership.Select(x => x.Id).ToList();
                dto.importedMembership = poco.importedMembership.Select(x => x.Id).ToList();
                dto.inheritedFeature = poco.inheritedFeature.Select(x => x.Id).ToList();
                dto.inheritedMembership = poco.inheritedMembership.Select(x => x.Id).ToList();
                dto.input = poco.input.Select(x => x.Id).ToList();
                dto.intersectingType = poco.intersectingType.Select(x => x.Id).ToList();
                dto.isConjugated = poco.isConjugated;
                dto.isLibraryElement = poco.isLibraryElement;
                dto.member = poco.member.Select(x => x.Id).ToList();
                dto.membership = poco.membership.Select(x => x.Id).ToList();
                dto.multiplicity = poco.multiplicity?.Id;
                dto.name = poco.name;
                dto.output = poco.output.Select(x => x.Id).ToList();
                dto.ownedAnnotation = poco.ownedAnnotation.Select(x => x.Id).ToList();
                dto.ownedConjugator = poco.ownedConjugator?.Id;
                dto.ownedDifferencing = poco.ownedDifferencing.Select(x => x.Id).ToList();
                dto.ownedDisjoining = poco.ownedDisjoining.Select(x => x.Id).ToList();
                dto.ownedElement = poco.ownedElement.Select(x => x.Id).ToList();
                dto.ownedEndFeature = poco.ownedEndFeature.Select(x => x.Id).ToList();
                dto.ownedFeature = poco.ownedFeature.Select(x => x.Id).ToList();
                dto.ownedFeatureMembership = poco.ownedFeatureMembership.Select(x => x.Id).ToList();
                dto.ownedImport = poco.ownedImport.Select(x => x.Id).ToList();
                dto.ownedIntersecting = poco.ownedIntersecting.Select(x => x.Id).ToList();
                dto.ownedMember = poco.ownedMember.Select(x => x.Id).ToList();
                dto.ownedMembership = poco.ownedMembership.Select(x => x.Id).ToList();
                dto.ownedSpecialization = poco.ownedSpecialization.Select(x => x.Id).ToList();
                dto.ownedSubclassification = poco.ownedSubclassification.Select(x => x.Id).ToList();
                dto.ownedUnioning = poco.ownedUnioning.Select(x => x.Id).ToList();
                dto.owner = poco.owner?.Id;
                dto.owningMembership = poco.owningMembership?.Id;
                dto.owningNamespace = poco.owningNamespace?.Id;
                dto.qualifiedName = poco.qualifiedName;
                dto.relatedType = poco.relatedType.Select(x => x.Id).ToList();
                dto.shortName = poco.shortName;
                dto.sourceType = poco.sourceType?.Id;
                dto.targetType = poco.targetType.Select(x => x.Id).ToList();
                dto.textualRepresentation = poco.textualRepresentation.Select(x => x.Id).ToList();
                dto.unioningType = poco.unioningType.Select(x => x.Id).ToList();
            }

            return dto;
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
