// ------------------------------------------------------------------------------------------------
// <copyright file="EnumerationUsageExtensions.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.Core.POCO.Systems.Enumerations
{
    using System;

    using SysML2.NET.Decorators;

    using SysML2.NET.Core.POCO.Core.Features;
    using SysML2.NET.Extensions;

    /// <summary>
    /// The <see cref="EnumerationUsageExtensions" /> class provides extensions methods for
    /// the <see cref="IEnumerationUsage" /> interface
    /// </summary>
    internal static class EnumerationUsageExtensions
    {
        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <param name="enumerationUsageSubject">
        /// The subject <see cref="IEnumerationUsage" />
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [DerivedProperty(name: nameof(IEnumerationUsage.enumerationDefinition))]
        internal static IEnumerationDefinition ComputeEnumerationDefinition(this IEnumerationUsage enumerationUsageSubject)
        {
            return enumerationUsageSubject == null
                ? throw new ArgumentNullException(nameof(enumerationUsageSubject))
                : enumerationUsageSubject.ComputeType().SingleStrict<IEnumerationDefinition>(nameof(enumerationUsageSubject));
        }
    }
}
