// -------------------------------------------------------------------------------------------------
// <copyright file="TextualNotationWriterContext.cs" company="Starion Group S.A.">
//
//   Copyright 2022-2026 Starion Group S.A.
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

namespace SysML2.NET.Serializer.TextualNotation.Writers
{
    using System;
    using System.Collections.Generic;

    using SysML2.NET.Core.POCO.Core.Features;
    using SysML2.NET.Core.POCO.Core.Types;
    using SysML2.NET.Core.POCO.Root.Elements;
    using SysML2.NET.Core.POCO.Root.Namespaces;

    /// <summary>
    /// Provides the serialization context for the textual notation builders.
    /// Carries the <see cref="ICursorCache"/> for cursor-based element traversal,
    /// the <see cref="INamespace"/> context for qualified name resolution (KerML 8.2.3.5),
    /// a per-namespace simple-name index cache used by
    /// <see cref="SharedTextualNotationBuilder.AppendQualifiedName"/>, and future writer
    /// settings (indentation, short/long notation preferences, etc.).
    /// </summary>
    public class TextualNotationWriterContext : IDisposable
    {
        /// <summary>
        /// Lazily-populated per-namespace simple-name index. For each <see cref="INamespace"/>
        /// scope previously queried, maps each visible simple name (member shortName / name)
        /// to the set of <see cref="IElement"/>s reachable by that simple name from that scope.
        /// Built on demand in <see cref="GetOrBuildSimpleNameIndex"/>.
        /// </summary>
        private readonly Dictionary<INamespace, Dictionary<string, HashSet<IElement>>> simpleNameIndex
            = new Dictionary<INamespace, Dictionary<string, HashSet<IElement>>>();

        /// <summary>
        /// Initializes a new instance of the <see cref="TextualNotationWriterContext"/> class.
        /// </summary>
        /// <param name="contextNamespace">
        /// The root <see cref="INamespace"/> being serialized. Used as the upper-bound of the
        /// upward-walk in <see cref="SharedTextualNotationBuilder.AppendQualifiedName"/>, and
        /// as the fallback local scope when a source POCO has no <c>owningNamespace</c>.
        /// </param>
        public TextualNotationWriterContext(INamespace contextNamespace)
        {
            this.CursorCache = new CursorCache();
            this.ContextNamespace = contextNamespace ?? throw new ArgumentNullException(nameof(contextNamespace));
        }

        /// <summary>
        /// Gets the <see cref="ICursorCache"/> used for cursor-based element traversal.
        /// </summary>
        public ICursorCache CursorCache { get; }

        /// <summary>
        /// Gets the root <see cref="INamespace"/> being serialized, providing the
        /// upper bound for the upward-walk performed in qualified name resolution
        /// per KerML specification section 8.2.3.5.
        /// </summary>
        public INamespace ContextNamespace { get; }

        /// <summary>
        /// Returns the cached simple-name index for the given <paramref name="scope"/>, building
        /// it on first access. The index maps every simple name that resolves locally in
        /// <paramref name="scope"/> to the set of <see cref="IElement"/>s reachable by that
        /// simple name. The set covers:
        /// <list type="bullet">
        ///   <item><description>Owned memberships (<c>scope.ownedMembership</c>).</description></item>
        ///   <item><description>Direct imports (<c>scope.ownedImport</c>) — both <c>IMembershipImport</c>
        ///   and <c>INamespaceImport</c>.</description></item>
        ///   <item><description>Inherited feature memberships when <paramref name="scope"/> is an
        ///   <c>IType</c>, by walking <c>type.featureMembership</c> (which already unions owned
        ///   and inherited per <c>TypeExtensions.ComputeFeatureMembership</c>).</description></item>
        /// </list>
        /// </summary>
        /// <param name="scope">The <see cref="INamespace"/> whose simple-name index is requested.</param>
        /// <returns>The simple-name → <see cref="IElement"/> set lookup for <paramref name="scope"/>.</returns>
        internal IReadOnlyDictionary<string, HashSet<IElement>> GetOrBuildSimpleNameIndex(INamespace scope)
        {
            if (scope == null)
            {
                return EmptyIndex;
            }

            if (this.simpleNameIndex.TryGetValue(scope, out var cached))
            {
                return cached;
            }

            var index = new Dictionary<string, HashSet<IElement>>(StringComparer.Ordinal);

            BuildOwnedAndImportedEntries(scope, index);

            if (scope is IType type)
            {
                BuildInheritedEntries(type, index);
            }

            this.simpleNameIndex[scope] = index;

            return index;
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting
        /// unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            this.CursorCache.Dispose();
        }

        /// <summary>
        /// Single shared empty result returned by <see cref="GetOrBuildSimpleNameIndex"/> when
        /// the requested scope is <see langword="null"/>.
        /// </summary>
        private static readonly IReadOnlyDictionary<string, HashSet<IElement>> EmptyIndex
            = new Dictionary<string, HashSet<IElement>>(StringComparer.Ordinal);

