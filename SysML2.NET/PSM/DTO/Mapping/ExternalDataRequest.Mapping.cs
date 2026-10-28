// ------------------------------------------------------------------------------------------------
// <copyright file="ExternalDataRequest.Mapping.cs" company="Starion Group S.A.">
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
    /// Declares the Platform Independent Model counterpart of the ExternalData request model
    /// </summary>
    public partial class ExternalDataRequest : IRequest<PIM.DTO.ExternalData>
    {
        /// <summary>
        /// Converts this model into its Platform Independent Model record
        /// </summary>
        /// <returns>The record carrying the members that the REST/HTTP PSM models.</returns>
        public PIM.DTO.ExternalData ToPim()
        {
            return new PIM.DTO.ExternalData
            {
                Id = this.Id ?? Guid.Empty,
                ResourceIdentifier = this.ResourceIdentifier?.OriginalString
            };
        }

        /// <summary>
        /// Populates this model from its Platform Independent Model record
        /// </summary>
        /// <param name="record">The record to read from.</param>
        /// <exception cref="ArgumentNullException">Thrown when the record is <c>null</c>.</exception>
        public void FromPim(PIM.DTO.ExternalData record)
        {
            if (record == null)
            {
                throw new ArgumentNullException(nameof(record));
            }

            this.Id = record.Id;

            this.ResourceIdentifier = Uri.TryCreate(record.ResourceIdentifier, UriKind.RelativeOrAbsolute, out var resourceIdentifier)
                ? resourceIdentifier
                : null;
        }
    }
}
