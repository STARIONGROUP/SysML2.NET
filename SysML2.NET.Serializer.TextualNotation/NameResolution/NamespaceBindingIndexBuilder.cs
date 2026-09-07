// -------------------------------------------------------------------------------------------------
// <copyright file="NamespaceBindingIndexBuilder.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.Serializer.TextualNotation.NameResolution
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using SysML2.NET.Core.POCO.Core.Types;
    using SysML2.NET.Core.POCO.Root.Elements;
    using SysML2.NET.Core.POCO.Root.Namespaces;
    using SysML2.NET.Core.Root.Namespaces;
    using SysML2.NET.Semantics.Implied;

    /// <summary>
    /// Builds the immutable <see cref="NamespaceBindingIndex" /> for a root Namespace, walking containment,
    /// imports and inheritance once and recording every binding each scope offers.
    /// </summary>
    internal sealed class NamespaceBindingIndexBuilder
    {
        /// <summary>
        /// Reverse index of alias bindings: scope to (aliased element to alias names).
        /// </summary>
        private readonly Dictionary<INamespace, Dictionary<IElement, List<string>>> aliasIndex = new();

        /// <summary>
        /// Reverse index: owning Namespace to the Namespaces re-exporting it through a public import.
        /// </summary>
        private readonly Dictionary<INamespace, HashSet<INamespace>> directFacadeIndex = new();

        /// <summary>
        /// The other root Namespaces forming the global Namespace (KerML §8.2.3.5.2).
        /// </summary>
        private readonly List<INamespace> globalNamespaces;

        /// <summary>
        /// The layered binding tables, one per indexed scope.
        /// </summary>
        private readonly Dictionary<INamespace, ScopeBindingTable> layeredBindings = new();

        /// <summary>
        /// Maps implied generals onto the graph being written.
        /// </summary>
        private readonly ResolutionGraph resolutionGraph;

        /// <summary>
        /// The root Namespace being indexed.
        /// </summary>
        private readonly INamespace rootNamespace;

        /// <summary>
        /// Whether the import whose contributions are being recorded is public.
        /// </summary>
        private bool currentImportIsPublic;

        /// <summary>
        /// The layer the entries currently being recorded belong to.
        /// </summary>
        private BindingLayer currentRecordingLayer;

        /// <summary>
        /// The table receiving bindings for the scope currently being indexed.
        /// </summary>
        private ScopeBindingTable currentScopeBindings;

        /// <summary>
        /// Initializes a new instance of the <see cref="NamespaceBindingIndexBuilder" /> class.
        /// </summary>
        /// <param name="rootNamespace">The root Namespace to index.</param>
        /// <param name="globalNamespaces">The other root Namespaces forming the global Namespace.</param>
        /// <param name="impliedRelationshipProvider">The provider supplying implied Relationships.</param>
        internal NamespaceBindingIndexBuilder(INamespace rootNamespace, List<INamespace> globalNamespaces, IImpliedRelationshipProvider impliedRelationshipProvider)
        {
            this.rootNamespace = rootNamespace;
            this.globalNamespaces = globalNamespaces;
            this.resolutionGraph = new ResolutionGraph(rootNamespace, globalNamespaces, impliedRelationshipProvider);
        }

        /// <summary>
        /// Records the facade edges eagerly, then returns an index that builds each scope's bindings on
        /// first probe through <see cref="BuildScopeIndex" />.
        /// </summary>
        /// <returns>The lazily-populated index.</returns>
        /// <remarks>
        /// Per-scope bindings are independent of every other scope's, so they can be built on demand — and
        /// resolution only ever probes the scopes on reference-site chains and candidate descents, a tiny
        /// fraction of the reachable graph. The facade index is the one cross-scope product: an edge is
        /// discovered on the IMPORTING namespace but queried by the imported one, so it cannot be found
        /// on demand and is recorded up front from raw containment.
        /// </remarks>
        internal NamespaceBindingIndex Build()
        {
            this.BuildFacadeIndex();

            return new NamespaceBindingIndex(this.rootNamespace, this.globalNamespaces, this, this.layeredBindings, this.directFacadeIndex, this.aliasIndex, this.resolutionGraph);
        }

        /// <summary>
        /// Builds the simple-name index of one scope, recording its layered bindings and aliases as a side
        /// effect.
        /// </summary>
        /// <param name="scope">The <see cref="INamespace" /> to index.</param>
        /// <returns>The simple-name → member-set lookup for <paramref name="scope" />.</returns>
        internal IReadOnlyDictionary<string, HashSet<IElement>> BuildScopeIndex(INamespace scope)
        {
            var index = new Dictionary<string, HashSet<IElement>>(StringComparer.Ordinal);
            var discardedQueue = new Queue<(INamespace Scope, bool IsGlobal)>();
            var isGlobal = this.IsGlobalScope(scope);

            this.currentScopeBindings = new ScopeBindingTable();
            this.layeredBindings[scope] = this.currentScopeBindings;

            this.BuildOwnedAndImportedEntries(scope, index, discardedQueue, isGlobal);

            if (scope is IType type)
            {
                this.currentRecordingLayer = BindingLayer.Inherited;
                this.BuildInheritedEntries(type, index, discardedQueue, isGlobal);
            }

            this.currentScopeBindings = null;

            return index;
        }

        /// <summary>
        /// Records every facade edge — a public <see cref="INamespaceImport" /> re-exporting its imported
        /// namespace — by walking raw containment from the root and global Namespaces.
        /// </summary>
        /// <remarks>
        /// The walk reads only the <c>OwnedRelationship</c> / <c>OwnedRelatedElement</c> storage lists;
        /// derived properties are never touched, which keeps this pass proportional to element count
        /// rather than to inheritance depth.
        /// </remarks>
        private void BuildFacadeIndex()
        {
            var pending = new Stack<IElement>();
            var visited = new HashSet<IElement>();

            foreach (var seed in this.globalNamespaces.Prepend(this.rootNamespace).Where(seed => seed != null))
            {
                pending.Push(seed);
            }

            while (pending.Count != 0)
            {
                var element = pending.Pop();

                if (!visited.Add(element))
                {
                    continue;
                }

                foreach (var relationship in element.OwnedRelationship)
                {
                    if (element is INamespace importOwner
                        && relationship is INamespaceImport { Visibility: VisibilityKind.Public, ImportedNamespace: { } imported })
                    {
                        this.RecordDirectFacade(imported, importOwner);
                    }

                    foreach (var ownedElement in relationship.OwnedRelatedElement)
                    {
                        pending.Push(ownedElement);
                    }
                }
            }
        }

        /// <summary>
        /// Asserts whether <paramref name="scope" /> is reached through the global Namespace: its
        /// containment root is not the root Namespace being serialized (KerML §8.2.3.5.2).
        /// </summary>
        /// <param name="scope">The scope about to be indexed.</param>
        /// <returns><see langword="true" /> when only VISIBLE members of <paramref name="scope" /> may be indexed.</returns>
        private bool IsGlobalScope(INamespace scope)
        {
            var visited = new HashSet<IElement>();
            IElement current = scope;

            while (current.owningNamespace != null && visited.Add(current))
            {
                current = current.owningNamespace;
            }

            return !ReferenceEquals(current, this.rootNamespace);
        }

        /// <summary>
        /// Indexes, for lookup only, the members an implied general contributes.
        /// </summary>
        /// <param name="inheritingType">The Type that reaches the general through an implied Specialization.</param>
        /// <param name="impliedGeneral">The Type reached through an implied Specialization.</param>
        /// <param name="index">The destination index.</param>
        /// <param name="isGlobal">Whether the owning scope is reached through the global namespace.</param>
        /// <remarks>
        /// Filtered through <c>Type::removeRedefinedFeatures</c>: KerML §8.3.3.1.10 derives
        /// <c>inheritedMemberships</c> as <c>removeRedefinedFeatures(inheritableMemberships(…))</c>. Only this
        /// path needs the filter, since an implied Specialization bypasses <c>inheritedMembership</c>.
        /// </remarks>
        private void AddImpliedLookupEntries(IType inheritingType, IType impliedGeneral, Dictionary<string, HashSet<IElement>> index, bool isGlobal)
        {
            if (impliedGeneral == null)
            {
                return;
            }

            // An implied general has no authored relationship, so its private members stay hidden.
            var contributed = new List<IMembership>(impliedGeneral.ownedMembership
                .Where(ownedMember => ownedMember.Visibility != VisibilityKind.Private));

            contributed.AddRange(ImportExpansion.QueryInheritedMemberships(impliedGeneral));

            contributed = inheritingType.RemoveRedefinedFeatures(contributed);

            foreach (var member in contributed.Where(member => ImportExpansion.PassesVisibilityFilter(member, isGlobal)))
            {
                this.AddLookupOnlyEntry(index, member);
            }
        }

        /// <summary>
        /// Adds <paramref name="element" /> to <paramref name="index" /> under
        /// <paramref name="simpleName" /> when the name is non-blank.
        /// </summary>
        /// <param name="index">The destination index.</param>
        /// <param name="simpleName">The simple name to use as the index key.</param>
        /// <param name="element">The element to record.</param>
        private static void AddIndexEntry(Dictionary<string, HashSet<IElement>> index, string simpleName, IElement element)
        {
            if (string.IsNullOrWhiteSpace(simpleName) || element == null)
            {
                return;
            }

            if (!index.TryGetValue(simpleName, out var bucket))
            {
                bucket = [];
                index[simpleName] = bucket;
            }

            bucket.Add(element);
        }

        /// <summary>
        /// Indexes a Membership for name lookup WITHOUT extending the namespace traversal.
        /// </summary>
        /// <param name="index">The destination index.</param>
        /// <param name="membership">The Membership to index.</param>
        /// <remarks>
        /// The counterpart of <see cref="AddMembershipEntry" />, minus its <c>pending</c> enqueue. Used for
        /// members reached through an IMPLIED Specialization: they must be resolvable by name, but the
        /// library Types they come from must not be pulled into the walk that produces output.
        /// </remarks>
        private void AddLookupOnlyEntry(Dictionary<string, HashSet<IElement>> index, IMembership membership)
        {
            if (membership is not { MemberElement: { } target })
            {
                return;
            }

            var (shortName, longName) = SegmentNaming.QueryMembershipNames(membership, target);

            AddIndexEntry(index, shortName, target);
            AddIndexEntry(index, longName, target);
            this.RecordLayeredBinding(membership, target);
        }

        /// <summary>
        /// Indexes <paramref name="membership" />'s member element under both lexical forms — the
        /// membership's explicit name overrides when present, else the element's own names — and enqueues
        /// the element when it is itself a namespace.
        /// </summary>
        /// <param name="index">The destination index.</param>
        /// <param name="membership">The membership whose target is indexed.</param>
        /// <param name="pending">Queue of namespaces yet to be indexed.</param>
        /// <param name="isGlobal">Whether the owning scope is reached through the global namespace.</param>
        private void AddMembershipEntry(Dictionary<string, HashSet<IElement>> index, IMembership membership, Queue<(INamespace Scope, bool IsGlobal)> pending, bool isGlobal)
        {
            if (membership is not { MemberElement: { } target })
            {
                return;
            }

            var (shortName, longName) = SegmentNaming.QueryMembershipNames(membership, target);

            AddIndexEntry(index, shortName, target);
            AddIndexEntry(index, longName, target);
            this.RecordLayeredBinding(membership, target);

            if (target is INamespace targetAsNamespace)
            {
                pending.Enqueue((targetAsNamespace, isGlobal));
            }
        }

        /// <summary>
        /// Indexes the entries inherited from <paramref name="type" />'s transitive supertypes; namespace
        /// supertypes are enqueued as scopes in their own right. Delegates to
        /// <c>Type::inheritedMembership</c> (KerML §8.3.3.1.10) rather than re-deriving it.
        /// </summary>
        /// <param name="type">The type whose inherited memberships are indexed.</param>
        /// <param name="index">The destination index.</param>
        /// <param name="pending">Queue of namespaces yet to be indexed.</param>
        /// <param name="isGlobal">Whether the owning scope is reached through the global namespace.</param>
        private void BuildInheritedEntries(IType type, Dictionary<string, HashSet<IElement>> index, Queue<(INamespace Scope, bool IsGlobal)> pending, bool isGlobal)
        {
            var inheritableSupertypes = type.AllSupertypes()
                .Where(candidate => !ReferenceEquals(candidate, type))
                .ToList();

            foreach (var supertypeAsNamespace in inheritableSupertypes.OfType<INamespace>())
            {
                pending.Enqueue((supertypeAsNamespace, isGlobal));
            }

            var inheritedMemberships = ImportExpansion.QueryInheritedMemberships(type);

            foreach (var inheritedMember in inheritedMemberships
                         .Where(inheritedMember => ImportExpansion.PassesVisibilityFilter(inheritedMember, isGlobal)))
            {
                this.AddMembershipEntry(index, inheritedMember, pending, isGlobal);
            }

            foreach (var impliedGeneral in this.resolutionGraph.QueryImpliedGeneralClosure(type, inheritableSupertypes))
            {
                pending.Enqueue((impliedGeneral, isGlobal));

                this.AddImpliedLookupEntries(type, impliedGeneral, index, isGlobal);
            }
        }

        /// <summary>
        /// Populates <paramref name="index" /> with <paramref name="scope" />'s owned memberships and
        /// imports; imported namespaces are enqueued for their own indexing. When
        /// <paramref name="isGlobal" /> is set only VISIBLE entries are admitted (KerML §8.2.3.5.2).
        /// </summary>
        /// <param name="scope">The namespace whose entries are populated.</param>
        /// <param name="index">The destination index.</param>
        /// <param name="pending">Queue of namespaces yet to be indexed.</param>
        /// <param name="isGlobal">Whether the scope is reached through the global namespace.</param>
        private void BuildOwnedAndImportedEntries(INamespace scope, Dictionary<string, HashSet<IElement>> index, Queue<(INamespace Scope, bool IsGlobal)> pending, bool isGlobal)
        {
            var ownedMemberships = new List<IMembership>();
            this.currentRecordingLayer = BindingLayer.Owned;

            foreach (var ownedMember in scope.ownedMembership.Where(ownedMember => ImportExpansion.PassesVisibilityFilter(ownedMember, isGlobal)))
            {
                this.AddMembershipEntry(index, ownedMember, pending, isGlobal);
                this.RecordAliasIfDeclared(scope, ownedMember);
                ownedMemberships.Add(ownedMember);
            }

            var ownedNames = new HashSet<string>(index.Keys, StringComparer.Ordinal);
            this.currentRecordingLayer = BindingLayer.Imported;

            foreach (var namespaceImport in scope.ownedImport
                         .Where(ownedImport => ImportExpansion.PassesVisibilityFilter(ownedImport, isGlobal))
                         .OfType<INamespaceImport>()
                         .Where(namespaceImport => namespaceImport.ImportedNamespace != null))
            {
                pending.Enqueue((namespaceImport.ImportedNamespace, isGlobal));

                // Only a PUBLIC import re-exports what it brings in, so only a public one makes
                // the importing namespace a usable facade for it (KerML §8.2.3.5.3).
                if (namespaceImport.Visibility == VisibilityKind.Public)
                {
                    this.RecordDirectFacade(namespaceImport.ImportedNamespace, scope);
                }
            }

            foreach (var (ownedImport, importedMember) in ImportExpansion.QueryImportedContributions(scope, isGlobal, isGlobal, ownedNames, ownedMemberships))
            {
                this.currentImportIsPublic = ownedImport.Visibility == VisibilityKind.Public;

                this.AddMembershipEntry(index, importedMember, pending, isGlobal);
                this.RecordAliasIfDeclared(scope, importedMember);

                if (ReferenceEquals(importedMember, (ownedImport as IMembershipImport)?.ImportedMembership)
                    && importedMember?.MemberElement?.owningNamespace is { } memberOwner)
                {
                    pending.Enqueue((memberOwner, isGlobal));
                }
            }
        }

        /// <summary>
        /// Records an <c>alias X for Y;</c> binding — a membership whose explicit
        /// <see cref="IMembership.MemberName" /> / <see cref="IMembership.MemberShortName" /> differs from
        /// the member element's own names.
        /// </summary>
        /// <param name="scope">The <see cref="INamespace" /> declaring the membership.</param>
        /// <param name="membership">The candidate alias <see cref="IMembership" />; may be <see langword="null" />.</param>
        private void RecordAliasIfDeclared(INamespace scope, IMembership membership)
        {
            if (membership is not { MemberElement: { } target })
            {
                return;
            }

            var aliasNames = new[] { membership.MemberName, membership.MemberShortName }
                .Where(aliasName => !string.IsNullOrWhiteSpace(aliasName)
                                    && !string.Equals(aliasName, target.name, StringComparison.Ordinal)
                                    && !string.Equals(aliasName, target.shortName, StringComparison.Ordinal))
                .ToList();

            if (aliasNames.Count == 0)
            {
                return;
            }

            if (!this.aliasIndex.TryGetValue(scope, out var scopeAliases))
            {
                scopeAliases = [];
                this.aliasIndex[scope] = scopeAliases;
            }

            if (!scopeAliases.TryGetValue(target, out var existingAliases))
            {
                existingAliases = [];
                scopeAliases[target] = existingAliases;
            }

            existingAliases.AddRange(aliasNames.Where(aliasName => !existingAliases.Contains(aliasName)));
        }

        /// <summary>
        /// Records <paramref name="facade" /> as a direct (single-hop) re-exporter of
        /// <paramref name="canonicalOwner" />.
        /// </summary>
        /// <param name="canonicalOwner">The namespace being directly imported.</param>
        /// <param name="facade">The namespace importing it.</param>
        private void RecordDirectFacade(INamespace canonicalOwner, INamespace facade)
        {
            if (!this.directFacadeIndex.TryGetValue(canonicalOwner, out var facades))
            {
                facades = [];
                this.directFacadeIndex[canonicalOwner] = facades;
            }

            facades.Add(facade);
        }

        /// <summary>
        /// Records one binding with the ambient provenance; called wherever the flat index gains an entry.
        /// </summary>
        /// <param name="membership">The Membership introducing the binding.</param>
        /// <param name="target">The bound element.</param>
        private void RecordLayeredBinding(IMembership membership, IElement target)
        {
            if (this.currentScopeBindings == null)
            {
                return;
            }

            var (shortName, longName) = SegmentNaming.QueryMembershipNames(membership, target);
            var binding = new ScopeBinding(membership, target, this.currentRecordingLayer, this.currentImportIsPublic);

            this.currentScopeBindings.Add(shortName, binding);
            this.currentScopeBindings.Add(longName, binding);
        }
    }
}
