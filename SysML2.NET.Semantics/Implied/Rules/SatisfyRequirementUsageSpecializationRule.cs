// -------------------------------------------------------------------------------------------------
// <copyright file="SatisfyRequirementUsageSpecializationRule.cs" company="Starion Group S.A.">
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
    using SysML2.NET.Core.POCO.Systems.Requirements;

    /// <summary>
    /// Implements checkSatisfyRequirementUsageSpecialization: a satisfy assertion subsets the library checks
    /// for the sense in which the requirement is asserted to be satisfied.
    /// </summary>
    /// <remarks>
    /// OCL: <c>if isNegated then specializesFromLibrary('Requirements::notSatisfiedRequirementChecks')
    /// else specializesFromLibrary('Requirements::satisfiedRequirementChecks')</c>.
    /// <para>Takes precedence over <see cref="AssertConstraintUsageSpecializationRule" />, which excludes
    /// this metaclass for that reason.</para>
    /// </remarks>
    public class SatisfyRequirementUsageSpecializationRule : LibrarySpecializationRule
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SatisfyRequirementUsageSpecializationRule" /> class.
        /// </summary>
        /// <param name="libraryTypeIndex">The index resolving the library Feature by qualified name.</param>
        /// <param name="factory">The factory creating the detached Subsetting.</param>
        public SatisfyRequirementUsageSpecializationRule(ILibraryTypeIndex libraryTypeIndex, IImpliedRelationshipFactory factory)
            : base(libraryTypeIndex, factory)
        {
        }

        /// <summary>
        /// Gets the name of the semantic constraint this rule implements.
        /// </summary>
        public override string ConstraintName => "checkSatisfyRequirementUsageSpecialization";

        /// <summary>
        /// Returns the satisfy assertion together with the library Feature its negation selects.
        /// </summary>
        /// <param name="element">The Element under evaluation.</param>
        /// <returns>The Feature and the library qualified name, or <c>null</c> when the constraint does not apply.</returns>
        protected override (IFeature SpecificFeature, string LibraryQualifiedName)? QuerySpecialization(IElement element)
        {
            if (element is not ISatisfyRequirementUsage satisfyRequirementUsage)
            {
                return null;
            }

            var libraryQualifiedName = satisfyRequirementUsage.IsNegated
                ? "Requirements::notSatisfiedRequirementChecks"
                : "Requirements::satisfiedRequirementChecks";

            return (satisfyRequirementUsage, libraryQualifiedName);
        }
    }
}
