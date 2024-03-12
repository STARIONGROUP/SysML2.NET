// -------------------------------------------------------------------------------------------------
// <copyright file="PortConjugationExtensions.cs" company="RHEA System S.A.">
//
//   Copyright 2022-2024 RHEA System S.A.
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

    using Core.POCO;

    /// <summary>
    /// A static class that provides extension methods for the <see cref="PortConjugation"/> class
    /// </summary>
    public static class PortConjugationExtensions
    {
        /// <summary>
        /// Updates the value properties of the <see cref="PortConjugation"/> by setting the value equal to that of the dto
        /// Removes deleted objects from the reference properties and returns the unique identifiers
        /// of the objects that have been removed from contained properties
        /// </summary>
        /// <param name="poco">
        /// The <see cref="PortConjugation"/> that is to be updated
        /// </param>
        /// <param name="dto">
        /// The DTO that is used to update the <see cref="PortConjugation"/> with
        /// </param>
        /// <returns>
        /// The unique identifiers of the objects that have been removed from contained properties
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when the <paramref name="poco"/> or <paramref name="dto"/> is null
        /// </exception>
        public static IEnumerable<Guid> UpdateValueAndRemoveDeletedReferenceProperties(this Core.POCO.PortConjugation poco, Core.DTO.PortConjugation dto)
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

            var sourceToDelete = poco.Source.Select(x => x.Id).Except(dto.Source);
            foreach (var identifier in sourceToDelete)
            {
                poco.Source.Remove(poco.Source.Single(x => x.Id == identifier));
            }

            var targetToDelete = poco.Target.Select(x => x.Id).Except(dto.Target);
            foreach (var identifier in targetToDelete)
            {
                poco.Target.Remove(poco.Target.Single(x => x.Id == identifier));
            }


            return identifiersOfObjectsToDelete;
        }

        /// <summary>
        /// Updates the Reference properties of the <see cref="PortConjugation"/> using the data (identifiers) encapsulated in the DTO
        /// and the provided cache to find the referenced object.
        /// </summary>
        /// <param name="poco">
        /// The <see cref="PortConjugation"/> that is to be updated
        /// </param>
        /// <param name="dto">
        /// The DTO that is used to update the <see cref="PortConjugation"/> with
        /// </param>
        /// <param name="cache">
        /// The <see cref="ConcurrentDictionary{Guid, Lazy{Core.POCO.IElement}}"/> that contains the
        /// <see cref="Core.POCO.IElement"/>s that are know and cached.
        /// </param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void UpdateReferenceProperties(this Core.POCO.PortConjugation poco, Core.DTO.PortConjugation dto, ConcurrentDictionary<Guid, Lazy<Core.POCO.IElement>> cache)
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

            Lazy<Core.POCO.IElement> lazyPoco;

            if (cache.TryGetValue(dto.ConjugatedType, out lazyPoco))
            {
                poco.ConjugatedType = (Core.POCO.Type)lazyPoco.Value;
            }
            else
            {
                poco.ConjugatedType = null;
            }

            if (cache.TryGetValue(dto.OriginalPortDefinition, out lazyPoco))
            {
                poco.OriginalPortDefinition = (Core.POCO.PortDefinition)lazyPoco.Value;
            }
            else
            {
                poco.OriginalPortDefinition = null;
            }

            if (cache.TryGetValue(dto.OriginalType, out lazyPoco))
            {
                poco.OriginalType = (Core.POCO.Type)lazyPoco.Value;
            }
            else
            {
                poco.OriginalType = null;
            }

            var ownedRelatedElementToAdd = dto.OwnedRelatedElement.Except(poco.OwnedRelatedElement.Select(x => x.Id));
            foreach (var identifier in ownedRelatedElementToAdd)
            {
                if (cache.TryGetValue(identifier, out lazyPoco))
                {
                    poco.OwnedRelatedElement.Add((IElement)lazyPoco.Value);
                }
            }

            var ownedRelationshipToAdd = dto.OwnedRelationship.Except(poco.OwnedRelationship.Select(x => x.Id));
            foreach (var identifier in ownedRelationshipToAdd)
            {
                if (cache.TryGetValue(identifier, out lazyPoco))
                {
                    poco.OwnedRelationship.Add((IRelationship)lazyPoco.Value);
                }
            }

            if (dto.OwningRelatedElement.HasValue && cache.TryGetValue(dto.OwningRelatedElement.Value, out lazyPoco))
            {
                poco.OwningRelatedElement = (IElement)lazyPoco.Value;
            }
            else
            {
                poco.OwningRelatedElement = null;
            }

            if (dto.OwningRelationship.HasValue && cache.TryGetValue(dto.OwningRelationship.Value, out lazyPoco))
            {
                poco.OwningRelationship = (IRelationship)lazyPoco.Value;
            }
            else
            {
                poco.OwningRelationship = null;
            }

            var sourceToAdd = dto.Source.Except(poco.Source.Select(x => x.Id));
            foreach (var identifier in sourceToAdd)
            {
                if (cache.TryGetValue(identifier, out lazyPoco))
                {
                    poco.Source.Add((IElement)lazyPoco.Value);
                }
            }

            var targetToAdd = dto.Target.Except(poco.Target.Select(x => x.Id));
            foreach (var identifier in targetToAdd)
            {
                if (cache.TryGetValue(identifier, out lazyPoco))
                {
                    poco.Target.Add((IElement)lazyPoco.Value);
                }
            }

        }

        /// <summary>
        /// Creates a <see cref="Core.DTO.PortConjugation"/> based on the provided POCO
        /// </summary>
        /// <param name="poco">
        /// The subject <see cref="Core.POCO.PortConjugation"/> from which a DTO is to be created
        /// </param>
        /// <returns>
        /// An instance of <see cref="Core.POCO.PortConjugation"/>
        /// </returns>
        public static Core.DTO.PortConjugation ToDto(this Core.POCO.PortConjugation poco)
        {
            var dto = new Core.DTO.PortConjugation();

            dto.Id = poco.Id;
            dto.AliasIds = poco.AliasIds;
            dto.ConjugatedType = poco.ConjugatedType.Id;
            dto.DeclaredName = poco.DeclaredName;
            dto.DeclaredShortName = poco.DeclaredShortName;
            dto.ElementId = poco.ElementId;
            dto.IsImplied = poco.IsImplied;
            dto.IsImpliedIncluded = poco.IsImpliedIncluded;
            dto.OriginalPortDefinition = poco.OriginalPortDefinition.Id;
            dto.OriginalType = poco.OriginalType.Id;
            dto.OwnedRelatedElement = poco.OwnedRelatedElement.Select(x => x.Id).ToList();
            dto.OwnedRelationship = poco.OwnedRelationship.Select(x => x.Id).ToList();
            dto.OwningRelatedElement = poco.OwningRelatedElement?.Id;
            dto.OwningRelationship = poco.OwningRelationship?.Id;
            dto.Source = poco.Source.Select(x => x.Id).ToList();
            dto.Target = poco.Target.Select(x => x.Id).ToList();

            return dto;
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
