// ------------------------------------------------------------------------------------------------
// <copyright file="StepExtensions.cs" company="Starion Group S.A.">
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

    using SysML2.NET.Core.Core.Types;
    using SysML2.NET.Core.Root.Namespaces;
    using SysML2.NET.Core.POCO.Core.Features;
    using SysML2.NET.Core.POCO.Core.Types;
    using SysML2.NET.Core.POCO.Root.Annotations;
    using SysML2.NET.Core.POCO.Root.Elements;
    using SysML2.NET.Core.POCO.Root.Namespaces;

    /// <summary>
    /// The <see cref="StepExtensions"/> class provides extensions methods for
    /// the <see cref="IStep"/> interface
    /// </summary>
    internal static class StepExtensions
    {
        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// behavior = type-&gt;selectByKind(Behavior)
        /// </code>
        /// </remarks>
        /// <param name="stepSubject">
        /// The subject <see cref="IStep"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [DerivedProperty(name: nameof(IStep.behavior))]
        internal static List<IBehavior> ComputeBehavior(this IStep stepSubject)
        {
            return stepSubject == null
                ? throw new ArgumentNullException(nameof(stepSubject))
                : [.. stepSubject.type.OfType<IBehavior>()];
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <param name="stepSubject">
        /// The subject <see cref="IStep"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [DerivedProperty(name: nameof(IStep.parameter))]
        internal static List<IFeature> ComputeParameter(this IStep stepSubject)
        {
            return stepSubject == null
                ? throw new ArgumentNullException(nameof(stepSubject))
                : [.. TypeExtensions.ComputeDirectedFeature(stepSubject)];
        }

    }
}
