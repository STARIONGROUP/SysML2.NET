// ------------------------------------------------------------------------------------------------
// <copyright file="IncompleteModelException.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.Exceptions
{
    using System;

    /// <summary>
    /// The <see cref="IncompleteModelException"/> provide custom exception that should be used when a SysML 2 is incomplete and is not compliant with defined multiplicity
    /// </summary>
    public class IncompleteModelException: Exception
    {
        /// <summary>Initializes a new instance of the <see cref="IncompleteModelException" /> class.</summary>
        public IncompleteModelException()
        {
        }

        /// <summary>Initializes a new instance of the <see cref="IncompleteModelException" /> class with a specified error message.</summary>
        /// <param name="message">The message that describes the error.</param>
        public IncompleteModelException(string message) : base(message)
        {
        }

        /// <summary>Initializes a new instance of the <see cref="IncompleteModelException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference (<see langword="Nothing" /> in Visual Basic) if no inner exception is specified.</param>
        public IncompleteModelException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
