// -------------------------------------------------------------------------------------------------
// <copyright file="PackageExtensions.cs" company="Starion Group S.A.">
// 
//   Copyright (C) 2022-2026 Starion Group S.A.
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

namespace SysML2.NET.Core.POCO.Kernel.Packages
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using SysML2.NET.Core.POCO.Kernel.Functions;
    using SysML2.NET.Core.POCO.Kernel.Metadata;
    using SysML2.NET.Core.POCO.Root.Elements;
    using SysML2.NET.Core.POCO.Root.Namespaces;
    using SysML2.NET.Decorators;

    /// <summary>
    /// The <see cref="PackageExtensions" /> class provides extensions methods for
    /// the <see cref="IPackage" /> interface
    /// </summary>
    internal static class PackageExtensions
    {
        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <remarks>
        /// OCL (KerML XMI):
        /// <code>
        /// filterCondition = ownedMembership-&gt;selectByKind(ElementFilterMembership).condition
        /// </code>
        /// </remarks>
        /// <param name="packageSubject">
        /// The subject <see cref="IPackage" />
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [DerivedProperty(nameof(IPackage.filterCondition))]
        internal static List<IExpression> ComputeFilterCondition(this IPackage packageSubject)
        {
            return packageSubject == null
                ? throw new ArgumentNullException(nameof(packageSubject))
                : [..packageSubject.ownedMembership.OfType<IElementFilterMembership>().Select(x => x.condition)];
        }

        /// <summary>
        /// Exclude Elements that do not meet all the filterConditions.
        /// </summary>
        /// <remarks>
        /// OCL (KerML XMI):
        /// <code>
        /// self.oclAsType(Namespace).importedMemberships(excluded)-&gt;select(m | self.includeAsMember(m.memberElement))
        /// </code>
        /// </remarks>
        /// <param name="packageSubject">
        /// The subject <see cref="IPackage" />
        /// </param>
        /// <param name="excluded">
        /// No documentation provided
        /// </param>
        /// <returns>
        /// The expected collection of <see cref="IMembership" />
        /// </returns>
        [Operation(nameof(IPackage.ImportedMemberships))]
        internal static List<IMembership> ComputeRedefinedImportedMembershipsOperation(this IPackage packageSubject, List<INamespace> excluded)
        {
            if (packageSubject == null)
            {
                throw new ArgumentNullException(nameof(packageSubject));
            }

            return
            [
                ..packageSubject.ComputeImportedMembershipsOperation(excluded)
                    .Where(membership => packageSubject.IncludeAsMember(membership.MemberElement))
            ];
        }

        /// <summary>
        /// Determine whether the given element meets all the filterConditions.
        /// </summary>
        /// <remarks>
        /// OCL (KerML XMI):
        /// <code>
        /// let metadataFeatures: Sequence(AnnotatingElement) =
        ///     element.ownedAnnotation.annotatingElement-&gt;selectByKind(MetadataFeature)
        /// in
        /// self.filterCondition-&gt;forAll(cond | metadataFeatures-&gt;exists(elem | cond.checkCondition(elem)))
        /// </code>
        /// </remarks>
        /// <param name="packageSubject">
        /// The subject <see cref="IPackage" />
        /// </param>
        /// <param name="element">
        /// No documentation provided
        /// </param>
        /// <returns>
        /// The expected <see cref="bool" />
        /// </returns>
        [Operation(nameof(IPackage.IncludeAsMember))]
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

            var metadataFeatures = element.ownedAnnotation
                .Select(annotation => annotation.annotatingElement)
                .OfType<IMetadataFeature>()
                .ToList();

            return packageSubject.filterCondition
                .All(condition => metadataFeatures.Any(metadataFeature => condition.CheckCondition(metadataFeature)));
        }
    }
}
