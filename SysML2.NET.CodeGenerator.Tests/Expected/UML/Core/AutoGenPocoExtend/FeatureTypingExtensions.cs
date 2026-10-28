// ------------------------------------------------------------------------------------------------
// <copyright file="FeatureTypingExtensions.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.Core.POCO.Core.Features
{
    using System;
    using System.Collections.Generic;

    using SysML2.NET.Decorators;

    using SysML2.NET.Core.POCO.Core.Types;
    using SysML2.NET.Core.POCO.Root.Annotations;
    using SysML2.NET.Core.POCO.Root.Elements;
    using SysML2.NET.Core.POCO.Root.Namespaces;

    /// <summary>
    /// The <see cref="FeatureTypingExtensions"/> class provides extensions methods for
    /// the <see cref="IFeatureTyping"/> interface
    /// </summary>
    internal static class FeatureTypingExtensions
    {
        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <param name="featureTypingSubject">
        /// The subject <see cref="IFeatureTyping"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        [DerivedProperty(name: nameof(IFeatureTyping.owningFeature))]
        internal static IFeature ComputeOwningFeature(this IFeatureTyping featureTypingSubject)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }

    }
}
