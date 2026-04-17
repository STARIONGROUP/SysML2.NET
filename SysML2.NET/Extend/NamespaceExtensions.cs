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

    using SysML2.NET.Comparer;
    using SysML2.NET.Core.Root.Namespaces;
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
            return namespaceSubject == null ? throw new ArgumentNullException(nameof(namespaceSubject)) : [..namespaceSubject.ownedMembership.Union(namespaceSubject.ImportedMemberships([]))];
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
        internal static List<IImport> ComputeOwnedImport(this INamespace namespaceSubject)
        {
            return namespaceSubject == null ? throw new ArgumentNullException(nameof(namespaceSubject)) : [..namespaceSubject.OwnedRelationship.OfType<IImport>()];
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <param name="namespaceSubject">
        /// The subject <see cref="INamespace"/>
        /// </param>
        /// <remarks>OCL Constraint : ownedMember = ownedMembership->selectByKind(OwningMembership).ownedMemberElement </remarks>
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

            var result = new List<IMembership>();

            if (includeAll)
            {
                result.AddRange(namespaceSubject.membership);
            }
            else
            {
                result.AddRange(namespaceSubject.ownedMembership.Where(m => m.Visibility == VisibilityKind.Public));

                var excludedWithSelf = new List<INamespace>(excluded) { namespaceSubject };

                var publicImported = namespaceSubject.ImportedMemberships(excludedWithSelf)
                    .Where(m => namespaceSubject.VisibilityOf(m) == VisibilityKind.Public);

                result.AddRange(publicImported);
            }

            if (isRecursive)
            {
                var namespaceMemberships = includeAll
                    ? namespaceSubject.ownedMembership
                    : namespaceSubject.ownedMembership.Where(m => m.Visibility == VisibilityKind.Public);

                foreach (var mem in namespaceMemberships)
                {
                    if (mem.MemberElement is INamespace nestedNamespace)
                    {
                        var nestedMemberships = nestedNamespace.VisibleMemberships(excluded, true, includeAll);
                        result.AddRange(nestedMemberships);
                    }
                }
            }

            return result;
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
        internal static List<IMembership> ComputeImportedMembershipsOperation(this INamespace namespaceSubject, List<INamespace> excluded)
        {
            var importedMemberships = namespaceSubject.ownedImport.Where(x => !excluded.Contains(x.importOwningNamespace))
                .SelectMany(x => x.ImportedMemberships(excluded))
                .Distinct()
                .ToList();

            var ownedMembershipNames = namespaceSubject.ownedMembership
                .Select(membership => membership.MemberName)
                .ToHashSet(NullSafeStringComparer.Instance);

            var ownedMembershipShortNames = namespaceSubject.ownedMembership
                .Select(membership => membership.MemberShortName)
                .ToHashSet(NullSafeStringComparer.Instance);

            var importedNameFrequency = new Dictionary<string, int>(importedMemberships.Count);
            var importedShortNameFrequency = new Dictionary<string, int>(importedMemberships.Count);
            var importedNullNameCount = 0;
            var importedNullShortNameCount = 0;

            foreach (var membership in importedMemberships)
            {
                var memberName = membership.MemberName;

                if (memberName != null)
                {
                    importedNameFrequency[memberName] = importedNameFrequency.TryGetValue(memberName, out var nameCount) ? nameCount + 1 : 1;
                }
                else
                {
                    importedNullNameCount++;
                }

                var memberShortName = membership.MemberShortName;

                if (memberShortName != null)
                {
                    importedShortNameFrequency[memberShortName] = importedShortNameFrequency.TryGetValue(memberShortName, out var shortNameCount) ? shortNameCount + 1 : 1;
                }
                else
                {
                    importedNullShortNameCount++;
                }
            }

            var nonCollidingImportedMemberships = importedMemberships.Where(membership =>
            {
                var memberName = membership.MemberName;
                var memberShortName = membership.MemberShortName;

                var nameCollidesWithOwned = ownedMembershipNames.Contains(memberName) || ownedMembershipShortNames.Contains(memberShortName);

                var nameCollidesWithImported =
                    (memberName != null && importedNameFrequency.TryGetValue(memberName, out var nameFrequency) && nameFrequency > 1)
                    || (memberName == null && importedNullNameCount > 1)
                    || (memberShortName != null && importedShortNameFrequency.TryGetValue(memberShortName, out var shortNameFrequency) && shortNameFrequency > 1)
                    || (memberShortName == null && importedNullShortNameCount > 1);

                return !nameCollidesWithOwned && !nameCollidesWithImported;
            }).ToList();

            return nonCollidingImportedMemberships;
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
        internal static List<IMembership> ComputeMembershipsOfVisibilityOperation(this INamespace namespaceSubject, VisibilityKind? visibility, List<INamespace> excluded)
        {
            if (namespaceSubject == null)
            {
                throw new ArgumentNullException(nameof(namespaceSubject));
            }

            var excludedWithSelf = new List<INamespace>(excluded) { namespaceSubject };

            if (visibility == null)
            {
                var result = new List<IMembership>(namespaceSubject.ownedMembership);
                result.AddRange(namespaceSubject.ImportedMemberships(excludedWithSelf));
                return result;
            }

            var filtered = new List<IMembership>();

            filtered.AddRange(namespaceSubject.ownedMembership.Where(m => m.Visibility == visibility.Value));

            filtered.AddRange(namespaceSubject.ImportedMemberships(excludedWithSelf)
                .Where(m => namespaceSubject.VisibilityOf(m) == visibility.Value));

            return filtered;
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

            if (namespaceSubject.owner == null)
            {
                return namespaceSubject.ResolveGlobal(name);
            }

            var resolved = namespaceSubject.ResolveVisible(name);

            return resolved ?? namespaceSubject.owningNamespace?.ResolveLocal(name);
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
