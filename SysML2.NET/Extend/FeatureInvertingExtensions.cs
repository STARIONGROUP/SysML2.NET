// -------------------------------------------------------------------------------------------------
// <copyright file="FeatureInvertingExtensions.cs" company="Starion Group S.A.">
//
//    Copyright (C) 2022-2026 Starion Group S.A.
//
//    Licensed under the Apache License, Version 2.0 (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
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

namespace SysML2.NET.Core.POCO.Core.Features
{
    using System;
    using System.Collections.Generic;

    using SysML2.NET.Core.POCO.Root.Annotations;
    using SysML2.NET.Core.POCO.Root.Elements;
    using SysML2.NET.Core.POCO.Root.Namespaces;

    /// <summary>
    /// The <see cref="FeatureInvertingExtensions"/> class provides extensions methods for
    /// the <see cref="IFeatureInverting"/> interface
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage(
        "Major Code Smell",
        "S1192:Define a constant instead of using this literal",
        Justification = "Placeholder message for unimplemented derived properties. Suppression to be removed after methods have been implemented")]
    internal static class FeatureInvertingExtensions
    {
        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <param name="featureInverting">
        /// The subject <see cref="IFeatureInverting"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        internal static IFeature ComputeOwningFeature(this IFeatureInverting featureInverting)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }

    }
}
