// ------------------------------------------------------------------------------------------------
// <copyright file="DataVersion.Mapping.cs" company="Starion Group S.A.">
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
    /// Declares the Platform Independent Model counterpart of the DataVersion response model
    /// </summary>
    /// <remarks>
    /// The REST/HTTP PSM models no <c>commit</c> member, so the mandatory Platform Independent Model
    /// <c>DataVersion::commit</c> and the derived <c>project</c> are supplied by the route the version was reached through.
    /// </remarks>
    public partial class DataVersion : IResponse<PIM.DTO.DataVersion>
    {
        /// <summary>
        /// Converts this model into its Platform Independent Model record
        /// </summary>
        /// <returns>The record carrying the members that the REST/HTTP PSM models.</returns>
        public PIM.DTO.DataVersion ToPim()
        {
            return new PIM.DTO.DataVersion
            {
                Id = this.Id,
                Alias = [.. this.Alias ?? []],
                Description = this.Description,
                Identity = this.Identity?.ToPim(),
                Name = this.Name,
                Payload = this.Payload
            };
        }

        /// <summary>
        /// Populates this model from its Platform Independent Model record
        /// </summary>
        /// <param name="record">The record to read from.</param>
        /// <exception cref="ArgumentNullException">Thrown when the record is <c>null</c>.</exception>
        public void FromPim(PIM.DTO.DataVersion record)
        {
            if (record == null)
            {
                throw new ArgumentNullException(nameof(record));
            }

            this.Id = record.Id;
            this.Alias = [.. record.Alias ?? []];
            this.Description = record.Description;
            this.Identity = PimMapper.Map<DataIdentity, PIM.DTO.DataIdentity>(record.Identity);
            this.Name = record.Name;
            this.Payload = record.Payload;
        }
    }
}
