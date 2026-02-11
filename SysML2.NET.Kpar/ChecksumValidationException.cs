namespace SysML2.NET.Kpar
{
    using System;
    using System.Collections.Generic;

    using SysML2.NET.ModelInterchange;
    
    /// <summary>
    /// Represents an exception that is thrown when checksum validation of a
    /// <c>.kpar</c> archive fails.
    /// </summary>
    /// <remarks>
    /// This exception is raised when checksum validation is enabled via
    /// <see cref="ReadOptions.ValidateChecksums"/> and one or more model
    /// files contained in the archive do not match the checksum values
    /// declared in <c>.meta.json</c>.
    ///
    /// The default behavior (when <see cref="ChecksumFailureBehavior"/> is configured
    /// to throw) is to abort archive opening and throw this exception
    /// immediately after validation.
    ///
    /// The exception exposes detailed mismatch information through
    /// the <see cref="Mismatches"/> property.
    /// </remarks>
    public sealed class ChecksumValidationException : InvalidOperationException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ChecksumValidationException"/> class.
        /// </summary>
        /// <param name="mismatches">
        /// The collection of checksum mismatches detected during validation.
        /// </param>
        /// <remarks>
        /// The exception message summarizes the number of failed archive entries.
        /// Detailed information about each mismatch is available via
        /// the <see cref="Mismatches"/> property.
        /// </remarks>
        public ChecksumValidationException(IReadOnlyList<ChecksumMismatch> mismatches)
            : base($"Checksum validation failed for {mismatches?.Count ?? 0} archive entr{(mismatches?.Count == 1 ? "y" : "ies")}.")
        {
            this.Mismatches = mismatches ?? Array.Empty<ChecksumMismatch>();
        }

        /// <summary>
        /// Gets the collection of checksum mismatches detected during archive validation.
        /// </summary>
        /// <value>
        /// A read-only list of <see cref="ChecksumMismatch"/> instances describing
        /// each model entry whose computed checksum differed from the value declared
        /// in <c>.meta.json</c>.
        /// </value>
        public IReadOnlyList<ChecksumMismatch> Mismatches { get; }
    }
}
