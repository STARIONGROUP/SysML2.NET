// -------------------------------------------------------------------------------------------------
// <copyright file="DataDifference.Mapping.cs" company="Starion Group S.A.">
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

    /// <summary>
    /// Declares the Platform Independent Model counterpart of the DataDifference response model
    /// </summary>
    /// <remarks>
    /// The REST/HTTP PSM carries whole data versions where the Platform Independent Model carries their identifiers,
    /// so a version reconstructed from a record holds its identifier alone.
    /// </remarks>
    public partial class DataDifference : IResponse<PIM.DTO.DataDifference>
    {
        /// <summary>
        /// Converts this model into its Platform Independent Model record
        /// </summary>
        /// <returns>The record carrying the members that the REST/HTTP PSM models.</returns>
        public PIM.DTO.DataDifference ToPim()
        {
            return new PIM.DTO.DataDifference
            {
                BaseData = this.BaseData?.Id,
                CompareData = this.CompareData?.Id
            };
        }

        /// <summary>
        /// Populates this model from its Platform Independent Model record
        /// </summary>
        /// <param name="record">The record to read from.</param>
        /// <exception cref="ArgumentNullException">Thrown when the record is <c>null</c>.</exception>
        public void FromPim(PIM.DTO.DataDifference record)
        {
            if (record == null)
            {
                throw new ArgumentNullException(nameof(record));
            }

            this.BaseData = record.BaseData.HasValue ? new DataVersion { Id = record.BaseData.Value } : null;
            this.CompareData = record.CompareData.HasValue ? new DataVersion { Id = record.CompareData.Value } : null;
        }
    }
}
