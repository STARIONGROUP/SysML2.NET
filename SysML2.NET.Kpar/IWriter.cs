// -------------------------------------------------------------------------------------------------
// <copyright file="IWriter.cs" company="Starion Group S.A.">
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
    /// Writes KerML Project Archives (<c>.kpar</c>) to a file path or stream.
    /// </summary>
    /// <remarks>
    /// A writer is expected to:
    /// <list type="bullet">
    /// <item><description>Create a ZIP archive.</description></item>
    /// <item><description>Write exactly one <c>.project.json</c> and one <c>.meta.json</c> at archive root.</description></item>
    /// <item><description>Write one model interchange file per root namespace, with stable archive-relative paths.</description></item>
    /// </list>
    /// </remarks>
    public interface IWriter
    {
        /// <summary>
        /// Writes a <see cref="Archive"/> to a file path.
        /// </summary>
        /// <param name="filePath">Destination path (typically ending in <c>.kpar</c>).</param>
        /// <param name="archive">The archive to write.</param>
        /// <param name="options">Optional write options.</param>
        void Write(string filePath, Archive archive, WriteOptions options = null);

        /// <summary>
        /// Writes a <see cref="Archive"/> to an output stream.
        /// </summary>
        /// <param name="destination">Destination stream for the ZIP archive.</param>
        /// <param name="archive">The archive to write.</param>
        /// <param name="leaveOpen">If true, the writer does not dispose the stream.</param>
        /// <param name="options">Optional write options.</param>
        void Write(Stream destination, Archive archive, bool leaveOpen = false, WriteOptions options = null);

        /// <summary>
        /// Writes a <see cref="Archive"/> to a file path asynchronously.
        /// </summary>
        /// <param name="filePath">Destination path (typically ending in <c>.kpar</c>).</param>
        /// <param name="archive">The package to write.</param>
        /// <param name="options">Optional write options.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        Task WriteAsync(string filePath, Archive archive, WriteOptions options = null, CancellationToken cancellationToken = default);
        
        /// <summary>
        /// Writes a <see cref="Archive"/> to an output stream asynchronously.
        /// </summary>
        /// <param name="destination">Destination stream for the ZIP archive.</param>
        /// <param name="archive">The archive to write.</param>
        /// <param name="leaveOpen">If true, the writer does not dispose the stream.</param>
        /// <param name="options">Optional write options.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        Task WriteAsync(Stream destination, Archive archive, bool leaveOpen = false, WriteOptions options = null, CancellationToken cancellationToken = default);
    }
}
