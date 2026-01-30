// -------------------------------------------------------------------------------------------------
// <copyright file="FlowEndFactory.cs" company="Starion Group S.A.">
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

    /// <summary>
    /// The purpose of the <see cref="FlowEndFactory"/> is to create a new instance of a
    /// <see cref="Core.POCO.Kernel.Interactions.FlowEnd"/> based on a <see cref="Core.DTO.Kernel.Interactions.FlowEnd"/>
    /// </summary>
    public static class FlowEndFactory
    {
        /// <summary>
        /// Creates an instance of the <see cref="Core.POCO.Kernel.Interactions.FlowEnd"/> and sets the value properties
        /// based on the DTO
        /// </summary>
        /// <param name="dto">
        /// The instance of the <see cref="Core.DTO.Kernel.Interactions.FlowEnd"/>
        /// </param>
        /// <returns>
        /// an instance of <see cref="Core.POCO.Kernel.Interactions.FlowEnd"/>
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// thrown when <paramref name="dto"/> is null
        /// </exception>
        public static Core.POCO.Kernel.Interactions.FlowEnd Create(Core.DTO.Kernel.Interactions.FlowEnd dto)
        {
            if (dto == null)
            {
                throw new ArgumentNullException(nameof(dto), $"the {nameof(dto)} may not be null");
            }

            var poco = new Core.POCO.Kernel.Interactions.FlowEnd();

            poco.Id = dto.Id;
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

            return poco;
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
