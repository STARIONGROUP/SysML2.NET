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

    using SysML2.NET.Core.Root.Namespaces;
    using SysML2.NET.Core.POCO.Root.Annotations;
    using SysML2.NET.Core.POCO.Root.Elements;

    /// <summary>
    /// The <see cref="NamespaceExtensions"/> class provides extensions methods for
    /// the <see cref="INamespace"/> interface
    /// </summary>
    internal static class NamespaceExtensions
    {
        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <param name="namespaceSubject">
        /// The subject <see cref="INamespace"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        internal static List<IMembership> ComputeImportedMembership(this INamespace namespaceSubject)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <param name="namespaceSubject">
        /// The subject <see cref="INamespace"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        internal static List<IElement> ComputeMember(this INamespace namespaceSubject)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <param name="namespaceSubject">
        /// The subject <see cref="INamespace"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        internal static List<IMembership> ComputeMembership(this INamespace namespaceSubject)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <param name="namespaceSubject">
        /// The subject <see cref="INamespace"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        internal static List<IImport> ComputeOwnedImport(this INamespace namespaceSubject)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <param name="namespaceSubject">
        /// The subject <see cref="INamespace"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        internal static List<IElement> ComputeOwnedMember(this INamespace namespaceSubject)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <param name="namespaceSubject">
        /// The subject <see cref="INamespace"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        internal static List<IMembership> ComputeOwnedMembership(this INamespace namespaceSubject)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }

        /// <summary>
        /// Return the names of the given element as it is known in this Namespace.
        /// </summary>
        /// <param name="namespaceSubject">
        /// The subject <see cref="INamespace"/>
        /// </param>
        /// <param name="element">
        /// No documentation provided
        /// </param>
        /// <returns>
        /// The expected collection of <see cref="string" />
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        internal static List<string> ComputeNamesOfOperation(this INamespace namespaceSubject, IElement element)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }

        /// <summary>
        /// Returns this visibility of mem relative to this Namespace. If mem is an importedMembership, this is
        /// the visibility of its Import. Otherwise it is the visibility of the Membership itself.
        /// </summary>
        /// <param name="namespaceSubject">
        /// The subject <see cref="INamespace"/>
        /// </param>
        /// <param name="mem">
        /// No documentation provided
        /// </param>
        /// <returns>
        /// The expected <see cref="VisibilityKind" />
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        internal static VisibilityKind ComputeVisibilityOfOperation(this INamespace namespaceSubject, IMembership mem)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }

        /// <summary>
        /// If includeAll = true, then return all the Memberships of this Namespace. Otherwise, return only the
        /// publicly visible Memberships of this Namespace, including ownedMemberships that have a visibility of
        /// public and Memberships imported with a visibility of public. If isRecursive = true, also recursively
        /// include all visible Memberships of any public owned Namespaces, or, if IncludeAll = true, all
        /// Memberships of all owned Namespaces. When computing imported Memberships, ignore this Namespace and
        /// any Namespaces in the given excluded set.
        /// </summary>
        /// <param name="namespaceSubject">
        /// The subject <see cref="INamespace"/>
        /// </param>
        /// <param name="excluded">
        /// No documentation provided
        /// </param>
        /// <param name="isRecursive">
        /// No documentation provided
        /// </param>
        /// <param name="includeAll">
        /// No documentation provided
        /// </param>
        /// <returns>
        /// The expected collection of <see cref="IMembership" />
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        internal static List<IMembership> ComputeVisibleMembershipsOperation(this INamespace namespaceSubject, List<INamespace> excluded, bool isRecursive, bool includeAll)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }

        /// <summary>
        /// Derive the imported Memberships of this Namespace as the importedMembership of all ownedImports,
        /// excluding those Imports whose importOwningNamespace is in the excluded set, and excluding
        /// Memberships that have distinguisibility collisions with each other or with any ownedMembership.
        /// </summary>
        /// <param name="namespaceSubject">
        /// The subject <see cref="INamespace"/>
        /// </param>
        /// <param name="excluded">
        /// No documentation provided
        /// </param>
        /// <returns>
        /// The expected collection of <see cref="IMembership" />
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        internal static List<IMembership> ComputeImportedMembershipsOperation(this INamespace namespaceSubject, List<INamespace> excluded)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }

        /// <summary>
        /// If visibility is not null, return the Memberships of this Namespace with the given visibility,
        /// including ownedMemberships with the given visibility and Memberships imported with the given
        /// visibility. If visibility is null, return all ownedMemberships and imported Memberships regardless
        /// of visibility. When computing imported Memberships, ignore this Namespace and any Namespaces in the
        /// given excluded set.
        /// </summary>
        /// <param name="namespaceSubject">
        /// The subject <see cref="INamespace"/>
        /// </param>
        /// <param name="visibility">
        /// No documentation provided
        /// </param>
        /// <param name="excluded">
        /// No documentation provided
        /// </param>
        /// <returns>
        /// The expected collection of <see cref="IMembership" />
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        internal static List<IMembership> ComputeMembershipsOfVisibilityOperation(this INamespace namespaceSubject, VisibilityKind? visibility, List<INamespace> excluded)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }

        /// <summary>
        /// Resolve the given qualified name to the named Membership (if any), starting with this Namespace as
        /// the local scope. The qualified name string must conform to the concrete syntax of the KerML textual
        /// notation. According to the KerML name resolution rules every qualified name will resolve to either a
        /// single Membership, or to none.
        /// </summary>
        /// <param name="namespaceSubject">
        /// The subject <see cref="INamespace"/>
        /// </param>
        /// <param name="qualifiedName">
        /// No documentation provided
        /// </param>
        /// <returns>
        /// The expected <see cref="IMembership" />
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        internal static IMembership ComputeResolveOperation(this INamespace namespaceSubject, string qualifiedName)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }

        /// <summary>
        /// Resolve the given qualified name to the named Membership (if any) in the effective global Namespace
        /// that is the outermost naming scope. The qualified name string must conform to the concrete syntax of
        /// the KerML textual notation.
        /// </summary>
        /// <param name="namespaceSubject">
        /// The subject <see cref="INamespace"/>
        /// </param>
        /// <param name="qualifiedName">
        /// No documentation provided
        /// </param>
        /// <returns>
        /// The expected <see cref="IMembership" />
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        internal static IMembership ComputeResolveGlobalOperation(this INamespace namespaceSubject, string qualifiedName)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }

        /// <summary>
        /// Resolve a simple name starting with this Namespace as the local scope, and continuing with
        /// containing outer scopes as necessary. However, if this Namespace is a root Namespace, then the
        /// resolution is done directly in global scope.
        /// </summary>
        /// <param name="namespaceSubject">
        /// The subject <see cref="INamespace"/>
        /// </param>
        /// <param name="name">
        /// No documentation provided
        /// </param>
        /// <returns>
        /// The expected <see cref="IMembership" />
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        internal static IMembership ComputeResolveLocalOperation(this INamespace namespaceSubject, string name)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }

        /// <summary>
        /// Resolve a simple name from the visible Memberships of this Namespace.
        /// </summary>
        /// <param name="namespaceSubject">
        /// The subject <see cref="INamespace"/>
        /// </param>
        /// <param name="name">
        /// No documentation provided
        /// </param>
        /// <returns>
        /// The expected <see cref="IMembership" />
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        internal static IMembership ComputeResolveVisibleOperation(this INamespace namespaceSubject, string name)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }

        /// <summary>
        /// Return a string with valid KerML syntax representing the qualification part of a given
        /// qualifiedName, that is, a qualified name with all the segment names of the given name except the
        /// last. If the given qualifiedName has only one segment, then return null.
        /// </summary>
        /// <param name="namespaceSubject">
        /// The subject <see cref="INamespace"/>
        /// </param>
        /// <param name="qualifiedName">
        /// No documentation provided
        /// </param>
        /// <returns>
        /// The expected <see cref="string" />
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        internal static string ComputeQualificationOfOperation(this INamespace namespaceSubject, string qualifiedName)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }

        /// <summary>
        /// Return the simple name that is the last segment name of the given qualifiedName. If this segment
        /// name has the form of a KerML unrestricted name, then "unescape" it by removing the surrounding
        /// single quotes and replacing all escape sequences with the specified character.
        /// </summary>
        /// <param name="namespaceSubject">
        /// The subject <see cref="INamespace"/>
        /// </param>
        /// <param name="qualifiedName">
        /// No documentation provided
        /// </param>
        /// <returns>
        /// The expected <see cref="string" />
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        internal static string ComputeUnqualifiedNameOfOperation(this INamespace namespaceSubject, string qualifiedName)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }
    }
}
