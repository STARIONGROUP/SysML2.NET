// ------------------------------------------------------------------------------------------------
// <copyright file="FlowUsageExtensionsTestFixture.cs" company="Starion Group S.A.">
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
