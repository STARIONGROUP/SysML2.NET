// -------------------------------------------------------------------------------------------------
// <copyright file="ArchiveExtensions.cs" company="Starion Group S.A.">
//
//   Copyright 2022-2026 Starion Group S.A.
//
//   Licensed under the Apache License, Version 2.0 (the "License");
//   you may not use this file except in compliance with the License.
//   You may obtain a copy of the License at
//
//       http://www.apache.org/licenses/LICENSE-2.0
//
//   Unless required by applicable law or agreed to in writing, software
//   distributed under the License is distributed on an "AS IS" BASIS,
//   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//   See the License for the specific language governing permissions and
//   limitations under the License.
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.Extensions.ModelInterchange
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    
    using SysML2.NET.ModelInterchange;
    using SysML2.NET.Extensions.Utilities;

    /// <summary>
    /// Provides convenience methods for working with <see cref="Archive"/> instances.
    /// </summary>
    public static class ArchiveExtensions
    {
        /// <summary>
        /// Tries to resolve a <see cref="ModelEntry"/> by metadata index key.
        /// </summary>
        /// <param name="archive">The <see cref="Archive"/> to query.</param>
        /// <param name="indexKey">
        /// The key in <see cref="InterchangeProjectMetadata.Index"/> that identifies a model file
        /// (for example <c>"Base"</c> maps to <c>"Base.kerml"</c>).
        /// </param>
        /// <param name="entry">
        /// When this method returns, contains the resolved <see cref="ModelEntry"/> if found;
        /// otherwise, <see langword="null"/>.
        /// </param>
        /// <returns>
        /// <see langword="true"/> if an entry is resolved; otherwise, <see langword="false"/>.
        /// </returns>
        public static bool TryGetModelEntryByIndexKey(this Archive archive, string indexKey, out ModelEntry entry)
        {
            if (archive == null)
            {
                throw new ArgumentNullException(nameof(archive));
            }

            if (string.IsNullOrWhiteSpace(indexKey))
            {
                throw new ArgumentException("The index key shall not be null or empty.", nameof(indexKey));
            }
            
            entry = null;

            var map = archive.Metadata?.Index;
            if (map is null) return false;

            if (!map.TryGetValue(indexKey, out var path) || string.IsNullOrWhiteSpace(path))
            {
                return false;
            }

            var normalized = path.NormalizeZipPath();

            entry = archive.Models?.FirstOrDefault(modelEntry =>
                string.Equals(modelEntry.Path.NormalizeZipPath(), normalized, StringComparison.Ordinal));

            return entry is not null;
        }

        /// <summary>
        /// Resolves a <see cref="ModelEntry"/> by metadata index key.
        /// </summary>
        /// <param name="archive">The <see cref="Archive"/> to query.</param>
        /// <param name="indexKey">The metadata index key identifying the model file.</param>
        /// <returns>The resolved <see cref="ModelEntry"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="archive"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="indexKey"/> is <see langword="null"/> or empty.</exception>
        /// <exception cref="InvalidOperationException">
        /// Thrown when the archive does not contain metadata index information.
        /// </exception>
        /// <exception cref="KeyNotFoundException">
        /// Thrown when the metadata index does not contain <paramref name="indexKey"/>.
        /// </exception>
        /// <exception cref="FileNotFoundException">
        /// Thrown when the metadata index points to a path that is not present in <see cref="Archive.Models"/>.
        /// </exception>
        public static ModelEntry GetModelEntryByIndexKey(this Archive archive, string indexKey)
        {
            if (archive is null) throw new ArgumentNullException(nameof(archive));
            
            if (string.IsNullOrWhiteSpace(indexKey))
            {
                throw new ArgumentException("The index key shall not be null or empty.", nameof(indexKey));
            }

            var map = archive.Metadata?.Index
                      ?? throw new InvalidOperationException("Archive metadata index is not available.");

            if (!map.TryGetValue(indexKey, out var path) || string.IsNullOrWhiteSpace(path))
            {
                throw new KeyNotFoundException($"No index entry found for key '{indexKey}'.");
            }

            var normalized = path.NormalizeZipPath();

            var entry = archive.Models?.FirstOrDefault(m =>
                string.Equals(m.Path.NormalizeZipPath(), normalized, StringComparison.Ordinal));

            if (entry is null)
            {
                throw new FileNotFoundException(
                    $"Model entry for index key '{indexKey}' points to missing path '{normalized}'.",
                    normalized);
            }

            return entry;
        }
        
        /// <summary>
        /// Opens the model content stream associated with the specified metadata index key.
        /// </summary>
        /// <param name="archive">The <see cref="Archive"/> to query.</param>
        /// <param name="indexKey">The metadata index key identifying the model file.</param>
        /// <param name="cancellationToken">The cancellation token used to cancel the operation.</param>
        /// <returns>
        /// A task that returns a readable stream for the model content.
        /// </returns>
        public static async Task<Stream> OpenModelByIndexKeyAsync(this Archive archive, string indexKey, CancellationToken cancellationToken = default)
        {
            var entry = archive.GetModelEntryByIndexKey(indexKey);
            return await entry.OpenReadAsync(cancellationToken);
        }
    }
}
