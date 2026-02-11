namespace SysML2.NET.ModelInterchange
{
    /// <summary>
    /// Represents a checksum mismatch detected during validation of a <c>.kpar</c> archive.
    /// </summary>
    public sealed class ChecksumMismatch
    {
        /// <summary>
        /// Gets the metadata index key associated with the model entry.
        /// </summary>
        /// <remarks>
        /// This corresponds to a key in the <c>index</c> object of <c>.meta.json</c>
        /// (for example <c>"Base"</c>).
        /// </remarks>
        public string IndexKey { get; set; }

        /// <summary>
        /// Gets the archive-relative path of the model file.
        /// </summary>
        /// <remarks>
        /// This is the path within the ZIP container (for example <c>"Base.kerml"</c>),
        /// normalized to use forward slashes.
        /// </remarks>
        public string Path { get; set; }

        /// <summary>
        /// Gets the checksum algorithm declared in <c>.meta.json</c>.
        /// </summary>
        /// <remarks>
        /// Typical values include <c>SHA256</c>, <c>SHA3-256</c>, or other
        /// algorithms supported by the KerML specification.
        /// </remarks>
        public ChecksumKind Algorithm { get; set; }

        /// <summary>
        /// Gets the checksum value declared in <c>.meta.json</c>.
        /// </summary>
        /// <remarks>
        /// This value represents the expected integrity hash for the model file.
        /// </remarks>
        public string Expected { get; set; }

        /// <summary>
        /// Gets the checksum value computed from the actual file contents in the archive.
        /// </summary>
        /// <remarks>
        /// If this value differs from <see cref="Expected"/>, the archive integrity
        /// has been compromised or the archive contents have changed since creation.
        /// </remarks>
        public string Actual { get; set; }
    }
}
