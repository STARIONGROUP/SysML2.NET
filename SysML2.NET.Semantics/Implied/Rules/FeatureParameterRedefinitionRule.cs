// -------------------------------------------------------------------------------------------------
// <copyright file="FeatureParameterRedefinitionRule.cs" company="Starion Group S.A.">
//
//    Copyright (C) 2022-2026 Starion Group S.A.
//
//    Licensed under the Apache License, Version 2.0 (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
//
//         http://www.apache.org/licenses/LICENSE-2.0
//
//    Unless required by applicable law or agreed to in writing, software
//    distributed under the License is distributed on an "AS IS" BASIS,
//    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//    See the License for the specific language governing permissions and
//    limitations under the License.
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.Semantics.Implied.Rules
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using SysML2.NET.Core.POCO.Core.Features;
    using SysML2.NET.Core.POCO.Core.Types;
    using SysML2.NET.Core.POCO.Kernel.Behaviors;
    using SysML2.NET.Core.POCO.Kernel.Expressions;
    using SysML2.NET.Core.POCO.Kernel.Functions;
    using SysML2.NET.Core.POCO.Root.Elements;

    /// <summary>
    /// Implements checkFeatureParameterRedefinition: the nth parameter of a Behavior or Step redefines the
    /// nth parameter of each Behavior or Step it specializes.
    /// </summary>
    /// <remarks>
    /// OCL: <c>owningType &lt;&gt; null and (owningType.oclIsKindOf(Behavior) or
    /// owningType.oclIsKindOf(Step) and (owningType.oclIsKindOf(InvocationExpression) implies not
    /// ownedRedefinition-&gt;exists(not isImplied))) implies let ownerParameters = owningType.ownedFeature
    /// -&gt;select(direction &lt;&gt; null)-&gt;reject(owningFeatureMembership.oclIsKindOf(
    /// ReturnParameterMembership)) in … ownedParameters-&gt;size() &gt;= i implies
    /// redefines(ownedParameters-&gt;at(i))</c>.
    /// <para>The subtype contributes its directed OWNED Features excluding the return parameter; the supertype
    /// contributes its <c>parameter</c>, matched positionally. An InvocationExpression that already declares an
    /// explicit (non-implied) Redefinition is excluded, since the modeller has bound its arguments by hand.</para>
    /// </remarks>
    public class FeatureParameterRedefinitionRule : IImpliedRelationshipRule
    {
        /// <summary>
        /// The factory creating the detached Redefinitions.
        /// </summary>
        private readonly IImpliedRelationshipFactory factory;

        /// <summary>
        /// The provider supplying the owning Type's implied Specializations, resolved lazily because it is
        /// the same provider this rule is registered with.
        /// </summary>
        private readonly Lazy<IImpliedRelationshipProvider> provider;

        /// <summary>
        /// Initializes a new instance of the <see cref="FeatureParameterRedefinitionRule" /> class.
        /// </summary>
        /// <param name="factory">The factory creating the detached Redefinitions.</param>
        /// <param name="provider">The provider supplying the owning Type's implied Specializations.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="factory" /> or <paramref name="provider" /> is null.</exception>
        public FeatureParameterRedefinitionRule(IImpliedRelationshipFactory factory, Lazy<IImpliedRelationshipProvider> provider)
        {
            this.factory = factory ?? throw new ArgumentNullException(nameof(factory));
            this.provider = provider ?? throw new ArgumentNullException(nameof(provider));
        }

        /// <summary>
        /// Gets the name of the semantic constraint this rule implements.
        /// </summary>
        public string ConstraintName => "checkFeatureParameterRedefinition";

        /// <summary>
        /// Computes the Redefinitions a parameter requires towards the corresponding parameters of its
        /// owning Type's supertypes.
        /// </summary>
        /// <param name="element">The Element under evaluation.</param>
        /// <returns>One Redefinition per supertype with a parameter at the same position; empty otherwise.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="element" /> is null.</exception>
        public IReadOnlyList<IRelationship> Apply(IElement element)
        {
            if (element == null)
            {
                throw new ArgumentNullException(nameof(element));
            }

            if (element is not IFeature { owningType: not null } parameter || !IsInScope(parameter.owningType))
            {
                return [];
            }

            var position = QueryParameters(parameter.owningType).IndexOf(parameter);

            if (position < 0)
            {
                return [];
            }

            return
            [
                ..this.QuerySupertypes(parameter.owningType)
                    .Where(supertype => supertype is IBehavior or IStep)
                    .Select(QuerySupertypeParameters)
                    .Where(supertypeParameters => supertypeParameters.Count > position)
                    .Select(supertypeParameters => this.factory.CreateImpliedRedefinition(parameter, supertypeParameters[position]))
            ];
        }

        /// <summary>
        /// Returns the direct supertypes of a Type over the effective model: those its declared
        /// Specializations reach, together with those its implied Specializations reach (KerML §8.4.2).
        /// </summary>
        /// <param name="owningType">The Type owning the parameter.</param>
        /// <returns>The direct supertypes, declared ones first.</returns>
        private IEnumerable<IType> QuerySupertypes(IType owningType)
        {
            return owningType.ownedSpecialization
                .Concat(this.provider.Value.GetImpliedSpecializations(owningType))
                .Select(specialization => specialization.General)
                .Where(supertype => supertype != null && !ReferenceEquals(supertype, owningType))
                .Distinct();
        }

        /// <summary>
        /// Asserts whether a Type's parameters are subject to the constraint.
        /// </summary>
        /// <param name="owningType">The Type owning the parameter.</param>
        /// <returns>True when the Type is a Behavior, or a Step whose explicit Redefinitions do not already bind it.</returns>
        private static bool IsInScope(IType owningType)
        {
            return owningType switch
            {
                IInvocationExpression invocationExpression => !invocationExpression.ownedRedefinition.Any(redefinition => !redefinition.IsImplied),
                IBehavior or IStep => true,
                _ => false
            };
        }

        /// <summary>
        /// Returns the parameters of a Type: its directed owned Features, excluding the return parameter.
        /// </summary>
        /// <param name="type">The Type to inspect.</param>
        /// <returns>The parameters, in declaration order.</returns>
        private static List<IFeature> QueryParameters(IType type)
        {
            return [..type.ownedFeature
                .Where(ownedFeature => ownedFeature.Direction.HasValue)
                .Where(ownedFeature => ownedFeature.owningFeatureMembership is not IReturnParameterMembership)];
        }

        /// <summary>
        /// Returns the parameters a supertype offers for positional matching, excluding the return parameter.
        /// </summary>
        /// <param name="supertype">The supertype to inspect.</param>
        /// <returns>The parameters, owned ones first and inherited ones after.</returns>
        /// <remarks>
        /// The supertype side is <c>parameter</c> — which KerML defines as <c>directedFeature</c>, so inherited
        /// parameters count and are ordered after owned ones — because §7.4.7.2 requires a subclassifier's owned
        /// parameters to redefine "the parameter at the same position" of each superclassifier. The constraint's
        /// OCL formalises this side as <c>ownedFeature</c>, which is narrower than the clause it formalises.
        /// </remarks>
        private static List<IFeature> QuerySupertypeParameters(IType supertype)
        {
            var parameters = supertype switch
            {
                IBehavior behavior => behavior.parameter,
                IStep step => step.parameter,
                _ => supertype.directedFeature
            };

            return [..parameters.Where(parameter => parameter.owningFeatureMembership is not IReturnParameterMembership)];
        }
    }
}
