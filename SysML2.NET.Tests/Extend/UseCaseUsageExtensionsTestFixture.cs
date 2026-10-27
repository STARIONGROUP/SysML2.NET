// ------------------------------------------------------------------------------------------------
// <copyright file="UseCaseUsageExtensionsTestFixture.cs" company="Starion Group S.A.">
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
    using SysML2.NET.Core.POCO.Systems.UseCases;
    using SysML2.NET.Exceptions;
    using SysML2.NET.Extensions;

    [TestFixture]
    public class UseCaseUsageExtensionsTestFixture
    {
        [Test]
        public void VerifyComputeIncludedUseCase()
        {
            Assert.That(() => ((IUseCaseUsage)null).ComputeIncludedUseCase(), Throws.TypeOf<ArgumentNullException>());

            // Empty: no nested usages at all → empty list.
            var emptySubject = new UseCaseUsage();

            Assert.That(emptySubject.ComputeIncludedUseCase(), Is.Empty);

            // A plain nested UseCaseUsage (not an IncludeUseCaseUsage) is filtered out by the selectByKind(IncludeUseCaseUsage).
            var subjectWithPlainNestedOnly = new UseCaseUsage();
            var plainNested = new UseCaseUsage();
            subjectWithPlainNestedOnly.AssignOwnership(new FeatureMembership(), plainNested);

            Assert.That(subjectWithPlainNestedOnly.ComputeIncludedUseCase(), Is.Empty);

            // Populated: nested IncludeUseCaseUsage with no ReferenceSubsetting (contributes itself, self-referencing)
            // + nested IncludeUseCaseUsage referencing a target UseCaseUsage (contributes the target), in nesting order.
            // Navigates nestedUseCase (Usage-side), NOT ownedUseCase (which does not exist on IUseCaseUsage).
            var subject = new UseCaseUsage();
            var selfInclude = new IncludeUseCaseUsage();
            var referencedTarget = new UseCaseUsage();
            var referencingInclude = new IncludeUseCaseUsage();
            referencingInclude.AssignOwnership(new ReferenceSubsetting { ReferencedFeature = referencedTarget });

            subject.AssignOwnership(new FeatureMembership(), selfInclude);
            subject.AssignOwnership(new FeatureMembership(), referencingInclude);

            Assert.That(subject.ComputeIncludedUseCase(), Is.EqualTo(new IUseCaseUsage[] { selfInclude, referencedTarget }));
        }

        [Test]
        public void VerifyComputeUseCaseDefinition()
        {
            Assert.That(() => ((IUseCaseUsage)null).ComputeUseCaseDefinition(), Throws.TypeOf<ArgumentNullException>());

            // Empty case: no FeatureTyping whose Type is a UseCaseDefinition → null.
            var subjectNoTyping = new UseCaseUsage();

            Assert.That(subjectNoTyping.ComputeUseCaseDefinition(), Is.Null);

            // Populated case: one FeatureTyping whose Type is a UseCaseDefinition → returned.
            var subjectOneTyping = new UseCaseUsage();
            var useCaseDefinition = new UseCaseDefinition();
            subjectOneTyping.AssignOwnership(new FeatureTyping { Type = useCaseDefinition });

            Assert.That(subjectOneTyping.ComputeUseCaseDefinition(), Is.SameAs(useCaseDefinition));

            // [0..1] upper-bound violation: two typings both resolving to UseCaseDefinition → MultiplicityViolationException.
            var subjectTwoTypings = new UseCaseUsage();
            var firstUseCaseDefinition = new UseCaseDefinition();
            var secondUseCaseDefinition = new UseCaseDefinition();
            subjectTwoTypings.AssignOwnership(new FeatureTyping { Type = firstUseCaseDefinition });
            subjectTwoTypings.AssignOwnership(new FeatureTyping { Type = secondUseCaseDefinition });

            Assert.That(() => subjectTwoTypings.ComputeUseCaseDefinition(), Throws.TypeOf<MultiplicityViolationException>());
        }
    }
}
