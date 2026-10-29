// ------------------------------------------------------------------------------------------------
// <copyright file="DataVersionRequest.Mapping.cs" company="Starion Group S.A.">
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
    /// Declares the Platform Independent Model counterpart of the DataVersion request model
    /// </summary>
    /// <remarks>
    /// The request carries no identifier and no <c>commit</c>; <see cref="CommitRequest" /> supplies both when it maps its change.
    /// </remarks>
    public partial class DataVersionRequest : IRequest<PIM.DTO.DataVersion>
    {
        /// <summary>
        /// Converts this model into its Platform Independent Model record
        /// </summary>
        /// <returns>The record carrying the members that the REST/HTTP PSM models.</returns>
        /// <exception cref="NotSupportedException">Thrown when the payload has no Platform Independent Model counterpart.</exception>
        public PIM.DTO.DataVersion ToPim()
        {
            return new PIM.DTO.DataVersion
            {
                Alias = [.. this.Alias ?? []],
                Description = this.Description,
                Identity = this.Identity?.ToPim(),
                Name = this.Name,
                Payload = DataMapper.ToPim(this.Payload)
            };
        }

        /// <summary>
        /// Populates this model from its Platform Independent Model record
        /// </summary>
        /// <param name="record">The record to read from.</param>
        /// <exception cref="ArgumentNullException">Thrown when the record is <c>null</c>.</exception>
        /// <exception cref="NotSupportedException">Thrown when the payload has no REST/HTTP PSM request counterpart.</exception>
        public void FromPim(PIM.DTO.DataVersion record)
        {
            if (record == null)
            {
                throw new ArgumentNullException(nameof(record));
            }

            this.Alias = [.. record.Alias ?? []];
            this.Description = record.Description;
            this.Identity = PimMapper.Map<DataIdentityRequest, PIM.DTO.DataIdentity>(record.Identity);
            this.Name = record.Name;
            this.Payload = DataMapper.ToRequest(record.Payload);
        }
    }
}
