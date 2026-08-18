// -------------------------------------------------------------------------------------------------
// <copyright file="RequirementUsageObjectiveRedefinitionRule.cs" company="Starion Group S.A.">
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
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using SysML2.NET.Core.POCO.Core.Features;
    using SysML2.NET.Core.POCO.Core.Types;
    using SysML2.NET.Core.POCO.Root.Elements;
    using SysML2.NET.Core.POCO.Systems.Cases;
    using SysML2.NET.Core.POCO.Systems.Requirements;

    /// <summary>
    /// Implements checkRequirementUsageObjectiveRedefinition: the objective of a case redefines the
    /// objective requirement of each case it specializes.
    /// </summary>
    /// <remarks>
    /// OCL: <c>owningfeatureMembership &lt;&gt; null and
    /// owningfeatureMembership.oclIsKindOf(ObjectiveMembership) implies
    /// owningType.ownedSpecialization.general-&gt;forAll(gen |
    /// (gen.oclIsKindOf(CaseDefinition) implies redefines(gen.oclAsType(CaseDefinition).objectiveRequirement))
    /// and (gen.oclIsKindOf(Feature) and gen.oclAsType(Feature).featureTarget.oclIsKindOf(CaseUsage) implies
    /// redefines(gen.oclAsType(Feature).featureTarget.oclAsType(CaseUsage).objectiveRequirement)))</c>.
    /// <para>A supertype reached as a Feature is resolved through its <c>featureTarget</c> before its
    /// objective is taken, so a case USAGE supertype contributes as well as a case DEFINITION.</para>
    /// </remarks>
    public class RequirementUsageObjectiveRedefinitionRule : IImpliedRelationshipRule
    {
        /// <summary>
        /// The factory creating the detached Redefinitions.
        /// </summary>
        private readonly IImpliedRelationshipFactory factory;

        /// <summary>
        /// Initializes a new instance of the <see cref="RequirementUsageObjectiveRedefinitionRule" /> class.
        /// </summary>
        /// <param name="factory">The factory creating the detached Redefinitions.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="factory" /> is null.</exception>
        public RequirementUsageObjectiveRedefinitionRule(IImpliedRelationshipFactory factory)
        {
            this.factory = factory ?? throw new ArgumentNullException(nameof(factory));
        }

        /// <summary>
        /// Gets the name of the semantic constraint this rule implements.
        /// </summary>
        public string ConstraintName => "checkRequirementUsageObjectiveRedefinition";

        /// <summary>
        /// Computes the Redefinitions an objective requirement requires towards the objectives of its owning
        /// Type's case supertypes.
        /// </summary>
        /// <param name="element">The Element under evaluation.</param>
        /// <returns>One Redefinition per case supertype carrying an objective; empty otherwise.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="element" /> is null.</exception>
        public IReadOnlyList<IRelationship> Apply(IElement element)
        {
            if (element == null)
            {
                throw new ArgumentNullException(nameof(element));
            }

            if (element is not IRequirementUsage { owningFeatureMembership: IObjectiveMembership, owningType: not null } objective)
            {
                return [];
            }

            return
            [
                ..objective.owningType.ownedSpecialization
                    .Select(specialization => QueryObjectiveRequirement(specialization.General))
                    .Where(supertypeObjective => supertypeObjective != null)
                    .Select(supertypeObjective => this.factory.CreateImpliedRedefinition(objective, supertypeObjective))
            ];
        }

        /// <summary>
        /// Returns the objective requirement of a supertype, resolving a Feature supertype through its
        /// feature target first.
        /// </summary>
        /// <param name="supertype">The supertype to inspect, which may be null.</param>
        /// <returns>The objective requirement, or <c>null</c> when the supertype is not a case.</returns>
        private static IRequirementUsage QueryObjectiveRequirement(IType supertype)
        {
            return supertype switch
            {
                ICaseDefinition caseDefinition => caseDefinition.objectiveRequirement,
                IFeature { featureTarget: ICaseUsage caseUsage } => caseUsage.objectiveRequirement,
                _ => null
            };
        }
    }
}
