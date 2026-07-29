// -------------------------------------------------------------------------------------------------
// <copyright file="UseCaseDefinitionExtensionsTestFixture.cs" company="Starion Group S.A.">
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
    using SysML2.NET.Core.POCO.Core.Types;
    using SysML2.NET.Core.POCO.Systems.UseCases;
    using SysML2.NET.Extensions;

    [TestFixture]
    public class UseCaseDefinitionExtensionsTestFixture
    {
        [Test]
        public void VerifyComputeIncludedUseCase()
        {
            Assert.That(() => ((IUseCaseDefinition)null).ComputeIncludedUseCase(), Throws.TypeOf<ArgumentNullException>());

            // Empty: no owned usages at all → empty list.
            var emptySubject = new UseCaseDefinition();

            Assert.That(emptySubject.ComputeIncludedUseCase(), Is.Empty);

            // A plain UseCaseUsage (not an IncludeUseCaseUsage) is filtered out by the selectByKind(IncludeUseCaseUsage).
            var subjectWithPlainUseCaseOnly = new UseCaseDefinition();
            var plainUseCase = new UseCaseUsage();
            subjectWithPlainUseCaseOnly.AssignOwnership(new FeatureMembership(), plainUseCase);

            Assert.That(subjectWithPlainUseCaseOnly.ComputeIncludedUseCase(), Is.Empty);

            // Populated: plain UseCaseUsage (excluded) + IncludeUseCaseUsage with no ReferenceSubsetting (contributes itself)
            // + IncludeUseCaseUsage referencing a target UseCaseUsage (contributes the target), in ownership order.
            var subject = new UseCaseDefinition();
            var otherPlainUseCase = new UseCaseUsage();
            var selfInclude = new IncludeUseCaseUsage();
            var referencedTarget = new UseCaseUsage();
            var referencingInclude = new IncludeUseCaseUsage();
            referencingInclude.AssignOwnership(new ReferenceSubsetting { ReferencedFeature = referencedTarget });

            subject.AssignOwnership(new FeatureMembership(), otherPlainUseCase);
            subject.AssignOwnership(new FeatureMembership(), selfInclude);
            subject.AssignOwnership(new FeatureMembership(), referencingInclude);

            Assert.That(subject.ComputeIncludedUseCase(), Is.EqualTo(new IUseCaseUsage[] { selfInclude, referencedTarget }));
        }
    }
}
