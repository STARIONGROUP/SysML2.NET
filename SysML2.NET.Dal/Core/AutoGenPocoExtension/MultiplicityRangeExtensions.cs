// -------------------------------------------------------------------------------------------------
// <copyright file="MultiplicityRangeExtensions.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.Dal
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;

    using Core.POCO.Kernel.Multiplicities;
    using SysML2.NET.Core.POCO.Root.Elements;

    /// <summary>
    /// A static class that provides extension methods for the <see cref="Core.POCO.Kernel.Multiplicities.MultiplicityRange"/> class
    /// </summary>
    public static class MultiplicityRangeExtensions
    {
        /// <summary>
        /// Updates the value properties of the <see cref="Core.POCO.Kernel.Multiplicities.MultiplicityRange"/> by setting the value equal to that of the dto
        /// Removes deleted objects from the reference properties and returns the unique identifiers
        /// of the objects that have been removed from contained properties
        /// </summary>
        /// <param name="poco">
        /// The <see cref="Core.POCO.Kernel.Multiplicities.MultiplicityRange"/> that is to be updated
        /// </param>
        /// <param name="dto">
        /// The DTO that is used to update the <see cref="Core.DTO.Kernel.Multiplicities.MultiplicityRange"/> with
        /// </param>
        /// <returns>
        /// The unique identifiers of the objects that have been removed from contained properties
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when the <paramref name="poco"/> or <paramref name="dto"/> is null
        /// </exception>
        public static IEnumerable<Guid> UpdateValueAndRemoveDeletedReferenceProperties(this Core.POCO.Kernel.Multiplicities.MultiplicityRange poco, Core.DTO.Kernel.Multiplicities.MultiplicityRange dto)
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

            poco.Direction = dto.Direction;

            poco.ElementId = dto.ElementId;

            poco.IsAbstract = dto.IsAbstract;

            poco.IsComposite = dto.IsComposite;

            poco.IsConstant = dto.IsConstant;

            poco.IsDerived = dto.IsDerived;

            poco.IsEnd = dto.IsEnd;

            poco.IsImpliedIncluded = dto.IsImpliedIncluded;

            poco.IsOrdered = dto.IsOrdered;

            poco.IsPortion = dto.IsPortion;

            poco.IsSufficient = dto.IsSufficient;

            poco.IsUnique = dto.IsUnique;

            poco.IsVariable = dto.IsVariable;

            var ownedRelationshipToDelete = poco.OwnedRelationship.Select(x => x.Id).Except(dto.OwnedRelationship);

            foreach (var identifier in ownedRelationshipToDelete)
            {
                ((IContainedElement)poco).OwnedRelationship.Remove(poco.OwnedRelationship.Single(x => x.Id == identifier));
            }

            identifiersOfObjectsToDelete.AddRange(ownedRelationshipToDelete);


            return identifiersOfObjectsToDelete;
        }

        /// <summary>
        /// Updates the Reference properties of the <see cref="Core.POCO.Kernel.Multiplicities.MultiplicityRange"/> using the data (identifiers) encapsulated in the DTO
        /// and the provided cache to find the referenced object.
        /// </summary>
        /// <param name="poco">
        /// The <see cref="Core.POCO.Kernel.Multiplicities.MultiplicityRange"/> that is to be updated
        /// </param>
        /// <param name="dto">
        /// The DTO that is used to update the <see cref="Core.DTO.Kernel.Multiplicities.MultiplicityRange"/> with
        /// </param>
        /// <param name="cache">
        /// The <see cref="ConcurrentDictionary{Guid, Lazy}"/> that contains the
        /// <see cref="Core.POCO.Root.Elements.IElement"/>s that are know and cached.
        /// </param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void UpdateReferenceProperties(this Core.POCO.Kernel.Multiplicities.MultiplicityRange poco, Core.DTO.Kernel.Multiplicities.MultiplicityRange dto, ConcurrentDictionary<Guid, Lazy<Core.POCO.Root.Elements.IElement>> cache)
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

            var ownedRelationshipToAdd = dto.OwnedRelationship.Except(poco.OwnedRelationship.Select(x => x.Id));

            foreach (var identifier in ownedRelationshipToAdd)
            {
                if (cache.TryGetValue(identifier, out lazyPoco))
                {
                    ((IContainedElement)poco).OwnedRelationship.Add((Core.POCO.Root.Elements.IRelationship)lazyPoco.Value);
                }
            }

            if (dto.OwningRelationship.HasValue && cache.TryGetValue(dto.OwningRelationship.Value, out lazyPoco))
            {
                ((IContainedElement)poco).OwningRelationship = (Core.POCO.Root.Elements.IRelationship)lazyPoco.Value;
            }
            else
            {
                ((IContainedElement)poco).OwningRelationship = null;
            }

        }

        /// <summary>
        /// Creates a <see cref="Core.DTO.Kernel.Multiplicities.MultiplicityRange"/> based on the provided POCO
        /// </summary>
        /// <param name="poco">
        /// The subject <see cref="Core.POCO.Kernel.Multiplicities.MultiplicityRange"/> from which a DTO is to be created
        /// </param>
        /// <param name="includeDerivedProperties">
        /// Asserts that derived properties should also be mapped during the creation of the <see cref="Core.DTO.Kernel.Multiplicities.MultiplicityRange"/>
        /// </param>
        /// <returns>
        /// An instance of <see cref="Core.POCO.Kernel.Multiplicities.MultiplicityRange"/>
        /// </returns>
        public static Core.DTO.Kernel.Multiplicities.MultiplicityRange ToDto(this Core.POCO.Kernel.Multiplicities.MultiplicityRange poco, bool includeDerivedProperties = false)
        {
            var dto = new Core.DTO.Kernel.Multiplicities.MultiplicityRange();

            dto.Id = poco.Id;
            dto.AliasIds = poco.AliasIds;
            dto.DeclaredName = poco.DeclaredName;
            dto.DeclaredShortName = poco.DeclaredShortName;
            dto.Direction = poco.Direction;
            dto.ElementId = poco.ElementId;
            dto.IsAbstract = poco.IsAbstract;
            dto.IsComposite = poco.IsComposite;
            dto.IsConstant = poco.IsConstant;
            dto.IsDerived = poco.IsDerived;
            dto.IsEnd = poco.IsEnd;
            dto.IsImpliedIncluded = poco.IsImpliedIncluded;
            dto.IsOrdered = poco.IsOrdered;
            dto.IsPortion = poco.IsPortion;
            dto.IsSufficient = poco.IsSufficient;
            dto.IsUnique = poco.IsUnique;
            dto.IsVariable = poco.IsVariable;
            dto.OwnedRelationship = poco.OwnedRelationship.Select(x => x.Id).ToList();
            dto.OwningRelationship = poco.OwningRelationship?.Id;

            if (includeDerivedProperties)
            {
                dto.bound = poco.bound.Select(x => x.Id).ToList();
                dto.chainingFeature = poco.chainingFeature.Select(x => x.Id).ToList();
                dto.crossFeature = poco.crossFeature?.Id;
                dto.differencingType = poco.differencingType.Select(x => x.Id).ToList();
                dto.directedFeature = poco.directedFeature.Select(x => x.Id).ToList();
                dto.documentation = poco.documentation.Select(x => x.Id).ToList();
                dto.endFeature = poco.endFeature.Select(x => x.Id).ToList();
                dto.endOwningType = poco.endOwningType?.Id;
                dto.feature = poco.feature.Select(x => x.Id).ToList();
                dto.featureMembership = poco.featureMembership.Select(x => x.Id).ToList();
                dto.featureTarget = poco.featureTarget.Id;
                dto.featuringType = poco.featuringType.Select(x => x.Id).ToList();
                dto.importedMembership = poco.importedMembership.Select(x => x.Id).ToList();
                dto.inheritedFeature = poco.inheritedFeature.Select(x => x.Id).ToList();
                dto.inheritedMembership = poco.inheritedMembership.Select(x => x.Id).ToList();
                dto.input = poco.input.Select(x => x.Id).ToList();
                dto.intersectingType = poco.intersectingType.Select(x => x.Id).ToList();
                dto.isConjugated = poco.isConjugated;
                dto.isLibraryElement = poco.isLibraryElement;
                dto.lowerBound = poco.lowerBound?.Id;
                dto.member = poco.member.Select(x => x.Id).ToList();
                dto.membership = poco.membership.Select(x => x.Id).ToList();
                dto.multiplicity = poco.multiplicity?.Id;
                dto.name = poco.name;
                dto.output = poco.output.Select(x => x.Id).ToList();
                dto.ownedAnnotation = poco.ownedAnnotation.Select(x => x.Id).ToList();
                dto.ownedConjugator = poco.ownedConjugator?.Id;
                dto.ownedCrossSubsetting = poco.ownedCrossSubsetting?.Id;
                dto.ownedDifferencing = poco.ownedDifferencing.Select(x => x.Id).ToList();
                dto.ownedDisjoining = poco.ownedDisjoining.Select(x => x.Id).ToList();
                dto.ownedElement = poco.ownedElement.Select(x => x.Id).ToList();
                dto.ownedEndFeature = poco.ownedEndFeature.Select(x => x.Id).ToList();
                dto.ownedFeature = poco.ownedFeature.Select(x => x.Id).ToList();
                dto.ownedFeatureChaining = poco.ownedFeatureChaining.Select(x => x.Id).ToList();
                dto.ownedFeatureInverting = poco.ownedFeatureInverting.Select(x => x.Id).ToList();
                dto.ownedFeatureMembership = poco.ownedFeatureMembership.Select(x => x.Id).ToList();
                dto.ownedImport = poco.ownedImport.Select(x => x.Id).ToList();
                dto.ownedIntersecting = poco.ownedIntersecting.Select(x => x.Id).ToList();
                dto.ownedMember = poco.ownedMember.Select(x => x.Id).ToList();
                dto.ownedMembership = poco.ownedMembership.Select(x => x.Id).ToList();
                dto.ownedRedefinition = poco.ownedRedefinition.Select(x => x.Id).ToList();
                dto.ownedReferenceSubsetting = poco.ownedReferenceSubsetting?.Id;
                dto.ownedSpecialization = poco.ownedSpecialization.Select(x => x.Id).ToList();
                dto.ownedSubsetting = poco.ownedSubsetting.Select(x => x.Id).ToList();
                dto.ownedTypeFeaturing = poco.ownedTypeFeaturing.Select(x => x.Id).ToList();
                dto.ownedTyping = poco.ownedTyping.Select(x => x.Id).ToList();
                dto.ownedUnioning = poco.ownedUnioning.Select(x => x.Id).ToList();
                dto.owner = poco.owner?.Id;
                dto.owningFeatureMembership = poco.owningFeatureMembership?.Id;
                dto.owningMembership = poco.owningMembership?.Id;
                dto.owningNamespace = poco.owningNamespace?.Id;
                dto.owningType = poco.owningType?.Id;
                dto.qualifiedName = poco.qualifiedName;
                dto.shortName = poco.shortName;
                dto.textualRepresentation = poco.textualRepresentation.Select(x => x.Id).ToList();
                dto.type = poco.type.Select(x => x.Id).ToList();
                dto.unioningType = poco.unioningType.Select(x => x.Id).ToList();
                dto.upperBound = poco.upperBound.Id;
            }

            return dto;
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
