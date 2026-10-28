// ------------------------------------------------------------------------------------------------
// <copyright file="TerminateActionUsageExtensions.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.Core.POCO.Systems.Actions
{
    using System;

    using SysML2.NET.Decorators;

    using SysML2.NET.Core.POCO.Kernel.Functions;

    /// <summary>
    /// The <see cref="TerminateActionUsageExtensions" /> class provides extensions methods for
    /// the <see cref="ITerminateActionUsage" /> interface
    /// </summary>
    internal static class TerminateActionUsageExtensions
    {
        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// terminatedOccurrenceArgument = argument(1)
        /// </code>
        /// </remarks>
        /// <param name="terminateActionUsageSubject">
        /// The subject <see cref="ITerminateActionUsage" />
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [DerivedProperty(name: nameof(ITerminateActionUsage.terminatedOccurrenceArgument))]
        internal static IExpression ComputeTerminatedOccurrenceArgument(this ITerminateActionUsage terminateActionUsageSubject)
        {
            return terminateActionUsageSubject == null
                ? throw new ArgumentNullException(nameof(terminateActionUsageSubject))
                : terminateActionUsageSubject.Argument(1);
        }
    }
}
