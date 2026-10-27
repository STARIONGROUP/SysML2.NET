// ------------------------------------------------------------------------------------------------
// <copyright file="StepExtensionsTestFixture.cs" company="Starion Group S.A.">
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

    using Type = SysML2.NET.Core.POCO.Core.Types.Type;

    [TestFixture]
    public class StepExtensionsTestFixture
    {
        [Test]
        public void VerifyComputeBehavior()
        {
            // Null subject:
            Assert.That(() => ((IStep)null).ComputeBehavior(), Throws.TypeOf<ArgumentNullException>());

            // Empty: no typings → empty.
            var emptySubject = new Step();
            Assert.That(emptySubject.ComputeBehavior(), Is.Empty);

            // Kind filter: a Behavior type plus a non-Behavior type → only the Behavior.
            var subject = new Step();
            var behavior = new Behavior();
            var plainType = new Type();
            subject.AssignOwnership(new FeatureTyping { Type = behavior });
            subject.AssignOwnership(new FeatureTyping { Type = plainType });

            using (Assert.EnterMultipleScope())
            {
                Assert.That(subject.ComputeBehavior(), Does.Contain(behavior));
                Assert.That(subject.ComputeBehavior(), Does.Not.Contain(plainType));
                Assert.That(subject.ComputeBehavior(), Has.Count.EqualTo(1));
            }
        }

        [Test]
        public void VerifyComputeParameter()
        {
            // Null subject:
            Assert.That(() => ((IStep)null).ComputeParameter(), Throws.TypeOf<ArgumentNullException>());

            // Empty: no directed features → empty.
            var emptySubject = new Step();
            Assert.That(emptySubject.ComputeParameter(), Is.Empty);

            // Directed features returned in order; a non-directed feature is excluded.
            var subject = new Step();
            var firstParameter = new Feature { Direction = FeatureDirectionKind.In };
            var secondParameter = new Feature { Direction = FeatureDirectionKind.Out };
            var plainFeature = new Feature();
            subject.AssignOwnership(new FeatureMembership(), firstParameter);
            subject.AssignOwnership(new FeatureMembership(), plainFeature);
            subject.AssignOwnership(new FeatureMembership(), secondParameter);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(subject.ComputeParameter(), Does.Not.Contain(plainFeature));
                Assert.That(subject.ComputeParameter(), Is.EqualTo([firstParameter, secondParameter]));
            }
        }
    }
}
