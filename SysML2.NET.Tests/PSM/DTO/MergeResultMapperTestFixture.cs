// ------------------------------------------------------------------------------------------------
// <copyright file="MergeResultMapperTestFixture.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.Tests.PSM.DTO
{
    using System;

    using NUnit.Framework;

    using SysML2.NET.Exceptions;
    using SysML2.NET.PIM.DTO;
    using SysML2.NET.PSM.DTO;

    using Commit = SysML2.NET.PSM.DTO.Commit;
    using DataIdentity = SysML2.NET.PSM.DTO.DataIdentity;

    [TestFixture]
    public class MergeResultMapperTestFixture
    {
        [Test]
        public void VerifyToPsm()
        {
            using (Assert.EnterMultipleScope())
            {
                Assert.That(MergeResultMapper.ToPsm(null), Is.Null);
                Assert.That(() => MergeResultMapper.ToPsm(new MergeResult()), Throws.TypeOf<IncompleteModelException>());
            }

            var mergeCommit = Guid.NewGuid();
            var merged = MergeResultMapper.ToPsm(new PIM.DTO.MergeResult { MergeCommit = mergeCommit });

            using (Assert.EnterMultipleScope())
            {
                Assert.That(merged.Kind, Is.EqualTo(MergeOutcomeKind.Merged));
                Assert.That(merged.IsMerged, Is.True);
                Assert.That(merged.TryGetMergeCommit(out var commit), Is.True);
                Assert.That(merged.TryGetConflict(out var noConflict), Is.False);
                Assert.That(commit.Id, Is.EqualTo(mergeCommit));
                Assert.That(noConflict, Is.Empty);
            }

            var firstIdentity = Guid.NewGuid();
            var secondIdentity = Guid.NewGuid();
            var conflicted = MergeResultMapper.ToPsm(new PIM.DTO.MergeResult { Conflict = [firstIdentity, secondIdentity] });

            using (Assert.EnterMultipleScope())
            {
                Assert.That(conflicted.Kind, Is.EqualTo(MergeOutcomeKind.Conflicted));
                Assert.That(conflicted.IsMerged, Is.False);
                Assert.That(conflicted.TryGetMergeCommit(out var absentCommit), Is.False);
                Assert.That(absentCommit, Is.Null);
                Assert.That(conflicted.TryGetConflict(out var conflict), Is.True);
                Assert.That(conflict, Has.Count.EqualTo(2));
                Assert.That(conflict[0].Id, Is.EqualTo(firstIdentity));
                Assert.That(conflict[1].Id, Is.EqualTo(secondIdentity));
            }

            var both = MergeResultMapper.ToPsm(new PIM.DTO.MergeResult { MergeCommit = mergeCommit, Conflict = [firstIdentity] });

            Assert.That(both.Kind, Is.EqualTo(MergeOutcomeKind.Conflicted));
        }

        [Test]
        public void VerifyToPim()
        {
            Assert.That(MergeResultMapper.ToPim(null), Is.Null);

            var mergeCommit = Guid.NewGuid();
            var merged = MergeResultMapper.ToPim(MergeOutcome.Merged(new Commit { Id = mergeCommit }));

            using (Assert.EnterMultipleScope())
            {
                Assert.That(merged.MergeCommit, Is.EqualTo(mergeCommit));
                Assert.That(merged.Conflict, Has.Count.EqualTo(0));
            }

            var identity = Guid.NewGuid();
            var conflicted = MergeResultMapper.ToPim(MergeOutcome.Conflicted([new DataIdentity { Id = identity }]));

            using (Assert.EnterMultipleScope())
            {
                Assert.That(conflicted.MergeCommit, Is.Null);
                Assert.That(conflicted.Conflict, Is.EqualTo([identity]));
            }
        }

        [Test]
        public void VerifyRoundTrip()
        {
            var merged = new PIM.DTO.MergeResult { MergeCommit = Guid.NewGuid() };
            var conflicted = new PIM.DTO.MergeResult { Conflict = [Guid.NewGuid(), Guid.NewGuid()] };

            using (Assert.EnterMultipleScope())
            {
                Assert.That(MergeResultMapper.ToPim(MergeResultMapper.ToPsm(merged)).MergeCommit, Is.EqualTo(merged.MergeCommit));
                Assert.That(MergeResultMapper.ToPim(MergeResultMapper.ToPsm(merged)).Conflict, Has.Count.EqualTo(0));
                Assert.That(MergeResultMapper.ToPim(MergeResultMapper.ToPsm(conflicted)).Conflict, Is.EqualTo(conflicted.Conflict));
                Assert.That(MergeResultMapper.ToPim(MergeResultMapper.ToPsm(conflicted)).MergeCommit, Is.Null);
            }
        }
    }
}
