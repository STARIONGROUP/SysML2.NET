// ------------------------------------------------------------------------------------------------
// <copyright file="NamespaceImportExtensions.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.Core.POCO.Root.Namespaces
{
    using System;
    using System.Collections.Generic;

    using SysML2.NET.Decorators;

    using SysML2.NET.Core.Root.Namespaces;
    using SysML2.NET.Core.POCO.Root.Annotations;
    using SysML2.NET.Core.POCO.Root.Elements;

    /// <summary>
    /// The <see cref="NamespaceImportExtensions"/> class provides extensions methods for
    /// the <see cref="INamespaceImport"/> interface
    /// </summary>
    internal static class NamespaceImportExtensions
    {
        /// <summary>
        /// Returns at least the visible Memberships of the importedNamespace. If isRecursive = true, then
        /// Memberships are also recursively imported from any ownedMembers of the importedNamespace that are
        /// themselves Namespaces.
        /// </summary>
        /// <remarks>
        /// OCL (KerML XMI):
        /// <code>
        /// if excluded-&gt;includes(importedNamespace) then Sequence{}
        /// else importedNamespace.visibleMemberships(excluded, isRecursive, isImportAll)
        /// endif
        /// </code>
        /// </remarks>
        /// <param name="namespaceImportSubject">
        /// The subject <see cref="INamespaceImport"/>
        /// </param>
        /// <param name="excluded">
        /// No documentation provided
        /// </param>
        /// <returns>
        /// The expected collection of <see cref="IMembership" />
        /// </returns>
        [Operation(name: nameof(INamespaceImport.ImportedMemberships))]
        internal static List<IMembership> ComputeRedefinedImportedMembershipsOperation(this INamespaceImport namespaceImportSubject, List<INamespace> excluded)
        {
            if (namespaceImportSubject == null)
            {
                throw new ArgumentNullException(nameof(namespaceImportSubject));
            }

            if (namespaceImportSubject.ImportedNamespace == null)
            {
                return [];
            }

            // `if excluded->includes(importedNamespace) then Sequence{}` is the first branch of the OCL and
            // is the circularity guard of KerML §8.2.3.5.1 — without it a Namespace that imports one of its
            // own ancestors re-enters that ancestor indefinitely.
            if (excluded != null && excluded.Contains(namespaceImportSubject.ImportedNamespace))
            {
                return [];
            }

            return namespaceImportSubject.ImportedNamespace.VisibleMemberships(excluded ?? [], namespaceImportSubject.IsRecursive, namespaceImportSubject.IsImportAll);
        }
    }
}
