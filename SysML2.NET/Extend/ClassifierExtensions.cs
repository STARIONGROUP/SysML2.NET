// -------------------------------------------------------------------------------------------------
// <copyright file="ClassifierExtensions.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.Core.POCO.Core.Classifiers
{
    using System;
    using System.Collections.Generic;

    using SysML2.NET.Core.POCO.Core.Features;
    using SysML2.NET.Core.POCO.Core.Types;
    using SysML2.NET.Core.POCO.Root.Annotations;
    using SysML2.NET.Core.POCO.Root.Elements;
    using SysML2.NET.Core.POCO.Root.Namespaces;

    /// <summary>
    /// The <see cref="ClassifierExtensions"/> class provides extensions methods for
    /// the <see cref="IClassifier"/> interface
    /// </summary>
    internal static class ClassifierExtensions
    {
        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <param name="classifier">
        /// The subject <see cref="IClassifier"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        internal static List<ISubclassification> ComputeOwnedSubclassification(this IClassifier classifier)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }

    }
}
