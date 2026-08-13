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
    using System.Linq;
    using System.Text;

    using SysML2.NET.Core.Root.Namespaces;
    using SysML2.NET.Core.POCO.Core.Types;
    using SysML2.NET.Core.POCO.Root.Annotations;
    using SysML2.NET.Core.POCO.Root.Elements;
    using SysML2.NET.Extensions;

    /// <summary>
    /// The <see cref="NamespaceExtensions"/> class provides extensions methods for
    /// the <see cref="INamespace"/> interface
    /// </summary>
    internal static class NamespaceExtensions
    {
        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <remarks>
        /// OCL (KerML XMI):
        /// <code>
        /// importedMembership = importedMemberships(Set{})
        /// </code>
        /// </remarks>
        /// <param name="namespaceSubject">
        /// The subject <see cref="INamespace"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        internal static List<IMembership> ComputeImportedMembership(this INamespace namespaceSubject)
        {
            return namespaceSubject == null ? throw new ArgumentNullException(nameof(namespaceSubject)) : namespaceSubject.ImportedMemberships([]);
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <remarks>
        /// OCL (KerML XMI):
        /// <code>
        /// member = membership.memberElement
        /// </code>
        /// </remarks>
        /// <param name="namespaceSubject">
        /// The subject <see cref="INamespace"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        internal static List<IElement> ComputeMember(this INamespace namespaceSubject)
        {
            return namespaceSubject == null ? throw new ArgumentNullException(nameof(namespaceSubject)) : [..namespaceSubject.membership.Select(x => x.MemberElement)];
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
        internal static List<IMembership> ComputeMembership(this INamespace namespaceSubject)
        {
            if (namespaceSubject == null)
            {
                throw new ArgumentNullException(nameof(namespaceSubject));
            }

            // `membership` is a derived UNION; its subsets are ownedMembership, importedMembership and —
            // only when the Namespace is a Type — inheritedMembership. KerML §8.2.3.5.3: memberships
            // "include owned, imported and (if the Namespace is a Type) inherited". Each subset is taken
            // from its own derived property rather than re-deriving it here.
            var result = namespaceSubject.ownedMembership.Union(namespaceSubject.importedMembership);

            return namespaceSubject is IType typeSubject
                ? [..result.Union(typeSubject.inheritedMembership)]
                : [..result];
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <remarks>
        /// OCL (KerML XMI):
        /// <code>
        /// ownedImport = ownedRelationship-&gt;selectByKind(Import)
        /// </code>
        /// </remarks>
        /// <param name="namespaceSubject">
        /// The subject <see cref="INamespace"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        internal static List<IImport> ComputeOwnedImport(this INamespace namespaceSubject)
        {
            return namespaceSubject == null ? throw new ArgumentNullException(nameof(namespaceSubject)) : [..namespaceSubject.OwnedRelationship.OfType<IImport>()];
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <remarks>
        /// OCL (KerML XMI):
        /// <code>
        /// ownedMember = ownedMembership-&gt;selectByKind(OwningMembership).ownedMemberElement
        /// </code>
        /// </remarks>
        /// <param name="namespaceSubject">
        /// The subject <see cref="INamespace"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        internal static List<IElement> ComputeOwnedMember(this INamespace namespaceSubject)
        {
            return namespaceSubject == null ? throw new ArgumentNullException(nameof(namespaceSubject)) : [..namespaceSubject.ownedMembership.OfType<IOwningMembership>().Select(x => x.ownedMemberElement)];
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <remarks>
        /// OCL (KerML XMI):
        /// <code>
        /// ownedMembership = ownedRelationship-&gt;selectByKind(Membership)
        /// </code>
        /// </remarks>
        /// <param name="namespaceSubject">
        /// The subject <see cref="INamespace"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        internal static List<IMembership> ComputeOwnedMembership(this INamespace namespaceSubject)
        {
            return namespaceSubject == null ? throw new ArgumentNullException(nameof(namespaceSubject)) : [..namespaceSubject.OwnedRelationship.OfType<IMembership>()];
        }

        /// <summary>
        /// Return the names of the given element as it is known in this Namespace.
        /// </summary>
        /// <remarks>
        /// OCL (KerML XMI):
        /// <code>
        /// let elementMemberships : Sequence(Membership) = memberships-&gt;select(memberElement = element) in
        /// memberships.memberShortName-&gt;union(memberships.memberName)-&gt;asSet()
        /// </code>
        /// </remarks>
        /// <param name="namespaceSubject">
        /// The subject <see cref="INamespace"/>
        /// </param>
        /// <param name="element">
        /// No documentation provided
        /// </param>
        /// <returns>
        /// The expected collection of <see cref="string" />
        /// </returns>
        internal static List<string> ComputeNamesOfOperation(this INamespace namespaceSubject, IElement element)
        {
            if (namespaceSubject == null)
            {
                throw new ArgumentNullException(nameof(namespaceSubject));
            }

            if (element == null)
            {
                throw new ArgumentNullException(nameof(element));
            }

            var memberElements = namespaceSubject.membership.Where(x => x.MemberElement == element).ToList();

            var names = new List<string>();

            foreach (var memberElement in memberElements)
            {
                if (!string.IsNullOrWhiteSpace(memberElement.MemberShortName))
                {
                    names.Add(memberElement.MemberShortName);
                }

                if (!string.IsNullOrWhiteSpace(memberElement.MemberName))
                {
                    names.Add(memberElement.MemberName);
                }
            }

            return names;
        }

        /// <summary>
        /// Returns this visibility of mem relative to this Namespace. If mem is an importedMembership, this is
        /// the visibility of its Import. Otherwise it is the visibility of the Membership itself.
        /// </summary>
        /// <remarks>
        /// OCL (KerML XMI):
        /// <code>
        /// if importedMembership-&gt;includes(mem) then
        ///     ownedImport-&gt;select(importedMemberships(Set{})-&gt;includes(mem)).first().visibility
        /// else
        ///     if memberships-&gt;includes(mem) then mem.visibility
        ///     else VisibilityKind::private endif
        /// endif
        /// </code>
        /// </remarks>
        /// <param name="namespaceSubject">
        /// The subject <see cref="INamespace"/>
        /// </param>
        /// <param name="mem">
        /// No documentation provided
        /// </param>
        /// <returns>
        /// The expected <see cref="VisibilityKind" />
        /// </returns>
        internal static VisibilityKind ComputeVisibilityOfOperation(this INamespace namespaceSubject, IMembership mem)
        {
            if (namespaceSubject == null)
            {
                throw new ArgumentNullException(nameof(namespaceSubject));
            }

            if (mem == null)
            {
                throw new ArgumentNullException(nameof(mem));
            }

            foreach (var ownedImport in namespaceSubject.ownedImport)
            {
                var membershipsFromImport = ownedImport.ImportedMemberships([]);

                if (membershipsFromImport.Contains(mem))
                {
                    return ownedImport.Visibility;
                }
            }

            return namespaceSubject.membership.Contains(mem) ? mem.Visibility : VisibilityKind.Private;
        }

        /// <summary>
        /// If includeAll = true, then return all the Memberships of this Namespace. Otherwise, return only the
        /// publicly visible Memberships of this Namespace, including ownedMemberships that have a visibility of
        /// public and Memberships imported with a visibility of public. If isRecursive = true, also recursively
        /// include all visible Memberships of any public owned Namespaces, or, if IncludeAll = true, all
        /// Memberships of all owned Namespaces. When computing imported Memberships, ignore this Namespace and
        /// any Namespaces in the given excluded set.
        /// </summary>
        /// <remarks>
        /// OCL (KerML XMI):
        /// <code>
        /// let visibleMemberships : OrderedSet(Membership) =
        ///     if includeAll then membershipsOfVisibility(null, excluded)
        ///     else membershipsOfVisibility(VisibilityKind::public, excluded) endif
        /// in
        /// if not isRecursive then visibleMemberships
        /// else visibleMemberships-&gt;union(
        ///     ownedMember-&gt;selectAsKind(Namespace).
        ///     select(includeAll or owningMembership.visibility = VisibilityKind::public)-&gt;
        ///     visibleMemberships(excluded-&gt;including(self), true, includeAll))
        /// endif
        /// </code>
        /// </remarks>
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
        internal static List<IMembership> ComputeVisibleMembershipsOperation(this INamespace namespaceSubject, List<INamespace> excluded, bool isRecursive, bool includeAll)
        {
            if (namespaceSubject == null)
            {
                throw new ArgumentNullException(nameof(namespaceSubject));
            }

            var safeExcluded = excluded ?? [];

            // Sourced from membershipsOfVisibility — NOT from `membership`. For a Type the two differ:
            // `membership` carries inheritedMembership, which Type::visibleMemberships adds back separately
            // with the excluded set threaded in. Reading `membership` here would double-count the inherited
            // part and drop that cycle guard.
            var result = namespaceSubject.MembershipsOfVisibility(includeAll ? null : VisibilityKind.Public, safeExcluded);

            if (!isRecursive)
            {
                return result;
            }

            // `excluded->including(self)`: descending into a nested Namespace must not round-trip back into
            // this one. KerML §8.2.3.5.1 makes that guard normative — a nested Namespace importing its own
            // owner would otherwise re-export this Namespace's members, private ones included when the
            // Import is `import all`.
            var excludedWithSelf = new List<INamespace>(safeExcluded) { namespaceSubject };

            var nestedNamespaces = namespaceSubject.ownedMembership
                .OfType<IOwningMembership>()
                .Where(mem => includeAll || mem.Visibility == VisibilityKind.Public)
                .Select(mem => mem.ownedMemberElement)
                .OfType<INamespace>();

            foreach (var nestedNamespace in nestedNamespaces)
            {
                result.AddRange(nestedNamespace.VisibleMemberships(excludedWithSelf, true, includeAll));
            }

            return result;
        }

        /// <summary>
        /// Derive the imported Memberships of this Namespace as the importedMembership of all ownedImports,
        /// excluding those Imports whose importOwningNamespace is in the excluded set, and excluding
        /// Memberships that have distinguisibility collisions with each other or with any ownedMembership.
        /// </summary>
        /// <remarks>
        /// OCL (KerML XMI):
        /// <code>
        /// ownedImport.importedMemberships(excluded-&gt;including(self))
        /// </code>
        /// </remarks>
        /// <param name="namespaceSubject">
        /// The subject <see cref="INamespace"/>
        /// </param>
        /// <param name="excluded">
        /// No documentation provided
        /// </param>
        /// <returns>
        /// The expected collection of <see cref="IMembership" />
        /// </returns>
        internal static List<IMembership> ComputeImportedMembershipsOperation(this INamespace namespaceSubject, List<INamespace> excluded)
        {
            if (namespaceSubject == null)
            {
                throw new ArgumentNullException(nameof(namespaceSubject));
            }

            var excludedWithSelf = new List<INamespace>(excluded) { namespaceSubject };

            var importedMemberships = namespaceSubject.ownedImport
                .SelectMany(import => import.ImportedMemberships(excludedWithSelf))
                .Distinct()
                .ToList();

            var ownedMemberships = namespaceSubject.ownedMembership;

            return
            [
                ..importedMemberships.Where(import =>
                ownedMemberships.All(import.IsDistinguishableFrom)
                && importedMemberships.All(other => other == import || import.IsDistinguishableFrom(other)))
            ];
        }

        /// <summary>
        /// If visibility is not null, return the Memberships of this Namespace with the given visibility,
        /// including ownedMemberships with the given visibility and Memberships imported with the given
        /// visibility. If visibility is null, return all ownedMemberships and imported Memberships regardless
        /// of visibility. When computing imported Memberships, ignore this Namespace and any Namespaces in the
        /// given excluded set.
        /// </summary>
        /// <remarks>
        /// OCL (KerML XMI):
        /// <code>
        /// ownedMembership-&gt;select(mem | visibility = null or mem.visibility = visibility)-&gt;union(
        ///     ownedImport-&gt;select(imp | visibility = null or imp.visibility = visibility).
        ///     importedMemberships(excluded-&gt;including(self)))
        /// </code>
        /// </remarks>
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
        internal static List<IMembership> ComputeMembershipsOfVisibilityOperation(this INamespace namespaceSubject, VisibilityKind? visibility, List<INamespace> excluded)
        {
            if (namespaceSubject == null)
            {
                throw new ArgumentNullException(nameof(namespaceSubject));
            }

            var safeExcluded = excluded ?? [];

            var result = new List<IMembership>(
                namespaceSubject.ownedMembership.Where(mem => visibility == null || mem.Visibility == visibility.Value));

            // `Namespace::importedMemberships` additionally drops Memberships with distinguishability
            // collisions (KerML §8.2.3.5.1), which the terse OCL above does not spell out — so the imported
            // side is taken from it and then narrowed to the Memberships contributed by Imports of the
            // requested visibility.
            //
            // The visibility filter is applied to the IMPORTS, per the OCL, rather than by asking
            // `visibilityOf` for each resulting Membership. The two readings agree — visibilityOf(mem) IS
            // the visibility of the Import that produced mem — but only the import-side filter is usable
            // here: visibilityOf falls back to `membership`, and for a Type `membership` includes
            // inheritedMembership, whose derivation runs back through this very operation. visibilityOf
            // also hard-codes Set{} where the excluded set has to be threaded through.
            var excludedWithSelf = new List<INamespace>(safeExcluded) { namespaceSubject };

            var membershipsOfVisibleImports = namespaceSubject.ownedImport
                .Where(import => visibility == null || import.Visibility == visibility.Value)
                .SelectMany(import => import.ImportedMemberships(excludedWithSelf))
                .ToHashSet();

            result.AddRange(namespaceSubject.ImportedMemberships(safeExcluded).Where(membershipsOfVisibleImports.Contains));

            return result;
        }

        /// <summary>
        /// Resolve the given qualified name to the named Membership (if any), starting with this Namespace as
        /// the local scope. The qualified name string must conform to the concrete syntax of the KerML textual
        /// notation. According to the KerML name resolution rules every qualified name will resolve to either a
        /// single Membership, or to none.
        /// </summary>
        /// <remarks>
        /// OCL (KerML XMI):
        /// <code>
        /// let qualification : String = qualificationOf(qualifiedName) in
        /// let name : String = unqualifiedNameOf(qualifiedName) in
        /// if qualification = null then resolveLocal(name)
        /// else
        ///     if qualification = '$' then resolveGlobal(name)
        ///     else
        ///         let namespaceMembership : Membership = resolve(qualification) in
        ///         if namespaceMembership = null or
        ///             not namespaceMembership.memberElement.oclIsKindOf(Namespace) then null
        ///         else namespaceMembership.memberElement.oclAsType(Namespace).resolveVisible(name)
        ///         endif
        ///     endif
        /// endif
        /// </code>
        /// </remarks>
        /// <param name="namespaceSubject">
        /// The subject <see cref="INamespace"/>
        /// </param>
        /// <param name="qualifiedName">
        /// No documentation provided
        /// </param>
        /// <returns>
        /// The expected <see cref="IMembership" />
        /// </returns>
        internal static IMembership ComputeResolveOperation(this INamespace namespaceSubject, string qualifiedName)
        {
            if (namespaceSubject == null)
            {
                throw new ArgumentNullException(nameof(namespaceSubject));
            }

            if (string.IsNullOrWhiteSpace(qualifiedName))
            {
                return null;
            }

            var qualification = namespaceSubject.QualificationOf(qualifiedName);
            var simpleName = namespaceSubject.UnqualifiedNameOf(qualifiedName);

            if (qualification == null)
            {
                return namespaceSubject.ResolveLocal(simpleName);
            }

            var qualificationMembership = namespaceSubject.Resolve(qualification);

            if (qualificationMembership?.MemberElement is INamespace qualificationNamespace)
            {
                return qualificationNamespace.ResolveVisible(simpleName);
            }

            return null;
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
        internal static IMembership ComputeResolveGlobalOperation(this INamespace namespaceSubject, string qualifiedName)
        {
            if (namespaceSubject == null)
            {
                throw new ArgumentNullException(nameof(namespaceSubject));
            }

            if (string.IsNullOrWhiteSpace(qualifiedName))
            {
                return null;
            }

            var rootNamespace = namespaceSubject;

            while (rootNamespace.owningNamespace != null)
            {
                rootNamespace = rootNamespace.owningNamespace;
            }

            var qualification = rootNamespace.QualificationOf(qualifiedName);
            var simpleName = rootNamespace.UnqualifiedNameOf(qualifiedName);

            if (qualification == null)
            {
                return rootNamespace.ResolveVisible(simpleName);
            }

            var qualificationMembership = rootNamespace.Resolve(qualification);

            if (qualificationMembership?.MemberElement is INamespace qualificationNamespace)
            {
                return qualificationNamespace.ResolveVisible(simpleName);
            }

            return null;
        }

        /// <summary>
        /// Resolve a simple name starting with this Namespace as the local scope, and continuing with
        /// containing outer scopes as necessary. However, if this Namespace is a root Namespace, then the
        /// resolution is done directly in global scope.
        /// </summary>
        /// <remarks>
        /// OCL (KerML XMI):
        /// <code>
        /// if owningNamespace = null then resolveGlobal(name)
        /// else
        ///     let memberships : Membership = membership-&gt;select(memberShortName = name or memberName = name)
        ///     in
        ///     if memberships-&gt;notEmpty() then memberships-&gt;first()
        ///     else owningNamespace.resolveLocal(name)
        ///     endif
        /// endif
        /// </code>
        /// </remarks>
        /// <param name="namespaceSubject">
        /// The subject <see cref="INamespace"/>
        /// </param>
        /// <param name="name">
        /// No documentation provided
        /// </param>
        /// <returns>
        /// The expected <see cref="IMembership" />
        /// </returns>
        internal static IMembership ComputeResolveLocalOperation(this INamespace namespaceSubject, string name)
        {
            if (namespaceSubject == null)
            {
                throw new ArgumentNullException(nameof(namespaceSubject));
            }

            if (string.IsNullOrWhiteSpace(name))
            {
                return null;
            }

            if (namespaceSubject.owningNamespace == null)
            {
                return namespaceSubject.ResolveGlobal(name);
            }

            // Local resolution searches EVERY membership of this Namespace regardless of visibility, per
            // the OCL above and KerML §8.2.3.5.3. Filtering to the visible ones (ResolveVisible) is the
            // rule for a NON-FIRST segment of a qualified name, not for local resolution, and made every
            // reference to a private owned member fail to resolve locally. `membership` carries a Type's
            // INHERITED memberships too (§8.2.3.5.3), so an inherited feature is nameable from within the
            // Type that inherits it.
            var resolved = namespaceSubject.membership
                .FirstOrDefault(membership => string.Equals(membership.MemberShortName, name, StringComparison.Ordinal)
                                              || string.Equals(membership.MemberName, name, StringComparison.Ordinal));

            return resolved ?? namespaceSubject.owningNamespace.ResolveLocal(name);
        }

        /// <summary>
        /// Resolve a simple name from the visible Memberships of this Namespace.
        /// </summary>
        /// <remarks>
        /// OCL (KerML XMI):
        /// <code>
        /// let memberships : Sequence(Membership) =
        ///     visibleMemberships(Set{}, false, false)-&gt;select(memberShortName = name or memberName = name)
        /// in
        /// if memberships-&gt;isEmpty() then null else memberships-&gt;first() endif
        /// </code>
        /// </remarks>
        /// <param name="namespaceSubject">
        /// The subject <see cref="INamespace"/>
        /// </param>
        /// <param name="name">
        /// No documentation provided
        /// </param>
        /// <returns>
        /// The expected <see cref="IMembership" />
        /// </returns>
        internal static IMembership ComputeResolveVisibleOperation(this INamespace namespaceSubject, string name)
        {
            if (namespaceSubject == null)
            {
                throw new ArgumentNullException(nameof(namespaceSubject));
            }

            if (string.IsNullOrWhiteSpace(name))
            {
                return null;
            }

            var visibleMemberships = namespaceSubject.VisibleMemberships([], false, false);

            return visibleMemberships.FirstOrDefault(m => m.MemberName == name || m.MemberShortName == name);
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
        internal static string ComputeQualificationOfOperation(this INamespace namespaceSubject, string qualifiedName)
        {
            if (namespaceSubject == null)
            {
                throw new ArgumentNullException(nameof(namespaceSubject));
            }

            if (string.IsNullOrWhiteSpace(qualifiedName))
            {
                return null;
            }

            var lastSeparatorIndex = qualifiedName.FindLastQualifiedNameSeparatorIndex();

            return lastSeparatorIndex < 0 ? null : qualifiedName[..lastSeparatorIndex];
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
        internal static string ComputeUnqualifiedNameOfOperation(this INamespace namespaceSubject, string qualifiedName)
        {
            if (namespaceSubject == null)
            {
                throw new ArgumentNullException(nameof(namespaceSubject));
            }

            if (string.IsNullOrWhiteSpace(qualifiedName))
            {
                return null;
            }

            var lastSeparatorIndex = qualifiedName.FindLastQualifiedNameSeparatorIndex();

            var lastSegment = lastSeparatorIndex < 0
                ? qualifiedName
                : qualifiedName.Substring(lastSeparatorIndex + 2);

            return lastSegment.UnescapeUnrestrictedName();
        }
    }
}
