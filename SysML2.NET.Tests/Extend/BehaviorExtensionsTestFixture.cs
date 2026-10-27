// ------------------------------------------------------------------------------------------------
// <copyright file="BehaviorExtensionsTestFixture.cs" company="Starion Group S.A.">
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
    using SysML2.NET.Extensions;

    [TestFixture]
    public class BehaviorExtensionsTestFixture
    {
        [Test]
        public void VerifyComputeParameter()
        {
            // Null subject:
            Assert.That(() => ((IBehavior)null).ComputeParameter(), Throws.TypeOf<ArgumentNullException>());

            // Empty Behavior Parameter list:
            var emptySubject = new Behavior();
            Assert.That(emptySubject.ComputeParameter(), Has.Count.EqualTo(0));

            // Typed by DirectedFeature:
            var subject = new Behavior();
            var parameter = new Feature { Direction = FeatureDirectionKind.In };
            var plainFeature = new Feature();
            subject.AssignOwnership(new FeatureMembership(), parameter);
            subject.AssignOwnership(new FeatureMembership(), plainFeature);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(subject.ComputeParameter(), Does.Contain(parameter));
                Assert.That(subject.ComputeParameter(), Does.Not.Contain(plainFeature));
            }
        }

        [Test]
        public void VerifyComputeStep()
        {
            // Null subject:
            Assert.That(() => ((IBehavior)null).ComputeStep(), Throws.TypeOf<ArgumentNullException>());

            // Empty Behavior Step list:
            var emptySubject = new Behavior();
            Assert.That(emptySubject.ComputeStep(), Has.Count.EqualTo(0));

            // Typed by Step:
            var subject = new Behavior();
            var step = new Step();
            var plainFeature = new Feature();
            subject.AssignOwnership(new FeatureMembership(), step);
            subject.AssignOwnership(new FeatureMembership(), plainFeature);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(subject.ComputeStep(), Does.Contain(step));
                Assert.That(subject.ComputeStep(), Does.Not.Contain(plainFeature));
            }
        }
    }
}
