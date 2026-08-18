// -------------------------------------------------------------------------------------------------
// <copyright file="RenderingUsageRedefinitionRule.cs" company="Starion Group S.A.">
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
