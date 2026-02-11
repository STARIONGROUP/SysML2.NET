// -------------------------------------------------------------------------------------------------
// <copyright file="Reader.cs" company="Starion Group S.A.">
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
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.IO.Compression;
    using System.Linq;
    using System.Text.Json;
    using System.Threading;
    using System.Threading.Tasks;
    
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Logging.Abstractions;
    
    using SysML2.NET.Kpar.Cryptography;
    using SysML2.NET.Extensions.Utilities;
    using SysML2.NET.ModelInterchange;
    using SysML2.NET.Serializer.Json.ModelInterchange;

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
    public sealed class Reader : IReader
    {
        /// <summary>
        /// The file name of the project descriptor located at the root of a <c>.kpar</c> archive.
        /// </summary>
        /// <remarks>
        /// The <c>.project.json</c> descriptor defines the logical project identity,
        /// versioning, and structural metadata of the KerML archive.
        /// </remarks>
        private const string ProjectDescriptorFileName = ".project.json";
    
        /// <summary>
        /// The file name of the metadata descriptor located at the root of a <c>.kpar</c> archive.
        /// </summary>
        /// <remarks>
        /// The <c>.meta.json</c> descriptor provides supplementary archive metadata,
        /// including the model index and optional checksum information.
        /// </remarks>
        private const string MetadataDescriptorFileName = ".meta.json";

        /// <summary>
        /// The default <see cref="JsonReaderOptions"/> used when parsing descriptor files.
        /// </summary>
        /// <remarks>
        /// The reader:
        /// <list type="bullet">
        /// <item><description>Skips JSON comments.</description></item>
        /// <item><description>Allows trailing commas.</description></item>
        /// </list>
        /// These relaxed options improve interoperability with hand-authored or
        /// tool-generated descriptor files while still enforcing structural correctness.
        /// </remarks>
        private static readonly JsonReaderOptions DefaultReaderOptions = new()
        {
            CommentHandling = JsonCommentHandling.Skip,
            AllowTrailingCommas = true
        };

        /// <summary>
        /// The injected logger
        /// </summary>
        private readonly ILogger<Reader> logger;

        /// <summary>
        /// The injected <see cref="IChecksumService"/>
        /// </summary>
        private readonly IChecksumService checksumService;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="Reader"/> class.
        /// </summary>
        /// <param name ="checksumService">
        /// The (injected) <see cref="IChecksumService"/>
        /// </param>
        /// <param name="logger">
        /// The injected logger
        /// </param>
        public Reader(IChecksumService checksumService, ILogger<Reader> logger = null)
        {
            this.checksumService = checksumService;
            this.logger = logger ?? NullLogger<Reader>.Instance;
        }
        
        /// <summary>
        /// Gets the path to the Kpar file that has been read
        /// </summary>
        public string Path { get; internal set; }
        
        /// <summary>
        /// Reads a KerML project archive from a file path.
        /// </summary>
        /// <param name="filePath">
        /// The path to the <c>.kpar</c> file.
        /// </param>
        /// <param name="options">
        /// Optional read options.</param>
        /// <returns>The parsed <see cref="Archive"/>.
        /// </returns>
        public Archive Read(string filePath, ReadOptions options = null)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                throw new ArgumentException("The path to the kpar file shall not be null or empty", nameof(filePath));
            }

            var sw = Stopwatch.StartNew();
            
            this.logger.LogDebug("starting to read kpar at {Path}", filePath);
            
            options ??= new ReadOptions();
            using var fileStream = File.OpenRead(filePath);
            
            var archive = this.Read(fileStream, options);

            archive.Path = filePath;
            
            this.Path = filePath;
            
            this.logger.LogDebug("kpar at {Path} read in {ElapsedMilliseconds} [ms]", filePath, sw.ElapsedMilliseconds);

            return archive;
        }

        /// <summary>
        /// Reads a KerML project archive from a stream.
        /// </summary>
        /// <param name="source">
        /// The stream containing the <c>.kpar</c> archive.
        /// </param>
        /// <param name="options">
        /// Optional read options.
        /// </param>
        /// <returns>
        /// The parsed <see cref="Archive"/>.
        /// </returns>
        public Archive Read(Stream source, ReadOptions options = null)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source), "The source stream shall not be null");
            }
            
            var sw = Stopwatch.StartNew();
            
            this.logger.LogDebug("starting to read kpar");
            
            options ??= new ReadOptions();

            using var zip = OpenZip(source);
            var archive = ReadFromZip(zip, options);
            
            this.logger.LogDebug("kpar read in {ElapsedMilliseconds} [ms]", sw.ElapsedMilliseconds);

            return archive;
        }
        
        /// <summary>
        /// Reads a KerML project archive from a file path asynchronously.
        /// </summary>
        /// <param name="filePath">
        /// The path to the <c>.kpar</c> file.
        /// </param>
        /// <param name="options">
        /// Optional read options.
        /// </param>
        /// <param name="cancellationToken">
        /// The Cancellation token used to cancel the operation
        /// </param>
        /// <returns>The parsed <see cref="Archive"/>.</returns>
        public async Task<Archive> ReadAsync(string filePath, ReadOptions options = null, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                throw new ArgumentException("The path to the kpar file shall not be null or empty", nameof(filePath));
            }

            var sw = Stopwatch.StartNew();
            
            this.logger.LogDebug("starting to read kpar at {Path}", filePath);
            
            options ??= new ReadOptions();
            
            await using var fs = File.OpenRead(filePath);
            
            var archive =  await this.ReadAsync(fs, options, cancellationToken).ConfigureAwait(false);
            
            archive.Path = filePath;
            
            this.Path = filePath;
            
            this.logger.LogDebug("kpar at {Path} read in {ElapsedMilliseconds} [ms]", filePath, sw.ElapsedMilliseconds);

            return archive;
        }


        /// <summary>
        /// Reads a KerML project archive from a stream asynchronously.
        /// </summary>
        /// <param name="source">
        /// The stream containing the <c>.kpar</c> archive.
        /// </param>
        /// <param name="options">Optional read options.
        /// </param>
        /// <param name="cancellationToken">
        /// The Cancellation token used to cancel the operation
        /// </param>
        /// <returns>
        /// The parsed <see cref="Archive"/>.
        /// </returns>
        public async Task<Archive> ReadAsync(Stream source, ReadOptions options = null, CancellationToken cancellationToken = default)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source), "The source stream shall not be null");
            }
            
            var sw = Stopwatch.StartNew();
            
            this.logger.LogDebug("starting to read kpar");
            
            options ??= new ReadOptions();

            using var zip = OpenZip(source);
            
            var archive = await ReadFromZipAsync(zip, options, cancellationToken).ConfigureAwait(false);

            this.logger.LogDebug("kpar read in {ElapsedMilliseconds} [ms]", sw.ElapsedMilliseconds);

            return archive;
        }

        /// <summary>
        /// Opens a <c>.kpar</c> file and returns an <see cref="ArchiveSession"/> that keeps the underlying
        /// <c>.kpar</c> container open for on-demand access to model and entry streams.
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
        public ArchiveSession Open(string filePath, ReadOptions options = null)
        {
            if (string.IsNullOrWhiteSpace(filePath))
            {
                throw new ArgumentException("The path to the kpar file shall not be null or empty", nameof(filePath));
            }

            options ??= new ReadOptions();

            var fs = File.OpenRead(filePath);

            try
            {
                var archiveSession = this.Open(fs, options);

                archiveSession.Archive.Path = filePath;

                WireModelEntryOpeners(archiveSession);
                
                return archiveSession;
            }
            catch
            {
                fs.Dispose();
                throw;
            }
        }
        
        /// <summary>
        /// Opens a <c>.kpar</c> from an input stream and returns an <see cref="ArchiveSession"/> that keeps the underlying
        /// <c>.kpar</c> container open for on-demand content access.
        /// </summary>
        /// <param name="source">The stream containing the ZIP archive.</param>
        /// <param name="options">Optional read options.</param>
        /// <returns>
        /// An <see cref="ArchiveSession"/> containing the parsed <see cref="Archive"/> and providing
        /// methods to open entry/model streams. The caller must dispose the session.
        /// </returns>
        public ArchiveSession Open(Stream source, ReadOptions options = null)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source), "The source stream shall not be null");
            }

            options ??= new ReadOptions();

            var zip = new ZipArchive(source, ZipArchiveMode.Read);

            try
            {
                var archive = ReadFromZip(zip, options);

                var archiveSession = new ArchiveSession(source, zip, archive);

                WireModelEntryOpeners(archiveSession);
                
                return archiveSession;
            }
            catch
            {
                zip.Dispose();
                source.Dispose();
                throw;
            }
        }
        
        /// <summary>
        /// Asynchronously opens a <c>.kpar</c> file and returns an <see cref="ArchiveSession"/> that keeps the underlying
        /// <c>.kpar</c> container open for on-demand content access.
        /// </summary>
        /// <param name="filePath">Absolute or relative path to the archive.</param>
        /// <param name="options">Optional read options.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>
        /// A task that returns an <see cref="ArchiveSession"/>. The caller must dispose the session.
        /// </returns>
        public async Task<ArchiveSession> OpenAsync(string filePath, ReadOptions options = null, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(filePath))
            {
                throw new ArgumentException("The path to the kpar file shall not be null or empty", nameof(filePath));
            }

            cancellationToken.ThrowIfCancellationRequested();
            options ??= new ReadOptions();

            var fs = File.OpenRead(filePath);

            try
            {
                var archiveSession = await this.OpenAsync(fs, options, cancellationToken).ConfigureAwait(false);

                archiveSession.Archive.Path = filePath;

                WireModelEntryOpeners(archiveSession);
                
                return archiveSession;
            }
            catch
            {
                await fs.DisposeAsync().ConfigureAwait(false);
                throw;
            }
        }

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
        public async Task<ArchiveSession> OpenAsync(Stream source, ReadOptions options = null, CancellationToken cancellationToken = default)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source), "The source stream shall not be null");
            }

            cancellationToken.ThrowIfCancellationRequested();
            options ??= new ReadOptions();

            var zip = new ZipArchive(source, ZipArchiveMode.Read);

            try
            {
                var archive = await ReadFromZipAsync(zip, options, cancellationToken).ConfigureAwait(false);

                var archiveSession = new ArchiveSession(source, zip, archive);

                WireModelEntryOpeners(archiveSession);
                
                return archiveSession;
            }
            catch
            {
                zip.Dispose();

                await source.DisposeAsync().ConfigureAwait(false);

                throw;
            }
        }

        /// <summary>
        /// Reads an <see cref="Archive"/> from the specified <see cref="ZipArchive"/>
        /// using the provided <see cref="ReadOptions"/>.
        /// </summary>
        /// <param name="zip">
        /// The <see cref="ZipArchive"/> containing the archive data to read.
        /// </param>
        /// <param name="options">
        /// The <see cref="ReadOptions"/> that control how the archive contents
        /// are interpreted, filtered, and materialized.
        /// </param>
        /// <returns>
        /// An <see cref="Archive"/> instance representing the logical contents
        /// of the ZIP archive.
        /// </returns>
        private Archive ReadFromZip(ZipArchive zip, ReadOptions options)
        {
            var projectEntry = FindDescriptorEntryAtRoot(zip, ProjectDescriptorFileName, options);
            var metaEntry = FindDescriptorEntryAtRoot(zip, MetadataDescriptorFileName, options);

            var interchangeProject = ReadJsonEntry(projectEntry, static (ref Utf8JsonReader r) => InterchangeProjectDeSerializer.DeSerialize(ref r));

            var interchangeProjectMetadata = ReadJsonEntry(metaEntry, static (ref Utf8JsonReader r) => InterchangeProjectMetadataDeSerializer.DeSerialize(ref r));

            var checksumMismatches = this.ValidateChecksums(zip, interchangeProjectMetadata, options);
            
            var modelEntries = BuildModelEntries(zip, interchangeProjectMetadata, options);

            return new Archive
            {
                Project = interchangeProject,
                Metadata = interchangeProjectMetadata,
                Models = modelEntries,
                ChecksumMismatches = checksumMismatches
            };
        }

        /// <summary>
        /// Reads and builds the <see cref="Archive"/> from a ZIP container (asynchronous path).
        /// </summary>
        /// <param name="zip">The <see cref="ZipArchive"/> that is to be read</param>
        /// <param name="options">The <see cref="ReadOptions"/> used to read</param>
        /// <param name="cancellationToken">The Cancellation token used to cancel the operation</param>
        private async Task<Archive> ReadFromZipAsync(ZipArchive zip, ReadOptions options, CancellationToken cancellationToken)
        {
            var projectEntry = FindDescriptorEntryAtRoot(zip, ProjectDescriptorFileName, options);
            var metaEntry = FindDescriptorEntryAtRoot(zip, MetadataDescriptorFileName, options);

            var project = await ReadJsonEntryAsync(projectEntry, static (ref Utf8JsonReader utf8JsonReader) => InterchangeProjectDeSerializer.DeSerialize(ref utf8JsonReader),
                    cancellationToken)
                .ConfigureAwait(false);

            var meta = await ReadJsonEntryAsync(metaEntry, static (ref Utf8JsonReader utf8JsonReader) => InterchangeProjectMetadataDeSerializer.DeSerialize(ref utf8JsonReader),
                    cancellationToken)
                .ConfigureAwait(false);

            var checksumMismatches = await this.ValidateChecksumsAsync(zip, meta, options, cancellationToken)
                .ConfigureAwait(false);
            
            var models = BuildModelEntries(zip, meta, options);

            return new Archive
            {
                Project = project,
                Metadata = meta,
                Models = models,
                ChecksumMismatches = checksumMismatches
            };
        }

        /// <summary>
        /// Opens a ZIP archive in read mode.
        /// </summary>
        /// <param name="source">
        /// The <see cref="Stream"/> to read from
        /// </param>
        private static ZipArchive OpenZip(Stream source)
        {
            return new ZipArchive(source, ZipArchiveMode.Read);
        }

        /// <summary>
        /// Finds a descriptor entry with the given file name at archive root.
        /// </summary>
        /// <param name="zip">The ZIP archive to search.</param>
        /// <param name="descriptorFileName">The descriptor file name (e.g. <c>.project.json</c>).</param>
        /// <param name="options">Read options controlling validation behavior.</param>
        /// <returns>The matching <see cref="ZipArchiveEntry"/>, or <see langword="null"/> if not found and validation is disabled.</returns>
        /// <exception cref="InvalidDataException">
        /// Thrown when descriptor validation is enabled and the descriptor is missing or duplicated.
        /// </exception>
        private static ZipArchiveEntry FindDescriptorEntryAtRoot(ZipArchive zip, string descriptorFileName, ReadOptions options)
        {
            ZipArchiveEntry result = null;

            foreach (var entry in zip.Entries)
            {
                if (!IsRootEntry(entry))
                {
                    continue;
                }

                if (string.Equals(entry.Name, descriptorFileName, StringComparison.OrdinalIgnoreCase))
                {
                    if (result != null && options?.ValidateRequiredDescriptors == true)
                    {
                        throw new InvalidDataException($"Multiple {descriptorFileName} files found at archive root.");
                    }

                    result = entry;
                }
            }

            if (result == null && options?.ValidateRequiredDescriptors == true)
            {
                throw new InvalidDataException($"{descriptorFileName} descriptor not found at archive root.");
            }

            return result;
        }

        /// <summary>
        /// Determines whether the specified <see cref="ZipArchiveEntry"/> represents
        /// a file located at the root level of the archive (i.e., not contained
        /// within a subdirectory and not a directory entry).
        /// </summary>
        /// <param name="entry">
        /// The <see cref="ZipArchiveEntry"/> to evaluate.
        /// </param>
        /// <returns>
        /// <see langword="true"/> if the entry is a non-directory file located
        /// at the archive root; otherwise, <see langword="false"/>.
        /// </returns>
        private static bool IsRootEntry(ZipArchiveEntry entry)
        {
            if (string.IsNullOrEmpty(entry.Name))
            {
                return false; 
            }

            return entry.FullName.IndexOf('/') < 0;
        }

        /// <summary>
        /// Builds the set of <see cref="ModelEntry"/> instances contained in the specified
        /// <see cref="ZipArchive"/>, using the metadata index when available.
        /// </summary>
        /// <param name="zip">
        /// The <see cref="ZipArchive"/> containing the model interchange entries.
        /// </param>
        /// <param name="meta">
        /// The <see cref="InterchangeProjectMetadata"/> describing the archive contents,
        /// including any index of model entries, if present.
        /// </param>
        /// <param name="options">
        /// The <see cref="ReadOptions"/> controlling how entries are discovered and filtered.
        /// </param>
        /// <returns>
        /// An array of <see cref="ModelEntry"/> instances representing the model files
        /// included in the archive.
        /// </returns>
        private static ModelEntry[] BuildModelEntries(ZipArchive zip, InterchangeProjectMetadata meta, ReadOptions options)
        {
            var map = meta.Index;

            var entryByPath = zip.Entries
                .Where(e => !e.FullName.EndsWith("/", StringComparison.Ordinal))
                .ToDictionary(e => e.FullName.NormalizeZipPath(), e => e, StringComparer.Ordinal);

            entryByPath.RemoveWhereKeyEndsWith(ProjectDescriptorFileName);
            entryByPath.RemoveWhereKeyEndsWith(MetadataDescriptorFileName);

            var result = new List<ModelEntry>(map.Count > 0 ? map.Count : entryByPath.Count);

            if (map.Count > 0)
            {
                foreach (var kvp in map)
                {
                    var modelPath = kvp.Value.NormalizeZipPath();

                    if (options.ValidateIndexPaths && !entryByPath.TryGetValue(modelPath, out _))
                    {
                        throw new InvalidDataException($"Metadata index entry '{kvp.Key}' points to missing archive path '{modelPath}'.");
                    }

                    if (!entryByPath.ContainsKey(modelPath))
                    {
                        continue;
                    }

                    result.Add(CreateModelEntry(zip, modelPath));
                }

                return result.ToArray();
            }
            
            foreach (var path in entryByPath.Keys.OrderBy(p => p, StringComparer.Ordinal))
            {
                result.Add(CreateModelEntry(zip, path));
            }

            return result.ToArray();
        }

        /// <summary>
        /// Creates a <see cref="ModelEntry"/> instance for the specified
        /// normalized ZIP entry path within the given <see cref="ZipArchive"/>.
        /// </summary>
        /// <param name="zip">
        /// The <see cref="ZipArchive"/> containing the entry from which the
        /// <see cref="ModelEntry"/> is created.
        /// </param>
        /// <param name="normalizedPath">
        /// The normalized ZIP entry path identifying the archive entry.
        /// The path is expected to use forward slashes (<c>'/'</c>) as directory
        /// separators and must not contain a leading <c>"./"</c> segment.
        /// </param>
        /// <returns>
        /// A <see cref="ModelEntry"/> representing the archive entry located at
        /// <paramref name="normalizedPath"/>.
        /// </returns>
        private static ModelEntry CreateModelEntry(ZipArchive zip, string normalizedPath)
        {
            var contentType = QueryContentType(normalizedPath);

            return new ModelEntry
            {
                Path = normalizedPath,
                ContentType = contentType,

                OpenReadAsync = async (_) =>
                {
                    var entry = zip.GetEntry(normalizedPath);
                    if (entry is null)
                    {
                        throw new FileNotFoundException($"kpar entry '{normalizedPath}' not found.");
                    }

                    var s = entry.Open();
                    return await Task.FromResult(s).ConfigureAwait(false);
                }
            };
        }

        /// <summary>
        /// Determines the MIME content type associated with the file extension
        /// of the specified path.
        /// </summary>
        /// <param name="path">
        /// The file path or file name whose extension is used to resolve
        /// the corresponding MIME content type.
        /// </param>
        /// <returns>
        /// A string representing the MIME content type (for example,
        /// <c>application/json</c> or <c>application/zip</c>) associated
        /// with the file extension of <paramref name="path"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="path"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown if <paramref name="path"/> is empty or does not contain
        /// a valid file name or extension, depending on the implementation.
        /// </exception>
        private static string QueryContentType(string path)
        {
            if (path.EndsWith(".json", StringComparison.OrdinalIgnoreCase)) return "application/json";
            if (path.EndsWith(".xml", StringComparison.OrdinalIgnoreCase)) return "application/xml";
            if (path.EndsWith(".kerml", StringComparison.OrdinalIgnoreCase)) return "text/plain";
            return null;
        }

        /// <summary>
        /// Reads a JSON entry from the specified <see cref="ZipArchive"/> and parses
        /// its UTF-8 payload using the supplied low-level parser delegate.
        /// </summary>
        /// <typeparam name="T">
        /// The type of object produced by the <paramref name="parser"/>.
        /// </typeparam>
        /// <param name="entry">
        /// The <see cref="ZipArchiveEntry"/> representing the JSON file to read and parse.
        /// </param>
        /// <param name="parser">
        /// A delegate that parses the JSON payload using a ref-based
        /// <see cref="Utf8JsonReader"/> and materializes an instance of
        /// <typeparamref name="T"/>.
        /// </param>
        /// <returns>
        /// An instance of <typeparamref name="T"/> parsed from the JSON content
        /// of the specified ZIP entry.
        /// </returns>
        private static T ReadJsonEntry<T>(ZipArchiveEntry entry, FuncRefReader<T> parser)
        {
            using var stream = entry.Open();
            var bytes = stream.ReadAllBytes();
            return ParseJsonBytes(bytes, parser);
        }

        /// <summary>
        /// Asynchronously reads a JSON entry from the specified <see cref="ZipArchive"/>
        /// and parses its UTF-8 payload using the supplied low-level parser delegate.
        /// </summary>
        /// <typeparam name="T">
        /// The type of object produced by the <paramref name="parser"/>.
        /// </typeparam>
        /// <param name="entry">
        /// The <see cref="ZipArchiveEntry"/> representing the JSON file to read and parse.
        /// </param>
        /// <param name="parser">
        /// A delegate that parses the JSON payload using a ref-based
        /// <see cref="Utf8JsonReader"/> and materializes an instance of
        /// <typeparamref name="T"/>.
        /// </param>
        /// <param name="ct">
        /// A <see cref="CancellationToken"/> used to cancel the asynchronous
        /// read operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous operation. The task result contains
        /// the parsed instance of <typeparamref name="T"/>.
        /// </returns>
        private static async Task<T> ReadJsonEntryAsync<T>(ZipArchiveEntry entry, FuncRefReader<T> parser, CancellationToken ct)
        {
            await using var stream = entry.Open();
            var bytes = await stream.ReadAllBytesAsync(ct).ConfigureAwait(false);
            return ParseJsonBytes(bytes, parser);
        }

        /// <summary>
        /// Parses a JSON payload from the provided UTF-8 encoded byte span using
        /// a low-level, ref-based <see cref="Utf8JsonReader"/> parser delegate.
        /// </summary>
        /// <typeparam name="T">
        /// The type of object produced by the supplied <paramref name="parser"/>.
        /// </typeparam>
        /// <param name="bytes">
        /// A <see cref="ReadOnlySpan{T}"/> of UTF-8 encoded JSON bytes representing
        /// a single JSON value.
        /// </param>
        /// <param name="parser">
        /// A delegate that receives a <see cref="Utf8JsonReader"/> (passed by reference)
        /// positioned at the beginning of the JSON payload and is responsible for
        /// parsing and materializing an instance of <typeparamref name="T"/>.
        /// </param>
        /// <returns>
        /// An instance of <typeparamref name="T"/> produced by the
        /// <paramref name="parser"/> from the supplied JSON payload.
        /// </returns>
        /// <remarks>
        /// This method creates a <see cref="Utf8JsonReader"/> over the provided byte span
        /// without additional allocations. The reader is forward-only and must be
        /// fully consumed by the <paramref name="parser"/> implementation according
        /// to the parsing contract.
        /// 
        /// The input is expected to contain valid UTF-8 encoded JSON. Validation and
        /// structural correctness are enforced by <see cref="Utf8JsonReader"/>.
        /// </remarks>
        private static T ParseJsonBytes<T>(ReadOnlySpan<byte> bytes, FuncRefReader<T> parser)
        {
            var reader = new Utf8JsonReader(bytes, DefaultReaderOptions);

            if (!reader.Read())
            {
                throw new JsonException("Unexpected end of JSON payload.");
            }

            return parser(ref reader);
        }

        /// <summary>
        /// Wires <see cref="ModelEntry.OpenReadAsync"/> delegates for all <see cref="ModelEntry"/> instances
        /// in the specified <see cref="ArchiveSession"/> so that entry content streams can be opened on demand.
        /// </summary>
        /// <param name="session">
        /// The active <see cref="ArchiveSession"/> that owns the underlying <see cref="System.IO.Compression.ZipArchive"/>
        /// and backing <see cref="System.IO.Stream"/>.
        /// </param>
        private static void WireModelEntryOpeners(ArchiveSession session)
        {
            if (session?.Archive?.Models is null) return;

            foreach (var model in session.Archive.Models)
            {
                if (model is null) continue;

                var path = model.Path;

                model.OpenReadAsync = ct =>
                {
                    ct.ThrowIfCancellationRequested();
                    
                    var s = session.OpenEntry(path);
                    return new ValueTask<Stream>(Task.FromResult<Stream>(s));
                };
            }
        }
        
        /// <summary>
        /// Validates model file checksums declared in the archive metadata against the actual
        /// contents of the provided <see cref="ZipArchive"/>.
        /// </summary>
        /// <param name="zip">
        /// The open <see cref="ZipArchive"/> representing the underlying <c>.kpar</c> container.
        /// The archive must remain valid and readable for the duration of validation.
        /// </param>
        /// <param name="meta">
        /// The parsed <see cref="InterchangeProjectMetadata"/> instance originating from
        /// <c>.meta.json</c>. This metadata may contain a checksum map describing expected
        /// hash values for model entries.
        /// </param>
        /// <param name="options">
        /// The <see cref="ReadOptions"/> controlling checksum validation behavior.
        /// Validation is performed only when <see cref="ReadOptions.ValidateChecksums"/> is
        /// set to <see langword="true"/>.
        /// The handling of detected mismatches is governed by
        /// <see cref="ReadOptions.ChecksumFailureBehavior"/>.
        /// </param>
        /// <returns>
        /// A read-only collection of <see cref="ChecksumMismatch"/> instances representing
        /// detected checksum inconsistencies.
        /// <para>
        /// If checksum validation is disabled or the metadata does not declare any checksums,
        /// an empty collection is returned.
        /// </para>
        /// </returns>
        private IReadOnlyList<ChecksumMismatch> ValidateChecksums(ZipArchive zip, InterchangeProjectMetadata meta, ReadOptions options)
        {
            if (options?.ValidateChecksums != true) return Array.Empty<ChecksumMismatch>();

            var checksums = meta?.Checksum;
            if (checksums is null || checksums.Count == 0) return Array.Empty<ChecksumMismatch>();

            var behavior = options.ChecksumFailureBehavior;

            var mismatches = this.checksumService.Validate(zip, meta, behavior);

            HydrateMismatchIndexKeys(meta, mismatches);

            return mismatches;
        }

        /// <summary>
        /// Asynchronously validates all model file checksums declared in the specified
        /// <see cref="InterchangeProjectMetadata"/> against the actual contents of the
        /// provided <see cref="ZipArchive"/>.
        /// </summary>
        /// <param name="zip">
        /// The open <see cref="ZipArchive"/> representing the <c>.kpar</c> container.
        /// The archive must remain open for the duration of the validation process.
        /// </param>
        /// <param name="meta">
        /// The parsed <see cref="InterchangeProjectMetadata"/> containing the
        /// <c>checksum</c> declarations from <c>.meta.json</c>.
        /// </param>
        /// <param name="options">
        /// The <see cref="ReadOptions"/> controlling checksum validation behavior.
        /// This must be non-null. The <see cref="ReadOptions.ValidateChecksums"/> flag
        /// determines whether validation is enabled, and
        /// <see cref="ReadOptions.ChecksumFailureBehavior"/> determines how mismatches
        /// are handled.
        /// </param>
        /// <param name="ct">
        /// A <see cref="CancellationToken"/> that can be used to cancel the asynchronous
        /// validation operation.
        /// </param>
        /// <returns>
        /// A task representing the asynchronous validation operation. The task result
        /// contains a read-only list of detected <see cref="ChecksumMismatch"/> instances.
        /// If no mismatches are detected, an empty list is returned.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="zip"/>, <paramref name="meta"/>, or
        /// <paramref name="options"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="FileNotFoundException">
        /// Thrown if a checksum declaration refers to a ZIP entry that does not exist
        /// within the archive.
        /// </exception>
        /// <exception cref="ChecksumValidationException">
        /// Thrown when one or more checksum mismatches are detected and
        /// <see cref="ReadOptions.ChecksumFailureBehavior"/> is set to
        /// <see cref="ChecksumFailureBehavior.Throw"/>.
        /// </exception>
        private async Task<IReadOnlyList<ChecksumMismatch>> ValidateChecksumsAsync(ZipArchive zip, InterchangeProjectMetadata meta, ReadOptions options, CancellationToken ct)
        {
            if (options?.ValidateChecksums != true) return Array.Empty<ChecksumMismatch>();

            var checksums = meta?.Checksum;
            if (checksums is null || checksums.Count == 0) return Array.Empty<ChecksumMismatch>();

            var behavior = options.ChecksumFailureBehavior;

            var mismatches = await this.checksumService.ValidateAsync(zip, meta, behavior, ct).ConfigureAwait(false);

            HydrateMismatchIndexKeys(meta, mismatches);

            return mismatches;
        }

        /// <summary>
        /// Enriches the specified collection of <see cref="ChecksumMismatch"/> instances
        /// with metadata index keys derived from the provided
        /// <see cref="InterchangeProjectMetadata"/>.
        /// </summary>
        /// <param name="meta">
        /// The <see cref="InterchangeProjectMetadata"/> containing the
        /// <c>index</c> map that associates logical model index keys
        /// (for example <c>"Base"</c>) with archive-relative paths.
        /// </param>
        /// <param name="mismatches">
        /// The collection of <see cref="ChecksumMismatch"/> instances
        /// produced during checksum validation.
        /// </param>
        private static void HydrateMismatchIndexKeys(InterchangeProjectMetadata meta, IReadOnlyList<ChecksumMismatch> mismatches)
        {
            if (mismatches is null || mismatches.Count == 0) return;

            var index = meta?.Index;
            if (index is null || index.Count == 0) return;

            var byPath = new Dictionary<string, string>(StringComparer.Ordinal);
            foreach (var kvp in index)
            {
                var p = kvp.Value?.NormalizeZipPath();
                if (string.IsNullOrWhiteSpace(p)) continue;

                if (!byPath.ContainsKey(p))
                {
                    byPath[p] = kvp.Key;
                }
            }

            foreach (var m in mismatches)
            {
                if (m is null) continue;

                if (!string.IsNullOrWhiteSpace(m.Path) && byPath.TryGetValue(m.Path.NormalizeZipPath(), out var key))
                {
                    m.IndexKey = key;
                }
            }
        }
        
        /// <summary>
        /// Represents a delegate that parses a JSON value using a forward-only,
        /// ref-based <see cref="Utf8JsonReader"/>.
        /// </summary>
        /// <typeparam name="T">
        /// The type of the object produced from the JSON input.
        /// </typeparam>
        /// <param name="reader">
        /// A reference to the <see cref="Utf8JsonReader"/> positioned at the beginning
        /// of the JSON value to be parsed. The reader is passed by reference because it is a
        /// <c>ref struct</c> and because parsing advances its internal state.
        /// </param>
        /// <returns>
        /// An instance of <typeparamref name="T"/> created from the JSON value read
        /// from the provided <paramref name="reader"/>.
        /// </returns>
        /// <remarks>
        /// Implementations are expected to fully consume the JSON value they parse,
        /// leaving the <paramref name="reader"/> positioned on the final token of that value.
        /// The caller remains responsible for advancing the reader beyond that token,
        /// if required.
        /// </remarks>
        private delegate T FuncRefReader<out T>(ref Utf8JsonReader reader);
    }
}