        /// <summary>
        /// Adds entries for <paramref name="scope"/>'s <c>ownedMembership</c> and direct
        /// <c>ownedImport</c>s into <paramref name="index"/>. Handles both
        /// <see cref="IMembershipImport"/> and <see cref="INamespaceImport"/>; for the latter,
        /// the imported namespace's <c>ownedMembership</c> entries are projected into the index
        /// keyed by the member-relative names exposed by the import.
        /// </summary>
        /// <param name="scope">The namespace whose owned + imported members are indexed.</param>
        /// <param name="index">The destination index to populate.</param>
        private static void BuildOwnedAndImportedEntries(INamespace scope, Dictionary<string, HashSet<IElement>> index)
        {
            try
            {
                foreach (var ownedMember in scope.ownedMembership)
                {
                    AddMembershipEntry(index, ownedMember);
                }
            }
            catch (NotSupportedException)
            {
                // ownedMembership may not be implemented for this namespace; skip.
            }

            try
            {
                foreach (var ownedImport in scope.ownedImport)
                {
                    if (ownedImport is IMembershipImport membershipImport
                        && membershipImport.ImportedMembership is IMembership importedMembership)
                    {
                        AddMembershipEntry(index, importedMembership);
                    }
                    else if (ownedImport is INamespaceImport namespaceImport
                             && namespaceImport.ImportedNamespace != null)
                    {
                        foreach (var importedMember in namespaceImport.ImportedNamespace.ownedMembership)
                        {
                            AddMembershipEntry(index, importedMember);
                        }
                    }
                }
            }
            catch (NotSupportedException)
            {
                // ownedImport / ImportedMemberships may not be implemented; skip.
            }
        }

        /// <summary>
        /// Adds entries for inherited memberships of <paramref name="type"/> into
        /// <paramref name="index"/>, keyed by each inherited <see cref="IMembership"/>'s
        /// <c>MemberShortName</c> / <c>MemberName</c> and mapped to its <c>MemberElement</c>.
        /// <para>Walks the transitive supertype graph (via <c>AllSupertypes</c>) and indexes
        /// each supertype's own <c>ownedMembership</c> entries directly. This is
        /// intentionally <em>different</em> from
        /// <c>TypeExtensions.ComputeInheritedMembership</c>: that operation runs the
        /// <c>RemoveRedefinedFeatures</c> filter (which strips inherited Features that have
        /// been redefined by the local Type), so references such as <c>:&gt;&gt; elements</c>
        /// — whose very purpose is to redefine <c>elements</c> — would be excluded by name
        /// resolution that relied on it. For name-resolution purposes the redefined target
        /// MUST stay reachable, so we bypass that filter and index the raw owned memberships
        /// of every transitive supertype.</para>
        /// </summary>
        /// <param name="type">The type whose transitive-supertype memberships are indexed.</param>
        /// <param name="index">The destination index to populate.</param>
        private static void BuildInheritedEntries(IType type, Dictionary<string, HashSet<IElement>> index)
        {
            List<IType> supertypes;

            try
            {
                supertypes = type.AllSupertypes();
            }
            catch (NotSupportedException)
            {
                return;
            }

            foreach (var supertype in supertypes)
            {
                if (supertype == null || ReferenceEquals(supertype, type))
                {
                    continue;
                }

                try
                {
                    foreach (var ownedMember in supertype.ownedMembership)
                    {
                        AddMembershipEntry(index, ownedMember);
                    }
                }
                catch (NotSupportedException)
                {
                    // ownedMembership not implemented for this supertype; skip.
                }
            }
        }

        /// <summary>
        /// Adds the <see cref="IMembership.MemberElement"/> of <paramref name="membership"/>
        /// to the index under both its <see cref="IMembership.MemberShortName"/> and
        /// <see cref="IMembership.MemberName"/>, when present.
        /// </summary>
        /// <param name="index">The destination index to populate.</param>
        /// <param name="membership">The membership whose target is indexed.</param>
        private static void AddMembershipEntry(Dictionary<string, HashSet<IElement>> index, IMembership membership)
        {
            if (membership == null)
            {
                return;
            }

            var target = membership.MemberElement;

            if (target == null)
            {
                return;
            }

            AddIndexEntry(index, membership.MemberShortName, target);
            AddIndexEntry(index, membership.MemberName, target);
        }

        /// <summary>
        /// Adds <paramref name="element"/> to <paramref name="index"/> under
        /// <paramref name="simpleName"/> when the name is non-blank.
        /// </summary>
        /// <param name="index">The destination index to populate.</param>
        /// <param name="simpleName">The simple name to use as the index key.</param>
        /// <param name="element">The element to record under <paramref name="simpleName"/>.</param>
        private static void AddIndexEntry(Dictionary<string, HashSet<IElement>> index, string simpleName, IElement element)
        {
            if (string.IsNullOrWhiteSpace(simpleName) || element == null)
            {
                return;
            }

            if (!index.TryGetValue(simpleName, out var bucket))
            {
                bucket = new HashSet<IElement>();
                index[simpleName] = bucket;
            }

            bucket.Add(element);
        }
    }
}
