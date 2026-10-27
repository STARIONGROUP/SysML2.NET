// ------------------------------------------------------------------------------------------------
// <copyright file="RenderingUsageRedefinitionRule.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.Semantics.Implied.Rules
{
    using SysML2.NET.Core.POCO.Core.Features;
    using SysML2.NET.Core.POCO.Root.Elements;
    using SysML2.NET.Core.POCO.Systems.Views;

    /// <summary>
    /// Implements checkRenderingUsageRedefinition: a RenderingUsage owned by a ViewRenderingMembership
    /// redefines Views::View::viewRendering.
    /// </summary>
    /// <remarks>
    /// OCL: <c>owningFeatureMembership &lt;&gt; null and
    /// owningFeatureMembership.oclIsKindOf(ViewRenderingMembership) implies
    /// redefinesFromLibrary('Views::View::viewRendering')</c>. A RenderingUsage owned any other way is out
    /// of scope.
    /// </remarks>
    public class RenderingUsageRedefinitionRule : LibraryRedefinitionRule
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RenderingUsageRedefinitionRule" /> class.
        /// </summary>
        /// <param name="libraryTypeIndex">The index resolving the library Feature by qualified name.</param>
        /// <param name="factory">The factory creating the detached Redefinition.</param>
        public RenderingUsageRedefinitionRule(ILibraryTypeIndex libraryTypeIndex, IImpliedRelationshipFactory factory)
            : base(libraryTypeIndex, factory)
        {
        }

        /// <summary>
        /// Gets the name of the semantic constraint this rule implements.
        /// </summary>
        public override string ConstraintName => "checkRenderingUsageRedefinition";

        /// <summary>
        /// Returns the RenderingUsage itself when it is owned by a ViewRenderingMembership.
        /// </summary>
        /// <param name="element">The Element under evaluation.</param>
        /// <returns>The Element and the library qualified name, or <c>null</c> when the constraint does not apply.</returns>
        protected override (IFeature RedefiningFeature, string LibraryQualifiedName)? QueryRedefinition(IElement element)
        {
            return element is IRenderingUsage { owningFeatureMembership: IViewRenderingMembership } renderingUsage
                ? (renderingUsage, "Views::View::viewRendering")
                : null;
        }
    }
}
