// -------------------------------------------------------------------------------------------------
// <copyright file="CommitHistoryServiceTestFixture.cs" company="RHEA System S.A.">
// 
//   Copyright 2022-2023 RHEA System S.A.
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

namespace SysML2.NET.Viewer.Tests.Services.CommitHistory
{
    using System;
    using System.Collections.Generic;
    
    using NUnit.Framework;

    using SysML2.NET.PIM.DTO;
    
    using SysML2.NET.Viewer.Services.CommitHistory;

    /// <summary>
    /// Suite of tests for the <see cref="CommitHistoryService"/>
    /// </summary>
    [TestFixture]
    public class CommitHistoryServiceTestFixture
    {
        private CommitHistoryService commitHistoryService;

        private Branch branch;

        private List<Commit> commits;

        [SetUp]
        public void SetUp()
        {
            this.commitHistoryService = new CommitHistoryService();

            this.CreateTestData();
        }

        private void CreateTestData()
        {
            var projectId = Guid.NewGuid();

            var head = new Commit
            {
                Id = Guid.NewGuid(),
                OwningProject = projectId,
                Description = "5"
            };


            var commit_1 = new Commit
            {
                Id = Guid.NewGuid(),
                OwningProject = projectId,
                Description = "1"
            };

            var commit_2 = new Commit
            {
                Id = Guid.NewGuid(),
                OwningProject = projectId,
                Description = "2"
            };

            var commit_3 = new Commit
            {
                Id = Guid.NewGuid(),
                OwningProject = projectId,
                Description = "3"
            };

            var commit_4 = new Commit
            {
                Id = Guid.NewGuid(),
                OwningProject = projectId,
                Description = "4"
            };

            this.branch = new Branch
            {
                Id = Guid.NewGuid(),
                Head = head.Id,
            };

            head.PreviousCommit = commit_4.Id;
            commit_4.PreviousCommit = commit_3.Id;
            commit_3.PreviousCommit = commit_2.Id;
            commit_2.PreviousCommit = commit_1.Id;

            this.commits = new List<Commit>();
            this.commits.Add(commit_2);
            this.commits.Add(commit_4);
            this.commits.Add(commit_1);
            this.commits.Add(head);
            this.commits.Add(commit_3);
            this.commits.Add(new Commit() {Id = Guid.NewGuid()});
            this.commits.Add(new Commit() { Id = Guid.NewGuid() });
        }

        [Test]
        public void Verify_that_expected_commit_history_is_returned()
        {
            var history = this.commitHistoryService.QueryCommitHistory(this.branch, this.commits);

            Assert.That(history.Length, Is.EqualTo(5));

            Assert.That(history[0].Description, Is.EqualTo("5"));
            Assert.That(history[1].Description, Is.EqualTo("4"));
            Assert.That(history[2].Description, Is.EqualTo("3"));
            Assert.That(history[3].Description, Is.EqualTo("2"));
            Assert.That(history[4].Description, Is.EqualTo("1"));
        }
    }
}
