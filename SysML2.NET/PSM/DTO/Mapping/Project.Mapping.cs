// -------------------------------------------------------------------------------------------------
// <copyright file="Project.Mapping.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.PSM.DTO
{
    using System;

    /// <summary>
    /// Declares the Platform Independent Model counterpart of the Project response model
    /// </summary>
    /// <remarks>
    /// The REST/HTTP PSM models no project member for <c>commits</c>, <c>queries</c>, <c>commitReferences</c>,
    /// <c>branches</c> or <c>tags</c>; a client reaches those through their own endpoints, so they are not carried.
    /// </remarks>
    public partial class Project : IResponse<PIM.DTO.Project>
    {
        /// <summary>
        /// Converts this model into its Platform Independent Model record
        /// </summary>
        /// <returns>The record carrying the members that the REST/HTTP PSM models.</returns>
        public PIM.DTO.Project ToPim()
        {
            return new PIM.DTO.Project
            {
                Id = this.Id,
                Alias = [.. this.Alias ?? []],
                Created = this.Created,
                DefaultBranch = this.DefaultBranch,
                Description = this.Description,
                Name = this.Name
            };
        }

        /// <summary>
        /// Populates this model from its Platform Independent Model record
        /// </summary>
        /// <param name="record">The record to read from.</param>
        /// <exception cref="ArgumentNullException">Thrown when the record is <c>null</c>.</exception>
        public void FromPim(PIM.DTO.Project record)
        {
            if (record == null)
            {
                throw new ArgumentNullException(nameof(record));
            }

            this.Id = record.Id;
            this.Alias = [.. record.Alias ?? []];
            this.Created = record.Created;
            this.DefaultBranch = record.DefaultBranch;
            this.Description = record.Description;
            this.Name = record.Name;
        }
    }
}
