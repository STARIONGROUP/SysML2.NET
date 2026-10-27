// ------------------------------------------------------------------------------------------------
// <copyright file="PimMapper.cs" company="Starion Group S.A.">
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
