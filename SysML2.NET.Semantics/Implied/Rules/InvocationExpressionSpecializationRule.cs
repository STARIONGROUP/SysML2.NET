// -------------------------------------------------------------------------------------------------
// <copyright file="InvocationExpressionSpecializationRule.cs" company="Starion Group S.A.">
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

    using SysML2.NET.Core.POCO.Kernel.Expressions;
    using SysML2.NET.Core.POCO.Root.Elements;

    /// <summary>
    /// Implements checkInvocationExpressionSpecialization: an InvocationExpression is typed by the Type it
    /// instantiates.
    /// </summary>
    /// <remarks>
    /// OCL: <c>specializes(instantiatedType)</c>.
    /// <para>KerML 1.0 §8.4.4.9.5 Invocation Expressions (p. 262) supplies the Relationship kind the OCL
    /// omits: an InvocationExpression specializes its instantiatedType "via a FeatureTyping" — always, with
    /// no dependence on whether the instantiatedType is a Classifier or a Feature. This differs from
    /// <see cref="ConstructorExpressionResultSpecializationRule" />, where the kind DOES depend on that, so
    /// the two must not be generalised into one rule.</para>
    /// </remarks>
    public class InvocationExpressionSpecializationRule : IImpliedRelationshipRule
    {
        /// <summary>
        /// The factory creating the detached FeatureTyping.
        /// </summary>
        private readonly IImpliedRelationshipFactory factory;

        /// <summary>
        /// Initializes a new instance of the <see cref="InvocationExpressionSpecializationRule" /> class.
        /// </summary>
        /// <param name="factory">The factory creating the detached FeatureTyping.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="factory" /> is null.</exception>
        public InvocationExpressionSpecializationRule(IImpliedRelationshipFactory factory)
        {
            this.factory = factory ?? throw new ArgumentNullException(nameof(factory));
        }

        /// <summary>
        /// Gets the name of the semantic constraint this rule implements.
        /// </summary>
        public string ConstraintName => "checkInvocationExpressionSpecialization";

        /// <summary>
        /// Computes the FeatureTyping binding an InvocationExpression to the Type it instantiates.
        /// </summary>
        /// <param name="element">The Element under evaluation.</param>
        /// <returns>The FeatureTyping, or empty when the constraint does not apply.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="element" /> is null.</exception>
        public IReadOnlyList<IRelationship> Apply(IElement element)
        {
            if (element == null)
            {
                throw new ArgumentNullException(nameof(element));
            }

            return element is not IInvocationExpression { instantiatedType: not null } invocationExpression
                ? []
                : [this.factory.CreateImpliedFeatureTyping(invocationExpression, invocationExpression.instantiatedType)];
        }
    }
}
