// -------------------------------------------------------------------------------------------------
// <copyright file="IncludeUseCaseUsageExtensionsTestFixture.cs" company="Starion Group S.A.">
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
