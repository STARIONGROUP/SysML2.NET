// ------------------------------------------------------------------------------------------------
// <copyright file="XmiReaderResultExtensions.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.CodeGenerator.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using uml4net.Extensions;
    using uml4net.Packages;
    using uml4net.StructuredClassifiers;
    using uml4net.xmi.Readers;

    /// <summary>
    /// Extension methods for the <see cref="XmiReaderResult"/> class
    /// </summary>
    public static class XmiReaderResultExtensions
    {
        /// <summary>
        /// Queries all contained <see cref="IPackage"/> and imported <see cref="IPackage"/> contained under the root package
        /// </summary>
        /// <param name="xmiReaderResult">The <see cref="XmiReaderResult"/> that contains all read elements</param>
        /// <param name="rootName">The name of the root package to query</param>
        /// <returns>A <see cref="IReadOnlyCollection{T}"/> of all contained <see cref="IPackage"/> under the root <see cref="IPackage"/></returns>
        public static IReadOnlyList<IPackage> QueryContainedAndImported(this XmiReaderResult xmiReaderResult, string rootName)
        {
            ArgumentNullException.ThrowIfNull(xmiReaderResult);
            
            var packages = xmiReaderResult.QueryRoot(null, name: rootName).QueryPackages();

            var importedPackages = packages.SelectMany(x => x.PackageImport)
                .Select(x => x.ImportedPackage).ToList();
            
            var allPackages = new List<IPackage>();
            allPackages.AddRange(packages);

            foreach (var importedPackage in importedPackages)
            {
                if (importedPackage.Possessor is IPackage importedPackagePossessor)
                {
                    allPackages.AddRange(importedPackagePossessor.QueryPackages());
                }
                else
                {
                    allPackages.Add(importedPackage);
                    allPackages.AddRange(importedPackage.QueryPackages());
                }
            }

            return [..allPackages.DistinctBy(x => x.XmiId)];
        }
    }
}
