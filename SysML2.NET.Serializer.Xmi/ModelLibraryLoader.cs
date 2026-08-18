// -------------------------------------------------------------------------------------------------
// <copyright file="ModelLibraryLoader.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.Serializer.Xmi
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Logging.Abstractions;

    using SysML2.NET.Core.POCO.Root.Namespaces;

    /// <summary>
    /// Loads model libraries by recursively deserializing every KerML and SysML interchange file beneath a
    /// directory.
    /// </summary>
    /// <remarks>
    /// Both the root Namespace of each file AND the Namespaces it referenced are collected, so libraries
    /// reachable only as a dependency of another are indexed too. A file that fails to deserialize is logged
    /// and skipped rather than aborting the load, since one malformed library must not make every semantic
    /// constraint unresolvable.
    /// </remarks>
    public class ModelLibraryLoader : IModelLibraryLoader
    {
        /// <summary>
        /// The search patterns identifying a model-library file.
        /// </summary>
        private static readonly string[] LibrarySearchPatterns = ["*.kermlx", "*.sysmlx"];

        /// <summary>
        /// The factory used to create loggers for the deserializer.
        /// </summary>
        private readonly ILoggerFactory loggerFactory;

        /// <summary>
        /// The logger used to report skipped files.
        /// </summary>
        private readonly ILogger<ModelLibraryLoader> logger;

        /// <summary>
        /// The service resolving references between library files.
        /// </summary>
        private readonly IExternalReferenceService externalReferenceService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ModelLibraryLoader" /> class.
        /// </summary>
        /// <param name="loggerFactory">The injected factory used to set up logging.</param>
        /// <param name="externalReferenceService">
        /// The service resolving <c>href</c> references between library files; optional.
        /// </param>
        public ModelLibraryLoader(ILoggerFactory loggerFactory, IExternalReferenceService externalReferenceService = null)
        {
            this.loggerFactory = loggerFactory ?? NullLoggerFactory.Instance;
            this.logger = this.loggerFactory.CreateLogger<ModelLibraryLoader>();
            this.externalReferenceService = externalReferenceService;
        }

        /// <summary>
        /// Loads every model library found beneath a directory.
        /// </summary>
        /// <param name="libraryDirectory">The root directory to search recursively.</param>
        /// <returns>The distinct root Namespaces of the loaded libraries.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="libraryDirectory" /> is null.</exception>
        /// <exception cref="DirectoryNotFoundException">Thrown when the directory does not exist.</exception>
        public IReadOnlyCollection<INamespace> Load(string libraryDirectory)
        {
            var deSerializer = new DeSerializer(this.loggerFactory, this.externalReferenceService);
            var namespaces = new List<INamespace>();

            foreach (var libraryFile in QueryLibraryFiles(libraryDirectory))
            {
                try
                {
                    Collect(deSerializer.DeSerialize(new Uri(libraryFile)), namespaces);
                }
                catch (Exception exception)
                {
                    this.logger.LogWarning(exception, "The model library {LibraryFile} could not be loaded and was skipped.", libraryFile);
                }
            }

            return Distinct(namespaces);
        }

        /// <summary>
        /// Asynchronously loads every model library found beneath a directory.
        /// </summary>
        /// <param name="libraryDirectory">The root directory to search recursively.</param>
        /// <param name="cancellationToken">The token used to cancel the load.</param>
        /// <returns>The distinct root Namespaces of the loaded libraries.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="libraryDirectory" /> is null.</exception>
        /// <exception cref="DirectoryNotFoundException">Thrown when the directory does not exist.</exception>
        public async Task<IReadOnlyCollection<INamespace>> LoadAsync(string libraryDirectory, CancellationToken cancellationToken = default)
        {
            var deSerializer = new DeSerializer(this.loggerFactory, this.externalReferenceService);
            var namespaces = new List<INamespace>();

            foreach (var libraryFile in QueryLibraryFiles(libraryDirectory))
            {
                cancellationToken.ThrowIfCancellationRequested();

                try
                {
                    Collect(await deSerializer.DeSerializeAsync(new Uri(libraryFile), cancellationToken), namespaces);
                }
                catch (Exception exception) when (exception is not OperationCanceledException)
                {
                    this.logger.LogWarning(exception, "The model library {LibraryFile} could not be loaded and was skipped.", libraryFile);
                }
            }

            return Distinct(namespaces);
        }

        /// <summary>
        /// Returns the model-library files beneath a directory, in a stable order.
        /// </summary>
        /// <param name="libraryDirectory">The root directory to search recursively.</param>
        /// <returns>The absolute paths of the library files.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="libraryDirectory" /> is null.</exception>
        /// <exception cref="DirectoryNotFoundException">Thrown when the directory does not exist.</exception>
        private static IReadOnlyList<string> QueryLibraryFiles(string libraryDirectory)
        {
            if (libraryDirectory == null)
            {
                throw new ArgumentNullException(nameof(libraryDirectory));
            }

            if (!Directory.Exists(libraryDirectory))
            {
                throw new DirectoryNotFoundException($"The model-library directory '{libraryDirectory}' does not exist.");
            }

            return [..LibrarySearchPatterns
                .SelectMany(pattern => Directory.EnumerateFiles(libraryDirectory, pattern, SearchOption.AllDirectories))
                .OrderBy(libraryFile => libraryFile, StringComparer.Ordinal)];
        }

        /// <summary>
        /// Removes duplicate Namespaces, keeping first-seen order.
        /// </summary>
        /// <param name="namespaces">The collected Namespaces.</param>
        /// <returns>The distinct Namespaces.</returns>
        private static IReadOnlyCollection<INamespace> Distinct(List<INamespace> namespaces) => [..namespaces.Distinct()];

        /// <summary>
        /// Adds the root and referenced Namespaces of one read result to the accumulator.
        /// </summary>
        /// <param name="readResult">The result of deserializing one library file.</param>
        /// <param name="namespaces">The accumulator.</param>
        private static void Collect(XmiReadResult readResult, List<INamespace> namespaces)
        {
            if (readResult.RootNamespace != null)
            {
                namespaces.Add(readResult.RootNamespace);
            }

            namespaces.AddRange(readResult.ReferencedNamespaces.Where(referenced => referenced != null));
        }
    }
}
