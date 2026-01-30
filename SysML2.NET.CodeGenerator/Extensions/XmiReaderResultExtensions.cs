// -------------------------------------------------------------------------------------------------
// <copyright file="XmiReaderResultExtensions.cs" company="Starion Group S.A.">
// 
//   Copyright 2022-2025 Starion Group S.A.
// 
//   Licensed under the Apache License, Version 2.0 (the "License");
//   you may not use this file except in compliance with the License.
//   You may obtain a copy of the License at
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
        public static IReadOnlyList<IPackage> QueryAllExistingPackages(this XmiReaderResult xmiReaderResult, string rootName)
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
