// ------------------------------------------------------------------------------------------------
// <copyright file="FlowDefinitionExtensionsTestFixture.cs" company="Starion Group S.A.">
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
