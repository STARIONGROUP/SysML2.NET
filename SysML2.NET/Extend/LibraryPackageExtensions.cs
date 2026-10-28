// ------------------------------------------------------------------------------------------------
// <copyright file="LibraryPackageExtensions.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.Core.POCO.Kernel.Packages
{
    using System;
    using System.Collections.Generic;

    using SysML2.NET.Decorators;

    using SysML2.NET.Core.Root.Namespaces;
    using SysML2.NET.Core.POCO.Kernel.Functions;
    using SysML2.NET.Core.POCO.Root.Annotations;
    using SysML2.NET.Core.POCO.Root.Elements;
    using SysML2.NET.Core.POCO.Root.Namespaces;

    /// <summary>
    /// The <see cref="LibraryPackageExtensions"/> class provides extensions methods for
    /// the <see cref="ILibraryPackage"/> interface
    /// </summary>
    internal static class LibraryPackageExtensions
    {
        /// <summary>
        /// The libraryNamespace for a LibraryPackage is itself.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// self
        /// </code>
        /// </remarks>
        /// <param name="libraryPackageSubject">
        /// The subject <see cref="ILibraryPackage"/>
        /// </param>
        /// <returns>
        /// The expected <see cref="INamespace" />
        /// </returns>
        [Operation(name: nameof(ILibraryPackage.LibraryNamespace))]
        internal static INamespace ComputeRedefinedLibraryNamespaceOperation(this ILibraryPackage libraryPackageSubject)
        {
            if (libraryPackageSubject == null)
            {
                throw new ArgumentNullException(nameof(libraryPackageSubject));
            }

            return libraryPackageSubject;
        }
    }
}
