// ------------------------------------------------------------------------------------------------
// <copyright file="IResponseOfTRecord.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.PSM.DTO
{
    /// <summary>
    /// Marker interface implemented by the response models that declare a Platform Independent Model counterpart
    /// </summary>
    /// <typeparam name="TRecord">
    /// The Platform Independent Model type that this response model corresponds to.
    /// </typeparam>
    /// <remarks>
    /// Responses without a counterpart, such as <see cref="Error"/>, implement the non generic <see cref="IResponse"/> only.
    /// </remarks>
    public interface IResponse<TRecord> : IResponse, IPimMappable<TRecord>
    {
    }
}
