// -------------------------------------------------------------------------------------------------
// <copyright file="ViewpointDefinitionExtensionsTestFixture.cs" company="Starion Group S.A.">
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

            // Populated case: FramedConcernMembership is present; accessing framedConcern calls
            // FramedConcernMembershipExtensions.ComputeOwnedConcern which is an out-of-scope stub.
            var framedMembership = new FramedConcernMembership();
            var concernUsage = new ConcernUsage();
            viewpointDefinition.AssignOwnership(framedMembership, concernUsage);

            Assert.That(() => viewpointDefinition.ComputeViewpointStakeholder(), Throws.TypeOf<NotSupportedException>());
        }
    }
}
