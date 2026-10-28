// ------------------------------------------------------------------------------------------------
// <copyright file="IData.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.Common
{
    /// <summary>
    /// Data represents any entity that can be created, updated, deleted, and queried by the Systems Modeling API
    /// and Services
    /// </summary>
    /// <remarks>
    /// Realized by Element, ExternalData, ExternalRelationship and ProjectUsage; a Record is not a Data.
    /// </remarks>
    public interface IData : IIdentified
    {
    }
}
