// ------------------------------------------------------------------------------------------------
// <copyright file="ViewpointDefinitionExtensionsTestFixture.cs" company="Starion Group S.A.">
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

    using SysML2.NET.Core.POCO.Systems.Constraints;
    using SysML2.NET.Core.POCO.Systems.Requirements;
    using SysML2.NET.Core.POCO.Systems.Views;
    using SysML2.NET.Extensions;

    [TestFixture]
    public class ViewpointDefinitionExtensionsTestFixture
    {
        [Test]
        public void VerifyComputeViewpointStakeholder()
        {
            Assert.That(() => ((IViewpointDefinition)null).ComputeViewpointStakeholder(), Throws.TypeOf<ArgumentNullException>());

            var viewpointDefinition = new ViewpointDefinition();

            // Empty: no framedConcern (no FramedConcernMembership in featureMembership) → empty result.
            Assert.That(viewpointDefinition.ComputeViewpointStakeholder(), Is.Empty);

            // Populated: a FramedConcernMembership carrying a bare ConcernUsage (no StakeholderMembership
            // inside its featureMembership) → the OCL chain framedConcern.featureMembership
            // .selectByKind(StakeholderMembership).ownedStakeholderParameter projects to empty.
            var framedMembership = new FramedConcernMembership();
            var concernUsage = new ConcernUsage();
            viewpointDefinition.AssignOwnership(framedMembership, concernUsage);

            Assert.That(viewpointDefinition.ComputeViewpointStakeholder(), Is.Empty);
        }
    }
}
