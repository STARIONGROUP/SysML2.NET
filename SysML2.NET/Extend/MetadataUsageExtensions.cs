// ------------------------------------------------------------------------------------------------
// <copyright file="MetadataUsageExtensions.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.Core.POCO.Systems.Metadata
{
    using System;
    using System.Linq;

    using SysML2.NET.Core.POCO.Core.Features;
    using SysML2.NET.Core.POCO.Kernel.Metadata;
    using SysML2.NET.Decorators;

    /// <summary>
    /// The <see cref="MetadataUsageExtensions" /> class provides extensions methods for
    /// the <see cref="IMetadataUsage" /> interface
    /// </summary>
    internal static class MetadataUsageExtensions
    {
        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <param name="metadataUsageSubject">
        /// The subject <see cref="IMetadataUsage" />
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [DerivedProperty(nameof(IMetadataUsage.metadataDefinition))]
        internal static IMetaclass ComputeMetadataDefinition(this IMetadataUsage metadataUsageSubject)
        {
            return metadataUsageSubject == null
                ? throw new ArgumentNullException(nameof(metadataUsageSubject))
                : metadataUsageSubject.ComputeType().OfType<IMetaclass>().FirstOrDefault();
        }
    }
}
