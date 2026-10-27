// ------------------------------------------------------------------------------------------------
// <copyright file="MissingImpliedRuleGuardException.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
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
