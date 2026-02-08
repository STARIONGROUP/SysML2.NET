// -------------------------------------------------------------------------------------------------
// <copyright file="NamespaceExtensions.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.Core.POCO.Root.Namespaces
{
    using System;
    using System.Collections.Generic;

    using SysML2.NET.Core.POCO.Root.Annotations;
    using SysML2.NET.Core.POCO.Root.Elements;

    /// <summary>
    /// The <see cref="NamespaceExtensions"/> class provides extensions methods for
    /// the <see cref="INamespace"/> interface
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage(
        "Major Code Smell",
        "S1192:Define a constant instead of using this literal",
        Justification = "Placeholder message for unimplemented derived properties. Suppression to be removed after methods have been implemented")]
    internal static class NamespaceExtensions
    {
        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <param name="namespace">
        /// The subject <see cref="INamespace"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        internal static List<IMembership> ComputeImportedMembership(this INamespace @namespace)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <param name="namespace">
        /// The subject <see cref="INamespace"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        internal static List<IElement> ComputeMember(this INamespace @namespace)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <param name="namespace">
        /// The subject <see cref="INamespace"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        internal static List<IMembership> ComputeMembership(this INamespace @namespace)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <param name="namespace">
        /// The subject <see cref="INamespace"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        internal static List<IImport> ComputeOwnedImport(this INamespace @namespace)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <param name="namespace">
        /// The subject <see cref="INamespace"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        internal static List<IElement> ComputeOwnedMember(this INamespace @namespace)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <param name="namespace">
        /// The subject <see cref="INamespace"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        internal static List<IMembership> ComputeOwnedMembership(this INamespace @namespace)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }
    }
}
