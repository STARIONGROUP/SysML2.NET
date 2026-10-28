// ------------------------------------------------------------------------------------------------
// <copyright file="PrimitiveConstraintRequest.Mapping.cs" company="Starion Group S.A.">
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
    /// Declares the Platform Independent Model counterpart of the PrimitiveConstraint request model
    /// </summary>
    public partial class PrimitiveConstraintRequest : IRequest<PIM.DTO.PrimitiveConstraint>
    {
        /// <summary>
        /// Converts this model into its Platform Independent Model record
        /// </summary>
        /// <returns>The record carrying the members that the REST/HTTP PSM models.</returns>
        public PIM.DTO.PrimitiveConstraint ToPim()
        {
            return new PIM.DTO.PrimitiveConstraint
            {
                Property = this.Property,
                Operator = ConstraintMapper.ToPim(this.Operator),
                Inverse = this.Inverse ?? false,
                Value = [.. (this.Value ?? []).Select(ConstraintMapper.ToPimValue)]
            };
        }

        /// <summary>
        /// Populates this model from its Platform Independent Model record
        /// </summary>
        /// <param name="record">The record to read from.</param>
        /// <exception cref="ArgumentNullException">Thrown when the record is <c>null</c>.</exception>
        public void FromPim(PIM.DTO.PrimitiveConstraint record)
        {
            if (record == null)
            {
                throw new ArgumentNullException(nameof(record));
            }

            this.Property = record.Property;
            this.Operator = ConstraintMapper.ToRequest(record.Operator);
            this.Inverse = record.Inverse;
            this.Value = [.. (record.Value ?? []).Select(ConstraintMapper.ToPsmValue)];
        }
    }
}
