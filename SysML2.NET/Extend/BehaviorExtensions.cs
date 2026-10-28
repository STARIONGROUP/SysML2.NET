// ------------------------------------------------------------------------------------------------
// <copyright file="BehaviorExtensions.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.Core.POCO.Kernel.Behaviors
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using SysML2.NET.Decorators;

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
        [DerivedProperty(name: nameof(IBehavior.parameter))]
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
        [DerivedProperty(name: nameof(IBehavior.step))]
        internal static List<IStep> ComputeStep(this IBehavior behaviorSubject)
        {
            return behaviorSubject == null
                ? throw new ArgumentNullException(nameof(behaviorSubject))
                : [..behaviorSubject.feature.OfType<IStep>()];
        }
    }
}
