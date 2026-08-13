// -------------------------------------------------------------------------------------------------
// <copyright file="TypeExtensions.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.Core.POCO.Core.Types
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using SysML2.NET.Core.Core.Types;
    using SysML2.NET.Core.Root.Namespaces;
    using SysML2.NET.Core.POCO.Core.Features;
    using SysML2.NET.Core.POCO.Root.Annotations;
    using SysML2.NET.Core.POCO.Root.Elements;
    using SysML2.NET.Core.POCO.Root.Namespaces;

    /// <summary>
    /// The <see cref="TypeExtensions"/> class provides extensions methods for
    /// the <see cref="IType"/> interface
    /// </summary>
    internal static class TypeExtensions
    {
        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <remarks>
        /// English:
        /// <code>
        /// differencingType = ownedDifferencing.differencingType
        /// </code>
        /// </remarks>
        /// <param name="typeSubject">
        /// The subject <see cref="IType"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        internal static List<IType> ComputeDifferencingType(this IType typeSubject)
        {
            return typeSubject == null
                ? throw new ArgumentNullException(nameof(typeSubject))
                : [..typeSubject.ownedDifferencing.Select(x => x.DifferencingType).Where(x => x != null)];
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// directedFeature = feature-&gt;select(f | directionOf(f) &lt;&gt; null)
        /// </code>
        /// </remarks>
        /// <param name="typeSubject">
        /// The subject <see cref="IType"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        internal static List<IFeature> ComputeDirectedFeature(this IType typeSubject)
        {
            return typeSubject == null
                ? throw new ArgumentNullException(nameof(typeSubject))
                : [..typeSubject.feature.Where(memberFeature => typeSubject.DirectionOf(memberFeature) != null)];
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// endFeature = feature-&gt;select(isEnd)
        /// </code>
        /// </remarks>
        /// <param name="typeSubject">
        /// The subject <see cref="IType"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        internal static List<IFeature> ComputeEndFeature(this IType typeSubject)
        {
            return typeSubject == null
                ? throw new ArgumentNullException(nameof(typeSubject))
                : [..typeSubject.feature.Where(memberFeature => memberFeature.IsEnd)];
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// feature = featureMembership.ownedMemberFeature
        /// </code>
        /// </remarks>
        /// <param name="typeSubject">
        /// The subject <see cref="IType"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        internal static List<IFeature> ComputeFeature(this IType typeSubject)
        {
            return typeSubject == null
                ? throw new ArgumentNullException(nameof(typeSubject))
                : [..typeSubject.featureMembership.Select(membership => membership.ownedMemberFeature).Where(x => x != null)];
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// featureMembership = ownedFeatureMembership-&gt;union(
        ///                             inheritedMembership-&gt;selectByKind(FeatureMembership))
        /// </code>
        /// </remarks>
        /// <param name="typeSubject">
        /// The subject <see cref="IType"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        internal static List<IFeatureMembership> ComputeFeatureMembership(this IType typeSubject)
        {
            return typeSubject == null
                ? throw new ArgumentNullException(nameof(typeSubject))
                : [..typeSubject.ownedFeatureMembership.Union(typeSubject.inheritedMembership.OfType<IFeatureMembership>())];
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// inheritedFeature = inheritedMemberships-&gt;
        ///                             selectByKind(FeatureMembership).memberFeature
        /// </code>
        /// </remarks>
        /// <param name="typeSubject">
        /// The subject <see cref="IType"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        internal static List<IFeature> ComputeInheritedFeature(this IType typeSubject)
        {
            return typeSubject == null
                ? throw new ArgumentNullException(nameof(typeSubject))
                : [..typeSubject.inheritedMembership.OfType<IFeatureMembership>().Select(membership => membership.ownedMemberFeature).Where(x => x != null)];
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// inheritedMembership = inheritedMemberships(Set{}, Set{}, false)
        /// </code>
        /// </remarks>
        /// <param name="typeSubject">
        /// The subject <see cref="IType"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        internal static List<IMembership> ComputeInheritedMembership(this IType typeSubject)
        {
            return typeSubject == null
                ? throw new ArgumentNullException(nameof(typeSubject))
                : typeSubject.InheritedMemberships([], [], false);
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// input = feature-&gt;select(f |
        ///                             let direction: FeatureDirectionKind = directionOf(f) in
        ///                             direction = FeatureDirectionKind::_'in' or
        ///                             direction = FeatureDirectionKind::inout)
        /// </code>
        /// </remarks>
        /// <param name="typeSubject">
        /// The subject <see cref="IType"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        internal static List<IFeature> ComputeInput(this IType typeSubject)
        {
            return typeSubject == null
                ? throw new ArgumentNullException(nameof(typeSubject))
                : [..typeSubject.feature.Where(memberFeature => typeSubject.DirectionOf(memberFeature) is FeatureDirectionKind.In or FeatureDirectionKind.Inout)];
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// intersectingType = ownedIntersecting.intersectingType
        /// </code>
        /// </remarks>
        /// <param name="typeSubject">
        /// The subject <see cref="IType"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        internal static List<IType> ComputeIntersectingType(this IType typeSubject)
        {
            return typeSubject == null
                ? throw new ArgumentNullException(nameof(typeSubject))
                : [..typeSubject.ownedIntersecting.Select(intersecting => intersecting.IntersectingType).Where(x => x != null)];
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <param name="typeSubject">
        /// The subject <see cref="IType"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        internal static bool ComputeIsConjugated(this IType typeSubject)
        {
            return typeSubject == null
                ? throw new ArgumentNullException(nameof(typeSubject))
                : typeSubject.ownedConjugator != null;
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// multiplicity =
        ///                             let ownedMultiplicities: Sequence(Multiplicity) =
        ///                             ownedMember-&gt;selectByKind(Multiplicity) in
        ///                             if ownedMultiplicities-&gt;isEmpty() then null
        ///                             else ownedMultiplicities-&gt;first()
        ///                             endif
        /// </code>
        /// </remarks>
        /// <param name="typeSubject">
        /// The subject <see cref="IType"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        internal static IMultiplicity ComputeMultiplicity(this IType typeSubject)
        {
            return typeSubject == null
                ? throw new ArgumentNullException(nameof(typeSubject))
                : typeSubject.ownedMember.OfType<IMultiplicity>().FirstOrDefault();
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// output = feature-&gt;select(f |
        ///                             let direction: FeatureDirectionKind = directionOf(f) in
        ///                             direction = FeatureDirectionKind::out or
        ///                             direction = FeatureDirectionKind::inout)
        /// </code>
        /// </remarks>
        /// <param name="typeSubject">
        /// The subject <see cref="IType"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        internal static List<IFeature> ComputeOutput(this IType typeSubject)
        {
            return typeSubject == null
                ? throw new ArgumentNullException(nameof(typeSubject))
                : [..typeSubject.feature.Where(memberFeature => typeSubject.DirectionOf(memberFeature) is FeatureDirectionKind.Out or FeatureDirectionKind.Inout)];
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// ownedConjugator =
        ///                             let ownedConjugators: Sequence(Conjugator) =
        ///                             ownedRelationship-&gt;selectByKind(Conjugation) in
        ///                             if ownedConjugators-&gt;isEmpty() then null
        ///                             else ownedConjugators-&gt;at(1) endif
        /// </code>
        /// </remarks>
        /// <param name="typeSubject">
        /// The subject <see cref="IType"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        internal static IConjugation ComputeOwnedConjugator(this IType typeSubject)
        {
            return typeSubject == null
                ? throw new ArgumentNullException(nameof(typeSubject))
                : typeSubject.OwnedRelationship.OfType<IConjugation>().FirstOrDefault();
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// ownedDifferencing =
        ///                             ownedRelationship-&gt;selectByKind(Differencing)
        /// </code>
        /// </remarks>
        /// <param name="typeSubject">
        /// The subject <see cref="IType"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        internal static List<IDifferencing> ComputeOwnedDifferencing(this IType typeSubject)
        {
            return typeSubject == null
                ? throw new ArgumentNullException(nameof(typeSubject))
                : [..typeSubject.OwnedRelationship.OfType<IDifferencing>()];
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// ownedDisjoining =
        ///                             ownedRelationship-&gt;selectByKind(Disjoining)
        /// </code>
        /// </remarks>
        /// <param name="typeSubject">
        /// The subject <see cref="IType"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        internal static List<IDisjoining> ComputeOwnedDisjoining(this IType typeSubject)
        {
            return typeSubject == null
                ? throw new ArgumentNullException(nameof(typeSubject))
                : [..typeSubject.OwnedRelationship.OfType<IDisjoining>()];
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// ownedEndFeature = ownedFeature-&gt;select(isEnd)
        /// </code>
        /// </remarks>
        /// <param name="typeSubject">
        /// The subject <see cref="IType"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        internal static List<IFeature> ComputeOwnedEndFeature(this IType typeSubject)
        {
            return typeSubject == null
                ? throw new ArgumentNullException(nameof(typeSubject))
                : [..typeSubject.ownedFeature.Where(memberFeature => memberFeature.IsEnd)];
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <remarks>
        /// English:
        /// <code>
        /// ownedFeature = ownedFeatureMembership.ownedMemberFeature
        /// </code>
        /// </remarks>
        /// <param name="typeSubject">
        /// The subject <see cref="IType"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        internal static List<IFeature> ComputeOwnedFeature(this IType typeSubject)
        {
            return typeSubject == null
                ? throw new ArgumentNullException(nameof(typeSubject))
                : [..typeSubject.ownedFeatureMembership.Select(membership => membership.ownedMemberFeature).Where(x => x != null)];
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// ownedFeatureMembership = ownedRelationship-&gt;selectByKind(FeatureMembership)
        /// </code>
        /// </remarks>
        /// <param name="typeSubject">
        /// The subject <see cref="IType"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        internal static List<IFeatureMembership> ComputeOwnedFeatureMembership(this IType typeSubject)
        {
            return typeSubject == null
                ? throw new ArgumentNullException(nameof(typeSubject))
                : [..typeSubject.OwnedRelationship.OfType<IFeatureMembership>()];
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// ownedRelationship-&gt;selectByKind(Intersecting)
        /// </code>
        /// </remarks>
        /// <param name="typeSubject">
        /// The subject <see cref="IType"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        internal static List<IIntersecting> ComputeOwnedIntersecting(this IType typeSubject)
        {
            return typeSubject == null
                ? throw new ArgumentNullException(nameof(typeSubject))
                : [..typeSubject.OwnedRelationship.OfType<IIntersecting>()];
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// ownedSpecialization = ownedRelationship-&gt;selectByKind(Specialization)-&gt;
        ///                             select(s | s.special = self)
        /// </code>
        /// </remarks>
        /// <param name="typeSubject">
        /// The subject <see cref="IType"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        internal static List<ISpecialization> ComputeOwnedSpecialization(this IType typeSubject)
        {
            return typeSubject == null
                ? throw new ArgumentNullException(nameof(typeSubject))
                : [..typeSubject.OwnedRelationship.OfType<ISpecialization>().Where(specialization => specialization.Specific == typeSubject)];
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// ownedUnioning =
        ///                             ownedRelationship-&gt;selectByKind(Unioning)
        /// </code>
        /// </remarks>
        /// <param name="typeSubject">
        /// The subject <see cref="IType"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        internal static List<IUnioning> ComputeOwnedUnioning(this IType typeSubject)
        {
            return typeSubject == null
                ? throw new ArgumentNullException(nameof(typeSubject))
                : [..typeSubject.OwnedRelationship.OfType<IUnioning>()];
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// unioningType = ownedUnioning.unioningType
        /// </code>
        /// </remarks>
        /// <param name="typeSubject">
        /// The subject <see cref="IType"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        internal static List<IType> ComputeUnioningType(this IType typeSubject)
        {
            return typeSubject == null
                ? throw new ArgumentNullException(nameof(typeSubject))
                : [..typeSubject.ownedUnioning.Select(unioning => unioning.UnioningType).Where(x => x != null)];
        }

        /// <summary>
        /// The visible Memberships of a Type include inheritedMemberships.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// let visibleMemberships : OrderedSet(Membership) =
        ///                                 self.oclAsType(Namespace).
        ///                                 visibleMemberships(excluded, isRecursive, includeAll) in
        ///                                 let visibleInheritedMemberships : OrderedSet(Membership) =
        ///                                 inheritedMemberships(excluded-&gt;including(self), Set{}, isRecursive)-&gt;
        ///                                 select(includeAll or visibility = VisibilityKind::public) in
        ///                                 visibleMemberships-&gt;union(visibleInheritedMemberships)
        /// </code>
        /// </remarks>
        /// <param name="typeSubject">
        /// The subject <see cref="IType"/>
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
        internal static List<IMembership> ComputeRedefinedVisibleMembershipsOperation(this IType typeSubject, List<INamespace> excluded, bool isRecursive, bool includeAll)
        {
            if (typeSubject == null)
            {
                throw new ArgumentNullException(nameof(typeSubject));
            }

            var safeExcluded = excluded ?? [];

            // Call NamespaceExtensions directly: Type.VisibleMemberships is wired back to this method,
            // so going through INamespace.VisibleMemberships would recurse infinitely.
            var visibleMemberships = typeSubject.ComputeVisibleMembershipsOperation(safeExcluded, isRecursive, includeAll);

            var excludedWithSelf = new List<INamespace>(safeExcluded) { typeSubject };

            var visibleInheritedMemberships = typeSubject.InheritedMemberships(excludedWithSelf, [], isRecursive)
                .Where(membership => includeAll || membership.Visibility == VisibilityKind.Public);

            return [..visibleMemberships.Union(visibleInheritedMemberships)];
        }

        /// <summary>
        /// Return the Memberships inheritable from supertypes of this Type with redefined Features removed.
        /// When computing inheritable Memberships, exclude Imports of excludedNamespaces, Specializations of
        /// excludedTypes, and, if excludeImplied = true, all implied Specializations.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// removeRedefinedFeatures(
        ///                                 inheritableMemberships(excludedNamespaces, excludedTypes, excludeImplied))
        /// </code>
        /// </remarks>
        /// <param name="typeSubject">
        /// The subject <see cref="IType"/>
        /// </param>
        /// <param name="excludedNamespaces">
        /// No documentation provided
        /// </param>
        /// <param name="excludedTypes">
        /// No documentation provided
        /// </param>
        /// <param name="excludeImplied">
        /// No documentation provided
        /// </param>
        /// <returns>
        /// The expected collection of <see cref="IMembership" />
        /// </returns>
        internal static List<IMembership> ComputeInheritedMembershipsOperation(this IType typeSubject, List<INamespace> excludedNamespaces, List<IType> excludedTypes, bool excludeImplied)
        {
            if (typeSubject == null)
            {
                throw new ArgumentNullException(nameof(typeSubject));
            }

            var inheritable = new List<IMembership>();

            CollectInheritableMemberships(typeSubject, new InheritanceQuery(excludedNamespaces, excludedTypes, excludeImplied), inheritable);

            return typeSubject.RemoveRedefinedFeatures(inheritable);
        }

        /// <summary>
        /// Carries the values that stay fixed for one top-level inheritance query, together with the
        /// specialization path being walked and the results that may be reused across branches of it.
        /// </summary>
        /// <remarks>
        /// <c>excludedNamespaces</c> and <c>excludeImplied</c> are passed down the recursion unchanged, so
        /// within one query the only thing that varies is the path — which is why
        /// <see cref="PathIndependentResults"/> can be keyed on the Type alone and why the memo must NOT
        /// outlive the query. The model is a mutable object graph; a cache that survived the call would
        /// need invalidation the SDK has no hook for.
        /// </remarks>
        private sealed class InheritanceQuery
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="InheritanceQuery"/> class.
            /// </summary>
            /// <param name="excludedNamespaces">
            /// The Namespaces whose Imports are excluded, or null for none
            /// </param>
            /// <param name="excludedTypes">
            /// The Types to seed the specialization path with, or null for none
            /// </param>
            /// <param name="excludeImplied">
            /// Whether supertypes reached through implied Specializations are excluded
            /// </param>
            internal InheritanceQuery(List<INamespace> excludedNamespaces, List<IType> excludedTypes, bool excludeImplied)
            {
                this.ExcludedNamespaces = excludedNamespaces ?? [];
                this.PathTypes = [..excludedTypes ?? []];
                this.ExcludeImplied = excludeImplied;
            }

            /// <summary>
            /// Gets the Namespaces whose Imports are excluded.
            /// </summary>
            internal List<INamespace> ExcludedNamespaces { get; }

            /// <summary>
            /// Gets the Types on the specialization path currently being walked, mutated in place as the
            /// recursion descends and ascends.
            /// </summary>
            internal HashSet<IType> PathTypes { get; }

            /// <summary>
            /// Gets a value indicating whether supertypes reached through implied Specializations are excluded.
            /// </summary>
            internal bool ExcludeImplied { get; }

            /// <summary>
            /// Gets the non-private Memberships of Types whose subtree never consulted the cycle guard, and
            /// which are therefore the same no matter which path reaches them.
            /// </summary>
            internal Dictionary<IType, List<IMembership>> PathIndependentResults { get; } = [];
        }

        /// <summary>
        /// Collects the inheritable Memberships of a Type into <paramref name="result"/>, and reports whether
        /// the subtree it walked is independent of the path that reached it.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This helper and <see cref="ResolveNonPrivateMemberships"/> carry the mutual recursion
        /// <c>inheritedMemberships -&gt; inheritableMemberships -&gt; nonPrivateMemberships -&gt;
        /// inheritedMemberships</c>. The public operations keep their <see cref="List{T}"/> signatures, but
        /// the recursion runs through these helpers so the excluded Types can be ONE set pushed on descent
        /// and popped on return, rather than a fresh List allocated per supertype per level and searched
        /// with an O(n) <c>Contains</c>. Calling the helpers directly bypasses POCO dispatch, which is safe
        /// ONLY because none of those three operations is redefined by any subclass — every POCO wires all
        /// three to the methods in this file. Introducing a redefinition means routing that arm back through
        /// the POCO instance member.
        /// </para>
        /// <para>
        /// The returned flag is false as soon as the cycle guard rejects a supertype anywhere below this
        /// Type. It gates memoisation: in an ACYCLIC hierarchy every Type on the path to a Type T is a
        /// subtype of T, so no path node can also be one of T's supertypes and the guard never fires —
        /// making T's result reusable. The guard firing means the graph is circular through this Type, the
        /// answer genuinely differs per path (KerML §8.2.3.5.1), and nothing here may be reused.
        /// </para>
        /// </remarks>
        /// <param name="typeSubject">
        /// The subject <see cref="IType"/>
        /// </param>
        /// <param name="query">
        /// The state of the top-level inheritance query
        /// </param>
        /// <param name="result">
        /// The accumulator the Memberships are appended to
        /// </param>
        /// <returns>
        /// True when no cycle guard fired in the walked subtree
        /// </returns>
        private static bool CollectInheritableMemberships(IType typeSubject, InheritanceQuery query, List<IMembership> result)
        {
            var addedSelf = query.PathTypes.Add(typeSubject);

            // Failing to add means this Type is ALREADY on the path, i.e. a cycle closes on the subject
            // itself — path-dependent by construction.
            var isPathIndependent = addedSelf;

            try
            {
                foreach (var supertype in typeSubject.Supertypes(query.ExcludeImplied).Where(supertype => supertype != null))
                {
                    if (query.PathTypes.Contains(supertype))
                    {
                        isPathIndependent = false;
                        continue;
                    }

                    result.AddRange(ResolveNonPrivateMemberships(supertype, query, out var supertypeIsPathIndependent));

                    isPathIndependent &= supertypeIsPathIndependent;
                }
            }
            finally
            {
                if (addedSelf)
                {
                    query.PathTypes.Remove(typeSubject);
                }
            }

            return isPathIndependent;
        }

        /// <summary>
        /// Returns the public, protected and inherited Memberships of a Type, reusing an earlier result when
        /// that Type proved independent of the path reaching it.
        /// </summary>
        /// <remarks>
        /// See <see cref="CollectInheritableMemberships"/> for why the recursion runs through these helpers
        /// and what makes a result reusable. The returned list may be the memoised instance, so callers must
        /// read from it and never mutate it.
        /// </remarks>
        /// <param name="typeSubject">
        /// The subject <see cref="IType"/>
        /// </param>
        /// <param name="query">
        /// The state of the top-level inheritance query
        /// </param>
        /// <param name="isPathIndependent">
        /// True when no cycle guard fired in the walked subtree
        /// </param>
        /// <returns>
        /// The collected <see cref="IMembership"/>
        /// </returns>
        private static List<IMembership> ResolveNonPrivateMemberships(IType typeSubject, InheritanceQuery query, out bool isPathIndependent)
        {
            if (query.PathIndependentResults.TryGetValue(typeSubject, out var reusable))
            {
                isPathIndependent = true;

                return reusable;
            }

            // The OCL joins the three parts with `union`, which deduplicates — but only within THIS call.
            // inheritableMemberships deliberately concatenates its supertypes' results without deduplicating
            // across them, so the seen-set must not outlive this invocation.
            var seen = new HashSet<IMembership>();
            var result = new List<IMembership>();

            AppendDistinct(result, seen, typeSubject.MembershipsOfVisibility(VisibilityKind.Public, query.ExcludedNamespaces));
            AppendDistinct(result, seen, typeSubject.MembershipsOfVisibility(VisibilityKind.Protected, query.ExcludedNamespaces));

            var inheritable = new List<IMembership>();

            isPathIndependent = CollectInheritableMemberships(typeSubject, query, inheritable);

            AppendDistinct(result, seen, typeSubject.RemoveRedefinedFeatures(inheritable));

            if (isPathIndependent)
            {
                query.PathIndependentResults[typeSubject] = result;
            }

            return result;
        }

        /// <summary>
        /// Appends the Memberships of <paramref name="source"/> that are not yet in <paramref name="seen"/>.
        /// </summary>
        /// <param name="target">
        /// The accumulator the Memberships are appended to
        /// </param>
        /// <param name="seen">
        /// The set of Memberships already appended, extended in place
        /// </param>
        /// <param name="source">
        /// The Memberships to append
        /// </param>
        private static void AppendDistinct(List<IMembership> target, HashSet<IMembership> seen, List<IMembership> source)
        {
            // `seen.Add` is the filter AND the record of what was taken; AddRange enumerates once, in order,
            // so the side effect is well defined here.
            target.AddRange(source.Where(seen.Add));
        }

        /// <summary>
        /// Return all the non-private Memberships of all the supertypes of this Type, excluding any supertypes
        /// that are this Type or are in the given set of excludedTypes. If excludeImplied = true, then also
        /// transitively exclude any supertypes from implied Specializations.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// let excludingSelf : Set(Type) = excludedType-&gt;including(self) in
        ///                                 supertypes(excludeImplied)-&gt;reject(t | excludingSelf-&gt;includes(t)).
        ///                                 nonPrivateMemberships(excludedNamespaces, excludingSelf, excludeImplied)
        /// </code>
        /// </remarks>
        /// <param name="typeSubject">
        /// The subject <see cref="IType"/>
        /// </param>
        /// <param name="excludedNamespaces">
        /// No documentation provided
        /// </param>
        /// <param name="excludedTypes">
        /// No documentation provided
        /// </param>
        /// <param name="excludeImplied">
        /// No documentation provided
        /// </param>
        /// <returns>
        /// The expected collection of <see cref="IMembership" />
        /// </returns>
        internal static List<IMembership> ComputeInheritableMembershipsOperation(this IType typeSubject, List<INamespace> excludedNamespaces, List<IType> excludedTypes, bool excludeImplied)
        {
            if (typeSubject == null)
            {
                throw new ArgumentNullException(nameof(typeSubject));
            }

            var result = new List<IMembership>();

            CollectInheritableMemberships(typeSubject, new InheritanceQuery(excludedNamespaces, excludedTypes, excludeImplied), result);

            return result;
        }

        /// <summary>
        /// Return the public, protected and inherited Memberships of this Type. When computing imported
        /// Memberships, exclude the given set of excludedNamespaces. When computing inherited Memberships,
        /// exclude Types in the given set of excludedTypes. If excludeImplied = true, then also exclude any
        /// supertypes from implied Specializations.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// let publicMemberships : OrderedSet(Membership) =
        ///                                 membershipsOfVisibility(VisibilityKind::public, excludedNamespaces) in
        ///                                 let protectedMemberships : OrderedSet(Membership) =
        ///                                 membershipsOfVisibility(VisibilityKind::protected, excludedNamespaces) in
        ///                                 let inheritedMemberships : OrderedSet(Membership) =
        ///                                 inheritedMemberships(excludedNamespaces, excludedTypes, excludeImplied) in
        ///                                 publicMemberships-&gt;
        ///                                 union(protectedMemberships)-&gt;
        ///                                 union(inheritedMemberships)
        /// </code>
        /// </remarks>
        /// <param name="typeSubject">
        /// The subject <see cref="IType"/>
        /// </param>
        /// <param name="excludedNamespaces">
        /// No documentation provided
        /// </param>
        /// <param name="excludedTypes">
        /// No documentation provided
        /// </param>
        /// <param name="excludeImplied">
        /// No documentation provided
        /// </param>
        /// <returns>
        /// The expected collection of <see cref="IMembership" />
        /// </returns>
        internal static List<IMembership> ComputeNonPrivateMembershipsOperation(this IType typeSubject, List<INamespace> excludedNamespaces, List<IType> excludedTypes, bool excludeImplied)
        {
            if (typeSubject == null)
            {
                throw new ArgumentNullException(nameof(typeSubject));
            }

            return ResolveNonPrivateMemberships(typeSubject, new InheritanceQuery(excludedNamespaces, excludedTypes, excludeImplied), out _);
        }

        /// <summary>
        /// Return a subset of memberships, removing those Memberships whose memberElements are Features and for
        /// which either of the following two conditions holds:                            <ol>
        ///           <li>The memberElement of the Membership is included in redefined Features of another
        /// Membership in memberships.</li>                            <li>One of the redefined Features of the
        /// Membership is a directly redefinedFeature of an ownedFeature of this Type.</li>
        ///       </ol>                            For this purpose, the redefined Features of a Membership
        /// whose memberElement is a Feature includes the memberElement and all Features directly or indirectly
        /// redefined by the memberElement.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// let reducedMemberships : Sequence(Membership) =
        ///                                 memberships-&gt;reject(mem1 |
        ///                                 memberships-&gt;excluding(mem1)-&gt;
        ///                                 exists(mem2 | allRedefinedFeaturesOf(mem2)-&gt;
        ///                                 includes(mem1.memberElement))) in
        ///                                 let redefinedFeatures : Set(Feature) =
        ///                                 ownedFeature.redefinition.redefinedFeature-&gt;asSet() in
        ///                                 reducedMemberships-&gt;reject(mem | allRedefinedFeaturesOf(mem)-&gt;
        ///                                 exists(feature | redefinedFeatures-&gt;includes(feature)))
        /// </code>
        /// </remarks>
        /// <param name="typeSubject">
        /// The subject <see cref="IType"/>
        /// </param>
        /// <param name="memberships">
        /// No documentation provided
        /// </param>
        /// <returns>
        /// The expected collection of <see cref="IMembership" />
        /// </returns>
        internal static List<IMembership> ComputeRemoveRedefinedFeaturesOperation(this IType typeSubject, List<IMembership> memberships)
        {
            if (typeSubject == null)
            {
                throw new ArgumentNullException(nameof(typeSubject));
            }

            if (memberships == null)
            {
                throw new ArgumentNullException(nameof(memberships));
            }

            // AllRedefinedFeaturesOf walks a redefinition chain, so it is computed ONCE per membership
            // here rather than inside a nested loop: the previous form was O(n^2) calls over the whole
            // inherited closure. Condition 1 then reduces to a set lookup — a membership is rejected when
            // some OTHER membership in the same set redefines its memberElement. Because
            // AllRedefinedFeaturesOf always includes the membership's own memberElement, "some other"
            // is exactly "the feature appears in at least two memberships' redefined-sets".
            // Memberships may arrive more than once (the same supertype membership is reachable by several
            // paths through a branching hierarchy), so distinct memberships are counted — mirroring the
            // original `other != current` guard, which likewise never let a membership reject itself.
            var redefinedFeatureCounts = new Dictionary<IFeature, int>();

            foreach (var redefinedFeature in memberships
                         .Distinct()
                         .SelectMany(membership => typeSubject.AllRedefinedFeaturesOf(membership).Distinct()))
            {
                redefinedFeatureCounts.TryGetValue(redefinedFeature, out var occurrences);
                redefinedFeatureCounts[redefinedFeature] = occurrences + 1;
            }

            var reducedMemberships = memberships
                .Where(currentMembership => currentMembership.MemberElement is not IFeature memberFeature
                                            || !redefinedFeatureCounts.TryGetValue(memberFeature, out var occurrences)
                                            || occurrences < 2)
                .ToList();

            var redefinedFeatures = typeSubject.ownedFeature
                .SelectMany(ownedFeature => ownedFeature.OwnedRelationship.OfType<IRedefinition>())
                .Select(redefinition => redefinition.RedefinedFeature)
                .Where(x => x != null)
                .ToHashSet();

            return
            [
                ..reducedMemberships.Where(membership =>
                    !typeSubject.AllRedefinedFeaturesOf(membership).Any(redefinedFeatures.Contains))
            ];
        }

        /// <summary>
        /// If the memberElement of the given membership is a Feature, then return all Features directly or
        /// indirectly redefined by the memberElement.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// if not membership.memberElement.oclIsType(Feature) then Set{}
        ///                                 else membership.memberElement.oclAsType(Feature).allRedefinedFeatures()
        ///                                 endif
        /// </code>
        /// </remarks>
        /// <param name="typeSubject">
        /// The subject <see cref="IType"/>
        /// </param>
        /// <param name="membership">
        /// No documentation provided
        /// </param>
        /// <returns>
        /// The expected collection of <see cref="IFeature" />
        /// </returns>
        internal static List<IFeature> ComputeAllRedefinedFeaturesOfOperation(this IType typeSubject, IMembership membership)
        {
            if (typeSubject == null)
            {
                throw new ArgumentNullException(nameof(typeSubject));
            }

            if (membership == null)
            {
                throw new ArgumentNullException(nameof(membership));
            }

            // `oclIsType(Feature)` is read as a KIND check (`is IFeature`), not as OCL's exact-type test.
            // Feature is instantiated in practice only through its subclasses, so the literal exact-type
            // reading would make redefinition hiding never fire on any real model — which cannot be the
            // intent of a rule whose whole purpose is to remove redefined Features from inheritance.
            return membership.MemberElement is IFeature memberFeature
                ? memberFeature.AllRedefinedFeatures()
                : [];
        }

        /// <summary>
        /// If the given feature is a feature of this Type, then return its direction relative to this Type,
        /// taking conjugation into account.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// directionOfExcluding(f, Set{})
        /// </code>
        /// </remarks>
        /// <param name="typeSubject">
        /// The subject <see cref="IType"/>
        /// </param>
        /// <param name="feature">
        /// No documentation provided
        /// </param>
        /// <returns>
        /// The expected <see cref="FeatureDirectionKind" />
        /// </returns>
        internal static FeatureDirectionKind? ComputeDirectionOfOperation(this IType typeSubject, IFeature feature)
        {
            if (typeSubject == null)
            {
                throw new ArgumentNullException(nameof(typeSubject));
            }

            if (feature == null)
            {
                throw new ArgumentNullException(nameof(feature));
            }

            return typeSubject.DirectionOfExcluding(feature, []);
        }

        /// <summary>
        /// Return the direction of the given feature relative to this Type, excluding a given set of Types from
        /// the search of supertypes of this Type.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// let excludedSelf : Set(Type) = excluded-&gt;including(self) in
        ///                                 if feature.owningType = self then feature.direction
        ///                                 else
        ///                                 let directions : Sequence(FeatureDirectionKind) =
        ///                                 supertypes(false)-&gt;excluding(excludedSelf).
        ///                                 directionOfExcluding(feature, excludedSelf)-&gt;
        ///                                 select(d | d &lt;&gt; null) in
        ///                                 if directions-&gt;isEmpty() then null
        ///                                 else
        ///                                 let direction : FeatureDirectionKind = directions-&gt;first() in
        ///                                 if not isConjugated then direction
        ///                                 else if direction = FeatureDirectionKind::_'in' then FeatureDirectionKind::out
        ///                                 else if direction = FeatureDirectionKind::out then FeatureDirectionKind::_'in'
        ///                                 else direction
        ///                                 endif endif endif   endif
        ///                                 endif
        /// </code>
        /// </remarks>
        /// <param name="typeSubject">
        /// The subject <see cref="IType"/>
        /// </param>
        /// <param name="feature">
        /// No documentation provided
        /// </param>
        /// <param name="excluded">
        /// No documentation provided
        /// </param>
        /// <returns>
        /// The expected <see cref="FeatureDirectionKind" />
        /// </returns>
        internal static FeatureDirectionKind? ComputeDirectionOfExcludingOperation(this IType typeSubject, IFeature feature, List<IType> excluded)
        {
            if (typeSubject == null)
            {
                throw new ArgumentNullException(nameof(typeSubject));
            }

            if (feature == null)
            {
                throw new ArgumentNullException(nameof(feature));
            }

            var excludedWithSelf = new List<IType>(excluded ?? []) { typeSubject };

            if (feature.owningType == typeSubject)
            {
                return feature.Direction;
            }

            FeatureDirectionKind? direction = null;

            foreach (var supertype in typeSubject.Supertypes(false))
            {
                if (supertype == null || excludedWithSelf.Contains(supertype))
                {
                    continue;
                }

                var supertypeDirection = supertype.DirectionOfExcluding(feature, excludedWithSelf);

                if (supertypeDirection != null)
                {
                    direction = supertypeDirection;
                    break;
                }
            }

            if (direction == null)
            {
                return null;
            }

            return typeSubject.isConjugated
                ? direction switch
                {
                    FeatureDirectionKind.In => FeatureDirectionKind.Out,
                    FeatureDirectionKind.Out => FeatureDirectionKind.In,
                    _ => direction
                }
                : direction;
        }

        /// <summary>
        /// If this Type is conjugated, then return just the originalType of the Conjugation. Otherwise, return
        /// the general Types from all ownedSpecializations of this type, if excludeImplied = false, or all
        /// non-implied ownedSpecializations, if excludeImplied = true.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// if isConjugated then Sequence{conjugator.originalType}
        ///                                 else if not excludeImplied then ownedSpecialization.general
        ///                                 else ownedSpecialization-&gt;reject(isImplied).general
        ///                                 endif
        ///                                 endif
        /// </code>
        /// </remarks>
        /// <param name="typeSubject">
        /// The subject <see cref="IType"/>
        /// </param>
        /// <param name="excludeImplied">
        /// No documentation provided
        /// </param>
        /// <returns>
        /// The expected collection of <see cref="IType" />
        /// </returns>
        internal static List<IType> ComputeSupertypesOperation(this IType typeSubject, bool excludeImplied)
        {
            if (typeSubject == null)
            {
                throw new ArgumentNullException(nameof(typeSubject));
            }

            if (typeSubject.isConjugated)
            {
                var originalType = typeSubject.ownedConjugator?.OriginalType;

                return originalType == null ? [] : [originalType];
            }

            var specializations = excludeImplied
                ? typeSubject.ownedSpecialization.Where(specialization => !specialization.IsImplied)
                : typeSubject.ownedSpecialization;

            return [..specializations.Select(specialization => specialization.General).Where(x => x != null)];
        }

        /// <summary>
        /// Return this Type and all Types that are directly or transitively supertypes of this Type (as
        /// determined by the supertypes operation with excludeImplied = false).
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// OrderedSet{self}-&gt;closure(supertypes(false))
        /// </code>
        /// </remarks>
        /// <param name="typeSubject">
        /// The subject <see cref="IType"/>
        /// </param>
        /// <returns>
        /// The expected collection of <see cref="IType" />
        /// </returns>
        internal static List<IType> ComputeAllSupertypesOperation(this IType typeSubject)
        {
            if (typeSubject == null)
            {
                throw new ArgumentNullException(nameof(typeSubject));
            }

            // The result is an OrderedSet, so discovery order is kept in `ordered` while `visited` answers
            // membership in O(1) — a List doing both made the BFS O(n^2).
            var ordered = new List<IType> { typeSubject };
            var visited = new HashSet<IType> { typeSubject };
            var queue = new Queue<IType>();
            queue.Enqueue(typeSubject);

            while (queue.Count > 0)
            {
                foreach (var supertype in queue.Dequeue().Supertypes(false).Where(supertype => supertype != null && visited.Add(supertype)))
                {
                    ordered.Add(supertype);
                    queue.Enqueue(supertype);
                }
            }

            return ordered;
        }

        /// <summary>
        /// Check whether this Type is a direct or indirect specialization of the given supertype.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// if isConjugated then
        ///                                 ownedConjugator.originalType.specializes(supertype)
        ///                                 else
        ///                                 allSupertypes()-&gt;includes(supertype)
        ///                                 endif
        /// </code>
        /// </remarks>
        /// <param name="typeSubject">
        /// The subject <see cref="IType"/>
        /// </param>
        /// <param name="supertype">
        /// No documentation provided
        /// </param>
        /// <returns>
        /// The expected <see cref="bool" />
        /// </returns>
        internal static bool ComputeSpecializesOperation(this IType typeSubject, IType supertype)
        {
            if (typeSubject == null)
            {
                throw new ArgumentNullException(nameof(typeSubject));
            }

            if (supertype == null)
            {
                throw new ArgumentNullException(nameof(supertype));
            }

            if (typeSubject.isConjugated)
            {
                var originalType = typeSubject.ownedConjugator?.OriginalType;

                return originalType != null && originalType.Specializes(supertype);
            }

            return typeSubject.AllSupertypes().Contains(supertype);
        }

        /// <summary>
        /// Check whether this Type is a direct or indirect specialization of the named library Type.
        /// libraryTypeName must conform to the syntax of a KerML qualified name and must resolve to a Type in
        /// global scope.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// let mem : Membership = resolveGlobal(libraryTypeName) in
        ///                                 mem &lt;&gt; null and mem.memberElement.oclIsKindOf(Type) and
        ///                                 specializes(mem.memberElement.oclAsType(Type))
        /// </code>
        /// </remarks>
        /// <param name="typeSubject">
        /// The subject <see cref="IType"/>
        /// </param>
        /// <param name="libraryTypeName">
        /// No documentation provided
        /// </param>
        /// <returns>
        /// The expected <see cref="bool" />
        /// </returns>
        internal static bool ComputeSpecializesFromLibraryOperation(this IType typeSubject, string libraryTypeName)
        {
            if (typeSubject == null)
            {
                throw new ArgumentNullException(nameof(typeSubject));
            }

            if (string.IsNullOrWhiteSpace(libraryTypeName))
            {
                return false;
            }

            var resolved = typeSubject.ResolveGlobal(libraryTypeName);

            return resolved?.MemberElement is IType libraryType && typeSubject.Specializes(libraryType);
        }

        /// <summary>
        /// By default, this Type is compatible with an otherType if it directly or indirectly specializes the
        /// otherType.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// specializes(otherType)
        /// </code>
        /// </remarks>
        /// <param name="typeSubject">
        /// The subject <see cref="IType"/>
        /// </param>
        /// <param name="otherType">
        /// No documentation provided
        /// </param>
        /// <returns>
        /// The expected <see cref="bool" />
        /// </returns>
        internal static bool ComputeIsCompatibleWithOperation(this IType typeSubject, IType otherType)
        {
            if (typeSubject == null)
            {
                throw new ArgumentNullException(nameof(typeSubject));
            }

            if (otherType == null)
            {
                throw new ArgumentNullException(nameof(otherType));
            }

            return typeSubject.Specializes(otherType);
        }

        /// <summary>
        /// Return the owned or inherited Multiplicities for this Type
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// if multiplicity &lt;&gt; null then OrderedSet{multiplicity}
        ///                                 else
        ///                                 ownedSpecialization.general-&gt;closure(t |
        ///                                 if t.multiplicity &lt;&gt; null then OrderedSet{}
        ///                                 else ownedSpecialization.general
        ///                                 )-&gt;select(multiplicity &lt;&gt; null).multiplicity-&gt;asOrderedSet()
        ///                                 endif
        /// </code>
        /// </remarks>
        /// <param name="typeSubject">
        /// The subject <see cref="IType"/>
        /// </param>
        /// <returns>
        /// The expected collection of <see cref="IMultiplicity" />
        /// </returns>
        internal static List<IMultiplicity> ComputeMultiplicitiesOperation(this IType typeSubject)
        {
            if (typeSubject == null)
            {
                throw new ArgumentNullException(nameof(typeSubject));
            }

            if (typeSubject.multiplicity != null)
            {
                return [typeSubject.multiplicity];
            }

            var visited = new HashSet<IType> { typeSubject };
            var result = new List<IMultiplicity>();
            var queue = new Queue<IType>();

            foreach (var general in typeSubject.ownedSpecialization.Select(specialization => specialization.General).Where(x => x != null && visited.Add(x)))
            {
                queue.Enqueue(general);
            }

            while (queue.Count > 0)
            {
                var current = queue.Dequeue();

                if (current.multiplicity != null)
                {
                    result.Add(current.multiplicity);
                    continue;
                }

                foreach (var general in current.ownedSpecialization.Select(specialization => specialization.General).Where(x => x != null && visited.Add(x)))
                {
                    queue.Enqueue(general);
                }
            }

            return result;
        }
    }
}
