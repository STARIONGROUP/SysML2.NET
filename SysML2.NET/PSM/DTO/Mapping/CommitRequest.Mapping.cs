// ------------------------------------------------------------------------------------------------
// <copyright file="CommitRequest.Mapping.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.PSM.DTO
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Declares the Platform Independent Model counterpart of the Commit request model
    /// </summary>
    /// <remarks>
    /// The request embeds its change while the Platform Independent Model commit references it, so the mapping yields
    /// the commit together with its data versions, bound by identifiers generated at mapping time.
    /// </remarks>
    public partial class CommitRequest : IRequest<PIM.DTO.Commit>
    {
        /// <summary>
        /// Converts this model into its Platform Independent Model record
        /// </summary>
        /// <returns>The commit, whose <c>change</c> references data versions that only <see cref="ToPimWithChange" /> returns.</returns>
        /// <exception cref="NotSupportedException">Thrown when a change payload has no Platform Independent Model counterpart.</exception>
        public PIM.DTO.Commit ToPim()
        {
            return this.ToPimWithChange().Commit;
        }

        /// <summary>
        /// Converts this model into its Platform Independent Model commit and the data versions that commit changes
        /// </summary>
        /// <returns>The commit, and its change in the order of the request.</returns>
        /// <exception cref="NotSupportedException">Thrown when a change payload has no Platform Independent Model counterpart.</exception>
        public (PIM.DTO.Commit Commit, IReadOnlyList<PIM.DTO.DataVersion> Change) ToPimWithChange()
        {
            var commit = new PIM.DTO.Commit
            {
                Id = Guid.NewGuid(),
                Alias = [.. this.Alias ?? []],
                Description = this.Description,
                Name = this.Name
            };

            var change = (this.Change ?? []).Select(request =>
            {
                var version = request.ToPim();
                version.Id = Guid.NewGuid();
                version.Commit = commit.Id;
                return version;
            }).ToList();

            commit.Change = [.. change.Select(version => version.Id)];

            return (commit, change);
        }

        /// <summary>
        /// Populates this model from its Platform Independent Model record, leaving the change empty
        /// </summary>
        /// <param name="record">The record to read from.</param>
        /// <exception cref="ArgumentNullException">Thrown when the record is <c>null</c>.</exception>
        public void FromPim(PIM.DTO.Commit record)
        {
            if (record == null)
            {
                throw new ArgumentNullException(nameof(record));
            }

            this.Alias = [.. record.Alias ?? []];
            this.Description = record.Description;
            this.Name = record.Name;
            this.Change = [];
        }

        /// <summary>
        /// Populates this model from its Platform Independent Model record and the data versions its change references
        /// </summary>
        /// <param name="record">The record to read from.</param>
        /// <param name="change">The data versions that resolve the record's <c>change</c> identifiers.</param>
        /// <exception cref="ArgumentNullException">Thrown when the record or the data versions are <c>null</c>.</exception>
        /// <exception cref="ArgumentException">Thrown when a <c>change</c> identifier resolves to no data version, or two data versions share an identifier.</exception>
        /// <exception cref="NotSupportedException">Thrown when a change payload has no REST/HTTP PSM request counterpart.</exception>
        public void FromPim(PIM.DTO.Commit record, IEnumerable<PIM.DTO.DataVersion> change)
        {
            if (change == null)
            {
                throw new ArgumentNullException(nameof(change));
            }

            this.FromPim(record);

            var versions = change.ToDictionary(version => version.Id);

            this.Change =
            [
                .. (record.Change ?? []).Select(identifier => versions.TryGetValue(identifier, out var version)
                ? PimMapper.Map<DataVersionRequest, PIM.DTO.DataVersion>(version)
                : throw new ArgumentException($"The change {identifier} resolves to no data version.", nameof(change)))
            ];
        }
    }
}
