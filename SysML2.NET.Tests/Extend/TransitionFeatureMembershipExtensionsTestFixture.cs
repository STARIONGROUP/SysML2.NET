// -------------------------------------------------------------------------------------------------
// <copyright file="TransitionFeatureMembershipExtensionsTestFixture.cs" company="Starion Group S.A.">
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
    using SysML2.NET.Core.POCO.Systems.Actions;
    using SysML2.NET.Core.POCO.Systems.States;
    using SysML2.NET.Exceptions;
    using SysML2.NET.Extensions;

    using Type = SysML2.NET.Core.POCO.Core.Types.Type;

    [TestFixture]
    public class TransitionFeatureMembershipExtensionsTestFixture
    {
        [Test]
        public void VerifyComputeTransitionFeature()
        {
            Assert.That(() => ((ITransitionFeatureMembership)null).ComputeTransitionFeature(), Throws.TypeOf<ArgumentNullException>());

            // Empty OwnedRelatedElement → [1..1] violation: throws IncompleteModelException.
            var transitionFeatureMembership = new TransitionFeatureMembership();

            Assert.That(() => transitionFeatureMembership.ComputeTransitionFeature(), Throws.TypeOf<IncompleteModelException>());

            // Single AcceptActionUsage wired via the public API → returned.
            var owningType = new Type();
            var acceptActionUsage = new AcceptActionUsage();

            owningType.AssignOwnership(transitionFeatureMembership, acceptActionUsage);

            Assert.That(transitionFeatureMembership.ComputeTransitionFeature(), Is.SameAs(acceptActionUsage));

            // Two AcceptActionUsage instances in OwnedRelatedElement → [1..1] violation: throws IncompleteModelException.
            var twoStepMembership = new TransitionFeatureMembership();
            var firstStep = new AcceptActionUsage();
            var secondStep = new AcceptActionUsage();

            ((IContainedRelationship)twoStepMembership).OwnedRelatedElement.Add(firstStep);
            ((IContainedRelationship)twoStepMembership).OwnedRelatedElement.Add(secondStep);

            Assert.That(() => twoStepMembership.ComputeTransitionFeature(), Throws.TypeOf<IncompleteModelException>());

            // Mixed-type owned related elements: exactly one IStep alongside a non-IStep (Namespace).
            // The OfType<IStep>() projection MUST pick out the IStep regardless of its position
            // (this is the core robustness guarantee — never positionally index the unfiltered collection).
            var mixedMembership = new TransitionFeatureMembership();
            var siblingNonStep = new Namespace();
            var mixedStep = new AcceptActionUsage();

            ((IContainedRelationship)mixedMembership).OwnedRelatedElement.Add(siblingNonStep);
            ((IContainedRelationship)mixedMembership).OwnedRelatedElement.Add(mixedStep);

            Assert.That(mixedMembership.ComputeTransitionFeature(), Is.SameAs(mixedStep));

            // OwnedRelatedElement populated with non-IStep element(s) only → no IStep match:
            // [1..1] violation, throws IncompleteModelException.
            var nonStepMembership = new TransitionFeatureMembership();
            var nonStepElement = new Namespace();

            ((IContainedRelationship)nonStepMembership).OwnedRelatedElement.Add(nonStepElement);

            Assert.That(() => nonStepMembership.ComputeTransitionFeature(), Throws.TypeOf<IncompleteModelException>());
        }
    }
}
