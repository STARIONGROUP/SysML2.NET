// ------------------------------------------------------------------------------------------------
// <copyright file="ExternalRelationshipRequest.Mapping.cs" company="Starion Group S.A.">
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
    /// Declares the Platform Independent Model counterpart of the ExternalRelationship request model
    /// </summary>
    public partial class ExternalRelationshipRequest : IRequest<PIM.DTO.ExternalRelationship>
    {
        /// <summary>
        /// Converts this model into its Platform Independent Model record
        /// </summary>
        /// <returns>The record carrying the members that the REST/HTTP PSM models.</returns>
        public PIM.DTO.ExternalRelationship ToPim()
        {
            return new PIM.DTO.ExternalRelationship
            {
                Id = this.Id ?? Guid.Empty,
                ElementEnd = this.ElementEnd ?? Guid.Empty,
                ExternalDataEnd = this.ExternalDataEnd ?? Guid.Empty,
                Language = this.Language,
                Specification = this.Specification
            };
        }

        /// <summary>
        /// Populates this model from its Platform Independent Model record
        /// </summary>
        /// <param name="record">The record to read from.</param>
        /// <exception cref="ArgumentNullException">Thrown when the record is <c>null</c>.</exception>
        public void FromPim(PIM.DTO.ExternalRelationship record)
        {
            if (record == null)
            {
                throw new ArgumentNullException(nameof(record));
            }

            this.Id = record.Id;
            this.ElementEnd = record.ElementEnd;
            this.ExternalDataEnd = record.ExternalDataEnd;
            this.Language = record.Language;
            this.Specification = record.Specification;
        }
    }
}
