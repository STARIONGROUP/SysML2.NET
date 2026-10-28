// ------------------------------------------------------------------------------------------------
// <copyright file="FlowUsageExtensions.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.Core.POCO.Systems.Flows
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using SysML2.NET.Decorators;

    using SysML2.NET.Core.POCO.Core.Features;
    using SysML2.NET.Core.POCO.Kernel.Interactions;

    /// <summary>
    /// The <see cref="FlowUsageExtensions" /> class provides extensions methods for
    /// the <see cref="IFlowUsage" /> interface
    /// </summary>
    internal static class FlowUsageExtensions
    {
        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <param name="flowUsageSubject">
        /// The subject <see cref="IFlowUsage" />
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [DerivedProperty(name: nameof(IFlowUsage.flowDefinition))]
        internal static List<IInteraction> ComputeFlowDefinition(this IFlowUsage flowUsageSubject)
        {
            return flowUsageSubject == null
                ? throw new ArgumentNullException(nameof(flowUsageSubject))
                : [..flowUsageSubject.ComputeType().OfType<IInteraction>()];
        }
    }
}
