// -------------------------------------------------------------------------------------------------
// <copyright file="FeatureChainExpressionSourceTargetRedefinitionRule.cs" company="Starion Group S.A.">
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
    /// Implements checkFeatureChainExpressionSourceTargetRedefinition: the source-target Feature of a FeatureChainExpression redefines the expression's target Feature.
    /// </summary>
    /// <remarks>
    /// OCL: <c>let sourceTargetFeature : Feature = sourceTargetFeature() in sourceTargetFeature &lt;&gt; null and sourceTargetFeature.redefines(targetFeature)</c>
    /// </remarks>
    public class FeatureChainExpressionSourceTargetRedefinitionRule : IImpliedRelationshipRule
    {
        /// <summary>
        /// The factory creating the detached Redefinition.
        /// </summary>
        private readonly IImpliedRelationshipFactory factory;

        /// <summary>
        /// Initializes a new instance of the <see cref="FeatureChainExpressionSourceTargetRedefinitionRule" /> class.
        /// </summary>
        /// <param name="factory">The factory creating the detached Redefinition.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="factory" /> is null.</exception>
        public FeatureChainExpressionSourceTargetRedefinitionRule(IImpliedRelationshipFactory factory)
        {
            this.factory = factory ?? throw new ArgumentNullException(nameof(factory));
        }

        /// <summary>
        /// Gets the name of the semantic constraint this rule implements.
        /// </summary>
        public string ConstraintName => "checkFeatureChainExpressionSourceTargetRedefinition";

        /// <summary>
        /// Computes the implied Redefinition the constraint requires of the supplied Element.
        /// </summary>
        /// <param name="element">The Element under evaluation.</param>
        /// <returns>A single Redefinition, or empty when the constraint does not apply.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="element" /> is null.</exception>
        public IReadOnlyList<IRelationship> Apply(IElement element)
        {
            if (element == null)
            {
                throw new ArgumentNullException(nameof(element));
            }

            if (element is not IFeatureChainExpression { targetFeature: not null } featureChainExpression)
            {
                return [];
            }

            var sourceTargetFeature = featureChainExpression.SourceTargetFeature();

            return sourceTargetFeature == null
                ? []
                : [this.factory.CreateImpliedRedefinition(sourceTargetFeature, featureChainExpression.targetFeature)];
        }
    }
}
