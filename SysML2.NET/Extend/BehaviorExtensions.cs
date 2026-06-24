// -------------------------------------------------------------------------------------------------
// <copyright file="BehaviorExtensions.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.Core.POCO.Kernel.Behaviors
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using SysML2.NET.Core.POCO.Core.Features;

    /// <summary>
    /// The <see cref="BehaviorExtensions" /> class provides extensions methods for
    /// the <see cref="IBehavior" /> interface
    /// </summary>
    internal static class BehaviorExtensions
    {
        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <param name="behaviorSubject">
        /// The subject <see cref="IBehavior" />
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        internal static List<IFeature> ComputeParameter(this IBehavior behaviorSubject)
        {
            return behaviorSubject == null
                ? throw new ArgumentNullException(nameof(behaviorSubject))
                : [..behaviorSubject.feature.Where(memberFeature => behaviorSubject.DirectionOf(memberFeature) != null)];
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// step = feature-&gt;selectByKind(Step)
        /// </code>
        /// </remarks>
        /// <param name="behaviorSubject">
        /// The subject <see cref="IBehavior" />
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        internal static List<IStep> ComputeStep(this IBehavior behaviorSubject)
        {
            return behaviorSubject == null
                ? throw new ArgumentNullException(nameof(behaviorSubject))
                : [..behaviorSubject.feature.OfType<IStep>()];
        }
    }
}
