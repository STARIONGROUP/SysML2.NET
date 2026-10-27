// ------------------------------------------------------------------------------------------------
// <copyright file="MembershipImportExtensions.cs" company="Starion Group S.A.">
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

    /// <summary>
    /// The <see cref="MembershipImportExtensions" /> class provides extensions methods for
    /// the <see cref="IMembershipImport" /> interface
    /// </summary>
    internal static class MembershipImportExtensions
    {
        /// <summary>
        /// Returns at least the importedMembership. If isRecursive = true and the memberElement of the
        /// importedMembership is a Namespace, then Memberships are also recursively imported from that
        /// Namespace.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// if not isRecursive or
        ///                                 not importedElement.oclIsKindOf(Namespace) or
        ///                                 excluded-&gt;includes(importedElement)
        ///                                 then Sequence{importedMembership}
        ///                                 else importedElement.oclAsType(Namespace).
        ///                                 visibleMemberships(excluded, true, importAll)-&gt;
        ///                                 prepend(importedMembership)
        ///                                 endif
        /// </code>
        /// </remarks>
        /// <param name="membershipImportSubject">
        /// The subject <see cref="IMembershipImport" />
        /// </param>
        /// <param name="excluded">
        /// No documentation provided
        /// </param>
        /// <returns>
        /// The expected collection of <see cref="IMembership" />
        /// </returns>
        [Operation(nameof(IMembershipImport.ImportedMemberships))]
        internal static List<IMembership> ComputeRedefinedImportedMembershipsOperation(this IMembershipImport membershipImportSubject, List<INamespace> excluded)
        {
            if (membershipImportSubject == null)
            {
                throw new ArgumentNullException(nameof(membershipImportSubject));
            }

            if (!membershipImportSubject.IsRecursive
                || membershipImportSubject.importedElement is not INamespace importedNamespace
                || (excluded != null && excluded.Contains(importedNamespace)))
            {
                return [membershipImportSubject.ImportedMembership];
            }

            var visibleMemberships = importedNamespace.VisibleMemberships(excluded, true, membershipImportSubject.IsImportAll);

            return [membershipImportSubject.ImportedMembership, .. visibleMemberships];
        }
    }
}
