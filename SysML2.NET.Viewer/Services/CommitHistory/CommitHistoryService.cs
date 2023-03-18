// -------------------------------------------------------------------------------------------------
// <copyright file="CommitHistoryService.cs" company="RHEA System S.A.">
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

namespace SysML2.NET.Viewer.Services.CommitHistory
{
    using System.Linq;
    using System.Collections.Generic;
    
    using SysML2.NET.PIM.DTO;

    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Logging.Abstractions;

	/// <summary>
	/// The purpose of the <see cref="CommitHistoryService"/> is to compute the
	/// <see cref="Commit"/> history of a branch
	/// </summary>
	public class CommitHistoryService : ICommitHistoryService
    {
	    /// <summary>
	    /// The <see cref="ILogger"/> used to log
	    /// </summary>
	    private readonly ILogger<CommitHistoryService> logger;

		/// <summary>
		/// Initializes a new instance of the <see cref="CommitHistoryService" /> class.
		/// </summary>
		/// <param name="loggerFactory">
		/// The (injected) <see cref="ILoggerFactory"/> used to setup logging
		/// </param>
		public CommitHistoryService(ILoggerFactory loggerFactory = null)
	    {
		    this.logger = loggerFactory == null ? NullLogger<CommitHistoryService>.Instance : loggerFactory.CreateLogger<CommitHistoryService>();
		}

	    /// <summary>
		/// Queries the <see cref="Commit"/>s that belong to a <see cref="Branch"/> and
		/// returns them in array ordered using the commit history
		/// </summary>
		/// <param name="branch">
		/// The owning <see cref="Branch"/> of the <see cref="Commit"/>s
		/// </param>
		/// <param name="commits">
		/// A list of <see cref="Commit"/>s
		/// </param>
		/// <returns>
		/// An array of <see cref="Commit"/> where the <see cref="Commit"/>s are returned in historical order
		/// using the <see cref="Commit.PreviousCommit"/> property. The latest commit (Head) is returned first.
		/// </returns>
		public Commit[] QueryCommitHistory(Branch branch, IEnumerable<Commit> commits)
        {
            this.logger.LogDebug("Querying the Commit History");

            var result = new List<Commit>();

            var head = commits.SingleOrDefault(x => x.Id == branch.Head);

            if (head == null)
            {
				this.logger.LogWarning("The head commit is missing from the Branch:{branch}", branch.Id);
            }

            result.Add(head);

            var commitHistory = this.RecursivelyComputeCommitHistory(head, commits);

            result.AddRange(commitHistory);

            return result.ToArray();
        }

        /// <summary>
        /// Recursively iterates through the <paramref name="commits"/> to compute the historical
        /// order using the <see cref="Commit.PreviousCommit"/> property
        /// </summary>
        /// <param name="currentCommit">
        /// The current <see cref="Commit"/> for which the subsequent <see cref="Commit"/>s are to be computed
        /// </param>
        /// <param name="commits">
        /// The list of <see cref="Commit"/>s on the basis of which history is to be computed
        /// </param>
        /// <returns>
        /// an <see cref="IEnumerable{Commit}"/> s
        /// </returns>
        private IEnumerable<Commit> RecursivelyComputeCommitHistory(Commit currentCommit, IEnumerable<Commit> commits)
        {
            var result = new List<Commit>();

            if (currentCommit.PreviousCommit == null)
            {
                return result;
            }

            var commitsRange = commits.ToList();

            foreach (var commit in commits)
            {
                if (commit.Id == currentCommit.PreviousCommit)
                {
                    result.Add(commit);
                    commitsRange.Remove(commit);

                    result.AddRange(this.RecursivelyComputeCommitHistory(commit, commitsRange));
                }
            }

            return result;
        }
    }
}
