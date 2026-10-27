// ------------------------------------------------------------------------------------------------
// <copyright file="Tag.Mapping.cs" company="Starion Group S.A.">
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
    /// Declares the Platform Independent Model counterpart of the Tag response model
    /// </summary>
    /// <remarks>
    /// Systems Modeling API and Services, Clause 7.1.2 declares <c>taggedCommit</c> as redefining
    /// <c>referencedCommit</c>, so the one Platform Independent Model member supplies both REST/HTTP PSM members.
    /// </remarks>
    public partial class Tag : IResponse<PIM.DTO.Tag>
    {
        /// <summary>
        /// Converts this model into its Platform Independent Model record
        /// </summary>
        /// <returns>The record carrying the members that the REST/HTTP PSM models.</returns>
        public PIM.DTO.Tag ToPim()
        {
            return new PIM.DTO.Tag
            {
                Id = this.Id,
                Alias = [.. this.Alias ?? []],
                Created = this.Created,
                Deleted = this.Deleted ?? default,
                Description = this.Description,
                Name = this.Name,
                OwningProject = this.OwningProject,
                TaggedCommit = this.TaggedCommit
            };
        }

        /// <summary>
        /// Populates this model from its Platform Independent Model record
        /// </summary>
        /// <param name="record">The record to read from.</param>
        /// <exception cref="ArgumentNullException">Thrown when the record is <c>null</c>.</exception>
        public void FromPim(PIM.DTO.Tag record)
        {
            if (record == null)
            {
                throw new ArgumentNullException(nameof(record));
            }

            this.Id = record.Id;
            this.Alias = [.. record.Alias ?? []];
            this.Created = record.Created;
            this.Deleted = record.Deleted;
            this.Description = record.Description;
            this.Name = record.Name;
            this.OwningProject = record.OwningProject;
            this.ReferencedCommit = record.TaggedCommit;
            this.TaggedCommit = record.TaggedCommit;
        }
    }
}
