// -------------------------------------------------------------------------------------------------
// <copyright file="BehaviorExtensionsTestFixture.cs" company="Starion Group S.A.">
// 
//   Copyright (C) 2022-2026 Starion Group S.A.
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
