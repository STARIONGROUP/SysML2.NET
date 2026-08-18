// -------------------------------------------------------------------------------------------------
// <copyright file="FeatureFlowFeatureRedefinitionRule.cs" company="Starion Group S.A.">
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
    using System.Linq;

    using SysML2.NET.Core.POCO.Core.Features;
    using SysML2.NET.Core.POCO.Kernel.Interactions;
    using SysML2.NET.Core.POCO.Root.Elements;

    /// <summary>
    /// Implements checkFeatureFlowFeatureRedefinition: the first owned Feature of a Flow's first or second
    /// FlowEnd redefines the library source output or target input respectively.
    /// </summary>
    /// <remarks>
    /// OCL: <c>owningType &lt;&gt; null and owningType.oclIsKindOf(FlowEnd) and
    /// owningType.ownedFeature-&gt;at(1) = self implies let flowType : Type = owningType.owningType in
    /// flowType &lt;&gt; null implies let i : Integer = flowType.ownedFeature.indexOf(owningType) in
    /// (i = 1 implies redefinesFromLibrary('Transfers::Transfer::source::sourceOutput')) and
    /// (i = 2 implies redefinesFromLibrary('Transfers::Transfer::target::targetInput'))</c>.
    /// <para>OCL positions are 1-based: the FIRST FlowEnd of the flow carries the source output and the
    /// SECOND the target input. Any further end is unconstrained, which is why a position outside those two
    /// yields nothing.</para>
    /// </remarks>
    public class FeatureFlowFeatureRedefinitionRule : LibraryRedefinitionRule
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FeatureFlowFeatureRedefinitionRule" /> class.
        /// </summary>
        /// <param name="libraryTypeIndex">The index resolving the library Feature by qualified name.</param>
        /// <param name="factory">The factory creating the detached Redefinition.</param>
        public FeatureFlowFeatureRedefinitionRule(ILibraryTypeIndex libraryTypeIndex, IImpliedRelationshipFactory factory)
            : base(libraryTypeIndex, factory)
        {
        }

        /// <summary>
        /// Gets the name of the semantic constraint this rule implements.
        /// </summary>
        public override string ConstraintName => "checkFeatureFlowFeatureRedefinition";

        /// <summary>
        /// Returns the Feature together with the library Feature its FlowEnd's position selects.
        /// </summary>
        /// <param name="element">The Element under evaluation.</param>
        /// <returns>The Feature and the library qualified name, or <c>null</c> when the constraint does not apply.</returns>
        protected override (IFeature RedefiningFeature, string LibraryQualifiedName)? QueryRedefinition(IElement element)
        {
            if (element is not IFeature { owningType: IFlowEnd flowEnd } feature
                || !ReferenceEquals(flowEnd.ownedFeature.FirstOrDefault(), feature)
                || flowEnd.owningType == null)
            {
                return null;
            }

            var libraryQualifiedName = flowEnd.owningType.ownedFeature.IndexOf(flowEnd) switch
            {
                0 => "Transfers::Transfer::source::sourceOutput",
                1 => "Transfers::Transfer::target::targetInput",
                _ => null
            };

            return libraryQualifiedName == null
                ? null
                : (feature, libraryQualifiedName);
        }
    }
}
