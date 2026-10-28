// ------------------------------------------------------------------------------------------------
// <copyright file="IDataRequest.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.Common
{
    /// <summary>
    /// DataRequest represents any entity that can be sent to the Systems Modeling API and Services as the payload of
    /// a data version
    /// </summary>
    /// <remarks>
    /// Realized by Element, ExternalDataRequest, ExternalRelationshipRequest and ProjectUsageRequest; it carries no
    /// identifier because a request may omit one.
    /// </remarks>
    public interface IDataRequest
    {
    }
}
