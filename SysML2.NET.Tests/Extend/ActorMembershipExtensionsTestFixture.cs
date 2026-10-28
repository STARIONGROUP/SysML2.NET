// ------------------------------------------------------------------------------------------------
// <copyright file="ActorMembershipExtensionsTestFixture.cs" company="Starion Group S.A.">
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
    using SysML2.NET.Core.POCO.Systems.Parts;
    using SysML2.NET.Core.POCO.Systems.Requirements;
    using SysML2.NET.Exceptions;
    using SysML2.NET.Extensions;

    [TestFixture]
    public class ActorMembershipExtensionsTestFixture
    {
        [Test]
        public void VerifyComputeOwnedActorParameter()
        {
            Assert.That(() => ((IActorMembership)null).ComputeOwnedActorParameter(), Throws.TypeOf<ArgumentNullException>());

            // Empty OwnedRelatedElement → [1..1] violation: throws IncompleteModelException.
            var emptyMembership = new ActorMembership();

            Assert.That(() => emptyMembership.ComputeOwnedActorParameter(), Throws.TypeOf<IncompleteModelException>());

            // Single non-IPartUsage in OwnedRelatedElement → [1..1] violation: throws IncompleteModelException.
            var nonPartMembership = new ActorMembership();
            var nonPartElement = new Namespace();
            ((IContainedRelationship)nonPartMembership).OwnedRelatedElement.Add(nonPartElement);

            Assert.That(() => nonPartMembership.ComputeOwnedActorParameter(), Throws.TypeOf<IncompleteModelException>());

            // Single IPartUsage wired via the public API → returned.
            // ActorMembership is a FeatureMembership and requires an IType source per AssignOwnership;
            // RequirementDefinition is the natural Requirements-namespace IType for an actor parameter.
            var owningDefinition = new RequirementDefinition();
            var actorMembership = new ActorMembership();
            var partUsage = new PartUsage();
            owningDefinition.AssignOwnership(actorMembership, partUsage);

            Assert.That(actorMembership.ComputeOwnedActorParameter(), Is.SameAs(partUsage));

            // Two IPartUsage in OwnedRelatedElement → [1..1] violation: throws IncompleteModelException.
            var twoPartMembership = new ActorMembership();
            var firstPart = new PartUsage();
            var secondPart = new PartUsage();
            ((IContainedRelationship)twoPartMembership).OwnedRelatedElement.Add(firstPart);
            ((IContainedRelationship)twoPartMembership).OwnedRelatedElement.Add(secondPart);

            Assert.That(twoPartMembership.ComputeOwnedActorParameter, Throws.TypeOf<MultiplicityViolationException>());

            // Mixed: non-IPartUsage (Namespace) alongside a single IPartUsage — the type filter picks
            // out the PartUsage regardless of its position.
            var mixedMembership = new ActorMembership();
            var siblingNamespace = new Namespace();
            var mixedPart = new PartUsage();
            ((IContainedRelationship)mixedMembership).OwnedRelatedElement.Add(siblingNamespace);
            ((IContainedRelationship)mixedMembership).OwnedRelatedElement.Add(mixedPart);

            Assert.That(mixedMembership.ComputeOwnedActorParameter(), Is.SameAs(mixedPart));
        }
    }
}
