// -------------------------------------------------------------------------------------------------
// <copyright file="ObjectiveMembershipExtensionsTestFixture.cs" company="Starion Group S.A.">
// 
//   Copyright 2022-2026 Starion Group S.A.
// 
//   Licensed under the Apache License, Version 2.0 (the "License");
//   you may not use this file except in compliance with the License.
//   You may obtain a copy of the License at
// 
//        http://www.apache.org/licenses/LICENSE-2.0
// 
//    Unless required by applicable law or agreed to in writing, software
//    distributed under the License is distributed on an "AS IS" BASIS,
//    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//    See the License for the specific language governing permissions and
//    limitations under the License.
// 
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.Tests.Extend
{
    using System;

    using NUnit.Framework;

    using SysML2.NET.Core.POCO.Root.Elements;
    using SysML2.NET.Core.POCO.Root.Namespaces;
    using SysML2.NET.Core.POCO.Systems.Cases;
    using SysML2.NET.Core.POCO.Systems.Requirements;
    using SysML2.NET.Exceptions;
    using SysML2.NET.Extensions;

    using Type = SysML2.NET.Core.POCO.Core.Types.Type;

    [TestFixture]
    public class ObjectiveMembershipExtensionsTestFixture
    {
        [Test]
        public void VerifyComputeOwnedObjectiveRequirement()
        {
            Assert.That(() => ((IObjectiveMembership)null).ComputeOwnedObjectiveRequirement(), Throws.TypeOf<ArgumentNullException>());

            // Empty OwnedRelatedElement → [1..1] violation: throws IncompleteModelException.
            var objectiveMembership = new ObjectiveMembership();

            Assert.That(() => objectiveMembership.ComputeOwnedObjectiveRequirement(), Throws.TypeOf<IncompleteModelException>());

            // Single IRequirementUsage wired via the public API → returned.
            var owningType = new Type();
            var objectiveRequirement = new RequirementUsage();

            owningType.AssignOwnership(objectiveMembership, objectiveRequirement);

            Assert.That(objectiveMembership.ComputeOwnedObjectiveRequirement(), Is.SameAs(objectiveRequirement));

            // Two IRequirementUsages in OwnedRelatedElement → [1..1] violation: throws IncompleteModelException.
            var twoRequirementMembership = new ObjectiveMembership();
            var firstRequirement = new RequirementUsage();
            var secondRequirement = new RequirementUsage();

            ((IContainedRelationship)twoRequirementMembership).OwnedRelatedElement.Add(firstRequirement);
            ((IContainedRelationship)twoRequirementMembership).OwnedRelatedElement.Add(secondRequirement);

            Assert.That(() => twoRequirementMembership.ComputeOwnedObjectiveRequirement(), Throws.TypeOf<IncompleteModelException>());

            // Mixed-type owned related elements: exactly one IRequirementUsage alongside a non-IRequirementUsage (Namespace).
            // The OfType<IRequirementUsage>() projection MUST pick out the IRequirementUsage regardless of its position
            // (this is the core robustness guarantee — never positionally index the unfiltered collection).
            var mixedMembership = new ObjectiveMembership();
            var siblingNonRequirement = new Namespace();
            var mixedRequirement = new RequirementUsage();

            ((IContainedRelationship)mixedMembership).OwnedRelatedElement.Add(siblingNonRequirement);
            ((IContainedRelationship)mixedMembership).OwnedRelatedElement.Add(mixedRequirement);

            Assert.That(mixedMembership.ComputeOwnedObjectiveRequirement(), Is.SameAs(mixedRequirement));

            // OwnedRelatedElement populated with non-IRequirementUsage element(s) only → no IRequirementUsage match:
            // [1..1] violation, throws IncompleteModelException.
            var nonRequirementMembership = new ObjectiveMembership();
            var nonRequirementElement = new Namespace();

            ((IContainedRelationship)nonRequirementMembership).OwnedRelatedElement.Add(nonRequirementElement);

            Assert.That(() => nonRequirementMembership.ComputeOwnedObjectiveRequirement(), Throws.TypeOf<IncompleteModelException>());
        }
    }
}
