// ------------------------------------------------------------------------------------------------
// <copyright file="Commit.Mapping.cs" company="Starion Group S.A.">
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
    /// Declares the Platform Independent Model counterpart of the Commit response model
    /// </summary>
    /// <remarks>
    /// The REST/HTTP PSM commit response models no <c>change</c> or <c>versionedData</c> member; <c>change</c> is
    /// carried by the commit request, and the versioned data is reached through its own endpoint.
    /// </remarks>
    public partial class Commit : IResponse<PIM.DTO.Commit>
    {
        /// <summary>
        /// Converts this model into its Platform Independent Model record
        /// </summary>
        /// <returns>The record carrying the members that the REST/HTTP PSM models.</returns>
        public PIM.DTO.Commit ToPim()
        {
            return new PIM.DTO.Commit
            {
                Id = this.Id,
                Alias = [.. this.Alias ?? []],
                Created = this.Created,
                Description = this.Description,
                Name = this.Name,
                OwningProject = this.OwningProject,
                PreviousCommits = [.. this.PreviousCommit ?? []]
            };
        }

        /// <summary>
        /// Populates this model from its Platform Independent Model record
        /// </summary>
        /// <param name="record">The record to read from.</param>
        /// <exception cref="ArgumentNullException">Thrown when the record is <c>null</c>.</exception>
        public void FromPim(PIM.DTO.Commit record)
        {
            if (record == null)
            {
                throw new ArgumentNullException(nameof(record));
            }

            this.Id = record.Id;
            this.Alias = [.. record.Alias ?? []];
            this.Created = record.Created;
            this.Description = record.Description;
            this.Name = record.Name;
            this.OwningProject = record.OwningProject;
            this.PreviousCommit = [.. record.PreviousCommits ?? []];
        }
    }
}
