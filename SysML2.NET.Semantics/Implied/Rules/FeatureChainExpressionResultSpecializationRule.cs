// -------------------------------------------------------------------------------------------------
// <copyright file="FeatureChainExpressionResultSpecializationRule.cs" company="Starion Group S.A.">
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
    using System.Collections.Generic;
    using System.Linq;

    using SysML2.NET.Core.POCO.Core.Features;
    using SysML2.NET.Core.POCO.Kernel.Expressions;
    using SysML2.NET.Core.POCO.Root.Elements;

    /// <summary>
    /// Implements checkFeatureChainExpressionResultSpecialization: the result of a FeatureChainExpression
    /// subsets the feature chain the Expression denotes.
    /// </summary>
    /// <remarks>
    /// OCL: <c>let inputParameters = ownedFeatures-&gt;select(direction = 'in') in …
    /// result.subsetsChain(inputParameters-&gt;first(), sourceTargetFeature) and result.owningType = self</c>.
    /// <para>KerML 1.0 §8.3.4.8.4: "The result parameter of a FeatureChainExpression must specialize the
    /// feature chain of the FeatureChainExpression." The chain is
    /// <c>[first input parameter, sourceTargetFeature]</c> — the Expression's source, then the feature
    /// reached through it, which is exactly what <c>a.b</c> denotes.</para>
    /// <para>The OCL writes <c>owningExpression.sourceTargetFeature()</c>, but <c>sourceTargetFeature()</c>
    /// is declared on FeatureChainExpression itself and the constraint is too, so it is read as
    /// <c>self.sourceTargetFeature()</c>.</para>
    /// </remarks>
    public class FeatureChainExpressionResultSpecializationRule : ChainSubsettingRule
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FeatureChainExpressionResultSpecializationRule" /> class.
        /// </summary>
        /// <param name="factory">The factory creating the chain and the Subsetting.</param>
        public FeatureChainExpressionResultSpecializationRule(IImpliedRelationshipFactory factory)
            : base(factory)
        {
        }

        /// <summary>
        /// Gets the name of the semantic constraint this rule implements.
        /// </summary>
        public override string ConstraintName => "checkFeatureChainExpressionResultSpecialization";

        /// <summary>
        /// Returns the chain the Expression's result must subset.
        /// </summary>
        /// <param name="element">The Element under evaluation.</param>
        /// <returns>The result and the two Features forming the chain; empty otherwise.</returns>
        protected override IEnumerable<(IFeature Subsetting, IFeature First, IFeature Second)> QueryChains(IElement element)
        {
            if (element is not IFeatureChainExpression { result: not null } featureChainExpression
                || !ReferenceEquals(featureChainExpression.result.owningType, featureChainExpression))
            {
                return [];
            }

            var firstInputParameter = featureChainExpression.ownedFeature
                .FirstOrDefault(ownedFeature => ownedFeature.Direction == Core.Core.Types.FeatureDirectionKind.In);

            return [(featureChainExpression.result, firstInputParameter, featureChainExpression.SourceTargetFeature())];
        }
    }
}
