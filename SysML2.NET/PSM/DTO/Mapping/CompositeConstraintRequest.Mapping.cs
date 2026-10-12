// -------------------------------------------------------------------------------------------------
// <copyright file="CompositeConstraintRequest.Mapping.cs" company="Starion Group S.A.">
//
//   Copyright 2022-2025 Starion Group S.A.
//
//   Licensed under the Apache License, Version 2.0 (the "License");
//   you may not use this file except in compliance with the License.
//   You may obtain a copy of the License at
//
//        http://www.apache.org/licenses/LICENSE-2.0
//
//    Unless required by applicable law or agreed to in writing, software
//    distributed under the License is distributed on an "AS IS" BASIS,
//    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//    See the License for the specific language governing permissions and
//    limitations under the License.
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.PSM.DTO
{
    using System;
    using System.Linq;

    /// <summary>
    /// Declares the Platform Independent Model counterpart of the CompositeConstraint request model
    /// </summary>
    /// <remarks>
    /// The composed conditions are typed by the response family <see cref="IConstraint"/> rather than by
    /// <see cref="IConstraintRequest"/>, so the recursion crosses families.
    /// </remarks>
    public partial class CompositeConstraintRequest : IRequest<PIM.DTO.CompositeConstraint>
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

            this.Operator = ConstraintMapper.ToRequest(record.Operator);
            this.Constraint = [.. (record.Constraint ?? []).Select(ConstraintMapper.ToResponse)];
        }
    }
}
