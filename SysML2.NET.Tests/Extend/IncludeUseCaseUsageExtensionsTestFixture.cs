// ------------------------------------------------------------------------------------------------
// <copyright file="IncludeUseCaseUsageExtensionsTestFixture.cs" company="Starion Group S.A.">
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
    using SysML2.NET.Core.POCO.Systems.UseCases;
    using SysML2.NET.Extensions;

    [TestFixture]
    public class IncludeUseCaseUsageExtensionsTestFixture
    {
        [Test]
        public void VerifyComputeUseCaseIncluded()
        {
            Assert.That(() => ((IIncludeUseCaseUsage)null).ComputeUseCaseIncluded(), Throws.TypeOf<ArgumentNullException>());

            // No ownedReferenceSubsetting → returns the subject itself (IncludeUseCaseUsage IS a UseCaseUsage).
            var subjectNoSubsetting = new IncludeUseCaseUsage();

            Assert.That(subjectNoSubsetting.ComputeUseCaseIncluded(), Is.SameAs(subjectNoSubsetting));

            // ReferenceSubsetting whose ReferencedFeature is a UseCaseUsage → returns that usage.
            var subjectWithUseCaseTarget = new IncludeUseCaseUsage();
            var targetUseCase = new UseCaseUsage();
            subjectWithUseCaseTarget.AssignOwnership(new ReferenceSubsetting { ReferencedFeature = targetUseCase });

            Assert.That(subjectWithUseCaseTarget.ComputeUseCaseIncluded(), Is.SameAs(targetUseCase));

            // ReferenceSubsetting whose ReferencedFeature is NOT a UseCaseUsage → null (invalid-model branch, per validateIncludeUseCaseUsageReference).
            var subjectWithNonUseCaseTarget = new IncludeUseCaseUsage();
            var nonUseCaseTarget = new ActionUsage();
            subjectWithNonUseCaseTarget.AssignOwnership(new ReferenceSubsetting { ReferencedFeature = nonUseCaseTarget });

            Assert.That(subjectWithNonUseCaseTarget.ComputeUseCaseIncluded(), Is.Null);
        }
    }
}
