// -------------------------------------------------------------------------------------------------
// <copyright file="MergeOutcomeTestFixture.cs" company="Starion Group S.A.">
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

    using SysML2.NET.PSM.DTO;

    [TestFixture]
    public class MergeOutcomeTestFixture
    {
        private static readonly DataIdentity[] NoIdentities = [];

        [Test]
        public void VerifyMerged()
        {
            Assert.That(() => MergeOutcome.Merged(null), Throws.TypeOf<ArgumentNullException>());

            var commit = new Commit { Id = Guid.NewGuid() };
            var outcome = MergeOutcome.Merged(commit);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(outcome.Kind, Is.EqualTo(MergeOutcomeKind.Merged));
                Assert.That(outcome.IsMerged, Is.True);
                Assert.That(outcome.TryGetMergeCommit(out var held), Is.True);
                Assert.That(held, Is.SameAs(commit));
                Assert.That(outcome.TryGetConflict(out var conflict), Is.False);
                Assert.That(conflict, Is.Empty);
            }
        }

        [Test]
        public void VerifyConflicted()
        {
            using (Assert.EnterMultipleScope())
            {
                Assert.That(() => MergeOutcome.Conflicted(null), Throws.TypeOf<ArgumentNullException>());
                Assert.That(() => MergeOutcome.Conflicted(NoIdentities), Throws.TypeOf<ArgumentException>());
            }

            var identity = new DataIdentity { Id = Guid.NewGuid() };
            var outcome = MergeOutcome.Conflicted([identity]);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(outcome.Kind, Is.EqualTo(MergeOutcomeKind.Conflicted));
                Assert.That(outcome.IsMerged, Is.False);
                Assert.That(outcome.TryGetConflict(out var held), Is.True);
                Assert.That(held, Has.Count.EqualTo(1));
                Assert.That(held[0], Is.SameAs(identity));
                Assert.That(outcome.TryGetMergeCommit(out var commit), Is.False);
                Assert.That(commit, Is.Null);
            }
        }

        [Test]
        public void VerifyMatch()
        {
            var merged = MergeOutcome.Merged(new Commit { Id = Guid.NewGuid() });
            var conflicted = MergeOutcome.Conflicted([new DataIdentity { Id = Guid.NewGuid() }]);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(() => merged.Match<int>(null, conflict => conflict.Count), Throws.TypeOf<ArgumentNullException>());
                Assert.That(() => merged.Match(commit => 1, null), Throws.TypeOf<ArgumentNullException>());
                Assert.That(merged.Match(commit => "merged", conflict => "conflicted"), Is.EqualTo("merged"));
                Assert.That(conflicted.Match(commit => "merged", conflict => "conflicted"), Is.EqualTo("conflicted"));
                Assert.That(conflicted.Match(commit => 0, conflict => conflict.Count), Is.EqualTo(1));
            }
        }
    }
}
