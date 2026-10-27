// ------------------------------------------------------------------------------------------------
// <copyright file="BranchRequest.Mapping.cs" company="Starion Group S.A.">
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
    /// Declares the Platform Independent Model counterpart of the Branch request model
    /// </summary>
    /// <remarks>
    /// The request carries no <c>referencedCommit</c>; Clause 7.1.2 declares <c>head</c> as redefining it.
    /// </remarks>
    public partial class BranchRequest : IRequest<PIM.DTO.Branch>
    {
        /// <summary>
        /// Converts this model into its Platform Independent Model record
        /// </summary>
        /// <returns>The record carrying the members that the REST/HTTP PSM models.</returns>
        public PIM.DTO.Branch ToPim()
        {
            return new PIM.DTO.Branch
            {
                Alias = [.. this.Alias ?? []],
                Description = this.Description,
                Head = this.Head ?? Guid.Empty,
                Name = this.Name
            };
        }

        /// <summary>
        /// Populates this model from its Platform Independent Model record
        /// </summary>
        /// <param name="record">The record to read from.</param>
        /// <exception cref="ArgumentNullException">Thrown when the record is <c>null</c>.</exception>
        public void FromPim(PIM.DTO.Branch record)
        {
            if (record == null)
            {
                throw new ArgumentNullException(nameof(record));
            }

            this.Alias = [.. record.Alias ?? []];
            this.Description = record.Description;
            this.Head = record.Head;
            this.Name = record.Name;
        }
    }
}
