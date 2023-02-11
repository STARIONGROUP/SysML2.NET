// -------------------------------------------------------------------------------------------------
// <copyright file="ElementExtensions.cs" company="RHEA System S.A.">
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
	
	/// <summary>
	/// A static class that provides extension methods for the <see cref="Core.POCO.IElement"/> class
	/// </summary>
	public static class ElementExtensions
	{
        /// <summary>
        /// Updates the value properties of the <see cref="Core.POCO.IElement"/> by setting the value equal to that of the dto.
        /// Removes deleted objects from the reference properties and returns the unique identifiers
        /// of the objects that have been removed from containment properties
        /// </summary>
        /// <param name="poco">
        /// The <see cref="Core.POCO.IElement"/> that is to be updated
        /// </param>
        /// <param name="dto">
        /// The DTO that is used to update the <see cref="Core.POCO.IElement"/> with
        /// </param>
        /// <returns>
        /// The unique identifiers of the objects that have been removed from containment properties
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when the <paramref name="poco"/> or <paramref name="dto"/> is null
        /// </exception>
        public static IEnumerable<Guid> UpdateValueAndRemoveDeletedReferenceProperties(this Core.POCO.IElement poco, Core.DTO.IElement dto)
		{
			if (poco == null)
			{
				throw new ArgumentNullException(nameof(poco), $"the {nameof(poco)} may not be null");
			}

			if (dto == null)
			{
				throw new ArgumentNullException(nameof(dto), $"the {nameof(dto)} may not be null");
			}

			switch (poco)
			{
				case Core.POCO.PartDefinition partDefinition:
					return partDefinition.UpdateValueAndRemoveDeletedReferenceProperties(dto);
				default:
					throw new NotSupportedException($"{poco.GetType().Name} not yet supported");
			}
		}

        /// <summary>
        /// Updates the Reference properties of the <see cref="Core.POCO.IElement"/> using the data (identifiers) encapsulated in the DTO
        /// and the provided cache to find the referenced object.
        /// </summary>
        /// <param name="poco">
        /// The <see cref=""/> that is to be updated
        /// </param>
        /// <param name="dto">
        /// The DTO that is used to update the <see cref=""/> with
        /// </param>
        /// <param name="cache">
        /// The <see cref="ConcurrentDictionary{String, Lazy{Core.POCO.IElement}}"/> that contains the
        /// <see cref="Core.POCO.IElement"/>s that are know and cached.
        /// </param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void UpdateReferenceProperties(this Core.POCO.IElement poco, Core.DTO.IElement dto, ConcurrentDictionary<Guid, Lazy<Core.POCO.IElement>> cache)
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

			switch (poco)
			{
                case Core.POCO.AnnotatingElement annotatingElement:
                    annotatingElement.UpdateReferenceProperties((Core.DTO.AnnotatingElement)dto, cache);
                    break;
                case Core.POCO.Annotation annotation:
                    annotation.UpdateReferenceProperties((Core.DTO.Annotation)dto, cache);
                    break;
                case Core.POCO.Comment comment:
                    comment.UpdateReferenceProperties((Core.DTO.Comment)dto, cache);
                    break;
                case Core.POCO.Connector connector:
                    connector.UpdateReferenceProperties((Core.DTO.Connector)dto, cache);
                    break;
                case Core.POCO.Dependency dependency:
                    dependency.UpdateReferenceProperties((Core.DTO.Dependency)dto, cache);
                    break;
                case Core.POCO.Feature feature:
                    feature.UpdateReferenceProperties((Core.DTO.Feature)dto, cache);
                    break;
                case Core.POCO.NamespaceImport namespaceImport:
                    namespaceImport.UpdateReferenceProperties((Core.DTO.NamespaceImport)dto, cache);
                    break;
                case Core.POCO.LiteralInteger literalInteger:
                    literalInteger.UpdateReferenceProperties((Core.DTO.LiteralInteger)dto, cache);
                    break;
                case Core.POCO.LiteralRational literalRational:
                    literalRational.UpdateReferenceProperties((Core.DTO.LiteralRational)dto, cache);
                    break;
                default:
					throw new NotSupportedException($"{poco.GetType().Name} not yet supported");
			}
		}
	}
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
