// -------------------------------------------------------------------------------------------------
// <copyright file="IfActionUsageSpecializationRule.cs" company="Starion Group S.A.">
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
    /// Implements checkIfActionUsageSpecialization: an if action subsets the two-branch or three-branch
    /// library action according to whether it declares an else branch.
    /// </summary>
    /// <remarks>
    /// OCL: <c>if elseAction = null then specializesFromLibrary('Actions::ifThenActions')
    /// else specializesFromLibrary('Actions::ifThenElseActions') endif</c>.
    /// </remarks>
    public class IfActionUsageSpecializationRule : LibrarySpecializationRule
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IfActionUsageSpecializationRule" /> class.
        /// </summary>
        /// <param name="libraryTypeIndex">The index resolving the library Feature by qualified name.</param>
        /// <param name="factory">The factory creating the detached Subsetting.</param>
        public IfActionUsageSpecializationRule(ILibraryTypeIndex libraryTypeIndex, IImpliedRelationshipFactory factory)
            : base(libraryTypeIndex, factory)
        {
        }

        /// <summary>
        /// Gets the name of the semantic constraint this rule implements.
        /// </summary>
        public override string ConstraintName => "checkIfActionUsageSpecialization";

        /// <summary>
        /// Returns the if action together with the library Feature its else branch selects.
        /// </summary>
        /// <param name="element">The Element under evaluation.</param>
        /// <returns>The Feature and the library qualified name, or <c>null</c> when the constraint does not apply.</returns>
        protected override (IFeature SpecificFeature, string LibraryQualifiedName)? QuerySpecialization(IElement element)
        {
            return element is not IIfActionUsage ifActionUsage
                ? null
                : (ifActionUsage, ifActionUsage.elseAction == null
                    ? "Actions::ifThenActions"
                    : "Actions::ifThenElseActions");
        }
    }
}
