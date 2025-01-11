// -------------------------------------------------------------------------------------------------
// <copyright file="StakeholderMembershipFactory.cs" company="Starion Group S.A.">
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

    /// <summary>
    /// The purpose of the <see cref="StakeholderMembershipFactory"/> is to create a new instance of a
    /// <see cref="Core.POCO.StakeholderMembership"/> based on a <see cref="Core.DTO.StakeholderMembership"/>
    /// </summary>
    public class StakeholderMembershipFactory
    {
        /// <summary>
        /// Creates an instance of the <see cref="Core.POCO.StakeholderMembership"/> and sets the value properties
        /// based on the DTO
        /// </summary>
        /// <param name="dto">
        /// The instance of the <see cref="Core.DTO.StakeholderMembership"/>
        /// </param>
        /// <returns>
        /// an instance of <see cref="Core.POCO.StakeholderMembership"/>
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// thrown when <paramref name="dto"/> is null
        /// </exception>
        public Core.POCO.StakeholderMembership Create(Core.DTO.StakeholderMembership dto)
        {
            if (dto == null)
            {
                throw new ArgumentNullException(nameof(dto), $"the {nameof(dto)} may not be null");
            }

            var poco = new Core.POCO.StakeholderMembership
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
