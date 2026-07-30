// -------------------------------------------------------------------------------------------------
// <copyright file="FlowDefinitionExtensionsTestFixture.cs" company="Starion Group S.A.">
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

    using SysML2.NET.Core.POCO.Core.Features;
    using SysML2.NET.Core.POCO.Core.Types;
    using SysML2.NET.Core.POCO.Systems.DefinitionAndUsage;
    using SysML2.NET.Core.POCO.Systems.Flows;
    using SysML2.NET.Extensions;

    [TestFixture]
    public class FlowDefinitionExtensionsTestFixture
    {
        [Test]
        public void VerifyComputeFlowEnd()
        {
            Assert.That(() => ((IFlowDefinition)null).ComputeFlowEnd(), Throws.TypeOf<ArgumentNullException>());

            // Empty: no end features → empty list.
            var emptySubject = new FlowDefinition();

            Assert.That(emptySubject.ComputeFlowEnd(), Is.Empty);

            // Populated: an end feature that is a Usage (IsEnd = true), plus (a) a non-end Usage and
            // (b) an end feature that is NOT a Usage. flowEnd = endFeature->selectByKind(Usage) keeps only
            // the end Usage.
            var subject = new FlowDefinition();
            var endUsage = new ReferenceUsage { IsEnd = true };
            var nonEndUsage = new ReferenceUsage { IsEnd = false };
            var nonUsageEndFeature = new Feature { IsEnd = true };
            subject.AssignOwnership(new FeatureMembership(), endUsage);
            subject.AssignOwnership(new FeatureMembership(), nonEndUsage);
            subject.AssignOwnership(new FeatureMembership(), nonUsageEndFeature);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(subject.ComputeFlowEnd(), Has.Count.EqualTo(1));
                Assert.That(subject.ComputeFlowEnd(), Does.Contain(endUsage));
                Assert.That(subject.ComputeFlowEnd(), Does.Not.Contain(nonEndUsage));
                Assert.That(subject.ComputeFlowEnd(), Does.Not.Contain(nonUsageEndFeature));
            }
        }
    }
}
