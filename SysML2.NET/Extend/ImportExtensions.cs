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
        /// <remarks>
        /// Spec-text-only: the UML <c>Import.importedElement</c> attribute has no OCL body of its
        /// own. Each concrete subclass defines its own derivation rule
        /// (<c>deriveMembershipImportImportedElement</c>: <c>importedElement = importedMembership.memberElement</c>,
        /// and <c>deriveNamespaceImportImportedElement</c>: <c>importedElement = importedNamespace</c>).
        /// Both subtype POCOs route their <c>importedElement</c> getter through this single static
        /// extension, so the dispatch happens here via a switch on the subject's concrete type.
        /// </remarks>
        /// <param name="importSubject">
        /// The subject <see cref="IImport"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        internal static IElement ComputeImportedElement(this IImport importSubject)
        {
            if (importSubject == null)
            {
                throw new ArgumentNullException(nameof(importSubject));
            }

            return importSubject switch
            {
                IMembershipImport membershipImport => membershipImport.ImportedMembership?.MemberElement,
                INamespaceImport namespaceImport => namespaceImport.ImportedNamespace,
                _ => null
            };
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
        /// The UML <c>Import.importedMemberships</c> operation is abstract (no body in the XMI). Both
        /// concrete subclasses, <see cref="NamespaceImport"/> and <see cref="MembershipImport"/>, redefine
        /// it with their own OCL <c>bodyCondition</c>s, and their POCO partials provide explicit-interface
        /// implementations of <see cref="IImport.ImportedMemberships"/> that dispatch directly to their
        /// respective <c>ComputeRedefinedImportedMembershipsOperation</c> extension methods. Consequently,
        /// this static extension on the abstract base is never reached at runtime, and a deliberate
        /// <see cref="NotSupportedException"/> guards any future direct call.
        /// </remarks>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        internal static List<IMembership> ComputeImportedMembershipsOperation(this IImport importSubject, List<INamespace> excluded)
        {
            throw new NotSupportedException(
                "Import is abstract and its importedMemberships operation is redefined by every concrete subclass " +
                "(NamespaceImport, MembershipImport). The POCO partials route IImport.ImportedMemberships(...) " +
                "directly to ComputeRedefinedImportedMembershipsOperation on the matching subtype, so this static " +
                "extension is unreachable at runtime.");
        }
    }
}
