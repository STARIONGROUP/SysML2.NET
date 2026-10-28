// ------------------------------------------------------------------------------------------------
// <copyright file="FlowDefinitionExtensions.cs" company="Starion Group S.A.">
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

    using SysML2.NET.Core.POCO.Core.Types;
    using SysML2.NET.Core.POCO.Systems.DefinitionAndUsage;
    using SysML2.NET.Decorators;

    /// <summary>
    /// The <see cref="FlowDefinitionExtensions" /> class provides extensions methods for
    /// the <see cref="IFlowDefinition" /> interface
    /// </summary>
    internal static class FlowDefinitionExtensions
    {
        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <param name="flowDefinitionSubject">
        /// The subject <see cref="IFlowDefinition" />
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [DerivedProperty(nameof(IFlowDefinition.flowEnd))]
        internal static List<IUsage> ComputeFlowEnd(this IFlowDefinition flowDefinitionSubject)
        {
            // IType.endFeature delegates through associationEnd back to flowEnd; the static call breaks that cycle.
            return flowDefinitionSubject == null
                ? throw new ArgumentNullException(nameof(flowDefinitionSubject))
                : [..flowDefinitionSubject.ComputeEndFeature().OfType<IUsage>()];
        }
    }
}
