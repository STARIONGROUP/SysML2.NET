// -------------------------------------------------------------------------------------------------
// <copyright file="MultiplicityViolationException.cs" company="Starion Group S.A.">
//
//   Copyright 2022-2026 Starion Group S.A.
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

namespace SysML2.NET.Exceptions
{
    using System;

    /// <summary>
    /// The <see cref="MultiplicityViolationException"/> is thrown when a SysML 2 model carries
    /// more elements than the upper bound of a derived property allows (e.g. 2+ matches against
    /// a <c>[0..1]</c> or <c>[1..1]</c> derived reference).
    /// </summary>
    /// <remarks>
    /// Contrast with <see cref="IncompleteModelException"/>, which signals the opposite — the
    /// model is missing a required element against the lower bound of a property (e.g. 0
    /// matches against a <c>[1..1]</c> derived reference).
    /// </remarks>
    public class MultiplicityViolationException : Exception
    {
        /// <summary>Initializes a new instance of the <see cref="MultiplicityViolationException" /> class.</summary>
        public MultiplicityViolationException()
        {
        }

        /// <summary>Initializes a new instance of the <see cref="MultiplicityViolationException" /> class with a specified error message.</summary>
        /// <param name="message">The message that describes the error.</param>
        public MultiplicityViolationException(string message) : base(message)
        {
        }

        /// <summary>Initializes a new instance of the <see cref="MultiplicityViolationException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference (<see langword="Nothing" /> in Visual Basic) if no inner exception is specified.</param>
        public MultiplicityViolationException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
