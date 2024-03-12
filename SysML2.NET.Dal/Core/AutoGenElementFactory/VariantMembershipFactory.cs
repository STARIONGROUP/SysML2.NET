// -------------------------------------------------------------------------------------------------
// <copyright file="VariantMembershipFactory.cs" company="RHEA System S.A.">
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
    /// The purpose of the <see cref="VariantMembershipFactory"/> is to create a new instance of a
    /// <see cref="Core.POCO.VariantMembership"/> based on a <see cref="Core.DTO.VariantMembership"/>
    /// </summary>
    public class VariantMembershipFactory
    {
        /// <summary>
        /// Creates an instance of the <see cref="Core.POCO.VariantMembership"/> and sets the value properties
        /// based on the DTO
        /// </summary>
        /// <param name="dto">
        /// The instance of the <see cref="Core.DTO.VariantMembership"/>
        /// </param>
        /// <returns>
        /// an instance of <see cref="Core.POCO.VariantMembership"/>
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// thrown when <paramref name="dto"/> is null
        /// </exception>
        public Core.POCO.VariantMembership Create(Core.DTO.VariantMembership dto)
        {
            if (dto == null)
            {
                throw new ArgumentNullException(nameof(dto), $"the {nameof(dto)} may not be null");
            }

            var poco = new Core.POCO.VariantMembership
            {
                Id = dto.Id,
                AliasIds = dto.AliasIds,
                DeclaredName = dto.DeclaredName,
                DeclaredShortName = dto.DeclaredShortName,
                ElementId = dto.ElementId,
                IsImplied = dto.IsImplied,
                IsImpliedIncluded = dto.IsImpliedIncluded,
                MemberName = dto.MemberName,
                MemberShortName = dto.MemberShortName,
                Visibility = dto.Visibility,
            };

            return poco;
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
