// -------------------------------------------------------------------------------------------------
// <copyright file="MergeResultMapper.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.PSM.DTO
{
    using System;
    using System.Linq;

    using SysML2.NET.Exceptions;

    /// <summary>
    /// Maps the outcome of a merge between the Platform Independent Model and the REST/HTTP PSM
    /// </summary>
    /// <remarks>
    /// Systems Modeling API and Services, Table 9 maps <c>MergeResult.mergeCommit</c> onto <c>Commit</c> and
    /// <c>MergeResult.conflict</c> onto <c>DataIdentity [0..*]</c>, so the one record spans two REST/HTTP PSM models
    /// and cannot declare a counterpart through <see cref="IPimMappable{TRecord}"/>.
    /// </remarks>
    public static class MergeResultMapper
    {
        /// <summary>
        /// Maps a Platform Independent Model merge result onto its REST/HTTP PSM outcome
        /// </summary>
        /// <param name="record">The record to map.</param>
        /// <returns>The outcome, or <c>null</c> when the record is <c>null</c>.</returns>
        /// <exception cref="IncompleteModelException">Thrown when the record holds neither alternative.</exception>
        /// <remarks>
        /// A record holding conflicts is read as unsuccessful even when it also names a commit, because Clause 7.2.3
        /// admits conflicts only for an unsuccessful merge.
        /// </remarks>
        public static MergeOutcome ToPsm(PIM.DTO.MergeResult record)
        {
            if (record == null)
            {
                return null;
            }

            if (record.Conflict != null && record.Conflict.Count > 0)
            {
                return MergeOutcome.Conflicted(record.Conflict.Select(identity => new DataIdentity { Id = identity }));
            }

            return record.MergeCommit == null
                ? throw new IncompleteModelException("A MergeResult holds either the merge commit or the conflicting data identities.")
                : MergeOutcome.Merged(new Commit { Id = record.MergeCommit.Value });
        }

        /// <summary>
        /// Maps a REST/HTTP PSM merge outcome onto its Platform Independent Model record
        /// </summary>
        /// <param name="outcome">The outcome to map.</param>
        /// <returns>The record, or <c>null</c> when the outcome is <c>null</c>.</returns>
        public static PIM.DTO.MergeResult ToPim(MergeOutcome outcome)
        {
            return outcome?.Match(
                mergeCommit => new PIM.DTO.MergeResult { MergeCommit = mergeCommit.Id },
                conflict => new PIM.DTO.MergeResult { Conflict = [.. conflict.Select(identity => identity.Id)] });
        }
    }
}
