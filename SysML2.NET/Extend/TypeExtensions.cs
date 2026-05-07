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
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        internal static List<IType> ComputeDifferencingType(this IType typeSubject)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
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
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        internal static List<IFeature> ComputeDirectedFeature(this IType typeSubject)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
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
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        internal static List<IFeature> ComputeEndFeature(this IType typeSubject)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
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
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        internal static List<IFeature> ComputeFeature(this IType typeSubject)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
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
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        internal static List<IFeatureMembership> ComputeFeatureMembership(this IType typeSubject)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
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
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        internal static List<IFeature> ComputeInheritedFeature(this IType typeSubject)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
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
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        internal static List<IMembership> ComputeInheritedMembership(this IType typeSubject)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
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
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        internal static List<IFeature> ComputeInput(this IType typeSubject)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
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
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        internal static List<IType> ComputeIntersectingType(this IType typeSubject)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
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
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        internal static bool ComputeIsConjugated(this IType typeSubject)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
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
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        internal static IMultiplicity ComputeMultiplicity(this IType typeSubject)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
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
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        internal static List<IFeature> ComputeOutput(this IType typeSubject)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
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
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        internal static IConjugation ComputeOwnedConjugator(this IType typeSubject)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
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
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        internal static List<IDifferencing> ComputeOwnedDifferencing(this IType typeSubject)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
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
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        internal static List<IDisjoining> ComputeOwnedDisjoining(this IType typeSubject)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
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
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        internal static List<IFeature> ComputeOwnedEndFeature(this IType typeSubject)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
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
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        internal static List<IFeature> ComputeOwnedFeature(this IType typeSubject)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
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
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        internal static List<IFeatureMembership> ComputeOwnedFeatureMembership(this IType typeSubject)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
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
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        internal static List<IIntersecting> ComputeOwnedIntersecting(this IType typeSubject)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
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
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        internal static List<ISpecialization> ComputeOwnedSpecialization(this IType typeSubject)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
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
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        internal static List<IUnioning> ComputeOwnedUnioning(this IType typeSubject)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
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
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        internal static List<IType> ComputeUnioningType(this IType typeSubject)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
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
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        internal static List<IMembership> ComputeRedefinedVisibleMembershipsOperation(this IType typeSubject, List<INamespace> excluded, bool isRecursive, bool includeAll)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
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
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        internal static List<IMembership> ComputeInheritedMembershipsOperation(this IType typeSubject, List<INamespace> excludedNamespaces, List<IType> excludedTypes, bool excludeImplied)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
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
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        internal static List<IMembership> ComputeInheritableMembershipsOperation(this IType typeSubject, List<INamespace> excludedNamespaces, List<IType> excludedTypes, bool excludeImplied)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
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
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        internal static List<IMembership> ComputeNonPrivateMembershipsOperation(this IType typeSubject, List<INamespace> excludedNamespaces, List<IType> excludedTypes, bool excludeImplied)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
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
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        internal static List<IMembership> ComputeRemoveRedefinedFeaturesOperation(this IType typeSubject, List<IMembership> memberships)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
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
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        internal static List<IFeature> ComputeAllRedefinedFeaturesOfOperation(this IType typeSubject, IMembership membership)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
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
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        internal static FeatureDirectionKind? ComputeDirectionOfOperation(this IType typeSubject, IFeature feature)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
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
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        internal static FeatureDirectionKind? ComputeDirectionOfExcludingOperation(this IType typeSubject, IFeature feature, List<IType> excluded)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
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
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        internal static List<IType> ComputeSupertypesOperation(this IType typeSubject, bool excludeImplied)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
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
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        internal static List<IType> ComputeAllSupertypesOperation(this IType typeSubject)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
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
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        internal static bool ComputeSpecializesOperation(this IType typeSubject, IType supertype)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
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
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        internal static bool ComputeSpecializesFromLibraryOperation(this IType typeSubject, string libraryTypeName)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
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
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        internal static bool ComputeIsCompatibleWithOperation(this IType typeSubject, IType otherType)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }

        /// <summary>
        /// Return the owned or inherited Multiplicities for this Type<./code>.
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
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        internal static List<IMultiplicity> ComputeMultiplicitiesOperation(this IType typeSubject)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }
    }
}
