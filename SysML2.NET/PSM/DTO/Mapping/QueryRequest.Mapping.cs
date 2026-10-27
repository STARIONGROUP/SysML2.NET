// ------------------------------------------------------------------------------------------------
// <copyright file="QueryRequest.Mapping.cs" company="Starion Group S.A.">
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
    /// Declares the Platform Independent Model counterpart of the Query request model
    /// </summary>
    /// <remarks>
    /// The request models no member for <c>project</c>, <c>scope</c> or <c>orderBy</c>, so those are not carried.
    /// </remarks>
    public partial class QueryRequest : IRequest<PIM.DTO.Query>
    {
        /// <summary>
        /// Converts this model into its Platform Independent Model record
        /// </summary>
        /// <returns>The record carrying the members that the REST/HTTP PSM models.</returns>
        /// <exception cref="NotSupportedException">Thrown when the condition has no Platform Independent Model counterpart.</exception>
        public PIM.DTO.Query ToPim()
        {
            return new PIM.DTO.Query
            {
                Alias = [.. this.Alias ?? []],
                Description = this.Description,
                Name = this.Name,
                Select = [.. this.Select ?? []],
                Where = ConstraintMapper.ToPim(this.Where)
            };
        }

        /// <summary>
        /// Populates this model from its Platform Independent Model record
        /// </summary>
        /// <param name="record">The record to read from.</param>
        /// <exception cref="ArgumentNullException">Thrown when the record is <c>null</c>.</exception>
        /// <exception cref="NotSupportedException">Thrown when the condition has no REST/HTTP PSM counterpart.</exception>
        public void FromPim(PIM.DTO.Query record)
        {
            if (record == null)
            {
                throw new ArgumentNullException(nameof(record));
            }

            this.Alias = [.. record.Alias ?? []];
            this.Description = record.Description;
            this.Name = record.Name;
            this.Select = [.. record.Select ?? []];
            this.Where = ConstraintMapper.ToRequest(record.Where);
        }
    }
}
