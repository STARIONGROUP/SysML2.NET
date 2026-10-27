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
    /// <remarks>
    /// Requests whose payload the Platform Independent Model cannot carry implement the non generic <see cref="IRequest"/> only.
    /// </remarks>
    public interface IRequest<TRecord> : IRequest, IPimMappable<TRecord>
    {
    }
}
