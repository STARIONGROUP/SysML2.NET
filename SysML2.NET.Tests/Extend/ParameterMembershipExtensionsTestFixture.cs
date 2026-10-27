// ------------------------------------------------------------------------------------------------
// <copyright file="ParameterMembershipExtensionsTestFixture.cs" company="Starion Group S.A.">
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

    using SysML2.NET.Core.Core.Types;
    using SysML2.NET.Core.POCO.Core.Features;
    using SysML2.NET.Core.POCO.Core.Types;
    using SysML2.NET.Core.POCO.Kernel.Behaviors;
    using SysML2.NET.Core.POCO.Root.Elements;
    using SysML2.NET.Core.POCO.Root.Namespaces;
    using SysML2.NET.Exceptions;
    using SysML2.NET.Extensions;

    using Type = SysML2.NET.Core.POCO.Core.Types.Type;

    [TestFixture]
    public class ParameterMembershipExtensionsTestFixture
    {
        [Test]
        public void VerifyComputeOwnedMemberParameter()
        {
            Assert.That(() => ((IParameterMembership)null).ComputeOwnedMemberParameter(), Throws.TypeOf<ArgumentNullException>());

            // Empty OwnedRelatedElement → [1..1] violation: throws IncompleteModelException.
            var parameterMembership = new ParameterMembership();

            Assert.That(() => parameterMembership.ComputeOwnedMemberParameter(), Throws.TypeOf<IncompleteModelException>());

            // Single IFeature wired via the public API → returned.
            var owningType = new Type();
            var feature = new Feature();

            owningType.AssignOwnership(parameterMembership, feature);

            Assert.That(parameterMembership.ComputeOwnedMemberParameter(), Is.SameAs(feature));

            // Two IFeatures in OwnedRelatedElement → upper-bound violation: throws MultiplicityViolationException.
            var twoFeatureMembership = new ParameterMembership();
            var secondFeature = new Feature();

            ((IContainedRelationship)twoFeatureMembership).OwnedRelatedElement.Add(feature);
            ((IContainedRelationship)twoFeatureMembership).OwnedRelatedElement.Add(secondFeature);

            Assert.That(() => twoFeatureMembership.ComputeOwnedMemberParameter(), Throws.TypeOf<MultiplicityViolationException>());

            // Mixed-type owned related elements: exactly one IFeature alongside a non-IFeature (Namespace).
            // The OfType<IFeature>() projection MUST pick out the IFeature regardless of its position
            // (this is the core robustness guarantee — never positionally index the unfiltered collection).
            var mixedMembership = new ParameterMembership();
            var siblingNonFeature = new Namespace();
            var mixedFeature = new Feature();

            ((IContainedRelationship)mixedMembership).OwnedRelatedElement.Add(siblingNonFeature);
            ((IContainedRelationship)mixedMembership).OwnedRelatedElement.Add(mixedFeature);

            Assert.That(mixedMembership.ComputeOwnedMemberParameter(), Is.SameAs(mixedFeature));

            // OwnedRelatedElement populated with non-IFeature element(s) only → no IFeature match:
            // [1..1] violation, throws IncompleteModelException.
            var nonFeatureMembership = new ParameterMembership();
            var nonFeatureElement = new Namespace();

            ((IContainedRelationship)nonFeatureMembership).OwnedRelatedElement.Add(nonFeatureElement);

            Assert.That(() => nonFeatureMembership.ComputeOwnedMemberParameter(), Throws.TypeOf<IncompleteModelException>());
        }

        [Test]
        public void VerifyComputeParameterDirectionOperation()
        {
            Assert.That(() => ((IParameterMembership)null).ComputeParameterDirectionOperation(), Throws.TypeOf<ArgumentNullException>());

            var parameterMembership = new ParameterMembership();
            var owningType = new Type();
            var feature = new Feature();

            owningType.AssignOwnership(parameterMembership, feature);

            Assert.That(parameterMembership.ComputeParameterDirectionOperation(), Is.EqualTo(FeatureDirectionKind.In));
        }
    }
}
