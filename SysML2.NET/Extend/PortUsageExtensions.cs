// ------------------------------------------------------------------------------------------------
// <copyright file="PortUsageExtensions.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.Core.POCO.Systems.Ports
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using SysML2.NET.Decorators;

    using SysML2.NET.Core.POCO.Core.Features;

    /// <summary>
    /// The <see cref="PortUsageExtensions" /> class provides extensions methods for
    /// the <see cref="IPortUsage" /> interface
    /// </summary>
    internal static class PortUsageExtensions
    {
        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <param name="portUsageSubject">
        /// The subject <see cref="IPortUsage" />
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [DerivedProperty(name: nameof(IPortUsage.portDefinition))]
        internal static List<IPortDefinition> ComputePortDefinition(this IPortUsage portUsageSubject)
        {
            return portUsageSubject == null
                ? throw new ArgumentNullException(nameof(portUsageSubject))
                : [.. FeatureExtensions.ComputeType(portUsageSubject).OfType<IPortDefinition>()];
        }
    }
}
