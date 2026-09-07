// -------------------------------------------------------------------------------------------------
// <copyright file="LocalScopeResolver.cs" company="Starion Group S.A.">
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

    using SysML2.NET.Core.POCO.Core.Features;
    using SysML2.NET.Core.POCO.Core.Types;
    using SysML2.NET.Core.POCO.Kernel.Behaviors;
    using SysML2.NET.Core.POCO.Kernel.Connectors;
    using SysML2.NET.Core.POCO.Kernel.Expressions;
    using SysML2.NET.Core.POCO.Kernel.FeatureValues;
    using SysML2.NET.Core.POCO.Kernel.Interactions;
    using SysML2.NET.Core.POCO.Root.Elements;
    using SysML2.NET.Core.POCO.Root.Namespaces;
    using SysML2.NET.Core.POCO.Systems.Actions;

    /// <summary>
    /// Determines the local Namespace a reference resolves against, by the kind of Relationship the
    /// reference is the target of (KerML §8.2.3.5.2).
    /// </summary>
    internal static class LocalScopeResolver
    {
        /// <summary>
        /// Materialises the <c>owningNamespace</c> chain from <paramref name="start" /> up to the root, and
        /// locates <paramref name="matchFloorScope" /> in it.
        /// </summary>
        /// <param name="start">The starting namespace.</param>
        /// <param name="matchFloorScope">The innermost scope a match may come from (may be <see langword="null" />).</param>
        /// <returns>The chain.</returns>
        internal static SourceScopeChain BuildChain(INamespace start, INamespace matchFloorScope)
        {
            var scopes = new List<INamespace>();
            var current = start;

            while (current != null)
            {
                scopes.Add(current);
                current = current.owningNamespace;
            }

            return new SourceScopeChain(scopes, QueryMatchFloorDepth(scopes, matchFloorScope));
        }

        /// <summary>
        /// Resolves the local scope of <paramref name="sourcePoco" />: the first <see cref="INamespace" />
        /// reached by climbing <c>OwningRelatedElement</c>, <c>owningNamespace</c>, then <c>owner</c>.
        /// An anonymous nested namespace (no upward chain of its own) is skipped via <c>owner</c> so the
        /// reference site's real enclosing scope is found. Falls back to <paramref name="rootNamespace" />.
        /// </summary>
        /// <param name="sourcePoco">The source POCO; may be <see langword="null" />.</param>
        /// <param name="rootNamespace">The scope returned when no enclosing namespace is reached.</param>
        /// <returns>The local scope or <paramref name="rootNamespace" />.</returns>
        internal static INamespace GetSourceLocalScope(IElement sourcePoco, INamespace rootNamespace)
        {
            if (sourcePoco == null)
            {
                return rootNamespace;
            }

            if (QueryContextRelationshipLocalScope(sourcePoco) is { } contextScope)
            {
                return contextScope;
            }

            var visited = new HashSet<IElement>();
            var current = sourcePoco;

            while (current != null && visited.Add(current))
            {
                if (current is IRelationship { OwningRelatedElement: not null } relationship)
                {
                    current = relationship.OwningRelatedElement;
                    continue;
                }

                if (current is INamespace asNamespace)
                {
                    if (asNamespace.owningNamespace != null || ReferenceEquals(asNamespace, rootNamespace))
                    {
                        return asNamespace;
                    }

                    var namespaceOwner = asNamespace.owner;

                    if (namespaceOwner == null)
                    {
                        return asNamespace;
                    }

                    current = namespaceOwner;
                    continue;
                }

                var owningNamespace = current.owningNamespace;

                if (owningNamespace != null)
                {
                    return owningNamespace;
                }

                current = current.owner;
            }

            return rootNamespace;
        }

        /// <summary>
        /// Determines whether <paramref name="sourcePoco" /> is the right-hand side of a chain accessor —
        /// a reference the parser resolves against the preceding segment's type instead of the lexical
        /// scope, so the bare simple name is the correct emission.
        /// </summary>
        /// <param name="sourcePoco">The source POCO at the reference site.</param>
        /// <returns><see langword="true" /> when the source is a chain accessor.</returns>
        internal static bool IsChainAccessor(IElement sourcePoco)
        {
            if (sourcePoco is IMembership { OwningRelatedElement: { } membershipOwner } and not IParameterMembership
                && EstablishesRelativeNamespace(membershipOwner))
            {
                return true;
            }

            if (IsFlowFeatureAccessor(sourcePoco))
            {
                return true;
            }

            if (sourcePoco is not IFeatureChaining { OwningRelatedElement: IFeature chainOwner } chaining)
            {
                return false;
            }

            var siblings = chainOwner.OwnedRelationship.OfType<IFeatureChaining>().ToList();
            var index = siblings.IndexOf(chaining);

            if (index > 0)
            {
                return true;
            }

            return chainOwner.OwningRelationship is IMembership { OwningRelatedElement: { } chainMemberOwner } and not IParameterMembership
                   && EstablishesRelativeNamespace(chainMemberOwner);
        }

        /// <summary>
        /// Returns the Namespace a chain accessor resolves against: the <c>result</c> of the argument
        /// Expression for a <see cref="IFeatureChainExpression" /> member, or the preceding
        /// <c>chainingFeature</c> for a later <see cref="IFeatureChaining" /> (KerML §8.2.3.5.2).
        /// </summary>
        /// <param name="sourcePoco">The source POCO at the reference site.</param>
        /// <returns>The relative Namespace, or <see langword="null" /> when the site has none.</returns>
        internal static INamespace QueryRelativeNamespace(IElement sourcePoco)
        {
            switch (sourcePoco)
            {
                case IMembership { OwningRelatedElement: IFeatureChainExpression chainExpression }:

                    try
                    {
                        // argument derives through instantiatedType, which resolves a qualified name and so
                        // can evaluate a filterCondition whose Expression has no model-level evaluation.
                        return chainExpression.argument.Count == 0 ? null : chainExpression.argument[0].result;
                    }
                    catch (NotSupportedException)
                    {
                        return null;
                    }

                case IFeatureChaining { OwningRelatedElement: IFeature chainOwner } chaining:
                {
                    var siblings = chainOwner.OwnedRelationship.OfType<IFeatureChaining>().ToList();
                    var index = siblings.IndexOf(chaining);

                    return index > 0 ? siblings[index - 1].ChainingFeature : null;
                }

                default:
                    return null;
            }
        }

        internal static SelfBinding QuerySelfBinding(IElement sourcePoco)
        {
            if (sourcePoco is IMembershipImport { ImportedMembership: { } importedMembership, OwningRelatedElement: INamespace importScope })
            {
                // Only when the import introduces the binding: a membership the scope already owns binds anyway.
                return OwnsMembership(importScope, importedMembership)
                    ? null
                    : new SelfBinding(importScope, importedMembership);
            }

            return sourcePoco is IMembership membership and not IOwningMembership
                   && string.IsNullOrWhiteSpace(membership.MemberName)
                   && string.IsNullOrWhiteSpace(membership.MemberShortName)
                   && membership.OwningRelatedElement is INamespace bindingScope
                ? new SelfBinding(bindingScope, membership)
                : null;
        }

        /// <summary>
        /// Returns the innermost scope of a reference's chain that may produce a MATCH, or
        /// <see langword="null" /> when every scope of the chain may.
        /// </summary>
        /// <param name="sourcePoco">The source POCO at the reference site.</param>
        /// <returns>The floor scope, or <see langword="null" />.</returns>
        /// <remarks>
        /// KerML §8.2.3.5.2 anchors a <see cref="IMembership" /> inside a
        /// <see cref="IFeatureReferenceExpression" /> at the "non-invocation Namespace" — the nearest
        /// containing Namespace that is neither an expression nor a parameter of one. For a
        /// <see cref="IFeatureValue" /> that is the value-carrying Feature itself, while a stricter reading
        /// steps one scope further out. The clause admits both, so the floor bars the disputed scopes from
        /// producing a match while keeping them as shadow sources.
        /// </remarks>
        internal static INamespace QueryValueExpressionMatchFloor(IElement sourcePoco)
        {
            if (sourcePoco is not IMembership sourceMembership)
            {
                return null;
            }

            var subject = sourceMembership;
            var scope = QueryExpressionScope(subject);
            var visited = new HashSet<IMembership>();
            var isFirstStep = true;

            while (scope != null && StepsOutOfInvocation(subject, scope, isFirstStep))
            {
                subject = scope.owningMembership;
                isFirstStep = false;

                if (subject == null || !visited.Add(subject))
                {
                    break;
                }

                scope = QueryExpressionScope(subject);
            }

            return scope;
        }

        /// <summary>
        /// Determines whether the local referencer's DECLARED name equals the target's effective name — in
        /// which case the bare simple name would re-resolve to the local member and the qualified form is
        /// required. An anonymous referencer (no declared names) can never collide.
        /// <para>
        /// Declared names only. An anonymous redefiner takes its effective name from the feature it
        /// redefines, so testing the effective name would qualify every such redefinition unnecessarily.
        /// </para>
        /// </summary>
        /// <param name="localRedefiner">The redefining/referencing feature; must be non-null.</param>
        /// <param name="target">The referenced target.</param>
        /// <returns><see langword="true" /> on a collision.</returns>
        internal static bool RedefinerDeclaredNameCollidesWith(IFeature localRedefiner, IElement target)
        {
            var declaredNames = new[] { localRedefiner.DeclaredName, localRedefiner.DeclaredShortName }
                .Where(declared => !string.IsNullOrWhiteSpace(declared));

            return declaredNames.Any(declared =>
                string.Equals(declared, target.name, StringComparison.Ordinal)
                || string.Equals(declared, target.shortName, StringComparison.Ordinal));
        }

        /// <summary>
        /// Determines whether <paramref name="redefiningFeature" /> also REFERENCES a different element that
        /// shares a simple name with <paramref name="target" /> — the <c>exhibit X :>> Y</c> shape, where the
        /// reference and the redefinition name distinct elements under one name. Qualifying the redefinition
        /// keeps them distinct under either reading of the §8.2.3.5.1 exception.
        /// </summary>
        /// <param name="redefiningFeature">The feature owning the redefinition.</param>
        /// <param name="target">The redefined feature being named.</param>
        /// <returns><see langword="true" /> when the qualified form is required to keep the two distinct.</returns>
        internal static bool ReferencedFeatureSharesSimpleName(IFeature redefiningFeature, IElement target)
        {
            var referencedFeature = redefiningFeature.OwnedRelationship
                .OfType<IReferenceSubsetting>()
                .Select(referenceSubsetting => referenceSubsetting.ReferencedFeature)
                .FirstOrDefault(referenced => referenced != null && !ReferenceEquals(referenced, target));

            if (referencedFeature == null)
            {
                return false;
            }

            var referencedNames = new[] { referencedFeature.name, referencedFeature.shortName }
                .Where(candidate => !string.IsNullOrWhiteSpace(candidate));

            return referencedNames.Any(candidate =>
                string.Equals(candidate, target.name, StringComparison.Ordinal)
                || string.Equals(candidate, target.shortName, StringComparison.Ordinal));
        }

        /// <summary>
        /// Determines whether <paramref name="owner" /> establishes a RELATIVE namespace — a scope taken
        /// from a preceding expression's result rather than lexical containment (KerML §8.2.3.5.2):
        /// <see cref="IFeatureChainExpression" /> always, and <see cref="IAssignmentActionUsage" /> only when
        /// it carries a target binding.
        /// </summary>
        /// <param name="owner">The element owning the membership at the reference site.</param>
        /// <returns><see langword="true" /> when the owner establishes a relative namespace.</returns>
        private static bool EstablishesRelativeNamespace(IElement owner)
        {
            return owner switch
            {
                IFeatureChainExpression => true,
                IAssignmentActionUsage assignmentActionUsage => assignmentActionUsage.targetArgument != null,
                _ => false
            };
        }

        /// <summary>
        /// Determines whether <paramref name="chaining" /> is the first <c>ownedFeatureChaining</c> of its
        /// <c>featureChained</c>.
        /// </summary>
        /// <param name="chaining">The chaining to test.</param>
        /// <returns><see langword="true" /> when it is the first.</returns>
        private static bool IsFirstOwnedChaining(IFeatureChaining chaining)
        {
            return chaining.OwningRelatedElement is IFeature chainOwner
                   && ReferenceEquals(chainOwner.OwnedRelationship.OfType<IFeatureChaining>().FirstOrDefault(), chaining);
        }

        /// <summary>
        /// Determines whether <paramref name="sourcePoco" /> is the flow-feature reference of a
        /// <see cref="IFlowEnd" /> that carries a subsetting prefix — the feature then resolves against the
        /// prefix's type, like a chain accessor. Without the prefix it keeps lexical resolution.
        /// </summary>
        /// <param name="sourcePoco">The source POCO at the reference site.</param>
        /// <returns><see langword="true" /> when the source is a prefixed flow-feature accessor.</returns>
        private static bool IsFlowFeatureAccessor(IElement sourcePoco)
        {
            if (sourcePoco is not IRedefinition { OwningRelatedElement: IFeature flowFeature })
            {
                return false;
            }

            return flowFeature.OwningRelationship is IFeatureMembership { OwningRelatedElement: IFlowEnd flowEnd }
                   && flowEnd.OwnedRelationship.OfType<IReferenceSubsetting>().Any();
        }

        /// <summary>
        /// Determines whether <paramref name="scope" /> is one of the four kinds the non-invocation
        /// Namespace excludes (KerML §8.2.3.5.2).
        /// </summary>
        /// <param name="scope">The candidate scope.</param>
        /// <returns><see langword="true" /> when the scope is excluded.</returns>
        private static bool IsInvocationScope(INamespace scope)
        {
            return scope switch
            {
                IFeatureReferenceExpression or IInstantiationExpression => true,
                IFeature { owningType: IInstantiationExpression } => true,
                IFeature { owningType: IFeature { owningType: IConstructorExpression constructor } constructorResult }
                    => ReferenceEquals(constructor.result, constructorResult),
                _ => false
            };
        }

        /// <summary>
        /// Determines the local <see cref="INamespace" /> from the KIND of context relationship, per
        /// KerML §8.2.3.5.2. Only the kinds whose local scope is NOT simply the nearest enclosing namespace
        /// are handled here; everything else falls back to the containment climb.
        /// <para>
        /// For a <see cref="ISpecialization" /> the spec anchors resolution at the
        /// <c>owningNamespace</c> of the <c>owningType</c> — one level OUT from the owning feature — so the
        /// owning feature's own and inherited members are NOT in scope.
        /// </para>
        /// <para>
        /// A <see cref="IReferenceSubsetting" /> whose <c>referencingFeature</c> is an end feature of a
        /// <see cref="IConnector" /> anchors at the connector's owning namespace, which reaches the referenced
        /// name only through implied Specializations. See <c>TranslateToResolutionGraph</c>.
        /// </para>
        /// </summary>
        /// <param name="sourcePoco">The context relationship at the reference site.</param>
        /// <returns>The local scope, or <see langword="null" /> when the generic climb applies.</returns>
        private static INamespace QueryContextRelationshipLocalScope(IElement sourcePoco)
        {
            return sourcePoco switch
            {
                // The FIRST ownedFeatureChaining anchors where its featureChained's owning Relationship
                // anchors; a later one resolves against the preceding chainingFeature and is written bare.
                IFeatureChaining chaining when IsFirstOwnedChaining(chaining)
                    => QueryContextRelationshipLocalScope((chaining.OwningRelatedElement as IFeature)?.OwningRelationship),

                // Must precede ISpecialization: a ReferenceSubsetting is one, and would otherwise be
                // re-anchored by the rule below.
                IReferenceSubsetting { referencingFeature: { IsEnd: true, owningType: IConnector connector } } => connector.owningNamespace,
                ISpecialization specialization => specialization.owningType != null ? specialization.owningType.owningNamespace : specialization.owningNamespace,
                IConjugation conjugation => conjugation.owningType != null ? conjugation.owningType.owningNamespace : conjugation.owningNamespace,
                _ => null
            };
        }

        /// <summary>
        /// Returns the namespace containing <paramref name="membership" />, except for a
        /// <see cref="IFeatureValue" /> on a parameter of an <see cref="IInstantiationExpression" />, whose
        /// value expression is resolved against the invocation rather than against the parameter.
        /// </summary>
        /// <param name="membership">The membership whose containing scope is requested.</param>
        /// <returns>The scope, or <see langword="null" /> when the membership has none.</returns>
        private static INamespace QueryExpressionScope(IMembership membership)
        {
            var scope = ContainmentPaths.QueryParentNamespace(membership);

            if (scope == null)
            {
                return null;
            }

            return membership is IFeatureValue && scope.owningNamespace is IInstantiationExpression invocation
                ? invocation
                : scope;
        }

        /// <summary>
        /// Returns the depth in <paramref name="scopes" /> at which a match becomes admissible.
        /// </summary>
        /// <param name="scopes">The chain, innermost first.</param>
        /// <param name="matchFloorScope">The floor scope, or <see langword="null" /> for no floor.</param>
        /// <returns>The depth; 0 admits the whole chain.</returns>
        /// <remarks>
        /// The floor is reached by a CONTAINMENT climb while the chain is materialised through
        /// <c>owningNamespace</c>, so the floor is not guaranteed to sit on the chain — the invocation
        /// redirect of <see cref="QueryExpressionScope" /> can elect a Namespace the chain does not pass
        /// through. Falling back to depth 0 there would silently re-admit the very scopes the floor exists
        /// to exclude, so the floor's own CONTAINERS are tried next: the innermost of them that IS on the
        /// chain sits at or outside the floor, which keeps the constraint at least as strict as intended.
        /// Depth 0 is reached only when the two are genuinely unrelated.
        /// </remarks>
        private static int QueryMatchFloorDepth(List<INamespace> scopes, INamespace matchFloorScope)
        {
            var visited = new HashSet<INamespace>();

            for (var candidate = matchFloorScope; candidate != null && visited.Add(candidate); candidate = ContainmentPaths.QueryParentNamespace(candidate))
            {
                var depth = scopes.IndexOf(candidate);

                if (depth >= 0)
                {
                    return depth;
                }
            }

            return 0;
        }

        /// <summary>
        /// Determines whether the walk towards the non-invocation Namespace must step out of
        /// <paramref name="scope" /> (KerML §8.2.3.5.2).
        /// </summary>
        /// <param name="subject">The membership whose scope is under test.</param>
        /// <param name="scope">The candidate scope.</param>
        /// <param name="isFirstStep">Whether this is the original membership rather than an enclosing one.</param>
        /// <returns><see langword="true" /> when the scope cannot be the non-invocation Namespace.</returns>
        /// <remarks>
        /// The clause gates the <see cref="IInstantiationExpression" /> entry on the Membership NOT being a
        /// <see cref="IFeatureMembership" />; that gate gets the original Membership only, since the walk
        /// past the first scope is governed by the definition of the non-invocation Namespace alone.
        /// </remarks>
        private static bool StepsOutOfInvocation(IMembership subject, INamespace scope, bool isFirstStep)
        {
            if (subject is IFeatureValue || scope is IFeatureReferenceExpression)
            {
                return true;
            }

            return (!isFirstStep || subject is not IFeatureMembership) && IsInvocationScope(scope);
        }

        /// <summary>
        /// Determines whether <paramref name="scope" /> declares <paramref name="membership" /> as one of
        /// its own owned memberships — in which case the binding exists independently of any import of it.
        /// </summary>
        /// <param name="scope">The namespace to inspect.</param>
        /// <param name="membership">The membership to look for.</param>
        /// <returns><see langword="true" /> when the scope owns the membership.</returns>
        private static bool OwnsMembership(INamespace scope, IMembership membership)
        {
            return scope.ownedMembership.Any(owned => ReferenceEquals(owned, membership));
        }
    }
}
