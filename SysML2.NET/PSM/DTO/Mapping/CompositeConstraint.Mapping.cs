// ------------------------------------------------------------------------------------------------
// <copyright file="CompositeConstraint.Mapping.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.PSM.DTO
{
    using System;
    using System.Linq;

    /// <summary>
    /// Declares the Platform Independent Model counterpart of the CompositeConstraint response model
    /// </summary>
    public partial class CompositeConstraint : IResponse<PIM.DTO.CompositeConstraint>
    {
        /// <summary>
        /// Converts this model into its Platform Independent Model record
        /// </summary>
        /// <returns>The record carrying the members that the REST/HTTP PSM models.</returns>
        /// <exception cref="NotSupportedException">Thrown when a composed condition has no Platform Independent Model counterpart.</exception>
        public PIM.DTO.CompositeConstraint ToPim()
        {
            return new PIM.DTO.CompositeConstraint
            {
                Operator = ConstraintMapper.ToPim(this.Operator),
                Constraint = [.. (this.Constraint ?? []).Select(ConstraintMapper.ToPim)]
            };
        }

        /// <summary>
        /// Populates this model from its Platform Independent Model record
        /// </summary>
        /// <param name="record">The record to read from.</param>
        /// <exception cref="ArgumentNullException">Thrown when the record is <c>null</c>.</exception>
        /// <exception cref="NotSupportedException">Thrown when a composed condition has no REST/HTTP PSM counterpart.</exception>
        public void FromPim(PIM.DTO.CompositeConstraint record)
        {
            if (record == null)
            {
                throw new ArgumentNullException(nameof(record));
            }

            this.Operator = ConstraintMapper.ToResponse(record.Operator);
            this.Constraint = [.. (record.Constraint ?? []).Select(ConstraintMapper.ToResponse)];
        }
    }
}
