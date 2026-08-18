// -------------------------------------------------------------------------------------------------
// <copyright file="MergeNodeIncomingSuccessionSpecializationRule.cs" company="Starion Group S.A.">
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
    using SysML2.NET.Core.POCO.Kernel.Connectors;
    using SysML2.NET.Core.POCO.Systems.Actions;

    /// <summary>
    /// Implements checkMergeNodeIncomingSuccessionSpecialization: a Succession arriving at a MergeNode
    /// subsets that node's incoming happens-before link.
    /// </summary>
    /// <remarks>
    /// OCL: <c>targetConnector-&gt;selectByKind(Succession)-&gt;forAll(subsetsChain(self,
    /// resolveGlobal('ControlPerformances::MergePerformance::incomingHBLink')))</c>.
    /// <para>Evaluated on the Succession — see <see cref="ControlNodeSuccessionChainRule" /> for why that is
    /// equivalent to the OCL's reverse navigation from the node.</para>
    /// </remarks>
    public class MergeNodeIncomingSuccessionSpecializationRule : ControlNodeSuccessionChainRule
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MergeNodeIncomingSuccessionSpecializationRule" /> class.
        /// </summary>
        /// <param name="libraryTypeIndex">The index resolving the library Feature by qualified name.</param>
        /// <param name="factory">The factory creating the chain and the Subsetting.</param>
        public MergeNodeIncomingSuccessionSpecializationRule(ILibraryTypeIndex libraryTypeIndex, IImpliedRelationshipFactory factory)
            : base(libraryTypeIndex, factory)
        {
        }

        /// <summary>
        /// Gets the name of the semantic constraint this rule implements.
        /// </summary>
        public override string ConstraintName => "checkMergeNodeIncomingSuccessionSpecialization";

        /// <summary>
        /// Gets the qualified name of the library happens-before link the chain ends in.
        /// </summary>
        protected override string LinkQualifiedName => "ControlPerformances::MergePerformance::incomingHBLink";

        /// <summary>
        /// Returns the MergeNode the Succession arrives at, if it arrives at one.
        /// </summary>
        /// <param name="succession">The Succession under evaluation.</param>
        /// <returns>The MergeNode, or <c>null</c> when no target end is one.</returns>
        protected override IFeature QueryControlNode(ISuccession succession)
        {
            // INCOMING: the node is a TARGET end, and targetFeature is [0..*] rather than single.
            return succession.targetFeature.OfType<IMergeNode>().FirstOrDefault();
        }
    }
}
