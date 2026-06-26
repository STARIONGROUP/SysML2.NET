// -------------------------------------------------------------------------------------------------
// <copyright file="StateSubactionMembershipExtensionsTestFixture.cs" company="Starion Group S.A.">
// 
//   Copyright (C) 2022-2026 Starion Group S.A.
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
    using SysML2.NET.Core.POCO.Systems.Actions;
    using SysML2.NET.Core.POCO.Systems.States;
    using SysML2.NET.Core.Systems.States;
    using SysML2.NET.Exceptions;
    using SysML2.NET.Extensions;

    [TestFixture]
    public class StateSubactionMembershipExtensionsTestFixture
    {
        [Test]
        public void VerifyComputeAction()
        {
            Assert.That(() => ((IStateSubactionMembership)null).ComputeAction(), Throws.TypeOf<ArgumentNullException>());

            // Empty OwnedRelatedElement → [1..1] violation: throws IncompleteModelException.
            var stateSubactionMembership = new StateSubactionMembership();

            Assert.That(() => stateSubactionMembership.ComputeAction(), Throws.TypeOf<IncompleteModelException>());

            // Single IActionUsage wired via the public API → returned.
            var owningType = new StateDefinition();
            var singleMembership = new StateSubactionMembership { Kind = StateSubactionKind.Do };
            var singleAction = new ActionUsage();

            owningType.AssignOwnership(singleMembership, singleAction);

            Assert.That(singleMembership.ComputeAction(), Is.SameAs(singleAction));

            // Two IActionUsages in OwnedRelatedElement → upper-bound violation: throws MultiplicityViolationException.
            var twoActionMembership = new StateSubactionMembership();
            var firstAction = new ActionUsage();
            var secondAction = new ActionUsage();

            ((IContainedRelationship)twoActionMembership).OwnedRelatedElement.Add(firstAction);
            ((IContainedRelationship)twoActionMembership).OwnedRelatedElement.Add(secondAction);

            Assert.That(() => twoActionMembership.ComputeAction(), Throws.TypeOf<MultiplicityViolationException>());

            // Mixed-type owned related elements: exactly one IActionUsage alongside a non-IActionUsage (Namespace).
            // The OfType<IActionUsage>() projection MUST pick out the IActionUsage regardless of its position
            // (this is the core robustness guarantee — never positionally index the unfiltered collection).
            var mixedMembership = new StateSubactionMembership();
            var siblingNonAction = new Namespace();
            var mixedAction = new ActionUsage();

            ((IContainedRelationship)mixedMembership).OwnedRelatedElement.Add(siblingNonAction);
            ((IContainedRelationship)mixedMembership).OwnedRelatedElement.Add(mixedAction);

            Assert.That(mixedMembership.ComputeAction(), Is.SameAs(mixedAction));

            // OwnedRelatedElement populated with non-IActionUsage element(s) only → no IActionUsage match:
            // [1..1] violation, throws IncompleteModelException.
            var nonActionMembership = new StateSubactionMembership();
            var nonActionElement = new Namespace();

            ((IContainedRelationship)nonActionMembership).OwnedRelatedElement.Add(nonActionElement);

            Assert.That(() => nonActionMembership.ComputeAction(), Throws.TypeOf<IncompleteModelException>());
        }
    }
}
