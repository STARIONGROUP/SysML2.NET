// ------------------------------------------------------------------------------------------------
// <copyright file="AssertConstraintUsageSpecializationRule.cs" company="Starion Group S.A.">
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
    using SysML2.NET.Core.POCO.Systems.Constraints;
    using SysML2.NET.Core.POCO.Systems.Requirements;

    /// <summary>
    /// Implements checkAssertConstraintUsageSpecialization: an asserted ConstraintUsage subsets the library
    /// checks for the sense in which it is asserted.
    /// </summary>
    /// <remarks>
    /// OCL: <c>if isNegated then specializesFromLibrary('Constraints::negatedConstraintChecks')
    /// else specializesFromLibrary('Constraints::assertedConstraintChecks')</c>.
    /// <para>A SatisfyRequirementUsage IS an AssertConstraintUsage, but carries its own more specific
    /// constraint selecting from <c>Requirements::</c> instead. The two targets are unrelated library
    /// Features, so redundancy reduction would not collapse them and BOTH would be implied — hence the
    /// explicit exclusion here, which mirrors the specific constraint taking precedence over the general.</para>
    /// </remarks>
    public class AssertConstraintUsageSpecializationRule : LibrarySpecializationRule
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AssertConstraintUsageSpecializationRule" /> class.
        /// </summary>
        /// <param name="libraryTypeIndex">The index resolving the library Feature by qualified name.</param>
        /// <param name="factory">The factory creating the detached Subsetting.</param>
        public AssertConstraintUsageSpecializationRule(ILibraryTypeIndex libraryTypeIndex, IImpliedRelationshipFactory factory)
            : base(libraryTypeIndex, factory)
        {
        }

        /// <summary>
        /// Gets the name of the semantic constraint this rule implements.
        /// </summary>
        public override string ConstraintName => "checkAssertConstraintUsageSpecialization";

        /// <summary>
        /// Returns the ConstraintUsage together with the library Feature its negation selects.
        /// </summary>
        /// <param name="element">The Element under evaluation.</param>
        /// <returns>The Feature and the library qualified name, or <c>null</c> when the constraint does not apply.</returns>
        protected override (IFeature SpecificFeature, string LibraryQualifiedName)? QuerySpecialization(IElement element)
        {
            if (element is not IAssertConstraintUsage assertConstraintUsage || element is ISatisfyRequirementUsage)
            {
                return null;
            }

            return (assertConstraintUsage, assertConstraintUsage.IsNegated
                ? "Constraints::negatedConstraintChecks"
                : "Constraints::assertedConstraintChecks");
        }
    }
}
