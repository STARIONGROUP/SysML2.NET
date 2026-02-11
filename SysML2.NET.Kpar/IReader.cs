// -------------------------------------------------------------------------------------------------
// <copyright file="IReader.cs" company="Starion Group S.A.">
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
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;
    
    using SysML2.NET.ModelInterchange;

    /// <summary>
    /// Reads KerML Project Archives (<c>.kpar</c>) from a file path or stream.
    /// </summary>
    /// <remarks>
    /// A reader is expected to:
    /// <list type="bullet">
    /// <item><description>Open the ZIP container.</description></item>
    /// <item><description>Locate and parse <c>.project.json</c> and <c>.meta.json</c> at archive root.</description></item>
    /// <item><description>Expose model interchange files (which may reside in subfolders) by their archive-relative paths.</description></item>
    /// </list>
    /// </remarks>
    public interface IReader
    {
        /// <summary>
        /// Reads a <c>.kpar</c> file from a file path.
        /// </summary>
        /// <param name="filePath">Absolute or relative path to the archive.</param>
        /// <param name="options">Optional read options.</param>
        /// <returns>A parsed <see cref="Archive"/>.</returns>
        Archive Read(string filePath, ReadOptions options = null);

        /// <summary>
        /// Reads a <c>.kpar</c> from an input stream.
        /// </summary>
        /// <param name="source">The stream containing the ZIP archive.</param>
        /// <param name="options">Optional read options.</param>
        /// <returns>A parsed <see cref="Archive"/>.</returns>
        Archive Read(Stream source, ReadOptions options = null);
        
        /// <summary>
        /// Reads a <c>.kpar</c> file from a file path asynchronously.
        /// </summary>
        /// <param name="filePath">Absolute or relative path to the archive.</param>
        /// <param name="options">Optional read options.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A parsed <see cref="Archive"/>.</returns>
        Task<Archive> ReadAsync(string filePath, ReadOptions options = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// Reads a <c>.kpar</c> from an input stream asynchronously.
        /// </summary>
        /// <param name="source">The stream containing the ZIP archive.</param>
        /// <param name="options">Optional read options.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A parsed <see cref="Archive"/>.</returns>
        Task<Archive> ReadAsync(Stream source, ReadOptions options = null, CancellationToken cancellationToken = default);
        
        /// <summary>
        /// Opens a <c>.kpar</c> file and returns an <see cref="ArchiveSession"/> that keeps the underlying
        /// ZIP container open for on-demand access to model and entry streams.
        /// </summary>
        /// <param name="filePath">
        /// The absolute or relative file system path to the <c>.kpar</c> archive.
        /// </param>
        /// <param name="options">
        /// Optional <see cref="ReadOptions"/> controlling descriptor validation,
        /// index validation, and other read-time behavior.
        /// </param>
        /// <returns>
        /// An <see cref="ArchiveSession"/> containing the parsed <see cref="Archive"/>
        /// representation and providing methods to open model or entry streams on demand.
        /// The caller is responsible for disposing the session.
        /// </returns>
        ArchiveSession Open(string filePath, ReadOptions options = null);
        
        /// <summary>
        /// Opens a <c>.kpar</c> from an input stream and returns an <see cref="ArchiveSession"/> that keeps the underlying
        /// ZIP container open for on-demand content access.
        /// </summary>
        /// <param name="source">The stream containing the ZIP archive.</param>
        /// <param name="options">Optional read options.</param>
        /// <returns>
        /// An <see cref="ArchiveSession"/> containing the parsed <see cref="Archive"/> and providing
        /// methods to open entry/model streams. The caller must dispose the session.
        /// </returns>
        ArchiveSession Open(Stream source, ReadOptions options = null);

        /// <summary>
        /// Asynchronously opens a <c>.kpar</c> file and returns an <see cref="ArchiveSession"/> that keeps the underlying
        /// ZIP container open for on-demand content access.
        /// </summary>
        /// <param name="filePath">Absolute or relative path to the archive.</param>
        /// <param name="options">Optional read options.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>
        /// A task that returns an <see cref="ArchiveSession"/>. The caller must dispose the session.
        /// </returns>
        Task<ArchiveSession> OpenAsync(string filePath, ReadOptions options = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// Asynchronously opens a <c>.kpar</c> from an input stream and returns an <see cref="ArchiveSession"/> that keeps the underlying
        /// ZIP container open for on-demand content access.
        /// </summary>
        /// <param name="source">The stream containing the ZIP archive.</param>
        /// <param name="options">Optional read options.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>
        /// A task that returns an <see cref="ArchiveSession"/>. The caller must dispose the session.
        /// </returns>
        Task<ArchiveSession> OpenAsync(Stream source, ReadOptions options = null, CancellationToken cancellationToken = default);
    }
}
