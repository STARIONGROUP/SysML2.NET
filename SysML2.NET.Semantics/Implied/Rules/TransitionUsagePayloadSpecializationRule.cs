// -------------------------------------------------------------------------------------------------
// <copyright file="TransitionUsagePayloadSpecializationRule.cs" company="Starion Group S.A.">
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
    using SysML2.NET.Core.POCO.Root.Elements;
    using SysML2.NET.Core.POCO.Systems.States;

    /// <summary>
    /// Implements checkTransitionUsagePayloadSpecialization: a triggered TransitionUsage's payload parameter
    /// subsets the payload of its trigger.
    /// </summary>
    /// <remarks>
    /// OCL: <c>triggerAction-&gt;notEmpty() implies let payloadParameter : Feature = inputParameter(2) in
    /// payloadParameter &lt;&gt; null and
    /// payloadParameter.subsetsChain(triggerAction-&gt;at(1), triggerPayloadParameter())</c>.
    /// <para>Both OCL positions are 1-BASED. <c>inputParameter(2)</c> is passed through unchanged because the
    /// metamodel operation is itself 1-based, whereas <c>triggerAction-&gt;at(1)</c> becomes index 0 on the
    /// C# list — the two conventions coexist and each call site keeps its own.</para>
    /// </remarks>
    public class TransitionUsagePayloadSpecializationRule : ChainSubsettingRule
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TransitionUsagePayloadSpecializationRule" /> class.
        /// </summary>
        /// <param name="factory">The factory creating the chain and the Subsetting.</param>
        public TransitionUsagePayloadSpecializationRule(IImpliedRelationshipFactory factory)
            : base(factory)
        {
        }

        /// <summary>
        /// Gets the name of the semantic constraint this rule implements.
        /// </summary>
        public override string ConstraintName => "checkTransitionUsagePayloadSpecialization";

        /// <summary>
        /// Returns the chain the payload parameter must subset.
        /// </summary>
        /// <param name="element">The Element under evaluation.</param>
        /// <returns>The payload parameter and the two Features forming the chain; empty otherwise.</returns>
        protected override IEnumerable<(IFeature Subsetting, IFeature First, IFeature Second)> QueryChains(IElement element)
        {
            if (element is not ITransitionUsage transitionUsage || transitionUsage.triggerAction.Count == 0)
            {
                return [];
            }

            // inputParameter(2) — 1-based operation, argument passed through unchanged.
            return [(transitionUsage.InputParameter(2), transitionUsage.triggerAction[0], transitionUsage.TriggerPayloadParameter())];
        }
    }
}
