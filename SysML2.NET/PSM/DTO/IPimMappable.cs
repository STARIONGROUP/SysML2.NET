// ------------------------------------------------------------------------------------------------
// <copyright file="IPimMappable.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.PSM.DTO
{
    /// <summary>
    /// Declares the correspondence between a REST/HTTP PSM model and its Platform Independent Model record
    /// </summary>
    /// <typeparam name="TRecord">
    /// The Platform Independent Model type that this model corresponds to, per Systems Modeling API and Services, Table 9.
    /// </typeparam>
    /// <remarks>
    /// <see cref="FromPim"/> is an instance mutator because static abstract interface members are unavailable on netstandard2.1.
    /// </remarks>
    public interface IPimMappable<TRecord>
    {
        /// <summary>
        /// Converts this model into its Platform Independent Model record
        /// </summary>
        /// <returns>The record carrying the members that the REST/HTTP PSM models.</returns>
        TRecord ToPim();

        /// <summary>
        /// Populates this model from its Platform Independent Model record
        /// </summary>
        /// <param name="record">The record to read from.</param>
        /// <exception cref="System.ArgumentNullException">Thrown when the record is <c>null</c>.</exception>
        void FromPim(TRecord record);
    }
}
