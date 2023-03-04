// -------------------------------------------------------------------------------------------------
// <copyright file="TransitionFeatureMembershipExtensions.cs" company="RHEA System S.A.">
//
//   Copyright 2022-2023 RHEA System S.A.
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
    /// A static class that provides extension methods for the <see cref="TransitionFeatureMembership"/> class
    /// </summary>
    public static class TransitionFeatureMembershipExtensions
    {
        /// <summary>
        /// Updates the value properties of the <see cref="TransitionFeatureMembership"/> by setting the value equal to that of the dto
        /// Removes deleted objects from the reference properties and returns the unique identifiers
        /// of the objects that have been removed from contained properties
        /// </summary>
        /// <param name="poco">
        /// The <see cref="TransitionFeatureMembership"/> that is to be updated
        /// </param>
        /// <param name="dto">
        /// The DTO that is used to update the <see cref="TransitionFeatureMembership"/> with
        /// </param>
        /// <returns>
        /// The unique identifiers of the objects that have been removed from contained properties
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when the <paramref name="poco"/> or <paramref name="dto"/> is null
        /// </exception>
        public static IEnumerable<Guid> UpdateValueAndRemoveDeletedReferenceProperties(this Core.POCO.TransitionFeatureMembership poco, Core.DTO.TransitionFeatureMembership dto)
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

            poco.Kind = dto.Kind;

            poco.MemberName = dto.MemberName;

            poco.MemberShortName = dto.MemberShortName;

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

            poco.Visibility = dto.Visibility;


            return identifiersOfObjectsToDelete;
        }

        /// <summary>
        /// Updates the Reference properties of the <see cref="TransitionFeatureMembership"/> using the data (identifiers) encapsulated in the DTO
        /// and the provided cache to find the referenced object.
        /// </summary>
        /// <param name="poco">
        /// The <see cref="TransitionFeatureMembership"/> that is to be updated
        /// </param>
        /// <param name="dto">
        /// The DTO that is used to update the <see cref="TransitionFeatureMembership"/> with
        /// </param>
        /// <param name="cache">
        /// The <see cref="ConcurrentDictionary{Guid, Lazy{Core.POCO.IElement}}"/> that contains the
        /// <see cref="Core.POCO.IElement"/>s that are know and cached.
        /// </param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void UpdateReferenceProperties(this Core.POCO.TransitionFeatureMembership poco, Core.DTO.TransitionFeatureMembership dto, ConcurrentDictionary<Guid, Lazy<Core.POCO.IElement>> cache)
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

            if (cache.TryGetValue(dto.Feature, out lazyPoco))
            {
                poco.Feature = (Core.POCO.Feature)lazyPoco.Value;
            }
            else
            {
                poco.Feature = null;
            }

            if (cache.TryGetValue(dto.MemberElement, out lazyPoco))
            {
                poco.MemberElement = (IElement)lazyPoco.Value;
            }
            else
            {
                poco.MemberElement = null;
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

            if (cache.TryGetValue(dto.Type, out lazyPoco))
            {
                poco.Type = (Core.POCO.Type)lazyPoco.Value;
            }
            else
            {
                poco.Type = null;
            }

        }

        /// <summary>
        /// Creates a <see cref="Core.DTO.TransitionFeatureMembership"/> based on the provided POCO
        /// </summary>
        /// <param name="poco">
        /// The subject <see cref="Core.POCO.TransitionFeatureMembership"/> from which a DTO is to be created
        /// </param>
        /// <returns>
        /// An instance of <see cref="Core.POCO.TransitionFeatureMembership"/>
        /// </returns>
        public static Core.DTO.TransitionFeatureMembership ToDto(this Core.POCO.TransitionFeatureMembership poco)
        {
            var dto = new Core.DTO.TransitionFeatureMembership();

            dto.Id = poco.Id;
            dto.AliasIds = poco.AliasIds;
            dto.DeclaredName = poco.DeclaredName;
            dto.DeclaredShortName = poco.DeclaredShortName;
            dto.ElementId = poco.ElementId;
            dto.Feature = poco.Feature.Id;
            dto.IsImplied = poco.IsImplied;
            dto.IsImpliedIncluded = poco.IsImpliedIncluded;
            dto.Kind = poco.Kind;
            dto.MemberElement = poco.MemberElement.Id;
            dto.MemberName = poco.MemberName;
            dto.MemberShortName = poco.MemberShortName;
            dto.OwnedRelatedElement = poco.OwnedRelatedElement.Select(x => x.Id).ToList();
            dto.OwnedRelationship = poco.OwnedRelationship.Select(x => x.Id).ToList();
            dto.OwningRelatedElement = poco.OwningRelatedElement?.Id;
            dto.OwningRelationship = poco.OwningRelationship?.Id;
            dto.Source = poco.Source.Select(x => x.Id).ToList();
            dto.Target = poco.Target.Select(x => x.Id).ToList();
            dto.Type = poco.Type.Id;
            dto.Visibility = poco.Visibility;

            return dto;
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
