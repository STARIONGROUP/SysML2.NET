// ------------------------------------------------------------------------------------------------
// <copyright file="MergeConflictException.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.REST.Exceptions
{
    using System;
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
        public MergeConflictException() : this(null, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MergeConflictException"/> class with a specified error message
        /// </summary>
        /// <param name="message">
        /// The message that describes the error
        /// </param>
        public MergeConflictException(string message) : this(message, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MergeConflictException"/> class with a specified error message and the exception that caused it
        /// </summary>
        /// <param name="message">
        /// The message that describes the error
        /// </param>
        /// <param name="innerException">
        /// The exception that is the cause of this exception, or <c>null</c> when none is specified
        /// </param>
        public MergeConflictException(string message, Exception innerException) : base(HttpStatusCode.Conflict, message, innerException)
        {
            this.Conflicts = [];
        }

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
