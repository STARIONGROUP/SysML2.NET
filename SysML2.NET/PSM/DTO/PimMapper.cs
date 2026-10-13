// -------------------------------------------------------------------------------------------------
// <copyright file="PimMapper.cs" company="Starion Group S.A.">
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
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Maps Platform Independent Model records onto the REST/HTTP PSM models that declare them
    /// </summary>
    public static class PimMapper
    {
        /// <summary>
        /// Maps a Platform Independent Model record onto the REST/HTTP PSM model that declares it
        /// </summary>
        /// <typeparam name="TPsm">The PSM model to create.</typeparam>
        /// <typeparam name="TRecord">The Platform Independent Model type that <typeparamref name="TPsm"/> declares.</typeparam>
        /// <param name="record">The record to map.</param>
        /// <returns>The mapped model, or <c>null</c> when the record is <c>null</c>.</returns>
        public static TPsm Map<TPsm, TRecord>(TRecord record)
            where TPsm : class, IPimMappable<TRecord>, new()
            where TRecord : class
        {
            if (record == null)
            {
                return null;
            }

            var model = new TPsm();
            model.FromPim(record);

            return model;
        }

        /// <summary>
        /// Maps a sequence of Platform Independent Model records onto the REST/HTTP PSM models that declare them
        /// </summary>
        /// <typeparam name="TPsm">The PSM model to create.</typeparam>
        /// <typeparam name="TRecord">The Platform Independent Model type that <typeparamref name="TPsm"/> declares.</typeparam>
        /// <param name="records">The records to map.</param>
        /// <returns>The mapped models, in the order of the source sequence.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the sequence is <c>null</c>.</exception>
        public static IReadOnlyList<TPsm> MapAll<TPsm, TRecord>(IEnumerable<TRecord> records)
            where TPsm : class, IPimMappable<TRecord>, new()
            where TRecord : class
        {
            return records == null
                ? throw new ArgumentNullException(nameof(records))
                : [.. records.Select(Map<TPsm, TRecord>)];
        }
    }
}
