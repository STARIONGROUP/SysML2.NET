// -------------------------------------------------------------------------------------------------
// <copyright file="ForLoopActionUsageVarRedefinitionRule.cs" company="Starion Group S.A.">
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
    using SysML2.NET.Core.POCO.Systems.Actions;

    /// <summary>
    /// Implements checkForLoopActionUsageVarRedefinition: the loop variable of a ForLoopActionUsage redefines
    /// Actions::ForLoopAction::var.
    /// </summary>
    /// <remarks>
    /// OCL: <c>loopVariable &lt;&gt; null and
    /// loopVariable.redefinesFromLibrary('Actions::ForLoopAction::var')</c>. The redefining Feature is the
    /// loop VARIABLE, not the ForLoopActionUsage itself — which is why the rule is keyed on the loop action
    /// but produces a Redefinition owned by a different Feature.
    /// </remarks>
    public class ForLoopActionUsageVarRedefinitionRule : LibraryRedefinitionRule
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ForLoopActionUsageVarRedefinitionRule" /> class.
        /// </summary>
        /// <param name="libraryTypeIndex">The index resolving the library Feature by qualified name.</param>
        /// <param name="factory">The factory creating the detached Redefinition.</param>
        public ForLoopActionUsageVarRedefinitionRule(ILibraryTypeIndex libraryTypeIndex, IImpliedRelationshipFactory factory)
            : base(libraryTypeIndex, factory)
        {
        }

        /// <summary>
        /// Gets the name of the semantic constraint this rule implements.
        /// </summary>
        public override string ConstraintName => "checkForLoopActionUsageVarRedefinition";

        /// <summary>
        /// Returns the loop variable of a ForLoopActionUsage as the redefining Feature.
        /// </summary>
        /// <param name="element">The Element under evaluation.</param>
        /// <returns>The loop variable and the library qualified name, or <c>null</c> when there is none.</returns>
        protected override (IFeature RedefiningFeature, string LibraryQualifiedName)? QueryRedefinition(IElement element)
        {
            return element is IForLoopActionUsage { loopVariable: not null } forLoopActionUsage
                ? (forLoopActionUsage.loopVariable, "Actions::ForLoopAction::var")
                : null;
        }
    }
}
