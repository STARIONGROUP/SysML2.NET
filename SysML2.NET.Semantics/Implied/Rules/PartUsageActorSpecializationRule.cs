// ------------------------------------------------------------------------------------------------
// <copyright file="PartUsageActorSpecializationRule.cs" company="Starion Group S.A.">
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
    using SysML2.NET.Core.POCO.Systems.Parts;
    using SysML2.NET.Core.POCO.Systems.Requirements;

    /// <summary>
    /// Implements checkPartUsageActorSpecialization: an actor parameter subsets the library actors of the
    /// requirement or the case that owns it.
    /// </summary>
    /// <remarks>
    /// OCL: <c>owningFeatureMembership &lt;&gt; null and
    /// owningFeatureMembership.oclIsKindOf(ActorMembership) implies if
    /// owningType.oclIsKindOf(RequirementDefinition) or owningType.oclIsKindOf(RequirementUsage) then
    /// specializesFromLibrary('Requirements::RequirementCheck::actors') else
    /// specializesFromLibrary('Cases::Case::actors')</c>.
    /// <para>The else branch is the DEFAULT, not a case-only branch: an actor owned by anything other than a
    /// requirement — a case, or any other Type that admits an ActorMembership — takes <c>Cases::Case::actors</c>.</para>
    /// </remarks>
    public class PartUsageActorSpecializationRule : LibrarySpecializationRule
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PartUsageActorSpecializationRule" /> class.
        /// </summary>
        /// <param name="libraryTypeIndex">The index resolving the library Feature by qualified name.</param>
        /// <param name="factory">The factory creating the detached Subsetting.</param>
        public PartUsageActorSpecializationRule(ILibraryTypeIndex libraryTypeIndex, IImpliedRelationshipFactory factory)
            : base(libraryTypeIndex, factory)
        {
        }

        /// <summary>
        /// Gets the name of the semantic constraint this rule implements.
        /// </summary>
        public override string ConstraintName => "checkPartUsageActorSpecialization";

        /// <summary>
        /// Returns the actor parameter together with the library Feature its owning Type selects.
        /// </summary>
        /// <param name="element">The Element under evaluation.</param>
        /// <returns>The Feature and the library qualified name, or <c>null</c> when the constraint does not apply.</returns>
        protected override (IFeature SpecificFeature, string LibraryQualifiedName)? QuerySpecialization(IElement element)
        {
            if (element is not IPartUsage { owningFeatureMembership: IActorMembership } actor)
            {
                return null;
            }

            return (actor, actor.owningType is IRequirementDefinition or IRequirementUsage
                ? "Requirements::RequirementCheck::actors"
                : "Cases::Case::actors");
        }
    }
}
