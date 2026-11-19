// ------------------------------------------------------------------------------------------------
// <copyright file="TransitionFeatureMembershipExtensionsTestFixture.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
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
    using SysML2.NET.Extensions;

    using Type = SysML2.NET.Core.POCO.Core.Types.Type;

    [TestFixture]
    public class TransitionFeatureMembershipExtensionsTestFixture
    {
        [Test]
        public void VerifyComputeTransitionFeature()
        {
            Assert.That(() => ((ITransitionFeatureMembership)null).ComputeTransitionFeature(), Throws.TypeOf<ArgumentNullException>());

            var transitionFeatureMembership = new TransitionFeatureMembership();

            Assert.That(transitionFeatureMembership.ComputeTransitionFeature(), Is.Null);

            // Single AcceptActionUsage wired via the public API → returned.
            var owningType = new Type();
            var acceptActionUsage = new AcceptActionUsage();

            owningType.AssignOwnership(transitionFeatureMembership, acceptActionUsage);

            Assert.That(transitionFeatureMembership.ComputeTransitionFeature(), Is.SameAs(acceptActionUsage));

            var twoStepMembership = new TransitionFeatureMembership();
            var firstStep = new AcceptActionUsage();
            var secondStep = new AcceptActionUsage();

            ((IContainedRelationship)twoStepMembership).OwnedRelatedElement.Add(firstStep);
            ((IContainedRelationship)twoStepMembership).OwnedRelatedElement.Add(secondStep);

            Assert.That(twoStepMembership.ComputeTransitionFeature(), Is.SameAs(firstStep));

            // Mixed-type owned related elements: exactly one IStep alongside a non-IStep (Namespace).
            // The OfType<IStep>() projection MUST pick out the IStep regardless of its position
            // (this is the core robustness guarantee — never positionally index the unfiltered collection).
            var mixedMembership = new TransitionFeatureMembership();
            var siblingNonStep = new Namespace();
            var mixedStep = new AcceptActionUsage();

            ((IContainedRelationship)mixedMembership).OwnedRelatedElement.Add(siblingNonStep);
            ((IContainedRelationship)mixedMembership).OwnedRelatedElement.Add(mixedStep);

            Assert.That(mixedMembership.ComputeTransitionFeature(), Is.SameAs(mixedStep));

            var nonStepMembership = new TransitionFeatureMembership();
            var nonStepElement = new Namespace();

            ((IContainedRelationship)nonStepMembership).OwnedRelatedElement.Add(nonStepElement);

            Assert.That(nonStepMembership.ComputeTransitionFeature(), Is.Null);
        }
    }
}
