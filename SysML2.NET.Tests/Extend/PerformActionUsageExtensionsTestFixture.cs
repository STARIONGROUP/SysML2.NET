// ------------------------------------------------------------------------------------------------
// <copyright file="PerformActionUsageExtensionsTestFixture.cs" company="Starion Group S.A.">
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
    using SysML2.NET.Core.POCO.Systems.Actions;
    using SysML2.NET.Core.POCO.Systems.Attributes;
    using SysML2.NET.Core.POCO.Systems.DefinitionAndUsage;
    using SysML2.NET.Extensions;

    [TestFixture]
    public class PerformActionUsageExtensionsTestFixture
    {
        [Test]
        public void VerifyComputePerformedAction()
        {
            Assert.That(() => ((IPerformActionUsage)null).ComputePerformedAction(), Throws.TypeOf<ArgumentNullException>());

            // No ownedReferenceSubsetting -> referencedFeatureTarget() is null -> returns self.
            var subjectNoReferent = new PerformActionUsage();

            Assert.That(subjectNoReferent.ComputePerformedAction(), Is.SameAs(subjectNoReferent));

            // ReferenceSubsetting whose referent IS an ActionUsage -> returns that ActionUsage.
            var subjectWithActionReferent = new PerformActionUsage();
            var actionReferent = new ActionUsage();
            subjectWithActionReferent.AssignOwnership(new ReferenceSubsetting { ReferencedFeature = actionReferent });

            // ReferenceSubsetting whose referent is NOT an ActionUsage -> the as-cast yields null.
            var subjectWithNonActionReferent = new PerformActionUsage();
            subjectWithNonActionReferent.AssignOwnership(new ReferenceSubsetting { ReferencedFeature = new AttributeUsage() });

            using (Assert.EnterMultipleScope())
            {
                Assert.That(subjectWithActionReferent.ComputePerformedAction(), Is.SameAs(actionReferent));
                Assert.That(subjectWithNonActionReferent.ComputePerformedAction(), Is.Null);
            }
        }

        [Test]
        public void VerifyComputeRedefinedNamingFeatureOperation()
        {
            Assert.That(() => ((IPerformActionUsage)null).ComputeRedefinedNamingFeatureOperation(), Throws.TypeOf<ArgumentNullException>());

            // performedAction <> self (an ActionUsage referent) -> returns that performedAction.
            var subjectWithActionReferent = new PerformActionUsage();
            var actionReferent = new ActionUsage();
            subjectWithActionReferent.AssignOwnership(new ReferenceSubsetting { ReferencedFeature = actionReferent });

            // performedAction == self (no referent) -> falls through to the Usage-level namingFeature.
            var subjectNoReferent = new PerformActionUsage();

            using (Assert.EnterMultipleScope())
            {
                Assert.That(subjectWithActionReferent.ComputeRedefinedNamingFeatureOperation(), Is.SameAs(actionReferent));
                Assert.That(subjectNoReferent.ComputeRedefinedNamingFeatureOperation(), Is.EqualTo(UsageExtensions.ComputeRedefinedNamingFeatureOperation(subjectNoReferent)));
            }
        }
    }
}
