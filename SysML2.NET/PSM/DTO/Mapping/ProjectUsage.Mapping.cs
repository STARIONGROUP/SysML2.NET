// ------------------------------------------------------------------------------------------------
// <copyright file="ProjectUsage.Mapping.cs" company="Starion Group S.A.">
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
    /// Declares the Platform Independent Model counterpart of the ProjectUsage response model
    /// </summary>
    public partial class ProjectUsage : IResponse<PIM.DTO.ProjectUsage>
    {
        /// <summary>
        /// Converts this model into its Platform Independent Model record
        /// </summary>
        /// <returns>The record carrying the members that the REST/HTTP PSM models.</returns>
        public PIM.DTO.ProjectUsage ToPim()
        {
            return new PIM.DTO.ProjectUsage
            {
                Id = this.Id,
                UsedProjectCommit = this.UsedCommit,
                UsedProject = this.UsedProject
            };
        }

        /// <summary>
        /// Populates this model from its Platform Independent Model record
        /// </summary>
        /// <param name="record">The record to read from.</param>
        /// <exception cref="ArgumentNullException">Thrown when the record is <c>null</c>.</exception>
        public void FromPim(PIM.DTO.ProjectUsage record)
        {
            if (record == null)
            {
                throw new ArgumentNullException(nameof(record));
            }

            this.Id = record.Id;
            this.UsedCommit = record.UsedProjectCommit;
            this.UsedProject = record.UsedProject;
        }
    }
}
