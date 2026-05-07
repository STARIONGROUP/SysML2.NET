// -------------------------------------------------------------------------------------------------
// <copyright file="PackageExtensions.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.Core.POCO.Kernel.Packages
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using SysML2.NET.Core.POCO.Kernel.Functions;
    using SysML2.NET.Core.POCO.Root.Annotations;
    using SysML2.NET.Core.POCO.Root.Elements;
    using SysML2.NET.Core.POCO.Root.Namespaces;

    /// <summary>
    /// The <see cref="PackageExtensions"/> class provides extensions methods for
    /// the <see cref="IPackage"/> interface
    /// </summary>
    internal static class PackageExtensions
    {
        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <remarks>OCL2: filterCondition = ownedMembership-> selectByKind(ElementFilterMembership).condition </remarks>
        /// <param name="packageSubject">
        /// The subject <see cref="IPackage"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        internal static List<IExpression> ComputeFilterCondition(this IPackage packageSubject)
        {
            return packageSubject == null ? throw new ArgumentNullException(nameof(packageSubject)) : [..packageSubject.ownedMembership.OfType<IElementFilterMembership>().Select(x => x.condition)];
        }

        /// <summary>
        /// Exclude Elements that do not meet all the filterConditions.
        /// </summary>
        /// <param name="packageSubject">
        /// The subject <see cref="IPackage"/>
        /// </param>
        /// <param name="excluded">
        /// No documentation provided
        /// </param>
        /// <returns>
        /// The expected collection of <see cref="IMembership" />
        /// </returns>
        internal static List<IMembership> ComputeRedefinedImportedMembershipsOperation(this IPackage packageSubject, List<INamespace> excluded)
        {
            if (packageSubject == null)
            {
                throw new ArgumentNullException(nameof(packageSubject));
            }

            var importedMembership= packageSubject.ComputeImportedMembershipsOperation(excluded);
            var filters = packageSubject.ComputeFilterCondition();

            if (filters.Count == 0)
            {
                return importedMembership;
            }

            var validImportedMembership = importedMembership.Where(membership => filters.All(x => x.CheckCondition(membership))).ToList();
            return validImportedMembership;
        }

        /// <summary>
        /// Determine whether the given element meets all the filterConditions.
        /// </summary>
        /// <param name="packageSubject">
        /// The subject <see cref="IPackage"/>
        /// </param>
        /// <param name="element">
        /// No documentation provided
        /// </param>
        /// <returns>
        /// The expected <see cref="bool" />
        /// </returns>
        internal static bool ComputeIncludeAsMemberOperation(this IPackage packageSubject, IElement element)
        {
            if (packageSubject == null)
            {
                throw new ArgumentNullException(nameof(packageSubject));
            }

            if (element == null)
            {
                return false;
            }

            var filters = packageSubject.ComputeFilterCondition();
            return filters.Count == 0 || filters.All(x => x.CheckCondition(element));
        }
    }
}
