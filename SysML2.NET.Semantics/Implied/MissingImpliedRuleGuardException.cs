// -------------------------------------------------------------------------------------------------
// <copyright file="MissingImpliedRuleGuardException.cs" company="Starion Group S.A.">
//
//    Copyright (C) 2022-2026 Starion Group S.A.
//
//    Licensed under the Apache License, Version 2.0 (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
//
//         http://www.apache.org/licenses/LICENSE-2.0
//
//    Unless required by applicable law or agreed to in writing, software
//    distributed under the License is distributed on an "AS IS" BASIS,
//    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//    See the License for the specific language governing permissions and
//    limitations under the License.
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.Semantics.Implied
{
    using System;

    /// <summary>
    /// Thrown when a semantic constraint is flagged as conditional but no <see cref="IImpliedRuleGuard" />
    /// is registered to decide it.
    /// </summary>
    /// <remarks>
    /// This is deliberately fatal rather than a silent yes: applying a conditional rule unconditionally
    /// injects Specializations the model does not require, which corrupts every inheritance result computed
    /// from it.
    /// </remarks>
    public class MissingImpliedRuleGuardException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MissingImpliedRuleGuardException" /> class.
        /// </summary>
        public MissingImpliedRuleGuardException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MissingImpliedRuleGuardException" /> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public MissingImpliedRuleGuardException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MissingImpliedRuleGuardException" /> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        /// <param name="innerException">The exception that caused this exception.</param>
        public MissingImpliedRuleGuardException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MissingImpliedRuleGuardException" /> class for a
        /// named constraint.
        /// </summary>
        /// <param name="constraintName">The constraint whose guard is missing.</param>
        /// <param name="declaringMetaclassName">The metaclass declaring the constraint.</param>
        public MissingImpliedRuleGuardException(string constraintName, string declaringMetaclassName)
            : base($"The semantic constraint '{constraintName}' declared by '{declaringMetaclassName}' is conditional, but no IImpliedRuleGuard is registered for it.")
        {
            this.ConstraintName = constraintName;
            this.DeclaringMetaclassName = declaringMetaclassName;
        }

        /// <summary>
        /// Gets the name of the constraint whose guard is missing.
        /// </summary>
        public string ConstraintName { get; }

        /// <summary>
        /// Gets the name of the metaclass declaring the constraint.
        /// </summary>
        public string DeclaringMetaclassName { get; }
    }
}
