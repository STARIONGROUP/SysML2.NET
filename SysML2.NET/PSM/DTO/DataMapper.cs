// ------------------------------------------------------------------------------------------------
// <copyright file="DataMapper.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.PSM.DTO
{
    using System;

    using SysML2.NET.Common;

    /// <summary>
    /// Maps the payload of a data version between the Platform Independent Model and the REST/HTTP PSM request
    /// </summary>
    /// <remarks>
    /// The payload alternatives are reached through <see cref="IData" /> and <see cref="IDataRequest" />,
    /// which carry no Platform Independent Model counterpart of their own, so the dispatch lives here.
    /// </remarks>
    public static class DataMapper
    {
        /// <summary>
        /// Maps a REST/HTTP PSM payload request onto its Platform Independent Model counterpart
        /// </summary>
        /// <param name="request">The payload to map.</param>
        /// <returns>The Platform Independent Model payload, or <c>null</c> when the payload is <c>null</c>.</returns>
        /// <exception cref="NotSupportedException">Thrown when the payload is of an alternative the Platform Independent Model does not model.</exception>
        public static IData ToPim(IDataRequest request)
        {
            return request switch
            {
                null => null,
                ExternalDataRequest externalData => externalData.ToPim(),
                ExternalRelationshipRequest externalRelationship => externalRelationship.ToPim(),
                ProjectUsageRequest projectUsage => projectUsage.ToPim(),
                IData element => element,
                _ => throw new NotSupportedException($"A {request.GetType().Name} has no Platform Independent Model counterpart.")
            };
        }

        /// <summary>
        /// Maps a Platform Independent Model payload onto its REST/HTTP PSM request alternative
        /// </summary>
        /// <param name="data">The payload to map.</param>
        /// <returns>The request payload, or <c>null</c> when the payload is <c>null</c>.</returns>
        /// <exception cref="NotSupportedException">Thrown when the payload is of a type the REST/HTTP PSM request does not model.</exception>
        public static IDataRequest ToRequest(IData data)
        {
            return data switch
            {
                null => null,
                PIM.DTO.ExternalData externalData => PimMapper.Map<ExternalDataRequest, PIM.DTO.ExternalData>(externalData),
                PIM.DTO.ExternalRelationship externalRelationship => PimMapper.Map<ExternalRelationshipRequest, PIM.DTO.ExternalRelationship>(externalRelationship),
                PIM.DTO.ProjectUsage projectUsage => PimMapper.Map<ProjectUsageRequest, PIM.DTO.ProjectUsage>(projectUsage),
                IDataRequest element => element,
                _ => throw new NotSupportedException($"A {data.GetType().Name} has no REST/HTTP PSM request counterpart.")
            };
        }
    }
}
