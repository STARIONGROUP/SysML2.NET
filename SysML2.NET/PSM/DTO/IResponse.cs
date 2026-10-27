// ------------------------------------------------------------------------------------------------
// <copyright file="IResponse.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.PSM.DTO
{
    /// <summary>
    /// Marker interface implemented by the data transfer objects that are received from the Systems Modeling API and Services
    /// </summary>
    /// <remarks>
    /// Not every response carries an identifier, so this family is broader than <see cref="SysML2.NET.Common.IIdentified"/>.
    /// </remarks>
    public interface IResponse
    {
    }
}
