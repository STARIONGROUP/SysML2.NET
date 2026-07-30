// -------------------------------------------------------------------------------------------------
// <copyright file="FlowDefinitionExtensions.cs" company="Starion Group S.A.">
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

    using SysML2.NET.Core.POCO.Core.Types;
    using SysML2.NET.Core.POCO.Systems.DefinitionAndUsage;

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
        internal static List<IUsage> ComputeFlowEnd(this IFlowDefinition flowDefinitionSubject)
        {
            return flowDefinitionSubject == null
                ? throw new ArgumentNullException(nameof(flowDefinitionSubject))
                : [..flowDefinitionSubject.ComputeEndFeature().OfType<IUsage>()];
        }
    }
}
