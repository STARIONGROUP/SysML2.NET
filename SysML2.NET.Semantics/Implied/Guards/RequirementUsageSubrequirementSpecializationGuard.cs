// ------------------------------------------------------------------------------------------------
// <copyright file="RequirementUsageSubrequirementSpecializationGuard.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.Semantics.Implied.Guards
{
    using System;

    using SysML2.NET.Core.POCO.Root.Elements;
    using SysML2.NET.Core.POCO.Systems.Requirements;

    /// <summary>
    /// Guards checkRequirementUsageSubrequirementSpecialization: a composite RequirementUsage owned by a
    /// RequirementDefinition or RequirementUsage, but not via a RequirementConstraintMembership, specializes
    /// Requirements::RequirementCheck::subrequirements.
    /// </summary>
    /// <remarks>
    /// OCL: <c>isComposite and owningType &lt;&gt; null and (owningType.oclIsKindOf(RequirementDefinition) or
    /// owningType.oclIsKindOf(RequirementUsage)) and not
    /// owningFeatureMembership.oclIsKindOf(RequirementConstraintMembership) implies
    /// specializesFromLibrary('Requirements::RequirementCheck::subrequirements')</c>
    /// </remarks>
    public class RequirementUsageSubrequirementSpecializationGuard : IImpliedRuleGuard
    {
        /// <summary>
        /// Gets the name of the semantic constraint this guard decides.
        /// </summary>
        public string ConstraintName => "checkRequirementUsageSubrequirementSpecialization";

        /// <summary>
        /// Asserts whether the constraint applies to the supplied Element.
        /// </summary>
        /// <param name="element">The Element under evaluation.</param>
        /// <returns>True when the constraint applies.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="element" /> is null.</exception>
        public bool Applies(IElement element)
        {
            if (element == null)
            {
                throw new ArgumentNullException(nameof(element));
            }

            return element is IRequirementUsage { IsComposite: true, owningType: IRequirementDefinition or IRequirementUsage, owningFeatureMembership: not IRequirementConstraintMembership };
        }
    }
}
