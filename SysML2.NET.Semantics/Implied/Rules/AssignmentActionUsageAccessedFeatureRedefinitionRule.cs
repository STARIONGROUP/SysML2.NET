// -------------------------------------------------------------------------------------------------
// <copyright file="AssignmentActionUsageAccessedFeatureRedefinitionRule.cs" company="Starion Group S.A.">
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
    /// Implements checkAssignmentActionUsageAccessedFeatureRedefinition: the accessed Feature of an assignment action's target parameter redefines AssigmentAction::target::startingAt::accessedFeature.
    /// </summary>
    /// <remarks>
    /// OCL: <c>let targetParameter : Feature = inputParameter(1) in targetParameter &lt;&gt; null and targetParameter.ownedFeature-&gt;notEmpty() and targetParameter.ownedFeature-&gt;first().ownedFeature-&gt;notEmpty() and targetParameter.ownedFeature-&gt;first().ownedFeature-&gt;first().redefinesFromLibrary('AssigmentAction::target::startingAt::accessedFeature')</c>
    /// </remarks>
    public class AssignmentActionUsageAccessedFeatureRedefinitionRule : LibraryRedefinitionRule
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AssignmentActionUsageAccessedFeatureRedefinitionRule" /> class.
        /// </summary>
        /// <param name="libraryTypeIndex">The index resolving the library Feature by qualified name.</param>
        /// <param name="factory">The factory creating the detached Redefinition.</param>
        public AssignmentActionUsageAccessedFeatureRedefinitionRule(ILibraryTypeIndex libraryTypeIndex, IImpliedRelationshipFactory factory)
            : base(libraryTypeIndex, factory)
        {
        }

        /// <summary>
        /// Gets the name of the semantic constraint this rule implements.
        /// </summary>
        public override string ConstraintName => "checkAssignmentActionUsageAccessedFeatureRedefinition";

        /// <summary>
        /// Returns the Feature that must redefine the library Feature.
        /// </summary>
        /// <param name="element">The Element under evaluation.</param>
        /// <returns>The Feature and the library qualified name, or <c>null</c> when the constraint does not apply.</returns>
        protected override (IFeature RedefiningFeature, string LibraryQualifiedName)? QueryRedefinition(IElement element)
        {
            var redefiningFeature = element is IAssignmentActionUsage assignmentActionUsage ? AssignmentActionUsageNavigation.QueryAccessedFeature(assignmentActionUsage) : null;

            return redefiningFeature == null
                ? null
                : (redefiningFeature, "AssigmentAction::target::startingAt::accessedFeature");
        }
    }
}
