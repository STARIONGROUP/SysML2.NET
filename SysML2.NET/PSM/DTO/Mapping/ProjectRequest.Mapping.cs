// ------------------------------------------------------------------------------------------------
// <copyright file="ProjectRequest.Mapping.cs" company="Starion Group S.A.">
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
    /// Declares the Platform Independent Model counterpart of the Project request model
    /// </summary>
    public partial class ProjectRequest : IRequest<PIM.DTO.Project>
    {
        /// <summary>
        /// Converts this model into its Platform Independent Model record
        /// </summary>
        /// <returns>The record carrying the members that the REST/HTTP PSM models.</returns>
        public PIM.DTO.Project ToPim()
        {
            return new PIM.DTO.Project
            {
                Alias = [.. this.Alias ?? []],
                DefaultBranch = this.DefaultBranch ?? Guid.Empty,
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

            this.Alias = [.. record.Alias ?? []];
            this.DefaultBranch = record.DefaultBranch;
            this.Description = record.Description;
            this.Name = record.Name;
        }
    }
}
