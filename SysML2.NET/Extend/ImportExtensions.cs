// -------------------------------------------------------------------------------------------------
// <copyright file="ImportExtensions.cs" company="Starion Group S.A.">
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

    using SysML2.NET.Core.Root.Namespaces;
    using SysML2.NET.Core.POCO.Root.Annotations;
    using SysML2.NET.Core.POCO.Root.Elements;

    /// <summary>
    /// The <see cref="ImportExtensions"/> class provides extensions methods for
    /// the <see cref="IImport"/> interface
    /// </summary>
    internal static class ImportExtensions
    {
        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <param name="importSubject">
        /// The subject <see cref="IImport"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        internal static IElement ComputeImportedElement(this IImport importSubject)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <param name="importSubject">
        /// The subject <see cref="IImport"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        internal static INamespace ComputeImportOwningNamespace(this IImport importSubject)
        {
            return importSubject == null ? throw new ArgumentNullException(nameof(importSubject)) : importSubject.OwningRelatedElement as INamespace;
        }

        /// <summary>
        /// Returns Memberships that are to become importedMemberships of the importOwningNamespace. (The
        /// excluded parameter is used to handle the possibility of circular Import Relationships.)
        /// </summary>
        /// <param name="importSubject">
        /// The subject <see cref="IImport"/>
        /// </param>
        /// <param name="excluded">
        /// No documentation provided
        /// </param>
        /// <returns>
        /// The expected collection of <see cref="IMembership" />
        /// </returns>
        /// <remarks>
        /// This method is no longer called at runtime. The POCO classes (<see cref="NamespaceImport"/>,
        /// <see cref="MembershipImport"/>) now provide explicit interface implementations of
        /// <see cref="IImport.ImportedMemberships"/> that dispatch directly to their respective
        /// <c>ComputeRedefinedImportedMembershipsOperation</c> extension methods.
        /// </remarks>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        internal static List<IMembership> ComputeImportedMembershipsOperation(this IImport importSubject, List<INamespace> excluded)
        {
            throw new NotSupportedException("This method should not be called. Import subtypes handle dispatch via explicit interface implementations.");
        }
    }
}
