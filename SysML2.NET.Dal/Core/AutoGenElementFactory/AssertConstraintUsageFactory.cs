// -------------------------------------------------------------------------------------------------
// <copyright file="AssertConstraintUsageFactory.cs" company="RHEA System S.A.">
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

    /// <summary>
    /// The purpose of the <see cref="AssertConstraintUsageFactory"/> is to create a new instance of a
    /// <see cref="Core.POCO.AssertConstraintUsage"/> based on a <see cref="Core.DTO.AssertConstraintUsage"/>
    /// </summary>
    public class AssertConstraintUsageFactory
    {
        /// <summary>
        /// Creates an instance of the <see cref="Core.POCO.AssertConstraintUsage"/> and sets the value properties
        /// based on the DTO
        /// </summary>
        /// <param name="dto">
        /// The instance of the <see cref="Core.DTO.AssertConstraintUsage"/>
        /// </param>
        /// <returns>
        /// an instance of <see cref="Core.POCO.AssertConstraintUsage"/>
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// thrown when <paramref name="dto"/> is null
        /// </exception>
        public Core.POCO.AssertConstraintUsage Create(Core.DTO.AssertConstraintUsage dto)
        {
            if (dto == null)
            {
                throw new ArgumentNullException(nameof(dto), $"the {nameof(dto)} may not be null");
            }

            var poco = new Core.POCO.AssertConstraintUsage
            {
                Id = dto.Id,
                AliasIds = dto.AliasIds,
                DeclaredName = dto.DeclaredName,
                DeclaredShortName = dto.DeclaredShortName,
                Direction = dto.Direction,
                ElementId = dto.ElementId,
                IsAbstract = dto.IsAbstract,
                IsComposite = dto.IsComposite,
                IsDerived = dto.IsDerived,
                IsEnd = dto.IsEnd,
                IsImpliedIncluded = dto.IsImpliedIncluded,
                IsIndividual = dto.IsIndividual,
                IsNegated = dto.IsNegated,
                IsOrdered = dto.IsOrdered,
                IsPortion = dto.IsPortion,
                IsReadOnly = dto.IsReadOnly,
                IsSufficient = dto.IsSufficient,
                IsUnique = dto.IsUnique,
                IsVariation = dto.IsVariation,
                PortionKind = dto.PortionKind,
            };

            return poco;
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
