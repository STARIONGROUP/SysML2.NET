// -------------------------------------------------------------------------------------------------
// <copyright file="FlowUsageExtensions.cs" company="Starion Group S.A.">
// 
//   Copyright (C) 2022-2026 Starion Group S.A.
// 
//   Licensed under the Apache License, Version 2.0 (the "License");
//   you may not use this file except in compliance with the License.
//   You may obtain a copy of the License at
// 
//        http://www.apache.org/licenses/LICENSE-2.0
// 
//    Unless required by applicable law or agreed to in writing, software
//    distributed under the License is distributed on an "AS IS" BASIS,
//    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//    See the License for the specific language governing permissions and
//    limitations under the License.
// 
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.Core.POCO.Systems.Flows
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

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
        internal static List<IInteraction> ComputeFlowDefinition(this IFlowUsage flowUsageSubject)
        {
            return flowUsageSubject == null
                ? throw new ArgumentNullException(nameof(flowUsageSubject))
                : [..flowUsageSubject.ComputeType().OfType<IInteraction>()];
        }
    }
}
