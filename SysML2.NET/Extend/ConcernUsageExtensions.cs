// ------------------------------------------------------------------------------------------------
// <copyright file="ConcernUsageExtensions.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.Core.POCO.Systems.Requirements
{
    using System;
    using System.Linq;

    using SysML2.NET.Decorators;

    using SysML2.NET.Core.POCO.Core.Features;
    using SysML2.NET.Extensions;

    /// <summary>
    /// The <see cref="ConcernUsageExtensions" /> class provides extensions methods for
    /// the <see cref="IConcernUsage" /> interface
    /// </summary>
    internal static class ConcernUsageExtensions
    {
        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <param name="concernUsageSubject">
        /// The subject <see cref="IConcernUsage" />
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [DerivedProperty(name: nameof(IConcernUsage.concernDefinition))]
        internal static IConcernDefinition ComputeConcernDefinition(this IConcernUsage concernUsageSubject)
        {
            return concernUsageSubject == null
                ? throw new ArgumentNullException(nameof(concernUsageSubject))
                : FeatureExtensions.ComputeType(concernUsageSubject).SingleOrDefaultStrict<IConcernDefinition>(nameof(concernUsageSubject));
        }
    }
}
