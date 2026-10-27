// ------------------------------------------------------------------------------------------------
// <copyright file="UnresolvedLibraryTypeException.cs" company="Starion Group S.A.">
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
    /// Thrown when a semantic constraint targets a model-library Type that the
    /// <see cref="ILibraryTypeIndex" /> cannot resolve.
    /// </summary>
    /// <remarks>
    /// The usual cause is that the standard libraries were never loaded, so the index is empty or partial.
    /// Failing loudly keeps that configuration error distinct from a model that genuinely requires no
    /// implied Relationship.
    /// </remarks>
    public class UnresolvedLibraryTypeException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UnresolvedLibraryTypeException" /> class.
        /// </summary>
        public UnresolvedLibraryTypeException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UnresolvedLibraryTypeException" /> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public UnresolvedLibraryTypeException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UnresolvedLibraryTypeException" /> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        /// <param name="innerException">The exception that caused this exception.</param>
        public UnresolvedLibraryTypeException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UnresolvedLibraryTypeException" /> class for a
        /// qualified name.
        /// </summary>
        /// <param name="qualifiedName">The qualified name that failed to resolve.</param>
        /// <param name="constraintName">The constraint requiring the Type.</param>
        public UnresolvedLibraryTypeException(string qualifiedName, string constraintName)
            : base($"The library Type '{qualifiedName}' required by the semantic constraint '{constraintName}' could not be resolved. Ensure the model libraries are loaded and indexed.")
        {
            this.QualifiedName = qualifiedName;
            this.ConstraintName = constraintName;
        }

        /// <summary>
        /// Gets the qualified name that failed to resolve.
        /// </summary>
        public string QualifiedName { get; }

        /// <summary>
        /// Gets the name of the constraint requiring the Type.
        /// </summary>
        public string ConstraintName { get; }
    }
}
