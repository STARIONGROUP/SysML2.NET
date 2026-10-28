// ------------------------------------------------------------------------------------------------
// <copyright file="EventOccurrenceUsageExtensionsTestFixture.cs" company="Starion Group S.A.">
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
    using SysML2.NET.Core.POCO.Systems.Attributes;
    using SysML2.NET.Core.POCO.Systems.Occurrences;
    using SysML2.NET.Extensions;

    [TestFixture]
    public class EventOccurrenceUsageExtensionsTestFixture
    {
        [Test]
        public void VerifyComputeEventOccurrence()
        {
            Assert.That(() => ((IEventOccurrenceUsage)null).ComputeEventOccurrence(), Throws.TypeOf<ArgumentNullException>());

            // No ownedReferenceSubsetting → referencedFeatureTarget() is null → returns the subject itself.
            var subjectNoSubsetting = new EventOccurrenceUsage();

            Assert.That(subjectNoSubsetting.ComputeEventOccurrence(), Is.SameAs(subjectNoSubsetting));

            // ReferenceSubsetting whose ReferencedFeature IS an OccurrenceUsage → returns that occurrence usage.
            var subjectWithOccurrenceTarget = new EventOccurrenceUsage();
            var targetOccurrence = new OccurrenceUsage();
            subjectWithOccurrenceTarget.AssignOwnership(new ReferenceSubsetting { ReferencedFeature = targetOccurrence });

            Assert.That(subjectWithOccurrenceTarget.ComputeEventOccurrence(), Is.SameAs(targetOccurrence));

            // ReferenceSubsetting whose ReferencedFeature is NOT an OccurrenceUsage → null (invalid-model branch, per validateEventOccurrenceUsageReference).
            var subjectWithNonOccurrenceTarget = new EventOccurrenceUsage();
            var nonOccurrenceTarget = new AttributeUsage();
            subjectWithNonOccurrenceTarget.AssignOwnership(new ReferenceSubsetting { ReferencedFeature = nonOccurrenceTarget });

            Assert.That(subjectWithNonOccurrenceTarget.ComputeEventOccurrence(), Is.Null);
        }

        [Test]
        public void VerifyComputeIsReference()
        {
            Assert.That(() => ((IEventOccurrenceUsage)null).ComputeIsReference(), Throws.TypeOf<ArgumentNullException>());

            // isReference is always true for an EventOccurrenceUsage (SysML 2.0 spec, Clause 8.3.9.2).
            var subject = new EventOccurrenceUsage();

            Assert.That(subject.ComputeIsReference(), Is.True);
        }
    }
}
