// ------------------------------------------------------------------------------------------------
// <copyright file="MergeConflictException.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SySML2.NET.REST
{
    using System.Collections.Generic;
    using System.Net;

    using SysML2.NET.PSM.DTO;

    /// <summary>
    /// The exception thrown when a merge is rejected because the source and target hold conflicting data
    /// </summary>
    public class MergeConflictException : PsmApiException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MergeConflictException"/> class
        /// </summary>
        /// <param name="conflicts">
        /// The <see cref="DataIdentity"/> of each conflicting data item
        /// </param>
        public MergeConflictException(IReadOnlyList<DataIdentity> conflicts)
            : base(HttpStatusCode.Conflict, null)
        {
            this.Conflicts = conflicts;
        }

        /// <summary>
        /// Gets the <see cref="DataIdentity"/> of each conflicting data item
        /// </summary>
        public IReadOnlyList<DataIdentity> Conflicts { get; }
    }
}
