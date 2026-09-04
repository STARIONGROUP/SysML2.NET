 // -------------------------------------------------------------------------------------------------
// <copyright file="ResolutionGraph.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.Serializer.TextualNotation.NameResolution
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using SysML2.NET.Core.POCO.Core.Types;
    using SysML2.NET.Core.POCO.Root.Elements;
    using SysML2.NET.Core.POCO.Root.Namespaces;
    using SysML2.NET.Semantics.Implied;

    /// <summary>
    /// Maps an implied general (KerML §8.4.2) onto the equivalent Element of the graph being written, so a
    /// name reachable only through an implied Specialization resolves against an indexed Namespace.
    /// </summary>
    internal sealed class ResolutionGraph
    {
        /// <summary>
        /// The other root Namespaces forming the global Namespace.
        /// </summary>
        private readonly List<INamespace> globalNamespaces;

        /// <summary>
        /// Supplies the implied Relationships an exported model omits.
        /// </summary>
        private readonly IImpliedRelationshipProvider impliedRelationshipProvider;

        /// <summary>
        /// The root Namespace being written.
        /// </summary>
        private readonly INamespace rootNamespace;

        /// <summary>
        /// Elements of the resolution graph keyed by Id, built on first use.
        /// </summary>
        private Dictionary<Guid, IElement> resolutionGraphElementsById;

        /// <summary>
        /// Initializes a new instance of the <see cref="ResolutionGraph" /> class.
        /// </summary>
        /// <param name="rootNamespace">The root Namespace being written.</param>
        /// <param name="globalNamespaces">The other root Namespaces forming the global Namespace.</param>
        /// <param name="impliedRelationshipProvider">The provider supplying implied Relationships.</param>
        internal ResolutionGraph(INamespace rootNamespace, List<INamespace> globalNamespaces, IImpliedRelationshipProvider impliedRelationshipProvider)
        {
            this.rootNamespace = rootNamespace;
            this.globalNamespaces = globalNamespaces;
            this.impliedRelationshipProvider = impliedRelationshipProvider;
        }

        /// <summary>
        /// Translates a Type produced by the implied-relationship layer into this cache's OWN object graph.
        /// </summary>
        /// <param name="impliedGeneral">The general of an implied Specialization, possibly from a foreign graph.</param>
        /// <returns>The same-Id Type of the resolution graph, or <c>null</c> when the graph does not carry it.</returns>
        /// <remarks>
        /// The implied layer may be wired against a SEPARATE library load — a full, model-independent one —
        /// so the generals it returns can be different POCO instances than the ones this cache resolves
        /// against, even for the same library element (same <c>Id</c>). Indexing a foreign instance is
        /// worse than useless: it can never equal a resolution target by reference, so it answers
        /// <c>Shadowed</c> and STOPS the outward walk that would otherwise have found the local instance.
        /// Translating by Id keeps reference equality authoritative everywhere else. A general the
        /// resolution graph does not carry is dropped: its members can never be targets here.
        /// </remarks>
        internal IType TranslateToResolutionGraph(IType impliedGeneral)
        {
            if (impliedGeneral == null)
            {
                return null;
            }

            this.resolutionGraphElementsById ??= this.BuildResolutionGraphIndex();

            if (this.resolutionGraphElementsById.TryGetValue(impliedGeneral.Id, out var local))
            {
                return local as IType;
            }

            return this.IsInResolutionGraph(impliedGeneral) ? impliedGeneral : null;
        }

        /// <summary>
        /// Builds the by-Id index of every Element reachable from the global namespaces.
        /// </summary>
        /// <returns>The index.</returns>
        private Dictionary<Guid, IElement> BuildResolutionGraphIndex()
        {
            var elementsById = new Dictionary<Guid, IElement>();
            var pendingElements = new Queue<IElement>();

            foreach (var globalNamespace in this.globalNamespaces)
            {
                pendingElements.Enqueue(globalNamespace);
            }

            while (pendingElements.Count > 0)
            {
                var current = pendingElements.Dequeue();

                if (!elementsById.TryAdd(current.Id, current))
                {
                    continue;
                }

                foreach (var owned in current.OwnedRelationship.SelectMany(relationship => relationship.OwnedRelatedElement))
                {
                    pendingElements.Enqueue(owned);
                }
            }

            return elementsById;
        }

        /// <summary>
        /// Asserts whether an Element belongs to this cache's own graph, by walking its owners to a known root.
        /// </summary>
        /// <param name="element">The Element to test.</param>
        /// <returns>True when an owner chain reaches the root or a global namespace.</returns>
        private bool IsInResolutionGraph(IElement element)
        {
            for (var current = element; current != null; current = current.owner)
            {
                if (ReferenceEquals(current, this.rootNamespace) || this.globalNamespaces.Contains(current))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Returns every Type reachable from a Type or its declared supertypes through implied
        /// Specializations, transitively.
        /// </summary>
        /// <param name="type">The Type whose implied generals are collected.</param>
        /// <param name="declaredSupertypes">The declared supertypes, which carry implied Specializations of their own.</param>
        /// <returns>The implied generals, without duplicates.</returns>
        internal List<IType> QueryImpliedGeneralClosure(IType type, List<IType> declaredSupertypes)
        {
            var visited = new HashSet<IType>();
            var pendingTypes = new Queue<IType>();

            pendingTypes.Enqueue(type);

            foreach (var declaredSupertype in declaredSupertypes)
            {
                pendingTypes.Enqueue(declaredSupertype);
            }

            var impliedGenerals = new List<IType>();

            while (pendingTypes.Count > 0)
            {
                var current = pendingTypes.Dequeue();

                foreach (var general in this.impliedRelationshipProvider.GetImpliedSpecializations(current)
                             .Select(specialization => this.TranslateToResolutionGraph(specialization.General))
                             .Where(general => general != null && visited.Add(general)))
                {
                    impliedGenerals.Add(general);
                    pendingTypes.Enqueue(general);
                }
            }

            return impliedGenerals;
        }
    }
}
