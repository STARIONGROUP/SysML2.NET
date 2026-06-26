// -------------------------------------------------------------------------------------------------
// <copyright file="ViewpointUsageExtensionsTestFixture.cs" company="Starion Group S.A.">
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

    using SysML2.NET.Core.POCO.Core.Features;
    using SysML2.NET.Core.POCO.Systems.Requirements;
    using SysML2.NET.Core.POCO.Systems.Views;
    using SysML2.NET.Exceptions;
    using SysML2.NET.Extensions;

    [TestFixture]
    public class ViewpointUsageExtensionsTestFixture
    {
        [Test]
        public void VerifyComputeViewpointDefinition()
        {
            Assert.That(() => ((IViewpointUsage)null).ComputeViewpointDefinition(), Throws.TypeOf<ArgumentNullException>());

            var viewpointUsage = new ViewpointUsage();

            // Empty: no OwnedRelationship → null.
            Assert.That(viewpointUsage.ComputeViewpointDefinition(), Is.Null);

            // Negative: FeatureTyping whose Type is a non-ViewpointDefinition (Feature) → null.
            var nonViewpointType = new Feature();
            var typingToNonViewpoint = new FeatureTyping { Type = nonViewpointType };
            viewpointUsage.AssignOwnership(typingToNonViewpoint);

            Assert.That(viewpointUsage.ComputeViewpointDefinition(), Is.Null);

            // Positive: FeatureTyping whose Type is a ViewpointDefinition → returned.
            var viewpointDefinition = new ViewpointDefinition();
            var typingToViewpointDefinition = new FeatureTyping { Type = viewpointDefinition };
            viewpointUsage.AssignOwnership(typingToViewpointDefinition);

            Assert.That(viewpointUsage.ComputeViewpointDefinition(), Is.SameAs(viewpointDefinition));

            // Two FeatureTypings whose Type is a ViewpointDefinition → MultiplicityViolationException
            // (upper-bound violation of the derived [0..1] property).
            var viewpointDefinition2 = new ViewpointDefinition();
            var typingToViewpointDefinition2 = new FeatureTyping { Type = viewpointDefinition2 };
            viewpointUsage.AssignOwnership(typingToViewpointDefinition2);

            Assert.That(viewpointUsage.ComputeViewpointDefinition, Throws.TypeOf<MultiplicityViolationException>());
        }

        [Test]
        public void VerifyComputeViewpointStakeholder()
        {
            Assert.That(() => ((IViewpointUsage)null).ComputeViewpointStakeholder(), Throws.TypeOf<ArgumentNullException>());

            var viewpointUsage = new ViewpointUsage();

            // Empty: no framedConcern (no FramedConcernMembership in featureMembership) → empty result.
            Assert.That(viewpointUsage.ComputeViewpointStakeholder(), Is.Empty);

            // Populated: a FramedConcernMembership carrying a bare ConcernUsage (no StakeholderMembership
            // inside its featureMembership) → the OCL chain framedConcern.featureMembership
            // .selectByKind(StakeholderMembership).ownedStakeholderParameter projects to empty.
            var framedMembership = new FramedConcernMembership();
            var concernUsage = new ConcernUsage();
            viewpointUsage.AssignOwnership(framedMembership, concernUsage);

            Assert.That(viewpointUsage.ComputeViewpointStakeholder(), Is.Empty);
        }
    }
}
