// -------------------------------------------------------------------------------------------------
// <copyright file="InvariantSpecializationRule.cs" company="Starion Group S.A.">
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
    using SysML2.NET.Core.POCO.Kernel.Functions;
    using SysML2.NET.Core.POCO.Root.Elements;

    /// <summary>
    /// Implements checkInvariantSpecialization: an Invariant subsets the library evaluations for the truth
    /// value it asserts.
    /// </summary>
    /// <remarks>
    /// OCL: <c>if isNegated then specializesFromLibrary('Performances::falseEvaluations')
    /// else specializesFromLibrary('Performances::trueEvaluations') endif</c>.
    /// </remarks>
    public class InvariantSpecializationRule : LibrarySpecializationRule
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvariantSpecializationRule" /> class.
        /// </summary>
        /// <param name="libraryTypeIndex">The index resolving the library Feature by qualified name.</param>
        /// <param name="factory">The factory creating the detached Subsetting.</param>
        public InvariantSpecializationRule(ILibraryTypeIndex libraryTypeIndex, IImpliedRelationshipFactory factory)
            : base(libraryTypeIndex, factory)
        {
        }

        /// <summary>
        /// Gets the name of the semantic constraint this rule implements.
        /// </summary>
        public override string ConstraintName => "checkInvariantSpecialization";

        /// <summary>
        /// Returns the Invariant together with the library Feature its negation selects.
        /// </summary>
        /// <param name="element">The Element under evaluation.</param>
        /// <returns>The Feature and the library qualified name, or <c>null</c> when the constraint does not apply.</returns>
        protected override (IFeature SpecificFeature, string LibraryQualifiedName)? QuerySpecialization(IElement element)
        {
            if (element is not IInvariant invariant)
            {
                return null;
            }

            var libraryQualifiedName = invariant.IsNegated
                ? "Performances::falseEvaluations"
                : "Performances::trueEvaluations";

            return (invariant, libraryQualifiedName);
        }
    }
}
