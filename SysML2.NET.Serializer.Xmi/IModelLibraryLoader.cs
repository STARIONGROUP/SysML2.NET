// ------------------------------------------------------------------------------------------------
// <copyright file="IModelLibraryLoader.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.Serializer.Xmi
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    using SysML2.NET.Core.POCO.Root.Namespaces;

    /// <summary>
    /// Loads a complete set of model libraries from disk, independently of any user model.
    /// </summary>
    /// <remarks>
    /// Deserializing a user model yields only the libraries that model transitively references. The KerML
    /// 8.4.2 semantic constraints need the whole library set regardless — every Class must specialize
    /// <c>Occurrences::Occurrence</c> and every Feature <c>Base::things</c>, whether or not the model
    /// mentions them — so a model-independent load is required to resolve them.
    /// </remarks>
    public interface IModelLibraryLoader
    {
        /// <summary>
        /// Loads every model library found beneath a directory.
        /// </summary>
        /// <param name="libraryDirectory">The root directory to search recursively.</param>
        /// <returns>The distinct root Namespaces of the loaded libraries.</returns>
        /// <exception cref="System.ArgumentNullException">Thrown when <paramref name="libraryDirectory" /> is null.</exception>
        /// <exception cref="System.IO.DirectoryNotFoundException">Thrown when the directory does not exist.</exception>
        IReadOnlyCollection<INamespace> Load(string libraryDirectory);

        /// <summary>
        /// Asynchronously loads every model library found beneath a directory.
        /// </summary>
        /// <param name="libraryDirectory">The root directory to search recursively.</param>
        /// <param name="cancellationToken">The token used to cancel the load.</param>
        /// <returns>The distinct root Namespaces of the loaded libraries.</returns>
        /// <exception cref="System.ArgumentNullException">Thrown when <paramref name="libraryDirectory" /> is null.</exception>
        /// <exception cref="System.IO.DirectoryNotFoundException">Thrown when the directory does not exist.</exception>
        Task<IReadOnlyCollection<INamespace>> LoadAsync(string libraryDirectory, CancellationToken cancellationToken = default);
    }
}
