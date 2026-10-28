// ------------------------------------------------------------------------------------------------
// <copyright file="InterfaceUsageExtensions.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.Core.POCO.Systems.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using SysML2.NET.Decorators;

    using SysML2.NET.Core.POCO.Core.Features;

    /// <summary>
    /// The <see cref="InterfaceUsageExtensions" /> class provides extensions methods for
    /// the <see cref="IInterfaceUsage" /> interface
    /// </summary>
    internal static class InterfaceUsageExtensions
    {
        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <param name="interfaceUsageSubject">
        /// The subject <see cref="IInterfaceUsage" />
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [DerivedProperty(name: nameof(IInterfaceUsage.interfaceDefinition))]
        internal static List<IInterfaceDefinition> ComputeInterfaceDefinition(this IInterfaceUsage interfaceUsageSubject)
        {
            return interfaceUsageSubject == null
                ? throw new ArgumentNullException(nameof(interfaceUsageSubject))
                : [.. FeatureExtensions.ComputeType(interfaceUsageSubject).OfType<IInterfaceDefinition>()];
        }
    }
}
