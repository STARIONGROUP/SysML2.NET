// -------------------------------------------------------------------------------------------------
// <copyright file="AssignmentActionUsageReferentRedefinitionRule.cs" company="Starion Group S.A.">
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
    using SysML2.NET.Core.POCO.Root.Elements;
    using SysML2.NET.Core.POCO.Systems.Actions;

    /// <summary>
    /// Implements checkAssignmentActionUsageReferentRedefinition: the accessed Feature of an assignment action's target parameter redefines the action's referent.
    /// </summary>
    /// <remarks>
    /// OCL: <c>let targetParameter : Feature = inputParameter(1) in targetParameter &lt;&gt; null and targetParameter.ownedFeature-&gt;notEmpty() and targetParameter.ownedFeature-&gt;first().ownedFeature-&gt;notEmpty() and targetParameter.ownedFeature-&gt;first().ownedFeature-&gt;first().redefines(referent)</c>
    /// </remarks>
    public class AssignmentActionUsageReferentRedefinitionRule : IImpliedRelationshipRule
    {
        /// <summary>
        /// The factory creating the detached Redefinition.
        /// </summary>
        private readonly IImpliedRelationshipFactory factory;

        /// <summary>
        /// Initializes a new instance of the <see cref="AssignmentActionUsageReferentRedefinitionRule" /> class.
        /// </summary>
        /// <param name="factory">The factory creating the detached Redefinition.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="factory" /> is null.</exception>
        public AssignmentActionUsageReferentRedefinitionRule(IImpliedRelationshipFactory factory)
        {
            this.factory = factory ?? throw new ArgumentNullException(nameof(factory));
        }

        /// <summary>
        /// Gets the name of the semantic constraint this rule implements.
        /// </summary>
        public string ConstraintName => "checkAssignmentActionUsageReferentRedefinition";

        /// <summary>
        /// Computes the implied Redefinition the constraint requires of the supplied Element.
        /// </summary>
        /// <param name="element">The Element under evaluation.</param>
        /// <returns>A single Redefinition, or empty when the constraint does not apply.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="element" /> is null.</exception>
        public IReadOnlyList<IRelationship> Apply(IElement element)
        {
            if (element == null)
            {
                throw new ArgumentNullException(nameof(element));
            }

            if (element is not IAssignmentActionUsage { referent: not null } assignmentActionUsage)
            {
                return [];
            }

            var accessedFeature = AssignmentActionUsageNavigation.QueryAccessedFeature(assignmentActionUsage);

            return accessedFeature == null
                ? []
                : [this.factory.CreateImpliedRedefinition(accessedFeature, assignmentActionUsage.referent)];
        }
    }
}
