// -------------------------------------------------------------------------------------------------
// <copyright file="MergeResultMapperTestFixture.cs" company="Starion Group S.A.">
//
//   Copyright 2022-2025 Starion Group S.A.
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
                Assert.That(conflicted.MergeCommit, Is.EqualTo(Guid.Empty));
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
                Assert.That(MergeResultMapper.ToPim(MergeResultMapper.ToPsm(conflicted)).MergeCommit, Is.EqualTo(Guid.Empty));
            }
        }
    }
}
