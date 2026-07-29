// -------------------------------------------------------------------------------------------------
// <copyright file="StepExtensionsTestFixture.cs" company="Starion Group S.A.">
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
