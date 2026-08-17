// -------------------------------------------------------------------------------------------------
// <copyright file="FeatureReferenceExpressionResultSpecializationRule.cs" company="Starion Group S.A.">
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
    /// Implements checkFeatureReferenceExpressionResultSpecialization: the result of a
    /// FeatureReferenceExpression subsets the Feature it refers to.
    /// </summary>
    /// <remarks>
    /// OCL: <c>result.owningType() = self and result.specializes(referent)</c>.
    /// <para>KerML 1.0 §8.4.4.9.3 Feature Reference Expressions (p. 260) gives both the Relationship kind
    /// and the reason: the result parameter "also subset the Feature", and although "this subsetting is
    /// technically implied by the semantics of the BindingConnector … including the Subsetting relationship
    /// allows for simpler static type checking".</para>
    /// <para>The first conjunct of the OCL is a precondition, not a second Relationship: it holds only when
    /// the result really is this Expression's own parameter, so a result reached from elsewhere implies
    /// nothing.</para>
    /// </remarks>
    public class FeatureReferenceExpressionResultSpecializationRule : IImpliedRelationshipRule
    {
        /// <summary>
        /// The factory creating the detached Subsetting.
        /// </summary>
        private readonly IImpliedRelationshipFactory factory;

        /// <summary>
        /// Initializes a new instance of the <see cref="FeatureReferenceExpressionResultSpecializationRule" /> class.
        /// </summary>
        /// <param name="factory">The factory creating the detached Subsetting.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="factory" /> is null.</exception>
        public FeatureReferenceExpressionResultSpecializationRule(IImpliedRelationshipFactory factory)
        {
            this.factory = factory ?? throw new ArgumentNullException(nameof(factory));
        }

        /// <summary>
        /// Gets the name of the semantic constraint this rule implements.
        /// </summary>
        public string ConstraintName => "checkFeatureReferenceExpressionResultSpecialization";

        /// <summary>
        /// Computes the Subsetting binding a FeatureReferenceExpression's result to its referent.
        /// </summary>
        /// <param name="element">The Element under evaluation.</param>
        /// <returns>The Subsetting, or empty when the constraint does not apply.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="element" /> is null.</exception>
        public IReadOnlyList<IRelationship> Apply(IElement element)
        {
            if (element == null)
            {
                throw new ArgumentNullException(nameof(element));
            }

            if (element is not IFeatureReferenceExpression { result: not null, referent: not null } featureReferenceExpression)
            {
                return [];
            }

            return ReferenceEquals(featureReferenceExpression.result.owningType, featureReferenceExpression)
                ? [this.factory.CreateImpliedSubsetting(featureReferenceExpression.result, featureReferenceExpression.referent)]
                : [];
        }
    }
}
