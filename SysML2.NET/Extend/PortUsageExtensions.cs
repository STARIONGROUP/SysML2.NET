// -------------------------------------------------------------------------------------------------
// <copyright file="PortUsageExtensions.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.Core.POCO.Systems.Ports
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

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
        internal static List<IPortDefinition> ComputePortDefinition(this IPortUsage portUsageSubject)
        {
            return portUsageSubject == null
                ? throw new ArgumentNullException(nameof(portUsageSubject))
                :
                [
                    ..portUsageSubject.OwnedRelationship
                        .OfType<IFeatureTyping>()
                        .Select(featureTyping => featureTyping.Type)
                        .OfType<IPortDefinition>()
                ];
        }
    }
}
