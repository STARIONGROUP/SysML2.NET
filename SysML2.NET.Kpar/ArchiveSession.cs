// -------------------------------------------------------------------------------------------------
// <copyright file="ArchiveSession.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.Kpar
{
    using System;
    using System.IO;
    using System.IO.Compression;
    using System.Threading.Tasks;
    using Extensions.ModelInterchange;
    using SysML2.NET.Extensions.Utilities;
    using SysML2.NET.ModelInterchange;

    /// <summary>
    /// Represents an open session on a <c>.kpar</c> archive that keeps the underlying ZIP container
    /// and source stream alive so that model content streams can be opened on demand.
    /// </summary>
    /// <remarks>
    /// The caller must dispose the session to release the underlying <see cref="ZipArchive"/> and
    /// its backing <see cref="Stream"/>. Any streams opened from the session become invalid once the
    /// session is disposed.
    /// </remarks>
    public sealed class ArchiveSession : IDisposable, IAsyncDisposable
    {
        /// <summary>
        /// The underlying source stream that contains the <c>.kpar</c> ZIP archive.
        /// </summary>
        /// <remarks>
        /// This stream is kept alive for the lifetime of the <see cref="ArchiveSession"/>
        /// to allow on-demand access to ZIP entries. It is disposed when the session
        /// is disposed, unless ownership is externally controlled.
        /// </remarks>
        private readonly Stream source;
        
        /// <summary>
        /// The open <see cref="ZipArchive"/> representing the <c>.kpar</c> container.
        /// </summary>
        /// <remarks>
        /// The ZIP archive remains open for the lifetime of the session to enable
        /// deferred opening of model entry streams. Disposing the session disposes
        /// this archive instance.
        /// </remarks>
        private readonly ZipArchive zip;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="ArchiveSession"/> class.
        /// </summary>
        /// <param name="source">The backing stream that contains the <c>.kpar</c> ZIP payload.</param>
        /// <param name="zip">The open <see cref="ZipArchive"/> used to access entries.</param>
        /// <param name="archive">The parsed logical archive representation.</param>
        internal ArchiveSession(Stream source, ZipArchive zip, Archive archive)
        {
            this.source = source ?? throw new ArgumentNullException(nameof(source));
            this.zip = zip ?? throw new ArgumentNullException(nameof(zip));
            this.Archive = archive ?? throw new ArgumentNullException(nameof(archive));
        }

        /// <summary>
        /// Gets the logical archive contents parsed from the <c>.kpar</c> descriptors.
        /// </summary>
        public Archive Archive { get; }

        /// <summary>
        /// Opens a model entry stream by metadata index key (for example <c>"Base"</c>).
        /// </summary>
        /// <param name="indexKey">The key in <c>.meta.json</c> <c>index</c>.</param>
        /// <returns>A readable stream for the model file mapped by <paramref name="indexKey"/>.</returns>
        /// <exception cref="ArgumentException">Thrown when <paramref name="indexKey"/> is null or empty.</exception>
        /// <exception cref="InvalidOperationException">Thrown when metadata index is not available.</exception>
        /// <exception cref="System.Collections.Generic.KeyNotFoundException">Thrown when <paramref name="indexKey"/> is not present.</exception>
        /// <exception cref="FileNotFoundException">Thrown when the mapped path does not exist in the archive.</exception>
        public Stream OpenModel(string indexKey)
        {
            var modelEntry = this.Archive.GetModelEntryByIndexKey(indexKey);
            
            return this.OpenEntry(modelEntry.Path);
        }
        
        /// <summary>
        /// Opens an entry stream by archive-relative path.
        /// </summary>
        /// <param name="archivePath">
        /// The archive-relative path of the entry to open (forward slashes preferred).
        /// </param>
        /// <returns>
        /// A readable stream for the requested entry.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// Thrown when <paramref name="archivePath"/> is null or empty.
        /// </exception>
        /// <exception cref="FileNotFoundException">
        /// Thrown when the specified entry does not exist in the archive.
        /// </exception>
        internal Stream OpenEntry(string archivePath)
        {
            if (string.IsNullOrWhiteSpace(archivePath))
            {
                throw new ArgumentException("Archive path shall not be null or empty.", nameof(archivePath));
            }

            var normalized = archivePath.NormalizeZipPath();

            var entry = this.zip.GetEntry(normalized);
            
            if (entry is null)
            {
                throw new FileNotFoundException($"kpar entry '{normalized}' not found.", normalized);
            }

            return entry.Open();
        }

        /// <inheritdoc />
        public void Dispose()
        {
            this.zip.Dispose();
            this.source.Dispose();
        }

        /// <inheritdoc />
        public ValueTask DisposeAsync()
        {
            this.zip.Dispose();
            return this.source.DisposeAsync();
        }
    }
}
