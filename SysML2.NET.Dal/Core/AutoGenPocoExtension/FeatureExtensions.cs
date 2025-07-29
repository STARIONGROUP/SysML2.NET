// -------------------------------------------------------------------------------------------------
// <copyright file="FeatureExtensions.cs" company="Starion Group S.A.">
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

    using Core.POCO;

    /// <summary>
    /// A static class that provides extension methods for the <see cref="Feature"/> class
    /// </summary>
    public static class FeatureExtensions
    {
        /// <summary>
        /// Updates the value properties of the <see cref="Feature"/> by setting the value equal to that of the dto
        /// Removes deleted objects from the reference properties and returns the unique identifiers
        /// of the objects that have been removed from contained properties
        /// </summary>
        /// <param name="poco">
        /// The <see cref="Feature"/> that is to be updated
        /// </param>
        /// <param name="dto">
        /// The DTO that is used to update the <see cref="Feature"/> with
        /// </param>
        /// <returns>
        /// The unique identifiers of the objects that have been removed from contained properties
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when the <paramref name="poco"/> or <paramref name="dto"/> is null
        /// </exception>
        public static IEnumerable<Guid> UpdateValueAndRemoveDeletedReferenceProperties(this Core.POCO.Feature poco, Core.DTO.Feature dto)
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
                poco.OwnedRelationship.Remove(poco.OwnedRelationship.Single(x => x.Id == identifier));
            }
            identifiersOfObjectsToDelete.AddRange(ownedRelationshipToDelete);


            return identifiersOfObjectsToDelete;
        }

        /// <summary>
        /// Updates the Reference properties of the <see cref="Feature"/> using the data (identifiers) encapsulated in the DTO
        /// and the provided cache to find the referenced object.
        /// </summary>
        /// <param name="poco">
        /// The <see cref="Feature"/> that is to be updated
        /// </param>
        /// <param name="dto">
        /// The DTO that is used to update the <see cref="Feature"/> with
        /// </param>
        /// <param name="cache">
        /// The <see cref="ConcurrentDictionary{Guid, Lazy{Core.POCO.IElement}}"/> that contains the
        /// <see cref="Core.POCO.IElement"/>s that are know and cached.
        /// </param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void UpdateReferenceProperties(this Core.POCO.Feature poco, Core.DTO.Feature dto, ConcurrentDictionary<Guid, Lazy<Core.POCO.IElement>> cache)
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

            var ownedRelationshipToAdd = dto.OwnedRelationship.Except(poco.OwnedRelationship.Select(x => x.Id));
            foreach (var identifier in ownedRelationshipToAdd)
            {
                if (cache.TryGetValue(identifier, out lazyPoco))
                {
                    poco.OwnedRelationship.Add((IRelationship)lazyPoco.Value);
                }
            }

            if (dto.OwningRelationship.HasValue && cache.TryGetValue(dto.OwningRelationship.Value, out lazyPoco))
            {
                poco.OwningRelationship = (IRelationship)lazyPoco.Value;
            }
            else
            {
                poco.OwningRelationship = null;
            }

        }

        /// <summary>
        /// Creates a <see cref="Core.DTO.Feature"/> based on the provided POCO
        /// </summary>
        /// <param name="poco">
        /// The subject <see cref="Core.POCO.Feature"/> from which a DTO is to be created
        /// </param>
        /// <returns>
        /// An instance of <see cref="Core.POCO.Feature"/>
        /// </returns>
        public static Core.DTO.Feature ToDto(this Core.POCO.Feature poco)
        {
            var dto = new Core.DTO.Feature();

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

            return dto;
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
