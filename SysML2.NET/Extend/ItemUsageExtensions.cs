// ------------------------------------------------------------------------------------------------
// <copyright file="ItemUsageExtensions.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.Core.POCO.Systems.Items
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using SysML2.NET.Decorators;

    using SysML2.NET.Core.POCO.Core.Features;
    using SysML2.NET.Core.POCO.Kernel.Structures;

    /// <summary>
    /// The <see cref="ItemUsageExtensions" /> class provides extensions methods for
    /// the <see cref="IItemUsage" /> interface
    /// </summary>
    internal static class ItemUsageExtensions
    {
        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// itemDefinition = occurrenceDefinition-&gt;selectByKind(Structure)
        /// </code>
        /// </remarks>
        /// <param name="itemUsageSubject">
        /// The subject <see cref="IItemUsage" />
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [DerivedProperty(name: nameof(IItemUsage.itemDefinition))]
        internal static List<IStructure> ComputeItemDefinition(this IItemUsage itemUsageSubject)
        {
            return itemUsageSubject == null
                ? throw new ArgumentNullException(nameof(itemUsageSubject))
                : [.. itemUsageSubject.occurrenceDefinition.OfType<IStructure>()];
        }
    }
}
