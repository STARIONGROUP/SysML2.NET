// -------------------------------------------------------------------------------------------------
// <copyright file="ControlNodeSuccessionChainRule.cs" company="Starion Group S.A.">
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

    using SysML2.NET.Core.POCO.Core.Features;
    using SysML2.NET.Core.POCO.Kernel.Connectors;
    using SysML2.NET.Core.POCO.Root.Elements;

    /// <summary>
    /// Base for the two constraints requiring a Succession attached to a control node to subset a chain
    /// through that node's library happens-before link.
    /// </summary>
    /// <remarks>
    /// The OCL reads <c>sourceConnector-&gt;selectByKind(Succession)-&gt;forAll(subsetsChain(self, …))</c> —
    /// a REVERSE navigation from the node to the Successions it is an end of. <c>sourceConnector</c> and
    /// <c>targetConnector</c> are not available as derived properties, but they need not be: the subject of
    /// <c>subsetsChain</c> is each Succession, not the node, so evaluating the rule on the SUCCESSION and
    /// asking whether its own end is a control node yields exactly the same set with no reverse walk.
    /// <para>The chain is <c>[controlNode, happensBeforeLink]</c>: the Succession subsets the link as
    /// reached THROUGH the node, which is what ties the ordering to that particular node rather than to
    /// control performances at large.</para>
    /// </remarks>
    public abstract class ControlNodeSuccessionChainRule : ChainSubsettingRule
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ControlNodeSuccessionChainRule" /> class.
        /// </summary>
        /// <param name="libraryTypeIndex">The index resolving the library Feature by qualified name.</param>
        /// <param name="factory">The factory creating the chain and the Subsetting.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="libraryTypeIndex" /> is null.</exception>
        protected ControlNodeSuccessionChainRule(ILibraryTypeIndex libraryTypeIndex, IImpliedRelationshipFactory factory)
            : base(factory)
        {
            this.LibraryTypeIndex = libraryTypeIndex ?? throw new ArgumentNullException(nameof(libraryTypeIndex));
        }

        /// <summary>
        /// Gets the index resolving the library Feature by qualified name.
        /// </summary>
        protected ILibraryTypeIndex LibraryTypeIndex { get; }

        /// <summary>
        /// Gets the qualified name of the library happens-before link the chain ends in.
        /// </summary>
        protected abstract string LinkQualifiedName { get; }

        /// <summary>
        /// Returns the chain obligation a Succession carries, when its relevant end is the control node.
        /// </summary>
        /// <param name="element">The Element under evaluation.</param>
        /// <returns>The Succession and the two Features forming the chain; empty otherwise.</returns>
        /// <exception cref="UnresolvedLibraryTypeException">Thrown when the library Feature is not indexed.</exception>
        protected override IEnumerable<(IFeature Subsetting, IFeature First, IFeature Second)> QueryChains(IElement element)
        {
            if (element is not ISuccession succession)
            {
                return [];
            }

            var controlNode = this.QueryControlNode(succession);

            if (controlNode == null)
            {
                return [];
            }

            if (!this.LibraryTypeIndex.TryGetType(this.LinkQualifiedName, out var libraryType))
            {
                throw new UnresolvedLibraryTypeException(this.LinkQualifiedName, this.ConstraintName);
            }

            return libraryType is not IFeature libraryFeature
                ? []
                : [(succession, controlNode, libraryFeature)];
        }

        /// <summary>
        /// Returns the control node at the end of the Succession this constraint governs.
        /// </summary>
        /// <param name="succession">The Succession under evaluation.</param>
        /// <returns>The control node, or <c>null</c> when the relevant end is not one.</returns>
        protected abstract IFeature QueryControlNode(ISuccession succession);
    }
}
