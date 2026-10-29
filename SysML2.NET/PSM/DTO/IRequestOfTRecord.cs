// ------------------------------------------------------------------------------------------------
// <copyright file="IRequestOfTRecord.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.PSM.DTO
{
    /// <summary>
    /// Marker interface implemented by the request models that declare a Platform Independent Model counterpart
    /// </summary>
    /// <typeparam name="TRecord">
    /// The Platform Independent Model type that this request model corresponds to.
    /// </typeparam>
    public interface IRequest<TRecord> : IRequest, IPimMappable<TRecord>
    {
    }
}
