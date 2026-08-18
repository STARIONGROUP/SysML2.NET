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
    /// <para>Parameters are the directed owned Features EXCLUDING the return parameter, matched positionally
    /// against the supertype's. An InvocationExpression that already declares an explicit (non-implied)
    /// Redefinition is excluded, since the modeller has bound its arguments by hand.</para>
    /// </remarks>
    public class FeatureParameterRedefinitionRule : IImpliedRelationshipRule
    {
        /// <summary>
        /// The factory creating the detached Redefinitions.
        /// </summary>
        private readonly IImpliedRelationshipFactory factory;

        /// <summary>
        /// Initializes a new instance of the <see cref="FeatureParameterRedefinitionRule" /> class.
        /// </summary>
        /// <param name="factory">The factory creating the detached Redefinitions.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="factory" /> is null.</exception>
        public FeatureParameterRedefinitionRule(IImpliedRelationshipFactory factory)
        {
            this.factory = factory ?? throw new ArgumentNullException(nameof(factory));
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
                ..parameter.owningType.ownedSpecialization
                    .Select(specialization => specialization.General)
                    .Where(supertype => supertype is IBehavior or IStep)
                    .Select(QueryParameters)
                    .Where(supertypeParameters => supertypeParameters.Count > position)
                    .Select(supertypeParameters => this.factory.CreateImpliedRedefinition(parameter, supertypeParameters[position]))
            ];
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
    }
}
