// -------------------------------------------------------------------------------------------------
// <copyright file="ConstraintUsageRequirementConstraintSpecializationRule.cs" company="Starion Group S.A.">
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
    using SysML2.NET.Core.POCO.Systems.Constraints;
    using SysML2.NET.Core.POCO.Systems.Requirements;
    using SysML2.NET.Core.Systems.Requirements;

    /// <summary>
    /// Implements checkConstraintUsageRequirementConstraintSpecialization: a composite ConstraintUsage owned
    /// by a requirement subsets the library assumptions or constraints according to the kind of its
    /// membership.
    /// </summary>
    /// <remarks>
    /// OCL: <c>isComposite and owningFeatureMembership &lt;&gt; null and
    /// owningFeatureMembership.oclIsKindOf(RequirementConstraintMembership) implies if
    /// owningFeatureMembership.oclAsType(RequirementConstraintMembership).kind =
    /// RequirementConstraintKind::assumption then
    /// specializesFromLibrary('Requirements::RequirementCheck::assumptions') else
    /// specializesFromLibrary('Requirements::RequirementCheck::constraints') endif</c>.
    /// <para>The <c>isComposite</c> guard matters: a referential ConstraintUsage in the same membership is
    /// NOT subject to the constraint.</para>
    /// </remarks>
    public class ConstraintUsageRequirementConstraintSpecializationRule : LibrarySpecializationRule
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConstraintUsageRequirementConstraintSpecializationRule" /> class.
        /// </summary>
        /// <param name="libraryTypeIndex">The index resolving the library Feature by qualified name.</param>
        /// <param name="factory">The factory creating the detached Subsetting.</param>
        public ConstraintUsageRequirementConstraintSpecializationRule(ILibraryTypeIndex libraryTypeIndex, IImpliedRelationshipFactory factory)
            : base(libraryTypeIndex, factory)
        {
        }

        /// <summary>
        /// Gets the name of the semantic constraint this rule implements.
        /// </summary>
        public override string ConstraintName => "checkConstraintUsageRequirementConstraintSpecialization";

        /// <summary>
        /// Returns the ConstraintUsage together with the library Feature its membership kind selects.
        /// </summary>
        /// <param name="element">The Element under evaluation.</param>
        /// <returns>The Feature and the library qualified name, or <c>null</c> when the constraint does not apply.</returns>
        protected override (IFeature SpecificFeature, string LibraryQualifiedName)? QuerySpecialization(IElement element)
        {
            if (element is not IConstraintUsage { IsComposite: true, owningFeatureMembership: IRequirementConstraintMembership membership } constraintUsage)
            {
                return null;
            }

            return (constraintUsage, membership.Kind == RequirementConstraintKind.Assumption
                ? "Requirements::RequirementCheck::assumptions"
                : "Requirements::RequirementCheck::constraints");
        }
    }
}
