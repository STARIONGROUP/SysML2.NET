// -------------------------------------------------------------------------------------------------
// <copyright file="InvocationExpressionBehaviorResultSpecializationRule.cs" company="Starion Group S.A.">
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

    using SysML2.NET.Core.POCO.Core.Classifiers;
    using SysML2.NET.Core.POCO.Core.Features;
    using SysML2.NET.Core.POCO.Core.Types;
    using SysML2.NET.Core.POCO.Kernel.Expressions;
    using SysML2.NET.Core.POCO.Kernel.Functions;
    using SysML2.NET.Core.POCO.Root.Elements;

    /// <summary>
    /// Implements checkInvocationExpressionBehaviorResultSpecialization: when an InvocationExpression
    /// instantiates a Behavior that is NOT a Function, its result parameter specializes that Behavior.
    /// </summary>
    /// <remarks>
    /// OCL: <c>not instantiatedType.oclIsKindOf(Function) and not
    /// (instantiatedType.oclIsKindOf(Feature) and
    /// instantiatedType.oclAsType(Feature).type-&gt;exists(oclIsKindOf(Function))) implies
    /// result.specializes(instantiatedType)</c>.
    /// <para>KerML 1.0 §8.4.4.9.5 Invocation Expressions (p. 262): "the result parameter of the expression
    /// specialize the instantiatedType" — the expression "evaluates, as an Expression, to itself, as an
    /// instance of B". A Function is excluded because a Function already declares its own result, so the
    /// invocation's result takes that instead of the Function itself.</para>
    /// <para>The Relationship kind follows the general rule the specification states for what "specialize"
    /// means of a Feature (§8.4.4.9.4): a FeatureTyping onto a Classifier, a Subsetting onto a Feature.
    /// Unlike <see cref="InvocationExpressionSpecializationRule" />, this clause does not name one kind, and
    /// the guard here explicitly contemplates a Feature instantiatedType — so both cases are live.</para>
    /// </remarks>
    public class InvocationExpressionBehaviorResultSpecializationRule : IImpliedRelationshipRule
    {
        /// <summary>
        /// The factory creating the detached Relationship.
        /// </summary>
        private readonly IImpliedRelationshipFactory factory;

        /// <summary>
        /// Initializes a new instance of the <see cref="InvocationExpressionBehaviorResultSpecializationRule" /> class.
        /// </summary>
        /// <param name="factory">The factory creating the detached Relationship.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="factory" /> is null.</exception>
        public InvocationExpressionBehaviorResultSpecializationRule(IImpliedRelationshipFactory factory)
        {
            this.factory = factory ?? throw new ArgumentNullException(nameof(factory));
        }

        /// <summary>
        /// Gets the name of the semantic constraint this rule implements.
        /// </summary>
        public string ConstraintName => "checkInvocationExpressionBehaviorResultSpecialization";

        /// <summary>
        /// Computes the Relationship binding an InvocationExpression's result to the Behavior it instantiates.
        /// </summary>
        /// <param name="element">The Element under evaluation.</param>
        /// <returns>The FeatureTyping or Subsetting, or empty when the constraint does not apply.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="element" /> is null.</exception>
        public IReadOnlyList<IRelationship> Apply(IElement element)
        {
            if (element == null)
            {
                throw new ArgumentNullException(nameof(element));
            }

            if (element is not IInvocationExpression { result: not null, instantiatedType: not null } invocationExpression
                || IsFunctionValued(invocationExpression.instantiatedType))
            {
                return [];
            }

            return invocationExpression.instantiatedType switch
            {
                IFeature instantiatedFeature => [this.factory.CreateImpliedSubsetting(invocationExpression.result, instantiatedFeature)],
                IClassifier instantiatedClassifier => [this.factory.CreateImpliedFeatureTyping(invocationExpression.result, instantiatedClassifier)],
                _ => []
            };
        }

        /// <summary>
        /// Asserts whether a Type is a Function, or a Feature typed by one.
        /// </summary>
        /// <param name="instantiatedType">The Type being instantiated.</param>
        /// <returns>True when the Type resolves to a Function.</returns>
        private static bool IsFunctionValued(IType instantiatedType)
        {
            return instantiatedType is IFunction
                || (instantiatedType is IFeature instantiatedFeature && instantiatedFeature.type.Any(type => type is IFunction));
        }
    }
}
