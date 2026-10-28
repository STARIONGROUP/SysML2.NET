// ------------------------------------------------------------------------------------------------
// <copyright file="Project.Mapping.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
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
