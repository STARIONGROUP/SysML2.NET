// -------------------------------------------------------------------------------------------------
// <copyright file="FlowUsageExtensionsTestFixture.cs" company="Starion Group S.A.">
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
    using SysML2.NET.Core.POCO.Kernel.Interactions;
    using SysML2.NET.Core.POCO.Systems.Flows;
    using SysML2.NET.Extensions;

    using Type = SysML2.NET.Core.POCO.Core.Types.Type;

    [TestFixture]
    public class FlowUsageExtensionsTestFixture
    {
        [Test]
        public void VerifyComputeFlowDefinition()
        {
            Assert.That(() => ((IFlowUsage)null).ComputeFlowDefinition(), Throws.TypeOf<ArgumentNullException>());

            // Empty: no typings → empty list.
            var emptySubject = new FlowUsage();

            Assert.That(emptySubject.ComputeFlowDefinition(), Is.Empty);

            // Populated: a type that is an Interaction plus a non-Interaction type. flowDefinition =
            // type->selectByKind(Interaction) keeps only the Interaction.
            var subject = new FlowUsage();
            var interaction = new Interaction();
            var plainType = new Type();
            subject.AssignOwnership(new FeatureTyping { Type = interaction });
            subject.AssignOwnership(new FeatureTyping { Type = plainType });

            using (Assert.EnterMultipleScope())
            {
                Assert.That(subject.ComputeFlowDefinition(), Has.Count.EqualTo(1));
                Assert.That(subject.ComputeFlowDefinition(), Does.Contain(interaction));
                Assert.That(subject.ComputeFlowDefinition(), Does.Not.Contain(plainType));
            }
        }
    }
}
